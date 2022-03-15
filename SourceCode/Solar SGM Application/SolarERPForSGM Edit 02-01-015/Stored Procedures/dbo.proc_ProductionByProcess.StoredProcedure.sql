USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_ProductionByProcess]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc proc_ProductionByProcess

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_ProductionByProcess]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mID							varchar(50) 	=Null,
@mProcessName					varchar(50)		=Null,
@mProcessDate					date			=Null,
@mShiftNo						varchar(10)		=Null,
@mMachineNo						varchar(50)		=Null,
@mSalesOrderNo					varchar(50)		=Null,
@mArticle						varchar(50)		=Null,
@mVariant						varchar(50)		=Null,
@mArticleGroup					varchar(50)		=Null,
@mArticleGroupCode				varchar(50)		=Null,
@mMaterialCode					varchar(50)		=Null,
@mSize							varchar(20)		=Null,
@mPcs							numeric(18, 0)	=Null,
@mQuantity						numeric(18, 2)	=Null,
@mUnit							varchar(10)		=Null,
@mPrice							decimal(18, 2)	=Null,
@mValue							decimal(18, 0)	=Null,
@mCompanyCode					varchar(20)		=Null,
@mJobberCode					varchar(50)		=Null,
@mLotNo							varchar(50)		=Null,
@mColor							varchar(20)		=Null,
@mWorkOrderNo					varchar(50)		=Null,
@mMaterialColor					varchar(20)		=Null,
@mLocationName					varchar(50)		=Null,
@mBuyerCode						varchar(20)		=Null,
@mBuyer							varchar(50)		=Null,
@mSupplierCode					varchar(50)		=Null,
@mBrandCode						varchar(50)		=Null,
@mSupplierMaterialCode			varchar(50)		=Null,
@mCreatedBy						varchar(100)	=Null,
@mCreatedDate					datetime		=Null,
@mModifiedBy					varchar(100)	=Null,
@mModifiedDate					datetime		=Null,
@mEnteredOnMachineID			varchar(50)		=Null,
@mExeVersionNo					varchar(50)		=Null,
@mIsApproved					bit				=Null,
@mApprovedBy					varchar(50)		=Null,
@mApprovedOn					datetime		=Null,
@mModuleName					varchar(50)		=Null,
@mJobCardDetailID				varchar(50)		=Null,
@mEmployeeCode					varchar(20)		=Null,
@mProductionID					varchar(50)		=Null,
@mLocation						varchar(50)		=Null,
@mStation						varchar(20)		=Null,
@mRejectPcs						decimal(18, 1)	=Null,
@mFromLocation					varchar(20)		=Null,
@mSeqNo							int				=Null,
@mCurrentQuantity				numeric(18, 2)	=Null,
@mLossQuantity					numeric(18, 2)	=Null,
@mCurrentPcs					numeric(18, 1)	=Null,
@mLossPcs						numeric(18, 1)	=Null,
@mSkinType						varchar(10)		=Null,
@mFromStage						varchar(50)		=Null,
@mOldFromLocation				varchar(3)		=Null,
@mOldLocation					varchar(3)		=Null,
@mIsHybrid						bit				=Null,
@mComponentGroup				varchar(50)		=Null,
@mIsLastStage					bit				=Null,
@mOldJobCardNo					varchar(50)		=Null,
@mLeatherCode					varchar(50)		=Null,
@mArticleDetailId				varchar(50)		=Null,
@mStage							varchar(50)		=Null,
@mArticleNo						varchar(50)		=Null,
@mWeight						numeric(18, 2)	=Null,
@mTotValue						decimal(18, 1)	=Null,
@mBaseStyle						varchar(50)		=Null,
@mCustomerArtilceNo				varchar(50)		=Null,
@mSizeSequenceNo				varchar(50)		=Null,
@mRsNo							varchar(50)		=Null,
@mBuyerGroup					varchar(50)		=Null,
@mMerchandiser					varchar(50)		=Null,
@mTransactionType				varchar(50)		=Null,
@mJobcardNo						Varchar(50)		=Null,
@mCartonNO						Int				=Null,
@mStatus						Varchar(20)		=Null,
@mPartQuantity					Int				=Null,
@mQuantity01					Int				=Null,
@mQuantity02					Int				=Null,
@mQuantity03					Int				=Null,
@mQuantity04					Int				=Null,
@mQuantity05					Int				=Null,
@mQuantity06					Int				=Null,
@mQuantity07					Int				=Null,
@mQuantity08					Int				=Null,
@mQuantity09					Int				=Null,
@mQuantity10					Int				=Null,
@mQuantity11					Int				=Null,
@mQuantity12					Int				=Null,
@mQuantity13					Int				=Null,
@mQuantity14					Int				=Null,
@mQuantity15					Int				=Null,
@mQuantity16					Int				=Null,
@mQuantity17					Int				=Null,
@mQuantity18					Int				=Null,
@mIsSinglesize					Bit				=Null


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

