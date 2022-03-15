Public Class frmKHLIOutstanding

    Dim myccKHLIOutstanding As New ccKHLIOutstanding
    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard
    Dim myccKHLIUpperDispatchBalance As New ccKHLIUpperDispatchBalance

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try
        If chkbxSelectCustomer.Checked = False Then
            mdlSGM.sSelectOption = "All Articles"
        Else
            If chkbxFilter.Checked = True Then
                mdlSGM.sSelectOption = "Customer & SalesOrder"
                mdlSGM.sSelectedCustomer = cbxCustomer.Text
                mdlSGM.sSelectedArticle = cbxSalesOrder.Text
                'If rbArticleName.Checked = True Then
                '    mdlSGM.sSelectOption = "Customers Article - Article Wise"
                '    mdlSGM.sSelectedCustomer = cbxCustomer.Text
                '    mdlSGM.sSelectedArticle = cbxSalesOrder.Text
                'ElseIf rbCodification.Checked = True Then
                '    mdlSGM.sSelectOption = "Customers Article - Codification Wise"
                '    mdlSGM.sSelectedCustomer = cbxCustomer.Text
                '    mdlSGM.sSelectedCodification = cbxCodification.Text
                'Else
                '    mdlSGM.sSelectOption = "Customers Article"
                '    mdlSGM.sSelectedCustomer = cbxCustomer.Text
                'End If
            Else
                mdlSGM.sSelectOption = "Customers"
                mdlSGM.sSelectedCustomer = cbxCustomer.Text
            End If
        End If

        LoadOutstanding()
        MsgBox("Updated")
        'asdfsad()
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
                'plSelectionCriteria.Enabled = True
                'rbArticleName.Checked = True
                cbxSalesOrder.Enabled = True
                LoadSalesOrder()
            Else
                'plSelectionCriteria.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show()
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

        'grdArticleMaster.DataSource = myccKHLIOutstanding.UpdateKHLIOutstanding
        grdArticleMaster.DataSource = myccKHLIOutstandingWithJobcard.UpdateKHLIOutstanding
        'Aa:
        With grdArticleMasterV1
            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1
            '.Columns(22).VisibleIndex = -1

            .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(15).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(15).DisplayFormat.FormatString = "dd-MMM-yyyy"

            ''.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(6).DisplayFormat.FormatString = "0.00"
            ''.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(7).DisplayFormat.FormatString = "0.00"
            ''.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(8).DisplayFormat.FormatString = "0.00"
            ''.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(9).DisplayFormat.FormatString = "0.00"
            Dim j As Integer = 19

            For j = 19 To 52
                .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            Next
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count


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
        cbxCustomer.DataSource = myccKHLIOutstandingWithJobcard.LoadCustomer
        cbxCustomer.DisplayMember = "BuyerGroupCode" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub LoadSalesOrder()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxSalesOrder.DataSource = Nothing : cbxSalesOrder.Items.Clear()
        cbxSalesOrder.DataSource = myccKHLIOutstandingWithJobcard.LoadSalesOrder(mdlSGM.sSelectedCustomer)
        cbxSalesOrder.DisplayMember = "SalesOrderNo" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToXls("D:\ArticleMaster.xls")
        MsgBox("Export Completed")

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LoadUpperDispatchBalance()
    End Sub

    Private Sub LoadUpperDispatchBalance()
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

        'grdArticleMaster.DataSource = myccKHLIOutstanding.UpdateKHLIOutstanding
        grdArticleMaster.DataSource = myccKHLIUpperDispatchBalance.UpdateKHLIUpperDispatchBalance
        'Aa:
        Exit Sub
        With grdArticleMasterV1
            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1
            '.Columns(22).VisibleIndex = -1

            .Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(15).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(15).DisplayFormat.FormatString = "dd-MMM-yyyy"

            ''.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(6).DisplayFormat.FormatString = "0.00"
            ''.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(7).DisplayFormat.FormatString = "0.00"
            ''.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(8).DisplayFormat.FormatString = "0.00"
            ''.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            ''.Columns(9).DisplayFormat.FormatString = "0.00"
            Dim j As Integer = 19

            For j = 19 To 52
                .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            Next
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count


        End With
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub
End Class