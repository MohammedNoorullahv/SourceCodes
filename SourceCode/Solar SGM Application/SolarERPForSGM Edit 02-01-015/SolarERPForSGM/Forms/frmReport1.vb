Imports System.Data.SqlClient

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports Microsoft.Office.Interop.Excel


Public Class frmReport1
    Dim Con As String = Global.SolarERPForSGM.My.Settings.AHGroup ' ptimizerERP_for_KHLI.My.Settings.OptimizerString
    Dim sCon As New SqlConnection(Con)

    Dim sFKJobCard, sFKWorkOrderNo As String


    ''Crystal Reports Parameter''
    Dim myParams As New ParameterFields()
    Dim myToParams As New ParameterFields()
    Dim myParam As New ParameterField()
    Dim myToParam As New ParameterField()
    Dim myDiscreteValue As New ParameterDiscreteValue()
    Dim myToDiscreteValue As New ParameterDiscreteValue()
    ''Crystal Reports Parameter''

    
    Dim rrptOrderPlanningSummary As New rptOrderPlanningSummary
    Dim rrptERPTrackingv1WS As New rptERPTrackingv1WS
    Dim rrptSpoolInfoBoxWiseinA4 As New rptSpoolInfoBoxWiseinA4
    Dim rrptProductStockSummary As New rptProductStockSummary

    Private Sub frmReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim dsSelReport As New DataSet
            Dim dsSelReport1 As New DataSet
            Dim dsSelReport2 As New DataSet

            If mdlSGM.sReportType = "ORDER PLANNING REPORT" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempOrderPlanningSummary Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptOrderPlanningSummary = New rptOrderPlanningSummary

                rrptOrderPlanningSummary.Database.Tables("TempOrderPlanningSummary").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptOrderPlanningSummary
            ElseIf mdlSGM.sReportType = "ERP TRACKING SYSTEM WS" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempERPTrackingWS where IPAddress = '" & mdlSGM.strIPAddress & _
                                                      "' Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptERPTrackingv1WS = New rptERPTrackingv1WS

                rrptERPTrackingv1WS.Database.Tables("TempERPTrackingWS").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptERPTrackingv1WS
            ElseIf mdlSGM.sReportType = "SPOOL INFO" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempSpoolPrintInfo where SpoolId = '" & mdlSGM.sSelectedOption & "' Order By JobcardNo", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptSpoolInfoBoxWiseinA4 = New rptSpoolInfoBoxWiseinA4

                rrptSpoolInfoBoxWiseinA4.Database.Tables("TempSpoolPrintInfo").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptSpoolInfoBoxWiseinA4
            ElseIf mdlSGM.sReportType = "PRODUCT STOCK" Then
                Dim daSelReport As New SqlDataAdapter("Select * from vw_SGMDailyProductStock Where WIPLocation = '" & mdlSGM.sSelectedOption & _
                                                      "' Order By WIPLocation, BuyerGroupCode, MaterialName, JobCardNo, Color", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptProductStockSummary = New rptProductStockSummary

                rrptProductStockSummary.Database.Tables("vw_SGMDailyProductStock").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptProductStockSummary
            End If



        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
        End Try

    End Sub


    Private Sub cbQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbQuit.Click
        Me.Close()
    End Sub


End Class