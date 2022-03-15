USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportMouldWB]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- drop Proc proc_SaleAnalysisReportMouldWB

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportMouldWB]
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
IF @mAction= '01AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 2
IF @mAction= '01AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 3
--IF @mAction= '01AAAAAAFA'

-- Option No. 4
--IF @mAction= '01AAAAAAFF'

-- Option No. 5
IF @mAction= '01AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 6
IF @mAction= '01AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 7
IF @mAction= '01AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 8
IF @mAction= '01AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 9
IF @mAction= '01AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 10
IF @mAction= '01AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 11
--IF @mAction= '01AAAAFAFA'

-- Option No. 12
--IF @mAction= '01AAAAFAFF'

-- Option No. 13
IF @mAction= '01AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 14
IF @mAction= '01AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 15
IF @mAction= '01AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 16
IF @mAction= '01AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 17
IF @mAction= '01AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 18
IF @mAction= '01AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 19
--IF @mAction= '01AAAFAAFA'

-- Option No. 20
--IF @mAction= '01AAAFAAFF'

-- Option No. 21
IF @mAction= '01AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 22
IF @mAction= '01AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 23
IF @mAction= '01AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 24
IF @mAction= '01AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 25
IF @mAction= '01AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 26
IF @mAction= '01AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 27
--IF @mAction= '01AAAFFAFA'

-- Option No. 28
--IF @mAction= '01AAAFFAFF'

-- Option No. 29
IF @mAction= '01AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 30
IF @mAction= '01AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 31
IF @mAction= '01AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 32
IF @mAction= '01AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 33
IF @mAction= '01AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 34
IF @mAction= '01AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 35
--IF @mAction= '01AAFAAAFA'

-- Option No. 36
--IF @mAction= '01AAFAAAFF'

-- Option No. 37
IF @mAction= '01AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 38
IF @mAction= '01AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 39
IF @mAction= '01AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 40
IF @mAction= '01AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 41
IF @mAction= '01AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 42
IF @mAction= '01AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 43
--IF @mAction= '01AAFAFAFA'

-- Option No. 44
--IF @mAction= '01AAFAFAFF'

-- Option No. 45
IF @mAction= '01AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 46
IF @mAction= '01AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 47
IF @mAction= '01AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 48
IF @mAction= '01AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 49
IF @mAction= '01AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 50
IF @mAction= '01AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 51
--IF @mAction= '01AAFFAAFA'

-- Option No. 52
--IF @mAction= '01AAFFAAFF'

-- Option No. 53
IF @mAction= '01AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 54
IF @mAction= '01AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 55
IF @mAction= '01AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 56
IF @mAction= '01AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 57
IF @mAction= '01AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 58
IF @mAction= '01AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 59
--IF @mAction= '01AAFFFAFA'

-- Option No. 60
--IF @mAction= '01AAFFFAFF'

-- Option No. 61
IF @mAction= '01AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 62
IF @mAction= '01AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 63
IF @mAction= '01AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 64
IF @mAction= '01AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 65
IF @mAction= '01AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 66
IF @mAction= '01AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 67
--IF @mAction= '01AFAAAAFA'

-- Option No. 68
--IF @mAction= '01AFAAAAFF'

-- Option No. 69
IF @mAction= '01AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 70
IF @mAction= '01AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 71
IF @mAction= '01AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 72
IF @mAction= '01AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 73
IF @mAction= '01AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 74
IF @mAction= '01AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 75
--IF @mAction= '01AFAAFAFA'

-- Option No. 76
--IF @mAction= '01AFAAFAFF'

-- Option No. 77
IF @mAction= '01AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 78
IF @mAction= '01AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 79
IF @mAction= '01AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 80
IF @mAction= '01AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 81
IF @mAction= '01AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 82
IF @mAction= '01AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 83
--IF @mAction= '01AFAFAAFA'

-- Option No. 84
--IF @mAction= '01AFAFAAFF'

-- Option No. 85
IF @mAction= '01AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 86
IF @mAction= '01AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 87
IF @mAction= '01AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 88
IF @mAction= '01AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 89
IF @mAction= '01AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 90
IF @mAction= '01AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 91
--IF @mAction= '01AFAFFAFA'

