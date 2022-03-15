USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_WastageDetails]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- drop Proc sgm_WastageDetails

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROCEDURE [dbo].[sgm_WastageDetails]
@mAction						varchar(20)	='SELALL',
@mPKId							int			=Null,
@mFromDate						Datetime	=Null,
@mToDate						Datetime	=Null,
@mType							Varchar(50)	=Null,
@mTypeofMaterial				Varchar(50)	=Null,
@mMaterialSubTypeDescription	Varchar(50) =Null,
@mROStatus						Varchar(20)	=Null

AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELALL'
BEGIN
Select
 wd.MaterialSubTypeDesc As TypeofMaterial,wd.MaterialUnit As UOM,IsNull(Sum(WD.InTotalWQtyInKGS),0) As InQty,IsNull(Sum(WD.TotalWQtyInKGS),0) As OutQty

From
SolarProductionWastageDetail As WD,SolarProductionWastage WH

Where
WD.HID = WH.ID And wd.MaterialType = 'GRA'
And Cast(WH.PWDate As Date) >= @mFromDate And Cast(WH.PWDate As Date) <= @mToDate

Group By
wd.MaterialSubTypeDesc, wd.MaterialUnit

Order by
wd.MaterialSubTypeDesc

END

IF @mAction='SELFILTER'
BEGIN
Select
 wd.MaterialSubTypeDesc As TypeofMaterial,wd.MaterialUnit As UOM,IsNull(Sum(WD.InTotalWQtyInKGS),0) As InQty,IsNull(Sum(WD.TotalWQtyInKGS),0) As OutQty

From
SolarProductionWastageDetail As WD,SolarProductionWastage WH

Where
WD.HID = WH.ID And wd.MaterialType = 'GRA'
And wd.TransactionType = @mType
And Cast(WH.PWDate As Date) >= @mFromDate And Cast(WH.PWDate As Date) <= @mToDate

Group By
wd.MaterialSubTypeDesc, wd.MaterialUnit

Order by
wd.MaterialSubTypeDesc

END


IF @mAction='SELALLDTLS'
BEGIN

SELECT        TOP (100) PERCENT WH.PWDate AS Date, WH.JobcardNo, WH.BuyerName, WH.ArticleName, WH.ArticelColor, WD.MaterialCode, WD.Description, 
                         WD.MaterialSubTypeDesc AS TypeofMaterial, WD.MaterialUnit AS UOM, WD.InTotalWQtyInKGS AS InQty, WD.TotalWQtyInKGS AS OutQty
FROM            dbo.SolarProductionWastageDetail AS WD INNER JOIN
                         dbo.SolarProductionWastage AS WH ON WD.HID = WH.ID
WHERE        (WD.MaterialType = 'GRA') AND (Cast(WH.PWDate As Date) >= @mFromDate) AND (Cast(WH.PWDate As Date) <= @mToDate)
AND (WD.MaterialSubTypeDesc = @mTypeofMaterial)
ORDER BY Date, WH.BuyerName, WD.MaterialCode
END

IF @mAction='SELFILTERDTLS'
BEGIN

SELECT        TOP (100) PERCENT WH.PWDate AS Date, WH.JobcardNo, WH.BuyerName, WH.ArticleName, WH.ArticelColor, WD.MaterialCode, WD.Description, 
                         WD.MaterialSubTypeDesc AS TypeofMaterial, WD.MaterialUnit AS UOM, WD.InTotalWQtyInKGS AS InQty, WD.TotalWQtyInKGS AS OutQty
FROM            dbo.SolarProductionWastageDetail AS WD INNER JOIN
                         dbo.SolarProductionWastage AS WH ON WD.HID = WH.ID
WHERE        (WD.MaterialType = 'GRA') 
And (wd.TransactionType = @mType)
AND (Cast(WH.PWDate As Date) >= @mFromDate) AND (Cast(WH.PWDate As Date) <= @mToDate)
AND (WD.MaterialSubTypeDesc = @mTypeofMaterial)
ORDER BY Date, WH.BuyerName, WD.MaterialCode
END


IF @mAction='SELALLDTLS2'
BEGIN

Select
sd.BuyerGroupCode,sd.SalesOrderNo,sd.CustomerOrderNo,sd.OrderReceivedDate,sd.ArticleName,mt.ArticleMould,
sd.orderQuantity,SD.ShippedQuantity,Sum(pw.RejectionQuantiy) As WastageQty,

((Sum(pw.RejectionQuantiy)) * 100 / sd.orderQuantity) As [WastageonOrderQty],
IsNull((((Sum(pw.RejectionQuantiy)) * 100 / NULLIF((sd.ShippedQuantity),0))),0) As [WastageonShpdQty],


