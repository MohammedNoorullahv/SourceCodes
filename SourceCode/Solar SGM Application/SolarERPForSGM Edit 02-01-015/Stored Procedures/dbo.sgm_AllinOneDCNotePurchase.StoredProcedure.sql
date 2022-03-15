USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOneDCNotePurchase]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_AllinOneDCNotePurchase

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOneDCNotePurchase]
@mAction						varchar(20)	='SELALL',
@mPKId							int			=Null,
@mFromDate						Datetime	=Null,
@mToDate						DateTime	=Null,
@mpartyname						Varchar(150)	=Null,
@mDescription					Varchar(50)		=NULL,
@mArticleCode					Varchar(50)		=NULL,
@mArticleName					Varchar(50)		=Null,
@mOrderStatus					Varchar(50)		=Null,
@mTypeofOrder					Varchar(50)		=NULL,
@mIsSampleOrder					Bit				=Null,
@mMaterialType					Varchar(50)		=Null,
@mMaterialSubType				Varchar(50)		=Null,
@mBuyerName						Varchar(200)	=Null

        
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int


--Option 01
If @mAction='P0AAAAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate

Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0AAAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'

Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0AAAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0AAAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0AAASAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType

Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0AAASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0AAASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0AAASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
Order by
	DCN.NoteDate

END

--Option 16 -- P0AAFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------


----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
--Option 01
If @mAction='P0ASAAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0ASAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0ASAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	 AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0ASAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0ASASAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0ASASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0ASASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0ASASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 16 -- P0ASASSSD
----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
If @mAction='P0ASFAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And pod.PurchaseOrderStatus = @mOrderStatus
	AND  Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0ASFAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 AND  Buy.partyname = @mBuyerName
	 And POD.ID = DCD.PODetailId 
	And pod.PurchaseOrderStatus = @mOrderStatus
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0ASFASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	 AND  Buy.partyname = @mBuyerName
	 And POD.ID = DCD.PODetailId 
	 And pod.PurchaseOrderStatus = @mOrderStatus

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0ASFASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  AND  Buy.partyname = @mBuyerName
					  And POD.ID = DCD.PODetailId 
	 And pod.PurchaseOrderStatus = @mOrderStatus

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0ASFSAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					AND  Buy.partyname = @mBuyerName
					And POD.ID = DCD.PODetailId 
	 And pod.PurchaseOrderStatus = @mOrderStatus
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0ASFSASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  AND  Buy.partyname = @mBuyerName
					  And POD.ID = DCD.PODetailId 
	 And pod.PurchaseOrderStatus = @mOrderStatus
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0ASFSSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  AND  Buy.partyname = @mBuyerName
					  And POD.ID = DCD.PODetailId 
	 And pod.PurchaseOrderStatus = @mOrderStatus
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0ASFSSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   AND  Buy.partyname = @mBuyerName
					   And POD.ID = DCD.PODetailId 
	 And pod.PurchaseOrderStatus = @mOrderStatus
Order by
	DCN.NoteDate

END

--Option 16 -- P0ASASSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------
----------------------------------------------------------------  02(01)  ----------------------------------------------------------------




----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SAAAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0SAAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0SAAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	 And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0SAAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0SAASAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0SAASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0SAASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0SAASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 16 -- P0ASASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SAFAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0SAFAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0SAFASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	 And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0SAFASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0SAFSAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0SAFSASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0SAFSSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0SAFSSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END


--Option 16 -- P0ASASSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------


----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SFAAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0SFAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0SFAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	 And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0SFAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0SFASAAS	'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0SFASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0SFASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0SFASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 AND Buy.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END


--Option 16 -- P0ASASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SFFAAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0SFFAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0SFFASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and DCD.MaterialCode like '%'+@mArticleCode+'%'
	 And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0SFFASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0SFFSAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0SFFSASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0SFFSSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0SFFSSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	 And POD.PurchaseOrderStatus = @mOrderStatus
	AND BUY.partyname = @mBuyerName
	And PO.POSubtype = @mTypeofOrder
