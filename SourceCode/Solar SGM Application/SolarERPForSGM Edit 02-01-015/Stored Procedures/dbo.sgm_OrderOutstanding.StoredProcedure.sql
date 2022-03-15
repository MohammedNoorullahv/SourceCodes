USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_OrderOutstanding]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- drop Proc sgm_OrderOutstanding

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[sgm_OrderOutstanding]
@mAction			varchar(20)	='SELALL',
@mPKId				int		=Null,
@mFromDate			Datetime	=Null,
@mToDate			DateTime	=Null,
@mBuyerName			Varchar(150)	=Null,
@mSoleName			Varchar(500)	=Null,
@mCodification		Varchar(50)		=Null,
@mSalesOrderDetailID	Varchar(200)	=NULL,
@mSalesOrderDate		datetime		=NULL,
@mCustomerName			varchar(100)	=NULL,
@mSalesOrderNo			varchar(50)		=NULL,
@mCustomerOrderNo		varchar(100)	=NULL,
@mSoleCode				varchar(50)		=NULL,
@mColour				varchar(100)	=NULL,
@mOrdQty				int				=NULL,
@mMoulding				int				=NULL,
@mMouldingWIP			int				=NULL,
@mFinishing				int				=NULL,
@mFinishingWIP			int				=NULL,
@mPacking				int				=NULL,
@mInStock				int				=NULL,
@mDispatch				int				=NULL,
@mUdpdatedOn			datetime 		=NULL,
@mIsCompleted			bit				=NULL,
@mIsClosed				bit				=NULL
	
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELUPDDATE'
BEGIN
	Select Distinct(UpdatedOn) As UpdatedOn from SolarOutstanding4SGM Order By UpdatedOn Desc
END

IF @mAction='SELALLORD'
BEGIN
	Select 
	SD.ID As SalesOrderDetailId,	SO.OrderRecivedDate As SalesOrderDate,		Bu.BuyerName As CustomerName,
	sd.SalesOrderNo,				sd.CustomerOrderNo,	sd.Article As SoleCode,	sd.ArticleName As SoleName,
	ma.MaterialColorDescription As Color,										ma.CodificationNew As Codification,
	sd.OrderQuantity As OrdQty		
	
	From
	SalesOrderDetails As SD, SalesOrder As SO, Materials Ma, Buyer Bu
	
	Where
	sd.SalesOrderId = so.ID And sd.Shipper = 'SSPL' And sd.Article = ma.MaterialCode And so.BuyerCode = bu.BuyerCode
	
	Order By
	SO.OrderRecivedDate,sd.SalesOrderNo
END

IF @mAction='SELPRODORD'
BEGIN
	Select 
	SD.ID As SalesOrderDetailId,	SO.OrderRecivedDate As SalesOrderDate,		Bu.BuyerName As CustomerName,
	sd.SalesOrderNo,				sd.CustomerOrderNo,	sd.Article As SoleCode,	sd.ArticleName As SoleName,
	ma.MaterialColorDescription As Color,										ma.CodificationNew As Codification,
	sd.OrderQuantity As OrdQty		
	
	From
	SalesOrderDetails As SD, SalesOrder As SO, Materials Ma, Buyer Bu
	
	Where
	sd.SalesOrderId = so.ID And sd.Shipper = 'SSPL' And sd.Article = ma.MaterialCode And so.BuyerCode = bu.BuyerCode And sd.SalesOrderNo in (
	Select Distinct(SalesOrderNo) From SalesOrder where shipper = 'SSPL' And SD.CreatedDate >= @mFromDate
	Union
	Select Distinct(SalesOrderNo) from ProductionByProcess where CompanyCode = 'SSPL' And ProcessDate >= @mFromDate
	Union
	Select Distinct(Left(JobcardNo,9)) As SalesOrderNo from PackingDetail where shipper = 'SSPL' And DcCartonNo > 0 And PackingDate >= @mFromDate
	union
	Select Distinct(Left(JobcardNo,9)) As SalesOrderNo from InvoiceDetail  where Shipper = 'SSPL' And InvoiceDate >= @mFromDate)
	--sd.SalesOrderNo = 'S-14-1066'
	
	Order By
	SO.OrderRecivedDate,sd.SalesOrderNo
END


IF @mAction='SELMOULDQTY'
BEGIN
	Select IsNull(SUM(Quantity),0)  As MouldQty FROM ProductionByProcess
	Where WorkOrderNo in 
	(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) And ProcessName = 'MOULD'
END

IF @mAction='SELFINISHQTY'
BEGIN
	Select IsNull(SUM(Quantity),0)  As Finish FROM ProductionByProcess
	Where WorkOrderNo in 
	(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) And ProcessName = 'FINISH'
END

IF @mAction='SELPACKQTY'
BEGIN
	Select IsNull(Sum(Quantity),0) As PackingQty FROM PackingDetail
	Where JobcardNo in 
	(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) And DCCartonNo > 0
END

IF @mAction='SELDISPQTY'
BEGIN
	Select IsNull(Sum(Quantity),0) As DispatchQty FROM InvoiceDetail
	Where JobcardNo in 
	(Select JobcardNo from JobCardDetail Where SalesOrderDetailID = @mSalesOrderDetailID) 
END

IF @mAction='SELSOD'
BEGIN
	SELECT * FROM SolarOutstanding4SGM Where SalesOrderDetailID = @mSalesOrderDetailID
END

