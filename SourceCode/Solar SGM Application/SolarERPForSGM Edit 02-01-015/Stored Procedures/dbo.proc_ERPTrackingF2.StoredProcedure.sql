USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_ERPTrackingF2]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop Proc proc_ERPTrackingF2

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_ERPTrackingF2]
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
@mSlNo							int				=NULL,
@mID							varchar(50)		=Null,
@mSalesOrderNo					varchar(50)		=Null,
@mCustomerOrderNo				varchar(50)		=Null,
@mOrderReceivedDate				datetime		=NULL,
@mArticle						varchar(50)		=Null,
@mArticleName					varchar(100)	=Null,
@mOrderQuantity					int				=NULL,
@mPrice							decimal(18, 2)	=Null,
@mOrderValue					decimal(18, 2)	=Null,
@mExpectedDeliveryDate			datetime		=NULL,
@mDispatch						int				=NULL,
@mBalance						int				=NULL,
@mAssortmentName				varchar(50)		=Null,
@mRowInfo						varchar(50)		=Null,
@mSize01						varchar(5)		=Null,
@mSize02						varchar(5)		=Null,
@mSize03						varchar(5)		=Null,
@mSize04						varchar(5)		=Null,
@mSize05						varchar(5)		=Null,
@mSize06						varchar(5)		=Null,
@mSize07						varchar(5)		=Null,
@mSize08						varchar(5)		=Null,
@mSize09						varchar(5)		=Null,
@mSize10						varchar(5)		=Null,
@mSize11						varchar(5)		=Null,
@mSize12						varchar(5)		=Null,
@mSize13						varchar(5)		=Null,
@mSize14						varchar(5)		=Null,
@mSize15						varchar(5)		=Null,
@mSize16						varchar(5)		=Null,
@mSize17						varchar(5)		=Null,
@mSize18						varchar(5)		=Null,
@mIPAddress						Varchar(30)		=Null,
@mInvoiceNo						Varchar(50)		=Null,
@mInvoiceDt						Datetime		=Null


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='LOADDATA'
BEGIN
	SELECT
		'0' As SlNo,sod.id,
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,			SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,			SOD.ShippedQuantity AS Dispatch,
		SOD.OrderQuantity - SOD.ShippedQuantity AS Balance,						SOD.OrderStatus,		ISNULL(SUM(Rej.RejectionQty), 0) AS RejectionQty,
		SOD.ExpectedDeliveryDate,					SOD.ID,
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) As ShipmentStatus,
		MAT.CodificationNew,MAT.Description,		MAT.ArticleMould,
		SOD.AssortmentName, 
		SOD.Size01  As Size01, SOD.Size02, SOD.Size03, SOD.Size04, SOD.Size05, SOD.Size06, SOD.Size07, SOD.Size08, SOD.Size09, SOD.Size10,
		SOD.Size11, SOD.Size12, SOD.Size13, SOD.Size14, SOD.Size15, SOD.Size16, SOD.Size17, SOD.Size18

	FROM
		dbo.SalesOrderDetails AS SOD INNER JOIN
		dbo.SalesOrder AS SO ON SOD.SalesOrderID = SO.ID INNER JOIN
		dbo.Materials As MAT ON SOD.Article = MAT.MaterialCode LEFT OUTER JOIN
		dbo.SoleRejection AS Rej ON SOD.ID = Rej.SalesOrderDetailID 
	
	GROUP BY
		SOD.SalesOrderNo,	SOD.CustomerOrderNo,	SOD.OrderReceivedDate,		SO.BuyerName,				SOD.Article,
		SOD.ArticleName,	SOD.OrderQuantity,		SOD.Price,					SOD.OrderValue,				SOD.ShippedQuantity,
		SOD.OrderQuantity - SOD.ShippedQuantity,	SOD.OrderStatus,			SOD.ExpectedDeliveryDate,	SOD.ID,
		SOD.ExpectedDeliveryDate,					SOD.LastInvDate,			MAT.CodificationNew,		MAT.Description,
		MAT.ArticleMould,
		SOD.AssortmentName, 
		SOD.Size01,  SOD.Size02, SOD.Size03, SOD.Size04, SOD.Size05, SOD.Size06, SOD.Size07, SOD.Size08, SOD.Size09, SOD.Size10,
		SOD.Size11, SOD.Size12, SOD.Size13, SOD.Size14, SOD.Size15, SOD.Size16, SOD.Size17, SOD.Size18

	HAVING
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate 
		

		ORDER BY
	SlNo,
	SOD.AssortmentName,
	SOD.OrderReceivedDate
