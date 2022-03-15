Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmRejection2
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup
    Dim sCnn As New SqlConnection(sConstr)

    Dim myWastageDetails As New ccWastageDetails
    Dim myccInvoicesWithDetails As New ccInvoicesWithDetails

    Dim mystrSolarInvoice4SGM4Print As New strSolarInvoice4SGM4Print
    Dim mystrSolarPurchaseInvoice4SGM4Print As New strSolarPurchaseInvoice4SGM4Print

    Dim sType As String

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'DstmpPurchaseOrder.Clear()
        'DstmpPurchaseOrder.EnforceConstraints = False

        'Me.Tmptbl_PurchaseOrderforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderforSGM)
        'Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM)













        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
        cbxTypeofOrder.SelectedIndex = 0
        cbxTypeofDocument.SelectedIndex = 0
        'LoadCustomer()
        'LoadArticle()
    End Sub

    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdWastageDetail2.ExportToXlsx("D:\GrindingDetails2.xlsx")
        MsgBox("Export Completed")

    End Sub


    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        
        mdlSGM.sIsLoaded = "N"
        If rbInAndOut.Checked = True Then
            mdlSGM.sSelectedOption = "All"
        ElseIf rbInJournal.Checked = True Then
            mdlSGM.sSelectedOption = "GRINDING IN"
        Else
            mdlSGM.sSelectedOption = "GRINDING OUT"
        End If
        grdWastageDetail2.DataSource = myWastageDetails.LoadWastagesDetails2(mdlSGM.sSelectedOption, Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"), sType)

        With grdWastageDetail2V1

            '.Columns(6).VisibleIndex = -1
            '.Columns(16).VisibleIndex = -1

            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            ''.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            '.Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(3).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(3).DisplayFormat.FormatString = "dd-MMM-yyyy"
            .Columns(8).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Columns(8).DisplayFormat.FormatString = "0.00"
            .Columns(9).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Columns(9).DisplayFormat.FormatString = "0.00"
            .Columns(10).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Columns(10).DisplayFormat.FormatString = "0.00"
            .Columns(12).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Columns(12).DisplayFormat.FormatString = "0.00"

        End With
        mdlSGM.sIsLoaded = "Y"

    End Sub

    Dim ngrdRowNo As Integer

 
    
End Class