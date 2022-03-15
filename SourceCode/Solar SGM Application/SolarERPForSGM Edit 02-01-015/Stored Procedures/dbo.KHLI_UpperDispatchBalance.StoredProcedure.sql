USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[KHLI_UpperDispatchBalance]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc KHLI_UpperDispatchBalance

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[KHLI_UpperDispatchBalance]
@mAction				varchar(20)	='SELALL',
@mPKID					int			= NULL,
@mCustomerName			varchar(100)= NULL,
@mJobcardNo				varchar(50)= NULL,
@mArticleName			varchar(100)= NULL,
@mArticleColour			varchar(50)= NULL,
@mDescription			varchar(50)= NULL,
@mDeliveryChallanNo		varchar(50)= NULL,
@mDeliveryDate			date= NULL,
@mSize01				varchar(5)= NULL,
@mSize02				varchar(5)= NULL,
@mSize03				varchar(5)= NULL,
@mSize04				varchar(5)= NULL,
@mSize05				varchar(5)= NULL,
@mSize06				varchar(5)= NULL,
@mSize07				varchar(5)= NULL,
@mSize08				varchar(5)= NULL,
@mSize09				varchar(5)= NULL,
@mSize10				varchar(5)= NULL,
@mSize11				varchar(5)= NULL,
@mSize12				varchar(5)= NULL,
@mSize13				varchar(5)= NULL,
@mSize14				varchar(5)= NULL,
@mSize15				varchar(5)= NULL,
@mSize16				varchar(5)= NULL,
@mSize17				varchar(5)= NULL,
@mSize18				varchar(5)= NULL,
@mSize19				varchar(5)= NULL,
@mSize20				varchar(5)= NULL,
@mSize21				varchar(5)= NULL,
@mSize22				varchar(5)= NULL,
@mSize23				varchar(5)= NULL,
@mSize24				varchar(5)= NULL,
@mQty01					int= NULL,
@mQty02					int= NULL,
@mQty03					int= NULL,
@mQty04					int= NULL,
@mQty05					int= NULL,
@mQty06					int= NULL,
@mQty07					int= NULL,
@mQty08					int= NULL,
@mQty09					int= NULL,
@mQty10					int= NULL,
@mQty11					int= NULL,
@mQty12					int= NULL,
@mQty13					int= NULL,
@mQty14					int= NULL,
@mQty15					int= NULL,
@mQty16					int= NULL,
@mQty17					int= NULL,
@mQty18					int= NULL,
@mQty19					int= NULL,
@mQty20					int= NULL,
@mQty21					int= NULL,
@mQty22					int= NULL,
@mQty23					int= NULL,
@mQty24					int= NULL,
@mTotalQty				int= NULL,
@mSizeName				Varchar(50)=Null,
@mUpdatedOn				Varchar(50)=Null,
@mDCNO					Varchar(50)=Null
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELJCANDSIZE'
BEGIN
	SELECT DISTINCT TOP (100) PERCENT dbo.MaterialIssues.JobcardNo, dbo.SalesOrder.SizeName
	FROM         dbo.MaterialIssues INNER JOIN
                      dbo.SalesOrder ON dbo.MaterialIssues.SalesOrderNo = dbo.SalesOrder.SalesOrderNo
	WHERE     (dbo.MaterialIssues.TransactionType = 'UPPERDESPATCHCHALLAN')
	ORDER BY dbo.SalesOrder.SizeName, dbo.MaterialIssues.JobcardNO
END

IF @mAction='SELSIZE'
BEGIN
	Select * from SizeMaster where SizeName = @mSizeName Order By SortSequenceNo
END

IF @mAction='SELJC'
BEGIN
	Select * from JobcardDetail where jobcardNo = @mjobcardNo And ComponentGroup ='upper'
END

