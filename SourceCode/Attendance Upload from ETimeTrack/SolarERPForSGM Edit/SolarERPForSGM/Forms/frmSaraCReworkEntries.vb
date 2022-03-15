Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO
Imports System.IO.Ports
Imports System.Drawing.Rectangle

Imports System.Data.OleDb
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop




Public Class frmSaraCReworkEntries

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)
    Dim keyascii As Integer

    'Dim myccProductionByProcess As New ccProductionByProcess
    'Dim mystrProductionByProcess As New strProductionByProcess
    'Dim mystrPartquantityproduction As New strPartquantityproduction
    'Dim mystrPackingDetail As New strPackingDetail

    Dim myccSARACProduction As New ccSARACProduction

    Dim dMouldingDate, dFinishingDate, dPackingDate As Date
    Dim nMouldCompletedQty, nMouldPendingQty, nFinishCompletedQty, nFinishPendingQty, nPackQty As Integer
    Dim sShiftCode, sShift As String
    Dim sSectionCode, sSection1 As String
    Dim sMachineCode, sMachine, sCompanyCode As String
    Dim sStationCode, sStation As String
    Dim sPartQuantity As String

    Dim sSalesOrderNo, sArticle, sBuyerCode, sBuyer, sJobcardDetailID As String
    Dim sSize1, sSize2, sSize3, sSize4, sSize5, sSize6, sSize7, sSize8, sSize9, sSize10 As String
    Dim sSize11, sSize12, sSize13, sSize14, sSize15, sSize16, sSize17, sSize18, sSize, sPartQtySize As String

    Dim nQuantity1, nQuantity2, nQuantity3, nQuantity4, nQuantity5, nQuantity6, nQuantity7, nQuantity8, nQuantity9, nQuantity10 As Integer
    Dim nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18, nQuantity As Integer

    Dim sFromLocation, sFromStageCode, sFromStageDescription, sPartQtyExceeded As String
    Dim nCurrentQuantity, nLossQuantity, nSizeCount As Integer

    Dim sID, sProcessName, sMachineNo, sArticleGroup, sArticleGroupCode, sMaterialCode, sUnit, sColor, sWorkOrderNo, sLocationName, sSupplierCode, sLocation, sComponentGroup, sLeatherCode, sArticleDetailId, sVariant As String
    Dim dProcessDate As Date
    Dim nPcs As Integer
    Dim sSalesOrderDetailid, sCustomerOrderNo As String

    Dim sIsRework As String
    Dim sReworkProcessName As String
