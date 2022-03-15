USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOnePurchaseInvoices]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_AllinOnePurchaseInvoices

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOnePurchaseInvoices]
@mAction						varchar(20)	='SELALL',
@mPKId							int			=Null,
@mFromDate						Datetime	=Null,
@mToDate						DateTime	=Null,
@mBuyerName						Varchar(150)	=Null,
@mSoleName						Varchar(500)	=Null,
@mCodification					Varchar(50)		=Null,
@mClient						varchar(150)	=NULL,
@mCode							varchar(50)		=NULL,
@mGender						varchar(50)		=NULL,
@mSoleType						varchar(50)		=NULL,
@mColour						varchar(50)		=NULL,
@mGranules						varchar(100)	=NULL,
@mNettWt						decimal(18, 2)	=NULL,
@mLeatherSQM					varchar(150)	=NULL,
@mSQMConsumption				decimal(18, 2)	=NULL,
@mSQMDeclaredConsumption		decimal(18, 2)	=NULL,
@mLeatherKGS					varchar(150)	=NULL,
@mKGSConsumption				decimal(18, 2)	=NULL,
@mKGSDeclaredConsumption		decimal(18, 2)	=NULL,
@mDeclaredWt					decimal(18, 2)	=NULL,
@mCodificationNew				varchar(50)		=NULL,
@mDescription					Varchar(50)		=NULL,
@mArticleCode					Varchar(50)		=NULL,
@mArticleName					Varchar(50)		=Null,
@mOrderStatus					Varchar(50)		=Null,
@mTypeofOrder					Varchar(50)		=NULL,
@mMaterialType					Varchar(50)		=NULL,
@mMaterialSubType				Varchar(50)		=NULL
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PIAAAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PIAAAAAAD

-- OPTION 03
If @mAction='PIAAAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PIAAAAASD

-- OPTION 05
If @mAction='PIAAAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PIAAAASAD

-- OPTION 07
If @mAction='PIAAAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PIAAAASSD

-- OPTION 09
If @mAction='PIAAASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PIAAASAAD

-- OPTION 11
If @mAction='PIAAASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PIAAASASD

-- OPTION 13
If @mAction='PIAAASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PIAAASSAD

-- OPTION 15
If @mAction='PIAAASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PIAAASSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 


--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PIAAFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PIAAFAAAD

-- OPTION 03
If @mAction='PIAAFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PIAAFAASD

-- OPTION 05
If @mAction='PIAAFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PIAAFASAD

-- OPTION 07
If @mAction='PIAAFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PIAAFASSD

-- OPTION 09
If @mAction='PIAAFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PIAAFSAAD

-- OPTION 11
If @mAction='PIAAFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PIAAFSASD

-- OPTION 13
If @mAction='PIAAFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PIAAFSSAD

-- OPTION 15
If @mAction='PIAAFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PIAAFSSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 


--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 03 --{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PIASAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PIASAAAAD

-- OPTION 03
If @mAction='PIASAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PIASAAASD

-- OPTION 05
If @mAction='PIASAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PIASAASAD

-- OPTION 07
If @mAction='PIASAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PIASAASSD

-- OPTION 09
If @mAction='PIASASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PIASASAAD

-- OPTION 11
If @mAction='PIASASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PIASASASD

-- OPTION 13
If @mAction='PIASASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PIASASSAD

-- OPTION 15
If @mAction='PIASASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PIASASSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 


--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PIASFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PIASFAAAD

-- OPTION 03
If @mAction='PIASFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PIASFAASD

-- OPTION 05
If @mAction='PIASFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PIASFASAD

-- OPTION 07
If @mAction='PIASFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PIASFASSD

-- OPTION 09
If @mAction='PIASFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PIASFSAAD

-- OPTION 11
If @mAction='PIASFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PIASFSASD

-- OPTION 13
If @mAction='PIASFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PIASFSSAD

-- OPTION 15
If @mAction='PIASFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PIASFSSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 
--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 03 --{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 


--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X] 04 --[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X] 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PISAAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)


	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PISAAAAAD

-- OPTION 03
If @mAction='PISAAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PISAAAASD

-- OPTION 05
If @mAction='PISAAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PISAAASAD

-- OPTION 07
If @mAction='PISAAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PISAAASSD

