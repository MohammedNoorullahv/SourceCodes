Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

#End Region

Public Class ccWIPManageTools

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

    Public Function LoadWIPSummary(ByVal dFromDate As Date, ByVal dToDate As Date, ByVal sCustomer As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_WIPMANAGETOOLS"
        sCmd.CommandType = CommandType.StoredProcedure


        If mdlSGM.sSelectedOption = "AA" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPINFO"
        ElseIf mdlSGM.sSelectedOption = "FA" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPINFOB"
        ElseIf mdlSGM.sSelectedOption = "AF" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPINFO"
        ElseIf mdlSGM.sSelectedOption = "FF" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPINFOB"
        End If

        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = sCustomer

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function LoadWIPDetails(ByVal sJobcardNo As String, ByVal sSection As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadInvoices As New SqlDataAdapter
        Dim dsLoadInvoices As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_WIPMANAGETOOLS"
        sCmd.CommandType = CommandType.StoredProcedure


        If mdlSGM.sSelectedOption = "AA" Then
            If Microsoft.VisualBasic.Len(sJobcardNo) = 13 Then
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPDTLS"
            Else
                sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPDTLSFSPL"
            End If

        ElseIf mdlSGM.sSelectedOption = "FA" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPDTLS"
        ElseIf mdlSGM.sSelectedOption = "AF" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPDTLSF"
        ElseIf mdlSGM.sSelectedOption = "FF" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWIPDTLSF"
        End If


        sCmd.Parameters.Add(New SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value() = sJobcardNo
        sCmd.Parameters.Add(New SqlParameter("@mSection", SqlDbType.VarChar)).Value() = sSection

        daLoadInvoices = New SqlDataAdapter(sCmd)
        daLoadInvoices.Fill(dsLoadInvoices, "Art")
        Return dsLoadInvoices.Tables(0)

        dsLoadInvoices = Nothing
        sCnn.Close()

    End Function

    Public Function LoadCustomer(ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelCustomer As New SqlDataAdapter
        Dim dsSelCustomer As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_WIPMANAGETOOLS"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADCUSTOMER"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate

        dsSelCustomer.Clear()
        daSelCustomer = New SqlDataAdapter(sCmd)
        daSelCustomer.Fill(dsSelCustomer, "Customer")
        Return dsSelCustomer.Tables(0)

        dsSelCustomer = Nothing
        sCnn.Close()

    End Function
#End Region
End Class

