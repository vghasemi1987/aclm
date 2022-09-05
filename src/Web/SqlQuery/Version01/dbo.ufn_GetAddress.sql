CREATE FUNCTION [dbo].[ufn_GetAddress](
				@AddressType NVARCHAR(50),
				@Address NVARCHAR(50))  
RETURNS NVARCHAR(50)  
AS  
BEGIN  
  DECLARE @Result NVARCHAR(50)  
  
  IF(LOWER(@AddressType) = 'any')  
    SET @Result = 'Any'  
  ELSE IF(LOWER(@AddressType) = 'host')  
    SET @Result = @Address  
  ELSE   
    SET @Result = @AddressType + '/' + @Address  
  
  RETURN @Result  
END  