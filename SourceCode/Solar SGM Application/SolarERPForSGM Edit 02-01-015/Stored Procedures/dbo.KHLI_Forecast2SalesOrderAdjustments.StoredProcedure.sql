USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[KHLI_Forecast2SalesOrderAdjustments]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc KHLI_Forecast2SalesOrderAdjustments

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[KHLI_Forecast2SalesOrderAdjustments]
@mAction				varchar(20)	='SELALL',
@mPKId					int		=Null,
@mFromDate				Datetime	=Null,
@mToDate				DateTime	=Null,
@mSalesOrderNo			varchar	(50) =NULL,
@mSeason				Varchar(50) = Null,
@mCustomer				Varchar(100) = Null,
@mOrderNo				Varchar(50)	= Null ,
@mForecastOrderNo 		Varchar(50)	= Null ,
@mArticleGroup 			Varchar(100) = Null ,
@mArticle 				Varchar(100) = Null ,
@mColorCode 			Varchar(50)	= Null ,
@mMaterialCode 			Varchar(50)	= Null ,
@mMaterialSize			Varchar(50)	= Null , 
@mFCOrderNo				Varchar(50)	= Null ,
@mBuyerGroupCode		Varchar(50)	= Null ,
@mLeatherCode			Varchar(50) = Null ,
@mLeatherName			Varchar(200) = Null ,
@mColorName				Varchar(100) = Null ,
@mDescription			Varchar(250) = Null ,
@mMaterialColor 		Varchar(50) = Null ,
@mUnit 					Varchar(50) = Null ,
@mSize 					Varchar(50) = Null ,
@mMaterialSizeGroupName Varchar(50) = Null ,
@mVariant 				Varchar(10) = Null ,
@mComponentGroup		Varchar(10) = Null ,

@mDWADID 				varchar(50) = Null ,
@mDWADShipper 			varchar(20) = Null ,
@mDWADOrderNo 			varchar(20) = Null ,
@mDWADSalesOrderNo 		varchar(20) = Null ,
@mDWADBuyerCode 		varchar(50) = Null ,
@mDWADBuyerGroupCode 	varchar(50) = Null ,
@mDWADBuyerOrderNo 		varchar(50) = Null ,
@mDWADArticleGroup 		varchar(50) = Null ,
@mDWADArticle 			varchar(50) = Null ,
@mDWADLeatherCode 		varchar(50) = Null ,
@mDWADLeatherName 		varchar(500) = Null ,
@mDWADColorCode 		varchar(50) = Null ,
@mDWADColorName 		varchar(50) = Null ,
@mDWADMaterialCode 		varchar(50) = Null ,
@mDWADDescription 		varchar(500) = Null ,
@mDWADMaterialTypeCode 	varchar(50) = Null ,
@mDWADMaterialCategory 	varchar(50) = Null ,
@mDWADMaterialColor 	varchar(50) = Null ,
@mDWADMaterialSize 		varchar(50) = Null ,
@mDWADMaterialSizeGroup varchar(50) = Null ,
@mDWADUnit 				varchar(50) = Null ,
@mDWADConsumptionQuantity	decimal(18, 2) = Null ,
@mDWADConsumptionPcs 	decimal(18, 2) = Null ,
@mDWADQuantity 			decimal(18, 2) = Null ,
@mDWADPcs 				decimal(18, 2) = Null ,
@mDWADCreatedBy 		varchar(100) = Null ,
@mDWADCreatedDate 		datetime  = NULL,
@mDWADModifiedBy 		varchar(100) = Null ,
@mDWADModifiedDate 		datetime = NULL,
@mDWADEnteredOnMachineID	varchar(50) = Null ,
@mDWADIsApproved 		bit = NULL,
@mDWADApprovedBy 		varchar(50) = Null ,
@mDWADApprovedOn 		datetime = NULL,
@mDWADModuleName 		varchar(50) = Null ,
@mDWADIndentQuantity 	decimal(18, 2) = Null ,
@mDWADIndentPcs 		decimal(18, 0) = Null ,
@mDWADPOQuantity 		decimal(18, 2) = Null ,
@mDWADPOQuantityPcs 	decimal(18, 0) = Null ,
@mDWADReceivedQuantity 	decimal(18, 2) = Null ,
@mDWADReceivedPcs 		decimal(18, 0) = Null ,
@mDWADDepartment 		varchar(50) = Null ,
@mDWADSize 				varchar(50) = Null ,
@mDWADMaterialSizeGroupName	varchar(50) = Null ,
@mDWADVariant 			varchar(50) = Null ,
@mDWADConsumption 		decimal(18, 4) = Null ,
@mDWADComponentGroup 	varchar(50) = Null ,
@mDWADStatus 			varchar(50) = Null ,
@mDWADParentMaterialCode	varchar(50) = Null ,
@mDWADForecastOrderNo 	varchar(50) = Null ,
@mDWADCommitmentOrderNo varchar(50) = Null ,
@mDWADExeVersionNo 		varchar(50) = Null ,
@mDWADProcessOrderQuantity	decimal(18, 2) = Null ,
@mDWADSlNo 				int = NULL,
@mIndentQuantity			decimal(18,2)	= Null ,
@mAdjustedQuantity			decimal(18,2)	= Null ,
@mBal2AdjustQuantity		decimal(18,2)	= Null ,
@mPOQuantity				decimal(18,2)	= Null ,
@mPOAdjustedQuantity		decimal(18,2)	= Null ,
@mPOBal2AdjustQuantity		decimal(18,2)	= Null 
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int


