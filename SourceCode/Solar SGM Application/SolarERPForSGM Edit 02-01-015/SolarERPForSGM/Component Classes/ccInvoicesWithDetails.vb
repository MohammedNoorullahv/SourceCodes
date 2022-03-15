Option Explicit On
Imports System.Data.SqlClient
#Region "Object Structures"

Public Structure strSolarInvoice4SGM4Print

    Dim PKID As Integer
    Dim BuyerGroup As String
    Dim BuyerCode As String
    Dim BuyerName As String
    Dim BuyerAddress As String
    Dim ConsigneeName As String
    Dim ConsigneeAdress As String
    Dim City As String
    Dim Pincode As String
    Dim InvoiceNo As String
    Dim InvDate As Date
    Dim InvType As String
    Dim CT3 As String
    Dim Accounted As String
    Dim Code As String
    Dim ArticleName As String
    Dim Colour As String
    Dim OldCodification As String
    Dim CodificationNew As String
    Dim Quantity As Decimal
    Dim Rate As Decimal
    Dim Value As Decimal
    Dim ExcisePercentage As Decimal
    Dim DWExciseDuty As Decimal
    Dim CessPercentage As Decimal
    Dim DWCessAmount As Decimal
    Dim EduCessPercentage As Decimal
    Dim DWEduCessAmount As Decimal
    Dim DutyPayable As Decimal
    Dim SubTotal As Decimal
    Dim CSTorVat As Decimal
    Dim CSTorVATAmount As Decimal
    Dim InvAmount As Decimal

    Dim HdrFromDate As Date
    Dim HdrToDate As Date
    Dim HdrSalesorPurchase As String
    Dim HdrTypeofDocument As String
    Dim HdrTypeofOrder As String
    Dim HdrCustomerSupplier As String
    Dim HdrBrand As String
    Dim HdrOrderStatus As String
    Dim HdrArticleName As String
    Dim HdrArticleCode As String
    Dim HdrArtilceDescription As String
    Dim HdrOrderType As String
    Dim HdrSummaryorDetailed As String
    Dim HdrDisplayInvoiceDetails As String

End Structure

Public Structure strSolarPurchaseInvoice4SGM4Print

    Dim PKID As Integer
    Dim ArrivalDate As Date
    Dim partyname As String
    Dim VoucherNo As String
    Dim PurchaseOrderNo As String
    Dim PurchaseOrderDate As Date
    Dim MaterialCode As String
    Dim Material As String
    Dim POSize As String
    Dim MaterialColorDescription As String
    Dim IssueUnits As String
    Dim IssueQuantity As Decimal
    Dim IssuePrice As Decimal
    Dim IssueValue As Decimal
    Dim MaterialTypeDescription As String
    Dim MaterialSubTypeDescription As String
    Dim TransactionType As String
    Dim SupplierBillNo As String
    Dim SupplierRefNo As String
    Dim SupplierBillDate As Date

    Dim HdrFromDate As Date
    Dim HdrToDate As Date
    Dim HdrSalesorPurchase As String
    Dim HdrTypeofDocument As String
    Dim HdrTypeofOrder As String
    Dim HdrCustomerSupplier As String
    Dim HdrBrand As String
    Dim HdrOrderStatus As String
    Dim HdrArticleName As String
    Dim HdrArticleCode As String
    Dim HdrArtilceDescription As String
    Dim HdrOrderType As String
    Dim HdrSummaryorDetailed As String
    Dim HdrDisplayInvoiceDetails As String

End Structure

#End Region

