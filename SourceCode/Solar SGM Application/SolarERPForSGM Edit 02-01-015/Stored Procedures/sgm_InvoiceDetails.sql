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

CREATE PROC sgm_InvoiceDetails
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



IF @mAction='SELALLINV'
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate

Order By
Inv.InvoiceDate

END

IF @mAction='J' -- Job Work Only
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
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
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3CE' -- Job + Sales - EOU, Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' or BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3CH' -- Job + Sales - EOU, Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' OR Inv.Accounted = 'H')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3E' -- Job + Sales - EOU & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-3H' -- Job + Sales - EOU & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' )

Order By
Inv.InvoiceDate


END

IF @mAction='JS-3HE' -- Job + Sales - EOU, Form H & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-A' -- Job + Sales - All
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate

Order By
Inv.InvoiceDate

END

IF @mAction='JS-C' -- Job + Sales - Form C
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' )

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CE' -- Job + Sales - Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'C' OR BU.CountryName <> 'INDIA' )

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CH' -- Job + Sales - Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR INV.Accounted = 'C')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-CHE' -- Job + Sales - Form C, Form H & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR INV.Accounted = 'C'  OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-E' -- Job + Sales - Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-H' -- Job + Sales - Form H
BEGIN


Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H')

Order By
Inv.InvoiceDate

END

IF @mAction='JS-HE' -- Job + Sales - Form H & Export
BEGIN


Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'J' OR Inv.Accounted = 'H' OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-3' -- Sales - EOU
BEGIN
SELECT * FROM INVOICE
END

IF @mAction='S-3C' -- Sales - EOU &  Form C
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C')

Order By
Inv.InvoiceDate

END

IF @mAction='S-3CE' -- Sales - EOU, Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C')  OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-3CH' -- Sales - EOU, Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C'  OR INV.Accounted = 'H'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-3E' -- Sales - EOU & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate


END

IF @mAction='S-3H' -- Sales - EOU & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And (Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-3HE' -- Sales - EOU, Form H & Export

BEGIN
Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'H') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-A' -- Sales - All
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
--And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'H') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-C' -- Sales - Form C
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-CE' -- Sales - Form C & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND INV.Accounted = 'C') OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-CH' -- Sales - Form C & Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR INV.Accounted = 'H')))

Order By
Inv.InvoiceDate

END

IF @mAction='S-CHE' -- Sales - Form C, Form H & Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'C' OR INV.Accounted = 'H')) OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate

END

IF @mAction='S-E' -- Sales - Export
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND  BU.CountryName <> 'INDIA'))

Order By
Inv.InvoiceDate

END

IF @mAction='S-H' -- Sales - Form H
BEGIN

Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')))

Order By
Inv.InvoiceDate

END

IF @mAction='S-HE' -- Sales - Form H & Export
BEGIN
Select
Inv.BuyerGroup,Inv.Buyer As BuyerCode,BU.BuyerName,BU.Address As BuyerAddress, co.BuyerName As ConsigneeName,co.Address As ConsigneeAdress,BU.City As City,BU.Pincode As Pincode,Inv.InvoiceNo,CONVERT(varchar(10), Inv.InvoiceDate, 103) As InvDate,
Left(id.SalesOrderNo,1) As InvType,Case WHEN Inv.Accounted='CT3' THEN'Y' ELSE'N' END AS CT3,Inv.Accounted,id.ArticleNo As Code,ma.Description As ArticleName,cmd.groupcolorname As Colour,
ma.Tannage As OldCodification,ma.CodificationNew, id.Quantity, Id.Rate, id.Value, inv.Excise As ExcisePercentage,Round(id.Value*Inv.Excise/100,2) As DWExciseDuty, 
inv.Cess As CessPercentage,Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) As DWCessAmount, inv.EduCess As EduCessPercentage,Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DWEduCessAmount,
Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2) As DutyPayable, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) As SubTotal, 
inv.CSTorVat, Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As CSTorVATAmount, 
Round(id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2),2) +
 Round((id.Value + Round(id.Value*Inv.Excise/100,2) + Round((id.Value*Inv.Excise/100)*Inv.CESS/100,2) + Round((id.Value*Inv.Excise/100)*Inv.EduCess/100,2)) * inv.CSTorVat/100,2) As InvAmount


From
Invoice Inv, InvoiceDetail ID,Materials Ma, ColorMasterDetail CMD,Buyer Bu,Buyer Co
where ID.InvoiceId = Inv.Id  And id.ArticleNo = ma.materialcode And ma.MaterialColor = cmd.GroupColorCode And Inv.Buyer = bu.BuyerCode And 
Inv.ConsigneeCode = co.BuyerCode
And Inv.Shipper = 'SSPL' and Inv.InvoiceDate >= @mFromDate And Inv.InvoiceDate <= @mToDate
And ((Left(id.SalesOrderNo,1) = 'S' AND (INV.Accounted = 'H')) OR BU.CountryName <> 'INDIA')

Order By
Inv.InvoiceDate
END


