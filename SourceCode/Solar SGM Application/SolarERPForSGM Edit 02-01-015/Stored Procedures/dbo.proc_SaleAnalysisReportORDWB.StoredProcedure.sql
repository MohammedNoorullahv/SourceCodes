USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportORDWB]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- drop Proc proc_SaleAnalysisReportORDWB

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportORDWB]
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
IF @mAction= '10AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 2
IF @mAction= '10AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 3
--IF @mAction= '10AAAAAAFA'

-- Option No. 4
--IF @mAction= '10AAAAAAFF'

-- Option No. 5
IF @mAction= '10AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 6
IF @mAction= '10AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 7
IF @mAction= '10AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 8
IF @mAction= '10AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 9
IF @mAction= '10AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 10
IF @mAction= '10AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 11
--IF @mAction= '10AAAAFAFA'

-- Option No. 12
--IF @mAction= '10AAAAFAFF'

-- Option No. 13
IF @mAction= '10AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 14
IF @mAction= '10AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 15
IF @mAction= '10AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 16
IF @mAction= '10AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 17
IF @mAction= '10AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 18
IF @mAction= '10AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 19
--IF @mAction= '10AAAFAAFA'

-- Option No. 20
--IF @mAction= '10AAAFAAFF'

-- Option No. 21
IF @mAction= '10AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 22
IF @mAction= '10AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 23
IF @mAction= '10AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 24
IF @mAction= '10AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 25
IF @mAction= '10AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 26
IF @mAction= '10AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 27
--IF @mAction= '10AAAFFAFA'

-- Option No. 28
--IF @mAction= '10AAAFFAFF'

-- Option No. 29
IF @mAction= '10AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 30
IF @mAction= '10AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 31
IF @mAction= '10AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 32
IF @mAction= '10AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 33
IF @mAction= '10AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 34
IF @mAction= '10AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 35
--IF @mAction= '10AAFAAAFA'

-- Option No. 36
--IF @mAction= '10AAFAAAFF'

-- Option No. 37
IF @mAction= '10AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 38
IF @mAction= '10AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 39
IF @mAction= '10AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 40
IF @mAction= '10AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 41
IF @mAction= '10AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 42
IF @mAction= '10AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 43
--IF @mAction= '10AAFAFAFA'

-- Option No. 44
--IF @mAction= '10AAFAFAFF'

-- Option No. 45
IF @mAction= '10AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 46
IF @mAction= '10AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 47
IF @mAction= '10AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 48
IF @mAction= '10AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 49
IF @mAction= '10AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 50
IF @mAction= '10AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 51
--IF @mAction= '10AAFFAAFA'

-- Option No. 52
--IF @mAction= '10AAFFAAFF'

-- Option No. 53
IF @mAction= '10AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 54
IF @mAction= '10AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 55
IF @mAction= '10AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 56
IF @mAction= '10AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 57
IF @mAction= '10AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 58
IF @mAction= '10AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 59
--IF @mAction= '10AAFFFAFA'

-- Option No. 60
--IF @mAction= '10AAFFFAFF'

-- Option No. 61
IF @mAction= '10AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 62
IF @mAction= '10AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 63
IF @mAction= '10AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 64
IF @mAction= '10AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 65
IF @mAction= '10AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 66
IF @mAction= '10AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 67
--IF @mAction= '10AFAAAAFA'

-- Option No. 68
--IF @mAction= '10AFAAAAFF'

-- Option No. 69
IF @mAction= '10AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 70
IF @mAction= '10AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 71
IF @mAction= '10AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 72
IF @mAction= '10AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 73
IF @mAction= '10AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 74
IF @mAction= '10AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 75
--IF @mAction= '10AFAAFAFA'

-- Option No. 76
--IF @mAction= '10AFAAFAFF'

-- Option No. 77
IF @mAction= '10AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 78
IF @mAction= '10AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 79
IF @mAction= '10AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 80
IF @mAction= '10AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 81
IF @mAction= '10AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 82
IF @mAction= '10AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 83
--IF @mAction= '10AFAFAAFA'

