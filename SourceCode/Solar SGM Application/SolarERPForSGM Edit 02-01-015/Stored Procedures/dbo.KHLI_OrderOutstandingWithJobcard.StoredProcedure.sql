USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[KHLI_OrderOutstandingWithJobcard]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc KHLI_OrderOutstandingWithJobcard

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[KHLI_OrderOutstandingWithJobcard]
@mAction				varchar(20)	='SELALL',
@mPKId					int		=Null,
@mFromDate				Datetime	=Null,
@mToDate				DateTime	=Null,
@mSalesOrderDetailId	varchar(50) =NULL,
@mSalesOrderDate		datetime	 =NULL,
@mCustomerName			varchar	(100) =NULL,
@mSalesOrderNo			varchar	(50) =NULL,
@mSalesOrderDetail		varchar	(50) =NULL,
@mJobcardNo				Varchar (50) =NULL,
@mPlanNo				Varchar (50) =NULL,
@mPlanDate				Date		 =NULL,
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
@mJobcardQuantity		int  =NULL,
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
@mLeatherCode			Varchar(50)	=Null,
@mBuyerGroupCode		Varchar(100)=Null,
@mProcessDate			Datetime	=Null,
@mFKJocardId			Varchar(100)=Null,
@mLastTransactionDate	Datetime	=Null,
@mRecordsUpdated		Int			=Null,
@mLastTranId			Varchar(50)	=Null
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELUPDDATE'
BEGIN
	Select Distinct(UpdatedOn) As UpdatedOn from [KHLIOutstandingWithJC] Order By UpdatedOn Desc
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
	JD.Id,					jd.JobcardDate,			jd.BuyergroupCode,		jd.SalesOrderNo,		jd.CustWorkOrderNo As SalesOrderDetail,
	jd.JobcardNo,			so.BuyerOrderNo,		jd.CustomerOrderNo,		so.Articlegroup,		jd.Article,
	sd.ColorName As Color,	so.Destination,			sd.BuyerDeliveryDate,	sd.Season,				sd.UserCategory,
	sd.Variant,				sd.ColorCode,			jd.MainRawMaterialCode,	jd.Quantity

	From 
	JobcardDetail as JD,	SalesOrderDetails As sd,	SalesOrder As so
	
	Where
	jd.CustWorkOrderNo = sd.CustWorkOrderNo And sd.SalesOrderID = so.Id And jd.ComponentGroup = 'UPPER' --And jd.BuyerGroupCode = 'ASTROMUELL'
	And jd.JobcardNo like  ('%AW15%') And sd.OrderStatus <> 'CANCEL'
	
	GROUP BY
	JD.ID,					JD.JobCardDate,			JD.BuyerGroupCode,		JD.SalesOrderNo,		JD.CustWorkOrderNo,
	JD.JobCardNo,			so.BuyerOrderNo,		JD.CustomerOrderNo,		so.ArticleGroup,		JD.Article,				sd.ColorName,
	so.Destination,			sd.BuyerDeliveryDate,	sd.season,				sd.UserCategory,		sd.Variant,				sd.ColorCode,
	JD.MainRawMaterialCode,	JD.Quantity

	Order by
	jd.JobcardNo

END

IF @mAction='SELCUSTORD'
BEGIN
	Select 
	JD.Id,					jd.JobcardDate,			jd.BuyergroupCode,		jd.SalesOrderNo,		jd.CustWorkOrderNo As SalesOrderDetail,
	jd.JobcardNo,			so.BuyerOrderNo,		jd.CustomerOrderNo,		so.Articlegroup,		jd.Article,
	sd.ColorName As Color,	so.Destination,			sd.BuyerDeliveryDate,	sd.Season,				sd.UserCategory,
	sd.Variant,				sd.ColorCode,			jd.MainRawMaterialCode,	jd.Quantity

	From 
	JobcardDetail as JD,	SalesOrderDetails As sd,	SalesOrder As so
	
	Where
	jd.CustWorkOrderNo = sd.CustWorkOrderNo And sd.SalesOrderID = so.Id And jd.ComponentGroup = 'UPPER' And jd.BuyerGroupCode = @mBuyerGroupCode
	--And jd.JobcardNo like  ('%AW15%') 
	And sd.OrderStatus <> 'CANCEL' And jd.InternalJobcardno <> ''
	
	GROUP BY
	JD.ID,					JD.JobCardDate,			JD.BuyerGroupCode,		JD.SalesOrderNo,		JD.CustWorkOrderNo,
	JD.JobCardNo,			so.BuyerOrderNo,		JD.CustomerOrderNo,		so.ArticleGroup,		JD.Article,				sd.ColorName,
	so.Destination,			sd.BuyerDeliveryDate,	sd.season,				sd.UserCategory,		sd.Variant,				sd.ColorCode,
	JD.MainRawMaterialCode,	JD.Quantity

	Order by
	jd.JobcardNo

