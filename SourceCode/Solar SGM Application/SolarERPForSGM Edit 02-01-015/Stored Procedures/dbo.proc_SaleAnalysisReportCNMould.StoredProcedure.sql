USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportCNMould]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop Proc proc_SaleAnalysisReportCNMould

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportCNMould]
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
IF @mAction= '21AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 2
IF @mAction= '21AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 3
--IF @mAction= '21AAAAAAFA'

-- Option No. 4
--IF @mAction= '21AAAAAAFF'

-- Option No. 5
IF @mAction= '21AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 6
IF @mAction= '21AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 7
IF @mAction= '21AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 8
IF @mAction= '21AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 9
IF @mAction= '21AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 10
IF @mAction= '21AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 11
--IF @mAction= '21AAAAFAFA'

-- Option No. 12
--IF @mAction= '21AAAAFAFF'

-- Option No. 13
IF @mAction= '21AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 14
IF @mAction= '21AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 15
IF @mAction= '21AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 16
IF @mAction= '21AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 17
IF @mAction= '21AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 18
IF @mAction= '21AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 19
--IF @mAction= '21AAAFAAFA'

-- Option No. 20
--IF @mAction= '21AAAFAAFF'

-- Option No. 21
IF @mAction= '21AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 22
IF @mAction= '21AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 23
IF @mAction= '21AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 24
IF @mAction= '21AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 25
IF @mAction= '21AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 26
IF @mAction= '21AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 27
--IF @mAction= '21AAAFFAFA'

-- Option No. 28
--IF @mAction= '21AAAFFAFF'

-- Option No. 29
IF @mAction= '21AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 30
IF @mAction= '21AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 31
IF @mAction= '21AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 32
IF @mAction= '21AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 33
IF @mAction= '21AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 34
IF @mAction= '21AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 35
--IF @mAction= '21AAFAAAFA'

-- Option No. 36
--IF @mAction= '21AAFAAAFF'

-- Option No. 37
IF @mAction= '21AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 38
IF @mAction= '21AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 39
IF @mAction= '21AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 40
IF @mAction= '21AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 41
IF @mAction= '21AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 42
IF @mAction= '21AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 43
--IF @mAction= '21AAFAFAFA'

-- Option No. 44
--IF @mAction= '21AAFAFAFF'

-- Option No. 45
IF @mAction= '21AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 46
IF @mAction= '21AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 47
IF @mAction= '21AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 48
IF @mAction= '21AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 49
IF @mAction= '21AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 50
IF @mAction= '21AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 51
--IF @mAction= '21AAFFAAFA'

-- Option No. 52
--IF @mAction= '21AAFFAAFF'

-- Option No. 53
IF @mAction= '21AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 54
IF @mAction= '21AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 55
IF @mAction= '21AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 56
IF @mAction= '21AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 57
IF @mAction= '21AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 58
IF @mAction= '21AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 59
--IF @mAction= '21AAFFFAFA'

-- Option No. 60
--IF @mAction= '21AAFFFAFF'

-- Option No. 61
IF @mAction= '21AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 62
IF @mAction= '21AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 63
IF @mAction= '21AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 64
IF @mAction= '21AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 65
IF @mAction= '21AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 66
IF @mAction= '21AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 67
--IF @mAction= '21AFAAAAFA'

-- Option No. 68
--IF @mAction= '21AFAAAAFF'

-- Option No. 69
IF @mAction= '21AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 70
IF @mAction= '21AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 71
IF @mAction= '21AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 72
IF @mAction= '21AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 73
IF @mAction= '21AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 74
IF @mAction= '21AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 75
--IF @mAction= '21AFAAFAFA'

-- Option No. 76
--IF @mAction= '21AFAAFAFF'

-- Option No. 77
IF @mAction= '21AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 78
IF @mAction= '21AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 79
IF @mAction= '21AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 80
IF @mAction= '21AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 81
IF @mAction= '21AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 82
IF @mAction= '21AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 83
--IF @mAction= '21AFAFAAFA'

