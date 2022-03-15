Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO




Public Class frmProductionEntries

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sConstrAudit As String = Global.SolarERPForSGM.My.Settings.AHGroupAudit '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnnAudit As New SqlConnection(sConstrAudit)


    Dim keyascii As Integer

    Dim myccProductionByProcess As New ccProductionByProcess
    Dim mystrProductionByProcess As New strProductionByProcess
    Dim mystrPartquantityproduction As New strPartquantityproduction
    Dim mystrPackingDetail As New strPackingDetail

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
        'End
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
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


    Dim sJobcardNo, sProcess As String

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
                        tbStatus.Text = "Invalid Machine Code"
                        UpdateNotePad()
                        tbStatus.ForeColor = Color.Red
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
                        tbStatus.Text = "Invalid Machine Code"
                        tbStatus.ForeColor = Color.Red
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
                    tbStatus.Text = "Section Not Selected"
                Else
                    tbStatus.Text = "Section Not Selected"
                End If
                tbStatus.ForeColor = Color.Red
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If


            If Trim(tbBarcode.Text) = "PART QTY" Then
                plPartQtyInfo.Visible = True
                plPartQtyInfo.BringToFront()
                sPartQuantity = "Y"
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            ElseIf Trim(tbBarcode.Text) = "FULL QTY" Then
                plPartQtyInfo.Visible = False
                sPartQuantity = "N"
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If


            tbLastScannedBarcode.Text = Trim(tbBarcode.Text)

            If sPartQuantity = "Y" Then
                LoadSizeInfo()
                Exit Sub
            End If

            BarcodeSettings()

            JobcardVerification()

            If Microsoft.VisualBasic.Left(sArticle, 10) = "SOL-SYN-EV" Or Microsoft.VisualBasic.Left(sArticle, 10) = "SOL-LEA-EV" Then
                If sEVAProcess = "" Then
                    tbStatus.Text = "Process Not Defined"
                    UpdateNotePad()
                    tbBarcode.Clear()
                    tbBarcode.Focus()
                    Exit Sub
                End If
                EVAProductionQuantityUpdates()
            ElseIf Microsoft.VisualBasic.Mid(sArticle, 9, 2) = "RU" Then
                If sEVAProcess = "" Then
                    tbStatus.Text = "Process Not Defined"
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
            tbStatus.Text = "Invalid Jobcard No."
            UpdateNotePad()
            tbBarcode.Clear()
            tbBarcode.Focus()
        ElseIf dsSelJobcardInfo.Tables(0).Rows.Count > 1 Then
            MsgBox("Invalid Jobcard No. Multiple Jobcards Existing")
            tbStatus.Text = "Invalid Jobcard No. Multiple Jobcards Existing"
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
                tbStatus.Text = "WIP NOT PLANNED"
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
            tbStatus.Text = "Invalid Box No"
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
                    tbStatus.Text = "Scanned Already"
                    tbStatus.ForeColor = Color.Red
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
                    tbStatus.Text = "Mould 2 Finish Not Done"
                    tbStatus.ForeColor = Color.Red
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
                            tbStatus.Text = "Part Quantity. In Complete"
                            tbStatus.ForeColor = Color.Red
                            UpdateNotePad()
                            tbBarcode.Clear()
                            Exit Sub
                        End If
                    End If
                End If

                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("FinishScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set FinishScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                 "', WIPLocation = 'PACKING', ReadyToDispatch = '0', Status = 'IN STOCK' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    tbStatus.Text = "Scanned Already"
                    tbStatus.ForeColor = Color.Red
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
                    tbStatus.Text = "Mould Production Not Done"
                    tbStatus.ForeColor = Color.Red
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
                        tbStatus.Text = "Scanned Already"
                        tbStatus.ForeColor = Color.Red
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

        tbStatus.Text = "Successfully Updated"
        tbStatus.ForeColor = Color.Green
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
                    tbStatus.Text = "Scanned Already"
                    tbStatus.ForeColor = Color.Red
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
                    tbStatus.Text = "Mould Production Not Done"
                    tbStatus.ForeColor = Color.Red
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
                            tbStatus.Text = "Part Quantity. In Complete"
                            tbStatus.ForeColor = Color.Red
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
                    tbStatus.Text = "Scanned Already"
                    tbStatus.ForeColor = Color.Red
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
                    tbStatus.Text = "Mould Production Not Done"
                    tbStatus.ForeColor = Color.Red
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
                        tbStatus.Text = "Scanned Already"
                        tbStatus.ForeColor = Color.Red
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

        tbStatus.Text = "Successfully Updated"
        tbStatus.ForeColor = Color.Green
        UpdateNotePad()
        tbBarcode.Clear()
        LoadJWWIPInfo()
    End Sub

    Private Sub Clear()
        tbMouldStartDate.Clear() : tbMouldCompleted.Clear() : tbMouldPending.Clear() : tbMouldEndDate.Clear()

        tbFinishingStartDate.Clear() : tbFinishingCompleted.Clear() : tbFinishingPending.Clear() : tbFinishingPending.Clear()

        tbPackStartDate.Clear() : tbPackCompleted.Clear() : tbPackPending.Clear() : tbPackEndDate.Clear()

        tbMouldJCCount.Clear() : tbMouldBoxCount.Clear() : tbMouldProductionSum.Clear()

        tbMouldSectionJCCount.Clear() : tbMouldSectionBoxCount.Clear() : tbMouldSectionProductionSum.Clear()

        tbM2FJCCount.Clear() : tbM2FBoxCount.Clear() : tbM2FProductionSum.Clear()

        tbFinishSectionJCCount.Clear() : tbFinishSectionBoxCount.Clear() : tbFinishSectionProductionSum.Clear()

        tbFinishingJCCount.Clear() : tbFinishingBoxCount.Clear() : tbFinishingProductionSum.Clear()

        tbPackJCCount.Clear() : tbPackBoxCount.Clear() : tbPackProductionSum.Clear()

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

        StockInfo()


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
        StockInfo()
        sPartQuantity = "N"
    End Sub

    Dim nPartBoxNo As Integer

    Private Sub StockInfo()
        Dim daSelMouldQty As New SqlDataAdapter("Select  IsNull(Count(Distinct WorkOrderNo),0) As JCCount,IsNull(Sum(Quantity),0) As Prod From ProductionByProcess Where Convert(Date, ProcessDate) = '" & Format(Date.Now, "dd-MMM-yy") & _
                                                "' And ModuleName = 'Barcode Scanning' And ProcessName = 'MOULD' And MachineNo <> 'M2F'", sConstr)
        Dim dsSelMouldQty As New DataSet
        daSelMouldQty.Fill(dsSelMouldQty)

        tbMouldJCCount.Text = Val(dsSelMouldQty.Tables(0).Rows(0).Item("JCCount").ToString)
        tbMouldProductionSum.Text = Val(dsSelMouldQty.Tables(0).Rows(0).Item("Prod").ToString)

        Dim daSelMouldBoxCount As New SqlDataAdapter("Select IsNull(Count(MouldScanDate),0) As BoxCount  from PackingDetail Where Convert(Date, MouldScanDate) = '" & Format(Date.Now, "dd-MMM-yy") & "'", sConstr)
        Dim dsSelMouldBoxCount As New DataSet
        daSelMouldBoxCount.Fill(dsSelMouldBoxCount)

        tbMouldBoxCount.Text = Val(dsSelMouldBoxCount.Tables(0).Rows(0).Item("BoxCount").ToString)


        Dim daSelMouldSectionQty As New SqlDataAdapter("Select  IsNull(Count(Distinct WorkOrderNo),0) As JCCount,IsNull(Sum(Quantity),0) As Prod From ProductStock Where ModuleName = 'Barcode Scanning' And Stage = 'MOULD' And Quantity > 0", sConstr)
        Dim dsSelMouldSectionQty As New DataSet
        daSelMouldSectionQty.Fill(dsSelMouldSectionQty)

        tbMouldSectionJCCount.Text = Val(dsSelMouldSectionQty.Tables(0).Rows(0).Item("JCCount").ToString)
        tbMouldSectionProductionSum.Text = Val(dsSelMouldSectionQty.Tables(0).Rows(0).Item("Prod").ToString)

        Dim daSelMouldSectionBoxCount As New SqlDataAdapter("Select IsNull(Count(ID),0) As BoxCount from PackingDetail Where WIPLocation = 'MOULD'", sConstr)
        Dim dsSelMouldSectionBoxCount As New DataSet
        daSelMouldSectionBoxCount.Fill(dsSelMouldSectionBoxCount)

        tbMouldSectionBoxCount.Text = Val(dsSelMouldSectionBoxCount.Tables(0).Rows(0).Item("BoxCount").ToString)

        Dim daSelM2FQty As New SqlDataAdapter("Select  IsNull(Count(Distinct JobCardNo),0) As JCCount,IsNull(Sum(Quantity),0) As Prod From PackingDetail Where Convert(Date, MtoFScanDate) = '" & Format(Date.Now, "dd-MMM-yy") & _
                                               "'", sConstr)
        Dim dsSelM2FQty As New DataSet
        daSelM2FQty.Fill(dsSelM2FQty)

        tbM2FJCCount.Text = Val(dsSelM2FQty.Tables(0).Rows(0).Item("JCCount").ToString)
        tbM2FProductionSum.Text = Val(dsSelM2FQty.Tables(0).Rows(0).Item("Prod").ToString)

        Dim daSelM2FBoxCount As New SqlDataAdapter("Select IsNull(Count(MtoFScanDate),0) As BoxCount  from PackingDetail Where Convert(Date, MtoFScanDate) = '" & Format(Date.Now, "dd-MMM-yy") & "'", sConstr)
        Dim dsSelM2FBoxCount As New DataSet
        daSelM2FBoxCount.Fill(dsSelM2FBoxCount)

        tbM2FBoxCount.Text = Val(dsSelM2FBoxCount.Tables(0).Rows(0).Item("BoxCount").ToString)


        Dim daSelFinishSectionQty As New SqlDataAdapter("Select  IsNull(Count(Distinct WorkOrderNo),0) As JCCount,IsNull(Sum(Quantity),0) As Prod From ProductStock Where ModuleName = 'Barcode Scanning' And Stage = 'FINISH' And Quantity > 0", sConstr)
        Dim dsSelFinishSectionQty As New DataSet
        daSelFinishSectionQty.Fill(dsSelFinishSectionQty)

        tbFinishSectionJCCount.Text = Val(dsSelFinishSectionQty.Tables(0).Rows(0).Item("JCCount").ToString)
        tbFinishSectionProductionSum.Text = Val(dsSelFinishSectionQty.Tables(0).Rows(0).Item("Prod").ToString)

        Dim daSelFinishSectionBoxCount As New SqlDataAdapter("Select IsNull(Count(ID),0) As BoxCount from PackingDetail Where WIPLocation = 'FINISH'", sConstr)
        Dim dsSelFinishSectionBoxCount As New DataSet
        daSelFinishSectionBoxCount.Fill(dsSelFinishSectionBoxCount)

        tbFinishSectionBoxCount.Text = Val(dsSelFinishSectionBoxCount.Tables(0).Rows(0).Item("BoxCount").ToString)


        Dim daSelFinishQty As New SqlDataAdapter("Select  IsNull(Count(Distinct WorkOrderNo),0) As JCCount,IsNull(Sum(Quantity),0) As Prod From ProductionByProcess Where Convert(Date, ProcessDate) = '" & Format(Date.Now, "dd-MMM-yy") & _
                                                "' And ModuleName = 'Barcode Scanning' And ProcessName = 'FINISH'", sConstr)
        Dim dsSelFinishQty As New DataSet
        daSelFinishQty.Fill(dsSelFinishQty)

        tbFinishingJCCount.Text = Val(dsSelFinishQty.Tables(0).Rows(0).Item("JCCount").ToString)
        tbFinishingProductionSum.Text = Val(dsSelFinishQty.Tables(0).Rows(0).Item("Prod").ToString)

        Dim daSelFinishBoxCount As New SqlDataAdapter("Select IsNull(Count(FinishScanDate),0) As BoxCount  from PackingDetail Where Convert(Date, FinishScanDate) = '" & Format(Date.Now, "dd-MMM-yy") & "'", sConstr)
        Dim dsSelFinishBoxCount As New DataSet
        daSelFinishBoxCount.Fill(dsSelFinishBoxCount)

        tbFinishingBoxCount.Text = Val(dsSelFinishBoxCount.Tables(0).Rows(0).Item("BoxCount").ToString)


        Dim daSelPackSectionQty As New SqlDataAdapter("Select  IsNull(Count(Distinct WorkOrderNo),0) As JCCount,IsNull(Sum(Quantity),0) As Prod From ProductStock Where ModuleName = 'Barcode Scanning' And Stage = 'PACKING'  And Quantity > 0", sConstr)
        Dim dsSelPackSectionQty As New DataSet
        daSelPackSectionQty.Fill(dsSelPackSectionQty)

        tbPackJCCount.Text = Val(dsSelPackSectionQty.Tables(0).Rows(0).Item("JCCount").ToString)
        tbPackProductionSum.Text = Val(dsSelPackSectionQty.Tables(0).Rows(0).Item("Prod").ToString)

        Dim daSelPackSectionBoxCount As New SqlDataAdapter("Select IsNull(Count(ID),0) As BoxCount from PackingDetail Where WIPLocation = 'PACKING'", sConstr)
        Dim dsSelPackSectionBoxCount As New DataSet
        daSelPackSectionBoxCount.Fill(dsSelPackSectionBoxCount)

        tbPackBoxCount.Text = Val(dsSelPackSectionBoxCount.Tables(0).Rows(0).Item("BoxCount").ToString)

    End Sub

    Private Sub LoadSizeInfo()

        nSizeCount = 0

        plPartQtyInfo.Visible = True
        plPartQtyInfo.BringToFront()

        tbQty01.ReadOnly = True : tbQty02.ReadOnly = True : tbQty03.ReadOnly = True : tbQty04.ReadOnly = True : tbQty05.ReadOnly = True
        tbQty06.ReadOnly = True : tbQty07.ReadOnly = True : tbQty08.ReadOnly = True : tbQty09.ReadOnly = True : tbQty10.ReadOnly = True
        tbQty11.ReadOnly = True : tbQty12.ReadOnly = True : tbQty13.ReadOnly = True : tbQty14.ReadOnly = True : tbQty15.ReadOnly = True
        tbQty16.ReadOnly = True : tbQty17.ReadOnly = True : tbQty18.ReadOnly = True

        sJobcardNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 13)
        sSalesOrderNo = Microsoft.VisualBasic.Left(Trim(tbBarcode.Text), 9)
        nBoxnoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(tbBarcode.Text, nBoxnoLength - 1)

        Dim daSelJobCardInfo As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo = '" & sJobcardNo & "'", sConstr)
        Dim dsSelJobcardInfo As New DataSet
        daSelJobCardInfo.Fill(dsSelJobcardInfo)

        If dsSelJobcardInfo.Tables(0).Rows.Count <= 0 Then
            MsgBox("Invalid Jobcard No.")
            tbStatus.Text = "Invalid Jobcard No."
            UpdateNotePad()
            tbBarcode.Clear()
            tbBarcode.Focus()
        ElseIf dsSelJobcardInfo.Tables(0).Rows.Count > 1 Then
            MsgBox("Invalid Jobcard No. Multiple Jobcards Existing")
            tbStatus.Text = "Invalid Jobcard No. Multiple Jobcards Existing"
            UpdateNotePad()
            tbBarcode.Clear()
            tbBarcode.Focus()
        Else

            lblSize01.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size01").ToString
            lblSize02.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size02").ToString
            lblSize03.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size03").ToString
            lblSize04.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size04").ToString
            lblSize05.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size05").ToString
            lblSize06.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size06").ToString
            lblSize07.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size07").ToString
            lblSize08.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size08").ToString
            lblSize09.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size09").ToString
            lblSize10.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size10").ToString
            lblSize11.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size11").ToString
            lblSize12.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size12").ToString
            lblSize13.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size13").ToString
            lblSize14.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size14").ToString
            lblSize15.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size15").ToString
            lblSize16.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size16").ToString
            lblSize17.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size17").ToString
            lblSize18.Text = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size18").ToString

            Dim daSelPkgDtl As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                                  "' And CartonNo = '" & nBoxNo & "'", sConstr)
            Dim dsSelPkgDtl As New DataSet
            daSelPkgDtl.Fill(dsSelPkgDtl)

            tbJCQty01.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity01").ToString)
            tbJCQty02.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity02").ToString)
            tbJCQty03.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity03").ToString)
            tbJCQty04.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity04").ToString)
            tbJCQty05.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity05").ToString)
            tbJCQty06.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity06").ToString)
            tbJCQty07.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity07").ToString)
            tbJCQty08.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity08").ToString)
            tbJCQty09.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity09").ToString)
            tbJCQty10.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity10").ToString)
            tbJCQty11.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity11").ToString)
            tbJCQty12.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity12").ToString)
            tbJCQty13.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity13").ToString)
            tbJCQty14.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity14").ToString)
            tbJCQty15.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity15").ToString)
            tbJCQty16.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity16").ToString)
            tbJCQty17.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity17").ToString)
            tbJCQty18.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity18").ToString)
            tbJCTotalQty.Text = Val(dsSelPkgDtl.Tables(0).Rows(0).Item("Quantity").ToString)

            If Val(tbJCQty01.Text) = 0 Then : tbJCQty01.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty01.ReadOnly = False : sPartQtySize = lblSize01.Text : End If
            If Val(tbJCQty02.Text) = 0 Then : tbJCQty02.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty02.ReadOnly = False : sPartQtySize = lblSize02.Text : End If
            If Val(tbJCQty03.Text) = 0 Then : tbJCQty03.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty03.ReadOnly = False : sPartQtySize = lblSize03.Text : End If
            If Val(tbJCQty04.Text) = 0 Then : tbJCQty04.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty04.ReadOnly = False : sPartQtySize = lblSize04.Text : End If
            If Val(tbJCQty05.Text) = 0 Then : tbJCQty05.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty05.ReadOnly = False : sPartQtySize = lblSize05.Text : End If
            If Val(tbJCQty06.Text) = 0 Then : tbJCQty06.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty06.ReadOnly = False : sPartQtySize = lblSize06.Text : End If
            If Val(tbJCQty07.Text) = 0 Then : tbJCQty07.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty07.ReadOnly = False : sPartQtySize = lblSize07.Text : End If
            If Val(tbJCQty08.Text) = 0 Then : tbJCQty08.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty08.ReadOnly = False : sPartQtySize = lblSize08.Text : End If
            If Val(tbJCQty09.Text) = 0 Then : tbJCQty09.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty09.ReadOnly = False : sPartQtySize = lblSize09.Text : End If
            If Val(tbJCQty10.Text) = 0 Then : tbJCQty10.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty10.ReadOnly = False : sPartQtySize = lblSize10.Text : End If
            If Val(tbJCQty11.Text) = 0 Then : tbJCQty11.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty11.ReadOnly = False : sPartQtySize = lblSize11.Text : End If
            If Val(tbJCQty12.Text) = 0 Then : tbJCQty12.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty12.ReadOnly = False : sPartQtySize = lblSize12.Text : End If
            If Val(tbJCQty13.Text) = 0 Then : tbJCQty13.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty13.ReadOnly = False : sPartQtySize = lblSize13.Text : End If
            If Val(tbJCQty14.Text) = 0 Then : tbJCQty14.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty14.ReadOnly = False : sPartQtySize = lblSize14.Text : End If
            If Val(tbJCQty15.Text) = 0 Then : tbJCQty15.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty15.ReadOnly = False : sPartQtySize = lblSize15.Text : End If
            If Val(tbJCQty16.Text) = 0 Then : tbJCQty16.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty16.ReadOnly = False : sPartQtySize = lblSize16.Text : End If
            If Val(tbJCQty17.Text) = 0 Then : tbJCQty17.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty17.ReadOnly = False : sPartQtySize = lblSize17.Text : End If
            If Val(tbJCQty18.Text) = 0 Then : tbJCQty18.Clear() : Else : nSizeCount = nSizeCount + 1 : tbQty18.ReadOnly = False : sPartQtySize = lblSize18.Text : End If


            If nSizeCount = 1 Then
                Dim daSelSinglePk As New SqlDataAdapter("Select * from Partquantityproduction Where ProcessName = '" & sSection & _
                                                        "' And JobcardNo = '" & sJobcardNo & _
                                                        "' And Size = '" & sPartQtySize & _
                                                        "' And IsSingleSize = '1'", sConstr)
                Dim dsSelSinglePK As New DataSet
                daSelSinglePk.Fill(dsSelSinglePK)

                If dsSelSinglePK.Tables(0).Rows.Count = 2 Then
                    Dim i As Integer = 0
                    For i = 0 To 1
                        nPartBoxNo = Val(dsSelSinglePK.Tables(0).Rows(i).Item("CartonNo").ToString)
                        If nPartBoxNo = nBoxNo Then
                            GoTo Aa
                        End If
                    Next
                    tbStatus.Text = "Part Qty Exceeded(2 Boxes/Size)"
                    tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If
            End If
