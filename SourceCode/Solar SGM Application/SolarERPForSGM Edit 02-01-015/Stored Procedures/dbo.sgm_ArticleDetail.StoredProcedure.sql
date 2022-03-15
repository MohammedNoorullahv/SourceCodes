USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[sgm_ArticleDetail]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- drop Proc sgm_ArticleDetail

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROCEDURE [dbo].[sgm_ArticleDetail]
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
@mCodificationNew				varchar(50)		=NULL
AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int



IF @mAction='SELALLARTICLES'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.SalesOrderDetails.Article AS Code, dbo.Materials.UserCategory AS Gender, 
                      dbo.MaterialType.MaterialTypeDescription + ' - ' + dbo.MaterialType.MaterialSubTypeDescription AS SoleType, 
                      dbo.Materials.MaterialName AS SoleName,dbo.Materials.Description AS ArticleName, dbo.ColorMaster.ColorName AS Colour, 
                     
                     ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                              
                      +
                      
                                           
                      ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And (ChildMaterialDescription Like 'RUBBER%'))
                              ), '0')                 
                     AS GranulesCount,
                     
                     --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                      ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ORDER BY Consumption DESC), '') 
                     --ELSE
                     --''
                     --END         
                       AS Granules, 
                       
                       
           --            CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                       
           --            ISNULL
           --               ((SELECT     TOP (1) Consumption
           --                   FROM         dbo.MaterialDetails AS MaterialDetails_2
           --                   WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 --And ChildMaterialDescription Like 'GRANUL%')
           --                   ORDER BY Consumption DESC), '0') 
           --          ELSE
           --          '0'
           --          END    
           --                  AS NettWt,
                             
                                   ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                              
                              +
                              
                              ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS NettWt,
                             
                             
                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '') AS [Leather-SQM],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND(ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-DeclaredConsumption],
                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '') AS [Leather-KGS],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-DeclaredConsumption], 
                              
           --                    CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                              
           --                   ISNULL
           --               ((SELECT     TOP (1) NetConsumption
           --                   FROM         dbo.MaterialDetails AS MaterialDetails_1
           --                   WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 --And ChildMaterialDescription Like 'GRANUL%')
           --                   ORDER BY Consumption DESC), '0')
                              
           --                   ELSE
           --          '0'
           --          END   
           --                    AS DeclaredWt, 
                               
                               
                                 ISNULL
                          ((SELECT		Sum(NetConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                              
                              +
                              
                              +
                              
                              ISNULL
                          ((SELECT		Sum(NetConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS DeclaredWt,
                               
                               dbo.Materials.Tannage AS Codification, dbo.Materials.CodificationNew,
                              
                               ISNULL
                          ((SELECT		Sum(BaseConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                              
                              +
                              
                              +
                              
                              ISNULL
                          ((SELECT		Sum(BaseConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS BaseConsumption,
                     
                      ISNULL
                          ((SELECT		Sum(MatarozzaCons)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                              
                              +
                              
                                                            
                              ISNULL
                          ((SELECT		Sum(MatarozzaCons)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS Matarozza
                     

FROM         dbo.MaterialType INNER JOIN
                      dbo.Materials ON dbo.MaterialType.MaterialTypeCode = dbo.Materials.MaterialTypeCode LEFT OUTER JOIN
                      dbo.ColorMaster ON dbo.Materials.MaterialColor = dbo.ColorMaster.ColorCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate
ORDER BY Code	
END


IF @mAction='SELARTWCUST'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.SalesOrderDetails.Article AS Code, dbo.Materials.UserCategory AS Gender, 
                      dbo.MaterialType.MaterialTypeDescription + ' - ' + dbo.MaterialType.MaterialSubTypeDescription AS SoleType, 
                      dbo.Materials.MaterialName AS SoleName,dbo.Materials.Description AS ArticleName, dbo.ColorMaster.ColorName AS Colour, 
                     
					  ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '') 
							    +
                      
                                           
                      ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And (ChildMaterialDescription Like 'RUBBER%'))
                              ), '0') 
                     AS GranulesCount,

                     --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                      ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ORDER BY Consumption DESC), '') 

                     --ELSE
                     --''
                     --END         
                       AS Granules, 
                       
                       
                     --  CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                       
                     --  ISNULL
                     --     ((SELECT     TOP (1) Consumption
                     --         FROM         dbo.MaterialDetails AS MaterialDetails_2
                     --         WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
                     --         										 And ChildMaterialDescription Like 'GRANUL%')
                     --         ORDER BY Consumption DESC), '0') 
                     --ELSE
                     --'0'
                     --END    
                     --        AS NettWt,

					         ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
							   +
                              
                              ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS NettWt,

                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '') AS [Leather-SQM],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND(ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-DeclaredConsumption],
                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '') AS [Leather-KGS],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-DeclaredConsumption], 
                              
                               --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                              
                     --         ISNULL
                     --     ((SELECT     TOP (1) NetConsumption
                     --         FROM         dbo.MaterialDetails AS MaterialDetails_1
                     --         WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
                     --         										 And ChildMaterialDescription Like 'GRANUL%')
                     --         ORDER BY Consumption DESC), '0')
                              
                     --         ELSE
                     --'0'
                     --END   
                     --          AS DeclaredWt, 

					       ISNULL
                          ((SELECT		Sum(NetConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') AS DeclaredWt,
                               
                               dbo.Materials.Tannage AS Codification, dbo.Materials.CodificationNew,
                              
                               ISNULL
                          ((SELECT		Sum(BaseConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS BaseConsumption,
                     
                      ISNULL
                          ((SELECT		Sum(MatarozzaCons)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS Matarozza
FROM         dbo.MaterialType INNER JOIN
                      dbo.Materials ON dbo.MaterialType.MaterialTypeCode = dbo.Materials.MaterialTypeCode LEFT OUTER JOIN
                      dbo.ColorMaster ON dbo.Materials.MaterialColor = dbo.ColorMaster.ColorCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article

WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate And
		 dbo.SalesOrder.BuyerName = @mBuyerName
ORDER BY Code	
END

IF @mAction='SELCUSTWART'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.SalesOrderDetails.Article AS Code, dbo.Materials.UserCategory AS Gender, 
                      dbo.MaterialType.MaterialTypeDescription + ' - ' + dbo.MaterialType.MaterialSubTypeDescription AS SoleType, 
                      dbo.Materials.MaterialName AS SoleName,dbo.Materials.Description AS ArticleName, dbo.ColorMaster.ColorName AS Colour, 
                     
					  ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '') 
							  +
                      
                                           
                      ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And (ChildMaterialDescription Like 'RUBBER%'))
                              ), '0') 
                     AS GranulesCount,

                     --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                      ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ORDER BY Consumption DESC), '') 
                     --ELSE
                     --''
                     --END         
                       AS Granules, 
                       
                       
                     --  CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                       
                     --  ISNULL
                     --     ((SELECT     TOP (1) Consumption
                     --         FROM         dbo.MaterialDetails AS MaterialDetails_2
                     --         WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
                     --         										 And ChildMaterialDescription Like 'GRANUL%')
                     --         ORDER BY Consumption DESC), '0') 
                     --ELSE
                     --'0'
                     --END    
                     --        AS NettWt,

					         ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
							          +
                              
                              ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS NettWt,
                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '') AS [Leather-SQM],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND(ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-DeclaredConsumption],
                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '') AS [Leather-KGS],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-DeclaredConsumption], 
                              
                               --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                              
                     --         ISNULL
                     --     ((SELECT     TOP (1) NetConsumption
                     --         FROM         dbo.MaterialDetails AS MaterialDetails_1
                     --         WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
                     --         										 And ChildMaterialDescription Like 'GRANUL%')
                     --         ORDER BY Consumption DESC), '0')
                              
                     --         ELSE
                     --'0'
                     --END   
                               --AS DeclaredWt, 

							         ISNULL
                          ((SELECT		Sum(NetConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS DeclaredWt,
                               
                               dbo.Materials.Tannage AS Codification, dbo.Materials.CodificationNew,
                              
                               ISNULL
                          ((SELECT		Sum(BaseConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS BaseConsumption,
                     
                      ISNULL
                          ((SELECT		Sum(MatarozzaCons)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS Matarozza
FROM         dbo.MaterialType INNER JOIN
                      dbo.Materials ON dbo.MaterialType.MaterialTypeCode = dbo.Materials.MaterialTypeCode LEFT OUTER JOIN
                      dbo.ColorMaster ON dbo.Materials.MaterialColor = dbo.ColorMaster.ColorCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article

WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate And
		 --dbo.SalesOrder.BuyerName = @mBuyerName And
		  dbo.Materials.MaterialName = @mSoleName
ORDER BY Code	
END

IF @mAction='SELCUSTWCODE'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.SalesOrderDetails.Article AS Code, dbo.Materials.UserCategory AS Gender, 
                      dbo.MaterialType.MaterialTypeDescription + ' - ' + dbo.MaterialType.MaterialSubTypeDescription AS SoleType, 
                      dbo.Materials.MaterialName AS SoleName,dbo.Materials.Description AS ArticleName, dbo.ColorMaster.ColorName AS Colour, 
                     
					  ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '') 
							     +
                      
                                           
                      ISNULL
                          ((SELECT		Count(ChildMaterialDescription)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And (ChildMaterialDescription Like 'RUBBER%'))
                              ), '0') 
                     AS GranulesCount,

                     --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                      ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ORDER BY Consumption DESC), '') 
                     --ELSE
                     --''
                     --END         
                       AS Granules, 
                       
                       
                     --  CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                       
                     --  ISNULL
                     --     ((SELECT     TOP (1) Consumption
                     --         FROM         dbo.MaterialDetails AS MaterialDetails_2
                     --         WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
                     --         										 And ChildMaterialDescription Like 'GRANUL%')
                     --         ORDER BY Consumption DESC), '0') 
                     --ELSE
                     --'0'
                     --END    
                     --        AS NettWt,

					         ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
							      +
                              
                              ISNULL
                          ((SELECT		Sum(Consumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 5, 2)
										 And ChildMaterialDescription Like 'Rubber%')
                              ), '0')
                     AS NettWt,

                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '') AS [Leather-SQM],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND(ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'SQM'
                              ORDER BY Consumption DESC), '0') AS [SQM-DeclaredConsumption],
                              ISNULL
                          ((SELECT     TOP (1) ChildMaterialDescription
                              FROM         dbo.MaterialDetails AS MaterialDetails_3
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '') AS [Leather-KGS],
                              ISNULL
                          ((SELECT     TOP (1) Consumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_4
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-Consumption],
                               ISNULL
                          ((SELECT     TOP (1) NetConsumption
                              FROM         dbo.MaterialDetails AS MaterialDetails_5
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (ChildMaterialDescription Like 'LEATHER%' or ChildMaterialDescription Like 'LETAHER%') And Unit = 'KGS'
                              ORDER BY Consumption DESC), '0') AS [KGS-DeclaredConsumption], 
                              
                               --CASE WHEN SUBSTRING(dbo.Materials.MaterialCode, 5, 3) <> 'LEA' THEN 
                              
                     --         ISNULL
                     --     ((SELECT     TOP (1) NetConsumption
                     --         FROM         dbo.MaterialDetails AS MaterialDetails_1
                     --         WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
                     --         										 And ChildMaterialDescription Like 'GRANUL%')
                     --         ORDER BY Consumption DESC), '0')
                              
                     --         ELSE
                     --'0'
                     --END   
                     --          AS DeclaredWt, 
                               
							         ISNULL
                          ((SELECT		Sum(NetConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS DeclaredWt,

                               dbo.Materials.Tannage AS Codification, dbo.Materials.CodificationNew,
                              
                               ISNULL
                          ((SELECT		Sum(BaseConsumption)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS BaseConsumption,
                     
                      ISNULL
                          ((SELECT		Sum(MatarozzaCons)
                              FROM         dbo.MaterialDetails
                              WHERE     (MaterialCode = dbo.Materials.MaterialCode) AND (SUBSTRING(MaterialCode, 9, 2) = SUBSTRING(ChildMaterialCode, 9, 2)
										 And ChildMaterialDescription Like 'GRANUL%')
                              ), '0') 
                     AS Matarozza

FROM         dbo.MaterialType INNER JOIN
                      dbo.Materials ON dbo.MaterialType.MaterialTypeCode = dbo.Materials.MaterialTypeCode LEFT OUTER JOIN
                      dbo.ColorMaster ON dbo.Materials.MaterialColor = dbo.ColorMaster.ColorCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article

WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate And
		 --dbo.SalesOrder.BuyerName = @mBuyerName And 
		 dbo.Materials.CodificationNew = @mCodification
ORDER BY Code	
END

IF @mAction='LoadCust'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.SalesOrder.BuyerName AS Client
FROM         dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate
ORDER BY  Client	
END

IF @mAction='LoadArt'
BEGIN

SELECT DISTINCT 
                      TOP (100) PERCENT dbo.Materials.MaterialName AS SoleName--,dbo.Materials.Description AS ArticleName
FROM         dbo.ColorMaster INNER JOIN
                      dbo.Materials ON dbo.ColorMaster.ColorCode = dbo.Materials.MaterialColor INNER JOIN
                      dbo.MaterialType ON dbo.Materials.MaterialTypeCode = dbo.MaterialType.MaterialTypeCode RIGHT OUTER JOIN
                      dbo.SalesOrder INNER JOIN
                      dbo.SalesOrderDetails ON dbo.SalesOrder.ID = dbo.SalesOrderDetails.SalesOrderID ON 
                      dbo.Materials.MaterialCode = dbo.SalesOrderDetails.Article
WHERE     (dbo.SalesOrder.Shipper = 'SSPL') And
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate
		  --And dbo.SalesOrder.BuyerName = @mBuyerName 
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
		 salesOrder.OrderRecivedDate >= @mFromDate And salesorder.OrderRecivedDate <= @mToDate
		 -- And dbo.SalesOrder.BuyerName = @mBuyerName 
ORDER BY  CodificationNew	
END





IF @mAction='DELART'
BEGIN
	DELETE FROM
	SolarArticleMaster4SGM4Print

END
	


IF @mAction='INSART'
BEGIN
	INSERT INTO
	SolarArticleMaster4SGM4Print
	
	VALUES
	(@mClient,				@mCode,						@mGender,			@mSoleType,				
	@mSoleName,				@mColour,					@mGranules,			@mNettWt,				@mLeatherSQM,	
	@mSQMConsumption,		@mSQMDeclaredConsumption,	@mLeatherKGS,		@mKGSConsumption,		@mKGSDeclaredConsumption,
	@mDeclaredWt,			@mCodification,				@mCodificationNew)
END	


IF @mAction='LoadMaterial'
BEGIN
	SELECT
	ID,						MaterialDiscription As Material,			MaterialTypeDescription As Type,
	MaterialSubTypeDescription As SubType,			MaterialCode,Description,
	PrintDescription,		MaterialColorDescription As Color,			Unit

	FROM
	Materials

	Where MaterialDepartment like '%S%'
	And MatErialCode Not like 'SOL%'

	Order By 
	MaterialDiscription,MaterialTypeDescription,MaterialSubTypeDescription,Description
END









GO
