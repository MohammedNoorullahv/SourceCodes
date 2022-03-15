USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportORDMouldWPTYPE]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc proc_SaleAnalysisReportORDMouldWPTYPE

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportORDMouldWPTYPE]
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
@mProductTypeMain				Varchar(50)		=Null


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

--'' As BuyerName, MAT.ArticleMould
--'' As BuyerName, MAT.ArticleMould
-- Option No. 1
IF @mAction= '11AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 2
IF @mAction= '11AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 3
--IF @mAction= '11AAAAAAFA'

-- Option No. 4
--IF @mAction= '11AAAAAAFF'

-- Option No. 5
IF @mAction= '11AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 6
IF @mAction= '11AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 7
IF @mAction= '11AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 8
IF @mAction= '11AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 9
IF @mAction= '11AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 10
IF @mAction= '11AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 11
--IF @mAction= '11AAAAFAFA'

-- Option No. 12
--IF @mAction= '11AAAAFAFF'

-- Option No. 13
IF @mAction= '11AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 14
IF @mAction= '11AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 15
IF @mAction= '11AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 16
IF @mAction= '11AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 17
IF @mAction= '11AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 18
IF @mAction= '11AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 19
--IF @mAction= '11AAAFAAFA'

-- Option No. 20
--IF @mAction= '11AAAFAAFF'

-- Option No. 21
IF @mAction= '11AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 22
IF @mAction= '11AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 23
IF @mAction= '11AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 24
IF @mAction= '11AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 25
IF @mAction= '11AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 26
IF @mAction= '11AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 27
--IF @mAction= '11AAAFFAFA'

-- Option No. 28
--IF @mAction= '11AAAFFAFF'

-- Option No. 29
IF @mAction= '11AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 30
IF @mAction= '11AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 31
IF @mAction= '11AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 32
IF @mAction= '11AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 33
IF @mAction= '11AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 34
IF @mAction= '11AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 35
--IF @mAction= '11AAFAAAFA'

-- Option No. 36
--IF @mAction= '11AAFAAAFF'

-- Option No. 37
IF @mAction= '11AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 38
IF @mAction= '11AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 39
IF @mAction= '11AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 40
IF @mAction= '11AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 41
IF @mAction= '11AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 42
IF @mAction= '11AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 43
--IF @mAction= '11AAFAFAFA'

-- Option No. 44
--IF @mAction= '11AAFAFAFF'

-- Option No. 45
IF @mAction= '11AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 46
IF @mAction= '11AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 47
IF @mAction= '11AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 48
IF @mAction= '11AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 49
IF @mAction= '11AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 50
IF @mAction= '11AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 51
--IF @mAction= '11AAFFAAFA'

-- Option No. 52
--IF @mAction= '11AAFFAAFF'

-- Option No. 53
IF @mAction= '11AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 54
IF @mAction= '11AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 55
IF @mAction= '11AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 56
IF @mAction= '11AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 57
IF @mAction= '11AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 58
IF @mAction= '11AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 59
--IF @mAction= '11AAFFFAFA'

-- Option No. 60
--IF @mAction= '11AAFFFAFF'

-- Option No. 61
IF @mAction= '11AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 62
IF @mAction= '11AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 63
IF @mAction= '11AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 64
IF @mAction= '11AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 65
IF @mAction= '11AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 66
IF @mAction= '11AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 67
--IF @mAction= '11AFAAAAFA'

-- Option No. 68
--IF @mAction= '11AFAAAAFF'

-- Option No. 69
IF @mAction= '11AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 70
IF @mAction= '11AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 71
IF @mAction= '11AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 72
IF @mAction= '11AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 73
IF @mAction= '11AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 74
IF @mAction= '11AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 75
--IF @mAction= '11AFAAFAFA'

-- Option No. 76
--IF @mAction= '11AFAAFAFF'

