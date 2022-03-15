Imports DevExpress.XtraGrid.Views.Grid
Public Class frmAllinOne1

    Dim myccAllinOne As New ccAllinOne

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub


    Dim ngrdRowCount As Integer



    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DsSalesOrder.SalesOrder' table. You can move, or remove it, as needed.
        Me.SalesOrderTableAdapter.Fill(Me.DsSalesOrder.SalesOrder)
        Me.SalesOrderDetailsTableAdapter1.Fill(Me.DsSalesOrder.SalesOrderDetails)

        GridView1.ExpandAllGroups()

        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        cbxTypeofOrder.SelectedIndex = 0
        cbxTypeofDocument.SelectedIndex = 0
        LoadCustomer()
        LoadArticle()
    End Sub


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        GridControl1.ExportToXlsx("E:\Sales Orders.xlsx")
        MsgBox("Export Completed")
        Exit Sub
        If rbSales.Checked = True And sTypeofDocument = "Order" Then
            grdSalesOrders.ExportToXlsx("E:\Sales Orders.xlsx")
        ElseIf rbSales.Checked = True And sTypeofDocument = "Invoice" Then
            grdSalesInvoices.ExportToXlsx("E:\Sales Invoices.xlsx")
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Order" Then
            grdPurchaseOrders.ExportToXlsx("E:\Purchase Orders.xlsx")
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Invoice" Then
            grdPurchaseInvoices.ExportToXlsx("E:\Purchase Invoices.xlsx")
        End If
        MsgBox("Export Completed")

    End Sub

    Dim sTypeofOrder, sTypeofDocument As String
    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        sTypeofOrder = cbxTypeofOrder.Text
        sTypeofDocument = cbxTypeofDocument.Text

        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()

        If rbSales.Checked = True Then
            cbxCustomer.DataSource = myccAllinOne.LoadCustomer(sTypeofOrder, sTypeofDocument)
            cbxCustomer.DisplayMember = "BuyerName" '': cbxArticleName.ValueMember = "PKID"
        Else

            cbxCustomer.DataSource = myccAllinOne.LoadSupplier(sTypeofOrder, sTypeofDocument)
            cbxCustomer.DisplayMember = "PartyName" '': cbxArticleName.ValueMember = "PKID"
            cbxCustomer.ValueMember = "PartyCode"
        End If

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
        If rbSales.Checked = True Then
            cbxArticleName.DataSource = myccAllinOne.LoadArticleofCustomer
            cbxArticleName.DisplayMember = "SoleName"
        Else
            cbxArticleName.DataSource = myccAllinOne.LoadMaterialsofSupplier
            cbxArticleName.DisplayMember = "MaterialWithType"
        End If





        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub cbxCustomer_GiveFeedback(ByVal sender As Object, ByVal e As System.Windows.Forms.GiveFeedbackEventArgs) Handles cbxCustomer.GiveFeedback

    End Sub

    Private Sub cbxCustomer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCustomer.GotFocus
        LoadCustomer()
    End Sub

    Private Sub cbxArticleName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxArticleName.GotFocus
        LoadArticle()
    End Sub

    Dim sPurchaseorSales, sCustomer, sOrderStatus, sArticleName, sArticleCode, sArticleDescription, sSummaryorDetailed As String

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Me.SalesOrderTableAdapter.Fill(Me.DsSalesOrder.SalesOrder)
        Me.SalesOrderDetailsTableAdapter1.Fill(Me.DsSalesOrder.SalesOrderDetails)

        Dim View As GridView = GridView1
        'Dim rowHandle As Integer = -1
        'Do
        '    'rowHandle = View.LocateByValue(rowHandle + 1, View.Columns("Is In Stock"), False)
        '    View.MakeRowVisible(rowHandle, False)
        'Loop Until rowHandle = GridControl1.InvalidRowHandle

        Dim i As Integer = 0

        For i = 0 To GridView1.RowCount - 1
            ''View.MakeRowVisible(i)
            View.ExpandMasterRow(i)
            'View.CollapseMasterRow(i)
        Next
        MsgBox("Loaded")
        Exit Sub
        If rbSales.Checked = True Then
            sPurchaseorSales = "Sales"
        Else
            sPurchaseorSales = "Purchase"
        End If
        sTypeofDocument = cbxTypeofDocument.Text
        sTypeofOrder = cbxTypeofOrder.Text
        sCustomer = cbxCustomer.Text
        mdlSGM.sSelectedCustomer = sCustomer

        If rbAll.Checked = True Then
            sOrderStatus = "All"
        ElseIf rbCompleted.Checked = True Then
            sOrderStatus = "Completed"
        ElseIf rbPending.Checked = True Then
            sOrderStatus = "Pending"
        End If


        sArticleName = cbxArticleName.Text
        mdlSGM.sSelectedArticle = sArticleName
        sArticleCode = tbArticleCode.Text
        sArticleDescription = tbArticleDescription.Text

        If rbSummary.Checked = True Then
            sSummaryorDetailed = "Summary"
        ElseIf rbDetailed.Checked = True Then
            sSummaryorDetailed = "Detailed"
        End If

        If sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sTypeofOrder = "All" Then '' Option 01


            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 01.01
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.01
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.01
                    mdlSGM.sSelectedOption = "S0AAAAAAS"     ''Option 01.01.01

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.02
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.02
                    mdlSGM.sSelectedOption = "S0AAAAAAD"     ''Option 01.01.02

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.03
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.03
                    mdlSGM.sSelectedOption = "S0AAAAASS"     ''Option 01.01.03

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.04
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.04
                    mdlSGM.sSelectedOption = "S0AAAAASD"     ''Option 01.01.04

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.05
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.05
                    mdlSGM.sSelectedOption = "S0AAAASAS"     ''Option 01.01.05

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.06
                    mdlSGM.sSelectedOption = "S0AAAASAD"     ''Option 01.01.06

                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.07
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.07
                    mdlSGM.sSelectedOption = "S0AAAASSS"     ''Option 01.01.07

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.08
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.08
                    mdlSGM.sSelectedOption = "S0AAAASSD"     ''Option 01.01.08

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.09
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.09
                    mdlSGM.sSelectedOption = "S0AAASAAS"     ''Option 01.01.09

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.10
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.10
                    mdlSGM.sSelectedOption = "S0AAASAAD"     ''Option 01.01.10

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.11
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.11
                    mdlSGM.sSelectedOption = "S0AAASASS"     ''Option 01.01.11

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.12
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.12
                    mdlSGM.sSelectedOption = "S0AAASASD"     ''Option 01.01.12

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.13
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.13
                    mdlSGM.sSelectedOption = "S0AAASSAS"     ''Option 01.01.13

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.14
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.14
                    mdlSGM.sSelectedOption = "S0AAASSAD"     ''Option 01.01.14

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.15
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.15
                    mdlSGM.sSelectedOption = "S0AAASSSS"     ''Option 01.01.15


                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.16
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.16
                    mdlSGM.sSelectedOption = "S0AAASSSD"     ''Option 01.01.16


                End If

                'ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Completed" Then '' Option 01.02
            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 01.02 & 01.03
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.01
                    mdlSGM.sSelectedOption = "S0AAFAAAS"     ''Option 01.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.02
                    mdlSGM.sSelectedOption = "S0AAFAAAD"     ''Option 01.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.03
                    mdlSGM.sSelectedOption = "S0AAFAASS"     ''Option 01.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.04
                    mdlSGM.sSelectedOption = "S0AAFAASD"     ''Option 01.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.05
                    mdlSGM.sSelectedOption = "S0AAFASAS"     ''Option 01.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.06
                    mdlSGM.sSelectedOption = "S0AAFASAD"     ''Option 01.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.07
                    mdlSGM.sSelectedOption = "S0AAFASSS"     ''Option 01.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.08
                    mdlSGM.sSelectedOption = "S0AAFASSD"     ''Option 01.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.09
                    mdlSGM.sSelectedOption = "S0AAFSAAS"     ''Option 01.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.10
                    mdlSGM.sSelectedOption = "S0AAFSAAD"     ''Option 01.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.11
                    mdlSGM.sSelectedOption = "S0AAFSASS"     ''Option 01.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.12
                    mdlSGM.sSelectedOption = "S0AAFSASD"     ''Option 01.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.13
                    mdlSGM.sSelectedOption = "S0AAFSSAS"     ''Option 01.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.14
                    mdlSGM.sSelectedOption = "S0AAFSSAD"     ''Option 01.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.15
                    mdlSGM.sSelectedOption = "S0AAFSSSS"     ''Option 01.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.16
                    mdlSGM.sSelectedOption = "S0AAFSSSD"     ''Option 01.02.16

                End If
            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 01.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 01.04
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.01
                    mdlSGM.sSelectedOption = "S0ASAAAAS"     ''Option 01.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.02
                    mdlSGM.sSelectedOption = "S0ASAAAAD"     ''Option 01.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.03
                    mdlSGM.sSelectedOption = "S0ASAAASS"     ''Option 01.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.04
                    mdlSGM.sSelectedOption = "S0ASAAASD"     ''Option 01.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.05
                    mdlSGM.sSelectedOption = "S0ASAASAS"     ''Option 01.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.06
                    mdlSGM.sSelectedOption = "S0ASAASAD"     ''Option 01.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.07
                    mdlSGM.sSelectedOption = "S0ASAASSS"     ''Option 01.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.08
                    mdlSGM.sSelectedOption = "S0ASAASSD"     ''Option 01.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.09
                    mdlSGM.sSelectedOption = "S0ASASAAS"     ''Option 01.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.10
                    mdlSGM.sSelectedOption = "S0ASASAAD"     ''Option 01.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.11
                    mdlSGM.sSelectedOption = "S0ASASASS"     ''Option 01.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.12
                    mdlSGM.sSelectedOption = "S0ASASASD"     ''Option 01.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.13
                    mdlSGM.sSelectedOption = "S0ASASSAS"     ''Option 01.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.14
                    mdlSGM.sSelectedOption = "S0ASASSAD"     ''Option 01.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.15
                    mdlSGM.sSelectedOption = "S0ASASSSS"     ''Option 01.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.16
                    mdlSGM.sSelectedOption = "S0ASASSSD"     ''Option 01.04.16
                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Completed" Then '' Option 01.05 & 01.06
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.01
                    mdlSGM.sSelectedOption = "S0ASFAAAS"     ''Option 01.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.02
                    mdlSGM.sSelectedOption = "S0ASFAAAD"     ''Option 01.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.03
                    mdlSGM.sSelectedOption = "S0ASFAASS"     ''Option 01.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.04
                    mdlSGM.sSelectedOption = "S0ASFAASD"     ''Option 01.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.05
                    mdlSGM.sSelectedOption = "S0ASFASAS"     ''Option 01.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.06
                    mdlSGM.sSelectedOption = "S0ASFASAD"     ''Option 01.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.07
                    mdlSGM.sSelectedOption = "S0ASFASSS"     ''Option 01.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.08
                    mdlSGM.sSelectedOption = "S0ASFASSD"     ''Option 01.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.09
                    mdlSGM.sSelectedOption = "S0ASFSAAS"     ''Option 01.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.10
                    mdlSGM.sSelectedOption = "S0ASFSAAD"     ''Option 01.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.11
                    mdlSGM.sSelectedOption = "S0ASFSASS"     ''Option 01.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.12
                    mdlSGM.sSelectedOption = "S0ASFSASD"     ''Option 01.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.13
                    mdlSGM.sSelectedOption = "S0ASFSSAS"     ''Option 01.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.14
                    mdlSGM.sSelectedOption = "S0ASFSSAD"     ''Option 01.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.15
                    mdlSGM.sSelectedOption = "S0ASFSSSS"     ''Option 01.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.16
                    mdlSGM.sSelectedOption = "S0ASFSSSD"     ''Option 01.05.16
                End If
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 01.06
            End If
        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sTypeofOrder <> "All" Then '' Option 02 / 03
            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 02.02
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.01
                    mdlSGM.sSelectedOption = "S0SAAAAAS"     ''Option 02.01.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.02
                    mdlSGM.sSelectedOption = "S0SAAAAAD"     ''Option 02.01.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.03
                    mdlSGM.sSelectedOption = "S0SAAAASS"     ''Option 02.01.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.04
                    mdlSGM.sSelectedOption = "S0SAAAASD"     ''Option 02.01.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.05
                    mdlSGM.sSelectedOption = "S0SAAASAS"     ''Option 02.01.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.06
                    mdlSGM.sSelectedOption = "S0SAAASAD"     ''Option 02.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.07
                    mdlSGM.sSelectedOption = "S0SAAASSS"     ''Option 02.01.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.08
                    mdlSGM.sSelectedOption = "S0SAAASSD"     ''Option 02.01.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.09
                    mdlSGM.sSelectedOption = "S0SAASAAS"     ''Option 02.01.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.10
                    mdlSGM.sSelectedOption = "S0SAASAAD"     ''Option 02.01.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.11
                    mdlSGM.sSelectedOption = "S0SAASASS"     ''Option 02.01.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.12
                    mdlSGM.sSelectedOption = "S0SAASASD"     ''Option 02.01.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.13
                    mdlSGM.sSelectedOption = "S0SAASSAS"     ''Option 02.01.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.14
                    mdlSGM.sSelectedOption = "S0SAASSAD"     ''Option 02.01.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.15
                    mdlSGM.sSelectedOption = "S0SAASSSS"     ''Option 02.01.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.16
                    mdlSGM.sSelectedOption = "S0SAASSSD"     ''Option 02.01.16
                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 02.02
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.01
                    mdlSGM.sSelectedOption = "S0SAFAAAS"     ''Option 02.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.02
                    mdlSGM.sSelectedOption = "S0SAFAAAD"     ''Option 02.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.03
                    mdlSGM.sSelectedOption = "S0SAFAASS"     ''Option 02.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.04
                    mdlSGM.sSelectedOption = "S0SAFAASD"     ''Option 02.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.05
                    mdlSGM.sSelectedOption = "S0SAFASAS"     ''Option 02.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.06
                    mdlSGM.sSelectedOption = "S0SAFASAD"     ''Option 02.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.07
                    mdlSGM.sSelectedOption = "S0SAFASSS"     ''Option 02.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.08
                    mdlSGM.sSelectedOption = "S0SAFASSD"     ''Option 02.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.09
                    mdlSGM.sSelectedOption = "S0SAFSAAS"     ''Option 02.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.10
                    mdlSGM.sSelectedOption = "S0SAFSAAD"     ''Option 02.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.11
                    mdlSGM.sSelectedOption = "S0SAFSASS"     ''Option 02.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.12
                    mdlSGM.sSelectedOption = "S0SAFSASD"     ''Option 02.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.13
                    mdlSGM.sSelectedOption = "S0SAFSSAS"     ''Option 02.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.14
                    mdlSGM.sSelectedOption = "S0SAFSSAD"     ''Option 02.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.15
                    mdlSGM.sSelectedOption = "S0SAFSSSS"     ''Option 02.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.16
                    mdlSGM.sSelectedOption = "S0SAFSSSD"     ''Option 02.02.16



                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 02.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 02.04
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.01
                    mdlSGM.sSelectedOption = "S0SSAAAAS"     ''Option 02.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.02
                    mdlSGM.sSelectedOption = "S0SSAAAAD"     ''Option 02.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.03
                    mdlSGM.sSelectedOption = "S0SSAAASS"     ''Option 02.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.04
                    mdlSGM.sSelectedOption = "S0SSAAASD"     ''Option 02.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.05
                    mdlSGM.sSelectedOption = "S0SSAASAS"     ''Option 02.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.06
                    mdlSGM.sSelectedOption = "S0SSAASAD"     ''Option 02.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.07
                    mdlSGM.sSelectedOption = "S0SSAASSS"     ''Option 02.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.08
                    mdlSGM.sSelectedOption = "S0SSAASSD"     ''Option 02.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.09
                    mdlSGM.sSelectedOption = "S0SSASAAS"     ''Option 02.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.10
                    mdlSGM.sSelectedOption = "S0SSASAAD"     ''Option 02.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.11
                    mdlSGM.sSelectedOption = "S0SSASASS"     ''Option 02.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.12
                    mdlSGM.sSelectedOption = "S0SSASASD"     ''Option 02.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.13
                    mdlSGM.sSelectedOption = "S0SSASSAS"     ''Option 02.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.14
                    mdlSGM.sSelectedOption = "S0SSASSAD"     ''Option 02.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.15
                    mdlSGM.sSelectedOption = "S0SSASSSS"     ''Option 02.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.16
                    mdlSGM.sSelectedOption = "S0SSASSSD"     ''Option 02.04.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 02.05
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.01
                    mdlSGM.sSelectedOption = "S0SSFAAAS"     ''Option 02.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.02
                    mdlSGM.sSelectedOption = "S0SSFAAAD"     ''Option 02.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.03
                    mdlSGM.sSelectedOption = "S0SSFAASS"     ''Option 02.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.04
                    mdlSGM.sSelectedOption = "S0SSFAASD"     ''Option 02.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.05
                    mdlSGM.sSelectedOption = "S0SSFASAS"     ''Option 02.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.06
                    mdlSGM.sSelectedOption = "S0SSFASAD"     ''Option 02.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.07
                    mdlSGM.sSelectedOption = "S0SSFASSS"     ''Option 02.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.08
                    mdlSGM.sSelectedOption = "S0SSFASSD"     ''Option 02.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.09
                    mdlSGM.sSelectedOption = "S0SSFSAAS"     ''Option 02.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.10
                    mdlSGM.sSelectedOption = "S0SSFSAAD"     ''Option 02.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.11
                    mdlSGM.sSelectedOption = "S0SSFSASS"     ''Option 02.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.12
                    mdlSGM.sSelectedOption = "S0SSFSASD"     ''Option 02.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.13
                    mdlSGM.sSelectedOption = "S0SSFSSAS"     ''Option 02.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.14
                    mdlSGM.sSelectedOption = "S0SSFSSAD"     ''Option 02.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.15
                    mdlSGM.sSelectedOption = "S0SSFSSSS"     ''Option 02.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.16
                    mdlSGM.sSelectedOption = "S0SSFSSSD"     ''Option 02.05.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 02.06
            End If
            'ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sTypeofOrder = "Sales" Then '' Option 03
        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Invoice" And sTypeofOrder = "All" Then '' Option 04

            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 04.01

                ''
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.01
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.01.01
                    mdlSGM.sSelectedOption = "SIAAAAAAS"     ''Option 04.01.01

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.02
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.01.02
                    mdlSGM.sSelectedOption = "SIAAAAAAD"     ''Option 04.01.02

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.03
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.01.03
                    mdlSGM.sSelectedOption = "SIAAAAASS"     ''Option 04.01.03

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.04
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.01.04
                    mdlSGM.sSelectedOption = "SIAAAAASD"     ''Option 04.01.04

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.05
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.01.05
                    mdlSGM.sSelectedOption = "SIAAAASAS"     ''Option 04.01.05

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.06
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.01.06
                    mdlSGM.sSelectedOption = "SIAAAASAD"     ''Option 04.01.06

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.07
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.01.07
                    mdlSGM.sSelectedOption = "SIAAAASSS"     ''Option 04.01.07

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.08
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.01.08
                    mdlSGM.sSelectedOption = "SIAAAASSD"     ''Option 04.01.08

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.09
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.01.09
                    mdlSGM.sSelectedOption = "SIAAASAAS"     ''Option 04.01.09

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.10
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.01.10
                    mdlSGM.sSelectedOption = "SIAAASAAD"     ''Option 04.01.10

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.11
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.01.11
                    mdlSGM.sSelectedOption = "SIAAASASS"     ''Option 04.01.11

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.12
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.01.12
                    mdlSGM.sSelectedOption = "SIAAASASD"     ''Option 04.01.12

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.13
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.01.13
                    mdlSGM.sSelectedOption = "SIAAASSAS"     ''Option 04.01.13

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.14
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.01.14
                    mdlSGM.sSelectedOption = "SIAAASSAD"     ''Option 04.01.14

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.01.15
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.01.15
                    mdlSGM.sSelectedOption = "SIAAASSSS"     ''Option 04.01.15

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.01.16
                    ''1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.01.16
                    mdlSGM.sSelectedOption = "SIAAASSSD"     ''Option 04.01.16

                End If
                ''


            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 04.02

                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.02.01
                    mdlSGM.sSelectedOption = "SIAAFAAAS"     ''Option 04.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.02.02
                    mdlSGM.sSelectedOption = "SIAAFAAAD"     ''Option 04.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.02.03
                    mdlSGM.sSelectedOption = "SIAAFAASS"     ''Option 04.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.02.04
                    mdlSGM.sSelectedOption = "SIAAFAASD"     ''Option 04.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.02.05
                    mdlSGM.sSelectedOption = "SIAAFASAS"     ''Option 04.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.02.06
                    mdlSGM.sSelectedOption = "SIAAFASAD"     ''Option 04.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.02.07
                    mdlSGM.sSelectedOption = "SIAAFASSS"     ''Option 04.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.02.08
                    mdlSGM.sSelectedOption = "SIAAFASSD"     ''Option 04.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.02.09
                    mdlSGM.sSelectedOption = "SIAAFSAAS"     ''Option 04.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.02.10
                    mdlSGM.sSelectedOption = "SIAAFSAAD"     ''Option 04.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.02.11
                    mdlSGM.sSelectedOption = "SIAAFSASS"     ''Option 04.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.02.12
                    mdlSGM.sSelectedOption = "SIAAFSASD"     ''Option 04.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.02.13
                    mdlSGM.sSelectedOption = "SIAAFSSAS"     ''Option 04.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.02.14
                    mdlSGM.sSelectedOption = "SIAAFSSAD"     ''Option 04.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.02.15
                    mdlSGM.sSelectedOption = "SIAAFSSSS"     ''Option 04.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.02.16
                    mdlSGM.sSelectedOption = "SIAAFSSSD"     ''Option 04.02.16



                End If


            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 04.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 04.04
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.04.01
                    mdlSGM.sSelectedOption = "SIASAAAAS"     ''Option 04.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.04.02
                    mdlSGM.sSelectedOption = "SIASAAAAD"     ''Option 04.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.04.03
                    mdlSGM.sSelectedOption = "SIASAAASS"     ''Option 04.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.04.04
                    mdlSGM.sSelectedOption = "SIASAAASD"     ''Option 04.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.04.05
                    mdlSGM.sSelectedOption = "SIASAASAS"     ''Option 04.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.04.06
                    mdlSGM.sSelectedOption = "SIASAASAD"     ''Option 04.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.04.07
                    mdlSGM.sSelectedOption = "SIASAASSS"     ''Option 04.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.04.08
                    mdlSGM.sSelectedOption = "SIASAASSD"     ''Option 04.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.04.09
                    mdlSGM.sSelectedOption = "SIASASAAS"     ''Option 04.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.04.10
                    mdlSGM.sSelectedOption = "SIASASAAD"     ''Option 04.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.04.11
                    mdlSGM.sSelectedOption = "SIASASASS"     ''Option 04.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.04.12
                    mdlSGM.sSelectedOption = "SIASASASD"     ''Option 04.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.04.13
                    mdlSGM.sSelectedOption = "SIASASSAS"     ''Option 04.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.04.14
                    mdlSGM.sSelectedOption = "SIASASSAD"     ''Option 04.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.04.15
                    mdlSGM.sSelectedOption = "SIASASSSS"     ''Option 04.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.04.16
                    mdlSGM.sSelectedOption = "SIASASSSD"     ''Option 04.04.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 04.05
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.01
                    mdlSGM.sSelectedOption = "SIASFAAAS"     ''Option 04.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.02
                    mdlSGM.sSelectedOption = "SIASFAAAD"     ''Option 04.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.03
                    mdlSGM.sSelectedOption = "SIASFAASS"     ''Option 04.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.04
                    mdlSGM.sSelectedOption = "SIASFAASD"     ''Option 04.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.05
                    mdlSGM.sSelectedOption = "SIASFASAS"     ''Option 04.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.06
                    mdlSGM.sSelectedOption = "SIASFASAD"     ''Option 04.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.07
                    mdlSGM.sSelectedOption = "SIASFASSS"     ''Option 04.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.08
                    mdlSGM.sSelectedOption = "SIASFASSD"     ''Option 04.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.09
                    mdlSGM.sSelectedOption = "SIASFSAAS"     ''Option 04.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.10
                    mdlSGM.sSelectedOption = "SIASFSAAD"     ''Option 04.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.11
                    mdlSGM.sSelectedOption = "SIASFSASS"     ''Option 04.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.12
                    mdlSGM.sSelectedOption = "SIASFSASD"     ''Option 04.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.13
                    mdlSGM.sSelectedOption = "SIASFSSAS"     ''Option 04.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.14
                    mdlSGM.sSelectedOption = "SIASFSSAD"     ''Option 04.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 04.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.15
                    mdlSGM.sSelectedOption = "SIASFSSSS"     ''Option 04.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 04.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.16
                    mdlSGM.sSelectedOption = "SIASFSSSD"     ''Option 04.05.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 04.06
            End If

        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Invoice" And sTypeofOrder <> "All" Then '' Option 05
            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 05.01
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.01
                    mdlSGM.sSelectedOption = "SISAAAAAS"     ''Option 05.01.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.02
                    mdlSGM.sSelectedOption = "SISAAAAAD"     ''Option 05.01.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.03
                    mdlSGM.sSelectedOption = "SISAAAASS"     ''Option 05.01.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.04
                    mdlSGM.sSelectedOption = "SISAAAASD"     ''Option 05.01.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.05
                    mdlSGM.sSelectedOption = "SISAAASAS"     ''Option 05.01.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.06
                    mdlSGM.sSelectedOption = "SISAAASAD"     ''Option 05.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.07
                    mdlSGM.sSelectedOption = "SISAAASSS"     ''Option 05.01.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.08
                    mdlSGM.sSelectedOption = "SISAAASSD"     ''Option 05.01.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.09
                    mdlSGM.sSelectedOption = "SISAASAAS"     ''Option 05.01.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.10
                    mdlSGM.sSelectedOption = "SISAASAAD"     ''Option 05.01.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.11
                    mdlSGM.sSelectedOption = "SISAASASS"     ''Option 05.01.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.12
                    mdlSGM.sSelectedOption = "SISAASASD"     ''Option 05.01.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 04.05.13
                    mdlSGM.sSelectedOption = "SISAASSAS"     ''Option 05.01.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 04.05.14
                    mdlSGM.sSelectedOption = "SISAASSAD"     ''Option 05.01.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.01.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 04.05.15
                    mdlSGM.sSelectedOption = "SISAASSSS"     ''Option 05.01.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.01.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 04.05.16
                    mdlSGM.sSelectedOption = "SISAASSSD"     ''Option 05.01.16



                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 05.02 / 05.03
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.02.01
                    mdlSGM.sSelectedOption = "SISAFAAAS"     ''Option 05.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.02.02
                    mdlSGM.sSelectedOption = "SISAFAAAD"     ''Option 05.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.02.03
                    mdlSGM.sSelectedOption = "SISAFAASS"     ''Option 05.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.02.04
                    mdlSGM.sSelectedOption = "SISAFAASD"     ''Option 05.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.02.05
                    mdlSGM.sSelectedOption = "SISAFASAS"     ''Option 05.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.02.06
                    mdlSGM.sSelectedOption = "SISAFASAD"     ''Option 05.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.02.07
                    mdlSGM.sSelectedOption = "SISAFASSS"     ''Option 05.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.02.08
                    mdlSGM.sSelectedOption = "SISAFASSD"     ''Option 05.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.02.09
                    mdlSGM.sSelectedOption = "SISAFSAAS"     ''Option 05.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.02.10
                    mdlSGM.sSelectedOption = "SISAFSAAD"     ''Option 05.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.02.11
                    mdlSGM.sSelectedOption = "SISAFSASS"     ''Option 05.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.02.12
                    mdlSGM.sSelectedOption = "SISAFSASD"     ''Option 05.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.02.13
                    mdlSGM.sSelectedOption = "SISAFSSAS"     ''Option 05.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.02.14
                    mdlSGM.sSelectedOption = "SISAFSSAD"     ''Option 05.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.02.15
                    mdlSGM.sSelectedOption = "SISAFSSSS"     ''Option 05.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.02.16
                    mdlSGM.sSelectedOption = "SISAFSSSD"     ''Option 05.02.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 05.04
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.04.01
                    mdlSGM.sSelectedOption = "SISSAAAAS"     ''Option 05.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.04.02
                    mdlSGM.sSelectedOption = "SISSAAAAD"     ''Option 05.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.04.03
                    mdlSGM.sSelectedOption = "SISSAAASS"     ''Option 05.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.04.04
                    mdlSGM.sSelectedOption = "SISSAAASD"     ''Option 05.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.04.05
                    mdlSGM.sSelectedOption = "SISSAASAS"     ''Option 05.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.04.06
                    mdlSGM.sSelectedOption = "SISSAASAD"     ''Option 05.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.04.07
                    mdlSGM.sSelectedOption = "SISSAASSS"     ''Option 05.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.04.08
                    mdlSGM.sSelectedOption = "SISSAASSD"     ''Option 05.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.04.09
                    mdlSGM.sSelectedOption = "SISSASAAS"     ''Option 05.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.04.10
                    mdlSGM.sSelectedOption = "SISSASAAD"     ''Option 05.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.04.11
                    mdlSGM.sSelectedOption = "SISSASASS"     ''Option 05.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.04.12
                    mdlSGM.sSelectedOption = "SISSASASD"     ''Option 05.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.04.13
                    mdlSGM.sSelectedOption = "SISSASSAS"     ''Option 05.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.04.14
                    mdlSGM.sSelectedOption = "SISSASSAD"     ''Option 05.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.04.15
                    mdlSGM.sSelectedOption = "SISSASSSS"     ''Option 05.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.04.16
                    mdlSGM.sSelectedOption = "SISSASSSD"     ''Option 05.04.16



                End If


            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 05.05 / 05.06
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.05.01
                    mdlSGM.sSelectedOption = "SISSFAAAS"     ''Option 05.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.05.02
                    mdlSGM.sSelectedOption = "SISSFAAAD"     ''Option 05.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.05.03
                    mdlSGM.sSelectedOption = "SISSFAASS"     ''Option 05.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.05.04
                    mdlSGM.sSelectedOption = "SISSFAASD"     ''Option 05.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.05.05
                    mdlSGM.sSelectedOption = "SISSFASAS"     ''Option 05.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.05.06
                    mdlSGM.sSelectedOption = "SISSFASAD"     ''Option 05.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.05.07
                    mdlSGM.sSelectedOption = "SISSFASSS"     ''Option 05.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.05.08
                    mdlSGM.sSelectedOption = "SISSFASSD"     ''Option 05.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.05.09
                    mdlSGM.sSelectedOption = "SISSFSAAS"     ''Option 05.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.05.10
                    mdlSGM.sSelectedOption = "SISSFSAAD"     ''Option 05.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.05.11
                    mdlSGM.sSelectedOption = "SISSFSASS"     ''Option 05.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.05.12
                    mdlSGM.sSelectedOption = "SISSFSASD"     ''Option 05.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 05.05.13
                    mdlSGM.sSelectedOption = "SISSFSSAS"     ''Option 05.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 05.05.14
                    mdlSGM.sSelectedOption = "SISSFSSAD"     ''Option 05.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 05.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 05.05.15
                    mdlSGM.sSelectedOption = "SISSFSSSS"     ''Option 05.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 05.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 05.05.16
                    mdlSGM.sSelectedOption = "SISSFSSSD"     ''Option 05.05.16



                End If

            End If

        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Invoice" And sTypeofOrder = "Sales" Then '' Option 06

        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Order" And sTypeofOrder = "All" Then '' Option 07

            If sCustomer = " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 07.01
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.01
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.01
                    mdlSGM.sSelectedOption = "P0AAAAAAS"     ''Option 07.01.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.02
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.02
                    mdlSGM.sSelectedOption = "P0AAAAAAD"     ''Option 07.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.03
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.03
                    mdlSGM.sSelectedOption = "P0AAAAASS"     ''Option 07.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.04
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.04
                    mdlSGM.sSelectedOption = "P0AAAAASD"     ''Option 07.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.05
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.05
                    mdlSGM.sSelectedOption = "P0AAAASAS"     ''Option 07.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.06
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.06
                    mdlSGM.sSelectedOption = "P0AAAASAD"     ''Option 07.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.07
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.07
                    mdlSGM.sSelectedOption = "P0AAAASSS"     ''Option 07.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.08
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.08
                    mdlSGM.sSelectedOption = "P0AAAASSD"     ''Option 07.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.09
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.09
                    mdlSGM.sSelectedOption = "P0AAASAAS"     ''Option 07.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.10
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.10
                    mdlSGM.sSelectedOption = "P0AAASAAD"     ''Option 07.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.11
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.11
                    mdlSGM.sSelectedOption = "P0AAASASS"     ''Option 07.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.12
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.12
                    mdlSGM.sSelectedOption = "P0AAASASD"     ''Option 07.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.13
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.13
                    mdlSGM.sSelectedOption = "P0AAASSAS"     ''Option 07.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.14
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.14
                    mdlSGM.sSelectedOption = "P0AAASSAD"     ''Option 07.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.15
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.15
                    mdlSGM.sSelectedOption = "P0AAASSSS"     ''Option 07.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.16
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.16
                    mdlSGM.sSelectedOption = "P0AAASSSD"     ''Option 07.01.16
                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 07.02 / 07.03
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.01
                    mdlSGM.sSelectedOption = "P0AAFAAAS"     ''Option 07.02.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.02
                    mdlSGM.sSelectedOption = "P0AAFAAAD"     ''Option 07.02.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.03
                    mdlSGM.sSelectedOption = "P0AAFAASS"     ''Option 07.02.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.04
                    mdlSGM.sSelectedOption = "P0AAFAASD"     ''Option 07.02.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.05
                    mdlSGM.sSelectedOption = "P0AAFASAS"     ''Option 07.02.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.06
                    mdlSGM.sSelectedOption = "P0AAFASAD"     ''Option 07.02.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.07
                    mdlSGM.sSelectedOption = "P0AAFASSS"     ''Option 07.02.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.08
                    mdlSGM.sSelectedOption = "P0AAFASSD"     ''Option 07.02.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.09
                    mdlSGM.sSelectedOption = "P0AAFSAAS"     ''Option 07.02.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.10
                    mdlSGM.sSelectedOption = "P0AAFSAAD"     ''Option 07.02.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.11
                    mdlSGM.sSelectedOption = "P0AAFSASS"     ''Option 07.02.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.12
                    mdlSGM.sSelectedOption = "P0AAFSASD"     ''Option 07.02.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.13
                    mdlSGM.sSelectedOption = "P0AAFSSAS"     ''Option 07.02.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.14
                    mdlSGM.sSelectedOption = "P0AAFSSAD"     ''Option 07.02.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.15
                    mdlSGM.sSelectedOption = "P0AAFSSSS"     ''Option 07.02.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.16
                    mdlSGM.sSelectedOption = "P0AAFSSSD"     ''Option 07.02.16



                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus = "Pending" Then '' Option 07.03
            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 07.04
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.01
                    mdlSGM.sSelectedOption = "P0ASAAAAS"     ''Option 07.04.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.02
                    mdlSGM.sSelectedOption = "P0ASAAAAD"     ''Option 07.04.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.03
                    mdlSGM.sSelectedOption = "P0ASAAASS"     ''Option 07.04.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.04
                    mdlSGM.sSelectedOption = "P0ASAAASD"     ''Option 07.04.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.05
                    mdlSGM.sSelectedOption = "P0ASAASAS"     ''Option 07.04.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.06
                    mdlSGM.sSelectedOption = "P0ASAASAD"     ''Option 07.04.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.07
                    mdlSGM.sSelectedOption = "P0ASAASSS"     ''Option 07.04.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.08
                    mdlSGM.sSelectedOption = "P0ASAASSD"     ''Option 07.04.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.09
                    mdlSGM.sSelectedOption = "P0ASASAAS"     ''Option 07.04.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.10
                    mdlSGM.sSelectedOption = "P0ASASAAD"     ''Option 07.04.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.11
                    mdlSGM.sSelectedOption = "P0ASASASS"     ''Option 07.04.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.12
                    mdlSGM.sSelectedOption = "P0ASASASD"     ''Option 07.04.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.13
                    mdlSGM.sSelectedOption = "P0ASASSAS"     ''Option 07.04.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.14
                    mdlSGM.sSelectedOption = "P0ASASSAD"     ''Option 07.04.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.15
                    mdlSGM.sSelectedOption = "P0ASASSSS"     ''Option 07.04.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.16
                    mdlSGM.sSelectedOption = "P0ASASSSD"     ''Option 07.04.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 07.05 / 07.06
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.01
                    mdlSGM.sSelectedOption = "P0ASFAAAS"     ''Option 07.05.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.02
                    mdlSGM.sSelectedOption = "P0ASFAAAD"     ''Option 07.05.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.03
                    mdlSGM.sSelectedOption = "P0ASFAASS"     ''Option 07.05.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.04
                    mdlSGM.sSelectedOption = "P0ASFAASD"     ''Option 07.05.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.05
                    mdlSGM.sSelectedOption = "P0ASFASAS"     ''Option 07.05.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.06
                    mdlSGM.sSelectedOption = "P0ASFASAD"     ''Option 07.05.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.07
                    mdlSGM.sSelectedOption = "P0ASFASSS"     ''Option 07.05.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.08
                    mdlSGM.sSelectedOption = "P0ASFASSD"     ''Option 07.05.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.09
                    mdlSGM.sSelectedOption = "P0ASFSAAS"     ''Option 07.05.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.10
                    mdlSGM.sSelectedOption = "P0ASFSAAD"     ''Option 07.05.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.11
                    mdlSGM.sSelectedOption = "P0ASFSASS"     ''Option 07.05.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.12
                    mdlSGM.sSelectedOption = "P0ASFSASD"     ''Option 07.05.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.13
                    mdlSGM.sSelectedOption = "P0ASFSSAS"     ''Option 07.05.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.14
                    mdlSGM.sSelectedOption = "P0ASFSSAD"     ''Option 07.05.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.15
                    mdlSGM.sSelectedOption = "P0ASFSSSS"     ''Option 07.05.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.16
                    mdlSGM.sSelectedOption = "P0ASFSSSD"     ''Option 07.05.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "Pending" Then '' Option 07.06
            End If
        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Order" And sTypeofOrder <> "All" Then '' Option 08
            If sCustomer = " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 08.01
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.01
                    mdlSGM.sSelectedOption = "P0SAAAAAS"     ''Option 08.01.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.02
                    mdlSGM.sSelectedOption = "P0SAAAAAD"     ''Option 08.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.03
                    mdlSGM.sSelectedOption = "P0SAAAASS"     ''Option 08.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.04
                    mdlSGM.sSelectedOption = "P0SAAAASD"     ''Option 08.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.05
                    mdlSGM.sSelectedOption = "P0SAAASAS"     ''Option 08.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.06
                    mdlSGM.sSelectedOption = "P0SAAASAD"     ''Option 08.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.07
                    mdlSGM.sSelectedOption = "P0SAAASSS"     ''Option 08.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.08
                    mdlSGM.sSelectedOption = "P0SAAASSD"     ''Option 08.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.09
                    mdlSGM.sSelectedOption = "P0SAASAAS"     ''Option 08.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.10
                    mdlSGM.sSelectedOption = "P0SAASAAD"     ''Option 08.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.11
                    mdlSGM.sSelectedOption = "P0SAASASS"     ''Option 08.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.12
                    mdlSGM.sSelectedOption = "P0SAASASD"     ''Option 08.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.13
                    mdlSGM.sSelectedOption = "P0SAASSAS"     ''Option 08.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.14
                    mdlSGM.sSelectedOption = "P0SAASSAD"     ''Option 08.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.15
                    mdlSGM.sSelectedOption = "P0SAASSSS"     ''Option 08.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.16
                    mdlSGM.sSelectedOption = "P0SAASSSD"     ''Option 08.01.16



                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 08.02 / 08.03
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.02.01
                    mdlSGM.sSelectedOption = "P0SAFAAAS"     ''Option 08.02.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.02.02
                    mdlSGM.sSelectedOption = "P0SAFAAAD"     ''Option 08.02.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.02.03
                    mdlSGM.sSelectedOption = "P0SAFAASS"     ''Option 08.02.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.02.04
                    mdlSGM.sSelectedOption = "P0SAFAASD"     ''Option 08.02.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.02.05
                    mdlSGM.sSelectedOption = "P0SAFASAS"     ''Option 08.02.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.02.06
                    mdlSGM.sSelectedOption = "P0SAFASAD"     ''Option 08.02.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.02.07
                    mdlSGM.sSelectedOption = "P0SAFASSS"     ''Option 08.02.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.02.08
                    mdlSGM.sSelectedOption = "P0SAFASSD"     ''Option 08.02.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.02.09
                    mdlSGM.sSelectedOption = "P0SAFSAAS"     ''Option 08.02.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.02.10
                    mdlSGM.sSelectedOption = "P0SAFSAAD"     ''Option 08.02.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.02.11
                    mdlSGM.sSelectedOption = "P0SAFSASS"     ''Option 08.02.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.02.12
                    mdlSGM.sSelectedOption = "P0SAFSASD"     ''Option 08.02.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.02.13
                    mdlSGM.sSelectedOption = "P0SAFSSAS"     ''Option 08.02.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.02.14
                    mdlSGM.sSelectedOption = "P0SAFSSAD"     ''Option 08.02.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.02.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.02.15
                    mdlSGM.sSelectedOption = "P0SAFSSSS"     ''Option 08.02.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.02.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.02.16
                    mdlSGM.sSelectedOption = "P0SAFSSSD"     ''Option 08.02.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 08.04
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.04.01
                    mdlSGM.sSelectedOption = "P0SSAAAAS"     ''Option 08.04.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.04.02
                    mdlSGM.sSelectedOption = "P0SSAAAAD"     ''Option 08.04.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.04.03
                    mdlSGM.sSelectedOption = "P0SSAAASS"     ''Option 08.04.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.04.04
                    mdlSGM.sSelectedOption = "P0SSAAASD"     ''Option 08.04.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.04.05
                    mdlSGM.sSelectedOption = "P0SSAASAS"     ''Option 08.04.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.04.06
                    mdlSGM.sSelectedOption = "P0SSAASAD"     ''Option 08.04.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.04.07
                    mdlSGM.sSelectedOption = "P0SSAASSS"     ''Option 08.04.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.04.08
                    mdlSGM.sSelectedOption = "P0SSAASSD"     ''Option 08.04.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.04.09
                    mdlSGM.sSelectedOption = "P0SSASAAS"     ''Option 08.04.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.04.10
                    mdlSGM.sSelectedOption = "P0SSASAAD"     ''Option 08.04.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.04.11
                    mdlSGM.sSelectedOption = "P0SSASASS"     ''Option 08.04.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.04.12
                    mdlSGM.sSelectedOption = "P0SSASASD"     ''Option 08.04.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.04.13
                    mdlSGM.sSelectedOption = "P0SSASSAS"     ''Option 08.04.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.04.14
                    mdlSGM.sSelectedOption = "P0SSASSAD"     ''Option 08.04.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.04.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.04.15
                    mdlSGM.sSelectedOption = "P0SSASSSS"     ''Option 08.04.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.04.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.04.16
                    mdlSGM.sSelectedOption = "P0SSASSSD"     ''Option 08.04.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 08.05 / 08.06
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.05.01
                    mdlSGM.sSelectedOption = "P0SSFAAAS"     ''Option 08.05.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.05.02
                    mdlSGM.sSelectedOption = "P0SSFAAAD"     ''Option 08.05.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.05.03
                    mdlSGM.sSelectedOption = "P0SSFAASS"     ''Option 08.05.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.05.04
                    mdlSGM.sSelectedOption = "P0SSFAASD"     ''Option 08.05.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.05.05
                    mdlSGM.sSelectedOption = "P0SSFASAS"     ''Option 08.05.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.05.06
                    mdlSGM.sSelectedOption = "P0SSFASAD"     ''Option 08.05.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.05.07
                    mdlSGM.sSelectedOption = "P0SSFASSS"     ''Option 08.05.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.05.08
                    mdlSGM.sSelectedOption = "P0SSFASSD"     ''Option 08.05.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.05.09
                    mdlSGM.sSelectedOption = "P0SSFSAAS"     ''Option 08.05.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.05.10
                    mdlSGM.sSelectedOption = "P0SSFSAAD"     ''Option 08.05.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.05.11
                    mdlSGM.sSelectedOption = "P0SSFSASS"     ''Option 08.05.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.05.12
                    mdlSGM.sSelectedOption = "P0SSFSASD"     ''Option 08.05.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.05.13
                    mdlSGM.sSelectedOption = "P0SSFSSAS"     ''Option 08.05.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.05.14
                    mdlSGM.sSelectedOption = "P0SSFSSAD"     ''Option 08.05.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.05.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.05.15
                    mdlSGM.sSelectedOption = "P0SSFSSSS"     ''Option 08.05.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.05.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.05.16
                    mdlSGM.sSelectedOption = "P0SSFSSSD"     ''Option 08.05.16



                End If

            End If

        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Order" And sTypeofOrder = "Sales" Then '' Option 09
        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Invoice" And sTypeofOrder = "All" Then '' Option 10

            If sCustomer = " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 10.01
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.01
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.01.01
                    mdlSGM.sSelectedOption = "PIAAAAAAS"     ''Option 10.01.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.02
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.01.02
                    mdlSGM.sSelectedOption = "PIAAAAAAD"     ''Option 10.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.03
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.01.03
                    mdlSGM.sSelectedOption = "PIAAAAASS"     ''Option 10.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.04
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.01.04
                    mdlSGM.sSelectedOption = "PIAAAAASD"     ''Option 10.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.05
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.01.05
                    mdlSGM.sSelectedOption = "PIAAAASAS"     ''Option 10.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.06
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.01.06
                    mdlSGM.sSelectedOption = "PIAAAASAD"     ''Option 10.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.07
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.01.07
                    mdlSGM.sSelectedOption = "PIAAAASSS"     ''Option 10.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.08
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.01.08
                    mdlSGM.sSelectedOption = "PIAAAASSD"     ''Option 10.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.09
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.01.09
                    mdlSGM.sSelectedOption = "PIAAASAAS"     ''Option 10.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.10
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.01.10
                    mdlSGM.sSelectedOption = "PIAAASAAD"     ''Option 10.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.11
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.01.11
                    mdlSGM.sSelectedOption = "PIAAASASS"     ''Option 10.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.12
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.01.12
                    mdlSGM.sSelectedOption = "PIAAASASD"     ''Option 10.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.13
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.01.13
                    mdlSGM.sSelectedOption = "PIAAASSAS"     ''Option 10.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.14
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.01.14
                    mdlSGM.sSelectedOption = "PIAAASSAD"     ''Option 10.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.01.15
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.01.15
                    mdlSGM.sSelectedOption = "PIAAASSSS"     ''Option 10.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.01.16
                    ''1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.01.16
                    mdlSGM.sSelectedOption = "PIAAASSSD"     ''Option 10.01.16
                End If



            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 10.02
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.02.01
                    mdlSGM.sSelectedOption = "PIAAFAAAS"     ''Option 10.02.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.02.02
                    mdlSGM.sSelectedOption = "PIAAFAAAD"     ''Option 10.02.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.02.03
                    mdlSGM.sSelectedOption = "PIAAFAASS"     ''Option 10.02.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.02.04
                    mdlSGM.sSelectedOption = "PIAAFAASD"     ''Option 10.02.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.02.05
                    mdlSGM.sSelectedOption = "PIAAFASAS"     ''Option 10.02.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.02.06
                    mdlSGM.sSelectedOption = "PIAAFASAD"     ''Option 10.02.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.02.07
                    mdlSGM.sSelectedOption = "PIAAFASSS"     ''Option 10.02.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.02.08
                    mdlSGM.sSelectedOption = "PIAAFASSD"     ''Option 10.02.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.02.09
                    mdlSGM.sSelectedOption = "PIAAFSAAS"     ''Option 10.02.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.02.10
                    mdlSGM.sSelectedOption = "PIAAFSAAD"     ''Option 10.02.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.02.11
                    mdlSGM.sSelectedOption = "PIAAFSASS"     ''Option 10.02.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.02.12
                    mdlSGM.sSelectedOption = "PIAAFSASD"     ''Option 10.02.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.02.13
                    mdlSGM.sSelectedOption = "PIAAFSSAS"     ''Option 10.02.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.02.14
                    mdlSGM.sSelectedOption = "PIAAFSSAD"     ''Option 10.02.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.02.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.02.15
                    mdlSGM.sSelectedOption = "PIAAFSSSS"     ''Option 10.02.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.02.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.02.16
                    mdlSGM.sSelectedOption = "PIAAFSSSD"     ''Option 10.02.16



                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus = "Pending" Then '' Option 10.03

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 10.04

                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.01
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.04.01
                    mdlSGM.sSelectedOption = "PIASAAAAS"     ''Option 10.04.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.02
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.04.02
                    mdlSGM.sSelectedOption = "PIASAAAAD"     ''Option 10.04.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.03
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.04.03
                    mdlSGM.sSelectedOption = "PIASAAASS"     ''Option 10.04.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.04
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.04.04
                    mdlSGM.sSelectedOption = "PIASAAASD"     ''Option 10.04.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.05
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.04.05
                    mdlSGM.sSelectedOption = "PIASAASAS"     ''Option 10.04.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.06
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.04.06
                    mdlSGM.sSelectedOption = "PIASAASAD"     ''Option 10.04.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.07
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.04.07
                    mdlSGM.sSelectedOption = "PIASAASSS"     ''Option 10.04.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.08
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.04.08
                    mdlSGM.sSelectedOption = "PIASAASSD"     ''Option 10.04.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.09
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.04.09
                    mdlSGM.sSelectedOption = "PIASASAAS"     ''Option 10.04.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.10
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.04.10
                    mdlSGM.sSelectedOption = "PIASASAAD"     ''Option 10.04.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.11
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.04.11
                    mdlSGM.sSelectedOption = "PIASASASS"     ''Option 10.04.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.12
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.04.12
                    mdlSGM.sSelectedOption = "PIASASASD"     ''Option 10.04.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.13
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.04.13
                    mdlSGM.sSelectedOption = "PIASASSAS"     ''Option 10.04.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.14
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.04.14
                    mdlSGM.sSelectedOption = "PIASASSAD"     ''Option 10.04.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.04.15
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.04.15
                    mdlSGM.sSelectedOption = "PIASASSSS"     ''Option 10.04.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.04.16
                    ' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.04.16
                    mdlSGM.sSelectedOption = "PIASASSSD"     ''Option 10.04.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 10.05
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.05.01
                    mdlSGM.sSelectedOption = "PIASFAAAS"     ''Option 10.05.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.05.02
                    mdlSGM.sSelectedOption = "PIASFAAAD"     ''Option 10.05.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.05.03
                    mdlSGM.sSelectedOption = "PIASFAASS"     ''Option 10.05.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.05.04
                    mdlSGM.sSelectedOption = "PIASFAASD"     ''Option 10.05.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.05.05
                    mdlSGM.sSelectedOption = "PIASFASAS"     ''Option 10.05.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.05.06
                    mdlSGM.sSelectedOption = "PIASFASAD"     ''Option 10.05.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.05.07
                    mdlSGM.sSelectedOption = "PIASFASSS"     ''Option 10.05.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.05.08
                    mdlSGM.sSelectedOption = "PIASFASSD"     ''Option 10.05.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.05.09
                    mdlSGM.sSelectedOption = "PIASFSAAS"     ''Option 10.05.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.05.10
                    mdlSGM.sSelectedOption = "PIASFSAAD"     ''Option 10.05.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.05.11
                    mdlSGM.sSelectedOption = "PIASFSASS"     ''Option 10.05.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.05.12
                    mdlSGM.sSelectedOption = "PIASFSASD"     ''Option 10.05.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 10.05.13
                    mdlSGM.sSelectedOption = "PIASFSSAS"     ''Option 10.05.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 10.05.14
                    mdlSGM.sSelectedOption = "PIASFSSAD"     ''Option 10.05.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 10.05.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 10.05.15
                    mdlSGM.sSelectedOption = "PIASFSSSS"     ''Option 10.05.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 10.05.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 10.05.16
                    mdlSGM.sSelectedOption = "PIASFSSSD"     ''Option 10.05.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "Pending" Then '' Option 10.06
            End If

        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Invoice" And sTypeofOrder <> "All" Then '' Option 11
            If sCustomer = " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 11.02
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.01.01
                    mdlSGM.sSelectedOption = "PISAAAAAS"     ''Option 11.01.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.01.02
                    mdlSGM.sSelectedOption = "PISAAAAAD"     ''Option 11.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.01.03
                    mdlSGM.sSelectedOption = "PISAAAASS"     ''Option 11.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.01.04
                    mdlSGM.sSelectedOption = "PISAAAASD"     ''Option 11.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.01.05
                    mdlSGM.sSelectedOption = "PISAAASAS"     ''Option 11.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.01.06
                    mdlSGM.sSelectedOption = "PISAAASAD"     ''Option 11.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.01.07
                    mdlSGM.sSelectedOption = "PISAAASSS"     ''Option 11.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.01.08
                    mdlSGM.sSelectedOption = "PISAAASSD"     ''Option 11.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.01.09
                    mdlSGM.sSelectedOption = "PISAASAAS"     ''Option 11.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.01.10
                    mdlSGM.sSelectedOption = "PISAASAAD"     ''Option 11.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.01.11
                    mdlSGM.sSelectedOption = "PISAASASS"     ''Option 11.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.01.12
                    mdlSGM.sSelectedOption = "PISAASASD"     ''Option 11.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.01.13
                    mdlSGM.sSelectedOption = "PISAASSAS"     ''Option 11.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.01.14
                    mdlSGM.sSelectedOption = "PISAASSAD"     ''Option 11.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.01.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.01.15
                    mdlSGM.sSelectedOption = "PISAASSSS"     ''Option 11.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.01.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.01.16
                    mdlSGM.sSelectedOption = "PISAASSSD"     ''Option 11.01.16



                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 11.02
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.02.01
                    mdlSGM.sSelectedOption = "PISAFAAAS"     ''Option 11.02.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.02.02
                    mdlSGM.sSelectedOption = "PISAFAAAD"     ''Option 11.02.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.02.03
                    mdlSGM.sSelectedOption = "PISAFAASS"     ''Option 11.02.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.02.04
                    mdlSGM.sSelectedOption = "PISAFAASD"     ''Option 11.02.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.02.05
                    mdlSGM.sSelectedOption = "PISAFASAS"     ''Option 11.02.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.02.06
                    mdlSGM.sSelectedOption = "PISAFASAD"     ''Option 11.02.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.02.07
                    mdlSGM.sSelectedOption = "PISAFASSS"     ''Option 11.02.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.02.08
                    mdlSGM.sSelectedOption = "PISAFASSD"     ''Option 11.02.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.02.09
                    mdlSGM.sSelectedOption = "PISAFSAAS"     ''Option 11.02.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.02.10
                    mdlSGM.sSelectedOption = "PISAFSAAD"     ''Option 11.02.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.02.11
                    mdlSGM.sSelectedOption = "PISAFSASS"     ''Option 11.02.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.02.12
                    mdlSGM.sSelectedOption = "PISAFSASD"     ''Option 11.02.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.02.13
                    mdlSGM.sSelectedOption = "PISAFSSAS"     ''Option 11.02.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.02.14
                    mdlSGM.sSelectedOption = "PISAFSSAD"     ''Option 11.02.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.02.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.02.15
                    mdlSGM.sSelectedOption = "PISAFSSSS"     ''Option 11.02.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.02.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.02.16
                    mdlSGM.sSelectedOption = "PISAFSSSD"     ''Option 11.02.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 11.04
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.04.01
                    mdlSGM.sSelectedOption = "PISSAAAAS"     ''Option 11.04.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.04.02
                    mdlSGM.sSelectedOption = "PISSAAAAD"     ''Option 11.04.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.04.03
                    mdlSGM.sSelectedOption = "PISSAAASS"     ''Option 11.04.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.04.04
                    mdlSGM.sSelectedOption = "PISSAAASD"     ''Option 11.04.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.04.05
                    mdlSGM.sSelectedOption = "PISSAASAS"     ''Option 11.04.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.04.06
                    mdlSGM.sSelectedOption = "PISSAASAD"     ''Option 11.04.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.04.07
                    mdlSGM.sSelectedOption = "PISSAASSS"     ''Option 11.04.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.04.08
                    mdlSGM.sSelectedOption = "PISSAASSD"     ''Option 11.04.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.04.09
                    mdlSGM.sSelectedOption = "PISSASAAS"     ''Option 11.04.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.04.10
                    mdlSGM.sSelectedOption = "PISSASAAD"     ''Option 11.04.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.04.11
                    mdlSGM.sSelectedOption = "PISSASASS"     ''Option 11.04.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.04.12
                    mdlSGM.sSelectedOption = "PISSASASD"     ''Option 11.04.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.04.13
                    mdlSGM.sSelectedOption = "PISSASSAS"     ''Option 11.04.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.04.14
                    mdlSGM.sSelectedOption = "PISSASSAD"     ''Option 11.04.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.04.15
                    mdlSGM.sSelectedOption = "PISSASSSS"     ''Option 11.04.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.04.16
                    mdlSGM.sSelectedOption = "PISSASSSD"     ''Option 11.04.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 11.05

                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.05.01
                    mdlSGM.sSelectedOption = "PISSFAAAS"     ''Option 11.05.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.05.02
                    mdlSGM.sSelectedOption = "PISSFAAAD"     ''Option 11.05.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.05.03
                    mdlSGM.sSelectedOption = "PISSFAASS"     ''Option 11.05.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.05.04
                    mdlSGM.sSelectedOption = "PISSFAASD"     ''Option 11.05.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.05.05
                    mdlSGM.sSelectedOption = "PISSFASAS"     ''Option 11.05.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.05.06
                    mdlSGM.sSelectedOption = "PISSFASAD"     ''Option 11.05.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.05.07
                    mdlSGM.sSelectedOption = "PISSFASSS"     ''Option 11.05.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.05.08
                    mdlSGM.sSelectedOption = "PISSFASSD"     ''Option 11.05.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.05.09
                    mdlSGM.sSelectedOption = "PISSFSAAS"     ''Option 11.05.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.05.10
                    mdlSGM.sSelectedOption = "PISSFSAAD"     ''Option 11.05.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.05.11
                    mdlSGM.sSelectedOption = "PISSFSASS"     ''Option 11.05.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.05.12
                    mdlSGM.sSelectedOption = "PISSFSASD"     ''Option 11.05.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 11.05.13
                    mdlSGM.sSelectedOption = "PISSFSSAS"     ''Option 11.05.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 11.05.14
                    mdlSGM.sSelectedOption = "PISSFSSAD"     ''Option 11.05.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 11.05.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 11.05.15
                    mdlSGM.sSelectedOption = "PISSFSSSS"     ''Option 11.05.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 11.05.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Invoice : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 11.05.16
                    mdlSGM.sSelectedOption = "PISSFSSSD"     ''Option 11.05.16



                End If

            End If

        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Invoice" And sTypeofOrder = "Sales" Then '' Option 12
        End If


        If rbSales.Checked = True And sTypeofDocument = "Order" Then
            LoadSalesOrders()
        ElseIf rbSales.Checked = True And sTypeofDocument = "Invoice" Then
            LoadSalesInvoices()
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Order" Then
            LoadPurchaseOrders()
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Invoice" Then
            LoadPurchaseInvoices()
        End If

        MsgBox("Loading Completed", MsgBoxStyle.Information)

    End Sub

    Private Sub LoadSalesOrders()
        Dim i As Integer = 0

        grdSalesOrders.BringToFront()

