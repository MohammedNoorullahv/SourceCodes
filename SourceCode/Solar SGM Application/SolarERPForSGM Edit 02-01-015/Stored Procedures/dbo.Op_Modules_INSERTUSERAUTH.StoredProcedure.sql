USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Modules_INSERTUSERAUTH]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Drop Procedure Op_Modules_INSERTUSERAUTH


CREATE PROCEDURE [dbo].[Op_Modules_INSERTUSERAUTH] 
@mPKId			int		=Null,
@mUserName 		varchar(100)  	=Null,
@mIPAddress		Varchar(50)	=Null,
@mSystemName		Varchar(50)	=Null,
@mServer		Varchar(50)	=Null,
@mLoginTime		Varchar(50)	=Null,
@mLogoutTime		Varchar(50)	=Null,
@mIsActive 		Bit		=Null,
@mVersion		Varchar(100)	=Null
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ERPUserAuthentication VALUES (@mUserName,@mIPAddress,@mSystemName,@mServer,@mLoginTime,@mLogoutTime,@mIsActive,'',@mVersion)
END



GO
