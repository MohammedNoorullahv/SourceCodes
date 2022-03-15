USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Modules_UPDATEUSERAUTH]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Op_Modules_UPDATEUSERAUTH] 
@mUserName 		varchar(100)  	=Null,
@mLogoutTime		Varchar(50)	=Null,
@mIsActive 		Bit		=Null
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE 
	ERPUserAuthentication
	SET
	LogoutTime=@mLogoutTime, IsActive=@mIsActive
	WHERE 
	UserName=@mUserName And
	IsActive = '1'
END


GO
