USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Modules_CHECKUSERALRDYLOG]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Op_Modules_CHECKUSERALRDYLOG] 
@mUserName 		varchar(100)  	=Null
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM ERPUserAuthentication WHERE UserName=@mUserName And IsActive='1'
END


GO
