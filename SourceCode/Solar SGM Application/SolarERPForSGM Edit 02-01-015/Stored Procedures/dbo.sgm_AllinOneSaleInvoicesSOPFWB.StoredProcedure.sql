USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOneSaleInvoicesSOPFWB]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







-- drop Proc sgm_AllinOneSaleInvoicesSOPFWB
	
--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOneSaleInvoicesSOPFWB]
@mAction						varchar(20)	='SELALL',
@mPKId							int			=Null,
@mFromDate						Datetime	=Null,
@mToDate						DateTime	=Null,
@mBuyerName						Varchar(150)	=Null,
@mSoleName						Varchar(500)	=Null,
@mCodification					Varchar(50)		=Null,
@mClient						varchar(150)	=NULL,
@mCode							varchar(50)		=NULL,
@mGender						varchar(50)		=NULL,
@mSoleType						varchar(50)		=NULL,
@mColour						varchar(50)		=NULL,
@mGranules						varchar(100)	=NULL,
@mNettWt						decimal(18, 2)	=NULL,
@mLeatherSQM					varchar(150)	=NULL,
@mSQMConsumption				decimal(18, 2)	=NULL,
@mSQMDeclaredConsumption		decimal(18, 2)	=NULL,
@mLeatherKGS					varchar(150)	=NULL,
@mKGSConsumption				decimal(18, 2)	=NULL,
@mKGSDeclaredConsumption		decimal(18, 2)	=NULL,
@mDeclaredWt					decimal(18, 2)	=NULL,
@mCodificationNew				varchar(50)		=NULL,
@mDescription					Varchar(50)		=NULL,
@mArticleCode					Varchar(50)		=NULL,
@mArticleName					Varchar(50)		=Null,
@mOrderStatus					Varchar(50)		=Null,
@mTypeofOrder					Varchar(50)		=NULL,
@mIsSample						Bit				=Null,
@mSampleOrderType				Varchar(50)		=Null,
@mBrand							Varchar(50)		=Null
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SIAAAAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
		WHERE SOD.Brand = @mBrand
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) AS InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo

	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE       (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIAAAAAAD

-- OPTION 03
If @mAction='SIAAAAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo

	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mDescription+'%'

	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIAAAAASD

-- OPTION 05
If @mAction='SIAAAASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mArticleCode+'%'

	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIAAAASAD

-- OPTION 07
If @mAction='SIAAAASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ( ID.ArticleNo like '%'+@mArticleCode+'%'
				or ID.ArticleNo like '%'+@mDescription+'%' )
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIAAAASSD

-- OPTION 09
If @mAction='SIAAASAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIAAASAAD

-- OPTION 11
If @mAction='SIAAASASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIAAASASD

-- OPTION 13
If @mAction='SIAAASSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIAAASSAD

-- OPTION 13
If @mAction='SIAAASSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIAAASSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SIAAFAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	
	WHERE SOD.Brand = @mBrand And 	ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIAAFAAAD

-- OPTION 03
If @mAction='SIAAFAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIAAFAASD

-- OPTION 05
If @mAction='SIAAFASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIAAFASAD

-- OPTION 07
If @mAction='SIAAFASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIAAFASSD

-- OPTION 09
If @mAction='SIAAFSAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
				
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIAAFSAAD

-- OPTION 11
If @mAction='SIAAFSASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIAAFSASD

-- OPTION 13
If @mAction='SIAAFSSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIAAFSSAD

-- OPTION 13
If @mAction='SIAAFSSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIAAFSSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]


