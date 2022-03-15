USE [AHGroup_SSPL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop Proc proc_SaleAnalysisReportCNMACWPTYPE

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_SaleAnalysisReportCNMACWPTYPE]
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
IF @mAction= '22AAAAAAAA'

BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
	
END

-- Option No. 2
IF @mAction= '22AAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 3
--IF @mAction= '22AAAAAAFA'

-- Option No. 4
--IF @mAction= '22AAAAAAFF'

-- Option No. 5
IF @mAction= '22AAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 6
IF @mAction= '22AAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 7
IF @mAction= '22AAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 8
IF @mAction= '22AAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 9
IF @mAction= '22AAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 10
IF @mAction= '22AAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 11
--IF @mAction= '22AAAAFAFA'

-- Option No. 12
--IF @mAction= '22AAAAFAFF'

-- Option No. 13
IF @mAction= '22AAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 14
IF @mAction= '22AAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 15
IF @mAction= '22AAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 16
IF @mAction= '22AAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 17
IF @mAction= '22AAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 18
IF @mAction= '22AAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 19
--IF @mAction= '22AAAFAAFA'

-- Option No. 20
--IF @mAction= '22AAAFAAFF'

-- Option No. 21
IF @mAction= '22AAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 22
IF @mAction= '22AAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 23
IF @mAction= '22AAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 24
IF @mAction= '22AAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 25
IF @mAction= '22AAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 26
IF @mAction= '22AAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 27
--IF @mAction= '22AAAFFAFA'

-- Option No. 28
--IF @mAction= '22AAAFFAFF'

-- Option No. 29
IF @mAction= '22AAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 30
IF @mAction= '22AAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 31
IF @mAction= '22AAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 32
IF @mAction= '22AAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 33
IF @mAction= '22AAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 34
IF @mAction= '22AAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 35
--IF @mAction= '22AAFAAAFA'

-- Option No. 36
--IF @mAction= '22AAFAAAFF'

-- Option No. 37
IF @mAction= '22AAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 38
IF @mAction= '22AAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)	
				And (MAT.CodificationNew = @mProductType
				And Mat.ArticleMould = @mArticleMould)
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 39
IF @mAction= '22AAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 40
IF @mAction= '22AAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 41
IF @mAction= '22AAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 42
IF @mAction= '22AAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 43
--IF @mAction= '22AAFAFAFA'

-- Option No. 44
--IF @mAction= '22AAFAFAFF'

-- Option No. 45
IF @mAction= '22AAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 46
IF @mAction= '22AAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 47
IF @mAction= '22AAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 48
IF @mAction= '22AAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 49
IF @mAction= '22AAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 50
IF @mAction= '22AAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 51
--IF @mAction= '22AAFFAAFA'

-- Option No. 52
--IF @mAction= '22AAFFAAFF'

-- Option No. 53
IF @mAction= '22AAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 54
IF @mAction= '22AAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 55
IF @mAction= '22AAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 56
IF @mAction= '22AAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 57
IF @mAction= '22AAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 58
IF @mAction= '22AAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 59
--IF @mAction= '22AAFFFAFA'

-- Option No. 60
--IF @mAction= '22AAFFFAFF'

-- Option No. 61
IF @mAction= '22AAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 62
IF @mAction= '22AAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 63
IF @mAction= '22AAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 64
IF @mAction= '22AAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 65
IF @mAction= '22AFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 66
IF @mAction= '22AFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 67
--IF @mAction= '22AFAAAAFA'

-- Option No. 68
--IF @mAction= '22AFAAAAFF'

-- Option No. 69
IF @mAction= '22AFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 70
IF @mAction= '22AFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 71
IF @mAction= '22AFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 72
IF @mAction= '22AFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 73
IF @mAction= '22AFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 74
IF @mAction= '22AFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 75
--IF @mAction= '22AFAAFAFA'

-- Option No. 76
--IF @mAction= '22AFAAFAFF'

-- Option No. 77
IF @mAction= '22AFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 78
IF @mAction= '22AFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 79
IF @mAction= '22AFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 80
IF @mAction= '22AFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 81
IF @mAction= '22AFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 82
IF @mAction= '22AFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 83
--IF @mAction= '22AFAFAAFA'

-- Option No. 84
--IF @mAction= '22AFAFAAFF'

-- Option No. 85
IF @mAction= '22AFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 86
IF @mAction= '22AFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 87
IF @mAction= '22AFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 88
IF @mAction= '22AFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 89
IF @mAction= '22AFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 90
IF @mAction= '22AFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 91
--IF @mAction= '22AFAFFAFA'

-- Option No. 92
--IF @mAction= '22AFAFFAFF'

-- Option No. 93
IF @mAction= '22AFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 94
IF @mAction= '22AFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 95
IF @mAction= '22AFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 96
IF @mAction= '22AFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 97
IF @mAction= '22AFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 98
IF @mAction= '22AFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 99
--IF @mAction= '22AFFAAAFA'

-- Option No. 100
--IF @mAction= '22AFFAAAFF'