Order by
	DCN.NoteDate

END


--Option 16 -- P0ASASSSD
----------------------------------------------------------------  02  ----------------------------------------------------------------
----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------




----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SSAAAAS'

BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0SSAAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0SSAASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	and DCD.MaterialCode like '%'+@mArticleCode+'%'
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0SSAASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0SSASAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0SSASASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0SSASSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0SSASSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And PO.POSubtype = @mTypeofOrder
	AND BUY.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 16 -- P0ASASSSD
----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SSFAAAS'

BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0SSFAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0SSFASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	and DCD.MaterialCode like '%'+@mArticleCode+'%'
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0SSFASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName

Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0SSFSAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0SSFSASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0SSFSSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0SSFSSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END
--------------
----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0AAFAAAS'

BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,		Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue


FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'						And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	
Order by
	DCN.NoteDate
END

--Option 02 -- S0AAAAAAD

--Option 03
If @mAction='P0AAFAASS'
BEGIN
SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	 and Mat.Description like '%'+@mDescription+'%'
	 And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus

Order by
	DCN.NoteDate
END

--Option 04 -- S0AAAAASD

--Option 05
If @mAction='P0AAFASAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
	and DCD.MaterialCode like '%'+@mArticleCode+'%'
	And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	

Order by
	DCN.NoteDate

END

--Option 06 -- S0AAAASAD

--Option 07
If @mAction='P0AAFASSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus


Order by
	DCN.NoteDate

END

--Option 08 -- S0AAAASSD

--Option 09
If @mAction='P0AAFSAAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus

Order by
	DCN.NoteDate

END

--Option 10 -- S0AAASAAD

--Option 11
If @mAction='P0AAFSASS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and Mat.Description like '%'+@mDescription+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus

Order by
	DCN.NoteDate

END

--Option 12 -- S0AAASASD

--Option 13
If @mAction='P0AAFSSAS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					  And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus

Order by
	DCN.NoteDate

END

--Option 14 -- S0AAASSAD

--Option 15
If @mAction='P0AAFSSSS'
BEGIN

SELECT
	DCN.ID,				DCN.NoteDate,			dcn.PartyName,			buy.partyname,			DCN.NoteNumber,			dcn.Reason,
	dcd.InvoiceNo,		DCD.InvoiceDate,		DCD.MaterialCode,				Mat.Description As MaterialDescription,
	Mat.MaterialColorDescription,				Mat.MaterialTypeDescription As [Type],			Mat.MaterialSubTypeDescription As [Sub Type],		DCD.Quantity,DCD.InvoiceValue,
	dcd.CreditValue,	DCD.CGSTValue + DCD.SGSTValue + DCD.IGSTValue As GSTValue,				DCD.AdjustInvoiceAdjustValue

FROM
	DebitCreditNote As DCN,		Party As Buy,		DebitCreditNoteDetails As DCD,	Materials As Mat, PurchaseOrderDetails As POD, PurchaseOrder As PO
Where 
	BUY.PartyCode = DCN.PartyName				And DCD.DCHdrId = DCN.ID						And DCD.MaterialCode = Mat.MaterialCode And
	DCN.NoteType = 'DEBIT'					And Cast(DCN.NoteDate As Date) >= @mFromDate					And Cast(DCN.NoteDate As Date) <= @mToDate
					  AND Mat.MaterialTypeDescription = @mMaterialType
					AND Mat.MaterialSubTypeDescription = @mMaterialSubType
					  and DCD.MaterialCode like '%'+@mArticleCode+'%'
					   and Mat.Description like '%'+@mDescription+'%'
					   And POD.ID = DCD.PODetailId 
	 And DCD.PurchaseOrderNo = PO.PurchaseOrderNo
	And POD.PurchaseOrderStatus = @mOrderStatus
	And PO.POSubtype = @mTypeofOrder
	AND Buy.partyname = @mBuyerName
Order by
	DCN.NoteDate

END
GO