If @mAction= 'INSPRODUCTION'
BEGIN
	INSERT INTO
	PRODUCTIONBYPROCESS
	 
	VALUES
	(
		@mID,					@mProcessName,				@mProcessDate,				@mShiftNo,						@mMachineNo,
		@mSalesOrderNo,			@mArticle,					@mVariant,					@mArticleGroup,					@mArticleGroupCode,
		@mMaterialCode,			@mSize,						@mPcs,						@mQuantity,						@mUnit,
		@mPrice,				@mValue,					@mCompanyCode,				@mJobberCode,					@mLotNo,
		@mColor,				@mWorkOrderNo,				@mMaterialColor,			@mLocationName,					@mBuyerCode,
		@mBuyer,				@mSupplierCode,				@mBrandCode,				@mSupplierMaterialCode,			@mCreatedBy,
		@mCreatedDate,			@mModifiedBy,				@mModifiedDate,				@mEnteredOnMachineID,			@mExeVersionNo,
		@mIsApproved,			@mApprovedBy,				@mApprovedOn,				@mModuleName,					@mJobCardDetailID,
		@mEmployeeCode,			@mProductionID,				@mLocation,					@mStation,						@mRejectPcs,
		@mFromLocation,			@mSeqNo,					@mCurrentQuantity,			@mLossQuantity,					@mCurrentPcs,
		@mLossPcs,				@mSkinType,					@mFromStage,				@mOldFromLocation,				@mOldLocation,
		@mIsHybrid,				@mComponentGroup,			@mIsLastStage,				@mOldJobCardNo,					@mLeatherCode,
		@mArticleDetailId
	)
	
	
END	

IF @mAction='SELPRODUCTION'
BEGIN
	SELECT 
	*

	FROM
	PRODUCTIONBYPROCESS

	WHERE
	ProcessName = @mProcessName			And			ProcessDate = @mProcessDate			And
    ShiftNo = @mShiftNo					And			MachineNo = @mMachineNo				And
	Size = @mSize						And			JobCardDetailID = @mJobCardDetailID And
    Station = @mStation
END

IF @mAction='UPDPRODUCTION'
BEGIN
	UPDATE
	PRODUCTIONBYPROCESS

	SET
	Quantity = @mQuantity,		CurrentQuantity = @mCurrentQuantity,		ModifiedDate = @mModifiedDate
	
	WHERE ID = @mID
END

If @mAction= 'INSPRODUCSTOCK'
BEGIN

	INSERT INTO
	PRODUCTSTOCK
	 
	VALUES
	(
		@mID,					@mLocation,					@mStage,					@mSalesOrderNo,					@mWorkOrderNo,
		@mArticleNo,			@mWeight,					@mQuantity,					@mUnit,							@mPrice,
		@mTotValue,				@mCompanyCode,				@mJobberCode,				@mCreatedBy,					@mCreatedDate,
		@mModifiedBy,			@mModifiedDate,				@mEnteredOnMachineID,		@mBaseStyle,					@mColor,
		@mSize,					@mVariant,					@mCustomerArtilceNo,		@mSizeSequenceNo,				@mRsNo,
		@mLocationName,			@mBuyerGroup,				@mMerchandiser,				@mTransactionType,				@mExeVersionNo,
		@mIsApproved,			@mApprovedBy,				@mApprovedOn,				@mModuleName,					@mBuyer,
		@mOldLocation,			@mComponentGroup,			@mLeatherCode,				@mSupplierCode,					@mMaterialCode
	)

	
END	

IF @mAction='SELPRODUCTSTOCK'
BEGIN
	SELECT 
	*

	FROM
	PRODUCTSTOCK

	WHERE
	Stage = @mProcessName			And			Size = @mSize			And
	WorkOrderNo = @mWorkOrderNo
END

IF @mAction='UPDPRODUCTSTOCK'
BEGIN
	UPDATE
	PRODUCTSTOCK

	SET
	Quantity = @mQuantity,		ModifiedDate = @mModifiedDate
	
	WHERE ID = @mID
END


IF @mAction='SELPARTPROD'
BEGIN
	SELECT 
	*

	FROM
	Partquantityproduction
	
	WHERE
	ProcessName = @mProcessName			And			JobcardNo = @mJobcardNo			And
    CartonNo = @mCartonNO				
END

IF @mAction='INSPARTPROD'
BEGIN
	INSERT INTO 
	Partquantityproduction
	
	VALUES
	(
		@mID,			@mCreatedBy,				@mCreatedDate,			@mModifiedBy,			@mModifiedDate,
		@mExeVersionNo,	@mIsApproved,				@mApprovedBy,			@mApprovedOn,			@mModuleName,
		@mJobCardNo,	@mQuantity,					@mCartonNo,				@mStatus,				@mPartQuantity,
		@mQuantity01,	@mQuantity02,				@mQuantity03,			@mQuantity04,			@mQuantity05,
		@mQuantity06,	@mQuantity07,				@mQuantity08,			@mQuantity09,			@mQuantity10,
		@mQuantity11,	@mQuantity12,				@mQuantity13,			@mQuantity14,			@mQuantity15,
		@mQuantity16,	@mQuantity17,				@mQuantity18,			@mProcessName,			@mIsSingleSize,
		@mSize
	)
	
END

IF @mAction='UPDPARTPROD'
BEGIN
	UPDATE
	Partquantityproduction
	
	SET
		[Status] = @mStatus,			PartQuantity = @mPartQuantity,
		Quantity01 = @mQuantity01,		Quantity02 = @mQuantity02,				Quantity03 = @mQuantity03,
		Quantity04 = @mQuantity04,		Quantity05 = @mQuantity05,				Quantity06 = @mQuantity06,
		Quantity07 = @mQuantity07,		Quantity08 = @mQuantity08,				Quantity09 = @mQuantity09,
		Quantity10 = @mQuantity10,		Quantity11 = @mQuantity11,				Quantity12 = @mQuantity12,
		Quantity13 = @mQuantity13,		Quantity14 = @mQuantity14,				Quantity15 = @mQuantity15,
		Quantity16 = @mQuantity16,		Quantity17 = @mQuantity17,				Quantity18 = @mQuantity18
		
	WHERE
		ID = @mID
	
END

GO
