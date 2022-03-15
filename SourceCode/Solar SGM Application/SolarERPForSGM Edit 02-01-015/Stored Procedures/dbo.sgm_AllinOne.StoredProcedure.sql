USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOne]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- drop Proc sgm_AllinOne

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOne]
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
@mOrderType						Varchar(50)		=Null
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
			 Cast(OrderRecivedDate As Date) >= @mFromDate And Cast(OrderRecivedDate As Date) <= @mToDate
	ORDER BY  BuyerName	
END

IF @mAction='LoadOrdBrandA'
BEGIN
	Select ' ALL BRANDS' As Brand
	UNION
	SELECT DISTINCT TOP (100) PERCENT IsNull(Brand,'') AS Brand
	FROM         dbo.SalesOrderDetails
	WHERE     (Shipper = 'SSPL') And
			 Cast(OrderReceivedDate As Date) >= @mFromDate And Cast(OrderReceivedDate As Date) <= @mToDate
			 And IsNull(Brand,'') <> ''
	ORDER BY  Brand
END

IF @mAction='LoadInvBrandA'
BEGIN
	Select ' ALL BRANDS' As Brand
	UNION
	SELECT DISTINCT TOP (100) PERCENT IsNull(SOD.Brand,'') AS Brand
	FROM         dbo.InvoiceDetail As ID, SalesOrderDetails As SOD
	WHERE    ID.SalesOrderDetailID = SOD.ID And 
			 Cast(ID.InvoiceDate As Date) >= '2019-12-01' And Cast(ID.InvoiceDate As Date) <= '2020-01-28'
			 And IsNull(sod.Brand,'') <> ''
	ORDER BY  Brand
END

IF @mAction='LoadINVCustA'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	
	Select BuyerName from Buyer where BuyerCode in (
	SELECT DISTINCT BUYER
	FROM         dbo.Invoice
	WHERE     (Shipper = 'SSPL') And
			 Cast(InvoiceDate As Date) >= @mFromDate And Cast(InvoiceDate As Date) <= @mToDate )
	ORDER BY  BuyerName	
	
END


IF @mAction='LoadOrdSuppA'
BEGIN
	Select '' As PartyCode, ' ALL SUPPLIERS' As PartyName
	UNION
	Select PartyCode,PartyName from Party where PARTYCODE IN (
	SELECT DISTINCT TOP (100) PERCENT SupplierCode
	FROM         dbo.PurchaseoRDER
	WHERE     (Shipper = 'SSPL')  And Cast(PurchaseOrderDate As Date) >= @mFromDate And Cast(PurchaseOrderDate As Date) <= @mToDate)
	ORDER BY  PartyName	
	
END



IF @mAction='LoadOrdCustS'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	SELECT DISTINCT TOP (100) PERCENT BuyerName
	FROM         dbo.SalesOrder
	WHERE     (Shipper = 'SSPL') AND (OrderType = 'SO') And 
			 Cast(OrderRecivedDate As Date) >= @mFromDate And Cast(OrderRecivedDate As Date) <= @mToDate
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadOrdBrandS'
BEGIN
	Select ' ALL BRANDS' As Brand
	UNION
	SELECT DISTINCT TOP (100) PERCENT IsNull(Brand,'') AS Brand
	FROM         dbo.SalesOrderDetails
	WHERE     (Shipper = 'SSPL') And  (Type = 'SO') And 
			 Cast(OrderReceivedDate As Date) >= @mFromDate And Cast(OrderReceivedDate As Date) <= @mToDate
			 And IsNull(Brand,'') <> ''
	ORDER BY  Brand
END

IF @mAction='LoadInvBrandS'
BEGIN
	Select ' ALL BRANDS' As Brand
	UNION

	SELECT DISTINCT TOP (100) PERCENT IsNull(SOD.Brand,'') AS Brand
	FROM         dbo.InvoiceDetail As ID, SalesOrderDetails As SOD
	WHERE    ID.SalesOrderDetailID = SOD.ID And (SOD.Type = 'SO') And 
			 Cast(ID.InvoiceDate As Date) >= '2019-12-01' And Cast(ID.InvoiceDate As Date) <= '2020-01-28'
			 And IsNull(sod.Brand,'') <> ''
	ORDER BY  Brand
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
			Cast(InvoiceDate As Date) >= @mFromDate And Cast(InvoiceDate As Date) <= @mToDate )
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadOrdSuppS'
BEGIN
	Select '' As PartyCode, ' ALL SUPPLIERS' As PartyName
	UNION
	Select PartyCode,PartyName from Party where PARTYCODE IN (
	SELECT DISTINCT TOP (100) PERCENT SupplierCode
	FROM         dbo.PurchaseoRDER
	WHERE     (Shipper = 'SSPL')  And POSubType = '01' And Cast(PurchaseOrderDate As Date) >= @mFromDate And Cast(PurchaseOrderDate As Date) <= @mToDate)
	ORDER BY  PartyName	
	