-- Option No. 92
--IF @mAction= '01AFAFFAFF'

-- Option No. 93
IF @mAction= '01AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 94
IF @mAction= '01AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 95
IF @mAction= '01AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 96
IF @mAction= '01AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 97
IF @mAction= '01AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 98
IF @mAction= '01AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 99
--IF @mAction= '01AFFAAAFA'

-- Option No. 100
--IF @mAction= '01AFFAAAFF'

-- Option No. 101
IF @mAction= '01AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 102
IF @mAction= '01AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 103
IF @mAction= '01AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 104
IF @mAction= '01AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 105
IF @mAction= '01AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 106
IF @mAction= '01AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 107
--IF @mAction= '01AFFAFAFA'

-- Option No. 108
--IF @mAction= '01AFFAFAFF'

-- Option No. 109
IF @mAction= '01AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 110
IF @mAction= '01AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 111
IF @mAction= '01AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 112
IF @mAction= '01AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 113
IF @mAction= '01AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 114
IF @mAction= '01AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 115
--IF @mAction= '01AFFFAAFA'

-- Option No. 116
--IF @mAction= '01AFFFAAFF'

-- Option No. 117
IF @mAction= '01AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 118
IF @mAction= '01AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 119
IF @mAction= '01AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 120
IF @mAction= '01AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 121
IF @mAction= '01AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 122
IF @mAction= '01AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 123
--IF @mAction= '01AFFFFAFA'

-- Option No. 124
--IF @mAction= '01AFFFFAFF'

-- Option No. 125
IF @mAction= '01AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 126
IF @mAction= '01AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 127
IF @mAction= '01AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 128
IF @mAction= '01AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 129
IF @mAction= '01FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 130
IF @mAction= '01FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 131
--IF @mAction= '01FAAAAAFA'

-- Option No. 132
--IF @mAction= '01FAAAAAFF'

-- Option No. 133
IF @mAction= '01FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 134
IF @mAction= '01FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 135
IF @mAction= '01FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 136
IF @mAction= '01FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 138
IF @mAction= '01FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 138
IF @mAction= '01FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 139
--IF @mAction= '01FAAAFAFA'

-- Option No. 140
--IF @mAction= '01FAAAFAFF'

-- Option No. 141
IF @mAction= '01FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 142
IF @mAction= '01FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 143
IF @mAction= '01FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 144
IF @mAction= '01FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 145
IF @mAction= '01FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 146
IF @mAction= '01FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 147
--IF @mAction= '01FAAFAAFA'

-- Option No. 148
--IF @mAction= '01FAAFAAFF'

-- Option No. 149
IF @mAction= '01FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 150
IF @mAction= '01FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 151
IF @mAction= '01FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 152
IF @mAction= '01FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 153
IF @mAction= '01FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 154
IF @mAction= '01FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 155
--IF @mAction= '01FAAFFAFA'

-- Option No. 156
--IF @mAction= '01FAAFFAFF'

-- Option No. 157
IF @mAction= '01FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 158
IF @mAction= '01FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 159
IF @mAction= '01FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 160
IF @mAction= '01FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 161
IF @mAction= '01FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 162
IF @mAction= '01FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 163
--IF @mAction= '01FAFAAAFA'

-- Option No. 164
--IF @mAction= '01FAFAAAFF'

-- Option No. 165
IF @mAction= '01FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 166
IF @mAction= '01FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 167
IF @mAction= '01FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 168
IF @mAction= '01FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 169
IF @mAction= '01FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 170
IF @mAction= '01FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 171
--IF @mAction= '01FAFAFAFA'

-- Option No. 172
--IF @mAction= '01FAFAFAFF'

-- Option No. 173
IF @mAction= '01FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 174
IF @mAction= '01FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 175
IF @mAction= '01FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 176
IF @mAction= '01FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 177
IF @mAction= '01FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 178
IF @mAction= '01FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 179
--IF @mAction= '01FAFFAAFA'

-- Option No. 180
--IF @mAction= '01FAFFAAFF'

-- Option No. 181
IF @mAction= '01FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 182
IF @mAction= '01FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 183
IF @mAction= '01FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 184
IF @mAction= '01FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 185
IF @mAction= '01FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 186
IF @mAction= '01FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 187
--IF @mAction= '01FAFFFAFA'

