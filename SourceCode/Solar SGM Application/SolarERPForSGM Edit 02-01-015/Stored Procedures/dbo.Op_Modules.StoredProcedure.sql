USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[Op_Modules]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop proc Op_Modules

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active
CREATE PROC [dbo].[Op_Modules]
@mAction		varchar(20)	='SELALL',
@mPKId			int		=Null,
@mFKUnit		Int		=Null,
@mStoreName		Varchar(30)	=Null,
@mFKUser		Int		=Null,
@mFKStore		Int		=Null,
@mIsDeleted		Varchar(1)	=Null,
@mCreatedBy		Bigint		=Null,
@mCreatedDt		Varchar(22)	=Null,
@mModifiedBy		Bigint		=Null,
@mModifiedDt		Varchar(22)	=Null,
@mDeletedBy		BigInt		=Null,
@mDeletedDt		Varchar(22)	=Null,
@mSeasonCode		Varchar(6)	=Null,
@mSeasonDesc		Varchar(20)	=Null,
@mFKLookUpHdr		Int		=Null,
@mFKUnitType		Int		=NUll,
@mDescription		Varchar(100)    =Null,
@mFunctions		varchar(10)	=Null,
@mFunctionsId		Int		=Null,
@mFirmName		Varchar(50)	=Null,
@mFKFirm 		int 		=NULL ,
@mUserName 		varchar(100)  	=Null,
@mFKDesignation 	int		= NULL ,
@mLoginName 		varchar(50)  	=Null,
@mPassword 		varchar(15)  	=Null,
@mUserType 		varchar(1) 	=Null ,
@mUserPhoto 		image 		=NULL ,
@mModifedDt		Varchar(11)	=Null,
@mFKMenu 		int 		=NULL ,
@mAllFunctions 		varchar(1) 	=Null,
@mAdding 		varchar(1)	=Null, 
@mEditing 		varchar(1) 	=Null,
@mDeleting 		varchar(1) 	=Null,
@mViewing 		varchar(1) 	=Null,
@mNoEntry 		varchar(1)  	=NULL,
@mIPAddress		Varchar(50)	=Null,
@mSystemName		Varchar(50)	=Null,
@mServer		Varchar(50)	=Null,
@mLoginTime		Varchar(50)	=Null,
@mLogoutTime		Varchar(50)	=Null,
@mIsActive 		Bit		=Null,
@mReason		Varchar(50)	=Null,
@mVersion		Varchar(100)	=Null
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

IF @mAction='SELSTORE1'  -- 4
BEGIN
	SELECT
	a.PKID				As [nStoreId],
	a.StoreName	As [StoresName]

	FROM
	ERPUnitStores a,
	ERPFirms b
	
	WHERE
	a.FKUnit = b.PKID 
END


IF @mAction='SELSEASON' 
BEGIN
	SELECT
	PKID				As [nFKSeason],
	SeasonCode			As [sSeasonCode],
	SeasonDesc			As [sSeasonDescription],
	StartDate			As [dStartDate],
	EndDate				As [dEndDate]

	FROM
	ERPSeason
	
	Order By
	PKID DESC
END

IF @mAction='SELCONVEYOUR' 
BEGIN
	SELECT
	PKID,	
	Description
	
	FROM
	ERPLookUpMaster

	Where
	FKLookUpHdr = '44' And
	IsDeleted <> 'Y'

	Order By
	Description,	PKID
END

--Select * from ERPLookUpMaster Order by PKID Desc  -- 44

IF @mAction='SELSTORE'  -- 1
BEGIN
	SELECT
	a.PKID							As [FKStore],
	b.FirmName +' : ' +a.StoreName + ' - ' +a.Code1 	As StoresName

	FROM
	ERPUNITSTORES A,
	ERPFIRMS B

	WHERE
	 a.FKUnit = b.PKID
	
	Order By
	a.PKID

END


