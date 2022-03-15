Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmAllinOnev1
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccAllinOne As New ccAllinOne
    Dim myccInvoicesWithDetails As New ccInvoicesWithDetails

    Dim mystrSolarInvoice4SGM4Print As New strSolarInvoice4SGM4Print
    Dim mystrSolarPurchaseInvoice4SGM4Print As New strSolarPurchaseInvoice4SGM4Print
    Public sSampleOrderType As String

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
        cbxTypeofOrder.SelectedIndex = 0
        cbxTypeofDocument.SelectedIndex = 0
        cbxOrderStatus.SelectedIndex = 0
        LoadCustomer()
        LoadArticle()
        LoadBrand()
    End Sub


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        If rbSales.Checked = True And sTypeofDocument = "Order" Then
            grdSalesOrders.ExportToXlsx("E:\Sales Orders.xlsx")
            grdSalesOrderDetails.ExportToXlsx("E:\Sales Orders Details.xlsx")
        ElseIf rbSales.Checked = True And sTypeofDocument = "Invoice" Then
            grdSalesInvoices.ExportToXlsx("E:\Sales Invoices.xlsx")
        ElseIf rbSales.Checked = True And sTypeofDocument = "Credit Note Issued / Debit Not Received - For Sales" Then
            grdDCAgainstSales.ExportToXlsx("E:\Debit Note Against Sales.xlsx")
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Order" Then
            grdPurchaseOrders.ExportToXlsx("E:\Purchase Orders.xlsx")
            grdPurchaseOrder.ExportToXlsx("E:\Purchase Order Details.xlsx")
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Invoice" Then
            grdPurchaseInvoices.ExportToXlsx("E:\Purchase Invoices.xlsx")
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Credit Note Received / Debit Note Issued - For Purchase" Then
            grdDCAgainstPurchase.ExportToXlsx("E:\Credit Note Against Purchase.xlsx")
        End If
        MsgBox("Export Completed")

    End Sub

    Dim sTypeofOrder, sTypeofDocument As String
    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")



        sTypeofOrder = cbxTypeofOrder.Text
        If cbxTypeofDocument.Text = "Credit Note Issued / Debit Not Received - For Sales" Or cbxTypeofDocument.Text = "Credit Note Received / Debit Note Issued - For Purchase" Then
            sTypeofDocument = "Order"
        Else
            sTypeofDocument = cbxTypeofDocument.Text
        End If


        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()

        If rbSales.Checked = True Then
            cbxCustomer.DataSource = myccAllinOne.LoadCustomer(sTypeofOrder, sTypeofDocument)
            cbxCustomer.DisplayMember = "BuyerName" '': cbxArticleName.ValueMember = "PKID"
        Else

            cbxCustomer.DataSource = myccAllinOne.LoadSupplier(sTypeofOrder, sTypeofDocument)
            cbxCustomer.DisplayMember = "PartyName" '': cbxArticleName.ValueMember = "PKID"
            cbxCustomer.ValueMember = "PartyCode"
        End If
        sTypeofDocument = cbxTypeofDocument.Text
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadBrand()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        cbxBrand.DataSource = Nothing : cbxBrand.Items.Clear()

        If rbSales.Checked = True Then
            cbxBrand.DataSource = myccAllinOne.LoadBrand(sTypeofOrder, sTypeofDocument)
            cbxBrand.DisplayMember = "Brand" '': cbxArticleName.ValueMember = "PKID"
        Else

            cbxBrand.DataSource = myccAllinOne.LoadSupplier(sTypeofOrder, sTypeofDocument)
            cbxBrand.DisplayMember = "PartyName" '': cbxArticleName.ValueMember = "PKID"
            cbxBrand.ValueMember = "PartyCode"
        End If
        sTypeofDocument = cbxTypeofDocument.Text
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

    Private Sub LoadSampleType()
        ''Try
        'mdlSGM.sSelectedCustomer = cbxCustomer.Text
        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxSampleOrderType.DataSource = Nothing : cbxSampleOrderType.Items.Clear()
        If rbOrderProduction.Checked = True Then
            sOrderType = "PRODUCTIONSOLETYPE"
        Else
            sOrderType = "SOLESAMPLETYPE"
        End If
        cbxSampleOrderType.DataSource = myccAllinOne.LoadSampleType(sOrderType)
        cbxSampleOrderType.DisplayMember = "FullName_"
        cbxSampleOrderType.ValueMember = "Abbrev_"





        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub cbxCustomer_GiveFeedback(ByVal sender As Object, ByVal e As System.Windows.Forms.GiveFeedbackEventArgs) Handles cbxCustomer.GiveFeedback

    End Sub

    Private Sub cbxCustomer_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxCustomer.GotFocus
        LoadCustomer()
    End Sub

    Private Sub cbxBrand_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxBrand.GotFocus
        LoadBrand()
    End Sub

    Private Sub cbxArticleName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxArticleName.GotFocus
        LoadArticle()
    End Sub

    Dim sPurchaseorSales, sCustomer, sOrderStatus, sArticleName, sArticleCode, sArticleDescription, sSummaryorDetailed As String

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click

        If cbxOrderStatus.Text = "All" Then
            rbAll.Checked = True
        ElseIf cbxOrderStatus.Text = "Completed" Then
            rbCompleted.Checked = True
        ElseIf cbxOrderStatus.Text = "Pending" Then
            rbPending.Checked = True
        Else
            rbClose.Checked = True
        End If

        If cbxBrand.Text = "" Then
            sBrand = " ALL BRANDS"
        Else
            sBrand = cbxBrand.Text
        End If


        If rbSales.Checked = True Then
            sPurchaseorSales = "Sales"
            If cbxTypeofDocument.Text = "Credit Note Received / Debit Note Issued - For Purchase" Then
                MsgBox("In Correct Combination with Sales to Credit Note from Supplier", MsgBoxStyle.Critical)
                Exit Sub
            End If
        Else
            sPurchaseorSales = "Purchase"
            If cbxTypeofDocument.Text = "Credit Note Issued / Debit Not Received - For Sales" Then
                MsgBox("In Correct Combination with Purchase to Debit Note from Customer", MsgBoxStyle.Critical)
                Exit Sub
            End If
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
        ElseIf rbClose.Checked = True Then
            sOrderStatus = "Close"
        End If


        sArticleName = cbxArticleName.Text
        If sArticleName = "" Then
            sArticleName = " ALL ARTICLES"
        End If
        mdlSGM.sSelectedArticle = sArticleName
        sArticleCode = tbArticleCode.Text
        sArticleDescription = tbArticleDescription.Text

        If rbSummary.Checked = True Then
            sSummaryorDetailed = "Summary"
        ElseIf rbDetailed.Checked = True Then
            sSummaryorDetailed = "Detailed"
        End If

        If sTypeofDocument = "Credit Note Issued / Debit Not Received - For Sales" Then
            DebitCreditNoteSales()
            GoTo Aa
        ElseIf sTypeofDocument = "Credit Note Received / Debit Note Issued - For Purchase" Then
            DebitCreditNotePurchase()
            GoTo Aa
        End If



        If sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sTypeofOrder = "All" Then '' Option 01


            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 01.01
                mdlSGM.nOption = 101
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.01
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.01
                    mdlSGM.sSelectedOption = "S0AAAAAAS"     ''Option 01.01.01

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.02
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.02
                    mdlSGM.sSelectedOption = "S0AAAAAAS"     ''Option 01.01.02

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.03
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.03
                    mdlSGM.sSelectedOption = "S0AAAAASS"     ''Option 01.01.03

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.04
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.04
                    mdlSGM.sSelectedOption = "S0AAAAASS"     ''Option 01.01.04

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.05
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.05
                    mdlSGM.sSelectedOption = "S0AAAASAS"     ''Option 01.01.05

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.06
                    mdlSGM.sSelectedOption = "S0AAAASAS"     ''Option 01.01.06

                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.07
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.07
                    mdlSGM.sSelectedOption = "S0AAAASSS"     ''Option 01.01.07

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.08
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.08
                    mdlSGM.sSelectedOption = "S0AAAASSS"     ''Option 01.01.08

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.09
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.09
                    mdlSGM.sSelectedOption = "S0AAASAAS"     ''Option 01.01.09

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.10
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.10
                    mdlSGM.sSelectedOption = "S0AAASAAS"     ''Option 01.01.10

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.11
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.11
                    mdlSGM.sSelectedOption = "S0AAASASS"     ''Option 01.01.11

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.12
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.12
                    mdlSGM.sSelectedOption = "S0AAASASS"     ''Option 01.01.12

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.13
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.13
                    mdlSGM.sSelectedOption = "S0AAASSAS"     ''Option 01.01.13

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.14
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.14
                    mdlSGM.sSelectedOption = "S0AAASSAS"     ''Option 01.01.14

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.15
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.15
                    mdlSGM.sSelectedOption = "S0AAASSSS"     ''Option 01.01.15


                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.16
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.16
                    mdlSGM.sSelectedOption = "S0AAASSSS"     ''Option 01.01.16


                End If

                'ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Completed" Then '' Option 01.02
            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 01.02 & 01.03
                mdlSGM.nOption = 102
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.01
                    mdlSGM.sSelectedOption = "S0AAFAAAS"     ''Option 01.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.02
                    mdlSGM.sSelectedOption = "S0AAFAAAS"     ''Option 01.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.03
                    mdlSGM.sSelectedOption = "S0AAFAASS"     ''Option 01.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.04
                    mdlSGM.sSelectedOption = "S0AAFAASS"     ''Option 01.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.05
                    mdlSGM.sSelectedOption = "S0AAFASAS"     ''Option 01.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.06
                    mdlSGM.sSelectedOption = "S0AAFASAS"     ''Option 01.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.07
                    mdlSGM.sSelectedOption = "S0AAFASSS"     ''Option 01.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.08
                    mdlSGM.sSelectedOption = "S0AAFASSS"     ''Option 01.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.09
                    mdlSGM.sSelectedOption = "S0AAFSAAS"     ''Option 01.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.10
                    mdlSGM.sSelectedOption = "S0AAFSAAS"     ''Option 01.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.11
                    mdlSGM.sSelectedOption = "S0AAFSASS"     ''Option 01.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.12
                    mdlSGM.sSelectedOption = "S0AAFSASS"     ''Option 01.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.13
                    mdlSGM.sSelectedOption = "S0AAFSSAS"     ''Option 01.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.14
                    mdlSGM.sSelectedOption = "S0AAFSSAS"     ''Option 01.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.15
                    mdlSGM.sSelectedOption = "S0AAFSSSS"     ''Option 01.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.16
                    mdlSGM.sSelectedOption = "S0AAFSSSS"     ''Option 01.02.16

                End If
            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 01.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 01.04
                mdlSGM.nOption = 104
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.01
                    mdlSGM.sSelectedOption = "S0ASAAAAS"     ''Option 01.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.02
                    mdlSGM.sSelectedOption = "S0ASAAAAS"     ''Option 01.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.03
                    mdlSGM.sSelectedOption = "S0ASAAASS"     ''Option 01.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.04
                    mdlSGM.sSelectedOption = "S0ASAAASS"     ''Option 01.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.05
                    mdlSGM.sSelectedOption = "S0ASAASAS"     ''Option 01.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.06
                    mdlSGM.sSelectedOption = "S0ASAASAS"     ''Option 01.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.07
                    mdlSGM.sSelectedOption = "S0ASAASSS"     ''Option 01.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.08
                    mdlSGM.sSelectedOption = "S0ASAASSS"     ''Option 01.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.09
                    mdlSGM.sSelectedOption = "S0ASASAAS"     ''Option 01.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.10
                    mdlSGM.sSelectedOption = "S0ASASAAS"     ''Option 01.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.11
                    mdlSGM.sSelectedOption = "S0ASASASS"     ''Option 01.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.12
                    mdlSGM.sSelectedOption = "S0ASASASS"     ''Option 01.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.13
                    mdlSGM.sSelectedOption = "S0ASASSAS"     ''Option 01.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.14
                    mdlSGM.sSelectedOption = "S0ASASSAS"     ''Option 01.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.15
                    mdlSGM.sSelectedOption = "S0ASASSSS"     ''Option 01.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.16
                    mdlSGM.sSelectedOption = "S0ASASSSS"     ''Option 01.04.16
                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 01.05 & 01.06
                mdlSGM.nOption = 105
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.01
                    mdlSGM.sSelectedOption = "S0ASFAAAS"     ''Option 01.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.02
                    mdlSGM.sSelectedOption = "S0ASFAAAS"     ''Option 01.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.03
                    mdlSGM.sSelectedOption = "S0ASFAASS"     ''Option 01.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.04
                    mdlSGM.sSelectedOption = "S0ASFAASS"     ''Option 01.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.05
                    mdlSGM.sSelectedOption = "S0ASFASAS"     ''Option 01.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.06
                    mdlSGM.sSelectedOption = "S0ASFASAS"     ''Option 01.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.07
                    mdlSGM.sSelectedOption = "S0ASFASSS"     ''Option 01.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.08
                    mdlSGM.sSelectedOption = "S0ASFASSS"     ''Option 01.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.09
                    mdlSGM.sSelectedOption = "S0ASFSAAS"     ''Option 01.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.10
                    mdlSGM.sSelectedOption = "S0ASFSAAS"     ''Option 01.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.11
                    mdlSGM.sSelectedOption = "S0ASFSASS"     ''Option 01.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.12
                    mdlSGM.sSelectedOption = "S0ASFSASS"     ''Option 01.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.13
                    mdlSGM.sSelectedOption = "S0ASFSSAS"     ''Option 01.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.14
                    mdlSGM.sSelectedOption = "S0ASFSSAS"     ''Option 01.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.15
                    mdlSGM.sSelectedOption = "S0ASFSSSS"     ''Option 01.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.16
                    mdlSGM.sSelectedOption = "S0ASFSSSS"     ''Option 01.05.16
                End If
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 01.06
            End If
        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sTypeofOrder <> "All" Then '' Option 02 / 03
            mdlSGM.nOption = 201
            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 02.02

                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.01
                    mdlSGM.sSelectedOption = "S0SAAAAAS"     ''Option 02.01.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.02
                    mdlSGM.sSelectedOption = "S0SAAAAAS"     ''Option 02.01.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.03
                    mdlSGM.sSelectedOption = "S0SAAAASS"     ''Option 02.01.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.04
                    mdlSGM.sSelectedOption = "S0SAAAASS"     ''Option 02.01.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.05
                    mdlSGM.sSelectedOption = "S0SAAASAS"     ''Option 02.01.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.06
                    mdlSGM.sSelectedOption = "S0SAAASAS"     ''Option 02.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.07
                    mdlSGM.sSelectedOption = "S0SAAASSS"     ''Option 02.01.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.08
                    mdlSGM.sSelectedOption = "S0SAAASSS"     ''Option 02.01.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.09
                    mdlSGM.sSelectedOption = "S0SAASAAS"     ''Option 02.01.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.10
                    mdlSGM.sSelectedOption = "S0SAASAAS"     ''Option 02.01.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.11
                    mdlSGM.sSelectedOption = "S0SAASASS"     ''Option 02.01.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.12
                    mdlSGM.sSelectedOption = "S0SAASASS"     ''Option 02.01.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.13
                    mdlSGM.sSelectedOption = "S0SAASSAS"     ''Option 02.01.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.14
                    mdlSGM.sSelectedOption = "S0SAASSAS"     ''Option 02.01.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.15
                    mdlSGM.sSelectedOption = "S0SAASSSS"     ''Option 02.01.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.16
                    mdlSGM.sSelectedOption = "S0SAASSSS"     ''Option 02.01.16
                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 02.02
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.01
                    mdlSGM.sSelectedOption = "S0SAFAAAS"     ''Option 02.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.02
                    mdlSGM.sSelectedOption = "S0SAFAAAS"     ''Option 02.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.03
                    mdlSGM.sSelectedOption = "S0SAFAASS"     ''Option 02.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.04
                    mdlSGM.sSelectedOption = "S0SAFAASS"     ''Option 02.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.05
                    mdlSGM.sSelectedOption = "S0SAFASAS"     ''Option 02.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.06
                    mdlSGM.sSelectedOption = "S0SAFASAS"     ''Option 02.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.07
                    mdlSGM.sSelectedOption = "S0SAFASSS"     ''Option 02.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.08
                    mdlSGM.sSelectedOption = "S0SAFASSS"     ''Option 02.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.09
                    mdlSGM.sSelectedOption = "S0SAFSAAS"     ''Option 02.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.10
                    mdlSGM.sSelectedOption = "S0SAFSAAS"     ''Option 02.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.11
                    mdlSGM.sSelectedOption = "S0SAFSASS"     ''Option 02.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.12
                    mdlSGM.sSelectedOption = "S0SAFSASS"     ''Option 02.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.13
                    mdlSGM.sSelectedOption = "S0SAFSSAS"     ''Option 02.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.14
                    mdlSGM.sSelectedOption = "S0SAFSSAS"     ''Option 02.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.15
                    mdlSGM.sSelectedOption = "S0SAFSSSS"     ''Option 02.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.16
                    mdlSGM.sSelectedOption = "S0SAFSSSS"     ''Option 02.02.16



                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 02.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 02.04
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.01
                    mdlSGM.sSelectedOption = "S0SSAAAAS"     ''Option 02.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.02
                    mdlSGM.sSelectedOption = "S0SSAAAAS"     ''Option 02.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.03
                    mdlSGM.sSelectedOption = "S0SSAAASS"     ''Option 02.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.04
                    mdlSGM.sSelectedOption = "S0SSAAASS"     ''Option 02.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.05
                    mdlSGM.sSelectedOption = "S0SSAASAS"     ''Option 02.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.06
                    mdlSGM.sSelectedOption = "S0SSAASAS"     ''Option 02.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.07
                    mdlSGM.sSelectedOption = "S0SSAASSS"     ''Option 02.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.08
                    mdlSGM.sSelectedOption = "S0SSAASSS"     ''Option 02.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.09
                    mdlSGM.sSelectedOption = "S0SSASAAS"     ''Option 02.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.10
                    mdlSGM.sSelectedOption = "S0SSASAAS"     ''Option 02.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.11
                    mdlSGM.sSelectedOption = "S0SSASASS"     ''Option 02.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.12
                    mdlSGM.sSelectedOption = "S0SSASASS"     ''Option 02.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.13
                    mdlSGM.sSelectedOption = "S0SSASSAS"     ''Option 02.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.14
                    mdlSGM.sSelectedOption = "S0SSASSAS"     ''Option 02.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.15
                    mdlSGM.sSelectedOption = "S0SSASSSS"     ''Option 02.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.16
                    mdlSGM.sSelectedOption = "S0SSASSSS"     ''Option 02.04.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 02.05
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.01
                    mdlSGM.sSelectedOption = "S0SSFAAAS"     ''Option 02.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.02
                    mdlSGM.sSelectedOption = "S0SSFAAAS"     ''Option 02.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.03
                    mdlSGM.sSelectedOption = "S0SSFAASS"     ''Option 02.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.04
                    mdlSGM.sSelectedOption = "S0SSFAASS"     ''Option 02.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.05
                    mdlSGM.sSelectedOption = "S0SSFASAS"     ''Option 02.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.06
                    mdlSGM.sSelectedOption = "S0SSFASAS"     ''Option 02.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.07
                    mdlSGM.sSelectedOption = "S0SSFASSS"     ''Option 02.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.08
                    mdlSGM.sSelectedOption = "S0SSFASSS"     ''Option 02.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.09
                    mdlSGM.sSelectedOption = "S0SSFSAAS"     ''Option 02.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.10
                    mdlSGM.sSelectedOption = "S0SSFSAAS"     ''Option 02.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.11
                    mdlSGM.sSelectedOption = "S0SSFSASS"     ''Option 02.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.12
                    mdlSGM.sSelectedOption = "S0SSFSASS"     ''Option 02.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.13
                    mdlSGM.sSelectedOption = "S0SSFSSAS"     ''Option 02.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.14
                    mdlSGM.sSelectedOption = "S0SSFSSAS"     ''Option 02.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.15
                    mdlSGM.sSelectedOption = "S0SSFSSSS"     ''Option 02.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.16
                    mdlSGM.sSelectedOption = "S0SSFSSSS"     ''Option 02.05.16



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
                    mdlSGM.sSelectedOption = "P0AAAAAAS"     ''Option 07.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.03
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.03
                    mdlSGM.sSelectedOption = "P0AAAAASS"     ''Option 07.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.04
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.04
                    mdlSGM.sSelectedOption = "P0AAAAASS"     ''Option 07.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.05
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.05
                    mdlSGM.sSelectedOption = "P0AAAASAS"     ''Option 07.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.06
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.06
                    mdlSGM.sSelectedOption = "P0AAAASAS"     ''Option 07.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.07
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.07
                    mdlSGM.sSelectedOption = "P0AAAASSS"     ''Option 07.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.08
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.08
                    mdlSGM.sSelectedOption = "P0AAAASSS"     ''Option 07.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.09
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.09
                    mdlSGM.sSelectedOption = "P0AAASAAS"     ''Option 07.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.10
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.10
                    mdlSGM.sSelectedOption = "P0AAASAAS"     ''Option 07.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.11
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.11
                    mdlSGM.sSelectedOption = "P0AAASASS"     ''Option 07.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.12
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.12
                    mdlSGM.sSelectedOption = "P0AAASASS"     ''Option 07.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.13
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.13
                    mdlSGM.sSelectedOption = "P0AAASSAS"     ''Option 07.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.14
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.14
                    mdlSGM.sSelectedOption = "P0AAASSAS"     ''Option 07.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.15
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.15
                    mdlSGM.sSelectedOption = "P0AAASSSS"     ''Option 07.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.16
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.16
                    mdlSGM.sSelectedOption = "P0AAASSSS"     ''Option 07.01.16
                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 07.02 / 07.03
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.01
                    mdlSGM.sSelectedOption = "P0AAFAAAS"     ''Option 07.02.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.02
                    mdlSGM.sSelectedOption = "P0AAFAAAS"     ''Option 07.02.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.03
                    mdlSGM.sSelectedOption = "P0AAFAASS"     ''Option 07.02.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.04
                    mdlSGM.sSelectedOption = "P0AAFAASS"     ''Option 07.02.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.05
                    mdlSGM.sSelectedOption = "P0AAFASAS"     ''Option 07.02.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.06
                    mdlSGM.sSelectedOption = "P0AAFASAS"     ''Option 07.02.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.07
                    mdlSGM.sSelectedOption = "P0AAFASSS"     ''Option 07.02.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.08
                    mdlSGM.sSelectedOption = "P0AAFASSS"     ''Option 07.02.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.09
                    mdlSGM.sSelectedOption = "P0AAFSAAS"     ''Option 07.02.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.10
                    mdlSGM.sSelectedOption = "P0AAFSAAS"     ''Option 07.02.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.11
                    mdlSGM.sSelectedOption = "P0AAFSASS"     ''Option 07.02.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.12
                    mdlSGM.sSelectedOption = "P0AAFSASS"     ''Option 07.02.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.13
                    mdlSGM.sSelectedOption = "P0AAFSSAS"     ''Option 07.02.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.14
                    mdlSGM.sSelectedOption = "P0AAFSSAS"     ''Option 07.02.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.15
                    mdlSGM.sSelectedOption = "P0AAFSSSS"     ''Option 07.02.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.16
                    mdlSGM.sSelectedOption = "P0AAFSSSS"     ''Option 07.02.16



                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus = "Pending" Then '' Option 07.03
            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 07.04
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.01
                    mdlSGM.sSelectedOption = "P0ASAAAAS"     ''Option 07.04.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.02
                    mdlSGM.sSelectedOption = "P0ASAAAAS"     ''Option 07.04.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.03
                    mdlSGM.sSelectedOption = "P0ASAAASS"     ''Option 07.04.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.04
                    mdlSGM.sSelectedOption = "P0ASAAASS"     ''Option 07.04.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.05
                    mdlSGM.sSelectedOption = "P0ASAASAS"     ''Option 07.04.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.06
                    mdlSGM.sSelectedOption = "P0ASAASAS"     ''Option 07.04.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.07
                    mdlSGM.sSelectedOption = "P0ASAASSS"     ''Option 07.04.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.08
                    mdlSGM.sSelectedOption = "P0ASAASSS"     ''Option 07.04.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.09
                    mdlSGM.sSelectedOption = "P0ASASAAS"     ''Option 07.04.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.10
                    mdlSGM.sSelectedOption = "P0ASASAAS"     ''Option 07.04.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.11
                    mdlSGM.sSelectedOption = "P0ASASASS"     ''Option 07.04.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.12
                    mdlSGM.sSelectedOption = "P0ASASASS"     ''Option 07.04.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.13
                    mdlSGM.sSelectedOption = "P0ASASSAS"     ''Option 07.04.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "DetaileS" Then  ' Option 07.04.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.14
                    mdlSGM.sSelectedOption = "P0ASASSAS"     ''Option 07.04.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.15
                    mdlSGM.sSelectedOption = "P0ASASSSS"     ''Option 07.04.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.16
                    mdlSGM.sSelectedOption = "P0ASASSSS"     ''Option 07.04.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 07.05 / 07.06
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.01
                    mdlSGM.sSelectedOption = "P0ASFAAAS"     ''Option 07.05.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.02
                    mdlSGM.sSelectedOption = "P0ASFAAAS"     ''Option 07.05.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.03
                    mdlSGM.sSelectedOption = "P0ASFAASS"     ''Option 07.05.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.04
                    mdlSGM.sSelectedOption = "P0ASFAASS"       ''Option 07.05.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.05
                    mdlSGM.sSelectedOption = "P0ASFASAS"     ''Option 07.05.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.06
                    mdlSGM.sSelectedOption = "P0ASFASAS"       ''Option 07.05.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.07
                    mdlSGM.sSelectedOption = "P0ASFASSS"     ''Option 07.05.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.08
                    mdlSGM.sSelectedOption = "P0ASFASSS"       ''Option 07.05.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.09
                    mdlSGM.sSelectedOption = "P0ASFSAAS"     ''Option 07.05.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.10
                    mdlSGM.sSelectedOption = "P0ASFSAAS"       ''Option 07.05.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.11
                    mdlSGM.sSelectedOption = "P0ASFSASS"     ''Option 07.05.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.12
                    mdlSGM.sSelectedOption = "P0ASFSASS"       ''Option 07.05.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.13
                    mdlSGM.sSelectedOption = "P0ASFSSAS"     ''Option 07.05.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.14
                    mdlSGM.sSelectedOption = "P0ASFSSAS"       ''Option 07.05.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.15
                    mdlSGM.sSelectedOption = "P0ASFSSSS"     ''Option 07.05.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.16
                    mdlSGM.sSelectedOption = "P0ASFSSSS"       ''Option 07.05.16



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
                    mdlSGM.sSelectedOption = "P0SAAAAAS"     ''Option 08.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.03
                    mdlSGM.sSelectedOption = "P0SAAAASS"     ''Option 08.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.04
                    mdlSGM.sSelectedOption = "P0SAAAASS"     ''Option 08.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.05
                    mdlSGM.sSelectedOption = "P0SAAASAS"     ''Option 08.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.06
                    mdlSGM.sSelectedOption = "P0SAAASAS"     ''Option 08.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.07
                    mdlSGM.sSelectedOption = "P0SAAASSS"     ''Option 08.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.08
                    mdlSGM.sSelectedOption = "P0SAAASSS"     ''Option 08.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.09
                    mdlSGM.sSelectedOption = "P0SAASAAS"     ''Option 08.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.10
                    mdlSGM.sSelectedOption = "P0SAASAAS"     ''Option 08.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.11
                    mdlSGM.sSelectedOption = "P0SAASASS"     ''Option 08.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.12
                    mdlSGM.sSelectedOption = "P0SAASASS"     ''Option 08.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.13
                    mdlSGM.sSelectedOption = "P0SAASSAS"     ''Option 08.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.14
                    mdlSGM.sSelectedOption = "P0SAASSAS"     ''Option 08.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.15
                    mdlSGM.sSelectedOption = "P0SAASSSS"     ''Option 08.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.16
                    mdlSGM.sSelectedOption = "P0SAASSSS"     ''Option 08.01.16



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
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All :
                    '' 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.04.01
                    mdlSGM.sSelectedOption = "P0SSAAAAS"     ''Option 08.04.01
                    ''mdlSGM.sSelectedOption = "P0SAAAAAS"     ''Option 08.01.01
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
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Ord   er Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.05.02
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

