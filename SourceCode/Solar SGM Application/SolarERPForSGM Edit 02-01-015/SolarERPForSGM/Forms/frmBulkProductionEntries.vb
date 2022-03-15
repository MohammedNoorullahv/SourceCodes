Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO




Public Class frmBulkProductionEntries

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)
    Dim keyascii As Integer

    Dim myccProductionByProcess As New ccProductionByProcess
    Dim mystrProductionByProcess As New strProductionByProcess
    Dim mystrPartquantityproduction As New strPartquantityproduction
    Dim mystrPackingDetail As New strPackingDetail
    Dim myccInvoice As New ccInvoice

    Dim dMouldingDate, dFinishingDate, dPackingDate As Date
    Dim nMouldCompletedQty, nMouldPendingQty, nFinishCompletedQty, nFinishPendingQty, nPackQty As Integer
    Dim sShiftCode, sShift As String
    Dim sSectionCode, sSection As String
    Dim sMachineCode, sMachine As String
    Dim sStationCode, sStation As String
    Dim sPartQuantity As String

    Dim sSalesOrderNo, sArticle, sBuyerCode, sBuyer, sJobcardDetailID As String
    Dim sSize1, sSize2, sSize3, sSize4, sSize5, sSize6, sSize7, sSize8, sSize9, sSize10 As String
    Dim sSize11, sSize12, sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize, sPartQtySize As String

    Dim nQuantity1, nQuantity2, nQuantity3, nQuantity4, nQuantity5, nQuantity6, nQuantity7, nQuantity8, nQuantity9, nQuantity10 As Integer
    Dim nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18, nQuantity As Integer

    Dim sFromLocation, sFromStage, sPartQtyExceeded As String
    Dim nCurrentQuantity, nLossQuantity, nSizeCount As Integer

