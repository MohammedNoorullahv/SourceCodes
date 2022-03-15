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

Public Class frmAHGroupBackUp

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrBkpUp As String = Global.SolarERPForSGM.My.Settings.AHGroupBkpup
    Dim sCnnBkpUp As New SqlConnection(sConstrBkpUp)

    Dim sConstrAudit As String = Global.SolarERPForSGM.My.Settings.AHGroupAudit
    'Dim sCnnAudit As SqlConnection(sConstrAudit)

    Dim myccKHLIOutstandingWithJobcard As New ccKHLIOutstandingWithJobcard


    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        Try
            UpdateShippedInfo()
            'UpdateBackUp()

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
        'Me.Hide()
        'Form1.Show()
    End Sub

    Dim ngrdRowCount As Integer
    Dim sIpAddress As String
    Private Sub frmAHGroupBackUp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'UpdateShippedInfo()
        'Timer1.Enabled = True
    End Sub

    Dim keyascii As Integer
    Dim sBackUPStarted As String = "N"

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        'lblTime.Text = Format(Date.Now, "tt-hh:mm:ss")

        If sBackUPStarted = "N" Then
            If Format(Date.Now, "hh") = "08" And Format(Date.Now, "mm") = "00" And Format(Date.Now, "tt") = "PM" Then
                sBackUPStarted = "Y"

                UpdateBackUp()
            End If

            If Format(Date.Now, "hh") = "20" And Format(Date.Now, "mm") = "00" Then
                sBackUPStarted = "Y"

                UpdateBackUp()
            End If


        End If

        If Format(Date.Now, "hh") = "10" And Format(Date.Now, "mm") = "00" And Format(Date.Now, "tt") = "PM" Then
            System.Diagnostics.Process.Start("shutdown", "-s -t 00")
            End
        End If
        
    End Sub


    Dim sTableName As String
    Dim dYesterDay, dToday, dTomorrow As Date
    Dim sIncompleteTable As String = ""
    Dim nErrorcount, nTransferCompleted As Integer

    Private Sub UpdateBackUp()
        Try
            nTransferCompleted = 0

            Dim daSelAllTables As New SqlDataAdapter("Select * from sys.Tables Order By Name", sConstr)
            Dim dsSelAllTables As New DataSet
            daSelAllTables.Fill(dsSelAllTables)

            dToday = Format(Date.Now, "dd-MMM-yyyy")
            dYesterDay = Format((DateAdd(DateInterval.Day, -1, Date.Now)), "dd-MMM-yyyy")
            dTomorrow = Format((DateAdd(DateInterval.Day, 1, Date.Now)), "dd-MMM-yyyy")

            Dim i As Integer = 0
            nErrorcount = 0
            For i = 0 To dsSelAllTables.Tables(0).Rows.Count - 1
                Try

                    sTableName = dsSelAllTables.Tables(0).Rows(i).Item("name")

                    'Dim daselRecords As New SqlDataAdapter("Select * from " & sTableName & "", sConstr)
                    'Dim dsSelRecords As New DataSet
                    'daselRecords.Fill(dsSelRecords)

                    nRecords = 0 'dsSelRecords.Tables(0).Rows.Count

                    'Dim daLastData As New SqlDataAdapter("Select Top 1 ModifiedDate from " & sTableName & " Order by Modifieddate Desc", sConstrBkpUp)
                    'Dim dsLastData As New DataSet
                    'daLastData.Fill(dsLastData)

                    'dLastModifiedDate = Format(dsLastData.Tables(0).Rows(0).Item(0), "dd-MMM-yyyy hh:mm:ss")

                    'Dim daDelModifiedData As New SqlDataAdapter("Delete from " & sTableName & " Where ID in (Select ID From AHServer.AHGroup.dbo." & sTableName & _
                    '                                       " Where (ModifiedDate > '" & Format(dLastModifiedDate.Date, "dd-MMM-yyyy") & "'))", sConstrBkpUp)
                    'Dim dsDelModifiedDate As New DataSet
                    'daDelModifiedData.Fill(dsDelModifiedDate)
                    'dsDelModifiedDate.AcceptChanges()

                    'If sTableName = "ActionAmend" Then
                    'CompleteDeleteAndAdd(sTableName)
                    Dim daInsNewData As New SqlDataAdapter("Insert Into " & sTableName & " Select * from AHServer.AHGroup.dbo." & sTableName & _
                                                           " Where ID Not In (Select ID From AHGroup.dbo." & sTableName & ")", sConstrBkpUp)
                    Dim dsInsNewData As New DataSet
                    daInsNewData.Fill(dsInsNewData)
                    dsInsNewData.AcceptChanges()

                    Dim daDelModifiedData As New SqlDataAdapter("Delete from " & sTableName & " Where ID in (Select ID From AHServer.AHGroup.dbo." & sTableName & _
                                                           " Where (ModifiedDate > '" & Format(dYesterDay.Date, "dd-MMM-yyyy") & "' And ModifiedDate < '" & Format(dTomorrow.Date, "dd-MMM-yyyy") & _
                                                           "') And (CreatedDate < '" & Format(dToday.Date, "dd-MMM-yyyy") & "'))", sConstrBkpUp)
                    Dim dsDelModifiedDate As New DataSet
                    daDelModifiedData.Fill(dsDelModifiedDate)
                    dsDelModifiedDate.AcceptChanges()

                    Dim daInsModifiedData As New SqlDataAdapter("Insert Into " & sTableName & " Select * From AHServer.AHGroup.dbo." & sTableName & _
                                                           " Where (ModifiedDate > '" & Format(dYesterDay.Date, "dd-MMM-yyyy") & "' And ModifiedDate < '" & Format(dTomorrow.Date, "dd-MMM-yyyy") & _
                                                           "') And (CreatedDate < '" & Format(dToday.Date, "dd-MMM-yyyy") & "')", sConstrBkpUp)
                    Dim dsInsModifiedDate As New DataSet
                    daInsModifiedData.Fill(dsInsModifiedDate)
                    dsInsModifiedDate.AcceptChanges()
                    'End If


                    sError = ""
                    nTransferCompleted = 1


                    Dim daInsError As New SqlDataAdapter("Insert Into ErrorTrace Values ('" & sTableName & "','" & sError & "','" & nRecords & _
                                                         "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','" & nTransferCompleted & "')", sConstrBkpUp)
                    Dim dsInsError As New DataSet
                    daInsError.Fill(dsInsError)
                    dsInsError.AcceptChanges()

                    nTransferCompleted = 0
