USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[GetRowCountByDate]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRowCountByDate]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @Sql nvarchar(MAX) = N'' 
, @Sql2 nvarchar(MAX) = N'' 
, @Sql3 nvarchar(MAX) = N'' 
, @Params nvarchar(MAX) = N'@Date date' 
, @Date date = getdate() 
, @ColName sysname = N'CreatedDate'; 

SELECT @Sql = @Sql + CASE WHEN LEN(@Sql) <> 0 THEN ' 
UNION ALL' ELSE '' END + ' 
SELECT ''' + t.name + ''' AS TableName , ''INSERTED'' as Type
, COUNT(*) as NoOfRecords FROM ' + QUOTENAME(s.name) +'.' + QUOTENAME(t.name) + ' 
WHERE CAST(' + QUOTENAME(c.name) + ' AS date) = @date' 
--SELECT s.name, t.name, c.name 
FROM sys.tables t 
JOIN sys.schemas s 
ON t.schema_id = s.schema_id 
JOIN sys.columns c 
ON t.object_id = c.object_id 
WHERE c.name = @ColName; 

Set @ColName='ModifiedDate';
SELECT @Sql = @Sql + CASE WHEN LEN(@Sql) <> 0 THEN ' 
UNION ALL' ELSE '' END + ' 
SELECT ''' + t.name + ''' AS TableName , ''MODIFIED'' as Type
, COUNT(*) as NoOfRecords FROM ' + QUOTENAME(s.name) +'.' + QUOTENAME(t.name) + ' 
WHERE CAST(' + QUOTENAME(c.name) + ' AS date) = @date ' 
--SELECT s.name, t.name, c.name 
FROM sys.tables t 
JOIN sys.schemas s 
ON t.schema_id = s.schema_id 
JOIN sys.columns c 
ON t.object_id = c.object_id 
WHERE c.name = @ColName; 


Set @ColName='ApprovedOn';
SELECT @Sql = @Sql + CASE WHEN LEN(@Sql) <> 0 THEN ' 
UNION ALL' ELSE '' END + ' 
SELECT ''' + t.name + ''' AS TableName , ''APPROVED'' as Type
, COUNT(*) as NoOfRecords FROM ' + QUOTENAME(s.name) +'.' + QUOTENAME(t.name) + ' 
WHERE CAST(' + QUOTENAME(c.name) + ' AS date) = @date ' 
--SELECT s.name, t.name, c.name 
FROM sys.tables t 
JOIN sys.schemas s 
ON t.schema_id = s.schema_id 
JOIN sys.columns c 
ON t.object_id = c.object_id 
WHERE c.name = @ColName; 

Set @ColName='IsApproved';

SELECT @Sql = @Sql + CASE WHEN LEN(@Sql) <> 0 THEN ' 
UNION ALL' ELSE '' END + ' 
SELECT ''' + t.name + ''' AS TableName , ''PENDING APPROVAL'' as Type
, COUNT(*) as NoOfRecords FROM ' + QUOTENAME(s.name) +'.' + QUOTENAME(t.name) + ' 
WHERE (' + QUOTENAME(c.name) + ' = 0 OR ' + QUOTENAME(c.name) + ' is null ) ' 
--SELECT s.name, t.name, c.name 
FROM sys.tables t 
JOIN sys.schemas s 
ON t.schema_id = s.schema_id 
JOIN sys.columns c 
ON t.object_id = c.object_id 
WHERE c.name = @ColName; 


SET @Sql = N'Select * from (' + @Sql + ') as AA WHERE  NoOfRecords > 0 ORDER BY TableName';
--SET @Sql = @Sql + N' 
--ORDER BY TableName;'; 
PRINT @Sql; 

EXEC dbo.sp_executesql @Sql, @Params, @Date = @Date; 
END

GO