-- Option No. 101
IF @mAction= '22AFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 102
IF @mAction= '22AFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 103
IF @mAction= '22AFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 104
IF @mAction= '22AFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 105
IF @mAction= '22AFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 106
IF @mAction= '22AFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 107
--IF @mAction= '22AFFAFAFA'

-- Option No. 108
--IF @mAction= '22AFFAFAFF'

-- Option No. 109
IF @mAction= '22AFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 110
IF @mAction= '22AFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 111
IF @mAction= '22AFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 112
IF @mAction= '22AFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 113
IF @mAction= '22AFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 114
IF @mAction= '22AFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 115
--IF @mAction= '22AFFFAAFA'

-- Option No. 116
--IF @mAction= '22AFFFAAFF'

-- Option No. 117
IF @mAction= '22AFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Mat.MaterialSubTypeDescription = @GranuleType
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 118
IF @mAction= '22AFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 119
IF @mAction= '22AFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 120
IF @mAction= '22AFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 121
IF @mAction= '22AFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 122
IF @mAction= '22AFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 123
--IF @mAction= '22AFFFFAFA'

-- Option No. 124
--IF @mAction= '22AFFFFAFF'

-- Option No. 125
IF @mAction= '22AFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 126
IF @mAction= '22AFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 127
IF @mAction= '22AFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 128
IF @mAction= '22AFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 129
IF @mAction= '22FAAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 130
IF @mAction= '22FAAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 131
--IF @mAction= '22FAAAAAFA'

-- Option No. 132
--IF @mAction= '22FAAAAAFF'

-- Option No. 133
IF @mAction= '22FAAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 134
IF @mAction= '22FAAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				--And (SO.IsSampleOrder = @mIsSampleOrder)
					And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 135
IF @mAction= '22FAAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 136
IF @mAction= '22FAAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And (MAT.CodificationNew = @mProductType)
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 138
IF @mAction= '22FAAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 138
IF @mAction= '22FAAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 139
--IF @mAction= '22FAAAFAFA'

-- Option No. 140
--IF @mAction= '22FAAAFAFF'

-- Option No. 141
IF @mAction= '22FAAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 142
IF @mAction= '22FAAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 143
IF @mAction= '22FAAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
					And Mat.Description like '%' + @mDescription + '%'
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 144
IF @mAction= '22FAAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 145
IF @mAction= '22FAAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 146
IF @mAction= '22FAAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 147
--IF @mAction= '22FAAFAAFA'

-- Option No. 148
--IF @mAction= '22FAAFAAFF'

-- Option No. 149
IF @mAction= '22FAAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 150
IF @mAction= '22FAAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 151
IF @mAction= '22FAAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And MAT.MaterialCode = @mMaterialCode
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 152
IF @mAction= '22FAAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 153
IF @mAction= '22FAAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And MAT.MaterialCode = @mMaterialCode
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 154
IF @mAction= '22FAAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 155
--IF @mAction= '22FAAFFAFA'

-- Option No. 156
--IF @mAction= '22FAAFFAFF'

-- Option No. 157
IF @mAction= '22FAAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 158
IF @mAction= '22FAAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 159
IF @mAction= '22FAAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 160
IF @mAction= '22FAAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 161
IF @mAction= '22FAFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 162
IF @mAction= '22FAFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 163
--IF @mAction= '22FAFAAAFA'

-- Option No. 164
--IF @mAction= '22FAFAAAFF'

-- Option No. 165
IF @mAction= '22FAFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 166
IF @mAction= '22FAFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 167
IF @mAction= '22FAFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 168
IF @mAction= '22FAFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 169
IF @mAction= '22FAFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 170
IF @mAction= '22FAFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 171
--IF @mAction= '22FAFAFAFA'

-- Option No. 172
--IF @mAction= '22FAFAFAFF'

-- Option No. 173
IF @mAction= '22FAFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 174
IF @mAction= '22FAFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 175
IF @mAction= '22FAFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 176
IF @mAction= '22FAFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 177
IF @mAction= '22FAFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.ArticleMould = @mArticleMould
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 178
IF @mAction= '22FAFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 179
--IF @mAction= '22FAFFAAFA'

-- Option No. 180
--IF @mAction= '22FAFFAAFF'

-- Option No. 181
IF @mAction= '22FAFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.ArticleMould = @mArticleMould
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 182
IF @mAction= '22FAFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 183
IF @mAction= '22FAFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 184
IF @mAction= '22FAFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 185
IF @mAction= '22FAFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 186
IF @mAction= '22FAFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 187
--IF @mAction= '22FAFFFAFA'

-- Option No. 188
--IF @mAction= '22FAFFFAFF'

-- Option No. 189
IF @mAction= '22FAFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 190
IF @mAction= '22FAFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 191
IF @mAction= '22FAFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 192
IF @mAction= '22FAFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 193
IF @mAction= '22FFAAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 194
IF @mAction= '22FFAAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				And (MAT.CodificationNew = @mProductType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 195
--IF @mAction= '22FFAAAAFA'

-- Option No. 196
--IF @mAction= '22FFAAAAFF'

