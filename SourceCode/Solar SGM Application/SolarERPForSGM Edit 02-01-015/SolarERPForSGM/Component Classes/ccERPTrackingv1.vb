Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strERPTrackingV1

    Dim FromDate As Date
    Dim ToDate As Date
    Dim Customer As String
    Dim ArticleMould As String
    Dim ArticleDescription As String
    Dim ProductionStatus As String
    Dim ShipmentStatus As String
    Dim ProductType As String
    Dim ProductTypeMain As String

End Structure

Public Structure strERPTrackingV2

    Dim nPKID As Integer
    Dim nSlNo As Integer
    Dim sID As String
    Dim sSalesOrderNo As String
    Dim sCustomerOrderNo As String
    Dim dOrderReceivedDate As Date
    Dim sBuyerName As String
    Dim sArticle As String
    Dim sArticleName As String
    Dim nOrderQuantity As Integer
    Dim nPrice As Decimal
    Dim nOrderValue As Decimal
    Dim dExpectedDeliveryDate As Date
    Dim sShipmentStatus As String
    Dim sCodificationNew As String
    Dim nDispatch As Integer
    Dim nBalance As Integer
    Dim sOrderStatus As String
    Dim sArticleMould As String
    Dim sAssortmentName As String
    Dim sRowInfo As String
    Dim sSize01 As String
    Dim sSize02 As String
    Dim sSize03 As String
    Dim sSize04 As String
    Dim sSize05 As String
    Dim sSize06 As String
    Dim sSize07 As String
    Dim sSize08 As String
    Dim sSize09 As String
    Dim sSize10 As String
    Dim sSize11 As String
    Dim sSize12 As String
    Dim sSize13 As String
    Dim sSize14 As String
    Dim sSize15 As String
    Dim sSize16 As String
    Dim sSize17 As String
    Dim sSize18 As String
    Dim sIPAddress As String
    Dim sInvoiceNo As String
    Dim dInvoiceDt As Date
End Structure

#End Region

Public Class ccERPTrackingv1

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

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

