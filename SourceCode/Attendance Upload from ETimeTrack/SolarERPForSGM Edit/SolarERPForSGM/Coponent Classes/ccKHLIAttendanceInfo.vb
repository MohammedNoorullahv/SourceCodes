Option Explicit On
Imports System.Data.SqlClient

#Region "Object Structures"

Public Structure strAttendanceLog

    Dim ID As String
    Dim StaffId As Integer
    Dim AttendanceDate As Date
    Dim ShiftId As Integer
    Dim FirstPunch As Date
    Dim FirstPunchDevice As String
    Dim LastPunch As Date
    Dim LastPunchDevice As String
    Dim LateComingBy As Integer
    Dim EarlyGoingBy As Integer
    Dim EarlyOT As Integer
    Dim LateOT As Integer
    Dim Duration As Integer
    Dim Leave As String
    Dim Holiday As String
    Dim SpecialHoliday As String
    Dim SpecialOff As String
    Dim LogRecords As String
    Dim DayStatusId As Integer
    Dim WorkStatusId As Integer
    Dim Period1Status As String
    Dim Period2Status As String
    Dim LeaveReason As String
    Dim CreatedBy As String
    Dim CreatedDate As Date
    Dim ModifiedBy As Integer
    Dim ModifiedDate As Date
    Dim EnteredOnMachineID As String
    Dim ModuleName As String
    Dim IsApproved As Integer
    Dim ApprovedBy As String
    Dim ApprovedOn As Date
    Dim UnitCode As String
    Dim RefID As Integer

End Structure



#End Region

Public Class ccKHLIAttendanceInfo