Public Class ccInvoicesWithDetails

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


    Public Function LoadAllInvoices() As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadArticles As New SqlDataAdapter
        Dim dsLoadArticles As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_InvoiceDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-A"


        If mdlSGM.sSelectOption = "A-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-A" : MsgBox("A-A")
        ElseIf mdlSGM.sSelectOption = "A-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-H" : MsgBox("A-H")
        ElseIf mdlSGM.sSelectOption = "A-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-E" : MsgBox("A-E")

        ElseIf mdlSGM.sSelectOption = "A-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-EH" : MsgBox("A-EH")
        ElseIf mdlSGM.sSelectOption = "A-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3" : MsgBox("A-3")
        ElseIf mdlSGM.sSelectOption = "A-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3H" : MsgBox("A-3H")
        ElseIf mdlSGM.sSelectOption = "A-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3E" : MsgBox("A-3E")
        ElseIf mdlSGM.sSelectOption = "A-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3EH" : MsgBox("A-3EH")
        ElseIf mdlSGM.sSelectOption = "A-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C" : MsgBox("A-C")
        ElseIf mdlSGM.sSelectOption = "A-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-CH" : MsgBox("A-CH")
        ElseIf mdlSGM.sSelectOption = "A-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-CE" : MsgBox("A-CE")
        ElseIf mdlSGM.sSelectOption = "A-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-CEH" : MsgBox("A-CEH")
        ElseIf mdlSGM.sSelectOption = "A-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C3" : MsgBox("A-C3")
        ElseIf mdlSGM.sSelectOption = "A-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C3H" : MsgBox("A-C3H")
        ElseIf mdlSGM.sSelectOption = "A-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C3E" : MsgBox("A-C3E")

        ElseIf mdlSGM.sSelectOption = "G-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-A" : MsgBox("G-A")
        ElseIf mdlSGM.sSelectOption = "G-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-H" : MsgBox("G-H")
        ElseIf mdlSGM.sSelectOption = "G-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-E" : MsgBox("G-E")
        ElseIf mdlSGM.sSelectOption = "G-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-EH" : MsgBox("G-EH")
        ElseIf mdlSGM.sSelectOption = "G-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3" : MsgBox("G-3")
        ElseIf mdlSGM.sSelectOption = "G-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3H" : MsgBox("G-3H")
        ElseIf mdlSGM.sSelectOption = "G-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3E" : MsgBox("G-3E")
        ElseIf mdlSGM.sSelectOption = "G-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3EH" : MsgBox("G-3EH")
        ElseIf mdlSGM.sSelectOption = "G-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C" : MsgBox("G-C")
        ElseIf mdlSGM.sSelectOption = "G-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-CH" : MsgBox("G-CH")
        ElseIf mdlSGM.sSelectOption = "G-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-CE" : MsgBox("G-CE")
        ElseIf mdlSGM.sSelectOption = "G-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-CEH" : MsgBox("G-CEH")
        ElseIf mdlSGM.sSelectOption = "G-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C3" : MsgBox("G-C3")
        ElseIf mdlSGM.sSelectOption = "G-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C3H" : MsgBox("G-C3H")
        ElseIf mdlSGM.sSelectOption = "G-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C3E" : MsgBox("G-C3E")

        ElseIf mdlSGM.sSelectOption = "J-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-A" : MsgBox("J-A")
        ElseIf mdlSGM.sSelectOption = "J-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-H" : MsgBox("J-H")
        ElseIf mdlSGM.sSelectOption = "J-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-E" : MsgBox("J-E")
        ElseIf mdlSGM.sSelectOption = "J-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-EH" : MsgBox("J-EH")
        ElseIf mdlSGM.sSelectOption = "J-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3" : MsgBox("J-3")
        ElseIf mdlSGM.sSelectOption = "J-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3H" : MsgBox("J-3H")
        ElseIf mdlSGM.sSelectOption = "J-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3E" : MsgBox("J-3E")
        ElseIf mdlSGM.sSelectOption = "J-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3EH" : MsgBox("J-3EH")
        ElseIf mdlSGM.sSelectOption = "J-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C" : MsgBox("J-C")
        ElseIf mdlSGM.sSelectOption = "J-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-CH" : MsgBox("J-CH")
        ElseIf mdlSGM.sSelectOption = "J-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-CE" : MsgBox("J-CE")
        ElseIf mdlSGM.sSelectOption = "J-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-CEH" : MsgBox("J-CEH")
        ElseIf mdlSGM.sSelectOption = "J-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C3" : MsgBox("J-C3")
        ElseIf mdlSGM.sSelectOption = "J-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C3H" : MsgBox("J-C3H")
        ElseIf mdlSGM.sSelectOption = "J-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C3E" : MsgBox("J-C3E")

        ElseIf mdlSGM.sSelectOption = "JG-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-A" : MsgBox("JG-A")
        ElseIf mdlSGM.sSelectOption = "JG-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-H" : MsgBox("JG-H")
        ElseIf mdlSGM.sSelectOption = "JG-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-E" : MsgBox("JG-E")
        ElseIf mdlSGM.sSelectOption = "JG-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-EH" : MsgBox("JG-EH")
        ElseIf mdlSGM.sSelectOption = "JG-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3" : MsgBox("JG-3")
        ElseIf mdlSGM.sSelectOption = "JG-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3H" : MsgBox("JG-3H")
        ElseIf mdlSGM.sSelectOption = "JG-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3E" : MsgBox("JG-3E")
        ElseIf mdlSGM.sSelectOption = "JG-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3EH" : MsgBox("JG-3EH")
        ElseIf mdlSGM.sSelectOption = "JG-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C" : MsgBox("JG-C")
        ElseIf mdlSGM.sSelectOption = "JG-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-CH" : MsgBox("JG-CH")
        ElseIf mdlSGM.sSelectOption = "JG-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-CE" : MsgBox("JG-CE")
        ElseIf mdlSGM.sSelectOption = "JG-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-CEH" : MsgBox("JG-CEH")
        ElseIf mdlSGM.sSelectOption = "JG-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C3" : MsgBox("JG-C3")
        ElseIf mdlSGM.sSelectOption = "JG-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C3H" : MsgBox("JG-C3H")
        ElseIf mdlSGM.sSelectOption = "JG-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C3E" : MsgBox("JG-C3E")

        ElseIf mdlSGM.sSelectOption = "S-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-A" : MsgBox("S-A")
        ElseIf mdlSGM.sSelectOption = "S-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-H" : MsgBox("S-H")
        ElseIf mdlSGM.sSelectOption = "S-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-E" : MsgBox("S-E")
        ElseIf mdlSGM.sSelectOption = "S-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-EH" : MsgBox("S-EH")
        ElseIf mdlSGM.sSelectOption = "S-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3" : MsgBox("S-3")
        ElseIf mdlSGM.sSelectOption = "S-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3H" : MsgBox("S-3H")
        ElseIf mdlSGM.sSelectOption = "S-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3E" : MsgBox("S-3E")
        ElseIf mdlSGM.sSelectOption = "S-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3EH" : MsgBox("S-3EH")
        ElseIf mdlSGM.sSelectOption = "S-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C" : MsgBox("S-C")
        ElseIf mdlSGM.sSelectOption = "S-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CH" : MsgBox("S-CH")
        ElseIf mdlSGM.sSelectOption = "S-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CE" : MsgBox("S-CE")
        ElseIf mdlSGM.sSelectOption = "S-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CEH" : MsgBox("S-CEH")
        ElseIf mdlSGM.sSelectOption = "S-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C3" : MsgBox("S-C3")
        ElseIf mdlSGM.sSelectOption = "S-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C3H" : MsgBox("S-C3H")
        ElseIf mdlSGM.sSelectOption = "S-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C3E" : MsgBox("S-C3E")

        ElseIf mdlSGM.sSelectOption = "SG-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-A" : MsgBox("SG-A")
        ElseIf mdlSGM.sSelectOption = "SG-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-H" : MsgBox("SG-H")
        ElseIf mdlSGM.sSelectOption = "SG-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-E" : MsgBox("SG-E")
        ElseIf mdlSGM.sSelectOption = "SG-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-EH" : MsgBox("SG-EH")
        ElseIf mdlSGM.sSelectOption = "SG-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3" : MsgBox("SG-3")
        ElseIf mdlSGM.sSelectOption = "SG-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3H" : MsgBox("SG-3H")
        ElseIf mdlSGM.sSelectOption = "SG-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3E" : MsgBox("SG-3E")
        ElseIf mdlSGM.sSelectOption = "SG-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3EH" : MsgBox("SG-3EH")
        ElseIf mdlSGM.sSelectOption = "SG-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C" : MsgBox("SG-C")
        ElseIf mdlSGM.sSelectOption = "SG-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-CH" : MsgBox("SG-CH")
        ElseIf mdlSGM.sSelectOption = "SG-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-CE" : MsgBox("SG-CE")
        ElseIf mdlSGM.sSelectOption = "SG-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-CEH" : MsgBox("SG-CEH")
        ElseIf mdlSGM.sSelectOption = "SG-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C3" : MsgBox("SG-C3")
        ElseIf mdlSGM.sSelectOption = "SG-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C3H" : MsgBox("SG-C3H")
        ElseIf mdlSGM.sSelectOption = "SG-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C3E" : MsgBox("SG-C3E")

        ElseIf mdlSGM.sSelectOption = "SJ-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-A" : MsgBox("SJ-A")
        ElseIf mdlSGM.sSelectOption = "SJ-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-H" : MsgBox("SJ-H")
        ElseIf mdlSGM.sSelectOption = "SJ-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-E" : MsgBox("SJ-E")
        ElseIf mdlSGM.sSelectOption = "SJ-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-EH" : MsgBox("SJ-EH")
        ElseIf mdlSGM.sSelectOption = "SJ-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3" : MsgBox("SJ-3")
        ElseIf mdlSGM.sSelectOption = "SJ-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3H" : MsgBox("SJ-3H")
        ElseIf mdlSGM.sSelectOption = "SJ-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3E" : MsgBox("SJ-3E")
        ElseIf mdlSGM.sSelectOption = "SJ-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3EH" : MsgBox("SJ-3EH")
        ElseIf mdlSGM.sSelectOption = "SJ-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C" : MsgBox("SJ-C")
        ElseIf mdlSGM.sSelectOption = "SJ-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-CH" : MsgBox("SJ-CH")
        ElseIf mdlSGM.sSelectOption = "SJ-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-CE" : MsgBox("SJ-CE")
        ElseIf mdlSGM.sSelectOption = "SJ-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-CEH" : MsgBox("SJ-CEH")
        ElseIf mdlSGM.sSelectOption = "SJ-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C3" : MsgBox("SJ-C3")
        ElseIf mdlSGM.sSelectOption = "SJ-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C3H" : MsgBox("SJ-C3H")
        ElseIf mdlSGM.sSelectOption = "SJ-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C3E" : MsgBox("SJ-C3E")
        End If
        Exit Function
        If mdlSGM.sSelectOption = "S-A" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-A"
        ElseIf mdlSGM.sSelectOption = "S-E" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-E"
        ElseIf mdlSGM.sSelectOption = "S-H" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-H"
        ElseIf mdlSGM.sSelectOption = "S-HE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-HE"
        ElseIf mdlSGM.sSelectOption = "S-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C"
        ElseIf mdlSGM.sSelectOption = "S-CE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CE"
        ElseIf mdlSGM.sSelectOption = "S-CH" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CH"
        ElseIf mdlSGM.sSelectOption = "S-CHE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CHE"
        ElseIf mdlSGM.sSelectOption = "S-3" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3"
        ElseIf mdlSGM.sSelectOption = "S-3E" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3E"
        ElseIf mdlSGM.sSelectOption = "S-3H" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3H"
        ElseIf mdlSGM.sSelectOption = "S-3HE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3HE"
        ElseIf mdlSGM.sSelectOption = "S-3C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3C"
        ElseIf mdlSGM.sSelectOption = "S-3CE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3CE"
        ElseIf mdlSGM.sSelectOption = "S-3CH" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3CH"
        ElseIf mdlSGM.sSelectOption = "JS-A" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-A"
        ElseIf mdlSGM.sSelectOption = "J" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J"
        ElseIf mdlSGM.sSelectOption = "JS-E" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-E"
        ElseIf mdlSGM.sSelectOption = "JS-H" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-H"
        ElseIf mdlSGM.sSelectOption = "JS-HE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-HE"
        ElseIf mdlSGM.sSelectOption = "JS-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-C"
        ElseIf mdlSGM.sSelectOption = "JS-CE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-CE"
        ElseIf mdlSGM.sSelectOption = "JS-CH" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-CH"
        ElseIf mdlSGM.sSelectOption = "JS-CHE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-CHE"
        ElseIf mdlSGM.sSelectOption = "JS-3" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3"
        ElseIf mdlSGM.sSelectOption = "JS-3E" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3E"
        ElseIf mdlSGM.sSelectOption = "JS-3H" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3H"
        ElseIf mdlSGM.sSelectOption = "JS-3HE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3HE"
        ElseIf mdlSGM.sSelectOption = "JS-3C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3C"
        ElseIf mdlSGM.sSelectOption = "JS-3CE" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3CE"
        ElseIf mdlSGM.sSelectOption = "JS-3CH" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3CH"
        ElseIf mdlSGM.sSelectOption = "J-G" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-G"
        ElseIf mdlSGM.sSelectOption = "G" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        daLoadArticles = New SqlDataAdapter(sCmd)
        daLoadArticles.Fill(dsLoadArticles, "Art")
        Return dsLoadArticles.Tables(0)

        dsLoadArticles = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_InvoiceDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCust"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadArticleofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_InvoiceDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectedCustomer = " ALL CUSTOMERS" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadALLArt"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadArt"
        End If

        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCodificationofCustomer() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_InvoiceDetails"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCode"
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function

    Public Function DelInvoice4Print() As Boolean
        ''aasdfas()
        Dim sCmd1 As New SqlCommand

        Dim daDelOutstanding As New SqlDataAdapter
        Dim dsDelOutstanding As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "sgm_InvoiceDetails"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "DELINV"

        daDelOutstanding = New SqlDataAdapter(sCmd1)
        daDelOutstanding.Fill(dsDelOutstanding, "Date")
        dsDelOutstanding.AcceptChanges()

    End Function

    Public Function DelPurchaseInvoice4Print() As Boolean
        ''aasdfas()
        Dim sCmd1 As New SqlCommand

        Dim daDelOutstanding As New SqlDataAdapter
        Dim dsDelOutstanding As New DataSet

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "sgm_PurchaseInvoiceDetails"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "DELINV"

        daDelOutstanding = New SqlDataAdapter(sCmd1)
        daDelOutstanding.Fill(dsDelOutstanding, "Date")
        dsDelOutstanding.AcceptChanges()

    End Function

    Public Function InsInvoice4Print(ByVal oNV As strSolarInvoice4SGM4Print) As DataTable
        ''aasdfas()
        Dim sCmd2 As New SqlCommand

        Dim daInsOutstanding As New SqlDataAdapter
        Dim dsInsOutstanding As New DataSet

        sCmd2.Connection = sCnn
        sCmd2.CommandText = "sgm_InvoiceDetails"
        sCmd2.CommandType = CommandType.StoredProcedure

        sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSINV"

        sCmd2.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = oNV.PKID
        sCmd2.Parameters.Add(New SqlParameter("@mBuyerGroup", SqlDbType.VarChar)).Value() = oNV.BuyerGroup
        sCmd2.Parameters.Add(New SqlParameter("@mBuyerCode", SqlDbType.VarChar)).Value() = oNV.BuyerCode
        sCmd2.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = oNV.BuyerName
        sCmd2.Parameters.Add(New SqlParameter("@mBuyerAddress", SqlDbType.VarChar)).Value() = oNV.BuyerAddress
        sCmd2.Parameters.Add(New SqlParameter("@mConsigneeName", SqlDbType.VarChar)).Value() = oNV.ConsigneeName
        sCmd2.Parameters.Add(New SqlParameter("@mConsigneeAdress", SqlDbType.VarChar)).Value() = oNV.ConsigneeAdress
        sCmd2.Parameters.Add(New SqlParameter("@mCity", SqlDbType.VarChar)).Value() = oNV.City
        sCmd2.Parameters.Add(New SqlParameter("@mPincode", SqlDbType.VarChar)).Value() = oNV.Pincode
        sCmd2.Parameters.Add(New SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value() = oNV.InvoiceNo
        sCmd2.Parameters.Add(New SqlParameter("@mInvDate", SqlDbType.Date)).Value() = oNV.InvDate
        sCmd2.Parameters.Add(New SqlParameter("@mInvType", SqlDbType.VarChar)).Value() = oNV.InvType
        sCmd2.Parameters.Add(New SqlParameter("@mCT3", SqlDbType.VarChar)).Value() = oNV.CT3
        sCmd2.Parameters.Add(New SqlParameter("@mAccounted", SqlDbType.VarChar)).Value() = oNV.Accounted
        sCmd2.Parameters.Add(New SqlParameter("@mCode", SqlDbType.VarChar)).Value() = oNV.Code
        sCmd2.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = oNV.ArticleName
        sCmd2.Parameters.Add(New SqlParameter("@mColour", SqlDbType.VarChar)).Value() = oNV.Colour
        sCmd2.Parameters.Add(New SqlParameter("@mOldCodification", SqlDbType.VarChar)).Value() = oNV.OldCodification
        sCmd2.Parameters.Add(New SqlParameter("@mCodificationNew", SqlDbType.VarChar)).Value() = oNV.CodificationNew
        sCmd2.Parameters.Add(New SqlParameter("@mQuantity", SqlDbType.Decimal)).Value() = oNV.Quantity
        sCmd2.Parameters.Add(New SqlParameter("@mRate", SqlDbType.Decimal)).Value() = oNV.Rate
        sCmd2.Parameters.Add(New SqlParameter("@mValue", SqlDbType.Decimal)).Value() = oNV.Value
        sCmd2.Parameters.Add(New SqlParameter("@mExcisePercentage", SqlDbType.Decimal)).Value() = oNV.ExcisePercentage
        sCmd2.Parameters.Add(New SqlParameter("@mDWExciseDuty", SqlDbType.Decimal)).Value() = oNV.DWExciseDuty
        sCmd2.Parameters.Add(New SqlParameter("@mCessPercentage", SqlDbType.Decimal)).Value() = oNV.CessPercentage
        sCmd2.Parameters.Add(New SqlParameter("@mDWCessAmount", SqlDbType.Decimal)).Value() = oNV.DWCessAmount
        sCmd2.Parameters.Add(New SqlParameter("@mEduCessPercentage", SqlDbType.Decimal)).Value() = oNV.EduCessPercentage
        sCmd2.Parameters.Add(New SqlParameter("@mDWEduCessAmount", SqlDbType.Decimal)).Value() = oNV.DWEduCessAmount
        sCmd2.Parameters.Add(New SqlParameter("@mDutyPayable", SqlDbType.Decimal)).Value() = oNV.DutyPayable
        sCmd2.Parameters.Add(New SqlParameter("@mSubTotal", SqlDbType.Decimal)).Value() = oNV.SubTotal
        sCmd2.Parameters.Add(New SqlParameter("@mCSTorVat", SqlDbType.Decimal)).Value() = oNV.CSTorVat
        sCmd2.Parameters.Add(New SqlParameter("@mCSTorVATAmount", SqlDbType.Decimal)).Value() = oNV.CSTorVATAmount
        sCmd2.Parameters.Add(New SqlParameter("@mInvAmount", SqlDbType.Decimal)).Value() = oNV.InvAmount


        sCmd2.Parameters.Add(New SqlParameter("@mHdrFromDate", SqlDbType.DateTime)).Value() = oNV.HdrFromDate
        sCmd2.Parameters.Add(New SqlParameter("@mHdrToDate", SqlDbType.DateTime)).Value() = oNV.HdrToDate
        sCmd2.Parameters.Add(New SqlParameter("@mHdrSalesorPurchase", SqlDbType.VarChar)).Value() = oNV.HdrSalesorPurchase
        sCmd2.Parameters.Add(New SqlParameter("@mHdrTypeofDocument", SqlDbType.VarChar)).Value() = oNV.HdrTypeofDocument
        sCmd2.Parameters.Add(New SqlParameter("@mHdrTypeofOrder", SqlDbType.VarChar)).Value() = oNV.HdrTypeofOrder
        sCmd2.Parameters.Add(New SqlParameter("@mHdrCustomerSupplier", SqlDbType.VarChar)).Value() = oNV.HdrCustomerSupplier
        sCmd2.Parameters.Add(New SqlParameter("@mHdrBrand", SqlDbType.VarChar)).Value() = oNV.HdrBrand
        sCmd2.Parameters.Add(New SqlParameter("@mHdrOrderStatus", SqlDbType.VarChar)).Value() = oNV.HdrOrderStatus
        sCmd2.Parameters.Add(New SqlParameter("@mHdrArticleName", SqlDbType.VarChar)).Value() = oNV.HdrArticleName
        sCmd2.Parameters.Add(New SqlParameter("@mHdrArticleCode", SqlDbType.VarChar)).Value() = oNV.HdrArticleCode
        sCmd2.Parameters.Add(New SqlParameter("@mHdrArtilceDescription", SqlDbType.VarChar)).Value() = oNV.HdrArtilceDescription
        sCmd2.Parameters.Add(New SqlParameter("@mHdrOrderType", SqlDbType.VarChar)).Value() = oNV.HdrOrderType
        sCmd2.Parameters.Add(New SqlParameter("@mHdrSummaryorDetailed", SqlDbType.VarChar)).Value() = oNV.HdrSummaryorDetailed
        sCmd2.Parameters.Add(New SqlParameter("@mHdrDisplayInvoiceDetails", SqlDbType.VarChar)).Value() = oNV.HdrDisplayInvoiceDetails

        sCnn.Open()

        Dim sRes As String = sCmd2.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            setError(Val(sRes))
        End If
        sCnn.Close()

    End Function

    Public Function InsPurchaseInvoice4Print(ByVal oNV As strSolarPurchaseInvoice4SGM4Print) As DataTable

        Dim sCmd2 As New SqlCommand

        Dim daInsOutstanding As New SqlDataAdapter
        Dim dsInsOutstanding As New DataSet

        sCmd2.Connection = sCnn
        sCmd2.CommandText = "sgm_PurchaseInvoiceDetails"
        sCmd2.CommandType = CommandType.StoredProcedure

        sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSINV"

        sCmd2.Parameters.Add(New SqlParameter("@mPKID", SqlDbType.Int)).Value() = oNV.PKID
        sCmd2.Parameters.Add(New SqlParameter("@mArrivalDate", SqlDbType.DateTime)).Value() = oNV.ArrivalDate
        sCmd2.Parameters.Add(New SqlParameter("@mpartyname", SqlDbType.VarChar)).Value() = oNV.partyname
        sCmd2.Parameters.Add(New SqlParameter("@mVoucherNo", SqlDbType.VarChar)).Value() = oNV.VoucherNo
        sCmd2.Parameters.Add(New SqlParameter("@mPurchaseOrderNo", SqlDbType.VarChar)).Value() = oNV.PurchaseOrderNo
        sCmd2.Parameters.Add(New SqlParameter("@mPurchaseOrderDate", SqlDbType.DateTime)).Value() = oNV.PurchaseOrderDate
        sCmd2.Parameters.Add(New SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value() = oNV.MaterialCode
        sCmd2.Parameters.Add(New SqlParameter("@mMaterial", SqlDbType.VarChar)).Value() = oNV.Material
        sCmd2.Parameters.Add(New SqlParameter("@mPOSize", SqlDbType.VarChar)).Value() = oNV.POSize
        sCmd2.Parameters.Add(New SqlParameter("@mMaterialColorDescription", SqlDbType.VarChar)).Value() = oNV.MaterialColorDescription
        sCmd2.Parameters.Add(New SqlParameter("@mIssueUnits", SqlDbType.VarChar)).Value() = oNV.IssueUnits
        sCmd2.Parameters.Add(New SqlParameter("@mIssueQuantity", SqlDbType.Decimal)).Value() = oNV.IssueQuantity
        sCmd2.Parameters.Add(New SqlParameter("@mIssuePrice", SqlDbType.Decimal)).Value() = oNV.IssuePrice
        sCmd2.Parameters.Add(New SqlParameter("@mIssueValue", SqlDbType.Decimal)).Value() = oNV.IssueValue
        sCmd2.Parameters.Add(New SqlParameter("@mMaterialTypeDescription", SqlDbType.VarChar)).Value() = oNV.MaterialTypeDescription
        sCmd2.Parameters.Add(New SqlParameter("@mMaterialSubTypeDescription", SqlDbType.VarChar)).Value() = oNV.MaterialSubTypeDescription
        sCmd2.Parameters.Add(New SqlParameter("@mTransactionType", SqlDbType.VarChar)).Value() = oNV.TransactionType
        sCmd2.Parameters.Add(New SqlParameter("@mSupplierBillNo", SqlDbType.VarChar)).Value() = oNV.SupplierBillNo
        sCmd2.Parameters.Add(New SqlParameter("@mSupplierRefNo", SqlDbType.VarChar)).Value() = oNV.SupplierRefNo
        sCmd2.Parameters.Add(New SqlParameter("@mSupplierBillDate", SqlDbType.DateTime)).Value() = oNV.SupplierBillDate


        sCmd2.Parameters.Add(New SqlParameter("@mHdrFromDate", SqlDbType.DateTime)).Value() = oNV.HdrFromDate
        sCmd2.Parameters.Add(New SqlParameter("@mHdrToDate", SqlDbType.DateTime)).Value() = oNV.HdrToDate
        sCmd2.Parameters.Add(New SqlParameter("@mHdrSalesorPurchase", SqlDbType.VarChar)).Value() = oNV.HdrSalesorPurchase
        sCmd2.Parameters.Add(New SqlParameter("@mHdrTypeofDocument", SqlDbType.VarChar)).Value() = oNV.HdrTypeofDocument
        sCmd2.Parameters.Add(New SqlParameter("@mHdrTypeofOrder", SqlDbType.VarChar)).Value() = oNV.HdrTypeofOrder
        sCmd2.Parameters.Add(New SqlParameter("@mHdrCustomerSupplier", SqlDbType.VarChar)).Value() = oNV.HdrCustomerSupplier
        sCmd2.Parameters.Add(New SqlParameter("@mHdrBrand", SqlDbType.VarChar)).Value() = oNV.HdrBrand
        sCmd2.Parameters.Add(New SqlParameter("@mHdrOrderStatus", SqlDbType.VarChar)).Value() = oNV.HdrOrderStatus
        sCmd2.Parameters.Add(New SqlParameter("@mHdrArticleName", SqlDbType.VarChar)).Value() = oNV.HdrArticleName
        sCmd2.Parameters.Add(New SqlParameter("@mHdrArticleCode", SqlDbType.VarChar)).Value() = oNV.HdrArticleCode
        sCmd2.Parameters.Add(New SqlParameter("@mHdrArtilceDescription", SqlDbType.VarChar)).Value() = oNV.HdrArtilceDescription
        sCmd2.Parameters.Add(New SqlParameter("@mHdrOrderType", SqlDbType.VarChar)).Value() = oNV.HdrOrderType
        sCmd2.Parameters.Add(New SqlParameter("@mHdrSummaryorDetailed", SqlDbType.VarChar)).Value() = oNV.HdrSummaryorDetailed
        sCmd2.Parameters.Add(New SqlParameter("@mHdrDisplayInvoiceDetails", SqlDbType.VarChar)).Value() = oNV.HdrDisplayInvoiceDetails

        sCnn.Open()

        Dim sRes As String = sCmd2.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            setError(Val(sRes))
        End If
        sCnn.Close()

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

#End Region

End Class