-- Option No. 84
--IF @mAction= '10AFAFAAFF'

-- Option No. 85
IF @mAction= '10AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 86
IF @mAction= '10AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 87
IF @mAction= '10AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 88
IF @mAction= '10AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 89
IF @mAction= '10AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 90
IF @mAction= '10AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 91
--IF @mAction= '10AFAFFAFA'

-- Option No. 92
--IF @mAction= '10AFAFFAFF'

-- Option No. 93
IF @mAction= '10AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 94
IF @mAction= '10AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 95
IF @mAction= '10AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 96
IF @mAction= '10AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 97
IF @mAction= '10AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 98
IF @mAction= '10AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 99
--IF @mAction= '10AFFAAAFA'

-- Option No. 100
--IF @mAction= '10AFFAAAFF'

-- Option No. 101
IF @mAction= '10AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 112
IF @mAction= '10AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 103
IF @mAction= '10AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 104
IF @mAction= '10AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 105
IF @mAction= '10AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 106
IF @mAction= '10AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 107
--IF @mAction= '10AFFAFAFA'

-- Option No. 108
--IF @mAction= '10AFFAFAFF'

-- Option No. 109
IF @mAction= '10AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 110
IF @mAction= '10AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 111
IF @mAction= '10AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 112
IF @mAction= '10AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 113
IF @mAction= '10AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 114
IF @mAction= '10AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 115
--IF @mAction= '10AFFFAAFA'

-- Option No. 116
--IF @mAction= '10AFFFAAFF'

-- Option No. 117
IF @mAction= '10AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 118
IF @mAction= '10AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 119
IF @mAction= '10AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 120
IF @mAction= '10AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 121
IF @mAction= '10AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 122
IF @mAction= '10AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 123
--IF @mAction= '10AFFFFAFA'

-- Option No. 124
--IF @mAction= '10AFFFFAFF'

-- Option No. 125
IF @mAction= '10AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 126
IF @mAction= '10AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 127
IF @mAction= '10AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 128
IF @mAction= '10AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 129
IF @mAction= '10FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 130
IF @mAction= '10FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 131
--IF @mAction= '10FAAAAAFA'

-- Option No. 132
--IF @mAction= '10FAAAAAFF'

-- Option No. 133
IF @mAction= '10FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 134
IF @mAction= '10FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 135
IF @mAction= '10FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 136
IF @mAction= '10FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 138
IF @mAction= '10FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 138
IF @mAction= '10FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 139
--IF @mAction= '10FAAAFAFA'

-- Option No. 140
--IF @mAction= '10FAAAFAFF'

-- Option No. 141
IF @mAction= '10FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 142
IF @mAction= '10FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 143
IF @mAction= '10FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 144
IF @mAction= '10FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 145
IF @mAction= '10FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 146
IF @mAction= '10FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 147
--IF @mAction= '10FAAFAAFA'

-- Option No. 148
--IF @mAction= '10FAAFAAFF'

-- Option No. 149
IF @mAction= '10FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 150
IF @mAction= '10FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 151
IF @mAction= '10FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 152
IF @mAction= '10FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 153
IF @mAction= '10FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 154
IF @mAction= '10FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 155
--IF @mAction= '10FAAFFAFA'

-- Option No. 156
--IF @mAction= '10FAAFFAFF'

-- Option No. 157
IF @mAction= '10FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 158
IF @mAction= '10FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 159
IF @mAction= '10FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 160
IF @mAction= '10FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 161
IF @mAction= '10FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 162
IF @mAction= '10FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 163
--IF @mAction= '10FAFAAAFA'

-- Option No. 164
--IF @mAction= '10FAFAAAFF'

-- Option No. 165
IF @mAction= '10FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 166
IF @mAction= '10FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 167
IF @mAction= '10FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 168
IF @mAction= '10FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 169
IF @mAction= '10FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 170
IF @mAction= '10FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 171
--IF @mAction= '10FAFAFAFA'

