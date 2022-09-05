CREATE PROCEDURE [dbo].[uspConfirmAccessibilities] @UserId INT   
AS  
UPDATE  Accessibilities  
SET     IsTemp = 0 
WHERE UserId = @UserId

  
  
  
  
  