END
--Select * from SalesOrderDetails
IF @mAction='SELCUSTSOD'
BEGIN
	Select 
	JD.Id,					jd.JobcardDate,			jd.BuyergroupCode,		jd.SalesOrderNo,		jd.CustWorkOrderNo As SalesOrderDetail,
	jd.JobcardNo,			so.BuyerOrderNo,		jd.CustomerOrderNo,		so.Articlegroup,		jd.Article,
	sd.ColorName As Color,	so.Destination,			sd.BuyerDeliveryDate,	sd.Season,				sd.UserCategory,
	sd.Variant,				sd.ColorCode,			jd.MainRawMaterialCode,	jd.Quantity

	From 
	JobcardDetail as JD,	SalesOrderDetails As sd,	SalesOrder As so
	
	Where
	jd.CustWorkOrderNo = sd.CustWorkOrderNo And sd.SalesOrderID = so.Id And jd.ComponentGroup = 'UPPER' And jd.BuyerGroupCode = @mBuyerGroupCode
	And sd.OrderStatus <> 'CANCEL' And sd.SalesOrderNo = @mSalesOrderNo
	--And sd.SalesOrderNo in 
	--(Select * from TempSalesOrder)
	
	GROUP BY
	JD.ID,					JD.JobCardDate,			JD.BuyerGroupCode,		JD.SalesOrderNo,		JD.CustWorkOrderNo,
	JD.JobCardNo,			so.BuyerOrderNo,		JD.CustomerOrderNo,		so.ArticleGroup,		JD.Article,				sd.ColorName,
	so.Destination,			sd.BuyerDeliveryDate,	sd.season,				sd.UserCategory,		sd.Variant,				sd.ColorCode,
	JD.MainRawMaterialCode,	JD.Quantity

	Order by
	jd.JobcardNo

END

IF @mAction='SELPRODORD'
BEGIN
	Select 
	JD.Id,					jd.JobcardDate,			jd.BuyergroupCode,		jd.SalesOrderNo,		jd.CustWorkOrderNo As SalesOrderDetail,
	jd.JobcardNo,			so.BuyerOrderNo,		jd.CustomerOrderNo,		so.Articlegroup,		jd.Article,
	sd.ColorName As Color,	so.Destination,			sd.BuyerDeliveryDate,	sd.Season,				sd.UserCategory,
	sd.Variant,				sd.ColorCode,			jd.MainRawMaterialCode,	jd.Quantity

	From 
	JobcardDetail as JD,	SalesOrderDetails As sd,	SalesOrder As so
	
	Where
	jd.CustWorkOrderNo = sd.CustWorkOrderNo And sd.SalesOrderID = so.Id And jd.ComponentGroup = 'UPPER' And jd.BuyerGroupCode = 'BEXLEY'--jd.JobcardNo in ('K-F-SS15-026-10-002-01','K-F-SS15-026-06-008-01')
	
	--jd.JobcardNo in 
	--(
	--Select JobcardNo from JobCardDetail where jobcarddate >= '2015-01-12' And shipper in ('KHLI','SLI')  -- 17
	--UNION ALL
	--Select WorkOrderNo  from ProductionByProcess where ProcessDate >= '2015-01-12' And Companycode in ('KHLI','SLI')  -- 17
	--UNION ALL
	--Select Jobcardno from jobcarddetailperpair where BarCode in (
	--SELECT Barcode From AHGroup_SSPLProduction.dbo.jobcardpairwise where CreatedDate >= '2015-01-12')
	--	) 
		
	GROUP BY
	JD.ID,					JD.JobCardDate,			JD.BuyerGroupCode,		JD.SalesOrderNo,		JD.CustWorkOrderNo,
	JD.JobCardNo,			so.BuyerOrderNo,		JD.CustomerOrderNo,		so.ArticleGroup,		JD.Article,				sd.ColorName,
	so.Destination,			sd.BuyerDeliveryDate,	sd.season,				sd.UserCategory,		sd.Variant,				sd.ColorCode,
	JD.MainRawMaterialCode,	JD.Quantity

	Order by
	jd.JobcardNo
END

IF @mAction='SELJOBCARD'
BEGIN
	Select * from jobcarddetail where JobcardNo like left(@mSalesOrderDetail,19)+'%' And ComponentGroup = 'UPPER'
END

IF @mAction='SELSTAGES'
BEGIN
	Select * from ProductionStages Where ProductionName = @mProductionName ORDER BY ProductionType Desc,SequenceNo
END

