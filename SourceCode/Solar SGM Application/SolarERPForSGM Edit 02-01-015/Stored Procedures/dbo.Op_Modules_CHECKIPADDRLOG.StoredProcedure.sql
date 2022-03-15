USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Modules_CHECKIPADDRLOG]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Op_Modules_CHECKIPADDRLOG] 
@mIPAddress		Varchar(50)	=Null
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT a.*,b.UserName FROM ERPUserAuthentication  a, ERPUserHeader b WHERE a.FKUserName = b.PKID And a.IPAddress = @mIPAddress And a.IsActive='1'
END


GO
