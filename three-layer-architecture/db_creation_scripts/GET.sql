CREATE PROCEDURE [dbo].[Get]
	@selectID int	
AS
	SELECT ID, FirstName, LastName, Address, Phone, City from [dbo].[Clients]
	where ID = @selectID
Go