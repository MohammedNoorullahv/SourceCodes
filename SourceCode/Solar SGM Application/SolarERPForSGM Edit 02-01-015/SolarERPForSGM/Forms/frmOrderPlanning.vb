Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmAOrderPlanning
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccOrderPlanningReport As New ccOrderPlanningReport
    Dim mystrOrderPlanning As New strOrderPlanning

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub


    Dim ngrdRowCount As Integer

    'Dim grid As GridControl = grdSalesOrderDetails
    'Dim gridview1 As GridView = New GridView(grid)
    'Dim gridview2 As GridView = New GridView(grid)

    'Dim node1 As GridLevelNode = grid.LevelTree.Nodes.Add("tmptbl_SalesOrderDetailforSGM", gridview1)
    'Dim node11 As GridLevelNode = node1.Nodes.Add("tmptbl_SalesOrderDetailforSGM", gridview2)


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'DstmpPurchaseOrder.Clear()
        'DstmpPurchaseOrder.EnforceConstraints = False

        'Me.Tmptbl_PurchaseOrderforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderforSGM)
        'Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM)



        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        LoadComboItems()
    End Sub


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdSalesOrderDetails.ExportToXlsx("E:\Order Planning Report.xlsx")
        grdSalesOrderDetailsWKSummary.ExportToXlsx("E:\Order Planning Report Summary.xlsx")
        MsgBox("Export Completed")

    End Sub

    Dim sTypeofOrder, sTypeofDocument As String
    Private Sub LoadProductionStatus()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")



        


        'cbxCustomer.DataSource = myccAllinOne.LoadCustomer(sTypeofOrder, sTypeofDocument)
        'cbxCustomer.DisplayMember = "BuyerName" '': cbxArticleName.ValueMember = "PKID"
        
    End Sub

 
  



    
    Dim sSelOption, sOrderStatus As String
    Dim nIsEDDNegotiable As Integer

    Private Sub LoadData()


        mdlSGM.sSelectedOption = ""

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

        If rbAll.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If
        If rbYes.Checked = True Then
            nIsEDDNegotiable = 1
        Else
            nIsEDDNegotiable = 0
        End If



        If cbxYear.Text = " ALL" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If Trim(tbWeekFrom.Text) = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If


        If cbxProductType.Text = " ALL PRODUCT TYPE" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If


        sSelOption = mdlSGM.sSelectedOption + "A"
        If cbxSortingType.Text = "WEEK / DELIVERY DATE / CUSTOMER ORDER DATE" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If


        sIsLoaded = "N"
        Dim i As Integer = 0

        grdSalesOrderDetails.BringToFront()

Ab:
        ngrdRowCount = grdSalesOrderDetailsV1.RowCount
        For i = 0 To ngrdRowCount
            grdSalesOrderDetailsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdSalesOrderDetailsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If
        sIsLoaded = "Y"
        mystrOrderPlanning.FromDate = Format(dpFrom.Value, "dd-MMM-yyyy")
        mystrOrderPlanning.ToDate = Format(dpTo.Value, "dd-MMM-yyyy")
        mystrOrderPlanning.Customer = Trim(cbxCustomer.Text)
        mystrOrderPlanning.ArticleMould = Trim(cbxArticleMould.Text)
        mystrOrderPlanning.ArticleDescription = Trim(tbArticleDescription.Text)
        mystrOrderPlanning.ProductionStatus = sOrderStatus
        mystrOrderPlanning.ShipmentStatus = Trim(cbxShipmentStatus.Text)
        mystrOrderPlanning.Negotiable = nIsEDDNegotiable
        mystrOrderPlanning.nYear = Val(cbxYear.Text)
        mystrOrderPlanning.FromWeek = Val(tbWeekFrom.Text)
        mystrOrderPlanning.ToWeek = Val(tbWeekTo.Text)
        mystrOrderPlanning.ProductType = Trim(cbxProductType.Text)
        mystrOrderPlanning.SortingType = ""
        mystrOrderPlanning.ProductTypeMain = Trim(cbxProductTypeMain.Text)

        mdlSGM.sReferencedFrom = "Details"

        grdSalesOrderDetails.DataSource = myccOrderPlanningReport.LoadSalesOrderDetails(mystrOrderPlanning)

        With grdSalesOrderDetailsV1

            '.Columns(13).VisibleIndex = -1
            .Columns(15).VisibleIndex = -1

            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(11).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(11).DisplayFormat.FormatString = "dd-MMM-yyyy"

            .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        End With


        i = 0

        grdSalesOrderDetailsWKSummary.BringToFront()

