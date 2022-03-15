Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strPPCProductionPackingListHeader
    Dim PKID As Integer
    Dim FKYear As Integer
    Dim Month As Integer
    Dim FKSection As String
    Dim OrderType As String
    Dim FKSeason As Integer
    Dim PPLNo As Integer
    Dim PPLDate As Date
    Dim PPLWeek As String
    Dim RevisionNo As Integer
    Dim FKOrderHeader As Integer
    Dim TypeofPacking As String
    Dim FKAssortment As Integer
    Dim BoxQuantity As Integer
    Dim BoxPackingType As String
    Dim TotalBoxes As Integer
    Dim Remarks As String
    Dim CreatedBy As Integer
    Dim CreatedDt As String
    Dim ModifiedBy As Integer
    Dim ModifiedDt As String
    Dim DeletedBy As Integer
    Dim DeletedDt As String

End Structure

Public Structure strUpperPackingDetail

    Dim ID As String
    Dim SODID As String
    Dim BoxNo As Integer
    Dim Quantity As Integer
    Dim Quantity01 As Integer
    Dim Quantity02 As Integer
    Dim Quantity03 As Integer
    Dim Quantity04 As Integer
    Dim Quantity05 As Integer
    Dim Quantity06 As Integer
    Dim Quantity07 As Integer
    Dim Quantity08 As Integer
    Dim Quantity09 As Integer
    Dim Quantity10 As Integer
    Dim Quantity11 As Integer
    Dim Quantity12 As Integer
    Dim Quantity13 As Integer
    Dim Quantity14 As Integer
    Dim Quantity15 As Integer
    Dim Quantity16 As Integer
    Dim Quantity17 As Integer
    Dim Quantity18 As Integer

    

End Structure

Public Structure strPPCInvoiceHeader

    Dim PKID As Integer
    Dim FKYear As Integer
    Dim Month As Integer
    Dim MonthSlNo As Integer
    Dim YearSlNo As Integer
    Dim FKSeason As Integer
    Dim SeasonSlNo As Integer
    Dim InvoiceType As String
    Dim InvoiceTypeSlNo As Integer
    Dim InvNo As Integer
    Dim InvoiceNo As String
    Dim InvDate As Date
    Dim Week As String
    Dim FKCustomer As Integer
    Dim BuyerOrderNo As String
    Dim OtherReference1 As String
    Dim OtherReference2 As String
    Dim FKAttnConsignee As Integer
    Dim FKConsignee As Integer
    Dim FKAttnBuyer As Integer
    Dim FKBuyer As Integer
    Dim FKAttnNotify As Integer
    Dim FKNotify As Integer
    Dim FKPreCarriageBy As Integer
    Dim FKPlaceofReceipt As Integer
    Dim FKVesselorFlight As Integer
    Dim FKPortofLoading As Integer
    Dim FKPortofDischarge As Integer
    Dim FKFinalDestination As Integer
    Dim FKCountryofFinalDestination As Integer
    Dim TOD01 As String
    Dim TOD02 As String
    Dim TOD03 As String
    Dim TOD04 As String
    Dim TOD05 As String
    Dim TOD06 As String
    Dim TOD07 As String
    Dim TOD08 As String
    Dim TOD09 As String
    Dim TOD10 As String
    Dim FKOurBankDetails As Integer
    Dim FKMark1 As Integer
    Dim FKMark2 As Integer
    Dim FKMark3 As Integer
    Dim BoxFrom As String
    Dim BoxTo As Integer
    Dim NoAndKindofPkgs As String
    Dim FKDescription1 As Integer
    Dim FKDescripton2 As Integer
    Dim FKDescription3 As Integer
    Dim FKDescription4 As Integer
    Dim FKCurrency As Integer
    Dim TotalBoxes As Integer
    Dim TotalPairs As Integer
    Dim PairsValue As Decimal
    Dim FKLess As Integer
    Dim LessPercentage As Decimal
    Dim LessValue As Decimal
    Dim FKAdd As Integer
    Dim AddPercentage As Decimal
    Dim AddValue As Decimal
    Dim Commission As Decimal
    Dim Insurance As Decimal
    Dim Freight As Decimal
    Dim NettAmount As Decimal
    Dim NettAmountinWords As String
    Dim GrossWeight As Decimal
    Dim NettWeight As Decimal
    Dim TotalSqFt As Decimal
    Dim ARE3No As String
    Dim ARE3Dt As Date
    Dim Container1No As String
    Dim Container1Seal As String
    Dim Container2No As String
    Dim Container2Seal As String
    Dim Container3No As String
    Dim Container3Seal As String
    Dim FKPackSize1 As Integer
    Dim FKPackSize2 As Integer
    Dim FKPackSize3 As Integer
    Dim FKPackSize4 As Integer
    Dim FKPackSize5 As Integer
    Dim FKDeclaration1 As Integer
    Dim FKDeclaration2 As Integer
    Dim FKDeclaration3 As Integer
    Dim FKDeclaration4 As Integer
    Dim FKDeclaration5 As Integer
    Dim CreatedBy As Integer
    Dim CreatedDt As String
    Dim ModifiedBy As Integer
    Dim ModifiedDt As String
    Dim DeletedBy As Integer
    Dim DeletedDt As String
    Dim RevisionNo As Integer
    Dim ConsiderAsShipped As Integer
    Dim Shipped As Integer
    Dim ShippedOn As Date
    Dim ExciseDutyPercentage As Decimal
    Dim ExciseDutyValue As Decimal
    Dim CessPercentage As Decimal
    Dim CessValue As Decimal
    Dim SHCessPercentage As Decimal
    Dim SHCessValue As Decimal
    Dim DutyPayable As Decimal
    Dim SubTotal As Decimal
    Dim VARorCSTPercentage As Decimal
    Dim VARorCSTValue As Decimal
    Dim GrandTotal As Decimal
    Dim DutyPaidInWords As String
    Dim ContainerSize As String
    Dim ExchangeRate As Decimal
    Dim FKSignedBy As Integer
    Dim GrandTotalinWords As String
    Dim ModeofTransport As String
    Dim TimeofIssue As String
    Dim TimeofRemoval As String
    Dim NettAmountinIRS As Decimal
    Dim DescCertificate As String

