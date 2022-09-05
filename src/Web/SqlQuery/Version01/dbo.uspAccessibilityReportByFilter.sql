CREATE PROCEDURE dbo.uspAccessibilityReportByFilter
    @SourceSystemId INT = NULL, 
	@DestinationSystemId INT = NULL ,

    @SourceIP NVARCHAR(15) = NULL ,  
	@DestinationIP NVARCHAR(15) = NULL ,    

	@ServiceId INT = NULL,
	@DestinationServiceId INT = NULL,

	@RequestingUserId INT = NULL ,  
	@ConfirmUserId INT = NULL ,  
    
    @UserDepartmentId INT = NULL ,  
    @RequestDepartmentId INT = NULL , 
		 
    @AccessibilityTypeId INT = NULL 
	 
	--@DestinationAccessLevelId INT = NULL , 
	--@SourceAccessLevelId INT = NULL 

    --@Illegal BIT = NULL ,  
    --@SenstiveService BIT = NULL ,  
    --@DestinationPortAccessLevelId INT = NULL  

AS
 BEGIN

 SELECT ROW_NUMBER() over(order by A.SourceIp) Id,
		A.SourceIp, srcSys.SystemName SourceSystemName, srcService.[Port] SourcePort,
		A.DestinationIp, desSys.SystemName DestinationSystemName, descService.[Port] DestinationPort,
		Ru.FirstName +' '+ Ru.LastName RequestingUserName,
		Cu.FirstName +' '+ Cu.LastName ConfirmerUserName,
		Rd.[Name] RequestDepartment,
		Ud.[Name] UserDepartment,
		Ac.Title AccessType
 FROM Accessibilities A
 LEFT JOIN AccessibilityRequests AR ON A.SourceIp = AR.SourceIp AND A.DestinationIp = AR.DestinationIp
 LEFT JOIN Systems srcSys on srcSys.Id = A.SourceSystemId
 LEFT JOIN Systems desSys on desSys.Id = A.DestinationSystemId
 LEFT JOIN [Services] srcService on srcService.Id = A.ServiceId 
 LEFT JOIN [Services] descService on descService.Id = A.ServiceId 
 LEFT JOIN ApplicationUserItems Ru on Ru.Id = AR.RequestingUserId
 LEFT JOIN ApplicationUserItems Cu on Cu.Id = AR.RequestingUserId
 LEFT JOIN Departments Rd on Rd.Id = AR.RequestDepartmentId
 LEFT JOIN Departments Ud on Rd.Id = AR.UserDepartmentId
 LEFT JOIN AccessTypes Ac on Ac.Id = AR.AccessibilityTypeId
 WHERE 
	(@SourceSystemId is NULL OR A.SourceSystemId = @SourceSystemId) AND
	(@DestinationSystemId IS NULL OR A.DestinationSystemId = @DestinationSystemId) AND
	(@SourceIP IS NULL OR A.SourceIp = @SourceIP) AND
	(@DestinationIP IS NULL OR A.DestinationIp = @DestinationIP) AND
	(@ServiceId IS NULL OR A.ServiceId = @ServiceId ) AND
	(@DestinationServiceId IS NULL OR A.DestinationServiceId = @DestinationServiceId ) AND
	(@RequestingUserId IS NULL OR AR.RequestingUserId = @RequestingUserId) AND
	(@ConfirmUserId IS NULL OR AR.ConfirmUserId = @ConfirmUserId) AND
	(@UserDepartmentId IS NULL OR AR.UserDepartmentId = @UserDepartmentId) AND
	(@RequestDepartmentId IS NULL OR AR.RequestDepartmentId = @RequestDepartmentId) AND
	(@AccessibilityTypeId IS NULL OR AR.AccessibilityTypeId = @AccessibilityTypeId) AND
	--(@DestinationAccessLevelId IS NULL OR AR.DestAccessibilityLevelId = @DestinationAccessLevelId) AND
	--(@SourceAccessLevelId IS NULL OR AR.SourceAccessibilityLevelId = @SourceAccessLevelId) AND
	 A.RecordStatus <> 1 AND AR.RecordStatus <> 1 
 END