--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }-- 03 --{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SIASAAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
		And BU.BuyerName = @mBuyerName
	where sod.brand = @mBrand

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G')
				And BU.BuyerName = @mBuyerName
	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIASAAAAD

-- OPTION 03
If @mAction='SIASAAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName	
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName
				
	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIASAAASD

-- OPTION 05
If @mAction='SIASAASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
				And BU.BuyerName = @mBuyerName
					
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mArticleCode+'%'
				And BU.BuyerName = @mBuyerName
				
	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIASAASAD

-- OPTION 07
If @mAction='SIASAASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName
					
					
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ( ID.ArticleNo like '%'+@mArticleCode+'%'
				or ID.ArticleNo like '%'+@mDescription+'%' )
				And BU.BuyerName = @mBuyerName				
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIASAASSD

-- OPTION 09
If @mAction='SIASASAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIASASAAD

-- OPTION 11
If @mAction='SIASASASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName			
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIASASASD

-- OPTION 13
If @mAction='SIASASSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
				And BU.BuyerName = @mBuyerName			
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIASASSAD

-- OPTION 13
If @mAction='SIASASSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName			
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIASASSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SIASFAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	
	WHERE SOD.Brand = @mBrand And 	ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName			
	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIASFAAAD

-- OPTION 03
If @mAction='SIASFAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName			
	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIASFAASD

-- OPTION 05
If @mAction='SIASFASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIASFASAD

-- OPTION 07
If @mAction='SIASFASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIASFASSD

-- OPTION 09
If @mAction='SIASFSAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIASFSAAD

-- OPTION 11
If @mAction='SIASFSASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIASFSASD

-- OPTION 13
If @mAction='SIASFSSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIASFSSAD

-- OPTION 13
If @mAction='SIASFSSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIASFSSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]

--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }-- 03 --{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }

--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]-- 04 --[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SISAAAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And  Inv.WareHouse = @mTypeofOrder
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G')

	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIAAAAAAD

-- OPTION 03
If @mAction='SISAAAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
			And Inv.WareHouse = @mTypeofOrder			
			
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mDescription+'%'

	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIAAAAASD

-- OPTION 05
If @mAction='SISAAASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
			And Inv.WareHouse = @mTypeofOrder			
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mArticleCode+'%'

	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIAAAASAD

-- OPTION 07
If @mAction='SISAAASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			And Inv.WareHouse = @mTypeofOrder			
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ( ID.ArticleNo like '%'+@mArticleCode+'%'
				or ID.ArticleNo like '%'+@mDescription+'%' )
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIAAAASSD

-- OPTION 09
If @mAction='SISAASAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIAAASAAD

-- OPTION 11
If @mAction='SISAASASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIAAASASD

-- OPTION 13
If @mAction='SISAASSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIAAASSAD

-- OPTION 13
If @mAction='SISAASSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIAAASSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SISAFAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	
	WHERE SOD.Brand = @mBrand And 	ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIAAFAAAD

-- OPTION 03
If @mAction='SISAFAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIAAFAASD

-- OPTION 05
If @mAction='SISAFASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIAAFASAD

-- OPTION 07
If @mAction='SISAFASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIAAFASSD

-- OPTION 09
If @mAction='SISAFSAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIAAFSAAD

-- OPTION 11
If @mAction='SISAFSASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIAAFSASD

-- OPTION 13
If @mAction='SISAFSSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIAAFSSAD

-- OPTION 13
If @mAction='SISAFSSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
			And Inv.WareHouse = @mTypeofOrder			
			
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIAAFSSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]


--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }-- 03 --{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SISSAAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
		And BU.BuyerName = @mBuyerName
	
	WHERE SOD.Brand = @mBrand And  			Inv.WareHouse = @mTypeofOrder			

	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G')
				And BU.BuyerName = @mBuyerName
	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIASAAAAD

-- OPTION 03
If @mAction='SISSAAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName	
			And Inv.WareHouse = @mTypeofOrder			
	
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
				
	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIASAAASD

-- OPTION 05
If @mAction='SISSAASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
					
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ID.ArticleNo like '%'+@mArticleCode+'%'
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
				
	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIASAASAD

-- OPTION 07
If @mAction='SISSAASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
					
					
	UNION  ALL 

	SELECT    TOP (100) PERCENT Inv.BuyerGroup, Inv.Buyer AS BuyerCode, Bu.BuyerName, Bu.Address AS BuyerAddress, Co.BuyerName AS ConsigneeName, 
                      Co.Address AS ConsigneeAdress, Bu.city AS City, Bu.PinCode AS Pincode, Inv.InvoiceNo, CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate, 
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
					  Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
	FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL'
                       and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	WHERE SOD.Brand = @mBrand And      (Inv.accounted = 'G') 
				And ( ID.ArticleNo like '%'+@mArticleCode+'%'
				or ID.ArticleNo like '%'+@mDescription+'%' )
				And BU.BuyerName = @mBuyerName				
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIASAASSD

-- OPTION 09
If @mAction='SISSASAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIASASAAD

-- OPTION 11
If @mAction='SISSASASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName			
			And Inv.WareHouse = @mTypeofOrder			

	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIASASASD

-- OPTION 13
If @mAction='SISSASSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
				And BU.BuyerName = @mBuyerName			
			And Inv.WareHouse = @mTypeofOrder			

	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIASASSAD

-- OPTION 13
If @mAction='SISSASSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
				And BU.BuyerName = @mBuyerName			
			And Inv.WareHouse = @mTypeofOrder			

	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIASASSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]

