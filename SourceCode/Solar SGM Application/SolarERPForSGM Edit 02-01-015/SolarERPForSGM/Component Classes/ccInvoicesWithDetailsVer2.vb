Option Explicit On
Imports System.Data.SqlClient
#Region "Object Structures"


#End Region

Public Class ccInvoicesWithDetailsVer2

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


    Public Function LoadAllInvoicesACAA() As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadArticles As New SqlDataAdapter
        Dim dsLoadArticles As New DataSet

        sCmd.Connection = sCnn

        If mdlSGM.sInvoiceFilterOption = "ACAA" Then
            sCmd.CommandText = "[sgm_InvoiceDetailsACAA]"
        ElseIf mdlSGM.sInvoiceFilterOption = "ACSA" Then
            sCmd.CommandText = "[sgm_InvoiceDetailsACSA]"
        ElseIf mdlSGM.sInvoiceFilterOption = "SCAA" Then
            sCmd.CommandText = "[sgm_InvoiceDetailsSCAA]"
        ElseIf mdlSGM.sInvoiceFilterOption = "SCSA" Then
            sCmd.CommandText = "[sgm_InvoiceDetailsSCSA]"
        End If

        sCmd.CommandType = CommandType.StoredProcedure


        If mdlSGM.sSelectOption = "A-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-A"
        ElseIf mdlSGM.sSelectOption = "A-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-H"
        ElseIf mdlSGM.sSelectOption = "A-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-E"

        ElseIf mdlSGM.sSelectOption = "A-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-EH"
        ElseIf mdlSGM.sSelectOption = "A-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3"
        ElseIf mdlSGM.sSelectOption = "A-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3H"
        ElseIf mdlSGM.sSelectOption = "A-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3E"
        ElseIf mdlSGM.sSelectOption = "A-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-3EH"
        ElseIf mdlSGM.sSelectOption = "A-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C"
        ElseIf mdlSGM.sSelectOption = "A-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-CH"
        ElseIf mdlSGM.sSelectOption = "A-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-CE"
        ElseIf mdlSGM.sSelectOption = "A-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-CEH"
        ElseIf mdlSGM.sSelectOption = "A-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C3"
        ElseIf mdlSGM.sSelectOption = "A-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C3H"
        ElseIf mdlSGM.sSelectOption = "A-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "A-C3E"

        ElseIf mdlSGM.sSelectOption = "G-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-A"
        ElseIf mdlSGM.sSelectOption = "G-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-H"
        ElseIf mdlSGM.sSelectOption = "G-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-E"
        ElseIf mdlSGM.sSelectOption = "G-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-EH"
        ElseIf mdlSGM.sSelectOption = "G-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3"
        ElseIf mdlSGM.sSelectOption = "G-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3H"
        ElseIf mdlSGM.sSelectOption = "G-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3E"
        ElseIf mdlSGM.sSelectOption = "G-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-3EH"
        ElseIf mdlSGM.sSelectOption = "G-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C"
        ElseIf mdlSGM.sSelectOption = "G-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-CH"
        ElseIf mdlSGM.sSelectOption = "G-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-CE"
        ElseIf mdlSGM.sSelectOption = "G-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-CEH"
        ElseIf mdlSGM.sSelectOption = "G-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C3"
        ElseIf mdlSGM.sSelectOption = "G-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C3H"
        ElseIf mdlSGM.sSelectOption = "G-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "G-C3E"

        ElseIf mdlSGM.sSelectOption = "J-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-A"
        ElseIf mdlSGM.sSelectOption = "J-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-H"
        ElseIf mdlSGM.sSelectOption = "J-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-E"
        ElseIf mdlSGM.sSelectOption = "J-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-EH"
        ElseIf mdlSGM.sSelectOption = "J-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3"
        ElseIf mdlSGM.sSelectOption = "J-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3H"
        ElseIf mdlSGM.sSelectOption = "J-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3E"
        ElseIf mdlSGM.sSelectOption = "J-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-3EH"
        ElseIf mdlSGM.sSelectOption = "J-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C"
        ElseIf mdlSGM.sSelectOption = "J-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-CH"
        ElseIf mdlSGM.sSelectOption = "J-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-CE"
        ElseIf mdlSGM.sSelectOption = "J-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-CEH"
        ElseIf mdlSGM.sSelectOption = "J-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C3"
        ElseIf mdlSGM.sSelectOption = "J-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C3H"
        ElseIf mdlSGM.sSelectOption = "J-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "J-C3E"

        ElseIf mdlSGM.sSelectOption = "JG-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-A"
        ElseIf mdlSGM.sSelectOption = "JG-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-H"
        ElseIf mdlSGM.sSelectOption = "JG-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-E"
        ElseIf mdlSGM.sSelectOption = "JG-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-EH"
        ElseIf mdlSGM.sSelectOption = "JG-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3"
        ElseIf mdlSGM.sSelectOption = "JG-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3H"
        ElseIf mdlSGM.sSelectOption = "JG-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3E"
        ElseIf mdlSGM.sSelectOption = "JG-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-3EH"
        ElseIf mdlSGM.sSelectOption = "JG-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C"
        ElseIf mdlSGM.sSelectOption = "JG-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-CH"
        ElseIf mdlSGM.sSelectOption = "JG-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-CE"
        ElseIf mdlSGM.sSelectOption = "JG-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-CEH"
        ElseIf mdlSGM.sSelectOption = "JG-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C3"
        ElseIf mdlSGM.sSelectOption = "JG-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C3H"
        ElseIf mdlSGM.sSelectOption = "JG-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "JG-C3E"

        ElseIf mdlSGM.sSelectOption = "S-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-A"
        ElseIf mdlSGM.sSelectOption = "S-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-H"
        ElseIf mdlSGM.sSelectOption = "S-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-E"
        ElseIf mdlSGM.sSelectOption = "S-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-EH"
        ElseIf mdlSGM.sSelectOption = "S-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3"
        ElseIf mdlSGM.sSelectOption = "S-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3H"
        ElseIf mdlSGM.sSelectOption = "S-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3E"
        ElseIf mdlSGM.sSelectOption = "S-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-3EH"
        ElseIf mdlSGM.sSelectOption = "S-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C"
        ElseIf mdlSGM.sSelectOption = "S-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CH"
        ElseIf mdlSGM.sSelectOption = "S-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CE"
        ElseIf mdlSGM.sSelectOption = "S-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-CEH"
        ElseIf mdlSGM.sSelectOption = "S-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C3"
        ElseIf mdlSGM.sSelectOption = "S-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C3H"
        ElseIf mdlSGM.sSelectOption = "S-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "S-C3E"

        ElseIf mdlSGM.sSelectOption = "SG-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-A"
        ElseIf mdlSGM.sSelectOption = "SG-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-H"
        ElseIf mdlSGM.sSelectOption = "SG-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-E"
        ElseIf mdlSGM.sSelectOption = "SG-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-EH"
        ElseIf mdlSGM.sSelectOption = "SG-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3"
        ElseIf mdlSGM.sSelectOption = "SG-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3H"
        ElseIf mdlSGM.sSelectOption = "SG-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3E"
        ElseIf mdlSGM.sSelectOption = "SG-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-3EH"
        ElseIf mdlSGM.sSelectOption = "SG-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C"
        ElseIf mdlSGM.sSelectOption = "SG-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-CH"
        ElseIf mdlSGM.sSelectOption = "SG-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-CE"
        ElseIf mdlSGM.sSelectOption = "SG-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-CEH"
        ElseIf mdlSGM.sSelectOption = "SG-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C3"
        ElseIf mdlSGM.sSelectOption = "SG-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C3H"
        ElseIf mdlSGM.sSelectOption = "SG-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SG-C3E"

        ElseIf mdlSGM.sSelectOption = "SJ-A" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-A"
        ElseIf mdlSGM.sSelectOption = "SJ-H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-H"
        ElseIf mdlSGM.sSelectOption = "SJ-E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-E"
        ElseIf mdlSGM.sSelectOption = "SJ-EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-EH"
        ElseIf mdlSGM.sSelectOption = "SJ-3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3"
        ElseIf mdlSGM.sSelectOption = "SJ-3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3H"
        ElseIf mdlSGM.sSelectOption = "SJ-3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3E"
        ElseIf mdlSGM.sSelectOption = "SJ-3EH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-3EH"
        ElseIf mdlSGM.sSelectOption = "SJ-C" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C"
        ElseIf mdlSGM.sSelectOption = "SJ-CH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-CH"
        ElseIf mdlSGM.sSelectOption = "SJ-CE" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-CE"
        ElseIf mdlSGM.sSelectOption = "SJ-CEH" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-CEH"
        ElseIf mdlSGM.sSelectOption = "SJ-C3" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C3"
        ElseIf mdlSGM.sSelectOption = "SJ-C3H" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C3H"
        ElseIf mdlSGM.sSelectOption = "SJ-C3E" Then : sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SJ-C3E"
        End If

        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate
        sCmd.Parameters.Add(New SqlParameter("@mBuyerName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedCustomer
        sCmd.Parameters.Add(New SqlParameter("@mArticleName", SqlDbType.VarChar)).Value() = mdlSGM.sSelectedArticle

        daLoadArticles = New SqlDataAdapter(sCmd)
        daLoadArticles.Fill(dsLoadArticles, "Art")
        Return dsLoadArticles.Tables(0)

        dsLoadArticles = Nothing
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