ISNULL((SELECT		Sum(Consumption)
		FROM         dbo.MaterialDetails
        WHERE     (MaterialCode = MT.MaterialCode) AND (SUBSTRING(MT.MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
				And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 

							  As [Unit-KGS],
                              

Sum(pw.TotalQtyInKGS) As [Estimated KGS Wasted]


From
SalesOrderDetails As SD,
SolarProductionWastage As PW,
Materials As MT

Where
PW.SalesOrderDtlID = SD.ID
And SD.Article = MT.MaterialCode
--And pw.SalesOrderDtlID = 'c117a46a-809e-4f8c-b1e9-9f1fd9e2fb54'
And pw.TransactionType = 'GRINDING OUT'
And (Cast(sd.OrderReceivedDate As Date) >= @mFromDate And Cast(sd.OrderReceivedDate As Date) <= @mToDate)


Group By
sd.BuyerGroupCode,sd.SalesOrderNo,sd.CustomerOrderNo,sd.OrderReceivedDate,sd.ArticleName,mt.ArticleMould,MT.MaterialCode,
sd.orderQuantity,SD.ShippedQuantity

Order By
sd.SalesOrderNo,sd.CustomerOrderNo,sd.OrderReceivedDate,sd.BuyerGroupCode

END

If @mAction='IAA'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,			IsNull(WSTG.PWDate,'') As [Grinding Date],
		CASE WHEN
			WSTG.PWDate IS NULL THEN 'NO'
		ELSE
			'YES'
		END As [Grinded YES or NO],
		ISNULL(WSTGOUT.SerialNo,'') As [Grinding Out Id]					 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo LEFT OUTER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID LEFT OUTER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
		
	Order by RejectionDate
END

If @mAction='IAFY'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,			IsNull(WSTG.PWDate,'') As [Grinding Date],
		CASE WHEN
			WSTG.PWDate IS NULL THEN 'NO'
		ELSE
			'YES'
		END As [Grinded YES or NO],
		ISNULL(WSTGOUT.SerialNo,'') As [Grinding Out Id]				 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo LEFT OUTER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID LEFT OUTER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
		And WSTG.PWDate IS NOT NULL
	Order by RejectionDate
END

If @mAction='IAFN'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,			IsNull(WSTG.PWDate,'') As [Grinding Date],
		CASE WHEN
			WSTG.PWDate IS NULL THEN 'NO'
		ELSE
			'YES'
		END As [Grinded YES or NO],
		ISNULL(WSTGOUT.SerialNo,'') As [Grinding Out Id]			 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo LEFT OUTER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID LEFT OUTER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
		And WSTG.PWDate IS NULL
	Order by RejectionDate
END

IF @mAction='SELMATTYPE'
BEGIN
	SELECT ' ALL TYPES'   AS MaterialSubTypeDescription
	UNION 
	Select Distinct MaterialSubTypeDescription From Materials
	Where MaterialDiscription = 'SOLE'
	And MaterialType <> 'HEE'
	Order by MaterialSubTypeDescription
END


If @mAction='OAA'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,			
		WSTGOUT.TotalQtyInKgs,				WSTGOut.MaterialStatus						 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo INNER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID INNER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
	Order by RejectionDate
END

If @mAction='OAF'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,
		WSTGOUT.TotalQtyInKgs,				WSTGOut.MaterialStatus						 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo INNER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID INNER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
		And WSTG.PWDate IS NOT NULL
		And WSTGOut.MaterialStatus = @mROStatus

	Order by RejectionDate
END

If @mAction='OFA'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,			
		WSTGOUT.TotalQtyInKgs,				WSTGOut.MaterialStatus						 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo INNER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID INNER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
		And Mat.MaterialSubTypeDescription = @mMaterialSubTypeDescription
	Order by RejectionDate
END

If @mAction='OFF'
BEGIN
	Select 
		SR.RejectionDate As EntryDate,		Mat.MaterialType,Mat.MaterialSubType,	JCD.BuyerGroupCode,				SR.CustomerOrderNo,
		SR.JobcardNo,						Mat.MaterialCode,						Mat.ArticleMould,				JCD.Quantity As JCPairs,
		SR.RejectionQty,					SR.ExpectedWeight,						SR.ReasonForRejection,
		WSTGOUT.TotalQtyInKgs,				WSTGOut.MaterialStatus						 

	from
	dbo.SoleRejection AS SR INNER JOIN
                         dbo.Materials AS Mat ON SR.ArticleCode = Mat.MaterialCode INNER JOIN
                         dbo.JobCardDetail AS JCD ON SR.JobcardNo = JCD.JobCardNo INNER JOIN
                         dbo.SolarProductionWastage AS WSTG ON SR.ID = WSTG.RejectionID INNER JOIN
						 dbo.SolarProductionWastage As WSTGOUT ON WSTG.ID = WSTGOUT.RejectionID

	Where 
		Cast(SR.RejectionDate As Date) >= @mFromDate And Cast(SR.RejectionDate As Date) <= @mToDate
		And WSTG.PWDate IS NOT NULL
		And Mat.MaterialSubTypeDescription = @mMaterialSubTypeDescription
		And WSTGOut.MaterialStatus = @mROStatus
	Order by RejectionDate
END







GO
