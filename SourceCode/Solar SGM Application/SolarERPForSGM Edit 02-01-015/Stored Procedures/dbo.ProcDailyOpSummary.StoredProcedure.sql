USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[ProcDailyOpSummary]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[ProcDailyOpSummary](@dt date)
as
BEGIN
DECLARE 
@SummaryTable TABLE(
TableName nvarchar,
ModifiedBy nvarchar,
UpdateMode nvarchar,
RowModified int
);
declare 
@TablesCursor CURSOR,
@Params nvarchar = N'@Date date',
@tname nvarchar(255) = N'',
@sql nvarchar(MAX) = N'',
@sqlCheck nvarchar,
@vmodifiedby nvarchar(255) = N'',
@vupdatemode nvarchar(255) = N'',
@vcnt int = N'';
set @TablesCursor = CURSOR FOR select distinct t.name as TableName  FROM sys.tables t JOIN sys.schemas s ON t.schema_id = s.schema_id 
                  JOIN sys.columns c ON t.object_id = c.object_id 
         where EXISTS(SELECT 1 FROM sys.columns c WHERE c.[object_id] = t.OBJECT_ID    AND c.name = 'ModuleName')
                  and EXISTS(SELECT 1 from sys.columns c where c.object_id = t.object_id and c.name = 'UpdateMode')
        order by t.name;
        
 OPEN @TablesCursor
FETCH NEXT
FROM @TablesCursor INTO @tname
WHILE @@FETCH_STATUS = 0
BEGIN
set @sql = 'if exists (select * from ' + @tname + ') select  ''' + @tname  +''' as TableName, ModifiedBy,UpdateMode,Count(*)as Cnt into SummaryTable From '+  @tname + ' where Convert(Date,ModifiedDate,103) =  '''+ Convert(varchar(10),Convert(Date,@dt,103)) + ''' group by ModifiedBy , UpdateMode ';
print 'Query : ' + @sql
EXEC dbo.sp_executesql @sql;
select * from @SummaryTable;
FETCH NEXT
FROM @TablesCursor INTO @tname
 
END
 
CLOSE @TablesCursor
DEALLOCATE @TablesCursor

END

GO