END

IF @mAction='LoadOrdCustJ'
BEGIN
	Select ' ALL CUSTOMERS' As BuyerName
	UNION
	SELECT DISTINCT TOP (100) PERCENT BuyerName
	FROM         dbo.SalesOrder
	WHERE     (Shipper = 'SSPL') AND (OrderType = 'JO') And 
			 Cast(OrderRecivedDate As Date) >= @mFromDate And Cast(OrderRecivedDate As Date) <= @mToDate
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadOrdBrandJ'
BEGIN
	Select ' ALL BRANDS' As Brand
	UNION
	SELECT DISTINCT TOP (100) PERCENT IsNull(Brand,'') AS Brand
	FROM         dbo.SalesOrderDetails
	WHERE     (Shipper = 'SSPL') And  (Type = 'JO') And 
			 Cast(OrderReceivedDate As Date) >= @mFromDate And Cast(OrderReceivedDate As Date) <= @mToDate
			 And IsNull(Brand,'') <> ''
	ORDER BY  Brand
END

IF @mAction='LoadInvBrandJ'
BEGIN
	Select ' ALL BRANDS' As Brand
	UNION
		SELECT DISTINCT TOP (100) PERCENT IsNull(SOD.Brand,'') AS Brand
	FROM         dbo.InvoiceDetail As ID, SalesOrderDetails As SOD
	WHERE    ID.SalesOrderDetailID = SOD.ID And (SOD.Type = 'JO') And 
			 Cast(ID.InvoiceDate As Date) >= '2019-12-01' And Cast(ID.InvoiceDate As Date) <= '2020-01-28'
			 And IsNull(sod.Brand,'') <> ''
	ORDER BY  Brand
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
			Cast(InvoiceDate As Date) >= @mFromDate And Cast(InvoiceDate As Date) <= @mToDate )
	ORDER BY  BuyerName	
	
END

IF @mAction='LoadOrdSuppJ'
BEGIN
	Select '' As PartyCode, ' ALL SUPPLIERS' As PartyName
	UNION
	Select PartyCode,PartyName from Party where PARTYCODE IN (
	SELECT DISTINCT TOP (100) PERCENT SupplierCode
	FROM         dbo.PurchaseoRDER
	WHERE     (Shipper = 'SSPL')  And POSubType = '02' And Cast(PurchaseOrderDate As Date) >= @mFromDate And Cast(PurchaseOrderDate As Date) <= @mToDate)
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
				(Select ID From SalesOrder Where  Cast(OrderRecivedDate As Date) >= @mFromDate And Cast(OrderRecivedDate As Date) <= @mToDate And BuyerName = @mBuyerName)
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
				(Select ID From SalesOrder Where  Cast(OrderRecivedDate As Date) >= @mFromDate And Cast(OrderRecivedDate As Date) <= @mToDate)
	ORDER BY SoleName		 
END
		
IF @mAction='LoadPurALLMat'
BEGIN
	Select ' ALL MATERIALS' AS MaterialWithType
	
	UNION

	Select Distinct MaterialTypeDescription + ' : '+ MaterialSubTypeDescription As MaterialWithType from Materials 
	where MaterialCode in 
		(Select MaterialCode from PurchaseOrderDetails where Shipper = 'SSPL'
		And Cast(PurchaseOrderDate As Date) >= @mFromDate And Cast(PurchaseOrderDate As Date) <= @mToDate
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
		And Cast(PurchaseOrderDate As Date) >= @mFromDate And Cast(PurchaseOrderDate As Date) <= @mToDate
		And PurchaseOrderNo in 
		(Select PurchaseOrderNo From PurchaseOrder Where SupplierCode in
		(Select PartyCode from Party Where PartyName = @mBuyerName)
		)
		)
	Order By  MaterialWithType
	
END


If @mAction='LOADSAMPTYPE'
BEGIN

	Select '000' As Abbrev_,' ALL SAMPLE ORDER' As FullName_
	UNION
	Select Abbrev_,FullName_ from AbbrevTable 
	Where Group_ =  @mOrderType
	Order by Abbrev_
END

