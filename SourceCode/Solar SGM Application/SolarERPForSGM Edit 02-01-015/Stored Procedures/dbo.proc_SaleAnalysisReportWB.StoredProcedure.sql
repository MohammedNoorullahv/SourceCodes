USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportWB]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- drop Proc proc_SaleAnalysisReportWB

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportWB]
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


-- Option No. 1
IF @mAction= '00AAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 2
IF @mAction= '00AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 3
--IF @mAction= '00AAAAAAFA'

-- Option No. 4
--IF @mAction= '00AAAAAAFF'

-- Option No. 5
IF @mAction= '00AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 6
IF @mAction= '00AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 7
IF @mAction= '00AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 8
IF @mAction= '00AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 9
IF @mAction= '00AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 10
IF @mAction= '00AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 11
--IF @mAction= '00AAAAFAFA'

-- Option No. 12
--IF @mAction= '00AAAAFAFF'

-- Option No. 13
IF @mAction= '00AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 14
IF @mAction= '00AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 15
IF @mAction= '00AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 16
IF @mAction= '00AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 17
IF @mAction= '00AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 18
IF @mAction= '00AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 19
--IF @mAction= '00AAAFAAFA'

-- Option No. 20
--IF @mAction= '00AAAFAAFF'

-- Option No. 21
IF @mAction= '00AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 22
IF @mAction= '00AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 23
IF @mAction= '00AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 24
IF @mAction= '00AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 25
IF @mAction= '00AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 26
IF @mAction= '00AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 27
--IF @mAction= '00AAAFFAFA'

-- Option No. 28
--IF @mAction= '00AAAFFAFF'

-- Option No. 29
IF @mAction= '00AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 30
IF @mAction= '00AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 31
IF @mAction= '00AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 32
IF @mAction= '00AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 33
IF @mAction= '00AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 34
IF @mAction= '00AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 35
--IF @mAction= '00AAFAAAFA'

-- Option No. 36
--IF @mAction= '00AAFAAAFF'

-- Option No. 37
IF @mAction= '00AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 38
IF @mAction= '00AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 39
IF @mAction= '00AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 40
IF @mAction= '00AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 41
IF @mAction= '00AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 42
IF @mAction= '00AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 43
--IF @mAction= '00AAFAFAFA'

-- Option No. 44
--IF @mAction= '00AAFAFAFF'

-- Option No. 45
IF @mAction= '00AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 46
IF @mAction= '00AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 47
IF @mAction= '00AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 48
IF @mAction= '00AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 49
IF @mAction= '00AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 50
IF @mAction= '00AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 51
--IF @mAction= '00AAFFAAFA'

-- Option No. 52
--IF @mAction= '00AAFFAAFF'

-- Option No. 53
IF @mAction= '00AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 54
IF @mAction= '00AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 55
IF @mAction= '00AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 56
IF @mAction= '00AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 57
IF @mAction= '00AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 58
IF @mAction= '00AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 59
--IF @mAction= '00AAFFFAFA'

-- Option No. 60
--IF @mAction= '00AAFFFAFF'

-- Option No. 61
IF @mAction= '00AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 62
IF @mAction= '00AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 63
IF @mAction= '00AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 64
IF @mAction= '00AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 65
IF @mAction= '00AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 66
IF @mAction= '00AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 67
--IF @mAction= '00AFAAAAFA'

-- Option No. 68
--IF @mAction= '00AFAAAAFF'

-- Option No. 69
IF @mAction= '00AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 70
IF @mAction= '00AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 71
IF @mAction= '00AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 72
IF @mAction= '00AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 73
IF @mAction= '00AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 74
IF @mAction= '00AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 75
--IF @mAction= '00AFAAFAFA'

-- Option No. 76
--IF @mAction= '00AFAAFAFF'

-- Option No. 77
IF @mAction= '00AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 78
IF @mAction= '00AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 79
IF @mAction= '00AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 80
IF @mAction= '00AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 81
IF @mAction= '00AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 82
IF @mAction= '00AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 83
--IF @mAction= '00AFAFAAFA'

