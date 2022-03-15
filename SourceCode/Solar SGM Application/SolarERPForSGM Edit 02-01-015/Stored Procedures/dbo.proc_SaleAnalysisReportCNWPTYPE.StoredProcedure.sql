USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_SaleAnalysisReportCNWPTYPE]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop Proc proc_SaleAnalysisReportCNWPTYPE

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportCNWPTYPE]
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
IF @mAction= '20AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 2
IF @mAction= '20AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 3
--IF @mAction= '20AAAAAAFA'

-- Option No. 4
--IF @mAction= '20AAAAAAFF'

-- Option No. 5
IF @mAction= '20AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 6
IF @mAction= '20AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 7
IF @mAction= '20AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 8
IF @mAction= '20AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 9
IF @mAction= '20AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 10
IF @mAction= '20AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 11
--IF @mAction= '20AAAAFAFA'

-- Option No. 12
--IF @mAction= '20AAAAFAFF'

-- Option No. 13
IF @mAction= '20AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 14
IF @mAction= '20AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 15
IF @mAction= '20AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 16
IF @mAction= '20AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 17
IF @mAction= '20AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 18
IF @mAction= '20AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 19
--IF @mAction= '20AAAFAAFA'

-- Option No. 20
--IF @mAction= '20AAAFAAFF'

-- Option No. 21
IF @mAction= '20AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 22
IF @mAction= '20AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 23
IF @mAction= '20AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 24
IF @mAction= '20AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 25
IF @mAction= '20AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 26
IF @mAction= '20AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 27
--IF @mAction= '20AAAFFAFA'

-- Option No. 28
--IF @mAction= '20AAAFFAFF'

-- Option No. 29
IF @mAction= '20AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 30
IF @mAction= '20AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 31
IF @mAction= '20AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 32
IF @mAction= '20AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 33
IF @mAction= '20AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 34
IF @mAction= '20AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 35
--IF @mAction= '20AAFAAAFA'

-- Option No. 36
--IF @mAction= '20AAFAAAFF'

-- Option No. 37
IF @mAction= '20AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 38
IF @mAction= '20AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 39
IF @mAction= '20AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 40
IF @mAction= '20AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 41
IF @mAction= '20AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 42
IF @mAction= '20AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 43
--IF @mAction= '20AAFAFAFA'

-- Option No. 44
--IF @mAction= '20AAFAFAFF'

-- Option No. 45
IF @mAction= '20AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 46
IF @mAction= '20AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 47
IF @mAction= '20AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 48
IF @mAction= '20AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 49
IF @mAction= '20AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 50
IF @mAction= '20AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 51
--IF @mAction= '20AAFFAAFA'

-- Option No. 52
--IF @mAction= '20AAFFAAFF'

-- Option No. 53
IF @mAction= '20AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 54
IF @mAction= '20AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 55
IF @mAction= '20AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 56
IF @mAction= '20AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 57
IF @mAction= '20AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 58
IF @mAction= '20AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 59
--IF @mAction= '20AAFFFAFA'

-- Option No. 60
--IF @mAction= '20AAFFFAFF'

-- Option No. 61
IF @mAction= '20AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 62
IF @mAction= '20AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 63
IF @mAction= '20AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 64
IF @mAction= '20AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 65
IF @mAction= '20AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 66
IF @mAction= '20AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 67
--IF @mAction= '20AFAAAAFA'

-- Option No. 68
--IF @mAction= '20AFAAAAFF'

-- Option No. 69
IF @mAction= '20AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 70
IF @mAction= '20AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 71
IF @mAction= '20AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 72
IF @mAction= '20AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 73
IF @mAction= '20AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 74
IF @mAction= '20AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 75
--IF @mAction= '20AFAAFAFA'

-- Option No. 76
--IF @mAction= '20AFAAFAFF'

-- Option No. 77
IF @mAction= '20AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 78
IF @mAction= '20AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 79
IF @mAction= '20AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 80
IF @mAction= '20AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 81
IF @mAction= '20AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 82
IF @mAction= '20AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 83
--IF @mAction= '20AFAFAAFA'