-- Option No. 197
IF @mAction= '22FFAAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 198
IF @mAction= '22FFAAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 199
IF @mAction= '22FFAAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And (SO.IsSampleOrder = @mIsSampleOrder) And (SOD.SampleType = @mSampleType)
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 200
IF @mAction= '22FFAAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 201
IF @mAction= '22FFAAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.Description like '%' + @mDescription + '%'
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 202
IF @mAction= '22FFAAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 203
--IF @mAction= '22FFAAFAFA'

-- Option No. 204
--IF @mAction= '22FFAAFAFF'

-- Option No. 205
IF @mAction= '22FFAAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 206
IF @mAction= '22FFAAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 207
IF @mAction= '22FFAAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 208
IF @mAction= '22FFAAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 209
IF @mAction= '22FFAFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And MAT.MaterialCode = @mMaterialCode
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 210
IF @mAction= '22FFAFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 211
--IF @mAction= '22FFAFAAFA'

-- Option No. 212
--IF @mAction= '22FFAFAAFF'

-- Option No. 213
IF @mAction= '22FFAFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
				-- And SO.IsSampleOrder = @mIsSampleOrder And MAT.MaterialCode = @mMaterialCode
				And Mat.MaterialSubTypeDescription = @GranuleType
				And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 214
IF @mAction= '22FFAFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 215
IF @mAction= '22FFAFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 216
IF @mAction= '22FFAFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 217
IF @mAction= '22FFAFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 218
IF @mAction= '22FFAFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 219
--IF @mAction= '22FFAFFAFA'

-- Option No. 220
--IF @mAction= '22FFAFFAFF'

-- Option No. 221
IF @mAction= '22FFAFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 222
IF @mAction= '22FFAFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 223
IF @mAction= '22FFAFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 224
IF @mAction= '22FFAFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 225
IF @mAction= '22FFFAAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
                         / SUM(INVDTL.quantity) AS Average
	FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
                         
                         
	WHERE        (Cast(INV.NoteDate As Date) >= @mFromDate) And (Cast(INV.NoteDate As Date) <= @mToDate) And MAT.ProductType = @mProductTypeMain
					And Mat.ArticleMould = @mArticleMould
					And Mat.MaterialSubTypeDescription = @GranuleType
					And Buy.BuyerName = @mBuyerName
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 226
IF @mAction= '22FFFAAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 227
--IF @mAction= '22FFFAAAFA'

-- Option No. 228
--IF @mAction= '22FFFAAAFF'

-- Option No. 229
IF @mAction= '22FFFAAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 230
IF @mAction= '22FFFAAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 231
IF @mAction= '22FFFAAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 232
IF @mAction= '22FFFAAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 233
IF @mAction= '22FFFAFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 234
IF @mAction= '22FFFAFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 235
--IF @mAction= '22FFFAFAFA'

-- Option No. 236
--IF @mAction= '22FFFAFAFF'

-- Option No. 237
IF @mAction= '22FFFAFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 238
IF @mAction= '22FFFAFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 239
IF @mAction= '22FFFAFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 240
IF @mAction= '22FFFAFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 241
IF @mAction= '22FFFFAAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 242
IF @mAction= '22FFFFAAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 243
--IF @mAction= '22FFFFAAFA'

-- Option No. 244
--IF @mAction= '22FFFFAAFF'

-- Option No. 245
IF @mAction= '22FFFFAFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 246
IF @mAction= '22FFFFAFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 247
IF @mAction= '22FFFFAFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 248
IF @mAction= '22FFFFAFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 249
IF @mAction= '22FFFFFAAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 250
IF @mAction= '22FFFFFAAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 251
--IF @mAction= '22FFFFFAFA'

-- Option No. 252
--IF @mAction= '22FFFFFAFF'

-- Option No. 253
IF @mAction= '22FFFFFFAA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 254
IF @mAction= '22FFFFFFAF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 255
IF @mAction= '22FFFFFFFA'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

-- Option No. 256
IF @mAction= '22FFFFFFFF'
BEGIN
	SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew As Type,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.Creditvalue) AS Value, SUM(INVDTL.Creditvalue) 
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
	GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew
	ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName
END

--SELECT        TOP (100) PERCENT Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew,SUM(INVDTL.quantity) AS InvDtlQty, SUM(INVDTL.value) AS Value, SUM(INVDTL.value) 
--                         / SUM(INVDTL.quantity) AS Average
--FROM             dbo.CreditNoteforRejRep AS INV INNER JOIN
--                        dbo.Buyer AS Buy ON INV.PartyName = Buy.BuyerCode INNER JOIN
--                         dbo.CreditNoteforRejRepDetails AS INVDTL ON INV.ID = INVDTL.HID INNER JOIN
--                         dbo.Materials AS MAT ON INVDTL.MaterialCode = MAT.MaterialCode
--                         
--                         dbo.SalesOrder ON SOD.SalesOrderID = dbo.SalesOrder.ID
--WHERE        (INV.NoteDate >= '2018-07-01')
--GROUP BY Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew, MAT.ArticleMould, MAT.CodificationNew
--ORDER BY MAT.ArticleMould, MAT.CodificationNew, Buy.BuyerName, MAT.ArticleMould, MAT.CodificationNew






GO
