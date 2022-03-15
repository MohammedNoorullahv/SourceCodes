Option Explicit On

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Drawing
Imports Microsoft.Win32
Imports System.Data

Public Class frmSave

#Region "SAVE"
    Dim sConstr As String = "Data Source=192.168.25.5;Initial Catalog=AHGroup_SSPL;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccMCD As New ccMCD
    Dim myccOptimizerComponentUser As New ccOptimizerComponent
    Dim myOptimizerStrUserAuthentication As New StrUserAuthentication
    Dim mystrProductionByProcess As New strProductionByProcess
    Dim myccProductionByProcess As New ccProductionByProcess
    Dim mystrReadytoDispatch As New strReadytoDispatch
    Dim myccPackingEntries As New ccPackingEntries

    Dim sSpoolId, sSpoolCode, sBarcode, sJobcardNo, sSalesOrderNo As String
    Dim nYesNo, nRowCount, keyascii, nStringLength, nWithSize, nBoxNoLength, nColonLocation, nBoxNo, nSize, nBoxQuantity As Integer
    Dim nSavingQuantity, nSizeCount, nIncludeCount As Integer
    Dim FinYear As String = Nothing

    Dim sSDID, stbBarcodeText As String
    Private Sub cbSaveBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSaveBack.Click

        Me.Hide()
        frmScanning.Show()
    End Sub

    Dim nCorrectBox, nWrongBox, nTotalBox, nIsManualSave As Integer
    Dim sEntryUpdates As String = ""
    Private Sub cbSaveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSaveAll.Click
        Dim daSelSpoolHdr As New SqlDataAdapter("Select * from Spool Where UserName = '" & mdlSGM.sUserName & _
                                                "' And IsUpdated = '0'", sConstr)
        Dim dsSelSpoolHdr As New DataSet
        daSelSpoolHdr.Fill(dsSelSpoolHdr)

        Dim j As Integer = 0

        nTotalBox = 0 : nCorrectBox = 0 : nWrongBox = 0
        sEntryUpdates = "Without Sizes"
        For j = 0 To dsSelSpoolHdr.Tables(0).Rows.Count - 1
            sSpoolId = dsSelSpoolHdr.Tables(0).Rows(j).Item("SpoolId").ToString
            tbSpoolNo.Text = sSpoolId.ToString.ToUpper

            If dsSelSpoolHdr.Tables(0).Rows(j).Item("Department").ToString = "MOULDING" Then
                sSection = "MOULD"
            ElseIf dsSelSpoolHdr.Tables(0).Rows(j).Item("Department").ToString = "FINISHING" Then
                sSection = "FINISH"
            ElseIf dsSelSpoolHdr.Tables(0).Rows(j).Item("Department").ToString = "PACKING" Then
                sSection = "PACK"
            End If

