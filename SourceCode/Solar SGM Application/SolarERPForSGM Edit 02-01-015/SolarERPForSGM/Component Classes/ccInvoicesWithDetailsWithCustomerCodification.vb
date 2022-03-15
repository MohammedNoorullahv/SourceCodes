Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"


#End Region

Public Class ccInvoicesWithDetailsWithCustomerCodification

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


    Public Function LoadAllInvoices(ByVal sBuyerName As String, ByVal sCodification As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadArticles As New SqlDataAdapter
        Dim dsLoadArticles As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "sgm_InvoiceDetailsWithCustCodification"
        sCmd.CommandType = CommandType.StoredProcedure

        If mdlSGM.sSelectOption = "S-A-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-A-C"
        ElseIf mdlSGM.sSelectOption = "S-E-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-E-C"
        ElseIf mdlSGM.sSelectOption = "S-H-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-H-C"
        ElseIf mdlSGM.sSelectOption = "S-HE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-HE-C"
        ElseIf mdlSGM.sSelectOption = "S-C-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C-C"
        ElseIf mdlSGM.sSelectOption = "S-CE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CE-C"
        ElseIf mdlSGM.sSelectOption = "S-CH-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CH-C"
        ElseIf mdlSGM.sSelectOption = "S-CHE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CHE-C"
        ElseIf mdlSGM.sSelectOption = "S-3-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3-C"
        ElseIf mdlSGM.sSelectOption = "S-3E-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3E-C"
        ElseIf mdlSGM.sSelectOption = "S-3H-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3H-C"
        ElseIf mdlSGM.sSelectOption = "S-3HE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3HE-C"
        ElseIf mdlSGM.sSelectOption = "S-3C-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3C-C"
        ElseIf mdlSGM.sSelectOption = "S-3CE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3CE-C"
        ElseIf mdlSGM.sSelectOption = "S-3CH-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3CH-C"
        ElseIf mdlSGM.sSelectOption = "JS-A-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-A-C"
        ElseIf mdlSGM.sSelectOption = "J-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C"
        ElseIf mdlSGM.sSelectOption = "JS-E-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-E-C"
        ElseIf mdlSGM.sSelectOption = "JS-H-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-H-C"
        ElseIf mdlSGM.sSelectOption = "JS-HE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-HE-C"
        ElseIf mdlSGM.sSelectOption = "JS-C-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-C-C"
        ElseIf mdlSGM.sSelectOption = "JS-CE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-CE-C"
        ElseIf mdlSGM.sSelectOption = "JS-CH-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-CH-C"
        ElseIf mdlSGM.sSelectOption = "JS-CHE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-CHE-C"
        ElseIf mdlSGM.sSelectOption = "JS-3-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3-C"
        ElseIf mdlSGM.sSelectOption = "JS-3E-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3E-C"
        ElseIf mdlSGM.sSelectOption = "JS-3H-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3H-C"
        ElseIf mdlSGM.sSelectOption = "JS-3HE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3HE-C"
        ElseIf mdlSGM.sSelectOption = "JS-3C-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3C-C"
        ElseIf mdlSGM.sSelectOption = "JS-3CE-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3CE-C"
        ElseIf mdlSGM.sSelectOption = "JS-3CH-C" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JS-3CH-C"
        End If
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = sBuyerName
        sCmd.Parameters.Add(New SqlParameter("@mCodification", SqlDbType.VarChar)).Value() = sCodification

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

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadArt"
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

