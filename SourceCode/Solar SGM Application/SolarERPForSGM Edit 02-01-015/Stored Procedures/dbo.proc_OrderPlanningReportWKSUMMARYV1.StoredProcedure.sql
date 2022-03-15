USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_OrderPlanningReportWKSUMMARYV1]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-------------proc_OrderPlanningReportWKSUMMARYV1
-- drop Proc proc_OrderPlanningReportWKSUMMARYV1

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_OrderPlanningReportWKSUMMARYV1]
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
@mProductTypeMain					Varchar(50)		=Null


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

-- Option No. 1 & 2
IF @mAction='1AAAAAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 3 & 4
IF @mAction='1AAAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 5 & 6
IF @mAction='1AAAAAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 7 & 8
IF @mAction='1AAAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 9 & 10
IF @mAction='1AAAAAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 11 & 12
IF @mAction='1AAAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 13 & 14
IF @mAction='1AAAAAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 15 & 16
IF @mAction='1AAAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 17 & 18
IF @mAction='1AAAAAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 19 & 20
IF @mAction='1AAAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 21 & 22
IF @mAction='1AAAAAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 23 & 24
IF @mAction='1AAAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 25 & 26
IF @mAction='1AAAAAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 27 & 28
IF @mAction='1AAAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 29 & 30
IF @mAction='1AAAAAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 31 & 32
IF @mAction='1AAAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 35 & 36
IF @mAction='1AAAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 37 & 38
IF @mAction='1AAAAFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 39 & 40
IF @mAction='1AAAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 43 & 44
IF @mAction='1AAAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 45 & 46
IF @mAction='1AAAAFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 51 & 52
IF @mAction='1AAAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 53 & 54
IF @mAction='1AAAAFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 59 & 60
IF @mAction='1AAAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 67 & 68
IF @mAction='1AAAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 69 & 70
IF @mAction='1AAAFAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 71 & 72
IF @mAction='1AAAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 73 & 74
IF @mAction='1AAAFAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 75 & 76
IF @mAction='1AAAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 77 & 78
IF @mAction='1AAAFAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 79 & 80
IF @mAction='1AAAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 83 & 84
IF @mAction='1AAAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 85 & 86
IF @mAction='1AAAFAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 87 & 88
IF @mAction='1AAAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 91 & 92
IF @mAction='1AAAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 93 & 94
IF @mAction='1AAAFAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 99 & 100
IF @mAction='1AAAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 101 & 102
IF @mAction='1AAAFFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 107 & 108
IF @mAction='1AAAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 115 & 116
IF @mAction='1AAAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 131 & 132
IF @mAction='1AAFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 133 & 134
IF @mAction='1AAFAAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 135 & 136
IF @mAction='1AAFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 137 & 138
IF @mAction='1AAFAAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 139 & 140
IF @mAction='1AAFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 141 & 142
IF @mAction='1AAFAAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 143 & 144
IF @mAction='1AAFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 147 & 148
IF @mAction='1AAFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 149 & 150
IF @mAction='1AAFAAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 151 & 152
IF @mAction='1AAFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 155 & 156
IF @mAction='1AAFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 157 & 158
IF @mAction='1AAFAAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 163 & 164
IF @mAction='1AAFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 165 & 166
IF @mAction='1AAFAFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 171 & 172
IF @mAction='1AAFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 179 & 180
IF @mAction='1AAFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 195 & 196
IF @mAction='1AAFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 197 & 198
IF @mAction='1AAFFAAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(WeekNo As Integer) >= @mWeekFrom  And
		Cast(WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 199 & 200
IF @mAction='1AAFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 203 & 204
IF @mAction='1AAFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 205 & 206
IF @mAction='1AAFFAAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 211 & 212
IF @mAction='1AAFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 213 & 214
IF @mAction='1AAFFAFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 219 & 220
IF @mAction='1AAFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By WeekNo
	Order by WeekNo
END

-- Option No. 227 & 228
IF @mAction='1AAFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFAAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFAFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFFAAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFFFAA'
BEGIN
	SELECT 
		WeekNo,								Sum(OrderQuantity) As OrderQuantity,
		Sum(ShippedQuantity) As Dispatch,	Sum(OrderQuantity) - Sum(ShippedQuantity) As Balance


	FROM
		Salesorderdetails
	
	WHERE
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AAFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 3 & 4
IF @mAction='1AFAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 5 & 6
IF @mAction='1AFAAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 7 & 8
IF @mAction='1AFAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 9 & 10
IF @mAction='1AFAAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 11 & 12
IF @mAction='1AFAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 13 & 14
IF @mAction='1AFAAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 15 & 16
IF @mAction='1AFAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 17 & 18
IF @mAction='1AFAAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 19 & 20
IF @mAction='1AFAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 21 & 22
IF @mAction='1AFAAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 23 & 24
IF @mAction='1AFAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 25 & 26
IF @mAction='1AFAAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 27 & 28
IF @mAction='1AFAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 29 & 30
IF @mAction='1AFAAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 31 & 32
IF @mAction='1AFAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 35 & 36
IF @mAction='1AFAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 37 & 38
IF @mAction='1AFAAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 39 & 40
IF @mAction='1AFAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 43 & 44
IF @mAction='1AFAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 45 & 46
IF @mAction='1AFAAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 51 & 52
IF @mAction='1AFAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 53 & 54
IF @mAction='1AFAAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 59 & 60
IF @mAction='1AFAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 67 & 68
IF @mAction='1AFAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 69 & 70
IF @mAction='1AFAFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 71 & 72
IF @mAction='1AFAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 73 & 74
IF @mAction='1AFAFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 75 & 76
IF @mAction='1AFAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 77 & 78
IF @mAction='1AFAFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 79 & 80
IF @mAction='1AFAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 83 & 84
IF @mAction='1AFAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 85 & 86
IF @mAction='1AFAFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 87 & 88
IF @mAction='1AFAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 91 & 92
IF @mAction='1AFAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 93 & 94
IF @mAction='1AFAFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 99 & 100
IF @mAction='1AFAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 101 & 102
IF @mAction='1AFAFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 107 & 108
IF @mAction='1AFAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 115 & 116
IF @mAction='1AFAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 131 & 132
IF @mAction='1AFFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 133 & 134
IF @mAction='1AFFAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 135 & 136
IF @mAction='1AFFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 137 & 138
IF @mAction='1AFFAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 139 & 140
IF @mAction='1AFFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 141 & 142
IF @mAction='1AFFAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 143 & 144
IF @mAction='1AFFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 147 & 148
IF @mAction='1AFFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 149 & 150
IF @mAction='1AFFAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 151 & 152
IF @mAction='1AFFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 155 & 156
IF @mAction='1AFFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 157 & 158
IF @mAction='1AFFAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 163 & 164
IF @mAction='1AFFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 165 & 166
IF @mAction='1AFFAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 171 & 172
IF @mAction='1AFFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 179 & 180
IF @mAction='1AFFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 195 & 196
IF @mAction='1AFFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 197 & 198
IF @mAction='1AFFFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 199 & 200
IF @mAction='1AFFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 203 & 204
IF @mAction='1AFFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 205 & 206
IF @mAction='1AFFFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 211 & 212
IF @mAction='1AFFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 213 & 214
IF @mAction='1AFFFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(ExpectedDeliveryDate)) AS int) = @mYear And
		IsEDDNegotiable = @mIsEDDNegotiable And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 219 & 220
IF @mAction='1AFFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
		ExpectedDeliveryDate IS NOT NULL And
		LastInvDate Is Not Null And
		(CASE WHEN Cast(LastInvDate As Date) <= Cast(ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		OrderStatus = @mOrderStatus And
		ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 256 + 227 & 228
IF @mAction='1AFFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		ExpectedDeliveryDate >=  @mFromDate And
		ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1AFFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, Materials As MAT
	
	WHERE SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 3 & 4
IF @mAction='1FAAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 5 & 6
IF @mAction='1FAAAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 7 & 8
IF @mAction='1FAAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 9 & 10
IF @mAction='1FAAAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 11 & 12
IF @mAction='1FAAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 13 & 14
IF @mAction='1FAAAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 15 & 16
IF @mAction='1FAAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 17 & 18
IF @mAction='1FAAAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 19 & 20
IF @mAction='1FAAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 21 & 22
IF @mAction='1FAAAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 23 & 24
IF @mAction='1FAAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 25 & 26
IF @mAction='1FAAAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 27 & 28
IF @mAction='1FAAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 29 & 30
IF @mAction='1FAAAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 31 & 32
IF @mAction='1FAAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 35 & 36
IF @mAction='1FAAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 37 & 38
IF @mAction='1FAAAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 39 & 40
IF @mAction='1FAAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 43 & 44
IF @mAction='1FAAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 45 & 46
IF @mAction='1FAAAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 51 & 52
IF @mAction='1FAAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 53 & 54
IF @mAction='1FAAAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 59 & 60
IF @mAction='1FAAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 67 & 68
IF @mAction='1FAAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 69 & 70
IF @mAction='1FAAFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 71 & 72
IF @mAction='1FAAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 73 & 74
IF @mAction='1FAAFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 75 & 76
IF @mAction='1FAAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 77 & 78
IF @mAction='1FAAFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 79 & 80
IF @mAction='1FAAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 83 & 84
IF @mAction='1FAAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 85 & 86
IF @mAction='1FAAFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 87 & 88
IF @mAction='1FAAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 91 & 92
IF @mAction='1FAAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 93 & 94
IF @mAction='1FAAFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 99 & 100
IF @mAction='1FAAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 101 & 102
IF @mAction='1FAAFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 107 & 108
IF @mAction='1FAAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 115 & 116
IF @mAction='1FAAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 131 & 132
IF @mAction='1FAFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 133 & 134
IF @mAction='1FAFAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 135 & 136
IF @mAction='1FAFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 137 & 138
IF @mAction='1FAFAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 139 & 140
IF @mAction='1FAFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 141 & 142
IF @mAction='1FAFAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 143 & 144
IF @mAction='1FAFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 147 & 148
IF @mAction='1FAFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 149 & 150
IF @mAction='1FAFAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 151 & 152
IF @mAction='1FAFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 155 & 156
IF @mAction='1FAFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 157 & 158
IF @mAction='1FAFAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 163 & 164
IF @mAction='1FAFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 165 & 166
IF @mAction='1FAFAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 171 & 172
IF @mAction='1FAFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 179 & 180
IF @mAction='1FAFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 195 & 196
IF @mAction='1FAFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 197 & 198
IF @mAction='1FAFFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 199 & 200
IF @mAction='1FAFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 203 & 204
IF @mAction='1FAFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 205 & 206
IF @mAction='1FAFFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 211 & 212
IF @mAction='1FAFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 213 & 214
IF @mAction='1FAFFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 219 & 220
IF @mAction='1FAFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 227 & 228
IF @mAction='1FAFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FAFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And
		SOD.Article = MAT.MaterialCode And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 3 & 4
IF @mAction='1FFAAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 5 & 6
IF @mAction='1FFAAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 7 & 8
IF @mAction='1FFAAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 9 & 10
IF @mAction='1FFAAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 11 & 12
IF @mAction='1FFAAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 13 & 14
IF @mAction='1FFAAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 15 & 16
IF @mAction='1FFAAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 17 & 18
IF @mAction='1FFAAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 19 & 20
IF @mAction='1FFAAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 21 & 22
IF @mAction='1FFAAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 23 & 24
IF @mAction='1FFAAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 25 & 26
IF @mAction='1FFAAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 27 & 28
IF @mAction='1FFAAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 29 & 30
IF @mAction='1FFAAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 31 & 32
IF @mAction='1FFAAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 35 & 36
IF @mAction='1FFAAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 37 & 38
IF @mAction='1FFAAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 39 & 40
IF @mAction='1FFAAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 43 & 44
IF @mAction='1FFAAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 45 & 46
IF @mAction='1FFAAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 51 & 52
IF @mAction='1FFAAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 53 & 54
IF @mAction='1FFAAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 59 & 60
IF @mAction='1FFAAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 67 & 68
IF @mAction='1FFAFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 69 & 70
IF @mAction='1FFAFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 71 & 72
IF @mAction='1FFAFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 73 & 74
IF @mAction='1FFAFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 75 & 76
IF @mAction='1FFAFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 77 & 78
IF @mAction='1FFAFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 79 & 80
IF @mAction='1FFAFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 83 & 84
IF @mAction='1FFAFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 85 & 86
IF @mAction='1FFAFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 87 & 88
IF @mAction='1FFAFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 91 & 92
IF @mAction='1FFAFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 93 & 94
IF @mAction='1FFAFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 99 & 100
IF @mAction='1FFAFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 101 & 102
IF @mAction='1FFAFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 107 & 108
IF @mAction='1FFAFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 115 & 116
IF @mAction='1FFAFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFAFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 131 & 132
IF @mAction='1FFFAAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 133 & 134
IF @mAction='1FFFAAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 135 & 136
IF @mAction='1FFFAAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 137 & 138
IF @mAction='1FFFAAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 139 & 140
IF @mAction='1FFFAAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 141 & 142
IF @mAction='1FFFAAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 143 & 144
IF @mAction='1FFFAAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 147 & 148
IF @mAction='1FFFAAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 149 & 150
IF @mAction='1FFFAAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 151 & 152
IF @mAction='1FFFAAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 155 & 156
IF @mAction='1FFFAAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 157 & 158
IF @mAction='1FFFAAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 163 & 164
IF @mAction='1FFFAFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 165 & 166
IF @mAction='1FFFAFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 171 & 172
IF @mAction='1FFFAFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 179 & 180
IF @mAction='1FFFAFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFAFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 195 & 196
IF @mAction='1FFFFAAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 197 & 198
IF @mAction='1FFFFAAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		Cast(SOD.WeekNo As Integer) >= @mWeekFrom  And
		Cast(SOD.WeekNo As Integer) <= @mWeekTo And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 199 & 200
IF @mAction='1FFFFAAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 203 & 204
IF @mAction='1FFFFAAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 205 & 206
IF @mAction='1FFFFAAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 211 & 212
IF @mAction='1FFFFAFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL AND
		MAT.CodificationNew = @mCodificationNew And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 213 & 214
IF @mAction='1FFFFAFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		CAST((YEAR(SOD.ExpectedDeliveryDate)) AS int) = @mYear And
		SOD.IsEDDNegotiable = @mIsEDDNegotiable And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 219 & 220
IF @mAction='1FFFFAFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFAFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
		SOD.ExpectedDeliveryDate IS NOT NULL And
		SOD.LastInvDate Is Not Null And
		(CASE WHEN Cast(SOD.LastInvDate As Date) <= Cast(SOD.ExpectedDeliveryDate As Date) Then 'ON TIME' ELSE 'DELAYED' END) = @mShipmentStatus And
		SOD.OrderStatus = @mOrderStatus And
		SOD.ArticleName like '%' + @mDescription + '%'
	
	Group By SOD.WeekNo
	Order by SOD.WeekNo
END

-- Option No. 512 + 256 + 227 & 228
IF @mAction='1FFFFFAAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFAFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFAAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFAAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFAFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFAFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFFAAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFFAFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFFFAA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
IF @mAction='1FFFFFFFFFA'
BEGIN
	SELECT 
		SOD.WeekNo,								Sum(SOD.OrderQuantity) As OrderQuantity,
		Sum(SOD.ShippedQuantity) As Dispatch,	Sum(SOD.OrderQuantity) - Sum(SOD.ShippedQuantity) As Balance


	FROM
		Salesorderdetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE SOD.SalesOrderID = SO.ID And SO.BuyerName = @mBuyerName And SOD.Article = MAT.MaterialCode And  MAT.ArticleMould = @mArticleMould And
		
		SOD.ExpectedDeliveryDate >=  @mFromDate And
		SOD.ExpectedDeliveryDate <=  @mToDate And
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
