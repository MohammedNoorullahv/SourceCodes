Imports System.Data.SqlClient

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports Microsoft.Office.Interop.Excel


Public Class frmReport
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

    Dim rrptArticle As New rptArticle
    Dim rrptInvoice As New rptInvoice
    Dim rrptOutstanding As New rptOutstanding
    Dim rrptDitpatchDetails As New rptDitpatchDetails
    Dim rrptSalesOrder As New rptSalesOrder
    Dim rrptSalesOrderMain As New rptSalesOrderMain
    Dim rrptPurchaseOrder As New rptPurchaseOrder
    Dim rrptPurchaseOrderMain1 As New rptPurchaseOrderMain
    Dim rrptPurchaseOrderSummary As New rptPurchaseOrderSummary
    Dim rrptSalesAnalysis As New rptSalesAnalysis
    Dim rrptOrderPlanning As New rptOrderPlanning
    Dim rrptERPTrackingv1 As New rptERPTrackingv1
    Dim rrptERPTrackingv1Details As New rptERPTrackingv1Details
    Dim rrptERPTrackingv1DtlsNFormat As New rptERPTrackingv1DtlsNFormat
    Dim rrptSpoolInfo As New rptSpoolInfo
    Dim rrptSpoolInfoBoxWise As New rptSpoolInfoBoxWise
    Dim rrptSpoolInfoBoxWiseinA4 As New rptSpoolInfoBoxWiseinA4
    Dim rrptProductStock As New rptProductStock



    Private Sub frmReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim dsSelReport As New DataSet
            Dim dsSelReport1 As New DataSet
            Dim dsSelReport2 As New DataSet

            If mdlSGM.sReportType = "Article" Then

                Dim daSelReport As New SqlDataAdapter("Select * from SolarArticleMaster4SGM4Print Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptArticle = New rptArticle

                rrptArticle.Database.Tables("SolarArticleMaster4SGM4Print").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptArticle


            ElseIf mdlSGM.sReportType = "Invoice" Then

                Dim daSelReport As New SqlDataAdapter("Select * from SolarInvoice4SGM4Print Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptInvoice = New rptInvoice

                rrptInvoice.Database.Tables("SolarInvoice4SGM4Print").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptInvoice

            ElseIf mdlSGM.sReportType = "Purchase Invoice" Then

                Dim daSelReport As New SqlDataAdapter("Select * from SolarPurchaseInvoice4SGM4Print Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptPurchaseOrderSummary = New rptPurchaseOrderSummary

                rrptPurchaseOrderSummary.Database.Tables("SolarPurchaseInvoice4SGM4Print").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptPurchaseOrderSummary

            ElseIf mdlSGM.sReportType = "Outstanding" Then

                Dim daSelReport As New SqlDataAdapter("Select * from SolarOutstanding4SGM4Print Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptOutstanding = New rptOutstanding

                rrptOutstanding.Database.Tables("SolarOutstanding4SGM4Print").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptOutstanding

            ElseIf mdlSGM.sReportType = "Deatails of Dispatch" Then

                Dim daSelReport As New SqlDataAdapter("Select * from vw_SolarDispatchDetails Where SalesOrderNo = '" & mdlSGM.sSelectedSalesOrderNo & _
                                                      "' And Article = '" & mdlSGM.sSelectedArticle & "' Order By Article", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptDitpatchDetails = New rptDitpatchDetails

                rrptDitpatchDetails.Database.Tables("vw_SolarDispatchDetails").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptDitpatchDetails
            ElseIf mdlSGM.sReportType = "Order Details" Then

                Dim daSelReport As New SqlDataAdapter("Select * from vw_SolarOrderWithInvoiceDetailforSGM Order By SlNo", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptSalesOrder = New rptSalesOrder

                rrptSalesOrder.Database.Tables("vw_SolarOrderWithInvoiceDetailforSGM").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptSalesOrder


            ElseIf mdlSGM.sReportType = "Order Header" Then

                Dim daSelReport As New SqlDataAdapter("Select * from vw_SolarOrderWithInvoiceDetailforSGM Order By SlNo", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptSalesOrderMain = New rptSalesOrderMain

                rrptSalesOrderMain.Database.Tables("vw_SolarOrderWithInvoiceDetailforSGM").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptSalesOrderMain


            ElseIf mdlSGM.sReportType = "Purchase Details" Then

                Dim daSelReport As New SqlDataAdapter("Select * from vw_PurchaseOrderforSGM Order By SlNo", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptPurchaseOrder = New rptPurchaseOrder

                rrptPurchaseOrder.Database.Tables("vw_PurchaseOrderforSGM").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptPurchaseOrder

            ElseIf mdlSGM.sReportType = "Purchase Header" Then

                Dim daSelReport As New SqlDataAdapter("Select * from vw_PurchaseOrderforSGM Order By SlNo", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptPurchaseOrderMain1 = New rptPurchaseOrderMain

                rrptPurchaseOrderMain1.Database.Tables("vw_PurchaseOrderforSGM").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptPurchaseOrderMain1

            ElseIf mdlSGM.sReportType = "SALES ANALYSIS REPORT" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempSalesAnalysis Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptSalesAnalysis = New rptSalesAnalysis

                rrptSalesAnalysis.Database.Tables("TempSalesAnalysis").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptSalesAnalysis

            ElseIf mdlSGM.sReportType = "ORDER PLANNING REPORT" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempOrderPlanning Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptOrderPlanning = New rptOrderPlanning

                rrptOrderPlanning.Database.Tables("TempOrderPlanning").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptOrderPlanning
            ElseIf mdlSGM.sReportType = "ERP TRACKING SYSTEM" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempERPTrackingSystem Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptERPTrackingv1 = New rptERPTrackingv1

                rrptERPTrackingv1.Database.Tables("TempERPTrackingSystem").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptERPTrackingv1
            ElseIf mdlSGM.sReportType = "ERP TRACKING SYSTEM DETAILS" Then
                Dim daSelReport As New SqlDataAdapter("Select * from TempERPTrackingSystem Order By PKID", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptERPTrackingv1DtlsNFormat = New rptERPTrackingv1DtlsNFormat

                rrptERPTrackingv1DtlsNFormat.Database.Tables("TempERPTrackingSystem").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptERPTrackingv1DtlsNFormat

            ElseIf mdlSGM.sReportType = "SPOOL INFO" Then
                'Dim daSelReport As New SqlDataAdapter("Select * from vw_SpoolDetails where SpoolId = '" & mdlSGM.sSelectedOption & "' Order By JobcardNo", Con)
                'daSelReport.Fill(dsSelReport, "Article")

                'rrptSpoolInfo = New rptSpoolInfo

                'rrptSpoolInfo.Database.Tables("vw_SpoolDetails").SetDataSource(dsSelReport.Tables("Article"))
                'crVeiwer.ReportSource = rrptSpoolInfo

                Dim daSelReport As New SqlDataAdapter("Select * from TempSpoolPrintInfo where SpoolId = '" & mdlSGM.sSelectedOption & "' Order By JobcardNo", Con)
                daSelReport.Fill(dsSelReport, "Article")

                If frmSpoolManagingTools.rbA5.Checked = True Then
                    rrptSpoolInfoBoxWise = New rptSpoolInfoBoxWise

                    rrptSpoolInfoBoxWise.Database.Tables("TempSpoolPrintInfo").SetDataSource(dsSelReport.Tables("Article"))
                    crVeiwer.ReportSource = rrptSpoolInfoBoxWise
                Else
                    rrptSpoolInfoBoxWiseinA4 = New rptSpoolInfoBoxWiseinA4

                    rrptSpoolInfoBoxWiseinA4.Database.Tables("TempSpoolPrintInfo").SetDataSource(dsSelReport.Tables("Article"))
                    crVeiwer.ReportSource = rrptSpoolInfoBoxWiseinA4
                End If

            ElseIf mdlSGM.sReportType = "PRODUCT STOCK" Then
                Dim daSelReport As New SqlDataAdapter("Select * from vw_SGMDailyProductStock Where WIPLocation = '" & mdlSGM.sSelectedOption & _
                                                      "' Order By WIPLocation, BuyerGroupCode, MaterialName, JobCardNo, Color", Con)
                daSelReport.Fill(dsSelReport, "Article")

                rrptProductStock = New rptProductStock

                rrptProductStock.Database.Tables("vw_SGMDailyProductStock").SetDataSource(dsSelReport.Tables("Article"))
                crVeiwer.ReportSource = rrptProductStock
            End If



        Catch Exp As Exception
            'HandleException(Me.Name, Exp)
        End Try

    End Sub


    Private Sub cbQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbQuit.Click
        Me.Close()
    End Sub

  
End Class