USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_InvoiceDetailsSCSA]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_InvoiceDetailsSCSA

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_InvoiceDetailsSCSA]
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


-- Option : 001
IF @mAction='A-A'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
                  		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 002
IF @mAction='A-H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND (INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
 
UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
	Inv.InvoiceDate

END

-- Option : 003
IF @mAction='A-E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND BU.CountryName <> 'INDIA'
 		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')  AND  BU.CountryName <> 'INDIA'

	Order By
	Inv.InvoiceDate

END

-- Option : 004
IF @mAction='A-EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'H')  OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
 
UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')  AND  BU.CountryName <> 'INDIA'

	Order By
	Inv.InvoiceDate

END

-- Option : 005
IF @mAction='A-3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And INV.Accounted = 'CT3'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 006
IF @mAction='A-3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And (INV.Accounted = 'CT3' or INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 007
IF @mAction='A-3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And (INV.Accounted = 'CT3' OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G') AND  BU.CountryName <> 'INDIA'

	Order By
			Inv.InvoiceDate

END

-- Option : 008
IF @mAction='A-3EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And (INV.Accounted = 'CT3' OR  INV.Accounted = 'H' OR BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G') AND  BU.CountryName <> 'INDIA'

	Order By
			Inv.InvoiceDate

END

-- Option : 009
IF @mAction='A-C'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		 AND INV.Accounted = 'C'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 010	
	IF @mAction='A-CH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND (INV.Accounted = 'C' or  INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 011
IF @mAction='A-CE'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		 AND (INV.Accounted = 'C'  AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'

	Order By
			Inv.InvoiceDate

END

-- Option : 012
IF @mAction='A-CEH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'C' OR INV.Accounted ='H') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'

	Order By
			Inv.InvoiceDate

END

-- Option : 013
IF @mAction='A-C3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND (INV.Accounted = 'C'  OR INV.Accounted = 'CT3')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 014
IF @mAction='A-C3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3' OR INV.Accounted='H')) 
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate

END

-- Option : 015
IF @mAction='A-C3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'

	Order By
			Inv.InvoiceDate

END


-- Option : 016
IF @mAction='G-A'
BEGIN

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')

	Order By
	Inv.InvoiceDate

END

-- Option : 017

-- Option : 018	

-- Option : 019

-- Option : 020

-- Option : 021

-- Option : 022

-- Option : 023

-- Option : 024

-- Option : 025

-- Option : 026

-- Option : 027

-- Option : 028

-- Option : 029

-- Option : 030


-- Option : 031
IF @mAction='J-A'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By
			Inv.InvoiceDate

END

