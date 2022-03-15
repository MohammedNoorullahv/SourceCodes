USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_AllinOnePurchaseOrdersNOMRP]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_AllinOnePurchaseOrdersNOMRP

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_AllinOnePurchaseOrdersNOMRP]
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
@mMaterialType					Varchar(50)		=NULL,
@mMaterialSubType				Varchar(50)		=NULL
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int


----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0AAAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0AAAAAAD

-- Option 03
If @mAction='P0AAAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
										And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0AAAAASD

-- Option 05
If @mAction='P0AAAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0AAAASAD

-- Option 07
If @mAction='P0AAAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0AAAASSD

-- Option 09
If @mAction='P0AAASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0AAASAAD

-- Option 11
If @mAction='P0AAASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0AAASASD

-- Option 13
If @mAction='P0AAASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0AAASSAD

-- Option 15
If @mAction='P0AAASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0AAASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0AAFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0AAFAAAD

-- Option 03
If @mAction='P0AAFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0AAFAASD

-- Option 05
If @mAction='P0AAFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0AAFASAD

-- Option 07
If @mAction='P0AAFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0AAFASSD

-- Option 09
If @mAction='P0AAFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0AAFSAAD

-- Option 11
If @mAction='P0AAFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0AAFSASD

-- Option 13
If @mAction='P0AAFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0AAFSSAD

-- Option 15
If @mAction='P0AAFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0AAFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------


----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0ASAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0ASAAAAD

-- Option 03
If @mAction='P0ASAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0ASAAASD

-- Option 05
If @mAction='P0ASAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0ASAASAD

-- Option 07
If @mAction='P0ASAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0ASAASSD

-- Option 09
If @mAction='P0ASASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0ASASAAD

-- Option 11
If @mAction='P0ASASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0ASASASD

-- Option 13
If @mAction='P0ASASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0ASASSAD

-- Option 15
If @mAction='P0ASASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0ASASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0ASFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0ASFAAAD

-- Option 03
If @mAction='P0ASFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0ASFAASD

-- Option 05
If @mAction='P0ASFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0ASFASAD

-- Option 07
If @mAction='P0ASFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0ASFASSD

-- Option 09
If @mAction='P0ASFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0ASFSAAD

-- Option 11
If @mAction='P0ASFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0ASFSASD

-- Option 13
If @mAction='P0ASFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0ASFSSAD

-- Option 15
If @mAction='P0ASFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0ASFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------
----------------------------------------------------------------  02(01)  ----------------------------------------------------------------




----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SAAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SAAAAAD

-- Option 03
If @mAction='P0SAAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SAAAASD

-- Option 05
If @mAction='P0SAAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SAAASAD

-- Option 07
If @mAction='P0SAAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SAAASSD

-- Option 09
If @mAction='P0SAASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SAASAAD

-- Option 11
If @mAction='P0SAASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SAASASD

-- Option 13
If @mAction='P0SAASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SAASSAD

-- Option 15
If @mAction='P0SAASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SAASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SAFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SAFAAAD

-- Option 03
If @mAction='P0SAFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SAFAASD

-- Option 05
If @mAction='P0SAFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder

	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SAFASAD

-- Option 07
If @mAction='P0SAFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SAFASSD

-- Option 09
If @mAction='P0SAFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SAFSAAD

-- Option 11
If @mAction='P0SAFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SAFSASD

-- Option 13
If @mAction='P0SAFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SAFSSAD

-- Option 15
If @mAction='P0SAFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SAFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------


----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SAAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SAAAAAD

-- Option 03
If @mAction='P0SAAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SAAAASD

-- Option 05
If @mAction='P0SAAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SAAASAD

-- Option 07
If @mAction='P0SAAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SAAASSD

-- Option 09
If @mAction='P0SAASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SAASAAD

-- Option 11
If @mAction='P0SAASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SAASASD

-- Option 13
If @mAction='P0SAASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SAASSAD

-- Option 15
If @mAction='P0SAASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SAASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SAFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SAFAAAD

-- Option 03
If @mAction='P0SAFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SAFAASD

-- Option 05
If @mAction='P0SAFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SAFASAD

-- Option 07
If @mAction='P0SAFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SAFASSD

-- Option 09
If @mAction='P0SAFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SAFSAAD

-- Option 11
If @mAction='P0SAFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SAFSASD

-- Option 13
If @mAction='P0SAFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SAFSSAD

-- Option 15
If @mAction='P0SAFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SAFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------
----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------




----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SSAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SSAAAAD

-- Option 03
If @mAction='P0SSAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SSAAASD

-- Option 05
If @mAction='P0SSAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SSAASAD

-- Option 07
If @mAction='P0SSAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SSAASSD

-- Option 09
If @mAction='P0SSASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SSASAAD

-- Option 11
If @mAction='P0SSASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SSASASD