-- Option No. 84
--IF @mAction= '21AFAFAAFF'

-- Option No. 85
IF @mAction= '21AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 86
IF @mAction= '21AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 87
IF @mAction= '21AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 88
IF @mAction= '21AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 89
IF @mAction= '21AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 90
IF @mAction= '21AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 91
--IF @mAction= '21AFAFFAFA'

-- Option No. 92
--IF @mAction= '21AFAFFAFF'

-- Option No. 93
IF @mAction= '21AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 94
IF @mAction= '21AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 95
IF @mAction= '21AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 96
IF @mAction= '21AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 97
IF @mAction= '21AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 98
IF @mAction= '21AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 99
--IF @mAction= '21AFFAAAFA'

-- Option No. 100
--IF @mAction= '21AFFAAAFF'

-- Option No. 101
IF @mAction= '21AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 102
IF @mAction= '21AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 103
IF @mAction= '21AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 104
IF @mAction= '21AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 105
IF @mAction= '21AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 106
IF @mAction= '21AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 107
--IF @mAction= '21AFFAFAFA'

-- Option No. 108
--IF @mAction= '21AFFAFAFF'

-- Option No. 109
IF @mAction= '21AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 110
IF @mAction= '21AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 111
IF @mAction= '21AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 112
IF @mAction= '21AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 113
IF @mAction= '21AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 114
IF @mAction= '21AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 115
--IF @mAction= '21AFFFAAFA'

-- Option No. 116
--IF @mAction= '21AFFFAAFF'

-- Option No. 117
IF @mAction= '21AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 118
IF @mAction= '21AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 119
IF @mAction= '21AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 120
IF @mAction= '21AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 121
IF @mAction= '21AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 122
IF @mAction= '21AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 123
--IF @mAction= '21AFFFFAFA'

-- Option No. 124
--IF @mAction= '21AFFFFAFF'

-- Option No. 125
IF @mAction= '21AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 126
IF @mAction= '21AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 127
IF @mAction= '21AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 128
IF @mAction= '21AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 129
IF @mAction= '21FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 130
IF @mAction= '21FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 131
--IF @mAction= '21FAAAAAFA'

-- Option No. 132
--IF @mAction= '21FAAAAAFF'

-- Option No. 133
IF @mAction= '21FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 134
IF @mAction= '21FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 135
IF @mAction= '21FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 136
IF @mAction= '21FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 138
IF @mAction= '21FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 138
IF @mAction= '21FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 139
--IF @mAction= '21FAAAFAFA'

-- Option No. 140
--IF @mAction= '21FAAAFAFF'

-- Option No. 141
IF @mAction= '21FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 142
IF @mAction= '21FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 143
IF @mAction= '21FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 144
IF @mAction= '21FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 145
IF @mAction= '21FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 146
IF @mAction= '21FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 147
--IF @mAction= '21FAAFAAFA'

-- Option No. 148
--IF @mAction= '21FAAFAAFF'

-- Option No. 149
IF @mAction= '21FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 150
IF @mAction= '21FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 151
IF @mAction= '21FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 152
IF @mAction= '21FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 153
IF @mAction= '21FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 154
IF @mAction= '21FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 155
--IF @mAction= '21FAAFFAFA'

-- Option No. 156
--IF @mAction= '21FAAFFAFF'

-- Option No. 157
IF @mAction= '21FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 158
IF @mAction= '21FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 159
IF @mAction= '21FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 160
IF @mAction= '21FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 161
IF @mAction= '21FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 162
IF @mAction= '21FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 163
--IF @mAction= '21FAFAAAFA'

-- Option No. 164
--IF @mAction= '21FAFAAAFF'

-- Option No. 165
IF @mAction= '21FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 166
IF @mAction= '21FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 167
IF @mAction= '21FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 168
IF @mAction= '21FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 169
IF @mAction= '21FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 170
IF @mAction= '21FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 171
--IF @mAction= '21FAFAFAFA'