AA:
                Catch ex As Exception

                    sIncompleteTable = sIncompleteTable + " - " + sTableName
                    nErrorcount = nErrorcount + 1

                    'HandleException(Me.Name, ex)
                    sError = ex.Message
                    sError = Replace(sError, "'", "")

                    CompleteDeleteAndAdd(sTableName)
                    'InsertTrace()



                End Try
            Next
        MsgBox("Successfull Completed")
        Catch Exp As Exception
            sTableName = sTableName


            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Dim sError As String
    Dim nRecords As Integer
    Private Sub CompleteDeleteAndAdd(ByVal sTableName1 As String)
        Try

        
            Dim daDelModifiedData As New SqlDataAdapter("Delete from " & sTableName1 & "", sConstrBkpUp)
            Dim dsDelModifiedDate As New DataSet
            daDelModifiedData.Fill(dsDelModifiedDate)
            dsDelModifiedDate.AcceptChanges()

            Dim daInsNewData As New SqlDataAdapter("Insert Into " & sTableName1 & " Select * from AHServer.AHGroup.dbo." & sTableName1 & _
                                                               "", sConstrBkpUp)
            Dim dsInsNewData As New DataSet
            daInsNewData.Fill(dsInsNewData)
            dsInsNewData.AcceptChanges()


            sError = ""
            nTransferCompleted = 1


            Dim daInsError As New SqlDataAdapter("Insert Into ErrorTrace Values ('" & sTableName & "','" & sError & "','" & nRecords & _
                                                 "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','" & nTransferCompleted & "')", sConstrBkpUp)
            Dim dsInsError As New DataSet
            daInsError.Fill(dsInsError)
            dsInsError.AcceptChanges()

            nTransferCompleted = 0

        Catch ex As Exception
            sError = ex.Message
            sError = Replace(sError, "'", "")

            InsertTrace()
        End Try
    End Sub


    Private Sub InsertTrace()
        Try


            Dim daselRecords As New SqlDataAdapter("Select * from " & sTableName & "", sConstr)
            Dim dsSelRecords As New DataSet
            daselRecords.Fill(dsSelRecords)

            nRecords = dsSelRecords.Tables(0).Rows.Count

            Dim daInsError As New SqlDataAdapter("Insert Into ErrorTrace Values ('" & sTableName & "','" & sError & "','" & nRecords & _
                                                 "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','" & nTransferCompleted & "')", sConstrBkpUp)

            Dim dsInsError As New DataSet
            daInsError.Fill(dsInsError)
            dsInsError.AcceptChanges()
        Catch ex As Exception

            InsertBigTableData()
            'sError = ex.Message
            'sError = Replace(sError, "'", "")

            'nRecords = 0

            'Dim daInsError As New SqlDataAdapter("Insert Into ErrorTrace Values ('" & sTableName & "','" & sError & "','" & nRecords & "')", sConstrBkpUp)
            'Dim dsInsError As New DataSet
            'daInsError.Fill(dsInsError)
            'dsInsError.AcceptChanges()
        End Try

    End Sub

    Dim dLastModifiedDate As Date
    Private Sub InsertBigTableData()
        Try
            Dim daLastData As New SqlDataAdapter("Select Top 1 ModifiedDate from " & sTableName & " Order by Modifieddate Desc", sConstrBkpUp)
            Dim dsLastData As New DataSet
            daLastData.Fill(dsLastData)

            dLastModifiedDate = Format(dsLastData.Tables(0).Rows(0).Item(0), "dd-MMM-yyyy hh:mm:ss")

            'Dim daDelModifiedData As New SqlDataAdapter("Delete from " & sTableName & " Where ID in (Select ID From AHServer.AHGroup.dbo." & sTableName & _
            '                                               " Where ModifiedDate > '" & Format(dLastModifiedDate, "dd-MMM-yyyy hh:mm:ss") & "')", sConstrBkpUp)
            'Dim dsDelModifiedDate As New DataSet
            'daDelModifiedData.Fill(dsDelModifiedDate)
            'dsDelModifiedDate.AcceptChanges()

            Dim daDelMSModifiedData As New SqlDataAdapter("Select ID From " & sTableName & _
                                                         " Where ModifiedDate > '" & Format(dLastModifiedDate, "dd-MMM-yyyy hh:mm:ss") & "'", sConstr)
            Dim dsDelMSModifiedDate As New DataSet
            daDelMSModifiedData.Fill(dsDelMSModifiedDate)
            dsDelMSModifiedDate.AcceptChanges()

            Dim i As Integer = 0

            For i = 0 To dsDelMSModifiedDate.Tables(0).Rows.Count - 1
                Dim sId As String = dsDelMSModifiedDate.Tables(0).Rows(i).Item("ID")

                Dim daDelModifiedData As New SqlDataAdapter("Delete from " & sTableName & " Where ID = '" & sId & "'", sConstrBkpUp)
                Dim dsDelModifiedDate As New DataSet
                daDelModifiedData.Fill(dsDelModifiedDate)
                dsDelModifiedDate.AcceptChanges()

                Dim daInsModifiedData As New SqlDataAdapter("Insert Into " & sTableName & " Select * From AHServer.AHGroup.dbo." & sTableName & _
                                                            " Where ID > '" & sId & "'", sConstrBkpUp)
                Dim dsInsModifiedDate As New DataSet
                daInsModifiedData.Fill(dsInsModifiedDate)
                dsInsModifiedDate.AcceptChanges()
            Next


            'Dim daInsModifiedData As New SqlDataAdapter("Insert Into " & sTableName & " Select * From AHServer.AHGroup.dbo." & sTableName & _
            '                                            " Where ModifiedDate > '" & Format(dLastModifiedDate.Date, "dd-MMM-yyyy hh:mm:ss") & "'", sConstrBkpUp)
            'Dim dsInsModifiedDate As New DataSet
            'daInsModifiedData.Fill(dsInsModifiedDate)
            'dsInsModifiedDate.AcceptChanges()

        

        Catch Exp As Exception
            sError = Exp.Message
            sError = Replace(sError, "'", "")

            nRecords = 0

            Dim daInsError As New SqlDataAdapter("Insert Into ErrorTrace Values ('" & sTableName & "','" & sError & "','" & nRecords & _
                                                 "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','" & nTransferCompleted & "')", sConstrBkpUp)
            Dim dsInsError As New DataSet
            daInsError.Fill(dsInsError)
            dsInsError.AcceptChanges()
        End Try
    End Sub

    Private Sub UpdateShippedInfo()


        dToday = "2017-02-17" ''Format(Date.Now, "dd-MMM-yyyy")

        'Dim daSelInvDetails As New SqlDataAdapter("Select * from InvoiceDetail Where InvoiceNo in (Select InvoiceNo From Invoice Where Shipped = '1' And ShippedDate = '" & Format(dToday.Date, "dd-MMM-yyyy") & "') Order by InvoiceNo,CustWorkOrderNo", sConstr)
        Dim daSelInvDetails As New SqlDataAdapter("Select * from InvoiceDetail Where InvoiceNo in (Select InvoiceNo From Invoice Where Shipped = '1' And CustWorkOrderno = 'S-F-AW15-051-06-001') Order by InvoiceNo,CustWorkOrderNo", sConstr)
        Dim dsSelInvDetails As New DataSet
        daSelInvDetails.Fill(dsSelInvDetails)

        Dim i As Integer = 0

        Dim sSalesOrderNo, sBuyerOrderNo, sSalesOrderDetailsId, sJobcardId As String
        Dim nInvoiceQty, nInvBalQty, nJobcardQty, nJCShippedQty, nJCShippedBalQty As Integer
        For i = 0 To dsSelInvDetails.Tables(0).Rows.Count - 1
            sSalesOrderNo = dsSelInvDetails.Tables(0).Rows(i).Item("CustWorkOrderNo")
            nInvoiceQty = dsSelInvDetails.Tables(0).Rows(i).Item("Quantity")
            nInvBalQty = dsSelInvDetails.Tables(0).Rows(i).Item("Quantity")
            sBuyerOrderNo = dsSelInvDetails.Tables(0).Rows(i).Item("BuyerOrderNo")
            sSalesOrderDetailsId = dsSelInvDetails.Tables(0).Rows(i).Item("SalesOrderDetailId")

            Dim daSelJobcard As New SqlDataAdapter("Select * from JobcardDetail where Custworkorderno = '" & sSalesOrderNo & _
                                                   "' And ComponentGroup = 'UPPER' Order by JobcardNo", sConstr)
            Dim dsSelJobcard As New DataSet
            daSelJobcard.Fill(dsSelJobcard)

            Dim j As Integer = 0


            For j = 0 To dsSelJobcard.Tables(0).Rows.Count - 1

                sJobcardId = dsSelJobcard.Tables(0).Rows(j).Item("ID")
                nJobcardQty = Val(dsSelJobcard.Tables(0).Rows(j).Item("Quantity").ToString)
                nJCShippedQty = Val(dsSelJobcard.Tables(0).Rows(j).Item("Shipped").ToString)

                nJCShippedBalQty = nJobcardQty - nJCShippedQty

                If nInvBalQty > 0 Then
                    If nJCShippedBalQty > 0 Then
                        If nJCShippedBalQty >= nInvoiceQty Then
                            nJCShippedQty = nJCShippedQty + nInvoiceQty
                            nInvBalQty = nInvBalQty - nInvoiceQty

                            Dim daUpdJobcard As New SqlDataAdapter("Update JobcardDetail Set Shipped = '" & nJCShippedQty & _
                                                                   "' Where ID = '" & sJobcardId & "'", sConstr)
                            Dim dsUpdJobcard As New DataSet
                            daUpdJobcard.Fill(dsUpdJobcard)
                            dsUpdJobcard.AcceptChanges()
                        Else
                            If nInvBalQty > 0 Then
                                If nInvoiceQty > nJCShippedBalQty Then
                                    nJCShippedQty = nJCShippedQty + nJCShippedBalQty
                                    nInvBalQty = nInvBalQty - nJCShippedBalQty
                                    Dim daUpdJobcard As New SqlDataAdapter("Update JobcardDetail Set Shipped = '" & nJCShippedQty & _
                                                                      "' Where ID = '" & sJobcardId & "'", sConstr)
                                    Dim dsUpdJobcard As New DataSet
                                    daUpdJobcard.Fill(dsUpdJobcard)
                                    dsUpdJobcard.AcceptChanges()
                                End If
                            End If
                        End If
                    End If
                End If
                'If nJobcardQty Then
            Next
        Next
        MsgBox("Completed")
    End Sub

    Dim sId, sSize As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim daSelPacStage As New SqlDataAdapter("Select * from ProductStock Where Size = '' And Stage = 'PAC' Order by WorkOrderNo", sConstr)
        Dim dsSelPacStage As New DataSet
        daSelPacStage.Fill(dsSelPacStage)

        Dim i As Integer = 0

        For i = 0 To dsSelPacStage.Tables(0).Rows.Count - 1
            sId = dsSelPacStage.Tables(0).Rows(i).Item("ID")

            Dim daSelAuditData As New SqlDataAdapter("Select Size from ProductStock where Id = '" & sId & _
                                                     "' Order by Modifieddate", sConstrBkpUp)
            Dim dsSelAuditData As New DataSet
            daSelAuditData.Fill(dsSelAuditData)

            If Microsoft.VisualBasic.Len(dsSelAuditData.Tables(0).Rows(0).Item("Size")) = 1 Then
                sSize = "0" + dsSelAuditData.Tables(0).Rows(0).Item("Size")
            Else
                sSize = dsSelAuditData.Tables(0).Rows(0).Item("Size")
            End If


            Dim daUpdDPacStage As New SqlDataAdapter("Update ProductStock Set Size = '" & sSize & _
                                                     "' Where Id = '" & sId & "'", sConstr)
            Dim dsUpdPacStage As New DataSet
            daUpdDPacStage.Fill(dsUpdPacStage)
            dsUpdPacStage.AcceptChanges()

        Next

        MsgBox("Completed")
    End Sub
End Class