﻿Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmERPTrackingSystemv2
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccOrderPlanningReport As New ccOrderPlanningReport
    Dim myccERPTrackingv1 As New ccERPTrackingv1
    Dim mystrERPTrackingV1 As New strERPTrackingV1
    Dim mystrERPTrackingV2 As New strERPTrackingV2
    Dim sDateChanged As String

#Region "Declaration"
    Dim nPKID As Integer
    Dim nSlNo As Integer
    Dim sID As String
    Dim sSalesOrderNo As String
    Dim sCustomerOrderNo As String
    Dim dOrderReceivedDate As Date
    Dim sBuyerName As String
    Dim sArticle As String
    Dim sArticleName As String
    Dim nOrderQuantity As Integer
    Dim nPrice As Decimal
    Dim nOrderValue As Decimal
    Dim dExpectedDeliveryDate As Date
    Dim sShipmentStatus As String
    Dim sCodificationNew As String
    Dim nDispatch As Integer
    Dim nBalance As Integer
    Dim sOrderStatus As String
    Dim sArticleMould As String
    Dim sAssortmentName As String
    Dim sRowInfo As String
    Dim sSize01 As String
    Dim sSize02 As String
    Dim sSize03 As String
    Dim sSize04 As String
    Dim sSize05 As String
    Dim sSize06 As String
    Dim sSize07 As String
    Dim sSize08 As String
    Dim sSize09 As String
    Dim sSize10 As String
    Dim sSize11 As String
    Dim sSize12 As String
    Dim sSize13 As String
    Dim sSize14 As String
    Dim sSize15 As String
    Dim sSize16 As String
    Dim sSize17 As String
    Dim sSize18 As String

    Dim nSODOrderQuantity, nSODQuantity01, nSODQuantity02, nSODQuantity03, nSODQuantity04, nSODQuantity05, nSODQuantity06, nSODQuantity07, nSODQuantity08, nSODQuantity09, nSODQuantity10 As Integer
    Dim nSODQuantity11, nSODQuantity12, nSODQuantity13, nSODQuantity14, nSODQuantity15, nSODQuantity16, nSODQuantity17, nSODQuantity18 As Integer

#End Region


    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub

    Dim ngrdRowCount As Integer

    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        LoadComboItems()
        ' sDateChanged = "Y"
        sDateChanged = "N"
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        If rbYes.Checked = True Then
            'grdSalesOrderv1.ExportToXlsx("E:\ERP Tracking System.xlsx")
        Else
            grdSalesOrderDetailsV1.ExportToXlsx("E:\ERP Tracking System Details.xlsx")
        End If

        MsgBox("Export Completed")

    End Sub

    Dim sTypeofOrder, sTypeofDocument As String
    Dim sSelOption As String
    Dim nIsEDDNegotiable As Integer

    Private Sub LoadData()

        mdlSGM.sSelectedOption = ""

        If rbGroupByArticle.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "O"
        End If

        If rbSynthetic.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "S"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        End If

        If rbYes.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "D"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "S"
        End If

        If rbOrderDate.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "0"
            mdlSGM.sSortType = "0"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "1"
            mdlSGM.sSortType = "1"
        End If


        If cbxCustomer.Text = " ALL CUSTOMERS" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxArticleMould.Text = " ALL ARTICLES" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If Trim(tbArticleDescription.Text) = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxProductionStatus.Text = "ALL DATA" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
            If Trim(cbxProductionStatus.Text) = "COMPLETED" Then
                sOrderStatus = "SHIPPED"
            ElseIf Trim(cbxProductionStatus.Text) = "PENDING" Then
                sOrderStatus = "OPEN"
            ElseIf Trim(cbxProductionStatus.Text) = "Closed" Then
                sOrderStatus = "CLOSE"
            Else
                sOrderStatus = ""
            End If
        End If

        If cbxShipmentStatus.Text = "ALL DATA" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxProductType.Text = " ALL PRODUCT TYPE" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If


       

        sIsLoaded = "N"
        Dim i As Integer = 0

        If rbGroupByOrder.Checked = True Then

            grdSalesOrder.BringToFront()