If @mAction='INSERT'
BEGIN

	INSERT INTO
	UpperDispatchBalance
	
	VALUES
	(
	@mCustomerName,			@mJobcardNo,		@mArticleName,		@mArticleColour,		@mDescription,		@mDeliveryChallanNo,
	@mDeliveryDate,			@mSizeName,
	@mSize01,		@mSize02,		@mSize03,		@mSize04,		@mSize05,		@mSize06,		@mSize07,		@mSize08,
	@mSize09,		@mSize10,		@mSize11,		@mSize12,		@mSize13,		@mSize14,		@mSize15,		@mSize16,
	@mSize17,		@mSize18,		@mSize19,		@mSize20,		@mSize21,		@mSize22,		@mSize23,		@mSize24,
	@mQty01,		@mQty02,		@mQty03,		@mQty04,		@mQty05,		@mQty06,		@mQty07,		@mQty08,
	@mQty09,		@mQty10,		@mQty11,		@mQty12,		@mQty13,		@mQty14,		@mQty15,		@mQty16,
	@mQty17,		@mQty18,		@mQty19,		@mQty20,		@mQty21,		@mQty22,		@mQty23,		@mQty24,
	@mTotalQty,		@mUpdatedOn)
END

If @mAction='SELDCCOUNT'
BEGIN
	Select SupplierRefNo,Sum(IssueQuantity) As DCQty from MaterialIssues where transactiontype = 'UPPERDESPATCHCHALLAN'  aND JobcardNo = @mJobcardNo And ToLocation IN ('KFD','KFE')
	Group By SupplierRefNo
	Order By SupplierRefNo
END

If @mAction='SELDCQTY'
BEGIN
	Select * from MaterialIssues where transactiontype = 'UPPERDESPATCHCHALLAN'  aND JobcardNo = @mJobcardNo And SupplierRefNo = @mDCNO aND ToLocation IN ('KFD','KFE')
	Order By SupplierRefNo
END

IF @mAction='LOADBALQTY'
BEGIN
	Select Description,
	Qty01,Qty02,Qty03,Qty04,Qty05,Qty06,Qty07,Qty08,Qty09,Qty10,Qty11,Qty12,Qty13,Qty14,Qty15,Qty16,Qty17,Qty18,Qty19,Qty20,Qty21,Qty22,Qty23,Qty24,TotalQty
	from UpperDispatchBalance where JobcardNo = @mJobcardNo And Description = 'Jobcard Qty'
	
	Union
	
	Select Description,
	Sum(Qty01) As DCQty01,Sum(Qty02) As DCQty02,Sum(Qty03) As DCQty03,Sum(Qty04) As DCQty04,Sum(Qty05) As DCQty05,Sum(Qty06) As DCQty06,Sum(Qty07) As DCQty07,
	Sum(Qty08) As DCQty08,Sum(Qty09) As DCQty09,Sum(Qty10) As DCQty10,Sum(Qty11) As DCQty11,Sum(Qty12) As DCQty12,Sum(Qty13) As DCQty13,Sum(Qty14) As DCQty14,
	Sum(Qty15) As DCQty15,Sum(Qty16) As DCQty16,Sum(Qty17) As DCQty17,Sum(Qty18) As DCQty18,Sum(Qty19) As DCQty19,Sum(Qty20) As DCQty20,Sum(Qty21) As DCQty21,
	Sum(Qty22) As DCQty22,Sum(Qty23) As DCQty23,Sum(Qty24) As DCQty24,Sum(TotalQty) As TotalQty
	from UpperDispatchBalance where JobcardNo = @mJobcardNo And Description = 'DC Qty' Group By Description

END

--Select * from SalesOrder where SalesOrderNo = 'S-U-AW14-001-01'
--sELECT * FROM JOBCARDDETAIL WHERE JOBCARDNO LIKE 'S-F-SS15-013-01-015-01'
--Select Size As s,* from MaterialIssues where transactiontype = 'UPPERDESPATCHCHALLAN'  aND JobcardNo = 'S-F-SS15-010-01-008-05' And FromStage = 'PAC'
--Order By Size

GO
