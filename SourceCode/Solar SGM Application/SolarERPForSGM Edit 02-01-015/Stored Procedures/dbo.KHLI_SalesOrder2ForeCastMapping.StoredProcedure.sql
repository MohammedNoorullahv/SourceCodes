USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[KHLI_SalesOrder2ForeCastMapping]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc KHLI_SalesOrder2ForeCastMapping

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[KHLI_SalesOrder2ForeCastMapping]
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

@mHdrID					varchar(50)	= Null ,
@mHdrCreatedBy			varchar(100)	= Null ,
@mHdrCreatedDate		datetime	= Null ,
@mHdrModifiedBy			varchar(100)	= Null ,
@mHdrModifiedDate		datetime	= Null ,
@mHdrExeVersionNo		varchar(50)	= Null ,
@mHdrIsApproved			bit	= Null ,
@mHdrApprovedBy			varchar(100)	= Null ,
@mHdrApprovedOn			datetime	= Null ,
@mHdrModuleName			varchar(100)	= Null ,
@mHdrSalesOrderId		varchar(100)	= Null ,
@mHdrSalesOrderNo		varchar(50)	= Null ,
@mHdrBuyerBuy			varchar(50)	= Null ,
@mHdrBuyerOrderNo		varchar(50)	= Null ,
@mHdrBuyerGroupCode		varchar(50)	= Null ,
@mHdrDestination		varchar(50)	= Null ,
@mHdrOrderRecivedDate	date	= Null ,
@mHdrOrderConfirmedDate	date	= Null ,
@mHdrOrderQuality		varchar(50)	= Null ,
@mHdrOrderStatus		varchar(50)	= Null ,
@mHdrSeason				varcHar(50)	= Null ,
@mHdrShipper			varchar(10)	= Null ,
@mHdrArticleGroup		varchar(50)	= Null ,
@mHdrUnit				varchar(50)	= Null ,
@mHdrCurrency			varchar(50)	= Null ,
@mHdrCurrencyConversion	decimal(18, 2)	= Null ,
@mHdrIsAssortedOrder	varchar(50)	= Null ,
@mHdrTotalOrderQuantity	decimal(18, 0)	= Null ,
@mHdrUserCategory		varchar(50)	= Null ,
@mHdrSalesOrderDate		date	= Null ,
@mHdrBuyerOrderType		varchar(50)	= Null ,
@mHdrOrderNo			varchar(50)	= Null ,
@mHdrSizeName			varchar(50)	= Null ,
@mHdrPortOfDischarge	varchar(50)	= Null ,
@mHdrInternalSalesOrderNo	varchar(50)	= Null ,
@mHdrInternalBuyer		varchar(50)	= Null ,
@mHdrType				varchar(50)	= Null ,
@mHdrBuyerOrderID		varchar(50)	= Null  ,

@mDtlID			varchar(50) 	= Null  ,
@mDtlCreatedBy			varchar(100) = NULL,
@mDtlCreatedDate			datetime 	= Null  ,
@mDtlModifiedBy			varchar(100) 	= Null  ,
@mDtlModifiedDate			datetime 	= Null  ,
@mDtlExeVersionNo			varchar(50) 	= Null  ,
@mDtlIsApproved			bit 	= Null  ,
@mDtlApprovedBy			varchar(100) 	= Null  ,
@mDtlApprovedOn			datetime 	= Null  ,
@mDtlModuleName			varchar(100) 	= Null  ,
@mDtlShipper			varchar(50) 	= Null  ,
@mDtlOrderNo			varchar(50) 	= Null  ,
@mDtlSalesOrderNo			varchar(50) 	= Null  ,
@mDtlBuyerGroupCode			varchar(50) 	= Null  ,
@mDtlBuyerOrderNo			varchar(50) 	= Null  ,
@mDtlArticleGroup			varchar(50) 	= Null  ,
@mDtlArticle			varchar(50) 	= Null  ,
@mDtlLeatherCode			varchar(50) 	= Null  ,
@mDtlLeatherName			varchar(50) 	= Null  ,
@mDtlColorCode			varchar(50) 	= Null  ,
@mDtlColorName			varchar(50) 	= Null  ,
@mDtlMaterialCode			varchar(50) 	= Null  ,
@mDtlDescription			varchar(150) 	= Null  ,
@mDtlMaterialTypeCode			varchar(50) 	= Null  ,
@mDtlMaterialColor			varchar(50) 	= Null  ,
@mDtlMaterialSize			varchar(50) 	= Null  ,
@mDtlMaterialSizeGroup			varchar(50) 	= Null  ,
@mDtlUnit			varchar(50) 	= Null  ,
@mDtlConsumptionQuantity			decimal(18, 4) 	= Null  ,
@mDtlQuantity			decimal(18, 4) 	= Null  ,
@mDtlIndentQuantity			decimal(18, 4) 	= Null  ,
@mDtlIndentPcs			decimal(18, 0) 	= Null  ,
@mDtlPOQuantity			decimal(18, 4) 	= Null  ,
@mDtlPOQuantityPcs			decimal(18, 0) 	= Null  ,
@mDtlReceivedQuantity			decimal(18, 4) 	= Null  ,
@mDtlReceivedPcs			decimal(18, 0) 	= Null  ,
@mDtlDepartment			varchar(50) 	= Null  ,
@mDtlSize			varchar(50) 	= Null  ,
@mDtlMaterialSizeGroupName			varchar(50) 	= Null  ,
@mDtlVariant			int 	= Null  ,
@mDtlConsumption			decimal(18, 4) 	= Null  ,
@mDtlComponentGroup			varchar(50) 	= Null  ,
@mDtlStatus			varchar(50) 	= Null  ,
@mDtlParentMaterialCode			varchar(50) 	= Null  ,
@mDtlForecastOrderNo			varchar(50) 	= Null  ,
@mDtlSOFMID			varchar(50) 	= Null  ,
@mDtlDemandId			varchar(50) 	= Null  

	
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