IF @mAction='SELCURRENCY' 
BEGIN
	SELECT
	PKID,	
	Currency + ' - ' + ISOCode As Code
	
	FROM
	ERPCurrency

	Order By
	PKID
END

IF @mAction='SELSEASONCODE' 
BEGIN
	SELECT
	PKID				As [nFKSeason],
	SeasonCode			As [sSeasonCode],
	SeasonDesc			As [sSeasonDescription],
	StartDate			As [dStartDate],
	EndDate				As [dEndDate]

	FROM
	ERPSeason
	
	WHERE
	SeasonCode = @mSeasonCode

	Order By
	PKID DESC
END


IF @mAction='SELFIRMS' 
BEGIN
	SELECT
	PKID,	
	ShortName	As Factory
	
	FROM
	ERPFirms

	Order By
	ShortName
END

IF @mAction='SELORDERTO' 
BEGIN
	SELECT
	PKID,	
	ShortName	As Factory
	
	FROM
	ERPFirms

	Order By
	ShortName
END

IF @mAction='SELBULKORBUY' 
BEGIN
	SELECT
	PKID,	
	Description	As BulkorBuy
	
	FROM
	ERPLookUpMaster

	Where
	FKLookUpHdr = '43' And
	IsDeleted <> 'Y'

	Order By
	Description,	PKID
END

IF @mAction='SELMOS' 
BEGIN
	SELECT
	PKID,	
	Description	As ModeofShipment
	
	FROM
	ERPLookUpMaster

	Where
	FKLookUpHdr = '40' And
	IsDeleted = 'N'

	Order By
	PKID
END

IF @mAction='SELSHIPREF'
BEGIN
	SELECT
	PKID,
	Description	As Reference

	FROM
	ERPShipmentLookUpMst

	WHERE
	FKLookUpHdr = @mFKLookUpHdr

	ORDER BY
	IsDefault Desc,
	Description
	
	
END



IF @mAction='SELSTORES'  -- 2
BEGIN
	SELECT
	a.PKID				As [nStoreId],
	b.FirmName +' : ' +a.StoreName	As [StoresName]

	FROM
	ERPUnitStores a,
	ERPFirms b
	
	WHERE
	a.FKUnit = b.PKID And
	a.PKID In
	(Select FKStore From ERPUsersofStores Where FKUser = @mFKUser)
	
END

IF @mAction='SELSEASON' 
BEGIN
	SELECT
	PKID				As [nFKSeason],
	SeasonCode			As [sSeasonCode],
	SeasonDesc			As [sSeasonDescription],
	StartDate			As [dStartDate],
	EndDate				As [dEndDate]

	FROM
	ERPSeason
	
	Order By
	PKID DESC
END

IF @mAction='SELCURRENCY' 
BEGIN
	SELECT
	PKID,	
	Currency + ' - ' + ISOCode As Code
	
	FROM
	ERPCurrency

	Order By
	PKID
END

IF @mAction='SELSEASONCODE' 
BEGIN
	SELECT
	PKID				As [nFKSeason],
	SeasonCode			As [sSeasonCode],
	SeasonDesc			As [sSeasonDescription],
	StartDate			As [dStartDate],
	EndDate				As [dEndDate]

	FROM
	ERPSeason
	
	WHERE
	SeasonCode = @mSeasonCode

	Order By
	PKID DESC
END


IF @mAction='SELFIRMS' 
BEGIN
	SELECT
	PKID,	
	ShortName	As Factory
	
	FROM
	ERPFirms

	Order By
	ShortName
END

IF @mAction='SELMOS' 
BEGIN
	SELECT
	PKID,	
	Description	As ModeofShipment
	
	FROM
	ERPLookUpMaster

	Where
	FKLookUpHdr = '40' And
	IsDeleted = 'N'

	Order By
	PKID
END

IF @mAction='SELSHIPREF'
BEGIN
	SELECT
	PKID,
	Description	As Reference

	FROM
	ERPShipmentLookUpMst

	WHERE
	FKLookUpHdr = @mFKLookUpHdr

	ORDER BY
	IsDefault Desc,
	Description
	