If @mAction='LoadSeason'
BEGIN
	Select SeasonCode From SeasonMaster ORDER BY SeasonCode
END


If @mAction='LoadCustomer'
BEGIN
	Select Distinct(BuyerGroupCode) From SalesOrder
	WHERE Shipper in ('SLI','KHLI') And Season = @mSeason
	ORDER BY BuyerGroupCode
END

If @mAction='LoadOrderNos'
BEGIN
	Select Distinct(OrderNo) From SalesOrder
	WHERE Shipper in ('SLI','KHLI') And Season = @mSeason And BuyerGroupCode = @mCustomer
	And Type = 'SALESORDER'
	ORDER BY OrderNo
END

If @mAction='LoadForecastOrder'
BEGIN
	Select Distinct(ForecastOrderNo) From SalesOrder
	WHERE Shipper in ('SLI','KHLI') And Season = @mSeason And BuyerGroupCode = @mCustomer
	And SalesORderType = 'FORECAST'
	ORDER BY ForecastOrderNo
END
 
If @mAction='FCDemand'
BEGIN
	SELECT * FROM vw_DemandQuantityforForecastAdjustments Where SalesOrderNo = @mFCOrderNo
	--And MaterialCode = 'MAT-ASG-HB-0001' AND MaterialSize = 'S'
END

If @mAction='SELD4TMP'
BEGIN
	Select * From tmpForecastIndentQty
	Where MaterialCode = @mMaterialCode And MaterialSize = @mMaterialSize And PurchaseIndentType = 'FC' 
	And ForecastORderNo = @mFCOrderNo
END

If @mAction='INSD4TMP'
BEGIN
	Insert Into  tmpForecastIndentQty
	Values 
	(@mFCOrderNo,			@mMaterialCode,			@mMaterialSize,				'FC',	
	@mIndentQuantity,		@mAdjustedQuantity,		@mBal2AdjustQuantity,
	@mPOQuantity,			@mPOAdjustedQuantity,	@mPOBal2AdjustQuantity)
	
END


If @mAction='UPDD4TMP'
BEGIN
	Update tmpForecastIndentQty
	Set
	AdjustedQuantity = @mAdjustedQuantity,		Bal2AdjustQuantity = @mBal2AdjustQuantity,
	POAdjustedQuantity = @mPOAdjustedQuantity,	POBal2AdjustQuantity = @mPOBal2AdjustQuantity
	
	WHERE
	PKID = @mPKID
	