#End Region


    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        frmSaraCProductionEntries.Visible = True
        frmSaraCProductionEntries.BringToFront()
        Me.Hide()
        'Form1.Show() : Form1.BringToFront()
    End Sub

    Dim ngrdRowCount As Integer


    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        MsgBox("Export Completed")
    End Sub


    Dim sJobcardNo, sProcess, sProcessCode As String

    Dim nStringLength, nBoxNo, nBoxnoLength As Integer
    Dim sBeginsWith, sDescription, sStage, sStageType As String
    Dim nStageSequenceNo, nExistingStage, nPreviousSequenceNo As Decimal
    Dim nReWorkCount As Integer
    Private Sub tbBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbBarcode.KeyPress

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then


            If Trim(tbBarcode.Text) = "EXIT" Then
                End
            End If
            tbLastScannedBarcode.Clear()

            If Trim(tbBarcode.Text) = "PRODUCTION ENTRIES" Or Trim(tbBarcode.Text).ToUpper = "PREN" Then
                frmSaraCProductionEntries.Visible = True
                frmSaraCProductionEntries.BringToFront()
                Me.Hide()
                Exit Sub
            End If


            nStringLength = Microsoft.VisualBasic.Len(tbBarcode.Text)

            If Microsoft.VisualBasic.Mid((tbBarcode.Text), 4, 1) = ":" Then
                If Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "SHI" Then
                    sShiftCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sShift = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    tbShift.Text = sShift
                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "STA" Then
                    sProcess = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sProcessCode = sProcess

                    Dim daSelProcessInfo As New SqlDataAdapter("Select * from StagesManualEntry where StageCode = '" & sProcessCode & _
                                                               "' And StageType = 'MOCASSIN'", sConstr)
                    Dim dsSelProcessInfo As New DataSet
                    daSelProcessInfo.Fill(dsSelProcessInfo)

                    If dsSelProcessInfo.Tables(0).Rows.Count = 0 Then
                        tbStatus.Text = "Invalid Stage Code"
                        UpdateNotePad()
                        tbStatus.ForeColor = Color.Red
                        tbBarcode.Clear()
                        sProcessCode = ""
                        tbStage.Clear()
                        tbBarcode.Focus()
                        Exit Sub
                    Else
                        tbStage.Text = dsSelProcessInfo.Tables(0).Rows(0).Item("StageDescription").ToString
                        sStage = dsSelProcessInfo.Tables(0).Rows(0).Item("StageDescription").ToString
                        nStageSequenceNo = Val(dsSelProcessInfo.Tables(0).Rows(0).Item("SequenceNo").ToString)
                        nPreviousSequenceNo = Val(dsSelProcessInfo.Tables(0).Rows(0).Item("PreviousSequenceNo").ToString)
                    End If

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
                        sCompanyCode = dsSelMachine.Tables(0).Rows(0).Item("CompanyCode").ToString
                        sMachine = dsSelMachine.Tables(0).Rows(0).Item("LocationName").ToString
                        tbMachineNo.Text = sMachine

                    End If
                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "STA" Then
                    sStation = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sStationCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    'tbStationNo.Text = sStation


                End If
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If

            If sProcess = "" Or sMachine = "" Then
                'MsgBox("Section / Machine Not Selected", MsgBoxStyle.Critical)
                If sProcess = "" Then
                    tbStatus.Text = "Process Not Selected"
                Else
                    tbStatus.Text = "Machine Not Selected"
                End If
                tbStatus.ForeColor = Color.Red
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            End If

            tbLastScannedBarcode.Text = Trim(tbBarcode.Text)

            If Len(tbBarcode.Text) <> 22 Then
                tbStatus.Text = "Invalid Barcode No."
                UpdateNotePad()
                tbBarcode.Clear()
                tbBarcode.Focus()
                Exit Sub
            End If

            tbTotalScanned.Text = Val(tbTotalScanned.Text) + 1


            Dim daSelJobCardInfo As New SqlDataAdapter("Select * from JobCardDetailPerPair where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
            Dim dsSelJobcardInfo As New DataSet
            daSelJobCardInfo.Fill(dsSelJobcardInfo)

            If dsSelJobcardInfo.Tables(0).Rows.Count <= 0 Then
                tbStatus.Text = "Invalid Jobcard No."
                UpdateNotePad()
                tbBarcode.Clear()
                tbBarcode.Focus()
                tbStatus.ForeColor = Color.Red
                GoTo Aa
            Else

                Dim daSelRWInfo As New SqlDataAdapter("Select * from UpRework Where Barcode = '" & Trim(tbBarcode.Text) & _
                                                      "' And IsReworkCompleted = '0' And IsRejected = '0'", sConstr)
                Dim dsSelRWInfo As New DataSet
                daSelRWInfo.Fill(dsSelRWInfo)

                If dsSelRWInfo.Tables(0).Rows.Count = 0 Then
                    tbStatus.Text = "Rework Not available for this Barcode"
                    UpdateNotePad()
                    tbBarcode.Clear()
                    tbBarcode.Focus()
                    tbStatus.ForeColor = Color.Red
                    GoTo Aa
                ElseIf dsSelRWInfo.Tables(0).Rows(0).Item("AssignedRWStage").ToString = "" Then
                    tbStatus.Text = "Rework Not Assigned for this Barcode"
                    UpdateNotePad()
                    'tbBarcode.Clear()
                    'tbBarcode.Focus()
                    'tbStatus.ForeColor = Color.Red
                    'GoTo Aa
                End If



                nExistingStage = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("CurrentStage").ToString)
                nReWorkCount = Val(dsSelRWInfo.Tables(0).Rows(0).Item("ReworkCount").ToString)

                sReworkProcessName = dsSelRWInfo.Tables(0).Rows(0).Item("ProcessName").ToString
                nSize = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("Size"))
                sJobcardNo = dsSelJobcardInfo.Tables(0).Rows(0).Item("JobcardNo").ToString
                sJobcardDetailID = dsSelJobcardInfo.Tables(0).Rows(0).Item("JobcardDetailId").ToString

                If sIsRework = "Y" Then
                Else

                    If Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("IsInRework")) * -1 = 0 Then
                        tbStatus.Text = "Not In Rework"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
                        GoTo Aa
                    End If


                    If nStageSequenceNo <= nExistingStage Then
                    Else
                        tbStatus.Text = "This Pair is not available in this Stage"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbBarcode.ForeColor = Color.Red
                        GoTo Aa
                    End If

                    Dim daSelRWLS As New SqlDataAdapter("Select * from UpRework Where Barcode = '" & Trim(tbBarcode.Text) & _
                                                        "' And FromStage = '' And ReworkCount = '" & nReWorkCount & "' Order  by ProcessDate Desc", sConstr)
                    Dim dsSelRWLS As New DataSet
                    daSelRWLS.Fill(dsSelRWLS)

                    Dim nRWPreviousSequenceNo As Integer
                    Dim sCurrentRWStage As String
                    If dsSelRWLS.Tables(0).Rows.Count = 0 Then
                        nRWPreviousSequenceNo = 0
                    Else
                        sCurrentRWStage = dsSelRWLS.Tables(0).Rows(0).Item("PROCESSNAME").ToString
                        Dim daSelFromStage4RW As New SqlDataAdapter("Select * from StagesManualEntry where StageCode = '" & sCurrentRWStage & _
                                                               "' And StageType = 'MOCASSIN'", sConstr)
                        Dim dsSelFromStage4RW As New DataSet
                        daSelFromStage4RW.Fill(dsSelFromStage4RW)

                        nRWPreviousSequenceNo = Val(dsSelFromStage4RW.Tables(0).Rows(0).Item("SequenceNo").ToString)
                    End If

                    Dim daSelFromStage As New SqlDataAdapter("Select * from StagesManualEntry where PreviousSequenceNo = '" & nRWPreviousSequenceNo & _
                                                               "' And StageType = 'MOCASSIN'", sConstr)
                    Dim dsSelFromStage As New DataSet
                    daSelFromStage.Fill(dsSelFromStage)

                    If dsSelFromStage.Tables(0).Rows.Count = 0 Then
                        sFromStageCode = "WIP"
                    Else
                        sFromStageCode = dsSelFromStage.Tables(0).Rows(0).Item("StageCode").ToString
                        sFromStageDescription = dsSelFromStage.Tables(0).Rows(0).Item("StageDescription").ToString
                    End If

                    If sFromStageCode <> sProcessCode Then
                        tbStatus.Text = "This Pair is not available in this Stage"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbBarcode.ForeColor = Color.Red
                        GoTo Aa
                    End If

                End If

                Dim daSelJCDtl As New SqlDataAdapter("Select * from JobcardDetail where Id = '" & sJobcardDetailID & "'", sConstr)
                Dim dsSelJCDtl As New DataSet
                daSelJCDtl.Fill(dsSelJCDtl)

                sSalesOrderDetailid = dsSelJCDtl.Tables(0).Rows(0).Item("SalesOrderDetailID").ToString()

                Dim daSelSOD As New SqlDataAdapter("Select * from SalesOrderDetails Where ID = '" & sSalesOrderDetailid & "'", sConstr)
                Dim dsSelSOD As New DataSet
                daSelSOD.Fill(dsSelSOD)

                sID = ""
                sProcessName = sProcessCode
                dProcessDate = Format(Date.Now, "dd-MMM-yyyy HH:mm:ss")
                sShiftCode = ""
                sSalesOrderNo = dsSelJCDtl.Tables(0).Rows(0).Item("SalesOrderNo").ToString
                sArticle = dsSelJCDtl.Tables(0).Rows(0).Item("Article").ToString
                sVariant = dsSelJCDtl.Tables(0).Rows(0).Item("Variants").ToString
                sArticleGroup = dsSelSOD.Tables(0).Rows(0).Item("ArticleGroup").ToString
                sMaterialCode = dsSelSOD.Tables(0).Rows(0).Item("MainRawMaterialCode").ToString
                sSize = dsSelJobcardInfo.Tables(0).Rows(0).Item("Size").ToString
                nPcs = 0
                nQuantity = 1
                sUnit = "PRS"
                sCompanyCode = "SLI"
                sColor = dsSelSOD.Tables(0).Rows(0).Item("ColorCode").ToString
                sWorkOrderNo = sJobcardNo
                sLocationName = ""
                sBuyerCode = dsSelSOD.Tables(0).Rows(0).Item("BuyerGroupCode").ToString
                sSupplierCode = ""
                sLocation = sStage
                sFromLocation = ""
                nCurrentQuantity = 1

                

                sComponentGroup = "UPPER"
                sLeatherCode = dsSelSOD.Tables(0).Rows(0).Item("MainRawMaterialCode").ToString
                sArticleDetailId = dsSelSOD.Tables(0).Rows(0).Item("ArticleDetailID").ToString
                sCustomerOrderNo = dsSelSOD.Tables(0).Rows(0).Item("CustomerOrderNo").ToString

                Dim daSelArticle As New SqlDataAdapter("Select * from ArticleMaster Where ID IN (Select ArticleId From ArticleDetail Where ID = '" & sArticleDetailId & "')", sConstr)
                Dim dsSelArticle As New DataSet
                daSelArticle.Fill(dsSelArticle)

                sStageType = dsSelArticle.Tables(0).Rows(0).Item("ProductionName").ToString


            End If


            If sReworkProcessName = sProcessName Then
                Dim daUPDJCDPP As New SqlDataAdapter("Update JobcardDetailPerPair Set IsInRework = '0' Where Barcode = '" & Trim(tbBarcode.Text) & "'", sConstr)
                Dim dsUPDJCDPP As New DataSet
                daUPDJCDPP.Fill(dsUPDJCDPP)
                dsUPDJCDPP.AcceptChanges()

                Dim daUpdRWCompleted As New SqlDataAdapter("Update UpRework Set IsReworkCompleted = '1', CompletedOn = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & "' Where Barcode = '" & Trim(tbBarcode.Text) & _
                                                           "' And ProcessName = '" & sProcessName & _
                                                           "' And ReworkCount = '" & nReWorkCount & "'", sConstr)
                Dim dsUpdRWCompleted As New DataSet
                daUpdRWCompleted.Fill(dsUpdRWCompleted)
                dsUpdRWCompleted.AcceptChanges()

            Else
                InsertUPRework()
            End If


            tbBarcode.Clear()
            'tbFaultDescription.Clear() : sReworkCode = ""


