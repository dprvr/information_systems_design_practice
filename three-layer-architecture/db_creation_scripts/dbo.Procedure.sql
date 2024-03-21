CREATE PROCEDURE [dbo].[GetAll]	
AS
    Select ID, FirstName, LastName, Address, Phone, City from [dbo].[Clients]
Go
