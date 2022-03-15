Option Explicit On

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing
Imports Microsoft.Win32
Imports System.Data

Public Class frmScanning
    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccMCD As New ccMCD
    Dim myccOptimizerComponentUser As New ccOptimizerComponent
    Dim myOptimizerStrUserAuthentication As New StrUserAuthentication

    Dim sSpoolCode, sBarcode, sJobcardNo, sSalesOrderNo As String
    Dim nYesNo, nRowCount, keyascii, nStringLength, nWithSize, nBoxNoLength, nColonLocation, nBoxNo, nSize, nBoxQuantity As Integer
    Dim nSavingQuantity, nSizeCount, nIncludeCount As Integer
    Dim FinYear As String = Nothing

#Region "BARCODE SCANNING"

    Private Sub tbBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbBarcode.KeyPress

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then

            nStringLength = Microsoft.VisualBasic.Len(Trim(tbBarcode.Text))
            If nStringLength < 5 Then
                GoTo Aa
            End If

            sBarcode = Trim(tbBarcode.Text)
            Dim dsSelBoxInfo As New DataSet

            If sBarcode.Contains(":") = True Then
                nWithSize = 1

                sJobcardNo = Microsoft.VisualBasic.Left(sBarcode, 13)
                sSalesOrderNo = Microsoft.VisualBasic.Left(sBarcode, 9)
                nBoxNoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
                nColonLocation = sBarcode.IndexOf(":")
                If nColonLocation = 15 Then
                    nBoxNo = Microsoft.VisualBasic.Mid(sBarcode, 15, 1)
                ElseIf nColonLocation = 16 Then
                    nBoxNo = Microsoft.VisualBasic.Mid(sBarcode, 15, 2)
                ElseIf nColonLocation = 17 Then
                    nBoxNo = Microsoft.VisualBasic.Mid(sBarcode, 15, 3)
                End If

                Dim daSelSpoolDtl As New SqlDataAdapter("Select * from  SpoolDetails Where  SpoolHID = '" & sSpoolId & _
                                                      "' And Barcode = '" & Trim(Microsoft.VisualBasic.Left(sBarcode, nColonLocation)) & "'", sConstr)
                Dim dsSelSpoolDtl As New DataSet
                daSelSpoolDtl.Fill(dsSelSpoolDtl)

                If dsSelSpoolDtl.Tables(0).Rows.Count > 0 Then
                    MsgBox("This Box Already Scanned With Size Info", MsgBoxStyle.Information)
                    Exit Sub
                End If

                nSize = Val(Microsoft.VisualBasic.Right(sBarcode, (nStringLength - nColonLocation) - 1))
                tbSize.Text = nSize.ToString

                Dim daSelBoxInfo As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                                       "' And CartonNo = '" & nBoxNo & "'", sConstr)
                daSelBoxInfo.Fill(dsSelBoxInfo)

                If dsSelBoxInfo.Tables(0).Rows.Count = 0 Then
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    tbLastBarcode.Text = Trim(tbBarcode.Text)
                    tbBarcode.Text = ""
                    Exit Sub
                End If

                If dsSelBoxInfo.Tables(0).Rows.Count = 1 Then
                    If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString) = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString) Then

                    End If
                    nBoxQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                    nSavingQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                Else
                    Exit Sub
                End If

                Dim daSelBoxInfoWSize As New SqlDataAdapter("Select * from vw_SolarPackingSingleSizeLabel Where JobcardNo = '" & sJobcardNo & _
                                                                "' And CartonNo = '" & nBoxNo & "'", sConstr)
                Dim dsSelBoxInfoWSize As New DataSet
                daSelBoxInfoWSize.Fill(dsSelBoxInfoWSize)

                nSizeCount = dsSelBoxInfoWSize.Tables(0).Rows.Count

                'If nSizeCount = 1 Then
                '    tbSize.Text = dsSelBoxInfoWSize.Tables(0).Rows(0).Item("Size").ToString
                '    nSize = Val(dsSelBoxInfoWSize.Tables(0).Rows(0).Item("Size").ToString)
                'Else
                '    tbSize.Text = "MULTI SIZE"
                '    nSize = 0
                'End If

                Dim i As Integer = 0

                For i = 0 To dsSelBoxInfoWSize.Tables(0).Rows.Count - 1
                    If Val(dsSelBoxInfoWSize.Tables(0).Rows(i).Item("Size").ToString) = nSize Then
                        nBoxQuantity = Val(dsSelBoxInfoWSize.Tables(0).Rows(i).Item("Qty").ToString)
                        nSavingQuantity = Val(dsSelBoxInfoWSize.Tables(0).Rows(i).Item("Qty").ToString)
                        Exit For
                    End If
                Next


            Else
                nWithSize = 0
                sJobcardNo = Microsoft.VisualBasic.Left(sBarcode, 13)
                sSalesOrderNo = Microsoft.VisualBasic.Left(sBarcode, 9)
                nBoxNoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
                nBoxNo = Microsoft.VisualBasic.Right(tbBarcode.Text, nBoxNoLength - 1)

                Dim daSelBoxInfo As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                                       "' And CartonNo = '" & nBoxNo & "'", sConstr)
                daSelBoxInfo.Fill(dsSelBoxInfo)

                If dsSelBoxInfo.Tables(0).Rows.Count = 0 Then