-- Option 13
If @mAction='P0SSASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SSASSAD

-- Option 15
If @mAction='P0SSASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SSASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SSFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SSFAAAD

-- Option 03
If @mAction='P0SSFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SSFAASD

-- Option 05
If @mAction='P0SSFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SSFASAD

-- Option 07
If @mAction='P0SSFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SSFASSD

-- Option 09
If @mAction='P0SSFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SSFSAAD

-- Option 11
If @mAction='P0SSFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SSFSASD

-- Option 13
If @mAction='P0SSFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SSFSSAD

-- Option 15
If @mAction='P0SSFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SSFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------


----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  01  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SSAAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					

	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SSAAAAD

-- Option 03
If @mAction='P0SSAAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SSAAASD

-- Option 05
If @mAction='P0SSAASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SSAASAD

-- Option 07
If @mAction='P0SSAASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SSAASSD

-- Option 09
If @mAction='P0SSASAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SSASAAD

-- Option 11
If @mAction='P0SSASASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SSASASD

-- Option 13
If @mAction='P0SSASSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SSASSAD

-- Option 15
If @mAction='P0SSASSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SSASSSD

----------------------------------------------------------------  01  ----------------------------------------------------------------


----------------------------------------------------------------  02  ----------------------------------------------------------------
-- Option 01
If @mAction='P0SSFAAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 02 - P0SSFAAAD

-- Option 03
If @mAction='P0SSFAASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 04 - P0SSFAASD

-- Option 05
If @mAction='P0SSFASAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 06 - P0SSFASAD

-- Option 07
If @mAction='P0SSFASSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 08 - P0SSFASSD

-- Option 09
If @mAction='P0SSFSAAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 10 - P0SSFSAAD

-- Option 11
If @mAction='P0SSFSASS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 12 - P0SSFSASD

-- Option 13
If @mAction='P0SSFSSAS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')										
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 14 - P0SSFSSAD

-- Option 15
If @mAction='P0SSFSSSS'
BEGIN
	SELECT     TOP (100) PERCENT dbo.PurchaseOrderDetails.PurchaseOrderDate, dbo.PARTY.partyname, dbo.PurchaseOrderDetails.PurchaseOrderNo, 
                      dbo.PurchaseOrder.CurrencyCode, dbo.PurchaseOrder.PurchaseOrderType, dbo.PurchaseOrderDetails.MaterialCode, dbo.PurchaseOrderDetails.MaterialDescription, 
                      dbo.PurchaseOrderDetails.MaterialSize, dbo.Materials.MaterialColorDescription, dbo.PurchaseOrderDetails.Unit,dbo.PurchaseOrderDetails.Quantity, dbo.PurchaseOrderDetails.Price, 
                      dbo.PurchaseOrderDetails.MaterialValue, dbo.Materials.MaterialTypeDescription, dbo.Materials.MaterialSubTypeDescription, IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As ReceivedQuantity, dbo.PurchaseOrderDetails.Quantity - IsNull(dbo.PurchaseOrderDetails.ReceivedQuantity,'0') As BalanceQuantity, dbo.PurchaseOrderDetails.PurchaseOrderStatus
	FROM         dbo.PurchaseOrderDetails INNER JOIN
                      dbo.PurchaseOrder ON dbo.PurchaseOrderDetails.PID = dbo.PurchaseOrder.ID INNER JOIN
                      dbo.PARTY ON dbo.PurchaseOrder.SupplierCode = dbo.PARTY.partycode INNER JOIN
                      dbo.Materials ON dbo.PurchaseOrderDetails.MaterialCode = dbo.Materials.MaterialCode
	WHERE     (dbo.PurchaseOrderDetails.Shipper = 'SSPL') 
					AND (Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) >= @mFromDate And Cast(dbo.PurchaseOrderDetails.PurchaseOrderDate As Date) <= @mToDate )
					AND dbo.Materials.MaterialTypeDescription = @mMaterialType
					AND dbo.Materials.MaterialSubTypeDescription = @mMaterialSubType
					AND dbo.PurchaseOrderDetails.MaterialCode like '%'+@mArticleCode+'%'
					AND dbo.PurchaseOrderDetails.MaterialDescription like '%'+@mDescription+'%'
					And dbo.PurchaseOrderDetails.PurchaseOrderStatus = @mOrderStatus
					AND  dbo.PARTY.partyname = @mBuyerName
					And dbo.PurchaseOrder.POSubtype = @mTypeofOrder
					And dbo.PurchaseOrder.PurchaseOrderType NOT IN ('TS','TR')					
	ORDER BY dbo.PurchaseOrderDetails.PurchaseOrderDate
END

-- Option 16 - P0SSFSSSD

----------------------------------------------------------------  02  ----------------------------------------------------------------
----------------------------------------------------------------  02(01)  ----------------------------------------------------------------
----------------------------------------------------------------  03(02(01))  ----------------------------------------------------------------




GO
