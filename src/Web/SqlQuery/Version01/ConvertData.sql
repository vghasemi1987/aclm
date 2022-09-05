USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Routers] ON
INSERT INTO [dbo].[Routers]
           ([Id]
		   ,[RecordStatus]
           ,[Name]
           ,[AccessNo])
select RouterID,
case 
when [RecordStatus] is null then 0
else [RecordStatus]
end
           ,[Name]
           ,[AccessNo]


from BankSAM.RTR.Router
GO
SET IDENTITY_INSERT [dbo].[Routers] OFF
--------------------------------
SET IDENTITY_INSERT [dbo].[ServiceLevels] ON
GO
INSERT INTO [dbo].[ServiceLevels]
           ([Id],[RecordStatus]
           ,[Title])
select ServiceLevelID,0,Title From BankSAM.REC.ServiceLevel

SET IDENTITY_INSERT [dbo].[ServiceLevels] OFF
GO

---------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].ServiceGroups ON

INSERT INTO [dbo].[ServiceGroups]
           ([Id],[RecordStatus]
           ,[Name]
           ,[Description])
select [ServiceGroupID],
case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,
[Name]
,[Description]
from BankSAM.rec.ServiceGroup

SET IDENTITY_INSERT [dbo].ServiceGroups OFF
GO
--------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].Protocols ON
INSERT INTO [dbo].[Protocols]
           (Id,[RecordStatus]
           ,[Name]
           ,[Description])
     select ProtocolID,
case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,[Name]
           ,[Description]
from BankSAM.RTR.Protocol
SET IDENTITY_INSERT [dbo].Protocols OFF
GO
-----------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Positions] ON
INSERT INTO [dbo].[Positions]
           (Id,[RecordStatus]
           ,[Title]
           ,[Description])
    select PositionID, 
case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,Title
           ,[Description]
from BankSAM.USR.Position
SET IDENTITY_INSERT [dbo].[Positions] OFF
GO
-----------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Policies] ON
INSERT INTO [dbo].[Policies]
           (Id,[RecordStatus]
           ,[Title]
           ,[Description]
           ,[ProtocolId])
    select PolicyId, case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,[Title]
           ,[Description]
           ,[ProtocolId]
		   from BankSAM.REC.Policy
GO
SET IDENTITY_INSERT [dbo].[Policies] OFF
--------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT INTO [dbo].[Departments]
           (Id,[RecordStatus]
           ,[Name])
     select DepartmentID,case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,name from BankSAM.RTR.Department
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
---------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[AddressTypes] ON
INSERT INTO [dbo].[AddressTypes]
           (Id,[RecordStatus]
           ,[Title]
           ,[Description])
    select AddressTypeID,
case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,Title
           ,[Description]
from BankSAM.REC.AddressType
SET IDENTITY_INSERT [dbo].[AddressTypes] OFF
GO
-------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[ActionTypes] ON
INSERT INTO [dbo].[ActionTypes]
           (Id,[RecordStatus]
           ,[Title])
 select  ActionTypeID,  case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,Title
from BankSAM. RTR.ActionType
SET IDENTITY_INSERT [dbo].[ActionTypes] OFF
GO
---------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[AclFilesUpload] ON
INSERT INTO [dbo].[AclFilesUpload]
           (Id
		   ,[RecordStatus]
           ,[FileName]
           ,[Description]
           ,[RouterId])
     
           select  ACLFilesUploadID,  case 
when [RecordStatus] is null then 0
else [RecordStatus]
end ,[FileName]
           ,[Description]
           ,[RouterId]
		   from BankSAM.rec.ACLFilesUpload
SET IDENTITY_INSERT [dbo].[AclFilesUpload] OFF
GO
-----------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[AccessTypes] ON
INSERT INTO [dbo].[AccessTypes]
           (Id,[RecordStatus]
           ,[Title]
           ,[Description])
      select  AccessTypeID,  case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,[Title]
           ,[Description]
		   from BankSAM.REC.AccessType
