USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_PackingEntries]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- drop Proc proc_PackingEntries

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_PackingEntries]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mID							varchar(50) 	=Null,
@minvoiceno						varchar(25) 	=Null,
@mInvoiceDate					datetime 		=Null,
@mInvoiceSerialNo				varchar(10) 	=Null,
@mbuyer							varchar(20) 	=Null,
@mshipper						varchar(20) 	=Null,
@mSalesOrderNo					varchar(25) 	=Null,
@mArticleNo						varchar(250) 	=Null,
@mrate							decimal(14, 2) 	=Null,
@mquantity						decimal(14, 2) 	=Null,
@mBuyerGroup					varchar(50) 	=Null,
@mCreatedBy						varchar(100) 	=Null,
@mCreatedDate					datetime 		=Null,
@mModifiedBy					varchar(100) 	=Null,
@mModifiedDate					datetime 		=Null,
@mEnteredOnMachineID			varchar(50) 	=Null,
@mPackNo						varchar(100) 	=Null,
@mJobCardNo						varchar(50) 	=Null,
@mIsApproved					bit 			=Null,
@mApprovedBy					varchar(50) 	=Null,
@mApprovedOn					datetime 		=Null,
@mModuleName					varchar(50) 	=Null,
@mJobCardDetailID				varchar(50) 	=Null,
@mInvoiceID						varchar(50) 	=Null,
@mSalesOrderDetailID			varchar(50) 	=Null,
@mSize01						varchar(7) 		=Null,
@mQuantity01					decimal(14, 2) 	=Null,
@mSize02						varchar(7) 		=Null,
@mQuantity02					decimal(14, 2) 	=Null,
@mSize03						varchar(7) 		=Null,
@mQuantity03					decimal(14, 2) 	=Null,
@mSize04						varchar(7) 		=Null,
@mQuantity04					decimal(14, 2) 	=Null,
@mSize05						varchar(7) 		=Null,
@mQuantity05					decimal(14, 2) 	=Null,
@mSize06						varchar(7) 		=Null,
@mQuantity06					decimal(14, 2) 	=Null,
@mSize07						varchar(7) 		=Null,
@mQuantity07					decimal(14, 2) 	=Null,
@mSize08						varchar(7) 		=Null,
@mQuantity08					decimal(14, 2) 	=Null,
@mSize09						varchar(7) 		=Null,
@mQuantity09					decimal(14, 2) 	=Null,
@mSize10						varchar(7) 		=Null,
@mQuantity10					decimal(14, 2) 	=Null,
@mSize11						varchar(7) 		=Null,
@mQuantity11					decimal(14, 2) 	=Null,
@mSize12						varchar(7) 		=Null,
@mQuantity12					decimal(14, 2) 	=Null,
@mSize13						varchar(7) 		=Null,
@mQuantity13					decimal(14, 2) 	=Null,
@mSize14						varchar(7) 		=Null,
@mQuantity14					decimal(14, 2) 	=Null,
@mSize15						varchar(7) 		=Null,
@mQuantity15					decimal(14, 2) 	=Null,
@mSize16						varchar(7) 		=Null,
@mQuantity16					decimal(14, 2) 	=Null,
@mSize17						varchar(7) 		=Null,
@mQuantity17					decimal(14, 2) 	=Null,
@mSize18						varchar(7) 		=Null,
@mQuantity18					decimal(14, 2) 	=Null,
@mReadyToDispatchDate			Datetime		=Null,
@mCartonNo						Int				=Null,
@mSpoolId						Varchar(25)		=Null,
@mSpoolDt						Datetime		=Null,
@mScanDate						Datetime		=Null,
@mFileName						Varchar(100)	=Null,
@mNumberofBoxes					Int				=Null,
@mPerfectBoxes					Int				=Null,
@mHID							Varchar(50)		=Null,
@mBarcode						Varchar(50)		=Null,
@mStatus						Varchar(50)		=Null,
@mPackedQuantity				Int				=Null
 	


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