End Structure

Public Structure strPPCInvoiceDetail
    Dim PKID As Integer
    Dim FKInvoiceHeader As Integer
    Dim FKOrderDetail As Integer
    Dim BoxFrom As Integer
    Dim BoxTo As Integer
    Dim TotalBoxes As Integer
    Dim Quantity As Integer
    Dim Rate As Decimal
    Dim Value As Decimal
End Structure

Public Structure strPPCInvoiceHeaderPackages
    Dim nPKID As Integer
    Dim nFKInvoice As Integer
    Dim nPackage01 As Integer
    Dim nPairs01 As Integer
    Dim nTotalPairs01 As Integer
    Dim nPackage02 As Integer
    Dim nPairs02 As Integer
    Dim nTotalPairs02 As Integer
    Dim nPackage03 As Integer
    Dim nPairs03 As Integer
    Dim nTotalPairs03 As Integer
    Dim nPackage04 As Integer
    Dim nPairs04 As Integer
    Dim nTotalPairs04 As Integer
    Dim nPackage05 As Integer
    Dim nPairs05 As Integer
    Dim nTotalPairs05 As Integer
    Dim nPackage06 As Integer
    Dim nPairs06 As Integer
    Dim nTotalPairs06 As Integer
    Dim nPackage07 As Integer
    Dim nPairs07 As Integer
    Dim nTotalPairs07 As Integer
    Dim nPackage08 As Integer
    Dim nPairs08 As Integer
    Dim nTotalPairs08 As Integer
    Dim nPackage09 As Integer
    Dim nPairs09 As Integer
    Dim nTotalPairs09 As Integer
    Dim nPackage10 As Integer
    Dim nPairs10 As Integer
    Dim nTotalPairs10 As Integer
    Dim nPackage11 As Integer
    Dim nPairs11 As Integer
    Dim nTotalPairs11 As Integer
    Dim nPackage12 As Integer
    Dim nPairs12 As Integer
    Dim nTotalPairs12 As Integer
    Dim nPackage13 As Integer
    Dim nPairs13 As Integer
    Dim nTotalPairs13 As Integer
    Dim nPackage14 As Integer
    Dim nPairs14 As Integer
    Dim nTotalPairs14 As Integer
    Dim nPackage15 As Integer
    Dim nPairs15 As Integer
    Dim nTotalPairs15 As Integer
    Dim nTotalPkgs As Integer
    Dim nTotalPairsH As Integer
End Structure
#End Region