Ab:
        ngrdRowCount = grdSalesOrdersV1.RowCount
        For i = 0 To ngrdRowCount
            grdSalesOrdersV1.DeleteRow(i)
        Next
        ngrdRowCount = grdSalesOrdersV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            sOrderStatus = "CLOSE"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "OPEN"
        Else
            sOrderStatus = "All"
        End If

        If sTypeofOrder = "Sales" Then
            sTypeofOrder = "SO"
        ElseIf sTypeofOrder = "Job" Then
            sTypeofOrder = "JO"
        Else
            sTypeofOrder = "All"
        End If



        grdSalesOrders.DataSource = myccAllinOne.LoadSalesOrders(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sTypeofOrder)

        With grdSalesOrdersV1

            .Columns(6).VisibleIndex = -1

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            .Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With
    End Sub

    Private Sub LoadSalesInvoices()
        Dim i As Integer = 0
        Dim j As Integer = 19
        grdSalesInvoices.BringToFront()

Ab:
        ngrdRowCount = grdSalesInvoicesV1.RowCount
        For i = 0 To ngrdRowCount
            grdSalesInvoicesV1.DeleteRow(i)
        Next
        ngrdRowCount = grdSalesInvoicesV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")


        If sOrderStatus = "Completed" Then
            sOrderStatus = "CLOSE"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "OPEN"
        Else
            sOrderStatus = "All"
        End If

        If sTypeofOrder = "Sales" Then
            sTypeofOrder = "SO"
        ElseIf sTypeofOrder = "Job" Then
            sTypeofOrder = "JO"
        Else
            sTypeofOrder = "All"
        End If

        'grdSalesInvoices.DataSource = myccAllinOne.LoadSalesInvoices(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text))

        With grdSalesInvoicesV1

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
    End Sub

    Dim sMaterialTypeDescription, sMaterialSubTypeDescription As String


    Private Sub LoadPurchaseOrders()
        Dim i As Integer = 0

        grdPurchaseOrders.BringToFront()

