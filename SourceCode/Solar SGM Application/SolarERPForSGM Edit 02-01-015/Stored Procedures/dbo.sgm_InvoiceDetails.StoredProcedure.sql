USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_InvoiceDetails]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_InvoiceDetails

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_InvoiceDetails]
@mAction			varchar(20)	='SELALL',
@mPKId				int		=Null,
@mFromDate			Datetime	=Null,
@mToDate			DateTime	=Null,
@mBuyerName			Varchar(150)	=Null,
@mSoleName			Varchar(500)	=Null,
@mCodification		Varchar(50)		=Null,
@mBuyerGroup		varchar(50)		=NULL,
@mBuyerCode			varchar(50)		=NULL,
@mBuyerAddress		varchar(150)	=NULL,
@mConsigneeName		varchar(50)		=NULL,
@mConsigneeAdress	varchar(150)	=NULL,
@mCity				varchar(50)		=NULL,
@mPincode			varchar(50)		=NULL,
@mInvoiceNo			varchar(50)		=NULL,
@mInvDate			datetime		=NULL,
@mInvType			varchar(50)		=NULL,
@mCT3				varchar(50)		=NULL,
@mAccounted			varchar(50)		=NULL,
@mCode				varchar(50)		=NULL,
@mArticleName		varchar(150)	=NULL,
@mColour			varchar(50)		=NULL,
@mOldCodification	varchar(50)		=NULL,
@mCodificationNew	varchar(50)		=NULL,
@mQuantity			decimal(18, 2)  =NULL,
@mRate				decimal(18, 2)  =NULL,
@mValue				decimal(18, 2)  =NULL,
@mExcisePercentage	decimal(18, 2)  =NULL,
@mDWExciseDuty		decimal(18, 2)  =NULL,
@mCessPercentage	decimal(18, 2)  =NULL,
@mDWCessAmount		decimal(18, 2)  =NULL,
@mEduCessPercentage decimal(18, 2)  =NULL,
@mDWEduCessAmount	decimal(18, 2)  =NULL,
@mDutyPayable		decimal(18, 2)  =NULL,
@mSubTotal			decimal(18, 2)  =NULL,
@mCSTorVat			decimal(18, 2)  =NULL,
@mCSTorVATAmount	decimal(18, 2)  =NULL,
@mInvAmount			decimal(18, 2)  =NULL


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELALLINV'
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate

Order By
Inv.InvoiceDate

END

IF @mAction='J' -- Job Work Only
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And Left(id.SalesOrderNo,1) = 'J'

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3' -- Job + Sales - EOU
BEGIN
SELECT * FROM INVOICE -- Data Not available for Testing
END

IF @mAction='JS-3C' -- Job + Sales - EOU &  Form C
BEGIN
Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3CE' -- Job + Sales - EOU, Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' or BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3CH' -- Job + Sales - EOU, Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' OR Inv.Accounted = 'H')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3E' -- Job + Sales - EOU & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3H' -- Job + Sales - EOU & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' )

Order By
Inv.InvoiceDate


END

IF @mAction='JS-3HE' -- Job + Sales - EOU, Form H & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-A' -- Job + Sales - All
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,
--CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
REPLACE(CONVERT(Nvarchar(20), Inv.InvoiceDate, 106), ' ', '-') AS InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
--Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
--where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor *= cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
--Inv.ConsigneeCode = co.BuyerCode
--And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate

dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
Where Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate

Order By
Inv.InvoiceDate

END

IF @mAction='JS-C' -- Job + Sales - Form C
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' )

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CE' -- Job + Sales - Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' OR BU.CountryName <> 'INDIA' )

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CH' -- Job + Sales - Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR INV.Accounted = 'C')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CHE' -- Job + Sales - Form C, Form H & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR INV.Accounted = 'C'  OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-E' -- Job + Sales - Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-H' -- Job + Sales - Form H
BEGIN


Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-HE' -- Job + Sales - Form H & Export
BEGIN


Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-3' -- Sales - EOU
BEGIN
Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'CT3')

Order By
Inv.InvoiceDate
END

