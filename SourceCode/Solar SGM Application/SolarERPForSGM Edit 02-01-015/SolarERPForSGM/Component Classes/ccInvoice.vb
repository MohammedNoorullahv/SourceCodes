Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strInvoice

    Dim InvoiceNo As String
    Dim InvoiceDate As Date
    Dim Buyer As String
    Dim Account As String
    Dim Shipper As String
    Dim BuyerDepartment As String
    Dim Origin As String
    Dim LCNo As String
    Dim InvoiceDescription As String
    Dim ShippingBillNo As String
    Dim ShippingBillDate As Date
    Dim Marks1 As String
    Dim Marks2 As String
    Dim Marks3 As String
    Dim Marks4 As String
    Dim Marks5 As String
    Dim Marks6 As String
    Dim Marks7 As String
    Dim Marks8 As String
    Dim ModeOfShipment As String
    Dim AwbBillNo As String
    Dim AwbBillDate As Date
    Dim HAwbBillNo As String
    Dim HAwbBillDate As Date
    Dim Destination As String
    Dim BanckAccount As String
    Dim Vessel As String
    Dim Bank As String
    Dim Currency As String
    Dim CurrencyConversion As Decimal
    Dim Nature As String
    Dim Quantity As Decimal
    Dim TotalValue As Decimal
    Dim Freight As Decimal
    Dim Insurance As Decimal
    Dim AgentPercentage As Decimal
    Dim Commission As Decimal
    Dim DbkPercentage As Decimal
    Dim DrawBack As Decimal
    Dim BankCertificate As String
    Dim BankAmount As Decimal
    Dim BankRate As Decimal
    Dim CertificateDate As Date
    Dim TotalPackNo As Decimal
    Dim harmnsdco As String
    Dim NetWeight As Decimal
    Dim GrossWeight As Decimal
    Dim dbkrecd As Decimal
    Dim aepccer_no As String
    Dim cust_clear As Date
    Dim rbicode As String
    Dim cot_rayon As String
    Dim shpblfob As Decimal
    Dim netfobamt As Decimal
    Dim remark1 As String
    Dim status As String
    Dim shipdate As Date
    Dim aepcdt As Date
    Dim drwrecddt As Date
    Dim negdate As Date
    Dim markrate As Decimal
    Dim cntrycode As String
    Dim realstdt As Date
    Dim usha As String
    Dim ugts As String
    Dim printdt As Date
    Dim percent As Decimal
    Dim plusamt As Decimal
    Dim cntryname As String
    Dim days As Decimal
    Dim due_date As Date
    Dim accounted As String
    Dim accountamt As Decimal
    Dim mode As String
    Dim ShipRate As Decimal
    Dim courier As String
    Dim docawb As String
    Dim docsentdt As Date
    Dim advdocdt As Date
    Dim inspdate As Date
    Dim advdocdays As Decimal
    Dim tokenno As String
    Dim tokendt As Date
    Dim qlfbonus As Decimal
    Dim midamount As Decimal
    Dim depb As String
    Dim depbamt As Decimal
    Dim depbrcvd As Decimal
    Dim depbrcvdon As Date
    Dim depbper As Decimal
    Dim licencenr As String
    Dim licenceamt As Decimal
    Dim licsoldon As Date
    Dim licsoldfor As Decimal
    Dim depbappldt As Date
    Dim port As String
    Dim epcopydt As Date
    Dim forwarder As String
    Dim forbillno As String
    Dim forbillamt As Decimal
    Dim forrcvdt As Date
    Dim sentforver As Date
    Dim verifieddt As Date
    Dim suppnr As String
    Dim suppbbnnr As String
    Dim warehouse As String
    Dim swiftcode As String
    Dim CorrespondingBank As String
    Dim CorrespondingBankAcount As String
    Dim CorrespondingBankSwiftCode As String
    Dim Msrks9 As String
    Dim Marks10 As String
    Dim STRPercentage As Decimal
    Dim STRAmount As Decimal
    Dim PAN_Number As String
    Dim VAT_TIN As String
    Dim CST_TIN As String
    Dim ExciseDuty As Decimal
    Dim VATAmount As Decimal
    Dim CessAmount As Decimal
    Dim EduCessAmount As Decimal
    Dim BuyerGroup As String
    Dim MinusAmount As Decimal
    Dim FinancialYear As String
    Dim InvoiceType As String
    Dim Shipped As Integer
    Dim IsShipped As Integer
    Dim ShippedDate As Date
    Dim ShippedBy As String
    Dim MarkToShipDoneDate As Date
    Dim PaymentReceiveFromBuyer As Integer
    Dim PaymentReceiveDate As Date
    Dim BankRefNo As String
    Dim BankRefDate As Date
    Dim ContractNo As String
    Dim ContractDate As Date
    Dim ExciseInvoiceNo As String
    Dim IsAdvanceReceived As Integer
    Dim AdvanceAmount As Decimal
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As String
    Dim ModifiedDate As Date
    Dim EnteredOnMachineID As String
    Dim HandoverDate As Date
    Dim HangerPack As String
    Dim Covering As String
    Dim Declaration As String
    Dim PostDescription As String
    Dim LCDescription As String
    Dim Mark11 As String
    Dim Mark12 As String
    Dim DiscountUpCharge As String
    Dim Percentage As Decimal
    Dim Amount As Decimal
    Dim ShipperLC As String
    Dim BuyerLC As String
    Dim FOBValueInCurrency As Decimal
    Dim TaxType As String
    Dim IsRecalRequired As Integer
    Dim IsApproved As Integer
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim ModuleName As String
    Dim UpdateMode As String
    Dim ID As String
    Dim ConsigneeCode As String
    Dim Notify1 As String
    Dim Notify2 As String
    Dim Notify3 As String
    Dim AuthSignEmpCode As String
    Dim AuthSignEmpName As String
    Dim AuthSignDesi As String
    Dim CSTorVAT As Decimal
    Dim EduCess As Decimal
    Dim CESS As Decimal
    Dim Excise As Decimal
    Dim CT3No As String
    Dim CT3Date As Date
    Dim ARENo As String
    Dim AREDate As Date
    Dim ConveredBy As String
    Dim Declaration1 As String
    Dim ContainerSize As String
    Dim ContainerName As String
    Dim ContainerSealNo As String
    Dim GoodsDescription As String
    Dim MarksAndNos As String
    Dim NoAndKindOfPackages As String
    Dim PreCarriageBy As String
    Dim PreCarrierRecvPlace As String
    Dim PortDischarge As String
    Dim DestinationCountry As String
    Dim AssortmentYear As String
    Dim PaymentTerms As String
    Dim DeliveryTerms As String
    Dim AREDuty As Decimal
    Dim AreCess As Decimal
    Dim AREHCess As Decimal
    Dim ContainerNo As String
    Dim FromPackNo As String
    Dim ToPackNo As String
    Dim EmailToCustDate As Date
    Dim FreightFwdrPlotLetterDate As Date
    Dim ContainerApplnDate As Date
    Dim GSPSlNo As String
    Dim OriginCriterion As String
    Dim StuffingDate As Date
    Dim GateOpeningDate As Date
    Dim CutOffDate As Date
    Dim ClosingDate As Date
    Dim SailingVesselDetails As String
    Dim ShipmentType As String
    Dim RevShipmentType As String
    Dim HaltingCharges As Decimal
    Dim Demurrage As Decimal
    Dim VoyageNo As String
    Dim ETDFeederVessel As Date
    Dim ETAFeederVessel As Date
    Dim FeederVessel As String
    Dim FeederVoyageNo As String
    Dim ETAMotherVessel As Date
    Dim ETDMotherVessel As Date
    Dim InternalSealNo As String
    Dim CentralExciseSealNo As String
    Dim TypesOfBL As String
    Dim CustomClearence As String
    Dim TransportCharges As Decimal
    Dim CHACharges As Decimal
    Dim ClearingCharges As Decimal
    Dim CFSCharges As Decimal
    Dim ForwardingCharges As Decimal
    Dim GSPCharges As Decimal
    Dim CourierCharges As Decimal
    Dim InsuranceCharges As Decimal
    Dim MiscCharges As Decimal
    Dim ContainerArrivalDate As Date
    Dim Memo As String
    Dim DestinationArrivalDate As Date
    Dim FreightFwdrPlotLetterExpDate As Date
    Dim ForwarderCode As String
    Dim CHACode As String
    Dim RevVessel As String
    Dim RevVoyageNo As String
    Dim RevETDMotherVessel As Date
    Dim RevETAMotherVessel As Date
    Dim RevFeederVessel As String
    Dim RevFeederVoyageNo As String
    Dim RevETDFeederVessel As Date
    Dim RevETAFeederVessel As Date
    Dim Commodity As String
    Dim PremiumRate As Decimal
    Dim PremiumAmount As Decimal
    Dim InvoiceNoAutoGen As String
    Dim CourierPayment As String
    Dim CourierNo As String
    Dim CourierDate As Date
    Dim CartonDia As String
    Dim OneCarton As String
    Dim TotalOne As String
    Dim FinalDestination As String
    Dim MatType As String
    Dim AmtOfDutyPayable As Decimal
    Dim SubTotal As Decimal
    Dim InvYear As String
    Dim InvCode As String
    Dim DispatchFrom As String
    Dim InoviceStatus As String
    Dim ExpDocDate As Date
    Dim GoodsDescription2 As String
    Dim RBBankName As String
    Dim RBAccountNo As String
    Dim RBSwiftCode As String
    Dim CurValue As Decimal
    Dim AnnexureAPortOfLoading As String
    Dim LCID As String
    Dim LCValue As Decimal
    Dim ShippedLCValue As Decimal
    Dim CGSTPercentage As Decimal
    Dim CGSTValue As Decimal
    Dim SGSTPercentage As Decimal
    Dim SGSTVlaue As Decimal
    Dim IGSTPercentage As Decimal
    Dim IGSTValue As Decimal
    Dim FreightCharges As Decimal
    Dim LoadingCharges As Decimal
    Dim InsuranceChager As Decimal
    Dim OtherCharges As Decimal
    Dim Discount As Decimal
    Dim GSTTotalValue As Decimal
    Dim GSTInvNo As String
    Dim InvNo2 As String
    Dim InvNo3 As String
    Dim DUMMYINVDATE As Date
    Dim FreightCGSTPer As Decimal
    Dim FreightCGSTVal As Decimal
    Dim FreightSGSTPer As Decimal
    Dim FreightSGSTVal As Decimal
    Dim FreightIGSTPer As Decimal
    Dim FreightIGSTVal As Decimal
    Dim FreightTotalVal As Decimal
    Dim GSTValue As Decimal
    Dim SerialNoPrefix As String
    Dim InternalOrder As String
    Dim InvoiceValue As Decimal
    Dim TCSPercentage As Decimal
    Dim TCSValue As Decimal
    Dim GSTNo As String

End Structure

Public Structure strInvoiceDetails
    Dim invoiceno As String
    Dim InvoiceDate As Date
    Dim InvoiceSerialNo As String
    Dim buyer As String
    Dim shipper As String
    Dim SalesOrderNo As String
    Dim type As String
    Dim subordno As String
    Dim ArticleNo As String
    Dim smpstyl As String
    Dim Color As String
    Dim rate As Decimal
    Dim quantity As Decimal
    Dim shortshp As Decimal
    Dim rt As String
    Dim ratioqty As Decimal
    Dim currency As String
    Dim CurrencyConversionRate As Decimal
    Dim category As String
    Dim buyrdept As String
    Dim bankrate As Decimal
    Dim value As Decimal
    Dim size1 As String
    Dim qty1 As Decimal
    Dim size2 As String
    Dim qty2 As Decimal
    Dim size3 As String
    Dim qty3 As Decimal
    Dim size4 As String
    Dim qty4 As Decimal
    Dim size5 As String
    Dim qty5 As Decimal
    Dim size6 As String
    Dim qty6 As Decimal
    Dim size7 As String
    Dim qty7 As Decimal
    Dim size8 As String
    Dim qty8 As Decimal
    Dim size9 As String
    Dim qty9 As Decimal
    Dim size10 As String
    Dim qty10 As Decimal
    Dim inspdate As Date
    Dim cert_no As String
    Dim period As String
    Dim cert_type As String
    Dim cartalloc As String
    Dim annexno As String
    Dim anserial As String
    Dim awb_bl_no As String
    Dim lcno As String
    Dim shbilldt As Date
    Dim catii As String
    Dim catiiconv As Decimal
    Dim BuyerOrderNo As String
    Dim provordnr As String
    Dim CountryCode As String
    Dim smpdesc As String
    Dim ndate As Date
    Dim rvdate As Date
    Dim mrate As Decimal
    Dim usha As String
    Dim ugts As String
    Dim rsno As String
    Dim MaterialCode As String
    Dim mode As String
    Dim Group As String
    Dim bank As String
    Dim commpercnt As Decimal
    Dim mainfab As String
    Dim country As String
    Dim basestyl As String
    Dim curvalue As Decimal
    Dim size11 As String
    Dim size12 As String
    Dim size13 As String
    Dim size14 As String
    Dim size15 As String
    Dim size16 As String
    Dim size17 As String
    Dim size18 As String
    Dim qty11 As Decimal
    Dim qty12 As Decimal
    Dim qty13 As Decimal
    Dim qty14 As Decimal
    Dim qty15 As Decimal
    Dim qty16 As Decimal
    Dim qty17 As Decimal
    Dim qty18 As Decimal
    Dim OrderSerialNo As String
    Dim BuyerGroup As String
    Dim LCDiscount As Decimal
    Dim Shipped As Integer 'bit
    Dim CustomerStyleName As String
    Dim IsShipped As Integer 'bit
    Dim Status As String
    Dim PaymentREceiveFromBuyer As Integer 'bit
    Dim Store As String
    Dim Season As String
    Dim burdept As String
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As String
    Dim ModifiedDate As Date
    Dim EnteredOnMachineID As String
    Dim sVariant As String
    Dim CustomerArticleNo As String
    Dim InvoiceDescription As String
    Dim Merchandiser As String
    Dim PackNo As String
    Dim JobCardNo As String
    Dim Count As String
    Dim construction As String
    Dim CartonNo As String
    Dim MRPRate As Decimal
    Dim IsRecalRequired As Integer 'bit
    Dim OrderPrice As Decimal
    Dim IsApproved As Integer 'bit
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim ModuleName As String
    Dim JobCardDetailID As String
    Dim InvoiceID As String
    Dim UpdateMode As String
    Dim ID As String
    Dim DCNo As String
    Dim pcs As Integer
    Dim SalesOrderDetailID As String
    Dim CustWorkOrderNO As String
    Dim InvoiceNoAutoGen As String
    Dim ArticleCodification As String
    Dim InvoiceQuantity As Decimal
    Dim CBMSpace As Decimal
    Dim OrderQty As Decimal
    Dim CurrencyConversionRate4Tally As Decimal
    Dim curvalue4Tally As Decimal
    Dim CGSTPercentage As Decimal
    Dim CGSTValue As Decimal
    Dim SGSTPercentage As Decimal
    Dim SGSTValue As Decimal
    Dim IGSTPercentage As Decimal
    Dim IGSTValue As Decimal
    Dim Discount As Decimal
    Dim HSNCode As String
    Dim WareHouse As String
    Dim FValue As Decimal
    Dim LastInvDate As Date
    Dim Ready2DispatchID As String
    Dim InternalOrder As String
    Dim IsSampleOrder As Integer 'bit
    Dim SampleOrderType As String
    Dim ArticleandColor As String
    Dim NettValue As Decimal
End Structure
#End Region

Public Class ccInvoice

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrAudit As String = Global.SolarERPForSGM.My.Settings.AHGroupAudit
    Dim sCnnAudit As New SqlConnection(sConstrAudit)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer
#End Region

#Region "Properties"

    ReadOnly Property ErrorCode() As String
        Get
            Return sErrCode
        End Get
    End Property

    ReadOnly Property ErrorMessage() As String
        Get
            Return sErrMsg
        End Get
    End Property

#End Region

    Dim sID As String
    Dim nProducedQuantity As Integer