If @mAction='LOADPRODORDTYPE'
BEGIN

	Select '000' As Abbrev_,' ALL PRODUCTION ORDER' As FullName_
	UNION
	Select Abbrev_,FullName_ from AbbrevTable 
	Where Group_ =  @mOrderType
	Order by Abbrev_
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0AAASSSD



--Option 01
If @mAction='S0AAFAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0AAFAAAD

--Option 03
If @mAction='S0AAFAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0AAFAASD

--Option 05
If @mAction='S0AAFASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0AAFASAD

--Option 07
If @mAction='S0AAFASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0AAFASSD

--Option 09
If @mAction='S0AAFSAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0AAFSAAD

--Option 11
If @mAction='S0AAFSASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0AAFSASD

--Option 13
If @mAction='S0AAFSSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0AAFSSAD

--Option 15
If @mAction='S0AAFSSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0AAFSSSD

------------------

--Option 01
If @mAction='S0ASAAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0ASAAAAD

--Option 03
If @mAction='S0ASAAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0ASAAASD

--Option 05
If @mAction='S0ASAASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0ASAASAD

--Option 07
If @mAction='S0ASAASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0ASAASSD

--Option 09
If @mAction='S0ASASAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0ASASAAD

--Option 11
If @mAction='S0ASASASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0ASASASD

--Option 13
If @mAction='S0ASASSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0ASASSAD

--Option 15
If @mAction='S0ASASSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0ASASSSD



--Option 01
If @mAction='S0ASFAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0ASFAAAD

--Option 03
If @mAction='S0ASFAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0ASFAASD

--Option 05
If @mAction='S0ASFASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0ASFASAD

--Option 07
If @mAction='S0ASFASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0ASFASSD

--Option 09
If @mAction='S0ASFSAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0ASFSAAD

--Option 11
If @mAction='S0ASFSASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0ASFSASD

--Option 13
If @mAction='S0ASFSSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0ASFSSAD

--Option 15
If @mAction='S0ASFSSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0ASFSSSD
-------------------------------------------------------------------------------------------------------------------------------------------------------------


--Option 01
If @mAction='S0SAAAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0SAAAAAD

--Option 03
If @mAction='S0SAAAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0SAAAASD

--Option 05
If @mAction='S0SAAASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0SAAASAD

--Option 07
If @mAction='S0SAAASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0SAAASSD

--Option 09
If @mAction='S0SAASAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0SAASAAD

--Option 11
If @mAction='S0SAASASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0SAASASD

--Option 13
If @mAction='S0SAASSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0SAASSAD

--Option 15
If @mAction='S0SAASSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0SAASSSD



--Option 01
If @mAction='S0SAFAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0SAFAAAD

--Option 03
If @mAction='S0SAFAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0SAFAASD

--Option 05
If @mAction='S0SAFASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0SAFASAD

--Option 07
If @mAction='S0SAFASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0SAFASSD

--Option 09
If @mAction='S0SAFSAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0SAFSAAD

--Option 11
If @mAction='S0SAFSASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0SAFSASD

--Option 13
If @mAction='S0SAFSSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0SAFSSAD

--Option 15
If @mAction='S0SAFSSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0SAFSSSD

------------------

--Option 01
If @mAction='S0SSAAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0SSAAAAD

--Option 03
If @mAction='S0SSAAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0SSAAASD

--Option 05
If @mAction='S0SSAASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0SSAASAD

--Option 07
If @mAction='S0SSAASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0SSAASSD

--Option 09
If @mAction='S0SSASAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0SSASAAD

--Option 11
If @mAction='S0SSASASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0SSASASD

--Option 13
If @mAction='S0SSASSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0SSASSAD

--Option 15
If @mAction='S0SSASSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0SSASSSD



--Option 01
If @mAction='S0SSFAAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END


--Option 02 -- S0SSFAAAD

--Option 03
If @mAction='S0SSFAASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 04 -- S0SSFAASD

--Option 05
If @mAction='S0SSFASAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 06 -- S0SSFASAD

--Option 07
If @mAction='S0SSFASSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 08 -- S0SSFASSD

--Option 09
If @mAction='S0SSFSAAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 10 -- S0SSFSAAD

--Option 11
If @mAction='S0SSFSASS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 12 -- S0SSFSASD

--Option 13
If @mAction='S0SSFSSAS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 14 -- S0SSFSSAD

--Option 15
If @mAction='S0SSFSSSS'
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (Cast(dbo.SalesOrder.OrderRecivedDate As Date) >= @mFromDate And Cast(dbo.SalesOrder.OrderRecivedDate As Date) <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0SSFSSSD








GO
