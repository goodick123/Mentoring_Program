﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class spGetOrdersm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE GetOrders (@month INT, @status NVARCHAR(50), @year INT, @productId INT)
                        AS
                        BEGIN
                          SET NOCOUNT ON;

                          SELECT Id, Status, CreateDate, UpdateDate, ProductId
                          FROM Orders
                          WHERE (@month IS NULL OR MONTH(CreateDate) = MONTH(@month))
                            AND (@status IS NULL OR Status = @status)
                            AND (@year IS NULL OR YEAR(CreateDate) = @year)
                            AND (@productId IS NULL OR ProductId = @productId)
                        END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