-- Option No. 84
--IF @mAction= '00AFAFAAFF'

-- Option No. 85
IF @mAction= '00AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 86
IF @mAction= '00AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 87
IF @mAction= '00AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 88
IF @mAction= '00AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 89
IF @mAction= '00AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 90
IF @mAction= '00AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 91
--IF @mAction= '00AFAFFAFA'

-- Option No. 92
--IF @mAction= '00AFAFFAFF'

-- Option No. 93
IF @mAction= '00AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 94
IF @mAction= '00AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 95
IF @mAction= '00AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 96
IF @mAction= '00AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 97
IF @mAction= '00AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 98
IF @mAction= '00AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 99
--IF @mAction= '00AFFAAAFA'

-- Option No. 100
--IF @mAction= '00AFFAAAFF'

-- Option No. 101
IF @mAction= '00AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 102
IF @mAction= '00AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 103
IF @mAction= '00AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 104
IF @mAction= '00AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 105
IF @mAction= '00AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 106
IF @mAction= '00AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 107
--IF @mAction= '00AFFAFAFA'

-- Option No. 108
--IF @mAction= '00AFFAFAFF'

-- Option No. 109
IF @mAction= '00AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 110
IF @mAction= '00AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 111
IF @mAction= '00AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 112
IF @mAction= '00AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 113
IF @mAction= '00AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 114
IF @mAction= '00AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 115
--IF @mAction= '00AFFFAAFA'

-- Option No. 116
--IF @mAction= '00AFFFAAFF'

-- Option No. 117
IF @mAction= '00AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 118
IF @mAction= '00AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 119
IF @mAction= '00AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 120
IF @mAction= '00AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 121
IF @mAction= '00AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 122
IF @mAction= '00AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 123
--IF @mAction= '00AFFFFAFA'

-- Option No. 124
--IF @mAction= '00AFFFFAFF'

-- Option No. 125
IF @mAction= '00AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 126
IF @mAction= '00AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 127
IF @mAction= '00AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 128
IF @mAction= '00AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 129
IF @mAction= '00FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 130
IF @mAction= '00FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 131
--IF @mAction= '00FAAAAAFA'

-- Option No. 132
--IF @mAction= '00FAAAAAFF'

-- Option No. 133
IF @mAction= '00FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 134
IF @mAction= '00FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 135
IF @mAction= '00FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 136
IF @mAction= '00FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 138
IF @mAction= '00FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 138
IF @mAction= '00FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 139
--IF @mAction= '00FAAAFAFA'

-- Option No. 140
--IF @mAction= '00FAAAFAFF'

-- Option No. 141
IF @mAction= '00FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 142
IF @mAction= '00FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 143
IF @mAction= '00FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 144
IF @mAction= '00FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 145
IF @mAction= '00FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 146
IF @mAction= '00FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 147
--IF @mAction= '00FAAFAAFA'

-- Option No. 148
--IF @mAction= '00FAAFAAFF'

-- Option No. 149
IF @mAction= '00FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 150
IF @mAction= '00FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 151
IF @mAction= '00FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 152
IF @mAction= '00FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 153
IF @mAction= '00FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 154
IF @mAction= '00FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 155
--IF @mAction= '00FAAFFAFA'

-- Option No. 156
--IF @mAction= '00FAAFFAFF'

-- Option No. 157
IF @mAction= '00FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 158
IF @mAction= '00FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 159
IF @mAction= '00FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 160
IF @mAction= '00FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 161
IF @mAction= '00FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 162
IF @mAction= '00FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 163
--IF @mAction= '00FAFAAAFA'

-- Option No. 164
--IF @mAction= '00FAFAAAFF'

-- Option No. 165
IF @mAction= '00FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 166
IF @mAction= '00FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 167
IF @mAction= '00FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 168
IF @mAction= '00FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 169
IF @mAction= '00FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 170
IF @mAction= '00FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 171
--IF @mAction= '00FAFAFAFA'

-- Option No. 172
--IF @mAction= '00FAFAFAFF'