Ab:
        ngrdRowCount = grdPurchaseOrdersV1.RowCount
        For i = 0 To ngrdRowCount
            grdPurchaseOrdersV1.DeleteRow(i)
        Next
        ngrdRowCount = grdPurchaseOrdersV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            sOrderStatus = "COMPLETE"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "PENDING"
        Else
            sOrderStatus = "All"
        End If

        If sTypeofOrder = "Sales" Then
            sTypeofOrder = "01"
        ElseIf sTypeofOrder = "Job" Then
            sTypeofOrder = "02"
        Else
            sTypeofOrder = "All"
        End If

        If sArticleName <> " ALL MATERIALS" Then
            Dim nLen As Integer = Microsoft.VisualBasic.Len(sArticleName)

            Dim j As Integer = 1

            For j = 1 To nLen
                If Microsoft.VisualBasic.Mid(sArticleName, j, 1) = ":" Then
                    Exit For
                End If
            Next


            sMaterialTypeDescription = Microsoft.VisualBasic.Left(sArticleName, j - 1)
            sMaterialSubTypeDescription = Microsoft.VisualBasic.Right(sArticleName, (nLen - (j + 1)))
        Else
            sMaterialTypeDescription = ""
            sMaterialSubTypeDescription = ""
        End If

        grdPurchaseOrders.DataSource = myccAllinOne.LoadPurchaseOrders(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sMaterialTypeDescription, sMaterialSubTypeDescription)

        With grdPurchaseOrdersV1

            .Columns(4).VisibleIndex = -1
            '.Columns(12).VisibleIndex = -1
            .Columns(13).VisibleIndex = -1
            .Columns(7).VisibleIndex = -1
            .Columns(8).VisibleIndex = -1

            .Columns(3).VisibleIndex = 14

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With
    End Sub

    Private Sub LoadPurchaseInvoices()
        Dim i As Integer = 0

        grdPurchaseInvoices.BringToFront()

