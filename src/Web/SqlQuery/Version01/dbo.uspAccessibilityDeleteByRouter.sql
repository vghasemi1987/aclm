CREATE PROCEDURE dbo.uspAccessibilityDeleteByRouter
	@RouterId int
AS
	DELETE FROM Accessibilities
	WHERE RouterId = @RouterId