#Region "Functions"

    Public Function LoadCustomers(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADCUSTOMER"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadConsigneeCode(ByVal sBuyerCode As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADConsigneeCode"
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = sBuyerCode

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadReadytoDispatch(ByVal sBuyerCode As String, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        If mdlSGM.sSelectOption = "AA" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELR2DISP"
        ElseIf mdlSGM.sSelectOption = "AP" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELR2DISPP"
        ElseIf mdlSGM.sSelectOption = "AN" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELR2DISPTBP"
        ElseIf mdlSGM.sSelectOption = "FA" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELR2DISPC"
        ElseIf mdlSGM.sSelectOption = "FP" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELR2DISPPC"
        ElseIf mdlSGM.sSelectOption = "FN" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELR2DISPTBPC"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = sBuyerCode
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadSignatureBy() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADSIGNATORY"
        
        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadReasonforCancel() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADCANREASON"

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function InsertInvoiceMain(ByVal oNV As strInvoice) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSINVMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = oNV.InvoiceDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyer", SqlDbType.VarChar)).Value() = oNV.Buyer
        sCmd.Parameters.Add(New SqlParameter("@mAccount", SqlDbType.VarChar)).Value() = oNV.Account
        sCmd.Parameters.Add(New SqlParameter("@mShipper", SqlDbType.VarChar)).Value() = oNV.Shipper
        sCmd.Parameters.Add(New SqlParameter("@mBuyerDepartment", SqlDbType.VarChar)).Value() = oNV.BuyerDepartment
        sCmd.Parameters.Add(New SqlParameter("@mOrigin", SqlDbType.VarChar)).Value() = oNV.Origin
        sCmd.Parameters.Add(New SqlParameter("@mLCNo", SqlDbType.VarChar)).Value() = oNV.LCNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDescription", SqlDbType.VarChar)).Value() = oNV.InvoiceDescription
        sCmd.Parameters.Add(New SqlParameter("@mShippingBillNo", SqlDbType.VarChar)).Value() = oNV.ShippingBillNo
        'sCmd.Parameters.Add(New SqlParameter("@mShippingBillDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ShippingBillDate
        sCmd.Parameters.Add(New SqlParameter("@mMarks1", SqlDbType.VarChar)).Value() = oNV.Marks1
        sCmd.Parameters.Add(New SqlParameter("@mMarks2", SqlDbType.VarChar)).Value() = oNV.Marks2
        sCmd.Parameters.Add(New SqlParameter("@mMarks3", SqlDbType.VarChar)).Value() = oNV.Marks3
        sCmd.Parameters.Add(New SqlParameter("@mMarks4", SqlDbType.VarChar)).Value() = oNV.Marks4
        sCmd.Parameters.Add(New SqlParameter("@mMarks5", SqlDbType.VarChar)).Value() = oNV.Marks5
        sCmd.Parameters.Add(New SqlParameter("@mMarks6", SqlDbType.VarChar)).Value() = oNV.Marks6
        sCmd.Parameters.Add(New SqlParameter("@mMarks7", SqlDbType.VarChar)).Value() = oNV.Marks7
        sCmd.Parameters.Add(New SqlParameter("@mMarks8", SqlDbType.VarChar)).Value() = oNV.Marks8
        sCmd.Parameters.Add(New SqlParameter("@mModeOfShipment", SqlDbType.VarChar)).Value() = oNV.ModeOfShipment
        sCmd.Parameters.Add(New SqlParameter("@mAwbBillNo", SqlDbType.VarChar)).Value() = oNV.AwbBillNo
        'sCmd.Parameters.Add(New SqlParameter("@mAwbBillDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.AwbBillDate
        sCmd.Parameters.Add(New SqlParameter("@mHAwbBillNo", SqlDbType.VarChar)).Value() = oNV.HAwbBillNo
        'sCmd.Parameters.Add(New SqlParameter("@mHAwbBillDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.HAwbBillDate
        sCmd.Parameters.Add(New SqlParameter("@mDestination", SqlDbType.VarChar)).Value() = oNV.Destination
        sCmd.Parameters.Add(New SqlParameter("@mBanckAccount", SqlDbType.VarChar)).Value() = oNV.BanckAccount
        sCmd.Parameters.Add(New SqlParameter("@mVessel", SqlDbType.VarChar)).Value() = oNV.Vessel
        sCmd.Parameters.Add(New SqlParameter("@mBank", SqlDbType.VarChar)).Value() = oNV.Bank
        sCmd.Parameters.Add(New SqlParameter("@mCurrency", SqlDbType.VarChar)).Value() = oNV.Currency
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversion", SqlDbType.Decimal)).Value() = oNV.CurrencyConversion
        sCmd.Parameters.Add(New SqlParameter("@mNature", SqlDbType.VarChar)).Value() = oNV.Nature
        sCmd.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
        sCmd.Parameters.Add(New SqlParameter("@mTotalValue", SqlDbType.Decimal)).Value() = oNV.TotalValue
        sCmd.Parameters.Add(New SqlParameter("@mFreight", SqlDbType.Decimal)).Value() = oNV.Freight
        sCmd.Parameters.Add(New SqlParameter("@mInsurance", SqlDbType.Decimal)).Value() = oNV.Insurance
        sCmd.Parameters.Add(New SqlParameter("@mAgentPercentage", SqlDbType.Decimal)).Value() = oNV.AgentPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCommission", SqlDbType.Decimal)).Value() = oNV.Commission
        sCmd.Parameters.Add(New SqlParameter("@mDbkPercentage", SqlDbType.Decimal)).Value() = oNV.DbkPercentage
        sCmd.Parameters.Add(New SqlParameter("@mDrawBack", SqlDbType.Decimal)).Value() = oNV.DrawBack
        sCmd.Parameters.Add(New SqlParameter("@mBankCertificate", SqlDbType.VarChar)).Value() = oNV.BankCertificate
        sCmd.Parameters.Add(New SqlParameter("@mBankAmount", SqlDbType.Decimal)).Value() = oNV.BankAmount
        sCmd.Parameters.Add(New SqlParameter("@mBankRate", SqlDbType.Decimal)).Value() = oNV.BankRate
        'sCmd.Parameters.Add(New SqlParameter("@mCertificateDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CertificateDate
        sCmd.Parameters.Add(New SqlParameter("@mTotalPackNo", SqlDbType.Decimal)).Value() = oNV.TotalPackNo
        sCmd.Parameters.Add(New SqlParameter("@mharmnsdco", SqlDbType.VarChar)).Value() = oNV.harmnsdco
        sCmd.Parameters.Add(New SqlParameter("@mNetWeight", SqlDbType.Decimal)).Value() = oNV.NetWeight
        sCmd.Parameters.Add(New SqlParameter("@mGrossWeight", SqlDbType.Decimal)).Value() = oNV.GrossWeight
        sCmd.Parameters.Add(New SqlParameter("@mdbkrecd", SqlDbType.Decimal)).Value() = oNV.dbkrecd
        sCmd.Parameters.Add(New SqlParameter("@maepccer_no", SqlDbType.VarChar)).Value() = oNV.aepccer_no
        'sCmd.Parameters.Add(New SqlParameter("@mcust_clear", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.cust_clear
        sCmd.Parameters.Add(New SqlParameter("@mrbicode", SqlDbType.VarChar)).Value() = oNV.rbicode
        sCmd.Parameters.Add(New SqlParameter("@mcot_rayon", SqlDbType.VarChar)).Value() = oNV.cot_rayon
        sCmd.Parameters.Add(New SqlParameter("@mshpblfob", SqlDbType.Decimal)).Value() = oNV.shpblfob
        sCmd.Parameters.Add(New SqlParameter("@mnetfobamt", SqlDbType.Decimal)).Value() = oNV.netfobamt
        sCmd.Parameters.Add(New SqlParameter("@mremark1", SqlDbType.VarChar)).Value() = oNV.remark1
        sCmd.Parameters.Add(New SqlParameter("@mstatus", SqlDbType.VarChar)).Value() = oNV.status
        'sCmd.Parameters.Add(New SqlParameter("@mshipdate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.shipdate
        'sCmd.Parameters.Add(New SqlParameter("@maepcdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.aepcdt
        'sCmd.Parameters.Add(New SqlParameter("@mdrwrecddt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value ''oNV.drwrecddt
        'sCmd.Parameters.Add(New SqlParameter("@mnegdate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.negdate
        sCmd.Parameters.Add(New SqlParameter("@mmarkrate", SqlDbType.Decimal)).Value() = oNV.markrate
        sCmd.Parameters.Add(New SqlParameter("@mcntrycode", SqlDbType.VarChar)).Value() = oNV.cntrycode
        'sCmd.Parameters.Add(New SqlParameter("@mrealstdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.realstdt
        sCmd.Parameters.Add(New SqlParameter("@musha", SqlDbType.VarChar)).Value() = oNV.usha
        sCmd.Parameters.Add(New SqlParameter("@mugts", SqlDbType.VarChar)).Value() = oNV.ugts
        sCmd.Parameters.Add(New SqlParameter("@mprintdt", SqlDbType.DateTime)).Value() = Date.Now
        sCmd.Parameters.Add(New SqlParameter("@mpercent", SqlDbType.Decimal)).Value() = oNV.percent
        sCmd.Parameters.Add(New SqlParameter("@mplusamt", SqlDbType.Decimal)).Value() = oNV.plusamt
        sCmd.Parameters.Add(New SqlParameter("@mcntryname", SqlDbType.VarChar)).Value() = oNV.cntryname
        sCmd.Parameters.Add(New SqlParameter("@mdays", SqlDbType.Decimal)).Value() = oNV.days
        'sCmd.Parameters.Add(New SqlParameter("@mdue_date", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.due_date
        sCmd.Parameters.Add(New SqlParameter("@maccounted", SqlDbType.VarChar)).Value() = oNV.accounted
        sCmd.Parameters.Add(New SqlParameter("@maccountamt", SqlDbType.Decimal)).Value() = oNV.accountamt
        sCmd.Parameters.Add(New SqlParameter("@mmode", SqlDbType.VarChar)).Value() = oNV.mode
        sCmd.Parameters.Add(New SqlParameter("@mShipRate", SqlDbType.Decimal)).Value() = oNV.ShipRate
        sCmd.Parameters.Add(New SqlParameter("@mcourier", SqlDbType.VarChar)).Value() = oNV.courier
        sCmd.Parameters.Add(New SqlParameter("@mdocawb", SqlDbType.VarChar)).Value() = oNV.docawb
        'sCmd.Parameters.Add(New SqlParameter("@mdocsentdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.docsentdt
        'sCmd.Parameters.Add(New SqlParameter("@madvdocdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.advdocdt
        'sCmd.Parameters.Add(New SqlParameter("@minspdate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.inspdate
        sCmd.Parameters.Add(New SqlParameter("@madvdocdays", SqlDbType.Decimal)).Value() = oNV.advdocdays
        sCmd.Parameters.Add(New SqlParameter("@mtokenno", SqlDbType.VarChar)).Value() = oNV.tokenno
        'sCmd.Parameters.Add(New SqlParameter("@mtokendt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.tokendt
        sCmd.Parameters.Add(New SqlParameter("@mqlfbonus", SqlDbType.Decimal)).Value() = oNV.qlfbonus
        sCmd.Parameters.Add(New SqlParameter("@mmidamount", SqlDbType.Decimal)).Value() = oNV.midamount
        sCmd.Parameters.Add(New SqlParameter("@mdepb", SqlDbType.VarChar)).Value() = oNV.depb
        sCmd.Parameters.Add(New SqlParameter("@mdepbamt", SqlDbType.Decimal)).Value() = oNV.depbamt
        sCmd.Parameters.Add(New SqlParameter("@mdepbrcvd", SqlDbType.Decimal)).Value() = oNV.depbrcvd
        'sCmd.Parameters.Add(New SqlParameter("@mdepbrcvdon", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.depbrcvdon
        sCmd.Parameters.Add(New SqlParameter("@mdepbper", SqlDbType.Decimal)).Value() = oNV.depbper
        sCmd.Parameters.Add(New SqlParameter("@mlicencenr", SqlDbType.VarChar)).Value() = oNV.licencenr
        sCmd.Parameters.Add(New SqlParameter("@mlicenceamt", SqlDbType.Decimal)).Value() = oNV.licenceamt
        'sCmd.Parameters.Add(New SqlParameter("@mlicsoldon", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value ' oNV.licsoldon
        sCmd.Parameters.Add(New SqlParameter("@mlicsoldfor", SqlDbType.Decimal)).Value() = oNV.licsoldfor
        'sCmd.Parameters.Add(New SqlParameter("@mdepbappldt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.depbappldt
        sCmd.Parameters.Add(New SqlParameter("@mport", SqlDbType.VarChar)).Value() = oNV.port
        'sCmd.Parameters.Add(New SqlParameter("@mepcopydt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.epcopydt
        sCmd.Parameters.Add(New SqlParameter("@mforwarder", SqlDbType.VarChar)).Value() = oNV.forwarder
        sCmd.Parameters.Add(New SqlParameter("@mforbillno", SqlDbType.VarChar)).Value() = oNV.forbillno
        sCmd.Parameters.Add(New SqlParameter("@mforbillamt", SqlDbType.Decimal)).Value() = oNV.forbillamt
        'sCmd.Parameters.Add(New SqlParameter("@mforrcvdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.forrcvdt
        'sCmd.Parameters.Add(New SqlParameter("@msentforver", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.sentforver
        'sCmd.Parameters.Add(New SqlParameter("@mverifieddt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.verifieddt
        sCmd.Parameters.Add(New SqlParameter("@msuppnr", SqlDbType.VarChar)).Value() = oNV.suppnr
        sCmd.Parameters.Add(New SqlParameter("@msuppbbnnr", SqlDbType.VarChar)).Value() = oNV.suppbbnnr
        sCmd.Parameters.Add(New SqlParameter("@mwarehouse", SqlDbType.VarChar)).Value() = oNV.warehouse
        sCmd.Parameters.Add(New SqlParameter("@mswiftcode", SqlDbType.VarChar)).Value() = oNV.swiftcode
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBank", SqlDbType.VarChar)).Value() = oNV.CorrespondingBank
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBankAcount", SqlDbType.VarChar)).Value() = oNV.CorrespondingBankAcount
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBankSwiftCode", SqlDbType.VarChar)).Value() = oNV.CorrespondingBankSwiftCode
        sCmd.Parameters.Add(New SqlParameter("@mMsrks9", SqlDbType.VarChar)).Value() = oNV.Msrks9
        sCmd.Parameters.Add(New SqlParameter("@mMarks10", SqlDbType.VarChar)).Value() = oNV.Marks10
        sCmd.Parameters.Add(New SqlParameter("@mSTRPercentage", SqlDbType.Decimal)).Value() = oNV.STRPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSTRAmount", SqlDbType.Decimal)).Value() = oNV.STRAmount
        sCmd.Parameters.Add(New SqlParameter("@mPAN_Number", SqlDbType.VarChar)).Value() = oNV.PAN_Number
        sCmd.Parameters.Add(New SqlParameter("@mVAT_TIN", SqlDbType.VarChar)).Value() = oNV.VAT_TIN
        sCmd.Parameters.Add(New SqlParameter("@mCST_TIN", SqlDbType.VarChar)).Value() = oNV.CST_TIN
        sCmd.Parameters.Add(New SqlParameter("@mExciseDuty", SqlDbType.Decimal)).Value() = oNV.ExciseDuty
        sCmd.Parameters.Add(New SqlParameter("@mVATAmount", SqlDbType.Decimal)).Value() = oNV.VATAmount
        sCmd.Parameters.Add(New SqlParameter("@mCessAmount", SqlDbType.Decimal)).Value() = oNV.CessAmount
        sCmd.Parameters.Add(New SqlParameter("@mEduCessAmount", SqlDbType.Decimal)).Value() = oNV.EduCessAmount
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
        sCmd.Parameters.Add(New SqlParameter("@mMinusAmount", SqlDbType.Decimal)).Value() = oNV.MinusAmount
        sCmd.Parameters.Add(New SqlParameter("@mFinancialYear", SqlDbType.VarChar)).Value() = oNV.FinancialYear
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceType", SqlDbType.VarChar)).Value() = oNV.InvoiceType
        sCmd.Parameters.Add(New SqlParameter("@mShipped", SqlDbType.Int)).Value() = oNV.Shipped
        sCmd.Parameters.Add(New SqlParameter("@mIsShipped", SqlDbType.Int)).Value() = oNV.IsShipped
        'sCmd.Parameters.Add(New SqlParameter("@mShippedDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ShippedDate
        sCmd.Parameters.Add(New SqlParameter("@mShippedBy", SqlDbType.VarChar)).Value() = oNV.ShippedBy
        'sCmd.Parameters.Add(New SqlParameter("@mMarkToShipDoneDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.MarkToShipDoneDate
        sCmd.Parameters.Add(New SqlParameter("@mPaymentReceiveFromBuyer", SqlDbType.Int)).Value() = oNV.PaymentReceiveFromBuyer
        'sCmd.Parameters.Add(New SqlParameter("@mPaymentReceiveDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.PaymentReceiveDate
        sCmd.Parameters.Add(New SqlParameter("@mBankRefNo", SqlDbType.VarChar)).Value() = oNV.BankRefNo
        'sCmd.Parameters.Add(New SqlParameter("@mBankRefDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.BankRefDate
        sCmd.Parameters.Add(New SqlParameter("@mContractNo", SqlDbType.VarChar)).Value() = oNV.ContractNo
        'sCmd.Parameters.Add(New SqlParameter("@mContractDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ContractDate
        sCmd.Parameters.Add(New SqlParameter("@mExciseInvoiceNo", SqlDbType.VarChar)).Value() = oNV.ExciseInvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mIsAdvanceReceived", SqlDbType.Int)).Value() = oNV.IsAdvanceReceived
        sCmd.Parameters.Add(New SqlParameter("@mAdvanceAmount", SqlDbType.Decimal)).Value() = oNV.AdvanceAmount
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
        'sCmd.Parameters.Add(New SqlParameter("@mHandoverDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.HandoverDate
        sCmd.Parameters.Add(New SqlParameter("@mHangerPack", SqlDbType.VarChar)).Value() = oNV.HangerPack
        sCmd.Parameters.Add(New SqlParameter("@mCovering", SqlDbType.VarChar)).Value() = oNV.Covering
        sCmd.Parameters.Add(New SqlParameter("@mDeclaration", SqlDbType.VarChar)).Value() = oNV.Declaration
        sCmd.Parameters.Add(New SqlParameter("@mPostDescription", SqlDbType.VarChar)).Value() = oNV.PostDescription
        sCmd.Parameters.Add(New SqlParameter("@mLCDescription", SqlDbType.VarChar)).Value() = oNV.LCDescription
        sCmd.Parameters.Add(New SqlParameter("@mMark11", SqlDbType.VarChar)).Value() = oNV.Mark11
        sCmd.Parameters.Add(New SqlParameter("@mMark12", SqlDbType.VarChar)).Value() = oNV.Mark12
        sCmd.Parameters.Add(New SqlParameter("@mDiscountUpCharge", SqlDbType.VarChar)).Value() = oNV.DiscountUpCharge
        sCmd.Parameters.Add(New SqlParameter("@mPercentage", SqlDbType.Decimal)).Value() = oNV.Percentage
        sCmd.Parameters.Add(New SqlParameter("@mAmount", SqlDbType.Decimal)).Value() = oNV.Amount
        sCmd.Parameters.Add(New SqlParameter("@mShipperLC", SqlDbType.VarChar)).Value() = oNV.ShipperLC
        sCmd.Parameters.Add(New SqlParameter("@mBuyerLC", SqlDbType.VarChar)).Value() = oNV.BuyerLC
        sCmd.Parameters.Add(New SqlParameter("@mFOBValueInCurrency", SqlDbType.Decimal)).Value() = oNV.FOBValueInCurrency
        sCmd.Parameters.Add(New SqlParameter("@mTaxType", SqlDbType.VarChar)).Value() = oNV.TaxType
        sCmd.Parameters.Add(New SqlParameter("@mIsRecalRequired", SqlDbType.Int)).Value() = oNV.IsRecalRequired
        sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Int)).Value() = oNV.IsApproved
        sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        'sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mConsigneeCode", SqlDbType.VarChar)).Value() = oNV.ConsigneeCode
        sCmd.Parameters.Add(New SqlParameter("@mNotify1", SqlDbType.VarChar)).Value() = oNV.Notify1
        sCmd.Parameters.Add(New SqlParameter("@mNotify2", SqlDbType.VarChar)).Value() = oNV.Notify2
        sCmd.Parameters.Add(New SqlParameter("@mNotify3", SqlDbType.VarChar)).Value() = oNV.Notify3
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignEmpCode", SqlDbType.VarChar)).Value() = oNV.AuthSignEmpCode
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignEmpName", SqlDbType.VarChar)).Value() = oNV.AuthSignEmpName
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignDesi", SqlDbType.VarChar)).Value() = oNV.AuthSignDesi
        sCmd.Parameters.Add(New SqlParameter("@mCSTorVAT", SqlDbType.Decimal)).Value() = oNV.CSTorVAT
        sCmd.Parameters.Add(New SqlParameter("@mEduCess", SqlDbType.Decimal)).Value() = oNV.EduCess
        sCmd.Parameters.Add(New SqlParameter("@mCESS", SqlDbType.Decimal)).Value() = oNV.CESS
        sCmd.Parameters.Add(New SqlParameter("@mExcise", SqlDbType.Decimal)).Value() = oNV.Excise
        sCmd.Parameters.Add(New SqlParameter("@mCT3No", SqlDbType.VarChar)).Value() = oNV.CT3No
        'sCmd.Parameters.Add(New SqlParameter("@mCT3Date", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CT3Date
        sCmd.Parameters.Add(New SqlParameter("@mARENo", SqlDbType.VarChar)).Value() = oNV.ARENo
        'sCmd.Parameters.Add(New SqlParameter("@mAREDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.AREDate
        sCmd.Parameters.Add(New SqlParameter("@mConveredBy", SqlDbType.VarChar)).Value() = oNV.ConveredBy
        sCmd.Parameters.Add(New SqlParameter("@mDeclaration1", SqlDbType.VarChar)).Value() = oNV.Declaration1
        sCmd.Parameters.Add(New SqlParameter("@mContainerSize", SqlDbType.VarChar)).Value() = oNV.ContainerSize
        sCmd.Parameters.Add(New SqlParameter("@mContainerName", SqlDbType.VarChar)).Value() = oNV.ContainerName
        sCmd.Parameters.Add(New SqlParameter("@mContainerSealNo", SqlDbType.VarChar)).Value() = oNV.ContainerSealNo
        sCmd.Parameters.Add(New SqlParameter("@mGoodsDescription", SqlDbType.VarChar)).Value() = oNV.GoodsDescription
        sCmd.Parameters.Add(New SqlParameter("@mMarksAndNos", SqlDbType.VarChar)).Value() = oNV.MarksAndNos
        sCmd.Parameters.Add(New SqlParameter("@mNoAndKindOfPackages", SqlDbType.VarChar)).Value() = oNV.NoAndKindOfPackages
        sCmd.Parameters.Add(New SqlParameter("@mPreCarriageBy", SqlDbType.VarChar)).Value() = oNV.PreCarriageBy
        sCmd.Parameters.Add(New SqlParameter("@mPreCarrierRecvPlace", SqlDbType.VarChar)).Value() = oNV.PreCarrierRecvPlace
        sCmd.Parameters.Add(New SqlParameter("@mPortDischarge", SqlDbType.VarChar)).Value() = oNV.PortDischarge
        sCmd.Parameters.Add(New SqlParameter("@mDestinationCountry", SqlDbType.VarChar)).Value() = oNV.DestinationCountry
        sCmd.Parameters.Add(New SqlParameter("@mAssortmentYear", SqlDbType.VarChar)).Value() = oNV.AssortmentYear
        sCmd.Parameters.Add(New SqlParameter("@mPaymentTerms", SqlDbType.VarChar)).Value() = oNV.PaymentTerms
        sCmd.Parameters.Add(New SqlParameter("@mDeliveryTerms", SqlDbType.VarChar)).Value() = oNV.DeliveryTerms
        sCmd.Parameters.Add(New SqlParameter("@mAREDuty", SqlDbType.Decimal)).Value() = oNV.AREDuty
        sCmd.Parameters.Add(New SqlParameter("@mAreCess", SqlDbType.Decimal)).Value() = oNV.AreCess
        sCmd.Parameters.Add(New SqlParameter("@mAREHCess", SqlDbType.Decimal)).Value() = oNV.AREHCess
        sCmd.Parameters.Add(New SqlParameter("@mContainerNo", SqlDbType.VarChar)).Value() = oNV.ContainerNo
        sCmd.Parameters.Add(New SqlParameter("@mFromPackNo", SqlDbType.VarChar)).Value() = oNV.FromPackNo
        sCmd.Parameters.Add(New SqlParameter("@mToPackNo", SqlDbType.VarChar)).Value() = oNV.ToPackNo
        'sCmd.Parameters.Add(New SqlParameter("@mEmailToCustDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.EmailToCustDate
        'sCmd.Parameters.Add(New SqlParameter("@mFreightFwdrPlotLetterDate", SqlDbType.DateTime)) - 12345)).Value() = oNV.FreightFwdrPlotLetterDate
        'sCmd.Parameters.Add(New SqlParameter("@mContainerApplnDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ContainerApplnDate
        sCmd.Parameters.Add(New SqlParameter("@mGSPSlNo", SqlDbType.VarChar)).Value() = oNV.GSPSlNo
        sCmd.Parameters.Add(New SqlParameter("@mOriginCriterion", SqlDbType.VarChar)).Value() = oNV.OriginCriterion
        'sCmd.Parameters.Add(New SqlParameter("@mStuffingDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.StuffingDate
        'sCmd.Parameters.Add(New SqlParameter("@mGateOpeningDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.GateOpeningDate
        'sCmd.Parameters.Add(New SqlParameter("@mCutOffDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CutOffDate
        'sCmd.Parameters.Add(New SqlParameter("@mClosingDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ClosingDate
        sCmd.Parameters.Add(New SqlParameter("@mSailingVesselDetails", SqlDbType.VarChar)).Value() = oNV.SailingVesselDetails
        sCmd.Parameters.Add(New SqlParameter("@mShipmentType", SqlDbType.VarChar)).Value() = oNV.ShipmentType
        sCmd.Parameters.Add(New SqlParameter("@mRevShipmentType", SqlDbType.VarChar)).Value() = oNV.RevShipmentType
        sCmd.Parameters.Add(New SqlParameter("@mHaltingCharges", SqlDbType.Decimal)).Value() = oNV.HaltingCharges
        sCmd.Parameters.Add(New SqlParameter("@mDemurrage", SqlDbType.Decimal)).Value() = oNV.Demurrage
        sCmd.Parameters.Add(New SqlParameter("@mVoyageNo", SqlDbType.VarChar)).Value() = oNV.VoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mETDFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETDFeederVessel
        'sCmd.Parameters.Add(New SqlParameter("@mETAFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETAFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mFeederVessel", SqlDbType.VarChar)).Value() = oNV.FeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mFeederVoyageNo", SqlDbType.VarChar)).Value() = oNV.FeederVoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mETAMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETAMotherVessel
        'sCmd.Parameters.Add(New SqlParameter("@mETDMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETDMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mInternalSealNo", SqlDbType.VarChar)).Value() = oNV.InternalSealNo
        sCmd.Parameters.Add(New SqlParameter("@mCentralExciseSealNo", SqlDbType.VarChar)).Value() = oNV.CentralExciseSealNo
        sCmd.Parameters.Add(New SqlParameter("@mTypesOfBL", SqlDbType.VarChar)).Value() = oNV.TypesOfBL
        sCmd.Parameters.Add(New SqlParameter("@mCustomClearence", SqlDbType.VarChar)).Value() = oNV.CustomClearence
        sCmd.Parameters.Add(New SqlParameter("@mTransportCharges", SqlDbType.Decimal)).Value() = oNV.TransportCharges
        sCmd.Parameters.Add(New SqlParameter("@mCHACharges", SqlDbType.Decimal)).Value() = oNV.CHACharges
        sCmd.Parameters.Add(New SqlParameter("@mClearingCharges", SqlDbType.Decimal)).Value() = oNV.ClearingCharges
        sCmd.Parameters.Add(New SqlParameter("@mCFSCharges", SqlDbType.Decimal)).Value() = oNV.CFSCharges
        sCmd.Parameters.Add(New SqlParameter("@mForwardingCharges", SqlDbType.Decimal)).Value() = oNV.ForwardingCharges
        sCmd.Parameters.Add(New SqlParameter("@mGSPCharges", SqlDbType.Decimal)).Value() = oNV.GSPCharges
        sCmd.Parameters.Add(New SqlParameter("@mCourierCharges", SqlDbType.Decimal)).Value() = oNV.CourierCharges
        sCmd.Parameters.Add(New SqlParameter("@mInsuranceCharges", SqlDbType.Decimal)).Value() = oNV.InsuranceCharges
        sCmd.Parameters.Add(New SqlParameter("@mMiscCharges", SqlDbType.Decimal)).Value() = oNV.MiscCharges
        'sCmd.Parameters.Add(New SqlParameter("@mContainerArrivalDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ContainerArrivalDate
        sCmd.Parameters.Add(New SqlParameter("@mMemo", SqlDbType.VarChar)).Value() = oNV.Memo
        'sCmd.Parameters.Add(New SqlParameter("@mDestinationArrivalDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.DestinationArrivalDate
        'sCmd.Parameters.Add(New SqlParameter("@mFreightFwdrPlotLetterExpDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.FreightFwdrPlotLetterExpDate
        sCmd.Parameters.Add(New SqlParameter("@mForwarderCode", SqlDbType.VarChar)).Value() = oNV.ForwarderCode
        sCmd.Parameters.Add(New SqlParameter("@mCHACode", SqlDbType.VarChar)).Value() = oNV.CHACode
        sCmd.Parameters.Add(New SqlParameter("@mRevVessel", SqlDbType.VarChar)).Value() = oNV.RevVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevVoyageNo", SqlDbType.VarChar)).Value() = oNV.RevVoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mRevETDMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETDMotherVessel
        'sCmd.Parameters.Add(New SqlParameter("@mRevETAMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETAMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevFeederVessel", SqlDbType.VarChar)).Value() = oNV.RevFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevFeederVoyageNo", SqlDbType.VarChar)).Value() = oNV.RevFeederVoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mRevETDFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETDFeederVessel
        'sCmd.Parameters.Add(New SqlParameter("@mRevETAFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETAFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mCommodity", SqlDbType.VarChar)).Value() = oNV.Commodity
        sCmd.Parameters.Add(New SqlParameter("@mPremiumRate", SqlDbType.Decimal)).Value() = oNV.PremiumRate
        sCmd.Parameters.Add(New SqlParameter("@mPremiumAmount", SqlDbType.Decimal)).Value() = oNV.PremiumAmount
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNoAutoGen", SqlDbType.VarChar)).Value() = oNV.InvoiceNoAutoGen
        sCmd.Parameters.Add(New SqlParameter("@mCourierPayment", SqlDbType.VarChar)).Value() = oNV.CourierPayment
        sCmd.Parameters.Add(New SqlParameter("@mCourierNo", SqlDbType.VarChar)).Value() = oNV.CourierNo
        'sCmd.Parameters.Add(New SqlParameter("@mCourierDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CourierDate
        sCmd.Parameters.Add(New SqlParameter("@mCartonDia", SqlDbType.VarChar)).Value() = oNV.CartonDia
        sCmd.Parameters.Add(New SqlParameter("@mOneCarton", SqlDbType.VarChar)).Value() = oNV.OneCarton
        sCmd.Parameters.Add(New SqlParameter("@mTotalOne", SqlDbType.VarChar)).Value() = oNV.TotalOne
        sCmd.Parameters.Add(New SqlParameter("@mFinalDestination", SqlDbType.VarChar)).Value() = oNV.FinalDestination
        sCmd.Parameters.Add(New SqlParameter("@mMatType", SqlDbType.VarChar)).Value() = oNV.MatType
        sCmd.Parameters.Add(New SqlParameter("@mAmtOfDutyPayable", SqlDbType.Decimal)).Value() = oNV.AmtOfDutyPayable
        sCmd.Parameters.Add(New SqlParameter("@mSubTotal", SqlDbType.Decimal)).Value() = oNV.SubTotal
        sCmd.Parameters.Add(New SqlParameter("@mInvYear", SqlDbType.VarChar)).Value() = oNV.InvYear
        sCmd.Parameters.Add(New SqlParameter("@mInvCode", SqlDbType.VarChar)).Value() = oNV.InvCode
        sCmd.Parameters.Add(New SqlParameter("@mDispatchFrom", SqlDbType.VarChar)).Value() = oNV.DispatchFrom
        sCmd.Parameters.Add(New SqlParameter("@mInoviceStatus", SqlDbType.VarChar)).Value() = oNV.InoviceStatus
        'sCmd.Parameters.Add(New SqlParameter("@mExpDocDate", SqlDbType.DateTime)))) - 12345)).Value() = System.DBNull.Value 'oNV.ExpDocDate
        sCmd.Parameters.Add(New SqlParameter("@mGoodsDescription2", SqlDbType.VarChar)).Value() = oNV.GoodsDescription2
        sCmd.Parameters.Add(New SqlParameter("@mRBBankName", SqlDbType.VarChar)).Value() = oNV.RBBankName
        sCmd.Parameters.Add(New SqlParameter("@mRBAccountNo", SqlDbType.VarChar)).Value() = oNV.RBAccountNo
        sCmd.Parameters.Add(New SqlParameter("@mRBSwiftCode", SqlDbType.VarChar)).Value() = oNV.RBSwiftCode
        sCmd.Parameters.Add(New SqlParameter("@mCurValue", SqlDbType.Decimal)).Value() = oNV.CurValue
        sCmd.Parameters.Add(New SqlParameter("@mAnnexureAPortOfLoading", SqlDbType.VarChar)).Value() = oNV.AnnexureAPortOfLoading
        sCmd.Parameters.Add(New SqlParameter("@mLCID", SqlDbType.VarChar)).Value() = oNV.LCID
        sCmd.Parameters.Add(New SqlParameter("@mLCValue", SqlDbType.Decimal)).Value() = oNV.LCValue
        sCmd.Parameters.Add(New SqlParameter("@mShippedLCValue", SqlDbType.Decimal)).Value() = oNV.ShippedLCValue
        sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSGSTVlaue", SqlDbType.Decimal)).Value() = oNV.SGSTVlaue
        sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mFreightCharges", SqlDbType.Decimal)).Value() = oNV.FreightCharges
        sCmd.Parameters.Add(New SqlParameter("@mLoadingCharges", SqlDbType.Decimal)).Value() = oNV.LoadingCharges
        sCmd.Parameters.Add(New SqlParameter("@mInsuranceChager", SqlDbType.Decimal)).Value() = oNV.InsuranceChager
        sCmd.Parameters.Add(New SqlParameter("@mOtherCharges", SqlDbType.Decimal)).Value() = oNV.OtherCharges
        sCmd.Parameters.Add(New SqlParameter("@mDiscount", SqlDbType.Decimal)).Value() = oNV.Discount
        sCmd.Parameters.Add(New SqlParameter("@mGSTTotalValue", SqlDbType.Decimal)).Value() = oNV.GSTTotalValue
        sCmd.Parameters.Add(New SqlParameter("@mGSTInvNo", SqlDbType.VarChar)).Value() = oNV.GSTInvNo
        sCmd.Parameters.Add(New SqlParameter("@mInvNo2", SqlDbType.VarChar)).Value() = oNV.InvNo2
        sCmd.Parameters.Add(New SqlParameter("@mInvNo3", SqlDbType.VarChar)).Value() = oNV.InvNo3
        'sCmd.Parameters.Add(New SqlParameter("@mDUMMYINVDATE", SqlDbType.DateTime)))) - 12345)).Value() = System.DBNull.Value 'oNV.DUMMYINVDATE
        sCmd.Parameters.Add(New SqlParameter("@mFreightCGSTPer", SqlDbType.Decimal)).Value() = oNV.FreightCGSTPer
        sCmd.Parameters.Add(New SqlParameter("@mFreightCGSTVal", SqlDbType.Decimal)).Value() = oNV.FreightCGSTVal
        sCmd.Parameters.Add(New SqlParameter("@mFreightSGSTPer", SqlDbType.Decimal)).Value() = oNV.FreightSGSTPer
        sCmd.Parameters.Add(New SqlParameter("@mFreightSGSTVal", SqlDbType.Decimal)).Value() = oNV.FreightSGSTVal
        sCmd.Parameters.Add(New SqlParameter("@mFreightIGSTPer", SqlDbType.Decimal)).Value() = oNV.FreightIGSTPer
        sCmd.Parameters.Add(New SqlParameter("@mFreightIGSTVal", SqlDbType.Decimal)).Value() = oNV.FreightIGSTVal
        sCmd.Parameters.Add(New SqlParameter("@mFreightTotalVal", SqlDbType.Decimal)).Value() = oNV.FreightTotalVal
        sCmd.Parameters.Add(New SqlParameter("@mGSTValue", SqlDbType.Decimal)).Value() = oNV.GSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSerialNoPrefix", SqlDbType.VarChar)).Value() = oNV.SerialNoPrefix
        sCmd.Parameters.Add(New SqlParameter("@mInternalOrder", SqlDbType.VarChar)).Value() = oNV.InternalOrder

        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function InsertInvoiceMaininAudit(ByVal oNV As strInvoice) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnnAudit
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSINVMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mShipper", SqlDbType.VarChar)).Value() = oNV.Shipper
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = oNV.InvoiceDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyer", SqlDbType.VarChar)).Value() = oNV.Buyer
        sCmd.Parameters.Add(New SqlParameter("@mAccount", SqlDbType.VarChar)).Value() = oNV.Account
        sCmd.Parameters.Add(New SqlParameter("@mBuyerDepartment", SqlDbType.VarChar)).Value() = oNV.BuyerDepartment
        sCmd.Parameters.Add(New SqlParameter("@mOrigin", SqlDbType.VarChar)).Value() = oNV.Origin
        sCmd.Parameters.Add(New SqlParameter("@mLCNo", SqlDbType.VarChar)).Value() = oNV.LCNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDescription", SqlDbType.VarChar)).Value() = oNV.InvoiceDescription
        sCmd.Parameters.Add(New SqlParameter("@mShippingBillNo", SqlDbType.VarChar)).Value() = oNV.ShippingBillNo
        'If Format(oNV.ShippingBillDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mShippingBillDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mShippingBillDate", SqlDbType.DateTime)).Value() = oNV.ShippingBillDate
        'End If
        'sCmd.Parameters.Add(New SqlParameter("@mShippingBillDate", SqlDbType.DateTime)).Value() = oNV.ShippingBillDate
        sCmd.Parameters.Add(New SqlParameter("@mMarks1", SqlDbType.VarChar)).Value() = oNV.Marks1
        sCmd.Parameters.Add(New SqlParameter("@mMarks2", SqlDbType.VarChar)).Value() = oNV.Marks2
        sCmd.Parameters.Add(New SqlParameter("@mMarks3", SqlDbType.VarChar)).Value() = oNV.Marks3
        sCmd.Parameters.Add(New SqlParameter("@mMarks4", SqlDbType.VarChar)).Value() = oNV.Marks4
        sCmd.Parameters.Add(New SqlParameter("@mMarks5", SqlDbType.VarChar)).Value() = oNV.Marks5
        sCmd.Parameters.Add(New SqlParameter("@mMarks6", SqlDbType.VarChar)).Value() = oNV.Marks6
        sCmd.Parameters.Add(New SqlParameter("@mMarks7", SqlDbType.VarChar)).Value() = oNV.Marks7
        sCmd.Parameters.Add(New SqlParameter("@mMarks8", SqlDbType.VarChar)).Value() = oNV.Marks8
        sCmd.Parameters.Add(New SqlParameter("@mModeOfShipment", SqlDbType.VarChar)).Value() = oNV.ModeOfShipment
        sCmd.Parameters.Add(New SqlParameter("@mAwbBillNo", SqlDbType.VarChar)).Value() = oNV.AwbBillNo
        'If Format(oNV.AwbBillDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mAwbBillDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mAwbBillDate", SqlDbType.DateTime)).Value() = oNV.AwbBillDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mHAwbBillNo", SqlDbType.VarChar)).Value() = oNV.HAwbBillNo
        'If Format(oNV.HAwbBillDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mHAwbBillDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mHAwbBillDate", SqlDbType.DateTime)).Value() = oNV.HAwbBillDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mDestination", SqlDbType.VarChar)).Value() = oNV.Destination
        sCmd.Parameters.Add(New SqlParameter("@mBanckAccount", SqlDbType.VarChar)).Value() = oNV.BanckAccount
        sCmd.Parameters.Add(New SqlParameter("@mVessel", SqlDbType.VarChar)).Value() = oNV.Vessel
        sCmd.Parameters.Add(New SqlParameter("@mBank", SqlDbType.VarChar)).Value() = oNV.Bank
        sCmd.Parameters.Add(New SqlParameter("@mCurrency", SqlDbType.VarChar)).Value() = oNV.Currency
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversion", SqlDbType.Decimal)).Value() = oNV.CurrencyConversion
        sCmd.Parameters.Add(New SqlParameter("@mNature", SqlDbType.VarChar)).Value() = oNV.Nature
        sCmd.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
        sCmd.Parameters.Add(New SqlParameter("@mTotalValue", SqlDbType.Decimal)).Value() = oNV.TotalValue
        sCmd.Parameters.Add(New SqlParameter("@mFreight", SqlDbType.Decimal)).Value() = oNV.Freight
        sCmd.Parameters.Add(New SqlParameter("@mInsurance", SqlDbType.Decimal)).Value() = oNV.Insurance
        sCmd.Parameters.Add(New SqlParameter("@mAgentPercentage", SqlDbType.Decimal)).Value() = oNV.AgentPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCommission", SqlDbType.Decimal)).Value() = oNV.Commission
        sCmd.Parameters.Add(New SqlParameter("@mDbkPercentage", SqlDbType.Decimal)).Value() = oNV.DbkPercentage
        sCmd.Parameters.Add(New SqlParameter("@mDrawBack", SqlDbType.Decimal)).Value() = oNV.DrawBack
        sCmd.Parameters.Add(New SqlParameter("@mBankCertificate", SqlDbType.VarChar)).Value() = oNV.BankCertificate
        sCmd.Parameters.Add(New SqlParameter("@mBankAmount", SqlDbType.Decimal)).Value() = oNV.BankAmount
        sCmd.Parameters.Add(New SqlParameter("@mBankRate", SqlDbType.Decimal)).Value() = oNV.BankRate
        sCmd.Parameters.Add(New SqlParameter("@mCertificateDate", SqlDbType.DateTime)).Value() = oNV.CertificateDate
        sCmd.Parameters.Add(New SqlParameter("@mTotalPackNo", SqlDbType.Decimal)).Value() = oNV.TotalPackNo
        sCmd.Parameters.Add(New SqlParameter("@mharmnsdco", SqlDbType.VarChar)).Value() = oNV.harmnsdco
        sCmd.Parameters.Add(New SqlParameter("@mNetWeight", SqlDbType.Decimal)).Value() = oNV.NetWeight
        sCmd.Parameters.Add(New SqlParameter("@mGrossWeight", SqlDbType.Decimal)).Value() = oNV.GrossWeight
        sCmd.Parameters.Add(New SqlParameter("@mdbkrecd", SqlDbType.Decimal)).Value() = oNV.dbkrecd
        sCmd.Parameters.Add(New SqlParameter("@maepccer_no", SqlDbType.VarChar)).Value() = oNV.aepccer_no
        sCmd.Parameters.Add(New SqlParameter("@mcust_clear", SqlDbType.DateTime)).Value() = oNV.cust_clear
        sCmd.Parameters.Add(New SqlParameter("@mrbicode", SqlDbType.VarChar)).Value() = oNV.rbicode
        sCmd.Parameters.Add(New SqlParameter("@mcot_rayon", SqlDbType.VarChar)).Value() = oNV.cot_rayon
        sCmd.Parameters.Add(New SqlParameter("@mshpblfob", SqlDbType.Decimal)).Value() = oNV.shpblfob
        sCmd.Parameters.Add(New SqlParameter("@mnetfobamt", SqlDbType.Decimal)).Value() = oNV.netfobamt
        sCmd.Parameters.Add(New SqlParameter("@mremark1", SqlDbType.VarChar)).Value() = oNV.remark1
        sCmd.Parameters.Add(New SqlParameter("@mstatus", SqlDbType.VarChar)).Value() = oNV.status
        'If Format(oNV.shipdate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mshipdate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mshipdate", SqlDbType.DateTime)).Value() = oNV.shipdate
        'End If
        'If Format(oNV.aepcdt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@maepcdt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@maepcdt", SqlDbType.DateTime)).Value() = oNV.aepcdt
        'End If
        'If Format(oNV.drwrecddt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mdrwrecddt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mdrwrecddt", SqlDbType.DateTime)).Value() = oNV.drwrecddt
        'End If
        'If Format(oNV.negdate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mnegdate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mnegdate", SqlDbType.DateTime)).Value() = oNV.negdate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mmarkrate", SqlDbType.Decimal)).Value() = oNV.markrate
        sCmd.Parameters.Add(New SqlParameter("@mcntrycode", SqlDbType.VarChar)).Value() = oNV.cntrycode
        'If Format(oNV.realstdt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mrealstdt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mrealstdt", SqlDbType.DateTime)).Value() = oNV.realstdt
        'End If
        sCmd.Parameters.Add(New SqlParameter("@musha", SqlDbType.VarChar)).Value() = oNV.usha
        sCmd.Parameters.Add(New SqlParameter("@mugts", SqlDbType.VarChar)).Value() = oNV.ugts
        'If Format(oNV.printdt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mprintdt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mprintdt", SqlDbType.DateTime)).Value() = oNV.printdt
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mpercent", SqlDbType.Decimal)).Value() = oNV.percent
        sCmd.Parameters.Add(New SqlParameter("@mplusamt", SqlDbType.Decimal)).Value() = oNV.plusamt
        sCmd.Parameters.Add(New SqlParameter("@mcntryname", SqlDbType.VarChar)).Value() = oNV.cntryname
        sCmd.Parameters.Add(New SqlParameter("@mdays", SqlDbType.Decimal)).Value() = oNV.days
        'If Format(oNV.due_date, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mdue_date", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mdue_date", SqlDbType.DateTime)).Value() = oNV.due_date
        'End If
        sCmd.Parameters.Add(New SqlParameter("@maccounted", SqlDbType.VarChar)).Value() = oNV.accounted
        sCmd.Parameters.Add(New SqlParameter("@maccountamt", SqlDbType.Decimal)).Value() = oNV.accountamt
        sCmd.Parameters.Add(New SqlParameter("@mmode", SqlDbType.VarChar)).Value() = oNV.mode
        sCmd.Parameters.Add(New SqlParameter("@mShipRate", SqlDbType.Decimal)).Value() = oNV.ShipRate
        sCmd.Parameters.Add(New SqlParameter("@mcourier", SqlDbType.VarChar)).Value() = oNV.courier
        sCmd.Parameters.Add(New SqlParameter("@mdocawb", SqlDbType.VarChar)).Value() = oNV.docawb
        'If Format(oNV.docsentdt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mdocsentdt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mdocsentdt", SqlDbType.DateTime)).Value() = oNV.docsentdt
        'End If
        'If Format(oNV.advdocdt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@madvdocdt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@madvdocdt", SqlDbType.DateTime)).Value() = oNV.advdocdt
        'End If
        'If Format(oNV.inspdate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@minspdate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@minspdate", SqlDbType.DateTime)).Value() = oNV.inspdate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@madvdocdays", SqlDbType.Decimal)).Value() = oNV.advdocdays
        sCmd.Parameters.Add(New SqlParameter("@mtokenno", SqlDbType.VarChar)).Value() = oNV.tokenno
        'If Format(oNV.tokendt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mtokendt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mtokendt", SqlDbType.DateTime)).Value() = oNV.tokendt
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mqlfbonus", SqlDbType.Decimal)).Value() = oNV.qlfbonus
        sCmd.Parameters.Add(New SqlParameter("@mmidamount", SqlDbType.Decimal)).Value() = oNV.midamount
        sCmd.Parameters.Add(New SqlParameter("@mdepb", SqlDbType.VarChar)).Value() = oNV.depb
        sCmd.Parameters.Add(New SqlParameter("@mdepbamt", SqlDbType.Decimal)).Value() = oNV.depbamt
        sCmd.Parameters.Add(New SqlParameter("@mdepbrcvd", SqlDbType.Decimal)).Value() = oNV.depbrcvd
        'If Format(oNV.depbrcvdon, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mdepbrcvdon", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mdepbrcvdon", SqlDbType.DateTime)).Value() = oNV.depbrcvdon
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mdepbper", SqlDbType.Decimal)).Value() = oNV.depbper
        sCmd.Parameters.Add(New SqlParameter("@mlicencenr", SqlDbType.VarChar)).Value() = oNV.licencenr
        sCmd.Parameters.Add(New SqlParameter("@mlicenceamt", SqlDbType.Decimal)).Value() = oNV.licenceamt
        'If Format(oNV.licsoldon, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mlicsoldon", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mlicsoldon", SqlDbType.DateTime)).Value() = oNV.licsoldon
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mlicsoldfor", SqlDbType.Decimal)).Value() = oNV.licsoldfor
        'If Format(oNV.depbappldt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mdepbappldt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mdepbappldt", SqlDbType.DateTime)).Value() = oNV.depbappldt
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mport", SqlDbType.VarChar)).Value() = oNV.port
        'If Format(oNV.epcopydt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mepcopydt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mepcopydt", SqlDbType.DateTime)).Value() = oNV.epcopydt
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mforwarder", SqlDbType.VarChar)).Value() = oNV.forwarder
        sCmd.Parameters.Add(New SqlParameter("@mforbillno", SqlDbType.VarChar)).Value() = oNV.forbillno
        sCmd.Parameters.Add(New SqlParameter("@mforbillamt", SqlDbType.Decimal)).Value() = oNV.forbillamt
        'If Format(oNV.forrcvdt, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mforrcvdt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mforrcvdt", SqlDbType.DateTime)).Value() = oNV.forrcvdt
        'End If
        'If Format(oNV.sentforver, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@msentforver", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@msentforver", SqlDbType.DateTime)).Value() = oNV.sentforver
        'End If
        'If Format(oNV.suppnr, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mverifieddt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mverifieddt", SqlDbType.DateTime)).Value() = oNV.suppnr
        'End If
        sCmd.Parameters.Add(New SqlParameter("@msuppnr", SqlDbType.VarChar)).Value() = oNV.suppnr
        sCmd.Parameters.Add(New SqlParameter("@msuppbbnnr", SqlDbType.VarChar)).Value() = oNV.suppbbnnr
        sCmd.Parameters.Add(New SqlParameter("@mwarehouse", SqlDbType.VarChar)).Value() = oNV.warehouse
        sCmd.Parameters.Add(New SqlParameter("@mswiftcode", SqlDbType.VarChar)).Value() = oNV.swiftcode
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBank", SqlDbType.VarChar)).Value() = oNV.CorrespondingBank
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBankAcount", SqlDbType.VarChar)).Value() = oNV.CorrespondingBankAcount
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBankSwiftCode", SqlDbType.VarChar)).Value() = oNV.CorrespondingBankSwiftCode
        sCmd.Parameters.Add(New SqlParameter("@mMsrks9", SqlDbType.VarChar)).Value() = oNV.Msrks9
        sCmd.Parameters.Add(New SqlParameter("@mMarks10", SqlDbType.VarChar)).Value() = oNV.Marks10
        sCmd.Parameters.Add(New SqlParameter("@mSTRPercentage", SqlDbType.Decimal)).Value() = oNV.STRPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSTRAmount", SqlDbType.Decimal)).Value() = oNV.STRAmount
        sCmd.Parameters.Add(New SqlParameter("@mPAN_Number", SqlDbType.VarChar)).Value() = oNV.PAN_Number
        sCmd.Parameters.Add(New SqlParameter("@mVAT_TIN", SqlDbType.VarChar)).Value() = oNV.VAT_TIN
        sCmd.Parameters.Add(New SqlParameter("@mCST_TIN", SqlDbType.VarChar)).Value() = oNV.CST_TIN
        sCmd.Parameters.Add(New SqlParameter("@mExciseDuty", SqlDbType.Decimal)).Value() = oNV.ExciseDuty
        sCmd.Parameters.Add(New SqlParameter("@mVATAmount", SqlDbType.Decimal)).Value() = oNV.VATAmount
        sCmd.Parameters.Add(New SqlParameter("@mCessAmount", SqlDbType.Decimal)).Value() = oNV.CessAmount
        sCmd.Parameters.Add(New SqlParameter("@mEduCessAmount", SqlDbType.Decimal)).Value() = oNV.EduCessAmount
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
        sCmd.Parameters.Add(New SqlParameter("@mMinusAmount", SqlDbType.Decimal)).Value() = oNV.MinusAmount
        sCmd.Parameters.Add(New SqlParameter("@mFinancialYear", SqlDbType.VarChar)).Value() = oNV.FinancialYear
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceType", SqlDbType.VarChar)).Value() = oNV.InvoiceType
        sCmd.Parameters.Add(New SqlParameter("@mShipped", SqlDbType.Bit)).Value() = oNV.Shipped
        sCmd.Parameters.Add(New SqlParameter("@mIsShipped", SqlDbType.Bit)).Value() = oNV.IsShipped
        'If Format(oNV.ShippedDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mShippedDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mShippedDate", SqlDbType.DateTime)).Value() = oNV.ShippedDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mShippedDate", SqlDbType.DateTime)).Value() = oNV.ShippedDate
        sCmd.Parameters.Add(New SqlParameter("@mShippedBy", SqlDbType.VarChar)).Value() = oNV.ShippedBy
        'If Format(oNV.MarkToShipDoneDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mMarkToShipDoneDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mMarkToShipDoneDate", SqlDbType.DateTime)).Value() = oNV.MarkToShipDoneDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mPaymentReceiveFromBuyer", SqlDbType.Bit)).Value() = oNV.PaymentReceiveFromBuyer
        sCmd.Parameters.Add(New SqlParameter("@mPaymentReceiveDate", SqlDbType.Date)).Value() = oNV.PaymentReceiveDate
        sCmd.Parameters.Add(New SqlParameter("@mBankRefNo", SqlDbType.VarChar)).Value() = oNV.BankRefNo
        'If Format(oNV.BankRefDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mBankRefDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mBankRefDate", SqlDbType.DateTime)).Value() = oNV.BankRefDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mContractNo", SqlDbType.VarChar)).Value() = oNV.ContractNo
        'If Format(oNV.ContractDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mContractDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mContractDate", SqlDbType.DateTime)).Value() = oNV.ContractDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mExciseInvoiceNo", SqlDbType.VarChar)).Value() = oNV.ExciseInvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mIsAdvanceReceived", SqlDbType.Bit)).Value() = oNV.IsAdvanceReceived
        sCmd.Parameters.Add(New SqlParameter("@mAdvanceAmount", SqlDbType.Decimal)).Value() = oNV.AdvanceAmount
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        'If Format(oNV.CreatedDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        'If Format(oNV.ModifiedDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
        'If Format(oNV.HandoverDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mHandoverDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mHandoverDate", SqlDbType.DateTime)).Value() = oNV.HandoverDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mHangerPack", SqlDbType.VarChar)).Value() = oNV.HangerPack
        sCmd.Parameters.Add(New SqlParameter("@mCovering", SqlDbType.VarChar)).Value() = oNV.Covering
        sCmd.Parameters.Add(New SqlParameter("@mDeclaration", SqlDbType.VarChar)).Value() = oNV.Declaration
        sCmd.Parameters.Add(New SqlParameter("@mPostDescription", SqlDbType.VarChar)).Value() = oNV.PostDescription
        sCmd.Parameters.Add(New SqlParameter("@mLCDescription", SqlDbType.VarChar)).Value() = oNV.LCDescription
        sCmd.Parameters.Add(New SqlParameter("@mMark11", SqlDbType.VarChar)).Value() = oNV.Mark11
        sCmd.Parameters.Add(New SqlParameter("@mMark12", SqlDbType.VarChar)).Value() = oNV.Mark12
        sCmd.Parameters.Add(New SqlParameter("@mDiscountUpCharge", SqlDbType.VarChar)).Value() = oNV.DiscountUpCharge
        sCmd.Parameters.Add(New SqlParameter("@mPercentage", SqlDbType.Decimal)).Value() = oNV.Percentage
        sCmd.Parameters.Add(New SqlParameter("@mAmount", SqlDbType.Decimal)).Value() = oNV.Amount
        sCmd.Parameters.Add(New SqlParameter("@mShipperLC", SqlDbType.VarChar)).Value() = oNV.ShipperLC
        sCmd.Parameters.Add(New SqlParameter("@mBuyerLC", SqlDbType.VarChar)).Value() = oNV.BuyerLC
        sCmd.Parameters.Add(New SqlParameter("@mFOBValueInCurrency", SqlDbType.Decimal)).Value() = oNV.FOBValueInCurrency
        sCmd.Parameters.Add(New SqlParameter("@mTaxType", SqlDbType.VarChar)).Value() = oNV.TaxType
        sCmd.Parameters.Add(New SqlParameter("@mIsRecalRequired", SqlDbType.Bit)).Value() = oNV.IsRecalRequired
        sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
        sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        'If Format(oNV.ApprovedOn, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mUpdateMode", SqlDbType.VarChar)).Value() = oNV.UpdateMode
        sCmd.Parameters.Add(New SqlParameter("@mConsigneeCode", SqlDbType.VarChar)).Value() = oNV.ConsigneeCode
        sCmd.Parameters.Add(New SqlParameter("@mNotify1", SqlDbType.VarChar)).Value() = oNV.Notify1
        sCmd.Parameters.Add(New SqlParameter("@mNotify2", SqlDbType.VarChar)).Value() = oNV.Notify2
        sCmd.Parameters.Add(New SqlParameter("@mNotify3", SqlDbType.VarChar)).Value() = oNV.Notify3
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignEmpCode", SqlDbType.VarChar)).Value() = oNV.AuthSignEmpCode
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignEmpName", SqlDbType.VarChar)).Value() = oNV.AuthSignEmpName
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignDesi", SqlDbType.VarChar)).Value() = oNV.AuthSignDesi
        sCmd.Parameters.Add(New SqlParameter("@mCSTorVAT", SqlDbType.Decimal)).Value() = oNV.CSTorVAT
        sCmd.Parameters.Add(New SqlParameter("@mEduCess", SqlDbType.Decimal)).Value() = oNV.EduCess
        sCmd.Parameters.Add(New SqlParameter("@mCESS", SqlDbType.Decimal)).Value() = oNV.CESS
        sCmd.Parameters.Add(New SqlParameter("@mExcise", SqlDbType.Decimal)).Value() = oNV.Excise
        sCmd.Parameters.Add(New SqlParameter("@mCT3No", SqlDbType.VarChar)).Value() = oNV.CT3No
        'If Format(oNV.CT3Date, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mCT3Date", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mCT3Date", SqlDbType.DateTime)).Value() = oNV.CT3Date
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mARENo", SqlDbType.VarChar)).Value() = oNV.ARENo
        'If Format(oNV.AREDate, "dd-MMM-yy") = "01-Jan-01" Then
        '    sCmd.Parameters.Add(New SqlParameter("@mAREDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        'Else
        '    sCmd.Parameters.Add(New SqlParameter("@mAREDate", SqlDbType.DateTime)).Value() = oNV.AREDate
        'End If
        sCmd.Parameters.Add(New SqlParameter("@mConveredBy", SqlDbType.VarChar)).Value() = oNV.ConveredBy
        sCmd.Parameters.Add(New SqlParameter("@mDeclaration1", SqlDbType.VarChar)).Value() = oNV.Declaration1
        sCmd.Parameters.Add(New SqlParameter("@mContainerSize", SqlDbType.VarChar)).Value() = oNV.ContainerSize
        sCmd.Parameters.Add(New SqlParameter("@mContainerName", SqlDbType.VarChar)).Value() = oNV.ContainerName
        sCmd.Parameters.Add(New SqlParameter("@mContainerSealNo", SqlDbType.VarChar)).Value() = oNV.ContainerSealNo
        sCmd.Parameters.Add(New SqlParameter("@mGoodsDescription", SqlDbType.VarChar)).Value() = oNV.GoodsDescription
        sCmd.Parameters.Add(New SqlParameter("@mMarksAndNos", SqlDbType.VarChar)).Value() = oNV.MarksAndNos
        sCmd.Parameters.Add(New SqlParameter("@mNoAndKindOfPackages", SqlDbType.VarChar)).Value() = oNV.NoAndKindOfPackages
        sCmd.Parameters.Add(New SqlParameter("@mPreCarriageBy", SqlDbType.VarChar)).Value() = oNV.PreCarriageBy
        sCmd.Parameters.Add(New SqlParameter("@mPreCarrierRecvPlace", SqlDbType.VarChar)).Value() = oNV.PreCarrierRecvPlace
        sCmd.Parameters.Add(New SqlParameter("@mPortDischarge", SqlDbType.VarChar)).Value() = oNV.PortDischarge
        sCmd.Parameters.Add(New SqlParameter("@mDestinationCountry", SqlDbType.VarChar)).Value() = oNV.DestinationCountry
        sCmd.Parameters.Add(New SqlParameter("@mAssortmentYear", SqlDbType.VarChar)).Value() = oNV.AssortmentYear
        sCmd.Parameters.Add(New SqlParameter("@mPaymentTerms", SqlDbType.VarChar)).Value() = oNV.PaymentTerms
        sCmd.Parameters.Add(New SqlParameter("@mDeliveryTerms", SqlDbType.VarChar)).Value() = oNV.DeliveryTerms
        sCmd.Parameters.Add(New SqlParameter("@mAREDuty", SqlDbType.Decimal)).Value() = oNV.AREDuty
        sCmd.Parameters.Add(New SqlParameter("@mAreCess", SqlDbType.Decimal)).Value() = oNV.AreCess
        sCmd.Parameters.Add(New SqlParameter("@mAREHCess", SqlDbType.Decimal)).Value() = oNV.AREHCess
        sCmd.Parameters.Add(New SqlParameter("@mContainerNo", SqlDbType.VarChar)).Value() = oNV.ContainerNo
        sCmd.Parameters.Add(New SqlParameter("@mFromPackNo", SqlDbType.VarChar)).Value() = oNV.FromPackNo
        sCmd.Parameters.Add(New SqlParameter("@mToPackNo", SqlDbType.VarChar)).Value() = oNV.ToPackNo
        sCmd.Parameters.Add(New SqlParameter("@mEmailToCustDate", SqlDbType.Date)).Value() = oNV.EmailToCustDate
        sCmd.Parameters.Add(New SqlParameter("@mFreightFwdrPlotLetterDate", SqlDbType.Date)).Value() = oNV.FreightFwdrPlotLetterDate
        sCmd.Parameters.Add(New SqlParameter("@mContainerApplnDate", SqlDbType.Date)).Value() = oNV.ContainerApplnDate
        sCmd.Parameters.Add(New SqlParameter("@mGSPSlNo", SqlDbType.VarChar)).Value() = oNV.GSPSlNo
        sCmd.Parameters.Add(New SqlParameter("@mOriginCriterion", SqlDbType.VarChar)).Value() = oNV.OriginCriterion
        sCmd.Parameters.Add(New SqlParameter("@mStuffingDate", SqlDbType.Date)).Value() = oNV.StuffingDate
        sCmd.Parameters.Add(New SqlParameter("@mGateOpeningDate", SqlDbType.Date)).Value() = oNV.GateOpeningDate
        sCmd.Parameters.Add(New SqlParameter("@mCutOffDate", SqlDbType.Date)).Value() = oNV.CutOffDate
        sCmd.Parameters.Add(New SqlParameter("@mClosingDate", SqlDbType.Date)).Value() = oNV.ClosingDate
        sCmd.Parameters.Add(New SqlParameter("@mSailingVesselDetails", SqlDbType.VarChar)).Value() = oNV.SailingVesselDetails
        sCmd.Parameters.Add(New SqlParameter("@mShipmentType", SqlDbType.VarChar)).Value() = oNV.ShipmentType
        sCmd.Parameters.Add(New SqlParameter("@mRevShipmentType", SqlDbType.VarChar)).Value() = oNV.RevShipmentType
        sCmd.Parameters.Add(New SqlParameter("@mHaltingCharges", SqlDbType.Decimal)).Value() = oNV.HaltingCharges
        sCmd.Parameters.Add(New SqlParameter("@mDemurrage", SqlDbType.Decimal)).Value() = oNV.Demurrage
        sCmd.Parameters.Add(New SqlParameter("@mVoyageNo", SqlDbType.VarChar)).Value() = oNV.VoyageNo
        sCmd.Parameters.Add(New SqlParameter("@mETDFeederVessel", SqlDbType.Date)).Value() = oNV.ETDFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mETAFeederVessel", SqlDbType.Date)).Value() = oNV.ETAFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mFeederVessel", SqlDbType.VarChar)).Value() = oNV.FeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mFeederVoyageNo", SqlDbType.VarChar)).Value() = oNV.FeederVoyageNo
        sCmd.Parameters.Add(New SqlParameter("@mETAMotherVessel", SqlDbType.Date)).Value() = oNV.ETAMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mETDMotherVessel", SqlDbType.Date)).Value() = oNV.ETDMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mInternalSealNo", SqlDbType.VarChar)).Value() = oNV.InternalSealNo
        sCmd.Parameters.Add(New SqlParameter("@mCentralExciseSealNo", SqlDbType.VarChar)).Value() = oNV.CentralExciseSealNo
        sCmd.Parameters.Add(New SqlParameter("@mTypesOfBL", SqlDbType.VarChar)).Value() = oNV.TypesOfBL
        sCmd.Parameters.Add(New SqlParameter("@mCustomClearence", SqlDbType.VarChar)).Value() = oNV.CustomClearence
        sCmd.Parameters.Add(New SqlParameter("@mTransportCharges", SqlDbType.Decimal)).Value() = oNV.TransportCharges
        sCmd.Parameters.Add(New SqlParameter("@mCHACharges", SqlDbType.Decimal)).Value() = oNV.CHACharges
        sCmd.Parameters.Add(New SqlParameter("@mClearingCharges", SqlDbType.Decimal)).Value() = oNV.ClearingCharges
        sCmd.Parameters.Add(New SqlParameter("@mCFSCharges", SqlDbType.Decimal)).Value() = oNV.CFSCharges
        sCmd.Parameters.Add(New SqlParameter("@mForwardingCharges", SqlDbType.Decimal)).Value() = oNV.ForwardingCharges
        sCmd.Parameters.Add(New SqlParameter("@mGSpCharges", SqlDbType.Decimal)).Value() = oNV.GSPCharges
        sCmd.Parameters.Add(New SqlParameter("@mCourierCharges", SqlDbType.Decimal)).Value() = oNV.CourierCharges
        sCmd.Parameters.Add(New SqlParameter("@mInsuranceCharges", SqlDbType.Decimal)).Value() = oNV.InsuranceCharges
        sCmd.Parameters.Add(New SqlParameter("@mMiscCharges", SqlDbType.Decimal)).Value() = oNV.MiscCharges
        sCmd.Parameters.Add(New SqlParameter("@mContainerArrivalDate", SqlDbType.Date)).Value() = oNV.ContainerArrivalDate
        sCmd.Parameters.Add(New SqlParameter("@mMemo", SqlDbType.VarChar)).Value() = oNV.Memo
        sCmd.Parameters.Add(New SqlParameter("@mDestinationArrivalDate", SqlDbType.Date)).Value() = oNV.DestinationArrivalDate
        sCmd.Parameters.Add(New SqlParameter("@mFreightFwdrPlotLetterExpDate", SqlDbType.Date)).Value() = oNV.FreightFwdrPlotLetterExpDate
        sCmd.Parameters.Add(New SqlParameter("@mForwarderCode", SqlDbType.VarChar)).Value() = oNV.ForwarderCode
        sCmd.Parameters.Add(New SqlParameter("@mCHACode", SqlDbType.VarChar)).Value() = oNV.CHACode
        sCmd.Parameters.Add(New SqlParameter("@mRevVessel", SqlDbType.VarChar)).Value() = oNV.RevVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevVoyageNo", SqlDbType.VarChar)).Value() = oNV.RevVoyageNo
        sCmd.Parameters.Add(New SqlParameter("@mRevETDMotherVessel", SqlDbType.Date)).Value() = oNV.RevETDMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevETAMotherVessel", SqlDbType.Date)).Value() = oNV.RevETAMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevFeederVessel", SqlDbType.VarChar)).Value() = oNV.RevFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevFeederVoyageNo", SqlDbType.VarChar)).Value() = oNV.RevFeederVoyageNo
        sCmd.Parameters.Add(New SqlParameter("@mRevETDFeederVessel", SqlDbType.Date)).Value() = oNV.RevETDFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevETAFeederVessel", SqlDbType.Date)).Value() = oNV.RevETAFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mCommodity", SqlDbType.VarChar)).Value() = oNV.Commodity
        sCmd.Parameters.Add(New SqlParameter("@mPremiumRate", SqlDbType.Decimal)).Value() = oNV.PremiumRate
        sCmd.Parameters.Add(New SqlParameter("@mPremiumAmount", SqlDbType.Decimal)).Value() = oNV.PremiumAmount
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNoAutoGen", SqlDbType.VarChar)).Value() = oNV.InvoiceNoAutoGen
        sCmd.Parameters.Add(New SqlParameter("@mCourierPayment", SqlDbType.VarChar)).Value() = oNV.CourierPayment
        sCmd.Parameters.Add(New SqlParameter("@mCourierNo", SqlDbType.VarChar)).Value() = oNV.CourierNo
        sCmd.Parameters.Add(New SqlParameter("@mCourierDate", SqlDbType.Date)).Value() = oNV.CourierDate
        sCmd.Parameters.Add(New SqlParameter("@mCartonDia", SqlDbType.VarChar)).Value() = oNV.CartonDia
        sCmd.Parameters.Add(New SqlParameter("@mOneCarton", SqlDbType.VarChar)).Value() = oNV.OneCarton
        sCmd.Parameters.Add(New SqlParameter("@mTotalOne", SqlDbType.VarChar)).Value() = oNV.TotalOne
        sCmd.Parameters.Add(New SqlParameter("@mFinalDestination", SqlDbType.VarChar)).Value() = oNV.FinalDestination
        sCmd.Parameters.Add(New SqlParameter("@mMatType", SqlDbType.VarChar)).Value() = oNV.MatType
        sCmd.Parameters.Add(New SqlParameter("@mAmtOfDutyPayable", SqlDbType.Decimal)).Value() = oNV.AmtOfDutyPayable
        sCmd.Parameters.Add(New SqlParameter("@mSubTotal", SqlDbType.Decimal)).Value() = oNV.SubTotal
        sCmd.Parameters.Add(New SqlParameter("@mInvYear", SqlDbType.VarChar)).Value() = oNV.InvYear
        sCmd.Parameters.Add(New SqlParameter("@mInvCode", SqlDbType.VarChar)).Value() = oNV.InvCode
        sCmd.Parameters.Add(New SqlParameter("@mDispatchFrom", SqlDbType.VarChar)).Value() = oNV.DispatchFrom
        sCmd.Parameters.Add(New SqlParameter("@mInoviceStatus", SqlDbType.VarChar)).Value() = oNV.InoviceStatus
        sCmd.Parameters.Add(New SqlParameter("@mExpDocDate", SqlDbType.DateTime)).Value() = oNV.ExpDocDate
        sCmd.Parameters.Add(New SqlParameter("@mGoodsDescription2", SqlDbType.VarChar)).Value() = oNV.GoodsDescription2
        sCmd.Parameters.Add(New SqlParameter("@mRBBankName", SqlDbType.VarChar)).Value() = oNV.RBBankName
        sCmd.Parameters.Add(New SqlParameter("@mRBAccountNo", SqlDbType.VarChar)).Value() = oNV.RBAccountNo
        sCmd.Parameters.Add(New SqlParameter("@mRBSwiftCode", SqlDbType.VarChar)).Value() = oNV.RBSwiftCode
        sCmd.Parameters.Add(New SqlParameter("@mCurValue", SqlDbType.Decimal)).Value() = oNV.CurValue
        sCmd.Parameters.Add(New SqlParameter("@mAnnexureAPortOfLoading", SqlDbType.VarChar)).Value() = oNV.AnnexureAPortOfLoading
        sCmd.Parameters.Add(New SqlParameter("@mLCID", SqlDbType.VarChar)).Value() = oNV.LCID
        sCmd.Parameters.Add(New SqlParameter("@mLCValue", SqlDbType.Decimal)).Value() = oNV.LCValue
        sCmd.Parameters.Add(New SqlParameter("@mShippedLCValue", SqlDbType.Decimal)).Value() = oNV.ShippedLCValue
        sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSGSTVlaue", SqlDbType.Decimal)).Value() = oNV.SGSTVlaue
        sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mFreightCharges", SqlDbType.Decimal)).Value() = oNV.FreightCharges
        sCmd.Parameters.Add(New SqlParameter("@mLoadingCharges", SqlDbType.Decimal)).Value() = oNV.LoadingCharges
        sCmd.Parameters.Add(New SqlParameter("@mInsuranceChager", SqlDbType.Decimal)).Value() = oNV.InsuranceChager
        sCmd.Parameters.Add(New SqlParameter("@mOtherCharges", SqlDbType.Decimal)).Value() = oNV.OtherCharges
        sCmd.Parameters.Add(New SqlParameter("@mDiscount", SqlDbType.Decimal)).Value() = oNV.Discount
        sCmd.Parameters.Add(New SqlParameter("@mGSTTotalValue", SqlDbType.Decimal)).Value() = oNV.GSTTotalValue
        sCmd.Parameters.Add(New SqlParameter("@mGSTInvNo", SqlDbType.VarChar)).Value() = oNV.GSTInvNo
        sCmd.Parameters.Add(New SqlParameter("@mInvNo2", SqlDbType.VarChar)).Value() = oNV.InvNo2
        sCmd.Parameters.Add(New SqlParameter("@mInvNo3", SqlDbType.VarChar)).Value() = oNV.InvNo3
        sCmd.Parameters.Add(New SqlParameter("@mDUMMYINVDATE", SqlDbType.DateTime)).Value() = oNV.DUMMYINVDATE
        sCmd.Parameters.Add(New SqlParameter("@mGSTValue", SqlDbType.Decimal)).Value() = oNV.GSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSerialNoPrefix", SqlDbType.VarChar)).Value() = oNV.SerialNoPrefix
        sCmd.Parameters.Add(New SqlParameter("@mInternalOrder", SqlDbType.VarChar)).Value() = oNV.InternalOrder


        sCnnAudit.Close()
        sCnnAudit.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function InsertInvoiceDetails(ByVal oNV As strInvoiceDetails) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSINVDTL"
        sCmd.Parameters.Add(New SqlParameter("@minvoiceno", SqlDbType.VarChar)).Value() = oNV.invoiceno
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = oNV.InvoiceDate
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceSerialNo", SqlDbType.VarChar)).Value() = oNV.InvoiceSerialNo
        sCmd.Parameters.Add(New SqlParameter("@mbuyer", SqlDbType.VarChar)).Value() = oNV.buyer
        sCmd.Parameters.Add(New SqlParameter("@mshipper", SqlDbType.VarChar)).Value() = oNV.shipper
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mtype", SqlDbType.VarChar)).Value() = oNV.type
        sCmd.Parameters.Add(New SqlParameter("@msubordno", SqlDbType.VarChar)).Value() = oNV.subordno
        sCmd.Parameters.Add(New SqlParameter("@mArticleNo", SqlDbType.VarChar)).Value() = oNV.ArticleNo
        sCmd.Parameters.Add(New SqlParameter("@msmpstyl", SqlDbType.VarChar)).Value() = oNV.smpstyl
        sCmd.Parameters.Add(New SqlParameter("@mColor", SqlDbType.VarChar)).Value() = oNV.Color
        sCmd.Parameters.Add(New SqlParameter("@mrate", SqlDbType.Decimal)).Value() = oNV.rate
        sCmd.Parameters.Add(New SqlParameter("@mquantity", SqlDbType.Decimal)).Value() = oNV.quantity
        sCmd.Parameters.Add(New SqlParameter("@mshortshp", SqlDbType.Decimal)).Value() = oNV.shortshp
        sCmd.Parameters.Add(New SqlParameter("@mrt", SqlDbType.VarChar)).Value() = oNV.rt
        sCmd.Parameters.Add(New SqlParameter("@mratioqty", SqlDbType.Decimal)).Value() = oNV.ratioqty
        sCmd.Parameters.Add(New SqlParameter("@mcurrency", SqlDbType.VarChar)).Value() = oNV.currency
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversionRate", SqlDbType.Decimal)).Value() = oNV.CurrencyConversionRate
        sCmd.Parameters.Add(New SqlParameter("@mcategory", SqlDbType.VarChar)).Value() = oNV.category
        sCmd.Parameters.Add(New SqlParameter("@mbuyrdept", SqlDbType.VarChar)).Value() = oNV.buyrdept
        sCmd.Parameters.Add(New SqlParameter("@mbankrate", SqlDbType.Decimal)).Value() = oNV.bankrate
        sCmd.Parameters.Add(New SqlParameter("@mvalue", SqlDbType.Decimal)).Value() = oNV.value
        sCmd.Parameters.Add(New SqlParameter("@msize01", SqlDbType.VarChar)).Value() = oNV.size1
        sCmd.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Decimal)).Value() = oNV.qty1
        sCmd.Parameters.Add(New SqlParameter("@msize02", SqlDbType.VarChar)).Value() = oNV.size2
        sCmd.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Decimal)).Value() = oNV.qty2
        sCmd.Parameters.Add(New SqlParameter("@msize03", SqlDbType.VarChar)).Value() = oNV.size3
        sCmd.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Decimal)).Value() = oNV.qty3
        sCmd.Parameters.Add(New SqlParameter("@msize04", SqlDbType.VarChar)).Value() = oNV.size4
        sCmd.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Decimal)).Value() = oNV.qty4
        sCmd.Parameters.Add(New SqlParameter("@msize05", SqlDbType.VarChar)).Value() = oNV.size5
        sCmd.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Decimal)).Value() = oNV.qty5
        sCmd.Parameters.Add(New SqlParameter("@msize06", SqlDbType.VarChar)).Value() = oNV.size6
        sCmd.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Decimal)).Value() = oNV.qty6
        sCmd.Parameters.Add(New SqlParameter("@msize07", SqlDbType.VarChar)).Value() = oNV.size7
        sCmd.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Decimal)).Value() = oNV.qty7
        sCmd.Parameters.Add(New SqlParameter("@msize08", SqlDbType.VarChar)).Value() = oNV.size8
        sCmd.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Decimal)).Value() = oNV.qty8
        sCmd.Parameters.Add(New SqlParameter("@msize09", SqlDbType.VarChar)).Value() = oNV.size9
        sCmd.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Decimal)).Value() = oNV.qty9
        sCmd.Parameters.Add(New SqlParameter("@msize10", SqlDbType.VarChar)).Value() = oNV.size10
        sCmd.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Decimal)).Value() = oNV.qty10
        sCmd.Parameters.Add(New SqlParameter("@minspDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.inspdate
        sCmd.Parameters.Add(New SqlParameter("@mcert_no", SqlDbType.VarChar)).Value() = oNV.cert_no
        sCmd.Parameters.Add(New SqlParameter("@mperiod", SqlDbType.VarChar)).Value() = oNV.period
        sCmd.Parameters.Add(New SqlParameter("@mcert_type", SqlDbType.VarChar)).Value() = oNV.cert_type
        sCmd.Parameters.Add(New SqlParameter("@mcartalloc", SqlDbType.VarChar)).Value() = oNV.cartalloc
        sCmd.Parameters.Add(New SqlParameter("@mannexno", SqlDbType.VarChar)).Value() = oNV.annexno
        sCmd.Parameters.Add(New SqlParameter("@manserial", SqlDbType.VarChar)).Value() = oNV.anserial
        sCmd.Parameters.Add(New SqlParameter("@mawb_bl_no", SqlDbType.VarChar)).Value() = oNV.awb_bl_no
        sCmd.Parameters.Add(New SqlParameter("@mlcno", SqlDbType.VarChar)).Value() = oNV.lcno
        sCmd.Parameters.Add(New SqlParameter("@mshbilldt", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.shbilldt
        sCmd.Parameters.Add(New SqlParameter("@mcatii", SqlDbType.VarChar)).Value() = oNV.catii
        sCmd.Parameters.Add(New SqlParameter("@mcatiiconv", SqlDbType.Decimal)).Value() = oNV.catiiconv
        sCmd.Parameters.Add(New SqlParameter("@mBuyerOrderNo", SqlDbType.VarChar)).Value() = oNV.BuyerOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mprovordnr", SqlDbType.VarChar)).Value() = oNV.provordnr
        sCmd.Parameters.Add(New SqlParameter("@mCountryCode", SqlDbType.VarChar)).Value() = oNV.CountryCode
        sCmd.Parameters.Add(New SqlParameter("@msmpdesc", SqlDbType.VarChar)).Value() = oNV.smpdesc
        sCmd.Parameters.Add(New SqlParameter("@mnDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.ndate
        sCmd.Parameters.Add(New SqlParameter("@mrvDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.rvdate
        sCmd.Parameters.Add(New SqlParameter("@mmrate", SqlDbType.Decimal)).Value() = oNV.mrate
        sCmd.Parameters.Add(New SqlParameter("@musha", SqlDbType.VarChar)).Value() = oNV.usha
        sCmd.Parameters.Add(New SqlParameter("@mugts", SqlDbType.VarChar)).Value() = oNV.ugts
        sCmd.Parameters.Add(New SqlParameter("@mrsno", SqlDbType.VarChar)).Value() = oNV.rsno
        sCmd.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.MaterialCode
        sCmd.Parameters.Add(New SqlParameter("@mmode", SqlDbType.VarChar)).Value() = oNV.mode
        sCmd.Parameters.Add(New SqlParameter("@mGroup", SqlDbType.VarChar)).Value() = oNV.Group
        sCmd.Parameters.Add(New SqlParameter("@mbank", SqlDbType.VarChar)).Value() = oNV.bank
        sCmd.Parameters.Add(New SqlParameter("@mcommpercnt", SqlDbType.Decimal)).Value() = oNV.commpercnt
        sCmd.Parameters.Add(New SqlParameter("@mmainfab", SqlDbType.VarChar)).Value() = oNV.mainfab
        sCmd.Parameters.Add(New SqlParameter("@mcountry", SqlDbType.VarChar)).Value() = oNV.country
        sCmd.Parameters.Add(New SqlParameter("@mbasestyl", SqlDbType.VarChar)).Value() = oNV.basestyl
        sCmd.Parameters.Add(New SqlParameter("@mcurvalue", SqlDbType.Decimal)).Value() = oNV.curvalue
        sCmd.Parameters.Add(New SqlParameter("@msize11", SqlDbType.VarChar)).Value() = oNV.size11
        sCmd.Parameters.Add(New SqlParameter("@msize12", SqlDbType.VarChar)).Value() = oNV.size12
        sCmd.Parameters.Add(New SqlParameter("@msize13", SqlDbType.VarChar)).Value() = oNV.size13
        sCmd.Parameters.Add(New SqlParameter("@msize14", SqlDbType.VarChar)).Value() = oNV.size14
        sCmd.Parameters.Add(New SqlParameter("@msize15", SqlDbType.VarChar)).Value() = oNV.size15
        sCmd.Parameters.Add(New SqlParameter("@msize16", SqlDbType.VarChar)).Value() = oNV.size16
        sCmd.Parameters.Add(New SqlParameter("@msize17", SqlDbType.VarChar)).Value() = oNV.size17
        sCmd.Parameters.Add(New SqlParameter("@msize18", SqlDbType.VarChar)).Value() = oNV.size18
        sCmd.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Decimal)).Value() = oNV.qty11
        sCmd.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Decimal)).Value() = oNV.qty12
        sCmd.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Decimal)).Value() = oNV.qty13
        sCmd.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Decimal)).Value() = oNV.qty14
        sCmd.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Decimal)).Value() = oNV.qty15
        sCmd.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Decimal)).Value() = oNV.qty16
        sCmd.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Decimal)).Value() = oNV.qty17
        sCmd.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Decimal)).Value() = oNV.qty18
        sCmd.Parameters.Add(New SqlParameter("@mOrderSerialNo", SqlDbType.VarChar)).Value() = oNV.OrderSerialNo
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
        sCmd.Parameters.Add(New SqlParameter("@mLCDiscount", SqlDbType.Decimal)).Value() = oNV.LCDiscount
        sCmd.Parameters.Add(New SqlParameter("@mShipped", SqlDbType.Bit)).Value() = oNV.Shipped
        sCmd.Parameters.Add(New SqlParameter("@mCustomerStyleName", SqlDbType.VarChar)).Value() = oNV.CustomerStyleName
        sCmd.Parameters.Add(New SqlParameter("@mIsShipped", SqlDbType.Bit)).Value() = oNV.IsShipped
        sCmd.Parameters.Add(New SqlParameter("@mStatus", SqlDbType.VarChar)).Value() = oNV.Status
        sCmd.Parameters.Add(New SqlParameter("@mPaymentREceiveFromBuyer", SqlDbType.Bit)).Value() = oNV.PaymentREceiveFromBuyer
        sCmd.Parameters.Add(New SqlParameter("@mStore", SqlDbType.VarChar)).Value() = oNV.Store
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = oNV.Season
        sCmd.Parameters.Add(New SqlParameter("@mburdept", SqlDbType.VarChar)).Value() = oNV.burdept
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
        sCmd.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = oNV.sVariant
        sCmd.Parameters.Add(New SqlParameter("@mCustomerArticleNo", SqlDbType.VarChar)).Value() = oNV.CustomerArticleNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDescription", SqlDbType.VarChar)).Value() = oNV.InvoiceDescription
        sCmd.Parameters.Add(New SqlParameter("@mMerchandiser", SqlDbType.VarChar)).Value() = oNV.Merchandiser
        sCmd.Parameters.Add(New SqlParameter("@mPackNo", SqlDbType.VarChar)).Value() = oNV.PackNo
        sCmd.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo
        sCmd.Parameters.Add(New SqlParameter("@mCount", SqlDbType.VarChar)).Value() = oNV.Count
        sCmd.Parameters.Add(New SqlParameter("@mconstruction", SqlDbType.VarChar)).Value() = oNV.construction
        sCmd.Parameters.Add(New SqlParameter("@mCartonNo", SqlDbType.VarChar)).Value() = oNV.CartonNo
        sCmd.Parameters.Add(New SqlParameter("@mMRPRate", SqlDbType.Decimal)).Value() = oNV.MRPRate
        sCmd.Parameters.Add(New SqlParameter("@mIsRecalRequired", SqlDbType.Bit)).Value() = oNV.IsRecalRequired
        sCmd.Parameters.Add(New SqlParameter("@mOrderPrice", SqlDbType.Decimal)).Value() = oNV.OrderPrice
        sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
        sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.ApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value() = oNV.JobCardDetailID
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value() = oNV.InvoiceID
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mDCNo", SqlDbType.VarChar)).Value() = oNV.DCNo
        sCmd.Parameters.Add(New SqlParameter("@mpcs", SqlDbType.Int)).Value() = oNV.pcs
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = oNV.SalesOrderDetailID
        sCmd.Parameters.Add(New SqlParameter("@mCustWorkOrderNO", SqlDbType.VarChar)).Value() = oNV.CustWorkOrderNO
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNoAutoGen", SqlDbType.VarChar)).Value() = oNV.InvoiceNoAutoGen
        sCmd.Parameters.Add(New SqlParameter("@mArticleCodification", SqlDbType.VarChar)).Value() = oNV.ArticleCodification
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceQuantity", SqlDbType.Decimal)).Value() = oNV.InvoiceQuantity
        sCmd.Parameters.Add(New SqlParameter("@mCBMSpace", SqlDbType.Decimal)).Value() = oNV.CBMSpace
        sCmd.Parameters.Add(New SqlParameter("@mOrderQty", SqlDbType.Decimal)).Value() = oNV.OrderQty
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversionRate4Tally", SqlDbType.Decimal)).Value() = oNV.CurrencyConversionRate4Tally
        sCmd.Parameters.Add(New SqlParameter("@mcurvalue4Tally", SqlDbType.Decimal)).Value() = oNV.curvalue4Tally
        sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSGSTValue", SqlDbType.Decimal)).Value() = oNV.SGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mDiscount", SqlDbType.Decimal)).Value() = oNV.Discount
        sCmd.Parameters.Add(New SqlParameter("@mHSNCode", SqlDbType.VarChar)).Value() = oNV.HSNCode
        sCmd.Parameters.Add(New SqlParameter("@mWareHouse", SqlDbType.VarChar)).Value() = oNV.WareHouse
        sCmd.Parameters.Add(New SqlParameter("@mFValue", SqlDbType.Decimal)).Value() = oNV.FValue
        sCmd.Parameters.Add(New SqlParameter("@mLastInvDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'oNV.LastInvDate
        sCmd.Parameters.Add(New SqlParameter("@mReady2DispatchID", SqlDbType.VarChar)).Value() = oNV.Ready2DispatchID
        sCmd.Parameters.Add(New SqlParameter("@mInternalOrder", SqlDbType.VarChar)).Value() = oNV.InternalOrder
        sCmd.Parameters.Add(New SqlParameter("@mIsSampleOrder", SqlDbType.Bit)).Value() = oNV.IsSampleOrder
        sCmd.Parameters.Add(New SqlParameter("@mSampleOrderType", SqlDbType.VarChar)).Value() = oNV.SampleOrderType
        sCmd.Parameters.Add(New SqlParameter("@mArticleandColor", SqlDbType.VarChar)).Value() = oNV.ArticleandColor
        sCmd.Parameters.Add(New SqlParameter("@mNetValue", SqlDbType.Decimal)).Value() = oNV.NettValue



        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function InsertInvoiceDetailsinAuditDatabase(ByVal oNV As strInvoiceDetails) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnnAudit
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSINVDTL"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceSerialNo", SqlDbType.VarChar)).Value() = oNV.InvoiceSerialNo
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@minvoiceno", SqlDbType.VarChar)).Value() = oNV.invoiceno
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = oNV.InvoiceDate
        sCmd.Parameters.Add(New SqlParameter("@mDCNo", SqlDbType.VarChar)).Value() = oNV.DCNo
        sCmd.Parameters.Add(New SqlParameter("@mbuyer", SqlDbType.VarChar)).Value() = oNV.buyer
        sCmd.Parameters.Add(New SqlParameter("@mshipper", SqlDbType.VarChar)).Value() = oNV.shipper
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.SalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mtype", SqlDbType.VarChar)).Value() = oNV.type
        sCmd.Parameters.Add(New SqlParameter("@msubordno", SqlDbType.VarChar)).Value() = oNV.subordno
        sCmd.Parameters.Add(New SqlParameter("@mArticleNo", SqlDbType.VarChar)).Value() = oNV.ArticleNo
        sCmd.Parameters.Add(New SqlParameter("@msmpstyl", SqlDbType.VarChar)).Value() = oNV.smpstyl
        sCmd.Parameters.Add(New SqlParameter("@mColor", SqlDbType.VarChar)).Value() = oNV.Color
        sCmd.Parameters.Add(New SqlParameter("@mrate", SqlDbType.Decimal)).Value() = oNV.rate
        sCmd.Parameters.Add(New SqlParameter("@mquantity", SqlDbType.Decimal)).Value() = oNV.quantity
        sCmd.Parameters.Add(New SqlParameter("@mshortshp", SqlDbType.Decimal)).Value() = oNV.shortshp
        sCmd.Parameters.Add(New SqlParameter("@mrt", SqlDbType.VarChar)).Value() = oNV.rt
        sCmd.Parameters.Add(New SqlParameter("@mratioqty", SqlDbType.Decimal)).Value() = oNV.ratioqty
        sCmd.Parameters.Add(New SqlParameter("@mcurrency", SqlDbType.VarChar)).Value() = oNV.currency
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversionRate", SqlDbType.Decimal)).Value() = oNV.CurrencyConversionRate
        sCmd.Parameters.Add(New SqlParameter("@mcategory", SqlDbType.VarChar)).Value() = oNV.category
        sCmd.Parameters.Add(New SqlParameter("@mbuyrdept", SqlDbType.VarChar)).Value() = oNV.buyrdept
        sCmd.Parameters.Add(New SqlParameter("@mbankrate", SqlDbType.Decimal)).Value() = oNV.bankrate
        sCmd.Parameters.Add(New SqlParameter("@mvalue", SqlDbType.Decimal)).Value() = oNV.value
        sCmd.Parameters.Add(New SqlParameter("@msize1", SqlDbType.VarChar)).Value() = oNV.size1
        sCmd.Parameters.Add(New SqlParameter("@mqty1", SqlDbType.Decimal)).Value() = oNV.qty1
        sCmd.Parameters.Add(New SqlParameter("@msize2", SqlDbType.VarChar)).Value() = oNV.size2
        sCmd.Parameters.Add(New SqlParameter("@mqty2", SqlDbType.Decimal)).Value() = oNV.qty2
        sCmd.Parameters.Add(New SqlParameter("@msize3", SqlDbType.VarChar)).Value() = oNV.size3
        sCmd.Parameters.Add(New SqlParameter("@mqty3", SqlDbType.Decimal)).Value() = oNV.qty3
        sCmd.Parameters.Add(New SqlParameter("@msize4", SqlDbType.VarChar)).Value() = oNV.size4
        sCmd.Parameters.Add(New SqlParameter("@mqty4", SqlDbType.Decimal)).Value() = oNV.qty4
        sCmd.Parameters.Add(New SqlParameter("@msize5", SqlDbType.VarChar)).Value() = oNV.size5
        sCmd.Parameters.Add(New SqlParameter("@mqty5", SqlDbType.Decimal)).Value() = oNV.qty5
        sCmd.Parameters.Add(New SqlParameter("@msize6", SqlDbType.VarChar)).Value() = oNV.size6
        sCmd.Parameters.Add(New SqlParameter("@mqty6", SqlDbType.Decimal)).Value() = oNV.qty6
        sCmd.Parameters.Add(New SqlParameter("@msize7", SqlDbType.VarChar)).Value() = oNV.size7
        sCmd.Parameters.Add(New SqlParameter("@mqty7", SqlDbType.Decimal)).Value() = oNV.qty7
        sCmd.Parameters.Add(New SqlParameter("@msize8", SqlDbType.VarChar)).Value() = oNV.size8
        sCmd.Parameters.Add(New SqlParameter("@mqty8", SqlDbType.Decimal)).Value() = oNV.qty8
        sCmd.Parameters.Add(New SqlParameter("@msize9", SqlDbType.VarChar)).Value() = oNV.size9
        sCmd.Parameters.Add(New SqlParameter("@mqty9", SqlDbType.Decimal)).Value() = oNV.qty9
        sCmd.Parameters.Add(New SqlParameter("@msize10", SqlDbType.VarChar)).Value() = oNV.size10
        sCmd.Parameters.Add(New SqlParameter("@mqty10", SqlDbType.Decimal)).Value() = oNV.qty10
        sCmd.Parameters.Add(New SqlParameter("@minspdate", SqlDbType.DateTime)).Value() = oNV.inspdate
        sCmd.Parameters.Add(New SqlParameter("@mcert_no", SqlDbType.VarChar)).Value() = oNV.cert_no
        sCmd.Parameters.Add(New SqlParameter("@mperiod", SqlDbType.VarChar)).Value() = oNV.period
        sCmd.Parameters.Add(New SqlParameter("@mcert_type", SqlDbType.VarChar)).Value() = oNV.cert_type
        sCmd.Parameters.Add(New SqlParameter("@mcartalloc", SqlDbType.VarChar)).Value() = oNV.cartalloc
        sCmd.Parameters.Add(New SqlParameter("@mannexno", SqlDbType.VarChar)).Value() = oNV.annexno
        sCmd.Parameters.Add(New SqlParameter("@manserial", SqlDbType.VarChar)).Value() = oNV.anserial
        sCmd.Parameters.Add(New SqlParameter("@mawb_bl_no", SqlDbType.VarChar)).Value() = oNV.awb_bl_no
        sCmd.Parameters.Add(New SqlParameter("@mlcno", SqlDbType.VarChar)).Value() = oNV.lcno
        sCmd.Parameters.Add(New SqlParameter("@mshbilldt", SqlDbType.DateTime)).Value() = oNV.shbilldt
        sCmd.Parameters.Add(New SqlParameter("@mcatii", SqlDbType.VarChar)).Value() = oNV.catii
        sCmd.Parameters.Add(New SqlParameter("@mcatiiconv", SqlDbType.Decimal)).Value() = oNV.catiiconv
        sCmd.Parameters.Add(New SqlParameter("@mBuyerOrderNo", SqlDbType.VarChar)).Value() = oNV.BuyerOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mprovordnr", SqlDbType.VarChar)).Value() = oNV.provordnr
        sCmd.Parameters.Add(New SqlParameter("@mCountryCode", SqlDbType.VarChar)).Value() = oNV.CountryCode
        sCmd.Parameters.Add(New SqlParameter("@msmpdesc", SqlDbType.VarChar)).Value() = oNV.smpdesc
        sCmd.Parameters.Add(New SqlParameter("@mndate", SqlDbType.DateTime)).Value() = oNV.ndate
        sCmd.Parameters.Add(New SqlParameter("@mrvdate", SqlDbType.DateTime)).Value() = oNV.rvdate
        sCmd.Parameters.Add(New SqlParameter("@mmrate", SqlDbType.Decimal)).Value() = oNV.mrate
        sCmd.Parameters.Add(New SqlParameter("@musha", SqlDbType.VarChar)).Value() = oNV.usha
        sCmd.Parameters.Add(New SqlParameter("@mugts", SqlDbType.VarChar)).Value() = oNV.ugts
        sCmd.Parameters.Add(New SqlParameter("@mrsno", SqlDbType.VarChar)).Value() = oNV.rsno
        sCmd.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.MaterialCode
        sCmd.Parameters.Add(New SqlParameter("@mmode", SqlDbType.VarChar)).Value() = oNV.mode
        sCmd.Parameters.Add(New SqlParameter("@mGroup", SqlDbType.VarChar)).Value() = oNV.Group
        sCmd.Parameters.Add(New SqlParameter("@mbank", SqlDbType.VarChar)).Value() = oNV.bank
        sCmd.Parameters.Add(New SqlParameter("@mcommpercnt", SqlDbType.Decimal)).Value() = oNV.commpercnt
        sCmd.Parameters.Add(New SqlParameter("@mmainfab", SqlDbType.VarChar)).Value() = oNV.mainfab
        sCmd.Parameters.Add(New SqlParameter("@mcountry", SqlDbType.VarChar)).Value() = oNV.country
        sCmd.Parameters.Add(New SqlParameter("@mbasestyl", SqlDbType.VarChar)).Value() = oNV.basestyl
        sCmd.Parameters.Add(New SqlParameter("@mcurvalue", SqlDbType.Decimal)).Value() = oNV.curvalue
        sCmd.Parameters.Add(New SqlParameter("@msize11", SqlDbType.VarChar)).Value() = oNV.size11
        sCmd.Parameters.Add(New SqlParameter("@msize12", SqlDbType.VarChar)).Value() = oNV.size12
        sCmd.Parameters.Add(New SqlParameter("@msize13", SqlDbType.VarChar)).Value() = oNV.size13
        sCmd.Parameters.Add(New SqlParameter("@msize14", SqlDbType.VarChar)).Value() = oNV.size14
        sCmd.Parameters.Add(New SqlParameter("@msize15", SqlDbType.VarChar)).Value() = oNV.size15
        sCmd.Parameters.Add(New SqlParameter("@msize16", SqlDbType.VarChar)).Value() = oNV.size16
        sCmd.Parameters.Add(New SqlParameter("@msize17", SqlDbType.VarChar)).Value() = oNV.size17
        sCmd.Parameters.Add(New SqlParameter("@msize18", SqlDbType.VarChar)).Value() = oNV.size18
        sCmd.Parameters.Add(New SqlParameter("@mqty11", SqlDbType.Decimal)).Value() = oNV.qty11
        sCmd.Parameters.Add(New SqlParameter("@mqty12", SqlDbType.Decimal)).Value() = oNV.qty12
        sCmd.Parameters.Add(New SqlParameter("@mqty13", SqlDbType.Decimal)).Value() = oNV.qty13
        sCmd.Parameters.Add(New SqlParameter("@mqty14", SqlDbType.Decimal)).Value() = oNV.qty14
        sCmd.Parameters.Add(New SqlParameter("@mqty15", SqlDbType.Decimal)).Value() = oNV.qty15
        sCmd.Parameters.Add(New SqlParameter("@mqty16", SqlDbType.Decimal)).Value() = oNV.qty16
        sCmd.Parameters.Add(New SqlParameter("@mqty17", SqlDbType.Decimal)).Value() = oNV.qty17
        sCmd.Parameters.Add(New SqlParameter("@mqty18", SqlDbType.Decimal)).Value() = oNV.qty18
        sCmd.Parameters.Add(New SqlParameter("@mOrderSerialNo", SqlDbType.VarChar)).Value() = oNV.OrderSerialNo
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
        sCmd.Parameters.Add(New SqlParameter("@mLCDiscount", SqlDbType.Decimal)).Value() = oNV.LCDiscount
        sCmd.Parameters.Add(New SqlParameter("@mShipped", SqlDbType.Bit)).Value() = oNV.Shipped
        sCmd.Parameters.Add(New SqlParameter("@mCustomerStyleName", SqlDbType.VarChar)).Value() = oNV.CustomerStyleName
        sCmd.Parameters.Add(New SqlParameter("@mIsShipped", SqlDbType.Bit)).Value() = oNV.IsShipped
        sCmd.Parameters.Add(New SqlParameter("@mStatus", SqlDbType.VarChar)).Value() = oNV.Status
        sCmd.Parameters.Add(New SqlParameter("@mPaymentREceiveFromBuyer", SqlDbType.Bit)).Value() = oNV.PaymentREceiveFromBuyer
        sCmd.Parameters.Add(New SqlParameter("@mStore", SqlDbType.VarChar)).Value() = oNV.Store
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = oNV.Season
        sCmd.Parameters.Add(New SqlParameter("@mburdept", SqlDbType.VarChar)).Value() = oNV.burdept
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
        sCmd.Parameters.Add(New SqlParameter("@mVariant", SqlDbType.VarChar)).Value() = oNV.sVariant
        sCmd.Parameters.Add(New SqlParameter("@mCustomerArticleNo", SqlDbType.VarChar)).Value() = oNV.CustomerArticleNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDescription", SqlDbType.VarChar)).Value() = oNV.InvoiceDescription
        sCmd.Parameters.Add(New SqlParameter("@mMerchandiser", SqlDbType.VarChar)).Value() = oNV.Merchandiser
        sCmd.Parameters.Add(New SqlParameter("@mPackNo", SqlDbType.VarChar)).Value() = oNV.PackNo
        sCmd.Parameters.Add(New SqlParameter("@mJobCardNo", SqlDbType.VarChar)).Value() = oNV.JobCardNo
        sCmd.Parameters.Add(New SqlParameter("@mCount", SqlDbType.VarChar)).Value() = oNV.Count
        sCmd.Parameters.Add(New SqlParameter("@mconstruction", SqlDbType.VarChar)).Value() = oNV.construction
        sCmd.Parameters.Add(New SqlParameter("@mCartonNo", SqlDbType.VarChar)).Value() = oNV.CartonNo
        sCmd.Parameters.Add(New SqlParameter("@mMRPRate", SqlDbType.Decimal)).Value() = oNV.MRPRate
        sCmd.Parameters.Add(New SqlParameter("@mIsRecalRequired", SqlDbType.Bit)).Value() = oNV.IsRecalRequired
        sCmd.Parameters.Add(New SqlParameter("@mOrderPrice", SqlDbType.Decimal)).Value() = oNV.OrderPrice
        sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Bit)).Value() = oNV.IsApproved
        sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)).Value() = oNV.ApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mJobCardDetailID", SqlDbType.VarChar)).Value() = oNV.JobCardDetailID
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value() = oNV.InvoiceID
        sCmd.Parameters.Add(New SqlParameter("@mUpdateMode", SqlDbType.VarChar)).Value() = oNV.UpdateMode
        sCmd.Parameters.Add(New SqlParameter("@mpcs", SqlDbType.Int)).Value() = oNV.pcs
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderDetailID", SqlDbType.VarChar)).Value() = oNV.SalesOrderDetailID
        sCmd.Parameters.Add(New SqlParameter("@mCustWorkOrderNO", SqlDbType.VarChar)).Value() = oNV.CustWorkOrderNO
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNoAutoGen", SqlDbType.VarChar)).Value() = oNV.InvoiceNoAutoGen
        sCmd.Parameters.Add(New SqlParameter("@mArticleCodification", SqlDbType.VarChar)).Value() = oNV.ArticleCodification
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceQuantity", SqlDbType.Decimal)).Value() = oNV.InvoiceQuantity
        sCmd.Parameters.Add(New SqlParameter("@mCBMSpace", SqlDbType.Decimal)).Value() = oNV.CBMSpace
        sCmd.Parameters.Add(New SqlParameter("@mOrderQty", SqlDbType.Decimal)).Value() = oNV.OrderQty
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversionRate4Tally", SqlDbType.Decimal)).Value() = oNV.CurrencyConversionRate4Tally
        sCmd.Parameters.Add(New SqlParameter("@mcurvalue4Tally", SqlDbType.Decimal)).Value() = oNV.curvalue4Tally
        sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSGSTValue", SqlDbType.Decimal)).Value() = oNV.SGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mDiscount", SqlDbType.Decimal)).Value() = oNV.Discount
        sCmd.Parameters.Add(New SqlParameter("@mHSNCode", SqlDbType.VarChar)).Value() = oNV.HSNCode
        sCmd.Parameters.Add(New SqlParameter("@mWareHouse", SqlDbType.VarChar)).Value() = oNV.WareHouse
        sCmd.Parameters.Add(New SqlParameter("@mFValue", SqlDbType.Decimal)).Value() = oNV.FValue
        sCmd.Parameters.Add(New SqlParameter("@mLastInvDate", SqlDbType.DateTime)).Value() = oNV.LastInvDate
        sCmd.Parameters.Add(New SqlParameter("@mReady2DispatchID", SqlDbType.VarChar)).Value() = oNV.Ready2DispatchID
        sCmd.Parameters.Add(New SqlParameter("@mInternalOrder", SqlDbType.VarChar)).Value() = oNV.InternalOrder
        sCmd.Parameters.Add(New SqlParameter("@mIsSampleOrder", SqlDbType.Bit)).Value() = oNV.IsSampleOrder
        sCmd.Parameters.Add(New SqlParameter("@mSampleOrderType", SqlDbType.VarChar)).Value() = oNV.SampleOrderType
        sCmd.Parameters.Add(New SqlParameter("@mArticleandColor", SqlDbType.VarChar)).Value() = oNV.ArticleandColor
        sCmd.Parameters.Add(New SqlParameter("@mNetValue", SqlDbType.Decimal)).Value() = oNV.NettValue




        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function UpdateInvoiceMain(ByVal oNV As strInvoice) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDINVMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mforbillamt", SqlDbType.Decimal)).Value() = oNV.forbillamt
        sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSGSTVlaue", SqlDbType.Decimal)).Value() = oNV.SGSTVlaue
        sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mGSTTotalValue", SqlDbType.Decimal)).Value() = oNV.GSTTotalValue
        sCmd.Parameters.Add(New SqlParameter("@mTotalValue", SqlDbType.Decimal)).Value() = oNV.TotalValue
        sCmd.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Int)).Value() = oNV.Quantity
        'sCmd.Parameters.Add(New SqlParameter("@mInvoiceValue", SqlDbType.Decimal)).Value() = oNV.InvoiceValue
        'sCmd.Parameters.Add(New SqlParameter("@mTCSPercentage", SqlDbType.Decimal)).Value() = oNV.TCSPercentage
        'sCmd.Parameters.Add(New SqlParameter("@mTCSValue", SqlDbType.Decimal)).Value() = oNV.TCSValue



        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function


    Public Function EditInvoiceMain(ByVal oNV As strInvoice) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "EDIINVMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
        'sCmd.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = oNV.InvoiceDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyer", SqlDbType.VarChar)).Value() = oNV.Buyer
        sCmd.Parameters.Add(New SqlParameter("@mAccount", SqlDbType.VarChar)).Value() = oNV.Account
        sCmd.Parameters.Add(New SqlParameter("@mShipper", SqlDbType.VarChar)).Value() = oNV.Shipper
        sCmd.Parameters.Add(New SqlParameter("@mBuyerDepartment", SqlDbType.VarChar)).Value() = oNV.BuyerDepartment
        sCmd.Parameters.Add(New SqlParameter("@mOrigin", SqlDbType.VarChar)).Value() = oNV.Origin
        sCmd.Parameters.Add(New SqlParameter("@mLCNo", SqlDbType.VarChar)).Value() = oNV.LCNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDescription", SqlDbType.VarChar)).Value() = oNV.InvoiceDescription
        sCmd.Parameters.Add(New SqlParameter("@mShippingBillNo", SqlDbType.VarChar)).Value() = oNV.ShippingBillNo
        'sCmd.Parameters.Add(New SqlParameter("@mShippingBillDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ShippingBillDate
        sCmd.Parameters.Add(New SqlParameter("@mMarks1", SqlDbType.VarChar)).Value() = oNV.Marks1
        sCmd.Parameters.Add(New SqlParameter("@mMarks2", SqlDbType.VarChar)).Value() = oNV.Marks2
        sCmd.Parameters.Add(New SqlParameter("@mMarks3", SqlDbType.VarChar)).Value() = oNV.Marks3
        sCmd.Parameters.Add(New SqlParameter("@mMarks4", SqlDbType.VarChar)).Value() = oNV.Marks4
        sCmd.Parameters.Add(New SqlParameter("@mMarks5", SqlDbType.VarChar)).Value() = oNV.Marks5
        sCmd.Parameters.Add(New SqlParameter("@mMarks6", SqlDbType.VarChar)).Value() = oNV.Marks6
        sCmd.Parameters.Add(New SqlParameter("@mMarks7", SqlDbType.VarChar)).Value() = oNV.Marks7
        sCmd.Parameters.Add(New SqlParameter("@mMarks8", SqlDbType.VarChar)).Value() = oNV.Marks8
        sCmd.Parameters.Add(New SqlParameter("@mModeOfShipment", SqlDbType.VarChar)).Value() = oNV.ModeOfShipment
        sCmd.Parameters.Add(New SqlParameter("@mAwbBillNo", SqlDbType.VarChar)).Value() = oNV.AwbBillNo
        'sCmd.Parameters.Add(New SqlParameter("@mAwbBillDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.AwbBillDate
        sCmd.Parameters.Add(New SqlParameter("@mHAwbBillNo", SqlDbType.VarChar)).Value() = oNV.HAwbBillNo
        'sCmd.Parameters.Add(New SqlParameter("@mHAwbBillDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.HAwbBillDate
        sCmd.Parameters.Add(New SqlParameter("@mDestination", SqlDbType.VarChar)).Value() = oNV.Destination
        sCmd.Parameters.Add(New SqlParameter("@mBanckAccount", SqlDbType.VarChar)).Value() = oNV.BanckAccount
        sCmd.Parameters.Add(New SqlParameter("@mVessel", SqlDbType.VarChar)).Value() = oNV.Vessel
        sCmd.Parameters.Add(New SqlParameter("@mBank", SqlDbType.VarChar)).Value() = oNV.Bank
        sCmd.Parameters.Add(New SqlParameter("@mCurrency", SqlDbType.VarChar)).Value() = oNV.Currency
        sCmd.Parameters.Add(New SqlParameter("@mCurrencyConversion", SqlDbType.Decimal)).Value() = oNV.CurrencyConversion
        sCmd.Parameters.Add(New SqlParameter("@mNature", SqlDbType.VarChar)).Value() = oNV.Nature
        sCmd.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
        sCmd.Parameters.Add(New SqlParameter("@mTotalValue", SqlDbType.Decimal)).Value() = oNV.TotalValue
        sCmd.Parameters.Add(New SqlParameter("@mFreight", SqlDbType.Decimal)).Value() = oNV.Freight
        sCmd.Parameters.Add(New SqlParameter("@mInsurance", SqlDbType.Decimal)).Value() = oNV.Insurance
        sCmd.Parameters.Add(New SqlParameter("@mAgentPercentage", SqlDbType.Decimal)).Value() = oNV.AgentPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCommission", SqlDbType.Decimal)).Value() = oNV.Commission
        sCmd.Parameters.Add(New SqlParameter("@mDbkPercentage", SqlDbType.Decimal)).Value() = oNV.DbkPercentage
        sCmd.Parameters.Add(New SqlParameter("@mDrawBack", SqlDbType.Decimal)).Value() = oNV.DrawBack
        sCmd.Parameters.Add(New SqlParameter("@mBankCertificate", SqlDbType.VarChar)).Value() = oNV.BankCertificate
        sCmd.Parameters.Add(New SqlParameter("@mBankAmount", SqlDbType.Decimal)).Value() = oNV.BankAmount
        sCmd.Parameters.Add(New SqlParameter("@mBankRate", SqlDbType.Decimal)).Value() = oNV.BankRate
        'sCmd.Parameters.Add(New SqlParameter("@mCertificateDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CertificateDate
        sCmd.Parameters.Add(New SqlParameter("@mTotalPackNo", SqlDbType.Decimal)).Value() = oNV.TotalPackNo
        sCmd.Parameters.Add(New SqlParameter("@mharmnsdco", SqlDbType.VarChar)).Value() = oNV.harmnsdco
        sCmd.Parameters.Add(New SqlParameter("@mNetWeight", SqlDbType.Decimal)).Value() = oNV.NetWeight
        sCmd.Parameters.Add(New SqlParameter("@mGrossWeight", SqlDbType.Decimal)).Value() = oNV.GrossWeight
        sCmd.Parameters.Add(New SqlParameter("@mdbkrecd", SqlDbType.Decimal)).Value() = oNV.dbkrecd
        sCmd.Parameters.Add(New SqlParameter("@maepccer_no", SqlDbType.VarChar)).Value() = oNV.aepccer_no
        'sCmd.Parameters.Add(New SqlParameter("@mcust_clear", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.cust_clear
        sCmd.Parameters.Add(New SqlParameter("@mrbicode", SqlDbType.VarChar)).Value() = oNV.rbicode
        sCmd.Parameters.Add(New SqlParameter("@mcot_rayon", SqlDbType.VarChar)).Value() = oNV.cot_rayon
        sCmd.Parameters.Add(New SqlParameter("@mshpblfob", SqlDbType.Decimal)).Value() = oNV.shpblfob
        sCmd.Parameters.Add(New SqlParameter("@mnetfobamt", SqlDbType.Decimal)).Value() = oNV.netfobamt
        sCmd.Parameters.Add(New SqlParameter("@mremark1", SqlDbType.VarChar)).Value() = oNV.remark1
        sCmd.Parameters.Add(New SqlParameter("@mstatus", SqlDbType.VarChar)).Value() = oNV.status
        'sCmd.Parameters.Add(New SqlParameter("@mshipdate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.shipdate
        'sCmd.Parameters.Add(New SqlParameter("@maepcdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.aepcdt
        'sCmd.Parameters.Add(New SqlParameter("@mdrwrecddt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value ''oNV.drwrecddt
        'sCmd.Parameters.Add(New SqlParameter("@mnegdate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.negdate
        sCmd.Parameters.Add(New SqlParameter("@mmarkrate", SqlDbType.Decimal)).Value() = oNV.markrate
        sCmd.Parameters.Add(New SqlParameter("@mcntrycode", SqlDbType.VarChar)).Value() = oNV.cntrycode
        'sCmd.Parameters.Add(New SqlParameter("@mrealstdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.realstdt
        sCmd.Parameters.Add(New SqlParameter("@musha", SqlDbType.VarChar)).Value() = oNV.usha
        sCmd.Parameters.Add(New SqlParameter("@mugts", SqlDbType.VarChar)).Value() = oNV.ugts
        'sCmd.Parameters.Add(New SqlParameter("@mprintdt", SqlDbType.DateTime)).Value() = Date.Now
        sCmd.Parameters.Add(New SqlParameter("@mpercent", SqlDbType.Decimal)).Value() = oNV.percent
        sCmd.Parameters.Add(New SqlParameter("@mplusamt", SqlDbType.Decimal)).Value() = oNV.plusamt
        sCmd.Parameters.Add(New SqlParameter("@mcntryname", SqlDbType.VarChar)).Value() = oNV.cntryname
        sCmd.Parameters.Add(New SqlParameter("@mdays", SqlDbType.Decimal)).Value() = oNV.days
        'sCmd.Parameters.Add(New SqlParameter("@mdue_date", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.due_date
        sCmd.Parameters.Add(New SqlParameter("@maccounted", SqlDbType.VarChar)).Value() = oNV.accounted
        sCmd.Parameters.Add(New SqlParameter("@maccountamt", SqlDbType.Decimal)).Value() = oNV.accountamt
        sCmd.Parameters.Add(New SqlParameter("@mmode", SqlDbType.VarChar)).Value() = oNV.mode
        sCmd.Parameters.Add(New SqlParameter("@mShipRate", SqlDbType.Decimal)).Value() = oNV.ShipRate
        sCmd.Parameters.Add(New SqlParameter("@mcourier", SqlDbType.VarChar)).Value() = oNV.courier
        sCmd.Parameters.Add(New SqlParameter("@mdocawb", SqlDbType.VarChar)).Value() = oNV.docawb
        'sCmd.Parameters.Add(New SqlParameter("@mdocsentdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.docsentdt
        'sCmd.Parameters.Add(New SqlParameter("@madvdocdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.advdocdt
        'sCmd.Parameters.Add(New SqlParameter("@minspdate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.inspdate
        sCmd.Parameters.Add(New SqlParameter("@madvdocdays", SqlDbType.Decimal)).Value() = oNV.advdocdays
        sCmd.Parameters.Add(New SqlParameter("@mtokenno", SqlDbType.VarChar)).Value() = oNV.tokenno
        'sCmd.Parameters.Add(New SqlParameter("@mtokendt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.tokendt
        sCmd.Parameters.Add(New SqlParameter("@mqlfbonus", SqlDbType.Decimal)).Value() = oNV.qlfbonus
        sCmd.Parameters.Add(New SqlParameter("@mmidamount", SqlDbType.Decimal)).Value() = oNV.midamount
        sCmd.Parameters.Add(New SqlParameter("@mdepb", SqlDbType.VarChar)).Value() = oNV.depb
        sCmd.Parameters.Add(New SqlParameter("@mdepbamt", SqlDbType.Decimal)).Value() = oNV.depbamt
        sCmd.Parameters.Add(New SqlParameter("@mdepbrcvd", SqlDbType.Decimal)).Value() = oNV.depbrcvd
        'sCmd.Parameters.Add(New SqlParameter("@mdepbrcvdon", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.depbrcvdon
        sCmd.Parameters.Add(New SqlParameter("@mdepbper", SqlDbType.Decimal)).Value() = oNV.depbper
        sCmd.Parameters.Add(New SqlParameter("@mlicencenr", SqlDbType.VarChar)).Value() = oNV.licencenr
        sCmd.Parameters.Add(New SqlParameter("@mlicenceamt", SqlDbType.Decimal)).Value() = oNV.licenceamt
        'sCmd.Parameters.Add(New SqlParameter("@mlicsoldon", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value ' oNV.licsoldon
        sCmd.Parameters.Add(New SqlParameter("@mlicsoldfor", SqlDbType.Decimal)).Value() = oNV.licsoldfor
        'sCmd.Parameters.Add(New SqlParameter("@mdepbappldt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.depbappldt
        sCmd.Parameters.Add(New SqlParameter("@mport", SqlDbType.VarChar)).Value() = oNV.port
        'sCmd.Parameters.Add(New SqlParameter("@mepcopydt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.epcopydt
        sCmd.Parameters.Add(New SqlParameter("@mforwarder", SqlDbType.VarChar)).Value() = oNV.forwarder
        sCmd.Parameters.Add(New SqlParameter("@mforbillno", SqlDbType.VarChar)).Value() = oNV.forbillno
        sCmd.Parameters.Add(New SqlParameter("@mforbillamt", SqlDbType.Decimal)).Value() = oNV.forbillamt
        'sCmd.Parameters.Add(New SqlParameter("@mforrcvdt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.forrcvdt
        'sCmd.Parameters.Add(New SqlParameter("@msentforver", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.sentforver
        'sCmd.Parameters.Add(New SqlParameter("@mverifieddt", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.verifieddt
        sCmd.Parameters.Add(New SqlParameter("@msuppnr", SqlDbType.VarChar)).Value() = oNV.suppnr
        sCmd.Parameters.Add(New SqlParameter("@msuppbbnnr", SqlDbType.VarChar)).Value() = oNV.suppbbnnr
        sCmd.Parameters.Add(New SqlParameter("@mwarehouse", SqlDbType.VarChar)).Value() = oNV.warehouse
        sCmd.Parameters.Add(New SqlParameter("@mswiftcode", SqlDbType.VarChar)).Value() = oNV.swiftcode
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBank", SqlDbType.VarChar)).Value() = oNV.CorrespondingBank
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBankAcount", SqlDbType.VarChar)).Value() = oNV.CorrespondingBankAcount
        sCmd.Parameters.Add(New SqlParameter("@mCorrespondingBankSwiftCode", SqlDbType.VarChar)).Value() = oNV.CorrespondingBankSwiftCode
        sCmd.Parameters.Add(New SqlParameter("@mMsrks9", SqlDbType.VarChar)).Value() = oNV.Msrks9
        sCmd.Parameters.Add(New SqlParameter("@mMarks10", SqlDbType.VarChar)).Value() = oNV.Marks10
        sCmd.Parameters.Add(New SqlParameter("@mSTRPercentage", SqlDbType.Decimal)).Value() = oNV.STRPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSTRAmount", SqlDbType.Decimal)).Value() = oNV.STRAmount
        sCmd.Parameters.Add(New SqlParameter("@mPAN_Number", SqlDbType.VarChar)).Value() = oNV.PAN_Number
        sCmd.Parameters.Add(New SqlParameter("@mVAT_TIN", SqlDbType.VarChar)).Value() = oNV.VAT_TIN
        sCmd.Parameters.Add(New SqlParameter("@mCST_TIN", SqlDbType.VarChar)).Value() = oNV.CST_TIN
        sCmd.Parameters.Add(New SqlParameter("@mExciseDuty", SqlDbType.Decimal)).Value() = oNV.ExciseDuty
        sCmd.Parameters.Add(New SqlParameter("@mVATAmount", SqlDbType.Decimal)).Value() = oNV.VATAmount
        sCmd.Parameters.Add(New SqlParameter("@mCessAmount", SqlDbType.Decimal)).Value() = oNV.CessAmount
        sCmd.Parameters.Add(New SqlParameter("@mEduCessAmount", SqlDbType.Decimal)).Value() = oNV.EduCessAmount
        sCmd.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
        sCmd.Parameters.Add(New SqlParameter("@mMinusAmount", SqlDbType.Decimal)).Value() = oNV.MinusAmount
        sCmd.Parameters.Add(New SqlParameter("@mFinancialYear", SqlDbType.VarChar)).Value() = oNV.FinancialYear
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceType", SqlDbType.VarChar)).Value() = oNV.InvoiceType
        sCmd.Parameters.Add(New SqlParameter("@mShipped", SqlDbType.Int)).Value() = oNV.Shipped
        sCmd.Parameters.Add(New SqlParameter("@mIsShipped", SqlDbType.Int)).Value() = oNV.IsShipped
        'sCmd.Parameters.Add(New SqlParameter("@mShippedDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ShippedDate
        sCmd.Parameters.Add(New SqlParameter("@mShippedBy", SqlDbType.VarChar)).Value() = oNV.ShippedBy
        'sCmd.Parameters.Add(New SqlParameter("@mMarkToShipDoneDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.MarkToShipDoneDate
        sCmd.Parameters.Add(New SqlParameter("@mPaymentReceiveFromBuyer", SqlDbType.Int)).Value() = oNV.PaymentReceiveFromBuyer
        'sCmd.Parameters.Add(New SqlParameter("@mPaymentReceiveDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.PaymentReceiveDate
        sCmd.Parameters.Add(New SqlParameter("@mBankRefNo", SqlDbType.VarChar)).Value() = oNV.BankRefNo
        'sCmd.Parameters.Add(New SqlParameter("@mBankRefDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.BankRefDate
        sCmd.Parameters.Add(New SqlParameter("@mContractNo", SqlDbType.VarChar)).Value() = oNV.ContractNo
        'sCmd.Parameters.Add(New SqlParameter("@mContractDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ContractDate
        sCmd.Parameters.Add(New SqlParameter("@mExciseInvoiceNo", SqlDbType.VarChar)).Value() = oNV.ExciseInvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mIsAdvanceReceived", SqlDbType.Int)).Value() = oNV.IsAdvanceReceived
        sCmd.Parameters.Add(New SqlParameter("@mAdvanceAmount", SqlDbType.Decimal)).Value() = oNV.AdvanceAmount
        sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = oNV.CreatedBy
        'sCmd.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.DateTime)).Value() = oNV.CreatedDate
        sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = oNV.ModifiedBy
        'sCmd.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value() = oNV.ModifiedDate
        sCmd.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = oNV.EnteredOnMachineID
        'sCmd.Parameters.Add(New SqlParameter("@mHandoverDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.HandoverDate
        sCmd.Parameters.Add(New SqlParameter("@mHangerPack", SqlDbType.VarChar)).Value() = oNV.HangerPack
        sCmd.Parameters.Add(New SqlParameter("@mCovering", SqlDbType.VarChar)).Value() = oNV.Covering
        sCmd.Parameters.Add(New SqlParameter("@mDeclaration", SqlDbType.VarChar)).Value() = oNV.Declaration
        sCmd.Parameters.Add(New SqlParameter("@mPostDescription", SqlDbType.VarChar)).Value() = oNV.PostDescription
        sCmd.Parameters.Add(New SqlParameter("@mLCDescription", SqlDbType.VarChar)).Value() = oNV.LCDescription
        sCmd.Parameters.Add(New SqlParameter("@mMark11", SqlDbType.VarChar)).Value() = oNV.Mark11
        sCmd.Parameters.Add(New SqlParameter("@mMark12", SqlDbType.VarChar)).Value() = oNV.Mark12
        sCmd.Parameters.Add(New SqlParameter("@mDiscountUpCharge", SqlDbType.VarChar)).Value() = oNV.DiscountUpCharge
        sCmd.Parameters.Add(New SqlParameter("@mPercentage", SqlDbType.Decimal)).Value() = oNV.Percentage
        sCmd.Parameters.Add(New SqlParameter("@mAmount", SqlDbType.Decimal)).Value() = oNV.Amount
        sCmd.Parameters.Add(New SqlParameter("@mShipperLC", SqlDbType.VarChar)).Value() = oNV.ShipperLC
        sCmd.Parameters.Add(New SqlParameter("@mBuyerLC", SqlDbType.VarChar)).Value() = oNV.BuyerLC
        sCmd.Parameters.Add(New SqlParameter("@mFOBValueInCurrency", SqlDbType.Decimal)).Value() = oNV.FOBValueInCurrency
        sCmd.Parameters.Add(New SqlParameter("@mTaxType", SqlDbType.VarChar)).Value() = oNV.TaxType
        sCmd.Parameters.Add(New SqlParameter("@mIsRecalRequired", SqlDbType.Int)).Value() = oNV.IsRecalRequired
        sCmd.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.Int)).Value() = oNV.IsApproved
        sCmd.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = oNV.ApprovedBy
        'sCmd.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ApprovedOn
        sCmd.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = oNV.ModuleName
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.ID
        sCmd.Parameters.Add(New SqlParameter("@mConsigneeCode", SqlDbType.VarChar)).Value() = oNV.ConsigneeCode
        sCmd.Parameters.Add(New SqlParameter("@mNotify1", SqlDbType.VarChar)).Value() = oNV.Notify1
        sCmd.Parameters.Add(New SqlParameter("@mNotify2", SqlDbType.VarChar)).Value() = oNV.Notify2
        sCmd.Parameters.Add(New SqlParameter("@mNotify3", SqlDbType.VarChar)).Value() = oNV.Notify3
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignEmpCode", SqlDbType.VarChar)).Value() = oNV.AuthSignEmpCode
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignEmpName", SqlDbType.VarChar)).Value() = oNV.AuthSignEmpName
        sCmd.Parameters.Add(New SqlParameter("@mAuthSignDesi", SqlDbType.VarChar)).Value() = oNV.AuthSignDesi
        sCmd.Parameters.Add(New SqlParameter("@mCSTorVAT", SqlDbType.Decimal)).Value() = oNV.CSTorVAT
        sCmd.Parameters.Add(New SqlParameter("@mEduCess", SqlDbType.Decimal)).Value() = oNV.EduCess
        sCmd.Parameters.Add(New SqlParameter("@mCESS", SqlDbType.Decimal)).Value() = oNV.CESS
        sCmd.Parameters.Add(New SqlParameter("@mExcise", SqlDbType.Decimal)).Value() = oNV.Excise
        sCmd.Parameters.Add(New SqlParameter("@mCT3No", SqlDbType.VarChar)).Value() = oNV.CT3No
        'sCmd.Parameters.Add(New SqlParameter("@mCT3Date", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CT3Date
        sCmd.Parameters.Add(New SqlParameter("@mARENo", SqlDbType.VarChar)).Value() = oNV.ARENo
        'sCmd.Parameters.Add(New SqlParameter("@mAREDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.AREDate
        sCmd.Parameters.Add(New SqlParameter("@mConveredBy", SqlDbType.VarChar)).Value() = oNV.ConveredBy
        sCmd.Parameters.Add(New SqlParameter("@mDeclaration1", SqlDbType.VarChar)).Value() = oNV.Declaration1
        sCmd.Parameters.Add(New SqlParameter("@mContainerSize", SqlDbType.VarChar)).Value() = oNV.ContainerSize
        sCmd.Parameters.Add(New SqlParameter("@mContainerName", SqlDbType.VarChar)).Value() = oNV.ContainerName
        sCmd.Parameters.Add(New SqlParameter("@mContainerSealNo", SqlDbType.VarChar)).Value() = oNV.ContainerSealNo
        sCmd.Parameters.Add(New SqlParameter("@mGoodsDescription", SqlDbType.VarChar)).Value() = oNV.GoodsDescription
        sCmd.Parameters.Add(New SqlParameter("@mMarksAndNos", SqlDbType.VarChar)).Value() = oNV.MarksAndNos
        sCmd.Parameters.Add(New SqlParameter("@mNoAndKindOfPackages", SqlDbType.VarChar)).Value() = oNV.NoAndKindOfPackages
        sCmd.Parameters.Add(New SqlParameter("@mPreCarriageBy", SqlDbType.VarChar)).Value() = oNV.PreCarriageBy
        sCmd.Parameters.Add(New SqlParameter("@mPreCarrierRecvPlace", SqlDbType.VarChar)).Value() = oNV.PreCarrierRecvPlace
        sCmd.Parameters.Add(New SqlParameter("@mPortDischarge", SqlDbType.VarChar)).Value() = oNV.PortDischarge
        sCmd.Parameters.Add(New SqlParameter("@mDestinationCountry", SqlDbType.VarChar)).Value() = oNV.DestinationCountry
        sCmd.Parameters.Add(New SqlParameter("@mAssortmentYear", SqlDbType.VarChar)).Value() = oNV.AssortmentYear
        sCmd.Parameters.Add(New SqlParameter("@mPaymentTerms", SqlDbType.VarChar)).Value() = oNV.PaymentTerms
        sCmd.Parameters.Add(New SqlParameter("@mDeliveryTerms", SqlDbType.VarChar)).Value() = oNV.DeliveryTerms
        sCmd.Parameters.Add(New SqlParameter("@mAREDuty", SqlDbType.Decimal)).Value() = oNV.AREDuty
        sCmd.Parameters.Add(New SqlParameter("@mAreCess", SqlDbType.Decimal)).Value() = oNV.AreCess
        sCmd.Parameters.Add(New SqlParameter("@mAREHCess", SqlDbType.Decimal)).Value() = oNV.AREHCess
        sCmd.Parameters.Add(New SqlParameter("@mContainerNo", SqlDbType.VarChar)).Value() = oNV.ContainerNo
        sCmd.Parameters.Add(New SqlParameter("@mFromPackNo", SqlDbType.VarChar)).Value() = oNV.FromPackNo
        sCmd.Parameters.Add(New SqlParameter("@mToPackNo", SqlDbType.VarChar)).Value() = oNV.ToPackNo
        'sCmd.Parameters.Add(New SqlParameter("@mEmailToCustDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.EmailToCustDate
        'sCmd.Parameters.Add(New SqlParameter("@mFreightFwdrPlotLetterDate", SqlDbType.DateTime)) - 12345)).Value() = oNV.FreightFwdrPlotLetterDate
        'sCmd.Parameters.Add(New SqlParameter("@mContainerApplnDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ContainerApplnDate
        sCmd.Parameters.Add(New SqlParameter("@mGSPSlNo", SqlDbType.VarChar)).Value() = oNV.GSPSlNo
        sCmd.Parameters.Add(New SqlParameter("@mOriginCriterion", SqlDbType.VarChar)).Value() = oNV.OriginCriterion
        'sCmd.Parameters.Add(New SqlParameter("@mStuffingDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.StuffingDate
        'sCmd.Parameters.Add(New SqlParameter("@mGateOpeningDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.GateOpeningDate
        'sCmd.Parameters.Add(New SqlParameter("@mCutOffDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CutOffDate
        'sCmd.Parameters.Add(New SqlParameter("@mClosingDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ClosingDate
        sCmd.Parameters.Add(New SqlParameter("@mSailingVesselDetails", SqlDbType.VarChar)).Value() = oNV.SailingVesselDetails
        sCmd.Parameters.Add(New SqlParameter("@mShipmentType", SqlDbType.VarChar)).Value() = oNV.ShipmentType
        sCmd.Parameters.Add(New SqlParameter("@mRevShipmentType", SqlDbType.VarChar)).Value() = oNV.RevShipmentType
        sCmd.Parameters.Add(New SqlParameter("@mHaltingCharges", SqlDbType.Decimal)).Value() = oNV.HaltingCharges
        sCmd.Parameters.Add(New SqlParameter("@mDemurrage", SqlDbType.Decimal)).Value() = oNV.Demurrage
        sCmd.Parameters.Add(New SqlParameter("@mVoyageNo", SqlDbType.VarChar)).Value() = oNV.VoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mETDFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETDFeederVessel
        'sCmd.Parameters.Add(New SqlParameter("@mETAFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETAFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mFeederVessel", SqlDbType.VarChar)).Value() = oNV.FeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mFeederVoyageNo", SqlDbType.VarChar)).Value() = oNV.FeederVoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mETAMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETAMotherVessel
        'sCmd.Parameters.Add(New SqlParameter("@mETDMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ETDMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mInternalSealNo", SqlDbType.VarChar)).Value() = oNV.InternalSealNo
        sCmd.Parameters.Add(New SqlParameter("@mCentralExciseSealNo", SqlDbType.VarChar)).Value() = oNV.CentralExciseSealNo
        sCmd.Parameters.Add(New SqlParameter("@mTypesOfBL", SqlDbType.VarChar)).Value() = oNV.TypesOfBL
        sCmd.Parameters.Add(New SqlParameter("@mCustomClearence", SqlDbType.VarChar)).Value() = oNV.CustomClearence
        sCmd.Parameters.Add(New SqlParameter("@mTransportCharges", SqlDbType.Decimal)).Value() = oNV.TransportCharges
        sCmd.Parameters.Add(New SqlParameter("@mCHACharges", SqlDbType.Decimal)).Value() = oNV.CHACharges
        sCmd.Parameters.Add(New SqlParameter("@mClearingCharges", SqlDbType.Decimal)).Value() = oNV.ClearingCharges
        sCmd.Parameters.Add(New SqlParameter("@mCFSCharges", SqlDbType.Decimal)).Value() = oNV.CFSCharges
        sCmd.Parameters.Add(New SqlParameter("@mForwardingCharges", SqlDbType.Decimal)).Value() = oNV.ForwardingCharges
        sCmd.Parameters.Add(New SqlParameter("@mGSPCharges", SqlDbType.Decimal)).Value() = oNV.GSPCharges
        sCmd.Parameters.Add(New SqlParameter("@mCourierCharges", SqlDbType.Decimal)).Value() = oNV.CourierCharges
        sCmd.Parameters.Add(New SqlParameter("@mInsuranceCharges", SqlDbType.Decimal)).Value() = oNV.InsuranceCharges
        sCmd.Parameters.Add(New SqlParameter("@mMiscCharges", SqlDbType.Decimal)).Value() = oNV.MiscCharges
        'sCmd.Parameters.Add(New SqlParameter("@mContainerArrivalDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.ContainerArrivalDate
        sCmd.Parameters.Add(New SqlParameter("@mMemo", SqlDbType.VarChar)).Value() = oNV.Memo
        'sCmd.Parameters.Add(New SqlParameter("@mDestinationArrivalDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.DestinationArrivalDate
        'sCmd.Parameters.Add(New SqlParameter("@mFreightFwdrPlotLetterExpDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.FreightFwdrPlotLetterExpDate
        sCmd.Parameters.Add(New SqlParameter("@mForwarderCode", SqlDbType.VarChar)).Value() = oNV.ForwarderCode
        sCmd.Parameters.Add(New SqlParameter("@mCHACode", SqlDbType.VarChar)).Value() = oNV.CHACode
        sCmd.Parameters.Add(New SqlParameter("@mRevVessel", SqlDbType.VarChar)).Value() = oNV.RevVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevVoyageNo", SqlDbType.VarChar)).Value() = oNV.RevVoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mRevETDMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETDMotherVessel
        'sCmd.Parameters.Add(New SqlParameter("@mRevETAMotherVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETAMotherVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevFeederVessel", SqlDbType.VarChar)).Value() = oNV.RevFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mRevFeederVoyageNo", SqlDbType.VarChar)).Value() = oNV.RevFeederVoyageNo
        'sCmd.Parameters.Add(New SqlParameter("@mRevETDFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETDFeederVessel
        'sCmd.Parameters.Add(New SqlParameter("@mRevETAFeederVessel", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.RevETAFeederVessel
        sCmd.Parameters.Add(New SqlParameter("@mCommodity", SqlDbType.VarChar)).Value() = oNV.Commodity
        sCmd.Parameters.Add(New SqlParameter("@mPremiumRate", SqlDbType.Decimal)).Value() = oNV.PremiumRate
        sCmd.Parameters.Add(New SqlParameter("@mPremiumAmount", SqlDbType.Decimal)).Value() = oNV.PremiumAmount
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNoAutoGen", SqlDbType.VarChar)).Value() = oNV.InvoiceNoAutoGen
        sCmd.Parameters.Add(New SqlParameter("@mCourierPayment", SqlDbType.VarChar)).Value() = oNV.CourierPayment
        sCmd.Parameters.Add(New SqlParameter("@mCourierNo", SqlDbType.VarChar)).Value() = oNV.CourierNo
        'sCmd.Parameters.Add(New SqlParameter("@mCourierDate", SqlDbType.DateTime)) - 12345)).Value() = System.DBNull.Value 'oNV.CourierDate
        sCmd.Parameters.Add(New SqlParameter("@mCartonDia", SqlDbType.VarChar)).Value() = oNV.CartonDia
        sCmd.Parameters.Add(New SqlParameter("@mOneCarton", SqlDbType.VarChar)).Value() = oNV.OneCarton
        sCmd.Parameters.Add(New SqlParameter("@mTotalOne", SqlDbType.VarChar)).Value() = oNV.TotalOne
        sCmd.Parameters.Add(New SqlParameter("@mFinalDestination", SqlDbType.VarChar)).Value() = oNV.FinalDestination
        sCmd.Parameters.Add(New SqlParameter("@mMatType", SqlDbType.VarChar)).Value() = oNV.MatType
        sCmd.Parameters.Add(New SqlParameter("@mAmtOfDutyPayable", SqlDbType.Decimal)).Value() = oNV.AmtOfDutyPayable
        sCmd.Parameters.Add(New SqlParameter("@mSubTotal", SqlDbType.Decimal)).Value() = oNV.SubTotal
        sCmd.Parameters.Add(New SqlParameter("@mInvYear", SqlDbType.VarChar)).Value() = oNV.InvYear
        sCmd.Parameters.Add(New SqlParameter("@mInvCode", SqlDbType.VarChar)).Value() = oNV.InvCode
        sCmd.Parameters.Add(New SqlParameter("@mDispatchFrom", SqlDbType.VarChar)).Value() = oNV.DispatchFrom
        sCmd.Parameters.Add(New SqlParameter("@mInoviceStatus", SqlDbType.VarChar)).Value() = oNV.InoviceStatus
        'sCmd.Parameters.Add(New SqlParameter("@mExpDocDate", SqlDbType.DateTime)))) - 12345)).Value() = System.DBNull.Value 'oNV.ExpDocDate
        sCmd.Parameters.Add(New SqlParameter("@mGoodsDescription2", SqlDbType.VarChar)).Value() = oNV.GoodsDescription2
        sCmd.Parameters.Add(New SqlParameter("@mRBBankName", SqlDbType.VarChar)).Value() = oNV.RBBankName
        sCmd.Parameters.Add(New SqlParameter("@mRBAccountNo", SqlDbType.VarChar)).Value() = oNV.RBAccountNo
        sCmd.Parameters.Add(New SqlParameter("@mRBSwiftCode", SqlDbType.VarChar)).Value() = oNV.RBSwiftCode
        sCmd.Parameters.Add(New SqlParameter("@mCurValue", SqlDbType.Decimal)).Value() = oNV.CurValue
        sCmd.Parameters.Add(New SqlParameter("@mAnnexureAPortOfLoading", SqlDbType.VarChar)).Value() = oNV.AnnexureAPortOfLoading
        sCmd.Parameters.Add(New SqlParameter("@mLCID", SqlDbType.VarChar)).Value() = oNV.LCID
        sCmd.Parameters.Add(New SqlParameter("@mLCValue", SqlDbType.Decimal)).Value() = oNV.LCValue
        sCmd.Parameters.Add(New SqlParameter("@mShippedLCValue", SqlDbType.Decimal)).Value() = oNV.ShippedLCValue
        sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mSGSTVlaue", SqlDbType.Decimal)).Value() = oNV.SGSTVlaue
        sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
        sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
        sCmd.Parameters.Add(New SqlParameter("@mFreightCharges", SqlDbType.Decimal)).Value() = oNV.FreightCharges
        sCmd.Parameters.Add(New SqlParameter("@mLoadingCharges", SqlDbType.Decimal)).Value() = oNV.LoadingCharges
        sCmd.Parameters.Add(New SqlParameter("@mInsuranceChager", SqlDbType.Decimal)).Value() = oNV.InsuranceChager
        sCmd.Parameters.Add(New SqlParameter("@mOtherCharges", SqlDbType.Decimal)).Value() = oNV.OtherCharges
        sCmd.Parameters.Add(New SqlParameter("@mDiscount", SqlDbType.Decimal)).Value() = oNV.Discount
        sCmd.Parameters.Add(New SqlParameter("@mGSTTotalValue", SqlDbType.Decimal)).Value() = oNV.GSTTotalValue
        sCmd.Parameters.Add(New SqlParameter("@mGSTInvNo", SqlDbType.VarChar)).Value() = oNV.GSTInvNo
        sCmd.Parameters.Add(New SqlParameter("@mInvNo2", SqlDbType.VarChar)).Value() = oNV.InvNo2
        sCmd.Parameters.Add(New SqlParameter("@mInvNo3", SqlDbType.VarChar)).Value() = oNV.InvNo3
        'sCmd.Parameters.Add(New SqlParameter("@mDUMMYINVDATE", SqlDbType.DateTime)))) - 12345)).Value() = System.DBNull.Value 'oNV.DUMMYINVDATE
        sCmd.Parameters.Add(New SqlParameter("@mFreightCGSTPer", SqlDbType.Decimal)).Value() = oNV.FreightCGSTPer
        sCmd.Parameters.Add(New SqlParameter("@mFreightCGSTVal", SqlDbType.Decimal)).Value() = oNV.FreightCGSTVal
        sCmd.Parameters.Add(New SqlParameter("@mFreightSGSTPer", SqlDbType.Decimal)).Value() = oNV.FreightSGSTPer
        sCmd.Parameters.Add(New SqlParameter("@mFreightSGSTVal", SqlDbType.Decimal)).Value() = oNV.FreightSGSTVal
        sCmd.Parameters.Add(New SqlParameter("@mFreightIGSTPer", SqlDbType.Decimal)).Value() = oNV.FreightIGSTPer
        sCmd.Parameters.Add(New SqlParameter("@mFreightIGSTVal", SqlDbType.Decimal)).Value() = oNV.FreightIGSTVal
        sCmd.Parameters.Add(New SqlParameter("@mFreightTotalVal", SqlDbType.Decimal)).Value() = oNV.FreightTotalVal
        sCmd.Parameters.Add(New SqlParameter("@mGSTValue", SqlDbType.Decimal)).Value() = oNV.GSTValue
        sCmd.Parameters.Add(New SqlParameter("@mSerialNoPrefix", SqlDbType.VarChar)).Value() = oNV.SerialNoPrefix
        sCmd.Parameters.Add(New SqlParameter("@mInternalOrder", SqlDbType.VarChar)).Value() = oNV.InternalOrder

        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    'Public Function UpdateInvoiceMain(ByVal oNV As strInvoice) As Boolean

    '    Dim sCmd As New SqlCommand
    '    Dim daLoadInvoices As New SqlDataAdapter
    '    Dim dsLoadInvoices As New DataSet

    '    sCmd.Connection = sCnn
    '    sCmd.CommandText = "proc_Invoice"
    '    sCmd.CommandType = CommandType.StoredProcedure


    '    sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDINVMAIN"
    '    sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
    '    sCmd.Parameters.Add(New SqlParameter("@mforbillamt", SqlDbType.Decimal)).Value() = oNV.forbillamt
    '    sCmd.Parameters.Add(New SqlParameter("@mCGSTPercentage", SqlDbType.Decimal)).Value() = oNV.CGSTPercentage
    '    sCmd.Parameters.Add(New SqlParameter("@mCGSTValue", SqlDbType.Decimal)).Value() = oNV.CGSTValue
    '    sCmd.Parameters.Add(New SqlParameter("@mSGSTPercentage", SqlDbType.Decimal)).Value() = oNV.SGSTPercentage
    '    sCmd.Parameters.Add(New SqlParameter("@mSGSTVlaue", SqlDbType.Decimal)).Value() = oNV.SGSTVlaue
    '    sCmd.Parameters.Add(New SqlParameter("@mIGSTPercentage", SqlDbType.Decimal)).Value() = oNV.IGSTPercentage
    '    sCmd.Parameters.Add(New SqlParameter("@mIGSTValue", SqlDbType.Decimal)).Value() = oNV.IGSTValue
    '    sCmd.Parameters.Add(New SqlParameter("@mGSTTotalValue", SqlDbType.Decimal)).Value() = oNV.GSTTotalValue
    '    sCmd.Parameters.Add(New SqlParameter("@mTotalValue", SqlDbType.Decimal)).Value() = oNV.TotalValue

    '    'sCnn.Close()
    '    sCnn.Open()

    '    ''sCmd.ExecuteNonQuery()

    '    Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

    '    If Val(sRes) = 0 Then
    '        sCnn.Close()
    '        Return True
    '    Else
    '        Return False
    '        'setError(Val(sRes))
    '    End If

    '    sCnn.Close()


    '    Return False


    'End Function

    Public Function UpdateSalesOrderDetails(ByVal ID As String, ByVal ShpdQty As Integer, ByVal OrdSta As String, ByVal LastInvDate As Date) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDSOD"
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = ID
        sCmd.Parameters.Add(New SqlParameter("@mShippedQuantity", SqlDbType.Decimal)).Value() = ShpdQty
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = OrdSta
        sCmd.Parameters.Add(New SqlParameter("@mLastInvDate", SqlDbType.DateTime)).Value() = System.DBNull.Value 'LastInvDate

        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function UpdateSalesOrder(ByVal SalesOrderNo As String, ByVal ShpdQty As Integer, ByVal OrdSta As String) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDSO"
        sCmd.Parameters.Add(New SqlParameter("@mShippedQuantity", SqlDbType.Decimal)).Value() = ShpdQty
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = OrdSta
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = SalesOrderNo

        'sCnn.Close()
        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function UpdateReadyToDispatch(ByVal ID As String, ByVal InvoiceNo As String, ByVal InvoiceDate As Date) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDR2D"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoicedate", SqlDbType.DateTime)).Value() = InvoiceDate
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = ID


        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function UpdatePackingDetail(ByVal ID As String, ByVal InvoiceNo As String, ByVal InvoiceID As String, ByVal FinYear As Integer) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet
        Dim daLoadPKGDtl As New SqlDataAdapter
        Dim dsLoadPKGDtl As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPKGDTL"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = Microsoft.VisualBasic.Left(InvoiceNo, 20)
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value() = InvoiceID
        sCmd.Parameters.Add(New SqlParameter("@mInvYear", SqlDbType.Int)).Value() = FinYear
        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = ID

        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar '
        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            'setError(Val(sRes))
        End If

        sCnn.Close()
        ''Coding for Updating DCCartonNo
        Dim sCmd1 As New SqlCommand
        sCmd1.Connection = sCnn
        sCmd1.CommandText = "proc_Invoice"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPKGDTL"
        sCmd1.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = Microsoft.VisualBasic.Left(InvoiceNo, 20)
        sCmd1.Parameters.Add(New SqlParameter("@mInvoiceID", SqlDbType.VarChar)).Value() = InvoiceID
        sCmd1.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = ID

        dsLoadPKGDtl.Clear()
        daLoadPKGDtl = New SqlDataAdapter(sCmd1)
        daLoadPKGDtl.Fill(dsLoadPKGDtl, "Packing")



        Dim i As Integer = 0
        For i = 0 To dsLoadPKGDtl.Tables(0).Rows.Count - 1
            sID = dsLoadPKGDtl.Tables(0).Rows(i).Item("ID").ToString

            Dim sCmd2 As New SqlCommand
            sCmd2.Connection = sCnn
            sCmd2.CommandText = "proc_Invoice"
            sCmd2.CommandType = CommandType.StoredProcedure

            sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPKGDTLBOXNO"
            sCmd2.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = sID
            sCmd2.Parameters.Add(New SqlParameter("@mInvYear", SqlDbType.Int)).Value() = FinYear

            sCnn.Open()

            Dim sRes2 As String = sCmd2.ExecuteScalar '
            sCnn.Close()
        Next
        ''Coding for Updating DCCartonNo

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function UpdatePackingDetailAfterDelete(ByVal ID As String, ByVal InvoiceNo As String, ByVal InvoiceID As String) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDPKGDTLDLT"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = Microsoft.VisualBasic.Left(InvoiceNo, 20)
        

        'sCnn.Close()

        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function UpdateReadyToDispatchAfterDelete(ByVal ID As String, ByVal InvoiceNo As String, ByVal InvoiceDate As Date) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "UPDR2DDLT"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceDate", SqlDbType.DateTime)).Value() = System.DBNull.Value
        


        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function DeleteInvoiceDetail(ByVal InvoiceNo As String) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "DELINVOICEDTL"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = InvoiceNo



        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function DeleteInvoiceMain(ByVal InvoiceNo As String) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "DELINVOICEMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = InvoiceNo



        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function CancelInvoiceMain(ByVal InvoiceNo As String, ByVal Remarks As String) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "CANCELINVOICEMAIN"
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = InvoiceNo
        sCmd.Parameters.Add(New SqlParameter("@mRemark1", SqlDbType.VarChar)).Value() = Remarks



        sCnn.Open()

        ''sCmd.ExecuteNonQuery()

        Dim sRes As String = sCmd.ExecuteScalar '.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
            Return True
        Else
            Return False
            'setError(Val(sRes))
        End If

        sCnn.Close()


        Return False


    End Function

    Public Function LoadAllCartons(ByVal sJobcardNo As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_Invoice"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELJOBCARD"
        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

#End Region

End Class