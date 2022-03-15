Public Class frmArticleList

    Dim myccArticleMaster As New ccArticleMaster

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plSelectionCriteria.Enter

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try
        If chkbxSelectCustomer.Checked = False Then
            mdlSGM.sSelectOption = "All Articles"
        Else
            If chkbxFilter.Checked = True Then
            
                If rbArticleName.Checked = True Then
                    mdlSGM.sSelectOption = "Customers Article - Article Wise"
                    mdlSGM.sSelectedCustomer = cbxCustomer.Text
                    mdlSGM.sSelectedArticle = cbxArticleName.Text
                ElseIf rbCodification.Checked = True Then
                    mdlSGM.sSelectOption = "Customers Article - Codification Wise"
                    mdlSGM.sSelectedCustomer = cbxCustomer.Text
                    mdlSGM.sSelectedCodification = cbxCodification.Text
                Else
                    mdlSGM.sSelectOption = "Customers Article"
                    mdlSGM.sSelectedCustomer = cbxCustomer.Text
                End If
            Else
                mdlSGM.sSelectOption = "Customers Article"
                mdlSGM.sSelectedCustomer = cbxCustomer.Text
            End If
        End If

        LoadAllTransactions()

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
        End
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
    Private Sub LoadAllTransactions()
        ''Try


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

        grdArticleMaster.DataSource = myccArticleMaster.LoadArticles

        With grdArticleMasterV1
            '.Columns(3).VisibleIndex = -1

            '.Columns(2).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(2).DisplayFormat.FormatString = "dd-MMM-yyyy"
            '.Columns(6).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(6).DisplayFormat.FormatString = "0.00"
            '.Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(7).DisplayFormat.FormatString = "0.00"
            '.Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(8).DisplayFormat.FormatString = "0.00"
            '.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            '.Columns(9).DisplayFormat.FormatString = "0.00"

            '.Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
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
        cbxCustomer.DataSource = myccArticleMaster.LoadCustomer
        cbxCustomer.DisplayMember = "Client" '': cbxArticleName.ValueMember = "PKID"

        
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
        cbxArticleName.DataSource = myccArticleMaster.LoadArticleofCustomer
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
        cbxCodification.DataSource = myccArticleMaster.LoadCodificationofCustomer
        cbxCodification.DisplayMember = "CodificationNew" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
    End Sub

    Private Sub rbArticleName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbArticleName.CheckedChanged
        If rbArticleName.Checked = True Then
            LoadArticle()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToExcelOld("D:\ArticleMaster.xls")
        MsgBox("Export Completed")

    End Sub
End Class