-- Option No. 173
IF @mAction= '00FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 174
IF @mAction= '00FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 175
IF @mAction= '00FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 176
IF @mAction= '00FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 177
IF @mAction= '00FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 178
IF @mAction= '00FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 179
--IF @mAction= '00FAFFAAFA'

-- Option No. 180
--IF @mAction= '00FAFFAAFF'

-- Option No. 181
IF @mAction= '00FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 182
IF @mAction= '00FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 183
IF @mAction= '00FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 184
IF @mAction= '00FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 185
IF @mAction= '00FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 186
IF @mAction= '00FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 187
--IF @mAction= '00FAFFFAFA'

-- Option No. 188
--IF @mAction= '00FAFFFAFF'

-- Option No. 189
IF @mAction= '00FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 190
IF @mAction= '00FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 191
IF @mAction= '00FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 192
IF @mAction= '00FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 193
IF @mAction= '00FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 194
IF @mAction= '00FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 195
--IF @mAction= '00FFAAAAFA'

-- Option No. 196
--IF @mAction= '00FFAAAAFF'

-- Option No. 197
IF @mAction= '00FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 198
IF @mAction= '00FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 199
IF @mAction= '00FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 200
IF @mAction= '00FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 201
IF @mAction= '00FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 202
IF @mAction= '00FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 203
--IF @mAction= '00FFAAFAFA'

-- Option No. 204
--IF @mAction= '00FFAAFAFF'

-- Option No. 205
IF @mAction= '00FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 206
IF @mAction= '00FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 207
IF @mAction= '00FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 208
IF @mAction= '00FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 209
IF @mAction= '00FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 210
IF @mAction= '00FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 211
--IF @mAction= '00FFAFAAFA'

-- Option No. 212
--IF @mAction= '00FFAFAAFF'

-- Option No. 213
IF @mAction= '00FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 214
IF @mAction= '00FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 215
IF @mAction= '00FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 216
IF @mAction= '00FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 217
IF @mAction= '00FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 218
IF @mAction= '00FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 219
--IF @mAction= '00FFAFFAFA'

-- Option No. 220
--IF @mAction= '00FFAFFAFF'

-- Option No. 221
IF @mAction= '00FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 222
IF @mAction= '00FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 223
IF @mAction= '00FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 224
IF @mAction= '00FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 225
IF @mAction= '00FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 226
IF @mAction= '00FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 227
--IF @mAction= '00FFFAAAFA'

-- Option No. 228
--IF @mAction= '00FFFAAAFF'

-- Option No. 229
IF @mAction= '00FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 230
IF @mAction= '00FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 231
IF @mAction= '00FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 232
IF @mAction= '00FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 233
IF @mAction= '00FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 234
IF @mAction= '00FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 235
--IF @mAction= '00FFFAFAFA'

-- Option No. 236
--IF @mAction= '00FFFAFAFF'

-- Option No. 237
IF @mAction= '00FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 238
IF @mAction= '00FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 239
IF @mAction= '00FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 240
IF @mAction= '00FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 241
IF @mAction= '00FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 242
IF @mAction= '00FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 243
--IF @mAction= '00FFFFAAFA'

-- Option No. 244
--IF @mAction= '00FFFFAAFF'

-- Option No. 245
IF @mAction= '00FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 246
IF @mAction= '00FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 247
IF @mAction= '00FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 248
IF @mAction= '00FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 249
IF @mAction= '00FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 250
IF @mAction= '00FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 251
--IF @mAction= '00FFFFFAFA'

-- Option No. 252
--IF @mAction= '00FFFFFAFF'

-- Option No. 253
IF @mAction= '00FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 254
IF @mAction= '00FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 255
IF @mAction= '00FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 256
IF @mAction= '00FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
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
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

--SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
--                         / SUM(INVDTL.quantity) AS Average
--FROM            dbo.INVOICE AS INV INNER JOIN
--                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
--                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
--                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
--                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
--                         dbo.SalesOrder ON SOD.SalesOrderID = dbo.SalesOrder.ID
--WHERE        (Cast(INV.InvoiceDate As Date) >= '2018-07-01')
--GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
--ORDER BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew







GO
