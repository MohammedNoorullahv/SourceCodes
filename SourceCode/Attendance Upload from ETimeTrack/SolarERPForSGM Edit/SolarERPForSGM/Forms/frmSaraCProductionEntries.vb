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




Public Class frmSaraCProductionEntries

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
    Dim sPartQuantity, sReworkCode As String

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
    Dim sIsRework As String = "N"

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


    Dim sJobcardNo, sProcess, sProcessCode As String

    Dim nStringLength, nBoxNo, nBoxnoLength As Integer
    Dim sBeginsWith, sDescription, sStage, sStageType As String
    Dim nStageSequenceNo, nExistingStage, nPreviousSequenceNo As Decimal

    Private Sub tbBarcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbBarcode.KeyPress

        keyascii = AscW(e.KeyChar)
        If keyascii = 13 Then


            If Trim(tbBarcode.Text) = "EXIT" Then
                End
            End If

            If Trim(tbBarcode.Text) = "REWORK ENTRIES" Or Trim(tbBarcode.Text) = "REEN" Then
                frmSaraCReworkEntries.Visible = True
                frmSaraCReworkEntries.BringToFront()
                Me.Hide()
                Exit Sub
            End If

            tbLastScannedBarcode.Clear()

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
                    LoadConveyorProduction()
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

                    LoadConveyorProduction()
                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "STA" Then
                    sStation = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sStationCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    'tbStationNo.Text = sStation


                End If
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
            Else
                If Trim(tbBarcode.Text).ToUpper = "RE WORK" Or Trim(tbBarcode.Text).ToUpper = "REWK" Then
                    sIsRework = "Y"
                    tbStage.BackColor = Color.Red
                    tbStage.ForeColor = Color.White
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                ElseIf Trim(tbBarcode.Text).ToUpper = "PRODUCTION" Or Trim(tbBarcode.Text).ToUpper = "PROD" Then
                    sIsRework = "N"
                    tbStage.BackColor = Color.White
                    tbStage.ForeColor = Color.Black
                    UpdateNotePad()
                    tbBarcode.Clear()
                    Exit Sub
                End If


            End If

            If sprocess = "" Or sMachine = "" Then
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

            If Len(tbBarcode.Text) = 22 Then
            Else
                If Len(tbBarcode.Text) <> 22 And sIsRework = "N" Then

                    tbStatus.Text = "Invalid Barcode No."
                    UpdateNotePad()
                    tbBarcode.Clear()
                    tbBarcode.Focus()
                    Exit Sub

                Else
                    If sIsRework = "Y" Then
                        Dim daSelFaultCode As New SqlDataAdapter("Select * from AbbrevTable where Group_ = 'UPPERPRODUCTIONQC' And Abbrev_ = '" & Trim(tbBarcode.Text) & "'", sConstr)
                        Dim dsSelFaultCode As New DataSet
                        daSelFaultCode.Fill(dsSelFaultCode)

                        If dsSelFaultCode.Tables(0).Rows.Count = 0 Or dsSelFaultCode.Tables(0).Rows.Count > 1 Then
                            tbStatus.Text = "Invalid Fault Code"
                            UpdateNotePad()
                            tbBarcode.Clear()
                            tbBarcode.Focus()
                            Exit Sub
                        Else
                            sReworkCode = Trim(tbBarcode.Text)
                            tbFaultDescription.Text = dsSelFaultCode.Tables(0).Rows(0).Item("FullName_").ToString
                        End If
                    End If
                End If
            End If

            tbTotalScanned.Text = Val(tbTotalScanned.Text) + 1

            grdBarcodeStatus.DataSource = myccSARACProduction.BarcodeStatus(Trim(tbBarcode.Text))
            grdBarcodeStatus.Visible = True
            grdBarcodeStatus.BringToFront()


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
                nExistingStage = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("CurrentStage").ToString)

                nSize = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("Size"))
                sJobcardNo = dsSelJobcardInfo.Tables(0).Rows(0).Item("JobcardNo").ToString
                sJobcardDetailID = dsSelJobcardInfo.Tables(0).Rows(0).Item("JobcardDetailId").ToString

                If sIsRework = "N" Then

                    If Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("IsInRework").ToString) * -1 = 1 Then
                        tbStatus.Text = "In Rework"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
                        GoTo Aa
                    End If

                    If nExistingStage = 0 And nPreviousSequenceNo = 0 Then
                    ElseIf nExistingStage = nPreviousSequenceNo Then

                    ElseIf nExistingStage = nStageSequenceNo Then
                        tbStatus.Text = "This Pair Already Scanned for this Stage"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
                        GoTo Aa
                    ElseIf nExistingStage > nStageSequenceNo Then
                        tbStatus.Text = "This Pair is not available in this Stage"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
                        GoTo Aa
                    ElseIf nExistingStage < nStageSequenceNo Then
                        tbStatus.Text = "This Pair is not available in this Stage"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
                        GoTo Aa
                    ElseIf nExistingStage <> nPreviousSequenceNo Then
                        tbStatus.Text = "Already Scanned"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
                        GoTo Aa
                    Else
                        tbStatus.Text = "Invalid,UnAsigned Stage"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        tbStatus.ForeColor = Color.Red
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

                Dim daSelFromStage As New SqlDataAdapter("Select * from StagesManualEntry where SequenceNo = '" & nPreviousSequenceNo & _
                                                               "' And StageType = 'MOCASSIN'", sConstr)
                Dim dsSelFromStage As New DataSet
                daSelFromStage.Fill(dsSelFromStage)

                If dsSelFromStage.Tables(0).Rows.Count = 0 Then
                    sFromStageCode = "WIP"
                Else
                    sFromStageCode = dsSelFromStage.Tables(0).Rows(0).Item("StageCode").ToString
                    sFromStageDescription = dsSelFromStage.Tables(0).Rows(0).Item("StageDescription").ToString
                End If

                sComponentGroup = "UPPER"
                sLeatherCode = dsSelSOD.Tables(0).Rows(0).Item("MainRawMaterialCode").ToString
                sArticleDetailId = dsSelSOD.Tables(0).Rows(0).Item("ArticleDetailID").ToString
                sCustomerOrderNo = dsSelSOD.Tables(0).Rows(0).Item("CustomerOrderNo").ToString

                Dim daSelArticle As New SqlDataAdapter("Select * from ArticleMaster Where ID IN (Select ArticleId From ArticleDetail Where ID = '" & sArticleDetailId & "')", sConstr)
                Dim dsSelArticle As New DataSet
                daSelArticle.Fill(dsSelArticle)

                sStageType = dsSelArticle.Tables(0).Rows(0).Item("ProductionName").ToString


                End If

                Dim daSelRework As New SqlDataAdapter("Select * from UpRework Where Barcode = '" & Trim(tbBarcode.Text) & _
                                                      "' Order by ProcessDate Desc", sConstr)
                Dim dsSelRework As New DataSet
                daSelRework.Fill(dsSelRework)

                If dsSelRework.Tables(0).Rows.Count = 0 Then
                Else
                If Val(dsSelRework.Tables(0).Rows(0).Item("IsReworkCompleted")) * -1 = 0 Then
                    tbStatus.Text = "Already in Rework"
                    GoTo Aa
                End If

                End If

                If sIsRework = "Y" Then
                    If nPreviousSequenceNo <> nExistingStage Then
                        tbStatus.Text = "Not Available for this Status"
                        GoTo Aa
                    End If
                    If sReworkCode = "" Then
                        tbStatus.Text = "Fault Code Not Selected"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        Exit Sub
                    End If
                    InsertUPRework()
                    Dim daUPDJCDPP As New SqlDataAdapter("Update JobcardDetailPerPair Set IsInRework = '1' Where Barcode = '" & Trim(tbBarcode.Text) & "'", sConstr)
                    Dim dsUPDJCDPP As New DataSet
                    daUPDJCDPP.Fill(dsUPDJCDPP)
                    dsUPDJCDPP.AcceptChanges()

                    tbBarcode.Clear()
                    tbFaultDescription.Clear() : sReworkCode = ""
                Else
                    ProductionQuantityUpdates()
                End If

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


    Private Sub frmProductionEntries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadSummary()
        'MultipleScreensForSARAC()
    End Sub



    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static intCurrentChar As Int32 = 0

        'Dim font As New Font("Verdana", 8)

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
        'sPrintingMessage = Trim(tbBarcode.Text) + " | " + Trim(tbJobcardNo.Text)
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
        'sPrintingMessage = Trim(tbBarcode.Text) + " | " + Trim(tbJobcardNo.Text)
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


    Dim sPSWorkOrderNo, sPSStage, sPSArticleNo, sPSSize As String
    Dim nPSQuantity As Integer

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click

        'LoadSummary()
    End Sub

    Private Sub UpdateNotePad()

        'Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        'Using writer As New StreamWriter(filePath, True)
        '    If File.Exists(filePath) Then
        '        writer.WriteLine(Trim(tbBarcode.Text) + " | " + Trim(tbStatus.Text) + " | " & DateTime.Now)
        '    Else
        '        writer.WriteLine("Start Error Log for today")
        '    End If
        'End Using
    End Sub

    Private Sub tbBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbBarcode.TextChanged

    End Sub

    Dim nHourCount As Integer
    Private Sub ProductionQuantityUpdates()


        nHourCount = Format(Date.Now, "HH") - 8

        'If Format(Date.Now, "mm") < 30 Then
        '    nHourCount = nHourCount - 1
        'End If

                Dim daUpdJCDPPCI As New SqlDataAdapter("Update JobCardDetailPerPair Set Stage = '" & sStage & _
                                                       "', Revisioncount = '" & nHourCount & _
                                                       "', InConveyor = '" & sMachineCode & _
                                                       "', ConveyorInDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                       "', ConveyorInUpdated = '0', InDateString = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                       "', CurrentStage = '" & nStageSequenceNo & _
                                                       "' where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
                Dim dsUpdJCDPPCI As New DataSet
                daUpdJCDPPCI.Fill(dsUpdJCDPPCI)
                dsUpdJCDPPCI.AcceptChanges()

        InsertUPProduction()


                tbStatus.Text = "Successfully Scanned"
                tbStatus.ForeColor = Color.Green
                tbBarcode.Clear()

        UpdateProductionByProcess()
        UpdateProductStock()

    End Sub

    Dim nHour, nTotalProduction As Integer
    Dim sConveyor As String

    Dim nExistingQuantity As Integer
    Dim SPBPID As String
    Private Sub UpdateProductionByProcess()
        Try
            Dim daSelPBP As New SqlDataAdapter("Select * from UPProductionByProcess Where ProcessName = '" & sProcessName & _
                                               "' And ProcessDate = '" & Format(dProcessDate.Date, "dd-MMM-yyyy") & _
                                               "' And MachineNo = '" & sMachineCode & _
                                               "' And WorkOrderNo = '" & sWorkOrderNo & _
                                               "' And Size= '" & sSize & "'", sConstr)
            Dim dsSelPBP As New DataSet
            daSelPBP.Fill(dsSelPBP)

            If dsSelPBP.Tables(0).Rows.Count = 0 Then
                sID = System.Guid.NewGuid.ToString()
                nExistingQuantity = 1
                Dim daInsPBP As New SqlDataAdapter("Insert Into UPProductionByProcess(ID,ProcessName,ProcessDate,ShiftNo,MachineNo,SalesOrderNo,Article,Variant,ArticleGroup,MaterialCode,Size,Pcs,Quantity,Unit,CompanyCode,Color,WorkOrderNo,LocationName,BuyerCode,JobCardDetailID,Location,FromLocation,CurrentQuantity,FromStage,ComponentGroup,LeatherCode,ArticleDetailId,CustomerOrderNo) Values ('" & sID & _
                                                   "','" & sProcessName & "','" & Format(dProcessDate.Date, "dd-MMM-yyyy") & "','" & sShiftCode & "','" & sMachineCode & _
                                                   "','" & sSalesOrderNo & "','" & sArticle & "','" & sVariant & "','" & sArticleGroup & _
                                                   "','" & sMaterialCode & "','" & sSize & "','" & nPcs & "','" & nExistingQuantity & "','" & sUnit & _
                                                   "','" & sCompanyCode & "','" & sColor & "','" & sWorkOrderNo & "','" & sLocationName & _
                                                   "','" & sBuyerCode & "','" & sJobcardDetailID & "','" & sLocation & "','" & sFromLocation & _
                                                   "','" & nExistingQuantity & "','" & sFromStageDescription & "','" & sComponentGroup & "','" & sLeatherCode & _
                                                   "','" & sArticleDetailId & "','" & sCustomerOrderNo & "')", sConstr)
                Dim dsInsPBP As New DataSet
                daInsPBP.Fill(dsInsPBP)
                dsInsPBP.AcceptChanges()
            ElseIf dsSelPBP.Tables(0).Rows.Count = 1 Then
                SPBPID = dsSelPBP.Tables(0).Rows(0).Item("ID").ToString
                nExistingQuantity = Val(dsSelPBP.Tables(0).Rows(0).Item("Quantity").ToString) + 1

                Dim daUpdPBP As New SqlDataAdapter("Update UPProductionByProcess Set Quantity = '" & nExistingQuantity & _
                                                   "', CurrentQuantity = '" & nExistingQuantity & _
                                                   "' Where ID = '" & SPBPID & "'", sConstr)
                Dim dsUpdPBP As New DataSet
                daUpdPBP.Fill(dsUpdPBP)
                dsUpdPBP.AcceptChanges()
            Else
                MsgBox("To Check ProductionByProcess", MsgBoxStyle.Critical)
            End If

            grdJobCardProduction.DataSource = myccSARACProduction.LoadJobcardProduction(sStageType, sJobcardNo)
            With grdJobCardProductionV1
                .Columns(0).VisibleIndex = -1
            End With
            grdJobCardProduction.Visible = False
        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub UpdateProductStock()
        Try
            Dim daSelPSCS As New SqlDataAdapter("Select * from UPProductStock Where Stage = '" & sProcessName & _
                                               "' And WorkOrderNo = '" & sWorkOrderNo & _
                                               "' And Size= '" & sSize & "'", sConstr)
            Dim dsSelPSCS As New DataSet
            daSelPSCS.Fill(dsSelPSCS)

            If dsSelPSCS.Tables(0).Rows.Count = 0 Then
                nExistingQuantity = 1
                sID = System.Guid.NewGuid.ToString()
                Dim daInsPS As New SqlDataAdapter("Insert Into UPProductStock(ID,Location,Stage,SalesOrderNo,WorkOrderNo,	ArticleNo,	Quantity,	Unit,	CompanyCode,	Color,	Size,	Variant,	LocationName,	BuyerGroup,	ComponentGroup,	LeatherCode,	MaterialCode,	JobcardDetailID) Values('" & sID & _
                                                  "','" & sMachineCode & "','" & sProcessName & "','" & sSalesOrderNo & "','" & sWorkOrderNo & _
                                                  "','" & sArticle & "','" & nExistingQuantity & "','" & sUnit & "','" & sCompanyCode & _
                                                  "','" & sColor & "','" & sSize & "','" & sVariant & "','" & sLocationName & _
                                                  "','" & sBuyerCode & "','" & sComponentGroup & "','" & sLeatherCode & _
                                                  "','" & sMaterialCode & "','" & sJobcardDetailID & "')", sConstr)
                Dim dsInsPS As New DataSet
                daInsPS.Fill(dsInsPS)
                dsInsPS.AcceptChanges()
            ElseIf dsSelPSCS.Tables(0).Rows.Count = 1 Then
                sID = dsSelPSCS.Tables(0).Rows(0).Item("ID").ToString
                nExistingQuantity = Val(dsSelPSCS.Tables(0).Rows(0).Item("Quantity").ToString) + 1

                Dim daUpdPS As New SqlDataAdapter("Update UPProductStock Set Quantity = '" & nExistingQuantity & _
                                                  "' Where ID = '" & sID & "'", sConstr)
                Dim dsUpdPS As New DataSet
                daUpdPS.Fill(dsUpdPS)
                dsUpdPS.AcceptChanges()
            Else
                MsgBox("To Check ProductStock", MsgBoxStyle.Critical)
            End If

            Dim daSelPSPS As New SqlDataAdapter("Select * from UPProductStock Where Stage = '" & sFromStageCode & _
                                                "' And WorkOrderNo = '" & sWorkOrderNo & _
                                                "' And Size= '" & sSize & "'", sConstr)
            Dim dsSelPSPS As New DataSet
            daSelPSPS.Fill(dsSelPSPS)


            If dsSelPSPS.Tables(0).Rows.Count = 1 Then
                sID = dsSelPSPS.Tables(0).Rows(0).Item("ID").ToString
                nExistingQuantity = Val(dsSelPSPS.Tables(0).Rows(0).Item("Quantity").ToString) - 1
                Dim daUpdPS As New SqlDataAdapter("Update UPProductStock Set Quantity = '" & nExistingQuantity & _
                                                  "' Where ID = '" & sID & "'", sConstr)
                Dim dsUpdPS As New DataSet
                daUpdPS.Fill(dsUpdPS)
                dsUpdPS.AcceptChanges()
            ElseIf dsSelPSPS.Tables(0).Rows.Count = 0 Then

            Else
                MsgBox("To Check ProductStock", MsgBoxStyle.Critical)
            End If

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub InsertUPProduction()
        Try
            sID = System.Guid.NewGuid.ToString()
            Dim daInsUPP As New SqlDataAdapter("Insert Into UPProduction(ID,Barcode,ProcessName,ProcessDate,DayHour,MachineNo,Quantity,StageType) Values('" & sID & _
                                               "','" & Trim(tbBarcode.Text) & "','" & sProcessName & _
                                               "','" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                               "','" & nHourCount & "','" & sMachineCode & "','1','" & sStageType & "')", sConstr)
            Dim dsInsUPP As New DataSet
            daInsUPP.Fill(dsInsUPP)
            dsInsUPP.AcceptChanges()

            LoadConveyorProduction()

            

            

        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

    Private Sub MultipleScreensForSARAC()
        'TO Update after Attaching Multiple Screen''
        Dim nUpperBound As Integer

        Dim Screens() As Screen = screen.AllScreens

        LoadOutstanding()
        ''TO Update after Attaching Multiple Screen''
        'ScreenShotCapture()
    End Sub
    Dim Screen4SARAC As Screen
    Private Sub LoadOutstanding()

        Screen4SARAC = Screen.AllScreens(1)


        frmSaraCPRODSummary.StartPosition = FormStartPosition.Manual
        frmSaraCPRODSummary.Location = Screen4SARAC.Bounds.Location '+ Point(100, 100)
        frmSaraCPRODSummary.Show()


    End Sub

    Private Sub cbClearBuffer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbClearBuffer.Click
        tbTotalScanned.Clear()
        tbCorrectQty.Clear()
        tbWrongQty.Clear()
        tbBarcode.Focus()
    End Sub

   

    Private Sub LoadConveyorProduction()
        grdConveyorProduction.DataSource = myccSARACProduction.LoadConveyorProduction(sStageType, sMachineCode, Format(Date.Now, "dd-MMM-yyyy HH:mm:ss"))

        With grdConveyorProductionV1
            .Columns(0).VisibleIndex = -1
            .Columns(2).VisibleIndex = -1

            .Columns(3).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(4).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(5).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(6).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            .Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        End With
    End Sub

    Private Sub InsertUPRework()
        Try
            Dim nReworkCount As Integer

            Dim daSelReworkCount As New SqlDataAdapter("Select * from UpRework Where Barcode = '" & Trim(tbBarcode.Text) & _
                                                       "' And FromStage <> ''", sConstr)
            Dim dsSelReworkCount As New DataSet
            daSelReworkCount.Fill(dsSelReworkCount)

            nReworkCount = dsSelReworkCount.Tables(0).Rows.Count + 1

            nHourCount = Format(Date.Now, "HH") - 8
            sID = System.Guid.NewGuid.ToString()
            Dim daInsUPP As New SqlDataAdapter("Insert Into UpRework(ID,Barcode,ProcessName,ProcessDate,DayHour,MachineNo,Quantity,StageType,IsReworkCompleted,FromStage,IsRejected,FaultCode,ReWorkCount) Values('" & sID & _
                                               "','" & Trim(tbBarcode.Text) & "','" & sFromStageCode & _
                                               "','" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                               "','" & nHourCount & "','" & sMachineCode & "','1','" & sStageType & _
                                               "','0','" & sProcessName & "','0','" & sReworkCode & "','" & nReworkCount & "')", sConstr)
            Dim dsInsUPP As New DataSet
            daInsUPP.Fill(dsInsUPP)
            dsInsUPP.AcceptChanges()


        Catch Exp As Exception
            HandleException(Me.Name, Exp)
        End Try
    End Sub

End Class