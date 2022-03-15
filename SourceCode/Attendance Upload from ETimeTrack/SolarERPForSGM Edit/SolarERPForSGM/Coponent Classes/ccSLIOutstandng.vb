Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

#End Region

Public Class ccSLIOutstandng

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

    Dim nPKID As Integer
    Dim sSalesOrderDetailId As String
    Dim dSalesOrderDate As Date
    Dim sCustomerName As String
    Dim sSalesOrderNo As String
    Dim sCustomerOrderNo As String
    Dim sSoleCode As String
    Dim sSoleName As String
    Dim sColour As String
    Dim sCodification As String
    Dim nOrdQty As Integer
    Dim nMoulding As Integer
    Dim nMouldingWIP As Integer
    Dim nFinishing As Integer
    Dim nFinishingWIP As Integer
    Dim nPacking As Integer
    Dim nInStock As Integer
    Dim nDispatch As Integer
    Dim UdpdatedOn As Date
    Dim nIsCompleted, nIsClosed As Integer
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


    Public Function LoadOutstanding(ByVal sSeason As String, ByVal sTypeofOrder As String) As DataTable

        Dim sCmd As New SqlCommand

        Dim daLoadPending As New SqlDataAdapter
        Dim dsLoadPending As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "SLI_OrderOutstanding"
        sCmd.CommandType = CommandType.StoredProcedure



        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTANDING"
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = sSeason
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder


        daLoadPending = New SqlDataAdapter(sCmd)
        daLoadPending.Fill(dsLoadPending, "Pending")
        Return dsLoadPending.Tables(0)

        dsLoadPending = Nothing
        sCnn.Close()

    End Function

    Public Function LoadOutstandingSummary(ByVal sSeason As String, ByVal sTypeofOrder As String) As DataTable

        Dim sCmd As New SqlCommand

        Dim daLoadPending As New SqlDataAdapter
        Dim dsLoadPending As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "SLI_OrderOutstanding"
        sCmd.CommandType = CommandType.StoredProcedure



        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOUTSTANDINGSUM"
        sCmd.Parameters.Add(New SqlParameter("@mSeason", SqlDbType.VarChar)).Value() = sSeason
        sCmd.Parameters.Add(New SqlParameter("@mTypeofOrder", SqlDbType.VarChar)).Value() = sTypeofOrder


        daLoadPending = New SqlDataAdapter(sCmd)
        daLoadPending.Fill(dsLoadPending, "Pending")
        Return dsLoadPending.Tables(0)

        dsLoadPending = Nothing
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