-- OPTION 09
If @mAction='PISAASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PISAASAAD

-- OPTION 11
If @mAction='PISAASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PISAASASD

-- OPTION 13
If @mAction='PISAASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PISAASSAD

-- OPTION 15
If @mAction='PISAASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PISAASSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 


--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PISAFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PISAFAAAD

-- OPTION 03
If @mAction='PISAFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PISAFAASD

-- OPTION 05
If @mAction='PISAFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PISAFASAD

-- OPTION 07
If @mAction='PISAFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PISAFASSD

-- OPTION 09
If @mAction='PISAFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PISAFSAAD

-- OPTION 11
If @mAction='PISAFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PISAFSASD

-- OPTION 13
If @mAction='PISAFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PISAFSSAD

-- OPTION 15
If @mAction='PISAFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PISAFSSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 


--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 03 --{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PISSAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PISSAAAAD

-- OPTION 03
If @mAction='PISSAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PISSAAASD

-- OPTION 05
If @mAction='PISSAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PISSAASAD

-- OPTION 07
If @mAction='PISSAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PISSAASSD

-- OPTION 09
If @mAction='PISSASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PISSASAAD

-- OPTION 11
If @mAction='PISSASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PISSASASD

-- OPTION 13
If @mAction='PISSASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PISSASSAD

-- OPTION 15
If @mAction='PISSASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PISSASSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 


--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='PISSFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 02 - PISSFAAAD

-- OPTION 03
If @mAction='PISSFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 04 - PISSFAASD

-- OPTION 05
If @mAction='PISSFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 06 - PISSFASAD

-- OPTION 07
If @mAction='PISSFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 08 - PISSFASSD

-- OPTION 09
If @mAction='PISSFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END


-- OPTION 10 - PISSFSAAD

-- OPTION 11
If @mAction='PISSFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 12 - PISSFSASD

-- OPTION 13
If @mAction='PISSFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 14 - PISSFSSAD

-- OPTION 15
If @mAction='PISSFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.MaterialIssues.IssueDate As ArrivalDate,dbo.PARTY.partyname, dbo.MaterialIssues.VoucherNo,  dbo.MaterialIssues.PurchaseOrderNo,dbo.MaterialIssues.PurchaseOrderDate,
						 dbo.MaterialIssues.MaterialCode,dbo.MaterialIssues.Material, dbo.MaterialIssues.POSize,
						 dbo.Materials.MaterialColorDescription,dbo.MaterialIssues.IssueUnits,dbo.MaterialIssues.IssueQuantity,
						 dbo.MaterialIssues.IssuePrice,dbo.MaterialIssues.IssueValue,
                      dbo.Materials.MaterialTypeDescription,dbo.Materials.MaterialSubTypeDescription,  
                      dbo.MaterialIssues.TransactionType,   dbo.MaterialIssues.SupplierBillNo, 
                      dbo.MaterialIssues.SupplierRefNo, dbo.MaterialIssues.SupplierBillDate
	FROM         dbo.PARTY INNER JOIN
                      dbo.MaterialIssues ON dbo.PARTY.partycode = dbo.MaterialIssues.SupplierCode INNER JOIN
                      dbo.Materials ON dbo.MaterialIssues.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.MaterialIssues.CompanyCode = 'SSPL') AND (dbo.MaterialIssues.TransactionType = 'NEW PURCHASE')
					AND (dbo.MaterialIssues.IssueDate >= @mFromDate And dbo.MaterialIssues.IssueDate <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.MaterialIssues.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.MaterialIssues.Material like '%'+@mDescription+'%'
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrderDetails Where PurchaseOrderStatus = @mOrderStatus And MaterialCode = dbo.MaterialIssues.MaterialCode)
					AND  dbo.PARTY.partyname = @mBuyerName
					AND dbo.MaterialIssues.PurchaseOrderNo in (
					Select PurchaseOrderNo From PurchaseOrder Where POSubtype = @mTypeofOrder)

	ORDER BY dbo.MaterialIssues.IssueDate
END

-- OPTION 16 - PISSFSSSD

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
--|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 02 --|--|--|--|--|--|--|--|--|--|--|--|--|--|--| 
--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 03 --{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{}--{} 
--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X] 04 --[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]--[X]

GO