Aa:
            If tbStatus.Text = "Successfully Scanned" Then
                tbCorrectQty.Text = Val(tbCorrectQty.Text) + 1
            Else
                tbWrongQty.Text = Val(tbWrongQty.Text) + 1
            End If

            'PrintDocument1.Print()

            'tbLastScannedBarcode.Text = tbBarcode.Text
        End If
    End Sub

    Dim nSize As Decimal
    Dim nKittingQty, nConvInQty As Decimal


    Private Sub frmSaraCReworkEntries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadSummary()
        sIsRework = "N"
    End Sub

   
    Private Sub cbClearBuffer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbClearBuffer.Click
        tbTotalScanned.Clear()
        tbCorrectQty.Clear()
        tbWrongQty.Clear()
        tbBarcode.Focus()
    End Sub

 
 
    Private Sub tbBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBarcode.TextChanged

    End Sub
    Private Sub UpdateNotePad()
        mdlSGM.strIPAddress = ""
        'Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then
                writer.WriteLine(Trim(tbBarcode.Text) + " | " + Trim(tbStatus.Text) + " | " & DateTime.Now)
            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub
    Dim nHourCount As Integer
    Private Sub InsertUPRework()
        Try
            nHourCount = Format(Date.Now, "HH") - 8
            sID = System.Guid.NewGuid.ToString()
            Dim daInsUPP As New SqlDataAdapter("Insert Into UpRework(ID,Barcode,ProcessName,ProcessDate,DayHour,MachineNo,Quantity,StageType,IsReworkCompleted,FromStage,IsRejected,FaultCode) Values('" & sID & _
                                               "','" & Trim(tbBarcode.Text) & "','" & sProcessName & _
                                               "','" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                               "','" & nHourCount & "','" & sMachineCode & "','1','" & sStageType & _
                                               "','1','','0','')", sConstr)
            Dim dsInsUPP As New DataSet
            daInsUPP.Fill(dsInsUPP)
            dsInsUPP.AcceptChanges()


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub
End Class