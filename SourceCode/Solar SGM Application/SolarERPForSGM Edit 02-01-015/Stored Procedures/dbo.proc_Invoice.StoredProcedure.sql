USE [AHGroup_SSPL]
GO
/****** Object:  StoredProcedure [dbo].[proc_Invoice]    Script Date: 10-02-2020 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









-- drop Proc proc_Invoice

--Return value tables
--Value		Meaning
--=====		=======
--0		Successfull execution
--1		Required parameters value not specified
--2		Invalid parameters value specified
--3		SQL Server error
--4		Contact is updated as New
--5		Contact is updated as Active

CREATE PROC [dbo].[proc_Invoice]
@mAction						varchar(50)		='SELALL',
@mPKId							int				=Null,
@mFromDate						Datetime		=Null,
@mToDate						DateTime		=Null,
@mBuyerName						Varchar(150)	=Null,
@mInvoiceSerialNo				varchar(10) 	=Null,
@mSalesOrderNo					varchar(25) 	=Null,
@mArticleNo						varchar(250) 	=Null,
@mrate							decimal(14, 2) 	=Null,
@mPackNo						varchar(100) 	=Null,
@mJobCardNo						varchar(50) 	=Null,
@mJobCardDetailID				varchar(50) 	=Null,
@mInvoiceID						varchar(50) 	=Null,
@mSalesOrderDetailID			varchar(50) 	=Null,
@mSize01						varchar(7) 		=Null,
@mQuantity01					decimal(14, 2) 	=Null,
@mSize02						varchar(7) 		=Null,
@mQuantity02					decimal(14, 2) 	=Null,
@mSize03						varchar(7) 		=Null,
@mQuantity03					decimal(14, 2) 	=Null,
@mSize04						varchar(7) 		=Null,
@mQuantity04					decimal(14, 2) 	=Null,
@mSize05						varchar(7) 		=Null,
@mQuantity05					decimal(14, 2) 	=Null,
@mSize06						varchar(7) 		=Null,
@mQuantity06					decimal(14, 2) 	=Null,
@mSize07						varchar(7) 		=Null,
@mQuantity07					decimal(14, 2) 	=Null,
@mSize08						varchar(7) 		=Null,
@mQuantity08					decimal(14, 2) 	=Null,
@mSize09						varchar(7) 		=Null,
@mQuantity09					decimal(14, 2) 	=Null,
@mSize10						varchar(7) 		=Null,
@mQuantity10					decimal(14, 2) 	=Null,
@mSize11						varchar(7) 		=Null,
@mQuantity11					decimal(14, 2) 	=Null,
@mSize12						varchar(7) 		=Null,
@mQuantity12					decimal(14, 2) 	=Null,
@mSize13						varchar(7) 		=Null,
@mQuantity13					decimal(14, 2) 	=Null,
@mSize14						varchar(7) 		=Null,
@mQuantity14					decimal(14, 2) 	=Null,
@mSize15						varchar(7) 		=Null,
@mQuantity15					decimal(14, 2) 	=Null,
@mSize16						varchar(7) 		=Null,
@mQuantity16					decimal(14, 2) 	=Null,
@mSize17						varchar(7) 		=Null,
@mQuantity17					decimal(14, 2) 	=Null,
@mSize18						varchar(7) 		=Null,
@mQuantity18					decimal(14, 2) 	=Null,
@mReadyToDispatchDate			Datetime		=Null,
@mCartonNo						Int				=Null,
@mSpoolId						Varchar(25)		=Null,
@mSpoolDt						Datetime		=Null,


@mInvoiceNo						varchar(25)  		=Null,
@mInvoiceDate						datetime 		=Null,
@mBuyer						varchar(20) 		=Null,
@mAccount						varchar(20) 		=Null,
@mShipper						varchar(20)  		=Null,
@mBuyerDepartment						varchar(50) 		=Null,
@mOrigin						varchar(50) 		=Null,
@mLCNo						varchar(50) 		=Null,
@mInvoiceDescription						varchar(65) 		=Null,
@mShippingBillNo						varchar(50) 		=Null,
@mShippingBillDate						datetime 		=Null,
@mMarks1						varchar(50) 		=Null,
@mMarks2						varchar(50) 		=Null,
@mMarks3						varchar(50) 		=Null,
@mMarks4						varchar(50) 		=Null,
@mMarks5						varchar(50) 		=Null,
@mMarks6						varchar(50) 		=Null,
@mMarks7						varchar(50) 		=Null,
@mMarks8						varchar(50) 		=Null,
@mModeOfShipment						varchar(7) 		=Null,
@mAwbBillNo						varchar(30) 		=Null,
@mAwbBillDate						datetime 		=Null,
@mHAwbBillNo						varchar(15) 		=Null,
@mHAwbBillDate						datetime 		=Null,
@mDestination						varchar(50) 		=Null,
@mBanckAccount						varchar(50) 		=Null,
@mVessel						varchar(30) 		=Null,
@mBank						varchar(10) 		=Null,
@mCurrency						varchar(4) 		=Null,
@mCurrencyConversion						decimal(14, 2) 		=Null,
@mNature						varchar(5) 		=Null,
@mQuantity						decimal(14, 2) 		=Null,
@mTotalValue						decimal(14, 2) 		=Null,
@mFreight						decimal(14, 2) 		=Null,
@mInsurance						decimal(14, 2) 		=Null,
@mAgentPercentage						decimal(14, 2) 		=Null,
@mCommission						decimal(14, 2) 		=Null,
@mDbkPercentage						decimal(14, 2) 		=Null,
@mDrawBack						decimal(14, 2) 		=Null,
@mBankCertificate						varchar(15) 		=Null,
@mBankAmount						decimal(14, 2) 		=Null,
@mBankRate						decimal(14, 2) 		=Null,
@mCertificateDate						datetime 		=Null,
@mTotalPackNo						decimal(14, 2) 		=Null,
@mharmnsdco						varchar(100) 		=Null,
@mNetWeight						decimal(14, 2) 		=Null,
@mGrossWeight						decimal(14, 2) 		=Null,
@mdbkrecd						decimal(14, 2) 		=Null,
@maepccer_no						varchar(20) 		=Null,
@mcust_clear						datetime 		=Null,
@mrbicode						varchar(20) 		=Null,
@mcot_rayon						varchar(1) 		=Null,
@mshpblfob						decimal(14, 2) 		=Null,
@mnetfobamt						decimal(14, 2) 		=Null,
@mremark1						varchar(60) 		=Null,
@mstatus						varchar(20) 		=Null,
@mshipdate						datetime 		=Null,
@maepcdt						datetime 		=Null,
@mdrwrecddt						datetime 		=Null,
@mnegdate						datetime 		=Null,
@mmarkrate						decimal(14, 2) 		=Null,
@mcntrycode						varchar(20) 		=Null,
@mrealstdt						datetime 		=Null,
@musha						varchar(1) 		=Null,
@mugts						varchar(1) 		=Null,
@mprintdt						datetime 		=Null,
@mpercent						decimal(14, 2) 		=Null,
@mplusamt						decimal(14, 2) 		=Null,
@mcntryname						varchar(25) 		=Null,
@mdays						decimal(14, 2) 		=Null,
@mdue_date						datetime 		=Null,
@maccounted						varchar(10) 		=Null,
@maccountamt						decimal(14, 2) 		=Null,
@mmode						varchar(7) 		=Null,
@mShipRate						decimal(14, 2) 		=Null,
@mcourier						varchar(20) 		=Null,
@mdocawb						varchar(15) 		=Null,
@mdocsentdt						datetime 		=Null,
@madvdocdt						datetime 		=Null,
@minspdate						datetime 		=Null,
@madvdocdays						decimal(14, 2) 		=Null,
@mtokenno						varchar(10) 		=Null,
@mtokendt						datetime 		=Null,
@mqlfbonus						decimal(14, 2) 		=Null,
@mmidamount						decimal(14, 2) 		=Null,
@mdepb						varchar(1) 		=Null,
@mdepbamt						decimal(14, 2) 		=Null,
@mdepbrcvd						decimal(14, 2) 		=Null,
@mdepbrcvdon						datetime 		=Null,
@mdepbper						decimal(14, 2) 		=Null,
@mlicencenr						varchar(20) 		=Null,
@mlicenceamt						decimal(14, 2) 		=Null,
@mlicsoldon						datetime 		=Null,
@mlicsoldfor						decimal(14, 2) 		=Null,
@mdepbappldt						datetime 		=Null,
@mport						varchar(255) 		=Null,
@mepcopydt						datetime 		=Null,
@mforwarder						varchar(5) 		=Null,
@mforbillno						varchar(12) 		=Null,
@mforbillamt						decimal(14, 2) 		=Null,
@mforrcvdt						datetime 		=Null,
@msentforver						datetime 		=Null,
@mverifieddt						datetime 		=Null,
@msuppnr						varchar(15) 		=Null,
@msuppbbnnr						varchar(15) 		=Null,
@mwarehouse						varchar(20) 		=Null,
@mswiftcode						varchar(15) 		=Null,
@mCorrespondingBank						varchar(20) 		=Null,
@mCorrespondingBankAcount						varchar(50) 		=Null,
@mCorrespondingBankSwiftCode						varchar(20) 		=Null,
@mMsrks9						varchar(50) 		=Null,
@mMarks10						varchar(50) 		=Null,
@mSTRPercentage						decimal(3, 2) 		=Null,
@mSTRAmount						decimal(16, 2) 		=Null,
@mPAN_Number						varchar(20) 		=Null,
@mVAT_TIN						varchar(50) 		=Null,
@mCST_TIN						varchar(50) 		=Null,
@mExciseDuty						decimal(16, 2) 		=Null,
@mVATAmount						decimal(16, 2) 		=Null,
@mCessAmount						decimal(16, 2) 		=Null,
@mEduCessAmount						decimal(16, 2) 		=Null,
@mBuyerGroup						varchar(30) 		=Null,
@mMinusAmount						decimal(16, 2) 		=Null,
@mFinancialYear						varchar(10) 		=Null,
@mInvoiceType						varchar(10) 		=Null,
@mShipped						bit 		=Null,
@mIsShipped						bit 		=Null,
@mShippedDate						datetime 		=Null,
@mShippedBy						varchar(50) 		=Null,
@mMarkToShipDoneDate						datetime 		=Null,
@mPaymentReceiveFromBuyer						bit 		=Null,
@mPaymentReceiveDate						date 		=Null,
@mBankRefNo						varchar(20) 		=Null,
@mBankRefDate						datetime 		=Null,
@mContractNo						varchar(50) 		=Null,
@mContractDate						datetime 		=Null,
@mExciseInvoiceNo						varchar(25) 		=Null,
@mIsAdvanceReceived						bit 		=Null,
@mAdvanceAmount						decimal(14, 2) 		=Null,
@mCreatedBy						varchar(100) 		=Null,
@mCreatedDate						datetime 		=Null,
@mModifiedBy						varchar(100) 		=Null,
@mModifiedDate						datetime 		=Null,
@mEnteredOnMachineID						varchar(50) 		=Null,
@mHandoverDate						datetime 		=Null,
@mHangerPack						varchar(200) 		=Null,
@mCovering						varchar(200) 		=Null,
@mDeclaration						varchar(200) 		=Null,
@mPostDescription						varchar(200) 		=Null,
@mLCDescription						varchar(200) 		=Null,
@mMark11						varchar(50) 		=Null,
@mMark12						varchar(50) 		=Null,
@mDiscountUpCharge						varchar(50) 		=Null,
@mPercentage						decimal(14, 2) 		=Null,
@mAmount						decimal(14, 2) 		=Null,
@mShipperLC						varchar(50) 		=Null,
@mBuyerLC						varchar(50) 		=Null,
@mFOBValueInCurrency						decimal(14, 2) 		=Null,
@mTaxType						varchar(20) 		=Null,
@mIsRecalRequired						bit 		=Null,
@mIsApproved						bit 		=Null,
@mApprovedBy						varchar(50) 		=Null,
@mApprovedOn						datetime 		=Null,
@mModuleName						varchar(50) 		=Null,
@mID						varchar(50)  		=Null,
@mConsigneeCode						varchar(50) 		=Null,
@mNotify1						varchar(50) 		=Null,
@mNotify2						varchar(50) 		=Null,
@mNotify3						varchar(50) 		=Null,
@mAuthSignEmpCode						varchar(20) 		=Null,
@mAuthSignEmpName						varchar(50) 		=Null,
@mAuthSignDesi						varchar(50) 		=Null,
@mCSTorVAT						decimal(16, 2) 		=Null,
@mEduCess						decimal(16, 2) 		=Null,
@mCESS						decimal(16, 2) 		=Null,
@mExcise						decimal(16, 2) 		=Null,
@mCT3No						varchar(50) 		=Null,
@mCT3Date						datetime 		=Null,
@mARENo						varchar(20) 		=Null,
@mAREDate						datetime 		=Null,
@mConveredBy						varchar(50) 		=Null,
@mDeclaration1						varchar(200) 		=Null,
@mContainerSize						varchar(20) 		=Null,
@mContainerName						varchar(50) 		=Null,
@mContainerSealNo						varchar(20) 		=Null,
@mGoodsDescription						varchar(200) 		=Null,
@mMarksAndNos						varchar(200) 		=Null,
@mNoAndKindOfPackages						varchar(200) 		=Null,
@mPreCarriageBy						varchar(50) 		=Null,
@mPreCarrierRecvPlace						varchar(50) 		=Null,
@mPortDischarge						varchar(50) 		=Null,
@mDestinationCountry						varchar(20) 		=Null,
@mAssortmentYear						varchar(20) 		=Null,
@mPaymentTerms						varchar(50) 		=Null,
@mDeliveryTerms						varchar(50) 		=Null,
@mAREDuty						decimal(18, 2) 		=Null,
@mAreCess						decimal(18, 2) 		=Null,
@mAREHCess						decimal(18, 2) 		=Null,
@mContainerNo						varchar(50) 		=Null,
@mFromPackNo						varchar(30) 		=Null,
@mToPackNo						varchar(30) 		=Null,
@mEmailToCustDate						date 		=Null,
@mFreightFwdrPlotLetterDate						date 		=Null,
@mContainerApplnDate						date 		=Null,
@mGSPSlNo						varchar(20) 		=Null,
@mOriginCriterion						varchar(20) 		=Null,
@mStuffingDate						date 		=Null,
@mGateOpeningDate						date 		=Null,
@mCutOffDate						date 		=Null,
@mClosingDate						date 		=Null,
@mSailingVesselDetails						varchar(200) 		=Null,
@mShipmentType						varchar(20) 		=Null,
@mRevShipmentType						varchar(20) 		=Null,
@mHaltingCharges						decimal(18, 2) 		=Null,
@mDemurrage						decimal(18, 2) 		=Null,
@mVoyageNo						varchar(20) 		=Null,
@mETDFeederVessel						date 		=Null,
@mETAFeederVessel						date 		=Null,
@mFeederVessel						varchar(50) 		=Null,
@mFeederVoyageNo						varchar(20) 		=Null,
@mETAMotherVessel						date 		=Null,
@mETDMotherVessel						date 		=Null,
@mInternalSealNo						varchar(20) 		=Null,
@mCentralExciseSealNo						varchar(20) 		=Null,
@mTypesOfBL						varchar(50) 		=Null,
@mCustomClearence						varchar(200) 		=Null,
@mTransportCharges						decimal(18, 2) 		=Null,
@mCHACharges						decimal(18, 2) 		=Null,
@mClearingCharges						decimal(18, 2) 		=Null,
@mCFSCharges						decimal(18, 2) 		=Null,
@mForwardingCharges						decimal(18, 2) 		=Null,
@mGSPCharges						decimal(18, 2) 		=Null,
@mCourierCharges						decimal(18, 2) 		=Null,
@mInsuranceCharges						decimal(18, 2) 		=Null,
@mMiscCharges						decimal(18, 2) 		=Null,
@mContainerArrivalDate						date 		=Null,
@mMemo						varchar(200) 		=Null,
@mDestinationArrivalDate						date 		=Null,
@mFreightFwdrPlotLetterExpDate						date 		=Null,
@mForwarderCode						varchar(50) 		=Null,
@mCHACode						varchar(50) 		=Null,
@mRevVessel						varchar(20) 		=Null,
@mRevVoyageNo						varchar(20) 		=Null,
@mRevETDMotherVessel						date 		=Null,
@mRevETAMotherVessel						date 		=Null,
@mRevFeederVessel						varchar(20) 		=Null,
@mRevFeederVoyageNo						varchar(20) 		=Null,
@mRevETDFeederVessel						date 		=Null,
@mRevETAFeederVessel						date 		=Null,
@mCommodity						varchar(20) 		=Null,
@mPremiumRate						decimal(18, 2) 		=Null,
@mPremiumAmount						decimal(18, 2) 		=Null,
@mInvoiceNoAutoGen						varchar(50) 		=Null,
@mCourierPayment						varchar(50) 		=Null,
@mCourierNo						varchar(50) 		=Null,
@mCourierDate						date 		=Null,
@mCartonDia						varchar(25) 		=Null,
@mOneCarton						varchar(20) 		=Null,
@mTotalOne						varchar(20) 		=Null,
@mFinalDestination						varchar(25) 		=Null,
@mMatType						varchar(25) 		=Null,
@mAmtOfDutyPayable						decimal(16, 2) 		=Null,
@mSubTotal						decimal(16, 2) 		=Null,
@mInvYear						varchar(10) 		=Null,
@mInvCode						varchar(10) 		=Null,
@mDispatchFrom						varchar(25) 		=Null,
@mInoviceStatus						varchar(20) 		=Null,
@mExpDocDate						datetime 		=Null,
@mGoodsDescription2						varchar(100) 		=Null,
@mRBBankName						varchar(50) 		=Null,
@mRBAccountNo						varchar(20) 		=Null,
@mRBSwiftCode						varchar(20) 		=Null,
@mCurValue						decimal(18, 2) 		=Null,
@mAnnexureAPortOfLoading						varchar(50) 		=Null,
@mLCID						varchar(50) 		=Null,
@mLCValue						decimal(14, 2) 		=Null,
@mShippedLCValue						decimal(14, 2) 		=Null,
@mCGSTPercentage						decimal(18, 2) 		=Null,
@mCGSTValue						decimal(18, 2) 		=Null,
@mSGSTPercentage						decimal(18, 2) 		=Null,
@mSGSTVlaue						decimal(18, 2) 		=Null,
@mIGSTPercentage						decimal(18, 2) 		=Null,
@mIGSTValue						decimal(18, 2) 		=Null,
@mFreightCharges						decimal(18, 2) 		=Null,
@mLoadingCharges						decimal(18, 2) 		=Null,
@mInsuranceChager						decimal(18, 2) 		=Null,
@mOtherCharges						decimal(18, 2) 		=Null,
@mDiscount						decimal(18, 2) 		=Null,
@mGSTTotalValue						decimal(18, 2) 		=Null,
@mGSTInvNo						varchar(20) 		=Null,
@mInvNo2						varchar(20) 		=Null,
@mInvNo3						varchar(20) 		=Null,
@mDUMMYINVDATE						datetime 		=Null,
@mFreightCGSTPer						decimal(18, 2) 		=Null,
@mFreightCGSTVal						decimal(18, 2) 		=Null,
@mFreightSGSTPer						decimal(18, 2) 		=Null,
@mFreightSGSTVal						decimal(18, 2) 		=Null,
@mFreightIGSTPer						decimal(18, 2) 		=Null,
@mFreightIGSTVal						decimal(18, 2) 		=Null,
@mFreightTotalVal						decimal(18, 2) 		=Null,
@mGSTValue						decimal(18, 2) 		=Null,
@mSerialNoPrefix						varchar(20) 		=Null,
@mInternalOrder						varchar(20) 		=Null,


@mtype						varchar(1) =NULL,
@msubordno						varchar(20) =NULL,
@msmpstyl						varchar(20) =NULL,
@mColor						varchar(20) =NULL,
@mshortshp						decimal(14, 2) =NULL,
@mrt						varchar(36) =NULL,
@mratioqty						decimal(14, 2) =NULL,
@mCurrencyConversionRate						decimal(14, 2) =NULL,
@mcategory						varchar(7) =NULL,
@mbuyrdept						varchar(50) =NULL,
@mvalue						decimal(14, 2) =NULL,
@mcert_no						varchar(24) =NULL,
@mperiod						varchar(2) =NULL,
@mcert_type						varchar(6) =NULL,
@mcartalloc						varchar(1) =NULL,
@mannexno						varchar(5) =NULL,
@manserial						varchar(2) =NULL,
@mawb_bl_no						varchar(30) =NULL,
@mshbilldt						datetime =NULL,
@mcatii						varchar(7) =NULL,
@mcatiiconv						decimal(14, 2) =NULL,
@mBuyerOrderNo						varchar(20) =NULL,
@mprovordnr						varchar(20) =NULL,
@mCountryCode						varchar(5) =NULL,
@msmpdesc						varchar(50) =NULL,
@mndate						datetime =NULL,
@mrvdate						datetime =NULL,
@mmrate						decimal(14, 2) =NULL,
@mrsno						varchar(10) =NULL,
@mMaterialCode						varchar(50) =NULL,
@mGroup						varchar(50) =NULL,
@mcommpercnt						decimal(14, 2) =NULL,
@mmainfab						varchar(1) =NULL,
@mcountry						varchar(15) =NULL,
@mbasestyl						varchar(20) =NULL,
@mOrderSerialNo						varchar(50) =NULL,
@mLCDiscount						decimal(10, 2) =NULL,
@mCustomerStyleName						varchar(50) =NULL,
@mStore						varchar(5) =NULL,
@mSeason						varchar(6) =NULL,
@mburdept						varchar(5) =NULL,
@mVariant						varchar(10) =NULL,
@mCustomerArticleNo						varchar(50) =NULL,
@mMerchandiser						varchar(50) =NULL,
@mCount						varchar(50) =NULL,
@mconstruction						varchar(50) =NULL,
@mMRPRate						decimal(14, 2) =NULL,
@mOrderPrice						decimal(18, 2) =NULL,
@mDCNo						varchar(50) =NULL,
@mpcs						int =NULL,
@mCustWorkOrderNO						varchar(50) =NULL,
@mArticleCodification						varchar(20) =NULL,
@mInvoiceQuantity						decimal(16, 2) =NULL,
@mCBMSpace						decimal(16, 4) =NULL,
@mOrderQty						decimal(14, 2) =NULL,
@mCurrencyConversionRate4Tally						decimal(14, 2) =NULL,
@mcurvalue4Tally						decimal(14, 2) =NULL,
@mSGSTValue						decimal(18, 2) =NULL,
@mHSNCode						varchar(20) =NULL,
@mFValue						decimal(14, 2) =NULL,
@mLastInvDate						datetime =NULL,
@mReady2DispatchID						varchar(50) =NULL,
@mIsSampleOrder						bit =NULL,
@mSampleOrderType						varchar(50) =NULL,
@mArticleandColor						varchar(150) =NULL,
@mNetValue						decimal(14, 2) =NULL,
@mShippedQuantity				Int				=Null,
@mOrderStatus					Varchar(50)		=Null


 	


AS
DECLARE @nCode int
DECLARE @nCodeDtl int
Declare @nErr int
DECLARE @idoc int

If @mAction= 'SELR2DISP'
BEGIN
	SELECT 
		Cast('' As Bit) As [Select],R2D.SpoolId,		R2D.SpoolDate,		R2D.Buyer,
	R2D.Quantity,					R2D.InvoiceNo,		R2D.InvoiceDate,	SO.OrderType,		
	SOD.ID As SalesOrderDetailId,	SO.ID As SalesOrderId,					R2D.ArticleNo,
	mat.MaterialTypeCode,			MAT.Description,
	R2D.Size01,			R2D.Quantity01,				R2D.Size02,				R2D.Quantity02,
	R2D.Size03,			R2D.Quantity03,				R2D.Size04,				R2D.Quantity04,
	R2D.Size05,			R2D.Quantity05,				R2D.Size06,				R2D.Quantity06,
	R2D.Size07,			R2D.Quantity07,				R2D.Size08,				R2D.Quantity08,
	R2D.Size09,			R2D.Quantity09,				R2D.Size10,				R2D.Quantity10,
	R2D.Size11,			R2D.Quantity11,				R2D.Size12,				R2D.Quantity12,
	R2D.Size13,			R2D.Quantity13,				R2D.Size14,				R2D.Quantity14,
	R2D.Size15,			R2D.Quantity15,				R2D.Size16,				R2D.Quantity16,
	R2D.Size17,			R2D.Quantity17,				R2D.Size18,				R2D.Quantity18,
	R2D.JobcardNo,		R2D.JobcardDetailId,		R2D.ID,					R2D.JobcardNo,
	SOD.Type
	
	FROM
	ReadyToDispatch As R2D, SalesOrderDetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE
	R2D.SalesOrderDetailID = SOD.ID And
	SOD.SalesOrderID = SO.ID And R2D.ArticleNo = MAT.MaterialCode And
	Cast(R2D.SpoolDate As Date) >= @mFromDate And
	Cast(R2D.SpoolDate As Date) <= @mToDate
	
	ORDER BY
	R2D.Buyer,			R2D.SpoolId,		R2D.SpoolDate
END	

If @mAction= 'SELR2DISPTBP'
BEGIN
	SELECT 
	Cast('' As Bit) As [Select],	R2D.SpoolId,		R2D.SpoolDate,		R2D.Buyer,
	R2D.Quantity,					R2D.InvoiceNo,		R2D.InvoiceDate,	SO.OrderType,		
	SOD.ID As SalesOrderDetailId,	SO.ID As SalesOrderId,					R2D.ArticleNo,
	mat.MaterialTypeCode,			MAT.Description,
	R2D.Size01,			R2D.Quantity01,				R2D.Size02,				R2D.Quantity02,
	R2D.Size03,			R2D.Quantity03,				R2D.Size04,				R2D.Quantity04,
	R2D.Size05,			R2D.Quantity05,				R2D.Size06,				R2D.Quantity06,
	R2D.Size07,			R2D.Quantity07,				R2D.Size08,				R2D.Quantity08,
	R2D.Size09,			R2D.Quantity09,				R2D.Size10,				R2D.Quantity10,
	R2D.Size11,			R2D.Quantity11,				R2D.Size12,				R2D.Quantity12,
	R2D.Size13,			R2D.Quantity13,				R2D.Size14,				R2D.Quantity14,
	R2D.Size15,			R2D.Quantity15,				R2D.Size16,				R2D.Quantity16,
	R2D.Size17,			R2D.Quantity17,				R2D.Size18,				R2D.Quantity18,
	R2D.JobcardNo,		R2D.JobcardDetailId,		R2D.ID,					R2D.JobcardNo,
	SOD.Type
	
	FROM
	ReadyToDispatch As R2D, SalesOrderDetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE
	R2D.SalesOrderDetailID = SOD.ID And
	SOD.SalesOrderID = SO.ID And R2D.ArticleNo = MAT.MaterialCode And
	Cast(R2D.SpoolDate As Date) >= @mFromDate And
	Cast(R2D.SpoolDate As Date) <= @mToDate And
	R2D.InvoiceNo = ''
	
	ORDER BY
	R2D.Buyer,			R2D.SpoolId,		R2D.SpoolDate
END	

If @mAction= 'SELR2DISPP'
BEGIN
	SELECT 
	Cast('' As Bit) As [Select],	R2D.SpoolId,		R2D.SpoolDate,		R2D.Buyer,
	R2D.Quantity,					R2D.InvoiceNo,		R2D.InvoiceDate,	SO.OrderType,		
	SOD.ID As SalesOrderDetailId,	SO.ID As SalesOrderId,					R2D.ArticleNo,
	mat.MaterialTypeCode,			MAT.Description,
	R2D.Size01,			R2D.Quantity01,				R2D.Size02,				R2D.Quantity02,
	R2D.Size03,			R2D.Quantity03,				R2D.Size04,				R2D.Quantity04,
	R2D.Size05,			R2D.Quantity05,				R2D.Size06,				R2D.Quantity06,
	R2D.Size07,			R2D.Quantity07,				R2D.Size08,				R2D.Quantity08,
	R2D.Size09,			R2D.Quantity09,				R2D.Size10,				R2D.Quantity10,
	R2D.Size11,			R2D.Quantity11,				R2D.Size12,				R2D.Quantity12,
	R2D.Size13,			R2D.Quantity13,				R2D.Size14,				R2D.Quantity14,
	R2D.Size15,			R2D.Quantity15,				R2D.Size16,				R2D.Quantity16,
	R2D.Size17,			R2D.Quantity17,				R2D.Size18,				R2D.Quantity18,
	R2D.JobcardNo,		R2D.JobcardDetailId,		R2D.ID,					R2D.JobcardNo,
	SOD.Type
	
	FROM
	ReadyToDispatch As R2D, SalesOrderDetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE
	R2D.SalesOrderDetailID = SOD.ID And
	SOD.SalesOrderID = SO.ID And R2D.ArticleNo = MAT.MaterialCode And
	Cast(R2D.SpoolDate As Date) >= @mFromDate And
	Cast(R2D.SpoolDate As Date) <= @mToDate And
	R2D.InvoiceNo <> ''
	
	ORDER BY
	R2D.Buyer,			R2D.SpoolId,		R2D.SpoolDate
END	

If @mAction= 'SELR2DISPC'
BEGIN
	SELECT 
	Cast('' As Bit) As [Select],	R2D.SpoolId,		R2D.SpoolDate,		R2D.Buyer,
	R2D.Quantity,					R2D.InvoiceNo,		R2D.InvoiceDate,	SO.OrderType,		
	SOD.ID As SalesOrderDetailId,	SO.ID As SalesOrderId,					R2D.ArticleNo,
	mat.MaterialTypeCode,			MAT.Description,
	R2D.Size01,			R2D.Quantity01,				R2D.Size02,				R2D.Quantity02,
	R2D.Size03,			R2D.Quantity03,				R2D.Size04,				R2D.Quantity04,
	R2D.Size05,			R2D.Quantity05,				R2D.Size06,				R2D.Quantity06,
	R2D.Size07,			R2D.Quantity07,				R2D.Size08,				R2D.Quantity08,
	R2D.Size09,			R2D.Quantity09,				R2D.Size10,				R2D.Quantity10,
	R2D.Size11,			R2D.Quantity11,				R2D.Size12,				R2D.Quantity12,
	R2D.Size13,			R2D.Quantity13,				R2D.Size14,				R2D.Quantity14,
	R2D.Size15,			R2D.Quantity15,				R2D.Size16,				R2D.Quantity16,
	R2D.Size17,			R2D.Quantity17,				R2D.Size18,				R2D.Quantity18,
	R2D.JobcardNo,		R2D.JobcardDetailId,		R2D.ID,					R2D.JobcardNo,
	SOD.Type
	
	FROM
	ReadyToDispatch As R2D, SalesOrderDetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE
	R2D.SalesOrderDetailID = SOD.ID And
	SOD.SalesOrderID = SO.ID And R2D.ArticleNo = MAT.MaterialCode And
	Cast(R2D.SpoolDate As Date) >= @mFromDate And
	Cast(R2D.SpoolDate As Date) <= @mToDate And
	R2D.BuyerGroup = @mBuyerGroup
	
	ORDER BY
	R2D.Buyer,			R2D.SpoolId,		R2D.SpoolDate
END	

If @mAction= 'SELR2DISPTBPC'
BEGIN
	SELECT 
	Cast('' As Bit) As [Select],	R2D.SpoolId,		R2D.SpoolDate,		R2D.Buyer,
	R2D.Quantity,					R2D.InvoiceNo,		R2D.InvoiceDate,	SO.OrderType,		
	SOD.ID As SalesOrderDetailId,	SO.ID As SalesOrderId,					R2D.ArticleNo,
	mat.MaterialTypeCode,			MAT.Description,
	R2D.Size01,			R2D.Quantity01,				R2D.Size02,				R2D.Quantity02,
	R2D.Size03,			R2D.Quantity03,				R2D.Size04,				R2D.Quantity04,
	R2D.Size05,			R2D.Quantity05,				R2D.Size06,				R2D.Quantity06,
	R2D.Size07,			R2D.Quantity07,				R2D.Size08,				R2D.Quantity08,
	R2D.Size09,			R2D.Quantity09,				R2D.Size10,				R2D.Quantity10,
	R2D.Size11,			R2D.Quantity11,				R2D.Size12,				R2D.Quantity12,
	R2D.Size13,			R2D.Quantity13,				R2D.Size14,				R2D.Quantity14,
	R2D.Size15,			R2D.Quantity15,				R2D.Size16,				R2D.Quantity16,
	R2D.Size17,			R2D.Quantity17,				R2D.Size18,				R2D.Quantity18,
	R2D.JobcardNo,		R2D.JobcardDetailId,		R2D.ID,					R2D.JobcardNo,
	SOD.Type
	
	FROM
	ReadyToDispatch As R2D, SalesOrderDetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE
	R2D.SalesOrderDetailID = SOD.ID And
	SOD.SalesOrderID = SO.ID And R2D.ArticleNo = MAT.MaterialCode And
	Cast(R2D.SpoolDate As Date) >= @mFromDate And
	Cast(R2D.SpoolDate As Date) <= @mToDate And
	R2D.InvoiceNo = '' And
	R2D.BuyerGroup = @mBuyerGroup
	
	ORDER BY
	R2D.Buyer,			R2D.SpoolId,		R2D.SpoolDate
END	

If @mAction= 'SELR2DISPPC'
BEGIN
	SELECT 
	Cast('' As Bit) As [Select],	R2D.SpoolId,		R2D.SpoolDate,		R2D.Buyer,
	R2D.Quantity,					R2D.InvoiceNo,		R2D.InvoiceDate,	SO.OrderType,		
	SOD.ID As SalesOrderDetailId,	SO.ID As SalesOrderId,					R2D.ArticleNo,
	mat.MaterialTypeCode,			MAT.Description,
	R2D.Size01,			R2D.Quantity01,				R2D.Size02,				R2D.Quantity02,
	R2D.Size03,			R2D.Quantity03,				R2D.Size04,				R2D.Quantity04,
	R2D.Size05,			R2D.Quantity05,				R2D.Size06,				R2D.Quantity06,
	R2D.Size07,			R2D.Quantity07,				R2D.Size08,				R2D.Quantity08,
	R2D.Size09,			R2D.Quantity09,				R2D.Size10,				R2D.Quantity10,
	R2D.Size11,			R2D.Quantity11,				R2D.Size12,				R2D.Quantity12,
	R2D.Size13,			R2D.Quantity13,				R2D.Size14,				R2D.Quantity14,
	R2D.Size15,			R2D.Quantity15,				R2D.Size16,				R2D.Quantity16,
	R2D.Size17,			R2D.Quantity17,				R2D.Size18,				R2D.Quantity18,
	R2D.JobcardNo,		R2D.JobcardDetailId,		R2D.ID,					R2D.JobcardNo,
	SOD.Type
	
	FROM
	ReadyToDispatch As R2D, SalesOrderDetails As SOD, SalesOrder As SO, Materials As MAT
	
	WHERE
	R2D.SalesOrderDetailID = SOD.ID And
	SOD.SalesOrderID = SO.ID And R2D.ArticleNo = MAT.MaterialCode And
	Cast(R2D.SpoolDate As Date) >= @mFromDate And
	Cast(R2D.SpoolDate As Date) <= @mToDate And
	R2D.InvoiceNo <> '' And
	R2D.BuyerGroup = @mBuyerGroup
	
	ORDER BY
	R2D.Buyer,			R2D.SpoolId,		R2D.SpoolDate
END	

IF @mAction='LOADCUSTOMER'
BEGIN
	Select '' As BuyerGroup,' ALL CUSTOMERS' As BuyerName
	UNION
	Select DISTINCT TOP (100) PERCENT R2D.BuyerGroup,buy.BuyerName
	From ReadyToDispatch As R2D, Buyer As Buy
	Where r2d.BuyerGroup = buy.BuyerCode
	And Cast(R2D.SpoolDate As Date) >= @mFromDate And Cast(R2D.SpoolDate As Date) <= @mToDate
	ORDER BY  BuyerName	
END

IF @mAction='LOADSIGNATORY'
BEGIN
	Select 
	Empcode, EmpFullName + ' - ' + Designation As EmpName 
	
	from Employee 
	Where EmpCode in ('SS-00008','KH-00501','SL-00013') 
	Order by Designation
END

IF @mAction='LOADCANREASON'
BEGIN
	Select 
	FullName_
 
	
	from AbbrevTable 
	Where  Group_ = 'REASONFORINVCANCEL'
	Order by FullName_
END

If @mAction='INSINVMAIN'
BEGIN
	INSERT INTO
	INVOICE(InvoiceNo,InvoiceDate,Buyer,Account,Shipper,Origin,LCNo,Marks1,Marks4,Marks7,Marks8,ModeOfShipment,Destination,Bank,Currency,
		CurrencyConversion,Nature,Quantity,TotalValue,Freight,Insurance,TotalPackNo,NetWeight,GrossWeight,remark1,[status],shipdate,cntrycode,printdt,
		[percent],plusamt,cntryname,[days],due_date,accounted,depb,depbamt,depbrcvd,depbrcvdon,depbper,licsoldfor,port,forbillamt,warehouse,PAN_Number,
		VAT_TIN,CST_TIN,ExciseDuty,VATAmount,CessAmount,EduCessAmount,BuyerGroup,MinusAmount,FinancialYear,InvoiceType,Shipped,IsShipped,ShippedDate,
		ExciseInvoiceNo,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,EnteredOnMachineID,Percentage,Amount,ModuleName,ID,ConsigneeCode,Notify1,AuthSignEmpCode,
		AuthSignEmpName,AuthSignDesi,CSTorVAT,EduCess,CESS,Excise,CT3No,CT3Date,ARENo,AREDate,ConveredBy,ContainerSealNo,GoodsDescription,MarksAndNos,PortDischarge,
		DestinationCountry,PaymentTerms,FromPackNo,ToPackNo,GSPSlNo,CartonDia,OneCarton,TotalOne,FinalDestination,MatType,AmtOfDutyPayable,SubTotal,InvYear,
		InvCode,LCID,LCValue,ShippedLCValue,CGSTPercentage,CGSTValue,SGSTPercentage,SGSTVlaue,IGSTPercentage,IGSTValue,FreightCharges,GSTTotalValue,GSTInvNo,
		InvNo2,InvNo3,FreightCGSTPer,FreightCGSTVal,FreightSGSTPer,FreightSGSTVal,FreightIGSTPer,FreightIGSTVal,FreightTotalVal,GSTValue,SerialNoPrefix,InternalOrder)

	VALUES
	(@mInvoiceNo,@mInvoiceDate,@mBuyer,@mAccount,@mShipper,@mOrigin,@mLCNo,@mMarks1,@mMarks4,@mMarks7,@mMarks8,@mModeOfShipment,@mDestination,@mBank,@mCurrency,
		@mCurrencyConversion,@mNature,@mQuantity,@mTotalValue,@mFreight,@mInsurance,@mTotalPackNo,@mNetWeight,@mGrossWeight,@mremark1,@mstatus,@mshipdate,@mcntrycode,@mprintdt,
		@mpercent,@mplusamt,@mcntryname,@mdays,@mdue_date,@maccounted,@mdepb,@mdepbamt,@mdepbrcvd,@mdepbrcvdon,@mdepbper,@mlicsoldfor,@mport,@mforbillamt,@mwarehouse,@mPAN_Number,
		@mVAT_TIN,@mCST_TIN,@mExciseDuty,@mVATAmount,@mCessAmount,@mEduCessAmount,@mBuyerGroup,@mMinusAmount,@mFinancialYear,@mInvoiceType,@mShipped,@mIsShipped,@mShippedDate,
		@mExciseInvoiceNo,@mCreatedBy,@mCreatedDate,@mModifiedBy,@mModifiedDate,@mEnteredOnMachineID,@mPercentage,@mAmount,@mModuleName,@mID,@mConsigneeCode,@mNotify1,@mAuthSignEmpCode,
		@mAuthSignEmpName,@mAuthSignDesi,@mCSTorVAT,@mEduCess,@mCESS,@mExcise,@mCT3No,@mCT3Date,@mARENo,@mAREDate,@mConveredBy,@mContainerSealNo,@mGoodsDescription,@mMarksAndNos,@mPortDischarge,
		@mDestinationCountry,@mPaymentTerms,@mFromPackNo,@mToPackNo,@mGSPSlNo,@mCartonDia,@mOneCarton,@mTotalOne,@mFinalDestination,@mMatType,@mAmtOfDutyPayable,@mSubTotal,@mInvYear,
		@mInvCode,@mLCID,@mLCValue,@mShippedLCValue,@mCGSTPercentage,@mCGSTValue,@mSGSTPercentage,@mSGSTVlaue,@mIGSTPercentage,@mIGSTValue,@mFreightCharges,@mGSTTotalValue,@mGSTInvNo,
		@mInvNo2,@mInvNo3,@mFreightCGSTPer,@mFreightCGSTVal,@mFreightSGSTPer,@mFreightSGSTVal,@mFreightIGSTPer,@mFreightIGSTVal,@mFreightTotalVal,@mGSTValue,@mSerialNoPrefix,@mInternalOrder)
END

If @mAction='INSINVDTL'
BEGIN
	INSERT INTO
	InvoiceDetail(invoiceno,InvoiceDate,InvoiceSerialNo,buyer,shipper,SalesOrderNo,[type],ArticleNo,rate,quantity,ratioqty,currency,CurrencyConversionRate,category,value,
		size1,qty1,size2,qty2,size3,qty3,size4,qty4,size5,qty5,size6,qty6,size7,qty7,size8,qty8,size9,qty9,size10,qty10,CountryCode,curvalue,size11,size12,size13,size14,
		size15,size16,size17,size18,qty11,qty12,qty13,qty14,qty15,qty16,qty17,qty18,BuyerGroup,IsShipped,burdept,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,
		EnteredOnMachineID,JobCardNo,ModuleName,JobCardDetailID,InvoiceID,ID,SalesOrderDetailID,CGSTPercentage,CGSTValue,SGSTPercentage,SGSTValue,IGSTPercentage,
		IGSTValue,HSNCode,WareHouse,FValue,Ready2DispatchID,InternalOrder,IsSampleOrder,SampleOrderType,ArticleandColor,NetValue)

	VALUES
	(@minvoiceno,@mInvoiceDate,@mInvoiceSerialNo,@mbuyer,@mshipper,@mSalesOrderNo,@mtype,@mArticleNo,@mrate,@mquantity,@mratioqty,@mcurrency,@mCurrencyConversionRate,@mcategory,@mvalue,
		@mSize01,@mQuantity01,@msize02,@mQuantity02,@msize03,@mQuantity03,@msize04,@mQuantity04,@msize05,@mQuantity05,@msize06,@mQuantity06,@msize07,@mQuantity07,@msize08,@mQuantity08,@msize09,@mQuantity09,@msize10,@mQuantity10,@mCountryCode,@mcurvalue,@msize11,@msize12,@msize13,@msize14,
		@msize15,@msize16,@msize17,@msize18,@mQuantity11,@mQuantity12,@mQuantity13,@mQuantity14,@mQuantity15,@mQuantity16,@mQuantity17,@mQuantity18,@mBuyerGroup,@mIsShipped,@mburdept,@mCreatedBy,@mCreatedDate,@mModifiedBy,@mModifiedDate,
		@mEnteredOnMachineID,@mJobCardNo,@mModuleName,@mJobCardDetailID,@mInvoiceID,@mID,@mSalesOrderDetailID,@mCGSTPercentage,@mCGSTValue,@mSGSTPercentage,@mSGSTValue,@mIGSTPercentage,
		@mIGSTValue,@mHSNCode,@mWareHouse,@mFValue,@mReady2DispatchID,@mInternalOrder,@mIsSampleOrder,@mSampleOrderType,@mArticleandColor,@mNetValue)
END



If @mAction='UPDINVMAIN'
BEGIN
	UPDATE
	INVOICE
	
	SET
	Quantity = @mQuantity,
	TotalValue = @mTotalValue,				CGSTPercentage = @mCGSTPercentage,		CGSTValue = @mCGSTValue,
	SGSTPercentage = @mSGSTPercentage,		SGSTVlaue = @mSGSTVlaue,
	IGSTPercentage = @mIGSTPercentage,		IGSTValue = @mIGSTValue,
	GSTTotalValue = @mGSTTotalValue,		Forbillamt = @mForBillAmt

	WHERE
	InvoiceNo = @mInvoiceNo

END
--Select * from INvoice
If @mAction='UPDSOD'
BEGIN
	UPDATE
	SalesOrderDetails

	Set
	ShippedQuantity = @mShippedQuantity,		OrderStatus = @mOrderStatus,
	LastInvDate = @mLastInvDate

	Where
	ID = @mID
END

If @mAction='UPDSO'
BEGIN
	UPDATE
	SalesOrder

	Set
	TotalShippedQuantity = @mShippedQuantity,		OrderStatus = @mOrderStatus

	Where
	SalesOrderNo = @mSalesOrderNo
END

If @mAction='UPDR2D'
BEGIN
	UPDATE
	ReadyToDispatch

	Set
	invoiceno = @mInvoiceNo,		Invoicedate = @mInvoiceDate

	Where
	ID = @mID
END

If @mAction='UPDPKGDTL'
BEGIN
	UPDATE
	PackingDetail

	Set
	invoiceno = @mInvoiceNo,		InvoiceID = @mInvoiceID,
	WIPLocation = 'DISPATCHED',		Invyear = @mInvYear

	Where
	JobcardNo = @mJobcardNo		And ReadyToDispatch=1 And
	(ISNULL(InvoiceNo,0) = '0' Or InvoiceNo='')
END

If @mAction='SELPKGDTL'
BEGIN
	Select * From
	PackingDetail

	WHERE
	invoiceno = @mInvoiceNo			And		InvoiceID = @mInvoiceID		AND
	JobcardNo = @mJobcardNo			And		DCCartonNo Is Null
	
	Order by CartonNo	
END


If @mAction='UPDPKGDTLBOXNO'
BEGIN
	UPDATE
	PackingDetail

	SET
	DCCartonNo = (SELECT IsNull(Max(DCCartonNo),0) + 1 From PackingDetail Where InvYear = @mInvYear)

	Where
	Id = @mID
	
	
END
If @mAction='UPDPKGDTLDLT'
BEGIN
	UPDATE
	PackingDetail

	Set
	invoiceno = '',		InvoiceID = '',
	WIPLocation = 'PACKING'

	Where
	InvoiceNo= @mInvoiceNo
END

If @mAction='UPDR2DDLT'
BEGIN
	UPDATE
	ReadyToDispatch

	Set
	invoiceno = '',		Invoicedate = @mInvoiceDate

	Where
	invoiceno = @mInvoiceNo
END

If @mAction='DELINVOICEDTL'
BEGIN
	DELETE FROM
	INVOICEDETAIL

	Where
	invoiceno = @mInvoiceNo
END

If @mAction='DELINVOICEMAIN'
BEGIN
	DELETE FROM
	INVOICE

	Where
	invoiceno = @mInvoiceNo
END

If @mAction='CANCELINVOICEMAIN'
BEGIN
	UPDATE
	INVOICE

	SET
	[Status] = 'CANCELLED', 
	Remark1 = @mRemark1 

	Where
	invoiceno = @mInvoiceNo
END

if @mAction='SELJOBCARD'
BEGIN
	SELECT CAST('1' as bit) as [Select], Jobcardno,cartonno,quantity from PackingDetail 
	where JobCardNo=@mJobCardNo
	 order by CartonNo

End

If @mAction='EDIINVMAIN'
BEGIN
	UPDATE
	INVOICE
	
	SET
	AREDate = @mAREDate,					ARENo = @mARENo,					AuthSignDesi = @mAuthSignDesi,				AuthSignEmpCode = @mAuthSignEmpCode,			
	AuthSignEmpName = @mAuthSignEmpName,	CartonDia = @mCartonDia,			ContainerName = @mContainerName,			ContainerNo = @mContainerNo,
	ContainerSealNo = @mContainerSealNo,	ContainerSize = @mContainerSize,	Declaration = @mDeclaration,				Declaration1 = @mDeclaration1,
	DestinationCountry = @mDestinationCountry,Discount = @mDiscount,			FinalDestination = @mFinalDestination,		FreightCharges = @mFreightCharges,
	FromPackNo = @mFromPackNo,				GoodsDescription = @mGoodsDescription,InsuranceChager = @mInsuranceChager,		LoadingCharges = @mLoadingCharges,
	MarksAndNos = @mMarksAndNos,			MatType = @mMatType,				NoAndKindOfPackages = @mNoAndKindOfPackages,OneCarton = @mOneCarton,
	OtherCharges = @mOtherCharges,			PaymentTerms = @mPaymentTerms,		port = @mport,								PortDischarge = @mPortDischarge,
	PreCarriageBy = @mPreCarriageBy,		PreCarrierRecvPlace = @mPreCarrierRecvPlace,ToPackNo = @mToPackNo,				TotalOne = @mTotalOne,
	ConsigneeCode = @mConsigneeCode,		cntrycode = @mCountryCode,			cntryname = @mcountry		


	WHERE
	InvoiceNo = @mInvoiceNo
END

IF @mAction='LOADConsigneeCode'
BEGIN
		Select DISTINCT TOP (100) PERCENT BuyerCode,BuyerCode + ' [:] '+ BuyerName As BuyerName,BuyerGroupCode,CountryCode,CountryName
	From Buyer 
	Where BuyerGroupCode = @mBuyerGroup
	ORDER BY  BuyerGroupCode	

END








GO
