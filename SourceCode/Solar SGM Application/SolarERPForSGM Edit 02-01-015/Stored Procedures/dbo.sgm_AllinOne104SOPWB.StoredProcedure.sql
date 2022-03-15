USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOne104SOPWB]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_AllinOne104SOPWB

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOne104SOPWB]
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
@mIsSampleOrder					Bit				=Null,
@mBrand							Varchar(50)		=Null
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



--Option 01
If @mAction='S0ASAAAAS'
BEGIN
SELECT     TOP (100) PERCENT dbo.SalesOrder.OrderRecivedDate, dbo.SalesOrder.BuyerName, dbo.SalesOrderDetails.SalesOrderNo, dbo.SalesOrderDetails.CustomerOrderNo, 
                      dbo.SalesOrderDetails.Article, dbo.SalesOrderDetails.ArticleName, dbo.Materials.Description, dbo.Materials.MaterialColorDescription, dbo.Materials.CodificationNew, 
                      dbo.Materials.ArticleMould, LEFT(dbo.SalesOrder.OrderType,1) As OrderType, dbo.SalesOrderDetails.OrderQuantity, dbo.SalesOrderDetails.Price, dbo.SalesOrderDetails.OrderValue,
--                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
--                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
--                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' AND Buyer in
--                      (Select BuyerCode From Buyer where BuyerName = @mBuyerName) )
--                      ),0) As DispQty,
					dbo.SalesOrderDetails.ShippedQuantity As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
--                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
--                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
--                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL'  AND Buyer in
--                      (Select BuyerCode From Buyer where BuyerName = @mBuyerName) )
--                      ),0) As Bal2disp,
					dbo.SalesOrderDetails.ShippedQuantity As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' AND Buyer in
                      (Select BuyerCode From Buyer where BuyerName = @mBuyerName) )
                      ),0) As DispQty,
                      
                      dbo.SalesOrderDetails.OrderQuantity -
                      IsNull((Select Sum(Quantity) From InvoiceDetail Where SalesOrderNo = dbo.SalesOrderDetails.SalesOrderNo And ArticleNo = dbo.SalesOrderDetails.Article 
                      And JobcardNo IN (Select JobcardNo From JobcardDetail where SalesOrderDEtailId = dbo.SalesOrderDetails.ID)
                      And InvoiceNo IN (Select InvoiceNo From Invoice Where Shipper = 'SSPL' AND Buyer in
                      (Select BuyerCode From Buyer where BuyerName = @mBuyerName) )
                      ),0) As Bal2disp,dbo.SalesOrderDetails.ID,dbo.SalesOrderDetails.OrderStatus

FROM         dbo.SalesOrderDetails INNER JOIN
                      dbo.SalesOrder ON dbo.SalesOrderDetails.SalesOrderID = dbo.SalesOrder.ID INNER JOIN
                      dbo.Materials ON dbo.SalesOrderDetails.Article = dbo.Materials.MaterialCode
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
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
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And (dbo.salesorderdetails.Brand = @mBrand) And (dbo.SalesOrder.OrderRecivedDate >= @mFromDate And dbo.SalesOrder.OrderRecivedDate <= @mToDate)
					  and dbo.Materials.ArticleMould = @mArticleName
					  and dbo.SalesOrderDetails.Article like '%'+@mArticleCode+'%'
					  and dbo.Materials.Description like '%'+@mDescription+'%'
					  and dbo.SalesOrderDetails.OrderStatus = @mOrderStatus
					  And dbo.SalesOrder.BuyerName = @mBuyerName
					  And dbo.SalesOrder.IsSampleOrder = @mIsSampleOrder 
ORDER BY dbo.SalesOrder.OrderRecivedDate
END

--Option 16 -- S0ASFSSSD
-------------------------------------------------------------------------------------------------------------------------------------------------------------




GO
