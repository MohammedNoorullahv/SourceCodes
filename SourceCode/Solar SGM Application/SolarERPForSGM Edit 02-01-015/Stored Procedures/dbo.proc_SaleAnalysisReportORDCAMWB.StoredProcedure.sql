USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportORDCAMWB]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- drop Proc proc_SaleAnalysisReportORDCAMWB

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportORDCAMWB]
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
IF @mAction= '12AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 2
IF @mAction= '12AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 3
--IF @mAction= '12AAAAAAFA'

-- Option No. 4
--IF @mAction= '12AAAAAAFF'

-- Option No. 5
IF @mAction= '12AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 6
IF @mAction= '12AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 7
IF @mAction= '12AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 8
IF @mAction= '12AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 9
IF @mAction= '12AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 10
IF @mAction= '12AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 11
--IF @mAction= '12AAAAFAFA'

-- Option No. 12
--IF @mAction= '12AAAAFAFF'

-- Option No. 13
IF @mAction= '12AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 14
IF @mAction= '12AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 15
IF @mAction= '12AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 16
IF @mAction= '12AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 17
IF @mAction= '12AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 18
IF @mAction= '12AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 19
--IF @mAction= '12AAAFAAFA'

-- Option No. 20
--IF @mAction= '12AAAFAAFF'

-- Option No. 21
IF @mAction= '12AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 22
IF @mAction= '12AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 23
IF @mAction= '12AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 24
IF @mAction= '12AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 25
IF @mAction= '12AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 26
IF @mAction= '12AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 27
--IF @mAction= '12AAAFFAFA'

-- Option No. 28
--IF @mAction= '12AAAFFAFF'

-- Option No. 29
IF @mAction= '12AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 30
IF @mAction= '12AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 31
IF @mAction= '12AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 32
IF @mAction= '12AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 33
IF @mAction= '12AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 34
IF @mAction= '12AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 35
--IF @mAction= '12AAFAAAFA'

-- Option No. 36
--IF @mAction= '12AAFAAAFF'

-- Option No. 37
IF @mAction= '12AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 38
IF @mAction= '12AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 39
IF @mAction= '12AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 40
IF @mAction= '12AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 41
IF @mAction= '12AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 42
IF @mAction= '12AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 43
--IF @mAction= '12AAFAFAFA'

-- Option No. 44
--IF @mAction= '12AAFAFAFF'

-- Option No. 45
IF @mAction= '12AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM           dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 46
IF @mAction= '12AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 47
IF @mAction= '12AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM          dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 48
IF @mAction= '12AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 49
IF @mAction= '12AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 50
IF @mAction= '12AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 51
--IF @mAction= '12AAFFAAFA'

-- Option No. 52
--IF @mAction= '12AAFFAAFF'

-- Option No. 53
IF @mAction= '12AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 54
IF @mAction= '12AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 55
IF @mAction= '12AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 56
IF @mAction= '12AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 57
IF @mAction= '12AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 58
IF @mAction= '12AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 59
--IF @mAction= '12AAFFFAFA'

-- Option No. 60
--IF @mAction= '12AAFFFAFF'

-- Option No. 61
IF @mAction= '12AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 62
IF @mAction= '12AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 63
IF @mAction= '12AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 64
IF @mAction= '12AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 65
IF @mAction= '12AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 66
IF @mAction= '12AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 67
--IF @mAction= '12AFAAAAFA'

-- Option No. 68
--IF @mAction= '12AFAAAAFF'

-- Option No. 69
IF @mAction= '12AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 70
IF @mAction= '12AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 71
IF @mAction= '12AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 72
IF @mAction= '12AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 73
IF @mAction= '12AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 74
IF @mAction= '12AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 75
--IF @mAction= '12AFAAFAFA'

-- Option No. 76
--IF @mAction= '12AFAAFAFF'

-- Option No. 77
IF @mAction= '12AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 78
IF @mAction= '12AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 79
IF @mAction= '12AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 80
IF @mAction= '12AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 81
IF @mAction= '12AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 82
IF @mAction= '12AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 83
--IF @mAction= '12AFAFAAFA'

-- Option No. 84
--IF @mAction= '12AFAFAAFF'

