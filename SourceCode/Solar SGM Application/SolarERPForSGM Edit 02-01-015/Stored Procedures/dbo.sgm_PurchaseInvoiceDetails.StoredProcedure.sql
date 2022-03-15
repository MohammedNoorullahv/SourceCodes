USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_PurchaseInvoiceDetails]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_PurchaseInvoiceDetails

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_PurchaseInvoiceDetails]
@mAction						varchar(20)		='SELALL',
@mPKID							int				=Null,
@mArrivalDate					date			=Null,
@mpartyname						varchar(100)	=NULL,
@mVoucherNo						varchar(50)		=NULL,
@mPurchaseOrderNo				varchar(50)		=NULL,
@mPurchaseOrderDate				date			=NULL,
@mMaterialCode					varchar(50)		=NULL,
@mMaterial						varchar(200)	=NULL,
@mPOSize						varchar(50)		=NULL,
@mMaterialColorDescription		varchar(50)		=NULL,
@mIssueUnits					varchar(50)		=NULL,
@mIssueQuantity					decimal(18, 2)	=NULL,
@mIssuePrice					decimal(18, 2)	=NULL,
@mIssueValue					decimal(18, 2)	=NULL,
@mMaterialTypeDescription		varchar(50)		=NULL,
@mMaterialSubTypeDescription	varchar(50)		=NULL,
@mTransactionType				varchar(50)		=NULL,
@mSupplierBillNo				varchar(50)		=NULL,
@mSupplierRefNo					varchar(50)		=NULL,
@mSupplierBillDate				date			=NULL



AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='INSINV'
BEGIN
	INSERT INTO
	SolarPurchaseInvoice4SGM4Print
	
	VALUES
	(					@mArrivalDate,				@mpartyname,				@mVoucherNo,
	@mPurchaseOrderNo,	@mPurchaseOrderDate,		@mMaterialCode,				@mMaterial,
	@mPOSize,			@mMaterialColorDescription,	@mIssueUnits,				@mIssueQuantity,
	@mIssuePrice,		@mIssueValue,				@mMaterialTypeDescription,	@mMaterialSubTypeDescription,
	@mTransactionType,	@mSupplierBillNo,			@mSupplierRefNo,			@mSupplierBillDate)

END

IF @mAction='DELINV'
BEGIN
	DELETE FROM
	SolarPurchaseInvoice4SGM4Print

END

GO