-- Option No. 172
--IF @mAction= '10FAFAFAFF'

-- Option No. 173
IF @mAction= '10FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 174
IF @mAction= '10FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 175
IF @mAction= '10FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 176
IF @mAction= '10FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 177
IF @mAction= '10FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 178
IF @mAction= '10FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 179
--IF @mAction= '10FAFFAAFA'

-- Option No. 180
--IF @mAction= '10FAFFAAFF'

-- Option No. 181
IF @mAction= '10FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 182
IF @mAction= '10FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 183
IF @mAction= '10FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 184
IF @mAction= '10FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 185
IF @mAction= '10FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 186
IF @mAction= '10FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 187
--IF @mAction= '10FAFFFAFA'

-- Option No. 188
--IF @mAction= '10FAFFFAFF'

-- Option No. 189
IF @mAction= '10FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 190
IF @mAction= '10FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 191
IF @mAction= '10FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 192
IF @mAction= '10FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 193
IF @mAction= '10FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 194
IF @mAction= '10FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 195
--IF @mAction= '10FFAAAAFA'

-- Option No. 196
--IF @mAction= '10FFAAAAFF'

-- Option No. 197
IF @mAction= '10FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 198
IF @mAction= '10FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 199
IF @mAction= '10FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 200
IF @mAction= '10FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 201
IF @mAction= '10FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 212
IF @mAction= '10FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 203
--IF @mAction= '10FFAAFAFA'

-- Option No. 204
--IF @mAction= '10FFAAFAFF'

-- Option No. 205
IF @mAction= '10FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 206
IF @mAction= '10FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 207
IF @mAction= '10FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 208
IF @mAction= '10FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 209
IF @mAction= '10FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 210
IF @mAction= '10FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 211
--IF @mAction= '10FFAFAAFA'

-- Option No. 212
--IF @mAction= '10FFAFAAFF'

-- Option No. 213
IF @mAction= '10FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 214
IF @mAction= '10FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 215
IF @mAction= '10FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 216
IF @mAction= '10FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 217
IF @mAction= '10FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 218
IF @mAction= '10FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 219
--IF @mAction= '10FFAFFAFA'

-- Option No. 220
--IF @mAction= '10FFAFFAFF'

-- Option No. 221
IF @mAction= '10FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 222
IF @mAction= '10FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 223
IF @mAction= '10FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 224
IF @mAction= '10FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 225
IF @mAction= '10FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 226
IF @mAction= '10FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 227
--IF @mAction= '10FFFAAAFA'

-- Option No. 228
--IF @mAction= '10FFFAAAFF'

-- Option No. 229
IF @mAction= '10FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 230
IF @mAction= '10FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 231
IF @mAction= '10FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 232
IF @mAction= '10FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 233
IF @mAction= '10FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 234
IF @mAction= '10FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 235
--IF @mAction= '10FFFAFAFA'

-- Option No. 236
--IF @mAction= '10FFFAFAFF'

-- Option No. 237
IF @mAction= '10FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 238
IF @mAction= '10FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 239
IF @mAction= '10FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 240
IF @mAction= '10FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 241
IF @mAction= '10FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 242
IF @mAction= '10FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 243
--IF @mAction= '10FFFFAAFA'

-- Option No. 244
--IF @mAction= '10FFFFAAFF'

-- Option No. 245
IF @mAction= '10FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 246
IF @mAction= '10FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 247
IF @mAction= '10FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 248
IF @mAction= '10FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 249
IF @mAction= '10FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 250
IF @mAction= '10FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 251
--IF @mAction= '10FFFFFAFA'

-- Option No. 252
--IF @mAction= '10FFFFFAFF'

-- Option No. 253
IF @mAction= '10FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 254
IF @mAction= '10FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 255
IF @mAction= '10FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END

-- Option No. 256
IF @mAction= '10FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, '' As ArticleMould, '' AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName
	ORDER BY SO.BuyerName
END








GO