-- Option No. 172
--IF @mAction= '21FAFAFAFF'

-- Option No. 173
IF @mAction= '21FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 174
IF @mAction= '21FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 175
IF @mAction= '21FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 176
IF @mAction= '21FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 177
IF @mAction= '21FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 178
IF @mAction= '21FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 179
--IF @mAction= '21FAFFAAFA'

-- Option No. 180
--IF @mAction= '21FAFFAAFF'

-- Option No. 181
IF @mAction= '21FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 182
IF @mAction= '21FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 183
IF @mAction= '21FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 184
IF @mAction= '21FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 185
IF @mAction= '21FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 186
IF @mAction= '21FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 187
--IF @mAction= '21FAFFFAFA'

-- Option No. 188
--IF @mAction= '21FAFFFAFF'

-- Option No. 189
IF @mAction= '21FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 190
IF @mAction= '21FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 191
IF @mAction= '21FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 192
IF @mAction= '21FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 193
IF @mAction= '21FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 194
IF @mAction= '21FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 195
--IF @mAction= '21FFAAAAFA'

-- Option No. 196
--IF @mAction= '21FFAAAAFF'

-- Option No. 197
IF @mAction= '21FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 198
IF @mAction= '21FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 199
IF @mAction= '21FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 200
IF @mAction= '21FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 201
IF @mAction= '21FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 202
IF @mAction= '21FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 203
--IF @mAction= '21FFAAFAFA'

-- Option No. 204
--IF @mAction= '21FFAAFAFF'

-- Option No. 205
IF @mAction= '21FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 206
IF @mAction= '21FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 207
IF @mAction= '21FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 208
IF @mAction= '21FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 209
IF @mAction= '21FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 210
IF @mAction= '21FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 211
--IF @mAction= '21FFAFAAFA'

-- Option No. 212
--IF @mAction= '21FFAFAAFF'

-- Option No. 213
IF @mAction= '21FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 214
IF @mAction= '21FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 215
IF @mAction= '21FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 216
IF @mAction= '21FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 217
IF @mAction= '21FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 218
IF @mAction= '21FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 219
--IF @mAction= '21FFAFFAFA'

-- Option No. 220
--IF @mAction= '21FFAFFAFF'

-- Option No. 221
IF @mAction= '21FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 222
IF @mAction= '21FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 223
IF @mAction= '21FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 224
IF @mAction= '21FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 225
IF @mAction= '21FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 226
IF @mAction= '21FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 227
--IF @mAction= '21FFFAAAFA'

-- Option No. 228
--IF @mAction= '21FFFAAAFF'

-- Option No. 229
IF @mAction= '21FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 230
IF @mAction= '21FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 231
IF @mAction= '21FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 232
IF @mAction= '21FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 233
IF @mAction= '21FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 234
IF @mAction= '21FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 235
--IF @mAction= '21FFFAFAFA'

-- Option No. 236
--IF @mAction= '21FFFAFAFF'

-- Option No. 237
IF @mAction= '21FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 238
IF @mAction= '21FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 239
IF @mAction= '21FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 240
IF @mAction= '21FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 241
IF @mAction= '21FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 242
IF @mAction= '21FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 243
--IF @mAction= '21FFFFAAFA'

-- Option No. 244
--IF @mAction= '21FFFFAAFF'

-- Option No. 245
IF @mAction= '21FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 246
IF @mAction= '21FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 247
IF @mAction= '21FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 248
IF @mAction= '21FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 249
IF @mAction= '21FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 250
IF @mAction= '21FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
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
--IF @mAction= '21FFFFFAFA'

-- Option No. 252
--IF @mAction= '21FFFFFAFF'

-- Option No. 253
IF @mAction= '21FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 254
IF @mAction= '21FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 255
IF @mAction= '21FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END

-- Option No. 256
IF @mAction= '21FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT '' As BuyerName, MAT.ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate)
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY MAT.ArticleMould
	ORDER BY MAT.ArticleMould
END






GO