-- Option No. 188
--IF @mAction= '01FAFFFAFF'

-- Option No. 189
IF @mAction= '01FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 190
IF @mAction= '01FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 191
IF @mAction= '01FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 192
IF @mAction= '01FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 193
IF @mAction= '01FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 194
IF @mAction= '01FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 195
--IF @mAction= '01FFAAAAFA'

-- Option No. 196
--IF @mAction= '01FFAAAAFF'

-- Option No. 197
IF @mAction= '01FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 198
IF @mAction= '01FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 199
IF @mAction= '01FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 200
IF @mAction= '01FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 201
IF @mAction= '01FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 202
IF @mAction= '01FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 203
--IF @mAction= '01FFAAFAFA'

-- Option No. 204
--IF @mAction= '01FFAAFAFF'

-- Option No. 205
IF @mAction= '01FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 206
IF @mAction= '01FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 207
IF @mAction= '01FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 208
IF @mAction= '01FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 209
IF @mAction= '01FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 210
IF @mAction= '01FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 211
--IF @mAction= '01FFAFAAFA'

-- Option No. 212
--IF @mAction= '01FFAFAAFF'

-- Option No. 213
IF @mAction= '01FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 214
IF @mAction= '01FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 215
IF @mAction= '01FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 216
IF @mAction= '01FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 217
IF @mAction= '01FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 218
IF @mAction= '01FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 219
--IF @mAction= '01FFAFFAFA'

-- Option No. 220
--IF @mAction= '01FFAFFAFF'

-- Option No. 221
IF @mAction= '01FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 222
IF @mAction= '01FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 223
IF @mAction= '01FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 224
IF @mAction= '01FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 225
IF @mAction= '01FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 226
IF @mAction= '01FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 227
--IF @mAction= '01FFFAAAFA'

-- Option No. 228
--IF @mAction= '01FFFAAAFF'

-- Option No. 229
IF @mAction= '01FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 230
IF @mAction= '01FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 231
IF @mAction= '01FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 232
IF @mAction= '01FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 233
IF @mAction= '01FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 234
IF @mAction= '01FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 235
--IF @mAction= '01FFFAFAFA'

-- Option No. 236
--IF @mAction= '01FFFAFAFF'

-- Option No. 237
IF @mAction= '01FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 238
IF @mAction= '01FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 239
IF @mAction= '01FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 240
IF @mAction= '01FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 241
IF @mAction= '01FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 242
IF @mAction= '01FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 243
--IF @mAction= '01FFFFAAFA'

-- Option No. 244
--IF @mAction= '01FFFFAAFF'

-- Option No. 245
IF @mAction= '01FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 246
IF @mAction= '01FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 247
IF @mAction= '01FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 248
IF @mAction= '01FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 249
IF @mAction= '01FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 250
IF @mAction= '01FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 251
--IF @mAction= '01FFFFFAFA'

-- Option No. 252
--IF @mAction= '01FFFFFAFF'

-- Option No. 253
IF @mAction= '01FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 254
IF @mAction= '01FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 255
IF @mAction= '01FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 256
IF @mAction= '01FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
                         / SUM(INVDTL.quantity) AS Average
	FROM            dbo.INVOICE AS INV INNER JOIN
                         dbo.Buyer AS Buy ON INV.Buyer = Buy.BuyerCode INNER JOIN
                         dbo.InvoiceDetail AS INVDTL ON INV.InvoiceNo = INVDTL.invoiceno INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.ArticleNo = MAT.MaterialCode INNER JOIN
                         dbo.SalesOrderDetails AS SOD ON INVDTL.SalesOrderDetailID = SOD.ID INNER JOIN
                         dbo.SalesOrder As SO ON SOD.SalesOrderID = SO.ID
	WHERE         (Cast(INV.InvoiceDate As Date) >= @mFromDate) And (Cast(INV.InvoiceDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
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
--GROUP BY MAT.ArticleMould, MAT.ArticleMould, MAT.CodificationNew
--ORDER BY MAT.ArticleMould, MAT.ArticleMould, MAT.CodificationNew







GO
