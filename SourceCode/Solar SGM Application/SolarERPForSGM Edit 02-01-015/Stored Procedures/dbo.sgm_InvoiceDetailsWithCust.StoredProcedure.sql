USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_InvoiceDetailsWithCust]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_InvoiceDetailsWithCust

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_InvoiceDetailsWithCust]
@mAction			varchar(20)	='SELALL',
@mPKId				int		=Null,
@mFromDate			Datetime	=Null,
@mToDate			DateTime	=Null,
@mBuyerName			Varchar(150)	=Null,
@mSoleName			Varchar(500)	=Null,
@mCodification		Varchar(50)		=Null
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELALLINV-C'
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='J-C' -- Job Work Only
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And Left(id.SalesOrderNo,1) = 'J'  and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3-C' -- Job + Sales - EOU
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3CE-C' -- Job + Sales - EOU, Form C & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' or BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3CH-C' -- Job + Sales - EOU, Form C & Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' OR Inv.Accounted = 'H') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3E-C' -- Job + Sales - EOU & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3H-C' -- Job + Sales - EOU & Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' ) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate


END

IF @mAction='JS-3HE-C' -- Job + Sales - EOU, Form H & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-A-C' -- Job + Sales - All
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


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
Where Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate  and BU.BuyerName = @mBuyerName

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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' ) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CE-C' -- Job + Sales - Form C & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' OR BU.CountryName <> 'INDIA' ) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CH-C' -- Job + Sales - Form C & Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR INV.Accounted = 'C') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CHE-C' -- Job + Sales - Form C, Form H & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR INV.Accounted = 'C'  OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-E-C' -- Job + Sales - Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-H-C' -- Job + Sales - Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='JS-HE-C' -- Job + Sales - Form H & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-3-C' -- Sales - EOU
BEGIN
SELECT * FROM INVOICE 
END

IF @mAction='S-3C-C' -- Sales - EOU &  Form C
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-3CE-C' -- Sales - EOU, Form C & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C')  OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-3CH-C' -- Sales - EOU, Form C & Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C'  OR INV.Accounted = 'H')) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-3E-C' -- Sales - EOU & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate


END

IF @mAction='S-3H-C' -- Sales - EOU & Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-3HE-C' -- Sales - EOU, Form H & Export

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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'H') OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-A-C' -- Sales - All
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate  and BU.BuyerName = @mBuyerName
--And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'H') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-C-C' -- Sales - Form C
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C'))  and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-CE-C' -- Sales - Form C & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C') OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-CH-C' -- Sales - Form C & Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR INV.Accounted = 'H'))) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-CHE-C' -- Sales - Form C, Form H & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR INV.Accounted = 'H')) OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-E-C' -- Sales - Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND  BU.CountryName <> 'INDIA')) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-H-C' -- Sales - Form H
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H'))) and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate

END

IF @mAction='S-HE-C' -- Sales - Form H & Export
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
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Materials AS Ma ON ID.ArticleNo = Ma.MaterialCode LEFT OUTER JOIN
                      dbo.ColorMasterDetail AS CMD ON Ma.MaterialColor = CMD.GroupColorCode INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')) OR BU.CountryName <> 'INDIA') and BU.BuyerName = @mBuyerName

Order By
Inv.InvoiceDate
END

IF @mAction='G-C'
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
                      + ROUND(ID.value * Inv.Excise / 100 * Inv.CESS / 100, 2) + ROUND(ID.value * Inv.Excise / 100 * Inv.EduCess / 100, 2)) * Inv.CSTorVAT / 100, 2) AS InvAmount
FROM         dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail AS ID ON Inv.ID = ID.InvoiceID INNER JOIN
                      dbo.Buyer AS Bu ON Inv.Buyer = Bu.BuyerCode INNER JOIN
                      dbo.Buyer AS Co ON Inv.ConsigneeCode = Co.BuyerCode AND Inv.Shipper = 'SSPL' AND Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
                      and BU.BuyerName = @mBuyerName
WHERE     (Inv.accounted = 'G')
ORDER BY Inv.InvoiceDate
end

IF @mAction='LoadCust'
BEGIN

SELECT DISTINCT dbo.Buyer.BuyerName as Client
FROM         dbo.INVOICE INNER JOIN
                      dbo.InvoiceDetail ON dbo.INVOICE.ID = dbo.InvoiceDetail.InvoiceID LEFT OUTER JOIN
                      dbo.Buyer ON dbo.INVOICE.Buyer = dbo.Buyer.BuyerCode
WHERE     (dbo.INVOICE.Shipper = 'SSPL') and INVOICE.InvoiceDate >= @mFromDate And INVOICE.InvoiceDate <= @mToDate

ORDER BY  Client
END


IF @mAction='LoadArt'
BEGIN
SELECT DISTINCT TOP (100) PERCENT dbo.Materials.Description AS SoleName
FROM         dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor RIGHT OUTER JOIN
                      dbo.INVOICE AS Inv INNER JOIN
                      dbo.InvoiceDetail ON Inv.ID = dbo.InvoiceDetail.InvoiceID ON dbo.Materials.MaterialCode = dbo.InvoiceDetail.ArticleNo
WHERE     (Inv.Shipper = 'SSPL') And Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
ORDER BY  SoleName	
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

GO
