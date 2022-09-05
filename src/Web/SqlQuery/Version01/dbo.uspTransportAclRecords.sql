CREATE PROCEDURE dbo.uspTransportAclRecords 
    @ACLFilesUploadId INT  ,@UserId INT
AS  
    BEGIN  
       IF NOT EXISTS(SELECT * FROM Accessibilities WHERE AclFilesUploadId = @ACLFilesUploadId)
		 INSERT INTO Accessibilities  
                 (SourceIP ,  
                  SourcePort ,  
                  DestinationIP ,  
                  DestinationPort ,    
                  SourceSystemID ,   
                  DestinationSystemID ,  
                  Protocol ,  
                  ProtocolsId ,  
                  ActionType ,  
                  ActionTypesId ,   
                  RouterID ,   
                  ACLFilesUploadID ,  
                  IsTemp ,  
                  ServiceID ,    
                  [Port] ,
				  UserId ,
				  RecordStatus 
                )  
                SELECT dbo.ufn_GetAddress(R.SourceIP, R.SourceIP2) ,  
                        R.SourcePort ,  
                       dbo.ufn_GetAddress(R.DestinationIP, R.DestinationIP2) ,  
                        R.DestinationPort ,   
                        ( SELECT TOP 1  
                                    Id  
                          FROM      Systems 
                          WHERE     IpAddress = dbo.ufn_GetAddress(R.SourceIP, R.SourceIP2)  
                        ) ,  
                        ( SELECT TOP 1  
                                    Id  
                          FROM      Systems  
                          WHERE     IpAddress = dbo.ufn_GetAddress(R.DestinationIP,R.DestinationIP2)  
                        ) ,  
                        R.Protocol ,  
                        ( SELECT TOP 1  
                                    Id  
                          FROM      Protocols  
                          WHERE     Name = R.Protocol  
                        ) ,  
                        R.[Action] ,  
                        ( SELECT TOP 1  
                                    Id  
                          FROM      ActionTypes  
                          WHERE     Title = R.[Action]  
                        ) ,    
                        ( SELECT    Id  
                          FROM      Routers  
                          WHERE     AccessNo = R.RouterNo  
                        ) ,    
                        @ACLFilesUploadID ,  
                        1 ,  
                        ( SELECT TOP 1  
                                    Id  
                          FROM      [Services] srv  
                          WHERE     CAST(srv.[Port] AS NVARCHAR(50)) = R.SourcePort  
                        ) ,  
                        R.DestinationPort ,
						@UserId ,
						0
                FROM    ACLFilesRecords AS R  
                WHERE   AclFilesUploadId = @ACLFilesUploadId  AND R.RecordStatus <> 1    
    END
  
  
  
  