IF @mAction='SELPRODQTY'
BEGIN
	Select IsNull(Sum(Quantity) - (Select IsNull(Sum(Quantity),0) from productstock where WorkOrderNo = @mWorkOrderNo And Stage = @mProcessName
	),0) As ProdQty From ProductionByProcess
	Where ProcessName = @mProcessName And WorkOrderNo = @mWorkOrderNo
END

IF @mAction='SELCUTTING'
BEGIN 
	Select IsNull(Sum(CutterTicketQty),0) As ProdQty from Cutterticketdetails
	Where JobcardNo = @mWorkOrderNo And MaterialCode = @mLeatherCode And CutterTicketStatus = 'Closed'
END

IF @mAction='UPPDISPATCH'
BEGIN
	Select IsNull(Sum(IssueQuantity),0) As DispQuantity From MaterialIssues
	Where TransactionType = 'UPPERDESPATCHCHALLAN' And WorkOrderNo = @mWorkOrderNo
	--Select * from MaterialIssues order by createddate desc
END


IF @mAction='SELFSKIT'
BEGIN
	SELECT COUNT(STAGE) As ConvIn From AHGroup_SSPLProduction.dbo.jobcardpairwise
	WHERE barcode in (
		SELECT Barcode from jobcarddetailperpair where jobcardno = @mWorkOrderNo)
	AND STAGE = 'KITTING'
END

IF @mAction='SELFSFEED'
BEGIN
	Select IsNull(Sum(Quantity) - (Select IsNull(Sum(Quantity),0) from productstock where WorkOrderNo = @mWorkOrderNo And Stage = 'KITTING'
	),0) As ProdQty From ProductionByProcess
	Where ProcessName = 'KITTING' And WorkOrderNo = @mWorkOrderNo
END

IF @mAction='SELFSCONVIN'
BEGIN
	SELECT COUNT(STAGE) As ConvIn From AHGroup_SSPLProduction.dbo.jobcardpairwise
	WHERE barcode in (
		SELECT Barcode from jobcarddetailperpair where jobcardno = @mWorkOrderNo)
	AND STAGE = 'CONVIN'
END

IF @mAction='SELFSCONVOUT'
BEGIN
	SELECT COUNT(STAGE) As ConvIn From AHGroup_SSPLProduction.dbo.jobcardpairwise
	WHERE barcode in (
		SELECT Barcode from jobcarddetailperpair where jobcardno = @mWorkOrderNo)
	AND STAGE = 'CONVOUT'
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
	--Where JobcardNo in 
	--(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) 
	Where CustWorkOrderNo = @mSalesOrderDetailId
END

IF @mAction='SELSOD'
BEGIN
	SELECT * FROM [KHLIOutstandingWithJC] Where SalesOrderDetailID = @mSalesOrderDetailID
END

