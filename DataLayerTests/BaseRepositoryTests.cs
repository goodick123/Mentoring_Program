using System.Data;
using System.Data.SqlClient;
using ADO.NET.DataLayer.Enums;
using ADO.NET.DataLayer.Models;
using ADO.NET.DataLayer.Repositories;
using Xunit;
using Moq;

namespace DataLayerTests
{
    public class BaseRepositoryTests
    {
        [Fact]
        public void ExecuteConnectedQueryWithParams_ShouldOpenConnectionAndExecuteQueryWithParams()
        {
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.Setup(c => c.Close());

            var commandMock = new Mock<IDbCommand>();
            connectionMock.Setup(c => c.CreateCommand()).Returns(commandMock.Object);

            commandMock.Setup(c => c.ExecuteNonQuery()).Verifiable();
            commandMock.Setup(c => c.Connection).Returns(connectionMock.Object);

            var baseRepo = new BaseRepository<Order>(connectionMock.Object);
            baseRepo.ExecuteConnectedQueryWithParams("123", new Dictionary<string, object>());

            connectionMock.Verify(c => c.Open(), Times.Once);
            commandMock.Verify(c => c.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void ExecuteConnectedQueryWithParams_ShouldOpenConnectionAndExecuteQueryWithParams_ParsedDictionary()
        {
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.Setup(c => c.Close());

            var commandMock = new Mock<IDbCommand>();
            connectionMock.Setup(c => c.CreateCommand()).Returns(commandMock.Object);

            commandMock.Setup(c => c.ExecuteNonQuery()).Verifiable();
            commandMock.Setup(c => c.Connection).Returns(connectionMock.Object);
            commandMock.Setup(c => c.CreateParameter()).Returns(() => new SqlParameter());
            commandMock.Setup(c => c.Parameters).Returns(It.IsAny<IDataParameterCollection>());
            commandMock.Setup(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()));

            var baseRepo = new BaseRepository<Order>(connectionMock.Object);
            baseRepo.ExecuteConnectedQueryWithParams("INSERT INTO Table Value=@Value WHERE Id=@Id",
                new Dictionary<string, object>() { { "Id", 0 }, { "Value", 123 } });

            connectionMock.Verify(c => c.Open(), Times.Once);
            commandMock.Verify(c => c.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void ExecuteProcedureWithParams_ShouldOpenConnectionAndExecuteQueryWithParams_ParsedDictionary()
        {
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());
            connectionMock.Setup(c => c.Close());

            var commandMock = new Mock<IDbCommand>();
            connectionMock.Setup(c => c.CreateCommand()).Returns(commandMock.Object);

            commandMock.Setup(c => c.ExecuteNonQuery()).Verifiable();
            commandMock.Setup(c => c.Connection).Returns(connectionMock.Object);
            commandMock.Setup(c => c.CreateParameter()).Returns(() => new SqlParameter());
            commandMock.Setup(c => c.Parameters).Returns(It.IsAny<IDataParameterCollection>());
            commandMock.Setup(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()));

            var baseRepo = new BaseRepository<Order>(connectionMock.Object);
            baseRepo.ExecuteProcedureWithParams("CreateSmth",
                new Dictionary<string, object>() { { "Id", 0 }, { "Value", 123 } });

            connectionMock.Verify(c => c.Open(), Times.Once);
            commandMock.Verify(c => c.ExecuteNonQuery(), Times.Once);
        }

        [Fact]
        public void ShouldReturnCorrectResult()
        {
            var expectedOrders = new List<Order>
            {
                new Order { Id = 1, Status = Status.Done, CreateDate = new DateTime(2022, 1, 1), UpdateDate = new DateTime(2022, 1, 2), ProductId = 1 }
            };

            var parameters = new Dictionary<string, object>
            {
                { "CreateDate", new DateTime(2022, 1, 1) },
                { "UpdateDate", new DateTime(2022, 2, 2) }
            };

            var mockConnection = new Mock<IDbConnection>();
            var mockCommand = new Mock<IDbCommand>();
            var mockDataReader = new Mock<IDataReader>();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

            mockCommand.Setup(c => c.CommandText).Returns("TestProcedure");
            mockCommand.Setup(c => c.CommandType).Returns(CommandType.StoredProcedure);
            mockCommand.Setup(c => c.ExecuteReader()).Returns(mockDataReader.Object);
            mockCommand.Setup(c => c.CreateParameter()).Returns(() =>
            {
                var mockParameter = new Mock<IDbDataParameter>();
                mockParameter.Setup(p => p.ParameterName).Returns("@Id");
                mockParameter.Setup(p => p.Value).Returns(1);
                return mockParameter.Object;
            });
            mockCommand.Setup(c => c.Parameters).Returns(It.IsAny<IDataParameterCollection>());
            mockCommand.Setup(c => c.Parameters.Add(It.IsAny<IDbDataParameter>()));

            mockDataReader.Setup(r => r.Read()).Returns(() =>
            {
                int callCount = 0;
                return callCount++ < 1;
            });


            mockDataReader.Setup(r => r[It.IsAny<string>()]).Returns(
                (string name) =>
                {
                    switch (name)
                    {
                        case "Id": return expectedOrders[0].Id;
                        case "Status": return expectedOrders[0].Status;
                        case "CreateDate": return expectedOrders[0].CreateDate;
                        case "UpdateDate": return expectedOrders[0].UpdateDate;
                        case "ProductId": return expectedOrders[0].ProductId;
                        default: return null;
                    }
                }
            );

            var repository = new BaseRepository<Order>(mockConnection.Object);

            var result = repository.ConvertExecutedProcedureWithParamsToModel("TestProcedure", parameters);

            Assert.Equal(expectedOrders, result);
        }
    }
}
