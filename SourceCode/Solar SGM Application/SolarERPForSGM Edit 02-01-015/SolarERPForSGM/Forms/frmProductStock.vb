Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmProductStock
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    'Dim myccOrderPlanningReport As New ccOrderPlanningReport
    'Dim myccERPTrackingv1 As New ccERPTrackingv1
    'Dim mystrERPTrackingV1 As New strERPTrackingV1
    'Dim mystrERPTrackingV2 As New strERPTrackingV2
    Dim myccProductStock As New ccProductStock
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


        LoadComboItems()
        ' sDateChanged = "Y"
        sDateChanged = "N"
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
     
        grdSalesOrderV1.ExportToXlsx("E:\Product Stock.xlsx")
     
        MsgBox("Export Completed")

    End Sub

    Dim sTypeofOrder, sTypeofDocument As String
    Dim sSelOption As String
    Dim nIsEDDNegotiable As Integer

    Private Sub LoadData()

        mdlSGM.sSelectedOption = ""

        If cbxCustomer.Text.ToUpper = " ALL CUSTOMERS" Or cbxCustomer.Text = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxArticleMould.Text.ToUpper = " ALL MOULD" Or cbxArticleMould.Text = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxArticle.Text.ToUpper = " ALL ARTICLE" Or cbxArticle.Text = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If cbxDepartment.Text = "ALL DATA" Or cbxDepartment.Text = "" Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        End If

        If chkbxWorkingProcess.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "WP"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "WW"
        End If

        If chkbxFinished.Checked = True Then
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "F"
        Else
            mdlSGM.sSelectedOption = mdlSGM.sSelectedOption + "A"
        End If

        sIsLoaded = "N"
        
        grdSalesOrder.DataSource = myccProductStock.LoadProductStock(dpFrom.Value, cbxDepartment.Text, cbxArticle.Text, cbxArticleMould.Text, cbxCustomer.Text)

        With grdSalesOrderV1

            .Columns(0).VisibleIndex = -1

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(5).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            
        End With

        MsgBox("Loading Completed")
        sIsLoaded = "Y"
    End Sub

    Private Sub dpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dpFrom.ValueChanged

        'LoadComboItems()
        'sDateChanged = "Y"
    End Sub

    Private Sub LoadComboItems()

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")

        cbxCustomer.DataSource = myccProductStock.LoadCustomer(dpFrom.Value)
        cbxCustomer.DisplayMember = "BuyerName"

        cbxArticleMould.DataSource = myccProductStock.LoadArticleMould(dpFrom.Value)
        cbxArticleMould.DisplayMember = "ArticleMould"

        cbxArticle.DataSource = myccProductStock.LoadArticle(dpFrom.Value)
        cbxArticle.DisplayMember = "Article"

        cbxDepartment.SelectedIndex = 0

    End Sub



    Dim ngrdRowNo, nBal2Dispatch As Integer
    Dim sSalesOrderDetailsID, sIsNegotiable, sIsLoaded As String
    Dim WeekNumber As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        'InsertProductStock()
        LoadData()
        'cbReferesh.Enabled = False
    End Sub

    Dim nMouldQty, nM2FQty, nFinishQty, nPkdStockQty, nRejectionQty As Integer


    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim daDelOrderPlanning As New SqlDataAdapter("Delete from TempERPTrackingSystem", sConstr)
        Dim dsDelOrderPlanning As New DataSet
        daDelOrderPlanning.Fill(dsDelOrderPlanning)
        dsDelOrderPlanning.AcceptChanges()

        Dim i As Integer = 0
       

        i = 0
        'For i = 0 To grdSalesOrderDetailsV1.RowCount - 1

        '    InsertERPTracking()
        'Next

        mdlSGM.sReportType = "ERP TRACKING SYSTEM DETAILS"

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

  
    Dim nCount As Integer = 0
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Format(Date.Now, "HH") = "11" And Format(Date.Now, "mm") = "00" Then
            If nCount = 0 Then
                InsertProductStock()
            End If
            nCount = nCount + 1
            MessageBox.Show("Time - " + nCount.ToString)
        End If
    End Sub

    Private Sub InsertProductStock()
        myccProductStock.InsertProductStock()
        myccProductStock.UpdateStockDaysCount()
    End Sub
End Class