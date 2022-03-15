Imports System.Data.SqlClient

Public Class frmAttendanceImport

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrHR As String = Global.SolarERPForSGM.My.Settings.SSPLHR '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sConHR As New SqlConnection(sConstrHR)

    Dim myccKHLIAttendanceInfo As New ccKHLIAttendanceInfo
    Dim sIsLoading As String = "N"

    Dim StrUnitName, StrUnitCode As String
    Dim NFullDayAbsent, NDayNo As Integer

#Region "HRAttendanceStatusSummaryDetails Field"
    Dim SID As String
    Dim SCreatedBy As String
    Dim DCreatedDate As DateTime
    Dim SModifiedBy As String
    Dim DModifiedDate As DateTime
    Dim SExeVersionNo As String
    Dim NIsApproved As Integer
    Dim SApprovedBy As String
    Dim DApprovedOn As DateTime
    Dim SModuleName As String
    Dim SEMPCode As String
    Dim SDepartment As String
    Dim DAttDate As DateTime
    Dim SAttStatus As String
    Dim NAttMonth As Integer
    Dim NAttYear As Integer
    Dim SUnitCode As String
    Dim DLPresentDays As Decimal
    Dim DLAbsent As Decimal
    Dim DLEarnedLeave As Decimal
    Dim DLCasualLeave As Decimal
    Dim DLEligibleSunday As Decimal
    Dim DLWeeklyoff As Decimal
    Dim DLLongLeave As Decimal
    Dim DLLayOff As Decimal
    Dim DLSuspension As Decimal
    Dim DLSickLeave As Decimal
    Dim DLMaternityLeave As Decimal
    Dim DLElectionHoliday As Decimal
    Dim DLNationalHoliday As Decimal
    Dim DLFestivalHoliday As Decimal
    Dim DLOnDuty As Decimal
    Dim DLTotalPayableDays As Decimal
    Dim SD01 As String
    Dim SD02 As String
    Dim SD03 As String
    Dim SD04 As String
    Dim SD05 As String
    Dim SD06 As String
    Dim SD07 As String
    Dim SD08 As String
    Dim SD09 As String
    Dim SD10 As String
    Dim SD11 As String
    Dim SD12 As String
    Dim SD13 As String
    Dim SD14 As String
    Dim SD15 As String
    Dim SD16 As String
    Dim SD17 As String
    Dim SD18 As String
    Dim SD19 As String
    Dim SD20 As String
    Dim SD21 As String
    Dim SD22 As String
    Dim SD23 As String
    Dim SD24 As String
    Dim SD25 As String
    Dim SD26 As String
    Dim SD27 As String
    Dim SD28 As String
    Dim SD29 As String
    Dim SD30 As String
    Dim SD31 As String
    Dim SPID As String