If @mAction= 'SELBUYERS'
BEGIN
	SELECT DISTINCT TOP (100) PERCENT PKD.BuyerCode, PKD.BuyerGroupCode, BUY.BuyerName, BUY.Outlet,
	IsNull((Select Sum(Quantity) As PkdQty from Packingdetail where BuyerCode = PKD.BuyerCode And  ReadytoDispatch = '1'  And InvoiceNO Is Null),0) As PkdQty
		FROM            dbo.PackingDetail AS PKD INNER JOIN
                         dbo.Buyer AS BUY ON PKD.BuyerCode = BUY.BuyerCode
	WHERE        (PKD.PackingDate >= '2018-09-01')
	ORDER BY PKD.BuyerCode, PKD.BuyerGroupCode
	
	
END	

If @mAction = 'SELRDYTODIS'
BEGIN
	SELECT
	*

	FROM
	ReadyToDispatch

	WHERE
	JobcardNo = @mJobcardNo And
	InvoiceNo = ''

END

If @mAction='INSRDYTODIS'
BEGIN
	INSERT INTO
	ReadyToDispatch

	VALUES
	(
		@mID,					@minvoiceno,			@mInvoiceDate,			@mInvoiceSerialNo,		@mbuyer,
		@mshipper,				@mSalesOrderNo,			@mArticleNo,			@mrate,					@mquantity,
		@mBuyerGroup,			@mCreatedBy,			@mCreatedDate,			@mModifiedBy,			@mModifiedDate,
		@mEnteredOnMachineID,	@mPackNo,				@mJobCardNo,			@mIsApproved,			@mApprovedBy,
		@mApprovedOn,			@mModuleName,			@mJobCardDetailID,		@mInvoiceID,			@mSalesOrderDetailID,
		@mSize01,				@mQuantity01,			@mSize02,				@mQuantity02,			@mSize03,
		@mQuantity03,			@mSize04,				@mQuantity04,			@mSize05,				@mQuantity05,
		@mSize06,				@mQuantity06,			@mSize07,				@mQuantity07,			@mSize08,
		@mQuantity08,			@mSize09,				@mQuantity09,			@mSize10,				@mQuantity10,
		@mSize11,				@mQuantity11,			@mSize12,				@mQuantity12,			@mSize13,
		@mQuantity13,			@mSize14,				@mQuantity14,			@mSize15,				@mQuantity15,
		@mSize16,				@mQuantity16,			@mSize17,				@mQuantity17,			@mSize18,
		@mQuantity18,			@mSpoolId,				@mSpoolDt			
	)
END

If @mAction='UPDRDYTODIS'
BEGIN
	UPDATE
	ReadyToDispatch

	SET
		Quantity = @mquantity,
		Quantity01 = @mQuantity01,				Quantity02 = @mQuantity02,				Quantity03 = @mQuantity03,
		Quantity04 = @mQuantity04,				Quantity05 = @mQuantity05,				Quantity06 = @mQuantity06,
		Quantity07 = @mQuantity07,				Quantity08 = @mQuantity08,				Quantity09 = @mQuantity09,
		Quantity10 = @mQuantity10,				Quantity11 = @mQuantity11,				Quantity12 = @mQuantity12,
		Quantity13 = @mQuantity13,				Quantity14 = @mQuantity14,				Quantity15 = @mQuantity15,
		Quantity16 = @mQuantity16,				Quantity17 = @mQuantity17,				Quantity18 = @mQuantity18

	WHERE
	ID = @mID
	
END

If @mAction='UPDPKGDTL'
BEGIN
	UPDATE PackingDetail

	SET
	ReadyToDispatch = '1',	ReadyToDispatchDate = @mReadyToDispatchDate,
	PackedQuantity = @mPackedQuantity



	Where
	JobcardNo = @mJobCardNo And
	CartonNo = @mCartonNo

END