If @mAction='LoadSalesOrder'
BEGIN
	SELECT * FROM SalesOrder Where OrderNo = @mOrderNo Order by SalesOrderNo
END

If @mAction='LoadSalesOrderDetail'
BEGIN
	SELECT Distinct SalesOrderNO, ForeCastOrderNo FROM SalesOrderDetails where SalesOrderNo = @mOrderNo Order By SalesOrderNO, ForeCastOrderNo
END
If @mAction='LoadDemandBySO'
BEGIN
	Select * from Demand Where SalesOrderNo = @mSalesOrderNo And MaterialCode = 'PAC-SHB-XX-0142'
	Order By ArticleGroup,Article,ColorCode,MaterialCode,MaterialSize,CreatedDAte
END

If @mAction='LoadForecastQty'
BEGIN
	Select IsNull(Sum(IndentQuantity),0) from Demand Where 
	SalesOrderNo = @mForecastOrderNo And
	ArticleGroup = @mArticleGroup And
	Article = @mArticle And
	--ColorCode = @mColorCode And
	MaterialCode = @mMaterialCode And
	MaterialSize = @mMaterialSize
END

If @mAction='INSERTHDR'
BEGIN
	INSERT INTO SalesOrderforForecastMapping
	
	VALUES
	(@mHdrID,			@mHdrCreatedBy,			@mHdrCreatedDate,			@mHdrModifiedBy,			@mHdrModifiedDate,
	@mHdrExeVersionNo,	@mHdrIsApproved,		@mHdrApprovedBy,			@mHdrApprovedOn,			@mHdrModuleName,
	@mHdrSalesOrderId,	@mHdrSalesOrderNo,		@mHdrBuyerBuy,				@mHdrBuyerOrderNo,			@mHdrBuyerGroupCode,
	@mHdrDestination,	@mHdrOrderRecivedDate,	@mHdrOrderConfirmedDate,	@mHdrOrderQuality,			@mHdrOrderStatus,
	@mHdrSeason,		@mHdrShipper,			@mHdrArticleGroup,			@mHdrUnit,					@mHdrCurrency,
	@mHdrCurrencyConversion,					@mHdrIsAssortedOrder,		@mHdrTotalOrderQuantity,	@mHdrUserCategory,
	@mHdrSalesOrderDate,@mHdrBuyerOrderType,	@mHdrOrderNo,				@mHdrSizeName,				@mHdrPortOfDischarge,
	@mHdrInternalSalesOrderNo,					@mHdrInternalBuyer,			@mHdrType,					@mHdrBuyerOrderID
	)
END


If @mAction='INSERTDTL'
BEGIN
	INSERT INTO SalesOrderForForecastMappingDetails
	
	VALUES
	(
	@mDtlID,			@mDtlCreatedBy,		@mDtlCreatedDate,		@mDtlModifiedBy,		@mDtlModifiedDate,		@mDtlExeVersionNo,
	@mDtlIsApproved,	@mDtlApprovedBy,	@mDtlApprovedOn,		@mDtlModuleName,		@mDtlShipper,			@mDtlOrderNo,
	@mDtlSalesOrderNo,	@mDtlBuyerGroupCode,@mDtlBuyerOrderNo,		@mDtlArticleGroup,		@mDtlArticle,			@mDtlLeatherCode,
	@mDtlLeatherName,	@mDtlColorCode,		@mDtlColorName,			@mDtlMaterialCode,		@mDtlDescription,		@mDtlMaterialTypeCode,
	@mDtlMaterialColor,	@mDtlMaterialSize,	@mDtlMaterialSizeGroup,	@mDtlUnit,				@mDtlConsumptionQuantity,		
	@mDtlQuantity,		@mDtlIndentQuantity,@mDtlIndentPcs,			@mDtlPOQuantity,		@mDtlPOQuantityPcs,		@mDtlReceivedQuantity,
	@mDtlReceivedPcs,	@mDtlDepartment,	@mDtlSize,				@mDtlMaterialSizeGroupName,						@mDtlVariant,
	@mDtlConsumption,	@mDtlComponentGroup,@mDtlStatus,			@mDtlParentMaterialCode,@mDtlForecastOrderNo,	@mDtlSOFMID,
	@mDtlDemandId
	)
END

If @mAction= 'LoadCurrentFCQty'
Begin
	Select IsNull(Sum(IndentQuantity),0) from SalesOrderForForecastMappingDetails Where 
	SOFMID = @mDtlSOFMID
End

GO
