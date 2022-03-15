USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOne101]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_AllinOne101

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOne101]
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
@mTypeofOrder					Varchar(50)		=NULL
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='LoadOrdCustA'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	SELECT DISTINCT TOP (100) PERCENT BuyerName
	FROM         dbo.SalesOrder
	WHERE     (Shipper = 'SSPL') And
			 OrderRecivedDate >= @mFromDate And OrderRecivedDate <= @mToDate
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadINVCustA'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	
	Select BuyerName from Buyer where BuyerCode in (
	SELECT DISTINCT BUYER
	FROM         dbo.Invoice
	WHERE     (Shipper = 'SSPL') And
			 InvoiceDate >= @mFromDate And InvoiceDate <= @mToDate )
	ORDER BY  BuyerName	
	
END


IF @mAction='LoadOrdSuppA'
BEGIN
	Select '' As PartyCode, ' ALL SUPPLIERS' As PartyName
	UNION
	Select PartyCode,PartyName from Party where PARTYCODE IN (
	SELECT DISTINCT TOP (100) PERCENT SupplierCode
	FROM         dbo.PurchaseoRDER
	WHERE     (Shipper = 'SSPL')  And PurchaseOrderDate >= @mFromDate And PurchaseOrderDate <= @mToDate)
	ORDER BY  PartyName	
	
END



IF @mAction='LoadOrdCustS'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	SELECT DISTINCT TOP (100) PERCENT BuyerName
	FROM         dbo.SalesOrder
	WHERE     (Shipper = 'SSPL') AND (OrderType = 'SO') And 
			 OrderRecivedDate >= @mFromDate And OrderRecivedDate <= @mToDate
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadINVCustS'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	
	Select BuyerName from Buyer where BuyerCode in (
	SELECT DISTINCT BUYER
	FROM         dbo.Invoice
	WHERE   (Shipper = 'SSPL') And
			WareHouse = 'SO' And
			InvoiceDate >= @mFromDate And InvoiceDate <= @mToDate )
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadOrdSuppS'
BEGIN
	Select '' As PartyCode, ' ALL SUPPLIERS' As PartyName
	UNION
	Select PartyCode,PartyName from Party where PARTYCODE IN (
	SELECT DISTINCT TOP (100) PERCENT SupplierCode
	FROM         dbo.PurchaseoRDER
	WHERE     (Shipper = 'SSPL')  And POSubType = '01' And PurchaseOrderDate >= @mFromDate And PurchaseOrderDate <= @mToDate)
	ORDER BY  PartyName	
	
END

IF @mAction='LoadOrdCustJ'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	SELECT DISTINCT TOP (100) PERCENT BuyerName
	FROM         dbo.SalesOrder
	WHERE     (Shipper = 'SSPL') AND (OrderType = 'JO') And 
			 OrderRecivedDate >= @mFromDate And OrderRecivedDate <= @mToDate
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadINVCustJ'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	
	Select BuyerName from Buyer where BuyerCode in (
	SELECT DISTINCT BUYER
	FROM         dbo.Invoice
	WHERE   (Shipper = 'SSPL') And
			WareHouse = 'JO' And
			InvoiceDate >= @mFromDate And InvoiceDate <= @mToDate )
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadOrdSuppJ'
BEGIN
	Select '' As PartyCode, ' ALL SUPPLIERS' As PartyName
	UNION
	Select PartyCode,PartyName from Party where PARTYCODE IN (
	SELECT DISTINCT TOP (100) PERCENT SupplierCode
	FROM         dbo.PurchaseoRDER
	WHERE     (Shipper = 'SSPL')  And POSubType = '02' And PurchaseOrderDate >= @mFromDate And PurchaseOrderDate <= @mToDate)
	ORDER BY  PartyName	
	
END

IF @mAction='LoadOrdArt'
BEGIN
	Select ' ALL ARTICLES' AS SoleName
	
	UNION

	SELECT DISTINCT TOP (100) PERCENT dbo.Materials.ArticleMould  As SoleName
	FROM        dbo.SalesOrderDetails INNER JOIN
                dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
	WHERE		(dbo.SalesOrderDetails.shipper = 'SSPL') AND 
				dbo.SalesOrderDetails.SalesOrderId in 
				(Select ID From SalesOrder Where OrderRecivedDate >= @mFromDate And OrderRecivedDate <= @mToDate And BuyerName = @mBuyerName)
	ORDER BY SoleName		 

END

IF @mAction='LoadOrdALLArt'
BEGIN
	Select ' ALL ARTICLES' AS SoleName
	
	UNION

	SELECT DISTINCT TOP (100) PERCENT dbo.Materials.ArticleMould  As SoleName
	FROM        dbo.SalesOrderDetails INNER JOIN
                dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
	WHERE		(dbo.SalesOrderDetails.shipper = 'SSPL') AND 
				dbo.SalesOrderDetails.SalesOrderId in 
				(Select ID From SalesOrder Where OrderRecivedDate >= @mFromDate And OrderRecivedDate <= @mToDate)
	ORDER BY SoleName		 
END
		
IF @mAction='LoadPurALLMat'
BEGIN
	Select ' ALL MATERIALS' AS MaterialWithType
	
	UNION

	Select Distinct MaterialTypeDescription + ' : '+ MaterialSubTypeDescription As MaterialWithType from Materials 
	where MaterialCode in 
		(Select MaterialCode from PurchaseOrderDetails where Shipper = 'SSPL'
		And PurchaseOrderDate >= @mFromDate And PurchaseOrderDate <= @mToDate
		)
	Order By  MaterialWithType
	
END

IF @mAction='LoadPurMat'
BEGIN
	Select ' ALL MATERIALS' AS MaterialWithType
	
	UNION

	Select Distinct MaterialTypeDescription + ' : '+ MaterialSubTypeDescription As MaterialWithType from Materials 
	where MaterialCode in 
		(Select MaterialCode from PurchaseOrderDetails where Shipper = 'SSPL'
		And PurchaseOrderDate >= @mFromDate And PurchaseOrderDate <= @mToDate
		And PurchaseOrderNo in 
		(Select PurchaseOrderNo From PurchaseOrder Where SupplierCode in
		(Select PartyCode from Party Where PartyName = @mBuyerName)
		)
		)
	Order By  MaterialWithType
	
END


--Option 01
If @mAction='S0AAAAAAS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' ))
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus
FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='S0AAAAASS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='S0AAAASAS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='S0AAAASSS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='S0AAASAAS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='S0AAASASS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='S0AAASSAS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='S0AAASSSS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0AAASSSD



GO