-- Option No. 84
--IF @mAction= '20AFAFAAFF'

-- Option No. 85
IF @mAction= '20AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 86
IF @mAction= '20AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 87
IF @mAction= '20AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 88
IF @mAction= '20AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 89
IF @mAction= '20AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 90
IF @mAction= '20AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 91
--IF @mAction= '20AFAFFAFA'

-- Option No. 92
--IF @mAction= '20AFAFFAFF'

-- Option No. 93
IF @mAction= '20AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 94
IF @mAction= '20AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 95
IF @mAction= '20AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 96
IF @mAction= '20AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 97
IF @mAction= '20AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 98
IF @mAction= '20AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 99
--IF @mAction= '20AFFAAAFA'

-- Option No. 100
--IF @mAction= '20AFFAAAFF'

-- Option No. 101
IF @mAction= '20AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 102
IF @mAction= '20AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 103
IF @mAction= '20AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 104
IF @mAction= '20AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 105
IF @mAction= '20AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 106
IF @mAction= '20AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 107
--IF @mAction= '20AFFAFAFA'

-- Option No. 108
--IF @mAction= '20AFFAFAFF'

-- Option No. 109
IF @mAction= '20AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 110
IF @mAction= '20AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 111
IF @mAction= '20AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 112
IF @mAction= '20AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 113
IF @mAction= '20AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 114
IF @mAction= '20AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 115
--IF @mAction= '20AFFFAAFA'

-- Option No. 116
--IF @mAction= '20AFFFAAFF'

-- Option No. 117
IF @mAction= '20AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 118
IF @mAction= '20AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 119
IF @mAction= '20AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 120
IF @mAction= '20AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 121
IF @mAction= '20AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 122
IF @mAction= '20AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 123
--IF @mAction= '20AFFFFAFA'

-- Option No. 124
--IF @mAction= '20AFFFFAFF'

-- Option No. 125
IF @mAction= '20AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 126
IF @mAction= '20AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 127
IF @mAction= '20AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 128
IF @mAction= '20AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 129
IF @mAction= '20FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 130
IF @mAction= '20FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 131
--IF @mAction= '20FAAAAAFA'

-- Option No. 132
--IF @mAction= '20FAAAAAFF'

-- Option No. 133
IF @mAction= '20FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 134
IF @mAction= '20FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 135
IF @mAction= '20FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 136
IF @mAction= '20FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 138
IF @mAction= '20FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 138
IF @mAction= '20FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 139
--IF @mAction= '20FAAAFAFA'

-- Option No. 140
--IF @mAction= '20FAAAFAFF'

-- Option No. 141
IF @mAction= '20FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 142
IF @mAction= '20FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 143
IF @mAction= '20FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 144
IF @mAction= '20FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 145
IF @mAction= '20FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 146
IF @mAction= '20FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 147
--IF @mAction= '20FAAFAAFA'

-- Option No. 148
--IF @mAction= '20FAAFAAFF'

-- Option No. 149
IF @mAction= '20FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 150
IF @mAction= '20FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 151
IF @mAction= '20FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 152
IF @mAction= '20FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 153
IF @mAction= '20FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 154
IF @mAction= '20FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 155
--IF @mAction= '20FAAFFAFA'

-- Option No. 156
--IF @mAction= '20FAAFFAFF'

-- Option No. 157
IF @mAction= '20FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 158
IF @mAction= '20FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 159
IF @mAction= '20FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 160
IF @mAction= '20FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 161
IF @mAction= '20FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 162
IF @mAction= '20FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 163
--IF @mAction= '20FAFAAAFA'

-- Option No. 164
--IF @mAction= '20FAFAAAFF'

-- Option No. 165
IF @mAction= '20FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 166
IF @mAction= '20FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 167
IF @mAction= '20FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 168
IF @mAction= '20FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 169
IF @mAction= '20FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 170
IF @mAction= '20FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 171
--IF @mAction= '20FAFAFAFA'