END


IF @mAction='INSERT'
BEGIN
	INSERT INTO
	TempERPTrackingSystemv2

	VALUES
	(
							@mSlNo,					@mID,					@mSalesOrderNo,					@mCustomerOrderNo,
	@mOrderReceivedDate,	@mBuyerName,			@mArticle,				@mArticleName,					@mOrderQuantity,
	@mPrice,				@mOrderValue,			@mExpectedDeliveryDate,	@mShipmentStatus,				@mCodificationNew,
	@mDispatch,				@mBalance,				@mOrderStatus,			@mArticleMould,					@mAssortmentName,
	@mRowInfo,				@mSize01,				@mSize02,				@mSize03,						@mSize04,
	@mSize05,				@mSize06,				@mSize07,				@mSize08,						@mSize09,
	@mSize10,				@mSize11,				@mSize12,				@mSize13,						@mSize14,
	@mSize15,				@mSize16,				@mSize17,				@mSize18,						@mIPAddress,
	@mInvoiceNo,			@mInvoiceDt
	)
END

-- Option No. 1
IF @mAction='OSS0AAAAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,InvoiceNo,InvoiceDt,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
	OrderReceivedDate
END

-- Option No. 2
IF @mAction='OSS0AAAAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    

	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 3
IF @mAction='OSS0AAAAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		

	ORDER BY
		OrderReceivedDate
END

-- Option No. 4
IF @mAction='OSS0AAAAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
	OrderReceivedDate
END

-- Option No. 5
IF @mAction='OSS0AAAFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		OrderStatus = @mOrderStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 

	ORDER BY
	OrderReceivedDate
END

-- Option No. 6
IF @mAction='OSS0AAAFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 7
IF @mAction='OSS0AAAFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 8
IF @mAction='OSS0AAAFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END


-- Option No. 9
IF @mAction='OSS0AAFAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 10
IF @mAction='OSS0AAFAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 11
IF @mAction='OSS0AAFAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 12
IF @mAction='OSS0AAFAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 13
IF @mAction='OSS0AAFFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ArticleName = @mDescription And 
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 14
IF @mAction='OSS0AAFFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 15
IF @mAction='OSS0AAFFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 16
IF @mAction='OSS0AAFFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 17
IF @mAction='OSS0AFAAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 18
IF @mAction='OSS0AFAAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 19
IF @mAction='OSS0AFAAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 20
IF @mAction='OSS0AFAAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 21
IF @mAction='OSS0AFAFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 22
IF @mAction='OSS0AFAFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 23
IF @mAction='OSS0AFAFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 24
IF @mAction='OSS0AFAFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END


-- Option No. 25
IF @mAction='OSS0AFFAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 26
IF @mAction='OSS0AFFAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 27
IF @mAction='OSS0AFFAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 28
IF @mAction='OSS0AFFAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 29
IF @mAction='OSS0AFFFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And		
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 30
IF @mAction='OSS0AFFFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 31
IF @mAction='OSS0AFFFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    	
	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 

	ORDER BY
	OrderReceivedDate
END

-- Option No. 32
IF @mAction='OSS0AFFFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 33
IF @mAction='OSS0FAAAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 34
IF @mAction='OSS0FAAAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 35
IF @mAction='OSS0FAAAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 36
IF @mAction='OSS0FAAAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
	OrderReceivedDate
END

-- Option No. 37
IF @mAction='OSS0FAAFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 38
IF @mAction='OSS0FAAFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]


	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 39