-- Option No. 77
IF @mAction= '11AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 78
IF @mAction= '11AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 79
IF @mAction= '11AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 80
IF @mAction= '11AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 81
IF @mAction= '11AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 82
IF @mAction= '11AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 83
--IF @mAction= '11AFAFAAFA'

-- Option No. 84
--IF @mAction= '11AFAFAAFF'

-- Option No. 85
IF @mAction= '11AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 86
IF @mAction= '11AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 87
IF @mAction= '11AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 88
IF @mAction= '11AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 89
IF @mAction= '11AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 90
IF @mAction= '11AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 91
--IF @mAction= '11AFAFFAFA'

-- Option No. 92
--IF @mAction= '11AFAFFAFF'

-- Option No. 93
IF @mAction= '11AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 94
IF @mAction= '11AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 95
IF @mAction= '11AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 96
IF @mAction= '11AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 97
IF @mAction= '11AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 98
IF @mAction= '11AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 99
--IF @mAction= '11AFFAAAFA'

-- Option No. 100
--IF @mAction= '11AFFAAAFF'

-- Option No. 101
IF @mAction= '11AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 112
IF @mAction= '11AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 103
IF @mAction= '11AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 104
IF @mAction= '11AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 105
IF @mAction= '11AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 106
IF @mAction= '11AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 107
--IF @mAction= '11AFFAFAFA'

-- Option No. 108
--IF @mAction= '11AFFAFAFF'

-- Option No. 109
IF @mAction= '11AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 110
IF @mAction= '11AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 111
IF @mAction= '11AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 112
IF @mAction= '11AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 113
IF @mAction= '11AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 114
IF @mAction= '11AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 115
--IF @mAction= '11AFFFAAFA'

-- Option No. 116
--IF @mAction= '11AFFFAAFF'

-- Option No. 117
IF @mAction= '11AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 118
IF @mAction= '11AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 119
IF @mAction= '11AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 120
IF @mAction= '11AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 121
IF @mAction= '11AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 122
IF @mAction= '11AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 123
--IF @mAction= '11AFFFFAFA'

-- Option No. 124
--IF @mAction= '11AFFFFAFF'

-- Option No. 125
IF @mAction= '11AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 126
IF @mAction= '11AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 127
IF @mAction= '11AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 128
IF @mAction= '11AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
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
IF @mAction= '11FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 130
IF @mAction= '11FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 131
--IF @mAction= '11FAAAAAFA'

-- Option No. 132
--IF @mAction= '11FAAAAAFF'

-- Option No. 133
IF @mAction= '11FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 134
IF @mAction= '11FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 135
IF @mAction= '11FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 136
IF @mAction= '11FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 138
IF @mAction= '11FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 138
IF @mAction= '11FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 139
--IF @mAction= '11FAAAFAFA'

-- Option No. 140
--IF @mAction= '11FAAAFAFF'

-- Option No. 141
IF @mAction= '11FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 142
IF @mAction= '11FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 143
IF @mAction= '11FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 144
IF @mAction= '11FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 145
IF @mAction= '11FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 146
IF @mAction= '11FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 147
--IF @mAction= '11FAAFAAFA'

-- Option No. 148
--IF @mAction= '11FAAFAAFF'

-- Option No. 149
IF @mAction= '11FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 150
IF @mAction= '11FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 151
IF @mAction= '11FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 152
IF @mAction= '11FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 153
IF @mAction= '11FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 154
IF @mAction= '11FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 155
--IF @mAction= '11FAAFFAFA'

-- Option No. 156
--IF @mAction= '11FAAFFAFF'

-- Option No. 157
IF @mAction= '11FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 158
IF @mAction= '11FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 159
IF @mAction= '11FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 160
IF @mAction= '11FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 161
IF @mAction= '11FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 162
IF @mAction= '11FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 163
--IF @mAction= '11FAFAAAFA'

-- Option No. 164
--IF @mAction= '11FAFAAAFF'

-- Option No. 165
IF @mAction= '11FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 166
IF @mAction= '11FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 167
IF @mAction= '11FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 168
IF @mAction= '11FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 169
IF @mAction= '11FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 170
IF @mAction= '11FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 171
--IF @mAction= '11FAFAFAFA'