-- Option No. 85
IF @mAction= '12AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 86
IF @mAction= '12AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 87
IF @mAction= '12AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 88
IF @mAction= '12AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 89
IF @mAction= '12AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 90
IF @mAction= '12AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 91
--IF @mAction= '12AFAFFAFA'

-- Option No. 92
--IF @mAction= '12AFAFFAFF'

-- Option No. 93
IF @mAction= '12AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 94
IF @mAction= '12AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 95
IF @mAction= '12AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 96
IF @mAction= '12AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 97
IF @mAction= '12AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 98
IF @mAction= '12AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 99
--IF @mAction= '12AFFAAAFA'

-- Option No. 100
--IF @mAction= '12AFFAAAFF'

-- Option No. 101
IF @mAction= '12AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 112
IF @mAction= '12AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 103
IF @mAction= '12AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 104
IF @mAction= '12AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 105
IF @mAction= '12AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 106
IF @mAction= '12AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 107
--IF @mAction= '12AFFAFAFA'

-- Option No. 108
--IF @mAction= '12AFFAFAFF'

-- Option No. 109
IF @mAction= '12AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 110
IF @mAction= '12AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 111
IF @mAction= '12AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 112
IF @mAction= '12AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 113
IF @mAction= '12AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 114
IF @mAction= '12AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 115
--IF @mAction= '12AFFFAAFA'

-- Option No. 116
--IF @mAction= '12AFFFAAFF'

-- Option No. 117
IF @mAction= '12AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 118
IF @mAction= '12AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 119
IF @mAction= '12AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 120
IF @mAction= '12AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 121
IF @mAction= '12AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 122
IF @mAction= '12AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 123
--IF @mAction= '12AFFFFAFA'

-- Option No. 124
--IF @mAction= '12AFFFFAFF'

-- Option No. 125
IF @mAction= '12AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 126
IF @mAction= '12AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 127
IF @mAction= '12AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 128
IF @mAction= '12AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 129
IF @mAction= '12FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 130
IF @mAction= '12FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 131
--IF @mAction= '12FAAAAAFA'

-- Option No. 132
--IF @mAction= '12FAAAAAFF'

-- Option No. 133
IF @mAction= '12FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 134
IF @mAction= '12FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 135
IF @mAction= '12FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 136
IF @mAction= '12FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 138
IF @mAction= '12FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 138
IF @mAction= '12FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 139
--IF @mAction= '12FAAAFAFA'

-- Option No. 140
--IF @mAction= '12FAAAFAFF'

-- Option No. 141
IF @mAction= '12FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 142
IF @mAction= '12FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 143
IF @mAction= '12FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 144
IF @mAction= '12FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 145
IF @mAction= '12FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 146
IF @mAction= '12FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 147
--IF @mAction= '12FAAFAAFA'

-- Option No. 148
--IF @mAction= '12FAAFAAFF'

-- Option No. 149
IF @mAction= '12FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 150
IF @mAction= '12FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 151
IF @mAction= '12FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 152
IF @mAction= '12FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 153
IF @mAction= '12FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 154
IF @mAction= '12FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 155
--IF @mAction= '12FAAFFAFA'

-- Option No. 156
--IF @mAction= '12FAAFFAFF'

-- Option No. 157
IF @mAction= '12FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 158
IF @mAction= '12FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 159
IF @mAction= '12FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 160
IF @mAction= '12FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 161
IF @mAction= '12FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 162
IF @mAction= '12FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 163
--IF @mAction= '12FAFAAAFA'

-- Option No. 164
--IF @mAction= '12FAFAAAFF'

-- Option No. 165
IF @mAction= '12FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 166
IF @mAction= '12FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 167
IF @mAction= '12FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 168
IF @mAction= '12FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 169
IF @mAction= '12FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 170
IF @mAction= '12FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 171
--IF @mAction= '12FAFAFAFA'

-- Option No. 172
--IF @mAction= '12FAFAFAFF'

-- Option No. 173
IF @mAction= '12FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 174
IF @mAction= '12FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 175
IF @mAction= '12FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 176
IF @mAction= '12FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 177
IF @mAction= '12FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 178
IF @mAction= '12FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 179
--IF @mAction= '12FAFFAAFA'

-- Option No. 180
--IF @mAction= '12FAFFAAFF'