IF @mAction='INSSTATUS'
BEGIN
	INSERT INTO KHLIOutstandingWithJC
	
	VALUES
	(
	@mSalesOrderDetailId,		@mSalesOrderDate,		@mCustomerName,		@mSalesOrderNo,		@mSalesOrderDetail,		@mJobcardNo,
	@mPlanNo,					@mPlanDate,				@mBuyerOrderNo,
	@mCustomerOrderNo,			@mArticleGroup,			@mArticleName,		@mColour,			@mDestination,			@mBuyerDeliveryDate,
	@mSeason,					@mUserCategory,			@mProductionName,	@mJobcardQuantity,	@mUpperAndLiningCutting,@mUpperAndLiningCuttingBal,	
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
	KHLIOutstandingWithJC
	
	SET
	UpperAndLiningCutting = @mUpperAndLiningCutting,	UpperAndLiningCuttingBal = @mUpperAndLiningCuttingBal,	
	PreFitting = @mPreFitting,							PreFittingBal = @mPreFittingBal,
	SocksPrepration = @mSocksPrepration,				SocksPreprationBal = @mSocksPreprationBal,
	UpperConveyorIn = @mUpperConveyorIn,				UpperConveyorInBal = @mUpperConveyorInBal,
	UpperConveyorOut = @mUpperConveyorOut,				UpperConveyorOutBal = @mUpperConveyorOutBal,
	HandStitching = @mHandStitching,					HasndStitchingBal = @mHasndStitchingBal,
	Forming = @mForming,								FormingBal = @mFormingBal,
	Finishing = @mFinishing,							FinishingBal = @mFinishingBal,
	UpperPacking = @mUpperPacking,						UpperPackingBal = @mUpperPackingBal,
	UpperDispatch = @mUpperDispatch,					UpperDispatchBal = @mUpperDispatchBal,
	Feeding = @mFeeding,								FeedingBal = @mFeedingBal,
	Kitting = @mKitting,								KittingBal = @mKittingBal,
	ConveyorIn = @mConveyorIn,							ConveyorInBal = @mConveyorInBal,
	ConveyorOut = @mConveyorOut,						ConveyorOutBal = @mConveyorOutBal,
	Packing = @mPacking,								PackingBal = @mPackingBal,
	Dispatch = @mDispatch,								DispatchBal = @mDispatchBal,
	UpdatedOn = @mUpdatedOn,							IsCompleted = @mIsCompleted,
	IsClosed = @mIsClosed
	
	
	Where
	SalesOrderDetailId = @mSalesOrderDetailId
END

IF @mAction='LOADOUTST'
BEGIN
	Select *
                       from KHLIOutstandingWithJC 
	--Where SalesOrderDate >= @mFromDate
	Order By PKID
END


If @mAction='LoadCust'
BEGIN
	Select Distinct(BuyerGroupCode) From JobcardDetail where shipper in ('KHLI','SLI') ORDER BY BUYERGROUPCODE
END

If @mAction='LoadCust'
BEGIN
	Select Distinct(BuyerGroupCode) From JobcardDetail where shipper in ('KHLI','SLI') ORDER BY BUYERGROUPCODE
END
--Select * from SalesOrder
If @mAction='LOADORD'
BEGIN
	Select Distinct(SalesOrderNo) From SalesOrder where BuyerGroupCode = @mBuyerGroupCode And
	shipper in ('KHLI','SLI') ORDER BY SalesOrderNo
END


IF @mAction='SELCURRORD'
BEGIN
	Select 
	JD.Id,					jd.JobcardDate,			jd.BuyergroupCode,		jd.SalesOrderNo,		jd.CustWorkOrderNo As SalesOrderDetail,
	jd.JobcardNo,			so.BuyerOrderNo,		jd.CustomerOrderNo,		so.Articlegroup,		jd.Article,
	sd.ColorName As Color,	so.Destination,			sd.BuyerDeliveryDate,	sd.Season,				sd.UserCategory,
	sd.Variant,				sd.ColorCode,			jd.MainRawMaterialCode,	jd.Quantity

	From 
	JobcardDetail as JD,	SalesOrderDetails As sd,	SalesOrder As so
	
	Where
	jd.CustWorkOrderNo = sd.CustWorkOrderNo And sd.SalesOrderID = so.Id And jd.ComponentGroup = 'UPPER' --And jd.BuyerGroupCode = 'ASTROMUELL'
	And jd.JobcardNo like  ('%AW15%') And sd.OrderStatus <> 'CANCEL' And jd.JobcardNo in 
	(Select Distinct(WorkOrderNo) from ProductionByProcess where CreatedDate >= @mProcessDate And CompanyCode in ('SLI','KHLI'))
	
	GROUP BY
	JD.ID,					JD.JobCardDate,			JD.BuyerGroupCode,		JD.SalesOrderNo,		JD.CustWorkOrderNo,
	JD.JobCardNo,			so.BuyerOrderNo,		JD.CustomerOrderNo,		so.ArticleGroup,		JD.Article,				sd.ColorName,
	so.Destination,			sd.BuyerDeliveryDate,	sd.season,				sd.UserCategory,		sd.Variant,				sd.ColorCode,
	JD.MainRawMaterialCode,	JD.Quantity

	Order by
	jd.JobcardNo

END

IF @mAction='SELALLTRAN'
BEGIN
	Select Top 1 * from ProductionByProcess where CompanyCode in ('SLI','KHLI') Order By CreatedDate desc,id 
END

If @mAction= 'LOADPKDINFO'
BEGIN
	select * from Outercartontoimport where fkjobcardId = @mFKJocardId And PackingCompletedOn  <> '' order by BoxSlNo
END

If @mAction= 'LOADPKDINFOALL'
BEGIN
	select * from Outercartontoimport where fkjobcardId = @mFKJocardId order by BoxSlNo
END

If @mAction= 'TOTALCARTON'
BEGIN
	select * from Outercartontoimport where fkjobcardId = @mFKJocardId order by BoxSlNo
END

If @mAction= 'PACKEDCARTON'
BEGIN
	select * from Outercartontoimport where fkjobcardId = @mFKJocardId And PackingCompletedOn  <> '' order by BoxSlNo
END

If @mAction= 'BALANCECARTON'
BEGIN
	select * from Outercartontoimport where fkjobcardId = @mFKJocardId And PackingCompletedOn  = '' order by BoxSlNo
END

IF @mAction='LASTTRANON'
BEGIN
	Select Top 1 * from OrderOutstandingTill Order by pkid desc
END

IF @mAction='INSTRANON'
BEGIN
	Insert Into
	OrderOutstandingTill
	
	VALUES
	(@mLastTransactionDate, @mRecordsUpdated,	@mLastTranId,@mUpdatedOn)
END

GO