Aa:


        sIsLoaded = "N"

        If rbSales.Checked = True And sTypeofDocument = "Order" Then
            LoadSalesOrders()
        ElseIf rbSales.Checked = True And sTypeofDocument = "Invoice" Then
            LoadSalesInvoices()
        ElseIf rbSales.Checked = True And sTypeofDocument = "Credit Note Issued / Debit Not Received - For Sales" Then
            LoadDebitCreditNoteSales()
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Order" Then
            LoadPurchaseOrders()
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Invoice" Then
            LoadPurchaseInvoices()
        ElseIf rbPurchase.Checked = True And sTypeofDocument = "Credit Note Received / Debit Note Issued - For Purchase" Then
            LoadDebitCreditNotePurchase()
        End If

        sIsLoaded = "Y"
        MsgBox("Loading Completed", MsgBoxStyle.Information)
        pgbar.Visible = False

    End Sub
    Dim sIsSampleOrder As String
    Public sBrand As String
    Private Sub LoadSalesOrders()
        Dim i As Integer = 0

        grdSalesOrders.BringToFront()

Ab:
        'ngrdRowCount = grdSalesOrdersV1.RowCount
        'For i = 0 To ngrdRowCount
        '    grdSalesOrdersV1.DeleteRow(i)
        'Next
        'ngrdRowCount = grdSalesOrdersV1.RowCount
        'If ngrdRowCount > 0 Then
        '    GoTo Ab
        'End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            'sOrderStatus = "CLOSE"
            sOrderStatus = "Shipped"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "OPEN"
        ElseIf sOrderStatus = "Close" Then
            sOrderStatus = "Close"
        Else
            sOrderStatus = "All"
        End If

        If sTypeofOrder = "Sales" Then
            sTypeofOrder = "SO"
        ElseIf sTypeofOrder = "Job" Then
            sTypeofOrder = "JO"
        ElseIf sTypeofOrder = "Internal" Then
            sTypeofOrder = "IO"
        Else
            sTypeofOrder = "All"
        End If



        If rbOrderAll.Checked = True Then
            sIsSampleOrder = "All"
        ElseIf rbOrderProduction.Checked = True Then
            sIsSampleOrder = "Production"
        Else
            sIsSampleOrder = "Sample"
        End If
        sSampleOrderType = Trim(cbxSampleOrderType.SelectedValue)

        If Trim(cbxBrand.Text) = "" Or Trim(cbxBrand.Text) = "ALL BRANDS" Then
            sBrand = "ALL BRANDS"
        Else
            sBrand = Trim(cbxBrand.Text)
        End If

        grdSalesOrders.DataSource = myccAllinOne.LoadSalesOrders(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sIsSampleOrder)

        With grdSalesOrdersV1

            .Columns(6).VisibleIndex = -1
            .Columns(16).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1
            .Columns(7).Caption = "Color"


            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            .Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With

        If rbDetailed.Checked = True Then
            grdSalesOrderDetails.Visible = True
            grdSalesOrderDetails.BringToFront()
            pgbar.Visible = True

            UpdateInformationforSalesOrderWithInvoiceDetatils()
            DstmpSalesorder.EnforceConstraints = False
            Me.Tmptbl_SalesOrderHeaderforSGMTableAdapter.Fill(Me.DstmpSalesorder.tmptbl_SalesOrderHeaderforSGM)
            Me.Tmptbl_SalesOrderDetailforSGMTableAdapter.Fill(Me.DstmpSalesorder.tmptbl_SalesOrderDetailforSGM)


            Dim View As GridView = grdSalesOrderDetailsV1
            Dim J As Integer = 0
            If chkbxDisplayDtl.CheckState = CheckState.Checked Then
                For J = 0 To grdSalesOrderDetailsV1.RowCount - 1
                    View.ExpandMasterRow(J)
                Next
            Else

                For J = 0 To grdSalesOrderDetailsV1.RowCount - 1
                    View.CollapseMasterRow(J)
                Next
            End If

        Else
            ''TODO : To replace
            ''UpdateInformationforSalesOrder()
            grdSalesOrders.BringToFront()
        End If
    End Sub

    Private Sub grdSalesOrdersV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdSalesOrdersV1.RowStyle
        If sIsLoaded = "Y" Then
            If e.RowHandle > -1 Then
                If grdSalesOrdersV1.GetRowCellValue(e.RowHandle, grdSalesOrdersV1.Columns(17)).ToString().ToUpper = "SHIPPED" Then
                    e.Appearance.ForeColor = Color.Navy
                ElseIf grdSalesOrdersV1.GetRowCellValue(e.RowHandle, grdSalesOrdersV1.Columns(17)).ToString().ToUpper = "OPEN" Then
                    e.Appearance.ForeColor = Color.Black
                ElseIf grdSalesOrdersV1.GetRowCellValue(e.RowHandle, grdSalesOrdersV1.Columns(17)).ToString().ToUpper = "CLOSE" Then
                    e.Appearance.ForeColor = Color.Red
                End If
            End If
        End If
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
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") 'Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")


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
        ElseIf sTypeofOrder = "Internal" Then
            sTypeofOrder = "IO"
        Else
            sTypeofOrder = "All"
        End If

        If rbOrderAll.Checked = True Then
            sIsSampleOrder = "All"
        ElseIf rbOrderProduction.Checked = True Then
            sIsSampleOrder = "Production"
            sSampleOrderType = Trim(cbxSampleOrderType.SelectedValue)
        Else
            sIsSampleOrder = "Sample"
            sSampleOrderType = Trim(cbxSampleOrderType.SelectedValue)
        End If

        grdSalesInvoices.DataSource = myccAllinOne.LoadSalesInvoices(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sIsSampleOrder)

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
        sIsLoaded = "N"
        grdPurchaseOrders.BringToFront()
        grdPurchaseOrderV1.OptionsBehavior.ReadOnly = True
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
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") 'Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            sOrderStatus = "COMPLETE"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "PENDING"
        ElseIf sOrderStatus = "Close" Then
            sOrderStatus = "Closed"
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
            .Columns(13).VisibleIndex = -1
            .Columns(7).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1

            .Columns(8).Caption = "Color"

            .Columns(3).VisibleIndex = 14

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(16).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With

        If rbDetailed.Checked = True Then

            grdPurchaseOrder.Visible = True
            grdPurchaseOrder.BringToFront()
            pgbar.Visible = True

            UpdateInformationforPurchaseOrderWithArrivalDetatils()
            DstmpPurchaseOrder.EnforceConstraints = False


            DstmpPurchaseOrder.Clear()
            DstmpPurchaseOrder.EnforceConstraints = False

            Me.Tmptbl_PurchaseOrderforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderforSGM)
            Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM)


            'Me.Tmptbl_PurchaseOrderforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderforSGM)
            'Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM)



            Dim View As GridView = grdPurchaseOrderV1
            Dim J As Integer = 0
            If chkbxDisplayDtl.CheckState = CheckState.Checked Then
                For J = 0 To grdPurchaseOrderV1.RowCount - 1
                    View.ExpandMasterRow(J)
                Next
            Else

                For J = 0 To grdPurchaseOrderV1.RowCount - 1
                    View.CollapseMasterRow(J)
                Next
            End If

        Else
            ''UpdateInformationforPurchaseOrder()
            grdPurchaseOrders.BringToFront()
        End If
        sIsLoaded = "Y"
    End Sub

    Private Sub grdPurchaseOrdersV1_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdPurchaseOrdersV1.RowStyle
        If sIsLoaded = "Y" Then
            If e.RowHandle > -1 Then
                If grdPurchaseOrdersV1.GetRowCellValue(e.RowHandle, grdPurchaseOrdersV1.Columns(17)).ToString().ToUpper = "COMPLETE" Then
                    e.Appearance.ForeColor = Color.DarkBlue
                ElseIf grdPurchaseOrdersV1.GetRowCellValue(e.RowHandle, grdPurchaseOrdersV1.Columns(17)).ToString().ToUpper = "PENDING" Then
                    e.Appearance.ForeColor = Color.Black
                ElseIf grdPurchaseOrdersV1.GetRowCellValue(e.RowHandle, grdPurchaseOrdersV1.Columns(17)).ToString().ToUpper = "CANCEL" Or grdPurchaseOrdersV1.GetRowCellValue(e.RowHandle, grdPurchaseOrdersV1.Columns(17)).ToString().ToUpper = "CLOSED" Then
                    e.Appearance.ForeColor = Color.Red
                End If
            End If
        End If
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
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") 'Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

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

        If rbPurchase.Checked = True Then
            chkbxNoMRP.Enabled = True
            plOrderType.Enabled = False

        Else
            chkbxNoMRP.Enabled = False
            chkbxNoMRP.Checked = False

            plOrderType.Enabled = True

        End If
    End Sub

    Dim sID, sBuyerName, sPurchaseOrderNo, sSalesOrderNo, sCustomerOrderNo, sArticle, sDescription, sMaterialColorDescription, sCodificationNew, sArticleMould, sOrderType As String
    Dim dOrderRecivedDate As Date
    Dim sOrderRecivedDate As Date
    Dim nOrderQuantity, nDispQty, nBal2disp As Integer
    Dim nPrice, nOrderValue As Decimal


    Private Sub UpdateInformationforSalesOrderWithInvoiceDetatils()
        If grdSalesOrdersV1.RowCount >= 0 Then

            Dim daDelTmpOrdHdr As New SqlDataAdapter("Delete from tmptbl_SalesOrderHeaderforSGM", sConstr)
            Dim dsDelTmpOrdHdr As New DataSet
            daDelTmpOrdHdr.Fill(dsDelTmpOrdHdr)
            dsDelTmpOrdHdr.AcceptChanges()

            Dim daDelTmpOrdDtl As New SqlDataAdapter("Delete from tmptbl_SalesOrderDetailforSGM", sConstr)
            Dim dsDelTmpOrdDtl As New DataSet
            daDelTmpOrdDtl.Fill(dsDelTmpOrdDtl)
            dsDelTmpOrdDtl.AcceptChanges()


            ngrdRowCount = grdSalesOrdersV1.RowCount
            pgbar.Minimum = 0
            If ngrdRowCount = 0 Then
                pgbar.Maximum = 0
            Else
                pgbar.Maximum = ngrdRowCount - 1
            End If


            Dim x As Single
            Dim y As Single

            Dim gr As Graphics = Me.pgbar.CreateGraphics
            Dim percentage As String
            Dim sz As SizeF = gr.MeasureString(percentage, Me.pgbar.Font, Me.pgbar.Width)

            x = (Me.pgbar.Width / 2) - (sz.Width / 2)
            y = (Me.pgbar.Height / 2) - (sz.Height / 2)

            gr.DrawString(percentage, pgbar.Font, Brushes.Black, x, y)

            Dim i As Integer = 0

            For i = 0 To ngrdRowCount - 1

                pgbar.Value = i
                If i > 0 Then
                    percentage = CType((pgbar.Value / pgbar.Maximum * 100), Integer).ToString & "%"
                End If
                gr.DrawString(percentage, pgbar.Font, Brushes.Black, x, y)
                sID = grdSalesOrdersV1.GetDataRow(i).Item("ID").ToString()
                dOrderRecivedDate = Format(grdSalesOrdersV1.GetDataRow(i).Item("OrderRecivedDate"), "dd-MMM-yyyy") ''.ToString
                sOrderRecivedDate = Format(grdSalesOrdersV1.GetDataRow(i).Item("OrderRecivedDate"), "dd-MMM-yyyy") ''.ToString
                sBuyerName = grdSalesOrdersV1.GetDataRow(i).Item("BuyerName").ToString()
                sSalesOrderNo = grdSalesOrdersV1.GetDataRow(i).Item("SalesOrderNo").ToString()
                sCustomerOrderNo = grdSalesOrdersV1.GetDataRow(i).Item("CustomerOrderNo").ToString()
                sArticle = grdSalesOrdersV1.GetDataRow(i).Item("Article").ToString()
                sArticleName = grdSalesOrdersV1.GetDataRow(i).Item("ArticleName").ToString()
                sDescription = grdSalesOrdersV1.GetDataRow(i).Item("Description").ToString()
                sMaterialColorDescription = grdSalesOrdersV1.GetDataRow(i).Item("MaterialColorDescription").ToString()
                sCodificationNew = grdSalesOrdersV1.GetDataRow(i).Item("CodificationNew").ToString()
                sArticleMould = grdSalesOrdersV1.GetDataRow(i).Item("ArticleMould").ToString()
                sOrderType = grdSalesOrdersV1.GetDataRow(i).Item("OrderType").ToString()
                nOrderQuantity = Val(grdSalesOrdersV1.GetDataRow(i).Item("OrderQuantity").ToString())
                nPrice = Val(grdSalesOrdersV1.GetDataRow(i).Item("Price").ToString())
                nOrderValue = Val(grdSalesOrdersV1.GetDataRow(i).Item("OrderValue").ToString())
                nDispQty = Val(grdSalesOrdersV1.GetDataRow(i).Item("DispQty").ToString())
                nBal2disp = Val(grdSalesOrdersV1.GetDataRow(i).Item("Bal2disp").ToString())

                Dim daInsTempOrd As New SqlDataAdapter("Insert Into tmptbl_SalesOrderHeaderforSGM Values ('" & sID & _
                                                       "','" & Format(dOrderRecivedDate.Date, "dd-MMM-yy") & "','" & sBuyerName & "','" & sSalesOrderNo & _
                                                       "','" & sCustomerOrderNo & "','" & sArticle & "','" & sArticleName & _
                                                       "','" & sDescription & "','" & sMaterialColorDescription & "','" & sCodificationNew & _
                                                       "','" & sArticleMould & "','" & sOrderType & "','" & nOrderQuantity & _
                                                       "','" & nPrice & "','" & nOrderValue & "','" & nDispQty & _
                                                       "','" & nBal2disp & "')", sConstr)

                'Dim daInsTempOrd As New SqlDataAdapter("Insert Into tmptbl_SalesOrderHeaderforSGM Values ('" & sID & _
                '                                       "','" & sOrderRecivedDate & "','" & sBuyerName & "','" & sSalesOrderNo & _
                '                                       "','" & sCustomerOrderNo & "','" & sArticle & "','" & sArticleName & _
                '                                       "','" & sDescription & "','" & sMaterialColorDescription & "','" & sCodificationNew & _
                '                                       "','" & sArticleMould & "','" & sOrderType & "','" & nOrderQuantity & _
                '                                       "','" & nPrice & "','" & nOrderValue & "','" & nDispQty & _
                '                                       "','" & nBal2disp & "')", sConstr)
                Dim dsInsTempOrd As New DataSet
                daInsTempOrd.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()



                'Dim daInsTempOrdDtl As New SqlDataAdapter("Insert  tmptbl_SalesOrderDetailforSGM Select jd.SalesOrderDetailId,ID.InvoiceDate,ID.InvoiceNo,ID.SalesOrderNo,'','','','','','','','','0',ID.Rate,'0',ID.quantity from InvoiceDetail As ID, JobcardDetail As JD, Invoice As INV where ID.JobCardNo = JD.JobcardNo  And ID.InvoiceNo = Inv.InvoiceNo And Inv.IsShipped = '1' And JD.SalesOrderDetailId = '" & sID & "' Order By InvoiceNo", sConstr)
                Dim daInsTempOrdDtl As New SqlDataAdapter("Insert  tmptbl_SalesOrderDetailforSGM Select jd.SalesOrderDetailId,ID.InvoiceDate,ID.InvoiceNo,ID.SalesOrderNo,'','','','','','','','',ID.Rate,'0',Sum(ID.quantity),'0' As Quantity from InvoiceDetail As ID, JobcardDetail As JD, Invoice As INV where ID.JobCardNo = JD.JobcardNo  And ID.InvoiceNo = Inv.InvoiceNo And JD.SalesOrderDetailId = '" & sID & _
                                                          "' GROUP BY JD.SalesOrderDetailID, ID.InvoiceDate, ID.invoiceno, ID.SalesOrderNo, ID.rate, INV.InvoiceNo Order By InvoiceNo", sConstr)
                Dim dsInsTempOrdDtl As New DataSet
                daInsTempOrdDtl.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()




            Next
        End If
    End Sub

    Private Sub UpdateInformationforSalesOrder()
        If grdSalesOrdersV1.RowCount >= 0 Then

            Dim daDelTmpOrdHdr As New SqlDataAdapter("Delete from tmptbl_SalesOrderHeaderforSGM", sConstr)
            Dim dsDelTmpOrdHdr As New DataSet
            daDelTmpOrdHdr.Fill(dsDelTmpOrdHdr)
            dsDelTmpOrdHdr.AcceptChanges()

            Dim daDelTmpOrdDtl As New SqlDataAdapter("Delete from tmptbl_SalesOrderDetailforSGM", sConstr)
            Dim dsDelTmpOrdDtl As New DataSet
            daDelTmpOrdDtl.Fill(dsDelTmpOrdDtl)
            dsDelTmpOrdDtl.AcceptChanges()


            ngrdRowCount = grdSalesOrdersV1.RowCount

            Dim i As Integer = 0

            For i = 0 To ngrdRowCount - 1


                sID = grdSalesOrdersV1.GetDataRow(i).Item("ID").ToString()
                dOrderRecivedDate = grdSalesOrdersV1.GetDataRow(i).Item("OrderRecivedDate").ToString
                sBuyerName = grdSalesOrdersV1.GetDataRow(i).Item("BuyerName").ToString()
                sSalesOrderNo = grdSalesOrdersV1.GetDataRow(i).Item("SalesOrderNo").ToString()
                sCustomerOrderNo = grdSalesOrdersV1.GetDataRow(i).Item("CustomerOrderNo").ToString()
                sArticle = grdSalesOrdersV1.GetDataRow(i).Item("Article").ToString()
                sArticleName = grdSalesOrdersV1.GetDataRow(i).Item("ArticleName").ToString()
                sDescription = grdSalesOrdersV1.GetDataRow(i).Item("Description").ToString()
                sMaterialColorDescription = grdSalesOrdersV1.GetDataRow(i).Item("MaterialColorDescription").ToString()
                sCodificationNew = grdSalesOrdersV1.GetDataRow(i).Item("CodificationNew").ToString()
                sArticleMould = grdSalesOrdersV1.GetDataRow(i).Item("ArticleMould").ToString()
                sOrderType = grdSalesOrdersV1.GetDataRow(i).Item("OrderType").ToString()
                nOrderQuantity = Val(grdSalesOrdersV1.GetDataRow(i).Item("OrderQuantity").ToString())
                nPrice = Val(grdSalesOrdersV1.GetDataRow(i).Item("Price").ToString())
                nOrderValue = Val(grdSalesOrdersV1.GetDataRow(i).Item("OrderValue").ToString())
                nDispQty = Val(grdSalesOrdersV1.GetDataRow(i).Item("DispQty").ToString())
                nBal2disp = Val(grdSalesOrdersV1.GetDataRow(i).Item("Bal2disp").ToString())

                Dim daInsTempOrd As New SqlDataAdapter("Insert Into tmptbl_SalesOrderHeaderforSGM Values ('" & sID & _
                                                       "','" & Format(dOrderRecivedDate.Date, "dd-MMM-yy") & "','" & sBuyerName & "','" & sSalesOrderNo & _
                                                       "','" & sCustomerOrderNo & "','" & sArticle & "','" & sArticleName & _
                                                       "','" & sDescription & "','" & sMaterialColorDescription & "','" & sCodificationNew & _
                                                       "','" & sArticleMould & "','" & sOrderType & "','" & nOrderQuantity & _
                                                       "','" & nPrice & "','" & nOrderValue & "','" & nDispQty & _
                                                       "','" & nBal2disp & "')", sConstr)
                Dim dsInsTempOrd As New DataSet
                daInsTempOrd.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()



                'Dim daInsTempOrdDtl As New SqlDataAdapter("Insert  tmptbl_SalesOrderDetailforSGM Select jd.SalesOrderDetailId,ID.InvoiceDate,ID.InvoiceNo,ID.SalesOrderNo,'','','','','','','','','0',ID.Rate,'0',ID.quantity from InvoiceDetail As ID, JobcardDetail As JD, Invoice As INV where ID.JobCardNo = JD.JobcardNo  And ID.InvoiceNo = Inv.InvoiceNo And Inv.IsShipped = '1' And JD.SalesOrderDetailId = '" & sID & "' Order By InvoiceNo", sConstr)
                Dim daInsTempOrdDtl As New SqlDataAdapter("Insert  tmptbl_SalesOrderDetailforSGM Select jd.SalesOrderDetailId,ID.InvoiceDate,ID.InvoiceNo,ID.SalesOrderNo,'','','','','','','','',ID.Rate,'0',Sum(ID.quantity),'0' As Quantity from InvoiceDetail As ID, JobcardDetail As JD, Invoice As INV where ID.JobCardNo = JD.JobcardNo  And ID.InvoiceNo = Inv.InvoiceNo And JD.SalesOrderDetailId = '" & sID & _
                                                          "' GROUP BY JD.SalesOrderDetailID, ID.InvoiceDate, ID.invoiceno, ID.SalesOrderNo, ID.rate, INV.InvoiceNo Order By InvoiceNo", sConstr)
                Dim dsInsTempOrdDtl As New DataSet
                daInsTempOrdDtl.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()



            Next
        End If
    End Sub

    'Dim sID As String
    'Dim sPurchaseOrderNo As String
    Dim daPurchaseOrderDate As Date
    Dim sPartyName As String
    Dim sCurrencyCode As String
    Dim sPurchaseOrderType As String
    Dim sMaterialCode As String
    Dim sMaterialDescription As String
    Dim sMaterialSize As String
    'Dim sMaterialColorDescription As String
    Dim sUnit As String
    Dim dQuantity As Decimal
    Dim dPrice As Decimal
    Dim dMaterialValue As Decimal
    'Dim sMaterialTypeDescription As String
    Dim dReceivedQuantity As Decimal
    Dim dBalanceQuantity As Decimal
    Dim sModuleName As String
    Dim sCreatedBy As String
    Dim dCreatedDate As DateTime
    Dim sModifiedBy As String
    Dim sExeVersionNo As String
    Dim dModifiedDate As DateTime
    Dim sEnteredOnMachineID As String



    Private Sub UpdateInformationforPurchaseOrderWithArrivalDetatils()
        If grdPurchaseOrdersV1.RowCount >= 0 Then

            Dim daDelTmpOrdHdr As New SqlDataAdapter("Delete from tmptbl_PurchaseOrderforSGM", sConstr)
            Dim dsDelTmpOrdHdr As New DataSet
            daDelTmpOrdHdr.Fill(dsDelTmpOrdHdr)
            dsDelTmpOrdHdr.AcceptChanges()

            Dim daDelTmpOrdDtl As New SqlDataAdapter("Delete from tmptbl_PurchaseOrderDetailsforSGM", sConstr)
            Dim dsDelTmpOrdDtl As New DataSet
            daDelTmpOrdDtl.Fill(dsDelTmpOrdDtl)
            dsDelTmpOrdDtl.AcceptChanges()


            ngrdRowCount = grdPurchaseOrdersV1.RowCount

            Dim i As Integer = 0

            For i = 0 To ngrdRowCount - 1

                'If grdPurchaseOrdersV1.GetDataRow(i).Item("ReceivedQuantity").ToString() > 0 Then
                sID = System.Guid.NewGuid.ToString()

                sPurchaseOrderNo = grdPurchaseOrdersV1.GetDataRow(i).Item("PurchaseOrderNo").ToString
                daPurchaseOrderDate = grdPurchaseOrdersV1.GetDataRow(i).Item("PurchaseOrderDate").ToString
                sPartyName = grdPurchaseOrdersV1.GetDataRow(i).Item("PartyName").ToString
                sCurrencyCode = grdPurchaseOrdersV1.GetDataRow(i).Item("CurrencyCode").ToString
                sPurchaseOrderType = grdPurchaseOrdersV1.GetDataRow(i).Item("PurchaseOrderType").ToString
                sMaterialCode = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialCode").ToString
                sMaterialDescription = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialDescription").ToString
                sMaterialSize = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialSize").ToString
                sMaterialColorDescription = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialColorDescription").ToString
                sUnit = grdPurchaseOrdersV1.GetDataRow(i).Item("Unit").ToString
                dQuantity = grdPurchaseOrdersV1.GetDataRow(i).Item("Quantity").ToString
                dPrice = grdPurchaseOrdersV1.GetDataRow(i).Item("Price").ToString
                dMaterialValue = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialValue").ToString
                sMaterialTypeDescription = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialTypeDescription").ToString
                dReceivedQuantity = grdPurchaseOrdersV1.GetDataRow(i).Item("ReceivedQuantity").ToString
                dBalanceQuantity = grdPurchaseOrdersV1.GetDataRow(i).Item("BalanceQuantity").ToString
                sModuleName = ""
                sCreatedBy = mdlSGM.sUserName
                dCreatedDate = Date.Now
                sModifiedBy = ""
                sExeVersionNo = ""
                dModifiedDate = Date.Now
                sEnteredOnMachineID = ""


                Dim daInsTempOrd As New SqlDataAdapter("Insert Into tmptbl_PurchaseOrderforSGM Values ('" & sID & "','" & sPurchaseOrderNo & _
                                                       "','" & Format(daPurchaseOrderDate.Date, "dd-MMM-yy") & "','" & sPartyName & "','" & sCurrencyCode & _
                                                       "','" & sPurchaseOrderType & "','" & sMaterialCode & "','" & sMaterialDescription & _
                                                       "','" & sMaterialSize & "','" & sMaterialColorDescription & "','" & sUnit & "','" & dQuantity & _
                                                       "','" & dPrice & "','" & dMaterialValue & "','" & sMaterialTypeDescription & "','" & dReceivedQuantity & _
                                                       "','" & dBalanceQuantity & "','" & sModuleName & "','" & sCreatedBy & "','" & Format(dCreatedDate.Date, "dd-MMM-yyyy") & _
                                                       "','" & sModifiedBy & "','" & sExeVersionNo & "','" & Format(dModifiedDate.Date, "dd-MMM-yyyy") & "','" & sEnteredOnMachineID & _
                                                       "')", sConstr)
                Dim dsInsTempOrd As New DataSet
                daInsTempOrd.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()


                'Dim daInsempOrdDtl As New SqlDataAdapter("Insert  tmptbl_PurchaseOrderDetailforSGM Select ID,InvoiceDate,InvoiceNo,PurchaseOrderNo,'','','','','','','','','0',Rate,'0',quantity from InvoiceDetail where JobcardNo in (Select JobcardNo FROM JobcardDetail where PurchaseOrderDetailId = '" & sID & "') Order By InvoiceNo", sConstr)
                Dim daInsTempOrdDtl As New SqlDataAdapter("Insert tmptbl_PurchaseOrderDetailsforSGM Select PurchaseOrderNo As Id,SupplierBillNo,IssueDate,'' As PartyName,'' AS CurrencyCode,'' as PurchaseOrderType,MaterialCode,'' AS MaterialDescription, '' AS MaterialSize,'' as MaterialColorDescription,'' AS Unit,IssuePrice,'0' As Quantity,Issuequantity,'' AS MaterialTypeDescription,'0' AS ReceivedQuantity from MaterialIssues where PurchaseOrderNo = '" & sPurchaseOrderNo & _
                                                          "' And Materialcode = '" & sMaterialCode & "' And TransactionType = 'NEW PURCHASE'", sConstr)
                Dim dsInsTempOrdDtl As New DataSet
                daInsTempOrdDtl.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()


                'End If

            Next
        End If
    End Sub

    Private Sub UpdateInformationforPurchaseOrder()
        If grdPurchaseOrdersV1.RowCount >= 0 Then

            Dim daDelTmpOrdHdr As New SqlDataAdapter("Delete from tmptbl_PurchaseOrderforSGM", sConstr)
            Dim dsDelTmpOrdHdr As New DataSet
            daDelTmpOrdHdr.Fill(dsDelTmpOrdHdr)
            dsDelTmpOrdHdr.AcceptChanges()

            Dim daDelTmpOrdDtl As New SqlDataAdapter("Delete from tmptbl_PurchaseOrderDetailsforSGM", sConstr)
            Dim dsDelTmpOrdDtl As New DataSet
            daDelTmpOrdDtl.Fill(dsDelTmpOrdDtl)
            dsDelTmpOrdDtl.AcceptChanges()


            ngrdRowCount = grdPurchaseOrdersV1.RowCount

            Dim i As Integer = 0

            For i = 0 To ngrdRowCount - 1

                sID = System.Guid.NewGuid.ToString()

                sPurchaseOrderNo = grdPurchaseOrdersV1.GetDataRow(i).Item("PurchaseOrderNo").ToString
                daPurchaseOrderDate = grdPurchaseOrdersV1.GetDataRow(i).Item("PurchaseOrderDate").ToString
                sPartyName = grdPurchaseOrdersV1.GetDataRow(i).Item("PartyName").ToString
                sCurrencyCode = grdPurchaseOrdersV1.GetDataRow(i).Item("CurrencyCode").ToString
                sPurchaseOrderType = grdPurchaseOrdersV1.GetDataRow(i).Item("PurchaseOrderType").ToString
                sMaterialCode = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialCode").ToString
                sMaterialDescription = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialDescription").ToString
                sMaterialSize = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialSize").ToString
                sMaterialColorDescription = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialColorDescription").ToString
                sUnit = grdPurchaseOrdersV1.GetDataRow(i).Item("Unit").ToString
                dQuantity = grdPurchaseOrdersV1.GetDataRow(i).Item("Quantity").ToString
                dPrice = grdPurchaseOrdersV1.GetDataRow(i).Item("Price").ToString
                dMaterialValue = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialValue").ToString
                sMaterialTypeDescription = grdPurchaseOrdersV1.GetDataRow(i).Item("MaterialTypeDescription").ToString
                dReceivedQuantity = grdPurchaseOrdersV1.GetDataRow(i).Item("ReceivedQuantity").ToString
                dBalanceQuantity = grdPurchaseOrdersV1.GetDataRow(i).Item("BalanceQuantity").ToString
                sModuleName = ""
                sCreatedBy = mdlSGM.sUserName
                dCreatedDate = Date.Now
                sModifiedBy = ""
                sExeVersionNo = ""
                dModifiedDate = Date.Now
                sEnteredOnMachineID = ""


                Dim daInsTempOrd As New SqlDataAdapter("Insert Into tmptbl_PurchaseOrderforSGM Values ('" & sID & "','" & sPurchaseOrderNo & _
                                                       "','" & Format(daPurchaseOrderDate.Date, "dd-MMM-yy") & "','" & sPartyName & "','" & sCurrencyCode & _
                                                       "','" & sPurchaseOrderType & "','" & sMaterialCode & "','" & sMaterialDescription & _
                                                       "','" & sMaterialSize & "','" & sMaterialColorDescription & "','" & sUnit & "','" & dQuantity & _
                                                       "','" & dPrice & "','" & dMaterialValue & "','" & sMaterialTypeDescription & "','" & dReceivedQuantity & _
                                                       "','" & dBalanceQuantity & "','" & sModuleName & "','" & sCreatedBy & "','" & Format(dCreatedDate.Date, "dd-MMM-yyyy") & _
                                                       "','" & sModifiedBy & "','" & sExeVersionNo & "','" & Format(dModifiedDate.Date, "dd-MMM-yyyy") & "','" & sEnteredOnMachineID & _
                                                       "')", sConstr)
                Dim dsInsTempOrd As New DataSet
                daInsTempOrd.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()


                'Dim daInsempOrdDtl As New SqlDataAdapter("Insert  tmptbl_PurchaseOrderDetailforSGM Select ID,InvoiceDate,InvoiceNo,PurchaseOrderNo,'','','','','','','','','0',Rate,'0',quantity from InvoiceDetail where JobcardNo in (Select JobcardNo FROM JobcardDetail where PurchaseOrderDetailId = '" & sID & "') Order By InvoiceNo", sConstr)
                Dim daInsTempOrdDtl As New SqlDataAdapter("Insert tmptbl_PurchaseOrderDetailsforSGM Select PurchaseOrderNo As Id,SupplierBillNo,IssueDate,'' As PartyName,'' AS CurrencyCode,'' as PurchaseOrderType,MaterialCode,'' AS MaterialDescription, '' AS MaterialSize,'' as MaterialColorDescription,'' AS Unit,IssuePrice,'0' As Quantity,Issuequantity,'' AS MaterialTypeDescription,'0' AS ReceivedQuantity from MaterialIssues where PurchaseOrderNo = '" & sPurchaseOrderNo & _
                                                          "' And Materialcode = '" & sMaterialCode & "' And TransactionType = 'NEW PURCHASE'", sConstr)
                Dim dsInsTempOrdDtl As New DataSet
                daInsTempOrdDtl.Fill(dsInsTempOrd)
                dsInsTempOrd.AcceptChanges()


            Next
        End If
    End Sub

    'Private Sub CustomPattern()
    '    Dim customPatternView As New GridView(grdSalesOrderDetails)
    '    customPatternView.Columns.AddField("ID").VisibleIndex = 0
    '    customPatternView.OptionsView.ShowGroupPanel = False


    'End Sub

    Private Sub rbDetailed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDetailed.CheckedChanged
        If rbDetailed.Checked = True Then
            chkbxDisplayDtl.Enabled = True
        Else
            chkbxDisplayDtl.Enabled = False
            chkbxDisplayDtl.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub cbxTypeofDocument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxTypeofDocument.SelectedIndexChanged
        If cbxTypeofDocument.Text = "Invoice" Then
            rbDetailed.Enabled = False
            rbSummary.Checked = True
        Else
            rbDetailed.Enabled = True
        End If

        If cbxTypeofDocument.Text = "Credit Note Issued / Debit Not Received - For Sales" Or cbxTypeofDocument.Text = "Credit Note Received / Debit Note Issued - For Purchase" Then
            cbxTypeofOrder.Enabled = False
            cbxTypeofOrder.SelectedIndex = 0
            plOrderType.Enabled = False
            rbOrderAll.Checked = True
        Else
            cbxTypeofOrder.Enabled = True
            plOrderType.Enabled = True
        End If
    End Sub

    Dim dSupplierBillDate As Date

    Private Sub UpdateSupplierBillDate()
        Dim daSelAHGroupData As New SqlDataAdapter("Select * from MaterialIssuesSolarDataFromAHServer where SupplierBillDate Is Not Null Order By CreatedDate", sConstr)
        Dim dsSelAHGroupData As New DataSet
        daSelAHGroupData.Fill(dsSelAHGroupData)

        Dim i As Integer

        For i = 0 To dsSelAHGroupData.Tables(0).Rows.Count - 1

            sID = dsSelAHGroupData.Tables(0).Rows(i).Item("ID")
            dSupplierBillDate = Format(dsSelAHGroupData.Tables(0).Rows(i).Item("SupplierBillDate"), "dd-MMMM-yyyy")

            Dim daUpdMI As New SqlDataAdapter("Update MaterialIssues Set SupplierBillDate = '" & Format(dSupplierBillDate.Date, "dd-MMM-yyyy") & _
                                              "' Where ID = '" & sID & "'", sConstr)
            Dim dsUpdMI As New DataSet
            daUpdMI.Fill(dsUpdMI)
            dsUpdMI.AcceptChanges()

            Dim daSelMI As New SqlDataAdapter("Select SupplierBillDate From MaterialIssues where ID = '" & sID & "'", sConstr)
            Dim dsSelMI As New DataSet
            daSelMI.Fill(dsSelMI)

            Dim sIssueDate As String

            sIssueDate = Format(dsSelMI.Tables(0).Rows(0).Item(0), "dd-MMM-yyyy")
            'MsgBox(sID + " - " + sIssueDate)
        Next
        MsgBox("Completed")
    End Sub
    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        'UpdateSupplierBillDate()
        'Exit Sub
        If rbSales.Checked = True Then
            sPurchaseorSales = "Sales"
        Else
            sPurchaseorSales = "Purchase"
        End If
        sTypeofDocument = cbxTypeofDocument.Text

        If rbSummary.Checked = True Then
            sSummaryorDetailed = "Summary"
        ElseIf rbDetailed.Checked = True Then
            sSummaryorDetailed = "Detailed"
        End If

        If sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sSummaryorDetailed = "Detailed" Then
            mdlSGM.sReportType = "Order Details"
            frmReport.Show()
        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Order" And sSummaryorDetailed = "Detailed" Then
            mdlSGM.sReportType = "Purchase Details"
            frmReport.Show()
        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Invoice" Then
            UpdateInvoiceDetailsForPrint()
        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Invoice" Then
            UpdatePurchaseInvoiceDetailsForPrint()
        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sSummaryorDetailed <> "Detailed" Then
            UpdateInformationforSalesOrder()
            mdlSGM.sReportType = "Order Header"
            frmReport.Show()
        ElseIf sPurchaseorSales = "Purchase" And sTypeofDocument = "Order" And sSummaryorDetailed <> "Detailed" Then
            UpdateInformationforPurchaseOrder()
            mdlSGM.sReportType = "Purchase Header"
            frmReport.Show()
        End If
    End Sub

    Private Sub UpdateInvoiceDetailsForPrint()
        Dim i As Integer = 0


        myccInvoicesWithDetails.DelInvoice4Print()

        For i = 0 To grdSalesInvoicesV1.RowCount - 1


            mystrSolarInvoice4SGM4Print.HdrFromDate = dpFrom.Value
            mystrSolarInvoice4SGM4Print.HdrToDate = dpTo.Value
            If rbPurchase.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrSalesorPurchase = "Purchase"
            ElseIf rbSales.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrSalesorPurchase = "Sales"
            End If
            mystrSolarInvoice4SGM4Print.HdrTypeofDocument = cbxTypeofDocument.Text
            mystrSolarInvoice4SGM4Print.HdrTypeofOrder = cbxTypeofOrder.Text
            mystrSolarInvoice4SGM4Print.HdrCustomerSupplier = cbxCustomer.Text
            mystrSolarInvoice4SGM4Print.HdrBrand = cbxBrand.Text
            mystrSolarInvoice4SGM4Print.HdrOrderStatus = cbxOrderStatus.Text
            mystrSolarInvoice4SGM4Print.HdrArticleName = cbxArticleName.Text
            mystrSolarInvoice4SGM4Print.HdrArticleCode = Trim(tbArticleCode.Text)
            mystrSolarInvoice4SGM4Print.HdrArtilceDescription = Trim(tbArticleDescription.Text)

            If rbOrderAll.Checked Then
                mystrSolarInvoice4SGM4Print.HdrOrderType = "All - " + cbxSampleOrderType.Text
            ElseIf rbOrderProduction.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrOrderType = "Production - " + cbxSampleOrderType.Text
            ElseIf rbOrderSample.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrOrderType = "Sample - " + cbxSampleOrderType.Text
            End If

            If rbSummary.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrSummaryorDetailed = "Summary"
            ElseIf rbDetailed.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrSummaryorDetailed = "Detailed"
            End If

            If chkbxDisplayDtl.Checked = True Then
                mystrSolarInvoice4SGM4Print.HdrDisplayInvoiceDetails = "Detail View"
            Else
                mystrSolarInvoice4SGM4Print.HdrDisplayInvoiceDetails = ""
            End If


            'mystrSolarInvoice4SGM4Print.PKID = grdSalesInvoicesV1.GetDataRow(i).Item("").ToString 'As Integer
            mystrSolarInvoice4SGM4Print.BuyerGroup = grdSalesInvoicesV1.GetDataRow(i).Item("BuyerGroup").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerCode = grdSalesInvoicesV1.GetDataRow(i).Item("BuyerCode").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerName = grdSalesInvoicesV1.GetDataRow(i).Item("BuyerName").ToString 'As String
            mystrSolarInvoice4SGM4Print.BuyerAddress = grdSalesInvoicesV1.GetDataRow(i).Item("BuyerAddress").ToString 'As String
            mystrSolarInvoice4SGM4Print.ConsigneeName = grdSalesInvoicesV1.GetDataRow(i).Item("ConsigneeName").ToString 'As String
            mystrSolarInvoice4SGM4Print.ConsigneeAdress = grdSalesInvoicesV1.GetDataRow(i).Item("ConsigneeAdress").ToString 'As String
            mystrSolarInvoice4SGM4Print.City = grdSalesInvoicesV1.GetDataRow(i).Item("City").ToString 'As String
            mystrSolarInvoice4SGM4Print.Pincode = grdSalesInvoicesV1.GetDataRow(i).Item("Pincode").ToString 'As String
            mystrSolarInvoice4SGM4Print.InvoiceNo = grdSalesInvoicesV1.GetDataRow(i).Item("InvoiceNo").ToString 'As String
            mystrSolarInvoice4SGM4Print.InvDate = grdSalesInvoicesV1.GetDataRow(i).Item("InvDate").ToString 'As Date
            mystrSolarInvoice4SGM4Print.InvType = grdSalesInvoicesV1.GetDataRow(i).Item("InvType").ToString 'As String
            mystrSolarInvoice4SGM4Print.CT3 = grdSalesInvoicesV1.GetDataRow(i).Item("CT3").ToString 'As String
            mystrSolarInvoice4SGM4Print.Accounted = grdSalesInvoicesV1.GetDataRow(i).Item("Accounted").ToString 'As String
            mystrSolarInvoice4SGM4Print.Code = grdSalesInvoicesV1.GetDataRow(i).Item("Code").ToString 'As String
            mystrSolarInvoice4SGM4Print.ArticleName = grdSalesInvoicesV1.GetDataRow(i).Item("Sole").ToString 'As String
            mystrSolarInvoice4SGM4Print.Colour = grdSalesInvoicesV1.GetDataRow(i).Item("Colour").ToString 'As String
            mystrSolarInvoice4SGM4Print.OldCodification = grdSalesInvoicesV1.GetDataRow(i).Item("OldCodification").ToString 'As String
            mystrSolarInvoice4SGM4Print.CodificationNew = grdSalesInvoicesV1.GetDataRow(i).Item("CodificationNew").ToString 'As String
            mystrSolarInvoice4SGM4Print.Quantity = grdSalesInvoicesV1.GetDataRow(i).Item("Quantity").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.Rate = grdSalesInvoicesV1.GetDataRow(i).Item("Rate").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.Value = grdSalesInvoicesV1.GetDataRow(i).Item("Value").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.ExcisePercentage = grdSalesInvoicesV1.GetDataRow(i).Item("ExcisePercentage").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.DWExciseDuty = grdSalesInvoicesV1.GetDataRow(i).Item("DWExciseDuty").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.CessPercentage = grdSalesInvoicesV1.GetDataRow(i).Item("CessPercentage").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DWCessAmount = grdSalesInvoicesV1.GetDataRow(i).Item("DWCessAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.EduCessPercentage = grdSalesInvoicesV1.GetDataRow(i).Item("EduCessPercentage").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DWEduCessAmount = grdSalesInvoicesV1.GetDataRow(i).Item("DWEduCessAmount").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.DutyPayable = grdSalesInvoicesV1.GetDataRow(i).Item("DutyPayable").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.SubTotal = grdSalesInvoicesV1.GetDataRow(i).Item("SubTotal").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.CSTorVat = grdSalesInvoicesV1.GetDataRow(i).Item("CSTorVat").ToString 'As Decimal
            '' ''mystrSolarInvoice4SGM4Print.CSTorVATAmount = grdSalesInvoicesV1.GetDataRow(i).Item("CSTorVATAmount").ToString 'As Decimal
            mystrSolarInvoice4SGM4Print.InvAmount = grdSalesInvoicesV1.GetDataRow(i).Item("InvAmount").ToString 'As Decimal

            'myccArticleMaster.InsOutstanding4Print(mystrstrSolarArticleMaster4SGM4Print)
            myccInvoicesWithDetails.InsInvoice4Print(mystrSolarInvoice4SGM4Print)

        Next
        mdlSGM.sReportType = "Invoice"
        frmReport.Show()
    End Sub

    Private Sub UpdatePurchaseInvoiceDetailsForPrint()
        Dim i As Integer = 0


        myccInvoicesWithDetails.DelPurchaseInvoice4Print()

        For i = 0 To grdPurchaseInvoicesV1.RowCount - 1

            mystrSolarPurchaseInvoice4SGM4Print.HdrFromDate = dpFrom.Value
            mystrSolarPurchaseInvoice4SGM4Print.HdrToDate = dpTo.Value
            If rbPurchase.Checked = True Then
                mystrSolarPurchaseInvoice4SGM4Print.HdrSalesorPurchase = "Purchase"
            ElseIf rbSales.Checked = True Then
                mystrSolarPurchaseInvoice4SGM4Print.HdrSalesorPurchase = "Sales"
            End If
            mystrSolarPurchaseInvoice4SGM4Print.HdrTypeofDocument = cbxTypeofDocument.Text
            mystrSolarPurchaseInvoice4SGM4Print.HdrTypeofOrder = cbxTypeofOrder.Text
            mystrSolarPurchaseInvoice4SGM4Print.HdrCustomerSupplier = cbxCustomer.Text
            mystrSolarPurchaseInvoice4SGM4Print.HdrBrand = cbxBrand.Text
            mystrSolarPurchaseInvoice4SGM4Print.HdrOrderStatus = cbxOrderStatus.Text
            mystrSolarPurchaseInvoice4SGM4Print.HdrArticleName = cbxArticleName.Text
            mystrSolarPurchaseInvoice4SGM4Print.HdrArticleCode = Trim(tbArticleCode.Text)
            mystrSolarPurchaseInvoice4SGM4Print.HdrArtilceDescription = Trim(tbArticleDescription.Text)

            'mystrSolarPurchaseInvoice4SGM4Print.PKID = grdPurchaseInvoicesV1.GetDataRow(i).Item("").ToString
            mystrSolarPurchaseInvoice4SGM4Print.ArrivalDate = grdPurchaseInvoicesV1.GetDataRow(i).Item("ArrivalDate").ToString
            mystrSolarPurchaseInvoice4SGM4Print.partyname = grdPurchaseInvoicesV1.GetDataRow(i).Item("partyname").ToString
            mystrSolarPurchaseInvoice4SGM4Print.VoucherNo = grdPurchaseInvoicesV1.GetDataRow(i).Item("VoucherNo").ToString
            mystrSolarPurchaseInvoice4SGM4Print.PurchaseOrderNo = grdPurchaseInvoicesV1.GetDataRow(i).Item("PurchaseOrderNo").ToString
            mystrSolarPurchaseInvoice4SGM4Print.PurchaseOrderDate = grdPurchaseInvoicesV1.GetDataRow(i).Item("PurchaseOrderDate").ToString
            mystrSolarPurchaseInvoice4SGM4Print.MaterialCode = grdPurchaseInvoicesV1.GetDataRow(i).Item("MaterialCode").ToString
            mystrSolarPurchaseInvoice4SGM4Print.Material = grdPurchaseInvoicesV1.GetDataRow(i).Item("Material").ToString
            mystrSolarPurchaseInvoice4SGM4Print.POSize = grdPurchaseInvoicesV1.GetDataRow(i).Item("POSize").ToString
            mystrSolarPurchaseInvoice4SGM4Print.MaterialColorDescription = grdPurchaseInvoicesV1.GetDataRow(i).Item("MaterialColorDescription").ToString
            mystrSolarPurchaseInvoice4SGM4Print.IssueUnits = grdPurchaseInvoicesV1.GetDataRow(i).Item("IssueUnits").ToString
            mystrSolarPurchaseInvoice4SGM4Print.IssueQuantity = grdPurchaseInvoicesV1.GetDataRow(i).Item("IssueQuantity").ToString
            mystrSolarPurchaseInvoice4SGM4Print.IssuePrice = grdPurchaseInvoicesV1.GetDataRow(i).Item("IssuePrice").ToString
            mystrSolarPurchaseInvoice4SGM4Print.IssueValue = grdPurchaseInvoicesV1.GetDataRow(i).Item("IssueValue").ToString
            mystrSolarPurchaseInvoice4SGM4Print.MaterialTypeDescription = grdPurchaseInvoicesV1.GetDataRow(i).Item("MaterialTypeDescription").ToString
            mystrSolarPurchaseInvoice4SGM4Print.MaterialSubTypeDescription = grdPurchaseInvoicesV1.GetDataRow(i).Item("MaterialSubTypeDescription").ToString
            mystrSolarPurchaseInvoice4SGM4Print.TransactionType = grdPurchaseInvoicesV1.GetDataRow(i).Item("TransactionType").ToString
            mystrSolarPurchaseInvoice4SGM4Print.SupplierBillNo = grdPurchaseInvoicesV1.GetDataRow(i).Item("SupplierBillNo").ToString
            mystrSolarPurchaseInvoice4SGM4Print.SupplierRefNo = "" 'grdPurchaseInvoicesV1.GetDataRow(i).Item("").ToString
            mystrSolarPurchaseInvoice4SGM4Print.SupplierBillDate = grdPurchaseInvoicesV1.GetDataRow(i).Item("SupplierBillDate").ToString


            myccInvoicesWithDetails.InsPurchaseInvoice4Print(mystrSolarPurchaseInvoice4SGM4Print)

        Next
        mdlSGM.sReportType = "Purchase Invoice"
        frmReport.Show()
    End Sub

    Private Sub rbOrderSample_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbOrderSample.CheckedChanged
        If rbOrderSample.Checked = True Then
            LoadSampleType()
        End If
    End Sub


    Private Sub rbOrderProduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbOrderProduction.CheckedChanged
        If rbOrderProduction.Checked = True Then
            LoadSampleType()
        End If
    End Sub


#Region "Debit Credit Note Against Sales"

    Private Sub DebitCreditNoteSales()
        If sPurchaseorSales = "Sales" And sTypeofDocument = "Credit Note Issued / Debit Not Received - For Sales" And sTypeofOrder = "All" Then '' Option 01


            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 01.01
                mdlSGM.nOption = 101
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.01
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.01
                    mdlSGM.sSelectedOption = "S0AAAAAAS"     ''Option 01.01.01

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.02
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.02
                    mdlSGM.sSelectedOption = "S0AAAAAAS"     ''Option 01.01.02

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.03
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.03
                    mdlSGM.sSelectedOption = "S0AAAAASS"     ''Option 01.01.03

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.04
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.04
                    mdlSGM.sSelectedOption = "S0AAAAASS"     ''Option 01.01.04

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.05
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.05
                    mdlSGM.sSelectedOption = "S0AAAASAS"     ''Option 01.01.05

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.06
                    mdlSGM.sSelectedOption = "S0AAAASAS"     ''Option 01.01.06

                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.07
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.07
                    mdlSGM.sSelectedOption = "S0AAAASSS"     ''Option 01.01.07

                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.08
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.08
                    mdlSGM.sSelectedOption = "S0AAAASSS"     ''Option 01.01.08

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.09
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.09
                    mdlSGM.sSelectedOption = "S0AAASAAS"     ''Option 01.01.09

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.10
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.10
                    mdlSGM.sSelectedOption = "S0AAASAAS"     ''Option 01.01.10

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.11
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.11
                    mdlSGM.sSelectedOption = "S0AAASASS"     ''Option 01.01.11

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.12
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.12
                    mdlSGM.sSelectedOption = "S0AAASASS"     ''Option 01.01.12

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.13
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.01.13
                    mdlSGM.sSelectedOption = "S0AAASSAS"     ''Option 01.01.13

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.14
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.01.14
                    mdlSGM.sSelectedOption = "S0AAASSAS"     ''Option 01.01.14

                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then '' Option 01.01.15
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.01.15
                    mdlSGM.sSelectedOption = "S0AAASSSS"     ''Option 01.01.15


                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then '' Option 01.01.16
                    ''  1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.01.16
                    mdlSGM.sSelectedOption = "S0AAASSSS"     ''Option 01.01.16


                End If

                'ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Completed" Then '' Option 01.02
            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 01.02 & 01.03
                mdlSGM.nOption = 102
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.01
                    mdlSGM.sSelectedOption = "S0AAFAAAS"     ''Option 01.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.02
                    mdlSGM.sSelectedOption = "S0AAFAAAS"     ''Option 01.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.03
                    mdlSGM.sSelectedOption = "S0AAFAASS"     ''Option 01.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.04
                    mdlSGM.sSelectedOption = "S0AAFAASS"     ''Option 01.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.05
                    mdlSGM.sSelectedOption = "S0AAFASAS"     ''Option 01.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.06
                    mdlSGM.sSelectedOption = "S0AAFASAS"     ''Option 01.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.07
                    mdlSGM.sSelectedOption = "S0AAFASSS"     ''Option 01.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.08
                    mdlSGM.sSelectedOption = "S0AAFASSS"     ''Option 01.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.09
                    mdlSGM.sSelectedOption = "S0AAFSAAS"     ''Option 01.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.10
                    mdlSGM.sSelectedOption = "S0AAFSAAS"     ''Option 01.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.11
                    mdlSGM.sSelectedOption = "S0AAFSASS"     ''Option 01.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.12
                    mdlSGM.sSelectedOption = "S0AAFSASS"     ''Option 01.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.02.13
                    mdlSGM.sSelectedOption = "S0AAFSSAS"     ''Option 01.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.02.14
                    mdlSGM.sSelectedOption = "S0AAFSSAS"     ''Option 01.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.02.15
                    mdlSGM.sSelectedOption = "S0AAFSSSS"     ''Option 01.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.02.16
                    mdlSGM.sSelectedOption = "S0AAFSSSS"     ''Option 01.02.16

                End If
            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 01.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 01.04
                mdlSGM.nOption = 104
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.01
                    mdlSGM.sSelectedOption = "S0ASAAAAS"     ''Option 01.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.02
                    mdlSGM.sSelectedOption = "S0ASAAAAS"     ''Option 01.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.03
                    mdlSGM.sSelectedOption = "S0ASAAASS"     ''Option 01.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.04
                    mdlSGM.sSelectedOption = "S0ASAAASS"     ''Option 01.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.05
                    mdlSGM.sSelectedOption = "S0ASAASAS"     ''Option 01.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.06
                    mdlSGM.sSelectedOption = "S0ASAASAS"     ''Option 01.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.07
                    mdlSGM.sSelectedOption = "S0ASAASSS"     ''Option 01.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.08
                    mdlSGM.sSelectedOption = "S0ASAASSS"     ''Option 01.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.09
                    mdlSGM.sSelectedOption = "S0ASASAAS"     ''Option 01.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.10
                    mdlSGM.sSelectedOption = "S0ASASAAS"     ''Option 01.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.11
                    mdlSGM.sSelectedOption = "S0ASASASS"     ''Option 01.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.12
                    mdlSGM.sSelectedOption = "S0ASASASS"     ''Option 01.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.04.13
                    mdlSGM.sSelectedOption = "S0ASASSAS"     ''Option 01.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.04.14
                    mdlSGM.sSelectedOption = "S0ASASSAS"     ''Option 01.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.04.15
                    mdlSGM.sSelectedOption = "S0ASASSSS"     ''Option 01.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.04.16
                    mdlSGM.sSelectedOption = "S0ASASSSS"     ''Option 01.04.16
                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 01.05 & 01.06
                mdlSGM.nOption = 105
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.01
                    mdlSGM.sSelectedOption = "S0ASFAAAS"     ''Option 01.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.02
                    mdlSGM.sSelectedOption = "S0ASFAAAS"     ''Option 01.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.03
                    mdlSGM.sSelectedOption = "S0ASFAASS"     ''Option 01.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.04
                    mdlSGM.sSelectedOption = "S0ASFAASS"     ''Option 01.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.05
                    mdlSGM.sSelectedOption = "S0ASFASAS"     ''Option 01.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.06
                    mdlSGM.sSelectedOption = "S0ASFASAS"     ''Option 01.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.07
                    mdlSGM.sSelectedOption = "S0ASFASSS"     ''Option 01.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.08
                    mdlSGM.sSelectedOption = "S0ASFASSS"     ''Option 01.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.09
                    mdlSGM.sSelectedOption = "S0ASFSAAS"     ''Option 01.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.10
                    mdlSGM.sSelectedOption = "S0ASFSAAS"     ''Option 01.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.11
                    mdlSGM.sSelectedOption = "S0ASFSASS"     ''Option 01.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.12
                    mdlSGM.sSelectedOption = "S0ASFSASS"     ''Option 01.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 01.05.13
                    mdlSGM.sSelectedOption = "S0ASFSSAS"     ''Option 01.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 01.05.14
                    mdlSGM.sSelectedOption = "S0ASFSSAS"     ''Option 01.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 01.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 01.05.15
                    mdlSGM.sSelectedOption = "S0ASFSSSS"     ''Option 01.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 01.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 01.05.16
                    mdlSGM.sSelectedOption = "S0ASFSSSS"     ''Option 01.05.16
                End If
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 01.06
            End If
        ElseIf sPurchaseorSales = "Sales" And sTypeofDocument = "Order" And sTypeofOrder <> "All" Then '' Option 02 / 03
            mdlSGM.nOption = 201
            If sCustomer = " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 02.02

                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.01
                    mdlSGM.sSelectedOption = "S0SAAAAAS"     ''Option 02.01.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.02
                    mdlSGM.sSelectedOption = "S0SAAAAAS"     ''Option 02.01.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.03
                    mdlSGM.sSelectedOption = "S0SAAAASS"     ''Option 02.01.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.04
                    mdlSGM.sSelectedOption = "S0SAAAASS"     ''Option 02.01.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.05
                    mdlSGM.sSelectedOption = "S0SAAASAS"     ''Option 02.01.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.06
                    mdlSGM.sSelectedOption = "S0SAAASAS"     ''Option 02.01.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.07
                    mdlSGM.sSelectedOption = "S0SAAASSS"     ''Option 02.01.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.08
                    mdlSGM.sSelectedOption = "S0SAAASSS"     ''Option 02.01.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.09
                    mdlSGM.sSelectedOption = "S0SAASAAS"     ''Option 02.01.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.10
                    mdlSGM.sSelectedOption = "S0SAASAAS"     ''Option 02.01.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.11
                    mdlSGM.sSelectedOption = "S0SAASASS"     ''Option 02.01.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.12
                    mdlSGM.sSelectedOption = "S0SAASASS"     ''Option 02.01.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.01.13
                    mdlSGM.sSelectedOption = "S0SAASSAS"     ''Option 02.01.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.01.14
                    mdlSGM.sSelectedOption = "S0SAASSAS"     ''Option 02.01.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.01.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.01.15
                    mdlSGM.sSelectedOption = "S0SAASSSS"     ''Option 02.01.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.01.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.01.16
                    mdlSGM.sSelectedOption = "S0SAASSSS"     ''Option 02.01.16
                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 02.02
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.01
                    mdlSGM.sSelectedOption = "S0SAFAAAS"     ''Option 02.02.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.02
                    mdlSGM.sSelectedOption = "S0SAFAAAS"     ''Option 02.02.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.03
                    mdlSGM.sSelectedOption = "S0SAFAASS"     ''Option 02.02.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.04
                    mdlSGM.sSelectedOption = "S0SAFAASS"     ''Option 02.02.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.05
                    mdlSGM.sSelectedOption = "S0SAFASAS"     ''Option 02.02.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.06
                    mdlSGM.sSelectedOption = "S0SAFASAS"     ''Option 02.02.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.07
                    mdlSGM.sSelectedOption = "S0SAFASSS"     ''Option 02.02.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.08
                    mdlSGM.sSelectedOption = "S0SAFASSS"     ''Option 02.02.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.09
                    mdlSGM.sSelectedOption = "S0SAFSAAS"     ''Option 02.02.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.10
                    mdlSGM.sSelectedOption = "S0SAFSAAS"     ''Option 02.02.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.11
                    mdlSGM.sSelectedOption = "S0SAFSASS"     ''Option 02.02.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.12
                    mdlSGM.sSelectedOption = "S0SAFSASS"     ''Option 02.02.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.02.13
                    mdlSGM.sSelectedOption = "S0SAFSSAS"     ''Option 02.02.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.02.14
                    mdlSGM.sSelectedOption = "S0SAFSSAS"     ''Option 02.02.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.02.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.02.15
                    mdlSGM.sSelectedOption = "S0SAFSSSS"     ''Option 02.02.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.02.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.02.16
                    mdlSGM.sSelectedOption = "S0SAFSSSS"     ''Option 02.02.16



                End If

            ElseIf sCustomer = " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 02.03
            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "All" Then '' Option 02.04
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.01
                    mdlSGM.sSelectedOption = "S0SSAAAAS"     ''Option 02.04.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.02
                    mdlSGM.sSelectedOption = "S0SSAAAAS"     ''Option 02.04.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.03
                    mdlSGM.sSelectedOption = "S0SSAAASS"     ''Option 02.04.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.04
                    mdlSGM.sSelectedOption = "S0SSAAASS"     ''Option 02.04.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.05
                    mdlSGM.sSelectedOption = "S0SSAASAS"     ''Option 02.04.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.06
                    mdlSGM.sSelectedOption = "S0SSAASAS"     ''Option 02.04.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.07
                    mdlSGM.sSelectedOption = "S0SSAASSS"     ''Option 02.04.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.08
                    mdlSGM.sSelectedOption = "S0SSAASSS"     ''Option 02.04.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.09
                    mdlSGM.sSelectedOption = "S0SSASAAS"     ''Option 02.04.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.10
                    mdlSGM.sSelectedOption = "S0SSASAAS"     ''Option 02.04.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.11
                    mdlSGM.sSelectedOption = "S0SSASASS"     ''Option 02.04.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.12
                    mdlSGM.sSelectedOption = "S0SSASASS"     ''Option 02.04.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.04.13
                    mdlSGM.sSelectedOption = "S0SSASSAS"     ''Option 02.04.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.04.14
                    mdlSGM.sSelectedOption = "S0SSASSAS"     ''Option 02.04.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.04.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.04.15
                    mdlSGM.sSelectedOption = "S0SSASSSS"     ''Option 02.04.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.04.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.04.16
                    mdlSGM.sSelectedOption = "S0SSASSSS"     ''Option 02.04.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus <> "All" Then '' Option 02.05
                If sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.01
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.01
                    mdlSGM.sSelectedOption = "S0SSFAAAS"     ''Option 02.05.01
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.02
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.02
                    mdlSGM.sSelectedOption = "S0SSFAAAS"     ''Option 02.05.02
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.03
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.03
                    mdlSGM.sSelectedOption = "S0SSFAASS"     ''Option 02.05.03
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.04
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.04
                    mdlSGM.sSelectedOption = "S0SSFAASS"     ''Option 02.05.04
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.05
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.05
                    mdlSGM.sSelectedOption = "S0SSFASAS"     ''Option 02.05.05
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.06
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.06
                    mdlSGM.sSelectedOption = "S0SSFASAS"     ''Option 02.05.06
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.07
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.07
                    mdlSGM.sSelectedOption = "S0SSFASSS"     ''Option 02.05.07
                ElseIf sArticleName = " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.08
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.08
                    mdlSGM.sSelectedOption = "S0SSFASSS"     ''Option 02.05.08
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.09
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.09
                    mdlSGM.sSelectedOption = "S0SSFSAAS"     ''Option 02.05.09
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.10
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.10
                    mdlSGM.sSelectedOption = "S0SSFSAAS"     ''Option 02.05.10
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.11
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.11
                    mdlSGM.sSelectedOption = "S0SSFSASS"     ''Option 02.05.11
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.12
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.12
                    mdlSGM.sSelectedOption = "S0SSFSASS"     ''Option 02.05.12
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.13
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 02.05.13
                    mdlSGM.sSelectedOption = "S0SSFSSAS"     ''Option 02.05.13
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.14
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 02.05.14
                    mdlSGM.sSelectedOption = "S0SSFSSAS"     ''Option 02.05.14
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 02.05.15
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 02.05.15
                    mdlSGM.sSelectedOption = "S0SSFSSSS"     ''Option 02.05.15
                ElseIf sArticleName <> " ALL ARTICLES" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 02.05.16
                    '' 1. S/P = Sales : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 02.05.16
                    mdlSGM.sSelectedOption = "S0SSFSSSS"     ''Option 02.05.16



                End If

            ElseIf sCustomer <> " ALL CUSTOMERS" And sOrderStatus = "Pending" Then '' Option 02.06
            End If
        End If
    End Sub

    Private Sub LoadDebitCreditNoteSales()
        Dim i As Integer = 0



        grdDCAgainstSales.BringToFront()

Ab:
        ngrdRowCount = grdDCAgainstSalesV1.RowCount
        For i = 0 To ngrdRowCount
            grdDCAgainstSalesV1.DeleteRow(i)
        Next
        ngrdRowCount = grdDCAgainstSalesV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            'sOrderStatus = "CLOSE"
            sOrderStatus = "Shipped"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "OPEN"
        ElseIf sOrderStatus = "Close" Then
            sOrderStatus = "Close"
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

        Dim sIsSampleOrder As String

        If rbOrderAll.Checked = True Then
            sIsSampleOrder = "All"
        ElseIf rbOrderProduction.Checked = True Then
            sIsSampleOrder = "Production"
        Else
            sIsSampleOrder = "Sample"
            sSampleOrderType = Trim(cbxSampleOrderType.SelectedValue)

        End If


        grdDCAgainstSales.DataSource = myccAllinOne.DCNoteAgainstSales(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sIsSampleOrder)

        With grdDCAgainstSalesV1

            .Columns(0).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1
            
            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(16).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(17).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(1).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(1).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(7).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With

        If rbDetailed.Checked = True Then
            grdSalesOrderDetails.Visible = True
            grdSalesOrderDetails.BringToFront()
            pgbar.Visible = True

            UpdateInformationforSalesOrderWithInvoiceDetatils()
            DstmpSalesorder.EnforceConstraints = False
            Me.Tmptbl_SalesOrderHeaderforSGMTableAdapter.Fill(Me.DstmpSalesorder.tmptbl_SalesOrderHeaderforSGM)
            Me.Tmptbl_SalesOrderDetailforSGMTableAdapter.Fill(Me.DstmpSalesorder.tmptbl_SalesOrderDetailforSGM)


            Dim View As GridView = grdSalesOrderDetailsV1
            Dim J As Integer = 0
            If chkbxDisplayDtl.CheckState = CheckState.Checked Then
                For J = 0 To grdSalesOrderDetailsV1.RowCount - 1
                    View.ExpandMasterRow(J)
                Next
            Else

                For J = 0 To grdSalesOrderDetailsV1.RowCount - 1
                    View.CollapseMasterRow(J)
                Next
            End If

        Else
            ''TODO : To replace
            ''UpdateInformationforSalesOrder()
            grdDCAgainstSales.BringToFront()
        End If
    End Sub

#End Region

#Region "Debit Credit Note Against Purchase"

    Private Sub DebitCreditNotePurchase()
        If sPurchaseorSales = "Purchase" And sTypeofDocument = "Credit Note Received / Debit Note Issued - For Purchase" And sTypeofOrder = "All" Then '' Option 07

            If sCustomer = " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 07.01
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.01
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.01
                    mdlSGM.sSelectedOption = "P0AAAAAAS"     ''Option 07.01.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.02
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.02
                    mdlSGM.sSelectedOption = "P0AAAAAAS"     ''Option 07.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.03
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.03
                    mdlSGM.sSelectedOption = "P0AAAAASS"     ''Option 07.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.04
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.04
                    mdlSGM.sSelectedOption = "P0AAAAASS"     ''Option 07.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.05
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.05
                    mdlSGM.sSelectedOption = "P0AAAASAS"     ''Option 07.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.06
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.06
                    mdlSGM.sSelectedOption = "P0AAAASAS"     ''Option 07.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.07
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.07
                    mdlSGM.sSelectedOption = "P0AAAASSS"     ''Option 07.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.08
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.08
                    mdlSGM.sSelectedOption = "P0AAAASSS"     ''Option 07.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.09
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.09
                    mdlSGM.sSelectedOption = "P0AAASAAS"     ''Option 07.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.10
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.10
                    mdlSGM.sSelectedOption = "P0AAASAAS"     ''Option 07.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.11
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.11
                    mdlSGM.sSelectedOption = "P0AAASASS"     ''Option 07.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.12
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.12
                    mdlSGM.sSelectedOption = "P0AAASASS"     ''Option 07.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.13
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.01.13
                    mdlSGM.sSelectedOption = "P0AAASSAS"     ''Option 07.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.14
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.01.14
                    mdlSGM.sSelectedOption = "P0AAASSAS"     ''Option 07.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.01.15
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.01.15
                    mdlSGM.sSelectedOption = "P0AAASSSS"     ''Option 07.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.01.16
                    ''1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.01.16
                    mdlSGM.sSelectedOption = "P0AAASSSS"     ''Option 07.01.16
                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 07.02 / 07.03
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.01
                    mdlSGM.sSelectedOption = "P0AAFAAAS"     ''Option 07.02.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.02
                    mdlSGM.sSelectedOption = "P0AAFAAAS"     ''Option 07.02.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.03
                    mdlSGM.sSelectedOption = "P0AAFAASS"     ''Option 07.02.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.04
                    mdlSGM.sSelectedOption = "P0AAFAASS"     ''Option 07.02.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.05
                    mdlSGM.sSelectedOption = "P0AAFASAS"     ''Option 07.02.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.06
                    mdlSGM.sSelectedOption = "P0AAFASAS"     ''Option 07.02.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.07
                    mdlSGM.sSelectedOption = "P0AAFASSS"     ''Option 07.02.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.08
                    mdlSGM.sSelectedOption = "P0AAFASSS"     ''Option 07.02.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.09
                    mdlSGM.sSelectedOption = "P0AAFSAAS"     ''Option 07.02.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.10
                    mdlSGM.sSelectedOption = "P0AAFSAAS"     ''Option 07.02.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.11
                    mdlSGM.sSelectedOption = "P0AAFSASS"     ''Option 07.02.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.12
                    mdlSGM.sSelectedOption = "P0AAFSASS"     ''Option 07.02.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.02.13
                    mdlSGM.sSelectedOption = "P0AAFSSAS"     ''Option 07.02.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.02.14
                    mdlSGM.sSelectedOption = "P0AAFSSAS"     ''Option 07.02.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.02.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.02.15
                    mdlSGM.sSelectedOption = "P0AAFSSSS"     ''Option 07.02.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.02.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = All : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.02.16
                    mdlSGM.sSelectedOption = "P0AAFSSSS"     ''Option 07.02.16



                End If

            ElseIf sCustomer = " ALL SUPPLIERS" And sOrderStatus = "Pending" Then '' Option 07.03
            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus = "All" Then '' Option 07.04
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.01
                    mdlSGM.sSelectedOption = "P0ASAAAAS"     ''Option 07.04.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.02
                    mdlSGM.sSelectedOption = "P0ASAAAAS"     ''Option 07.04.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.03
                    mdlSGM.sSelectedOption = "P0ASAAASS"     ''Option 07.04.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.04
                    mdlSGM.sSelectedOption = "P0ASAAASS"     ''Option 07.04.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.05
                    mdlSGM.sSelectedOption = "P0ASAASAS"     ''Option 07.04.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.06
                    mdlSGM.sSelectedOption = "P0ASAASAS"     ''Option 07.04.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.07
                    mdlSGM.sSelectedOption = "P0ASAASSS"     ''Option 07.04.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.08
                    mdlSGM.sSelectedOption = "P0ASAASSS"     ''Option 07.04.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.09
                    mdlSGM.sSelectedOption = "P0ASASAAS"     ''Option 07.04.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.10
                    mdlSGM.sSelectedOption = "P0ASASAAS"     ''Option 07.04.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.11
                    mdlSGM.sSelectedOption = "P0ASASASS"     ''Option 07.04.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.12
                    mdlSGM.sSelectedOption = "P0ASASASS"     ''Option 07.04.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.04.13
                    mdlSGM.sSelectedOption = "P0ASASSAS"     ''Option 07.04.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "DetaileS" Then  ' Option 07.04.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.04.14
                    mdlSGM.sSelectedOption = "P0ASASSAS"     ''Option 07.04.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.04.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.04.15
                    mdlSGM.sSelectedOption = "P0ASASSSS"     ''Option 07.04.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.04.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.04.16
                    mdlSGM.sSelectedOption = "P0ASASSSS"     ''Option 07.04.16



                End If

            ElseIf sCustomer <> " ALL SUPPLIERS" And sOrderStatus <> "All" Then '' Option 07.05 / 07.06
                If sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.01
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.01
                    mdlSGM.sSelectedOption = "P0ASFAAAS"     ''Option 07.05.01
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.02
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.02
                    mdlSGM.sSelectedOption = "P0ASFAAAS"     ''Option 07.05.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.03
                    mdlSGM.sSelectedOption = "P0ASFAASS"     ''Option 07.05.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.04
                    mdlSGM.sSelectedOption = "P0ASFAASS"       ''Option 07.05.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.05
                    mdlSGM.sSelectedOption = "P0ASFASAS"     ''Option 07.05.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.06
                    mdlSGM.sSelectedOption = "P0ASFASAS"       ''Option 07.05.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.07
                    mdlSGM.sSelectedOption = "P0ASFASSS"     ''Option 07.05.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.08
                    mdlSGM.sSelectedOption = "P0ASFASSS"       ''Option 07.05.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.09
                    mdlSGM.sSelectedOption = "P0ASFSAAS"     ''Option 07.05.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.10
                    mdlSGM.sSelectedOption = "P0ASFSAAS"       ''Option 07.05.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.11
                    mdlSGM.sSelectedOption = "P0ASFSASS"     ''Option 07.05.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.12
                    mdlSGM.sSelectedOption = "P0ASFSASS"       ''Option 07.05.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 07.05.13
                    mdlSGM.sSelectedOption = "P0ASFSSAS"     ''Option 07.05.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 07.05.14
                    mdlSGM.sSelectedOption = "P0ASFSSAS"       ''Option 07.05.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 07.05.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 07.05.15
                    mdlSGM.sSelectedOption = "P0ASFSSSS"     ''Option 07.05.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 07.05.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = All : 4. Customer  = Selected : 5. Order Status  = Completed / Pending : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 07.05.16
                    mdlSGM.sSelectedOption = "P0ASFSSSS"       ''Option 07.05.16



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
                    mdlSGM.sSelectedOption = "P0SAAAAAS"     ''Option 08.01.02
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.03
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.03
                    mdlSGM.sSelectedOption = "P0SAAAASS"     ''Option 08.01.03
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.04
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.04
                    mdlSGM.sSelectedOption = "P0SAAAASS"     ''Option 08.01.04
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.05
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.05
                    mdlSGM.sSelectedOption = "P0SAAASAS"     ''Option 08.01.05
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.06
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.06
                    mdlSGM.sSelectedOption = "P0SAAASAS"     ''Option 08.01.06
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.07
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.07
                    mdlSGM.sSelectedOption = "P0SAAASSS"     ''Option 08.01.07
                ElseIf sArticleName = " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.08
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = All : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.08
                    mdlSGM.sSelectedOption = "P0SAAASSS"     ''Option 08.01.08
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.09
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.09
                    mdlSGM.sSelectedOption = "P0SAASAAS"     ''Option 08.01.09
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.10
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.10
                    mdlSGM.sSelectedOption = "P0SAASAAS"     ''Option 08.01.10
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.11
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.11
                    mdlSGM.sSelectedOption = "P0SAASASS"     ''Option 08.01.11
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode = "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.12
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = All : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.12
                    mdlSGM.sSelectedOption = "P0SAASASS"     ''Option 08.01.12
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.13
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Summary :: 08.01.13
                    mdlSGM.sSelectedOption = "P0SAASSAS"     ''Option 08.01.13
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription = "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.14
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = All : 9. Summary / Detailed = Detailed :: 08.01.14
                    mdlSGM.sSelectedOption = "P0SAASSAS"     ''Option 08.01.14
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Summary" Then  ' Option 08.01.15
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Summary :: 08.01.15
                    mdlSGM.sSelectedOption = "P0SAASSSS"     ''Option 08.01.15
                ElseIf sArticleName <> " ALL MATERIALS" And sArticleCode <> "" And sArticleDescription <> "" And sSummaryorDetailed = "Detailed" Then  ' Option 08.01.16
                    '' 1. S/P = Purchase : 2. Ord / Inv  = Order : 3. Order Type  = Job / Sales : 4. Customer  = All : 5. Order Status  = All : 6. Article  = Selected : 7. Article  Code = Selected : 8. Article  Description = Selected : 9. Summary / Detailed = Detailed :: 08.01.16
                    mdlSGM.sSelectedOption = "P0SAASSSS"     ''Option 08.01.16



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
            End If
        End If
    End Sub

    Private Sub LoadDebitCreditNotePurchase()
        Dim i As Integer = 0

        grdDCAgainstPurchase.BringToFront()

Ab:
        ngrdRowCount = grdDCAgainstPurchaseV1.RowCount
        For i = 0 To ngrdRowCount
            grdDCAgainstPurchaseV1.DeleteRow(i)
        Next
        ngrdRowCount = grdDCAgainstPurchaseV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        If sOrderStatus = "Completed" Then
            'sOrderStatus = "CLOSE"
            sOrderStatus = "Shipped"
        ElseIf sOrderStatus = "Pending" Then
            sOrderStatus = "OPEN"
        ElseIf sOrderStatus = "Close" Then
            sOrderStatus = "Close"
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

        Dim sIsSampleOrder As String

        If rbOrderAll.Checked = True Then
            sIsSampleOrder = "All"
        ElseIf rbOrderProduction.Checked = True Then
            sIsSampleOrder = "Production"
        Else
            sIsSampleOrder = "Sample"
            sSampleOrderType = Trim(cbxSampleOrderType.SelectedValue)
        End If

        If sArticleName <> " ALL MATERIALS" Then
            Dim nLen As Integer = Microsoft.VisualBasic.Len(sArticleName)

            Dim j As Integer = 1

            For j = 1 To nLen
                If Microsoft.VisualBasic.Mid(sArticleName, j, 1) = ":" Then
                    Exit For
                End If
            Next


            sMaterialTypeDescription = Trim(Microsoft.VisualBasic.Left(sArticleName, j - 1))
            sMaterialSubTypeDescription = Microsoft.VisualBasic.Right(sArticleName, (nLen - (j + 1)))
        Else
            sMaterialTypeDescription = ""
            sMaterialSubTypeDescription = ""
        End If


        grdDCAgainstPurchase.DataSource = myccAllinOne.DCNoteAgainstPurchase(sTypeofOrder, sOrderStatus, Trim(tbArticleCode.Text), Trim(tbArticleDescription.Text), sIsSampleOrder, sMaterialTypeDescription, sMaterialSubTypeDescription)

        With grdDCAgainstPurchaseV1

            .Columns(0).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(16).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(17).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(1).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(1).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(7).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(7).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With

        If rbDetailed.Checked = True Then
            grdSalesOrderDetails.Visible = True
            grdSalesOrderDetails.BringToFront()
            pgbar.Visible = True

            UpdateInformationforSalesOrderWithInvoiceDetatils()
            DstmpSalesorder.EnforceConstraints = False
            Me.Tmptbl_SalesOrderHeaderforSGMTableAdapter.Fill(Me.DstmpSalesorder.tmptbl_SalesOrderHeaderforSGM)
            Me.Tmptbl_SalesOrderDetailforSGMTableAdapter.Fill(Me.DstmpSalesorder.tmptbl_SalesOrderDetailforSGM)


            Dim View As GridView = grdSalesOrderDetailsV1
            Dim J As Integer = 0
            If chkbxDisplayDtl.CheckState = CheckState.Checked Then
                For J = 0 To grdSalesOrderDetailsV1.RowCount - 1
                    View.ExpandMasterRow(J)
                Next
            Else

                For J = 0 To grdSalesOrderDetailsV1.RowCount - 1
                    View.CollapseMasterRow(J)
                Next
            End If

        Else
            ''TODO : To replace
            ''UpdateInformationforSalesOrder()
            grdDCAgainstPurchase.BringToFront()
        End If
    End Sub

#End Region

  
    
End Class