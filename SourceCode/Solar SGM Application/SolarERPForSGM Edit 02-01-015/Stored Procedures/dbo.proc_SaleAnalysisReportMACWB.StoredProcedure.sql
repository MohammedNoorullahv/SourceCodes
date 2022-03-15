USE [AHGroup_SSPL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- drop Proc proc_SaleAnalysisReportMACWB

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportMACWB]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mCodificationNew				Varchar(50)		=Null,
@mWeekFrom						Int				=Null,
@mWeekTo						Int				=Null,
@mYear							Int				=Null,
@mIsSampleOrder					Bit				=Null,
@mShipmentStatus				Varchar(20)		=Null,
@mOrderStatus					Varchar(20)		=Null,
@mDescription					Varchar(50)		=NULL,
@mArticleMould					Varchar(50)		=Null,
@mProductType					Varchar(50)		=Null,
@mSampleType					Varchar(50)		=Null,
@mMaterialCode					Varchar(50)		=Null,
@GranuleType					Varchar(50)		=Null,
@mProductTypeMain				Varchar(50)		=Null,
@mBrand							Varchar(50)		=Null


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

--'' As BuyerName, MAT.ArticleMould
--'' As BuyerName, MAT.ArticleMould
-- Option No. 1
IF @mAction= '02AAAAAAAA'

BEGIN
--	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
	
END

-- Option No. 2
IF @mAction= '02AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 3
--IF @mAction= '02AAAAAAFA'

-- Option No. 4
--IF @mAction= '02AAAAAAFF'

-- Option No. 5
IF @mAction= '02AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 6
IF @mAction= '02AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 7
IF @mAction= '02AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 8
IF @mAction= '02AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 9
IF @mAction= '02AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 10
IF @mAction= '02AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 11
--IF @mAction= '02AAAAFAFA'

-- Option No. 12
--IF @mAction= '02AAAAFAFF'

-- Option No. 13
IF @mAction= '02AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 14
IF @mAction= '02AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 15
IF @mAction= '02AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 16
IF @mAction= '02AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 17
IF @mAction= '02AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 18
IF @mAction= '02AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 19
--IF @mAction= '02AAAFAAFA'

-- Option No. 20
--IF @mAction= '02AAAFAAFF'

-- Option No. 21
IF @mAction= '02AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 22
IF @mAction= '02AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 23
IF @mAction= '02AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 24
IF @mAction= '02AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 25
IF @mAction= '02AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 26
IF @mAction= '02AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 27
--IF @mAction= '02AAAFFAFA'

-- Option No. 28
--IF @mAction= '02AAAFFAFF'

-- Option No. 29
IF @mAction= '02AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 30
IF @mAction= '02AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 31
IF @mAction= '02AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 32
IF @mAction= '02AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 33
IF @mAction= '02AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 34
IF @mAction= '02AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 35
--IF @mAction= '02AAFAAAFA'

-- Option No. 36
--IF @mAction= '02AAFAAAFF'

-- Option No. 37
IF @mAction= '02AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 38
IF @mAction= '02AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 39
IF @mAction= '02AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 40
IF @mAction= '02AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 41
IF @mAction= '02AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 42
IF @mAction= '02AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 43
--IF @mAction= '02AAFAFAFA'

-- Option No. 44
--IF @mAction= '02AAFAFAFF'

-- Option No. 45
IF @mAction= '02AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 46
IF @mAction= '02AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 47
IF @mAction= '02AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 48
IF @mAction= '02AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 49
IF @mAction= '02AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 50
IF @mAction= '02AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 51
--IF @mAction= '02AAFFAAFA'

-- Option No. 52
--IF @mAction= '02AAFFAAFF'

-- Option No. 53
IF @mAction= '02AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 54
IF @mAction= '02AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 55
IF @mAction= '02AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 56
IF @mAction= '02AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 57
IF @mAction= '02AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 58
IF @mAction= '02AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 59
--IF @mAction= '02AAFFFAFA'

-- Option No. 60
--IF @mAction= '02AAFFFAFF'

-- Option No. 61
IF @mAction= '02AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 62
IF @mAction= '02AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 63
IF @mAction= '02AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 64
IF @mAction= '02AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 65
IF @mAction= '02AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 66
IF @mAction= '02AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 67
--IF @mAction= '02AFAAAAFA'

-- Option No. 68
--IF @mAction= '02AFAAAAFF'