Aa:

            Dim daSelPkdDtl As New SqlDataAdapter("Select * from Partquantityproduction Where JobcardNo = '" & sJobcardNo & _
                                                  "' And CartonNo = '" & nBoxNo & _
                                                  "' And ProcessName = '" & sProcess & "'", sConstr)
            Dim dsSelPkdDtl As New DataSet
            daSelPkdDtl.Fill(dsSelPkdDtl)

            If dsSelPkdDtl.Tables(0).Rows.Count = 0 Then
                tbPkd01.Clear() : tbPkd02.Clear() : tbPkd03.Clear() : tbPkd04.Clear() : tbPkd05.Clear() : tbPkd06.Clear() : tbPkd07.Clear() : tbPkd08.Clear() : tbPkd09.Clear()
                tbPkd10.Clear() : tbPkd11.Clear() : tbPkd12.Clear() : tbPkd13.Clear() : tbPkd14.Clear() : tbPkd15.Clear() : tbPkd16.Clear() : tbPkd17.Clear() : tbPkd18.Clear()
                tbPkdTotal.Clear()
            Else
                tbPkd01.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity01").ToString)
                tbPkd02.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity02").ToString)
                tbPkd03.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity03").ToString)
                tbPkd04.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity04").ToString)
                tbPkd05.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity05").ToString)
                tbPkd06.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity06").ToString)
                tbPkd07.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity07").ToString)
                tbPkd08.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity08").ToString)
                tbPkd09.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity09").ToString)
                tbPkd10.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity10").ToString)
                tbPkd11.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity11").ToString)
                tbPkd12.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity12").ToString)
                tbPkd13.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity13").ToString)
                tbPkd14.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity14").ToString)
                tbPkd15.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity15").ToString)
                tbPkd16.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity16").ToString)
                tbPkd17.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity17").ToString)
                tbPkd18.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("Quantity18").ToString)
                tbPkdTotal.Text = Val(dsSelPkdDtl.Tables(0).Rows(0).Item("PartQuantity").ToString)
            End If

            If Val(tbPkd01.Text) = 0 Then : tbPkd01.Clear() : End If
            If Val(tbPkd02.Text) = 0 Then : tbPkd02.Clear() : End If
            If Val(tbPkd03.Text) = 0 Then : tbPkd03.Clear() : End If
            If Val(tbPkd04.Text) = 0 Then : tbPkd04.Clear() : End If
            If Val(tbPkd05.Text) = 0 Then : tbPkd05.Clear() : End If
            If Val(tbPkd06.Text) = 0 Then : tbPkd06.Clear() : End If
            If Val(tbPkd07.Text) = 0 Then : tbPkd07.Clear() : End If
            If Val(tbPkd08.Text) = 0 Then : tbPkd08.Clear() : End If
            If Val(tbPkd09.Text) = 0 Then : tbPkd09.Clear() : End If
            If Val(tbPkd10.Text) = 0 Then : tbPkd10.Clear() : End If
            If Val(tbPkd11.Text) = 0 Then : tbPkd11.Clear() : End If
            If Val(tbPkd12.Text) = 0 Then : tbPkd12.Clear() : End If
            If Val(tbPkd13.Text) = 0 Then : tbPkd13.Clear() : End If
            If Val(tbPkd14.Text) = 0 Then : tbPkd14.Clear() : End If
            If Val(tbPkd15.Text) = 0 Then : tbPkd15.Clear() : End If
            If Val(tbPkd16.Text) = 0 Then : tbPkd16.Clear() : End If
            If Val(tbPkd17.Text) = 0 Then : tbPkd17.Clear() : End If
            If Val(tbPkd18.Text) = 0 Then : tbPkd18.Clear() : End If


            If sSection = "FINISH" Then
                Dim daSelMldDtl As New SqlDataAdapter("Select * from Partquantityproduction Where JobcardNo = '" & sJobcardNo & _
                                                  "' And CartonNo = '" & nBoxNo & _
                                                  "' And ProcessName = 'MOULD'", sConstr)
                Dim dsSelMldDtl As New DataSet
                daSelMldDtl.Fill(dsSelMldDtl)

                If dsSelMldDtl.Tables(0).Rows.Count = 0 Then
                    tbMld01.Clear() : tbMld02.Clear() : tbMld03.Clear() : tbMld04.Clear() : tbMld05.Clear() : tbMld06.Clear() : tbMld07.Clear() : tbMld08.Clear() : tbMld09.Clear()
                    tbMld10.Clear() : tbMld11.Clear() : tbMld12.Clear() : tbMld13.Clear() : tbMld14.Clear() : tbMld15.Clear() : tbMld16.Clear() : tbMld17.Clear() : tbMld18.Clear()
                    tbMldTotal.Clear()
                Else
                    tbMld01.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity01").ToString)
                    tbMld02.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity02").ToString)
                    tbMld03.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity03").ToString)
                    tbMld04.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity04").ToString)
                    tbMld05.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity05").ToString)
                    tbMld06.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity06").ToString)
                    tbMld07.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity07").ToString)
                    tbMld08.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity08").ToString)
                    tbMld09.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity09").ToString)
                    tbMld10.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity10").ToString)
                    tbMld11.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity11").ToString)
                    tbMld12.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity12").ToString)
                    tbMld13.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity13").ToString)
                    tbMld14.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity14").ToString)
                    tbMld15.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity15").ToString)
                    tbMld16.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity16").ToString)
                    tbMld17.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity17").ToString)
                    tbMld18.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("Quantity18").ToString)
                    tbMldTotal.Text = Val(dsSelMldDtl.Tables(0).Rows(0).Item("PartQuantity").ToString)
                End If

                If Val(tbMld01.Text) = 0 Then : tbMld01.Clear() : End If
                If Val(tbMld02.Text) = 0 Then : tbMld02.Clear() : End If
                If Val(tbMld03.Text) = 0 Then : tbMld03.Clear() : End If
                If Val(tbMld04.Text) = 0 Then : tbMld04.Clear() : End If
                If Val(tbMld05.Text) = 0 Then : tbMld05.Clear() : End If
                If Val(tbMld06.Text) = 0 Then : tbMld06.Clear() : End If
                If Val(tbMld07.Text) = 0 Then : tbMld07.Clear() : End If
                If Val(tbMld08.Text) = 0 Then : tbMld08.Clear() : End If
                If Val(tbMld09.Text) = 0 Then : tbMld09.Clear() : End If
                If Val(tbMld10.Text) = 0 Then : tbMld10.Clear() : End If
                If Val(tbMld11.Text) = 0 Then : tbMld11.Clear() : End If
                If Val(tbMld12.Text) = 0 Then : tbMld12.Clear() : End If
                If Val(tbMld13.Text) = 0 Then : tbMld13.Clear() : End If
                If Val(tbMld14.Text) = 0 Then : tbMld14.Clear() : End If
                If Val(tbMld15.Text) = 0 Then : tbMld15.Clear() : End If
                If Val(tbMld16.Text) = 0 Then : tbMld16.Clear() : End If
                If Val(tbMld17.Text) = 0 Then : tbMld17.Clear() : End If
                If Val(tbMld18.Text) = 0 Then : tbMld18.Clear() : End If
            End If
        End If
    End Sub

    Private Sub cbSavePartQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSavePartQty.Click

        If Val(tbTotalQty.Text) = 0 Then
            MsgBox("Quantity Not Entered Properly", MsgBoxStyle.Critical)
            tbStatus.Text = "Quantity Not Entered Properly"
            UpdateNotePad()
            Exit Sub
        End If

        sPartQtyExceeded = "N"

        If Val(tbPkd01.Text) + Val(tbQty01.Text) > Val(tbJCQty01.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd02.Text) + Val(tbQty02.Text) > Val(tbJCQty02.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd03.Text) + Val(tbQty03.Text) > Val(tbJCQty03.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd04.Text) + Val(tbQty04.Text) > Val(tbJCQty04.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd05.Text) + Val(tbQty05.Text) > Val(tbJCQty05.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd06.Text) + Val(tbQty06.Text) > Val(tbJCQty06.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd07.Text) + Val(tbQty07.Text) > Val(tbJCQty07.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd08.Text) + Val(tbQty08.Text) > Val(tbJCQty08.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd09.Text) + Val(tbQty09.Text) > Val(tbJCQty09.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd10.Text) + Val(tbQty10.Text) > Val(tbJCQty10.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd11.Text) + Val(tbQty11.Text) > Val(tbJCQty11.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd12.Text) + Val(tbQty12.Text) > Val(tbJCQty12.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd13.Text) + Val(tbQty13.Text) > Val(tbJCQty13.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd14.Text) + Val(tbQty14.Text) > Val(tbJCQty14.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd15.Text) + Val(tbQty15.Text) > Val(tbJCQty15.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd16.Text) + Val(tbQty16.Text) > Val(tbJCQty16.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd17.Text) + Val(tbQty17.Text) > Val(tbJCQty17.Text) Then : sPartQtyExceeded = "Y" : End If
        If Val(tbPkd18.Text) + Val(tbQty18.Text) > Val(tbJCQty18.Text) Then : sPartQtyExceeded = "Y" : End If


        If sSection = "FINISH" Then
            If Val(tbPkd01.Text) + Val(tbQty01.Text) > Val(tbMld01.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd02.Text) + Val(tbQty02.Text) > Val(tbMld02.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd03.Text) + Val(tbQty03.Text) > Val(tbMld03.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd04.Text) + Val(tbQty04.Text) > Val(tbMld04.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd05.Text) + Val(tbQty05.Text) > Val(tbMld05.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd06.Text) + Val(tbQty06.Text) > Val(tbMld06.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd07.Text) + Val(tbQty07.Text) > Val(tbMld07.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd08.Text) + Val(tbQty08.Text) > Val(tbMld08.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd09.Text) + Val(tbQty09.Text) > Val(tbMld09.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd10.Text) + Val(tbQty10.Text) > Val(tbMld10.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd11.Text) + Val(tbQty11.Text) > Val(tbMld11.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd12.Text) + Val(tbQty12.Text) > Val(tbMld12.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd13.Text) + Val(tbQty13.Text) > Val(tbMld13.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd14.Text) + Val(tbQty14.Text) > Val(tbMld14.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd15.Text) + Val(tbQty15.Text) > Val(tbMld15.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd16.Text) + Val(tbQty16.Text) > Val(tbMld16.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd17.Text) + Val(tbQty17.Text) > Val(tbMld17.Text) Then : sPartQtyExceeded = "Y" : End If
            If Val(tbPkd18.Text) + Val(tbQty18.Text) > Val(tbMld18.Text) Then : sPartQtyExceeded = "Y" : End If
        End If
        If sPartQtyExceeded = "Y" Then
            'MsgBox("Part Qty Exceeds the Box Quantity", MsgBoxStyle.Critical
            tbStatus.Text = "Part Qty Exceeds the Box Quantity"
            tbStatus.ForeColor = Color.Red
            UpdateNotePad()
            Exit Sub
        End If


        nBoxnoLength = nStringLength - Microsoft.VisualBasic.Len(sJobcardNo)
        nBoxNo = Microsoft.VisualBasic.Right(tbBarcode.Text, nBoxnoLength - 1)

        Dim daSelBoxInfo As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
                                               "' And CartonNo = '" & nBoxNo & "'", sConstr)
        Dim dsSelBoxInfo As New DataSet
        daSelBoxInfo.Fill(dsSelBoxInfo)

        nPackQty = Val(tbTotalQty.Text)

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

        nQuantity1 = Val(tbQty01.Text)
        nQuantity2 = Val(tbQty02.Text)
        nQuantity3 = Val(tbQty03.Text)
        nQuantity4 = Val(tbQty04.Text)
        nQuantity5 = Val(tbQty05.Text)
        nQuantity6 = Val(tbQty06.Text)
        nQuantity7 = Val(tbQty07.Text)
        nQuantity8 = Val(tbQty08.Text)
        nQuantity9 = Val(tbQty09.Text)
        nQuantity10 = Val(tbQty10.Text)
        nQuantity11 = Val(tbQty11.Text)
        nQuantity12 = Val(tbQty12.Text)
        nQuantity13 = Val(tbQty13.Text)
        nQuantity14 = Val(tbQty14.Text)
        nQuantity15 = Val(tbQty15.Text)
        nQuantity16 = Val(tbQty16.Text)
        nQuantity17 = Val(tbQty17.Text)
        nQuantity18 = Val(tbQty18.Text)

        If sProcess = "Production" Then
            Dim daSelJWWIP As New SqlDataAdapter("Select * from JobcardWIP Where JobcardNo = '" & sJobcardNo & "'", sConstr)
            Dim dsSelJWWIP As New DataSet
            daSelJWWIP.Fill(dsSelJWWIP)

            nMouldCompletedQty = Val(dsSelJWWIP.Tables(0).Rows(0).Item("MouldCompletedQty").ToString)
            nFinishCompletedQty = Val(dsSelJWWIP.Tables(0).Rows(0).Item("FINOutQty").ToString)

            If sSection = "MOULD" Then
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MouldScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MouldScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                 "', WIPLocation = 'MOULD' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    'tbStatus.Text = "Scanned Already"
                    'tbStatus.ForeColor = Color.Red
                    'tbBarcode.Clear1()
                    'Exit Sub123
                End If

                nMouldCompletedQty = nMouldCompletedQty + nPackQty
                nMouldPendingQty = Val(tbJobcardQty.Text) - nMouldCompletedQty
                If IsDBNull(dsSelJWWIP.Tables(0).Rows(0).Item("MouldStartDate")) = True Then
                    Dim daUpdJWWIPMD As New SqlDataAdapter("Update JobcardWIP Set MouldStartDate = '" & Format(Date.Now, "dd-MMM-yyyy  HH:mm:ss") & _
                                                           "'  Where JobcardNo = '" & sJobcardNo & "'", sConstr)
                    Dim dsUpdJWWIPMD As New DataSet
                    daUpdJWWIPMD.Fill(dsUpdJWWIPMD)
                    dsUpdJWWIPMD.AcceptChanges()
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
                    tbStatus.Text = "Mould 2 Finish Not Done"
                    tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("FinishScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set FinishScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                 "', WIPLocation = 'PACKING', ReadyToDispatch = '0', Status = 'IN STOCK' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    'tbStatus.Text = "Scanned Already"
                    'tbStatus.ForeColor = Color.Red
                    'tbBarcode.Clear1()
                    'Exit Sub123
                End If
                nFinishCompletedQty = nFinishCompletedQty + nPackQty
                nFinishPendingQty = Val(tbJobcardQty.Text) - nFinishCompletedQty

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
                    tbStatus.Text = "Mould Production Not Done"
                    tbStatus.ForeColor = Color.Red
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If
                If IsDBNull(dsSelBoxInfo.Tables(0).Rows(0).Item("MToFScanDate")) = True Then
                    Dim daUpdPackingDetail As New SqlDataAdapter("Update PackingDetail Set MToFScanDate = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                                 "', WIPLocation = 'FINISH' Where JobcardNo = '" & sJobcardNo & _
                                                                 "' And CartonNo = '" & nBoxNo & "'", sConstr)
                    Dim dsUpdPackingDetail As New DataSet
                    daUpdPackingDetail.Fill(dsUpdPackingDetail)
                Else
                    'tbStatus.Text = "Scanned Already"
                    'tbStatus.ForeColor = Color.Red
                    'tbBarcode.Clear1()
                    'Exit Sub123
                End If
            End If
            LoadJWWIPInfo()

        End If

        JobcardVerification()
        UpdateProductionByProcess()

        UpdatePartQtyProduction()

        tbStatus.Text = "Successfully Updated"
        tbStatus.ForeColor = Color.Green
        UpdateNotePad()
        tbBarcode.Clear()
        LoadJWWIPInfo()

        ClearPartQtyDetails()
        tbBarcode.Focus()
    End Sub

    Private Sub tbQty01_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbQty01.TextChanged, tbQty02.TextChanged, tbQty03.TextChanged, tbQty04.TextChanged, tbQty05.TextChanged, tbQty06.TextChanged, tbQty07.TextChanged, tbQty08.TextChanged, tbQty09.TextChanged, tbQty10.TextChanged, tbQty11.TextChanged, tbQty12.TextChanged, tbQty13.TextChanged, tbQty14.TextChanged, tbQty15.TextChanged, tbQty16.TextChanged, tbQty17.TextChanged, tbQty18.TextChanged
        tbTotalQty.Text = Val(tbQty01.Text) + Val(tbQty02.Text) + Val(tbQty03.Text) + Val(tbQty04.Text) + Val(tbQty05.Text) + Val(tbQty06.Text) + Val(tbQty07.Text) + Val(tbQty08.Text) + Val(tbQty09.Text) + Val(tbQty10.Text) + Val(tbQty11.Text) + Val(tbQty12.Text) + Val(tbQty13.Text) + Val(tbQty14.Text) + Val(tbQty15.Text) + Val(tbQty16.Text) + Val(tbQty17.Text) + Val(tbQty18.Text)
    End Sub

    Private Sub ClearPartQtyDetails()

        tbJCQty01.Clear() : tbJCQty02.Clear() : tbJCQty03.Clear() : tbJCQty04.Clear() : tbJCQty05.Clear() : tbJCQty06.Clear()
        tbJCQty07.Clear() : tbJCQty08.Clear() : tbJCQty09.Clear() : tbJCQty10.Clear() : tbJCQty11.Clear() : tbJCQty12.Clear()
        tbJCQty13.Clear() : tbJCQty14.Clear() : tbJCQty15.Clear() : tbJCQty16.Clear() : tbJCQty17.Clear() : tbJCQty18.Clear()

        tbPkd01.Clear() : tbPkd02.Clear() : tbPkd03.Clear() : tbPkd04.Clear() : tbPkd05.Clear() : tbPkd06.Clear()
        tbPkd07.Clear() : tbPkd08.Clear() : tbPkd09.Clear() : tbPkd10.Clear() : tbPkd11.Clear() : tbPkd12.Clear()
        tbPkd13.Clear() : tbPkd14.Clear() : tbPkd15.Clear() : tbPkd16.Clear() : tbPkd17.Clear() : tbPkd18.Clear()

        tbQty01.Clear() : tbQty02.Clear() : tbQty03.Clear() : tbQty04.Clear() : tbQty05.Clear() : tbQty06.Clear()
        tbQty07.Clear() : tbQty08.Clear() : tbQty09.Clear() : tbQty10.Clear() : tbQty11.Clear() : tbQty12.Clear()
        tbQty13.Clear() : tbQty14.Clear() : tbQty15.Clear() : tbQty16.Clear() : tbQty17.Clear() : tbQty18.Clear()
    End Sub

    Dim nPartQty01, nPartQty02, nPartQty03, nPartQty04, nPartQty05, nPartQty06, nPartQty07, nPartQty08, nPartQty09 As Integer
    Dim nPartQty10, nPartQty11, nPartQty12, nPartQty13, nPartQty14, nPartQty15, nPartQty16, nPartQty17, nPartQty18 As Integer
    Dim nPartTotalQty As Integer
    Dim sStatus As String

    Private Sub UpdatePartQtyProduction()

        nPartQty01 = Val(tbPkd01.Text) + Val(tbQty01.Text)
        nPartQty02 = Val(tbPkd02.Text) + Val(tbQty02.Text)
        nPartQty03 = Val(tbPkd03.Text) + Val(tbQty03.Text)
        nPartQty04 = Val(tbPkd04.Text) + Val(tbQty04.Text)
        nPartQty05 = Val(tbPkd05.Text) + Val(tbQty05.Text)
        nPartQty06 = Val(tbPkd06.Text) + Val(tbQty06.Text)
        nPartQty07 = Val(tbPkd07.Text) + Val(tbQty07.Text)
        nPartQty08 = Val(tbPkd08.Text) + Val(tbQty08.Text)
        nPartQty09 = Val(tbPkd09.Text) + Val(tbQty09.Text)
        nPartQty10 = Val(tbPkd10.Text) + Val(tbQty10.Text)
        nPartQty11 = Val(tbPkd11.Text) + Val(tbQty11.Text)
        nPartQty12 = Val(tbPkd12.Text) + Val(tbQty12.Text)
        nPartQty13 = Val(tbPkd13.Text) + Val(tbQty13.Text)
        nPartQty14 = Val(tbPkd14.Text) + Val(tbQty14.Text)
        nPartQty15 = Val(tbPkd15.Text) + Val(tbQty15.Text)
        nPartQty16 = Val(tbPkd16.Text) + Val(tbQty16.Text)
        nPartQty17 = Val(tbPkd17.Text) + Val(tbQty17.Text)
        nPartQty18 = Val(tbPkd18.Text) + Val(tbQty18.Text)

        nPartTotalQty = nPartQty01 + nPartQty02 + nPartQty03 + nPartQty04 + nPartQty05 + nPartQty06 + nPartQty07 + nPartQty08 + nPartQty09 + nPartQty10 + nPartQty11 + nPartQty12 + nPartQty13 + nPartQty14 + nPartQty15 + nPartQty16 + nPartQty17 + nPartQty18

        If Val(tbJCTotalQty.Text) = nPartTotalQty Then
            sStatus = "Completed"
        Else
            sStatus = "Pending"
        End If

        mystrPartquantityproduction.ID = System.Guid.NewGuid.ToString()
        mystrPartquantityproduction.CreatedBy = ""
        mystrPartquantityproduction.CreatedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrPartquantityproduction.ModifiedBy = ""
        mystrPartquantityproduction.ModifiedDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrPartquantityproduction.ExeVersionNo = ""
        mystrPartquantityproduction.IsApproved = 0
        mystrPartquantityproduction.ApprovedBy = ""
        mystrPartquantityproduction.ApprovedOn = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
        mystrPartquantityproduction.ModuleName = ""
        mystrPartquantityproduction.JobCardNo = sJobcardNo
        mystrPartquantityproduction.BoxQunatity = Val(tbJCTotalQty.Text)
        mystrPartquantityproduction.CartonNo = nBoxNo
        mystrPartquantityproduction.Status = sStatus
        mystrPartquantityproduction.PartQuantity = nPartTotalQty
        mystrPartquantityproduction.Quantity01 = nPartQty01
        mystrPartquantityproduction.Quantity02 = nPartQty02
        mystrPartquantityproduction.Quantity03 = nPartQty03
        mystrPartquantityproduction.Quantity04 = nPartQty04
        mystrPartquantityproduction.Quantity05 = nPartQty05
        mystrPartquantityproduction.Quantity06 = nPartQty06
        mystrPartquantityproduction.Quantity07 = nPartQty07
        mystrPartquantityproduction.Quantity08 = nPartQty08
        mystrPartquantityproduction.Quantity09 = nPartQty09
        mystrPartquantityproduction.Quantity10 = nPartQty10
        mystrPartquantityproduction.Quantity11 = nPartQty11
        mystrPartquantityproduction.Quantity12 = nPartQty12
        mystrPartquantityproduction.Quantity13 = nPartQty13
        mystrPartquantityproduction.Quantity14 = nPartQty14
        mystrPartquantityproduction.Quantity15 = nPartQty15
        mystrPartquantityproduction.Quantity16 = nPartQty16
        mystrPartquantityproduction.Quantity17 = nPartQty17
        mystrPartquantityproduction.Quantity18 = nPartQty18
        mystrPartquantityproduction.ProcessName = sSection
        If nSizeCount = 1 Then
            mystrPartquantityproduction.IsSingleSize = 1
        Else
            mystrPartquantityproduction.IsSingleSize = 0
        End If
        mystrPartquantityproduction.Size = sPartQtySize

        myccProductionByProcess.InsertPartProduction(mystrPartquantityproduction)
        UpdateAuditDatabase()
    End Sub

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

        'Dim daSelPackingDetail As New SqlDataAdapter("Select * from PackingDetail Where JobcardNo = '" & sJobcardNo & _
        '                                             "' And CartonNo = '" & nBoxNo & "'", sConstr)
        'Dim dsSelPackingDetail As New DataSet
        'daSelPackingDetail.Fill(dsSelPackingDetail)

        'mystrPackingDetail.ID = dsSelPackingDetail.Tables(0).Rows(0).Item("ID").ToString
        'mystrPackingDetail.JobCardNo = dsSelPackingDetail.Tables(0).Rows(0).Item("JobCardNo").ToString
        'mystrPackingDetail.PackingDate = dsSelPackingDetail.Tables(0).Rows(0).Item("PackingDate").ToString
        'mystrPackingDetail.BuyerGroupCode = dsSelPackingDetail.Tables(0).Rows(0).Item("BuyerGroupCode").ToString
        'mystrPackingDetail.BuyerCode = dsSelPackingDetail.Tables(0).Rows(0).Item("BuyerCode").ToString
        'mystrPackingDetail.Shipper = dsSelPackingDetail.Tables(0).Rows(0).Item("Shipper").ToString
        ''mystrPackingDetail.InvoiceNo = dsSelPackingDetail.Tables(0).Rows(0).Item("InvoiceNo").ToString
        ''mystrPackingDetail.ArticleGroup = dsSelPackingDetail.Tables(0).Rows(0).Item("ArticleGroup").ToString
        'mystrPackingDetail.Article = dsSelPackingDetail.Tables(0).Rows(0).Item("Article").ToString
        ''mystrPackingDetail.ColorCode = dsSelPackingDetail.Tables(0).Rows(0).Item("ColorCode").ToString
        ''mystrPackingDetail.LeatherCode = dsSelPackingDetail.Tables(0).Rows(0).Item("LeatherCode").ToString
        'mystrPackingDetail.CartonNo = dsSelPackingDetail.Tables(0).Rows(0).Item("CartonNo").ToString
        'mystrPackingDetail.Quantity = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity").ToString
        ''mystrPackingDetail.Unit = dsSelPackingDetail.Tables(0).Rows(0).Item("Unit").ToString
        ''mystrPackingDetail.Weight = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("Weight").ToString)
        'mystrPackingDetail.Size01 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size01").ToString
        'mystrPackingDetail.Quantity01 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity01").ToString
        'mystrPackingDetail.Size02 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size02").ToString
        'mystrPackingDetail.Quantity02 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity02").ToString
        'mystrPackingDetail.Size03 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size03").ToString
        'mystrPackingDetail.Quantity03 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity03").ToString
        'mystrPackingDetail.Size04 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size04").ToString
        'mystrPackingDetail.Quantity04 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity04").ToString
        'mystrPackingDetail.Size05 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size05").ToString
        'mystrPackingDetail.Quantity05 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity05").ToString
        'mystrPackingDetail.Size06 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size06").ToString
        'mystrPackingDetail.Quantity06 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity06").ToString
        'mystrPackingDetail.Size07 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size07").ToString
        'mystrPackingDetail.Quantity07 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity07").ToString
        'mystrPackingDetail.Size08 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size08").ToString
        'mystrPackingDetail.Quantity08 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity08").ToString
        'mystrPackingDetail.Size09 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size09").ToString
        'mystrPackingDetail.Quantity09 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity09").ToString
        'mystrPackingDetail.Size10 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size10").ToString
        'mystrPackingDetail.Quantity10 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity10").ToString
        'mystrPackingDetail.Size11 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size11").ToString
        'mystrPackingDetail.Quantity11 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity11").ToString
        'mystrPackingDetail.Size12 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size12").ToString
        'mystrPackingDetail.Quantity12 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity12").ToString
        'mystrPackingDetail.Size13 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size13").ToString
        'mystrPackingDetail.Quantity13 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity13").ToString
        'mystrPackingDetail.Size14 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size14").ToString
        'mystrPackingDetail.Quantity14 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity14").ToString
        'mystrPackingDetail.Size15 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size15").ToString
        'mystrPackingDetail.Quantity15 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity15").ToString
        'mystrPackingDetail.Size16 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size16").ToString
        'mystrPackingDetail.Quantity16 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity16").ToString
        'mystrPackingDetail.Size17 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size17").ToString
        'mystrPackingDetail.Quantity17 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity17").ToString
        'mystrPackingDetail.Size18 = dsSelPackingDetail.Tables(0).Rows(0).Item("Size18").ToString
        'mystrPackingDetail.Quantity18 = dsSelPackingDetail.Tables(0).Rows(0).Item("Quantity18").ToString
        'mystrPackingDetail.EnteredOnMachineID = dsSelPackingDetail.Tables(0).Rows(0).Item("EnteredOnMachineID").ToString
        'mystrPackingDetail.CreatedBy = dsSelPackingDetail.Tables(0).Rows(0).Item("CreatedBy").ToString
        'mystrPackingDetail.CreatedDate = dsSelPackingDetail.Tables(0).Rows(0).Item("CreatedDate").ToString
        'mystrPackingDetail.ModifiedBy = dsSelPackingDetail.Tables(0).Rows(0).Item("ModifiedBy").ToString
        'mystrPackingDetail.ModifiedDate = Date.Now ''dsSelPackingDetail.Tables(0).Rows(0).Item("ModifiedDate").ToString
        ''mystrPackingDetail.sVariant = dsSelPackingDetail.Tables(0).Rows(0).Item("Variant").ToString
        ''mystrPackingDetail.CustomerStyleNo = dsSelPackingDetail.Tables(0).Rows(0).Item("CustomerStyleNo").ToString
        'mystrPackingDetail.ExeVersionNo = dsSelPackingDetail.Tables(0).Rows(0).Item("ExeVersionNo").ToString
        ''mystrPackingDetail.IsApproved = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsApproved").ToString)
        ''mystrPackingDetail.ApprovedBy = dsSelPackingDetail.Tables(0).Rows(0).Item("ApprovedBy").ToString
        ''mystrPackingDetail.ApprovedOn = Date.Now 'Format((dsSelPackingDetail.Tables(0).Rows(0).Item("ApprovedOn").ToString), "dd-MMM-yy")
        'mystrPackingDetail.ModuleName = dsSelPackingDetail.Tables(0).Rows(0).Item("ModuleName").ToString
        ''mystrPackingDetail.IsPacked = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsPacked").ToString)
        ''mystrPackingDetail.DCCartonNo = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("DCCartonNo").ToString)
        'mystrPackingDetail.UpdateMode = "Modified" ''dsSelPackingDetail.Tables(0).Rows(0).Item("UpdateMode").ToString
        ''mystrPackingDetail.PackingNo = dsSelPackingDetail.Tables(0).Rows(0).Item("PackingNo").ToString
        ''mystrPackingDetail.Location = dsSelPackingDetail.Tables(0).Rows(0).Item("Location").ToString
        ''mystrPackingDetail.PackingListNo = dsSelPackingDetail.Tables(0).Rows(0).Item("PackingListNo").ToString
        ''mystrPackingDetail.JobCardDetailsID = dsSelPackingDetail.Tables(0).Rows(0).Item("JobCardDetailsID").ToString
        ''mystrPackingDetail.SalesOrderDetailID = dsSelPackingDetail.Tables(0).Rows(0).Item("SalesOrderDetailID").ToString
        ''mystrPackingDetail.AssortmentID = dsSelPackingDetail.Tables(0).Rows(0).Item("AssortmentID").ToString
        ''mystrPackingDetail.OrderNo = dsSelPackingDetail.Tables(0).Rows(0).Item("OrderNo").ToString
        ''mystrPackingDetail.SalesOrderNo = dsSelPackingDetail.Tables(0).Rows(0).Item("SalesOrderNo").ToString
        ''mystrPackingDetail.InvoiceID = dsSelPackingDetail.Tables(0).Rows(0).Item("InvoiceID").ToString
        ''mystrPackingDetail.IsAssorted = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsAssorted").ToString)
        ''mystrPackingDetail.MaterialCode = dsSelPackingDetail.Tables(0).Rows(0).Item("MaterialCode").ToString
        ''mystrPackingDetail.CartonCBM = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("CartonCBM").ToString)
        ''mystrPackingDetail.BarCode = dsSelPackingDetail.Tables(0).Rows(0).Item("BarCode").ToString

        'If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("MouldScanDate")) <> True Then
        '    mystrPackingDetail.MouldScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("MouldScanDate").ToString
        'End If

        'If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("FinishScanDate")) <> True Then
        '    mystrPackingDetail.FinishScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("FinishScanDate").ToString
        'End If
        'mystrPackingDetail.IsMouldUpdate = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsMouldUpdate").ToString)
        'mystrPackingDetail.IsFinishUpdate = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("IsFinishUpdate").ToString)
        'If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("MtoFScanDate")) <> True Then
        '    mystrPackingDetail.MtoFScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("MtoFScanDate").ToString
        'End If
        'If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("FtoPScanDate")) <> True Then
        '    mystrPackingDetail.FtoPScanDate = dsSelPackingDetail.Tables(0).Rows(0).Item("FtoPScanDate").ToString
        'End If

        'mystrPackingDetail.WIPLocation = dsSelPackingDetail.Tables(0).Rows(0).Item("WIPLocation").ToString
        'mystrPackingDetail.ReadyToDispatch = Val(dsSelPackingDetail.Tables(0).Rows(0).Item("ReadyToDispatch").ToString)
        'If IsDBNull(dsSelPackingDetail.Tables(0).Rows(0).Item("ReadyToDispatchDate")) <> True Then
        '    mystrPackingDetail.ReadyToDispatchDate = dsSelPackingDetail.Tables(0).Rows(0).Item("ReadyToDispatchDate").ToString
        'End If

        'myccProductionByProcess.INSPKGDTLINAUDIT(mystrPackingDetail)

        Dim daInsPkgDtlinAudit As New SqlDataAdapter("Insert Into PackingDetail SELECT ID, JobCardNo, PackingDate, BuyerGroupCode, BuyerCode, Shipper, InvoiceNo, ArticleGroup, Article, ColorCode, LeatherCode, CartonNo, Quantity, Unit, Weight, Size01, Quantity01, Size02, Quantity02, Size03, Quantity03, Size04, Quantity04, Size05, Quantity05, Size06, Quantity06, Size07, Quantity07, Size08, Quantity08, Size09, Quantity09, Size10, Quantity10, Size11, Quantity11, Size12, Quantity12, Size13, Quantity13, Size14, Quantity14, Size15, Quantity15, Size16, Quantity16, Size17, Quantity17, Size18, Quantity18, EnteredOnMachineID, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, Variant, CustomerStyleNo, ExeVersionNo, IsApproved, ApprovedBy, ApprovedOn, ModuleName, IsPacked, DCCartonNo, 'Modified', PackingNo, Location, PackingListNo, JobCardDetailsID, SalesOrderDetailID, AssortmentID, OrderNo, SalesOrderNo, InvoiceID, IsAssorted, MaterialCode, CartonCBM, BarCode, MouldScanDate, FinishScanDate, IsMouldUpdate, IsFinishUpdate, MtoFScanDate, FtoPScanDate, WIPLocation, ReadyToDispatch, ReadyToDispatchDate, InvYear, MouldQuantity, FinishedQuantity, PackedQuantity, InvoicedQuantity, PackingCartonNo,Remarks,Status FROM  AHGroup_SSPL.dbo.PackingDetail WHERE        (JobCardNo = '" & sJobcardNo & "') AND (CartonNo = '" & nBoxNo & "')", sConstrAudit)
        Dim dsInsPkgDtlinAudit As New DataSet
        daInsPkgDtlinAudit.Fill(dsInsPkgDtlinAudit)
        dsInsPkgDtlinAudit.AcceptChanges()
    End Sub

    Dim sPSWorkOrderNo, sPSStage, sPSArticleNo, sPSSize As String
    Dim nPSQuantity As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        StockInfo()
        sPartQuantity = "N"
    End Sub

    Private Sub UpdateNotePad()

        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then
                writer.WriteLine(Trim(tbBarcode.Text) + " | " + Trim(tbStatus.Text) + " | " & DateTime.Now)
            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub

    Private Sub tbBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBarcode.TextChanged

    End Sub
End Class