-- Option No. 172
--IF @mAction= '20FAFAFAFF'

-- Option No. 173
IF @mAction= '20FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 174
IF @mAction= '20FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 175
IF @mAction= '20FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 176
IF @mAction= '20FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 177
IF @mAction= '20FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 178
IF @mAction= '20FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 179
--IF @mAction= '20FAFFAAFA'

-- Option No. 180
--IF @mAction= '20FAFFAAFF'

-- Option No. 181
IF @mAction= '20FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 182
IF @mAction= '20FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 183
IF @mAction= '20FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 184
IF @mAction= '20FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 185
IF @mAction= '20FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 186
IF @mAction= '20FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 187
--IF @mAction= '20FAFFFAFA'

-- Option No. 188
--IF @mAction= '20FAFFFAFF'

-- Option No. 189
IF @mAction= '20FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 190
IF @mAction= '20FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 191
IF @mAction= '20FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 192
IF @mAction= '20FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 193
IF @mAction= '20FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 194
IF @mAction= '20FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 195
--IF @mAction= '20FFAAAAFA'

-- Option No. 196
--IF @mAction= '20FFAAAAFF'

-- Option No. 197
IF @mAction= '20FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 198
IF @mAction= '20FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 199
IF @mAction= '20FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 200
IF @mAction= '20FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 201
IF @mAction= '20FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 202
IF @mAction= '20FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 203
--IF @mAction= '20FFAAFAFA'

-- Option No. 204
--IF @mAction= '20FFAAFAFF'

-- Option No. 205
IF @mAction= '20FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 206
IF @mAction= '20FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 207
IF @mAction= '20FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 208
IF @mAction= '20FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 209
IF @mAction= '20FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 210
IF @mAction= '20FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 211
--IF @mAction= '20FFAFAAFA'

-- Option No. 212
--IF @mAction= '20FFAFAAFF'

-- Option No. 213
IF @mAction= '20FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 214
IF @mAction= '20FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 215
IF @mAction= '20FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 216
IF @mAction= '20FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 217
IF @mAction= '20FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 218
IF @mAction= '20FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 219
--IF @mAction= '20FFAFFAFA'

-- Option No. 220
--IF @mAction= '20FFAFFAFF'

-- Option No. 221
IF @mAction= '20FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 222
IF @mAction= '20FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 223
IF @mAction= '20FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 224
IF @mAction= '20FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 225
IF @mAction= '20FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 226
IF @mAction= '20FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 227
--IF @mAction= '20FFFAAAFA'

-- Option No. 228
--IF @mAction= '20FFFAAAFF'

-- Option No. 229
IF @mAction= '20FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 230
IF @mAction= '20FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 231
IF @mAction= '20FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 232
IF @mAction= '20FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 233
IF @mAction= '20FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 234
IF @mAction= '20FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 235
--IF @mAction= '20FFFAFAFA'

-- Option No. 236
--IF @mAction= '20FFFAFAFF'

-- Option No. 237
IF @mAction= '20FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 238
IF @mAction= '20FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 239
IF @mAction= '20FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 240
IF @mAction= '20FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 241
IF @mAction= '20FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 242
IF @mAction= '20FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 243
--IF @mAction= '20FFFFAAFA'

-- Option No. 244
--IF @mAction= '20FFFFAAFF'

-- Option No. 245
IF @mAction= '20FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 246
IF @mAction= '20FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 247
IF @mAction= '20FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 248
IF @mAction= '20FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 249
IF @mAction= '20FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 250
IF @mAction= '20FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 251
--IF @mAction= '20FFFFFAFA'

-- Option No. 252
--IF @mAction= '20FFFFFAFF'

-- Option No. 253
IF @mAction= '20FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 254
IF @mAction= '20FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 255
IF @mAction= '20FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END

-- Option No. 256
IF @mAction= '20FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, '' As ArticleMould, '' As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName
	ORDER BY Buy.BuyerName
END





GO