Ab:
        ngrdRowCount = grdPurchaseInvoicesV1.RowCount
        For i = 0 To ngrdRowCount
            grdPurchaseInvoicesV1.DeleteRow(i)
        Next
        ngrdRowCount = grdPurchaseInvoicesV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            sOrderStatus = "COMPLETE"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "PENDING"
        Else
            sOrderStatus = "All"
        End If

        If sTypeofOrder = "Sales" Then
            sTypeofOrder = "01"
        ElseIf sTypeofOrder = "Job" Then
            sTypeofOrder = "02"
        Else
            sTypeofOrder = "All"
        End If

        If sArticleName <> " ALL MATERIALS" Then
            Dim nLen As Integer = Microsoft.VisualBasic.Len(sArticleName)

            Dim j As Integer = 1

            For j = 1 To nLen
                If Microsoft.VisualBasic.Mid(sArticleName, j, 1) = ":" Then
                    Exit For
                End If
            Next


            sMaterialTypeDescription = Microsoft.VisualBasic.Left(sArticleName, j - 1)
            sMaterialSubTypeDescription = Microsoft.VisualBasic.Right(sArticleName, (nLen - (j + 1)))
        Else
            sMaterialTypeDescription = ""
            sMaterialSubTypeDescription = ""
        End If

        grdPurchaseInvoices.DataSource = myccAllinOne.LoadPurchaseInvoices(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sMaterialTypeDescription, sMaterialSubTypeDescription)

        With grdPurchaseInvoicesV1
            .Columns(2).VisibleIndex = -1
            .Columns(7).VisibleIndex = -1
            .Columns(8).VisibleIndex = -1
            .Columns(13).VisibleIndex = -1
            .Columns(14).VisibleIndex = -1
            .Columns(15).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1

            .Columns(16).VisibleIndex = 0
            .Columns(18).VisibleIndex = 1
            .Columns(3).VisibleIndex = 10
            .Columns(4).VisibleIndex = 11

            .Columns(16).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            .Columns(4).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
            .Columns(3).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right



            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

            .Columns(4).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(4).DisplayFormat.FormatString = "dd-MMM-yyyy"

            .Columns(18).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(18).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With
    End Sub

    Private Sub cbxCustomer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxCustomer.SelectedIndexChanged

    End Sub

    Private Sub cbxArticleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxArticleName.SelectedIndexChanged

    End Sub

    Private Sub rbPurchase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPurchase.CheckedChanged

        LoadCustomer()
        LoadArticle()

    End Sub

    Public Sub ExpandAllRows(ByVal View As GridView)
        View.BeginUpdate()
        Try
            Dim dataRowCount As Integer = View.DataRowCount
            Dim rHandle As Integer
            For rHandle = 0 To dataRowCount - 1
                View.SetMasterRowExpanded(rHandle, True)
            Next
        Finally
            View.EndUpdate()
        End Try
    End Sub
End Class