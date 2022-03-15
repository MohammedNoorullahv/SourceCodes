USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOneDCNote104]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_AllinOneDCNote104

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOneDCNote104]
@mAction						varchar(20)	='SELALL',
@mPKId							int			=Null,
@mFromDate						Datetime	=Null,
@mToDate						DateTime	=Null,
@mBuyerName						Varchar(150)	=Null,
--@mSoleName						Varchar(500)	=Null,
--@mCodification					Varchar(50)		=Null,
--@mClient						varchar(150)	=NULL,
--@mCode							varchar(50)		=NULL,
--@mGender						varchar(50)		=NULL,
--@mSoleType						varchar(50)		=NULL,
--@mColour						varchar(50)		=NULL,
--@mGranules						varchar(100)	=NULL,
--@mNettWt						decimal(18, 2)	=NULL,
--@mLeatherSQM					varchar(150)	=NULL,
--@mSQMConsumption				decimal(18, 2)	=NULL,
--@mSQMDeclaredConsumption		decimal(18, 2)	=NULL,
--@mLeatherKGS					varchar(150)	=NULL,
--@mKGSConsumption				decimal(18, 2)	=NULL,
--@mKGSDeclaredConsumption		decimal(18, 2)	=NULL,
--@mDeclaredWt					decimal(18, 2)	=NULL,
--@mCodificationNew				varchar(50)		=NULL,
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
If @mAction='S0ASAAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='S0ASAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='S0ASAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='S0ASAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='S0ASASAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='S0ASASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
					  and Mat.Description like '%'+@mDescription+'%'
					  	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='S0ASASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
		And buy.BuyerName = @mBuyerName				  
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='S0ASASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.BuyerName,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode As ArticleCode,				Mat.MaterialName As ArticleName,
	Mat.MaterialColorDescription,				Mat.CodificationNew,	Mat.ArticleMould,		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Buyer As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.BuyerCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'CREDITNOTE'					And dcn.NoteDate >= @mFromDate					And DCN.NoteDate <= @mToDate
					  and Mat.ArticleMould = @mArticleName
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   	And buy.BuyerName = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 16 -- S0AAASSSD
GO
