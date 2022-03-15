Option Explicit On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.IO.Ports
Imports System.Drawing.Rectangle
Imports System.Net.Mail

'Imports PCComm

Public Class frmSLIFSPacking

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard

    Private comm As New CommManager()
    Private transType As String = String.Empty


    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try


        ''Catch ex As Exception

        ''End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
        'Me.Hide()
        'Form1.Show()
    End Sub

    Dim ngrdRowCount As Integer

    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

  
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToXls("D:\ArticleMaster.xls")
        MsgBox("Export Completed")

    End Sub

    Dim keyascii As Integer
   
    Private Sub tbJobcardNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbJobcardNo.KeyPress
        'Try

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then

            If Trim(tbJobcardNo.Text) = "" Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Jobcard")
                Exit Sub
            End If

            Dim daSelPackingInfo As New SqlDataAdapter("Select * from PackingDetail Where Barcode = '" & Trim(tbJobcardNo.Text) & "'", sConstr)
            Dim dsSelPackingInfo As New DataSet
            daSelPackingInfo.Fill(dsSelPackingInfo)

            If dsSelPackingInfo.Tables(0).Rows.Count = 0 Then
                Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                MsgBox("Invalid Jobcard")
                Exit Sub
            Else
                If Val(dsSelPackingInfo.Tables(0).Rows(0).Item("IsPacked").ToString) <> 0 Then
                    Me.AxWindowsMediaPlayer1.URL = "E:\pingwarb-7327.mp3"
                    Exit Sub
                Else
                    Dim daUpdPackingInfo As New SqlDataAdapter("Update PackingDetail Set IsPacked = '1', PackedOn = '" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & _
                                                               "' Where Barcode = '" & Trim(tbJobcardNo.Text) & "'", sConstr)
                    Dim dsUpdPackingInfo As New DataSet
                    daUpdPackingInfo.Fill(dsUpdPackingInfo)
                    dsUpdPackingInfo.AcceptChanges()
                End If
            End If

        End If

        'Catch ex As Exception

        'End Try
    End Sub

    Dim nCartonNo As Integer
    Dim sImageFileName As String

    Private Sub Clear()
       

    End Sub

  
    Private Sub frmKHLIOuterAndInnerScanning_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub



    Private Sub Loadgrdinfo()
        ''Try


        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdCartonDtlsV1.RowCount
        For i = 0 To ngrdRowCount
            grdCartonDtlsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdCartonDtlsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'grdCartonDtls.DataSource = myccKHLIOutstandingWithJobcard.LoadPackedInfo(sFKJobcardId)

        With grdCartonDtlsV1

            .Columns(0).VisibleIndex = -1
            .Columns(1).VisibleIndex = -1
            .Columns(4).VisibleIndex = -1
            .Columns(5).VisibleIndex = -1
            '.Columns(6).VisibleIndex = -1
            '.Columns(7).VisibleIndex = -1
            .Columns(9).VisibleIndex = -1
            .Columns(10).VisibleIndex = -1
            .Columns(14).VisibleIndex = -1
            .Columns(15).VisibleIndex = -1
            .Columns(16).VisibleIndex = -1
            .Columns(17).VisibleIndex = -1
            .Columns(18).VisibleIndex = -1
            .Columns(19).VisibleIndex = -1


            .Columns(2).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count

        End With


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


  
   

 
   

    Private Sub tbJobcardNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbJobcardNo.TextChanged

    End Sub
End Class