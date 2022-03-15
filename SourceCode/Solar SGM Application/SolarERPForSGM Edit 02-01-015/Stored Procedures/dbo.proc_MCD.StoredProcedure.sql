USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_MCD]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- drop Proc proc_MCD

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_MCD]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mJobcardNo						Varchar(50)		=Null,
@mSection						Varchar(50)		=Null,
@mSpoolHID						Varchar(50)		=Null

AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

IF @mAction='LOADSHIFT'
BEGIN
	SELECT
		ABBREV_ AS SHIFTCODE,			FullName_ AS ShiftInfo

	FROM
		ABBREVTABLE
	
	WHERE
		GROUP_ = 'SHIFTNO'
		
	ORDER BY
		FullName_

END


IF @mAction='LOADSCNDBOXES'
BEGIN

	SELECT
		Barcode + ' | ' + Cast(Size As Varchar(5)) + ' | ' + Cast(SavingQuantity As Varchar(5)) As ScannedBoxes

	FROM
		Spooldetails
	
	WHERE
		SpoolHID = @mSpoolHID
		
	ORDER BY
		Barcode,Size

END

IF @mAction='LOADWRONGBOXES'
BEGIN

	SELECT
		Barcode + ' | ' + Cast(Size As Varchar(5)) + ' | ' + Cast(SavingQuantity As Varchar(5)) As ScannedBoxes

	FROM
		Spooldetails
	
	WHERE
		SpoolHID = @mSpoolHID And FKStatus <> '1'
		
	ORDER BY
		Barcode,Size

END

IF @mAction='LOADMACHINE'
BEGIN
	SELECT
		LocationCode,LocationName

	FROM
		Location
	
	WHERE
		CompanyCode = 'SSPL' And LocationType = 'PRODUCTION'
		
	ORDER BY
		LocationName

END




GO