Aa:
            Dim dsSelSpoolDtls As New DataSet
            If sEntryUpdates = "Without Sizes" Then
                Dim daSelSpoolDtls As New SqlDataAdapter("Select * from SpoolDetails Where SpoolHID = '" & sSpoolId & _
                                                     "' And WithSize = '0' Order by Barcode,Size", sConstr)
                daSelSpoolDtls.Fill(dsSelSpoolDtls)
            Else
                Dim daSelSpoolDtls As New SqlDataAdapter("Select '' As ID, Left(Barcode,17) AS Barcode, '0' As IsManualSave,Sum(SavingQuantity) As SavingQuantity  from SpoolDetails where SpoolHID = '" & sSpoolId & _
                                                         "' And WithSize = '1' Group By Left(Barcode,17) Order by Barcode", sConstr)
                daSelSpoolDtls.Fill(dsSelSpoolDtls)
            End If


            If dsSelSpoolDtls.Tables(0).Rows.Count > 0 Then
                Dim i As Integer = 0

                For i = 0 To dsSelSpoolDtls.Tables(0).Rows.Count - 1
                    sSDID = dsSelSpoolDtls.Tables(0).Rows(i).Item("ID").ToString
                    sBarcode = dsSelSpoolDtls.Tables(0).Rows(i).Item("Barcode").ToString
                    nStringLength = Microsoft.VisualBasic.Len(sBarcode)
                    sJobcardNo = Microsoft.VisualBasic.Left(Trim(sBarcode), 13)
                    sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(sBarcode), 9)
                    nBoxNoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
                    nBoxNo = Microsoft.VisualBasic.Right(sBarcode, nBoxNoLength - 1)
                    nIsManualSave = Val(dsSelSpoolDtls.Tables(0).Rows(i).Item("IsManualSave").ToString) * -1
                    nSavingQuantity = Val(dsSelSpoolDtls.Tables(0).Rows(i).Item("SavingQuantity").ToString)

                    BarcodeSettings()

                    JobcardVerification()

                    If sSection <> "PACK" Then
                        If Microsoft.VisualBasic.Left(sArticle, 10) = "SOL-SYN-EV" Or Microsoft.VisualBasic.Left(sArticle, 10) = "SOL-LEA-EV" Then
                            If sEVAProcess = "" Then
                                sStatus = "Process Not Defined"
                                Exit Sub
                            End If
                            EVAProductionQuantityUpdates()
                        ElseIf Microsoft.VisualBasic.Mid(sArticle, 9, 2) = "RU" Then
                            If sEVAProcess = "" Then
                                sStatus = "Process Not Defined"
                                Exit Sub
                            End If
                            EVAProductionQuantityUpdates()
                        Else
                            ProductionQuantityUpdates()
                        End If

                        If sStatus = "Successfully Updated" Then
                            nCorrectBox = nCorrectBox + 1
                        Else
                            nWrongBox = nWrongBox + 1
                        End If

                        nTotalBox = nTotalBox + 1

                    Else
                        UpdateReadyToDispatch()

                        If sStatus = "Successfully Updated" Then
                            nCorrectBox = nCorrectBox + 1
                        Else
                            nWrongBox = nWrongBox + 1
                        End If

                        nTotalBox = nTotalBox + 1
                    End If
                    Dim daSelStatus As New SqlDataAdapter("Select * from AbbrevTable where Group_= 'scanstatus' And FullName_ = '" & sStatus & "'", sConstr)
                    Dim dsSelStatus As New DataSet
                    daSelStatus.Fill(dsSelStatus)

                    If dsSelStatus.Tables(0).Rows.Count = 1 Then
                        nFKStatus = Val(dsSelStatus.Tables(0).Rows(0).Item("Abbrev_").ToString)
                    Else
                        MsgBox(sStatus)
                        nFKStatus = 99
                    End If
                    Dim dsUpdSpoolDtls As New DataSet

                    If sEntryUpdates = "Without Sizes" Then
                        Dim daUpdSpoolDtls As New SqlDataAdapter("Update SpoolDetails Set IsUpdated = '1', UpdatedOn = '" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & _
                                                             "', FKStatus = '" & nFKStatus & "' Where ID = '" & sSDID & "'", sConstr)
                        daUpdSpoolDtls.Fill(dsUpdSpoolDtls)
                        dsUpdSpoolDtls.AcceptChanges()
                    Else
                        Dim daUpdSpoolDtls As New SqlDataAdapter("Update SpoolDetails Set IsUpdated = '1', UpdatedOn = '" & Format(Date.Now, "dd-MMM-yyyy hh:mm:ss") & _
                                                                 "', FKStatus = '" & nFKStatus & "' Where  SpoolHID = '" & sSpoolId & _
                                                                 "' And Barcode Like '" & sBarcode & "' + '%'", sConstr)
                        daUpdSpoolDtls.Fill(dsUpdSpoolDtls)
                        dsUpdSpoolDtls.AcceptChanges()
                    End If

                Next
                If sEntryUpdates = "Without Sizes" Then
                    sEntryUpdates = "With Sizes"
                    GoTo Aa
                End If
            End If


            sSpoolId = dsSelSpoolHdr.Tables(0).Rows(j).Item("SpoolId").ToString

            Dim daUpdSpoolHdr As New SqlDataAdapter("Update Spool Set BoxCount = '" & Val(tbTotalCartons.Text) & _
                                                    "', Quantity = '" & Val(tbTotalQty.Text) & _
                                                    "', IsUPdated = '1' Where SpoolId = '" & sSpoolId & "'", sConstr)
            Dim dsUpdSpoolHdr As New DataSet
            daUpdSpoolHdr.Fill(dsUpdSpoolHdr)
            dsUpdSpoolHdr.AcceptChanges()
        Next

        tbTotalCartons.Text = ""
        tbTotalQty.Text = ""

        If nWrongBox > 0 Then
            plSave.Visible = False
            plwrongBoxStatus.Visible = True
            plwrongBoxStatus.BringToFront()

            tbTotalCartonScanned.Text = nTotalBox
            tbCorrectBox.Text = nCorrectBox
            tbWrongBox.Text = nWrongBox

            lbWrongBox.DataSource = myccMCD.LoadWrongScannedBoxes(sSpoolId)
            lbWrongBox.DisplayMember = "ScannedBoxes"
            lbWrongBox.ValueMember = "ScannedBoxes"
        Else
            frmSelection.Show()
            Me.Hide()
            sSection = ""
        End If

        
    End Sub

    Dim nFKStatus As Integer

    Private Sub cbExitSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExitSave.Click
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

    Private Sub BarcodeSettings()

        nStringLength = Microsoft.VisualBasic.Len(sBarcode)
        sBeginsWith = Microsoft.VisualBasic.Left(sBarcode, 1)

        Dim daSelBarcodeSetting As New SqlDataAdapter("Select * from BarcodeSettings Where StringLength = '" & nStringLength & _
                                                      "' And BeginsWith like  '%' + '" & sBeginsWith & "' + '%'", sConstr)
        Dim dsSelBarcodeSetting As New DataSet
        daSelBarcodeSetting.Fill(dsSelBarcodeSetting)

        If dsSelBarcodeSetting.Tables(0).Rows.Count <> 1 Then
            MsgBox("Invalid Jobcard", MsgBoxStyle.Critical)
        Else
            sDescription = dsSelBarcodeSetting.Tables(0).Rows(0).Item("Description")
        End If
    End Sub

    Dim sEVAProcess, sBeginsWith, sDescription, sStatus, sArticle, sBuyerCode, sBuyer, sJobcardDetailID As String
    Dim nJobcardQuantity As Integer
    Private Sub JobcardVerification()

        sJobcardNo = Microsoft.VisualBasic.Left(sBarcode, 13)
        sSalesOrderNo = Microsoft.VisualBasic.Left(sBarcode, 9)

        Dim daSelJobCardInfo As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo = '" & sJobcardNo & "'", sConstr)
        Dim dsSelJobcardInfo As New DataSet
        daSelJobCardInfo.Fill(dsSelJobcardInfo)

        If dsSelJobcardInfo.Tables(0).Rows.Count <= 0 Then
            MsgBox("Invalid Jobcard No.")
            sStatus = "Invalid Jobcard No."
        ElseIf dsSelJobcardInfo.Tables(0).Rows.Count > 1 Then
            MsgBox("Invalid Jobcard No. Multiple Jobcards Existing")
            sStatus = "Invalid Jobcard No. Multiple Jobcards Existing"
            'stbBarcodeText = ""
            'tbBarcode.Focus()
        Else
            nJobcardQuantity = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("Quantity").ToString)
            sArticle = dsSelJobcardInfo.Tables(0).Rows(0).Item("Article").ToString
            sBuyerCode = dsSelJobcardInfo.Tables(0).Rows(0).Item("BuyerCode").ToString
            sBuyer = dsSelJobcardInfo.Tables(0).Rows(0).Item("BuyerGroupCode").ToString

            Dim daSelBuyer As New SqlDataAdapter("Select * from Buyer where BuyerCode = '" & sBuyerCode & "'", sConstr)
            Dim dsSelBuyer As New DataSet
            daSelBuyer.Fill(dsSelBuyer)

            sBuyer = dsSelBuyer.Tables(0).Rows(0).Item("BuyerName").ToString


            sJobcardDetailID = dsSelJobcardInfo.Tables(0).Rows(0).Item("ID").ToString

            Dim daSelJCWIP As New SqlDataAdapter("Select * from jobcardwip Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelJCWIP As New DataSet
            daSelJCWIP.Fill(dsSelJCWIP)

            If dsSelJCWIP.Tables(0).Rows.Count = 0 Then
                'MsgBox("WIP NOT PLANNED", MsgBoxStyle.Critical)
                sStatus = "WIP NOT PLANNED"
            Else
                sEVAProcess = dsSelJCWIP.Tables(0).Rows(0).Item("Process").ToString
            End If

            If sSection = "PACK" Then

                sShipper = dsSelJobcardInfo.Tables(0).Rows(0).Item("Shipper").ToString

                sSize01 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size01").ToString
                sSize02 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size02").ToString
                sSize03 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size03").ToString
                sSize04 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size04").ToString
                sSize05 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size05").ToString
                sSize06 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size06").ToString
                sSize07 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size07").ToString
                sSize08 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size08").ToString
                sSize09 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size09").ToString
                sSize10 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size10").ToString
                sSize11 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size11").ToString
                sSize12 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size12").ToString
                sSize13 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size13").ToString
                sSize14 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size14").ToString
                sSize15 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size15").ToString
                sSize16 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size16").ToString
                sSize17 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size17").ToString
                sSize18 = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size18").ToString

                'Dim daSelBuyer As New SqlDataAdapter("Select * from Buyer where BuyerCode = '" & sBuyerCode & "'", sConstr)
                'Dim dsSelBuyer As New DataSet
                'daSelBuyer.Fill(dsSelBuyer)

                'sBuyerGroup = dsSelBuyer.Tables(0).Rows(0).Item("BuyerName").ToString


                sJobcardDetailID = dsSelJobcardInfo.Tables(0).Rows(0).Item("ID").ToString

                sSalesOrderDetailId = dsSelJobcardInfo.Tables(0).Rows(0).Item("SalesOrderDetailID").ToString

            End If
        End If



    End Sub

    Dim sSize1, sSize2, sSize3, sSize4, sSize5, sSize6, sSize7, sSize8, sSize9, sSize10 As String
    Dim sSize01, sSize02, sSize03, sSize04, sSize05, sSize06, sSize07, sSize08, sSize09 As String
    Dim sSize11, sSize12, sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize, sPartQtySize As String

    Dim nQuantity1, nQuantity2, nQuantity3, nQuantity4, nQuantity5, nQuantity6, nQuantity7, nQuantity8, nQuantity9, nQuantity10 As Integer
    Dim nQuantity01, nQuantity02, nQuantity03, nQuantity04, nQuantity05, nQuantity06, nQuantity07, nQuantity08, nQuantity09 As Integer
    Dim nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18, nQuantity, nPackQty, nTotalQuantity As Integer

    Dim nMouldCompletedQty, nFinishCompletedQty, nMouldPendingQty, nLossQuantity As Integer
    Dim sPartQuantity, nFinishPendingQty, sWIPLocation, sFromLocation, sFromStage As String

    Private Sub ProductionQuantityUpdates()
        'asdfasdfdasfdfas()
        nBoxNoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(sBarcode, nBoxNoLength - 1)

        Dim daSelBoxInfo As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                               "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelBoxInfo As New DataSet
        daSelBoxInfo.Fill(dsSelBoxInfo)

        If dsSelBoxInfo.Tables(0).Rows.Count = 0 Then
            sStatus = "Invalid Box No"
            stbBarcodeText = ""
            Exit Sub
        End If

        nPackQty = dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity")

        sSize1 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size01").ToString
        sSize2 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size02").ToString
        sSize3 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size03").ToString
        sSize4 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size04").ToString
        sSize5 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size05").ToString
        sSize6 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size06").ToString
        sSize7 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size07").ToString
        sSize8 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size08").ToString
        sSize9 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size09").ToString
        sSize10 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size10").ToString
        sSize11 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size11").ToString
        sSize12 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size12").ToString
        sSize13 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size13").ToString
        sSize14 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size14").ToString
        sSize15 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size15").ToString
        sSize16 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size16").ToString
        sSize17 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size17").ToString
        sSize18 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size18").ToString

        If sEntryUpdates = "Without Sizes" Then

            nQuantity1 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity01").ToString)
            nQuantity2 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity02").ToString)
            nQuantity3 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity03").ToString)
            nQuantity4 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity04").ToString)
            nQuantity5 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity05").ToString)
            nQuantity6 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity06").ToString)
            nQuantity7 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity07").ToString)
            nQuantity8 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity08").ToString)
            nQuantity9 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity09").ToString)
            nQuantity10 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity10").ToString)
            nQuantity11 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity11").ToString)
            nQuantity12 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity12").ToString)
            nQuantity13 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity13").ToString)
            nQuantity14 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity14").ToString)
            nQuantity15 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity15").ToString)
            nQuantity16 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity16").ToString)
            nQuantity17 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity17").ToString)
            nQuantity18 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity18").ToString)


            If Val(nQuantity1) > 0 Then : nQuantity1 = nSavingQuantity
            ElseIf Val(nQuantity2) > 0 Then : nQuantity2 = nSavingQuantity
            ElseIf Val(nQuantity3) > 0 Then : nQuantity3 = nSavingQuantity
            ElseIf Val(nQuantity4) > 0 Then : nQuantity4 = nSavingQuantity
            ElseIf Val(nQuantity5) > 0 Then : nQuantity5 = nSavingQuantity
            ElseIf Val(nQuantity6) > 0 Then : nQuantity6 = nSavingQuantity
            ElseIf Val(nQuantity7) > 0 Then : nQuantity7 = nSavingQuantity
            ElseIf Val(nQuantity8) > 0 Then : nQuantity8 = nSavingQuantity
            ElseIf Val(nQuantity9) > 0 Then : nQuantity9 = nSavingQuantity
            ElseIf Val(nQuantity10) > 0 Then : nQuantity10 = nSavingQuantity
            ElseIf Val(nQuantity11) > 0 Then : nQuantity11 = nSavingQuantity
            ElseIf Val(nQuantity12) > 0 Then : nQuantity12 = nSavingQuantity
            ElseIf Val(nQuantity13) > 0 Then : nQuantity13 = nSavingQuantity
            ElseIf Val(nQuantity14) > 0 Then : nQuantity14 = nSavingQuantity
            ElseIf Val(nQuantity15) > 0 Then : nQuantity15 = nSavingQuantity
            ElseIf Val(nQuantity16) > 0 Then : nQuantity16 = nSavingQuantity
            ElseIf Val(nQuantity17) > 0 Then : nQuantity17 = nSavingQuantity
            ElseIf Val(nQuantity18) > 0 Then : nQuantity18 = nSavingQuantity
            End If

        Else

            Dim daSelSpoolDtl As New SqlDataAdapter("Select * from  SpoolDetails Where  SpoolHID = '" & sSpoolId & _
                                                       "' And Barcode Like '" & sBarcode & _
                                                       "' + '%' Order by Size", sConstr)
            Dim dsSelSpoolDtl As New DataSet
            daSelSpoolDtl.Fill(dsSelSpoolDtl)

            Dim i As Integer = 0
            For i = 0 To dsSelSpoolDtl.Tables(0).Rows.Count - 1
                nSize = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("Size").ToString)

                If Val(sSize1) = nSize Then : nQuantity1 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize2) = nSize Then : nQuantity2 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize3) = nSize Then : nQuantity3 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize4) = nSize Then : nQuantity4 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize5) = nSize Then : nQuantity5 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize6) = nSize Then : nQuantity6 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize7) = nSize Then : nQuantity7 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize8) = nSize Then : nQuantity8 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize9) = nSize Then : nQuantity9 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize10) = nSize Then : nQuantity10 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize11) = nSize Then : nQuantity11 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize12) = nSize Then : nQuantity12 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize13) = nSize Then : nQuantity13 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize14) = nSize Then : nQuantity14 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize15) = nSize Then : nQuantity15 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize16) = nSize Then : nQuantity16 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize17) = nSize Then : nQuantity17 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize18) = nSize Then : nQuantity18 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                End If
            Next
        End If

        nTotalQuantity = nQuantity1 + nQuantity2 + nQuantity3 + nQuantity4 + nQuantity5 + nQuantity6 + nQuantity7 + nQuantity8 + nQuantity9 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18



        If sProcess = "Production" Then
            Dim daSelJWWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelJWWIP As New DataSet
            daSelJWWIP.Fill(dsSelJWWIP)

            nMouldCompletedQty = Val(dsSelJWWIP.Tables(0).Rows(0).Item("MouldCompletedQty").ToString)
            nFinishCompletedQty = Val(dsSelJWWIP.Tables(0).Rows(0).Item("FINCompletedQty").ToString)

            If sSection = "MOULD" Then
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MouldScanDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                                 "', WIPLocation = 'MOULD', MouldQuantity = '" & nTotalQuantity & _
                                                                 "' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    sStatus = "Scanned Already"


                    stbBarcodeText = ""
                    Exit Sub
                End If

                nMouldCompletedQty = nMouldCompletedQty + nPackQty
                nMouldPendingQty = nJobcardQuantity - nMouldCompletedQty

                If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("MouldStartDate")) = True Then
                    Dim daUpdJWWIPMD As New SqlDataAdapter("Update JobcardWIP Set MouldStartDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                           "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIPMD As New DataSet
                    daUpdJWWIPMD.Fill(dsUpdJWWIPMD)
                    dsUpdJWWIPMD.AcceptChanges()

                    Dim daUpdPkgdtL As New SqlDataAdapter("Update PackingDetail Set ReadyToDispatch = '0' Where JobcardNo = '" & sJobcardNo & _
                                                          "' And ReadyToDispatch Is Null", sConstr)
                    Dim dsUpdPkgDtl As New DataSet
                    daUpdPkgdtL.Fill(dsUpdPkgDtl)
                    dsUpdPkgDtl.AcceptChanges()
                End If

                Dim daUpdJWWIP As New SqlDataAdapter("Update JobcardWIP Set MouldCompletedQty = '" & nMouldCompletedQty & _
                                                     "', MouldPendingQty = '" & nMouldPendingQty & _
                                                     "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdJWWIP As New DataSet
                daUpdJWWIP.Fill(dsUpdJWWIP)
                dsUpdJWWIP.AcceptChanges()


                If nMouldPendingQty = 0 Then
                    Dim daUpdJWWIP1 As New SqlDataAdapter("Update JobcardWIP Set MouldEndDate = '" & Format(Date.Now, "dd-MMM-yyyy  HH:mm:ss") & _
                                                  "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIP1 As New DataSet
                    daUpdJWWIP1.Fill(dsUpdJWWIP1)
                    dsUpdJWWIP1.AcceptChanges()
                End If
            ElseIf sSection = "FINISH" Then

                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then
                    sStatus = "MOULD PRODUCTION NOT DONE"


                    stbBarcodeText = ""
                    Exit Sub
                End If

                If sPartQuantity = "N" Then
                    Dim daSelMouldQty As New SqlDataAdapter("Select * from Partquantityproduction Where JobcardNo = '" & sJobcardNo & _
                                                            "' And CartonNo = '" & nBoxNo & _
                                                            "' And ProcessName = 'MOULD'", sConstr)
                    Dim dsSelMouldQty As New DataSet
                    daSelMouldQty.Fill(dsSelMouldQty)

                    If dsSelMouldQty.Tables(0).Rows.Count > 0 Then
                        If dsSelMouldQty.Tables(0).Rows(0).Item("PartQuantity") < dsSelMouldQty.Tables(0).Rows(0).Item("BoxQunatity") Then
                            sStatus = "Part Quantity. In Complete"


                            stbBarcodeText = ""
                            Exit Sub
                        End If
                    End If
                End If

                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("FinishScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set FinishScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                 "', WIPLocation = 'PACKING', ReadyToDispatch = '0', FinishedQuantity = '" & nTotalQuantity & _
                                                                 "' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    sStatus = "Scanned Already"


                    stbBarcodeText = ""
                    Exit Sub
                End If
                nFinishCompletedQty = nFinishCompletedQty + nPackQty
                nFinishPendingQty = nJobcardQuantity - nFinishCompletedQty

                If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("FINStartDate")) = True Then
                    Dim daUpdJWWIPFD As New SqlDataAdapter("Update JobcardWIP Set FINStartDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                           "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIPFD As New DataSet
                    daUpdJWWIPFD.Fill(dsUpdJWWIPFD)
                    dsUpdJWWIPFD.AcceptChanges()
                End If

                Dim daUpdJWWIPF As New SqlDataAdapter("Update JobcardWIP Set FINCompletedQty = '" & nFinishCompletedQty & _
                                                      "', FINPendingQty = '" & nFinishPendingQty & _
                                                      "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdJWWIPF As New DataSet
                daUpdJWWIPF.Fill(dsUpdJWWIPF)
                dsUpdJWWIPF.AcceptChanges()

                If nFinishPendingQty = 0 Then
                    Dim daUpdJWWIP2 As New SqlDataAdapter("Update JobcardWIP Set FINEndDate = '" & Format(Date.Now, "dd-MMM-yyyy  HH:mm:ss") & _
                                                          "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIP2 As New DataSet
                    daUpdJWWIP2.Fill(dsUpdJWWIP2)
                    dsUpdJWWIP2.AcceptChanges()

                End If

            End If
        ElseIf sProcess = "Movement" Then
            If sSection = "MOULD" Then
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then
                    sStatus = "Mould Production Not Done"


                    stbBarcodeText = ""
                    Exit Sub
                End If

                If sPartQuantity = "Y" Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MToFScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                    "', WIPLocation = 'FINISH' Where JobcardNo = '" & sJobcardNo & _
                                                                    "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MToFScanDate")) = True Then
                        Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MToFScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                     "', WIPLocation = 'FINISH' Where JobcardNo = '" & sJobcardNo & _
                                                                     "' And CartonNo = '" & nBoxNo & "'", sConstr)
                        Dim dsUpdPackingDetail As New DataSet
                        daUpdPackingDetail.Fill(dsUpdPackingDetail)
                    Else
                        sStatus = "Scanned Already"


                        stbBarcodeText = ""
                        Exit Sub
                    End If
                End If
            End If

            Exit Sub
        End If


        UpdateProductionByProcess()
        nQuantity1 = 0 : nQuantity2 = 0 : nQuantity3 = 0 : nQuantity4 = 0 : nQuantity5 = 0 : nQuantity6 = 0
        nQuantity7 = 0 : nQuantity8 = 0 : nQuantity9 = 0 : nQuantity10 = 0 : nQuantity11 = 0 : nQuantity12 = 0
        nQuantity13 = 0 : nQuantity14 = 0 : nQuantity15 = 0 : nQuantity16 = 0 : nQuantity17 = 0 : nQuantity18 = 0

        sStatus = "Successfully Updated"



    End Sub


    Private Sub UpdateProductionByProcess()
        sSize = ""
        nQuantity = 0

        Dim i As Integer = 1
        For i = 1 To 18

            'sSize = "sSize" + i.ToString
            'nQuantity = "nQuantity" + i.ToString()
            If i = 1 Then : sSize = sSize1 : nQuantity = nQuantity1
            ElseIf i = 2 Then : sSize = sSize2 : nQuantity = nQuantity2
            ElseIf i = 3 Then : sSize = sSize3 : nQuantity = nQuantity3
            ElseIf i = 4 Then : sSize = sSize4 : nQuantity = nQuantity4
            ElseIf i = 5 Then : sSize = sSize5 : nQuantity = nQuantity5
            ElseIf i = 6 Then : sSize = sSize6 : nQuantity = nQuantity6
            ElseIf i = 7 Then : sSize = sSize7 : nQuantity = nQuantity7
            ElseIf i = 8 Then : sSize = sSize8 : nQuantity = nQuantity8
            ElseIf i = 9 Then : sSize = sSize9 : nQuantity = nQuantity9
            ElseIf i = 10 Then : sSize = sSize10 : nQuantity = nQuantity10
            ElseIf i = 11 Then : sSize = sSize11 : nQuantity = nQuantity11
            ElseIf i = 12 Then : sSize = sSize12 : nQuantity = nQuantity12
            ElseIf i = 13 Then : sSize = sSize13 : nQuantity = nQuantity13
            ElseIf i = 14 Then : sSize = sSize14 : nQuantity = nQuantity14
            ElseIf i = 15 Then : sSize = sSize15 : nQuantity = nQuantity15
            ElseIf i = 16 Then : sSize = sSize16 : nQuantity = nQuantity16
            ElseIf i = 17 Then : sSize = sSize17 : nQuantity = nQuantity17
            ElseIf i = 18 Then : sSize = sSize18 : nQuantity = nQuantity18
            End If


            If nQuantity > 0 Then
                mystrProductionByProcess.ID = System.Guid.NewGuid.ToString()
                mystrProductionByProcess.ProcessName = sSection
                mystrProductionByProcess.ProcessDate = Format(Date.Now, "dd-MMM-yyyy")
                mystrProductionByProcess.ShiftNo = sShiftCode
                mystrProductionByProcess.MachineNo = mdlSGM.sMachinecode
                mystrProductionByProcess.SalesOrderNo = sSalesOrderNo
                mystrProductionByProcess.Article = sArticle
                mystrProductionByProcess.sVariant = ""
                mystrProductionByProcess.ArticleGroup = ""
                mystrProductionByProcess.ArticleGroupCode = ""
                mystrProductionByProcess.MaterialCode = ""
                mystrProductionByProcess.Size = sSize
                mystrProductionByProcess.Pcs = 0
                mystrProductionByProcess.Quantity = nQuantity
                mystrProductionByProcess.Unit = "PRS"
                mystrProductionByProcess.Price = 0
                mystrProductionByProcess.Value = 0
                mystrProductionByProcess.CompanyCode = "SSPL"
                mystrProductionByProcess.JobberCode = ""
                mystrProductionByProcess.LotNo = ""
                mystrProductionByProcess.Color = ""
                mystrProductionByProcess.WorkOrderNo = sJobcardNo
                mystrProductionByProcess.MaterialColor = ""
                mystrProductionByProcess.LocationName = sMachine
                mystrProductionByProcess.BuyerCode = sBuyerCode
                mystrProductionByProcess.Buyer = sBuyer
                mystrProductionByProcess.SupplierCode = ""
                mystrProductionByProcess.BrandCode = ""
                mystrProductionByProcess.SupplierMaterialCode = ""
                mystrProductionByProcess.CreatedBy = "" '' CreatedBy
                mystrProductionByProcess.CreatedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
                mystrProductionByProcess.ModifiedBy = "" '' sModifiedBy
                mystrProductionByProcess.ModifiedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
                mystrProductionByProcess.EnteredOnMachineID = mdlSGM.strSystemName
                mystrProductionByProcess.ExeVersionNo = ""
                mystrProductionByProcess.IsApproved = 0
                mystrProductionByProcess.ApprovedBy = ""
                mystrProductionByProcess.ApprovedOn = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
                mystrProductionByProcess.ModuleName = "Barcode Scanning"
                mystrProductionByProcess.JobCardDetailID = sJobcardDetailID
                mystrProductionByProcess.EmployeeCode = "" ''sEmployeeCode
                mystrProductionByProcess.ProductionID = ""
                mystrProductionByProcess.Location = sMachinecode
                mystrProductionByProcess.Station = ""
                mystrProductionByProcess.RejectPcs = 0
                mystrProductionByProcess.FromLocation = sFromLocation
                mystrProductionByProcess.SeqNo = 0
                mystrProductionByProcess.CurrentQuantity = nQuantity
                mystrProductionByProcess.LossQuantity = nLossQuantity
                mystrProductionByProcess.CurrentPcs = 0
                mystrProductionByProcess.LossPcs = 0
                mystrProductionByProcess.SkinType = ""
                mystrProductionByProcess.FromStage = sFromStage
                mystrProductionByProcess.OldFromLocation = ""
                mystrProductionByProcess.OldLocation = ""
                mystrProductionByProcess.IsHybrid = 0
                mystrProductionByProcess.ComponentGroup = ""
                mystrProductionByProcess.IsLastStage = 0
                mystrProductionByProcess.OldJobCardNo = ""
                mystrProductionByProcess.LeatherCode = ""
                mystrProductionByProcess.ArticleDetailId = ""

                If sSection = "MOULD" And sProcess = "Production" Then
                    myccProductionByProcess.InsertProductionByProcess(mystrProductionByProcess)
                    myccProductionByProcess.InsertProductStock(mystrProductionByProcess)
                ElseIf sSection = "MOULD" And sProcess = "Movement" Then
                    myccProductionByProcess.InsertProductionByProcess(mystrProductionByProcess)
                    myccProductionByProcess.UpdateProductStock(mystrProductionByProcess)
                    mystrProductionByProcess.ProcessName = sFromStage
                    myccProductionByProcess.InsertProductStock(mystrProductionByProcess)
                ElseIf sSection = "FINISH" Then
                    myccProductionByProcess.InsertProductionByProcess(mystrProductionByProcess)
                    myccProductionByProcess.UpdateProductStock(mystrProductionByProcess)
                    mystrProductionByProcess.ProcessName = sFromStage
                    myccProductionByProcess.InsertProductStock(mystrProductionByProcess)

                End If

                'If sFromStage <> "" Then
                '    mystrProductionByProcess.ProcessName = sFromStage
                '    myccProductionByProcess.UpdateProductStock(mystrProductionByProcess)
                'End If

                'UpdateAuditDatabase()

            End If
        Next
    End Sub

    Dim nLen As Integer
    Private Sub UpdateReadyToDispatch()
        nLen = Microsoft.VisualBasic.Len(sBarcode)

        sStatus = ""
        nStringLength = Microsoft.VisualBasic.Len(Trim(sBarcode))

        If nStringLength < 15 Or nStringLength > 17 Then
            sStatus = "Invalid Barcode"
            Exit Sub
        End If


        nBoxNoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(Trim(sBarcode), nBoxNoLength - 1)
Aa:
        Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where JobcardNo = '" & sJobcardNo & _
                                              "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelPkgDtl As New DataSet
        daSelPkgDtl.Fill(dsSelPkgDtl)

        If dsSelPkgDtl.Tables(0).Rows.Count = 0 Then
            sStatus = "This Carton Does not belong to this Packing Detail"
            Exit Sub
        End If

        If Microsoft.VisualBasic.Left(dsSelPkgDtl.Tables(0).Rows(0).Item("Article").ToString, 7) = "SOL-LEA" Then
            Dim daSelWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelWIP As New DataSet
            daSelWIP.Fill(dsSelWIP)

            If dsSelWIP.Tables(0).Rows.Count = 0 Then
                sStatus = "Jobcard WIP Not Created!"
                Exit Sub
            End If

            mdlSGM.sSelectedArticle = Microsoft.VisualBasic.Left(dsSelPkgDtl.Tables(0).Rows(0).Item("Article").ToString, 7)
            If dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch").ToString = "" Then
                Dim daUpdPkgDtl As New SqlDataAdapter("Update PackingDetail Set ReadytoDispatch = '0' Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdPkgDtl As New DataSet
                daUpdPkgDtl.Fill(dsUpdPkgDtl)
                dsUpdPkgDtl.AcceptChanges()
                GoTo Aa
            End If
        Else
            If dsSelPkgDtl.Tables(0).Rows(0).Item("WIPLocation").ToString <> "PACKING" Then
                sStatus = "This Carton is Not Received at Packing Section"

                Exit Sub
            End If
        End If

        If Val(dsSelPkgDtl.Tables(0).Rows(0).Item("ReadytoDispatch")) * -1 = 1 Then
            sStatus = "This Carton Already Included for Invoice Generation"
            Exit Sub
        End If

        UpdatePackingFromMD()
        sStatus = "Successfully Updated"

    End Sub

    Dim sFrom, sShipper, sPackNo, sSalesOrderDetailId As String
    Private Sub UpdatePackingFromMD()

        Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail where JobcardNo = '" & sJobcardNo & "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelPkgDtl As New DataSet
        daSelPkgDtl.Fill(dsSelPkgDtl)

        If dsSelPkgDtl.Tables(0).Rows.Count = 0 Then
            sStatus = "This Carton Does not belong to this customer"
            Exit Sub
        End If

        If sEntryUpdates = "Without Sizes" Then
            nQuantity = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity").ToString)
            nQuantity01 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity01").ToString)
            nQuantity02 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity02").ToString)
            nQuantity03 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity03").ToString)
            nQuantity04 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity04").ToString)
            nQuantity05 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity05").ToString)
            nQuantity06 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity06").ToString)
            nQuantity07 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity07").ToString)
            nQuantity08 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity08").ToString)
            nQuantity09 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity09").ToString)
            nQuantity10 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity10").ToString)
            nQuantity11 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity11").ToString)
            nQuantity12 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity12").ToString)
            nQuantity13 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity13").ToString)
            nQuantity14 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity14").ToString)
            nQuantity15 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity15").ToString)
            nQuantity16 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity16").ToString)
            nQuantity17 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity17").ToString)
            nQuantity18 = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity18").ToString)

            If Val(nQuantity01) > 0 Then : nQuantity01 = nSavingQuantity
            ElseIf Val(nQuantity02) > 0 Then : nQuantity02 = nSavingQuantity
            ElseIf Val(nQuantity03) > 0 Then : nQuantity03 = nSavingQuantity
            ElseIf Val(nQuantity04) > 0 Then : nQuantity04 = nSavingQuantity
            ElseIf Val(nQuantity05) > 0 Then : nQuantity05 = nSavingQuantity
            ElseIf Val(nQuantity06) > 0 Then : nQuantity06 = nSavingQuantity
            ElseIf Val(nQuantity07) > 0 Then : nQuantity07 = nSavingQuantity
            ElseIf Val(nQuantity08) > 0 Then : nQuantity08 = nSavingQuantity
            ElseIf Val(nQuantity09) > 0 Then : nQuantity09 = nSavingQuantity
            ElseIf Val(nQuantity10) > 0 Then : nQuantity10 = nSavingQuantity
            ElseIf Val(nQuantity11) > 0 Then : nQuantity11 = nSavingQuantity
            ElseIf Val(nQuantity12) > 0 Then : nQuantity12 = nSavingQuantity
            ElseIf Val(nQuantity13) > 0 Then : nQuantity13 = nSavingQuantity
            ElseIf Val(nQuantity14) > 0 Then : nQuantity14 = nSavingQuantity
            ElseIf Val(nQuantity15) > 0 Then : nQuantity15 = nSavingQuantity
            ElseIf Val(nQuantity16) > 0 Then : nQuantity16 = nSavingQuantity
            ElseIf Val(nQuantity17) > 0 Then : nQuantity17 = nSavingQuantity
            ElseIf Val(nQuantity18) > 0 Then : nQuantity18 = nSavingQuantity
            End If

            nQuantity = nQuantity01 + nQuantity02 + nQuantity03 + nQuantity04 + nQuantity05 + nQuantity06 + nQuantity07 + nQuantity08 + nQuantity09 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18
        Else
            Dim daSelSpoolDtl As New SqlDataAdapter("Select * from  SpoolDetails Where  SpoolHID = '" & sSpoolId & _
                                                   "' And Barcode Like '" & sBarcode & _
                                                   "' + '%' Order by Size", sConstr)
            Dim dsSelSpoolDtl As New DataSet
            daSelSpoolDtl.Fill(dsSelSpoolDtl)

            Dim i As Integer = 0
            For i = 0 To dsSelSpoolDtl.Tables(0).Rows.Count - 1
                nSize = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("Size").ToString)

                If Val(sSize1) = nSize Then : nQuantity1 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize2) = nSize Then : nQuantity2 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize3) = nSize Then : nQuantity3 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize4) = nSize Then : nQuantity4 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize5) = nSize Then : nQuantity5 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize6) = nSize Then : nQuantity6 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize7) = nSize Then : nQuantity7 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize8) = nSize Then : nQuantity8 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize9) = nSize Then : nQuantity9 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize10) = nSize Then : nQuantity10 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize11) = nSize Then : nQuantity11 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize12) = nSize Then : nQuantity12 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize13) = nSize Then : nQuantity13 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize14) = nSize Then : nQuantity14 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize15) = nSize Then : nQuantity15 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize16) = nSize Then : nQuantity16 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize17) = nSize Then : nQuantity17 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                ElseIf Val(sSize18) = nSize Then : nQuantity18 = Val(dsSelSpoolDtl.Tables(0).Rows(i).Item("SavingQuantity").ToString)
                End If
            Next
        End If

        nTotalQuantity = nQuantity01 + nQuantity02 + nQuantity03 + nQuantity04 + nQuantity05 + nQuantity06 + nQuantity07 + nQuantity08 + nQuantity09 + nQuantity10 + nQuantity11 + nQuantity12 + nQuantity13 + nQuantity14 + nQuantity15 + nQuantity16 + nQuantity17 + nQuantity18

        sFrom = "Mobile Computer"
        'nQuantity = nTotalQuantity
        JobcardVerification()

        mystrReadytoDispatch.ID = System.Guid.NewGuid.ToString() 'AsString
        mystrReadytoDispatch.invoiceno = "" 'AsString
        mystrReadytoDispatch.InvoiceDate = Date.Now 'AsDate
        mystrReadytoDispatch.InvoiceSerialNo = "" 'AsString
        mystrReadytoDispatch.buyer = sBuyer 'AsString
        mystrReadytoDispatch.shipper = sShipper 'AsString
        mystrReadytoDispatch.SalesOrderNo = sSalesOrderNo 'AsString
        mystrReadytoDispatch.ArticleNo = sArticle 'AsString
        mystrReadytoDispatch.rate = 0 'AsDecimal
        mystrReadytoDispatch.quantity = nQuantity 'AsDecimal
        mystrReadytoDispatch.BuyerGroup = sBuyerCode 'AsString
        mystrReadytoDispatch.CreatedBy = "" 'AsString
        mystrReadytoDispatch.CreatedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrReadytoDispatch.ModifiedBy = ""
        mystrReadytoDispatch.ModifiedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrReadytoDispatch.EnteredOnMachineID = mdlSGM.strSystemName
        mystrReadytoDispatch.PackNo = sPackNo 'AsString
        mystrReadytoDispatch.JobCardNo = sJobcardNo 'AsString
        mystrReadytoDispatch.IsApproved = 0 'AsInteger
        mystrReadytoDispatch.ApprovedBy = "" 'AsString
        mystrReadytoDispatch.ApprovedOn = Date.Now 'AsDate
        mystrReadytoDispatch.ModuleName = "Barcode Scanning" 'AsString
        mystrReadytoDispatch.JobCardDetailID = sJobcardDetailID 'AsString
        mystrReadytoDispatch.InvoiceID = "" 'AsString
        mystrReadytoDispatch.SalesOrderDetailID = sSalesOrderDetailId 'AsString
        mystrReadytoDispatch.Size01 = sSize01 'AsString
        mystrReadytoDispatch.Quantity01 = nQuantity01 'AsDecimal
        mystrReadytoDispatch.Size02 = sSize02 'AsString
        mystrReadytoDispatch.Quantity02 = nQuantity02 'AsDecimal
        mystrReadytoDispatch.Size03 = sSize03 'AsString
        mystrReadytoDispatch.Quantity03 = nQuantity03 'AsDecimal
        mystrReadytoDispatch.Size04 = sSize04 'AsString
        mystrReadytoDispatch.Quantity04 = nQuantity04 'AsDecimal
        mystrReadytoDispatch.Size05 = sSize05 'AsString
        mystrReadytoDispatch.Quantity05 = nQuantity05 'AsDecimal
        mystrReadytoDispatch.Size06 = sSize06 'AsString
        mystrReadytoDispatch.Quantity06 = nQuantity06 'AsDecimal
        mystrReadytoDispatch.Size07 = sSize07 'AsString
        mystrReadytoDispatch.Quantity07 = nQuantity07 'AsDecimal
        mystrReadytoDispatch.Size08 = sSize08 'AsString
        mystrReadytoDispatch.Quantity08 = nQuantity08 'AsDecimal
        mystrReadytoDispatch.Size09 = sSize09 'AsString
        mystrReadytoDispatch.Quantity09 = nQuantity09 'AsDecimal
        mystrReadytoDispatch.Size10 = sSize10 'AsString
        mystrReadytoDispatch.Quantity10 = nQuantity10 'AsDecimal
        mystrReadytoDispatch.Size11 = sSize11 'AsString
        mystrReadytoDispatch.Quantity11 = nQuantity11 'AsDecimal
        mystrReadytoDispatch.Size12 = sSize12 'AsString
        mystrReadytoDispatch.Quantity12 = nQuantity12 'AsDecimal
        mystrReadytoDispatch.Size13 = sSize13 'AsString
        mystrReadytoDispatch.Quantity13 = nQuantity13 'AsDecimal
        mystrReadytoDispatch.Size14 = sSize14 'AsString
        mystrReadytoDispatch.Quantity14 = nQuantity14 'AsDecimal
        mystrReadytoDispatch.Size15 = sSize15 'AsString
        mystrReadytoDispatch.Quantity15 = nQuantity15 'AsDecimal
        mystrReadytoDispatch.Size16 = sSize16 'AsString
        mystrReadytoDispatch.Quantity16 = nQuantity16 'AsDecimal
        mystrReadytoDispatch.Size17 = sSize17 'AsString
        mystrReadytoDispatch.Quantity17 = nQuantity17 'AsDecimal
        mystrReadytoDispatch.Size18 = sSize18 'AsString
        mystrReadytoDispatch.Quantity18 = nQuantity18 'AsDecimal

        Dim daSelSOrdQty As New SqlDataAdapter("Select * from ReadyToDispatch Where SalesOrderNo = '" & sSalesOrderNo & "'", sConstr)
        Dim dsSelSOrdQty As New DataSet
        daSelSOrdQty.Fill(dsSelSOrdQty)

        nRowCount = dsSelSOrdQty.Tables(0).Rows.Count + 1
        mystrReadytoDispatch.SpoolId = sSpoolId
        mystrReadytoDispatch.SpoolDt = Format(Date.Now, "dd-MMM-yyyy")


        myccPackingEntries.InsertReadytoDispatch(mystrReadytoDispatch)
        sadf()
        myccPackingEntries.UpdatePackingDetail(sJobcardNo, nBoxNo, nTotalQuantity)
        myccPackingEntries.UpdateReadytoDispatch(sJobcardNo)


    End Sub