If @mAction='SELRDYTODISSUM'
BEGIN
	Select BuyerGroup,Buyer,JobcardNo,ArticleNo,quantity
	From ReadyToDispatch
	Where invoiceno = ''
	And BuyerGroup = @mBuyerGroup
	Order By JobcardNo,ArticleNo
END

If @mAction='SELRDYTODISDTL'
BEGIN
	SELECT
		ID,				JobcardNo,			CartonNo,		quantity,
		Size01,			Quantity01,			Size02,			Quantity02,			Size03,			Quantity03,
		Size04,			Quantity04,			Size05,			Quantity05,			Size06,			Quantity06,
		Size07,			Quantity07,			Size08,			Quantity08,			Size09,			Quantity09,
		Size10,			Quantity10,			Size11,			Quantity11,			Size12,			Quantity12,
		Size13,			Quantity13,			Size14,			Quantity14,			Size15,			Quantity15,
		Size16,			Quantity16,			Size17,			Quantity17,			Size18,			Quantity18
	
	FROM
	PackingDetail
	Where (Invoiceno IS NULL or InvoiceNo = '') And ReadyToDispatch = '1'
	And JobcardNo = @mJobCardNo
	Order By JobcardNo,CartonNo
END

If @mAction='SELPKGDTL'
BEGIN
	SELECT
	BuyerGroupCode,CartonNo,	Quantity,
	Size01,		Quantity01,			Size02,			Quantity02,			Size03,			Quantity03,	
	Size04,		Quantity04,			Size05,			Quantity05,			Size06,			Quantity06,
	Size07,		Quantity07,			Size08,			Quantity08,			Size09,			Quantity09,
	Size10,		Quantity10,			Size11,			Quantity11,			Size12,			Quantity12,
	Size13,		Quantity13,			Size14,			Quantity14,			Size15,			Quantity15,
	Size16,		Quantity16,			Size17,			Quantity17,			Size18,			Quantity18


	FROM
	PackingDetail
	
	WHERE 
	JobCardNo = @mJobcardNo 
	
	Order by
	CartonNo
END


If @mAction='SELPKGSUM'
BEGIN
	SELECT
		JobCardNo,									IsNull(Count(CartonNo),0) AS NoofCarton,	IsNull(SUM(Quantity),0) AS PkgStock, 
		IsNull(SUM(Quantity01),0) AS Quantity01,	IsNull(SUM(Quantity02),0) AS Quantity02,	IsNull(SUM(Quantity03),0) AS Quantity03, 
		IsNull(SUM(Quantity04),0) AS Quantity04,	IsNull(SUM(Quantity05),0) AS Quantity05,	IsNull(SUM(Quantity06),0) AS Quantity06, 
		IsNull(SUM(Quantity07),0) AS Quantity07,	IsNull(SUM(Quantity08),0) AS Quantity08,	IsNull(SUM(Quantity09),0) AS Quantity09, 
		IsNull(SUM(Quantity10),0) AS Quantity10,	IsNull(SUM(Quantity11),0) AS Quantity11,	IsNull(SUM(Quantity12),0) AS Quantity12, 
		IsNull(SUM(Quantity13),0) AS Quantity13,	IsNull(SUM(Quantity14),0) AS Quantity14,	IsNull(SUM(Quantity15),0) AS Quantity15, 
		IsNull(SUM(Quantity16),0) AS Quantity16,	IsNull(SUM(Quantity17),0) AS Quantity17,	IsNull(SUM(Quantity18),0) AS Quantity18
	FROM
		PackingDetail 
	WHERE
		(WIPLocation = 'PACKING') AND (ReadyToDispatch = '1')  AND (invoiceno = '' oR InvoiceNo is null)
		And JobcardNo = @mJobcardNo
	GROUP BY JobCardNo
	ORDER BY JobCardNo
END