SET IDENTITY_INSERT [dbo].[AccessTypes] OFF
GO
-------------------------------------------
SET IDENTITY_INSERT [dbo].[AccessibilityLevels] ON
INSERT INTO [dbo].[AccessibilityLevels]
           (Id,[RecordStatus]
           ,[Title])
      select  AccessibilityLevelID,  case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,[Title]
		   from BankSAM.REC.AccessibilityLevel
SET IDENTITY_INSERT [dbo].[AccessibilityLevels] OFF
GO
---------------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[AclFilesRecords] ON
INSERT INTO [dbo].[AclFilesRecords]
           (Id,[RecordStatus]
           ,[SourceIp]
           ,[DestinationIp]
           ,[Action]
           ,[SourceIp2]
           ,[DestinationIp2]
           ,[SourcePort]
           ,[DestinationPort]
           ,[Protocol]
           ,[RouterNo]
           ,[SourceAddressTypeId]
           ,[DestinationAddressTypeId]
           ,[AclFilesUploadId])
    
	select ACLFilesRecordID,
	case 
when [RecordStatus] is null then 0
else [RecordStatus]
end,[SourceIp]
           ,[DestinationIp]
           ,[Action]
           ,[SourceIp2]
           ,[DestinationIp2]
           ,[SourcePort]
           ,[DestinationPort]
           ,[Protocol]
           ,[RouterNo]
           ,[SourceAddressTypeId]
           ,[DestinationAddressTypeId]
           ,[ACLFilesUploadID] 
	from BankSAM.REC.ACLFilesRecord 
SET IDENTITY_INSERT [dbo].[AclFilesRecords] OFF
GO
---------------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Systems] ON
INSERT INTO [dbo].[Systems]
           (Id,[RecordStatus]
           ,[SystemName]
           ,[IpAddress]
           ,[Description]
           ,[AccessibilityLevelId]
           ,[InformationAccessibilityLevelId],
		   KindId)
select SystemID,
			case 
when [RecordStatus] is null then 0
else [RecordStatus]
end
           ,[SystemName]
           ,[IpAddress]
           ,[Description]
           ,[AccessibilityLevelId]
           ,[InformationAccessibilityLevelId]
		   ,1
		   from BankSAM.RTR.System
SET IDENTITY_INSERT [dbo].[Systems] OFF
GO


-------------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Services] ON
INSERT INTO [dbo].[Services]
           (Id,[RecordStatus]
           ,[Name]
           ,[Description]
           ,[Port]
           ,[ProtocolId]
           ,[ServiceLevelId])
   select 		ServiceID,	case 
when [RecordStatus] is null then 0
else [RecordStatus]
end
   ,[Name],
   	case 
when [Description] is null then '-'
else [Description]
end
           ,[Port]
           ,[ProtocolId]
           ,[ServiceLevelId]
		   from BankSAM.REC.Service
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
---------------------------------------------
USE [Sam]
GO
SET IDENTITY_INSERT [dbo].[Accessibilities] ON
INSERT INTO [dbo].[Accessibilities]
           (Id,[RecordStatus]
           ,[SourceIp]
           ,[DestinationIp]
           ,[SourcePort]
           ,[DestinationPort]
           ,[Port]
           ,[IsTemp]
           ,[Protocol]
           ,[ProtocolsId]
           ,[SourceSystemId]
           ,[DestinationSystemId]
           ,[ServiceId]
           ,[DestinationServiceId]
           ,[ActionType]
           ,[ActionTypesId]
           ,[RouterId]
           ,[UserId]
           ,[AclFilesUploadId])
   select AccessibilityID, case 
when [RecordStatus] is null then 0
else [RecordStatus]
end
           ,[SourceIp]
           ,[DestinationIp]
           ,[SourcePort]
           ,[DestinationPort]
           ,[Port]
           ,[IsTemp]
           ,[Protocol]
           ,[ProtocolId]
           ,[SourceSystemId]
           ,[DestinationSystemId]
           ,[ServiceId]
           ,null
           ,[ActionType]
           ,[ActionTypeId]
           ,[RoutersId]
           ,1
           ,[AclFilesUploadId]
		   FROM BankSAM.REC.Accessibility 
SET IDENTITY_INSERT [dbo].[Accessibilities] OFF
GO