-- Option No. 69
IF @mAction= '02AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 70
IF @mAction= '02AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 71
IF @mAction= '02AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 72
IF @mAction= '02AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 73
IF @mAction= '02AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 74
IF @mAction= '02AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 75
--IF @mAction= '02AFAAFAFA'

-- Option No. 76
--IF @mAction= '02AFAAFAFF'

-- Option No. 77
IF @mAction= '02AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 78
IF @mAction= '02AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 79
IF @mAction= '02AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 80
IF @mAction= '02AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 81
IF @mAction= '02AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 82
IF @mAction= '02AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 83
--IF @mAction= '02AFAFAAFA'

-- Option No. 84
--IF @mAction= '02AFAFAAFF'

-- Option No. 85
IF @mAction= '02AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 86
IF @mAction= '02AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 87
IF @mAction= '02AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 88
IF @mAction= '02AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 89
IF @mAction= '02AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 90
IF @mAction= '02AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 91
--IF @mAction= '02AFAFFAFA'

-- Option No. 92
--IF @mAction= '02AFAFFAFF'

-- Option No. 93
IF @mAction= '02AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 94
IF @mAction= '02AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 95
IF @mAction= '02AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 96
IF @mAction= '02AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 97
IF @mAction= '02AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 98
IF @mAction= '02AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 99
--IF @mAction= '02AFFAAAFA'

-- Option No. 100
--IF @mAction= '02AFFAAAFF'

-- Option No. 101
IF @mAction= '02AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 102
IF @mAction= '02AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 103
IF @mAction= '02AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 104
IF @mAction= '02AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 105
IF @mAction= '02AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 106
IF @mAction= '02AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 107
--IF @mAction= '02AFFAFAFA'

-- Option No. 108
--IF @mAction= '02AFFAFAFF'

-- Option No. 109
IF @mAction= '02AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 110
IF @mAction= '02AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 111
IF @mAction= '02AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 112
IF @mAction= '02AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 113
IF @mAction= '02AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 114
IF @mAction= '02AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 115
--IF @mAction= '02AFFFAAFA'

-- Option No. 116
--IF @mAction= '02AFFFAAFF'

-- Option No. 117
IF @mAction= '02AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 118
IF @mAction= '02AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 119
IF @mAction= '02AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 120
IF @mAction= '02AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 121
IF @mAction= '02AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 122
IF @mAction= '02AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 123
--IF @mAction= '02AFFFFAFA'

-- Option No. 124
--IF @mAction= '02AFFFFAFF'

-- Option No. 125
IF @mAction= '02AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 126
IF @mAction= '02AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 127
IF @mAction= '02AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 128
IF @mAction= '02AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 129
IF @mAction= '02FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 130
IF @mAction= '02FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 131
--IF @mAction= '02FAAAAAFA'

-- Option No. 132
--IF @mAction= '02FAAAAAFF'

-- Option No. 133
IF @mAction= '02FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 134
IF @mAction= '02FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 135
IF @mAction= '02FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 136
IF @mAction= '02FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 138
IF @mAction= '02FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 138
IF @mAction= '02FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 139
--IF @mAction= '02FAAAFAFA'

-- Option No. 140
--IF @mAction= '02FAAAFAFF'

-- Option No. 141
IF @mAction= '02FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 142
IF @mAction= '02FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 143
IF @mAction= '02FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 144
IF @mAction= '02FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 145
IF @mAction= '02FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 146
IF @mAction= '02FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 147
--IF @mAction= '02FAAFAAFA'

-- Option No. 148
--IF @mAction= '02FAAFAAFF'

-- Option No. 149
IF @mAction= '02FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 150
IF @mAction= '02FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 151
IF @mAction= '02FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 152
IF @mAction= '02FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 153
IF @mAction= '02FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 154
IF @mAction= '02FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 155
--IF @mAction= '02FAAFFAFA'

-- Option No. 156
--IF @mAction= '02FAAFFAFF'

-- Option No. 157
IF @mAction= '02FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 158
IF @mAction= '02FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 159
IF @mAction= '02FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 160
IF @mAction= '02FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 161
IF @mAction= '02FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 162
IF @mAction= '02FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 163
--IF @mAction= '02FAFAAAFA'

-- Option No. 164
--IF @mAction= '02FAFAAAFF'

-- Option No. 165
IF @mAction= '02FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 166
IF @mAction= '02FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 167
IF @mAction= '02FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 168
IF @mAction= '02FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 169
IF @mAction= '02FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 170
IF @mAction= '02FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 171
--IF @mAction= '02FAFAFAFA'