END

--ADD ON 03-10-12--
IF @mAction='SELGRADES'
BEGIN
	SELECT
	PKID,
	Description As Grade

	FROM
	ERPLookUpMaster

	WHERE
	FKLookUpHdr = 41

	ORDER BY
	IsDefault Desc,
	Description	
END
--ADD ON 03-10-12--
IF @mAction='SELUNITNAME'
BEGIN
	select 
	a.FirmName,a.FKUnitType 
	from 
	ERPFirms a
END
IF @mAction='SELUNITTYPE'
BEGIN
	select 
	b.Description 
	from 
	ERPFirms a,ERPLookUpMaster b
	where 
	a.FKUnitType=b.PKID AND   b.PKID=@mPKID

END

IF @mAction='SELDESGINATION'
BEGIN
	select 
	Description,PKID 
	from 
	ERPLookUpMaster 
	where 
	FKLookUpHdr='1'
END
IF @mAction='LOADMENUITEM'
BEGIN
	SELECT 
	
	PKID,			[Functions] AS Module,			MenuHeader AS Menu,			MenuItem,
	'' AS CheckAll,		'' AS Adding, 				'' AS Editing, 				'' AS Deleting,
	 '' AS ViewOnly, 	'' AS NoEntry,				MenuOrder 
	FROM 
	
	ERPMenuMaster 
	
	WHERE 
	
	Functions <> 'ERP Main Screen' 
	
	ORDER BY
	 MenuOrder
END
IF @mAction='LOADMODULE1'
BEGIN
	select 
	Distinct Functions,FunctionsID 
	From 
	ERPMenuMaster 
	where PKID<>'1' 
END
IF @mAction='LOADMENUITEMS'
BEGIN
	select 
	MenuItem 
	From 
	ERPMenuMaster 
	where PKID<>'1'  AND FunctionsId=@mFunctionsId
END
IF @mAction='LOADMODULE'
BEGIN
	Select 
	Functions+'-'+MenuHeader+'-'+MenuItem as MenuName,PKID 
	from 
	ERPMenuMaster 
	where PKID<>'1'
	Order by 
	MenuOrder 
END
IF @mAction='CHECKLOGINNAME'
BEGIN
	select * from 
	ERPUserHeader 
	where 
	LoginName=@mLoginName AND IsDeleted='N'
END
IF @mAction='CHECKUNITNAMEPKID'
BEGIN
	select
	 PKID 
	from 
	ERPFirms
	 where 
	FirmName=@mFirmName
END

IF @mAction='INSERTUSERHEADER'
BEGIN

	SELECT @mPKID=IsNull(MAX(PKID),0)+1 FROM ERPUserHeader
	INSERT INTO
	ERPUserHeader
	VALUES (@mPKID,
	@mFKFirm,	@mUserName, 		@mFKDesignation ,	
	@mLoginName ,	@mPassword ,		@mUserType ,		
	@mIsDeleted ,	@mCreatedBy ,		
	@mCreatedDt ,	@mModifiedBy ,		@mModifedDt, 		
	@mDeletedBy ,	@mDeletedDt ,'0'	)	
	
END
IF @mAction='INSERTUSERDETAIL'
BEGIN
	SELECT @mPKID=IsNull(MAX(PKID),0)+1 FROM ERPUserDetail
	INSERT INTO
	ERPUserDetail
	VALUES (@mPKID,
	@mFKUser ,	@mFKMenu ,	@mAllFunctions 	,
	@mAdding , 	@mEditing ,	@mDeleting ,
	@mViewing ,	@mNoEntry) 	
END
IF @mAction='USERHEADERPKID'
BEGIN
	select PKID from 
	ERPUserHeader 
	where 
	UserName=@mUserName AND LoginName=@mLoginName --AND IsDeleted='N'
END

IF @mAction='CHECKSAMEUSER'
BEGIN
	Select Count(PKID) from ERPUserHeader where PKID=@mPKID