-- Option No. 172
--IF @mAction= '11FAFAFAFF'

-- Option No. 173
IF @mAction= '11FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 174
IF @mAction= '11FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 175
IF @mAction= '11FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 176
IF @mAction= '11FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 177
IF @mAction= '11FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 178
IF @mAction= '11FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 179
--IF @mAction= '11FAFFAAFA'

-- Option No. 180
--IF @mAction= '11FAFFAAFF'

-- Option No. 181
IF @mAction= '11FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 182
IF @mAction= '11FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 183
IF @mAction= '11FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 184
IF @mAction= '11FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 185
IF @mAction= '11FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 186
IF @mAction= '11FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 187
--IF @mAction= '11FAFFFAFA'

-- Option No. 188
--IF @mAction= '11FAFFFAFF'

-- Option No. 189
IF @mAction= '11FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 190
IF @mAction= '11FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 191
IF @mAction= '11FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 192
IF @mAction= '11FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 193
IF @mAction= '11FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 194
IF @mAction= '11FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 195
--IF @mAction= '11FFAAAAFA'

-- Option No. 196
--IF @mAction= '11FFAAAAFF'

-- Option No. 197
IF @mAction= '11FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 198
IF @mAction= '11FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 199
IF @mAction= '11FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 200
IF @mAction= '11FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 201
IF @mAction= '11FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 212
IF @mAction= '11FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 203
--IF @mAction= '11FFAAFAFA'

-- Option No. 204
--IF @mAction= '11FFAAFAFF'

-- Option No. 205
IF @mAction= '11FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 206
IF @mAction= '11FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 207
IF @mAction= '11FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 208
IF @mAction= '11FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 209
IF @mAction= '11FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 210
IF @mAction= '11FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 211
--IF @mAction= '11FFAFAAFA'

-- Option No. 212
--IF @mAction= '11FFAFAAFF'

-- Option No. 213
IF @mAction= '11FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 214
IF @mAction= '11FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 215
IF @mAction= '11FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 216
IF @mAction= '11FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 217
IF @mAction= '11FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 218
IF @mAction= '11FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 219
--IF @mAction= '11FFAFFAFA'

-- Option No. 220
--IF @mAction= '11FFAFFAFF'

-- Option No. 221
IF @mAction= '11FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 222
IF @mAction= '11FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 223
IF @mAction= '11FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 224
IF @mAction= '11FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 225
IF @mAction= '11FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 226
IF @mAction= '11FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 227
--IF @mAction= '11FFFAAAFA'

-- Option No. 228
--IF @mAction= '11FFFAAAFF'

-- Option No. 229
IF @mAction= '11FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 230
IF @mAction= '11FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 231
IF @mAction= '11FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 232
IF @mAction= '11FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 233
IF @mAction= '11FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 234
IF @mAction= '11FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 235
--IF @mAction= '11FFFAFAFA'

-- Option No. 236
--IF @mAction= '11FFFAFAFF'

-- Option No. 237
IF @mAction= '11FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 238
IF @mAction= '11FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 239
IF @mAction= '11FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 240
IF @mAction= '11FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 241
IF @mAction= '11FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 242
IF @mAction= '11FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 243
--IF @mAction= '11FFFFAAFA'

-- Option No. 244
--IF @mAction= '11FFFFAAFF'

-- Option No. 245
IF @mAction= '11FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 246
IF @mAction= '11FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 247
IF @mAction= '11FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 248
IF @mAction= '11FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 249
IF @mAction= '11FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 250
IF @mAction= '11FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 251
--IF @mAction= '11FFFFFAFA'

-- Option No. 252
--IF @mAction= '11FFFFFAFF'

-- Option No. 253
IF @mAction= '11FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 254
IF @mAction= '11FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 255
IF @mAction= '11FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 256
IF @mAction= '11FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (SOD.OrderReceivedDate >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END






GO