END


If @mAction='FCIndQty'
BEGIN
	Select IsNull(Sum(Quantity),0) As IndentQuantity From PurchaseIndentDetail
	Where MaterialCode = @mMaterialCode And MaterialSize = @mMaterialSize And PurchaseIndentType = 'FC' 
	And ForecastORderNo = @mFCOrderNo
	--And CommitmentOrderNo in
	--(Select CommitmentOrderNo From SalesOrderDetails where ForecastOrderNo = @mFCOrderNo And Article = @mArticle
	--And ColorCode = @mColorCode And MainRawMaterialCode = @mLeathercode And Type <> 'SALESORDER' )
END

If @mAction='FCPOQty'
BEGIN
	Select IsNull(Sum(Quantity),0) As POQuantity From PurchaseOrderDetails
	Where MaterialCode = @mMaterialCode And MaterialSize = @mMaterialSize And PurchaseOrderType = 'FC' 
	And SalesORderNo = @mFCOrderNo
END

If @mAction='LoadSalesOrders'
BEGIN
	SELECT SalesOrderNo FROM SalesOrder where ForecastOrderNo = @mFCOrderNo And Type = 'SALESORDER' Order by SalesOrderNo
	
END

If @mAction = 'LoadDemandBySO'
BEGIN
	Select * from Demand WHERE
	
	SalesOrderNo = @mSalesOrderNo And			BuyerGroupCode = @mBuyerGroupCode And			ArticleGroup = @mArticleGroup And 
	Article = @mArticle And						LeatherCode = @mLeatherCode And					LeatherName = @mLeatherName And 
	ColorCode = @mColorCode And					ColorName = @mColorName And						MaterialCode = @mMaterialCode And 
	Description = @mDescription And				MaterialColor = @mMaterialColor And				MaterialSize = @mMaterialSize And 
	Unit = @mUnit And							--Size = @mSize And								MaterialSizeGroupName = @mMaterialSizeGroupName And 
	Variant = @mVariant And						ComponentGroup = @mComponentGroup

END

If @mAction='INSERTDWAD'
BEGIN
	Insert Into DemandWithAdjustmentDetails 
	VALUES
	(
	@mDWADID,				@mDWADShipper,			@mDWADOrderNo,			@mDWADSalesOrderNo,			@mDWADBuyerCode,
	@mDWADBuyerGroupCode,	@mDWADBuyerOrderNo,		@mDWADArticleGroup,		@mDWADArticle,				@mDWADLeatherCode,
	@mDWADLeatherName,		@mDWADColorCode,		@mDWADColorName,		@mDWADMaterialCode,			@mDWADDescription,
	@mDWADMaterialTypeCode,	@mDWADMaterialCategory,	@mDWADMaterialColor,	@mDWADMaterialSize,			@mDWADMaterialSizeGroup,
	@mDWADUnit,				@mDWADConsumptionQuantity,						@mDWADConsumptionPcs,		@mDWADQuantity,
	@mDWADPcs,				@mDWADCreatedBy,		@mDWADCreatedDate,		@mDWADModifiedBy,			@mDWADModifiedDate,
	@mDWADEnteredOnMachineID,						@mDWADIsApproved,		@mDWADApprovedBy,			@mDWADApprovedOn,
	@mDWADModuleName,		@mDWADIndentQuantity,	@mDWADIndentPcs,		@mDWADPOQuantity,			@mDWADPOQuantityPcs,
	@mDWADReceivedQuantity,	@mDWADReceivedPcs,		@mDWADDepartment,		@mDWADSize,					@mDWADMaterialSizeGroupName,
	@mDWADVariant,			@mDWADConsumption,		@mDWADComponentGroup,	@mDWADStatus,				@mDWADParentMaterialCode,
	@mDWADForecastOrderNo,	@mDWADCommitmentOrderNo,@mDWADExeVersionNo,		@mDWADProcessOrderQuantity
	)
END

GO
