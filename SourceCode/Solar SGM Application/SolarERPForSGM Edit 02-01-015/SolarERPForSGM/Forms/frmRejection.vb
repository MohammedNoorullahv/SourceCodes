Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmRejection
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
        grdWastageDetails.ExportToXlsx("D:\GrindingDetails.xlsx")
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
        grdWastage.DataSource = myWastageDetails.LoadWastages(mdlSGM.sSelectedOption, Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"))

        With grdPurchaseInvoicesV1

            '.Columns(6).VisibleIndex = -1
            '.Columns(16).VisibleIndex = -1

            '.Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            ''.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            '.Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            '.Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With
        mdlSGM.sIsLoaded = "Y"

    End Sub

    Dim ngrdRowNo As Integer

    Private Sub grdPurchaseInvoicesV1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPurchaseInvoicesV1.DoubleClick

        ngrdRowNo = grdPurchaseInvoicesV1.FocusedRowHandle
        If ngrdRowNo < 0 Then
            MsgBox("No Data Exist in the table for Detail Display Purpose", MsgBoxStyle.Information)
            Exit Sub
        End If
        sType = grdPurchaseInvoicesV1.GetDataRow(ngrdRowNo).Item("TypeofMaterial").ToString
        LoadWastageDetails()
    End Sub
    Private Sub grdPurchaseInvoicesV1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grdPurchaseInvoicesV1.FocusedRowChanged
        If mdlSGM.sIsLoaded = "Y" Then
            ngrdRowNo = grdPurchaseInvoicesV1.FocusedRowHandle
            If ngrdRowNo < 0 Then
                MsgBox("No Data Exist in the table for Detail Display Purpose", MsgBoxStyle.Information)
                Exit Sub
            End If
            sType = grdPurchaseInvoicesV1.GetDataRow(ngrdRowNo).Item("TypeofMaterial").ToString
            LoadWastageDetails()
        End If
    End Sub

    Private Sub LoadWastageDetails()
        If rbInAndOut.Checked = True Then
            mdlSGM.sSelectedOption = "All"
        ElseIf rbInJournal.Checked = True Then
            mdlSGM.sSelectedOption = "GRINDING IN"
        Else
            mdlSGM.sSelectedOption = "GRINDING OUT"
        End If
        grdWastageDetails.DataSource = myWastageDetails.LoadWastagesDetails(mdlSGM.sSelectedOption, Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"), sType)

        With grdWastageDetailsV1

            '.Columns(6).VisibleIndex = -1
            '.Columns(16).VisibleIndex = -1

            '.Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            ''.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            .Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            .Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            .Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With
    End Sub
End Class