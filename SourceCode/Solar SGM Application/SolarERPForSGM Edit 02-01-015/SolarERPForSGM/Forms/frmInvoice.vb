Public Class frmInvoice

    Dim myccInvoicesWithDetails As New ccInvoicesWithDetails
    Dim myccInvoicesWithDetailsWithCustomer As New ccInvoicesWithDetailsWithCustomer
    Dim myccInvoicesWithDetailsWithCustomerArticle As New ccInvoicesWithDetailsWithCustomerArticle
    Dim myccInvoicesWithDetailsWithCustomerCodification As New ccInvoicesWithDetailsWithCustomerCodification

    Dim mystrSolarInvoice4SGM4Print As New strSolarInvoice4SGM4Print

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plSelectionCriteria.Enter

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try
        If chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = False Then
            MsgBox("Type of Order is not selected", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If chkbxSelectCustomer.Checked = False Then
            'mdlSGM.sSelectOption = "All Articles"

            If chkbxSalesOrder.Checked = True And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = False Then
                If chkbxAll.Checked = True Then
                    mdlSGM.sSelectOption = "S-A"
                Else
                    If chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = ""
                        MsgBox("No option for type of Invoice is selected", MsgBoxStyle.Critical)
                        Exit Sub
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-E"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-H"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-HE"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-CE"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-CH"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-CHE"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-3E"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3H"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-3HE"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-3CE"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3CH"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-A"
                    End If
                End If

            ElseIf chkbxJobWorkOrder.Checked = True And chkbxSalesOrder.Checked = True And chkbxGeneral.Checked = False Then
                If chkbxAll.Checked = True Then
                    mdlSGM.sSelectOption = "JS-A"
                Else
                    If chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "J"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-E"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-H"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-HE"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-CE"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-CH"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-CHE"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-3E"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3H"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-3HE"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-3CE"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3CH"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-A"
                    End If
                End If
            ElseIf chkbxJobWorkOrder.Checked = True And chkbxSalesOrder.Checked = False And chkbxGeneral.Checked = False Then
                mdlSGM.sSelectOption = "J"
            ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = True And chkbxGeneral.Checked = True Then
                mdlSGM.sSelectOption = "J-G"
            ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = True Then
                mdlSGM.sSelectOption = "G"

            End If
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
                    GoTo Aa
                End If
            Else
                mdlSGM.sSelectOption = "Customers Article"
                mdlSGM.sSelectedCustomer = cbxCustomer.Text
            End If
            ''Selection with Customer''
Aa:
            If chkbxSalesOrder.Checked = True And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = False Then
                If chkbxAll.Checked = True Then
                    mdlSGM.sSelectOption = "S-A-C"
                Else
                    If chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = ""
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-E-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-H-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-HE-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-C-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-CE-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-CH-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-CHE-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-3E-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3H-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-3HE-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3C-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-3CE-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "S-3CH-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "S-A-C"
                    End If
                End If

            ElseIf chkbxJobWorkOrder.Checked = True And chkbxSalesOrder.Checked = True And chkbxGeneral.Checked = False Then
                If chkbxAll.Checked = True Then
                    mdlSGM.sSelectOption = "JS-A-C"
                Else
                    If chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "J-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-E-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-H-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-HE-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-C-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-CE-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-CH-C"
                    ElseIf chkbxCT3.Checked = False And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-CHE-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-3E-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3H-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = False And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-3HE-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3C-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = False And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-3CE-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = False Then
                        mdlSGM.sSelectOption = "JS-3CH-C"
                    ElseIf chkbxCT3.Checked = True And chkbxFormC.Checked = True And chkbxFormH.Checked = True And chkbxExport.Checked = True Then
                        mdlSGM.sSelectOption = "JS-A-C"
                    End If
                End If
            ElseIf chkbxJobWorkOrder.Checked = True And chkbxSalesOrder.Checked = False And chkbxGeneral.Checked = False Then
                mdlSGM.sSelectOption = "J-C"
                'ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = True And chkbxGeneral.Checked = True Then
                '    mdlSGM.sSelectOption = "J-G"
            ElseIf chkbxSalesOrder.Checked = False And chkbxJobWorkOrder.Checked = False And chkbxGeneral.Checked = True Then
                mdlSGM.sSelectOption = "G-C"

            End If
            ''Selection With Customer''

        End If

        LoadAllInvoices()

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
    Private Sub LoadAllInvoices()
        ''Try

        'GoTo Ac
        Dim i As Integer = 0
        Dim j As Integer = 19

        If chkbxGeneral.Checked = False Then
            grdInvoices.BringToFront()

Ab:
            ngrdRowCount = grdInvoicesV1.RowCount
            For i = 0 To ngrdRowCount
                grdInvoicesV1.DeleteRow(i)
            Next
            ngrdRowCount = grdInvoicesV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ab
            End If

            mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
            mdlSGM.dToDate = Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

            If chkbxSelectCustomer.Checked = False Then
                grdInvoices.DataSource = myccInvoicesWithDetails.LoadAllInvoices
            Else
                If chkbxFilter.Checked = True Then
                    If rbArticleName.Checked = True Then
                        grdInvoices.DataSource = myccInvoicesWithDetailsWithCustomerArticle.LoadAllInvoices(mdlSGM.sSelectedCustomer, mdlSGM.sSelectedArticle)
                    ElseIf rbCodification.Checked = True Then
                        grdInvoices.DataSource = myccInvoicesWithDetailsWithCustomerCodification.LoadAllInvoices(mdlSGM.sSelectedCustomer, mdlSGM.sSelectedCodification)
                    End If
                Else
                    grdInvoices.DataSource = myccInvoicesWithDetailsWithCustomer.LoadAllInvoices(mdlSGM.sSelectedCustomer)
                End If

            End If

            With grdInvoicesV1

                .Columns(1).VisibleIndex = -1
                .Columns(3).VisibleIndex = -1
                .Columns(4).VisibleIndex = -1
                .Columns(5).VisibleIndex = -1
                .Columns(6).VisibleIndex = -1
                .Columns(7).VisibleIndex = -1
                .Columns(11).VisibleIndex = -1
                .Columns(12).VisibleIndex = -1
                .Columns(23).VisibleIndex = -1
                .Columns(25).VisibleIndex = -1
                .Columns(29).VisibleIndex = -1

                .Columns(0).VisibleIndex = 30
                .Columns(8).VisibleIndex = 4

                .Columns(15).VisibleIndex = 31
                .Columns(16).VisibleIndex = 32
                .Columns(17).VisibleIndex = 33

                .Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                .Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                .Columns(2).Width = 150
                .Columns(14).Width = 100

                .Columns(32).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

                .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

                '.Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                '.Columns(9).DisplayFormat.FormatString = "dd-MMM-yyyy"

                For j = 19 To 32
                    .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(j).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .Columns(j).DisplayFormat.FormatString = "0.00"
                Next
                .Columns(19).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                .Columns(19).DisplayFormat.FormatString = "0"

            End With
        Else
            grdGeneralInvoices.BringToFront()

Ac:
            ngrdRowCount = grdGeneralInvoicesV1.RowCount
            For i = 0 To ngrdRowCount
                grdGeneralInvoicesV1.DeleteRow(i)
            Next
            ngrdRowCount = grdGeneralInvoicesV1.RowCount
            If ngrdRowCount > 0 Then
                GoTo Ac
            End If

            mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
            mdlSGM.dToDate = Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

            If chkbxSelectCustomer.Checked = False Then
                grdGeneralInvoices.DataSource = myccInvoicesWithDetails.LoadAllInvoices
            Else
                If chkbxFilter.Checked = True Then
                    If rbArticleName.Checked = True Then
                        grdGeneralInvoices.DataSource = myccInvoicesWithDetailsWithCustomerArticle.LoadAllInvoices(mdlSGM.sSelectedCustomer, mdlSGM.sSelectedArticle)
                    ElseIf rbCodification.Checked = True Then
                        grdInvoices.DataSource = myccInvoicesWithDetailsWithCustomerCodification.LoadAllInvoices(mdlSGM.sSelectedCustomer, mdlSGM.sSelectedCodification)
                    End If
                Else
                    grdGeneralInvoices.DataSource = myccInvoicesWithDetailsWithCustomer.LoadAllInvoices(mdlSGM.sSelectedCustomer)
                End If

            End If

            With grdGeneralInvoicesV1

                .Columns(1).VisibleIndex = -1
                .Columns(3).VisibleIndex = -1
                .Columns(4).VisibleIndex = -1
                .Columns(5).VisibleIndex = -1
                .Columns(6).VisibleIndex = -1
                .Columns(7).VisibleIndex = -1
                .Columns(11).VisibleIndex = -1
                .Columns(12).VisibleIndex = -1
                .Columns(23).VisibleIndex = -1
                .Columns(25).VisibleIndex = -1
                .Columns(29).VisibleIndex = -1

                .Columns(0).VisibleIndex = 30
                .Columns(15).VisibleIndex = 31
                .Columns(16).VisibleIndex = 32
                .Columns(17).VisibleIndex = 33

                .Columns(2).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                .Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left

                .Columns(2).Width = 150
                .Columns(14).Width = 100

                .Columns(32).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right

                .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                For j = 19 To 32
                    .Columns(j).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                    .Columns(j).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .Columns(j).DisplayFormat.FormatString = "0.00"
                Next
                .Columns(19).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                .Columns(19).DisplayFormat.FormatString = "0"

            End With
        End If
        MsgBox("Loading Completed", MsgBoxStyle.Information)
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub
    'Public Function LoadCustomer() As DataTable

    '    Dim sCmd As New SqlCommand
    '    Dim daSelCustomer As New SqlDataAdapter
    '    Dim dsSelCustomer As New DataSet

    '    sCmd.Connection = sCnn
    '    sCmd.CommandText = "sgm_InvoiceDetails"
    '    sCmd.CommandType = CommandType.StoredProcedure

    '    sCmd.Parameters.Add(New SqlParameter("@mAction", SqlDbType.VarChar)).Value() = "LoadCust"
    '    sCmd.Parameters.Add(New SqlParameter("@mFromDate", SqlDbType.DateTime)).Value() = mdlSGM.dFromDate
    '    sCmd.Parameters.Add(New SqlParameter("@mToDate", SqlDbType.DateTime)).Value() = mdlSGM.dToDate


    '    dsSelCustomer.Clear()
    '    daSelCustomer = New SqlDataAdapter(sCmd)
    '    daSelCustomer.Fill(dsSelCustomer, "Customer")
    '    Return dsSelCustomer.Tables(0)

    '    dsSelCustomer = Nothing
    '    sCnn.Close()

    'End Function
    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()
        cbxCustomer.DataSource = myccInvoicesWithDetails.LoadCustomer
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
        cbxArticleName.DataSource = myccInvoicesWithDetails.LoadArticleofCustomer
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
        cbxCodification.DataSource = myccInvoicesWithDetails.LoadCodificationofCustomer
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

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdInvoices.ExportToXlsx("E:\InvoiceDetails.xlsx")
        MsgBox("Export Completed")

    End Sub

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim i As Integer = 0


        myccInvoicesWithDetails.DelInvoice4Print()

        For i = 0 To grdInvoicesV1.RowCount - 1

            'mystrSolarInvoice4SGM4Print.PKID = grdInvoicesV1.GetDataRow(i).Item("").ToString 'As Integer
            mystrSolarInvoice4SGM4Print.BuyerGroup = grdInvoicesV1.GetDataRow(i).Item("BuyerGroup").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerCode = grdInvoicesV1.GetDataRow(i).Item("BuyerCode").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerName = grdInvoicesV1.GetDataRow(i).Item("BuyerName").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerAddress = grdInvoicesV1.GetDataRow(i).Item("BuyerAddress").ToString 'As String
            mystrSolarInvoice4SGM4Print.ConsigneeName = grdInvoicesV1.GetDataRow(i).Item("ConsigneeName").ToString 'As String
            mystrSolarInvoice4SGM4Print.ConsigneeAdress = grdInvoicesV1.GetDataRow(i).Item("ConsigneeAdress").ToString 'As String
            mystrSolarInvoice4SGM4Print.City = grdInvoicesV1.GetDataRow(i).Item("City").ToString 'As String
            mystrSolarInvoice4SGM4Print.Pincode = grdInvoicesV1.GetDataRow(i).Item("Pincode").ToString 'As String
            mystrSolarInvoice4SGM4Print.InvoiceNo = grdInvoicesV1.GetDataRow(i).Item("InvoiceNo").ToString 'As String
            mystrSolarInvoice4SGM4Print.InvDate = grdInvoicesV1.GetDataRow(i).Item("InvDate").ToString 'As Date
            mystrSolarInvoice4SGM4Print.InvType = grdInvoicesV1.GetDataRow(i).Item("InvType").ToString 'As String
            mystrSolarInvoice4SGM4Print.CT3 = grdInvoicesV1.GetDataRow(i).Item("CT3").ToString 'As String
            mystrSolarInvoice4SGM4Print.Accounted = grdInvoicesV1.GetDataRow(i).Item("Accounted").ToString 'As String
            mystrSolarInvoice4SGM4Print.Code = grdInvoicesV1.GetDataRow(i).Item("Code").ToString 'As String
            mystrSolarInvoice4SGM4Print.ArticleName = grdInvoicesV1.GetDataRow(i).Item("Sole").ToString 'As String
            mystrSolarInvoice4SGM4Print.Colour = grdInvoicesV1.GetDataRow(i).Item("Colour").ToString 'As String
            mystrSolarInvoice4SGM4Print.OldCodification = grdInvoicesV1.GetDataRow(i).Item("OldCodification").ToString 'As String
            mystrSolarInvoice4SGM4Print.CodificationNew = grdInvoicesV1.GetDataRow(i).Item("CodificationNew").ToString 'As String
            mystrSolarInvoice4SGM4Print.Quantity = grdInvoicesV1.GetDataRow(i).Item("Quantity").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.Rate = grdInvoicesV1.GetDataRow(i).Item("Rate").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.Value = grdInvoicesV1.GetDataRow(i).Item("Value").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.ExcisePercentage = grdInvoicesV1.GetDataRow(i).Item("ExcisePercentage").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.DWExciseDuty = grdInvoicesV1.GetDataRow(i).Item("DWExciseDuty").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.CessPercentage = grdInvoicesV1.GetDataRow(i).Item("CessPercentage").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DWCessAmount = grdInvoicesV1.GetDataRow(i).Item("DWCessAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.EduCessPercentage = grdInvoicesV1.GetDataRow(i).Item("EduCessPercentage").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DWEduCessAmount = grdInvoicesV1.GetDataRow(i).Item("DWEduCessAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DutyPayable = grdInvoicesV1.GetDataRow(i).Item("DutyPayable").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.SubTotal = grdInvoicesV1.GetDataRow(i).Item("SubTotal").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.CSTorVat = grdInvoicesV1.GetDataRow(i).Item("CSTorVat").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.CSTorVATAmount = grdInvoicesV1.GetDataRow(i).Item("CSTorVATAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.InvAmount = grdInvoicesV1.GetDataRow(i).Item("InvAmount").ToString 'As Decimal

            'myccArticleMaster.InsOutstanding4Print(mystrstrSolarArticleMaster4SGM4Print)
            myccInvoicesWithDetails.InsInvoice4Print(mystrSolarInvoice4SGM4Print)

        Next
        mdlSGM.sReportType = "Invoice"
        frmReport.Show()
    End Sub

    Private Sub chkbxSalesOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxSalesOrder.CheckedChanged
        If chkbxSalesOrder.Checked = True Then
            If chkbxAll.Checked = False And chkbxCT3.Checked = False And chkbxExport.Checked = False And chkbxFormC.Checked = False And chkbxFormH.Checked = False Then
                chkbxAll.Checked = True
            End If
            chkbxGeneral.Checked = False
        Else
            chkbxAll.Checked = False : chkbxCT3.Checked = False : chkbxExport.Checked = False : chkbxFormC.Checked = False : chkbxFormH.Checked = False
        End If
    End Sub

    Private Sub chkbxGeneral_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxGeneral.CheckedChanged
        If chkbxGeneral.Checked = True Then
            chkbxSalesOrder.Checked = False
            chkbxJobWorkOrder.Checked = False
        End If
    End Sub

    Private Sub chkbxJobWorkOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxJobWorkOrder.CheckedChanged
        If chkbxJobWorkOrder.Checked = True Then
            chkbxGeneral.Checked = False
        End If
    End Sub
End Class