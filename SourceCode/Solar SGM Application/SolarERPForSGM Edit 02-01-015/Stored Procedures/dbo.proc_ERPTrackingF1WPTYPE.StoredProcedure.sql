USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_ERPTrackingF1WPTYPE]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc proc_ERPTrackingF1WPTYPE

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_ERPTrackingF1WPTYPE]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mCodificationNew				Varchar(50)		=Null,
@mWeekFrom						Int				=Null,
@mWeekTo						Int				=Null,
@mYear							Int				=Null,
@mIsEDDNegotiable				Bit				=Null,
@mShipmentStatus				Varchar(20)		=Null,
@mOrderStatus					Varchar(20)		=Null,
@mDescription					Varchar(50)		=NULL,
@mArticleMould					Varchar(50)		=Null,
@mProductTypeMain				Varchar(50)		=Null


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int


-- Option No. 1
IF @mAction='S0AAAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials AS MAT ON SOD.Article = mat.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ProductType = @mProductTypeMain


	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 2
IF @mAction='S0AAAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 3
IF @mAction='S0AAAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
		AND MAT.ProductType = @mProductTypeMain
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 4
IF @mAction='S0AAAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 5
IF @mAction='S0AAAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus
		AND MAT.ProductType = @mProductTypeMain
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 6
IF @mAction='S0AAAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 7
IF @mAction='S0AAAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 8
IF @mAction='S0AAAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 9
IF @mAction='S0AAFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 10
IF @mAction='S0AAFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 11
IF @mAction='S0AAFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 12
IF @mAction='S0AAFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 13
IF @mAction='S0AAFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 14
IF @mAction='S0AAFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 15
IF @mAction='S0AAFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 16
IF @mAction='S0AAFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 17
IF @mAction='S0AFAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould
	AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 18
IF @mAction='S0AFAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould
	AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 19
IF @mAction='S0AFAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 20
IF @mAction='S0AFAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 21
IF @mAction='S0AFAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 22
IF @mAction='S0AFAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 23
IF @mAction='S0AFAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 24
IF @mAction='S0AFAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 25
IF @mAction='S0AFFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 26
IF @mAction='S0AFFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 27
IF @mAction='S0AFFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 28
IF @mAction='S0AFFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 29
IF @mAction='S0AFFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 30
IF @mAction='S0AFFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 31
IF @mAction='S0AFFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 32
IF @mAction='S0AFFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 33
IF @mAction='S0FAAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 34
IF @mAction='S0FAAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 35
IF @mAction='S0FAAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 36
IF @mAction='S0FAAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 37
IF @mAction='S0FAAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 38
IF @mAction='S0FAAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 39
IF @mAction='S0FAAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 40
IF @mAction='S0FAAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 41
IF @mAction='S0FAFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 42
IF @mAction='S0FAFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 43
IF @mAction='S0FAFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 44
IF @mAction='S0FAFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 45
IF @mAction='S0FAFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 46
IF @mAction='S0FAFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 47
IF @mAction='S0FAFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 48
IF @mAction='S0FAFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 49
IF @mAction='S0FFAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 50
IF @mAction='S0FFAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 51
IF @mAction='S0FFAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 52
IF @mAction='S0FFAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 53
IF @mAction='S0FFAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 54
IF @mAction='S0FFAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 55
IF @mAction='S0FFAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 56
IF @mAction='S0FFAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 57
IF @mAction='S0FFFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 58
IF @mAction='S0FFFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 59
IF @mAction='S0FFFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 60
IF @mAction='S0FFFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 61
IF @mAction='S0FFFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 62
IF @mAction='S0FFFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 63
IF @mAction='S0FFFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 64
IF @mAction='S0FFFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 65
IF @mAction='D0AAAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 66
IF @mAction='D0AAAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 67
IF @mAction='D0AAAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 68
IF @mAction='D0AAAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 69
IF @mAction='D0AAAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 70
IF @mAction='D0AAAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 71
IF @mAction='D0AAAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 72
IF @mAction='D0AAAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 73
IF @mAction='D0AAFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 74
IF @mAction='D0AAFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 75
IF @mAction='D0AAFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 76
IF @mAction='D0AAFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 77
IF @mAction='D0AAFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 78
IF @mAction='D0AAFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 79
IF @mAction='D0AAFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 80
IF @mAction='D0AAFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 81
IF @mAction='D0AFAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould
	AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 82
IF @mAction='D0AFAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould
	AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 83
IF @mAction='D0AFAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 84
IF @mAction='D0AFAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain


	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 85
IF @mAction='D0AFAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 86
IF @mAction='D0AFAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 87
IF @mAction='D0AFAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 88
IF @mAction='D0AFAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 89
IF @mAction='D0AFFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 90
IF @mAction='D0AFFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 91
IF @mAction='D0AFFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 92
IF @mAction='D0AFFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 93
IF @mAction='D0AFFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 94
IF @mAction='D0AFFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 95
IF @mAction='D0AFFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 96
IF @mAction='D0AFFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 97
IF @mAction='D0FAAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 98
IF @mAction='D0FAAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 99
IF @mAction='D0FAAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials AS MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 100
IF @mAction='D0FAAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 101
IF @mAction='D0FAAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 102
IF @mAction='D0FAAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 103
IF @mAction='D0FAAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 104
IF @mAction='D0FAAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 105
IF @mAction='D0FAFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 106
IF @mAction='D0FAFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 107
IF @mAction='D0FAFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 108
IF @mAction='D0FAFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 109
IF @mAction='D0FAFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 110
IF @mAction='D0FAFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 111
IF @mAction='D0FAFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 112
IF @mAction='D0FAFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 113
IF @mAction='D0FFAAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 114
IF @mAction='D0FFAAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
	MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 115
IF @mAction='D0FFAAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 116
IF @mAction='D0FFAAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 117
IF @mAction='D0FFAFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 118
IF @mAction='D0FFAFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 119
IF @mAction='D0FFAFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 120
IF @mAction='D0FFAFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


-- Option No. 121
IF @mAction='D0FFFAAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 122
IF @mAction='D0FFFAAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 123
IF @mAction='D0FFFAFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 124
IF @mAction='D0FFFAFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 And
		MAT.CodificationNew = @mCodificationNew

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 125
IF @mAction='D0FFFFAA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 126
IF @mAction='D0FFFFAF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 127
IF @mAction='D0FFFFFA'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain
		
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END

-- Option No. 128
IF @mAction='D0FFFFFF'
BEGIN
	SELECT
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('MOULD','MOULDING')) As MouldQty,
						 (Select IsNull(Sum(PD.Quantity),0) From PackingDetail As PD, JobcardDetail As JC Where PD.JobcardNo = JC.JobcardNo And JC.SalesOrderDetailID = SOD.ID  And MtoFScanDate Is Not Null) As M2FQty,
						 (Select IsNull(Sum(Quantity),0) From View_5 Where ID = SOD.ID And ProcessName in ('FINISH','FINISHING')) As FinishQty,
						 '0' As PkdStockQty

	FROM
	dbo.SalesOrderDetails AS SOD INNER JOIN
    dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
    dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
    dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID

	WHERE
		SOD.OrderStatus = @mOrderStatus And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		MAT.CodificationNew = @mCodificationNew And
		MAT.Description = @mDescription And
		MAT.ArticleMould = @mArticleMould And
		SO.BuyerName = @mBuyerName
		AND MAT.ProductType = @mProductTypeMain

	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		MAT.CodificationNew

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate
		 

	ORDER BY
	SOD.OrderReceivedDate
END


GO