If @mAction='SELPKGSUML'
BEGIN
	SELECT
		JobCardNo,									IsNull(Count(CartonNo),0) AS NoofCarton,	IsNull(SUM(Quantity),0) AS PkgStock, 
		IsNull(SUM(Quantity01),0) AS Quantity01,	IsNull(SUM(Quantity02),0) AS Quantity02,	IsNull(SUM(Quantity03),0) AS Quantity03, 
		IsNull(SUM(Quantity04),0) AS Quantity04,	IsNull(SUM(Quantity05),0) AS Quantity05,	IsNull(SUM(Quantity06),0) AS Quantity06, 
		IsNull(SUM(Quantity07),0) AS Quantity07,	IsNull(SUM(Quantity08),0) AS Quantity08,	IsNull(SUM(Quantity09),0) AS Quantity09, 
		IsNull(SUM(Quantity10),0) AS Quantity10,	IsNull(SUM(Quantity11),0) AS Quantity11,	IsNull(SUM(Quantity12),0) AS Quantity12, 
		IsNull(SUM(Quantity13),0) AS Quantity13,	IsNull(SUM(Quantity14),0) AS Quantity14,	IsNull(SUM(Quantity15),0) AS Quantity15, 
		IsNull(SUM(Quantity16),0) AS Quantity16,	IsNull(SUM(Quantity17),0) AS Quantity17,	IsNull(SUM(Quantity18),0) AS Quantity18
	FROM
		PackingDetail 
	WHERE
		(ReadyToDispatch = '1')  AND (invoiceno = '' oR InvoiceNo is null)
		And JobcardNo = @mJobcardNo
	GROUP BY JobCardNo
	ORDER BY JobCardNo
END

If @mAction='UPDRDYTODISBYJC'
BEGIN
	UPDATE
	ReadyToDispatch

	SET
		Quantity = @mquantity,					PackNo = @mPackNo,
		Quantity01 = @mQuantity01,				Quantity02 = @mQuantity02,				Quantity03 = @mQuantity03,
		Quantity04 = @mQuantity04,				Quantity05 = @mQuantity05,				Quantity06 = @mQuantity06,
		Quantity07 = @mQuantity07,				Quantity08 = @mQuantity08,				Quantity09 = @mQuantity09,
		Quantity10 = @mQuantity10,				Quantity11 = @mQuantity11,				Quantity12 = @mQuantity12,
		Quantity13 = @mQuantity13,				Quantity14 = @mQuantity14,				Quantity15 = @mQuantity15,
		Quantity16 = @mQuantity16,				Quantity17 = @mQuantity17,				Quantity18 = @mQuantity18

	WHERE
	JobCardNo = @mJobCardNo
	AND (invoiceno = '' oR InvoiceNo is null)
END

If @mAction='INSMDDATA'
BEGIN
	Insert Into 
	MDData(ID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ScanDate,FileName,NumberofBoxes,PerfectBoxes)

	VALUES
	(@mID,@mCreatedBy,@mCreatedDate,@mModifiedBy,@mModifiedDate,@mScanDate,@mFileName,@mNumberofBoxes,@mPerfectBoxes)
END

If @mAction='INSMDDATADTLS'
BEGIN
	Insert Into 
	MDDataDtls(ID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,HID,BarCode,Status,FileName,CartonNo)

	VALUES
	(@mID,@mCreatedBy,@mCreatedDate,@mModifiedBy,@mModifiedDate,@mHID,@mBarCode,@mStatus,@mFileName,@mCartonNo)
END

If @mAction='UPDMDDATA'
BEGIN
	UPDATE
	MDData 
	SET NumberofBoxes = @mNumberofBoxes,	PerfectBoxes = @mPerfectBoxes

	WHERE
	ID = @mID
END

--Select * from ReadyToDispatch
--Describe MDDataDtls


If @mAction='SELMCDATA'
BEGIN
	Select id,[FileName],NumberofBoxes,PerfectBoxes 
	From MDData
	Where Cast(ScanDate As Date) = Cast(@mScanDate As Date)
	Order By ScanDate
END

If @mAction='SELMCDATADTLS'
BEGIN
	Select Barcode,[Status]
	From MDDataDtls
	Where HID = @mHID
	Order By  CartonNo
END





GO
