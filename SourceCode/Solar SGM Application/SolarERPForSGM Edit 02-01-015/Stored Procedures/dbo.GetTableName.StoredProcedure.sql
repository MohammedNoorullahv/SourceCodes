USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[GetTableName]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTableName]

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
SELECT ''' + t.name + ''' AS TableName  
FROM AHGroup_SSPL.dbo.' +  QUOTENAME(t.name) + ' 
WHERE ID not in (select Distinct ID From AHGroup_SSPLAudit.dbo.'+QUOTENAME(t.name)+')' 
--SELECT s.name, t.name, c.name 
FROM sys.tables t 
JOIN sys.schemas s 
ON t.schema_id = s.schema_id 
JOIN sys.columns c 
ON t.object_id = c.object_id 
WHERE c.name = @ColName and t.name not in ('AutoSerial_Gen','tbl_m_ScreenTemplate','tbl_m_APShortCuts','tbl_m_AliasTransferTrace','Stages','ScreenHelpText','ReportsParameters','Reports','Messages','ImagePaths','Functions','DecexpressHelp','Constant','ColumnHelp','Columns','ColumnRelationTable')
order by t.name

 



SET @Sql = N'Select * from (' + @Sql + ') as AA  ORDER BY TableName';
--SET @Sql = @Sql + N' 
--ORDER BY TableName;'; 
PRINT @Sql; 

EXEC dbo.sp_executesql @Sql, @Params, @Date = @Date; 
END

GO
