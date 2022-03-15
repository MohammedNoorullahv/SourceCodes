Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

#End Region

Public Class ccSARACProduction


#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Private sErrMsg As String
    Private sErrCode As String

    Dim nRowCount As Integer

    Dim daPLDetail As New SqlDataAdapter

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


    Public Function LoadConveyorProduction(ByVal sStageType As String, ByVal sMachineNo As String, ByVal dProcessDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsPLDetail As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADCONVPROD"
        sCmd.Parameters.Add(New SqlParameter("@mStageType", SqlDbType.VarChar)).Value() = sStageType
        sCmd.Parameters.Add(New SqlParameter("@mMachineNo", SqlDbType.VarChar)).Value() = sMachineNo
        sCmd.Parameters.Add(New SqlParameter("@mProcessDate", SqlDbType.Date)).Value() = dProcessDate

        daPLDetail = New SqlDataAdapter(sCmd)
        daPLDetail.Fill(dsPLDetail, "PLDetail")
        Return dsPLDetail.Tables(0)

        dsPLDetail = Nothing
        sCnn.Close()

    End Function

    Public Function LoadJobcardProduction(ByVal sStageType As String, ByVal sJobcardNo As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daJCProd As New SqlDataAdapter
        Dim dsJCProd As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADJCPROD"
        sCmd.Parameters.Add(New SqlParameter("@mStageType", SqlDbType.VarChar)).Value() = sStageType
        sCmd.Parameters.Add(New SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value() = sJobcardNo

        daJCProd = New SqlDataAdapter(sCmd)
        daJCProd.Fill(dsJCProd, "PLDetail")
        Return dsJCProd.Tables(0)

        dsJCProd = Nothing
        sCnn.Close()

    End Function


    Public Function BarcodeStatus(ByVal sBarcode As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim daJCProd As New SqlDataAdapter
        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADBCSTATUS"
        sCmd.Parameters.Add(New SqlParameter("@mBarcode", SqlDbType.VarChar)).Value() = sBarcode


        daJCProd = New SqlDataAdapter(sCmd)
        Dim dsJCProd As New DataSet

        daJCProd.Fill(dsJCProd, "PLDetail")
        Return dsJCProd.Tables(0)

        dsJCProd = Nothing
        sCnn.Close()

    End Function

    Public Function LoadAllConveyorProduction(ByVal dProcessDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsPLDetail As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADALLCONVPROD"
        'sCmd.Parameters.Add(New SqlParameter("@mStageType", SqlDbType.VarChar)).Value() = sStageType
        'sCmd.Parameters.Add(New SqlParameter("@mMachineNo", SqlDbType.VarChar)).Value() = sMachineNo
        sCmd.Parameters.Add(New SqlParameter("@mProcessDate", SqlDbType.Date)).Value() = dProcessDate

        daPLDetail = New SqlDataAdapter(sCmd)
        daPLDetail.Fill(dsPLDetail, "PLDetail")
        Return dsPLDetail.Tables(0)

        dsPLDetail = Nothing
        sCnn.Close()

    End Function

    Public Function LoadWeeklyPlan(ByVal dProcessDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsPLDetail As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLAN"
        'sCmd.Parameters.Add(New SqlParameter("@mStageType", SqlDbType.VarChar)).Value() = sStageType
        'sCmd.Parameters.Add(New SqlParameter("@mMachineNo", SqlDbType.VarChar)).Value() = sMachineNo
        sCmd.Parameters.Add(New SqlParameter("@mProcessDate", SqlDbType.Date)).Value() = dProcessDate

        daPLDetail = New SqlDataAdapter(sCmd)
        daPLDetail.Fill(dsPLDetail, "PLDetail")
        Return dsPLDetail.Tables(0)

        dsPLDetail = Nothing
        sCnn.Close()

    End Function

    Public Function LoadWeeklyPlanWithFilter(ByVal sWeekNo As String, ByVal sConveyorNo As String) As DataTable

        Dim sCmd As New SqlCommand
        Dim dsPLDetail As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        If frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilterCategory = "All week" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLAN"
        ElseIf frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilterCategory = "Week" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLANWWK"
        ElseIf frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilterCategory = "All Conveyors" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLAN"
        ElseIf frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilterCategory = "Conveyor" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLANWConv"
        ElseIf frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilterCategory = "Week + Conveyor" Then
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLANWWKPConv"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADWKPLAN"
        End If

        sCmd.Parameters.Add(New SqlParameter("@mPlanWeek", SqlDbType.VarChar)).Value() = sWeekNo
        sCmd.Parameters.Add(New SqlParameter("@mConveyor", SqlDbType.VarChar)).Value() = sConveyorNo


        daPLDetail = New SqlDataAdapter(sCmd)
        daPLDetail.Fill(dsPLDetail, "PLDetail")
        Return dsPLDetail.Tables(0)

        dsPLDetail = Nothing
        sCnn.Close()

    End Function

    Public Function LoadOption() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelSeason As New SqlDataAdapter
        Dim dsSelSeason As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOPTON"

        dsSelSeason.Clear()
        daSelSeason = New SqlDataAdapter(sCmd)
        daSelSeason.Fill(dsSelSeason, "Season")
        Return dsSelSeason.Tables(0)

        dsSelSeason = Nothing
        sCnn.Close()

    End Function

    Public Function LoadOptionForFilter() As Boolean

        Dim sCmd As New SqlCommand
        Dim daSelSeason As New SqlDataAdapter
        Dim dsSelSeason As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "proc_SCProduction"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LOADOPTON"

        dsSelSeason.Clear()
        daSelSeason = New SqlDataAdapter(sCmd)
        daSelSeason.Fill(dsSelSeason, "Season")

        frmSaraCPRODSummary.sInfoWithFilterCategory = dsSelSeason.Tables(0).Rows(frmSaraCPRODSummary.nSelectRowNo).Item("FilterOption").ToString
        frmSaraCPRODSummary.sInfoWithFilter = dsSelSeason.Tables(0).Rows(frmSaraCPRODSummary.nSelectRowNo).Item("PlanWeek").ToString

        frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilterCategory = dsSelSeason.Tables(0).Rows(frmSaraCPRODSummaryFDDinBigScreen.nSelectRowNo).Item("FilterOption").ToString
        frmSaraCPRODSummaryFDDinBigScreen.sInfoWithFilter = dsSelSeason.Tables(0).Rows(frmSaraCPRODSummaryFDDinBigScreen.nSelectRowNo).Item("PlanWeek").ToString

        dsSelSeason = Nothing
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
