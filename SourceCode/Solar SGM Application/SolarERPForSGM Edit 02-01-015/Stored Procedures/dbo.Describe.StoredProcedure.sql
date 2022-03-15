USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Describe]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Describe] @tbname char(50)
AS
BEGIN
    select substring(sys1.name,1,45) as FieldName,substring(sys3.name,1,15) as Type,sys1.length as Width 
    from syscolumns sys1, sysobjects sys2,systypes sys3 
    where sys2.name=@tbname and sys1.id=sys2.id and sys3.xtype=sys1.xtype order by sys1.id
END

GO