#End Region

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

    Private Sub EVAProductionQuantityUpdates()

        nBoxNoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(stbBarcodeText, nBoxNoLength - 1)

        Dim daSelBoxInfo As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                               "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelBoxInfo As New DataSet
        daSelBoxInfo.Fill(dsSelBoxInfo)

        nPackQty = dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity")

        sSize1 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size01").ToString
        sSize2 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size02").ToString
        sSize3 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size03").ToString
        sSize4 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size04").ToString
        sSize5 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size05").ToString
        sSize6 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size06").ToString
        sSize7 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size07").ToString
        sSize8 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size08").ToString
        sSize9 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size09").ToString
        sSize10 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size10").ToString
        sSize11 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size11").ToString
        sSize12 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size12").ToString
        sSize13 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size13").ToString
        sSize14 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size14").ToString
        sSize15 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size15").ToString
        sSize16 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size16").ToString
        sSize17 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size17").ToString
        sSize18 = dsSelBoxInfo.Tables(0).Rows(0).Item("Size18").ToString

        nQuantity1 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity01").ToString)
        nQuantity2 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity02").ToString)
        nQuantity3 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity03").ToString)
        nQuantity4 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity04").ToString)
        nQuantity5 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity05").ToString)
        nQuantity6 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity06").ToString)
        nQuantity7 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity07").ToString)
        nQuantity8 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity08").ToString)
        nQuantity9 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity09").ToString)
        nQuantity10 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity10").ToString)
        nQuantity11 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity11").ToString)
        nQuantity12 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity12").ToString)
        nQuantity13 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity13").ToString)
        nQuantity14 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity14").ToString)
        nQuantity15 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity15").ToString)
        nQuantity16 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity16").ToString)
        nQuantity17 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity17").ToString)
        nQuantity18 = Val(dsSelBoxInfo.Tables(0).Rows(0).Item("Quantity18").ToString)

        If sProcess = "Production" Then
            Dim daSelJWWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelJWWIP As New DataSet
            daSelJWWIP.Fill(dsSelJWWIP)

            nMouldCompletedQty = Val(dsSelJWWIP.Tables(0).Rows(0).Item("MouldCompletedQty").ToString)
            nFinishCompletedQty = Val(dsSelJWWIP.Tables(0).Rows(0).Item("FINCompletedQty").ToString)

            If sSection = "MOULD" Then
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then

                    If sEVAProcess = "M2S" Then
                        sWIPLocation = "FINISH"
                    ElseIf sEVAProcess = "M2L" Then
                        sWIPLocation = ""
                    ElseIf sEVAProcess = "M2P" Then
                        sWIPLocation = "PACKING"
                    End If

                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MouldScanDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                                 "', WIPLocation = '" & sWIPLocation & "' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    sStatus = "Scanned Already"


                    stbBarcodeText = ""
                    Exit Sub
                End If

                nMouldCompletedQty = nMouldCompletedQty + nPackQty
                nMouldPendingQty = nJobcardQuantity - nMouldCompletedQty

                If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("MouldStartDate")) = True Then
                    Dim daUpdJWWIPMD As New SqlDataAdapter("Update JobcardWIP Set MouldStartDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                           "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIPMD As New DataSet
                    daUpdJWWIPMD.Fill(dsUpdJWWIPMD)
                    dsUpdJWWIPMD.AcceptChanges()

                    Dim daUpdPkgdtL As New SqlDataAdapter("Update PackingDetail Set ReadyToDispatch = '0' Where JobcardNo = '" & sJobcardNo & _
                                                          "' And ReadyToDispatch Is Null", sConstr)
                    Dim dsUpdPkgDtl As New DataSet
                    daUpdPkgdtL.Fill(dsUpdPkgDtl)
                    dsUpdPkgDtl.AcceptChanges()
                End If

                Dim daUpdJWWIP As New SqlDataAdapter("Update JobcardWIP Set MouldCompletedQty = '" & nMouldCompletedQty & _
                                                     "', MouldPendingQty = '" & nMouldPendingQty & _
                                                     "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdJWWIP As New DataSet
                daUpdJWWIP.Fill(dsUpdJWWIP)
                dsUpdJWWIP.AcceptChanges()


                If nMouldPendingQty = 0 Then
                    Dim daUpdJWWIP1 As New SqlDataAdapter("Update JobcardWIP Set MouldEndDate = '" & Format(Date.Now, "dd-MMM-yyyy  HH:mm:ss") & _
                                                  "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIP1 As New DataSet
                    daUpdJWWIP1.Fill(dsUpdJWWIP1)
                    dsUpdJWWIP1.AcceptChanges()
                End If
            ElseIf sSection = "FINISH" Then

                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then
                    sStatus = "Mould Production Not Done"


                    stbBarcodeText = ""
                    Exit Sub
                End If

                If sPartQuantity = "N" Then
                    Dim daSelMouldQty As New SqlDataAdapter("Select * from Partquantityproduction Where JobcardNo = '" & sJobcardNo & _
                                                            "' And CartonNo = '" & nBoxNo & _
                                                            "' And ProcessName = 'MOULD'", sConstr)
                    Dim dsSelMouldQty As New DataSet
                    daSelMouldQty.Fill(dsSelMouldQty)

                    If dsSelMouldQty.Tables(0).Rows.Count > 0 Then
                        If dsSelMouldQty.Tables(0).Rows(0).Item("PartQuantity") < dsSelMouldQty.Tables(0).Rows(0).Item("BoxQunatity") Then
                            sStatus = "Part Quantity. In Complete"


                            stbBarcodeText = ""
                            Exit Sub
                        End If
                    End If
                End If

                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("FinishScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set FinishScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                 "', WIPLocation = 'PACKING', ReadyToDispatch = '0' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    sStatus = "Scanned Already"


                    stbBarcodeText = ""
                    Exit Sub
                End If
                nFinishCompletedQty = nFinishCompletedQty + nPackQty
                nFinishPendingQty = nJobcardQuantity - nFinishCompletedQty

                If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("FINStartDate")) = True Then
                    Dim daUpdJWWIPFD As New SqlDataAdapter("Update JobcardWIP Set FINStartDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                           "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIPFD As New DataSet
                    daUpdJWWIPFD.Fill(dsUpdJWWIPFD)
                    dsUpdJWWIPFD.AcceptChanges()
                End If

                Dim daUpdJWWIPF As New SqlDataAdapter("Update JobcardWIP Set FINCompletedQty = '" & nFinishCompletedQty & _
                                                      "', FINPendingQty = '" & nFinishPendingQty & _
                                                      "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdJWWIPF As New DataSet
                daUpdJWWIPF.Fill(dsUpdJWWIPF)
                dsUpdJWWIPF.AcceptChanges()

                If nFinishPendingQty = 0 Then
                    Dim daUpdJWWIP2 As New SqlDataAdapter("Update JobcardWIP Set FINEndDate = '" & Format(Date.Now, "dd-MMM-yyyy  HH:mm:ss") & _
                                                          "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIP2 As New DataSet
                    daUpdJWWIP2.Fill(dsUpdJWWIP2)
                    dsUpdJWWIP2.AcceptChanges()

                End If

            End If
        ElseIf sProcess = "Movement" Then
            If sSection = "MOULD" Then
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then
                    sStatus = "Mould Production Not Done"


                    stbBarcodeText = ""
                    Exit Sub
                End If

                If sPartQuantity = "Y" Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MToFScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                    "', WIPLocation = 'FINISH' Where JobcardNo = '" & sJobcardNo & _
                                                                    "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MToFScanDate")) = True Then
                        Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MToFScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                     "', WIPLocation = 'FINISH' Where JobcardNo = '" & sJobcardNo & _
                                                                     "' And CartonNo = '" & nBoxNo & "'", sConstr)
                        Dim dsUpdPackingDetail As New DataSet
                        daUpdPackingDetail.Fill(dsUpdPackingDetail)
                    Else
                        sStatus = "Scanned Already"


                        stbBarcodeText = ""
                        Exit Sub
                    End If
                End If
            End If


        End If


        UpdateProductionByProcess()

        sStatus = "Successfully Updated"

    End Sub

    Private Sub frmSave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sProcess = "Production"
    End Sub

    Private Sub cbExitSplDtls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExitSplDtls.Click
        
        myOptimizerStrUserAuthentication.sUserName = mdlSGM.sUserName
        myOptimizerStrUserAuthentication.sLogoutTime = Format(Date.Now, "dd-MMM-yyyy::hh:mm:ss-tt")
        myOptimizerStrUserAuthentication.sIsActive = "0"
        If myccOptimizerComponentUser.UpdateUserAuthentication(myOptimizerStrUserAuthentication) = True Then
            Me.DialogResult = DialogResult.OK
        End If
        Application.Exit()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbBackSplDtls.Click
        frmSelection.Show()
        Me.Hide()
        sSection = ""
    End Sub

    Dim sBoxNoWithError As String

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim filePath As String = String.Format("D:\" + Trim(sSpoolId) + DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then

                Dim daSelSpoolDtls As New SqlDataAdapter("Select spd.BarCode + ' :: ' + abb.FullName_ As BoxInfo From SpoolDetails As SPD, AbbrevTable As Abb Where spd.FKStatus = Cast(abb.Abbrev_ As Int)  And abb.Group_= 'scanstatus'  And spd.SpoolHID = ' " & sSpoolId & "'", sConstr)
                Dim dsSelSpoolDtls As New DataSet
                daSelSpoolDtls.Fill(dsSelSpoolDtls)

                Dim i As Integer = 0

                For i = 0 To dsSelSpoolDtls.Tables(0).Rows.Count - 1
                    sBoxNoWithError = dsSelSpoolDtls.Tables(0).Rows(i).Item("BoxInfo").ToString
                    writer.WriteLine(Trim(sBoxNoWithError))
                Next

            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub

    Private Sub tbSpoolNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbSpoolNo.KeyPress

    End Sub

    Private Sub tbSpoolNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSpoolNo.TextChanged

    End Sub
End Class