IF @mAction='OSS0FAAFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 40
IF @mAction='OSS0FAAFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	 
	ORDER BY
		OrderReceivedDate
END


-- Option No. 41
IF @mAction='OSS0FAFAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleName = @mCodificationNew And 
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 42
IF @mAction='OSS0FAFAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 43
IF @mAction='OSS0FAFAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 44
IF @mAction='OSS0FAFAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 45
IF @mAction='OSS0FAFFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 46
IF @mAction='OSS0FAFFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 47
IF @mAction='OSS0FAFFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 48
IF @mAction='OSS0FAFFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 49
IF @mAction='OSS0FFAAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 50
IF @mAction='OSS0FFAAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 51
IF @mAction='OSS0FFAAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 52
IF @mAction='OSS0FFAAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 53
IF @mAction='OSS0FFAFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]


	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 54
IF @mAction='OSS0FFAFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 55
IF @mAction='OSS0FFAFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 56
IF @mAction='OSS0FFAFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
 
	ORDER BY
		OrderReceivedDate
END


-- Option No. 57
IF @mAction='OSS0FFFAAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 58
IF @mAction='OSS0FFFAAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
	OrderReceivedDate
END

-- Option No. 59
IF @mAction='OSS0FFFAFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
		
	ORDER BY
		OrderReceivedDate
END

-- Option No. 60
IF @mAction='OSS0FFFAFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	ORDER BY
		OrderReceivedDate
END

-- Option No. 61
IF @mAction='OSS0FFFFAA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	 
	ORDER BY
		OrderReceivedDate
END

-- Option No. 62
IF @mAction='OSS0FFFFAF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 63
IF @mAction='OSS0FFFFFA'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

-- Option No. 64
IF @mAction='OSS0FFFFFF'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2

	WHERE
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
		OrderReceivedDate
END

--------------------------------------------
--------------------------------------------

-- Option No. 1
IF @mAction='ASS0AAAAAA'
BEGIN
	
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And 
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	

END

-- Option No. 2
IF @mAction='ASS0AAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
	OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
	OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    

	
END

-- Option No. 3
IF @mAction='ASS0AAAAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
	ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
	ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 4
IF @mAction='ASS0AAAAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
	ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
	ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 5
IF @mAction='ASS0AAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
	OrderStatus = @mOrderStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
	OrderStatus = @mOrderStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 6
IF @mAction='ASS0AAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
	OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
	OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 7
IF @mAction='ASS0AAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 8
IF @mAction='ASS0AAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END


-- Option No. 9
IF @mAction='ASS0AAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 10
IF @mAction='ASS0AAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 11
IF @mAction='ASS0AAFAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 12
IF @mAction='ASS0AAFAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 13
IF @mAction='ASS0AAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mDescription And 
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mDescription And 
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 14
IF @mAction='ASS0AAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 15
IF @mAction='ASS0AAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mDescription And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 16
IF @mAction='ASS0AAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 17
IF @mAction='ASS0AFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 18
IF @mAction='ASS0AFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 19
IF @mAction='ASS0AFAAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 20
IF @mAction='ASS0AFAAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 21
IF @mAction='ASS0AFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 22
IF @mAction='ASS0AFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 23
IF @mAction='ASS0AFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 24
IF @mAction='ASS0AFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END


-- Option No. 25
IF @mAction='ASS0AFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 26
IF @mAction='ASS0AFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 27
IF @mAction='ASS0AFFAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    
	
END

-- Option No. 28
IF @mAction='ASS0AFFAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 29
IF @mAction='ASS0AFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And		
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And		
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 30
IF @mAction='ASS0AFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 31
IF @mAction='ASS0AFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    	
	
END

-- Option No. 32
IF @mAction='ASS0AFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And 
		ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 33
IF @mAction='ASS0FAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 


END

-- Option No. 34
IF @mAction='ASS0FAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
    

END

-- Option No. 35
IF @mAction='ASS0FAAAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 36
IF @mAction='ASS0FAAAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 


