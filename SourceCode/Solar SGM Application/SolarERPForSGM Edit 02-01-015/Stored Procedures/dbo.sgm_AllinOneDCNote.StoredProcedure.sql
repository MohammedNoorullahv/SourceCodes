USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOneDCNote]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_AllinOneDCNote

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOneDCNote]
@mAction						varchar(20)	='SELALL',
@mPKId							int			=Null,
@mFromDate						Datetime	=Null,
@mToDate						DateTime	=Null,
@mBuyerName						Varchar(150)	=Null,
@mDescription					Varchar(50)		=NULL,
@mArticleCode					Varchar(50)		=NULL,
@mArticleName					Varchar(50)		=Null,
@mOrderStatus					Varchar(50)		=Null,
@mTypeofOrder					Varchar(50)		=NULL,
@mIsSampleOrder					Bit				=Null

        
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int


--Option 01
If @mAction='S0AAAAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate

Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='S0AAAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'

Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='S0AAAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
	 and DCD.ArticleCode like '%'+@mArticleCode+'%'

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='S0AAAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and DCD.ArticleCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='S0AAASAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName

Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='S0AAASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
					  and Mat.Description like '%'+@mDescription+'%'
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='S0AAASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
					  and DCD.ArticleCode like '%'+@mArticleCode+'%'
					  
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='S0AAASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	DCDDtls.InvoiceNo,	DCDDtls.InvoiceDate,	DCD.ArticleCode As ArticleCode,					Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.OrderQuantity,DCDDtls.InvoiceValue,
	DCDDtls.CreditValue,	DCDDtls.CGSTValue + DCDDtls.SGSTValue + DCDDtls.IGSTValue As GSTValue,				DCDDtls.AdjustInvoiceAdjustValue,	DCDDtls.Rate

FROM
	CreditNoteforRejrep As DCN,		Buyer As Buy,		CreditNoteforRejRepSizeDtls As DCD,	Materials As Mat,		CreditNoteforRejrepDetails As DCDDtls
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.HId = DCN.ID						And DCD.ArticleCode = Mat.MaterialCode And
	DCDDtls.HID = DCN.ID						And DCDDtls.DCHdrId = dcd.ID				And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
					  and DCD.ArticleCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
Order by
	DCN.NoteDate

END

--Option 16 -- S0AAASSSD


GO