IF @mAction='INSSTATUS'
BEGIN
	INSERT INTO
	SolarOutstanding4SGM
	
	VALUES
	(@mSalesOrderDetailId,		@mSalesOrderDate,		@mCustomerName,			@mSalesOrderNo,			@mCustomerOrderNo,
	@mSoleCode,					@mSoleName,				@mColour,				@mCodification,			@mOrdQty,
	@mMoulding,					@mMouldingWIP,			@mFinishing,			@mFinishingWIP,			@mPacking,
	@mInStock,					@mDispatch,				@mUdpdatedOn,			@mIsCompleted,			@mIsClosed)
END

IF @mAction='UPDSTATUS'
BEGIN
	UPDATE
	SolarOutstanding4SGM
	
	SET
	Moulding = @mMoulding,		MouldingWIP = @mMouldingWIP,					Finishing = @mFinishing,	FinishingWIP = @mFinishingWIP,
	Packing = @mPacking,		InStock = @mInStock,							Dispatch = @mDispatch,		UpdatedOn = @mUdpdatedOn,
	IsCompleted = @mIsCompleted,IsClosed = @mIsClosed
	
	Where
	SalesOrderDetailId = @mSalesOrderDetailId
END

IF @mAction='LOADOUTST'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate  And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	
	Order By PKID
END

IF @mAction='LOADOUTST1'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate  And IsCompleted = '1'
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTST0'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate  And IsCompleted = '0'
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTC'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) 
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTC1'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And IsCompleted = '1'
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END


IF @mAction='LOADOUTSTC0'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And IsCompleted = '0'
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTALL'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate 
	--And CustomerName IN (Select BuyerName from Buyer where BuyerName = @mBuyerName
	--) And SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTCA'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTCA'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTCA'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTCA1'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And SoleName in (Select MaterialName from Materials where MaterialName  = @mSoleName) And IsCompleted = '1'
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END


IF @mAction='LOADOUTSTCA0'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And SoleName in (Select MaterialName from Materials where MaterialName  = @mSoleName) And IsCompleted = '0'
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END



IF @mAction='LOADOUTSTAW'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And 
	SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTAW0'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And 
	SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	And IsCompleted = '0'
	Order By PKID
END

IF @mAction='LOADOUTSTAW1'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And 
	SoleName in (Select MaterialName from Materials where MaterialName = @mSoleName)
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	And IsCompleted = '1'
	Order By PKID
END

IF @mAction='LOADOUTSTCC'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName
	) And Codification = @mCodification
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	
	Order By PKID
END
IF @mAction='LOADOUTSTCC1'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName And IsCompleted = '1'
	) And Codification = @mCodification
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END

IF @mAction='LOADOUTSTCC0'
BEGIN
	Select PKID, SalesOrderDetailId, SalesOrderDate, CustomerName, SalesOrderNo, CustomerOrderNo, SoleCode, SoleName, Colour, Codification, OrdQty, 
                      Moulding,  OrdQty - Moulding As MouldBal,MouldingWIP, Finishing, OrdQty - Finishing As FinBal, FinishingWIP, Packing, OrdQty - Packing As PackBal, InStock, Dispatch, OrdQty - Dispatch As DispBal, UpdatedOn, IsCompleted, IsClosed
                       from SolarOutstanding4SGM 
	Where SalesOrderDate >= @mFromDate AND SalesOrderDate <= @mToDate And CustomerName IN (
	Select BuyerName from Buyer where BuyerName = @mBuyerName And IsCompleted = '0'
	) And Codification = @mCodification
	And SalesORderDetailId not in (
													Select ID from SalesOrderDetails where OrderStatus IN ('CLOSE','CANCEL'))
	Order By PKID
END


IF @mAction='LoadCust'
BEGIN
Select ' ALL CUSTOMERS' As BuyerName
	UNION
SELECT DISTINCT dbo.Buyer.BuyerName
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
Select ' ALL ARTICLES' As SoleName
	UNION
SELECT DISTINCT 
                      TOP (100) PERCENT dbo.Materials.MaterialName AS SoleName
FROM         dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate And
		 dbo.SalesOrder.BuyerName = @mBuyerName 
ORDER BY  SoleName	
END

IF @mAction='LoadAllArt'
BEGIN
Select ' ALL ARTICLES' As SoleName
	UNION
SELECT DISTINCT 
                      TOP (100) PERCENT dbo.Materials.MaterialName AS SoleName
FROM         dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate --And
		 --dbo.SalesOrder.BuyerName = @mBuyerName 
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


IF @mAction='DELOUTST'
BEGIN
	DELETE FROM
	SolarOutstanding4SGM4Print
	
END

IF @mAction='INSOUTST'
BEGIN
	INSERT INTO
	SolarOutstanding4SGM4Print
	
	VALUES
	(@mSalesOrderDetailId,		@mSalesOrderDate,		@mCustomerName,			@mSalesOrderNo,			@mCustomerOrderNo,
	@mSoleCode,					@mSoleName,				@mColour,				@mCodification,			@mOrdQty,
	@mMoulding,					@mMouldingWIP,			@mFinishing,			@mFinishingWIP,			@mPacking,
	@mInStock,					@mDispatch,				@mUdpdatedOn,			@mIsCompleted,			@mIsClosed)
END

--Select * from SolarOutstanding4SGM
--Select Top 3 * from SalesOrderDetails where Shipper = 'SSPL'
--Select Top 3 * from SalesOrder where Shipper = 'SSPL'
--Select * from PackingDetail
--Select * from Materials where materialcode = 'SOL-SYN-PV-0058'



GO
