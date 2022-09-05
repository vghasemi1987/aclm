CREATE PROCEDURE dbo.uspAccessibilityDeleteTemp
    @UserId INT  
AS  
	DELETE  FROM Accessibilities
	WHERE IsTemp = 1 AND UserId = @UserId

