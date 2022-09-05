CREATE PROCEDURE dbo.uspAbundanceReport 
AS
select Group1.[Count],Group1.SuccessStatus,Group1.UserId,Group1.UserName,
		CAST(ROW_NUMBER() over(order by Group1.UserId) as int) Id
from	(select count(*) [Count],
		case IsSuccess when 1 then 'موفق'
					   else 'ناموفق'
		end SuccessStatus,
		S.UserId, U.UserName
	from SearchLogDetails S
	inner join ApplicationUserItems U on U.Id = S.UserId
	group by IsSuccess,UserId,U.UserName)Group1