Ab:
            ngrdRowCount = grdSalesOrderV1.RowCount
            For i = 0 To ngrdRowCount
                grdSalesOrderV1.DeleteRow(i)
            Next
            ngrdRowCount = grdSalesOrderV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If
            sIsLoaded = "Y"

            mystrERPTrackingV1.FromDate = Format(dpFrom.Value, "dd-MMM-yyyy")
            mystrERPTrackingV1.ToDate = Format(dpTo.Value, "dd-MMM-yyyy")
            mystrERPTrackingV1.Customer = Trim(cbxCustomer.Text)
            mystrERPTrackingV1.ArticleMould = Trim(cbxArticleMould.Text)
            mystrERPTrackingV1.ArticleDescription = Trim(tbArticleDescription.Text)
            mystrERPTrackingV1.ProductionStatus = sOrderStatus
            mystrERPTrackingV1.ShipmentStatus = Trim(cbxShipmentStatus.Text)
            mystrERPTrackingV1.ProductType = Trim(cbxProductType.Text)

            grdSalesOrder.DataSource = myccERPTrackingv1.LoadSalesOrderDetailsv2(mystrERPTrackingV1, mdlSGM.strIPAddress)

            'With grdSalesOrderv1

            '    .Columns(14).VisibleIndex = -1

            '    .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            '    .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '    .Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            '    .Columns(13).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '    .Columns(13).DisplayFormat.FormatString = "dd-MMM-yyyy"

            '    .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '    .Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '    .Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '    .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '    .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            'End With
        Else
            i = 0
            grdSalesOrderGBArticle.BringToFront()

Ac:
            ngrdRowCount = grdSalesOrderGBArticleV1.RowCount
            For i = 0 To ngrdRowCount
                grdSalesOrderGBArticleV1.DeleteRow(i)
            Next
            ngrdRowCount = grdSalesOrderGBArticleV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ac
            End If
            sIsLoaded = "Y"

            mystrERPTrackingV1.FromDate = Format(dpFrom.Value, "dd-MMM-yyyy")
            mystrERPTrackingV1.ToDate = Format(dpTo.Value, "dd-MMM-yyyy")
            mystrERPTrackingV1.Customer = Trim(cbxCustomer.Text)
            mystrERPTrackingV1.ArticleMould = Trim(cbxArticleMould.Text)
            mystrERPTrackingV1.ArticleDescription = Trim(tbArticleDescription.Text)
            mystrERPTrackingV1.ProductionStatus = sOrderStatus
            mystrERPTrackingV1.ShipmentStatus = Trim(cbxShipmentStatus.Text)
            mystrERPTrackingV1.ProductType = Trim(cbxProductType.Text)

            grdSalesOrderGBArticle.DataSource = myccERPTrackingv1.LoadSalesOrderDetailsv2(mystrERPTrackingV1, mdlSGM.strIPAddress)

            With grdSalesOrderGBArticleV1

                '.Columns(14).VisibleIndex = -1
                '.Columns(15).VisibleIndex = 11
                '.Columns(16).VisibleIndex = 12
                '.Columns(17).VisibleIndex = 13
                '.Columns(18).VisibleIndex = 14

                '.Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                '.Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '.Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
                '.Columns(13).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '.Columns(13).DisplayFormat.FormatString = "dd-MMM-yyyy"

                '.Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(16).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(17).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                '.Columns(18).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            End With
        End If

        MsgBox("Loading Completed")
        sIsLoaded = "Y"
    End Sub

    Private Sub dpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpTo.ValueChanged, dpFrom.ValueChanged

        LoadComboItems()
        sDateChanged = "Y"
    End Sub

    Private Sub LoadComboItems()

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        cbxCustomer.DataSource = myccOrderPlanningReport.LoadCustomer(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxCustomer.DisplayMember = "BuyerName"

        cbxArticleMould.DataSource = myccOrderPlanningReport.LoadArticles(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxArticleMould.DisplayMember = "SoleName"

        cbxProductionStatus.SelectedIndex = 0

        cbxShipmentStatus.SelectedIndex = 0

        cbxProductType.DataSource = myccOrderPlanningReport.LoadProductType(mdlSGM.dFromDate, mdlSGM.dToDate, "")
        cbxProductType.DisplayMember = "SoleName"

    End Sub

    

    Dim ngrdRowNo, nBal2Dispatch As Integer
    Dim sSalesOrderDetailsID, sIsNegotiable, sIsLoaded As String
    Dim WeekNumber As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        If sDateChanged = "Y" Then
            GenerateData()
        End If
        LoadData()
    End Sub

    Dim nMouldQty, nM2FQty, nFinishQty, nPkdStockQty, nRejectionQty As Integer
    

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim daDelOrderPlanning As New SqlDataAdapter("Delete from TempERPTrackingSystem", sConstr)
        Dim dsDelOrderPlanning As New DataSet
        daDelOrderPlanning.Fill(dsDelOrderPlanning)
        dsDelOrderPlanning.AcceptChanges()

        Dim i As Integer = 0
        If rbYes.Checked = False Then
            'For i = 0 To grdSalesOrderv1.RowCount - 1
            '    sSalesOrderNo = grdSalesOrderv1.GetDataRow(i).Item("SalesOrderNo").ToString
            '    sCustomerOrderNo = grdSalesOrderv1.GetDataRow(i).Item("CustomerOrderNo").ToString
            '    sBuyerName = grdSalesOrderv1.GetDataRow(i).Item("BuyerName").ToString
            '    sArticle = grdSalesOrderv1.GetDataRow(i).Item("Article").ToString
            '    sArticleName = grdSalesOrderv1.GetDataRow(i).Item("ArticleName").ToString
            '    sOrderStatus = grdSalesOrderv1.GetDataRow(i).Item("OrderStatus").ToString

            '    nOrderQuantity = Val(grdSalesOrderv1.GetDataRow(i).Item("OrderQuantity").ToString)
            '    nDispatch = Val(grdSalesOrderv1.GetDataRow(i).Item("Dispatch").ToString)
            '    nBalance = Val(grdSalesOrderv1.GetDataRow(i).Item("Balance").ToString)
            '    nMouldQty = 0
            '    nM2FQty = 0
            '    nFinishQty = 0
            '    nPkdStockQty = 0
            '    nRejectionQty = 0

            '    nPrice = Val(grdSalesOrderv1.GetDataRow(i).Item("Price").ToString)
            '    nOrderValue = Val(grdSalesOrderv1.GetDataRow(i).Item("OrderValue").ToString)


            '    dOrderReceivedDate = grdSalesOrderv1.GetDataRow(i).Item("OrderReceivedDate").ToString
            '    dExpectedDeliveryDate = grdSalesOrderv1.GetDataRow(i).Item("ExpectedDeliveryDate").ToString

            '    InsertERPTracking()
            'Next

            mdlSGM.sReportType = "ERP TRACKING SYSTEM"
        Else

            i = 0
            For i = 0 To grdSalesOrderDetailsV1.RowCount - 1
                sSalesOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("SalesOrderNo").ToString
                sCustomerOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("CustomerOrderNo").ToString
                sBuyerName = grdSalesOrderDetailsV1.GetDataRow(i).Item("BuyerName").ToString
                sArticle = grdSalesOrderDetailsV1.GetDataRow(i).Item("Article").ToString
                sArticleName = grdSalesOrderDetailsV1.GetDataRow(i).Item("ArticleName").ToString
                sOrderStatus = grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderStatus").ToString

                nOrderQuantity = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderQuantity").ToString)
                nDispatch = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Dispatch").ToString)
                nBalance = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Balance").ToString)
                nMouldQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("MouldQty").ToString)
                nM2FQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("M2FQty").ToString)
                nFinishQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("FinishQty").ToString)
                nPkdStockQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("PkdStockQty").ToString)
                nRejectionQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("RejectionQty").ToString)

                nPrice = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Price").ToString)
                nOrderValue = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderValue").ToString)


                dOrderReceivedDate = grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderReceivedDate").ToString
                dExpectedDeliveryDate = grdSalesOrderDetailsV1.GetDataRow(i).Item("ExpectedDeliveryDate").ToString

                InsertERPTracking()
            Next

            mdlSGM.sReportType = "ERP TRACKING SYSTEM DETAILS"
        End If
        frmReport.Show()
    End Sub

    Private Sub InsertERPTracking()
        Dim daInsSalesAnalysis As New SqlDataAdapter("Insert Into TempERPTrackingSystem Values ('" & sSalesOrderNo & "','" & sCustomerOrderNo & _
                                                     "','" & Format(dOrderReceivedDate, "dd-MMM-yy") & "','" & sBuyerName & _
                                                     "','" & sArticle & "','" & sArticleName & "','" & nOrderQuantity & _
                                                     "','" & nPrice & "','" & nOrderValue & "','" & nDispatch & "','" & nBalance & _
                                                     "','" & nMouldQty & "','" & nM2FQty & "','" & nFinishQty & "','" & nPkdStockQty & _
                                                     "','" & sOrderStatus & "','" & nRejectionQty & _
                                                     "','" & Format(dExpectedDeliveryDate, "dd-MMM-yy") & "')", sConstr)
        Dim dsInsSalesAnalysis As New DataSet
        daInsSalesAnalysis.Fill(dsInsSalesAnalysis)
        dsInsSalesAnalysis.AcceptChanges()

    End Sub

    Private Sub GenerateData()
        pgbar.Visible = True
        pgbar.BringToFront()
        Dim i As Integer = 0
        grdSalesOrderDetails.BringToFront()