-- Option No. 181
IF @mAction= '12FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 182
IF @mAction= '12FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 183
IF @mAction= '12FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 184
IF @mAction= '12FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 185
IF @mAction= '12FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 186
IF @mAction= '12FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 187
--IF @mAction= '12FAFFFAFA'

-- Option No. 188
--IF @mAction= '12FAFFFAFF'

-- Option No. 189
IF @mAction= '12FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 190
IF @mAction= '12FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 191
IF @mAction= '12FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 192
IF @mAction= '12FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 193
IF @mAction= '12FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 194
IF @mAction= '12FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 195
--IF @mAction= '12FFAAAAFA'

-- Option No. 196
--IF @mAction= '12FFAAAAFF'

-- Option No. 197
IF @mAction= '12FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 198
IF @mAction= '12FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 199
IF @mAction= '12FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 200
IF @mAction= '12FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 201
IF @mAction= '12FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 212
IF @mAction= '12FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 203
--IF @mAction= '12FFAAFAFA'

-- Option No. 204
--IF @mAction= '12FFAAFAFF'

-- Option No. 205
IF @mAction= '12FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 206
IF @mAction= '12FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 207
IF @mAction= '12FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 208
IF @mAction= '12FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 209
IF @mAction= '12FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 210
IF @mAction= '12FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 211
--IF @mAction= '12FFAFAAFA'

-- Option No. 212
--IF @mAction= '12FFAFAAFF'

-- Option No. 213
IF @mAction= '12FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 214
IF @mAction= '12FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 215
IF @mAction= '12FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 216
IF @mAction= '12FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 217
IF @mAction= '12FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 218
IF @mAction= '12FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 219
--IF @mAction= '12FFAFFAFA'

-- Option No. 220
--IF @mAction= '12FFAFFAFF'

-- Option No. 221
IF @mAction= '12FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 222
IF @mAction= '12FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 223
IF @mAction= '12FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 224
IF @mAction= '12FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 225
IF @mAction= '12FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 226
IF @mAction= '12FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 227
--IF @mAction= '12FFFAAAFA'

-- Option No. 228
--IF @mAction= '12FFFAAAFF'

-- Option No. 229
IF @mAction= '12FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 230
IF @mAction= '12FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 231
IF @mAction= '12FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 232
IF @mAction= '12FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 233
IF @mAction= '12FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 234
IF @mAction= '12FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 235
--IF @mAction= '12FFFAFAFA'

-- Option No. 236
--IF @mAction= '12FFFAFAFF'

-- Option No. 237
IF @mAction= '12FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 238
IF @mAction= '12FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 239
IF @mAction= '12FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 240
IF @mAction= '12FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 241
IF @mAction= '12FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 242
IF @mAction= '12FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 243
--IF @mAction= '12FFFFAAFA'

-- Option No. 244
--IF @mAction= '12FFFFAAFF'

-- Option No. 245
IF @mAction= '12FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 246
IF @mAction= '12FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 247
IF @mAction= '12FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 248
IF @mAction= '12FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 249
IF @mAction= '12FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 250
IF @mAction= '12FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 251
--IF @mAction= '12FFFFFAFA'

-- Option No. 252
--IF @mAction= '12FFFFFAFF'

-- Option No. 253
IF @mAction= '12FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 254
IF @mAction= '12FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder)	And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 255
IF @mAction= '12FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
                         / SUM(SOD.OrderQuantity) AS Average, Sum(ShippedQuantity) As Dispatch, SUM(SOD.OrderQuantity) - Sum(ShippedQuantity) As Balance
	FROM            dbo.SalesOrderDetails AS SOD INNER JOIN dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode INNER JOIN dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID
                         
                         
                         
                         
                         
	WHERE        (Cast(SOD.OrderReceivedDate As Date) >= @mFromDate) And (Cast(SOD.OrderReceivedDate As Date) <= @mToDate) And SOD.Brand = @mBrand
				And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And SO.BuyerName = @mBuyerName
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END

-- Option No. 256
IF @mAction= '12FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew AS Type, SUM(SOD.OrderQuantity) AS OrdQty, SUM(SOD.OrderValue) AS Value, SUM(SOD.OrderValue) 
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
	GROUP BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY SO.BuyerName, MAT.ArticleMould, MAT.CodificationNew
END








GO
