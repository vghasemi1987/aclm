create procedure dbo.uspUpdateAccessibilitiesByRouterId
@VersionDate dateTime,@RouterId int
as
	update Accessibilities
	set VersionDate = cast(@VersionDate as date) , RecordStatus = 1
	where RecordStatus = 0 and RouterId = @RouterId and IsTemp = 0