END
IF @mAction='CHECKSAMEMENU'
BEGIN
	select * from ERPUserDetail where FKUser=@mFKUser
END

IF @mAction='LOADMODULEDETAIL'
BEGIN

Select 
	Distinct Functions+'-'+a.MenuHeader+'-'+a.MenuItem as MenuNames,a.PKID as UserPKID
	from 
	ERPMenuMaster a
	where  NOT Exists(Select * from 
	ERPUserDetail b 
	where 
	a.PKID<>'1' 	AND 	b.FKMenu=a.PKID AND 	b.FKUser IN(@mFKUser) )
END



IF @mAction='LOADUSERHDR'	
BEGIN 
		
SELECT     ERPFirms.FirmName,ERPUserHeader.PKID, ERPUserHeader.UserName, ERPUserHeader.FKDesignation, ERPUserHeader.LoginName, ERPUserHeader.Password, 
                      ERPLookUPMaster_1.Description AS Designation, ERPUserHeader.UserType,  ERPUserHeader.FKFirm, 
                      ERPLookUPMaster.Description AS UnitType
FROM         ERPLookUPMaster RIGHT OUTER JOIN
                      ERPFirms ON ERPLookUPMaster.PKID = ERPFirms.FKUnitType RIGHT OUTER JOIN
                      ERPUserHeader ON ERPFirms.PKID = ERPUserHeader.FKFirm LEFT OUTER JOIN
                      ERPLookUPMaster AS ERPLookUPMaster_1 ON ERPUserHeader.FKDesignation = ERPLookUPMaster_1.PKID
WHERE     (dbo.ERPUserHeader.IsDeleted='N')
ORDER BY ERPFirms.FirmName, ERPUserHeader.UserName

END 



IF @mAction='DELETEUSER'	
BEGIN 

Update ERPUserHeader Set IsDeleted = 'Y' Where PKID = @mPKID

END


IF @mAction='LOADUSERDTL'
BEGIN 

SELECT 
	a.PKID,b.[Functions] As Module,b.MenuHeader As Menu,a.FKMenu,b.MenuItem,a.AllFunctions As CheckAll,
	a.Adding,a.Editing,a.Deleting,a.Viewing As ViewOnly,a.NoEntry 
From 
	ERPUserDetail a,	ERPMenuMaster b 
Where
	 a.FKMenu = b.PKID aND a.FKUser = @mPKID 
Order By 
	b.MenuOrder,b.Functions,b.MenuHeader,b.PKID



END

IF @mAction='LOADALL'
BEGIN 
SELECT     TOP 100 PERCENT dbo.ERPFirms.FirmName, dbo.ERPUserHeader.PKID, dbo.ERPUserHeader.UserName, dbo.ERPUserHeader.FKDesignation, 
                      dbo.ERPUserHeader.LoginName, dbo.ERPUserHeader.Password, ERPLookUPMaster_1.Description AS Designation, dbo.ERPUserHeader.UserType, 
                      dbo.ERPUserHeader.FKFirm, dbo.ERPLookUPMaster.Description AS UnitType, dbo.ERPUserDetail.FKUser, dbo.ERPUserDetail.FKMenu, 
                      dbo.ERPUserDetail.AllFunctions, dbo.ERPUserDetail.Adding, dbo.ERPUserDetail.Editing, dbo.ERPUserDetail.Deleting, dbo.ERPUserDetail.Viewing, 
                      dbo.ERPUserDetail.NoEntry, dbo.ERPMenuMaster.MenuItem
