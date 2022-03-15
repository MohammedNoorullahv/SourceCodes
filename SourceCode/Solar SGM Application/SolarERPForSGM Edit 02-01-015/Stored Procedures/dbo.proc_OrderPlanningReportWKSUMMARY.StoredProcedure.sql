USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_OrderPlanningReportWKSUMMARY]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc proc_OrderPlanningReportWKSUMMARY

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_OrderPlanningReportWKSUMMARY]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mCodificationNew				Varchar(50)		=Null,
@mWeekFrom						Int				=Null,
@mWeekTo						Int				=Null,
@mYear							Int				=Null,
@mIsEDDNegotiable				Int				=Null,
@mShipmentStatus				Varchar(20)		=Null,
@mOrderStatus					Varchar(20)		=Null,
@mDescription					Varchar(50)		=Null,
@mArticleMould					Varchar(50)		=Null,
@mProductTypeMain				Varchar(50)		=Null

AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

-- Option No. 1 & 2
IF @mAction='0AAAAAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 3 & 4
IF @mAction='0AAAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 5 & 6
IF @mAction='0AAAAAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 7 & 8
IF @mAction='0AAAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 9 & 10
IF @mAction='0AAAAAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 11 & 12
IF @mAction='0AAAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 13 & 14
IF @mAction='0AAAAAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 15 & 16
IF @mAction='0AAAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 17 & 18
IF @mAction='0AAAAAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 19 & 20
IF @mAction='0AAAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 21 & 22
IF @mAction='0AAAAAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 23 & 24
IF @mAction='0AAAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 25 & 26
IF @mAction='0AAAAAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 27 & 28
IF @mAction='0AAAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 29 & 30
IF @mAction='0AAAAAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 31 & 32
IF @mAction='0AAAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 33 & 34
IF @mAction='0AAAAFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 35 & 36
IF @mAction='0AAAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 37 & 38
IF @mAction='0AAAAFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 39 & 40
IF @mAction='0AAAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 41 & 42
IF @mAction='0AAAAFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 43 & 44
IF @mAction='0AAAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 45 & 46
IF @mAction='0AAAAFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 47 & 48
IF @mAction='0AAAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 49 & 50
IF @mAction='0AAAAFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 51 & 52
IF @mAction='0AAAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 53 & 54
IF @mAction='0AAAAFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 55 & 56
IF @mAction='0AAAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 57 & 58
IF @mAction='0AAAAFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 59 & 60
IF @mAction='0AAAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 61 & 62
IF @mAction='0AAAAFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 63 & 64
IF @mAction='0AAAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 65 & 66
IF @mAction='0AAAFAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 67 & 68
IF @mAction='0AAAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 69 & 70
IF @mAction='0AAAFAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 71 & 72
IF @mAction='0AAAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 73 & 74
IF @mAction='0AAAFAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 75 & 76
IF @mAction='0AAAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 77 & 78
IF @mAction='0AAAFAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 79 & 80
IF @mAction='0AAAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 81 & 82
IF @mAction='0AAAFAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 83 & 84
IF @mAction='0AAAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 85 & 86
IF @mAction='0AAAFAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 87 & 88
IF @mAction='0AAAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 89 & 90
IF @mAction='0AAAFAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 91 & 92
IF @mAction='0AAAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 93 & 94
IF @mAction='0AAAFAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 95 & 96
IF @mAction='0AAAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 97 & 98
IF @mAction='0AAAFFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 99 & 100
IF @mAction='0AAAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 101 & 102
IF @mAction='0AAAFFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 103 & 104
IF @mAction='0AAAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 105 & 106
IF @mAction='0AAAFFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 107 & 108
IF @mAction='0AAAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 109 & 110
IF @mAction='0AAAFFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 111 & 112
IF @mAction='0AAAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 113 & 114
IF @mAction='0AAAFFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 115 & 116
IF @mAction='0AAAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 117 & 118
IF @mAction='0AAAFFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 119 & 120
IF @mAction='0AAAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 121 & 122
IF @mAction='0AAAFFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 123 & 124
IF @mAction='0AAAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 125 & 126
IF @mAction='0AAAFFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 127 & 128
IF @mAction='0AAAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 129 & 130
IF @mAction='0AAFAAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 131 & 132
IF @mAction='0AAFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 133 & 134
IF @mAction='0AAFAAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 135 & 136
IF @mAction='0AAFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 137 & 138
IF @mAction='0AAFAAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 139 & 140
IF @mAction='0AAFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 141 & 142
IF @mAction='0AAFAAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 143 & 144
IF @mAction='0AAFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 145 & 146
IF @mAction='0AAFAAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 147 & 148
IF @mAction='0AAFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 149 & 150
IF @mAction='0AAFAAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 151 & 152
IF @mAction='0AAFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 153 & 154
IF @mAction='0AAFAAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 155 & 156
IF @mAction='0AAFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 157 & 158
IF @mAction='0AAFAAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 159 & 160
IF @mAction='0AAFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 161 & 162
IF @mAction='0AAFAFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 163 & 164
IF @mAction='0AAFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 165 & 166
IF @mAction='0AAFAFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 167 & 168
IF @mAction='0AAFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 169 & 170
IF @mAction='0AAFAFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 171 & 172
IF @mAction='0AAFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 173 & 174
IF @mAction='0AAFAFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 175 & 176
IF @mAction='0AAFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 177 & 178
IF @mAction='0AAFAFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 179 & 180
IF @mAction='0AAFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 181 & 182
IF @mAction='0AAFAFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 183 & 184
IF @mAction='0AAFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 185 & 186
IF @mAction='0AAFAFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 187 & 188
IF @mAction='0AAFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 189 & 190
IF @mAction='0AAFAFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 191 & 192
IF @mAction='0AAFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 193 & 194
IF @mAction='0AAFFAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 195 & 196
IF @mAction='0AAFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 197 & 198
IF @mAction='0AAFFAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 199 & 200
IF @mAction='0AAFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 201 & 202
IF @mAction='0AAFFAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 203 & 204
IF @mAction='0AAFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 205 & 206
IF @mAction='0AAFFAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 207 & 208
IF @mAction='0AAFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 209 & 210
IF @mAction='0AAFFAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 211 & 212
IF @mAction='0AAFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 213 & 214
IF @mAction='0AAFFAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 215 & 216
IF @mAction='0AAFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 217 & 218
IF @mAction='0AAFFAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 219 & 220
IF @mAction='0AAFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 221 & 222
IF @mAction='0AAFFAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 223 & 224
IF @mAction='0AAFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 225 & 226
IF @mAction='0AAFFFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 227 & 228
IF @mAction='0AAFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 229 & 230
IF @mAction='0AAFFFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 231 & 232
IF @mAction='0AAFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 233 & 234
IF @mAction='0AAFFFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 235 & 236
IF @mAction='0AAFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 237 & 238
IF @mAction='0AAFFFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 239 & 240
IF @mAction='0AAFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 241 & 242
IF @mAction='0AAFFFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 243 & 244
IF @mAction='0AAFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 245 & 246
IF @mAction='0AAFFFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 247 & 248
IF @mAction='0AAFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 249 & 250
IF @mAction='0AAFFFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 251 & 252
IF @mAction='0AAFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 253 & 254
IF @mAction='0AAFFFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 255 & 256
IF @mAction='0AAFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 256 + 1 & 2
IF @mAction='0AFAAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 3 & 4
IF @mAction='0AFAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 5 & 6
IF @mAction='0AFAAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 7 & 8
IF @mAction='0AFAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 9 & 10
IF @mAction='0AFAAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 11 & 12
IF @mAction='0AFAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 13 & 14
IF @mAction='0AFAAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 15 & 16
IF @mAction='0AFAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 17 & 18
IF @mAction='0AFAAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 19 & 20
IF @mAction='0AFAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 21 & 22
IF @mAction='0AFAAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 23 & 24
IF @mAction='0AFAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 25 & 26
IF @mAction='0AFAAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 27 & 28
IF @mAction='0AFAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 29 & 30
IF @mAction='0AFAAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 31 & 32
IF @mAction='0AFAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 33 & 34
IF @mAction='0AFAAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 35 & 36
IF @mAction='0AFAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 37 & 38
IF @mAction='0AFAAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 39 & 40
IF @mAction='0AFAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 41 & 42
IF @mAction='0AFAAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 43 & 44
IF @mAction='0AFAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 45 & 46
IF @mAction='0AFAAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 47 & 48
IF @mAction='0AFAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 49 & 50
IF @mAction='0AFAAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 51 & 52
IF @mAction='0AFAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 53 & 54
IF @mAction='0AFAAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 55 & 56
IF @mAction='0AFAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 57 & 58
IF @mAction='0AFAAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 59 & 60
IF @mAction='0AFAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 61 & 62
IF @mAction='0AFAAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 63 & 64
IF @mAction='0AFAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 65 & 66
IF @mAction='0AFAFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 67 & 68
IF @mAction='0AFAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 69 & 70
IF @mAction='0AFAFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 71 & 72
IF @mAction='0AFAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 73 & 74
IF @mAction='0AFAFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 75 & 76
IF @mAction='0AFAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 77 & 78
IF @mAction='0AFAFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 79 & 80
IF @mAction='0AFAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 81 & 82
IF @mAction='0AFAFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 83 & 84
IF @mAction='0AFAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 85 & 86
IF @mAction='0AFAFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 87 & 88
IF @mAction='0AFAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 89 & 90
IF @mAction='0AFAFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 91 & 92
IF @mAction='0AFAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 93 & 94
IF @mAction='0AFAFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 95 & 96
IF @mAction='0AFAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 97 & 98
IF @mAction='0AFAFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 99 & 100
IF @mAction='0AFAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 101 & 102
IF @mAction='0AFAFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 103 & 104
IF @mAction='0AFAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 105 & 106
IF @mAction='0AFAFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 107 & 108
IF @mAction='0AFAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 109 & 110
IF @mAction='0AFAFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 111 & 112
IF @mAction='0AFAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 113 & 114
IF @mAction='0AFAFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 115 & 116
IF @mAction='0AFAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 117 & 118
IF @mAction='0AFAFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 119 & 120
IF @mAction='0AFAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 121 & 122
IF @mAction='0AFAFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 123 & 124
IF @mAction='0AFAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 125 & 126
IF @mAction='0AFAFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 127 & 128
IF @mAction='0AFAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 129 & 130
IF @mAction='0AFFAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 131 & 132
IF @mAction='0AFFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 133 & 134
IF @mAction='0AFFAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 135 & 136
IF @mAction='0AFFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 137 & 138
IF @mAction='0AFFAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 139 & 140
IF @mAction='0AFFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 141 & 142
IF @mAction='0AFFAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 143 & 144
IF @mAction='0AFFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 145 & 146
IF @mAction='0AFFAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 147 & 148
IF @mAction='0AFFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 149 & 150
IF @mAction='0AFFAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 151 & 152
IF @mAction='0AFFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 153 & 154
IF @mAction='0AFFAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 155 & 156
IF @mAction='0AFFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 157 & 158
IF @mAction='0AFFAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 159 & 160
IF @mAction='0AFFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 161 & 162
IF @mAction='0AFFAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 163 & 164
IF @mAction='0AFFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 165 & 166
IF @mAction='0AFFAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 167 & 168
IF @mAction='0AFFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 169 & 170
IF @mAction='0AFFAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 171 & 172
IF @mAction='0AFFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 173 & 174
IF @mAction='0AFFAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 175 & 176
IF @mAction='0AFFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 177 & 178
IF @mAction='0AFFAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 179 & 180
IF @mAction='0AFFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 181 & 182
IF @mAction='0AFFAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 183 & 184
IF @mAction='0AFFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 185 & 186
IF @mAction='0AFFAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 187 & 188
IF @mAction='0AFFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 189 & 190
IF @mAction='0AFFAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 191 & 192
IF @mAction='0AFFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 193 & 194
IF @mAction='0AFFFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 195 & 196
IF @mAction='0AFFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 197 & 198
IF @mAction='0AFFFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 199 & 200
IF @mAction='0AFFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 201 & 202
IF @mAction='0AFFFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 203 & 204
IF @mAction='0AFFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 205 & 206
IF @mAction='0AFFFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 207 & 208
IF @mAction='0AFFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 209 & 210
IF @mAction='0AFFFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 211 & 212
IF @mAction='0AFFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 213 & 214
IF @mAction='0AFFFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 215 & 216
IF @mAction='0AFFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 217 & 218
IF @mAction='0AFFFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 219 & 220
IF @mAction='0AFFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 221 & 222
IF @mAction='0AFFFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 223 & 224
IF @mAction='0AFFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 225 & 226
IF @mAction='0AFFFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 227 & 228
IF @mAction='0AFFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 229 & 230
IF @mAction='0AFFFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 231 & 232
IF @mAction='0AFFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 233 & 234
IF @mAction='0AFFFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 235 & 236
IF @mAction='0AFFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 237 & 238
IF @mAction='0AFFFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 239 & 240
IF @mAction='0AFFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 241 & 242
IF @mAction='0AFFFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 243 & 244
IF @mAction='0AFFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 245 & 246
IF @mAction='0AFFFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 247 & 248
IF @mAction='0AFFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 249 & 250
IF @mAction='0AFFFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 251 & 252
IF @mAction='0AFFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 253 & 254
IF @mAction='0AFFFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		OrderReceivedDate >= @mFromDate And
		OrderReceivedDate <= @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 255 & 256