Ac:
        ngrdRowCount = grdSalesOrderDetailsWKSummaryV1.RowCount
        For i = 0 To ngrdRowCount
            grdSalesOrderDetailsWKSummaryV1.DeleteRow(i)
        Next
        ngrdRowCount = grdSalesOrderDetailsWKSummaryV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ac
        End If

        mdlSGM.sReferencedFrom = "Summary"
        mdlSGM.sSelectedOption = sSelOption

        grdSalesOrderDetailsWKSummary.DataSource = myccOrderPlanningReport.LoadSalesOrderDetails(mystrOrderPlanning)

        With grdSalesOrderDetailsWKSummaryV1

            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

        End With

        MsgBox("Loading Completed")
        sIsLoaded = "Y"
    End Sub

    Private Sub dpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpTo.ValueChanged, dpFrom.ValueChanged

        LoadComboItems()

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

        cbxYear.DataSource = myccOrderPlanningReport.LoadYear(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxYear.DisplayMember = "DlyYear"

        cbxProductType.DataSource = myccOrderPlanningReport.LoadProductType(mdlSGM.dFromDate, mdlSGM.dToDate, "")
        cbxProductType.DisplayMember = "SoleName"

        cbxSortingType.SelectedIndex = 0

        cbxProductTypeMain.DataSource = myccOrderPlanningReport.LoadProductTypeMain(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxProductTypeMain.DisplayMember = "SoleName"

    End Sub

    

    Private Sub grdSalesOrderDetailsV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdSalesOrderDetailsV1.RowStyle
        If sIsLoaded = "Y" Then
            If e.RowHandle > -1 Then
                If grdSalesOrderDetailsV1.GetRowCellValue(e.RowHandle, grdSalesOrderDetailsV1.Columns(13)).ToString() = "Shipped" Then
                    e.Appearance.ForeColor = Color.DarkBlue
                ElseIf grdSalesOrderDetailsV1.GetRowCellValue(e.RowHandle, grdSalesOrderDetailsV1.Columns(13)).ToString() = "OPEN" Then
                    e.Appearance.ForeColor = Color.DarkOrange
                ElseIf grdSalesOrderDetailsV1.GetRowCellValue(e.RowHandle, grdSalesOrderDetailsV1.Columns(13)).ToString() = "CLOSE" Then
                    e.Appearance.ForeColor = Color.Red
                End If
            End If
        End If
    End Sub

    Dim ngrdRowNo, nBal2Dispatch As Integer
    Dim sSalesOrderDetailsID, sIsNegotiable, sIsLoaded As String
    Dim WeekNumber As Integer
    Private Sub cbOpenOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOpenOrder.Click

        sIsLoaded = "N"
        ngrdRowNo = grdSalesOrderDetailsV1.FocusedRowHandle


        If ngrdRowNo >= 0 Then
            sSalesOrderDetailsID = grdSalesOrderDetailsV1.GetDataRow(ngrdRowNo).Item("ID").ToString
            sIsNegotiable = grdSalesOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Neg").ToString
            nBal2Dispatch = Val(grdSalesOrderDetailsV1.GetDataRow(ngrdRowNo).Item("Balance").ToString)
            If sIsNegotiable = "Y" And nBal2Dispatch > 0 Then

                Dim daSelOrdDetails As New SqlDataAdapter("Select * from SalesOrderDetails Where ID = '" & sSalesOrderDetailsID & "'", sConstr)
                Dim dsSelOrdDetails As New DataSet
                daSelOrdDetails.Fill(dsSelOrdDetails)

                tbSalesOrderNo.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("SalesOrderNo").ToString
                tbArticleCode.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Article").ToString
                tbArticleName.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("ArticleName").ToString
                tbCustomerOrder.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("CustomerOrderNo").ToString
                dpBuyerDlyDt.Value = dsSelOrdDetails.Tables(0).Rows(0).Item("BuyerDeliveryDate").ToString
                dpExpectedDlyDt.Value = dsSelOrdDetails.Tables(0).Rows(0).Item("ExpectedDeliveryDate").ToString
                tbWeekNo.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("WeekNo").ToString

                lblS01.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size01").ToString
                lblS02.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size02").ToString
                lblS03.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size03").ToString
                lblS04.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size04").ToString
                lblS05.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size05").ToString
                lblS06.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size06").ToString
                lblS07.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size07").ToString
                lblS08.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size08").ToString
                lblS09.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size09").ToString
                lblS10.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size10").ToString
                lblS11.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size11").ToString
                lblS12.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size12").ToString
                lblS13.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size13").ToString
                lblS14.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size14").ToString
                lblS15.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size15").ToString
                lblS16.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size16").ToString
                lblS17.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size17").ToString
                lblS18.Text = dsSelOrdDetails.Tables(0).Rows(0).Item("Size18").ToString

                tbQty01.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity01").ToString)
                tbQty02.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity02").ToString)
                tbQty03.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity03").ToString)
                tbQty04.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity04").ToString)
                tbQty05.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity05").ToString)
                tbQty06.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity06").ToString)
                tbQty07.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity07").ToString)
                tbQty08.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity08").ToString)
                tbQty09.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity09").ToString)
                tbQty10.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity10").ToString)
                tbQty11.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity11").ToString)
                tbQty12.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity12").ToString)
                tbQty13.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity13").ToString)
                tbQty14.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity14").ToString)
                tbQty15.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity15").ToString)
                tbQty16.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity16").ToString)
                tbQty17.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity17").ToString)
                tbQty18.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("Quantity18").ToString)

                If Val(tbQty01.Text) = 0 Then : tbQty01.Clear() : End If
                If Val(tbQty02.Text) = 0 Then : tbQty02.Clear() : End If
                If Val(tbQty03.Text) = 0 Then : tbQty03.Clear() : End If
                If Val(tbQty04.Text) = 0 Then : tbQty04.Clear() : End If
                If Val(tbQty05.Text) = 0 Then : tbQty05.Clear() : End If
                If Val(tbQty06.Text) = 0 Then : tbQty06.Clear() : End If
                If Val(tbQty07.Text) = 0 Then : tbQty07.Clear() : End If
                If Val(tbQty08.Text) = 0 Then : tbQty08.Clear() : End If
                If Val(tbQty09.Text) = 0 Then : tbQty09.Clear() : End If
                If Val(tbQty10.Text) = 0 Then : tbQty10.Clear() : End If
                If Val(tbQty11.Text) = 0 Then : tbQty11.Clear() : End If
                If Val(tbQty12.Text) = 0 Then : tbQty12.Clear() : End If
                If Val(tbQty13.Text) = 0 Then : tbQty13.Clear() : End If
                If Val(tbQty14.Text) = 0 Then : tbQty14.Clear() : End If
                If Val(tbQty15.Text) = 0 Then : tbQty15.Clear() : End If
                If Val(tbQty16.Text) = 0 Then : tbQty16.Clear() : End If
                If Val(tbQty17.Text) = 0 Then : tbQty17.Clear() : End If
                If Val(tbQty18.Text) = 0 Then : tbQty18.Clear() : End If


                tbTotalQty.Text = Val(dsSelOrdDetails.Tables(0).Rows(0).Item("OrderQuantity").ToString)

            Else
                MsgBox("Selected Order Delivery Date Is Not Negotiable", MsgBoxStyle.Critical)
                Exit Sub
            End If

            plSalesOrdDtl.Visible = True
            plSalesOrdDtl.BringToFront()
        Else
            MsgBox("No Data Available", MsgBoxStyle.Critical)
            Exit Sub
        End If
        sIsLoaded = "Y"
    End Sub

    Private Sub cbExitUpdateModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExitUpdateModule.Click
        plSalesOrdDtl.Visible = False
    End Sub


    Private Sub dpExpectedDlyDt_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpExpectedDlyDt.ValueChanged
        If sIsLoaded = "Y" Then
            tbWeekNo.Text = (Format(dpExpectedDlyDt.Value, "yy")).ToString + (DatePart(DateInterval.WeekOfYear, dpExpectedDlyDt.Value)).ToString

            If Microsoft.VisualBasic.Len(tbWeekNo.Text) = 3 Then
                tbWeekNo.Text = (Format(dpExpectedDlyDt.Value, "yy")).ToString + "0" + (DatePart(DateInterval.WeekOfYear, dpExpectedDlyDt.Value)).ToString
            End If
        End If

    End Sub

    Private Sub cbUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbUpdate.Click
        Dim daUpdOrdDetails As New SqlDataAdapter("Update SalesOrderDetails Set ExpectedDeliveryDate = '" & Format(dpExpectedDlyDt.Value, "dd-MMM-yyyy") & _
                                                  "', WeekNo = '" & Trim(tbWeekNo.Text) & "' Where ID = '" & sSalesOrderDetailsID & "'", sConstr)
        Dim dsUpdOrdDetails As New DataSet
        daUpdOrdDetails.Fill(dsUpdOrdDetails)
        dsUpdOrdDetails.AcceptChanges()

        MsgBox("Expected Delivery Date Updated Successfully", MsgBoxStyle.Information)
        plSalesOrdDtl.Visible = False

        LoadData()
    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        LoadData()
    End Sub

    Dim sSaleOrderNo, sCustomerOrderNo, sBuyerName, sArticle, sArticleName, sNeg As String
    Dim nOrderQuantity, nDispatch, nBalance, nWeekNo As Integer
    Dim nPrice, nOrderValue As Decimal
    Dim dOrderReceivedDate, dExpectedDeliveryDate As Date


    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim daDelOrderPlanning As New SqlDataAdapter("Delete from TempOrderPlanning", sConstr)
        Dim dsDelOrderPlanning As New DataSet
        daDelOrderPlanning.Fill(dsDelOrderPlanning)
        dsDelOrderPlanning.AcceptChanges()

        Dim daDelOrderPlanningSummary As New SqlDataAdapter("Delete from TempOrderPlanningSummary", sConstr)
        Dim dsDelOrderPlanningSummary As New DataSet
        daDelOrderPlanningSummary.Fill(dsDelOrderPlanningSummary)
        dsDelOrderPlanningSummary.AcceptChanges()

        Dim i As Integer = 0

        For i = 0 To grdSalesOrderDetailsV1.RowCount - 1
            dOrderReceivedDate = grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderReceivedDate").ToString
            sSaleOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("SalesOrderNo").ToString
            sCustomerOrderNo = grdSalesOrderDetailsV1.GetDataRow(i).Item("CustomerOrderNo").ToString
            sBuyerName = grdSalesOrderDetailsV1.GetDataRow(i).Item("BuyerName").ToString
            sArticle = grdSalesOrderDetailsV1.GetDataRow(i).Item("Article").ToString
            sArticleName = grdSalesOrderDetailsV1.GetDataRow(i).Item("ArticleName").ToString
            sOrderStatus = grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderStatus").ToString
            sNeg = grdSalesOrderDetailsV1.GetDataRow(i).Item("Neg").ToString
            dExpectedDeliveryDate = grdSalesOrderDetailsV1.GetDataRow(i).Item("ExpectedDeliveryDate").ToString

            nOrderQuantity = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderQuantity").ToString)
            nDispatch = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Dispatch").ToString)
            nBalance = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Balance").ToString)
            nWeekNo = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("WeekNo").ToString)
            nPrice = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Price").ToString)
            nOrderValue = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("OrderValue").ToString)

            InsertOrderPlanning()
        Next

        i = 0
        For i = 0 To grdSalesOrderDetailsWKSummaryV1.RowCount - 1
            nWeekNo = Val(grdSalesOrderDetailsWKSummaryV1.GetDataRow(i).Item("WeekNo").ToString)
            nOrderQuantity = Val(grdSalesOrderDetailsWKSummaryV1.GetDataRow(i).Item("OrderQuantity").ToString)
            nDispatch = Val(grdSalesOrderDetailsWKSummaryV1.GetDataRow(i).Item("Dispatch").ToString)
            nBalance = Val(grdSalesOrderDetailsWKSummaryV1.GetDataRow(i).Item("Balance").ToString)

            InsertOrderPlanningSummary()
        Next

        

        mdlSGM.sReportType = "ORDER PLANNING REPORT"
        frmReport.Show()

        mdlSGM.sReportType = "ORDER PLANNING REPORT"
        frmReport1.Show()

    End Sub

    Private Sub InsertOrderPlanning()
        Dim daInsSalesAnalysis As New SqlDataAdapter("Insert Into TempOrderPlanning Values ('" & Format(dOrderReceivedDate, "dd-MMM-yyyy") & _
                                                     "','" & sBuyerName & "','" & sArticle & "','" & sArticleName & _
                                                     "','" & nOrderQuantity & "','" & nPrice & "','" & nOrderValue & _
                                                     "','" & nDispatch & "','" & nBalance & "','" & Format(dExpectedDeliveryDate, "dd-MMM-yyyy") & _
                                                     "','" & nWeekNo & "','" & sOrderStatus & "','" & sNeg & "')", sConstr)
        Dim dsInsSalesAnalysis As New DataSet
        daInsSalesAnalysis.Fill(dsInsSalesAnalysis)
        dsInsSalesAnalysis.AcceptChanges()

    End Sub

    Private Sub InsertOrderPlanningSummary()
        Dim daInsSalesAnalysis As New SqlDataAdapter("Insert Into TempOrderPlanningSummary Values ('" & nWeekNo & _
                                                     "','" & nOrderQuantity & "','" & nDispatch & "','" & nBalance & "')", sConstr)
        Dim dsInsSalesAnalysis As New DataSet
        daInsSalesAnalysis.Fill(dsInsSalesAnalysis)
        dsInsSalesAnalysis.AcceptChanges()

    End Sub

    Private Sub cbxProductType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxProductType.GotFocus
        If cbxProductTypeMain.Text = " ALL PRODUCT TYPE" Then
            cbxProductType.DataSource = myccOrderPlanningReport.LoadProductType(mdlSGM.dFromDate, mdlSGM.dToDate, "")
            cbxProductType.DisplayMember = "SoleName"
        Else
            cbxProductType.DataSource = myccOrderPlanningReport.LoadProductType(mdlSGM.dFromDate, mdlSGM.dToDate, cbxProductTypeMain.Text)
            cbxProductType.DisplayMember = "SoleName"
        End If

    End Sub
End Class