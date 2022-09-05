Create PROCEDURE dbo.uspAccessibilityReportByCount  
 @Count INT 
 AS
	SELECT 
	      CAST(ROW_NUMBER() over(order by Group1.DestinationSystemName) as int) Id,
		  Cast(Count(*) as int) [Count],
	      GROUP1.DestinationIP DestinationIP,
		  Group1.DestinationSystemName DestinationSystemName--,
	FROM(
		 SELECT 
	 		A.DestinationIP,A.DestinationSystemId,A.ActionTypesId,
	 		A.ProtocolsId,A.ServiceId,A.DestinationServiceId,
			S.SystemName As DestinationSystemName
		 FROM Accessibilities A
		 LEFT JOIN Systems S on S.Id = A.DestinationSystemId
		 WHERE A.RecordStatus <>1 AND A.DestinationIP <> 'any'
		 GROUP BY A.DestinationIP,A.DestinationSystemId,A.ActionTypesId,
	 				A.ProtocolsId,A.ServiceId,A.DestinationServiceId,
					S.SystemName
		 )AS Group1
	GROUP BY GROUP1.DestinationIP,Group1.DestinationSystemName,Group1.DestinationSystemId
	Having COUNT(*) >= @Count
