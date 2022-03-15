Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmRejectionMainReport
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccWastageDetails As New ccWastageDetails
    

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub


    Dim ngrdRowCount As Integer


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)

        cbxType.DataSource = Nothing : cbxType.Items.Clear()
        cbxType.DataSource = myccWastageDetails.LoadMaterialType
        cbxType.DisplayMember = "MaterialSubTypeDescription" '': cbxArticleName.ValueMember = "PKID"

    End Sub


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        
        MsgBox("Export Completed")

    End Sub

    Dim dSupplierBillDate As Date


    Private Sub Panel5_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub rbIn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbIn.CheckedChanged

        If rbIn.Checked = True Then
            plProcessed.Enabled = True
            plStatus.Enabled = False
        Else
            plProcessed.Enabled = False
            plStatus.Enabled = True
        End If
    End Sub

    Dim sSelectedOption, sIsResuable As String
    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click


        'cbxType.DataSource = Nothing : cbxType.Items.Clear()
        'cbxType.DataSource = myccWastageDetails.LoadMaterialType
        'cbxType.DisplayMember = "MaterialSubTypeDescription" '': cbxArticleName.ValueMember = "PKID"

        If rbIn.Checked = True Then
            ''All Materials
            If cbxType.Text = " ALL TYPES" Then
                If rbProcessAll.Checked = True Then
                    sSelectedOption = "IAA"
                    LoadRejectionIn()
                Else
                    If rbProcessYes.Checked = True Then
                        sSelectedOption = "IAFY"
                    Else
                        sSelectedOption = "IAFN"
                    End If
                    LoadRejectionIn()
                End If
                ''Selected Materials
            Else
                If rbProcessAll.Checked = True Then
                    sSelectedOption = "IFA"
                    LoadRejectionOut()
                Else
                    If rbProcessYes.Checked = True Then
                        sSelectedOption = "IFFY"
                    Else
                        sSelectedOption = "IFFN"
                    End If
                    LoadRejectionOut()
                End If
            End If
        ElseIf rbOut.Checked Then
            ''All Materials
            If cbxType.Text = " ALL TYPES" Then
                If rbOutStatusAll.Checked = True Then
                    sSelectedOption = "OAA"
                    LoadRejectionOut()
                Else

                    sSelectedOption = "OAF"
                    If rbOutStatusReUsable.Checked = True Then
                        sIsResuable = "USE"
                    Else
                        sIsResuable = "NON"
                    End If
                    LoadRejectionOut()
                End If
                ''Selected Materials
            Else
                If rbOutStatusAll.Checked = True Then
                    sSelectedOption = "OFA"
                    LoadRejectionOut()
                Else
                    sSelectedOption = "OFF"
                    If rbOutStatusReUsable.Checked = True Then
                        sIsResuable = "USE"
                    Else
                        sIsResuable = "NON"
                    End If
                    LoadRejectionOut()
                End If
            End If
        End If


    End Sub

    Private Sub LoadRejectionIn()
        Dim i As Integer = 0

        grdRejectionIn.BringToFront()

Ab:
        ngrdRowCount = grdRejectionInV1.RowCount
        For i = 0 To ngrdRowCount
            grdRejectionInV1.DeleteRow(i)
        Next
        ngrdRowCount = grdRejectionInV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        grdRejectionIn.DataSource = myccWastageDetails.RejectionIn(sSelectedOption, Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"), cbxType.Text)
        
        With grdRejectionInV1

            

            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            ''.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            '.Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(9).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(10).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            '.Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With

        
    End Sub

    Private Sub LoadRejectionOut()
        Dim i As Integer = 0

        grdRejectionOut.BringToFront()

Ab:
        ngrdRowCount = grdRejectionOutV1.RowCount
        For i = 0 To ngrdRowCount
            grdRejectionOutV1.DeleteRow(i)
        Next
        ngrdRowCount = grdRejectionOutV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        grdRejectionOut.DataSource = myccWastageDetails.RejectionOut(sSelectedOption, Format(dpFrom.Value, "dd-MMM-yyyy"), Format(dpTo.Value, "dd-MMM-yyyy"), cbxType.Text, sIsResuable)

        With grdRejectionOutV1

            '.Columns(6).VisibleIndex = -1
            '.Columns(16).VisibleIndex = -1

            '.Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(11).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            ''.Columns(12).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
            '.Columns(13).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(14).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(15).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum

            '.Columns(0).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            '.Columns(0).DisplayFormat.FormatString = "dd-MMM-yyyy"

        End With


    End Sub

End Class