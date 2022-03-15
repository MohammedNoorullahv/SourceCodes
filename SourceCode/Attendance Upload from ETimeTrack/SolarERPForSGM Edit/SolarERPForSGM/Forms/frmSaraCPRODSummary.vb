Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO
Imports System.IO.Ports
Imports System.Drawing.Rectangle

Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop




Public Class frmSaraCPRODSummary

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)
    Dim keyascii As Integer

    Dim myccSARACProduction As New ccSARACProduction

#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End
        'Me.Hide()
        'Form1.Show() : Form1.BringToFront()
    End Sub

    Dim ngrdRowCount As Integer


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'MsgBox("Export Completed")

        'Dim filePath As String = String.Format("E:\BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        'Using writer As New StreamWriter(filePath, True)
        '    If File.Exists(filePath) Then
        '        'writer.WriteLine("Error Message in  Occured at-- " & DateTime.Now)
        '        writer.WriteLine(Trim(tbBarcode.Text) + " | " & DateTime.Now)
        '    Else
        '        writer.WriteLine("Start Error Log for today")
        '    End If
        'End Using



    End Sub

    Dim nBalanceDuration, nDetailModeDuration As Integer
    Dim sInfo As String
    Private Sub frmSaraCPRODSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadSummary()
        Timer1.Start()
        nBalanceDuration = 30
        sInfo = "Hourly Production"
        Label1.Text = Format(Date.Now, "dd-MMM-yy")
        LoadOption()
    End Sub



 

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'LoadSummary()
    End Sub
    Dim dsSelCapacity As New DataSet
    Dim nRowNo, nHourCount, nBalHour, nDelayBy As Integer

    Private Sub LoadDailyProduction()
        Try
            
            If sInfo = "Hourly Production" Then
                lblDataInfo.Text = "Hourly Production"
                grdConveyorProduction.Visible = True
                grdConveyorProduction.BringToFront()
                grdWeeklyPlan.Visible = False
                plCapacity.Visible = False
                grdConveyorProduction.DataSource = myccSARACProduction.LoadAllConveyorProduction(Format(Date.Now, "dd-MMM-yyyy"))

                With grdConveyorProductionV1
                    .Columns(0).VisibleIndex = -1
                    .Columns(2).VisibleIndex = -1

                    .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(5).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                End With

                sInfo = "Week Plan"
            ElseIf sInfo = "Week Plan" Then
                lblDataInfo.Text = "Week Plan"
                grdWeeklyPlan.Visible = True
                grdWeeklyPlan.BringToFront()
                grdConveyorProduction.Visible = False
                plCapacity.Visible = False
                grdWeeklyPlan.DataSource = myccSARACProduction.LoadWeeklyPlan(Format(Date.Now, "dd-MMM-yyyy"))

                With grdWeeklyPlanV1

                    .Columns(3).VisibleIndex = -1
                    .Columns(4).VisibleIndex = -1
                    .Columns(9).VisibleIndex = -1

                    .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                    .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


                End With


                sInfo = "Capacity Status"
            ElseIf sInfo = "Capacity Status" Then
                plCapacity.Visible = True
                plCapacity.BringToFront()

                nHourCount = Format(Date.Now, "HH") - 8


                If Format(Date.Now, "HH") > 12 Then
                    lblElapsedHour.Text = nHourCount - 2
                Else
                    lblElapsedHour.Text = nHourCount - 1
                End If

                tbBalanceHours.Text = 8 - Val(lblElapsedHour.Text)
                nBalHour = Val(tbBalanceHours.Text)

                If nRowNo = 0 Then
                    Dim daSelCapacity As New SqlDataAdapter("Select * from upperdailyproductionplan Where PlanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                            "' Order by Conveyor", sConstr)

                    daSelCapacity.Fill(dsSelCapacity)
                End If

                Dim nToBeProducedQty As Integer
                lblCapacity.Text = dsSelCapacity.Tables(0).Rows(nRowNo).Item("Conveyor").ToString
                tbPlannedDay.Text = Val(dsSelCapacity.Tables(0).Rows(nRowNo).Item("TargetQuantity").ToString)
                tbPlannedHour.Text = Math.Ceiling(Val(dsSelCapacity.Tables(0).Rows(nRowNo).Item("TargetQuantity").ToString) / 8)


                nToBeProducedQty = Math.Ceiling(Val(Val(dsSelCapacity.Tables(0).Rows(nRowNo).Item("TargetQuantity").ToString) / 8) * nHourCount)

                Dim daProduction As New SqlDataAdapter("Select MachineNo,DayHour,IsNull(Sum(Quantity),0)  As FinishQty from UpProduction Where ProcessName = 'FIN' And Cast(ProcessDate As Date) = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                       "' And MachineNo = '" & dsSelCapacity.Tables(0).Rows(nRowNo).Item("Conveyor").ToString & _
                                                       "' And DayHour = '" & nHourCount & "' Group By MachineNo,DayHour", sConstr)
                Dim dsProduction As New DataSet
                daProduction.Fill(dsProduction)


                If dsProduction.Tables(0).Rows.Count <= 0 Then
                    tbAchievedHour.Text = 0
                Else
                    tbAchievedHour.Text = Val(dsProduction.Tables(0).Rows(0).Item("FinishQty").ToString)
                End If

                Dim daDayProduction As New SqlDataAdapter("Select MachineNo,IsNull(Sum(Quantity),0)  As FinishQty from UpProduction Where ProcessName = 'FIN' And Cast(ProcessDate As Date) = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                       "' And MachineNo = '" & dsSelCapacity.Tables(0).Rows(nRowNo).Item("Conveyor").ToString & _
                                                       "' Group By MachineNo", sConstr)
                Dim dsDayProduction As New DataSet
                daDayProduction.Fill(dsDayProduction)

                If dsDayProduction.Tables(0).Rows.Count <= 0 Then
                    tbAchievedDay.Text = 0
                Else
                    tbAchievedDay.Text = Math.Ceiling(Val(dsDayProduction.Tables(0).Rows(0).Item("FinishQty").ToString))
                End If

                nDelayBy = nToBeProducedQty - Val(tbAchievedDay.Text)

                tbDifferenceDay.Text = Val(tbPlannedDay.Text) - Val(tbAchievedDay.Text)
                tbDifferenceHour.Text = Val(tbPlannedHour.Text) - Val(tbAchievedHour.Text)

                'lblDayHour.Text = nBalHour

                tbToAchieveHour.Text = Format((Val(tbDifferenceDay.Text) / nBalHour), "0.00")

                If Val(tbToAchieveHour.Text) > Val(tbPlannedHour.Text) Then
                    tbStatusHour.Text = "Delay of " + nDelayBy.ToString + " Pairs"
                    tbStatusHour.ForeColor = Color.Red
                ElseIf Val(tbToAchieveHour.Text) = Val(tbPlannedHour.Text) Then
                    tbStatusHour.Text = "On Time"
                    tbStatusHour.ForeColor = Color.Orange
                ElseIf Val(tbToAchieveHour.Text) < Val(tbPlannedHour.Text) Then
                    tbStatusHour.Text = "Excess Productivity"
                    tbStatusHour.ForeColor = Color.Green
                End If

                If nRowNo = dsSelCapacity.Tables(0).Rows.Count - 1 Then
                    sInfo = "Hourly Production"
                    nRowNo = 0
                Else
                    sInfo = "Capacity Status"
                    nRowNo = nRowNo + 1
                End If

            End If

            grdConveyorProduction.DataSource = myccSARACProduction.LoadAllConveyorProduction(Format(Date.Now, "dd-MMM-yyyy"))

            With grdConveyorProductionV1
                .Columns(0).VisibleIndex = -1
                .Columns(2).VisibleIndex = -1
            End With



        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub



 

    Dim sVidoeDisplay As String = "N"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If sVidoeDisplay = "N" Then
            plVideos.Visible = True
            plVideos.BringToFront()
            nBalanceDuration = 1200
            sVidoeDisplay = "Y"
            PlayVideo()
            Button1.Text = "Stop Video"
            lblTrainingTimer.Visible = True
            lblSeconds.Visible = False
        ElseIf sVidoeDisplay = "Y" Then
            plVideos.Visible = False
            nBalanceDuration = 30
            sVidoeDisplay = "N"
            Button1.Text = "Play Video"
            Me.AxWindowsMediaPlayer1.Ctlcontrols.stop()
            lblTrainingTimer.Visible = False
            lblSeconds.Visible = True
        End If

    End Sub

    Private Sub PlayVideo()
        Try

            If cbxVideo.Text = "Marking 1" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\Noor\SARA C\Marking 1.mp4"
            ElseIf cbxVideo.Text = "Re Cutting" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\Noor\SARA C\Re Cutting.mp4"
            ElseIf cbxVideo.Text = "Skiving 1" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\Noor\SARA C\Skiving 1.mp4"
            ElseIf cbxVideo.Text = "Skiving 2" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\Noor\SARA C\Skiving 2.mp4"
            ElseIf cbxVideo.Text = "Stitching Deco 1" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\Noor\SARA C\Stitching Deco 1.mp4"
            End If
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub LoadOption()
        cbxOption.DataSource = Nothing : cbxOption.Items.Clear()
        cbxOption.DataSource = myccSARACProduction.LoadOption
        cbxOption.DisplayMember = "PlanWeek"
        cbxOption.ValueMember = "FilterOption"
    End Sub

    Dim sIsDetailMode As String = "N"
    Dim sIsAutoFilter As String = "N"

    Private Sub cbDetailMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDetailMode.Click

        If chkbxAutoLoad.Checked = True Then
            sIsAutoFilter = "Y"
        Else
            sIsAutoFilter = "N"
        End If


        If cbDetailMode.Text = "Detail Mode" Then
            nDetailModeDuration = 1800
            sIsDetailMode = "Y"
            cbDetailMode.Text = "Summary Mode"
            lblDetailModeTimer.Visible = True
            nSelectRowNo = 0
            LoadWeeklyPlanDetailMode()
        ElseIf cbDetailMode.Text = "Summary Mode" Then
            sIsDetailMode = "N"
            cbDetailMode.Text = "Detail Mode"
            lblDetailModeTimer.Visible = False
        End If


    End Sub
    Public nSelectRowNo As Integer
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Label2.Text = Format(Date.Now, "HH:mm:ss")
        lblTime.Text = Format(Date.Now, "HH:mm:ss")

        If sIsDetailMode = "N" Then
            If nBalanceDuration = 30 Then
                LoadDailyProduction()
            End If
            lblSeconds.Text = nBalanceDuration
            lblTrainingTimer.Text = nBalanceDuration
            nBalanceDuration = nBalanceDuration - 1


            If nBalanceDuration < 0 Then
                nBalanceDuration = 30
                If sVidoeDisplay = "Y" Then
                    plVideos.Visible = False
                    sVidoeDisplay = "N"
                    Button1.Text = "Play Video"
                    Me.AxWindowsMediaPlayer1.Ctlcontrols.stop()
                End If
            End If
        Else
            lblDetailModeTimer.Text = nDetailModeDuration
            nDetailModeDuration = nDetailModeDuration - 1

            If nBalanceDuration = 30 Then
                LoadWeeklyPlanDetailMode()
            End If
            lblSeconds.Text = nBalanceDuration
            nBalanceDuration = nBalanceDuration - 1


            If nBalanceDuration < 0 Then
                nBalanceDuration = 30
                If nDetailModeDuration < 0 Then
                    sIsDetailMode = "N"
                    cbDetailMode.Text = "Detail Mode"
                    lblDetailModeTimer.Visible = False
                End If
            End If
        End If
    End Sub

    Public sInfoWithFilter, sInfoWithFilterCategory As String
    Dim sWeekNo, sConveyorNo As String
    Private Sub LoadWeeklyPlanDetailMode()
        Try

            If sIsAutoFilter = "Y" Then
                myccSARACProduction.LoadOptionForFilter()
            Else
                sInfoWithFilterCategory = cbxOption.SelectedValue
                sInfoWithFilter = cbxOption.Text
            End If


            If sInfoWithFilterCategory = "Hourly Production" Then
                lblDataInfo.Text = "Hourly Production"
                grdConveyorProduction.Visible = True
                grdConveyorProduction.BringToFront()
                grdWeeklyPlan.Visible = False
                grdConveyorProduction.DataSource = myccSARACProduction.LoadAllConveyorProduction(Format(Date.Now, "dd-MMM-yyyy"))

                With grdConveyorProductionV1
                    .Columns(0).VisibleIndex = -1
                    .Columns(2).VisibleIndex = -1

                    .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(5).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                End With
            Else

                grdWeeklyPlan.Visible = True
                grdWeeklyPlan.BringToFront()
                grdConveyorProduction.Visible = False

                ' If sInfoWithFilter = "All week" Then

                ' ElseIf sInfoWithFilter = "Week" Then

                If sInfoWithFilterCategory = "All Week" Then
                    sWeekNo = "" : sConveyorNo = ""
                    lblDataInfo.Text = "[ Filter Mode ] Weekly Plan - All Week"
                ElseIf sInfoWithFilterCategory = "Week" Then
                    sWeekNo = sInfoWithFilter : sConveyorNo = ""
                    lblDataInfo.Text = "[ Filter Mode ] Weekly Plan - Week No " + sInfoWithFilter
                ElseIf sInfoWithFilterCategory = "All Conveyors" Then
                    sWeekNo = "" : sConveyorNo = ""
                    lblDataInfo.Text = "[ Filter Mode ] Weekly Plan - For "
                ElseIf sInfoWithFilterCategory = "Conveyor" Then
                    sWeekNo = "" : sConveyorNo = sInfoWithFilter
                    lblDataInfo.Text = "[ Filter Mode ] Weekly Plan - Conveyor No" + sConveyorNo
                ElseIf sInfoWithFilterCategory = "Week + Conveyor" Then
                    sWeekNo = Microsoft.VisualBasic.Left(sInfoWithFilter, 4) : sConveyorNo = Microsoft.VisualBasic.Right(sInfoWithFilter, Len(sInfoWithFilter) - 7)
                    lblDataInfo.Text = "[ Filter Mode ] Weekly Plan - Week No " + sWeekNo + " &  For " + sConveyorNo
                End If

                grdWeeklyPlan.DataSource = myccSARACProduction.LoadWeeklyPlanWithFilter(sWeekNo, sConveyorNo)



                With grdWeeklyPlanV1

                    .Columns(3).VisibleIndex = -1
                    .Columns(4).VisibleIndex = -1
                    .Columns(9).VisibleIndex = -1

                    .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                    .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


                End With


                sInfo = "Hourly Production"

            End If

            nSelectRowNo = nSelectRowNo + 1


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPause.Click
        If cbPause.Text = "Pause" Then
            Timer1.Stop()
            cbPause.Text = "Resume"
        ElseIf cbPause.Text = "Resume" Then
            Timer1.Start()
            cbPause.Text = "Pause"
        End If
    End Sub
End Class