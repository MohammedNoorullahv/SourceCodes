USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Login]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop proc Op_Login

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active


CREATE PROC [dbo].[Op_Login]
@mAction		varchar(20)	='SELALL',
@mPKId			int		=Null,
@mFKFirm		Int		=Null,
@mUserName		Varchar(50)	=Null,
@mFKDesignation		Int		=Null,
@mLoginName		Varchar(50)	=Null,
@mPassword		Varchar(50)	=Null,
@mUserType		Varchar(1)	=Null,
@mUserPhoto		Image		=Null,
@mIsDeleted		Varchar(1)	=Null,
@mCreatedBy		Bigint		=Null,
@mCreatedDt		Varchar(22)	=Null,
@mModifiedBy		Bigint		=Null,
@mModifiedDt		Varchar(22)	=Null,
@mDeletedBy		BigInt		=Null,
@mDeletedDt		Varchar(22)	=Null
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int


IF @mAction='SELALL' 
BEGIN
	--SELECT
	--a.PKID		As [nUserId],
	--a.FKFirm	As [nFKFirm],
	--b.FirmName	As [sFirmName],
	--d.Description	As [sUnitType],
	--a.UserName	As [sUserName],
	--a.FKDesignation	As [nFKDesigntion],
	--c.Description 	As [sDesignation],
	--a.LoginName	As [sLoginName],
	--a.Password	As [sPassword],
	--a.UserType	As [sUserType]

	--FROM
	--ERPUserHeader a,
	--ERPFirms b,
	--ERPLookUpMaster c,
	--ERPLookUpMaster d
	
	--WHERE
	--a.FKFirm = b.PKID And
	--a.FKDesignation = c.PKID And
	--b.FKUnitType = d.PKID And
	--a.LoginName = @mLoginName

	Select * from User_GroupDetails
	Where UserId = @mLoginName
	
END





GO