#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        'Form1.Show()
        End
    End Sub

    Dim ngrdRowCount As Integer
    Private Sub LoadImportedData()
        ''Try

        'GoTo Aa
        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdAttendaceInfoinAPv1.RowCount
        For i = 0 To ngrdRowCount
            grdAttendaceInfoinAPv1.DeleteRow(i)
        Next
        ngrdRowCount = grdAttendaceInfoinAPv1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        If StrUnitCode = "KH" Then
            grdAttendaceInfoinAP.DataSource = myccKHLIAttendanceInfo.LoadAttendanceInfoinAP("SD", Format(dFirstDate.Date, "dd-MMMM-yyyy"))
        Else
            grdAttendaceInfoinAP.DataSource = myccKHLIAttendanceInfo.LoadAttendanceInfoinAP(StrUnitCode, Format(dFirstDate.Date, "dd-MMMM-yyyy"))
        End If


        'Aa:
        With grdAttendaceInfoinAPv1
            '.Columns(0).VisibleIndex = -1
            '.Columns(1).VisibleIndex = -1
            ''.Columns(22).VisibleIndex = -1

            '.Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            '.Columns(15).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(15).DisplayFormat.FormatString = "dd-MMM-yyyy"

            ' ''.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(6).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(7).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(8).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(9).DisplayFormat.FormatString = "0.00"
            'Dim j As Integer = 19

            'For j = 19 To 52
            '    .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            'Next
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count


        End With


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadPendingDataForImport()
        ''Try

        'GoTo Aa
        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdAttendaceInfoineTimeTrackV1.RowCount
        For i = 0 To ngrdRowCount
            grdAttendaceInfoineTimeTrackV1.DeleteRow(i)
        Next
        ngrdRowCount = grdAttendaceInfoineTimeTrackV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        Dim sUnitCode As String = StrUnitCode
        If sUnitCode = "SSPL" Then
            sUnitCode = "SL"
        End If

        grdAttendaceInfoineTimeTrack.DataSource = myccKHLIAttendanceInfo.LoadAttendanceLogfromETT(sUnitCode, Format(dpFrom.Value, "dd-MMMM-yyyy"))

        'Aa:
        With grdAttendaceInfoineTimeTrackV1
            '.Columns(0).VisibleIndex = -1
            '.Columns(1).VisibleIndex = -1
            ''.Columns(22).VisibleIndex = -1

            '.Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            '.Columns(15).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(15).DisplayFormat.FormatString = "dd-MMM-yyyy"

            ' ''.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(6).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(7).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(8).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(9).DisplayFormat.FormatString = "0.00"
            'Dim j As Integer = 19

            'For j = 19 To 52
            '    .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            'Next
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count


        End With

        If grdAttendaceInfoineTimeTrackV1.RowCount <= 0 Then
            cbImport.Enabled = False
        Else
            cbImport.Enabled = True
        End If
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadUnits()
        ''Try

        Dim daSelUnit As New SqlDataAdapter("Select * from UnitMaster Where UnitCode in (Select FullName_ From AbbrevTable Where Abbrev_ = '" & mdlSGM.strIPAddress & "')", sConHR)
        Dim dsSelUnit As New DataSet
        daSelUnit.Fill(dsSelUnit)

        If dsSelUnit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("This System IP Is Not Assigned. So Attendance Log will not be allowed to Import from this System")
        ElseIf dsSelUnit.Tables(0).Rows.Count > 1 Then
            MessageBox.Show("This System IP Is Assigned for Multiple Times. So Attendance Log will not be allowed to Import from this System")
        Else
            StrUnitCode = dsSelUnit.Tables(0).Rows(0).Item("UnitCode").ToString()
            If StrUnitCode = "SD" Then
                StrUnitCode = "KH"
            ElseIf StrUnitCode = "SD" Then
                StrUnitCode = "KH"
            End If
            StrUnitName = dsSelUnit.Tables(0).Rows(0).Item("UnitName").ToString()
            tbUnitName.Text = dsSelUnit.Tables(0).Rows(0).Item("UnitName").ToString() + " [ " + dsSelUnit.Tables(0).Rows(0).Item("UnitCode").ToString() + " ] "

            myccKHLIAttendanceInfo.LastImportedDate(StrUnitCode)


            Dim dSelectedDate As Date = dpFrom.Value
            Dim nCurrentDay As Integer = Format(dpFrom.Value, "dd")
            dFirstDate = DateAdd(DateInterval.Day, -(nCurrentDay - 1), dpFrom.Value)

            LoadImportedData()
            LoadPendingDataForImport()
            If StrUnitCode = "KH" Then
                LoadAttendanceSummeryDtls("SD", Val(Format(dpSumaryDate.Value, "MM")), Val(Format(dpSumaryDate.Value, "yyyy")))
            Else
                LoadAttendanceSummeryDtls(StrUnitCode, Val(Format(dpSumaryDate.Value, "MM")), Val(Format(dpSumaryDate.Value, "yyyy")))
            End If

        End If


        'sIsLoading = "Y"
        'cbxUnitName.DataSource = Nothing : cbxUnitName.Items.Clear()
        'cbxUnitName.DataSource = myccKHLIAttendanceInfo.LoadUnit
        'cbxUnitName.DisplayMember = "UnitCode" '': cbxArticleName.ValueMember = "PKID"
        'sIsLoading = "N"

        'Catch Exp As Exception
        ''HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        mdlSGM.strHostName = System.Net.Dns.GetHostName()
        '        mdlSGM.strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()
        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)

        mdlSGM.strIPAddress = h.AddressList.GetValue(0).ToString
        LoadUnits()

        dpSumaryDate.MaxDate = Date.Now

        tbSummaryMonth.Text = Format(dpSumaryDate.Value, "MM")
        tbSummaryYear.Text = Format(dpSumaryDate.Value, "yyyy")

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'LoadUpperDispatchBalance()
    End Sub



    Private Sub cbxUnitName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxUnitName.SelectedIndexChanged
        If sIsLoading = "N" Then
            myccKHLIAttendanceInfo.LastImportedDate(StrUnitCode)
            LoadImportedData()
        End If
    End Sub


    Private Sub cbImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImport.Click
        myccKHLIAttendanceInfo.ImportAttendanceinAP(StrUnitCode, Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"))

        myccKHLIAttendanceInfo.LastImportedDate(StrUnitCode)

        Dim dSelectedDate As Date = dpFrom.Value
        Dim nCurrentDay As Integer = Format(dpFrom.Value, "dd")
        dFirstDate = DateAdd(DateInterval.Day, -(nCurrentDay - 1), dpFrom.Value)


        LoadImportedData()
        MsgBox("Import Completed")
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

#Region "Coding for Generating HRAttendanceStatusSummaryDetails"

    Private Sub dpSumaryDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpSumaryDate.ValueChanged
        tbSummaryMonth.Text = Format(dpSumaryDate.Value, "MM")
        tbSummaryYear.Text = Format(dpSumaryDate.Value, "yyyy")
        If StrUnitCode = "KH" Then
            LoadAttendanceSummeryDtls("SD", Val(Format(dpSumaryDate.Value, "MM")), Val(Format(dpSumaryDate.Value, "yyyy")))
        Else
            LoadAttendanceSummeryDtls(StrUnitCode, Val(Format(dpSumaryDate.Value, "MM")), Val(Format(dpSumaryDate.Value, "yyyy")))
        End If
    End Sub

    Dim dFirstDay, dLastDay, dFirstDate As Date
    Dim dAttenDate As Date
    Dim nDayNumber, nESEligibleDays, nLastDay As Integer
    Dim SOldEmpCode, SEmpName As String

    Private Sub cbUpdateStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUpdateStatus.Click
        If chkbxSingleDay.Checked = True Then
            If dpSumaryDate.Value.DayOfWeek = DayOfWeek.Sunday Then
                MessageBox.Show("For Single Day Option, SUNDAY cannot be processed", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        NFullDayAbsent = 0
        NDayNo = 0
        Dim daSelESEligibility As New SqlDataAdapter("Select * from HRAttendanceStatuseligiblecriteria Where AttendanceStatus = 'ES'", sConHR)
        Dim dsSelESEligibility As New DataSet
        daSelESEligibility.Fill(dsSelESEligibility)

        nESEligibleDays = Val(dsSelESEligibility.Tables(0).Rows(0).Item("NoOfEligibleDays").ToString())

        Dim dSelectedDate As Date = dpSumaryDate.Value
        Dim nCurrentDay As Integer = Format(dpSumaryDate.Value, "dd")
        dFirstDay = DateAdd(DateInterval.Day, -(nCurrentDay - 1), dpSumaryDate.Value)
        Dim dNextMonthFistDay As Date = DateAdd(DateInterval.Month, 1, dFirstDay.Date)
        dLastDay = DateAdd(DateInterval.Day, -1, dNextMonthFistDay.Date)
        nLastDay = Format(dLastDay.Date, "dd")

        If StrUnitCode = "KH" Then
            StrUnitCode = "SD"
        End If

        NAttMonth = Val(tbSummaryMonth.Text)
        NAttYear = Val(tbSummaryYear.Text)

        Dim daSelGeneratingMonth As New SqlDataAdapter("Select * from GeneratingMonth Where UnitCode = '" & StrUnitCode & _
                                                       "' And GeneratingMonth = '" & NAttMonth & _
                                                       "' And GeneratingYear = '" & NAttYear & "'", sConstrHR)
        Dim dsSelGeneratingMonth As New DataSet
        daSelGeneratingMonth.Fill(dsSelGeneratingMonth)

        If dsSelGeneratingMonth.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Generating Month Not assigned properly. Hence Attendance Update will not be completed")
            Exit Sub
        End If


        Dim daSelSummaryInfo As New SqlDataAdapter("Select * from HRAttendanceStatusSummery Where UnitCode = '" & StrUnitCode & "' And GeneratingMonth = '" & Val(tbSummaryMonth.Text) & _
                                                   "' And GeneratingYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
        Dim dsSelSummaryInfo As New DataSet
        daSelSummaryInfo.Fill(dsSelSummaryInfo)

        If dsSelSummaryInfo.Tables(0).Rows.Count > 0 Then

            If chkbxSingleDay.Checked = True Then

            Else
                Dim nYesNo As Integer
                nYesNo = MsgBox("Selected Period Attendance Already Imported. Do you the delete the existing one? And Import New Data?", MsgBoxStyle.YesNo)

                If nYesNo = 6 Then
                    Dim daDelSummaryDtls As New SqlDataAdapter("Delete from HRAttendanceStatusSummeryDetails Where UnitCode = '" & StrUnitCode & "' And AttMonth = '" & Val(tbSummaryMonth.Text) & _
                                                       "' And AttYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
                    Dim dsDelSummaryDtls As New DataSet
                    daDelSummaryDtls.Fill(dsDelSummaryDtls)
                    dsDelSummaryDtls.AcceptChanges()

                    Dim daDelSummary As New SqlDataAdapter("Delete from HRAttendanceStatusSummery Where UnitCode = '" & StrUnitCode & "' And GeneratingMonth = '" & Val(tbSummaryMonth.Text) & _
                                                           "' And GeneratingYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
                    Dim dsDelSummary As New DataSet
                    daDelSummary.Fill(dsDelSummary)
                    dsDelSummary.AcceptChanges()

                Else
                    Exit Sub
                End If
            End If
        Else
            If chkbxSingleDay.Checked = True Then
                MsgBox("Single Date Data cannot be updated withintout First Updating Month Status", MsgBoxStyle.Critical)
                Exit Sub
            End If

        End If
        If chkbxSingleDay.Checked = False Then
            InsertSummaryHdr()
        End If

        Dim daSelEmployee As New SqlDataAdapter("Select * from Employee Where IsActive = '1' And UnitCode = '" & StrUnitCode & _
                                                "' Order by EmpCode", sConHR)
        Dim dsSelEmployee As New DataSet
        daSelEmployee.Fill(dsSelEmployee)

        Dim nEmployeeCount As Integer = dsSelEmployee.Tables(0).Rows.Count()

        Dim daSelUnitInfo As New SqlDataAdapter("Select * from UnitMaster Where UnitCode = '" & StrUnitCode & "'", sConHR)
        Dim dsSelUnitInfo As New DataSet
        daSelUnitInfo.Fill(dsSelUnitInfo)

        nWeekWorkingDays = Val(dsSelUnitInfo.Tables(0).Rows(0).Item("WeekWorkingDays").ToString())

        If nWeekWorkingDays = 5 Then
            If chkbxSingleDay.Checked = True Then
                If dpSumaryDate.Value.DayOfWeek = DayOfWeek.Saturday Then
                    MessageBox.Show("For Single Day Option, SATURDAY & SUNDAY cannot be processed", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
        End If

        Dim i As Integer = 0

        For i = 0 To nEmployeeCount - 1
            pgb1.Maximum = nEmployeeCount
            pgb1.Value = i
            NFullDayAbsent = 0
            NDayNo = 0
            pgb1.PerformStep()
            ClearAttendanceInfo()
            SEMPCode = dsSelEmployee.Tables(0).Rows(i).Item("EmpCode").ToString()
            SDepartment = dsSelEmployee.Tables(0).Rows(i).Item("EmployeeDepartment").ToString()
            SUnitCode = dsSelEmployee.Tables(0).Rows(i).Item("UnitCode").ToString()
            SOldEmpCode = dsSelEmployee.Tables(0).Rows(i).Item("OlDEmpCode").ToString()
            SEmpName = dsSelEmployee.Tables(0).Rows(i).Item("EmpFullName").ToString()
            'If chkbxSingleDay.Checked = True Then
            '    dAttenDate = dpSumaryDate.Value
            '    nDayNumber = Val(Format(dpSumaryDate.Value, "dd"))
            'Else
            dAttenDate = dFirstDay
            nDayNumber = 1
            'End If

            NAttMonth = Val(tbSummaryMonth.Text)
            NAttYear = Val(tbSummaryYear.Text)

            LoadPreviousMonthInfo()
            Dim sRecordExist As String = ""

            Dim dsSelEmpAttInfo As New DataSet
            Dim dsSelEmpAttInfoFSD As New DataSet
            If chkbxSingleDay.Checked = True Then
                Dim daSelEmpAttInfo As New SqlDataAdapter("Select * from HRAttendanceStatusSummeryDetails Where Empcode = '" & SEMPCode & _
                                                      "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & _
                                                      "'", sConHR)
                daSelEmpAttInfo.Fill(dsSelEmpAttInfo)
                sSummaryDtlID = dsSelEmpAttInfo.Tables(0).Rows(0).Item("ID").ToString()
                Dim daSelEmpAttInfoFSD As New SqlDataAdapter("Select * from vw_AttendanceSummary Where Empcode = '" & SEMPCode & _
                                                             "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & "'", sConHR)

                daSelEmpAttInfoFSD.Fill(dsSelEmpAttInfoFSD)
                If dsSelEmpAttInfoFSD.Tables(0).Rows.Count = 0 Then
                    sRecordExist = "N"
                Else
                    sRecordExist = "Y"
                End If
            Else
                Dim daSelEmpAttInfo As New SqlDataAdapter("Select * from vw_AttendanceSummary Where Empcode = '" & SEMPCode & _
                                                          "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & "'", sConHR)
                daSelEmpAttInfo.Fill(dsSelEmpAttInfo)
            End If


            If dsSelEmpAttInfo.Tables(0).Rows.Count = 1 Then

                If nWeekWorkingDays = 6 Then
                    If chkbxSingleDay.Checked = True Then
                        If Format(dpSumaryDate.Value, "dd") = "01" And sRecordExist = "Y" Then
                            SD01 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("1").ToString()
                        Else
                            SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D01").ToString()
                        End If
                        CalculateES() : sDayStatus = SD01 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "02" And sRecordExist = "Y" Then
                            SD02 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("2").ToString()
                        Else
                            SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D02").ToString()
                        End If
                        CalculateES() : sDayStatus = SD02 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "03" And sRecordExist = "Y" Then
                            SD03 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("3").ToString()
                        Else
                            SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D03").ToString()
                        End If
                        CalculateES() : sDayStatus = SD03 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "04" And sRecordExist = "Y" Then
                            SD04 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("4").ToString()
                        Else
                            SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D04").ToString()
                        End If
                        CalculateES() : sDayStatus = SD04 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "05" And sRecordExist = "Y" Then
                            SD05 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("5").ToString()
                        Else
                            SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D05").ToString()
                        End If
                        CalculateES() : sDayStatus = SD05 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "06" And sRecordExist = "Y" Then
                            SD06 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("6").ToString()
                        Else
                            SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D06").ToString()
                        End If
                        CalculateES() : sDayStatus = SD06 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "07" And sRecordExist = "Y" Then
                            SD07 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("7").ToString()
                        Else
                            SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D07").ToString()
                        End If
                        CalculateES() : sDayStatus = SD07 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "08" And sRecordExist = "Y" Then
                            SD08 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("8").ToString()
                        Else
                            SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D08").ToString()
                        End If
                        CalculateES() : sDayStatus = SD08 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "09" And sRecordExist = "Y" Then
                            SD09 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("9").ToString()
                        Else
                            SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D09").ToString()
                        End If
                        CalculateES() : sDayStatus = SD09 : UpdateCount()


                        If Format(dpSumaryDate.Value, "dd") = "10" And sRecordExist = "Y" Then
                            SD10 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("10").ToString()
                        Else
                            SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D10").ToString()
                        End If
                        CalculateES() : sDayStatus = SD10 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "11" And sRecordExist = "Y" Then
                            SD11 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("11").ToString()
                        Else
                            SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D11").ToString()
                        End If
                        CalculateES() : sDayStatus = SD11 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "12" And sRecordExist = "Y" Then
                            SD12 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("12").ToString()
                        Else
                            SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D12").ToString()
                        End If
                        CalculateES() : sDayStatus = SD12 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "13" And sRecordExist = "Y" Then
                            SD13 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("13").ToString()
                        Else
                            SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D13").ToString()
                        End If
                        CalculateES() : sDayStatus = SD13 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "14" And sRecordExist = "Y" Then
                            SD14 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("14").ToString()
                        Else
                            SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D14").ToString()
                        End If
                        CalculateES() : sDayStatus = SD14 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "15" And sRecordExist = "Y" Then
                            SD15 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("15").ToString()
                        Else
                            SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D15").ToString()
                        End If
                        CalculateES() : sDayStatus = SD15 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "16" And sRecordExist = "Y" Then
                            SD16 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("16").ToString()
                        Else
                            SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D16").ToString()
                        End If
                        CalculateES() : sDayStatus = SD16 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "17" And sRecordExist = "Y" Then
                            SD17 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("17").ToString()
                        Else
                            SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D17").ToString()
                        End If
                        CalculateES() : sDayStatus = SD17 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "18" And sRecordExist = "Y" Then
                            SD18 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("18").ToString()
                        Else
                            SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D18").ToString()
                        End If
                        CalculateES() : sDayStatus = SD18 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "19" And sRecordExist = "Y" Then
                            SD19 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("19").ToString()
                        Else
                            SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D19").ToString()
                        End If
                        CalculateES() : sDayStatus = SD19 : UpdateCount()


                        If Format(dpSumaryDate.Value, "dd") = "20" And sRecordExist = "Y" Then
                            SD20 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("20").ToString()
                        Else
                            SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D20").ToString()
                        End If
                        CalculateES() : sDayStatus = SD20 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "21" And sRecordExist = "Y" Then
                            SD21 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("21").ToString()
                        Else
                            SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D21").ToString()
                        End If
                        CalculateES() : sDayStatus = SD21 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "22" And sRecordExist = "Y" Then
                            SD22 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("22").ToString()
                        Else
                            SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D22").ToString()
                        End If
                        CalculateES() : sDayStatus = SD22 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "23" And sRecordExist = "Y" Then
                            SD23 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("23").ToString()
                        Else
                            SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D23").ToString()
                        End If
                        CalculateES() : sDayStatus = SD23 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "24" And sRecordExist = "Y" Then
                            SD24 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("24").ToString()
                        Else
                            SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D24").ToString()
                        End If
                        CalculateES() : sDayStatus = SD24 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "25" And sRecordExist = "Y" Then
                            SD25 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("25").ToString()
                        Else
                            SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D25").ToString()
                        End If
                        CalculateES() : sDayStatus = SD25 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "26" And sRecordExist = "Y" Then
                            SD26 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("26").ToString()
                        Else
                            SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D26").ToString()
                        End If
                        CalculateES() : sDayStatus = SD26 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "27" And sRecordExist = "Y" Then
                            SD27 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("27").ToString()
                        Else
                            SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D27").ToString()
                        End If
                        CalculateES() : sDayStatus = SD27 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "28" And sRecordExist = "Y" Then
                            SD28 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("28").ToString()
                        Else
                            SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D28").ToString()
                        End If
                        CalculateES() : sDayStatus = SD28 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "29" And sRecordExist = "Y" Then
                            SD29 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("29").ToString()
                        Else
                            SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D29").ToString()
                        End If
                        CalculateES() : sDayStatus = SD29 : UpdateCount()


                        If Format(dpSumaryDate.Value, "dd") = "30" And sRecordExist = "Y" Then
                            SD30 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("30").ToString()
                        Else
                            SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D30").ToString()
                        End If
                        CalculateES() : sDayStatus = SD30 : UpdateCount()

                        If Format(dpSumaryDate.Value, "dd") = "31" And sRecordExist = "Y" Then
                            SD31 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("31").ToString()
                        Else
                            SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D31").ToString()
                        End If
                        CalculateES() : sDayStatus = SD31 : UpdateCount()
                    Else

                        SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                        SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("2").ToString() : CalculateES() : sDayStatus = SD02 : UpdateCount()
                        SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("3").ToString() : CalculateES() : sDayStatus = SD03 : UpdateCount()
                        SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("4").ToString() : CalculateES() : sDayStatus = SD04 : UpdateCount()
                        SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("5").ToString() : CalculateES() : sDayStatus = SD05 : UpdateCount()
                        SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("6").ToString() : CalculateES() : sDayStatus = SD06 : UpdateCount()
                        SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("7").ToString() : CalculateES() : sDayStatus = SD07 : UpdateCount()
                        SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("8").ToString() : CalculateES() : sDayStatus = SD08 : UpdateCount()
                        SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("9").ToString() : CalculateES() : sDayStatus = SD09 : UpdateCount()
                        SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("10").ToString() : CalculateES() : sDayStatus = SD10 : UpdateCount()

                        SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("11").ToString() : CalculateES() : sDayStatus = SD11 : UpdateCount()
                        SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("12").ToString() : CalculateES() : sDayStatus = SD12 : UpdateCount()
                        SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("13").ToString() : CalculateES() : sDayStatus = SD13 : UpdateCount()
                        SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("14").ToString() : CalculateES() : sDayStatus = SD14 : UpdateCount()
                        SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("15").ToString() : CalculateES() : sDayStatus = SD15 : UpdateCount()
                        SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("16").ToString() : CalculateES() : sDayStatus = SD16 : UpdateCount()
                        SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("17").ToString() : CalculateES() : sDayStatus = SD17 : UpdateCount()
                        SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("18").ToString() : CalculateES() : sDayStatus = SD18 : UpdateCount()
                        SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("19").ToString() : CalculateES() : sDayStatus = SD19 : UpdateCount()
                        SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("20").ToString() : CalculateES() : sDayStatus = SD20 : UpdateCount()

                        SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("21").ToString() : CalculateES() : sDayStatus = SD21 : UpdateCount()
                        SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("22").ToString() : CalculateES() : sDayStatus = SD22 : UpdateCount()
                        SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("23").ToString() : CalculateES() : sDayStatus = SD23 : UpdateCount()
                        SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString() : CalculateES() : sDayStatus = SD24 : UpdateCount()
                        SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString() : CalculateES() : sDayStatus = SD25 : UpdateCount()
                        SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString() : CalculateES() : sDayStatus = SD26 : UpdateCount()
                        SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString() : CalculateES() : sDayStatus = SD27 : UpdateCount()
                        SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString() : CalculateES() : sDayStatus = SD28 : UpdateCount()

                        If nLastDay > 28 Then
                            SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString() : CalculateES() : sDayStatus = SD29 : UpdateCount()
                            If nLastDay = 30 Then
                                SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : sDayStatus = SD30 : UpdateCount()
                            Else
                                SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : sDayStatus = SD30 : UpdateCount()
                                SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString() : CalculateES() : sDayStatus = SD31 : UpdateCount()
                            End If
                        End If
                    End If
                ElseIf nWeekWorkingDays = 5 Then


                    If chkbxSingleDay.Checked = True Then

                        'SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                        If Format(dpSumaryDate.Value, "dd") = "01" And sRecordExist = "Y" Then
                            SD01 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("1").ToString()
                        Else
                            SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D01").ToString()
                        End If
                        sDayStatus = SD01 : UpdateCount() : CalculateES()
                        nDayNumber += 1
                        If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If

                        If Format(dpSumaryDate.Value, "dd") = "02" And sRecordExist = "Y" Then
                            SD02 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("2").ToString()
                        Else
                            SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D02").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD02 : UpdateCount()
                        Else
                            sDayStatus = SD02 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "03" And sRecordExist = "Y" Then
                            SD03 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("3").ToString()
                        Else
                            SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D03").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD03 : UpdateCount()
                        Else
                            sDayStatus = SD03 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "04" And sRecordExist = "Y" Then
                            SD04 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("4").ToString()
                        Else
                            SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D04").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD04 : UpdateCount()
                        Else
                            sDayStatus = SD04 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "05" And sRecordExist = "Y" Then
                            SD05 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("5").ToString()
                        Else
                            SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D05").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD05 : UpdateCount()
                        Else
                            sDayStatus = SD05 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "06" And sRecordExist = "Y" Then
                            SD06 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("6").ToString()
                        Else
                            SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D06").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD06 : UpdateCount()
                        Else
                            sDayStatus = SD06 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "07" And sRecordExist = "Y" Then
                            SD07 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("7").ToString()
                        Else
                            SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D07").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD07 : UpdateCount()
                        Else
                            sDayStatus = SD07 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "08" And sRecordExist = "Y" Then
                            SD08 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("8").ToString()
                        Else
                            SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D08").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD08 : UpdateCount()
                        Else
                            sDayStatus = SD08 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "09" And sRecordExist = "Y" Then
                            SD09 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("9").ToString()
                        Else
                            SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D09").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD09 : UpdateCount()
                        Else
                            sDayStatus = SD09 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If



                        If Format(dpSumaryDate.Value, "dd") = "10" And sRecordExist = "Y" Then
                            SD10 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("10").ToString()
                        Else
                            SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D10").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD10 : UpdateCount()
                        Else
                            sDayStatus = SD10 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "11" And sRecordExist = "Y" Then
                            SD11 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("11").ToString()
                        Else
                            SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D11").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD11 : UpdateCount()
                        Else
                            sDayStatus = SD11 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "12" And sRecordExist = "Y" Then
                            SD12 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("12").ToString()
                        Else
                            SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D12").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD12 : UpdateCount()
                        Else
                            sDayStatus = SD12 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "13" And sRecordExist = "Y" Then
                            SD13 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("13").ToString()
                        Else
                            SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D13").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD13 : UpdateCount()
                        Else
                            sDayStatus = SD13 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "14" And sRecordExist = "Y" Then
                            SD14 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("14").ToString()
                        Else
                            SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D14").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD14 : UpdateCount()
                        Else
                            sDayStatus = SD14 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "15" And sRecordExist = "Y" Then
                            SD15 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("15").ToString()
                        Else
                            SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D15").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD15 : UpdateCount()
                        Else
                            sDayStatus = SD15 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "16" And sRecordExist = "Y" Then
                            SD16 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("16").ToString()
                        Else
                            SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D16").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD16 : UpdateCount()
                        Else
                            sDayStatus = SD16 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "17" And sRecordExist = "Y" Then
                            SD17 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("17").ToString()
                        Else
                            SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D17").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD17 : UpdateCount()
                        Else
                            sDayStatus = SD17 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "18" And sRecordExist = "Y" Then
                            SD18 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("18").ToString()
                        Else
                            SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D18").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD18 : UpdateCount()
                        Else
                            sDayStatus = SD18 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "19" And sRecordExist = "Y" Then
                            SD19 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("19").ToString()
                        Else
                            SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D19").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD19 : UpdateCount()
                        Else
                            sDayStatus = SD19 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "20" And sRecordExist = "Y" Then
                            SD20 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("20").ToString()
                        Else
                            SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D20").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD20 : UpdateCount()
                        Else
                            sDayStatus = SD20 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "21" And sRecordExist = "Y" Then
                            SD21 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("21").ToString()
                        Else
                            SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D21").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD21 : UpdateCount()
                        Else
                            sDayStatus = SD21 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "22" And sRecordExist = "Y" Then
                            SD22 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("22").ToString()
                        Else
                            SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D22").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD22 : UpdateCount()
                        Else
                            sDayStatus = SD22 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "23" And sRecordExist = "Y" Then
                            SD23 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("23").ToString()
                        Else
                            SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D23").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD23 : UpdateCount()
                        Else
                            sDayStatus = SD23 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "24" And sRecordExist = "Y" Then
                            SD24 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("24").ToString()
                        Else
                            SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D24").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD24 : UpdateCount()
                        Else
                            sDayStatus = SD24 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "25" And sRecordExist = "Y" Then
                            SD25 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("25").ToString()
                        Else
                            SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D25").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD25 : UpdateCount()
                        Else
                            sDayStatus = SD25 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "26" And sRecordExist = "Y" Then
                            SD26 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("26").ToString()
                        Else
                            SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D26").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD26 : UpdateCount()
                        Else
                            sDayStatus = SD26 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "27" And sRecordExist = "Y" Then
                            SD27 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("27").ToString()
                        Else
                            SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D27").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD27 : UpdateCount()
                        Else
                            sDayStatus = SD27 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "28" And sRecordExist = "Y" Then
                            SD28 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("28").ToString()
                        Else
                            SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D28").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD28 : UpdateCount()
                        Else
                            sDayStatus = SD28 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "29" And sRecordExist = "Y" Then
                            SD29 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("29").ToString()
                        Else
                            SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D29").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD29 : UpdateCount()
                        Else
                            sDayStatus = SD29 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If


                        If Format(dpSumaryDate.Value, "dd") = "30" And sRecordExist = "Y" Then
                            SD30 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("30").ToString()
                        Else
                            SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D30").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD30 : UpdateCount()
                        Else
                            sDayStatus = SD30 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                        End If

                        If Format(dpSumaryDate.Value, "dd") = "31" And sRecordExist = "Y" Then
                            SD31 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("31").ToString()
                        Else
                            SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D31").ToString()
                        End If
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                            nDayNumber += 1 : sDayStatus = SD31 : UpdateCount()
                        Else
                            sDayStatus = SD31 : UpdateCount()
                        End If
                    Else

                        SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                        If DateAdd(DateInterval.Day, 1, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("2").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD02 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 2, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("3").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD03 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 3, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("4").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD04 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 4, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("5").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD05 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 5, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("6").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD06 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 6, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("7").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD07 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 7, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("8").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD08 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 8, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("9").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD09 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 9, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("10").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD10 : UpdateCount() : End If : Else : nDayNumber += 1 : End If

                        If DateAdd(DateInterval.Day, 10, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("11").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD11 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 11, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("12").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD12 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 12, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("13").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD13 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 13, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("14").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD14 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 14, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("15").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD15 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 15, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("16").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD16 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 16, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("17").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD17 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 17, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("18").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD18 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 18, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("19").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD19 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 19, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("20").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD20 : UpdateCount() : End If : Else : nDayNumber += 1 : End If

                        If DateAdd(DateInterval.Day, 20, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("21").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD21 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 21, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("22").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD22 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 22, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("23").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD23 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 23, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD24 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 24, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD25 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 25, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD26 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 25, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD27 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                        If DateAdd(DateInterval.Day, 27, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD28 : UpdateCount() : End If : Else : nDayNumber += 1 : End If

                        If nLastDay > 28 Then
                            If DateAdd(DateInterval.Day, 28, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD29 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                            If nLastDay = 30 Then
                                If DateAdd(DateInterval.Day, 29, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD30 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                            Else
                                If DateAdd(DateInterval.Day, 29, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD30 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 30, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD31 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                            End If
                        End If
                    End If
                    'SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD02 : UpdateCount() : sNextDaySunday = "N" : Else : SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("2").ToString() : CalculateES() : sDayStatus = SD02 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD03 : UpdateCount() : sNextDaySunday = "N" : Else : SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("3").ToString() : CalculateES() : sDayStatus = SD03 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD04 : UpdateCount() : sNextDaySunday = "N" : Else : SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("4").ToString() : CalculateES() : sDayStatus = SD04 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD05 : UpdateCount() : sNextDaySunday = "N" : Else : SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("5").ToString() : CalculateES() : sDayStatus = SD05 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD06 : UpdateCount() : sNextDaySunday = "N" : Else : SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("6").ToString() : CalculateES() : sDayStatus = SD06 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD07 : UpdateCount() : sNextDaySunday = "N" : Else : SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("7").ToString() : CalculateES() : sDayStatus = SD07 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD08 : UpdateCount() : sNextDaySunday = "N" : Else : SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("8").ToString() : CalculateES() : sDayStatus = SD08 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD09 : UpdateCount() : sNextDaySunday = "N" : Else : SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("9").ToString() : CalculateES() : sDayStatus = SD09 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD10 : UpdateCount() : sNextDaySunday = "N" : Else : SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("10").ToString() : CalculateES() : sDayStatus = SD10 : UpdateCount() : End If

                    'If sNextDaySunday = "Y" Then : sDayStatus = SD11 : UpdateCount() : sNextDaySunday = "N" : Else : SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("11").ToString() : CalculateES() : sDayStatus = SD11 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD12 : UpdateCount() : sNextDaySunday = "N" : Else : SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("12").ToString() : CalculateES() : sDayStatus = SD12 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD13 : UpdateCount() : sNextDaySunday = "N" : Else : SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("13").ToString() : CalculateES() : sDayStatus = SD13 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD14 : UpdateCount() : sNextDaySunday = "N" : Else : SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("14").ToString() : CalculateES() : sDayStatus = SD14 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD15 : UpdateCount() : sNextDaySunday = "N" : Else : SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("15").ToString() : CalculateES() : sDayStatus = SD15 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD16 : UpdateCount() : sNextDaySunday = "N" : Else : SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("16").ToString() : CalculateES() : sDayStatus = SD16 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD17 : UpdateCount() : sNextDaySunday = "N" : Else : SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("17").ToString() : CalculateES() : sDayStatus = SD17 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD18 : UpdateCount() : sNextDaySunday = "N" : Else : SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("18").ToString() : CalculateES() : sDayStatus = SD18 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD19 : UpdateCount() : sNextDaySunday = "N" : Else : SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("19").ToString() : CalculateES() : sDayStatus = SD19 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD20 : UpdateCount() : sNextDaySunday = "N" : Else : SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("20").ToString() : CalculateES() : sDayStatus = SD20 : UpdateCount() : End If

                    'If sNextDaySunday = "Y" Then : sDayStatus = SD21 : UpdateCount() : sNextDaySunday = "N" : Else : SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("21").ToString() : CalculateES() : sDayStatus = SD21 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD22 : UpdateCount() : sNextDaySunday = "N" : Else : SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("22").ToString() : CalculateES() : sDayStatus = SD22 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD23 : UpdateCount() : sNextDaySunday = "N" : Else : SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("23").ToString() : CalculateES() : sDayStatus = SD23 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD24 : UpdateCount() : sNextDaySunday = "N" : Else : SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString() : CalculateES() : sDayStatus = SD24 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD25 : UpdateCount() : sNextDaySunday = "N" : Else : SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString() : CalculateES() : sDayStatus = SD25 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD26 : UpdateCount() : sNextDaySunday = "N" : Else : SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString() : CalculateES() : sDayStatus = SD26 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD27 : UpdateCount() : sNextDaySunday = "N" : Else : SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString() : CalculateES() : sDayStatus = SD27 : UpdateCount() : End If
                    'If sNextDaySunday = "Y" Then : sDayStatus = SD28 : UpdateCount() : sNextDaySunday = "N" : Else : SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString() : CalculateES() : sDayStatus = SD28 : UpdateCount() : End If

                    'If nLastDay > 28 Then
                    '    If sNextDaySunday = "Y" Then : sDayStatus = SD29 : UpdateCount() : sNextDaySunday = "N" : Else : SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString() : CalculateES() : sDayStatus = SD29 : UpdateCount() : End If
                    '    If nLastDay = 30 Then
                    '        If sNextDaySunday = "Y" Then : sDayStatus = SD30 : UpdateCount() : sNextDaySunday = "N" : Else : SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : sDayStatus = SD30 : UpdateCount() : End If
                    '    Else
                    '        If sNextDaySunday = "Y" Then : sDayStatus = SD30 : UpdateCount() : sNextDaySunday = "N" : Else : SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : sDayStatus = SD30 : UpdateCount() : End If
                    '        If sNextDaySunday = "Y" Then : sDayStatus = SD31 : UpdateCount() : sNextDaySunday = "N" : Else : SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString() : CalculateES() : sDayStatus = SD31 : UpdateCount() : End If
                    '    End If
                    'End If
                End If
            Else
                SD01 = "AA" : sDayStatus = SD01 : UpdateCount()
                SD02 = "AA" : sDayStatus = SD02 : UpdateCount()
                SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                SD10 = "AA" : sDayStatus = SD10 : UpdateCount()

                SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                SD20 = "AA" : sDayStatus = SD20 : UpdateCount()

                SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                SD28 = "AA" : sDayStatus = SD28 : UpdateCount()

                If nLastDay > 28 Then
                    SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                    If nLastDay = 30 Then
                        SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                    Else
                        SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                        SD31 = "AA" : sDayStatus = SD31 : UpdateCount()
                    End If
                End If
            End If
Aa:
            If chkbxSingleDay.Checked = True Then

                Dim daUpdSummaryDtl As New SqlDataAdapter("Update HRAttendanceStatusSummeryDetails Set PresentDays = '" & DLPresentDays & _
                                                          "', Absent = '" & DLAbsent & "', EarnedLeave = '" & DLEarnedLeave & _
                                                          "', CasualLeave = '" & DLCasualLeave & "', EligibleSunday = '" & DLEligibleSunday & _
                                                          "', WeeklyOff = '" & DLWeeklyoff & "', LongLeave = '" & DLLongLeave & _
                                                          "', LayOff = '" & DLLayOff & "', Suspension = '" & DLSuspension & _
                                                          "', SickLeave = '" & DLSickLeave & "', MaternityLeave = '" & DLMaternityLeave & _
                                                          "', ElectionHoliday = '" & DLElectionHoliday & "', NationalHoliday = '" & DLNationalHoliday & _
                                                          "', FestivalHoliday = '" & DLFestivalHoliday & "', OnDuty = '" & DLOnDuty & _
                                                          "', TotalPayableDays = '" & DLTotalPayableDays & "', D01 = '" & SD01 & "', D02 = '" & SD02 & "', D03 = '" & SD03 & _
                                                          "', D04 = '" & SD04 & "', D05 = '" & SD05 & "',  D06 = '" & SD06 & "', D07 = '" & SD07 & _
                                                          "', D08 = '" & SD08 & "', D09 = '" & SD09 & "', D10 = '" & SD10 & "', D11 = '" & SD11 & _
                                                          "', D12 = '" & SD12 & "', D13 = '" & SD13 & "', D14 = '" & SD14 & "', D15 = '" & SD15 & _
                                                          "', D16 = '" & SD16 & "', D17 = '" & SD17 & "', D18 = '" & SD18 & "', D19 = '" & SD19 & _
                                                          "', D20 = '" & SD20 & "', D21 = '" & SD21 & "', D22 = '" & SD22 & "', D23 = '" & SD23 & _
                                                          "', D24 = '" & SD24 & "', D25 = '" & SD25 & "', D26 = '" & SD26 & "', D27 = '" & SD27 & _
                                                          "', D28 = '" & SD28 & "', D29 = '" & SD29 & "', D30 = '" & SD30 & "', D31 = '" & SD31 & _
                                                          "' Where ID = '" & sSummaryDtlID & "'", sConHR)
                Dim dsUpdSummaryDtl As New DataSet
                daUpdSummaryDtl.Fill(dsUpdSummaryDtl)
                dsUpdSummaryDtl.AcceptChanges()

            Else
                InsertSummaryDtl()
            End If


        Next
        UpdateHolidays()
        MessageBox.Show("Completed")

        LoadAttendanceSummeryDtls(SUnitCode, NAttMonth, NAttYear)
    End Sub
    Dim sDayStatus, sNextDaySunday As String
    Dim nWeekWorkingDays As Integer
    Dim SPMD26, SPMD27, SPMD28, SPMD29, SPMD30, SPMD31 As String

    Private Sub LoadPreviousMonthInfo()
        Dim dLastMonth As Date
        dLastMonth = DateAdd(DateInterval.Day, -1, dFirstDay.Date)

        Dim NPMAttYear, NPMAttMonth As Integer

        NPMAttYear = dLastMonth.Year
        NPMAttMonth = dLastMonth.Month

        Dim daSelEmpAttInfo As New SqlDataAdapter("Select * from vw_AttendanceSummary Where Empcode = '" & SEMPCode & _
                                                  "' And AttYear = '" & NPMAttYear & "' And AttMonth = '" & NPMAttMonth & _
                                                  "' And Unitcode = 'SD'", sConHR)
        Dim dsSelEmpAttInfo As New DataSet
        daSelEmpAttInfo.Fill(dsSelEmpAttInfo)

        If dsSelEmpAttInfo.Tables(0).Rows.Count > 0 Then
            If nWeekWorkingDays = 6 Then
                If dLastMonth.Day = 31 Then
                    SPMD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString
                ElseIf dLastMonth.Day = 30 Then
                    SPMD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString
                ElseIf dLastMonth.Day = 29 Then
                    SPMD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString
                ElseIf dLastMonth.Day = 28 Then
                    SPMD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("23").ToString
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                End If
            ElseIf nWeekWorkingDays = 5 Then
                If dLastMonth.Day = 31 Then
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString
                ElseIf dLastMonth.Day = 30 Then
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString
                ElseIf dLastMonth.Day = 29 Then
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString
                ElseIf dLastMonth.Day = 28 Then
                    SPMD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString
                    SPMD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString
                    SPMD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString
                    SPMD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString
                    SPMD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString
                End If
            End If
        End If
    End Sub

    Private Sub CalculateES()
        Dim nWeeekWorkingDayCount As Decimal = 0
        sNextDaySunday = "N"
        If nWeekWorkingDays = 6 Then
            If nDayNumber = 1 Then
                If dAttenDate.DayOfWeek = DayOfWeek.Sunday And SD01 = "AA" Then
                    If SPMD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD26 = "XA" Or SPMD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD27 = "XA" Or SPMD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD28 = "XA" Or SPMD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD01 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 2 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD02 = "AA" Then
                    If SPMD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD27 = "XA" Or SPMD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD28 = "XA" Or SPMD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD02 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 3 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD03 = "AA" Then
                    If SPMD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD28 = "XA" Or SPMD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD03 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 4 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD04 = "AA" Then
                    If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD04 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 5 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD05 = "AA" Then
                    If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD05 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 6 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD06 = "AA" Then
                    If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD06 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 7 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD07 = "AA" Then
                    If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD07 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 8 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD08 = "AA" Then
                    If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD08 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 9 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD09 = "AA" Then
                    If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD09 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 10 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD10 = "AA" Then
                    If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD10 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 11 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD11 = "AA" Then
                    If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD11 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 12 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD12 = "AA" Then
                    If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD12 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 13 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD13 = "AA" Then
                    If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD13 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 14 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD14 = "AA" Then
                    If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD14 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 15 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD15 = "AA" Then
                    If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD15 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 16 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD16 = "AA" Then
                    If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD16 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 17 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD17 = "AA" Then
                    If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD17 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 18 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD18 = "AA" Then
                    If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD18 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 19 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD19 = "AA" Then
                    If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD19 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 20 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD20 = "AA" Then
                    If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD20 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 21 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD21 = "AA" Then
                    If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD21 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 22 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD22 = "AA" Then
                    If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD22 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 23 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD23 = "AA" Then
                    If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD23 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 24 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD24 = "AA" Then
                    If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD24 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 25 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD25 = "AA" Then
                    If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD25 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 26 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD26 = "AA" Then
                    If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD26 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 27 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD27 = "AA" Then
                    If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD27 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 28 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD28 = "AA" Then
                    If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD28 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 29 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD29 = "AA" Then
                    If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD29 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 30 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD30 = "AA" Then
                    If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD29 = "XA" Or SD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD30 = "ES"
                    End If
                End If
                nDayNumber += 1
            ElseIf nDayNumber = 31 Then
                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday And SD31 = "AA" Then
                    If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD29 = "XA" Or SD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If SD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD30 = "XA" Or SD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                    If nWeeekWorkingDayCount >= nESEligibleDays Then
                        SD31 = "ES"
                    End If
                End If
                nDayNumber = 1
            End If
        ElseIf nWeekWorkingDays = 5 Then
            If chkbxSingleDay.Checked = True Then
                'If dpSumaryDate.Value.DayOfWeek = DayOfWeek.Friday And dpSumaryDate.Value = DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate) Then
                If dpSumaryDate.Value.DayOfWeek = DayOfWeek.Friday Then
                    If nDayNumber = 1 Then
                        If dAttenDate.DayOfWeek = DayOfWeek.Friday Then ''And SD01 = "AA" Then
                            If SPMD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD28 = "XA" Or SPMD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD02 = "AA" : sDayStatus = SD02 : UpdateCount()
                                SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD02 = "XA" : sDayStatus = SD02 : UpdateCount()
                                SD03 = "ES" : sDayStatus = SD03 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD02 = "XX" : sDayStatus = SD02 : UpdateCount()
                                SD03 = "ES" : sDayStatus = SD03 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 2 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD02 = "AA" Then
                            If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                                SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD03 = "XA" : sDayStatus = SD03 : UpdateCount()
                                SD04 = "ES" : sDayStatus = SD04 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD03 = "XX" : sDayStatus = SD03 : UpdateCount()
                                SD04 = "ES" : sDayStatus = SD04 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 3 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD03 = "AA" Then
                            If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                                SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD04 = "XA" : sDayStatus = SD04 : UpdateCount()
                                SD05 = "ES" : sDayStatus = SD05 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD04 = "XX" : sDayStatus = SD04 : UpdateCount()
                                SD05 = "ES" : sDayStatus = SD05 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 4 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD04 = "AA" Then
                            If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                                SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD05 = "XA" : sDayStatus = SD05 : UpdateCount()
                                SD06 = "ES" : sDayStatus = SD06 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD05 = "XX" : sDayStatus = SD05 : UpdateCount()
                                SD06 = "ES" : sDayStatus = SD06 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 5 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD05 = "AA" Then
                            If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                                SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD06 = "XA" : sDayStatus = SD06 : UpdateCount()
                                SD07 = "ES" : sDayStatus = SD07 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD06 = "XX" : sDayStatus = SD06 : UpdateCount()
                                SD07 = "ES" : sDayStatus = SD07 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 6 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD06 = "AA" Then
                            If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD07 = "XA" : sDayStatus = SD07 : UpdateCount()
                                SD08 = "ES" : sDayStatus = SD08 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD07 = "XX" : sDayStatus = SD07 : UpdateCount()
                                SD08 = "ES" : sDayStatus = SD08 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 7 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD07 = "AA" Then
                            If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD08 = "XA" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD08 = "XX" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 8 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD08 = "AA" Then
                            If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD08 = "XA" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD08 = "XX" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 9 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD09 = "AA" Then
                            If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD10 = "AA" : sDayStatus = SD10 : UpdateCount()
                                SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD10 = "XA" : sDayStatus = SD10 : UpdateCount()
                                SD11 = "ES" : sDayStatus = SD11 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD10 = "XX" : sDayStatus = SD10 : UpdateCount()
                                SD11 = "ES" : sDayStatus = SD11 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 10 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD10 = "AA" Then
                            If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                                SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD11 = "XA" : sDayStatus = SD11 : UpdateCount()
                                SD12 = "ES" : sDayStatus = SD12 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD11 = "XX" : sDayStatus = SD11 : UpdateCount()
                                SD12 = "ES" : sDayStatus = SD12 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 11 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD11 = "AA" Then
                            If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                                SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD12 = "XA" : sDayStatus = SD12 : UpdateCount()
                                SD13 = "ES" : sDayStatus = SD13 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD12 = "XX" : sDayStatus = SD12 : UpdateCount()
                                SD13 = "ES" : sDayStatus = SD13 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 12 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD12 = "AA" Then
                            If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                                SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD13 = "XA" : sDayStatus = SD13 : UpdateCount()
                                SD14 = "ES" : sDayStatus = SD14 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD13 = "XX" : sDayStatus = SD13 : UpdateCount()
                                SD14 = "ES" : sDayStatus = SD14 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 13 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD13 = "AA" Then
                            If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                                SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD14 = "XA" : sDayStatus = SD14 : UpdateCount()
                                SD15 = "ES" : sDayStatus = SD15 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD14 = "XX" : sDayStatus = SD14 : UpdateCount()
                                SD15 = "ES" : sDayStatus = SD15 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 14 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD14 = "AA" Then
                            If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                                SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD15 = "XA" : sDayStatus = SD15 : UpdateCount()
                                SD16 = "ES" : sDayStatus = SD16 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD15 = "XX" : sDayStatus = SD15 : UpdateCount()
                                SD16 = "ES" : sDayStatus = SD16 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 15 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD15 = "AA" Then
                            If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                                SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD16 = "XA" : sDayStatus = SD16 : UpdateCount()
                                SD17 = "ES" : sDayStatus = SD17 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD16 = "XX" : sDayStatus = SD16 : UpdateCount()
                                SD17 = "ES" : sDayStatus = SD17 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 16 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD16 = "AA" Then
                            If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                                SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD17 = "XA" : sDayStatus = SD17 : UpdateCount()
                                SD18 = "ES" : sDayStatus = SD18 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD17 = "XX" : sDayStatus = SD17 : UpdateCount()
                                SD18 = "ES" : sDayStatus = SD18 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 17 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD17 = "AA" Then

                            If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                                SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD18 = "XA" : sDayStatus = SD18 : UpdateCount()
                                SD19 = "ES" : sDayStatus = SD19 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD18 = "XX" : sDayStatus = SD18 : UpdateCount()
                                SD19 = "ES" : sDayStatus = SD19 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 18 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD18 = "AA" Then
                            If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                                SD20 = "AA" : sDayStatus = SD20 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD19 = "XA" : sDayStatus = SD19 : UpdateCount()
                                SD20 = "ES" : sDayStatus = SD20 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD19 = "XX" : sDayStatus = SD19 : UpdateCount()
                                SD20 = "ES" : sDayStatus = SD20 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 19 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD19 = "AA" Then
                            If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD20 = "AA" : sDayStatus = SD20 : UpdateCount()
                                SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD20 = "XA" : sDayStatus = SD20 : UpdateCount()
                                SD21 = "ES" : sDayStatus = SD21 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD20 = "XX" : sDayStatus = SD20 : UpdateCount()
                                SD21 = "ES" : sDayStatus = SD21 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 20 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD20 = "AA" Then
                            If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                                SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD21 = "XA" : sDayStatus = SD21 : UpdateCount()
                                SD22 = "ES" : sDayStatus = SD22 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD21 = "XX" : sDayStatus = SD21 : UpdateCount()
                                SD22 = "ES" : sDayStatus = SD22 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 21 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD21 = "AA" Then
                            If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                                SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD22 = "XA" : sDayStatus = SD22 : UpdateCount()
                                SD23 = "ES" : sDayStatus = SD23 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD22 = "XX" : sDayStatus = SD22 : UpdateCount()
                                SD23 = "ES" : sDayStatus = SD23 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 22 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD22 = "AA" Then
                            If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                                SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD23 = "XA" : sDayStatus = SD23 : UpdateCount()
                                SD24 = "ES" : sDayStatus = SD24 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD23 = "XX" : sDayStatus = SD23 : UpdateCount()
                                SD24 = "ES" : sDayStatus = SD24 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 23 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD23 = "AA" Then
                            If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD23 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                                SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD24 = "XA" : sDayStatus = SD24 : UpdateCount()
                                SD25 = "ES" : sDayStatus = SD25 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD24 = "XX" : sDayStatus = SD24 : UpdateCount()
                                SD25 = "ES" : sDayStatus = SD25 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 24 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD24 = "AA" Then
                            If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD23 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                                SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD25 = "XA" : sDayStatus = SD25 : UpdateCount()
                                SD26 = "ES" : sDayStatus = SD26 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD25 = "XX" : sDayStatus = SD25 : UpdateCount()
                                SD26 = "ES" : sDayStatus = SD26 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 25 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD25 = "AA" Then
                            If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                                SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD26 = "XA" : sDayStatus = SD26 : UpdateCount()
                                SD27 = "ES" : sDayStatus = SD27 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD26 = "XX" : sDayStatus = SD26 : UpdateCount()
                                SD27 = "ES" : sDayStatus = SD27 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 26 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD26 = "AA" Then
                            If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                                SD28 = "AA" : sDayStatus = SD28 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD27 = "XA" : sDayStatus = SD27 : UpdateCount()
                                SD28 = "ES" : sDayStatus = SD28 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD27 = "XX" : sDayStatus = SD27 : UpdateCount()
                                SD28 = "ES" : sDayStatus = SD28 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 27 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD27 = "AA" Then
                            If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD28 = "AA" : sDayStatus = SD28 : UpdateCount()
                                SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD28 = "XA" : sDayStatus = SD28 : UpdateCount()
                                SD29 = "ES" : sDayStatus = SD29 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD28 = "XX" : sDayStatus = SD28 : UpdateCount()
                                SD29 = "ES" : sDayStatus = SD29 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 28 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD28 = "AA" Then
                            If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD29 = "XA" : sDayStatus = SD29 : UpdateCount()
                                SD30 = "ES" : sDayStatus = SD30 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD29 = "XX" : sDayStatus = SD29 : UpdateCount()
                                SD30 = "ES" : sDayStatus = SD30 : UpdateCount()
                            End If
                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 29 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD29 = "AA" Then
                            If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD29 = "XA" Or SD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "AA" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD30 = "XA" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "ES" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD30 = "XX" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "ES" : sDayStatus = SD31 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 30 Then
                        If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Friday Then ''And SD30 = "AA" Then
                            If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD29 = "XA" Or SD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If SD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD30 = "XA" Or SD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                            If nWeeekWorkingDayCount < 2 Then
                                SD31 = "AA" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD31 = "XA" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD31 = "XX" : sDayStatus = SD31 : UpdateCount()
                            End If

                            sNextDaySunday = "Y"
                        End If
                    ElseIf nDayNumber = 31 Then
                        sNextDaySunday = "Y"
                    End If
                    'nDayNumber = 1
                End If

            Else

                If nDayNumber = 1 Then
                    If dAttenDate.DayOfWeek = DayOfWeek.Saturday Then ''And SD01 = "AA" Then

                        If SPMD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD27 = "XA" Or SPMD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD28 = "XA" Or SPMD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD01 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD01 = "AA" : sDayStatus = SD01 : UpdateCount()
                                SD02 = "AA" : sDayStatus = SD02 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD01 = "XA" : sDayStatus = SD01 : UpdateCount()
                                SD02 = "ES" : sDayStatus = SD02 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD01 = "XX" : sDayStatus = SD01 : UpdateCount()
                                SD02 = "ES" : sDayStatus = SD02 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD01 : UpdateCount()
                                SD02 = "AA" : sDayStatus = SD02 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD01 : UpdateCount()
                                SD02 = "ES" : sDayStatus = SD02 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD01 : UpdateCount()
                                SD02 = "ES" : sDayStatus = SD02 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 2 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD02 = "AA" Then
                        If SPMD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD28 = "XA" Or SPMD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD02 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD02 = "AA" : sDayStatus = SD02 : UpdateCount()
                                SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD02 = "XA" : sDayStatus = SD02 : UpdateCount()
                                SD03 = "ES" : sDayStatus = SD03 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD02 = "XX" : sDayStatus = SD02 : UpdateCount()
                                SD03 = "ES" : sDayStatus = SD03 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD02 : UpdateCount()
                                SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD02 : UpdateCount()
                                SD03 = "ES" : sDayStatus = SD03 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD02 : UpdateCount()
                                SD03 = "ES" : sDayStatus = SD03 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 3 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD03 = "AA" Then
                        If SPMD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD29 = "XA" Or SPMD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD03 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                                SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD03 = "XA" : sDayStatus = SD03 : UpdateCount()
                                SD04 = "ES" : sDayStatus = SD04 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD03 = "XX" : sDayStatus = SD03 : UpdateCount()
                                SD04 = "ES" : sDayStatus = SD04 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD03 : UpdateCount()
                                SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD03 : UpdateCount()
                                SD04 = "ES" : sDayStatus = SD04 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD03 : UpdateCount()
                                SD04 = "ES" : sDayStatus = SD04 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 4 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD04 = "AA" Then
                        If SPMD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD30 = "XA" Or SPMD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD04 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                                SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD04 = "XA" : sDayStatus = SD04 : UpdateCount()
                                SD05 = "ES" : sDayStatus = SD05 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD04 = "XX" : sDayStatus = SD04 : UpdateCount()
                                SD05 = "ES" : sDayStatus = SD05 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD04 : UpdateCount()
                                SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD04 : UpdateCount()
                                SD05 = "ES" : sDayStatus = SD05 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD04 : UpdateCount()
                                SD05 = "ES" : sDayStatus = SD05 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 5 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD05 = "AA" Then
                        If SPMD31 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SPMD31 = "XA" Or SPMD31 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD05 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                                SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD05 = "XA" : sDayStatus = SD05 : UpdateCount()
                                SD06 = "ES" : sDayStatus = SD06 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD05 = "XX" : sDayStatus = SD05 : UpdateCount()
                                SD06 = "ES" : sDayStatus = SD06 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD05 : UpdateCount()
                                SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD05 : UpdateCount()
                                SD06 = "ES" : sDayStatus = SD06 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD05 : UpdateCount()
                                SD06 = "ES" : sDayStatus = SD06 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 6 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD06 = "AA" Then
                        ''If SD31 = "XX" Then : nWeeekWorkingDayCount += 1 : End If
                        If SD01 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD01 = "XA" Or SD01 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD06 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                                SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD06 = "XA" : sDayStatus = SD06 : UpdateCount()
                                SD07 = "ES" : sDayStatus = SD07 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD06 = "XX" : sDayStatus = SD06 : UpdateCount()
                                SD07 = "ES" : sDayStatus = SD07 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD06 : UpdateCount()
                                SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD06 : UpdateCount()
                                SD07 = "ES" : sDayStatus = SD07 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD06 : UpdateCount()
                                SD07 = "ES" : sDayStatus = SD07 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 7 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD07 = "AA" Then
                        If SD02 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD02 = "XA" Or SD02 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD07 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD07 = "XA" : sDayStatus = SD07 : UpdateCount()
                                SD08 = "ES" : sDayStatus = SD08 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD07 = "XX" : sDayStatus = SD07 : UpdateCount()
                                SD08 = "ES" : sDayStatus = SD08 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD07 : UpdateCount()
                                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD07 : UpdateCount()
                                SD08 = "ES" : sDayStatus = SD08 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD07 : UpdateCount()
                                SD08 = "ES" : sDayStatus = SD08 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 8 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD08 = "AA" Then
                        If SD03 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD03 = "XA" Or SD03 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD08 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD08 = "XA" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD08 = "XX" : sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD08 : UpdateCount()
                                SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD08 : UpdateCount()
                                SD09 = "ES" : sDayStatus = SD09 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 9 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD09 = "AA" Then
                        If SD04 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD04 = "XA" Or SD04 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD09 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                                SD10 = "AA" : sDayStatus = SD10 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD09 = "XA" : sDayStatus = SD09 : UpdateCount()
                                SD10 = "ES" : sDayStatus = SD10 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD09 = "XX" : sDayStatus = SD09 : UpdateCount()
                                SD10 = "ES" : sDayStatus = SD10 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD09 : UpdateCount()
                                SD10 = "AA" : sDayStatus = SD10 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD09 : UpdateCount()
                                SD10 = "ES" : sDayStatus = SD10 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD09 : UpdateCount()
                                SD10 = "ES" : sDayStatus = SD10 : UpdateCount()
                            End If
                        End If

                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 10 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD10 = "AA" Then
                        If SD05 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD05 = "XA" Or SD05 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD10 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD10 = "AA" : sDayStatus = SD10 : UpdateCount()
                                SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD10 = "XA" : sDayStatus = SD10 : UpdateCount()
                                SD11 = "ES" : sDayStatus = SD11 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD10 = "XX" : sDayStatus = SD10 : UpdateCount()
                                SD11 = "ES" : sDayStatus = SD11 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD10 : UpdateCount()
                                SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD10 : UpdateCount()
                                SD11 = "ES" : sDayStatus = SD11 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD10 : UpdateCount()
                                SD11 = "ES" : sDayStatus = SD11 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 11 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD11 = "AA" Then
                        If SD06 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD06 = "XA" Or SD06 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD11 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                                SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD11 = "XA" : sDayStatus = SD11 : UpdateCount()
                                SD12 = "ES" : sDayStatus = SD12 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD11 = "XX" : sDayStatus = SD11 : UpdateCount()
                                SD12 = "ES" : sDayStatus = SD12 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD11 : UpdateCount()
                                SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD11 : UpdateCount()
                                SD12 = "ES" : sDayStatus = SD12 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD11 : UpdateCount()
                                SD12 = "ES" : sDayStatus = SD12 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 12 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD12 = "AA" Then
                        If SD07 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD07 = "XA" Or SD07 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD12 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                                SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD12 = "XA" : sDayStatus = SD12 : UpdateCount()
                                SD13 = "ES" : sDayStatus = SD13 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD12 = "XX" : sDayStatus = SD12 : UpdateCount()
                                SD13 = "ES" : sDayStatus = SD13 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD12 : UpdateCount()
                                SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD12 : UpdateCount()
                                SD13 = "ES" : sDayStatus = SD13 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD12 : UpdateCount()
                                SD13 = "ES" : sDayStatus = SD13 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 13 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD13 = "AA" Then
                        If SD08 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD08 = "XA" Or SD08 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD13 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                                SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD13 = "XA" : sDayStatus = SD13 : UpdateCount()
                                SD14 = "ES" : sDayStatus = SD14 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD13 = "XX" : sDayStatus = SD13 : UpdateCount()
                                SD14 = "ES" : sDayStatus = SD14 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD13 : UpdateCount()
                                SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD13 : UpdateCount()
                                SD14 = "ES" : sDayStatus = SD14 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD13 : UpdateCount()
                                SD14 = "ES" : sDayStatus = SD14 : UpdateCount()
                            End If
                        End If

                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 14 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD14 = "AA" Then
                        If SD09 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD09 = "XA" Or SD09 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD14 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                                SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD14 = "XA" : sDayStatus = SD14 : UpdateCount()
                                SD15 = "ES" : sDayStatus = SD15 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD14 = "XX" : sDayStatus = SD14 : UpdateCount()
                                SD15 = "ES" : sDayStatus = SD15 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD14 : UpdateCount()
                                SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD14 : UpdateCount()
                                SD15 = "ES" : sDayStatus = SD15 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD14 : UpdateCount()
                                SD15 = "ES" : sDayStatus = SD15 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 15 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD15 = "AA" Then
                        If SD10 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD10 = "XA" Or SD10 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD15 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                                SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD15 = "XA" : sDayStatus = SD15 : UpdateCount()
                                SD16 = "ES" : sDayStatus = SD16 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD15 = "XX" : sDayStatus = SD15 : UpdateCount()
                                SD16 = "ES" : sDayStatus = SD16 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD15 : UpdateCount()
                                SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD15 : UpdateCount()
                                SD16 = "ES" : sDayStatus = SD16 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD15 : UpdateCount()
                                SD16 = "ES" : sDayStatus = SD16 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 16 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD16 = "AA" Then
                        If SD11 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD11 = "XA" Or SD11 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD16 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                                SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD16 = "XA" : sDayStatus = SD16 : UpdateCount()
                                SD17 = "ES" : sDayStatus = SD17 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD16 = "XX" : sDayStatus = SD16 : UpdateCount()
                                SD17 = "ES" : sDayStatus = SD17 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD16 : UpdateCount()
                                SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD16 : UpdateCount()
                                SD17 = "ES" : sDayStatus = SD17 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD16 : UpdateCount()
                                SD17 = "ES" : sDayStatus = SD17 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 17 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD17 = "AA" Then
                        If SD12 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD12 = "XA" Or SD12 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD17 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                                SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD17 = "XA" : sDayStatus = SD17 : UpdateCount()
                                SD18 = "ES" : sDayStatus = SD18 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD17 = "XX" : sDayStatus = SD17 : UpdateCount()
                                SD18 = "ES" : sDayStatus = SD18 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD17 : UpdateCount()
                                SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD17 : UpdateCount()
                                SD18 = "ES" : sDayStatus = SD18 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD17 : UpdateCount()
                                SD18 = "ES" : sDayStatus = SD18 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 18 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD18 = "AA" Then
                        If SD13 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD13 = "XA" Or SD13 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD18 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                                SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD18 = "XA" : sDayStatus = SD18 : UpdateCount()
                                SD19 = "ES" : sDayStatus = SD19 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD18 = "XX" : sDayStatus = SD18 : UpdateCount()
                                SD19 = "ES" : sDayStatus = SD19 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD18 : UpdateCount()
                                SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD18 : UpdateCount()
                                SD19 = "ES" : sDayStatus = SD19 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD18 : UpdateCount()
                                SD19 = "ES" : sDayStatus = SD19 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 19 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD19 = "AA" Then
                        If SD14 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD14 = "XA" Or SD14 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD19 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                                SD20 = "AA" : sDayStatus = SD20 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD19 = "XA" : sDayStatus = SD19 : UpdateCount()
                                SD20 = "ES" : sDayStatus = SD20 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD19 = "XX" : sDayStatus = SD19 : UpdateCount()
                                SD20 = "ES" : sDayStatus = SD20 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD19 : UpdateCount()
                                SD20 = "AA" : sDayStatus = SD20 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD19 : UpdateCount()
                                SD20 = "ES" : sDayStatus = SD20 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD19 : UpdateCount()
                                SD20 = "ES" : sDayStatus = SD20 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 20 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD20 = "AA" Then
                        If SD15 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD15 = "XA" Or SD15 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD20 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD20 = "AA" : sDayStatus = SD20 : UpdateCount()
                                SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD20 = "XA" : sDayStatus = SD20 : UpdateCount()
                                SD21 = "ES" : sDayStatus = SD21 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD20 = "XX" : sDayStatus = SD20 : UpdateCount()
                                SD21 = "ES" : sDayStatus = SD21 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD20 : UpdateCount()
                                SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD20 : UpdateCount()
                                SD21 = "ES" : sDayStatus = SD21 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD20 : UpdateCount()
                                SD21 = "ES" : sDayStatus = SD21 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 21 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD21 = "AA" Then
                        If SD16 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD16 = "XA" Or SD16 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD21 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                                SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD21 = "XA" : sDayStatus = SD21 : UpdateCount()
                                SD22 = "ES" : sDayStatus = SD22 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD21 = "XX" : sDayStatus = SD21 : UpdateCount()
                                SD22 = "ES" : sDayStatus = SD22 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD21 : UpdateCount()
                                SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD21 : UpdateCount()
                                SD22 = "ES" : sDayStatus = SD22 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD21 : UpdateCount()
                                SD22 = "ES" : sDayStatus = SD22 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 22 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD22 = "AA" Then
                        If SD17 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD17 = "XA" Or SD17 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD22 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                                SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD22 = "XA" : sDayStatus = SD22 : UpdateCount()
                                SD23 = "ES" : sDayStatus = SD23 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD22 = "XX" : sDayStatus = SD22 : UpdateCount()
                                SD23 = "ES" : sDayStatus = SD23 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD22 : UpdateCount()
                                SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD22 : UpdateCount()
                                SD23 = "ES" : sDayStatus = SD23 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD22 : UpdateCount()
                                SD23 = "ES" : sDayStatus = SD23 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 23 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD23 = "AA" Then
                        If SD18 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD18 = "XA" Or SD18 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD23 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                                SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD23 = "XA" : sDayStatus = SD23 : UpdateCount()
                                SD24 = "ES" : sDayStatus = SD24 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD23 = "XX" : sDayStatus = SD23 : UpdateCount()
                                SD24 = "ES" : sDayStatus = SD24 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD23 : UpdateCount()
                                SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD23 : UpdateCount()
                                SD24 = "ES" : sDayStatus = SD24 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD23 : UpdateCount()
                                SD24 = "ES" : sDayStatus = SD24 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 24 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD24 = "AA" Then
                        If SD19 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD19 = "XA" Or SD19 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD23 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD24 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                                SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD24 = "XA" : sDayStatus = SD24 : UpdateCount()
                                SD25 = "ES" : sDayStatus = SD25 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD24 = "XX" : sDayStatus = SD24 : UpdateCount()
                                SD25 = "ES" : sDayStatus = SD25 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD24 : UpdateCount()
                                SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD24 : UpdateCount()
                                SD25 = "ES" : sDayStatus = SD25 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD24 : UpdateCount()
                                SD25 = "ES" : sDayStatus = SD25 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 25 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD25 = "AA" Then
                        If SD20 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD20 = "XA" Or SD20 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD25 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                                SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD25 = "XA" : sDayStatus = SD25 : UpdateCount()
                                SD26 = "ES" : sDayStatus = SD26 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD25 = "XX" : sDayStatus = SD25 : UpdateCount()
                                SD26 = "ES" : sDayStatus = SD26 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD25 : UpdateCount()
                                SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD25 : UpdateCount()
                                SD26 = "ES" : sDayStatus = SD26 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD25 : UpdateCount()
                                SD26 = "ES" : sDayStatus = SD26 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 26 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD26 = "AA" Then
                        If SD21 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD21 = "XA" Or SD21 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD26 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                                SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD26 = "XA" : sDayStatus = SD26 : UpdateCount()
                                SD27 = "ES" : sDayStatus = SD27 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD26 = "XX" : sDayStatus = SD26 : UpdateCount()
                                SD27 = "ES" : sDayStatus = SD27 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD26 : UpdateCount()
                                SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD26 : UpdateCount()
                                SD27 = "ES" : sDayStatus = SD27 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD26 : UpdateCount()
                                SD27 = "ES" : sDayStatus = SD27 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 27 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD27 = "AA" Then
                        If SD22 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD22 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD27 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                                SD28 = "AA" : sDayStatus = SD28 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD27 = "XA" : sDayStatus = SD27 : UpdateCount()
                                SD28 = "ES" : sDayStatus = SD28 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD27 = "XX" : sDayStatus = SD27 : UpdateCount()
                                SD28 = "ES" : sDayStatus = SD28 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD27 : UpdateCount()
                                SD28 = "AA" : sDayStatus = SD28 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD27 : UpdateCount()
                                SD28 = "ES" : sDayStatus = SD28 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD27 : UpdateCount()
                                SD28 = "ES" : sDayStatus = SD28 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 28 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD28 = "AA" Then
                        If SD23 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD23 = "XA" Or SD22 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD28 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD28 = "AA" : sDayStatus = SD28 : UpdateCount()
                                SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD28 = "XA" : sDayStatus = SD28 : UpdateCount()
                                SD29 = "ES" : sDayStatus = SD29 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD28 = "XX" : sDayStatus = SD28 : UpdateCount()
                                SD29 = "ES" : sDayStatus = SD29 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD28 : UpdateCount()
                                SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD28 : UpdateCount()
                                SD29 = "ES" : sDayStatus = SD29 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD28 : UpdateCount()
                                SD29 = "ES" : sDayStatus = SD29 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 29 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD29 = "AA" Then
                        If SD24 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD24 = "XA" Or SD24 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD29 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD29 = "XA" : sDayStatus = SD29 : UpdateCount()
                                SD30 = "ES" : sDayStatus = SD30 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD29 = "XX" : sDayStatus = SD29 : UpdateCount()
                                SD30 = "ES" : sDayStatus = SD30 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD29 : UpdateCount()
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD29 : UpdateCount()
                                SD30 = "ES" : sDayStatus = SD30 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD29 : UpdateCount()
                                SD30 = "ES" : sDayStatus = SD30 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 30 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD30 = "AA" Then
                        If SD25 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD25 = "XA" Or SD25 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD29 = "XA" Or SD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD30 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "AA" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD30 = "XA" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "ES" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD30 = "XX" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "ES" : sDayStatus = SD31 : UpdateCount()
                            End If
                        Else
                            If nWeeekWorkingDayCount < 2 Then
                                sDayStatus = SD30 : UpdateCount()
                                SD31 = "AA" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                sDayStatus = SD30 : UpdateCount()
                                SD31 = "ES" : sDayStatus = SD31 : UpdateCount()
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                sDayStatus = SD30 : UpdateCount()
                                SD31 = "ES" : sDayStatus = SD31 : UpdateCount()
                            End If
                        End If

                        sNextDaySunday = "Y"
                    End If
                    nDayNumber += 1
                ElseIf nDayNumber = 31 Then
                    If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Then ''And SD31 = "AA" Then
                        If SD26 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD26 = "XA" Or SD26 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD27 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD27 = "XA" Or SD27 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD28 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD28 = "XA" Or SD28 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD29 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD29 = "XA" Or SD29 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD30 = "XX" Then : nWeeekWorkingDayCount += 1 : ElseIf SD30 = "XA" Or SD30 = "AX" Then : nWeeekWorkingDayCount += 0.5 : End If
                        If SD31 = "AA" Then
                            If nWeeekWorkingDayCount < 2 Then
                                SD31 = "AA"
                            ElseIf nWeeekWorkingDayCount >= 2 And nWeeekWorkingDayCount < 3 Then
                                SD31 = "XA"
                            ElseIf nWeeekWorkingDayCount >= 3 Then
                                SD31 = "XX"
                            End If
                        Else

                        End If


                        sNextDaySunday = "Y"
                    End If
                    nDayNumber = 1
                End If
            End If
        End If


    End Sub

    Private Sub ClearAttendanceInfo()
        DLPresentDays = 0 : DLAbsent = 0 : DLEarnedLeave = 0 : DLCasualLeave = 0 : DLEligibleSunday = 0 : DLWeeklyoff = 0 : DLLongLeave = 0
        DLLayOff = 0 : DLSuspension = 0 : DLSickLeave = 0 : DLMaternityLeave = 0 : DLElectionHoliday = 0 : DLNationalHoliday = 0
        DLFestivalHoliday = 0 : DLOnDuty = 0 : DLTotalPayableDays = 0
        SD01 = "" : SD02 = "" : SD03 = "" : SD04 = "" : SD05 = "" : SD06 = "" : SD07 = "" : SD08 = "" : SD09 = "" : SD10 = ""
        SD11 = "" : SD12 = "" : SD13 = "" : SD14 = "" : SD15 = "" : SD16 = "" : SD17 = "" : SD18 = "" : SD19 = "" : SD20 = ""
        SD21 = "" : SD22 = "" : SD23 = "" : SD24 = "" : SD25 = "" : SD26 = "" : SD27 = "" : SD28 = "" : SD29 = "" : SD30 = ""
        SD31 = ""
    End Sub

    Private Sub UpdateCount()
        NDayNo += 1

        If sDayStatus = "AA" And NFullDayAbsent = 0 Then

            If DateAdd(DateInterval.Day, NDayNo - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                GoTo Aa
            End If
            Dim daSelCompensateHoliday As New SqlDataAdapter("Select * from HRCompensateHolidays Where Cast(DATEPART(DAY,CompensateDate) As Int) = '" & NDayNo & "'", sConstrHR)
            Dim dsSelCompensateHoliday As New DataSet
            daSelCompensateHoliday.Fill(dsSelCompensateHoliday)

            If dsSelCompensateHoliday.Tables(0).Rows.Count > 0 Then
                GoTo Aa
            End If

            If DLEarnedLeave >= 3 Then
                GoTo Aa
            End If

            If DLCasualLeave >= 3 Then
                GoTo Aa
            End If
            Dim dsSelSalInfo As New DataSet
            If Val(tbSummaryMonth.Text) = 1 Then
                Dim daSelSalInfo As New SqlDataAdapter("Select CLEarned, CLPaid, CLBalance, ELEarned, ELPayed, ELBalance from HRMonthSalaryMaster Where EmpCode = '" & SEMPCode & _
                                                       "' And SalMonth = '12' And SalYear = '" & Val(tbSummaryYear.Text) - 1 & "'", sConstrHR)
                daSelSalInfo.Fill(dsSelSalInfo)
            Else
                Dim daSelSalInfo As New SqlDataAdapter("Select CLEarned, CLPaid, CLBalance, ELEarned, ELPayed, ELBalance from HRMonthSalaryMaster Where EmpCode = '" & SEMPCode & _
                                                       "' And SalMonth = '" & Val(tbSummaryMonth.Text) - 1 & _
                                                       "' And SalYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
                daSelSalInfo.Fill(dsSelSalInfo)
            End If

            If dsSelSalInfo.Tables(0).Rows.Count > 0 Then
                If Val(dsSelSalInfo.Tables(0).Rows(0).Item("CLBalance").ToString()) > 0 Then
                    If DLCasualLeave < Val(dsSelSalInfo.Tables(0).Rows(0).Item("CLBalance").ToString()) Then
                        sDayStatus = "CL"
                    Else
                        If Val(dsSelSalInfo.Tables(0).Rows(0).Item("ELBalance").ToString()) > 0 Then
                            If DLEarnedLeave < Val(dsSelSalInfo.Tables(0).Rows(0).Item("ELBalance").ToString()) Then
                                sDayStatus = "EL"
                            End If
                        End If
                    End If
                ElseIf Val(dsSelSalInfo.Tables(0).Rows(0).Item("ELBalance").ToString()) > 0 Then
                    If DLEarnedLeave < Val(dsSelSalInfo.Tables(0).Rows(0).Item("ELBalance").ToString()) Then
                        sDayStatus = "EL"
                    End If
                End If
            End If

            If sDayStatus <> "AA" Then
                If NDayNo = 1 Then : SD01 = sDayStatus
                ElseIf NDayNo = 2 Then : SD02 = sDayStatus
                ElseIf NDayNo = 3 Then : SD03 = sDayStatus
                ElseIf NDayNo = 4 Then : SD04 = sDayStatus
                ElseIf NDayNo = 5 Then : SD05 = sDayStatus
                ElseIf NDayNo = 6 Then : SD06 = sDayStatus
                ElseIf NDayNo = 7 Then : SD07 = sDayStatus
                ElseIf NDayNo = 8 Then : SD08 = sDayStatus
                ElseIf NDayNo = 9 Then : SD09 = sDayStatus
                ElseIf NDayNo = 10 Then : SD10 = sDayStatus
                ElseIf NDayNo = 11 Then : SD11 = sDayStatus
                ElseIf NDayNo = 12 Then : SD12 = sDayStatus
                ElseIf NDayNo = 13 Then : SD13 = sDayStatus
                ElseIf NDayNo = 14 Then : SD14 = sDayStatus
                ElseIf NDayNo = 15 Then : SD15 = sDayStatus
                ElseIf NDayNo = 16 Then : SD16 = sDayStatus
                ElseIf NDayNo = 17 Then : SD17 = sDayStatus
                ElseIf NDayNo = 18 Then : SD18 = sDayStatus
                ElseIf NDayNo = 19 Then : SD19 = sDayStatus
                ElseIf NDayNo = 20 Then : SD20 = sDayStatus
                ElseIf NDayNo = 21 Then : SD21 = sDayStatus
                ElseIf NDayNo = 22 Then : SD22 = sDayStatus
                ElseIf NDayNo = 23 Then : SD23 = sDayStatus
                ElseIf NDayNo = 24 Then : SD24 = sDayStatus
                ElseIf NDayNo = 25 Then : SD25 = sDayStatus
                ElseIf NDayNo = 26 Then : SD26 = sDayStatus
                ElseIf NDayNo = 27 Then : SD27 = sDayStatus
                ElseIf NDayNo = 28 Then : SD28 = sDayStatus
                ElseIf NDayNo = 29 Then : SD29 = sDayStatus
                ElseIf NDayNo = 30 Then : SD30 = sDayStatus
                ElseIf NDayNo = 31 Then : SD31 = sDayStatus
                End If
            End If
Aa:
        End If

        If sDayStatus = "XX" Or sDayStatus = "PP" Then
            DLPresentDays = DLPresentDays + 1
        ElseIf sDayStatus = "XA" Or sDayStatus = "AX" Or sDayStatus = "PA" Or sDayStatus = "AP" Then
            DLPresentDays = DLPresentDays + 0.5
            DLAbsent = DLAbsent + 0.5
        ElseIf sDayStatus = "AA" Then
            DLAbsent = DLAbsent + 1
            NFullDayAbsent += 1
        ElseIf sDayStatus = "EL" Then
            DLEarnedLeave = DLEarnedLeave + 1
        ElseIf sDayStatus = "CL" Then
            DLCasualLeave = DLCasualLeave + 1
        ElseIf sDayStatus = "ES" Then
            'MessageBox.Show(nDayNumber)
            DLEligibleSunday = DLEligibleSunday + 1
        ElseIf sDayStatus = "OO" Then
            DLWeeklyoff = DLWeeklyoff + 1
        ElseIf sDayStatus = "LL" Then
            DLLongLeave = DLLongLeave + 1
        ElseIf sDayStatus = "LO" Then
            DLLayOff = DLLayOff + 1
        ElseIf sDayStatus = "SS" Then
            DLSuspension = DLSuspension + 1
        ElseIf sDayStatus = "SL" Then
            DLSickLeave = DLSickLeave + 1
        ElseIf sDayStatus = "ML" Then
            DLMaternityLeave = DLMaternityLeave + 1
        ElseIf sDayStatus = "EH" Then
            DLElectionHoliday = DLElectionHoliday + 1
        ElseIf sDayStatus = "NH" Then
            DLNationalHoliday = DLNationalHoliday + 1
        ElseIf sDayStatus = "FH" Then
            DLFestivalHoliday = DLFestivalHoliday + 1
        ElseIf sDayStatus = "OD" Then
            DLOnDuty = DLOnDuty + 1
        End If

        DLTotalPayableDays = DLPresentDays + DLEarnedLeave + DLCasualLeave + DLEligibleSunday + DLWeeklyoff + (DLLayOff * 0.5) + DLElectionHoliday + DLNationalHoliday + DLFestivalHoliday + DLOnDuty

    End Sub

    Dim sSummaryID As String
    Private Sub InsertSummaryHdr()
        sSummaryID = System.Guid.NewGuid.ToString()
        Dim daInsSummaryHdr As New SqlDataAdapter("Insert Into HRAttendanceStatusSummery(Id,CreatedDate,UnitCode,Deparment,GeneratingDate,GeneratingMonth,GeneratingYear) Values('" & sSummaryID & _
                                                  "','" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & "','SD','" & sDepartmentFilter & "','" & Format(dpSumaryDate.Value, "dd-MMM-yyyy") & "','" & Val(tbSummaryMonth.Text) & "','" & Val(tbSummaryYear.Text) & "')", sConHR)
        Dim dsInsSummaryHdr As New DataSet()
        daInsSummaryHdr.Fill(dsInsSummaryHdr)
        dsInsSummaryHdr.AcceptChanges()
    End Sub

    Dim sSummaryDtlID, sDepartmentFilter As String
    Private Sub InsertSummaryDtl()

        sSummaryDtlID = System.Guid.NewGuid.ToString()
        Dim daInsSummaryDtl As New SqlDataAdapter("Insert Into HRAttendanceStatusSummeryDetails(ID,CreatedDate,EMPCode,Department,AttDate,	AttStatus,	AttMonth,	AttYear,	UnitCode,	PresentDays,	Absent,	EarnedLeave,	CasualLeave,	EligibleSunday,	Weeklyoff,	LongLeave,	LayOff,	Suspension,	SickLeave,	MaternityLeave,	ElectionHoliday,	NationalHoliday,	FestivalHoliday,	OnDuty,	TotalPayableDays,	D01,	D02,	D03,	D04,	D05,	D06,	D07,	D08,	D09,	D10,	D11,	D12,	D13,	D14,	D15,	D16,	D17,	D18,	D19,	D20,	D21,	D22,	D23,	D24,	D25,	D26,	D27,	D28,	D29,	D30,	D31,	PID, OldEmpCode, EmpFullName,MonthDays) Values('" & sSummaryDtlID & _
                                                  "','" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & "','" & SEMPCode & "','" & SDepartment & "','" & Format(dpSumaryDate.Value, "dd-MMM-yyyy") & "','','" & NAttMonth & "','" & NAttYear & "','SD','" & DLPresentDays & "','" & DLAbsent & "','" & DLEarnedLeave & "','" & DLCasualLeave & "','" & DLEligibleSunday & "','" & DLWeeklyoff & "','" & DLLongLeave & "','" & DLLayOff & "','" & DLSuspension & "','" & DLSickLeave & "','" & DLMaternityLeave & "','" & DLElectionHoliday & "','" & DLNationalHoliday & "','" & DLFestivalHoliday & "','" & DLOnDuty & "','" & DLTotalPayableDays & _
                                                  "','" & SD01 & "','" & SD02 & "','" & SD03 & "','" & SD04 & "','" & SD05 & "','" & SD06 & "','" & SD07 & "','" & SD08 & "','" & SD09 & "','" & SD10 & _
                                                  "','" & SD11 & "','" & SD12 & "','" & SD13 & "','" & SD14 & "','" & SD15 & "','" & SD16 & "','" & SD17 & "','" & SD18 & "','" & SD19 & "','" & SD20 & _
                                                  "','" & SD21 & "','" & SD22 & "','" & SD23 & "','" & SD24 & "','" & SD25 & "','" & SD26 & "','" & SD27 & "','" & SD28 & "','" & SD29 & "','" & SD30 & _
                                                  "','" & SD31 & "','" & sSummaryID & "','" & SOldEmpCode & "','" & SEmpName & "','" & nLastDay & "')", sConHR)
        Dim dsInsSummaryDtl As New DataSet
        daInsSummaryDtl.Fill(dsInsSummaryDtl)
        dsInsSummaryDtl.AcceptChanges()
    End Sub

    Dim dHoliday As Date
    Dim sHoliday, sHolidayDate As String
    Private Sub UpdateHolidays()
        Dim daSelHolidays As New SqlDataAdapter("Select * from Holidays Where HolDate >= '" & Format(dFirstDay.Date, "dd-MMM-yyyy") & _
                                                "' And HolDate <= '" & Format(dLastDay.Date, "dd-MMM-yyyy") & _
                                                "' And HolType in ('EH','FH','NH') Order By HolDate", sConHR)
        Dim dsSelHolidays As New DataSet
        daSelHolidays.Fill(dsSelHolidays)

        If dsSelHolidays.Tables(0).Rows.Count > 0 Then
            Dim i As Integer = dsSelHolidays.Tables(0).Rows.Count

            For i = 0 To dsSelHolidays.Tables(0).Rows.Count - 1
                dHoliday = dsSelHolidays.Tables(0).Rows(i).Item("HolDate")
                sHoliday = dsSelHolidays.Tables(0).Rows(i).Item("HolType").ToString()
                sHolidayDate = "D" + Format(dHoliday.Date, "dd").ToString()

                Dim daUpdHolidays As New SqlDataAdapter("Update HRAttendanceStatusSummeryDetails Set [" + sHolidayDate + "] = '" & sHoliday & _
                                                        "' Where AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & _
                                                        "' And UnitCode = 'SD' And TotalPayableDays >= 2 And [" + sHolidayDate + "] in ('AA','ES')", sConHR)
                Dim dsUpdHolidays As New DataSet
                daUpdHolidays.Fill(dsUpdHolidays)
                dsUpdHolidays.AcceptChanges()

                Dim daSelAttSummary As New SqlDataAdapter("Select * from HRAttendanceStatusSummeryDetails Where [" + sHolidayDate + "] = '" & sHoliday & _
                                                        "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & _
                                                        "' And UnitCode = 'SD' Order by EMPCode", sConHR)
                Dim dsSelAttSummary As New DataSet
                daSelAttSummary.Fill(dsSelAttSummary)

                Dim j As Integer = 0
                For j = 0 To dsSelAttSummary.Tables(0).Rows.Count - 1
                    ClearAttendanceInfo()
                    SEMPCode = dsSelAttSummary.Tables(0).Rows(j).Item("EmpCode").ToString()
                    SD01 = dsSelAttSummary.Tables(0).Rows(j).Item("D01").ToString() : sDayStatus = SD01 : UpdateCount()
                    SD02 = dsSelAttSummary.Tables(0).Rows(j).Item("D02").ToString() : sDayStatus = SD02 : UpdateCount()
                    SD03 = dsSelAttSummary.Tables(0).Rows(j).Item("D03").ToString() : sDayStatus = SD03 : UpdateCount()
                    SD04 = dsSelAttSummary.Tables(0).Rows(j).Item("D04").ToString() : sDayStatus = SD04 : UpdateCount()
                    SD05 = dsSelAttSummary.Tables(0).Rows(j).Item("D05").ToString() : sDayStatus = SD05 : UpdateCount()
                    SD06 = dsSelAttSummary.Tables(0).Rows(j).Item("D06").ToString() : sDayStatus = SD06 : UpdateCount()
                    SD07 = dsSelAttSummary.Tables(0).Rows(j).Item("D07").ToString() : sDayStatus = SD07 : UpdateCount()
                    SD08 = dsSelAttSummary.Tables(0).Rows(j).Item("D08").ToString() : sDayStatus = SD08 : UpdateCount()
                    SD09 = dsSelAttSummary.Tables(0).Rows(j).Item("D09").ToString() : sDayStatus = SD09 : UpdateCount()
                    SD10 = dsSelAttSummary.Tables(0).Rows(j).Item("D10").ToString() : sDayStatus = SD10 : UpdateCount()

                    SD11 = dsSelAttSummary.Tables(0).Rows(j).Item("D11").ToString() : sDayStatus = SD11 : UpdateCount()
                    SD12 = dsSelAttSummary.Tables(0).Rows(j).Item("D12").ToString() : sDayStatus = SD12 : UpdateCount()
                    SD13 = dsSelAttSummary.Tables(0).Rows(j).Item("D13").ToString() : sDayStatus = SD13 : UpdateCount()
                    SD14 = dsSelAttSummary.Tables(0).Rows(j).Item("D14").ToString() : sDayStatus = SD14 : UpdateCount()
                    SD15 = dsSelAttSummary.Tables(0).Rows(j).Item("D15").ToString() : sDayStatus = SD15 : UpdateCount()
                    SD16 = dsSelAttSummary.Tables(0).Rows(j).Item("D16").ToString() : sDayStatus = SD16 : UpdateCount()
                    SD17 = dsSelAttSummary.Tables(0).Rows(j).Item("D17").ToString() : sDayStatus = SD17 : UpdateCount()
                    SD18 = dsSelAttSummary.Tables(0).Rows(j).Item("D18").ToString() : sDayStatus = SD18 : UpdateCount()
                    SD19 = dsSelAttSummary.Tables(0).Rows(j).Item("D19").ToString() : sDayStatus = SD19 : UpdateCount()
                    SD20 = dsSelAttSummary.Tables(0).Rows(j).Item("D20").ToString() : sDayStatus = SD20 : UpdateCount()

                    SD21 = dsSelAttSummary.Tables(0).Rows(j).Item("D21").ToString() : sDayStatus = SD21 : UpdateCount()
                    SD22 = dsSelAttSummary.Tables(0).Rows(j).Item("D22").ToString() : sDayStatus = SD22 : UpdateCount()
                    SD23 = dsSelAttSummary.Tables(0).Rows(j).Item("D23").ToString() : sDayStatus = SD23 : UpdateCount()
                    SD24 = dsSelAttSummary.Tables(0).Rows(j).Item("D24").ToString() : sDayStatus = SD24 : UpdateCount()
                    SD25 = dsSelAttSummary.Tables(0).Rows(j).Item("D25").ToString() : sDayStatus = SD25 : UpdateCount()
                    SD26 = dsSelAttSummary.Tables(0).Rows(j).Item("D26").ToString() : sDayStatus = SD26 : UpdateCount()
                    SD27 = dsSelAttSummary.Tables(0).Rows(j).Item("D27").ToString() : sDayStatus = SD27 : UpdateCount()
                    SD28 = dsSelAttSummary.Tables(0).Rows(j).Item("D28").ToString() : sDayStatus = SD28 : UpdateCount()

                    If nLastDay > 28 Then
                        SD29 = dsSelAttSummary.Tables(0).Rows(j).Item("D29").ToString() : sDayStatus = SD29 : UpdateCount()
                        If nLastDay = 30 Then
                            SD30 = dsSelAttSummary.Tables(0).Rows(j).Item("D30").ToString() : sDayStatus = SD30 : UpdateCount()
                        Else
                            SD30 = dsSelAttSummary.Tables(0).Rows(j).Item("D30").ToString() : sDayStatus = SD30 : UpdateCount()
                            SD31 = dsSelAttSummary.Tables(0).Rows(j).Item("D31").ToString() : sDayStatus = SD31 : UpdateCount()
                        End If
                    End If

                    Dim daUpdSummaryDtl As New SqlDataAdapter("Update HRAttendanceStatusSummeryDetails Set PresentDays = '" & DLPresentDays & "', Absent = '" & DLAbsent & _
                                                              "', EarnedLeave = '" & DLEarnedLeave & "', CasualLeave = '" & DLCasualLeave & "',	EligibleSunday = '" & DLEligibleSunday & _
                                                              "', Weeklyoff = '" & DLWeeklyoff & "', LongLeave = '" & DLLongLeave & "',	LayOff = '" & DLLayOff & _
                                                              "', Suspension = '" & DLSuspension & "', SickLeave = '" & DLSickLeave & "', MaternityLeave = '" & DLMaternityLeave & _
                                                              "', ElectionHoliday = '" & DLElectionHoliday & "', NationalHoliday = '" & DLNationalHoliday & _
                                                              "', FestivalHoliday = '" & DLFestivalHoliday & "', OnDuty = '" & DLOnDuty & "', TotalPayableDays = '" & DLTotalPayableDays & _
                                                              "' Where EmpCode = '" & SEMPCode & "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & _
                                                              "' And UnitCode = 'SD'", sConHR)
                    Dim dsUpdSummaryDtl As New DataSet
                    daUpdSummaryDtl.Fill(dsUpdSummaryDtl)
                    dsUpdSummaryDtl.AcceptChanges()
                Next
            Next


        End If
    End Sub
#End Region




    Private Sub pgb1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pgb1.Click

    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        LoadPendingDataForImport()
    End Sub

    Private Sub LoadAttendanceSummeryDtls(ByVal SUnitCode, ByVal NGeneratingMonth, ByVal NGeneratingYear)
        grdAttSummeryDtls.BringToFront()
        grdAttSummeryDtls.DataSource = myccKHLIAttendanceInfo.LoadAttendanceSummaryDetails(SUnitCode, NGeneratingMonth, NGeneratingYear)


        With grdAttSummeryDtlsV1
            '.Columns(0).VisibleIndex = -1
            '.Columns(1).VisibleIndex = -1
            ''.Columns(22).VisibleIndex = -1

            '.Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            '.Columns(15).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(15).DisplayFormat.FormatString = "dd-MMM-yyyy"

            ' ''.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(6).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(7).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ' ''.Columns(8).DisplayFormat.FormatString = "0.00"
            ' ''.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(9).DisplayFormat.FormatString = "0.00"
            'Dim j As Integer = 19

            'For j = 19 To 52
            '    .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            'Next
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count


        End With
    End Sub

    Private Sub cbUpdateStatusDept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUpdateStatusDept.Click
        If chkbxSingleDay.Checked = True Then
            If dpSumaryDate.Value.DayOfWeek = DayOfWeek.Sunday Then
                MessageBox.Show("For Single Day Option, SUNDAY cannot be processed", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        NFullDayAbsent = 0
        NDayNo = 0
        Dim daSelESEligibility As New SqlDataAdapter("Select * from HRAttendanceStatuseligiblecriteria Where AttendanceStatus = 'ES'", sConHR)
        Dim dsSelESEligibility As New DataSet
        daSelESEligibility.Fill(dsSelESEligibility)

        nESEligibleDays = Val(dsSelESEligibility.Tables(0).Rows(0).Item("NoOfEligibleDays").ToString())

        Dim dSelectedDate As Date = dpSumaryDate.Value
        Dim nCurrentDay As Integer = Format(dpSumaryDate.Value, "dd")
        dFirstDay = DateAdd(DateInterval.Day, -(nCurrentDay - 1), dpSumaryDate.Value)
        Dim dNextMonthFistDay As Date = DateAdd(DateInterval.Month, 1, dFirstDay.Date)
        dLastDay = DateAdd(DateInterval.Day, -1, dNextMonthFistDay.Date)
        nLastDay = Format(dLastDay.Date, "dd")

        If StrUnitCode = "KH" Then
            StrUnitCode = "SD"
        End If

        NAttMonth = Val(tbSummaryMonth.Text)
        NAttYear = Val(tbSummaryYear.Text)

        Dim daSelGeneratingMonth As New SqlDataAdapter("Select * from GeneratingMonth Where UnitCode = '" & StrUnitCode & _
                                                       "' And GeneratingMonth = '" & NAttMonth & _
                                                       "' And GeneratingYear = '" & NAttYear & "'", sConstrHR)
        Dim dsSelGeneratingMonth As New DataSet
        daSelGeneratingMonth.Fill(dsSelGeneratingMonth)

        If dsSelGeneratingMonth.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Generating Month Not assigned properly. Hence Attendance Update will not be completed")
            Exit Sub
        End If

        Dim daSelEmpDept As New SqlDataAdapter("Select Distinct EmployeeDepartment  from Employee Where IsActive = '1' And UnitCode = '" & StrUnitCode & "' Order By EmployeeDepartment", sConstrHR)
        Dim dsSelEmpDept As New DataSet
        daSelEmpDept.Fill(dsSelEmpDept)

        If dsSelEmpDept.Tables(0).Rows.Count > 0 Then
            Dim k As Integer = 0
            Dim nYesNo As Integer
            For k = 0 To dsSelEmpDept.Tables(0).Rows.Count - 1
                pgbdepartment.Maximum = dsSelEmpDept.Tables(0).Rows.Count - 1
                pgbdepartment.Value = k
                pgbdepartment.PerformStep()
                sDepartmentFilter = dsSelEmpDept.Tables(0).Rows(k).Item("EmployeeDepartment").ToString()


                Dim daSelSummaryInfo As New SqlDataAdapter("Select * from HRAttendanceStatusSummery Where UnitCode = '" & StrUnitCode & "' And Deparment = '" & sDepartmentFilter & _
                                                           "' And GeneratingMonth = '" & Val(tbSummaryMonth.Text) & _
                                                           "' And GeneratingYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
                Dim dsSelSummaryInfo As New DataSet
                daSelSummaryInfo.Fill(dsSelSummaryInfo)

                If dsSelSummaryInfo.Tables(0).Rows.Count > 0 Then

                    If chkbxSingleDay.Checked = True Then

                    Else

                        If k = 0 Then
                            nYesNo = MsgBox("Selected Period Attendance Already Imported. Do you the delete the existing one? And Import New Data?", MsgBoxStyle.YesNo)
                        End If
                        If nYesNo = 6 Then
                            Dim daDelSummaryDtls As New SqlDataAdapter("Delete from HRAttendanceStatusSummeryDetails Where UnitCode = '" & StrUnitCode & "' And Department = '" & sDepartmentFilter & _
                                                                       "'  And AttMonth = '" & Val(tbSummaryMonth.Text) & _
                                                                       "' And AttYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
                            Dim dsDelSummaryDtls As New DataSet
                            daDelSummaryDtls.Fill(dsDelSummaryDtls)
                            dsDelSummaryDtls.AcceptChanges()

                            Dim daDelSummary As New SqlDataAdapter("Delete from HRAttendanceStatusSummery Where UnitCode = '" & StrUnitCode & "' And Deparment = '" & sDepartmentFilter & _
                                                                   "' And GeneratingMonth = '" & Val(tbSummaryMonth.Text) & _
                                                                   "' And GeneratingYear = '" & Val(tbSummaryYear.Text) & "'", sConstrHR)
                            Dim dsDelSummary As New DataSet
                            daDelSummary.Fill(dsDelSummary)
                            dsDelSummary.AcceptChanges()

                        Else
                            Exit Sub
                        End If


                    End If
                Else
                    If chkbxSingleDay.Checked = True Then
                        MsgBox("Single Date Data cannot be updated withintout First Updating Month Status", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                End If
                If chkbxSingleDay.Checked = False Then
                    InsertSummaryHdr()
                End If

                Dim daSelEmployee As New SqlDataAdapter("Select * from Employee Where IsActive = '1' And UnitCode = '" & StrUnitCode & _
                                                        "' And EmployeeDepartment = '" & sDepartmentFilter & "' Order by EmpCode", sConHR)
                Dim dsSelEmployee As New DataSet
                daSelEmployee.Fill(dsSelEmployee)

                Dim nEmployeeCount As Integer = dsSelEmployee.Tables(0).Rows.Count()

                Dim daSelUnitInfo As New SqlDataAdapter("Select * from UnitMaster Where UnitCode = '" & StrUnitCode & "'", sConHR)
                Dim dsSelUnitInfo As New DataSet
                daSelUnitInfo.Fill(dsSelUnitInfo)

                nWeekWorkingDays = Val(dsSelUnitInfo.Tables(0).Rows(0).Item("WeekWorkingDays").ToString())

                If nWeekWorkingDays = 5 Then
                    If chkbxSingleDay.Checked = True Then
                        If dpSumaryDate.Value.DayOfWeek = DayOfWeek.Saturday Then
                            MessageBox.Show("For Single Day Option, SATURDAY & SUNDAY cannot be processed", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End If
                End If

                Dim i As Integer = 0

                For i = 0 To nEmployeeCount - 1
                    pgb1.Maximum = nEmployeeCount
                    pgb1.Value = i
                    NFullDayAbsent = 0
                    NDayNo = 0
                    pgb1.PerformStep()
                    ClearAttendanceInfo()
                    SEMPCode = dsSelEmployee.Tables(0).Rows(i).Item("EmpCode").ToString()
                    SDepartment = dsSelEmployee.Tables(0).Rows(i).Item("EmployeeDepartment").ToString()
                    SUnitCode = dsSelEmployee.Tables(0).Rows(i).Item("UnitCode").ToString()
                    SOldEmpCode = dsSelEmployee.Tables(0).Rows(i).Item("OlDEmpCode").ToString()
                    SEmpName = dsSelEmployee.Tables(0).Rows(i).Item("EmpFullName").ToString()
                    'If chkbxSingleDay.Checked = True Then
                    '    dAttenDate = dpSumaryDate.Value
                    '    nDayNumber = Val(Format(dpSumaryDate.Value, "dd"))
                    'Else
                    dAttenDate = dFirstDay
                    nDayNumber = 1
                    'End If

                    NAttMonth = Val(tbSummaryMonth.Text)
                    NAttYear = Val(tbSummaryYear.Text)

                    LoadPreviousMonthInfo()
                    Dim sRecordExist As String = ""

                    Dim dsSelEmpAttInfo As New DataSet
                    Dim dsSelEmpAttInfoFSD As New DataSet
                    If chkbxSingleDay.Checked = True Then
                        Dim daSelEmpAttInfo As New SqlDataAdapter("Select * from HRAttendanceStatusSummeryDetails Where Empcode = '" & SEMPCode & _
                                                              "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & _
                                                              "'", sConHR)
                        daSelEmpAttInfo.Fill(dsSelEmpAttInfo)
                        sSummaryDtlID = dsSelEmpAttInfo.Tables(0).Rows(0).Item("ID").ToString()
                        Dim daSelEmpAttInfoFSD As New SqlDataAdapter("Select * from vw_AttendanceSummary Where Empcode = '" & SEMPCode & _
                                                                     "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & "'", sConHR)

                        daSelEmpAttInfoFSD.Fill(dsSelEmpAttInfoFSD)
                        If dsSelEmpAttInfoFSD.Tables(0).Rows.Count = 0 Then
                            sRecordExist = "N"
                        Else
                            sRecordExist = "Y"
                        End If
                    Else
                        Dim daSelEmpAttInfo As New SqlDataAdapter("Select * from vw_AttendanceSummary Where Empcode = '" & SEMPCode & _
                                                                  "' And AttYear = '" & NAttYear & "' And AttMonth = '" & NAttMonth & "'", sConHR)
                        daSelEmpAttInfo.Fill(dsSelEmpAttInfo)
                    End If


                    If dsSelEmpAttInfo.Tables(0).Rows.Count = 1 Then

                        If nWeekWorkingDays = 6 Then
                            If chkbxSingleDay.Checked = True Then
                                If Format(dpSumaryDate.Value, "dd") = "01" And sRecordExist = "Y" Then
                                    SD01 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("1").ToString()
                                Else
                                    SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D01").ToString()
                                End If
                                CalculateES() : sDayStatus = SD01 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "02" And sRecordExist = "Y" Then
                                    SD02 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("2").ToString()
                                Else
                                    SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D02").ToString()
                                End If
                                CalculateES() : sDayStatus = SD02 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "03" And sRecordExist = "Y" Then
                                    SD03 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("3").ToString()
                                Else
                                    SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D03").ToString()
                                End If
                                CalculateES() : sDayStatus = SD03 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "04" And sRecordExist = "Y" Then
                                    SD04 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("4").ToString()
                                Else
                                    SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D04").ToString()
                                End If
                                CalculateES() : sDayStatus = SD04 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "05" And sRecordExist = "Y" Then
                                    SD05 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("5").ToString()
                                Else
                                    SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D05").ToString()
                                End If
                                CalculateES() : sDayStatus = SD05 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "06" And sRecordExist = "Y" Then
                                    SD06 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("6").ToString()
                                Else
                                    SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D06").ToString()
                                End If
                                CalculateES() : sDayStatus = SD06 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "07" And sRecordExist = "Y" Then
                                    SD07 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("7").ToString()
                                Else
                                    SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D07").ToString()
                                End If
                                CalculateES() : sDayStatus = SD07 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "08" And sRecordExist = "Y" Then
                                    SD08 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("8").ToString()
                                Else
                                    SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D08").ToString()
                                End If
                                CalculateES() : sDayStatus = SD08 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "09" And sRecordExist = "Y" Then
                                    SD09 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("9").ToString()
                                Else
                                    SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D09").ToString()
                                End If
                                CalculateES() : sDayStatus = SD09 : UpdateCount()


                                If Format(dpSumaryDate.Value, "dd") = "10" And sRecordExist = "Y" Then
                                    SD10 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("10").ToString()
                                Else
                                    SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D10").ToString()
                                End If
                                CalculateES() : sDayStatus = SD10 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "11" And sRecordExist = "Y" Then
                                    SD11 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("11").ToString()
                                Else
                                    SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D11").ToString()
                                End If
                                CalculateES() : sDayStatus = SD11 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "12" And sRecordExist = "Y" Then
                                    SD12 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("12").ToString()
                                Else
                                    SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D12").ToString()
                                End If
                                CalculateES() : sDayStatus = SD12 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "13" And sRecordExist = "Y" Then
                                    SD13 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("13").ToString()
                                Else
                                    SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D13").ToString()
                                End If
                                CalculateES() : sDayStatus = SD13 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "14" And sRecordExist = "Y" Then
                                    SD14 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("14").ToString()
                                Else
                                    SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D14").ToString()
                                End If
                                CalculateES() : sDayStatus = SD14 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "15" And sRecordExist = "Y" Then
                                    SD15 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("15").ToString()
                                Else
                                    SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D15").ToString()
                                End If
                                CalculateES() : sDayStatus = SD15 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "16" And sRecordExist = "Y" Then
                                    SD16 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("16").ToString()
                                Else
                                    SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D16").ToString()
                                End If
                                CalculateES() : sDayStatus = SD16 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "17" And sRecordExist = "Y" Then
                                    SD17 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("17").ToString()
                                Else
                                    SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D17").ToString()
                                End If
                                CalculateES() : sDayStatus = SD17 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "18" And sRecordExist = "Y" Then
                                    SD18 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("18").ToString()
                                Else
                                    SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D18").ToString()
                                End If
                                CalculateES() : sDayStatus = SD18 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "19" And sRecordExist = "Y" Then
                                    SD19 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("19").ToString()
                                Else
                                    SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D19").ToString()
                                End If
                                CalculateES() : sDayStatus = SD19 : UpdateCount()


                                If Format(dpSumaryDate.Value, "dd") = "20" And sRecordExist = "Y" Then
                                    SD20 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("20").ToString()
                                Else
                                    SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D20").ToString()
                                End If
                                CalculateES() : sDayStatus = SD20 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "21" And sRecordExist = "Y" Then
                                    SD21 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("21").ToString()
                                Else
                                    SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D21").ToString()
                                End If
                                CalculateES() : sDayStatus = SD21 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "22" And sRecordExist = "Y" Then
                                    SD22 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("22").ToString()
                                Else
                                    SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D22").ToString()
                                End If
                                CalculateES() : sDayStatus = SD22 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "23" And sRecordExist = "Y" Then
                                    SD23 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("23").ToString()
                                Else
                                    SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D23").ToString()
                                End If
                                CalculateES() : sDayStatus = SD23 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "24" And sRecordExist = "Y" Then
                                    SD24 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("24").ToString()
                                Else
                                    SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D24").ToString()
                                End If
                                CalculateES() : sDayStatus = SD24 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "25" And sRecordExist = "Y" Then
                                    SD25 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("25").ToString()
                                Else
                                    SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D25").ToString()
                                End If
                                CalculateES() : sDayStatus = SD25 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "26" And sRecordExist = "Y" Then
                                    SD26 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("26").ToString()
                                Else
                                    SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D26").ToString()
                                End If
                                CalculateES() : sDayStatus = SD26 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "27" And sRecordExist = "Y" Then
                                    SD27 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("27").ToString()
                                Else
                                    SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D27").ToString()
                                End If
                                CalculateES() : sDayStatus = SD27 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "28" And sRecordExist = "Y" Then
                                    SD28 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("28").ToString()
                                Else
                                    SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D28").ToString()
                                End If
                                CalculateES() : sDayStatus = SD28 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "29" And sRecordExist = "Y" Then
                                    SD29 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("29").ToString()
                                Else
                                    SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D29").ToString()
                                End If
                                CalculateES() : sDayStatus = SD29 : UpdateCount()


                                If Format(dpSumaryDate.Value, "dd") = "30" And sRecordExist = "Y" Then
                                    SD30 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("30").ToString()
                                Else
                                    SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D30").ToString()
                                End If
                                CalculateES() : sDayStatus = SD30 : UpdateCount()

                                If Format(dpSumaryDate.Value, "dd") = "31" And sRecordExist = "Y" Then
                                    SD31 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("31").ToString()
                                Else
                                    SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D31").ToString()
                                End If
                                CalculateES() : sDayStatus = SD31 : UpdateCount()
                            Else

                                SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                                SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("2").ToString() : CalculateES() : sDayStatus = SD02 : UpdateCount()
                                SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("3").ToString() : CalculateES() : sDayStatus = SD03 : UpdateCount()
                                SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("4").ToString() : CalculateES() : sDayStatus = SD04 : UpdateCount()
                                SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("5").ToString() : CalculateES() : sDayStatus = SD05 : UpdateCount()
                                SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("6").ToString() : CalculateES() : sDayStatus = SD06 : UpdateCount()
                                SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("7").ToString() : CalculateES() : sDayStatus = SD07 : UpdateCount()
                                SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("8").ToString() : CalculateES() : sDayStatus = SD08 : UpdateCount()
                                SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("9").ToString() : CalculateES() : sDayStatus = SD09 : UpdateCount()
                                SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("10").ToString() : CalculateES() : sDayStatus = SD10 : UpdateCount()

                                SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("11").ToString() : CalculateES() : sDayStatus = SD11 : UpdateCount()
                                SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("12").ToString() : CalculateES() : sDayStatus = SD12 : UpdateCount()
                                SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("13").ToString() : CalculateES() : sDayStatus = SD13 : UpdateCount()
                                SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("14").ToString() : CalculateES() : sDayStatus = SD14 : UpdateCount()
                                SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("15").ToString() : CalculateES() : sDayStatus = SD15 : UpdateCount()
                                SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("16").ToString() : CalculateES() : sDayStatus = SD16 : UpdateCount()
                                SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("17").ToString() : CalculateES() : sDayStatus = SD17 : UpdateCount()
                                SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("18").ToString() : CalculateES() : sDayStatus = SD18 : UpdateCount()
                                SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("19").ToString() : CalculateES() : sDayStatus = SD19 : UpdateCount()
                                SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("20").ToString() : CalculateES() : sDayStatus = SD20 : UpdateCount()

                                SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("21").ToString() : CalculateES() : sDayStatus = SD21 : UpdateCount()
                                SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("22").ToString() : CalculateES() : sDayStatus = SD22 : UpdateCount()
                                SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("23").ToString() : CalculateES() : sDayStatus = SD23 : UpdateCount()
                                SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString() : CalculateES() : sDayStatus = SD24 : UpdateCount()
                                SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString() : CalculateES() : sDayStatus = SD25 : UpdateCount()
                                SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString() : CalculateES() : sDayStatus = SD26 : UpdateCount()
                                SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString() : CalculateES() : sDayStatus = SD27 : UpdateCount()
                                SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString() : CalculateES() : sDayStatus = SD28 : UpdateCount()

                                If nLastDay > 28 Then
                                    SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString() : CalculateES() : sDayStatus = SD29 : UpdateCount()
                                    If nLastDay = 30 Then
                                        SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : sDayStatus = SD30 : UpdateCount()
                                    Else
                                        SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : sDayStatus = SD30 : UpdateCount()
                                        SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString() : CalculateES() : sDayStatus = SD31 : UpdateCount()
                                    End If
                                End If
                            End If
                        ElseIf nWeekWorkingDays = 5 Then


                            If chkbxSingleDay.Checked = True Then

                                'SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                                If Format(dpSumaryDate.Value, "dd") = "01" And sRecordExist = "Y" Then
                                    SD01 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("1").ToString()
                                Else
                                    SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D01").ToString()
                                End If
                                sDayStatus = SD01 : UpdateCount() : CalculateES()
                                nDayNumber += 1
                                If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If

                                If Format(dpSumaryDate.Value, "dd") = "02" And sRecordExist = "Y" Then
                                    SD02 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("2").ToString()
                                Else
                                    SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D02").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD02 : UpdateCount()
                                Else
                                    sDayStatus = SD02 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "03" And sRecordExist = "Y" Then
                                    SD03 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("3").ToString()
                                Else
                                    SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D03").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD03 : UpdateCount()
                                Else
                                    sDayStatus = SD03 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "04" And sRecordExist = "Y" Then
                                    SD04 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("4").ToString()
                                Else
                                    SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D04").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD04 : UpdateCount()
                                Else
                                    sDayStatus = SD04 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "05" And sRecordExist = "Y" Then
                                    SD05 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("5").ToString()
                                Else
                                    SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D05").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD05 : UpdateCount()
                                Else
                                    sDayStatus = SD05 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "06" And sRecordExist = "Y" Then
                                    SD06 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("6").ToString()
                                Else
                                    SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D06").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD06 : UpdateCount()
                                Else
                                    sDayStatus = SD06 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "07" And sRecordExist = "Y" Then
                                    SD07 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("7").ToString()
                                Else
                                    SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D07").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD07 : UpdateCount()
                                Else
                                    sDayStatus = SD07 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "08" And sRecordExist = "Y" Then
                                    SD08 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("8").ToString()
                                Else
                                    SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D08").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD08 : UpdateCount()
                                Else
                                    sDayStatus = SD08 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "09" And sRecordExist = "Y" Then
                                    SD09 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("9").ToString()
                                Else
                                    SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D09").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD09 : UpdateCount()
                                Else
                                    sDayStatus = SD09 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If



                                If Format(dpSumaryDate.Value, "dd") = "10" And sRecordExist = "Y" Then
                                    SD10 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("10").ToString()
                                Else
                                    SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D10").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD10 : UpdateCount()
                                Else
                                    sDayStatus = SD10 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "11" And sRecordExist = "Y" Then
                                    SD11 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("11").ToString()
                                Else
                                    SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D11").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD11 : UpdateCount()
                                Else
                                    sDayStatus = SD11 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "12" And sRecordExist = "Y" Then
                                    SD12 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("12").ToString()
                                Else
                                    SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D12").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD12 : UpdateCount()
                                Else
                                    sDayStatus = SD12 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "13" And sRecordExist = "Y" Then
                                    SD13 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("13").ToString()
                                Else
                                    SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D13").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD13 : UpdateCount()
                                Else
                                    sDayStatus = SD13 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "14" And sRecordExist = "Y" Then
                                    SD14 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("14").ToString()
                                Else
                                    SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D14").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD14 : UpdateCount()
                                Else
                                    sDayStatus = SD14 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "15" And sRecordExist = "Y" Then
                                    SD15 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("15").ToString()
                                Else
                                    SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D15").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD15 : UpdateCount()
                                Else
                                    sDayStatus = SD15 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "16" And sRecordExist = "Y" Then
                                    SD16 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("16").ToString()
                                Else
                                    SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D16").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD16 : UpdateCount()
                                Else
                                    sDayStatus = SD16 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "17" And sRecordExist = "Y" Then
                                    SD17 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("17").ToString()
                                Else
                                    SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D17").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD17 : UpdateCount()
                                Else
                                    sDayStatus = SD17 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "18" And sRecordExist = "Y" Then
                                    SD18 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("18").ToString()
                                Else
                                    SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D18").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD18 : UpdateCount()
                                Else
                                    sDayStatus = SD18 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "19" And sRecordExist = "Y" Then
                                    SD19 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("19").ToString()
                                Else
                                    SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D19").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD19 : UpdateCount()
                                Else
                                    sDayStatus = SD19 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "20" And sRecordExist = "Y" Then
                                    SD20 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("20").ToString()
                                Else
                                    SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D20").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD20 : UpdateCount()
                                Else
                                    sDayStatus = SD20 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "21" And sRecordExist = "Y" Then
                                    SD21 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("21").ToString()
                                Else
                                    SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D21").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD21 : UpdateCount()
                                Else
                                    sDayStatus = SD21 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "22" And sRecordExist = "Y" Then
                                    SD22 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("22").ToString()
                                Else
                                    SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D22").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD22 : UpdateCount()
                                Else
                                    sDayStatus = SD22 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "23" And sRecordExist = "Y" Then
                                    SD23 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("23").ToString()
                                Else
                                    SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D23").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD23 : UpdateCount()
                                Else
                                    sDayStatus = SD23 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "24" And sRecordExist = "Y" Then
                                    SD24 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("24").ToString()
                                Else
                                    SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D24").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD24 : UpdateCount()
                                Else
                                    sDayStatus = SD24 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "25" And sRecordExist = "Y" Then
                                    SD25 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("25").ToString()
                                Else
                                    SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D25").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD25 : UpdateCount()
                                Else
                                    sDayStatus = SD25 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "26" And sRecordExist = "Y" Then
                                    SD26 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("26").ToString()
                                Else
                                    SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D26").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD26 : UpdateCount()
                                Else
                                    sDayStatus = SD26 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "27" And sRecordExist = "Y" Then
                                    SD27 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("27").ToString()
                                Else
                                    SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D27").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD27 : UpdateCount()
                                Else
                                    sDayStatus = SD27 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "28" And sRecordExist = "Y" Then
                                    SD28 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("28").ToString()
                                Else
                                    SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D28").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD28 : UpdateCount()
                                Else
                                    sDayStatus = SD28 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "29" And sRecordExist = "Y" Then
                                    SD29 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("29").ToString()
                                Else
                                    SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D29").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD29 : UpdateCount()
                                Else
                                    sDayStatus = SD29 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If


                                If Format(dpSumaryDate.Value, "dd") = "30" And sRecordExist = "Y" Then
                                    SD30 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("30").ToString()
                                Else
                                    SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D30").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD30 : UpdateCount()
                                Else
                                    sDayStatus = SD30 : UpdateCount() : CalculateES() : nDayNumber += 1 : If nDayNumber > Val(Format(dpSumaryDate.Value, "dd")) Then : GoTo Aa : End If
                                End If

                                If Format(dpSumaryDate.Value, "dd") = "31" And sRecordExist = "Y" Then
                                    SD31 = dsSelEmpAttInfoFSD.Tables(0).Rows(0).Item("31").ToString()
                                Else
                                    SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("D31").ToString()
                                End If
                                If DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Saturday Or DateAdd(DateInterval.Day, nDayNumber - 1, dAttenDate).DayOfWeek = DayOfWeek.Sunday Then
                                    nDayNumber += 1 : sDayStatus = SD31 : UpdateCount()
                                Else
                                    sDayStatus = SD31 : UpdateCount()
                                End If
                            Else

                                SD01 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("1").ToString() : CalculateES() : sDayStatus = SD01 : UpdateCount()
                                If DateAdd(DateInterval.Day, 1, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD02 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("2").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD02 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 2, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD03 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("3").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD03 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 3, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD04 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("4").ToString()
                                    CalculateES()
                                    If sNextDaySunday = "N" Then : sDayStatus = SD04 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 4, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD05 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("5").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD05 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 5, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD06 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("6").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD06 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 6, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD07 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("7").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD07 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 7, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD08 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("8").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD08 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 8, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD09 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("9").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD09 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 9, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD10 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("10").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD10 : UpdateCount() : End If : Else : nDayNumber += 1 : End If

                                If DateAdd(DateInterval.Day, 10, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD11 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("11").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD11 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 11, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD12 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("12").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD12 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 12, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD13 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("13").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD13 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 13, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD14 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("14").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD14 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 14, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD15 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("15").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD15 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 15, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD16 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("16").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD16 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 16, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD17 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("17").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD17 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 17, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD18 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("18").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD18 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 18, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD19 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("19").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD19 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 19, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD20 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("20").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD20 : UpdateCount() : End If : Else : nDayNumber += 1 : End If

                                If DateAdd(DateInterval.Day, 20, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD21 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("21").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD21 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 21, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD22 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("22").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD22 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 22, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD23 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("23").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD23 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 23, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD24 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("24").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD24 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 24, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD25 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("25").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD25 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 25, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD26 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("26").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD26 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 25, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD27 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("27").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD27 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                If DateAdd(DateInterval.Day, 27, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD28 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("28").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD28 : UpdateCount() : End If : Else : nDayNumber += 1 : End If

                                If nLastDay > 28 Then
                                    If DateAdd(DateInterval.Day, 28, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD29 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("29").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD29 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                    If nLastDay = 30 Then
                                        If DateAdd(DateInterval.Day, 29, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD30 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                    Else
                                        If DateAdd(DateInterval.Day, 29, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD30 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("30").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD30 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                        If DateAdd(DateInterval.Day, 30, dFirstDay).DayOfWeek <> DayOfWeek.Sunday Then : SD31 = dsSelEmpAttInfo.Tables(0).Rows(0).Item("31").ToString() : CalculateES() : If sNextDaySunday = "N" Then : sDayStatus = SD31 : UpdateCount() : End If : Else : nDayNumber += 1 : End If
                                    End If
                                End If
                            End If

                        End If
                    Else
                        SD01 = "AA" : sDayStatus = SD01 : UpdateCount()
                        SD02 = "AA" : sDayStatus = SD02 : UpdateCount()
                        SD03 = "AA" : sDayStatus = SD03 : UpdateCount()
                        SD04 = "AA" : sDayStatus = SD04 : UpdateCount()
                        SD05 = "AA" : sDayStatus = SD05 : UpdateCount()
                        SD06 = "AA" : sDayStatus = SD06 : UpdateCount()
                        SD07 = "AA" : sDayStatus = SD07 : UpdateCount()
                        SD08 = "AA" : sDayStatus = SD08 : UpdateCount()
                        SD09 = "AA" : sDayStatus = SD09 : UpdateCount()
                        SD10 = "AA" : sDayStatus = SD10 : UpdateCount()

                        SD11 = "AA" : sDayStatus = SD11 : UpdateCount()
                        SD12 = "AA" : sDayStatus = SD12 : UpdateCount()
                        SD13 = "AA" : sDayStatus = SD13 : UpdateCount()
                        SD14 = "AA" : sDayStatus = SD14 : UpdateCount()
                        SD15 = "AA" : sDayStatus = SD15 : UpdateCount()
                        SD16 = "AA" : sDayStatus = SD16 : UpdateCount()
                        SD17 = "AA" : sDayStatus = SD17 : UpdateCount()
                        SD18 = "AA" : sDayStatus = SD18 : UpdateCount()
                        SD19 = "AA" : sDayStatus = SD19 : UpdateCount()
                        SD20 = "AA" : sDayStatus = SD20 : UpdateCount()

                        SD21 = "AA" : sDayStatus = SD21 : UpdateCount()
                        SD22 = "AA" : sDayStatus = SD22 : UpdateCount()
                        SD23 = "AA" : sDayStatus = SD23 : UpdateCount()
                        SD24 = "AA" : sDayStatus = SD24 : UpdateCount()
                        SD25 = "AA" : sDayStatus = SD25 : UpdateCount()
                        SD26 = "AA" : sDayStatus = SD26 : UpdateCount()
                        SD27 = "AA" : sDayStatus = SD27 : UpdateCount()
                        SD28 = "AA" : sDayStatus = SD28 : UpdateCount()

                        If nLastDay > 28 Then
                            SD29 = "AA" : sDayStatus = SD29 : UpdateCount()
                            If nLastDay = 30 Then
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                            Else
                                SD30 = "AA" : sDayStatus = SD30 : UpdateCount()
                                SD31 = "AA" : sDayStatus = SD31 : UpdateCount()
                            End If
                        End If
                    End If
Aa:
                    If chkbxSingleDay.Checked = True Then

                        Dim daUpdSummaryDtl As New SqlDataAdapter("Update HRAttendanceStatusSummeryDetails Set PresentDays = '" & DLPresentDays & _
                                                                  "', Absent = '" & DLAbsent & "', EarnedLeave = '" & DLEarnedLeave & _
                                                                  "', CasualLeave = '" & DLCasualLeave & "', EligibleSunday = '" & DLEligibleSunday & _
                                                                  "', WeeklyOff = '" & DLWeeklyoff & "', LongLeave = '" & DLLongLeave & _
                                                                  "', LayOff = '" & DLLayOff & "', Suspension = '" & DLSuspension & _
                                                                  "', SickLeave = '" & DLSickLeave & "', MaternityLeave = '" & DLMaternityLeave & _
                                                                  "', ElectionHoliday = '" & DLElectionHoliday & "', NationalHoliday = '" & DLNationalHoliday & _
                                                                  "', FestivalHoliday = '" & DLFestivalHoliday & "', OnDuty = '" & DLOnDuty & _
                                                                  "', TotalPayableDays = '" & DLTotalPayableDays & "', D01 = '" & SD01 & "', D02 = '" & SD02 & "', D03 = '" & SD03 & _
                                                                  "', D04 = '" & SD04 & "', D05 = '" & SD05 & "',  D06 = '" & SD06 & "', D07 = '" & SD07 & _
                                                                  "', D08 = '" & SD08 & "', D09 = '" & SD09 & "', D10 = '" & SD10 & "', D11 = '" & SD11 & _
                                                                  "', D12 = '" & SD12 & "', D13 = '" & SD13 & "', D14 = '" & SD14 & "', D15 = '" & SD15 & _
                                                                  "', D16 = '" & SD16 & "', D17 = '" & SD17 & "', D18 = '" & SD18 & "', D19 = '" & SD19 & _
                                                                  "', D20 = '" & SD20 & "', D21 = '" & SD21 & "', D22 = '" & SD22 & "', D23 = '" & SD23 & _
                                                                  "', D24 = '" & SD24 & "', D25 = '" & SD25 & "', D26 = '" & SD26 & "', D27 = '" & SD27 & _
                                                                  "', D28 = '" & SD28 & "', D29 = '" & SD29 & "', D30 = '" & SD30 & "', D31 = '" & SD31 & _
                                                                  "' Where ID = '" & sSummaryDtlID & "'", sConHR)
                        Dim dsUpdSummaryDtl As New DataSet
                        daUpdSummaryDtl.Fill(dsUpdSummaryDtl)
                        dsUpdSummaryDtl.AcceptChanges()

                    Else
                        InsertSummaryDtl()
                    End If
                Next

            Next


        End If
        UpdateHolidays()
        MessageBox.Show("Completed")

        LoadAttendanceSummeryDtls(SUnitCode, NAttMonth, NAttYear)
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