Aa:
                    MsgBox("Invalid Barcode", MsgBoxStyle.Critical)
                    tbLastBarcode.Text = Trim(tbBarcode.Text)
                    tbBarcode.Text = ""
                    Exit Sub
                End If

                If dsSelBoxInfo.Tables(0).Rows.Count = 1 Then
                    If chkbxManualSave.Checked = False Then
                        If mdlSGM.sSelectedSection = "MOULDING" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldQuantity").ToString) > 0 Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        ElseIf mdlSGM.sSelectedSection = "FINISHING" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("FinishedQuantity").ToString) > 0 Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        ElseIf mdlSGM.sSelectedSection = "PACKING" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("PackedQuantity").ToString) > 0 Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        ElseIf mdlSGM.sSelectedSection = "DISPATCH" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("InvoicedQuantity").ToString) > 0 Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        End If

                        nBoxQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                        nSavingQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                    Else
                        If mdlSGM.sSelectedSection = "MOULDING" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldQuantity").ToString) >= Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString) Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                            nBoxQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldQuantity").ToString) - Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                            nSavingQuantity = nBoxQuantity
                        ElseIf mdlSGM.sSelectedSection = "FINISHING" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("FinishedQuantity").ToString) >= Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString) Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                            nBoxQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldQuantity").ToString) - Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                            nSavingQuantity = nBoxQuantity

                        ElseIf mdlSGM.sSelectedSection = "PACKING" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("PackedQuantity").ToString) >= Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString) Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                            nBoxQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldQuantity").ToString) - Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                            nSavingQuantity = nBoxQuantity

                        ElseIf mdlSGM.sSelectedSection = "DISPATCH" Then
                            If Val(dsSelBoxInfo.Tables(0).Rows(0).Item("InvoicedQuantity").ToString) >= Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString) Then
                                MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                            nBoxQuantity = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldQuantity").ToString) - Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                            nSavingQuantity = nBoxQuantity

                        End If
                    End If


                Else
                    Exit Sub
                End If

                Dim daSelBoxInfoWSize As New SqlDataAdapter("Select * from vw_SolarPackingSingleSizeLabel Where JobcardNo = '" & sJobcardNo & _
                                                                "' And CartonNo = '" & nBoxNo & "'", sConstr)
                Dim dsSelBoxInfoWSize As New DataSet
                daSelBoxInfoWSize.Fill(dsSelBoxInfoWSize)

                nSizeCount = dsSelBoxInfoWSize.Tables(0).Rows.Count

                If nSizeCount = 1 Then
                    tbSize.Text = dsSelBoxInfoWSize.Tables(0).Rows(0).Item("Size").ToString
                    nSize = Val(dsSelBoxInfoWSize.Tables(0).Rows(0).Item("Size").ToString)
                Else
                    tbSize.Text = "MULTI SIZE"
                    nSize = 0

                    If chkbxManualSave.Checked = True Then
                        MsgBox("Manual Change Quantity for Multiple Size Boxes without Size is not allowed.", MsgBoxStyle.Critical)
                        cbManualSave.Enabled = False
                        Exit Sub
                    End If
                End If
            End If


            If chkbxNoBoxAdd.Checked = True Then
                nIncludeCount = 0
            Else
                nIncludeCount = 1
            End If



            If nWithSize = 0 Then
                Dim daSelSpoolDtl As New SqlDataAdapter("Select * from  SpoolDetails Where  SpoolHID = '" & sSpoolId & _
                                                       "' And Barcode Like '" & Trim(tbBarcode.Text) & "' + '%' ", sConstr)
                Dim dsSelSpoolDtl As New DataSet
                daSelSpoolDtl.Fill(dsSelSpoolDtl)

                If dsSelSpoolDtl.Tables(0).Rows.Count > 0 Then
                    MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                    Exit Sub
                End If
            Else
                Dim daSelSpoolDtl As New SqlDataAdapter("Select * from  SpoolDetails Where  SpoolHID = '" & sSpoolId & _
                                                       "' And Barcode = '" & Trim(tbBarcode.Text) & "'", sConstr)
                Dim dsSelSpoolDtl As New DataSet
                daSelSpoolDtl.Fill(dsSelSpoolDtl)

                If dsSelSpoolDtl.Tables(0).Rows.Count > 0 Then
                    MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If

            If chkbxManualSave.Checked = False Then



                Dim daInsSpoolDtl As New SqlDataAdapter("Insert Into SpoolDetails(ID,CreatedBy,CreatedDate,ModuleName,SpoolHID,BarCode,WithSize,Size,BoxQuantity,SavingQuantity,IsUpdated,IncludeCount,IsManualSave,SizeCount) Values ('" & System.Guid.NewGuid.ToString() & _
                                                        "','" & mdlSGM.sUserName & "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','MCD','" & sSpoolId & _
                                                        "','" & Trim(tbBarcode.Text) & "','" & nWithSize & "','" & nSize & "','" & nBoxQuantity & _
                                                        "','" & nSavingQuantity & "','0','" & nIncludeCount & "','0','" & nSizeCount & "')", sConstr)
                Dim dsInsSpoolDtl As New DataSet
                daInsSpoolDtl.Fill(dsInsSpoolDtl)
                dsInsSpoolDtl.AcceptChanges()

                tbLastBarcode.Text = tbBarcode.Text
                tbQuantity.Text = nBoxQuantity
                tbBarcode.Text = ""
                tbBarcode.Focus()
            Else

                tbQuantity.Text = nBoxQuantity
                tbQuantity.Focus()
            End If


        End If
    End Sub

    Private Sub chkbxManualSave_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxManualSave.CheckStateChanged
        If chkbxManualSave.Checked = True Then
            cbManualSave.Enabled = True
        Else
            cbManualSave.Enabled = False
        End If
    End Sub

    Private Sub cbManualSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbManualSave.Click
        Dim daSelSpoolDtl As New SqlDataAdapter("Select * from  SpoolDetails Where  SpoolHID = '" & sSpoolId & _
                                                        "' And Barcode = '" & Trim(tbBarcode.Text) & "'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)

        If dsSelSpoolDtl.Tables(0).Rows.Count > 0 Then
            MsgBox("This Box Already Scanned", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim daSelBoxInfoWSize As New SqlDataAdapter("Select * from vw_SolarPackingSingleSizeLabel Where JobcardNo = '" & sJobcardNo & _
                                                           "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelBoxInfoWSize As New DataSet
        daSelBoxInfoWSize.Fill(dsSelBoxInfoWSize)

        nSizeCount = dsSelBoxInfoWSize.Tables(0).Rows.Count

        Dim daInsSpoolDtl As New SqlDataAdapter("Insert Into SpoolDetails(ID,CreatedBy,CreatedDate,ModuleName,SpoolHID,BarCode,WithSize,Size,BoxQuantity,SavingQuantity,IsUpdated,IncludeCount,IsManualSave,Sizecount) Values ('" & System.Guid.NewGuid.ToString() & _
                                                "','" & mdlSGM.sUserName & "','" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & "','MCD','" & sSpoolId & _
                                                "','" & Trim(tbBarcode.Text) & "','" & nWithSize & "','" & nSize & "','" & nBoxQuantity & _
                                                "','" & Val(tbQuantity.Text) & "','0','" & nIncludeCount & "','1','" & nSizeCount & "')", sConstr)
        Dim dsInsSpoolDtl As New DataSet
        daInsSpoolDtl.Fill(dsInsSpoolDtl)
        dsInsSpoolDtl.AcceptChanges()

        tbLastBarcode.Text = tbBarcode.Text
        tbQuantity.Text = nBoxQuantity
        tbBarcode.Text = ""
        tbBarcode.Focus()
    End Sub


    Private Sub cbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSaveBarcode.Click
        Dim daSelSpoolHdr As New SqlDataAdapter("Select * from Spool Where Department = '" & mdlSGM.sSelectedSection & _
                                                        "' And Shift = '" & mdlSGM.sShiftCode & _
                                                        "' And UserName = '" & mdlSGM.sUserName & _
                                                        "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolHdr As New DataSet
        daSelSpoolHdr.Fill(dsSelSpoolHdr)

        If dsSelSpoolHdr.Tables(0).Rows.Count <> 1 Then
            MsgBox("Multiple Spool Open Simultaneously", MsgBoxStyle.Critical)
            Exit Sub
        ElseIf dsSelSpoolHdr.Tables(0).Rows.Count = 0 Then
            MsgBox("No Spool Available for further process", MsgBoxStyle.Critical)
            Exit Sub
        ElseIf dsSelSpoolHdr.Tables(0).Rows.Count = 1 Then
            sSpoolId = dsSelSpoolHdr.Tables(0).Rows(0).Item("SpoolId").ToString

            Dim daSelSpoolDtlsSummary As New SqlDataAdapter("Select IsNull(Sum(IncludeCount),0) As BoxCount,IsNull(Sum(SavingQuantity),0) As Quantity From SpoolDetails Where SpoolHID = '" & sSpoolId & "'", sConstr)
            Dim dsSelSpoolDtlsSummary As New DataSet
            daSelSpoolDtlsSummary.Fill(dsSelSpoolDtlsSummary)


            If Val(dsSelSpoolDtlsSummary.Tables(0).Rows(0).Item("BoxCount").ToString) = 0 Then
                MsgBox("Boxes Not Scanned", MsgBoxStyle.Information)
                Exit Sub
            End If
            frmSave.tbTotalCartons.Text = Val(dsSelSpoolDtlsSummary.Tables(0).Rows(0).Item("BoxCount").ToString)
            frmSave.tbTotalQty.Text = Val(dsSelSpoolDtlsSummary.Tables(0).Rows(0).Item("Quantity").ToString)


            frmSave.lbScannedBoxes.DataSource = myccMCD.LoadScannedBoxes(sSpoolId)
            frmSave.lbScannedBoxes.DisplayMember = "ScannedBoxes"
            frmSave.lbScannedBoxes.ValueMember = "ScannedBoxes"
            frmSave.plSave.Visible = True
            frmSave.plSave.BringToFront()
        End If

        frmSave.Show()
        Me.Hide()

    End Sub

    Private Sub cbBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBack.Click
        Dim daSelSpoolDtl As New SqlDataAdapter("Select * from SpoolDetails Where SpoolHID = '" & sSpoolId & _
                                                "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)

        If dsSelSpoolDtl.Tables(0).Rows.Count = 0 Then
            sSection = ""
            DeleteSpoolHdr()
        End If
        lblScanning.Text = "BARCODE SCANNING : "
        frmSelection.Show()
        Me.Hide()
        
    End Sub


    Private Sub cbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCancel.Click
        Dim daSelSpoolDtl As New SqlDataAdapter("Select * from SpoolDetails Where SpoolHID = '" & sSpoolId & _
                                                "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)

        If dsSelSpoolDtl.Tables(0).Rows.Count = 0 Then
            DeleteSpoolHdr()
            sSection = ""
        Else
            nYesNo = MsgBox("R U Sure U Want to cancel all the scanned boxes.", MsgBoxStyle.YesNo)

            If nYesNo = 6 Then
                DeleteSpoolDtl()
                DeleteSpoolHdr()
                sSection = ""
            End If
        End If

        lblScanning.Text = "BARCODE SCANNING : "

        frmSelection.Show()
        Me.Hide()
    End Sub

    Private Sub cbExitBarcodeScanning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExitBarcodeScanning.Click
        Dim daSelSpoolDtl As New SqlDataAdapter("Select * from SpoolDetails Where SpoolHID = '" & sSpoolId & _
                                                "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)

        If dsSelSpoolDtl.Tables(0).Rows.Count <> 0 Then
            nYesNo = MsgBox("R U Sure U Want to Exit Without saving the scanned boxes.", MsgBoxStyle.YesNo)

            If nYesNo = 6 Then
                DeleteSpoolDtl()
                DeleteSpoolHdr()
            Else
                Exit Sub
            End If
        End If

        myOptimizerStrUserAuthentication.sUserName = mdlSGM.sUserName
        myOptimizerStrUserAuthentication.sLogoutTime = Format(Date.Now, "dd-MMM-yyyy::hh:mm:ss-tt")
        myOptimizerStrUserAuthentication.sIsActive = "0"
        If myccOptimizerComponentUser.UpdateUserAuthentication(myOptimizerStrUserAuthentication) = True Then
            Me.DialogResult = DialogResult.OK
        End If
        Application.Exit()

    End Sub

    Public Function GetFinancialYear(ByVal curDate As DateTime) As String
        Dim CurrentYear As Integer = Val(Microsoft.VisualBasic.Right(curDate.Year.ToString, 2))
        Dim PreviousYear As Integer = CurrentYear - 1 'curDate.Year - 1
        Dim NextYear As Integer = CurrentYear + 1 'curDate.Year + 1
        Dim PreYear As String = PreviousYear.ToString()
        Dim NexYear As String = NextYear.ToString()
        Dim CurYear As String = CurrentYear.ToString()

        If (curDate.Month > 3) Then
            FinYear = CurYear.ToString + NexYear.ToString
        Else
            FinYear = PreYear.ToString + CurYear.ToString
        End If
        Return FinYear
    End Function

    Private Sub DeleteSpoolHdr()
        Dim daSelSpoolHdr As New SqlDataAdapter("Delete from Spool Where SpoolID = '" & sSpoolId & "'", sConstr)
        Dim dsSelSpoolHdr As New DataSet
        daSelSpoolHdr.Fill(dsSelSpoolHdr)
        dsSelSpoolHdr.AcceptChanges()
    End Sub

    Private Sub DeleteSpoolDtl()
        Dim daSelSpoolDtl As New SqlDataAdapter("Delete from SpoolDetails Where SpoolHID = '" & sSpoolId & "'", sConstr)
        Dim dsSelSpoolDtl As New DataSet
        daSelSpoolDtl.Fill(dsSelSpoolDtl)
        dsSelSpoolDtl.AcceptChanges()
    End Sub
#End Region


    
    Private Sub tbBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    
    Private Sub tbBarcode_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBarcode.TextChanged

    End Sub
End Class