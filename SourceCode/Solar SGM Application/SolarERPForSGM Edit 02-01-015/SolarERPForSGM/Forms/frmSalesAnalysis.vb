Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmSalesAnalysis
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccOrderPlanningReport As New ccOrderPlanningReport
    Dim myccSalesAnalysisReport As New ccSalesAnalysisReport
    Dim mystrSalesAnalysis As New strSalesAnalysis
    Dim myccAllinOne As New ccAllinOne

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
        cbxTypeofDocument.SelectedIndex = 0
        LoadComboItems()
    End Sub


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        If cbxTypeofDocument.Text = "INVOICE" Then
            grdSalesOrderDetails.ExportToXlsx("E:\Salses Analysis Report-Invoice.xlsx")
        ElseIf cbxTypeofDocument.Text = "CREDIT NOTE" Then
            grdSalesOrderDetails.ExportToXlsx("E:\Salses Analysis Report-Credit Note.xlsx")
        ElseIf cbxTypeofDocument.Text = "ORDER" Then
            grdOrderDtls.ExportToXlsx("E:\Salses Analysis Report-Order.xlsx")
        End If
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

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click

        mdlSGM.sSelectedOption = ""
        If cbxTypeofDocument.Text = "INVOICE" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "0"
        ElseIf cbxTypeofDocument.Text = "ORDER" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "1"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "2"
        End If

        mdlSGM.sTypeofDocument = Trim(cbxTypeofDocument.Text)

        If cbxGroupBy.Text = "Customer" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "0"
        ElseIf cbxGroupBy.Text = "Moulds" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "1"
        ElseIf cbxGroupBy.Text = "Customer / Moulds" Or cbxGroupBy.Text = "Moulds / Customer" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "2"
            'Else
            '    mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "3"
        End If
        mdlSGM.sReferencedFrom = Trim(cbxGroupBy.Text)

        If cbxCustomer.Text = " ALL CUSTOMERS" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxGranuleType.Text = " ALL TYPES" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxArticleMould.Text = " ALL ARTICLES" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If
       
        If cbxArticleCode.Text = " ALL ARTICLES" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If Trim(tbArticleDescription.Text) = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If rbAll.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
            If rbProduction.Checked = True Then
                mystrSalesAnalysis.IsSampleOrder = 0
            Else
                mystrSalesAnalysis.IsSampleOrder = 1
            End If
        End If

        If rbSample.Checked = True Then
            If cbxSampleOrderType.Text = " ALL SAMPLE ORDER" Then
                mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
            Else
                mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
            End If
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        End If


        If cbxProductType.Text = " ALL PRODUCT TYPE" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        mdlSGM.sBrand = cbxBrand.Text
    
        '' Code to Start from Here

        sIsLoaded = "N"
        mystrSalesAnalysis.FromDate = Format(dpFrom.Value, "dd-MMM-yyyy")
        mystrSalesAnalysis.ToDate = Format(dpTo.Value, "dd-MMM-yyyy")
        mystrSalesAnalysis.Customer = Trim(cbxCustomer.Text)
        mystrSalesAnalysis.ArticleMould = Trim(cbxArticleMould.Text)
        mystrSalesAnalysis.ArticleDescription = Trim(tbArticleDescription.Text)
        mystrSalesAnalysis.ProductType = Trim(cbxProductType.Text)
        mystrSalesAnalysis.SampleType = Trim(cbxSampleOrderType.SelectedValue)
        mystrSalesAnalysis.ArticleCode = Trim(cbxArticleCode.Text)
        mystrSalesAnalysis.GranuleType = Trim(cbxGranuleType.Text)
        mystrSalesAnalysis.ProductTypeMain = Trim(cbxProductTypeMain.Text)

        If cbxTypeofDocument.Text = "INVOICE" Or cbxTypeofDocument.Text = "CREDIT NOTE" Then

            Dim i As Integer = 0
            grdSalesOrderDetails.Visible = True
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

            grdSalesOrderDetails.DataSource = myccSalesAnalysisReport.LoadInvoiceDetails(mystrSalesAnalysis)

            With grdSalesOrderDetailsV1

                .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


            End With
        ElseIf cbxTypeofDocument.Text = "ORDER" Then
            grdOrderDtls.Visible = True
            grdOrderDtls.BringToFront()