IF @mAction='0AFFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 1 & 2
IF @mAction='0FAAAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 3 & 4
IF @mAction='0FAAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 5 & 6
IF @mAction='0FAAAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 7 & 8
IF @mAction='0FAAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 9 & 10
IF @mAction='0FAAAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 11 & 12
IF @mAction='0FAAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 13 & 14
IF @mAction='0FAAAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 15 & 16
IF @mAction='0FAAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 17 & 18
IF @mAction='0FAAAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 19 & 20
IF @mAction='0FAAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 21 & 22
IF @mAction='0FAAAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 23 & 24
IF @mAction='0FAAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 25 & 26
IF @mAction='0FAAAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 27 & 28
IF @mAction='0FAAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 29 & 30
IF @mAction='0FAAAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 31 & 32
IF @mAction='0FAAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 33 & 34
IF @mAction='0FAAAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 35 & 36
IF @mAction='0FAAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 37 & 38
IF @mAction='0FAAAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 39 & 40
IF @mAction='0FAAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 41 & 42
IF @mAction='0FAAAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 43 & 44
IF @mAction='0FAAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 45 & 46
IF @mAction='0FAAAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 47 & 48
IF @mAction='0FAAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 49 & 50
IF @mAction='0FAAAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 51 & 52
IF @mAction='0FAAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 53 & 54
IF @mAction='0FAAAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 55 & 56
IF @mAction='0FAAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 57 & 58
IF @mAction='0FAAAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 59 & 60
IF @mAction='0FAAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 61 & 62
IF @mAction='0FAAAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 63 & 64
IF @mAction='0FAAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 65 & 66
IF @mAction='0FAAFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 67 & 68
IF @mAction='0FAAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 69 & 70
IF @mAction='0FAAFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 71 & 72
IF @mAction='0FAAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 73 & 74
IF @mAction='0FAAFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 75 & 76
IF @mAction='0FAAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 77 & 78
IF @mAction='0FAAFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 79 & 80
IF @mAction='0FAAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 81 & 82
IF @mAction='0FAAFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 83 & 84
IF @mAction='0FAAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 85 & 86
IF @mAction='0FAAFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 87 & 88
IF @mAction='0FAAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 89 & 90
IF @mAction='0FAAFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 91 & 92
IF @mAction='0FAAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 93 & 94
IF @mAction='0FAAFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 95 & 96
IF @mAction='0FAAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 97 & 98
IF @mAction='0FAAFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 99 & 100
IF @mAction='0FAAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 101 & 102
IF @mAction='0FAAFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 103 & 104
IF @mAction='0FAAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 105 & 106
IF @mAction='0FAAFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 107 & 108
IF @mAction='0FAAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 109 & 110
IF @mAction='0FAAFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 111 & 112
IF @mAction='0FAAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 113 & 114
IF @mAction='0FAAFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 115 & 116
IF @mAction='0FAAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 117 & 118
IF @mAction='0FAAFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 119 & 120
IF @mAction='0FAAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 121 & 122
IF @mAction='0FAAFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 123 & 124
IF @mAction='0FAAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 125 & 126
IF @mAction='0FAAFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 127 & 128
IF @mAction='0FAAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 129 & 130
IF @mAction='0FAFAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 131 & 132
IF @mAction='0FAFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 133 & 134
IF @mAction='0FAFAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 135 & 136
IF @mAction='0FAFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 137 & 138
IF @mAction='0FAFAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 139 & 140
IF @mAction='0FAFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 141 & 142
IF @mAction='0FAFAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 143 & 144
IF @mAction='0FAFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 145 & 146
IF @mAction='0FAFAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 147 & 148
IF @mAction='0FAFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 149 & 150
IF @mAction='0FAFAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 151 & 152
IF @mAction='0FAFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 153 & 154
IF @mAction='0FAFAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 155 & 156
IF @mAction='0FAFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 157 & 158
IF @mAction='0FAFAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 159 & 160
IF @mAction='0FAFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 161 & 162
IF @mAction='0FAFAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 163 & 164
IF @mAction='0FAFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 165 & 166
IF @mAction='0FAFAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 167 & 168
IF @mAction='0FAFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 169 & 170
IF @mAction='0FAFAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 171 & 172
IF @mAction='0FAFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 173 & 174
IF @mAction='0FAFAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 175 & 176
IF @mAction='0FAFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 177 & 178
IF @mAction='0FAFAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 179 & 180
IF @mAction='0FAFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 181 & 182
IF @mAction='0FAFAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 183 & 184
IF @mAction='0FAFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 185 & 186
IF @mAction='0FAFAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 187 & 188
IF @mAction='0FAFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 189 & 190
IF @mAction='0FAFAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 191 & 192
IF @mAction='0FAFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 193 & 194
IF @mAction='0FAFFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 195 & 196
IF @mAction='0FAFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 197 & 198
IF @mAction='0FAFFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 199 & 200
IF @mAction='0FAFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 201 & 202
IF @mAction='0FAFFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 203 & 204
IF @mAction='0FAFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 205 & 206
IF @mAction='0FAFFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 207 & 208
IF @mAction='0FAFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 209 & 210
IF @mAction='0FAFFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 211 & 212
IF @mAction='0FAFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 213 & 214
IF @mAction='0FAFFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 215 & 216
IF @mAction='0FAFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 217 & 218
IF @mAction='0FAFFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 219 & 220
IF @mAction='0FAFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 221 & 222
IF @mAction='0FAFFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 223 & 224
IF @mAction='0FAFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 225 & 226
IF @mAction='0FAFFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 227 & 228
IF @mAction='0FAFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 229 & 230
IF @mAction='0FAFFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 231 & 232
IF @mAction='0FAFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 233 & 234
IF @mAction='0FAFFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 235 & 236
IF @mAction='0FAFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 237 & 238
IF @mAction='0FAFFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 239 & 240
IF @mAction='0FAFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 241 & 242
IF @mAction='0FAFFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 243 & 244
IF @mAction='0FAFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 245 & 246
IF @mAction='0FAFFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 247 & 248
IF @mAction='0FAFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 249 & 250
IF @mAction='0FAFFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 251 & 252
IF @mAction='0FAFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 253 & 254
IF @mAction='0FAFFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 255 & 256
IF @mAction='0FAFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 1 & 2
IF @mAction='0FFAAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 3 & 4
IF @mAction='0FFAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 5 & 6
IF @mAction='0FFAAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 7 & 8
IF @mAction='0FFAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 9 & 10
IF @mAction='0FFAAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 11 & 12
IF @mAction='0FFAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 13 & 14
IF @mAction='0FFAAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 15 & 16
IF @mAction='0FFAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 17 & 18
IF @mAction='0FFAAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 19 & 20
IF @mAction='0FFAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 21 & 22
IF @mAction='0FFAAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 23 & 24
IF @mAction='0FFAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 25 & 26
IF @mAction='0FFAAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 27 & 28
IF @mAction='0FFAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 29 & 30
IF @mAction='0FFAAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 31 & 32
IF @mAction='0FFAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 33 & 34
IF @mAction='0FFAAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 35 & 36
IF @mAction='0FFAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 37 & 38
IF @mAction='0FFAAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 39 & 40
IF @mAction='0FFAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 41 & 42
IF @mAction='0FFAAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 43 & 44
IF @mAction='0FFAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 45 & 46
IF @mAction='0FFAAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 47 & 48
IF @mAction='0FFAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 49 & 50
IF @mAction='0FFAAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 51 & 52
IF @mAction='0FFAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 53 & 54
IF @mAction='0FFAAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 55 & 56
IF @mAction='0FFAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 57 & 58
IF @mAction='0FFAAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 59 & 60
IF @mAction='0FFAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 61 & 62
IF @mAction='0FFAAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 63 & 64
IF @mAction='0FFAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 65 & 66
IF @mAction='0FFAFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 67 & 68
IF @mAction='0FFAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 69 & 70
IF @mAction='0FFAFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 71 & 72
IF @mAction='0FFAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 73 & 74
IF @mAction='0FFAFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 75 & 76
IF @mAction='0FFAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 77 & 78
IF @mAction='0FFAFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 79 & 80
IF @mAction='0FFAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 81 & 82
IF @mAction='0FFAFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 83 & 84
IF @mAction='0FFAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 85 & 86
IF @mAction='0FFAFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 87 & 88
IF @mAction='0FFAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 89 & 90
IF @mAction='0FFAFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 91 & 92
IF @mAction='0FFAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 93 & 94
IF @mAction='0FFAFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 95 & 96
IF @mAction='0FFAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 97 & 98
IF @mAction='0FFAFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 99 & 100
IF @mAction='0FFAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 101 & 102
IF @mAction='0FFAFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 103 & 104
IF @mAction='0FFAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 105 & 106
IF @mAction='0FFAFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 107 & 108
IF @mAction='0FFAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 109 & 110
IF @mAction='0FFAFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 111 & 112
IF @mAction='0FFAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 113 & 114
IF @mAction='0FFAFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 115 & 116
IF @mAction='0FFAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 117 & 118
IF @mAction='0FFAFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 119 & 120
IF @mAction='0FFAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 121 & 122
IF @mAction='0FFAFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 123 & 124
IF @mAction='0FFAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 125 & 126
IF @mAction='0FFAFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 127 & 128
IF @mAction='0FFAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 129 & 130
IF @mAction='0FFFAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 131 & 132
IF @mAction='0FFFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 133 & 134
IF @mAction='0FFFAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 135 & 136
IF @mAction='0FFFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 137 & 138
IF @mAction='0FFFAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 139 & 140
IF @mAction='0FFFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 141 & 142
IF @mAction='0FFFAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 143 & 144
IF @mAction='0FFFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 145 & 146
IF @mAction='0FFFAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 147 & 148
IF @mAction='0FFFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 149 & 150
IF @mAction='0FFFAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 151 & 152
IF @mAction='0FFFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 153 & 154
IF @mAction='0FFFAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 155 & 156
IF @mAction='0FFFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 157 & 158
IF @mAction='0FFFAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 159 & 160
IF @mAction='0FFFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 161 & 162
IF @mAction='0FFFAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 163 & 164
IF @mAction='0FFFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 165 & 166
IF @mAction='0FFFAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 167 & 168
IF @mAction='0FFFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 169 & 170
IF @mAction='0FFFAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 171 & 172
IF @mAction='0FFFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 173 & 174
IF @mAction='0FFFAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 175 & 176
IF @mAction='0FFFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 177 & 178
IF @mAction='0FFFAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 179 & 180
IF @mAction='0FFFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 181 & 182
IF @mAction='0FFFAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 183 & 184
IF @mAction='0FFFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 185 & 186
IF @mAction='0FFFAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 187 & 188
IF @mAction='0FFFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 189 & 190
IF @mAction='0FFFAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 191 & 192
IF @mAction='0FFFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 193 & 194
IF @mAction='0FFFFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 195 & 196
IF @mAction='0FFFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 197 & 198
IF @mAction='0FFFFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 199 & 200
IF @mAction='0FFFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 201 & 202
IF @mAction='0FFFFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 203 & 204
IF @mAction='0FFFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 205 & 206
IF @mAction='0FFFFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 207 & 208
IF @mAction='0FFFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 209 & 210
IF @mAction='0FFFFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 211 & 212
IF @mAction='0FFFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 213 & 214
IF @mAction='0FFFFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 215 & 216
IF @mAction='0FFFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 217 & 218
IF @mAction='0FFFFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 219 & 220
IF @mAction='0FFFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 221 & 222
IF @mAction='0FFFFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 223 & 224
IF @mAction='0FFFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 225 & 226
IF @mAction='0FFFFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 227 & 228
IF @mAction='0FFFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 229 & 230
IF @mAction='0FFFFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 231 & 232
IF @mAction='0FFFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 233 & 234
IF @mAction='0FFFFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 235 & 236
IF @mAction='0FFFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 237 & 238
IF @mAction='0FFFFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 239 & 240
IF @mAction='0FFFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 241 & 242
IF @mAction='0FFFFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 243 & 244
IF @mAction='0FFFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 245 & 246
IF @mAction='0FFFFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 247 & 248
IF @mAction='0FFFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 249 & 250
IF @mAction='0FFFFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 251 & 252
IF @mAction='0FFFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 253 & 254
IF @mAction='0FFFFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 255 & 256
IF @mAction='0FFFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.OrderReceivedDate >= @mFromDate And
		SOD.OrderReceivedDate <= @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END



GO