IF @mAction='S-3C' -- Sales - EOU &  Form C
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C'  OR Inv.Accounted = 'CT3'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-3CE' -- Sales - EOU, Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR BU.CountryName <> 'INDIA' OR Inv.Accounted = 'CT3'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-3CH' -- Sales - EOU, Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C'  OR INV.Accounted = 'H'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-3E' -- Sales - EOU & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate


END

IF @mAction='S-3H' -- Sales - EOU & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-3HE' -- Sales - EOU, Form H & Export

BEGIN
Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'H') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-A' -- Sales - All
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
--And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'H') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-C' -- Sales - Form C
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-CE' -- Sales - Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-CH' -- Sales - Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR INV.Accounted = 'H')))

Order By
Inv.InvoiceDate

END

IF @mAction='S-CHE' -- Sales - Form C, Form H & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR INV.Accounted = 'H')) OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-E' -- Sales - Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND  BU.CountryName <> 'INDIA'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-H' -- Sales - Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')))

Order By
Inv.InvoiceDate

END

IF @mAction='S-HE' -- Sales - Form H & Export
BEGIN
Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')) OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate
END

IF @mAction='G'
BEGIN

SELECT     TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 103) AS InvDate, 
                      'G' AS InvType, CASE WHEN Inv.Accounted = 'CT3' THEN 'Y' ELSE 'N' END AS CT3, Inv.accounted, ID.ArticleNo AS Code, '' AS Sole, 
                      '' AS ArticleName, '' AS Colour, '' AS OldCodification, '' AS CodificationNew, ID.quantity, ID.rate, ID.value, Inv.Excise AS ExcisePercentage, 
                      ROUND(ID.value * Inv.Excise / 100, 2) AS DWExciseDuty, Inv.CESS AS CessPercentage, ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) AS DWCessAmount, 
                      Inv.EduCess AS EduCessPercentage, ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2) AS DWEduCessAmount, ROUND(ID.value * Inv.Excise / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2) AS DutyPayable, 
                      ROUND(ID.value + ROUND(ID.value * Inv.Excise / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2), 2) AS SubTotal, Inv.CSTorVAT, ROUND((ID.value + ROUND(ID.value * Inv.Excise / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS CSTorVATAmount, 
                      ROUND(ID.value + ROUND(ID.value * Inv.Excise / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2), 2) + ROUND((ID.value + ROUND(ID.value * Inv.Excise / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate
FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL' AND Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
WHERE     (Inv.accounted = 'G')
ORDER BY Inv.InvoiceDate

END

IF @mAction='J-G'
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And Left(id.SalesOrderNo,1) = 'J'

UNION ALL

SELECT     TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 103) AS InvDate, 
                      'G' AS InvType, CASE WHEN Inv.Accounted = 'CT3' THEN 'Y' ELSE 'N' END AS CT3, Inv.accounted, ID.ArticleNo AS Code, '' AS Sole, 
                      '' AS ArticleName, '' AS Colour, '' AS OldCodification, '' AS CodificationNew, ID.quantity, ID.rate, ID.value, Inv.Excise AS ExcisePercentage, 
                      ROUND(ID.value * Inv.Excise / 100, 2) AS DWExciseDuty, Inv.CESS AS CessPercentage, ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) AS DWCessAmount, 
                      Inv.EduCess AS EduCessPercentage, ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2) AS DWEduCessAmount, ROUND(ID.value * Inv.Excise / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2) AS DutyPayable, 
                      ROUND(ID.value + ROUND(ID.value * Inv.Excise / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2), 2) AS SubTotal, Inv.CSTorVAT, ROUND((ID.value + ROUND(ID.value * Inv.Excise / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS CSTorVATAmount, 
                      ROUND(ID.value + ROUND(ID.value * Inv.Excise / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2), 2) + ROUND((ID.value + ROUND(ID.value * Inv.Excise / 100, 2) 
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate
FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL' AND Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
WHERE     (Inv.accounted = 'G')

--ORDER BY Inv.InvoiceDate

END

IF @mAction='LoadCust'
BEGIN

Select ' ALL CUSTOMERS' AS BUYERNAME
UNION
SELECT DISTINCT dbo.Buyer.BuyerName As Client
FROM         dbo.INVOICE INNER JOIN
                      dbo.InvoiceDetail ON dbo.INVOICE.ID = dbo.InvoiceDetail.InvoiceID LEFT OUTER JOIN
                      dbo.Buyer ON dbo.INVOICE.Buyer = dbo.Buyer.BuyerCode LEFT OUTER JOIN
                      dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode ON 
                      dbo.InvoiceDetail.ArticleNo = dbo.Materials.MaterialCode
WHERE     (dbo.INVOICE.Shipper = 'SSPL') and INVOICE.InvoiceDate >= @mFromDate And INVOICE.InvoiceDate <= @mToDate

ORDER BY  BuyerName	
END


IF @mAction='LoadArt'
BEGIN
Select ' ALL ARTICLES' AS SoleName
UNION

--SELECT DISTINCT 
--                      TOP (100) PERCENT dbo.Materials.ArticleMould AS SoleName
--FROM         dbo.ColorMaster INNER JOIN
--                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
--                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode INNER JOIN
--                      dbo.SalesOrder INNER JOIN
--                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
--                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
--WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
--		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate And

SELECT DISTINCT TOP (100) PERCENT dbo.Materials.ArticleMould  As SoleName
FROM         dbo.InvoiceDetail INNER JOIN
                      dbo.Materials ON dbo.InvoiceDetail.ArticleNo = dbo.Materials.MaterialCode
WHERE     (dbo.InvoiceDetail.shipper = 'SSPL') AND (dbo.InvoiceDetail.InvoiceDate >= @mFromDate AND dbo.InvoiceDetail.InvoiceDate <= @mToDate)
		And dbo.InvoiceDetail.Buyer in
		(Select BuyerCode From SalesOrder Where BuyerName = @mBuyerName)
		 

ORDER BY SoleName		 
--ORDER BY  SoleName	
END

IF @mAction='LoadALLArt'
BEGIN
Select ' ALL ARTICLES' AS SoleName
UNION

--Select Distinct FullName_ As SoleName from AbbrevTable where Abbrev_ in

--SELECT DISTINCT 
--                      TOP (100) PERCENT dbo.Materials.ArticleMould AS SoleName
--FROM         dbo.ColorMaster INNER JOIN
--                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
--                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode INNER JOIN
--                      dbo.SalesOrder INNER JOIN
--                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
--                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
--WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
--		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate
SELECT DISTINCT TOP (100) PERCENT dbo.Materials.ArticleMould  As SoleName
FROM         dbo.InvoiceDetail INNER JOIN
                      dbo.Materials ON dbo.InvoiceDetail.ArticleNo = dbo.Materials.MaterialCode
WHERE     (dbo.InvoiceDetail.shipper = 'SSPL') AND (dbo.InvoiceDetail.InvoiceDate >= @mFromDate AND dbo.InvoiceDetail.InvoiceDate <= @mToDate)


ORDER BY SoleName		 
		 

END

IF @mAction='LoadCode'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.Materials.CodificationNew
FROM         dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate And
		 dbo.SalesOrder.BuyerName = @mBuyerName 
ORDER BY  CodificationNew	
END


IF @mAction='DELINV'
BEGIN
	DELETE FROM
	SolarInvoice4SGM4Print

END
	


IF @mAction='INSINV'
BEGIN
	INSERT INTO
	SolarInvoice4SGM4Print
	
	VALUES
	(@mBuyerGroup,			@mBuyerCode,			@mBuyerName,			@mBuyerAddress,				@mConsigneeName, 
	@mConsigneeAdress,		@mCity,					@mPincode,				@mInvoiceNo,				@mInvDate, 
	@mInvType,				@mCT3,					@mAccounted,			@mCode,						@mArticleName, 
	@mColour,				@mOldCodification,		@mCodificationNew,		@mQuantity,					@mRate,
	@mValue,				@mExcisePercentage,		@mDWExciseDuty,			@mCessPercentage,			@mDWCessAmount,
	@mEduCessPercentage,	@mDWEduCessAmount,		@mDutyPayable,			@mSubTotal,					@mCSTorVat,
	@mCSTorVATAmount,		@mInvAmount)

END

GO
