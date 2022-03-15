USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_WIPMANAGETOOLS]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop Proc proc_WIPMANAGETOOLS

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_WIPMANAGETOOLS]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mJobcardNo						Varchar(50)		=Null,
@mSection						Varchar(50)		=Null

AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

IF @mAction='LOADWIPINFO'
BEGIN
	--SELECT
	--	WIPID,			PlanDate As Date,		JobcardQty,		JobcardNo

	--FROM
	--	JobcardWIP
	
	--WHERE
	--	PlanDate >= @mFromDate And
	--	PlanDate <= @mToDate
		
	--ORDER BY
	--	WIPID,			PlanDate

	SELECT
		WIPID,			PlanDate As Date,		JobcardQty,		JobcardNo

	FROM
		JobcardWIP
	
	WHERE
		PlanDate >= @mFromDate And
		PlanDate <= @mToDate
		
	union all

Select 
SpoolID As WIPID,SpoolDate As Date,Quantity as JobcardQty,'' As JobcardNo
from Spool Where Cast(SpoolDate As Date) >= @mFromDate And Cast(SpoolDate As Date) <= @mToDate

ORDER BY
		WIPID,			PlanDate

END

IF @mAction='LOADWIPDTLS'
BEGIN
	SELECT 
		JCD.CustomerOrderNo,		SOD.OrderReceivedDate,		JCD.JobCardNo,			PBP.ProcessName,		PBP.ProcessDate,
		PBP.Size,					SUM(PBP.Quantity) AS Quantity

	FROM
		dbo.JobCardDetail AS JCD INNER JOIN
		dbo.SalesOrderDetails AS SOD ON JCD.SalesOrderDetailID = SOD.ID INNER JOIN
		dbo.ProductionByProcess AS PBP ON JCD.JobCardNo = PBP.WorkOrderNo

	WHERE
		JCD.JobCardNo = @mJobcardNo

	GROUP BY
		JCD.CustomerOrderNo,		SOD.OrderReceivedDate,		JCD.JobCardNo,			PBP.ProcessName,			PBP.ProcessDate,			PBP.Size

	ORDER BY
		PBP.ProcessDate,			PBP.ProcessName,			(CAST (PBP.Size  As Decimal(18,1)))
END

IF @mAction='LOADWIPDTLSF'
BEGIN
	SELECT 
		JCD.CustomerOrderNo,		SOD.OrderReceivedDate,		JCD.JobCardNo,			PBP.ProcessName,		PBP.ProcessDate,
		PBP.Size,					SUM(PBP.Quantity) AS Quantity

	FROM
		dbo.JobCardDetail AS JCD INNER JOIN
		dbo.SalesOrderDetails AS SOD ON JCD.SalesOrderDetailID = SOD.ID INNER JOIN
		dbo.ProductionByProcess AS PBP ON JCD.JobCardNo = PBP.WorkOrderNo

	WHERE
		JCD.JobCardNo = @mJobcardNo And
		pbp.ProcessName = @mSection

	GROUP BY
		JCD.CustomerOrderNo,		SOD.OrderReceivedDate,		JCD.JobCardNo,			PBP.ProcessName,			PBP.ProcessDate,			PBP.Size

	ORDER BY
		PBP.ProcessDate,			PBP.ProcessName,			(CAST (PBP.Size  As Decimal(18,1)))
END


IF @mAction='LOADWIPDTLSFSPL'
BEGIN
	SELECT 
		JCD.CustomerOrderNo,		SOD.OrderReceivedDate,		PBP.Barcode As JobCardNo,			--PBP.ProcessName,		
		CASE WHEN Left(PBP.SpoolHID,1) = 'M' THEN 'MOULD'
		WHEN Left(PBP.SpoolHID,1) = 'F' THEN 'FINISH'
		WHEN Left(PBP.SpoolHID,1) = 'P' THEN 'PACK'
		ELSE
		Left(PBP.SpoolHID,1) 
		END AS ProcessName,
		PBP.UPdatedOn As ProcessDate,
		PBP.Size,					PBP.SavingQuantity AS Quantity

	FROM
		dbo.JobCardDetail AS JCD INNER JOIN
		dbo.SalesOrderDetails AS SOD ON JCD.SalesOrderDetailID = SOD.ID INNER JOIN
		dbo.SpoolDetails AS PBP ON JCD.JobCardNo = Left(PBP.Barcode,13)

	WHERE
		PBP.SpoolHID = @mJobcardNo
		--PBP.SpoolHID = 'F-01-1920-000110'


	ORDER BY
		--PBP.ProcessDate,			PBP.ProcessName,			
		(CAST (PBP.Size  As Decimal(18,1)))
END
If @mAction= 'LOADCUSTOMER'
BEGIN
	SELECT 
	' ALL CUSTOMERS' As BuyerName
	
	UNION 
	
	Select 
		Distinct SO.BuyerName As BuyerName

	from 
		Salesorderdetails As SOD, SalesOrder As SO, JobcardDetail As JCD, JobcardWIP As WIP
	Where 
		SOD.SalesOrderID = SO.ID And
		WIP.JobcardNo = JCD.JobcardNo And
		JCD.SalesOrderDetailID = sod.ID And
		WIP.PlanDate >= @mFromDate 
		And WIP.PlanDate <= @mToDate

	Order by BuyerName


END	

IF @mAction='LOADWIPINFOB'
BEGIN
	SELECT
		WIPID,			PlanDate As Date,		JobcardQty,		JobcardNo

	FROM
		JobcardWIP
	
	WHERE
		PlanDate >= @mFromDate And
		PlanDate <= @mToDate And
		JobcardNo in ( 
			Select 
				JCD.JobCardNo
			
			from 
				Salesorderdetails As SOD, SalesOrder As SO, JobcardDetail As JCD
	
			Where 
				SOD.SalesOrderID = SO.ID And
				JCD.SalesOrderDetailID = sod.ID And
				SO.BuyerName = @mBuyerName
		)
		
	ORDER BY
		WIPID,			PlanDate
END





GO
