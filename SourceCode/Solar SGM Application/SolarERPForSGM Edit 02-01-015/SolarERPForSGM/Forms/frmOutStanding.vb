Public Class frmOutstanding

    Dim myccOutstanding As New ccOutstanding

    Dim mystrSolarOutstanding4SGM4Print As New strSolarOutstanding4SGM4Print


    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plSelectionCriteria.Enter

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.sSelectedArticle = cbxArticleName.Text

        If mdlSGM.sSelectedCustomer = " ALL CUSTOMERS" And (mdlSGM.sSelectedArticle = " ALL ARTICLES" Or mdlSGM.sSelectedArticle = "") Then
            mdlSGM.sSelectOption = "All Articles"
        ElseIf mdlSGM.sSelectedCustomer = " ALL CUSTOMERS" And mdlSGM.sSelectedArticle <> " ALL ARTICLES" Then
            mdlSGM.sSelectOption = "Article Wise"
        ElseIf mdlSGM.sSelectedCustomer <> " ALL CUSTOMERS" And mdlSGM.sSelectedArticle = " ALL ARTICLES" Then
            mdlSGM.sSelectOption = "Customers Article"
        ElseIf mdlSGM.sSelectedCustomer <> " ALL CUSTOMERS" And mdlSGM.sSelectedArticle <> " ALL ARTICLES" Then
            mdlSGM.sSelectOption = "Customers Article - Article Wise"
        End If

        'If chkbxSelectCustomer.Checked = False Then
        '    mdlSGM.sSelectOption = "All Articles"
        'Else
        '    If chkbxFilter.Checked = True Then
        '        If rbArticleName.Checked = True Then
        '            mdlSGM.sSelectOption = "Customers Article - Article Wise"
        '            mdlSGM.sSelectedCustomer = cbxCustomer.Text
        '            mdlSGM.sSelectedArticle = cbxArticleName.Text
        '        ElseIf rbCodification.Checked = True Then
        '            mdlSGM.sSelectOption = "Customers Article - Codification Wise"
        '            mdlSGM.sSelectedCustomer = cbxCustomer.Text
        '            mdlSGM.sSelectedCodification = cbxCodification.Text
        '        Else
        '            mdlSGM.sSelectOption = "Customers Article"
        '            mdlSGM.sSelectedCustomer = cbxCustomer.Text
        '        End If
        '    Else
        '        mdlSGM.sSelectOption = "Customers Article"
        '        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        '    End If
        'End If


        LoadOutstanding()

        ''Catch ex As Exception

        ''End Try
    End Sub


    Private Sub chkbxSelectCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxSelectCustomer.CheckedChanged
        ''Try
        If chkbxSelectCustomer.Checked = True Then
            chkbxFilter.Enabled = True
            LoadCustomer()

        Else
            chkbxFilter.Checked = False
            chkbxFilter.Enabled = False
        End If
        ''Catch ex As Exception

        ''End Try
    End Sub

    Private Sub chkbxFilter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxFilter.CheckedChanged
        Try
            If chkbxFilter.Checked = True Then
                plSelectionCriteria.Enabled = True
                rbArticleName.Checked = True
                cbxArticleName.Enabled = True
            Else
                plSelectionCriteria.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub

    Private Sub rbCodification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCodification.CheckedChanged
        Try
            If rbCodification.Checked = True Then
                cbxCodification.Enabled = True
                cbxArticleName.Enabled = False
                LoadCodification()
            Else
                cbxArticleName.Enabled = True
                cbxCodification.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim ngrdRowCount As Integer
    Private Sub LoadOutstanding()
        ''Try

        'GoTo Aa
        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdArticleMasterV1.RowCount
        For i = 0 To ngrdRowCount
            grdArticleMasterV1.DeleteRow(i)
        Next
        ngrdRowCount = grdArticleMasterV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        If rbAll.Checked = True Then
            mdlSGM.sOutstandingCriteria = "All"
        ElseIf rbCompleted.Checked = True Then
            mdlSGM.sOutstandingCriteria = "Completed"
        ElseIf rbPending.Checked = True Then
            mdlSGM.sOutstandingCriteria = "Pending"
        Else
            MsgBox("Filter Option NOt selected properly", MsgBoxStyle.Information)
        End If

        grdArticleMaster.DataSource = myccOutstanding.UpdateOutstanding
        'Aa:
        With grdArticleMasterV1
            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1
            .Columns(22).VisibleIndex = -1

            .Columns(21).VisibleIndex = 9


            .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"

            '.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(6).DisplayFormat.FormatString = "0.00"
            '.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(7).DisplayFormat.FormatString = "0.00"
            '.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(8).DisplayFormat.FormatString = "0.00"
            '.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(9).DisplayFormat.FormatString = "0.00"
            Dim j As Integer = 10

            For j = 10 To 21
                .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            Next
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


        End With
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()
        cbxCustomer.DataSource = myccOutstanding.LoadCustomer  'myccOutstanding As New ccOutstanding
        cbxCustomer.DisplayMember = "BuyerName" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub LoadArticle()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxArticleName.DataSource = Nothing : cbxArticleName.Items.Clear()
        cbxArticleName.DataSource = myccOutstanding.LoadArticleofCustomer
        cbxArticleName.DisplayMember = "SoleName" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCodification()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCodification.DataSource = Nothing : cbxCodification.Items.Clear()
        cbxCodification.DataSource = myccOutstanding.LoadCodificationofCustomer
        cbxCodification.DisplayMember = "CodificationNew" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        LoadCustomer()
        LoadArticle()
    End Sub

    Private Sub rbArticleName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbArticleName.CheckedChanged
        If rbArticleName.Checked = True Then
            LoadArticle()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToXlsx("E:\Outstanding.xlsx")
        MsgBox("Export Completed")

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim i As Integer = 0

        myccOutstanding.DelOutstanding4Print()

        For i = 0 To grdArticleMasterV1.RowCount - 1

            'myOptimizerCRMCutterTicketsComponent.InsertCutterTempTicket(myOPtimizerstrCRMCutterTicketDetail)

            mystrSolarOutstanding4SGM4Print.PKID = 0
            mystrSolarOutstanding4SGM4Print.SalesOrderDetailId = grdArticleMasterV1.GetDataRow(i).Item("SalesOrderDetailId").ToString
            mystrSolarOutstanding4SGM4Print.SalesOrderDate = grdArticleMasterV1.GetDataRow(i).Item("SalesOrderDate").ToString 'As Date
            mystrSolarOutstanding4SGM4Print.CustomerName = grdArticleMasterV1.GetDataRow(i).Item("CustomerName").ToString
            mystrSolarOutstanding4SGM4Print.SalesOrderNo = grdArticleMasterV1.GetDataRow(i).Item("SalesOrderNo").ToString
            mystrSolarOutstanding4SGM4Print.CustomerOrderNo = grdArticleMasterV1.GetDataRow(i).Item("CustomerOrderNo").ToString
            mystrSolarOutstanding4SGM4Print.SoleCode = grdArticleMasterV1.GetDataRow(i).Item("SoleCode").ToString
            mystrSolarOutstanding4SGM4Print.SoleName = grdArticleMasterV1.GetDataRow(i).Item("SoleName").ToString
            mystrSolarOutstanding4SGM4Print.Colour = grdArticleMasterV1.GetDataRow(i).Item("Colour").ToString
            mystrSolarOutstanding4SGM4Print.Codification = grdArticleMasterV1.GetDataRow(i).Item("Codification").ToString
            mystrSolarOutstanding4SGM4Print.OrdQty = Val(grdArticleMasterV1.GetDataRow(i).Item("OrdQty").ToString)
            mystrSolarOutstanding4SGM4Print.Moulding = Val(grdArticleMasterV1.GetDataRow(i).Item("Moulding").ToString)
            mystrSolarOutstanding4SGM4Print.MouldingWIP = Val(grdArticleMasterV1.GetDataRow(i).Item("MouldingWIP").ToString)
            mystrSolarOutstanding4SGM4Print.Finishing = Val(grdArticleMasterV1.GetDataRow(i).Item("Finishing").ToString)
            mystrSolarOutstanding4SGM4Print.FinishingWIP = Val(grdArticleMasterV1.GetDataRow(i).Item("FinishingWIP").ToString)
            mystrSolarOutstanding4SGM4Print.Packing = Val(grdArticleMasterV1.GetDataRow(i).Item("Packing").ToString)
            mystrSolarOutstanding4SGM4Print.InStock = Val(grdArticleMasterV1.GetDataRow(i).Item("InStock").ToString)
            mystrSolarOutstanding4SGM4Print.Dispatch = Val(grdArticleMasterV1.GetDataRow(i).Item("Dispatch").ToString)
            mystrSolarOutstanding4SGM4Print.UpdatedOn = grdArticleMasterV1.GetDataRow(i).Item("UpdatedOn").ToString '' As Date
            mystrSolarOutstanding4SGM4Print.IsCompleted = Val(grdArticleMasterV1.GetDataRow(i).Item("IsCompleted").ToString)
            mystrSolarOutstanding4SGM4Print.IsClosed = Val(grdArticleMasterV1.GetDataRow(i).Item("IsClosed").ToString)

            myccOutstanding.InsOutstanding4Print(mystrSolarOutstanding4SGM4Print)

        Next
        mdlSGM.sReportType = "Outstanding"
        frmReport.Show()
    End Sub

    Dim ngrdRowNo As Integer
    Private Sub grdArticleMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdArticleMaster.DoubleClick

        ngrdRowNo = grdArticleMasterV1.FocusedRowHandle

        mdlSGM.sSelectedSalesOrderNo = grdArticleMasterV1.GetDataRow(ngrdRowNo).Item("SalesOrderNo").ToString
        mdlSGM.sSelectedArticle = grdArticleMasterV1.GetDataRow(ngrdRowNo).Item("SoleCode").ToString
        mdlSGM.sReportType = "Deatails of Dispatch"
        frmReport.Show()
    End Sub

    Private Sub cbxCustomer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCustomer.GotFocus
        LoadCustomer()
    End Sub

    Private Sub cbxArticleName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxArticleName.GotFocus
        LoadArticle()

        

    End Sub

   

    
    Private Sub cbxArticleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxArticleName.SelectedIndexChanged

    End Sub
End Class