Ac:
        ngrdRowCount = grdSalesOrderDetailsV1.RowCount
        For i = 0 To ngrdRowCount
            grdSalesOrderDetailsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdSalesOrderDetailsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ac
        End If
        sIsLoaded = "Y"

        grdSalesOrderDetails.DataSource = myccERPTrackingv1.LoadData(Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"))

        With grdSalesOrderDetailsV1

            .Columns(14).VisibleIndex = -1
            .Columns(15).VisibleIndex = 11
            .Columns(16).VisibleIndex = 12
            .Columns(17).VisibleIndex = 13
            .Columns(18).VisibleIndex = 14

            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(13).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(13).DisplayFormat.FormatString = "dd-MMM-yyyy"

            '.Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(16).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(17).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(18).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        End With

        InsertData()
        'pgbar.Visible = False
    End Sub

    Private Sub InsertData()
        Dim daDelTempData As New SqlDataAdapter("Delete from TempERPTrackingSystemv2 where IPAddress = '" & mdlSGM.strIPAddress & "'", sConstr)
        Dim dsDelTempData As New DataSet
        daDelTempData.Fill(dsDelTempData)
        dsDelTempData.AcceptChanges()

        pgbar.Minimum = 0
        

        Dim i As Integer = 0
        Dim x As Single
        Dim y As Single

        Dim gr As Graphics = Me.pgbar.CreateGraphics
        Dim percentage As String
        Dim sz As SizeF = gr.MeasureString(percentage, Me.pgbar.Font, Me.pgbar.Width)

        x = (Me.pgbar.Width / 2) - (sz.Width / 2)
        y = (Me.pgbar.Height / 2) - (sz.Height / 2)

        gr.DrawString(percentage, pgbar.Font, Brushes.Black, x, y)

        For i = 0 To grdSalesOrderDetailsV1.RowCount - 1

            If grdSalesOrderDetailsV1.RowCount = 0 Then
                pgbar.Maximum = 0
            Else
                pgbar.Maximum = grdSalesOrderDetailsV1.RowCount - 1
            End If
            
            pgbar.Value = i
            If i > 0 Then
                percentage = CType((pgbar.Value / pgbar.Maximum * 100), Integer).ToString & "%"
            End If
            gr.DrawString(percentage, pgbar.Font, Brushes.Red, x, y)

            'mystrERPTrackingV2.nPKID = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("PKID").ToString)
            mystrERPTrackingV2.nSlNo = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("SlNo").ToString)
            mystrERPTrackingV2.sID = grdSalesOrderDetailsV1.GetDataRow(i).Item("ID").ToString
            mystrERPTrackingV2.sSalesOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("SalesOrderNo").ToString
            sSalesOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("SalesOrderNo").ToString
            mystrERPTrackingV2.sCustomerOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("CustomerOrderNo").ToString
            mystrERPTrackingV2.dOrderReceivedDate = grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderReceivedDate").ToString
            mystrERPTrackingV2.sBuyerName = grdSalesOrderDetailsV1.GetDataRow(i).Item("BuyerName").ToString
            mystrERPTrackingV2.sArticle = grdSalesOrderDetailsV1.GetDataRow(i).Item("Article").ToString
            mystrERPTrackingV2.sArticleName = grdSalesOrderDetailsV1.GetDataRow(i).Item("ArticleName").ToString
            mystrERPTrackingV2.nOrderQuantity = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderQuantity").ToString)
            mystrERPTrackingV2.nPrice = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Price").ToString)
            mystrERPTrackingV2.nOrderValue = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderValue").ToString)
            mystrERPTrackingV2.dExpectedDeliveryDate = grdSalesOrderDetailsV1.GetDataRow(i).Item("ExpectedDeliveryDate").ToString
            mystrERPTrackingV2.sShipmentStatus = grdSalesOrderDetailsV1.GetDataRow(i).Item("ShipmentStatus").ToString
            mystrERPTrackingV2.sCodificationNew = grdSalesOrderDetailsV1.GetDataRow(i).Item("CodificationNew").ToString
            mystrERPTrackingV2.nDispatch = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Dispatch").ToString)
            mystrERPTrackingV2.nBalance = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Balance").ToString)
            mystrERPTrackingV2.sOrderStatus = grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderStatus").ToString
            mystrERPTrackingV2.sArticleMould = grdSalesOrderDetailsV1.GetDataRow(i).Item("ArticleMould").ToString
            mystrERPTrackingV2.sAssortmentName = grdSalesOrderDetailsV1.GetDataRow(i).Item("AssortmentName").ToString
            mystrERPTrackingV2.sRowInfo = "01. SIZES"
            mystrERPTrackingV2.sSize01 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size01").ToString
            mystrERPTrackingV2.sSize02 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size02").ToString
            mystrERPTrackingV2.sSize03 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size03").ToString
            mystrERPTrackingV2.sSize04 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size04").ToString
            mystrERPTrackingV2.sSize05 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size05").ToString
            mystrERPTrackingV2.sSize06 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size06").ToString
            mystrERPTrackingV2.sSize07 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size07").ToString
            mystrERPTrackingV2.sSize08 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size08").ToString
            mystrERPTrackingV2.sSize09 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size09").ToString
            mystrERPTrackingV2.sSize10 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size10").ToString
            mystrERPTrackingV2.sSize11 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size11").ToString
            mystrERPTrackingV2.sSize12 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size12").ToString
            mystrERPTrackingV2.sSize13 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size13").ToString
            mystrERPTrackingV2.sSize14 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size14").ToString
            mystrERPTrackingV2.sSize15 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size15").ToString
            mystrERPTrackingV2.sSize16 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size16").ToString
            mystrERPTrackingV2.sSize17 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size17").ToString
            mystrERPTrackingV2.sSize18 = grdSalesOrderDetailsV1.GetDataRow(i).Item("Size18").ToString
            mystrERPTrackingV2.sInvoiceNo = ""
            mystrERPTrackingV2.sIPAddress = mdlSGM.strIPAddress
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)

            ''Inserting Order Quantity''
            Dim daSelOrdQty As New SqlDataAdapter("Select * from SalesOrderDetails where Id = '" & mystrERPTrackingV2.sID & "'", sConstr)
            Dim dsSelOrdQty As New DataSet
            daSelOrdQty.Fill(dsSelOrdQty)

            mystrERPTrackingV2.nOrderQuantity = Val(dsSelOrdQty.Tables(0).Rows(0).Item("OrderQuantity"))
            mystrERPTrackingV2.sRowInfo = "02. ORDER QUANTITY"
            mystrERPTrackingV2.sSize01 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)

            nSODOrderQuantity = Val(dsSelOrdQty.Tables(0).Rows(0).Item("OrderQuantity"))
            nSODQuantity01 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity01"))
            nSODQuantity02 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity02"))
            nSODQuantity03 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity03"))
            nSODQuantity04 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity04"))
            nSODQuantity05 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity05"))
            nSODQuantity06 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity06"))
            nSODQuantity07 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity07"))
            nSODQuantity08 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity08"))
            nSODQuantity09 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity09"))
            nSODQuantity10 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity10"))
            nSODQuantity11 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity11"))
            nSODQuantity12 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity12"))
            nSODQuantity13 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity13"))
            nSODQuantity14 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity14"))
            nSODQuantity15 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity15"))
            nSODQuantity16 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity16"))
            nSODQuantity17 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity17"))
            nSODQuantity18 = Val(dsSelOrdQty.Tables(0).Rows(0).Item("Quantity18"))

            ''Inserting Order Quantity''

            ''Inserting Mould Completed Quantity''
            Dim daSelMouldQty As New SqlDataAdapter("SELECT IsNull(SUM(Quantity),0) AS Quantity, IsNull(SUM(Quantity01),0) AS Quantity01, IsNull(SUM(Quantity02),0) AS Quantity02, IsNull(SUM(Quantity03),0) AS Quantity03, IsNull(SUM(Quantity04),0) AS Quantity04, IsNull(SUM(Quantity05),0) AS Quantity05, IsNull(SUM(Quantity06),0) AS Quantity06, IsNull(SUM(Quantity07),0) AS Quantity07, IsNull(SUM(Quantity08),0) AS Quantity08, IsNull(SUM(Quantity09),0) AS Quantity09, IsNull(SUM(Quantity10),0) AS Quantity10, IsNull(SUM(Quantity11),0) AS Quantity11, IsNull(SUM(Quantity12),0) AS Quantity12, IsNull(SUM(Quantity13),0) AS Quantity13, IsNull(SUM(Quantity14),0) AS Quantity14, IsNull(SUM(Quantity15),0) AS Quantity15, IsNull(SUM(Quantity16),0) AS Quantity16, IsNull(SUM(Quantity17),0) AS Quantity17, IsNull(SUM(Quantity18),0) AS Quantity18 FROM PackingDetail Where MouldScanDate Is Not Null And JobCardNo LIKE '%'+ '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsSelMouldQty As New DataSet
            daSelMouldQty.Fill(dsSelMouldQty)

            mystrERPTrackingV2.nOrderQuantity = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sRowInfo = "03. MOULD QUANTITY"
            mystrERPTrackingV2.sSize01 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting Mould Completed Quantity''

            ''Inserting Mould Out Quantity''
            Dim daSelMouldOutQty As New SqlDataAdapter("SELECT IsNull(SUM(Quantity),0) AS Quantity, IsNull(SUM(Quantity01),0) AS Quantity01, IsNull(SUM(Quantity02),0) AS Quantity02, IsNull(SUM(Quantity03),0) AS Quantity03, IsNull(SUM(Quantity04),0) AS Quantity04, IsNull(SUM(Quantity05),0) AS Quantity05, IsNull(SUM(Quantity06),0) AS Quantity06, IsNull(SUM(Quantity07),0) AS Quantity07, IsNull(SUM(Quantity08),0) AS Quantity08, IsNull(SUM(Quantity09),0) AS Quantity09, IsNull(SUM(Quantity10),0) AS Quantity10, IsNull(SUM(Quantity11),0) AS Quantity11, IsNull(SUM(Quantity12),0) AS Quantity12, IsNull(SUM(Quantity13),0) AS Quantity13, IsNull(SUM(Quantity14),0) AS Quantity14, IsNull(SUM(Quantity15),0) AS Quantity15, IsNull(SUM(Quantity16),0) AS Quantity16, IsNull(SUM(Quantity17),0) AS Quantity17, IsNull(SUM(Quantity18),0) AS Quantity18 FROM PackingDetail Where MtoFScanDate Is Not Null And JobCardNo LIKE '%'+ '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsSelMouldOutQty As New DataSet
            daSelMouldOutQty.Fill(dsSelMouldOutQty)

            mystrERPTrackingV2.nOrderQuantity = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sRowInfo = "04. MOULD OUT QUANTITY"
            mystrERPTrackingV2.sSize01 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting Mould Out Quantity''

            ''Inserting Mould Balance Quantity''
            mystrERPTrackingV2.sRowInfo = "05. MOULD BAL QUANTITY"

            mystrERPTrackingV2.nOrderQuantity = nSODOrderQuantity - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sSize01 = nSODQuantity01 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = nSODQuantity02 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = nSODQuantity03 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = nSODQuantity04 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = nSODQuantity05 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = nSODQuantity06 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = nSODQuantity07 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = nSODQuantity08 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = nSODQuantity09 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = nSODQuantity10 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = nSODQuantity11 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = nSODQuantity12 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = nSODQuantity13 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = nSODQuantity14 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = nSODQuantity15 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = nSODQuantity16 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = nSODQuantity17 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = nSODQuantity18 - Val(dsSelMouldOutQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting Mould Balance Quantity''

            ''Inserting FINISH Quantity''
            Dim daSelFinishQty As New SqlDataAdapter("SELECT IsNull(SUM(Quantity),0) AS Quantity, IsNull(SUM(Quantity01),0) AS Quantity01, IsNull(SUM(Quantity02),0) AS Quantity02, IsNull(SUM(Quantity03),0) AS Quantity03, IsNull(SUM(Quantity04),0) AS Quantity04, IsNull(SUM(Quantity05),0) AS Quantity05, IsNull(SUM(Quantity06),0) AS Quantity06, IsNull(SUM(Quantity07),0) AS Quantity07, IsNull(SUM(Quantity08),0) AS Quantity08, IsNull(SUM(Quantity09),0) AS Quantity09, IsNull(SUM(Quantity10),0) AS Quantity10, IsNull(SUM(Quantity11),0) AS Quantity11, IsNull(SUM(Quantity12),0) AS Quantity12, IsNull(SUM(Quantity13),0) AS Quantity13, IsNull(SUM(Quantity14),0) AS Quantity14, IsNull(SUM(Quantity15),0) AS Quantity15, IsNull(SUM(Quantity16),0) AS Quantity16, IsNull(SUM(Quantity17),0) AS Quantity17, IsNull(SUM(Quantity18),0) AS Quantity18 FROM PackingDetail Where FinishScanDate Is Not Null And JobCardNo LIKE '%'+ '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsSelFinishQty As New DataSet
            daSelFinishQty.Fill(dsSelFinishQty)

            mystrERPTrackingV2.nOrderQuantity = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sRowInfo = "06. FINISH QUANTITY"
            mystrERPTrackingV2.sSize01 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting FINISH Quantity''

            ''Inserting FINISH Balance Quantity''
            mystrERPTrackingV2.sRowInfo = "07. FINISH BAL QUANTITY"

            mystrERPTrackingV2.nOrderQuantity = nSODOrderQuantity - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sSize01 = nSODQuantity01 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = nSODQuantity02 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = nSODQuantity03 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = nSODQuantity04 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = nSODQuantity05 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = nSODQuantity06 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = nSODQuantity07 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = nSODQuantity08 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = nSODQuantity09 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = nSODQuantity10 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = nSODQuantity11 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = nSODQuantity12 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = nSODQuantity13 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = nSODQuantity14 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = nSODQuantity15 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = nSODQuantity16 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = nSODQuantity17 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = nSODQuantity18 - Val(dsSelFinishQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting FINISH Balance Quantity''

            ''Inserting PACKING STOCK Quantity''
            Dim daSelPackingQty As New SqlDataAdapter("SELECT IsNull(SUM(Quantity),0) AS Quantity, IsNull(SUM(Quantity01),0) AS Quantity01, IsNull(SUM(Quantity02),0) AS Quantity02, IsNull(SUM(Quantity03),0) AS Quantity03, IsNull(SUM(Quantity04),0) AS Quantity04, IsNull(SUM(Quantity05),0) AS Quantity05, IsNull(SUM(Quantity06),0) AS Quantity06, IsNull(SUM(Quantity07),0) AS Quantity07, IsNull(SUM(Quantity08),0) AS Quantity08, IsNull(SUM(Quantity09),0) AS Quantity09, IsNull(SUM(Quantity10),0) AS Quantity10, IsNull(SUM(Quantity11),0) AS Quantity11, IsNull(SUM(Quantity12),0) AS Quantity12, IsNull(SUM(Quantity13),0) AS Quantity13, IsNull(SUM(Quantity14),0) AS Quantity14, IsNull(SUM(Quantity15),0) AS Quantity15, IsNull(SUM(Quantity16),0) AS Quantity16, IsNull(SUM(Quantity17),0) AS Quantity17, IsNull(SUM(Quantity18),0) AS Quantity18 FROM PackingDetail Where FinishScanDate Is Not Null And InvoiceNo = '' And JobCardNo LIKE '%'+ '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsSelPackingQty As New DataSet
            daSelPackingQty.Fill(dsSelPackingQty)

            mystrERPTrackingV2.nOrderQuantity = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sRowInfo = "08. PACK STOCK QUANTITY"
            mystrERPTrackingV2.sSize01 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = Val(dsSelPackingQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting PACKING STOCK Quantity''


            ''Inserting Dispatch Quantity''
            Dim daSelDispatchQty As New SqlDataAdapter("SELECT IsNull(SUM(Quantity),0) AS Quantity, IsNull(SUM(Quantity01),0) AS Quantity01, IsNull(SUM(Quantity02),0) AS Quantity02, IsNull(SUM(Quantity03),0) AS Quantity03, IsNull(SUM(Quantity04),0) AS Quantity04, IsNull(SUM(Quantity05),0) AS Quantity05, IsNull(SUM(Quantity06),0) AS Quantity06, IsNull(SUM(Quantity07),0) AS Quantity07, IsNull(SUM(Quantity08),0) AS Quantity08, IsNull(SUM(Quantity09),0) AS Quantity09, IsNull(SUM(Quantity10),0) AS Quantity10, IsNull(SUM(Quantity11),0) AS Quantity11, IsNull(SUM(Quantity12),0) AS Quantity12, IsNull(SUM(Quantity13),0) AS Quantity13, IsNull(SUM(Quantity14),0) AS Quantity14, IsNull(SUM(Quantity15),0) AS Quantity15, IsNull(SUM(Quantity16),0) AS Quantity16, IsNull(SUM(Quantity17),0) AS Quantity17, IsNull(SUM(Quantity18),0) AS Quantity18 FROM PackingDetail Where InvoiceNo <> '' And JobCardNo LIKE '%'+ '" & sSalesOrderNo & "' + '%'", sConstr)
            Dim dsSelDispatchQty As New DataSet
            daSelDispatchQty.Fill(dsSelDispatchQty)

            mystrERPTrackingV2.nOrderQuantity = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sRowInfo = "09. DISPATCH QUANTITY"
            mystrERPTrackingV2.sSize01 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity18"))
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting PACKING STOCK Quantity''

            ''Inserting Invoice Quantity''
            Dim daSelInvoiceQty As New SqlDataAdapter("SELECT InvoiceNo,InvoiceDate,IsNull(SUM(Quantity),0) AS Quantity, IsNull(SUM(Qty1),0) AS Quantity01, IsNull(SUM(Qty2),0) AS Quantity02, IsNull(SUM(Qty3),0) AS Quantity03, IsNull(SUM(Qty4),0) AS Quantity04, IsNull(SUM(Qty5),0) AS Quantity05, IsNull(SUM(Qty6),0) AS Quantity06, IsNull(SUM(Qty7),0) AS Quantity07,  IsNull(SUM(Qty8),0) AS Quantity08, IsNull(SUM(Qty9),0) AS Quantity09, IsNull(SUM(Qty10),0) AS Quantity10, IsNull(SUM(Qty11),0) AS Quantity11,  IsNull(SUM(Qty12),0) AS Quantity12, IsNull(SUM(Qty13),0) AS Quantity13, IsNull(SUM(Qty14),0) AS Quantity14, IsNull(SUM(Qty15),0) AS Quantity15,   IsNull(SUM(Qty16),0) AS Quantity16, IsNull(SUM(Qty17),0) AS Quantity17, IsNull(SUM(Qty18),0) AS Quantity18    FROM InvoiceDetail Where SalesOrderNo = '" & sSalesOrderNo & "'   Group By InvoiceNo,InvoiceDate   Order By InvoiceNo,InvoiceDate", sConstr)
            Dim dsSelInvoiceQty As New DataSet
            daSelInvoiceQty.Fill(dsSelInvoiceQty)
            Dim j As Integer = 0

            For j = 0 To dsSelInvoiceQty.Tables(0).Rows.Count - 1
                mystrERPTrackingV2.nOrderQuantity = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity"))
                mystrERPTrackingV2.sRowInfo = "10. INVOICE QUANTITY"
                mystrERPTrackingV2.sSize01 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity01"))
                mystrERPTrackingV2.sSize02 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity02"))
                mystrERPTrackingV2.sSize03 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity03"))
                mystrERPTrackingV2.sSize04 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity04"))
                mystrERPTrackingV2.sSize05 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity05"))
                mystrERPTrackingV2.sSize06 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity06"))
                mystrERPTrackingV2.sSize07 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity07"))
                mystrERPTrackingV2.sSize08 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity08"))
                mystrERPTrackingV2.sSize09 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity09"))
                mystrERPTrackingV2.sSize10 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity10"))
                mystrERPTrackingV2.sSize11 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity11"))
                mystrERPTrackingV2.sSize12 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity12"))
                mystrERPTrackingV2.sSize13 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity13"))
                mystrERPTrackingV2.sSize14 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity14"))
                mystrERPTrackingV2.sSize15 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity15"))
                mystrERPTrackingV2.sSize16 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity16"))
                mystrERPTrackingV2.sSize17 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity17"))
                mystrERPTrackingV2.sSize18 = Val(dsSelInvoiceQty.Tables(0).Rows(j).Item("Quantity18"))
                mystrERPTrackingV2.sInvoiceNo = dsSelInvoiceQty.Tables(0).Rows(j).Item("InvoiceNo")
                mystrERPTrackingV2.dInvoiceDt = dsSelInvoiceQty.Tables(0).Rows(j).Item("InvoiceDate")
                myccERPTrackingv1.InsertData(mystrERPTrackingV2)

                ''Inserting PACKING STOCK Quantity''
            Next


            ''Inserting PACKING Balance Quantity''
            mystrERPTrackingV2.sRowInfo = "11. DISPATCH BAL QUANTITY"

            mystrERPTrackingV2.nOrderQuantity = nSODOrderQuantity - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity"))
            mystrERPTrackingV2.sSize01 = nSODQuantity01 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity01"))
            mystrERPTrackingV2.sSize02 = nSODQuantity02 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity02"))
            mystrERPTrackingV2.sSize03 = nSODQuantity03 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity03"))
            mystrERPTrackingV2.sSize04 = nSODQuantity04 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity04"))
            mystrERPTrackingV2.sSize05 = nSODQuantity05 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity05"))
            mystrERPTrackingV2.sSize06 = nSODQuantity06 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity06"))
            mystrERPTrackingV2.sSize07 = nSODQuantity07 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity07"))
            mystrERPTrackingV2.sSize08 = nSODQuantity08 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity08"))
            mystrERPTrackingV2.sSize09 = nSODQuantity09 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity09"))
            mystrERPTrackingV2.sSize10 = nSODQuantity10 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity10"))
            mystrERPTrackingV2.sSize11 = nSODQuantity11 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity11"))
            mystrERPTrackingV2.sSize12 = nSODQuantity12 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity12"))
            mystrERPTrackingV2.sSize13 = nSODQuantity13 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity13"))
            mystrERPTrackingV2.sSize14 = nSODQuantity14 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity14"))
            mystrERPTrackingV2.sSize15 = nSODQuantity15 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity15"))
            mystrERPTrackingV2.sSize16 = nSODQuantity16 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity16"))
            mystrERPTrackingV2.sSize17 = nSODQuantity17 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity17"))
            mystrERPTrackingV2.sSize18 = nSODQuantity18 - Val(dsSelDispatchQty.Tables(0).Rows(0).Item("Quantity18"))
            mystrERPTrackingV2.sInvoiceNo = ""
            myccERPTrackingv1.InsertData(mystrERPTrackingV2)
            ''Inserting PACKING Balance Quantity''

        Next
        sDateChanged = "N"
    End Sub

    Private Sub grdSalesOrderDetails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSalesOrderDetails.DoubleClick
        ngrdRowNo = grdSalesOrderDetailsV1.FocusedRowHandle
        MsgBox(ngrdRowNo)
    End Sub

    Private Sub grdSalesOrderGBArticle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSalesOrderGBArticle.Click

    End Sub

    Private Sub grdSalesOrderGBArticleV1_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles grdSalesOrderGBArticleV1.RowCellStyle

    End Sub

    Private Sub grdSalesOrderGBArticleV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdSalesOrderGBArticleV1.RowStyle
        If sIsLoaded = "Y" Then
            If e.RowHandle > -1 Then
                If grdSalesOrderGBArticleV1.GetRowCellValue(e.RowHandle, grdSalesOrderGBArticleV1.Columns(5)).ToString() = "01. SIZES" Then
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.ForeColor = Color.DarkRed
                    e.Appearance.Font = New System.Drawing.Font(e.Appearance.Font, FontStyle.Bold)
                End If
            End If
        End If
    End Sub

    Private Sub grdSalesOrderV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdSalesOrderV1.RowStyle
        If sIsLoaded = "Y" Then
            If e.RowHandle > -1 Then
                If grdSalesOrderV1.GetRowCellValue(e.RowHandle, grdSalesOrderV1.Columns(7)).ToString() = "01. SIZES" Then
                    e.Appearance.BackColor = Color.LightGreen
                    e.Appearance.ForeColor = Color.DarkRed
                    e.Appearance.Font = New System.Drawing.Font(e.Appearance.Font, FontStyle.Bold)
                End If
            End If
        End If
    End Sub
End Class