CREATE PROCEDURE DeleteOrders (@month INT, @status NVARCHAR(50), @year INT, @productId INT)
AS
BEGIN
  SET NOCOUNT ON;

  DELETE FROM Orders
  WHERE (@month IS NULL OR MONTH(CreateDate) = MONTH(@month))
    AND (@status IS NULL OR Status = @status)
    AND (@year IS NULL OR YEAR(CreateDate) = @year)
    AND (@productId IS NULL OR ProductId = @productId)
END