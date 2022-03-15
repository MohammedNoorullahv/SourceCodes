Option Explicit On
Imports System.Data.SqlClient
Imports System.Data

Public Class ccMCD
    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

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

    Public Function LoadWrongScannedBoxes(ByVal sSpoolId As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelSection As New SqlDataAdapter
        Dim dsSelSection As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_MCD"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWRONGBOXES"
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

End Class
