Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

#End Region

Public Class ccMCD

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


    Public Function LoadShift() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelShift As New SqlDataAdapter
        Dim dsSelShift As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_MCD"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADSHIFT"

        dsSelShift.Clear()
        daSelShift = New SqlDataAdapter(sCmd)
        daSelShift.Fill(dsSelShift, "Shift")
        Return dsSelShift.Tables(0)

        dsSelShift = Nothing
        sCnn.Close()

    End Function

    Public Function LoadMachine() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelMachine As New SqlDataAdapter
        Dim dsSelMachine As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_MCD"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADMACHINE"

        dsSelMachine.Clear()
        daSelMachine = New SqlDataAdapter(sCmd)
        daSelMachine.Fill(dsSelMachine, "Machine")
        Return dsSelMachine.Tables(0)

        dsSelMachine = Nothing
        sCnn.Close()

    End Function

    Public Function LoadScannedBoxes(ByVal sSpoolId As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelSection As New SqlDataAdapter
        Dim dsSelSection As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_MCD"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADSCNDBOXES"
        sCmd.Parameters.Add(New SqlParameter("@mSpoolHID", SqlDbType.VarChar)).Value() = sSpoolId

        dsSelSection.Clear()
        daSelSection = New SqlDataAdapter(sCmd)
        daSelSection.Fill(dsSelSection, "ScannedBoxes")
        Return dsSelSection.Tables(0)

        dsSelSection = Nothing
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