-- Option No. 172
--IF @mAction= '02FAFAFAFF'

-- Option No. 173
IF @mAction= '02FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 174
IF @mAction= '02FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 175
IF @mAction= '02FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 176
IF @mAction= '02FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 177
IF @mAction= '02FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 178
IF @mAction= '02FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 179
--IF @mAction= '02FAFFAAFA'

-- Option No. 180
--IF @mAction= '02FAFFAAFF'

-- Option No. 181
IF @mAction= '02FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 182
IF @mAction= '02FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 183
IF @mAction= '02FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 184
IF @mAction= '02FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 185
IF @mAction= '02FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 186
IF @mAction= '02FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 187
--IF @mAction= '02FAFFFAFA'

-- Option No. 188
--IF @mAction= '02FAFFFAFF'

-- Option No. 189
IF @mAction= '02FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 190
IF @mAction= '02FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 191
IF @mAction= '02FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 192
IF @mAction= '02FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 193
IF @mAction= '02FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 194
IF @mAction= '02FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 195
--IF @mAction= '02FFAAAAFA'

-- Option No. 196
--IF @mAction= '02FFAAAAFF'

-- Option No. 197
IF @mAction= '02FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 198
IF @mAction= '02FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 199
IF @mAction= '02FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 200
IF @mAction= '02FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 201
IF @mAction= '02FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 202
IF @mAction= '02FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 203
--IF @mAction= '02FFAAFAFA'

-- Option No. 204
--IF @mAction= '02FFAAFAFF'

-- Option No. 205
IF @mAction= '02FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 206
IF @mAction= '02FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 207
IF @mAction= '02FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 208
IF @mAction= '02FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 209
IF @mAction= '02FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 210
IF @mAction= '02FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 211
--IF @mAction= '02FFAFAAFA'

-- Option No. 212
--IF @mAction= '02FFAFAAFF'

-- Option No. 213
IF @mAction= '02FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 214
IF @mAction= '02FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 215
IF @mAction= '02FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 216
IF @mAction= '02FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 217
IF @mAction= '02FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 218
IF @mAction= '02FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 219
--IF @mAction= '02FFAFFAFA'

-- Option No. 220
--IF @mAction= '02FFAFFAFF'

-- Option No. 221
IF @mAction= '02FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 222
IF @mAction= '02FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 223
IF @mAction= '02FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 224
IF @mAction= '02FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 225
IF @mAction= '02FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 226
IF @mAction= '02FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 227
--IF @mAction= '02FFFAAAFA'

-- Option No. 228
--IF @mAction= '02FFFAAAFF'

-- Option No. 229
IF @mAction= '02FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 230
IF @mAction= '02FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 231
IF @mAction= '02FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 232
IF @mAction= '02FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 233
IF @mAction= '02FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 234
IF @mAction= '02FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 235
--IF @mAction= '02FFFAFAFA'

-- Option No. 236
--IF @mAction= '02FFFAFAFF'

-- Option No. 237
IF @mAction= '02FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 238
IF @mAction= '02FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 239
IF @mAction= '02FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 240
IF @mAction= '02FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 241
IF @mAction= '02FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 242
IF @mAction= '02FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 243
--IF @mAction= '02FFFFAAFA'

-- Option No. 244
--IF @mAction= '02FFFFAAFF'

-- Option No. 245
IF @mAction= '02FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 246
IF @mAction= '02FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 247
IF @mAction= '02FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 248
IF @mAction= '02FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 249
IF @mAction= '02FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 250
IF @mAction= '02FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 251
--IF @mAction= '02FFFFFAFA'

-- Option No. 252
--IF @mAction= '02FFFFFAFF'

-- Option No. 253
IF @mAction= '02FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 254
IF @mAction= '02FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 255
IF @mAction= '02FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

-- Option No. 256
IF @mAction= '02FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew
END

--SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
--                         / SUM(INVDTL.quantity) AS Average
--FROM            dbo.INVOICE AS INV INNER JOIN
--                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
--                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
--                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
--                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
--                         dbo.SalesOrder ON SOD.SalesOrderID = dbo.SalesOrder.ID
--WHERE        (INV.InvoiceDate >= '2018-07-01')
--GROUP BY Buy.BuyerName, MAT.ArticleMould--, MAT.CodificationNew, MAT.ArticleMould, MAT.CodificationNew
--ORDER BY MAT.ArticleMould, Buy.BuyerName--, MAT.CodificationNew, MAT.ArticleMould, MAT.CodificationNew








GO