Public Class PackingList

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim daSelPPL As New SqlDataAdapter

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

#Region "Functions"

    Public Function VerifyProductionPackingListStatus(ByVal nFKOrderHeader As Integer) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsSelPPL As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Inv_PL_Label"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPRODPL"
            sCmd.Parameters.Add(New SqlParameter("@mFKOrderHeader", SqlDbType.Int)).Value() = nFKOrderHeader


            daSelPPL = New SqlDataAdapter(sCmd)
            daSelPPL.Fill(dsSelPPL, "PPL")

            'nRowCount = dsOrderDtls.Tables(0).Rows.Count

            If dsSelPPL.Tables(0).Rows.Count > 0 Then
                'frmMDI.cfrmPackingListandlabels.sPackingListGenerated = "Y"
            Else
                'frmMDI.cfrmPackingListandlabels.sPackingListGenerated = "N"
            End If


        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function


    Public Function LoadPackedQuantity(ByVal nFKOrderDtl As Integer) As Boolean
        Try
            Dim sCmd As New SqlCommand
            Dim dsSelPPL As New DataSet

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Inv_PL_Label"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "PKDQTY"
            sCmd.Parameters.Add(New SqlParameter("@mFKOrderDetail", SqlDbType.Int)).Value() = nFKOrderDtl

            daSelPPL = New SqlDataAdapter(sCmd)
            daSelPPL.Fill(dsSelPPL, "TempPL")


            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty01.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity01")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty02.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity02")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty03.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity03")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty04.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity04")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty05.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity05")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty06.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity06")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty07.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity07")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty08.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity08")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty09.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity09")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty10.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity10")

            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty11.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity11")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty12.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity12")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty13.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity13")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty14.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity14")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty15.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity15")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty16.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity16")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty17.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity17")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty18.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity18")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty19.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity19")
            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQty20.Text = dsSelPPL.Tables(0).Rows(0).Item("Quantity20")

            'frmMDI.cfrmPackingListandlabels.tbPlGeneratedQtyTotal.Text = dsSelPPL.Tables(0).Rows(0).Item("TotalQty")



        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function InsertProductionPackingListHeader(ByVal oNV As strPPCProductionPackingListHeader) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsPPLHeader As New DataSet

            'Dim myRec As strCRMCustomerOrderHeader

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Inv_PL_Label"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTHEADER"
            sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value = oNV.PKID
            sCmd.Parameters.Add(New SqlParameter("@mFKYear", SqlDbType.Int)).Value = oNV.FKYear
            sCmd.Parameters.Add(New SqlParameter("@mMonth", SqlDbType.Int)).Value = oNV.Month
            sCmd.Parameters.Add(New SqlParameter("@mFKSection", SqlDbType.VarChar)).Value = oNV.FKSection
            sCmd.Parameters.Add(New SqlParameter("@mOrderType", SqlDbType.VarChar)).Value = oNV.OrderType
            sCmd.Parameters.Add(New SqlParameter("@mFKSeason", SqlDbType.Int)).Value = oNV.FKSeason
            sCmd.Parameters.Add(New SqlParameter("@mPPLNo", SqlDbType.Int)).Value = oNV.PPLNo
            sCmd.Parameters.Add(New SqlParameter("@mPPLDate", SqlDbType.DateTime)).Value = oNV.PPLDate
            sCmd.Parameters.Add(New SqlParameter("@mPPLWeek", SqlDbType.VarChar)).Value = oNV.PPLWeek
            sCmd.Parameters.Add(New SqlParameter("@mRevisionNo", SqlDbType.Int)).Value = oNV.RevisionNo
            sCmd.Parameters.Add(New SqlParameter("@mFKOrderHeader", SqlDbType.Int)).Value = oNV.FKOrderHeader
            sCmd.Parameters.Add(New SqlParameter("@mTypeofPacking", SqlDbType.VarChar)).Value = oNV.TypeofPacking
            sCmd.Parameters.Add(New SqlParameter("@mFKAssortment", SqlDbType.Int)).Value = oNV.FKAssortment
            sCmd.Parameters.Add(New SqlParameter("@mBoxQuantity", SqlDbType.Int)).Value = oNV.BoxQuantity
            sCmd.Parameters.Add(New SqlParameter("@mBoxPackingType", SqlDbType.VarChar)).Value = oNV.BoxPackingType
            sCmd.Parameters.Add(New SqlParameter("@mTotalBoxes", SqlDbType.Int)).Value = oNV.TotalBoxes
            sCmd.Parameters.Add(New SqlParameter("@mRemarks", SqlDbType.VarChar)).Value = oNV.Remarks
            sCmd.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.Int)).Value = oNV.CreatedBy
            sCmd.Parameters.Add(New SqlParameter("@mCreatedDt", SqlDbType.VarChar)).Value = oNV.CreatedDt
            sCmd.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.Int)).Value = oNV.ModifiedBy
            sCmd.Parameters.Add(New SqlParameter("@mModifiedDt", SqlDbType.VarChar)).Value = oNV.ModifiedDt
            sCmd.Parameters.Add(New SqlParameter("@mDeletedBy", SqlDbType.Int)).Value = oNV.DeletedBy
            sCmd.Parameters.Add(New SqlParameter("@mDeletedDt", SqlDbType.VarChar)).Value = oNV.DeletedDt

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function InsertProductionPackingListDetail(ByVal oNV As strUpperPackingDetail) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsPPLDetail As New DataSet

            'Dim myRec As strCRMCustomerOrderDetail

            sCmd.Connection = sCnn
            sCmd.CommandText = "KHLI_OrderDetails"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTPL"
            sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value = oNV.ID
            sCmd.Parameters.Add(New SqlParameter("@mSODID", SqlDbType.VarChar)).Value = oNV.SODID
            sCmd.Parameters.Add(New SqlParameter("@mBoxNo", SqlDbType.Int)).Value = oNV.BoxNo
            sCmd.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Int)).Value = oNV.Quantity
            sCmd.Parameters.Add(New SqlParameter("@mQuantity01", SqlDbType.Int)).Value = oNV.Quantity01
            sCmd.Parameters.Add(New SqlParameter("@mQuantity02", SqlDbType.Int)).Value = oNV.Quantity02
            sCmd.Parameters.Add(New SqlParameter("@mQuantity03", SqlDbType.Int)).Value = oNV.Quantity03
            sCmd.Parameters.Add(New SqlParameter("@mQuantity04", SqlDbType.Int)).Value = oNV.Quantity04
            sCmd.Parameters.Add(New SqlParameter("@mQuantity05", SqlDbType.Int)).Value = oNV.Quantity05
            sCmd.Parameters.Add(New SqlParameter("@mQuantity06", SqlDbType.Int)).Value = oNV.Quantity06
            sCmd.Parameters.Add(New SqlParameter("@mQuantity07", SqlDbType.Int)).Value = oNV.Quantity07
            sCmd.Parameters.Add(New SqlParameter("@mQuantity08", SqlDbType.Int)).Value = oNV.Quantity08
            sCmd.Parameters.Add(New SqlParameter("@mQuantity09", SqlDbType.Int)).Value = oNV.Quantity09
            sCmd.Parameters.Add(New SqlParameter("@mQuantity10", SqlDbType.Int)).Value = oNV.Quantity10
            sCmd.Parameters.Add(New SqlParameter("@mQuantity11", SqlDbType.Int)).Value = oNV.Quantity11
            sCmd.Parameters.Add(New SqlParameter("@mQuantity12", SqlDbType.Int)).Value = oNV.Quantity12
            sCmd.Parameters.Add(New SqlParameter("@mQuantity13", SqlDbType.Int)).Value = oNV.Quantity13
            sCmd.Parameters.Add(New SqlParameter("@mQuantity14", SqlDbType.Int)).Value = oNV.Quantity14
            sCmd.Parameters.Add(New SqlParameter("@mQuantity15", SqlDbType.Int)).Value = oNV.Quantity15
            sCmd.Parameters.Add(New SqlParameter("@mQuantity16", SqlDbType.Int)).Value = oNV.Quantity16
            sCmd.Parameters.Add(New SqlParameter("@mQuantity17", SqlDbType.Int)).Value = oNV.Quantity17
            sCmd.Parameters.Add(New SqlParameter("@mQuantity18", SqlDbType.Int)).Value = oNV.Quantity18
            
            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

   
    Public Function LoadAssortmentDetails(ByVal nFKAssortment As Integer) As Boolean
        Try
            Dim sCmd1 As New SqlCommand
            Dim dsAssortmentDtls As New DataSet
            sCmd1.Connection = sCnn
            sCmd1.CommandText = "Op_OrdersRevised"
            sCmd1.CommandType = CommandType.StoredProcedure

            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELASSORTDTLS"
            sCmd1.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = nFKAssortment

            daSelPPL = New SqlDataAdapter(sCmd1)
            daSelPPL.Fill(dsAssortmentDtls, "ASSORTMENT")

            If dsAssortmentDtls.Tables(0).Rows.Count > 0 Then
                'frmMDI.cfrmPackingListandlabels.nInsQuantity01 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size01")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity02 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size02")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity03 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size03")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity04 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size04")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity05 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size05")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity06 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size06")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity07 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size07")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity08 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size08")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity09 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size09")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity10 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size10")

                'frmMDI.cfrmPackingListandlabels.nInsQuantity11 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size11")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity12 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size12")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity13 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size13")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity14 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size14")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity15 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size15")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity16 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size16")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity17 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size17")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity18 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size18")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity19 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size19")
                'frmMDI.cfrmPackingListandlabels.nInsQuantity20 = dsAssortmentDtls.Tables(0).Rows(0).Item("Size20")

                'frmMDI.cfrmPackingListandlabels.nInsTotalQuantity = dsAssortmentDtls.Tables(0).Rows(0).Item("TotalPairs")
            End If

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Public Function UpdatePPLDetailFullAndPartQty(ByVal nFKProdPLHeader As Integer, ByVal nFKOrderDetail As Integer, ByVal nFullQty As Integer, ByVal nPartQty As Integer) As Boolean

        Try
            Dim sCmd As New SqlCommand
            Dim dsInsPPLDetail As New DataSet

            'Dim myRec As strCRMCustomerOrderDetail

            sCmd.Connection = sCnn
            sCmd.CommandText = "Op_Inv_PL_Label"
            sCmd.CommandType = CommandType.StoredProcedure

            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEPPLFAPQTY"
            sCmd.Parameters.Add(New SqlParameter("@mFKProdPLHeader", SqlDbType.Int)).Value = nFKProdPLHeader
            sCmd.Parameters.Add(New SqlParameter("@mFKOrderDetail", SqlDbType.Int)).Value = nFKOrderDetail
            sCmd.Parameters.Add(New SqlParameter("@mFullQty", SqlDbType.Int)).Value = nFullQty
            sCmd.Parameters.Add(New SqlParameter("@mPartQty", SqlDbType.Int)).Value = nPartQty

            sCnn.Open()

            Dim sRes As String = sCmd.ExecuteScalar

            If Val(sRes) = 0 Then
                sCnn.Close()
                Return True
            Else
                Return False
                setError(Val(sRes))
            End If

            sCnn.Close()

        Catch exp As SqlException

            sCnn.Close()
            sErrMsg = exp.Message
            Return False

        Catch exp As Exception
            'HandleException(Me.Name, exp)
            sCnn.Close()
            sErrMsg = exp.Message
            Return False
        End Try
    End Function

    Private Sub setError(ByVal nNo As Integer)
        If nNo = 1 Then
            sErrCode = "NOREQPARAM"
            sErrMsg = "Required parameters value not specified"
        ElseIf nNo = 2 Then
            sErrCode = "INVALIDPARAM"
            sErrMsg = "Invalid parameters value specified"
        ElseIf nNo = 3 Then
            sErrCode = "SQLERROR"
            sErrMsg = "Internal SQL Server Error"
        ElseIf nNo = 4 Then
            sErrCode = "NOITEM"
            sErrMsg = "No Such Contact"
        ElseIf nNo = 5 Then
            sErrCode = "ACTIVEITEM"
            sErrMsg = "User is active and cannot be deleted"
        End If
    End Sub

    Public Function LoadTempPL(ByVal nFKOrderDetail As Integer) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsPLDetail As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "Op_Inv_PL_Label"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADTEMPPL"
        sCmd.Parameters.Add(New SqlParameter("@mFKOrderDetail", SqlDbType.Int)).Value() = nFKOrderDetail

        'daPLDetail = New SqlDataAdapter(sCmd)
        'daPLDetail.Fill(dsPLDetail, "PLDetail")
        Return dsPLDetail.Tables(0)

        dsPLDetail = Nothing
        sCnn.Close()

    End Function

#End Region

End Class