END

-- Option No. 37
IF @mAction='ASS0FAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 38
IF @mAction='ASS0FAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 39
IF @mAction='ASS0FAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
		
END

-- Option No. 40
IF @mAction='ASS0FAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END


-- Option No. 41
IF @mAction='ASS0FAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mCodificationNew And 
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mCodificationNew And 
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 42
IF @mAction='ASS0FAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 43
IF @mAction='ASS0FAFAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 44
IF @mAction='ASS0FAFAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'  And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 


END

-- Option No. 45
IF @mAction='ASS0FAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 46
IF @mAction='ASS0FAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 47
IF @mAction='ASS0FAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 48
IF @mAction='ASS0FAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 


END

-- Option No. 49
IF @mAction='ASS0FFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
	
END

-- Option No. 50
IF @mAction='ASS0FFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 51
IF @mAction='ASS0FFAAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 52
IF @mAction='ASS0FFAAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

END

-- Option No. 53
IF @mAction='ASS0FFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 54
IF @mAction='ASS0FFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 55
IF @mAction='ASS0FFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 


END

-- Option No. 56
IF @mAction='ASS0FFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END


-- Option No. 57
IF @mAction='ASS0FFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
	
END

-- Option No. 58
IF @mAction='ASS0FFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 59
IF @mAction='ASS0FFFAFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
	
END

-- Option No. 60
IF @mAction='ASS0FFFAFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' And
		CodificationNew = @mCodificationNew
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
		
END

-- Option No. 61
IF @mAction='ASS0FFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 62
IF @mAction='ASS0FFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 63
IF @mAction='ASS0FFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	
END

-- Option No. 64
IF @mAction='ASS0FFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'

	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderStatus = @mOrderStatus And
		ShipmentStatus = @mShipmentStatus And
		CodificationNew = @mCodificationNew And
		ArticleName = @mCodificationNew And
		ArticleMould = @mArticleMould And
		BuyerName = @mBuyerName And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY'
	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 
		
END

IF @mAction='OS'
BEGIN
	SELECT
		CustomerOrderNo,			OrderReceivedDate,			BuyerName,					ArticleName,				OrderQuantity,
		Price,OrderValue,			RowInfo As Info,			Size01 As [Size/Qty01],		Size02 As [Size/Qty02],		Size03 As [Size/Qty03],
		Size04 As [Size/Qty04],		Size05 As [Size/Qty05],		Size06 As [Size/Qty06],		Size07 As [Size/Qty07],		Size08 As [Size/Qty08],
		Size09 As [Size/Qty09],		Size10 As [Size/Qty10],		Size11 As [Size/Qty11],		Size12 As [Size/Qty12],		Size13 As [Size/Qty13],
		Size14 As [Size/Qty14],		Size15 As [Size/Qty15],		Size16 As [Size/Qty16],		Size17 As [Size/Qty17],		Size18 As [Size/Qty18]

	FROM
		TempERPTrackingSystemv2
    
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	ORDER BY
	OrderReceivedDate
END