AC:
            ngrdRowCount = grdOrderDtlsV1.RowCount
            For i = 0 To ngrdRowCount
                grdOrderDtlsV1.DeleteRow(i)
            Next
            ngrdRowCount = grdOrderDtlsV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo AC
            End If
            sIsLoaded = "Y"

            grdOrderDtls.DataSource = myccSalesAnalysisReport.LoadInvoiceDetails(mystrSalesAnalysis)

            With grdOrderDtlsV1

                .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            End With
        End If
        MsgBox("Loading Completed")
        sIsLoaded = "Y"
    End Sub

    Private Sub dpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpTo.ValueChanged, dpFrom.ValueChanged

        'LoadComboItems()

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

    End Sub

    Private Sub LoadComboItems()

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        cbxCustomer.DataSource = myccSalesAnalysisReport.LoadCustomer(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxCustomer.DisplayMember = "BuyerName"

        cbxGranuleType.DataSource = myccSalesAnalysisReport.LoadGranuleType(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxGranuleType.DisplayMember = "Granules"

        cbxArticleMould.DataSource = myccSalesAnalysisReport.LoadArticles(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxArticleMould.DisplayMember = "SoleName"

        cbxArticleCode.DataSource = myccSalesAnalysisReport.LoadArticleCode(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxArticleCode.DisplayMember = "SoleCode"

        cbxSampleOrderType.DataSource = Nothing : cbxSampleOrderType.Items.Clear()
        cbxSampleOrderType.DataSource = myccSalesAnalysisReport.LoadSampleType
        cbxSampleOrderType.DisplayMember = "FullName_"
        cbxSampleOrderType.ValueMember = "Abbrev_"


        cbxProductType.DataSource = myccSalesAnalysisReport.LoadProductType(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxProductType.DisplayMember = "SoleName"

        cbxGroupBy.SelectedIndex = 0

        cbxProductTypeMain.DataSource = myccOrderPlanningReport.LoadProductTypeMain(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxProductTypeMain.DisplayMember = "SoleName"

        LoadBrand()


    End Sub



    Private Sub grdSalesOrderDetailsV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdSalesOrderDetailsV1.RowStyle
        If sIsLoaded = "Y" Then
            'If e.RowHandle > -1 Then
            '    If grdSalesOrderDetailsV1.GetRowCellValue(e.RowHandle, grdSalesOrderDetailsV1.Columns(13)).ToString() = "Shipped" Then
            '        e.Appearance.ForeColor = Color.DarkBlue
            '    ElseIf grdSalesOrderDetailsV1.GetRowCellValue(e.RowHandle, grdSalesOrderDetailsV1.Columns(13)).ToString() = "OPEN" Then
            '        e.Appearance.ForeColor = Color.DarkOrange
            '    ElseIf grdSalesOrderDetailsV1.GetRowCellValue(e.RowHandle, grdSalesOrderDetailsV1.Columns(13)).ToString() = "CLOSE" Then
            '        e.Appearance.ForeColor = Color.Red
            '    End If
            'End If
        End If
    End Sub

    Private Sub rbSample_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSample.CheckedChanged
        If rbSample.Checked = True Then
            cbxSampleOrderType.Enabled = True
        Else
            cbxSampleOrderType.Enabled = False
        End If
    End Sub

    Private Sub cbxTypeofDocument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTypeofDocument.SelectedIndexChanged
        If cbxTypeofDocument.Text = "CREDIT NOTE" Then
            rbAll.Checked = True
            rbProduction.Enabled = False
            rbSample.Enabled = False
            cbxSampleOrderType.SelectedIndex = 0
        Else
            rbProduction.Enabled = True
            rbSample.Enabled = True
        End If
    End Sub

    Dim sBuyerName, sArticleMould, sType As String
    Dim nOrdQty, nValue, nAverage, nDispatch, nBalance As Integer

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim daDelSalesAnalysis As New SqlDataAdapter("Delete from TempSalesAnalysis", sConstr)
        Dim dsDelSalesAnalysis As New DataSet
        daDelSalesAnalysis.Fill(dsDelSalesAnalysis)
        dsDelSalesAnalysis.AcceptChanges()

        Dim i As Integer = 0
        If cbxTypeofDocument.Text = "INVOICE" Then
            For i = 0 To grdSalesOrderDetailsV1.RowCount - 1
                sBuyerName = grdSalesOrderDetailsV1.GetDataRow(i).Item("BuyerName").ToString
                sArticleMould = grdSalesOrderDetailsV1.GetDataRow(i).Item("ArticleMould").ToString
                sType = grdSalesOrderDetailsV1.GetDataRow(i).Item("Type").ToString
                nOrdQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("InvDtlQty").ToString)
                nValue = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Value").ToString)
                nAverage = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Average").ToString)
                nDispatch = 0
                nBalance = 0

                InsertSalesAnalyses()
            Next
        ElseIf cbxTypeofDocument.Text = "CREDIT NOTE" Then
            For i = 0 To grdSalesOrderDetailsV1.RowCount - 1
                sBuyerName = grdSalesOrderDetailsV1.GetDataRow(i).Item("BuyerName").ToString
                sArticleMould = grdSalesOrderDetailsV1.GetDataRow(i).Item("ArticleMould").ToString
                sType = grdSalesOrderDetailsV1.GetDataRow(i).Item("Type").ToString
                nOrdQty = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("InvDtlQty").ToString)
                nValue = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Value").ToString)
                nAverage = Val(grdSalesOrderDetailsV1.GetDataRow(i).Item("Average").ToString)
                nDispatch = 0
                nBalance = 0

                InsertSalesAnalyses()
            Next
        ElseIf cbxTypeofDocument.Text = "ORDER" Then
            For i = 0 To grdOrderDtlsV1.RowCount - 1
                sBuyerName = grdOrderDtlsV1.GetDataRow(i).Item("BuyerName").ToString
                sArticleMould = grdOrderDtlsV1.GetDataRow(i).Item("ArticleMould").ToString
                sType = grdOrderDtlsV1.GetDataRow(i).Item("Type").ToString
                nOrdQty = Val(grdOrderDtlsV1.GetDataRow(i).Item("OrdQty").ToString)
                nValue = Val(grdOrderDtlsV1.GetDataRow(i).Item("Value").ToString)
                nAverage = Val(grdOrderDtlsV1.GetDataRow(i).Item("Average").ToString)
                nDispatch = Val(grdOrderDtlsV1.GetDataRow(i).Item("Dispatch").ToString)
                nBalance = Val(grdOrderDtlsV1.GetDataRow(i).Item("Balance").ToString)

                InsertSalesAnalyses()
            Next
        End If

        mdlSGM.sReportType = "SALES ANALYSIS REPORT"
        frmReport.Show()

    End Sub

    Private Sub InsertSalesAnalyses()
        Dim daInsSalesAnalysis As New SqlDataAdapter("Insert Into TempSalesAnalysis Values ('" & sBuyerName & _
                                                     "','" & sArticleMould & "','" & sType & _
                                                     "','" & nOrdQty & "','" & nValue & _
                                                     "','" & nAverage & "','" & nDispatch & "','" & nBalance & "')", sConstr)
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

        cbxProductType.DataSource = myccSalesAnalysisReport.LoadProductType(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxProductType.DisplayMember = "SoleName"

    End Sub

    Private Sub cbxCustomer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCustomer.GotFocus
        cbxCustomer.DataSource = myccSalesAnalysisReport.LoadCustomer(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxCustomer.DisplayMember = "BuyerName"
    End Sub

    Private Sub cbxGranuleType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxGranuleType.GotFocus
        cbxGranuleType.DataSource = myccSalesAnalysisReport.LoadGranuleType(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxGranuleType.DisplayMember = "Granules"
    End Sub

    Private Sub cbxArticleMould_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxArticleMould.GotFocus
        cbxArticleMould.DataSource = myccSalesAnalysisReport.LoadArticles(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxArticleMould.DisplayMember = "SoleName"
    End Sub


    
    Private Sub cbxArticleMould_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxArticleMould.SelectedIndexChanged

    End Sub

    Private Sub cbxArticleCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxArticleCode.SelectedIndexChanged
        cbxArticleCode.DataSource = myccSalesAnalysisReport.LoadArticleCode(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxArticleCode.DisplayMember = "SoleCode"

    End Sub

    Private Sub cbxSampleOrderType_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxSampleOrderType.GotFocus
        cbxSampleOrderType.DataSource = Nothing : cbxSampleOrderType.Items.Clear()
        cbxSampleOrderType.DataSource = myccSalesAnalysisReport.LoadSampleType
        cbxSampleOrderType.DisplayMember = "FullName_"
        cbxSampleOrderType.ValueMember = "Abbrev_"
    End Sub

    Private Sub cbxProductTypeMain_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxProductTypeMain.GotFocus
        cbxProductTypeMain.DataSource = myccOrderPlanningReport.LoadProductTypeMain(mdlSGM.dFromDate, mdlSGM.dToDate)
        cbxProductTypeMain.DisplayMember = "SoleName"
    End Sub

    Private Sub cbxBrand_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxBrand.GotFocus
        LoadBrand()
    End Sub
 
    Private Sub LoadBrand()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")
        sTypeofDocument = Trim(cbxTypeofDocument.Text)
        sTypeofOrder = "All"

        cbxBrand.DataSource = myccAllinOne.LoadBrand(sTypeofOrder, sTypeofDocument)
        cbxBrand.DisplayMember = "Brand" '': cbxArticleName.ValueMember = "PKID"
        
    End Sub

    

    Private Sub cbxBrand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxBrand.SelectedIndexChanged

    End Sub

    Private Sub cbxSampleOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxSampleOrderType.SelectedIndexChanged

    End Sub
End Class