#Region "Functions"

    

    Public Function LoadSalesOrderDetails(ByVal oNV As strERPTrackingV1) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn

        If oNV.ProductTypeMain = "ALL PRODUCT TYPE" Then
            sCmd.CommandText = "proc_ERPTrackingF1"
        Else
            If oNV.ProductTypeMain <> "ALL PRODUCT TYPE" And oNV.ProductType <> "ALL PRODUCT TYPE" Then
                sCmd.CommandText = "proc_ERPTrackingF1"
            Else
                sCmd.CommandText = "proc_ERPTrackingF1WPTYPE"
            End If

        End If

        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedOption '"A-A"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mCodificationNew", SqlDbType.VarChar)).Value() = oNV.ProductType
        sCmd.Parameters.Add(New SqlParameter("@mShipmentStatus", SqlDbType.VarChar)).Value() = oNV.ShipmentStatus
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = oNV.ProductionStatus
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = oNV.ArticleDescription
        sCmd.Parameters.Add(New SqlParameter("@mArticleMould", SqlDbType.VarChar)).Value() = oNV.ArticleMould
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = oNV.Customer
        sCmd.Parameters.Add(New SqlParameter("@mProductTypeMain", SqlDbType.VarChar)).Value() = oNV.ProductTypeMain

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function LoadData(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_ERPTrackingF2"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADDATA"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function InsertData(ByVal oNV As strERPTrackingV2) As Boolean

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_ERPTrackingF2"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSERT"
        sCmd.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = oNV.nPKID
        sCmd.Parameters.Add(New SqlParameter("@mSlNo", SqlDbType.Int)).Value() = oNV.nSlNo
        sCmd.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = oNV.sID
        sCmd.Parameters.Add(New SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value() = oNV.sSalesOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value() = oNV.sCustomerOrderNo
        sCmd.Parameters.Add(New SqlParameter("@mOrderReceivedDate", SqlDbType.DateTime)).Value() = oNV.dOrderReceivedDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = oNV.sBuyerName
        sCmd.Parameters.Add(New SqlParameter("@mArticle", SqlDbType.VarChar)).Value() = oNV.sArticle
        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = oNV.sArticleName
        sCmd.Parameters.Add(New SqlParameter("@mOrderQuantity", SqlDbType.Int)).Value() = oNV.nOrderQuantity
        sCmd.Parameters.Add(New SqlParameter("@mPrice", SqlDbType.Decimal)).Value() = oNV.nPrice
        sCmd.Parameters.Add(New SqlParameter("@mOrderValue", SqlDbType.Decimal)).Value() = oNV.nOrderValue
        sCmd.Parameters.Add(New SqlParameter("@mExpectedDeliveryDate", SqlDbType.DateTime)).Value() = oNV.dExpectedDeliveryDate
        sCmd.Parameters.Add(New SqlParameter("@mShipmentStatus", SqlDbType.VarChar)).Value() = oNV.sShipmentStatus
        sCmd.Parameters.Add(New SqlParameter("@mCodificationNew", SqlDbType.VarChar)).Value() = oNV.sCodificationNew
        sCmd.Parameters.Add(New SqlParameter("@mDispatch", SqlDbType.Int)).Value() = oNV.nDispatch
        sCmd.Parameters.Add(New SqlParameter("@mBalance", SqlDbType.Int)).Value() = oNV.nBalance
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = oNV.sOrderStatus
        sCmd.Parameters.Add(New SqlParameter("@mArticleMould", SqlDbType.VarChar)).Value() = oNV.sArticleMould
        sCmd.Parameters.Add(New SqlParameter("@mAssortmentName", SqlDbType.VarChar)).Value() = oNV.sAssortmentName
        sCmd.Parameters.Add(New SqlParameter("@mRowInfo", SqlDbType.VarChar)).Value() = oNV.sRowInfo
        sCmd.Parameters.Add(New SqlParameter("@mSize01", SqlDbType.VarChar)).Value() = oNV.sSize01
        sCmd.Parameters.Add(New SqlParameter("@mSize02", SqlDbType.VarChar)).Value() = oNV.sSize02
        sCmd.Parameters.Add(New SqlParameter("@mSize03", SqlDbType.VarChar)).Value() = oNV.sSize03
        sCmd.Parameters.Add(New SqlParameter("@mSize04", SqlDbType.VarChar)).Value() = oNV.sSize04
        sCmd.Parameters.Add(New SqlParameter("@mSize05", SqlDbType.VarChar)).Value() = oNV.sSize05
        sCmd.Parameters.Add(New SqlParameter("@mSize06", SqlDbType.VarChar)).Value() = oNV.sSize06
        sCmd.Parameters.Add(New SqlParameter("@mSize07", SqlDbType.VarChar)).Value() = oNV.sSize07
        sCmd.Parameters.Add(New SqlParameter("@mSize08", SqlDbType.VarChar)).Value() = oNV.sSize08
        sCmd.Parameters.Add(New SqlParameter("@mSize09", SqlDbType.VarChar)).Value() = oNV.sSize09
        sCmd.Parameters.Add(New SqlParameter("@mSize10", SqlDbType.VarChar)).Value() = oNV.sSize10
        sCmd.Parameters.Add(New SqlParameter("@mSize11", SqlDbType.VarChar)).Value() = oNV.sSize11
        sCmd.Parameters.Add(New SqlParameter("@mSize12", SqlDbType.VarChar)).Value() = oNV.sSize12
        sCmd.Parameters.Add(New SqlParameter("@mSize13", SqlDbType.VarChar)).Value() = oNV.sSize13
        sCmd.Parameters.Add(New SqlParameter("@mSize14", SqlDbType.VarChar)).Value() = oNV.sSize14
        sCmd.Parameters.Add(New SqlParameter("@mSize15", SqlDbType.VarChar)).Value() = oNV.sSize15
        sCmd.Parameters.Add(New SqlParameter("@mSize16", SqlDbType.VarChar)).Value() = oNV.sSize16
        sCmd.Parameters.Add(New SqlParameter("@mSize17", SqlDbType.VarChar)).Value() = oNV.sSize17
        sCmd.Parameters.Add(New SqlParameter("@mSize18", SqlDbType.VarChar)).Value() = oNV.sSize18
        sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value() = oNV.sIPAddress
        sCmd.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.sInvoiceNo
        If oNV.sInvoiceNo = "" Then
            sCmd.Parameters.Add(New SqlParameter("@mInvoiceDt", SqlDbType.DateTime)).Value() = System.DBNull.Value
        Else
            sCmd.Parameters.Add(New SqlParameter("@mInvoiceDt", SqlDbType.DateTime)).Value() = oNV.dInvoiceDt
        End If

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

    Public Function LoadSalesOrderDetailsv2(ByVal oNV As strERPTrackingV1, ByVal sIPAddress As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        If frmERPTrackingSystemv1.rbSynthetic.Checked = True Then
            sCmd.CommandText = "proc_ERPTrackingF2"
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "OSS0AAAAAA" 'mdlSGM.sSelectedOption '"A-A"
        Else
            sCmd.CommandText = "proc_ERPTrackingF2Analytics"
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "OAS0AAAAAA" 'mdlSGM.sSelectedOption '"A-A"
        End If
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mCodificationNew", SqlDbType.VarChar)).Value() = oNV.ProductType
        sCmd.Parameters.Add(New SqlParameter("@mShipmentStatus", SqlDbType.VarChar)).Value() = oNV.ShipmentStatus
        sCmd.Parameters.Add(New SqlParameter("@mOrderStatus", SqlDbType.VarChar)).Value() = oNV.ProductionStatus
        sCmd.Parameters.Add(New SqlParameter("@mDescription", SqlDbType.VarChar)).Value() = oNV.ArticleDescription
        sCmd.Parameters.Add(New SqlParameter("@mArticleMould", SqlDbType.VarChar)).Value() = oNV.ArticleMould
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = oNV.Customer
        sCmd.Parameters.Add(New SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value() = sIPAddress

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

#End Region
End Class