#Region "Declarations"
    Inherits System.ComponentModel.Component

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.SSPLHR '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstreTT As String = Global.SolarERPForSGM.My.Settings.eTimeTrackV1
    Dim sCnneTT As New SqlConnection(sConstreTT)

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

    Public Function LoadUnit() As DataTable

        Dim sCmd As New SqlCommand
        Dim daSelUnit As New SqlDataAdapter
        Dim dsSelUnit As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_AttendanceImport"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELUNITS"


        dsSelUnit.Clear()
        daSelUnit = New SqlDataAdapter(sCmd)
        daSelUnit.Fill(dsSelUnit, "Unit")
        Return dsSelUnit.Tables(0)

        dsSelUnit = Nothing
        sCnn.Close()

    End Function

    Public Function LastImportedDate(ByVal sUnit As String) As DataTable

        Dim sCmd, sCmd1, sCmd2 As New SqlCommand
        Dim daSelUnit, daSelETTMaxDate, daLastSalMonth As New SqlDataAdapter
        Dim dsSelUnit, dsSelETTMaxDate, dsLastSalMonth As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_AttendanceImport"
        sCmd.CommandType = CommandType.StoredProcedure

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LASTDATE"
        If sUnit = "SD" Then
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = "KH"
        ElseIf sUnit = "SSPL" Then
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = "SL"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = sUnit
        End If



        daSelUnit = New SqlDataAdapter(sCmd)
        daSelUnit.Fill(dsSelUnit, "Date")

        sCmd1.Connection = sCnn
        sCmd1.CommandText = "KHLI_AttendanceImport"
        sCmd1.CommandType = CommandType.StoredProcedure

        sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "ETTMAXDATE"
        If sUnit = "SD" Then
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = "KH"
        ElseIf sUnit = "SSPL" Then
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = "SL"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = sUnit
        End If

        daSelETTMaxDate = New SqlDataAdapter(sCmd1)
        daSelETTMaxDate.Fill(dsSelETTMaxDate, "Date")

        frmAttendanceImport.dpFrom.MaxDate = DateAdd(DateInterval.Year, 1, Date.Now)
        frmAttendanceImport.dpTo.MaxDate = DateAdd(DateInterval.Year, 1, Date.Now)

        If dsSelUnit.Tables(0).Rows(0).Item("AttendaceDAte") = "1900-01-01" Then
            frmAttendanceImport.dpImportedTill.Value = "2021-01-01"
            frmAttendanceImport.dpFrom.MinDate = DateAdd(DateInterval.Day, 0, frmAttendanceImport.dpImportedTill.Value)
            'frmAttendanceImport.dpFrom.MaxDate = DateAdd(DateInterval.Day, 0, frmAttendanceImport.dpImportedTill.Value)
            frmAttendanceImport.dpFrom.Value = DateAdd(DateInterval.Day, 0, frmAttendanceImport.dpImportedTill.Value)
            frmAttendanceImport.dpTo.MinDate = DateAdd(DateInterval.Day, 0, frmAttendanceImport.dpImportedTill.Value)
            'frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Month, 1, frmAttendanceImport.dpFrom.Value)
            'frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Day, -1, frmAttendanceImport.dpTo.Value)
        Else
            frmAttendanceImport.dpImportedTill.Value = dsSelUnit.Tables(0).Rows(0).Item("AttendaceDAte")
            frmAttendanceImport.dpFrom.MinDate = DateAdd(DateInterval.Day, 1, frmAttendanceImport.dpImportedTill.Value)
            frmAttendanceImport.dpFrom.MaxDate = DateAdd(DateInterval.Day, 1, frmAttendanceImport.dpImportedTill.Value)
            frmAttendanceImport.dpFrom.Value = DateAdd(DateInterval.Day, 1, frmAttendanceImport.dpImportedTill.Value)
            frmAttendanceImport.dpTo.MinDate = DateAdd(DateInterval.Day, 1, frmAttendanceImport.dpImportedTill.Value)
            'frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Month, 1, frmAttendanceImport.dpFrom.Value)
            'frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Day, -1, frmAttendanceImport.dpTo.Value)
        End If

        'frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Month, 1, frmAttendanceImport.dpFrom.Value)
        'frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Day, -1, frmAttendanceImport.dpTo.Value)

        If dsSelETTMaxDate.Tables(0).Rows(0).Item("AttendaceDAte") = "1900-01-01" Then
            frmAttendanceImport.dpTo.MaxDate = Date.Now
        Else
            frmAttendanceImport.dpTo.MaxDate = dsSelETTMaxDate.Tables(0).Rows(0).Item("AttendaceDAte")
        End If

        

        If frmAttendanceImport.dpTo.Value > Date.Now Then
            '    frmAttendanceImport.dpTo.Value = DateAdd(DateInterval.Day, 1, Date.Now)
            '    frmAttendanceImport.dpTo.MaxDate = DateAdd(DateInterval.Day, 1, Date.Now)
            'Else
            frmAttendanceImport.dpTo.MaxDate = Date.Now
        End If


        sCmd2.Connection = sCnn
        sCmd2.CommandText = "KHLI_AttendanceImport"
        sCmd2.CommandType = CommandType.StoredProcedure

        sCmd2.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LASTSALDATE"
        If sUnit = "KH" Then
            sCmd2.Parameters.Add(New SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value() = "SD"
        Else
            sCmd2.Parameters.Add(New SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value() = sUnit
        End If

        daLastSalMonth = New SqlDataAdapter(sCmd2)
        daLastSalMonth.Fill(dsLastSalMonth, "Date")

        Dim DLastSalDate As Date = dsLastSalMonth.Tables(0).Rows(0).Item("SalDate")
        Dim NLastSalDay As Integer
        If DLastSalDate = "1900-01-01" Then
            frmAttendanceImport.dpSumaryDate.MinDate = "2021-01-01"
        Else
            DLastSalDate = DateAdd(DateInterval.Month, 1, DLastSalDate)
            NLastSalDay = DLastSalDate.Date.Day - 1
            DLastSalDate = DateAdd(DateInterval.Day, -NLastSalDay, DLastSalDate)
            frmAttendanceImport.dpSumaryDate.MinDate = DLastSalDate.Date

        End If

        dsSelUnit = Nothing
        sCnn.Close()

    End Function

    Public Function LoadAttendanceInfoinAP(ByVal sUnitName As String, ByVal dDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadAttendance As New SqlDataAdapter
        Dim dsLoadAttendance As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_AttendanceImport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "ATT4AP"
        sCmd.Parameters.Add(New SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value() = sUnitName
        sCmd.Parameters.Add(New SqlParameter("@mAttendanceDate", SqlDbType.DateTime)).Value() = dDate

        daLoadAttendance = New SqlDataAdapter(sCmd)
        daLoadAttendance.Fill(dsLoadAttendance, "Att")
        Return dsLoadAttendance.Tables(0)

        dsLoadAttendance = Nothing
        sCnn.Close()

    End Function

    Public Function LoadAttendanceLogfromETT(ByVal sUnitName As String, ByVal dDate As Date) As DataTable
        

        Dim sCmd As New SqlCommand
        Dim daLoadAttendance As New SqlDataAdapter
        Dim dsLoadAttendance As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_AttendanceImport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELPENETT"
        sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = sUnitName
        sCmd.Parameters.Add(New SqlParameter("@mAttendanceDate", SqlDbType.DateTime)).Value() = dDate

        daLoadAttendance = New SqlDataAdapter(sCmd)
        daLoadAttendance.Fill(dsLoadAttendance, "Att")

        
        Return dsLoadAttendance.Tables(0)

        dsLoadAttendance = Nothing
        sCnn.Close()

    End Function

    Public Function LoadAttendanceSummaryDetails(ByVal sUnitName As String, ByVal NAttnMon As Integer, ByVal NAttYear As Integer) As DataTable


        Dim sCmd As New SqlCommand
        Dim daLoadAttendance As New SqlDataAdapter
        Dim dsLoadAttendance As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_AttendanceImport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELATTSUMDTLS"
        sCmd.Parameters.Add(New SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value() = sUnitName
        sCmd.Parameters.Add(New SqlParameter("@mAttYear", SqlDbType.Int)).Value() = NAttYear
        sCmd.Parameters.Add(New SqlParameter("@mAttMonth", SqlDbType.Int)).Value() = NAttnMon

        daLoadAttendance = New SqlDataAdapter(sCmd)
        daLoadAttendance.Fill(dsLoadAttendance, "Att")


        Return dsLoadAttendance.Tables(0)

        dsLoadAttendance = Nothing
        sCnn.Close()

    End Function

    Public Function ImportAttendanceinAP(ByVal sUnitName As String, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable

        Dim sCmd As New SqlCommand
        Dim daLoadAttendance As New SqlDataAdapter
        Dim dsLoadAttendance As New DataSet

        sCmd.Connection = sCnn
        sCmd.CommandText = "KHLI_AttendanceImport"
        sCmd.CommandType = CommandType.StoredProcedure


        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "BULKINST"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = FromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = ToDate
        If sUnitName = "KH" Then
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = "SD"
        ElseIf sUnitName = "SSPL" Then
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = "SL"
        Else
            sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = sUnitName
        End If


        sCnn.Open()

        Dim sRes As String = sCmd.ExecuteScalar

        If Val(sRes) = 0 Then
            sCnn.Close()
        Else
            setError(Val(sRes))
        End If
        sCnn.Close()
        Exit Function

        sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "SELATTEN"
        sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = FromDate
        sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = ToDate
        sCmd.Parameters.Add(New SqlParameter("@mStaffCode1", SqlDbType.VarChar)).Value() = sUnitName

        Dim i As Integer

        daLoadAttendance = New SqlDataAdapter(sCmd)
        daLoadAttendance.Fill(dsLoadAttendance, "Att")

        For i = 0 To dsLoadAttendance.Tables(0).Rows.Count - 1

            Dim sCmd1 As New SqlCommand
            Dim daInsAttendance As New SqlDataAdapter
            Dim dsInsAttendance As New DataSet

            sCmd1.Connection = sCnn
            sCmd1.CommandText = "KHLI_AttendanceImport"
            sCmd1.CommandType = CommandType.StoredProcedure

            
            sCmd1.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "INSATTINAP"
            sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("ID")
            sCmd1.Parameters.Add(New SqlParameter("@mEMPCode", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("EmpCode")
            sCmd1.Parameters.Add(New SqlParameter("@mDepartment", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("EmployeeDepartment")
            sCmd1.Parameters.Add(New SqlParameter("@mAttDate", SqlDbType.DateTime)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("AttDate")
            sCmd1.Parameters.Add(New SqlParameter("@mAttStatus", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("AttStatus")
            sCmd1.Parameters.Add(New SqlParameter("@mAttMonth", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("AttMonth")
            sCmd1.Parameters.Add(New SqlParameter("@mAttYear", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("AttYear")
            sCmd1.Parameters.Add(New SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value() = frmAttendanceImport.cbxUnitName.Text ''dsLoadAttendance.Tables(0).Rows(i).Item("AttYear")
            sCmd1.Parameters.Add(New SqlParameter("@mOldEmpCode", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("OldEmpCode")
            sCmd1.Parameters.Add(New SqlParameter("@mStaffId", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("StaffId")
            sCmd1.Parameters.Add(New SqlParameter("@mAttDay", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("AttDay")
            sCmd1.Parameters.Add(New SqlParameter("@mEmpName", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("EmpName")
            'sCmd1.Parameters.Add(New SqlParameter("@mID", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("ID")
            ''sCmd1.Parameters.Add(New SqlParameter("@mStaffId", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("StaffId")

            'sCmd1.Parameters.Add(New SqlParameter("@mStaffId", SqlDbType.VarChar)).Value() = Val(dsLoadAttendance.Tables(0).Rows(i).Item("StaffId").ToString)
            'sCmd1.Parameters.Add(New SqlParameter("@mOldEmpCode", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("StaffCode").ToString
            'sCmd1.Parameters.Add(New SqlParameter("@mEmpCode", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("EmpCode").ToString

            'sCmd1.Parameters.Add(New SqlParameter("@mAttendanceDate", SqlDbType.Date)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("AttendanceDate")
            'sCmd1.Parameters.Add(New SqlParameter("@mShiftId", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("ShiftId")
            'sCmd1.Parameters.Add(New SqlParameter("@mFirstPunch", SqlDbType.Date)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("FirstPunch")
            'sCmd1.Parameters.Add(New SqlParameter("@mFirstPunchDevice", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("FirstPunchDevice")
            'sCmd1.Parameters.Add(New SqlParameter("@mLastPunch", SqlDbType.Date)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("LastPunch")
            'sCmd1.Parameters.Add(New SqlParameter("@mLastPunchDevice", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("LastPunchDevice")
            'sCmd1.Parameters.Add(New SqlParameter("@mLateComingBy", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("LateComingBy")
            'sCmd1.Parameters.Add(New SqlParameter("@mEarlyGoingBy", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("EarlyGoingBy")
            'sCmd1.Parameters.Add(New SqlParameter("@mEarlyOT", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("EarlyOT")
            'sCmd1.Parameters.Add(New SqlParameter("@mLateOT", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("LateOT")
            'sCmd1.Parameters.Add(New SqlParameter("@mDuration", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("Duration")
            'sCmd1.Parameters.Add(New SqlParameter("@mLeave", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("Leave")
            'sCmd1.Parameters.Add(New SqlParameter("@mHoliday", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("Holiday")
            'sCmd1.Parameters.Add(New SqlParameter("@mSpecialHoliday", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("SpecialHoliday")
            'sCmd1.Parameters.Add(New SqlParameter("@mSpecialOff", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("SpecialOff")
            'sCmd1.Parameters.Add(New SqlParameter("@mLogRecords", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("LogRecords")
            'sCmd1.Parameters.Add(New SqlParameter("@mDayStatusId", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("DayStatusId")
            'sCmd1.Parameters.Add(New SqlParameter("@mWorkStatusId", SqlDbType.Int)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("WorkStatusId")
            'sCmd1.Parameters.Add(New SqlParameter("@mPeriod1Status", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("Period1Status")
            'sCmd1.Parameters.Add(New SqlParameter("@mPeriod2Status", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("Period2Status")
            'sCmd1.Parameters.Add(New SqlParameter("@mLeaveReason", SqlDbType.VarChar)).Value() = dsLoadAttendance.Tables(0).Rows(i).Item("LeaveReason")
            'sCmd1.Parameters.Add(New SqlParameter("@mCreatedBy", SqlDbType.VarChar)).Value() = ""
            'sCmd1.Parameters.Add(New SqlParameter("@mCreatedDate", SqlDbType.Date)).Value() = Format(Date.Now, "dd-MMM-yyyy hh:MM:ss") ''2015-02-25 17:06:25.010
            'sCmd1.Parameters.Add(New SqlParameter("@mModifiedBy", SqlDbType.VarChar)).Value() = ""
            'sCmd1.Parameters.Add(New SqlParameter("@mModifiedDate", SqlDbType.Date)).Value() = Format(Date.Now, "dd-MMM-yyyy hh:MM:ss")
            'sCmd1.Parameters.Add(New SqlParameter("@mEnteredOnMachineID", SqlDbType.VarChar)).Value() = ""
            'sCmd1.Parameters.Add(New SqlParameter("@mModuleName", SqlDbType.VarChar)).Value() = "ERP"
            'sCmd1.Parameters.Add(New SqlParameter("@mIsApproved", SqlDbType.VarChar)).Value() = ""
            'sCmd1.Parameters.Add(New SqlParameter("@mApprovedBy", SqlDbType.VarChar)).Value() = ""
            'sCmd1.Parameters.Add(New SqlParameter("@mApprovedOn", SqlDbType.Date)).Value() = Format(Date.Now, "dd-MMM-yyyy hh:MM:ss")
            'sCmd1.Parameters.Add(New SqlParameter("@mUnitCode", SqlDbType.VarChar)).Value() = sUnitName
            'sCmd1.Parameters.Add(New SqlParameter("@mRefID", SqlDbType.VarChar)).Value() = ""

            sCnn.Close()
            sCnn.Open()

            Dim sRes1 As String = sCmd1.ExecuteScalar

            If Val(sRes1) = 0 Then
                sCnn.Close()
            Else
                setError(Val(sRes1))
            End If
            sCnn.Close()


        Next

        Return dsLoadAttendance.Tables(0)

        dsLoadAttendance = Nothing
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