--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--: 
-- OPTION 01
If @mAction='SISSFAAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID
	
	WHERE SOD.Brand = @mBrand And 	ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName			
			And Inv.WareHouse = @mTypeofOrder			

	Order By
			Inv.InvoiceDate
END

-- OPTION 02 - SIASFAAAD

-- OPTION 03
If @mAction='SISSFAASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName			
			And Inv.WareHouse = @mTypeofOrder			

	Order By
			Inv.InvoiceDate
END

-- OPTION 04 - SIASFAASD

-- OPTION 05
If @mAction='SISSFASAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And  id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 06 - SIASFASAD

-- OPTION 07
If @mAction='SISSFASSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 08 - SIASFASSD

-- OPTION 09
If @mAction='SISSFSAAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 10 - SIASFSAAD

-- OPTION 11
If @mAction='SISSFSASS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 12 - SIASFSASD

-- OPTION 13
If @mAction='SISSFSSAS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 14 - SIASFSSAD

-- OPTION 13
If @mAction='SISSFSSSS'
BEGIN
Select
		Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 102) As InvDate,
		Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.MaterialName As Sole,ma.Description As ArticleName,cmd.groupcolorname As Colour,
		ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
		IsNull(inv.Cess,0) As CessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) As DWCessAmount, IsNull(inv.EduCess,0) As EduCessPercentage,Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DWEduCessAmount,
		Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2) As DutyPayable, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) As SubTotal, 
		inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
		Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2),2) +
		Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.Cess,0)/100,2) + Round((id.Value*Inv.Excise/100)*IsNull(inv.EduCess,0)/100,2)) * inv.CSTorVat/100,2) As InvAmount, Id.Currency, id.CurrencyConversionRate As ExchangeRate,Inv.InvoiceDate, Ma.ArticleMould,
		Inv.SGSTPercentage,Round(id.Value*Inv.SGSTPercentage/100,2) As SGSTVlaue,Inv.CGSTPercentage,Round(id.Value*Inv.CGSTPercentage/100,2) As CGSTVlaue,Inv.IGSTPercentage,Round(id.Value*Inv.IGSTPercentage/100,2) As IGSTVlaue,Round(id.Value*Inv.SGSTPercentage/100,2) + Round(id.Value*Inv.CGSTPercentage/100,2) + Round(id.Value*Inv.IGSTPercentage/100,2) GSTTotalValue, Bu.GSTNo
		
	From
		dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
		And Inv.Shipper = 'SSPL' and Cast(Inv.InvoiceDate As Date) >= @mFromDate And Cast(Inv.InvoiceDate As Date) <= @mToDate And ID.IsSampleOrder = @mIsSample And ID.SampleOrderType = @mSampleOrderType  INNER JOIN dbo.SalesOrderDetails AS SOD ON ID.SalesOrderDetailID = SOD.ID

	WHERE SOD.Brand = @mBrand And 	Ma.ArticleMould = @mArticleName
			And id.ArticleNo like '%'+@mArticleCode+'%'
			And ma.Description like '%'+@mDescription+'%'
			AND ID.JobcardDetailID in (
			Select ID From JobcardDetail WHERE SOD.Brand = @mBrand And  SalesOrderDetailId in (
			Select ID From SalesOrderDetails WHERE SOD.Brand = @mBrand And  OrderStatus = @mOrderStatus AND SalesOrderNo = ID.SalesOrderNo))
				And BU.BuyerName = @mBuyerName
			And Inv.WareHouse = @mTypeofOrder			
							
	Order By
			Inv.InvoiceDate
END

-- OPTION 16 - SIASFSSSD
--:--:--:--:--:--:--:--:--:--:--:--:--:--:--:-- 01 --:--:--:--:--:--:--:--:--:--:--:--:--:--:--:

--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]-- 02 --[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]--[ ]

--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }-- 03 --{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }--{ * }

--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]-- 04 --[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]--[ X ]








GO
