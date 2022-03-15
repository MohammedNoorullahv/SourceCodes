USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[KHLI_OrderOutstanding]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc KHLI_OrderOutstanding

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[KHLI_OrderOutstanding]
@mAction				varchar(20)	='SELALL',
@mPKId					int		=Null,
@mFromDate				Datetime	=Null,
@mToDate				DateTime	=Null,
@mSalesOrderDetailId	varchar(50) =NULL,
@mSalesOrderDate		datetime	 =NULL,
@mCustomerName			varchar	(100) =NULL,
@mSalesOrderNo			varchar	(50) =NULL,
@mSalesOrderDetail		varchar	(50) =NULL,
@mBuyerOrderNo			varchar	(100) =NULL,
@mCustomerOrderNo		varchar	(50) =NULL,
@mArticleGroup			varchar	(50) =NULL,
@mArticleName			varchar	(150) =NULL,
@mColour				varchar	(100) =NULL,
@mDestination			varchar	(50) =NULL,
@mAssortmentName		varchar	(50) =NULL,
@mNoofCartons			int	 =NULL,
@mAssortmentTotalPair	int	 =NULL,
@mBuyerDeliveryDate		datetime	 =NULL,
@mSeason				varchar	(50) =NULL,
@mUserCategory			varchar	(50) =NULL,
@mProductionName		varchar	(50) =NULL,
@mOrdQty				int	 =NULL,
@mCancelQuantity		int	 =NULL,
@mUpperAndLiningCutting	int	 =NULL,
@mUpperAndLiningCuttingBal	int	 =NULL,
@mPreFitting			int	 =NULL,
@mPreFittingBal			int	 =NULL,
@mSocksPrepration		int	 =NULL,
@mSocksPreprationBal	int	 =NULL,
@mUpperConveyorIn		int	 =NULL,
@mUpperConveyorInBal	int	 =NULL,
@mUpperConveyorOut		int	 =NULL,
@mUpperConveyorOutBal	int	 =NULL,
@mHandStitching			int	 =NULL,
@mHasndStitchingBal		int	 =NULL,
@mForming				int	 =NULL,
@mFormingBal			int	 =NULL,
@mFinishing				int	 =NULL,
@mFinishingBal			int	 =NULL,
@mUpperPacking			int	 =NULL,
@mUpperPackingBal		int	 =NULL,
@mUpperDispatch			int	 =NULL,
@mUpperDispatchBal		int	 =NULL,
@mFeeding				int	 =NULL,
@mFeedingBal			int	 =NULL,
@mKitting				int	 =NULL,
@mKittingBal			int	 =NULL,
@mConveyorIn			int	 =NULL,
@mConveyorInBal			int	 =NULL,
@mConveyorOut			int	 =NULL,
@mConveyorOutBal		int	 =NULL,
@mPacking				int	 =NULL,
@mPackingBal			int	 =NULL,
@mDispatch				int	 =NULL,
@mDispatchBal			int	 =NULL,
@mUpdatedOn				Varchar(50) =NULL,
@mIsCompleted			bit	 =NULL,
@mIsClosed				bit	 =NULL,
@mProcessName			Varchar(50)	=NULL,
@mWorkOrderNo			Varchar(50)	=NULL,
@mArticle				Varchar(50)	=NULL,
@mVariant				Varchar(50) =NULL,
@mColorCode				Varchar(50) =NULL,
@mLeatherCode			Varchar(50)	=Null
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELUPDDATE'
BEGIN
	Select Distinct(UpdatedOn) As UpdatedOn from [KHLIOutstanding] Order By UpdatedOn Desc
END

IF @mAction='SELPRODNAME'
BEGIN
	Select * from articlemaster where id in (
	Select ArticleID from ArticleDetail where Article = @mArticle And Variant = @mVariant And ColorCode = @mColorCode And 
	Leathercode = @mLeatherCode)

    
END


IF @mAction='SELALLORD'
BEGIN
	Select 
	SD.ID As SalesOrderDetailId,	SO.OrderRecivedDate As SalesOrderDate,		so.BuyerGroupCode As CustomerName,
	sd.SalesOrderNo,				sd.CustWorkOrderNo As SalesOrderDetail,		so.BuyerOrderNo,							sd.CustomerOrderNo,	
	so.Articlegroup,				sd.Article,									sd.ColorName As Color,
	so.Destination,					sd.AssortmentName,							sd.NoofCartons,								sd.AssortmentTotalPair,
	sd.BuyerDeliveryDate,			sd.Season,									sd.UserCategory,							sd.Variant,
	sd.ColorCode,					sd.MainRawMaterialCode,
	sd.OrderQuantity As OrdQty,		sd.CancelQuantity		
	
	From
	SalesOrderDetails As SD, SalesOrder As SO
	
	Where
	sd.SalesOrderId = so.ID And so.Shipper in ('KHLI','SLI') And sd.Type = 'SALESORDER' And sd.CustWorkOrderNo <> ''
	
	Order By
	SO.OrderRecivedDate,sd.SalesOrderNo, sd.CustWorkOrderNo,sd.AssortmentName
END