-------ASDDASFSADF
IF @mAction='AS'
BEGIN
	
	SELECT        TOP (100) PERCENT Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName,RowInfo AS Info, 

	Cast (((CASE WHEN ISNumeric(Size01) = 1 THEN CAST(SIZE01 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty01],
	Cast (((CASE WHEN ISNumeric(Size02) = 1 THEN CAST(SIZE02 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty02],
	Cast (((CASE WHEN ISNumeric(Size03) = 1 THEN CAST(SIZE03 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty03],
	Cast (((CASE WHEN ISNumeric(Size04) = 1 THEN CAST(SIZE04 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty04],
	Cast (((CASE WHEN ISNumeric(Size05) = 1 THEN CAST(SIZE05 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty05],
	Cast (((CASE WHEN ISNumeric(Size06) = 1 THEN CAST(SIZE06 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty06],
	Cast (((CASE WHEN ISNumeric(Size07) = 1 THEN CAST(SIZE07 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty07],
	Cast (((CASE WHEN ISNumeric(Size08) = 1 THEN CAST(SIZE08 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty08],
	Cast (((CASE WHEN ISNumeric(Size09) = 1 THEN CAST(SIZE09 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty09],
	Cast (((CASE WHEN ISNumeric(Size10) = 1 THEN CAST(SIZE10 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty10],
	Cast (((CASE WHEN ISNumeric(Size11) = 1 THEN CAST(SIZE11 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty11],
	Cast (((CASE WHEN ISNumeric(Size12) = 1 THEN CAST(SIZE12 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty12],
	Cast (((CASE WHEN ISNumeric(Size13) = 1 THEN CAST(SIZE13 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty13],
	Cast (((CASE WHEN ISNumeric(Size14) = 1 THEN CAST(SIZE14 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty14],
	Cast (((CASE WHEN ISNumeric(Size15) = 1 THEN CAST(SIZE15 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty15],
	Cast (((CASE WHEN ISNumeric(Size16) = 1 THEN CAST(SIZE16 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty16],
	Cast (((CASE WHEN ISNumeric(Size17) = 1 THEN CAST(SIZE17 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty17],
	Cast (((CASE WHEN ISNumeric(Size18) = 1 THEN CAST(SIZE18 AS decimal(18,2)) ELSE 0 END)) As decimal(18,2)) AS [Size/Qty18]

	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo = '01. SIZES' And 
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 
	GROUP BY Article, ArticleName,    RowInfo, AssortmentName,Size01, Size02, Size03, Size04, Size05, Size06, Size07, Size08, Size09, Size10, Size11, Size12,
	Size13, Size14, Size15, Size16, Size17, Size18

	UNION 

	SELECT        TOP (100) PERCENT

	Article, ArticleName, Sum(OrderQuantity) As OrderQuantity,  Sum(OrderValue) As OrderValue, AssortmentName, RowInfo AS Info, 
	Sum(Cast (SIZE01 As decimal(18,2))) AS [Size/Qty01],
	Sum(Cast (SIZE02 As decimal(18,2))) AS [Size/Qty02],
	Sum(Cast (SIZE03 As decimal(18,2))) AS [Size/Qty03],
	Sum(Cast (SIZE04 As decimal(18,2))) AS [Size/Qty04],
	Sum(Cast (SIZE05 As decimal(18,2))) AS [Size/Qty05],
	Sum(Cast (SIZE06 As decimal(18,2))) AS [Size/Qty06],
	Sum(Cast (SIZE07 As decimal(18,2))) AS [Size/Qty07],
	Sum(Cast (SIZE08 As decimal(18,2))) AS [Size/Qty08],
	Sum(Cast (SIZE09 As decimal(18,2))) AS [Size/Qty09],
	Sum(Cast (SIZE10 As decimal(18,2))) AS [Size/Qty10],
	Sum(Cast (SIZE11 As decimal(18,2))) AS [Size/Qty11],
	Sum(Cast (SIZE12 As decimal(18,2))) AS [Size/Qty12],
	Sum(Cast (SIZE13 As decimal(18,2))) AS [Size/Qty13],
	Sum(Cast (SIZE14 As decimal(18,2))) AS [Size/Qty14],
	Sum(Cast (SIZE15 As decimal(18,2))) AS [Size/Qty15],
	Sum(Cast (SIZE16 As decimal(18,2))) AS [Size/Qty16],
	Sum(Cast (SIZE17 As decimal(18,2))) AS [Size/Qty17],
	Sum(Cast (SIZE18 As decimal(18,2))) AS [Size/Qty18]


	FROM            dbo.TempERPTrackingSystemv2
	Where RowInfo <> '01. SIZES' And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate AND IPAddress = @mIPAddress And RowInfo <> '10. INVOICE QUANTITY' 

	GROUP BY Article, ArticleName,  AssortmentName,  RowInfo
	ORDER BY Article, ArticleName, AssortmentName, Info 

	

END



GO