-- Option : 032
IF @mAction='J-H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J' AND (INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By

			Inv.InvoiceDate

END

-- Option : 033
IF @mAction='J-E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J' AND BU.CountryName <> 'INDIA'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By

			Inv.InvoiceDate

END

-- Option : 034
IF @mAction='J-EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J' AND ((INV.Accounted = 'H')  OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By

			Inv.InvoiceDate

END

-- Option : 035
IF @mAction='J-3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And INV.Accounted = 'CT3'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 036
IF @mAction='J-3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And (INV.Accounted = 'CT3' or INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	Order By

			Inv.InvoiceDate

END

-- Option : 037
IF @mAction='J-3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And (INV.Accounted = 'CT3' OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 038
IF @mAction='J-3EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And (INV.Accounted = 'CT3' OR  INV.Accounted = 'H' OR BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 039
IF @mAction='J-C'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		 AND INV.Accounted = 'C'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	Order By

			Inv.InvoiceDate

END

-- Option : 040	
	IF @mAction='J-CH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND (INV.Accounted = 'C' or  INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 041
IF @mAction='J-CE'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND (INV.Accounted = 'C'  AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 042
IF @mAction='J-CEH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND ((INV.Accounted = 'C' OR INV.Accounted ='H') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 043
IF @mAction='J-C3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
	AND (INV.Accounted = 'C'  OR INV.Accounted = 'CT3')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 044
IF @mAction='J-C3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
	AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3' OR INV.Accounted='H'))
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	
	Order By

			Inv.InvoiceDate

END

-- Option : 045
IF @mAction='J-C3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 046
IF @mAction='JG-A'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G')
	
	Order By
			Inv.InvoiceDate

END

-- Option : 047
IF @mAction='JG-H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J' AND (INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G')
	
	Order By

			Inv.InvoiceDate

END

-- Option : 048
IF @mAction='JG-E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J' AND BU.CountryName <> 'INDIA'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 049
IF @mAction='JG-EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J' AND ((INV.Accounted = 'H')  OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	Order By

			Inv.InvoiceDate

END

-- Option : 050
IF @mAction='JG-3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And INV.Accounted = 'CT3'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 		
	Order By

			Inv.InvoiceDate

END

-- Option : 051
IF @mAction='JG-3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And (INV.Accounted = 'CT3' or INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'

Order By
			Inv.InvoiceDate

END

-- Option : 052
IF @mAction='JG-3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And (INV.Accounted = 'CT3' OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 053
IF @mAction='JG-3EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And (INV.Accounted = 'CT3' OR  INV.Accounted = 'H' OR BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
			
	Order By

			Inv.InvoiceDate

END

-- Option : 054
IF @mAction='JG-C'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		 AND INV.Accounted = 'C'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName

	WHERE     (Inv.accounted = 'G')		 
	Order By

			Inv.InvoiceDate

END

-- Option : 055
	IF @mAction='JG-CH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND (INV.Accounted = 'C' or  INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 
	Order By

			Inv.InvoiceDate

END

-- Option : 056
IF @mAction='JG-CE'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND (INV.Accounted = 'C'  AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 057
IF @mAction='JG-CEH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND ((INV.Accounted = 'C' OR INV.Accounted ='H') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 058
IF @mAction='JG-C3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	AND (INV.Accounted = 'C'  OR INV.Accounted = 'CT3')
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 
	
	Order By

			Inv.InvoiceDate

END

-- Option : 059
IF @mAction='JG-C3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
	AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3' OR INV.Accounted='H'))
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 
	
	Order By

			Inv.InvoiceDate

END

-- Option : 060
IF @mAction='JG-C3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'J'
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END



-- Option : 061
IF @mAction='S-A'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By
			Inv.InvoiceDate

END

-- Option : 062
IF @mAction='S-H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By

			Inv.InvoiceDate

END

-- Option : 063
IF @mAction='S-E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S' AND BU.CountryName <> 'INDIA'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By

			Inv.InvoiceDate

END

-- Option : 064
IF @mAction='S-EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S' AND ((INV.Accounted = 'H')  OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By

			Inv.InvoiceDate

END

-- Option : 065
IF @mAction='S-3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And INV.Accounted = 'CT3'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 066
IF @mAction='S-3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And (INV.Accounted = 'CT3' or INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	Order By

			Inv.InvoiceDate

END

-- Option : 067
IF @mAction='S-3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And (INV.Accounted = 'CT3' OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 068
IF @mAction='S-3EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And (INV.Accounted = 'CT3' OR  INV.Accounted = 'H' OR BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 069
IF @mAction='S-C'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		 AND INV.Accounted = 'C'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	Order By

			Inv.InvoiceDate

END

-- Option : 070	
	IF @mAction='S-CH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND (INV.Accounted = 'C' or  INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 071
IF @mAction='S-CE'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND (INV.Accounted = 'C'  AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 072
IF @mAction='S-CEH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND ((INV.Accounted = 'C' OR INV.Accounted ='H') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 073
IF @mAction='S-C3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
	AND (INV.Accounted = 'C'  OR INV.Accounted = 'CT3')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 074
IF @mAction='S-C3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue


	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
	AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3' OR INV.Accounted='H'))
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	
	Order By

			Inv.InvoiceDate

END

-- Option : 075
IF @mAction='S-C3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By

			Inv.InvoiceDate

END

-- Option : 076
IF @mAction='SG-A'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G')
	
	Order By
			Inv.InvoiceDate

END

-- Option : 077
IF @mAction='SG-H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G')
	
	Order By

			Inv.InvoiceDate

END

-- Option : 078
IF @mAction='SG-E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S' AND BU.CountryName <> 'INDIA'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 079
IF @mAction='SG-EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S' AND ((INV.Accounted = 'H')  OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	Order By

			Inv.InvoiceDate

END

-- Option : 080
IF @mAction='SG-3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And INV.Accounted = 'CT3'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 		
	Order By

			Inv.InvoiceDate

END

-- Option : 081
IF @mAction='SG-3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And (INV.Accounted = 'CT3' or INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'

Order By
			Inv.InvoiceDate

END

-- Option : 082
IF @mAction='SG-3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And (INV.Accounted = 'CT3' OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
			And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 083
IF @mAction='SG-3EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		And (INV.Accounted = 'CT3' OR  INV.Accounted = 'H' OR BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
			
	Order By

			Inv.InvoiceDate

END

-- Option : 084
IF @mAction='SG-C'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		 AND INV.Accounted = 'C'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G')		 
	Order By

			Inv.InvoiceDate

END

-- Option : 085
	IF @mAction='SG-CH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND (INV.Accounted = 'C' or  INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 
	Order By

			Inv.InvoiceDate

END

-- Option : 086
IF @mAction='SG-CE'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND (INV.Accounted = 'C'  AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 087
IF @mAction='SG-CEH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND ((INV.Accounted = 'C' OR INV.Accounted ='H') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
	
	Order By

			Inv.InvoiceDate

END

-- Option : 088
IF @mAction='SG-C3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
	AND (INV.Accounted = 'C'  OR INV.Accounted = 'CT3')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 
	
	Order By

			Inv.InvoiceDate

END

-- Option : 089
IF @mAction='SG-C3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
	AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3' OR INV.Accounted='H'))
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	WHERE     (Inv.accounted = 'G') 
	
	Order By

			Inv.InvoiceDate

END

-- Option : 090
IF @mAction='SG-C3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Left(id.SalesOrderNo,1) = 'S'
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
		UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
					  Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
	WHERE     (Inv.accounted = 'G') AND BU.CountryName <> 'INDIA'
		And Bu.BuyerName = @mBuyerName And ID.ArticleNo = @mArticleName
	
	Order By

			Inv.InvoiceDate

END


-- Option : 001
IF @mAction='SJ-A'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

	Order By
			Inv.InvoiceDate

END

-- Option : 002
IF @mAction='SJ-H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, '' As ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND (INV.Accounted = 'H')
 		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

Order By
	Inv.InvoiceDate

END

-- Option : 003
IF @mAction='SJ-E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND BU.CountryName <> 'INDIA'
 		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

Order By
	Inv.InvoiceDate

END

-- Option : 004
IF @mAction='SJ-EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'H')  OR  BU.CountryName <> 'INDIA')
 		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName

Order By
	Inv.InvoiceDate

END

-- Option : 005
IF @mAction='SJ-3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And INV.Accounted = 'CT3'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By
			Inv.InvoiceDate

END

-- Option : 006
IF @mAction='SJ-3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And (INV.Accounted = 'CT3' or INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	Order By
			Inv.InvoiceDate

END

-- Option : 007
IF @mAction='SJ-3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And (INV.Accounted = 'CT3' OR  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	Order By
			Inv.InvoiceDate

END

-- Option : 008
IF @mAction='SJ-3EH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		And (INV.Accounted = 'CT3' OR  INV.Accounted = 'H' OR BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	Order By
			Inv.InvoiceDate

END

-- Option : 009
IF @mAction='SJ-C'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		 AND INV.Accounted = 'C'
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	Order By
			Inv.InvoiceDate

END

-- Option : 010	
	IF @mAction='SJ-CH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND (INV.Accounted = 'C' or  INV.Accounted = 'H')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	Order By
			Inv.InvoiceDate

END

-- Option : 011
IF @mAction='SJ-CE'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		 AND (INV.Accounted = 'C'  AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		 
	Order By
			Inv.InvoiceDate

END

-- Option : 012
IF @mAction='SJ-CEH'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'C' OR INV.Accounted ='H') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By
			Inv.InvoiceDate

END

-- Option : 013
IF @mAction='SJ-C3'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND (INV.Accounted = 'C'  OR INV.Accounted = 'CT3')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
		
	Order By
			Inv.InvoiceDate

END

-- Option : 014
IF @mAction='SJ-C3H'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3' OR INV.Accounted='H')) 
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	Order By
			Inv.InvoiceDate

END

-- Option : 015
IF @mAction='SJ-C3E'
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
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, MA.ArticleMould,
		Inv.SGSTPercentage,Inv.SGSTVlaue,Inv.CGSTPercentage,Inv.CGSTValue,Inv.IGSTPercentage,Inv.IGSTValue,Inv.GSTTotalValue

	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
		AND ((INV.Accounted = 'C' OR INV.accounted = 'CT3') AND  BU.CountryName <> 'INDIA')
		And Bu.BuyerName = @mBuyerName And MA.ArticleMould = @mArticleName
	
	Order By
			Inv.InvoiceDate

END




--------------------------------------------------- OLD CONDITIONS ---------------------------------------------------


GO