#End Region

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        End
        'Me.Hide()
        'Form1.Show() : Form1.BringToFront()
    End Sub

    Dim ngrdRowCount As Integer


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        'MsgBox("Export Completed")

        'Dim filePath As String = String.Format("E:\BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        'Using writer As New StreamWriter(filePath, True)
        '    If File.Exists(filePath) Then
        '        'writer.WriteLine("Error Message in  Occured at-- " & DateTime.Now)
        '        writer.WriteLine(Trim(tbBarcode.Text) + " | " & DateTime.Now)
        '    Else
        '        writer.WriteLine("Start Error Log for today")
        '    End If
        'End Using

        Dim daSelProductStock As New SqlDataAdapter("Select Stage,WorkOrderNo,Size,Sum(Quantity) As Quantity from ProductStock Where WorkOrderNo in (Select JobcardNo From JobcardWIP) And Quantity <> '0' Group By Stage,WorkOrderNo,Size Order By Stage,WorkOrderNo,Size", sConstr)
        Dim dsSelProductStock As New DataSet
        daSelProductStock.Fill(dsSelProductStock)

        Dim i As Integer = 0

        For i = 0 To dsSelProductStock.Tables(0).Rows.Count - 1
            sPSStage = ""
        Next

    End Sub


    Dim sJobcardNo, sProcess, sStatus As String

    Dim nStringLength, nBoxNo, nBoxnoLength As Integer
    Dim sBeginsWith, sDescription As String
    Private Sub tbBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbBarcode.KeyPress

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then
            ' AppendText()
            'PrintDocument1.Print()
            'Exit Sub123


            If Trim(tbBarcode.Text) = "EXIT" Then
                End
            End If
            tbLastScannedBarcode.Clear()


            nStringLength = Microsoft.VisualBasic.Len(tbBarcode.Text)

            If nStringLength = 13 Then
                sJobcardNo = tbBarcode.Text
                LoadPackingList()
            End If
            If Microsoft.VisualBasic.Mid((tbBarcode.Text), 4, 1) = ":" Then
                If Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "SHI" Then
                    sShiftCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sShift = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    tbShift.Text = sShift
                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "PRO" Then
                    sProcess = "Production"
                    sMachine = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sMachineCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)

                    Dim daSelMachine As New SqlDataAdapter("Select * from Location where LocationCode = '" & sMachineCode & "'", sConstr)
                    Dim dsSelMachine As New DataSet
                    daSelMachine.Fill(dsSelMachine)

                    If dsSelMachine.Tables(0).Rows.Count = 0 Then
                        sStatus = "Invalid Machine Code"
                        UpdateNotePad()
                        'tbStatus.ForeColor = Color.Red
                        tbBarcode.Clear()
                        sMachineCode = ""
                        tbMachineNo.Clear()
                        tbBarcode.Focus()
                        Exit Sub
                    Else
                        sMachine = dsSelMachine.Tables(0).Rows(0).Item("LocationName").ToString
                        tbMachineNo.Text = sMachine
                        sSection = dsSelMachine.Tables(0).Rows(0).Item("MaterialTypeCode").ToString
                        sSectionCode = sSection
                        tbSection.Text = sSection

                        sFromStage = dsSelMachine.Tables(0).Rows(0).Item("BaggingType").ToString

                        sStation = ""
                        sStationCode = ""
                        tbStationNo.Clear()
                    End If


                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "STA" Then
                    sStation = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sStationCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    tbStationNo.Text = sStation

                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "MOV" Then
                    sProcess = "Movement"
                    sMachine = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sMachineCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)

                    Dim daSelMachine As New SqlDataAdapter("Select * from Location where LocationCode = '" & sMachineCode & "'", sConstr)
                    Dim dsSelMachine As New DataSet
                    daSelMachine.Fill(dsSelMachine)

                    If dsSelMachine.Tables(0).Rows.Count = 0 Then
                        sStatus = "Invalid Machine Code"
                        'tbStatus.ForeColor = Color.Red
                        UpdateNotePad()
                        tbBarcode.Clear()
                        sMachineCode = ""
                        tbMachineNo.Clear()
                        tbBarcode.Focus()
                        Exit Sub
                    Else
                        sMachine = dsSelMachine.Tables(0).Rows(0).Item("LocationName").ToString
                        tbMachineNo.Text = sMachine
                        sSection = dsSelMachine.Tables(0).Rows(0).Item("MaterialTypeCode").ToString
                        sSectionCode = sSection
                        tbSection.Text = sSection

                        sFromStage = dsSelMachine.Tables(0).Rows(0).Item("BaggingType").ToString

                    End If
                End If
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If

            If sSection = "" Or sMachine = "" Then
                'MsgBox("Section / Machine Not Selected", MsgBoxStyle.Critical)
                If sSection = "" Then
                    sStatus = "Section Not Selected"
                Else
                    sStatus = "Section Not Selected"
                End If
                'tbStatus.ForeColor = Color.Red
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If


            If Trim(tbBarcode.Text) = "PART QTY" Then
                'plPartQtyInfo.Visible = True
                'plPartQtyInfo.BringToFront()
                sPartQuantity = "Y"
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            ElseIf Trim(tbBarcode.Text) = "FULL QTY" Then
                'plPartQtyInfo.Visible = False
                sPartQuantity = "N"
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If


            tbLastScannedBarcode.Text = Trim(tbBarcode.Text)

            If sPartQuantity = "Y" Then
                'LoadSizeInfo()
                Exit Sub
            End If

            BarcodeSettings()

            JobcardVerification()

            If Microsoft.VisualBasic.Left(sArticle, 10) = "SOL-SYN-EV" Or Microsoft.VisualBasic.Left(sArticle, 10) = "SOL-LEA-EV" Then
                If sEVAProcess = "" Then
                    sStatus = "Process Not Defined"
                    UpdateNotePad()
                    tbBarcode.Clear()
                    tbBarcode.Focus()
                    Exit Sub
                End If
                EVAProductionQuantityUpdates()
            ElseIf Microsoft.VisualBasic.Mid(sArticle, 9, 2) = "RU" Then
                If sEVAProcess = "" Then
                    sStatus = "Process Not Defined"
                    UpdateNotePad()
                    tbBarcode.Clear()
                    tbBarcode.Focus()
                    Exit Sub
                End If
                EVAProductionQuantityUpdates()
            Else
                ProductionQuantityUpdates()
            End If


            'PrintDocument1.Print()

            tbLastScannedBarcode.Text = tbBarcode.Text
        End If
    End Sub

    Private Sub BarcodeSettings()

        nStringLength = Microsoft.VisualBasic.Len(tbBarcode.Text)
        sBeginsWith = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 1)

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

    Dim sEVAProcess As String

    Private Sub JobcardVerification()

        sJobcardNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 13)
        sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 9)

        Dim daSelJobCardInfo As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo = '" & sJobcardNo & "'", sConstr)
        Dim dsSelJobcardInfo As New DataSet
        daSelJobCardInfo.Fill(dsSelJobcardInfo)

        If dsSelJobcardInfo.Tables(0).Rows.Count <= 0 Then
            MsgBox("Invalid Jobcard No.")
            sStatus = "Invalid Jobcard No."
            UpdateNotePad()
            tbBarcode.Clear()
            tbBarcode.Focus()
        ElseIf dsSelJobcardInfo.Tables(0).Rows.Count > 1 Then
            MsgBox("Invalid Jobcard No. Multiple Jobcards Existing")
            sStatus = "Invalid Jobcard No. Multiple Jobcards Existing"
            UpdateNotePad()
            tbBarcode.Clear()
            tbBarcode.Focus()
        Else
            'sJobcardNo = dsSelJobcardInfo.Tables(0).Rows(0).Item("JobcardNo").ToString
            tbJobcardQty.Text = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("Quantity").ToString)
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
        End If



    End Sub

    Dim sWIPLocation As String

    Private Sub ProductionQuantityUpdates()

        nBoxnoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(tbBarcode.Text, nBoxnoLength - 1)

        Dim daSelBoxInfo As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                               "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelBoxInfo As New DataSet
        daSelBoxInfo.Fill(dsSelBoxInfo)

        If dsSelBoxInfo.Tables(0).Rows.Count = 0 Then
            sStatus = "Invalid Box No"
            tbBarcode.Clear()
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
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MouldScanDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                                 "', WIPLocation = 'MOULD' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    sStatus = "Scanned Already"
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If

                nMouldCompletedQty = nMouldCompletedQty + nPackQty
                nMouldPendingQty = Val(tbJobcardQty.Text) - nMouldCompletedQty

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

                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MtoFScanDate")) = True Then
                    sStatus = "Mould 2 Finish Not Done"
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
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
                            'tbStatus.ForeColor = Color.Red
                            UpdateNotePad()
                            tbBarcode.Clear()
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
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If
                nFinishCompletedQty = nFinishCompletedQty + nPackQty
                nFinishPendingQty = Val(tbJobcardQty.Text) - nFinishCompletedQty

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
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
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
                        'tbStatus.ForeColor = Color.Red
                        UpdateNotePad()
                        tbBarcode.Clear()
                        Exit Sub
                    End If
                End If
            End If
            LoadJWWIPInfo()
            Exit Sub
        End If


        UpdateProductionByProcess()
        LoadJWWIPInfo()

        sStatus = "Successfully Updated"
        'tbStatus.ForeColor = Color.Green
        UpdateNotePad()
        tbBarcode.Clear()
        LoadJWWIPInfo()
    End Sub

    Private Sub EVAProductionQuantityUpdates()

        nBoxnoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(tbBarcode.Text, nBoxnoLength - 1)

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
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If

                nMouldCompletedQty = nMouldCompletedQty + nPackQty
                nMouldPendingQty = Val(tbJobcardQty.Text) - nMouldCompletedQty

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
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
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
                            'tbStatus.ForeColor = Color.Red
                            UpdateNotePad()
                            tbBarcode.Clear()
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
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If
                nFinishCompletedQty = nFinishCompletedQty + nPackQty
                nFinishPendingQty = Val(tbJobcardQty.Text) - nFinishCompletedQty

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
                    'tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
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
                        'tbStatus.ForeColor = Color.Red
                        UpdateNotePad()
                        tbBarcode.Clear()
                        Exit Sub
                    End If
                End If
            End If
            LoadJWWIPInfo()

        End If


        UpdateProductionByProcess()
        LoadJWWIPInfo()

        sStatus = "Successfully Updated"
        'tbStatus.ForeColor = Color.Green
        UpdateNotePad()
        tbBarcode.Clear()
        LoadJWWIPInfo()
    End Sub

    Private Sub Clear()
        tbMouldStartDate.Clear() : tbMouldCompleted.Clear() : tbMouldPending.Clear() : tbMouldEndDate.Clear()

        tbFinishingStartDate.Clear() : tbFinishingCompleted.Clear() : tbFinishingPending.Clear() : tbFinishingPending.Clear()

        tbPackStartDate.Clear() : tbPackCompleted.Clear() : tbPackPending.Clear() : tbPackEndDate.Clear()

        

    End Sub

    Private Sub LoadJWWIPInfo()

        Clear()

        Dim daSelJWWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
        Dim dsSelJWWIP As New DataSet
        daSelJWWIP.Fill(dsSelJWWIP)

        If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("MouldStartDate")) <> True Then
            tbMouldStartDate.Text = dsSelJWWIP.Tables(0).Rows(0).Item("MouldStartDate")
        End If
        tbMouldCompleted.Text = Val(dsSelJWWIP.Tables(0).Rows(0).Item("MouldCompletedQty").ToString)
        tbMouldPending.Text = Val(dsSelJWWIP.Tables(0).Rows(0).Item("MouldPendingQty").ToString)
        If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("MouldEndDate")) <> True Then
            tbMouldEndDate.Text = dsSelJWWIP.Tables(0).Rows(0).Item("MouldEndDate")
        End If

        If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("FINStartDate")) <> True Then
            tbFinishingStartDate.Text = dsSelJWWIP.Tables(0).Rows(0).Item("FINStartDate")
        End If
        tbFinishingCompleted.Text = Val(dsSelJWWIP.Tables(0).Rows(0).Item("FINCompletedQty").ToString)
        tbFinishingPending.Text = Val(dsSelJWWIP.Tables(0).Rows(0).Item("FINPendingQty").ToString)
        If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("FINEndDate")) <> True Then
            tbFinishingEndDate.Text = dsSelJWWIP.Tables(0).Rows(0).Item("FINEndDate")
        End If

        If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("PackStartDate")) <> True Then
            tbPackStartDate.Text = dsSelJWWIP.Tables(0).Rows(0).Item("PackStartDate")
        End If
        tbPackCompleted.Text = Val(dsSelJWWIP.Tables(0).Rows(0).Item("PackCompletedQty").ToString)
        tbPackPending.Text = Val(dsSelJWWIP.Tables(0).Rows(0).Item("PackPendingQty").ToString)
        If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("PackEndDate")) <> True Then
            tbPackEndDate.Text = dsSelJWWIP.Tables(0).Rows(0).Item("PackEndDate")
        End If


        UpdateNotePad()
        tbBarcode.Clear()
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
                mystrProductionByProcess.MachineNo = sMachineCode
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
                mystrProductionByProcess.Location = sMachineCode
                mystrProductionByProcess.Station = sStationCode
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

                UpdateAuditDatabase()

            End If
        Next
    End Sub

    Private Sub frmProductionEntries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sPartQuantity = "N"
    End Sub

    Dim nPartBoxNo As Integer

    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static intCurrentChar As Int32 = 0

        Dim font As New Font("Verdana", 8)

        Dim PrintAreaHeight, PrintAreaWidth, marginLeft, marginTop As Int32

        With PrintDocument1.DefaultPageSettings

            .Margins.Top = 0
            .Margins.Bottom = 0
            .Margins.Left = 0
            .Margins.Right = 0
            '.PaperSize.Height = 400
            PrintAreaHeight = .PaperSize.Height - 826 ''.Margins.Top - .Margins.Bottom
            'PrintAreaHeight = 1167
            PrintAreaWidth = .PaperSize.Width + 100 '- .Margins.Left - .Margins.Right
            'PrintAreaWidth = 281
            marginLeft = .Margins.Left

            marginTop = .Margins.Top

        End With

        Dim intLineCount As Int32 = CInt(PrintAreaHeight / (font.Height))
        ''intLineCount = 90 : PrintAreaHeight = 1167 : font.Height = 13
        Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, PrintAreaWidth, PrintAreaHeight)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
        ''StringFormatFlags.LineLimit == 8192
        Dim intLinesFilled, intCharsFitted As Int32

        'sPrintingMessage = Trim(tbBarcode.Text) + " : " + Trim(tbStatus.Text)
        sPrintingMessage = Trim(tbBarcode.Text) + " | " + Trim(tbJobcardNo.Text)
        intCharsFitted = 1
        'e.Graphics.MeasureString(Mid(sPrintingMessage, intCurrentChar + 1), font, New SizeF(PrintAreaWidth, PrintAreaHeight), fmt, intCharsFitted, intLinesFilled)
        'e.Graphics.MeasureString(Mid(sPrintingMessage, 1), font, New SizeF(PrintAreaWidth, PrintAreaHeight), fmt, 1, intLinesFilled)
        ''-                                                 0                                   281             1167                        23      1           
        e.Graphics.DrawString(Mid(sPrintingMessage, intCurrentChar + 1), font, Brushes.Black, rectPrintingArea, fmt)

        ''intCurrentChar += intCharsFitted
        ''0                     23
    End Sub

    Dim sPrintingMessage As String
    Dim myStringBuilder As New System.Text.StringBuilder

    Private Sub AppendText()
        'TextBox1.AppendText("Data2" & Environment.NewLine)
        'If sPrintingMessage = "" Then
        sPrintingMessage = Trim(tbBarcode.Text) + " | " + Trim(tbJobcardNo.Text)
        'Else
        '    sPrintingMessage &= sPrintingMessage & Environment.NewLine & Trim(tbBarcode.Text) + " | " + Trim(tbJobcardNo.Text)
        'End If


        myStringBuilder.AppendLine("")
        myStringBuilder.AppendLine(sPrintingMessage)
        myStringBuilder.AppendLine("")
        'Label1.Text = myStringBuilder.ToString()
        sPrintingMessage = myStringBuilder.ToString()
        PrintDocument1.Print()
    End Sub

    Dim nJobcardQty, nBalQty As Integer
    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        'Exit Sub123
        'Dim daSelProducedQty As New SqlDataAdapter("Select WorkOrderno,ProcessName,Sum(Quantity) As Produced From ProductionByProcess Where ModuleName = 'Barcode Scanning' Group By WorkOrderNo,  ProcessName  Order BY WorkOrderNo,  ProcessName Desc", sConstr)
        Dim daSelProducedQty As New SqlDataAdapter("Select WorkOrderno,ProcessName,Sum(Quantity) As Produced From ProductionByProcess Where WorkOrderNo in (Select JobcardNo  from Jobcardwip) Group By WorkOrderNo,  ProcessName  Order BY WorkOrderNo,  ProcessName Desc", sConstr)
        Dim dsSelProducedQty As New DataSet
        daSelProducedQty.Fill(dsSelProducedQty)

        Dim i As Integer = 0

        For i = 0 To dsSelProducedQty.Tables(0).Rows.Count - 1
            sJobcardNo = dsSelProducedQty.Tables(0).Rows(i).Item("WorkOrderno").ToString
            sProcess = dsSelProducedQty.Tables(0).Rows(i).Item("ProcessName").ToString
            nQuantity = Val(dsSelProducedQty.Tables(0).Rows(i).Item("Produced").ToString)

            Dim daSelJobQty As New SqlDataAdapter("Select * from JobcardDetail Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelJobQty As New DataSet
            daSelJobQty.Fill(dsSelJobQty)

            nJobcardQty = Val(dsSelJobQty.Tables(0).Rows(0).Item("Quantity").ToString)
            nBalQty = nJobcardQty - nQuantity
            ''MouldCompletedQty,MouldPendingQty,FinCompletedQty,FinPendingQty,
            If sProcess = "MOULD" Then
                Dim daUpdJCWIP As New SqlDataAdapter("Update JobcardWIP Set MouldCompletedQty = '" & nQuantity & _
                                                     "', MouldPendingQty = '" & nBalQty & _
                                                     "' Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdJCWIP As New DataSet
                daUpdJCWIP.Fill(dsUpdJCWIP)
                dsUpdJCWIP.AcceptChanges()
            ElseIf sProcess = "FINISH" Then
                Dim daUpdJCWIP As New SqlDataAdapter("Update JobcardWIP Set FinCompletedQty = '" & nQuantity & _
                                                     "', FinPendingQty = '" & nBalQty & _
                                                     "' Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                Dim dsUpdJCWIP As New DataSet
                daUpdJCWIP.Fill(dsUpdJCWIP)
                dsUpdJCWIP.AcceptChanges()
            End If
        Next
        MsgBox("Completed")
    End Sub

    Private Sub UpdateAuditDatabase()

        Dim daSelPackingDetail As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                                     "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelPackingDetail As New DataSet
        daSelPackingDetail.Fill(dsSelPackingDetail)

        mystrPackingDetail.ID = dsSelPackingDetail.Tables(0).Rows(0).Item("ID").ToString
        mystrPackingDetail.JobCardNo = dsSelPackingDetail.Tables(0).Rows(0).Item("JobCardNo").ToString
        mystrPackingDetail.PackingDate = dsSelPackingDetail.Tables(0).Rows(0).Item("PackingDate").ToString
        mystrPackingDetail.BuyerGroupCode = dsSelPackingDetail.Tables(0).Rows(0).Item("BuyerGroupCode").ToString
        mystrPackingDetail.BuyerCode = dsSelPackingDetail.Tables(0).Rows(0).Item("BuyerCode").ToString
        mystrPackingDetail.Shipper = dsSelPackingDetail.Tables(0).Rows(0).Item("Shipper").ToString
        'mystrPackingDetail.InvoiceNo = dsSelPackingDetail.Tables(0).Rows(0).Item("InvoiceNo").ToString
        'mystrPackingDetail.ArticleGroup = dsSelPackingDetail.Tables(0).Rows(0).Item("ArticleGroup").ToString
        mystrPackingDetail.Article = dsSelPackingDetail.Tables(0).Rows(0).Item("Article").ToString
        'mystrPackingDetail.ColorCode = dsSelPackingDetail.Tables(0).Rows(0).Item("ColorCode").ToString
        'mystrPackingDetail.LeatherCode = dsSelPackingDetail.Tables(0).Rows(0).Item("LeatherCode").ToString
        mystrPackingDetail.CartonNo = dsSelPackingDetail.Tables(0).Rows(0).Item("CartonNo").ToString
        mystrPackingDetail.Quantity = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity").ToString
        'mystrPackingDetail.Unit = dsSelPackingDetail.Tables(0).Rows(0).Item("Unit").ToString
        'mystrPackingDetail.Weight = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("Weight").ToString)
        mystrPackingDetail.Size01 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size01").ToString
        mystrPackingDetail.Quantity01 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity01").ToString
        mystrPackingDetail.Size02 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size02").ToString
        mystrPackingDetail.Quantity02 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity02").ToString
        mystrPackingDetail.Size03 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size03").ToString
        mystrPackingDetail.Quantity03 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity03").ToString
        mystrPackingDetail.Size04 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size04").ToString
        mystrPackingDetail.Quantity04 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity04").ToString
        mystrPackingDetail.Size05 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size05").ToString
        mystrPackingDetail.Quantity05 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity05").ToString
        mystrPackingDetail.Size06 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size06").ToString
        mystrPackingDetail.Quantity06 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity06").ToString
        mystrPackingDetail.Size07 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size07").ToString
        mystrPackingDetail.Quantity07 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity07").ToString
        mystrPackingDetail.Size08 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size08").ToString
        mystrPackingDetail.Quantity08 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity08").ToString
        mystrPackingDetail.Size09 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size09").ToString
        mystrPackingDetail.Quantity09 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity09").ToString
        mystrPackingDetail.Size10 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size10").ToString
        mystrPackingDetail.Quantity10 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity10").ToString
        mystrPackingDetail.Size11 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size11").ToString
        mystrPackingDetail.Quantity11 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity11").ToString
        mystrPackingDetail.Size12 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size12").ToString
        mystrPackingDetail.Quantity12 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity12").ToString
        mystrPackingDetail.Size13 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size13").ToString
        mystrPackingDetail.Quantity13 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity13").ToString
        mystrPackingDetail.Size14 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size14").ToString
        mystrPackingDetail.Quantity14 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity14").ToString
        mystrPackingDetail.Size15 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size15").ToString
        mystrPackingDetail.Quantity15 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity15").ToString
        mystrPackingDetail.Size16 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size16").ToString
        mystrPackingDetail.Quantity16 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity16").ToString
        mystrPackingDetail.Size17 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size17").ToString
        mystrPackingDetail.Quantity17 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity17").ToString
        mystrPackingDetail.Size18 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size18").ToString
        mystrPackingDetail.Quantity18 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity18").ToString
        mystrPackingDetail.EnteredOnMachineID = dsSelPackingDetail.Tables(0).Rows(0).Item("EnteredOnMachineID").ToString
        mystrPackingDetail.CreatedBy = dsSelPackingDetail.Tables(0).Rows(0).Item("CreatedBy").ToString
        mystrPackingDetail.CreatedDate = dsSelPackingDetail.Tables(0).Rows(0).Item("CreatedDate").ToString
        mystrPackingDetail.ModifiedBy = dsSelPackingDetail.Tables(0).Rows(0).Item("ModifiedBy").ToString
        mystrPackingDetail.ModifiedDate = Date.Now ''dsSelPackingDetail.Tables(0).Rows(0).Item("ModifiedDate").ToString
        'mystrPackingDetail.sVariant = dsSelPackingDetail.Tables(0).Rows(0).Item("Variant").ToString
        'mystrPackingDetail.CustomerStyleNo = dsSelPackingDetail.Tables(0).Rows(0).Item("CustomerStyleNo").ToString
        mystrPackingDetail.ExeVersionNo = dsSelPackingDetail.Tables(0).Rows(0).Item("ExeVersionNo").ToString
        'mystrPackingDetail.IsApproved = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsApproved").ToString)
        'mystrPackingDetail.ApprovedBy = dsSelPackingDetail.Tables(0).Rows(0).Item("ApprovedBy").ToString
        'mystrPackingDetail.ApprovedOn = Date.Now 'Format((dsSelPackingDetail.Tables(0).Rows(0).Item("ApprovedOn").ToString), "dd-MMM-yy")
        mystrPackingDetail.ModuleName = dsSelPackingDetail.Tables(0).Rows(0).Item("ModuleName").ToString
        'mystrPackingDetail.IsPacked = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsPacked").ToString)
        'mystrPackingDetail.DCCartonNo = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("DCCartonNo").ToString)
        mystrPackingDetail.UpdateMode = "Modified" ''dsSelPackingDetail.Tables(0).Rows(0).Item("UpdateMode").ToString
        'mystrPackingDetail.PackingNo = dsSelPackingDetail.Tables(0).Rows(0).Item("PackingNo").ToString
        'mystrPackingDetail.Location = dsSelPackingDetail.Tables(0).Rows(0).Item("Location").ToString
        'mystrPackingDetail.PackingListNo = dsSelPackingDetail.Tables(0).Rows(0).Item("PackingListNo").ToString
        'mystrPackingDetail.JobCardDetailsID = dsSelPackingDetail.Tables(0).Rows(0).Item("JobCardDetailsID").ToString
        'mystrPackingDetail.SalesOrderDetailID = dsSelPackingDetail.Tables(0).Rows(0).Item("SalesOrderDetailID").ToString
        'mystrPackingDetail.AssortmentID = dsSelPackingDetail.Tables(0).Rows(0).Item("AssortmentID").ToString
        'mystrPackingDetail.OrderNo = dsSelPackingDetail.Tables(0).Rows(0).Item("OrderNo").ToString
        'mystrPackingDetail.SalesOrderNo = dsSelPackingDetail.Tables(0).Rows(0).Item("SalesOrderNo").ToString
        'mystrPackingDetail.InvoiceID = dsSelPackingDetail.Tables(0).Rows(0).Item("InvoiceID").ToString
        'mystrPackingDetail.IsAssorted = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsAssorted").ToString)
        'mystrPackingDetail.MaterialCode = dsSelPackingDetail.Tables(0).Rows(0).Item("MaterialCode").ToString
        'mystrPackingDetail.CartonCBM = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("CartonCBM").ToString)
        'mystrPackingDetail.BarCode = dsSelPackingDetail.Tables(0).Rows(0).Item("BarCode").ToString

        If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("MouldScanDate")) <> True Then
            mystrPackingDetail.MouldScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("MouldScanDate").ToString
        End If

        If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("FinishScanDate")) <> True Then
            mystrPackingDetail.FinishScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("FinishScanDate").ToString
        End If
        mystrPackingDetail.IsMouldUpdate = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsMouldUpdate").ToString)
        mystrPackingDetail.IsFinishUpdate = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsFinishUpdate").ToString)
        If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("MtoFScanDate")) <> True Then
            mystrPackingDetail.MtoFScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("MtoFScanDate").ToString
        End If
        If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("FtoPScanDate")) <> True Then
            mystrPackingDetail.FtoPScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("FtoPScanDate").ToString
        End If

        mystrPackingDetail.WIPLocation = dsSelPackingDetail.Tables(0).Rows(0).Item("WIPLocation").ToString
        mystrPackingDetail.ReadyToDispatch = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("ReadyToDispatch").ToString)
        If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("ReadyToDispatchDate")) <> True Then
            mystrPackingDetail.ReadyToDispatchDate = dsSelPackingDetail.Tables(0).Rows(0).Item("ReadyToDispatchDate").ToString
        End If

        myccProductionByProcess.INSPKGDTLINAUDIT(mystrPackingDetail)

    End Sub

    Dim sPSWorkOrderNo, sPSStage, sPSArticleNo, sPSSize As String
    Dim nPSQuantity As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click

        sPartQuantity = "N"
    End Sub

    Private Sub UpdateNotePad()

        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then
                writer.WriteLine(Trim(tbBarcode.Text) + " | " + sStatus + " | " & DateTime.Now)
            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub

    Private Sub tbBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBarcode.TextChanged

    End Sub

    Private Sub LoadPackingList()

        Dim i As Integer = 0
        grdArticleMaster.BringToFront()

Ab:
        ngrdRowCount = grdArticleMasterV1.RowCount
        For i = 0 To ngrdRowCount
            grdArticleMasterV1.DeleteRow(i)
        Next
        ngrdRowCount = grdArticleMasterV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy") ''Format(DateAdd(DateInterval.Day, 1, dpTo.Value), "dd-MMMM-yyyy")

        grdArticleMaster.DataSource = myccInvoice.LoadAllCartons(sJobcardNo)

        With grdArticleMasterV1

            

        End With
        sIsLoaded = "Y"

    End Sub
End Class