USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOne201]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_AllinOne201

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOne201]
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.OrderType = @mTypeofOrder
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0SSFSSSD
-------------------------------------------------------------------------------------------------------------------------------------------------------------


GO