FROM         dbo.ERPUserDetail INNER JOIN
                      dbo.ERPUserHeader ON dbo.ERPUserDetail.FKUser = dbo.ERPUserHeader.PKID INNER JOIN
                      dbo.ERPMenuMaster ON dbo.ERPUserDetail.FKMenu = dbo.ERPMenuMaster.PKID LEFT OUTER JOIN
                      dbo.ERPLookUPMaster RIGHT OUTER JOIN
                      dbo.ERPFirms ON dbo.ERPLookUPMaster.PKID = dbo.ERPFirms.FKUnitType ON dbo.ERPUserHeader.FKFirm = dbo.ERPFirms.PKID LEFT OUTER JOIN
                      dbo.ERPLookUPMaster ERPLookUPMaster_1 ON dbo.ERPUserHeader.FKDesignation = ERPLookUPMaster_1.PKID
WHERE     (dbo.ERPUserHeader.IsDeleted='N' AND dbo.ERPUserHeader.PKID=@mPKID)
ORDER BY dbo.ERPFirms.FirmName, dbo.ERPUserHeader.UserName  
END 

IF @mAction='UPDDTL'
BEGIN 
	

	--SELECT @mPKID=IsNull(MAX(PKID),0)+1 FROM ERPUserDetail
	Update 
	ERPUserDetail
	Set
	FKMenu=@mFKMenu ,AllFunctions=@mAllFunctions,
	Adding=@mAdding ,Editing=@mEditing ,Deleting=@mDeleting ,
	Viewing=@mViewing ,NoEntry=@mNoEntry 
	Where FKUser=@mFKUser	
END 
	
IF @mAction='DELETEUSERRIGHT'
BEGIN 

Delete from ERPUserDetail where PKID=@mPKID  and FKMenu =@mFKMenu 
END 

IF @mAction='UPDPASSWORD'
BEGIN 
 	UPDATE 	ERPUserHeader 
	SET 
	Password=@mPassword 
	Where PKID=@mPKID
	
END

IF @mAction='INSERTUSERAUTH'
BEGIN 

	--SELECT @mPKID=IsNull(MAX(PKID),0)+1 FROM ERPUserAuthentication
	
	INSERT INTO
	ERPUserAuthentication
	VALUES
	(--@mPKID,
	@mUserName,	@mIPAddress,	@mSystemName,
	@mServer,	@mLoginTime,	@mLogoutTime,	
	@mIsActive,	''	,	@mVersion)--

END 




IF @mAction='UPDATEUSERAUTH'
BEGIN 
	UPDATE 
	ERPUserAuthentication
	SET
	LogoutTime=@mLogoutTime, IsActive=@mIsActive
	WHERE 
	UserName=@mUserName And
	IsActive = '1'

END



IF @mAction='CHECKUSERALRDYLOG'
BEGIN 

	SELECT 
	*
	FROM 
	ERPUserAuthentication 
	WHERE 
	UserName=@mUserName And 
	IsActive='1'

END 

IF @mAction='CHECKSAMEIP'
BEGIN 

	SELECT 
	a.*,b.UserName
	FROM 
	ERPUserAuthentication  a, ERPUserHeader b
	WHERE 
	a.FKUserName = b.PKID And 
	a.IPAddress = @mIPAddress And 
	a.IsActive='1'

END 

IF @mAction='CHECKIPADDRLOG'
BEGIN 

SELECT 
	a.*,b.UserName
	FROM 
	ERPUserAuthentication  a, ERPUserHeader b
	WHERE 
	a.FKUserName = b.PKID And 
	--a.FKUserName=@mFKUserName AND 
	a.IPAddress = @mIPAddress And 
	a.IsActive='1'
	
	

END


IF @mAction='UPDATEUSER'
BEGIN 
	
UPDATE
	ERPUserAuthentication 
	
	SET
	IsActive=@mIsActive,
	Reason=@mReason
Where
	PKID=@mPKID
	
	
END 

IF @mAction='SELACTIVEUSER'
BEGIN 
	
	SELECT
	a.PKID,a.FKUserName,b.UserName,a.IPAddress,a.SystemName,a.LoginTime,a.Version

	From 
	ERPUserAuthentication a, ERPUserHeader b

	Where
	a.FKUserName = b.PKID And a.IsActive = '1'

	Order By
	a.PKID	
	
END 









GO