IF @mAction='SELPRODORD'
BEGIN
	Select 
	SD.ID As SalesOrderDetailId,	SO.OrderRecivedDate As SalesOrderDate,		Bu.BuyerGroupCode As CustomerName,
	sd.SalesOrderNo,				sd.CustomerOrderNo,	sd.Article As SoleCode,	sd.ArticleName As SoleName,
	ma.MaterialColorDescription As Color,										ma.CodificationNew As Codification,
	sd.OrderQuantity As OrdQty		
	
	From
	SalesOrderDetails As SD, SalesOrder As SO, Materials Ma, Buyer Bu
	
	Where
	sd.SalesOrderId = so.ID And sd.Shipper = 'SSPL' And sd.Article = ma.MaterialCode And so.BuyerCode = bu.BuyerCode And sd.SalesOrderNo in (
	Select Distinct(SalesOrderNo) from ProductionByProcess where CompanyCode = 'SSPL' And ProcessDate >= @mFromDate
	Union
	Select Distinct(Left(JobcardNo,9)) As SalesOrderNo from PackingDetail where shipper = 'SSPL' And DcCartonNo > 0 And PackingDate >= @mFromDate
	union
	Select Distinct(Left(JobcardNo,9)) As SalesOrderNo from InvoiceDetail  where Shipper = 'SSPL' And InvoiceDate >= @mFromDate)
	--sd.SalesOrderNo = 'S-14-1066'
	
	Order By
	SO.OrderRecivedDate,sd.SalesOrderNo
END



IF @mAction='SELSTAGES'
BEGIN
	Select * from ProductionStages Where ProductionName = @mProductionName ORDER BY ProductionType Desc,SequenceNo
END

IF @mAction='SELPRODQTY'
BEGIN
	Select IsNull(Sum(Quantity),0) As ProdQty From ProductionByProcess
	Where ProcessName = @mProcessName And WorkOrderNo like @mWorkOrderNo+'%'
	--Select * from productionByProcess where pROCESSNAME = 'ULC' and workorderno like 'S-F-SS15-020-03-003%'
END

IF @mAction='SELPACKQTY'
BEGIN
	Select IsNull(Sum(Quantity),0) As PackingQty FROM PackingDetail
	Where JobcardNo in 
	(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) And DCCartonNo > 0
END

IF @mAction='SELDISPQTY'
BEGIN
	Select IsNull(Sum(Quantity),0) As DispatchQty FROM InvoiceDetail
	Where JobcardNo in 
	(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) 
END

IF @mAction='SELSOD'
BEGIN
	SELECT * FROM [KHLIOutstanding] Where SalesOrderDetailID = @mSalesOrderDetailID
END

IF @mAction='INSSTATUS'
BEGIN
	INSERT INTO KHLIOutstanding
	
	VALUES
	(
	@mSalesOrderDetailId,		@mSalesOrderDate,		@mCustomerName,		@mSalesOrderNo,		@mSalesOrderDetail,		@mBuyerOrderNo,
	@mCustomerOrderNo,			@mArticleGroup,			@mArticleName,		@mColour,			@mDestination,			@mAssortmentName,
	@mNoofCartons,				@mAssortmentTotalPair,	@mBuyerDeliveryDate,@mSeason,			@mUserCategory,			@mProductionName,
	@mOrdQty,					@mCancelQuantity,		@mUpperAndLiningCutting,				@mUpperAndLiningCuttingBal,	
	@mPreFitting,				@mPreFittingBal,		@mSocksPrepration,	@mSocksPreprationBal,						@mUpperConveyorIn,
	@mUpperConveyorInBal,		@mUpperConveyorOut,		@mUpperConveyorOutBal,					@mHandStitching,		@mHasndStitchingBal,
	@mForming,					@mFormingBal,			@mFinishing,		@mFinishingBal,		@mUpperPacking,			@mUpperPackingBal,
	@mUpperDispatch,			@mUpperDispatchBal,		@mFeeding,			@mFeedingBal,		@mKitting,				@mKittingBal,
	@mConveyorIn,				@mConveyorInBal,		@mConveyorOut,		@mConveyorOutBal,	@mPacking,				@mPackingBal,
	@mDispatch,					@mDispatchBal,			@mUpdatedOn,		@mIsCompleted,		@mIsClosed
)
	
	
END

IF @mAction='UPDSTATUS'
BEGIN
	UPDATE
	SolarOutstanding4SGM
	
	SET
	--Moulding = @mMoulding,		MouldingWIP = @mMouldingWIP,					Finishing = @mFinishing,	FinishingWIP = @mFinishingWIP,
	--Packing = @mPacking,		InStock = @mInStock,							Dispatch = @mDispatch,		UpdatedOn = @mUdpdatedOn,
	IsCompleted = @mIsCompleted,IsClosed = @mIsClosed
	
	Where
	SalesOrderDetailId = @mSalesOrderDetailId
END

IF @mAction='LOADOUTST'
BEGIN
	Select *
                       from KHLIOutstanding 
	--Where SalesOrderDate >= @mFromDate
	Order By PKID
END
--Select * from SolarOutstanding4SGM
--Select Top 3 * from SalesOrderDetails where Shipper = 'SSPL'
--Select Top 3 * from SalesOrder where Shipper = 'SSPL'
--Select * from PackingDetail
--Select * from Materials where materialcode = 'SOL-SYN-PV-0058'

GO
