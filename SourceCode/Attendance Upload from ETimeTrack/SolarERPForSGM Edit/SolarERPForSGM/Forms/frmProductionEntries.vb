Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO




Public Class frmProductionEntries

#Region "Declaration"
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)
    Dim keyascii As Integer

    'Dim myccProductionByProcess As New ccProductionByProcess
    'Dim mystrProductionByProcess As New strProductionByProcess
    'Dim mystrPartquantityproduction As New strPartquantityproduction
    'Dim mystrPackingDetail As New strPackingDetail

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


    Dim sJobcardNo, sProcess, sProcessCode As String

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
                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "FPS" Then
                    sProcess = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sProcessCode = sProcess

                    Dim daSelProcessInfo As New SqlDataAdapter("Select * from AbbrevTable where Abbrev_ = '" & sProcessCode & "'", sConstr)
                    Dim dsSelProcessInfo As New DataSet
                    daSelProcessInfo.Fill(dsSelProcessInfo)

                    If dsSelProcessInfo.Tables(0).Rows.Count = 0 Then
                        tbStatus.Text = "Invalid Machine Code"
                        UpdateNotePad()
                        tbStatus.ForeColor = Color.Red
                        tbBarcode.Clear()
                        sProcessCode = ""
                        tbProcess.Clear()
                        tbBarcode.Focus()
                        Exit Sub
                    Else
                        tbProcess.Text = dsSelProcessInfo.Tables(0).Rows(0).Item("FullName_").ToString
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
                        'sSection = dsSelMachine.Tables(0).Rows(0).Item("MaterialTypeCode").ToString
                        'sSectionCode = sSection
                        'tbProcess.Text = sSection

                        'sFromStage = dsSelMachine.Tables(0).Rows(0).Item("BaggingType").ToString

                        'sStation = ""
                        'sStationCode = ""
                        'tbStationNo.Clear()
                    End If


                ElseIf Microsoft.VisualBasic.Left((tbBarcode.Text), 3) = "STA" Then
                    sStation = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    sStationCode = Microsoft.VisualBasic.Right((tbBarcode.Text), nStringLength - 4)
                    tbStationNo.Text = sStation

                
                End If
                UpdateNotePad()
                tbBarcode.Clear()
                Exit Sub
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

           

            Dim daSelJobCardInfo As New SqlDataAdapter("Select * from JobCardDetailPerPair where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
            Dim dsSelJobcardInfo As New DataSet
            daSelJobCardInfo.Fill(dsSelJobcardInfo)

            If dsSelJobcardInfo.Tables(0).Rows.Count <= 0 Then
                tbStatus.Text = "Invalid Jobcard No."
                UpdateNotePad()
                tbBarcode.Clear()
                tbBarcode.Focus()
                Exit Sub
            Else
                If Trim(tbProcess.Text) = "CONVEYOR IN" Then


                    nSize = Val(dsSelJobcardInfo.Tables(0).Rows(0).Item("Size"))
                    sJobcardNo = dsSelJobcardInfo.Tables(0).Rows(0).Item("JobcardNo").ToString
                    Dim daSelKitQty As New SqlDataAdapter("Select IsNull(SUM(Quantity),0) As Quantity from ProductionByProcess where WorkOrderno = '" & sJobcardNo & _
                                                          "' And Cast(Size As Decimal(18,2))  = '" & Val(nSize) & "' And ProcessName = 'KITTING'", sConstr)
                    Dim dsSelKitQty As New DataSet
                    daSelKitQty.Fill(dsSelKitQty)

                    nKittingQty = Val(dsSelKitQty.Tables(0).Rows(0).Item("Quantity"))
                    
                    Dim daSelConvInQty As New SqlDataAdapter("Select IsNull(Count(ID),0) As Quantity from JobcardDetailPerPair where JobcardNo = '" & sJobcardNo & _
                                                             "' And Cast(Size As Decimal(18,2))  = '" & Val(nSize) & "' And ConveyorInDate Is Not Null", sConstr)
                    Dim dsSelConvInQty As New DataSet
                    daSelConvInQty.Fill(dsSelConvInQty)

                    nConvInQty = dsSelConvInQty.Tables(0).Rows(0).Item("Quantity")

                    If nKittingQty <= nConvInQty Then
                        tbStatus.Text = "Kitting Entry Not Done. Update Kitting"
                        UpdateNotePad()
                        tbBarcode.Clear()
                        tbBarcode.Focus()
                        Exit Sub
                    End If
                End If

            End If


            'kghkhjgkh()

            ProductionQuantityUpdates()



            'PrintDocument1.Print()

            'tbLastScannedBarcode.Text = tbBarcode.Text
        End If
    End Sub

    Dim nSize As Decimal
    Dim nKittingQty, nConvInQty As Decimal

    Private Sub Clear()
        
        tb1In1.Clear() : tb1In2.Clear() : tb1In3.Clear() : tb1In4.Clear() : tb1In5.Clear() : tb1In6.Clear() : tb1In7.Clear() : tb1In8.Clear() : tb1In9.Clear() : tb1InT.Clear()
        tb2In1.Clear() : tb2In2.Clear() : tb2In3.Clear() : tb2In4.Clear() : tb2In5.Clear() : tb2In6.Clear() : tb2In7.Clear() : tb2In8.Clear() : tb2In9.Clear() : tb2InT.Clear()
        tb3In1.Clear() : tb3In2.Clear() : tb3In3.Clear() : tb3In4.Clear() : tb3In5.Clear() : tb3In6.Clear() : tb3In7.Clear() : tb3In8.Clear() : tb3In9.Clear() : tb3InT.Clear()
        tbTIn1.Clear() : tbTIn2.Clear() : tbTIn3.Clear() : tbTIn4.Clear() : tbTIn5.Clear() : tbTIn6.Clear() : tbTIn7.Clear() : tbTIn8.Clear() : tbTIn9.Clear() : tbTInT.Clear()

        tb1Out1.Clear() : tb1Out2.Clear() : tb1Out3.Clear() : tb1Out4.Clear() : tb1Out5.Clear() : tb1Out6.Clear() : tb1Out7.Clear() : tb1Out8.Clear() : tb1Out9.Clear() : tb1OutT.Clear()
        tb2Out1.Clear() : tb2Out2.Clear() : tb2Out3.Clear() : tb2Out4.Clear() : tb2Out5.Clear() : tb2Out6.Clear() : tb2Out7.Clear() : tb2Out8.Clear() : tb2Out9.Clear() : tb2OutT.Clear()
        tb3Out1.Clear() : tb3Out2.Clear() : tb3Out3.Clear() : tb3Out4.Clear() : tb3Out5.Clear() : tb3Out6.Clear() : tb3Out7.Clear() : tb3Out8.Clear() : tb3Out9.Clear() : tb3OutT.Clear()
        tbTOut1.Clear() : tbTOut2.Clear() : tbTOut3.Clear() : tbTOut4.Clear() : tbTOut5.Clear() : tbTOut6.Clear() : tbTOut7.Clear() : tbTOut8.Clear() : tbTOut9.Clear() : tbTOutT.Clear()
        

    End Sub

    Private Sub frmProductionEntries_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadSummary()
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

        LoadSummary()
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


        nHourCount = Format(Date.Now, "HH") - 7

        If Format(Date.Now, "mm") < 30 Then
            nHourCount = nHourCount - 1
        End If

        If Trim(tbProcess.Text) = "CONVEYOR IN" Then
            Dim daSelJCDPPCI As New SqlDataAdapter("Select ISNULL(InConveyor,'') As InConveyor from JobCardDetailPerPair where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
            Dim dsSelJCDPPCI As New DataSet
            daSelJCDPPCI.Fill(dsSelJCDPPCI)

            If dsSelJCDPPCI.Tables(0).Rows(0).Item("InConveyor").ToString = "" Then
                Dim daUpdJCDPPCI As New SqlDataAdapter("Update JobCardDetailPerPair Set Revisioncount = '" & nHourCount & _
                                                       "', InConveyor = '" & sMachineCode & _
                                                       "', ConveyorInDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                       "', ConveyorInUpdated = '0', InDateString = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                       "' where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
                Dim dsUpdJCDPPCI As New DataSet
                daUpdJCDPPCI.Fill(dsUpdJCDPPCI)
                dsUpdJCDPPCI.AcceptChanges()

                UpdateSummary()

                tbStatus.Text = "Successfully Scanned"
                tbStatus.ForeColor = Color.Green
                tbBarcode.Clear()
            Else
                tbStatus.Text = "This Pair already scanned"
                tbStatus.ForeColor = Color.Red
                tbBarcode.Clear()
            End If
        ElseIf Trim(tbProcess.Text) = "CONVEYOR OUT" Then
            Dim daSelJCDPPCO As New SqlDataAdapter("Select ISNULL(InConveyor,'') As InConveyor,ISNULL(OutConveyor,'') As OutConveyor from JobCardDetailPerPair where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
            Dim dsSelJCDPPCO As New DataSet
            daSelJCDPPCO.Fill(dsSelJCDPPCO)

            If dsSelJCDPPCO.Tables(0).Rows(0).Item("OutConveyor").ToString = "" Then
                If dsSelJCDPPCO.Tables(0).Rows(0).Item("InConveyor").ToString = "" Then
                    tbStatus.Text = "Conveyor In Not Scanned"
                    tbStatus.ForeColor = Color.Red
                    tbBarcode.Clear()
                    Exit Sub
                End If
                Dim daUpdJCDPPCI As New SqlDataAdapter("Update JobCardDetailPerPair Set Revisioncount = '" & nHourCount & _
                                                       "', OutConveyor = '" & sMachineCode & _
                                                       "', ConveyorOutDate = '" & Format(Date.Now, "dd-MMM-yyyy HH:mm:ss") & _
                                                       "', ConveyorOutUpdated = '0', OutDateString = '" & Format(Date.Now, "dd-MMM-yyyy") & _
                                                       "' where BarCode = '" & Trim(tbBarcode.Text) & "'", sConstr)
                Dim dsUpdJCDPPCI As New DataSet
                daUpdJCDPPCI.Fill(dsUpdJCDPPCI)
                dsUpdJCDPPCI.AcceptChanges()

                UpdateSummary()
                tbStatus.Text = "Successfully Scanned"
                tbStatus.ForeColor = Color.Green
                tbBarcode.Clear()
            Else
                tbStatus.Text = "This Pair already scanned"
                tbStatus.ForeColor = Color.Red
                tbBarcode.Clear()
            End If

        End If
    End Sub

    Dim nHour, nTotalProduction As Integer
    Dim sConveyor As String

    Private Sub LoadSummary()
        Clear()

        Dim daHrlyIn As New SqlDataAdapter("Select RevisionCount,InConveyor,Count(ID) As TotalCount from JobCardDetailPERPAIR Where Cast(ConveyorInDate As DATE) = '" & Format(Date.Now, "dd-MMM-yy") & "' Group By RevisionCount,InConveyor", sConstr)
        Dim dsHrlyIn As New DataSet
        daHrlyIn.Fill(dsHrlyIn)

        Dim i As Integer = 0

        For i = 0 To dsHrlyIn.Tables(0).Rows.Count - 1

            nHour = Val(dsHrlyIn.Tables(0).Rows(i).Item("RevisionCount"))
            sConveyor = dsHrlyIn.Tables(0).Rows(i).Item("InConveyor").ToString
            nTotalProduction = Val(dsHrlyIn.Tables(0).Rows(i).Item("TotalCount"))

            If sConveyor = "KW1" Or sConveyor = "SW1" Then
                If nHour = 1 Then : tb1In1.Text = Val(tb1In1.Text) + nTotalProduction
                ElseIf nHour = 2 Then : tb1In2.Text = Val(tb1In2.Text) + nTotalProduction
                ElseIf nHour = 3 Then : tb1In3.Text = Val(tb1In3.Text) + nTotalProduction
                ElseIf nHour = 4 Then : tb1In4.Text = Val(tb1In4.Text) + nTotalProduction
                ElseIf nHour = 5 Then : tb1In5.Text = Val(tb1In5.Text) + nTotalProduction
                ElseIf nHour = 6 Then : tb1In6.Text = Val(tb1In6.Text) + nTotalProduction
                ElseIf nHour = 7 Then : tb1In7.Text = Val(tb1In7.Text) + nTotalProduction
                ElseIf nHour = 8 Then : tb1In8.Text = Val(tb1In8.Text) + nTotalProduction
                ElseIf nHour = 9 Then : tb1In9.Text = Val(tb1In9.Text) + nTotalProduction
                End If
            ElseIf sConveyor = "KW2" Or sConveyor = "SW2" Then
                If nHour = 1 Then : tb2In1.Text = Val(tb2In1.Text) + nTotalProduction
                ElseIf nHour = 2 Then : tb2In2.Text = Val(tb2In2.Text) + nTotalProduction
                ElseIf nHour = 3 Then : tb2In3.Text = Val(tb2In3.Text) + nTotalProduction
                ElseIf nHour = 4 Then : tb2In4.Text = Val(tb2In4.Text) + nTotalProduction
                ElseIf nHour = 5 Then : tb2In5.Text = Val(tb2In5.Text) + nTotalProduction
                ElseIf nHour = 6 Then : tb2In6.Text = Val(tb2In6.Text) + nTotalProduction
                ElseIf nHour = 7 Then : tb2In7.Text = Val(tb2In7.Text) + nTotalProduction
                ElseIf nHour = 8 Then : tb2In8.Text = Val(tb2In8.Text) + nTotalProduction
                ElseIf nHour = 9 Then : tb2In9.Text = Val(tb2In9.Text) + nTotalProduction
                End If
            ElseIf sConveyor = "KW3" Or sConveyor = "SW3" Then
                If nHour = 1 Then : tb3In1.Text = Val(tb3In1.Text) + nTotalProduction
                ElseIf nHour = 2 Then : tb3In2.Text = Val(tb3In2.Text) + nTotalProduction
                ElseIf nHour = 3 Then : tb3In3.Text = Val(tb3In3.Text) + nTotalProduction
                ElseIf nHour = 4 Then : tb3In4.Text = Val(tb3In4.Text) + nTotalProduction
                ElseIf nHour = 5 Then : tb3In5.Text = Val(tb3In5.Text) + nTotalProduction
                ElseIf nHour = 6 Then : tb3In6.Text = Val(tb3In6.Text) + nTotalProduction
                ElseIf nHour = 7 Then : tb3In7.Text = Val(tb3In7.Text) + nTotalProduction
                ElseIf nHour = 8 Then : tb3In8.Text = Val(tb3In8.Text) + nTotalProduction
                ElseIf nHour = 9 Then : tb3In9.Text = Val(tb3In9.Text) + nTotalProduction
                End If
            End If
        Next

        Dim daHrlyOut As New SqlDataAdapter("Select RevisionCount,OutConveyor,Count(ID) As TotalCount from JobCardDetailPERPAIR Where Cast(ConveyorOutDate As DATE) = '" & Format(Date.Now, "dd-MMM-yy") & "' Group By RevisionCount,OutConveyor", sConstr)
        Dim dsHrlyOut As New DataSet
        daHrlyOut.Fill(dsHrlyOut)

        i = 0

        For i = 0 To dsHrlyOut.Tables(0).Rows.Count - 1

            nHour = Val(dsHrlyOut.Tables(0).Rows(i).Item("RevisionCount"))
            sConveyor = dsHrlyOut.Tables(0).Rows(i).Item("OutConveyor").ToString
            nTotalProduction = Val(dsHrlyOut.Tables(0).Rows(i).Item("TotalCount"))

            If sConveyor = "KW1" Or sConveyor = "SW1" Then
                If nHour = 1 Then : tb1Out1.Text = Val(tb1Out1.Text) + nTotalProduction
                ElseIf nHour = 2 Then : tb1Out2.Text = Val(tb1Out2.Text) + nTotalProduction
                ElseIf nHour = 3 Then : tb1Out3.Text = Val(tb1Out3.Text) + nTotalProduction
                ElseIf nHour = 4 Then : tb1Out4.Text = Val(tb1Out4.Text) + nTotalProduction
                ElseIf nHour = 5 Then : tb1Out5.Text = Val(tb1Out5.Text) + nTotalProduction
                ElseIf nHour = 6 Then : tb1Out6.Text = Val(tb1Out6.Text) + nTotalProduction
                ElseIf nHour = 7 Then : tb1Out7.Text = Val(tb1Out7.Text) + nTotalProduction
                ElseIf nHour = 8 Then : tb1Out8.Text = Val(tb1Out8.Text) + nTotalProduction
                ElseIf nHour = 9 Then : tb1Out9.Text = Val(tb1Out9.Text) + nTotalProduction
                End If
            ElseIf sConveyor = "KW2" Or sConveyor = "SW2" Then
                If nHour = 1 Then : tb2Out1.Text = Val(tb2Out1.Text) + nTotalProduction
                ElseIf nHour = 2 Then : tb2Out2.Text = Val(tb2Out2.Text) + nTotalProduction
                ElseIf nHour = 3 Then : tb2Out3.Text = Val(tb2Out3.Text) + nTotalProduction
                ElseIf nHour = 4 Then : tb2Out4.Text = Val(tb2Out4.Text) + nTotalProduction
                ElseIf nHour = 5 Then : tb2Out5.Text = Val(tb2Out5.Text) + nTotalProduction
                ElseIf nHour = 6 Then : tb2Out6.Text = Val(tb2Out6.Text) + nTotalProduction
                ElseIf nHour = 7 Then : tb2Out7.Text = Val(tb2Out7.Text) + nTotalProduction
                ElseIf nHour = 8 Then : tb2Out8.Text = Val(tb2Out8.Text) + nTotalProduction
                ElseIf nHour = 9 Then : tb2Out9.Text = Val(tb2Out9.Text) + nTotalProduction
                End If
            ElseIf sConveyor = "KW3" Or sConveyor = "SW3" Then
                If nHour = 1 Then : tb3Out1.Text = Val(tb3Out1.Text) + nTotalProduction
                ElseIf nHour = 2 Then : tb3Out2.Text = Val(tb3Out2.Text) + nTotalProduction
                ElseIf nHour = 3 Then : tb3Out3.Text = Val(tb3Out3.Text) + nTotalProduction
                ElseIf nHour = 4 Then : tb3Out4.Text = Val(tb3Out4.Text) + nTotalProduction
                ElseIf nHour = 5 Then : tb3Out5.Text = Val(tb3Out5.Text) + nTotalProduction
                ElseIf nHour = 6 Then : tb3Out6.Text = Val(tb3Out6.Text) + nTotalProduction
                ElseIf nHour = 7 Then : tb3Out7.Text = Val(tb3Out7.Text) + nTotalProduction
                ElseIf nHour = 8 Then : tb3Out8.Text = Val(tb3Out8.Text) + nTotalProduction
                ElseIf nHour = 9 Then : tb3Out9.Text = Val(tb3Out9.Text) + nTotalProduction
                End If
            End If
        Next

        CalculateTotal()
        '08.30 A.M to 09.30 A.M
       
        '09.30 A.M to 10.30 A.M
        '10.30 A.M to 11.30 A.M
        '11.30 A.M to 12.30 P.M
        '12.30 P.M to 01.30 P.M
        '01.30 P.M to 02.30 P.M
        '02.30 P.M to 03.30 P.M
        '03.30 P.M to 04.30 P.M
        '04.30 P.M to 05.30 P.M
       
        '12.30 P.M to 01.30 A.M

    End Sub

    Private Sub CalculateTotal()
        tb1InT.Text = Val(tb1In1.Text) + Val(tb1In2.Text) + Val(tb1In3.Text) + Val(tb1In4.Text) + Val(tb1In5.Text) + Val(tb1In6.Text) + Val(tb1In7.Text) + Val(tb1In8.Text) + Val(tb1In9.Text)
        tb2InT.Text = Val(tb2In1.Text) + Val(tb2In2.Text) + Val(tb2In3.Text) + Val(tb2In4.Text) + Val(tb2In5.Text) + Val(tb2In6.Text) + Val(tb2In7.Text) + Val(tb2In8.Text) + Val(tb2In9.Text)
        tb3InT.Text = Val(tb3In1.Text) + Val(tb3In2.Text) + Val(tb3In3.Text) + Val(tb3In4.Text) + Val(tb3In5.Text) + Val(tb3In6.Text) + Val(tb3In7.Text) + Val(tb3In8.Text) + Val(tb3In9.Text)

        tb1OutT.Text = Val(tb1Out1.Text) + Val(tb1Out2.Text) + Val(tb1Out3.Text) + Val(tb1Out4.Text) + Val(tb1Out5.Text) + Val(tb1Out6.Text) + Val(tb1Out7.Text) + Val(tb1Out8.Text) + Val(tb1Out9.Text)
        tb2OutT.Text = Val(tb2Out1.Text) + Val(tb2Out2.Text) + Val(tb2Out3.Text) + Val(tb2Out4.Text) + Val(tb2Out5.Text) + Val(tb2Out6.Text) + Val(tb2Out7.Text) + Val(tb2Out8.Text) + Val(tb2Out9.Text)
        tb3OutT.Text = Val(tb3Out1.Text) + Val(tb3Out2.Text) + Val(tb3Out3.Text) + Val(tb3Out4.Text) + Val(tb3Out5.Text) + Val(tb3Out6.Text) + Val(tb3Out7.Text) + Val(tb3Out8.Text) + Val(tb3Out9.Text)

        tbTIn1.Text = Val(tb1In1.Text) + Val(tb2In1.Text) + Val(tb3In1.Text)
        tbTIn2.Text = Val(tb1In2.Text) + Val(tb2In2.Text) + Val(tb3In2.Text)
        tbTIn3.Text = Val(tb1In3.Text) + Val(tb2In3.Text) + Val(tb3In3.Text)
        tbTIn4.Text = Val(tb1In4.Text) + Val(tb2In4.Text) + Val(tb3In4.Text)
        tbTIn5.Text = Val(tb1In5.Text) + Val(tb2In5.Text) + Val(tb3In5.Text)
        tbTIn6.Text = Val(tb1In6.Text) + Val(tb2In6.Text) + Val(tb3In6.Text)
        tbTIn7.Text = Val(tb1In7.Text) + Val(tb2In7.Text) + Val(tb3In7.Text)
        tbTIn8.Text = Val(tb1In8.Text) + Val(tb2In8.Text) + Val(tb3In8.Text)
        tbTIn9.Text = Val(tb1In9.Text) + Val(tb2In9.Text) + Val(tb3In9.Text)

        tbTOut1.Text = Val(tb1Out1.Text) + Val(tb2Out1.Text) + Val(tb3Out1.Text)
        tbTOut2.Text = Val(tb1Out2.Text) + Val(tb2Out2.Text) + Val(tb3Out2.Text)
        tbTOut3.Text = Val(tb1Out3.Text) + Val(tb2Out3.Text) + Val(tb3Out3.Text)
        tbTOut4.Text = Val(tb1Out4.Text) + Val(tb2Out4.Text) + Val(tb3Out4.Text)
        tbTOut5.Text = Val(tb1Out5.Text) + Val(tb2Out5.Text) + Val(tb3Out5.Text)
        tbTOut6.Text = Val(tb1Out6.Text) + Val(tb2Out6.Text) + Val(tb3Out6.Text)
        tbTOut7.Text = Val(tb1Out7.Text) + Val(tb2Out7.Text) + Val(tb3Out7.Text)
        tbTOut8.Text = Val(tb1Out8.Text) + Val(tb2Out8.Text) + Val(tb3Out8.Text)
        tbTOut9.Text = Val(tb1Out9.Text) + Val(tb2Out9.Text) + Val(tb3Out9.Text)

        tbTInT.Text = Val(tbTIn1.Text) + Val(tbTIn2.Text) + Val(tbTIn3.Text) + Val(tbTIn4.Text) + Val(tbTIn5.Text) + Val(tbTIn6.Text) + Val(tbTIn7.Text) + Val(tbTIn8.Text) + Val(tbTIn9.Text)
        tbTOutT.Text = Val(tbTOut1.Text) + Val(tbTOut2.Text) + Val(tbTOut3.Text) + Val(tbTOut4.Text) + Val(tbTOut5.Text) + Val(tbTOut6.Text) + Val(tbTOut7.Text) + Val(tbTOut8.Text) + Val(tbTOut9.Text)


        If Val(tbTIn1.Text) = 0 Then : tbTIn1.Clear() : End If
        If Val(tbTIn2.Text) = 0 Then : tbTIn2.Clear() : End If
        If Val(tbTIn3.Text) = 0 Then : tbTIn3.Clear() : End If
        If Val(tbTIn4.Text) = 0 Then : tbTIn4.Clear() : End If
        If Val(tbTIn5.Text) = 0 Then : tbTIn5.Clear() : End If
        If Val(tbTIn6.Text) = 0 Then : tbTIn6.Clear() : End If
        If Val(tbTIn7.Text) = 0 Then : tbTIn7.Clear() : End If
        If Val(tbTIn8.Text) = 0 Then : tbTIn8.Clear() : End If
        If Val(tbTIn9.Text) = 0 Then : tbTIn9.Clear() : End If

        If Val(tbTOut1.Text) = 0 Then : tbTOut1.Clear() : End If
        If Val(tbTOut2.Text) = 0 Then : tbTOut2.Clear() : End If
        If Val(tbTOut3.Text) = 0 Then : tbTOut3.Clear() : End If
        If Val(tbTOut4.Text) = 0 Then : tbTOut4.Clear() : End If
        If Val(tbTOut5.Text) = 0 Then : tbTOut5.Clear() : End If
        If Val(tbTOut6.Text) = 0 Then : tbTOut6.Clear() : End If
        If Val(tbTOut7.Text) = 0 Then : tbTOut7.Clear() : End If
        If Val(tbTOut8.Text) = 0 Then : tbTOut8.Clear() : End If
        If Val(tbTOut9.Text) = 0 Then : tbTOut9.Clear() : End If
    End Sub

    Private Sub UpdateSummary()
        


        nHour = nHourCount
        sConveyor = sMachineCode

        If Trim(tbProcess.Text) = "CONVEYOR IN" Then


            If sConveyor = "KW1" Or sConveyor = "SW1" Then
                If nHour = 1 Then : tb1In1.Text = Val(tb1In1.Text) + 1
                ElseIf nHour = 2 Then : tb1In2.Text = Val(tb1In2.Text) + 1
                ElseIf nHour = 3 Then : tb1In3.Text = Val(tb1In3.Text) + 1
                ElseIf nHour = 4 Then : tb1In4.Text = Val(tb1In4.Text) + 1
                ElseIf nHour = 5 Then : tb1In5.Text = Val(tb1In5.Text) + 1
                ElseIf nHour = 6 Then : tb1In6.Text = Val(tb1In6.Text) + 1
                ElseIf nHour = 7 Then : tb1In7.Text = Val(tb1In7.Text) + 1
                ElseIf nHour = 8 Then : tb1In8.Text = Val(tb1In8.Text) + 1
                ElseIf nHour = 9 Then : tb1In9.Text = Val(tb1In9.Text) + 1
                End If
            ElseIf sConveyor = "KW2" Or sConveyor = "SW2" Then
                If nHour = 1 Then : tb2In1.Text = Val(tb2In1.Text) + 1
                ElseIf nHour = 2 Then : tb2In2.Text = Val(tb2In2.Text) + 1
                ElseIf nHour = 3 Then : tb2In3.Text = Val(tb2In3.Text) + 1
                ElseIf nHour = 4 Then : tb2In4.Text = Val(tb2In4.Text) + 1
                ElseIf nHour = 5 Then : tb2In5.Text = Val(tb2In5.Text) + 1
                ElseIf nHour = 6 Then : tb2In6.Text = Val(tb2In6.Text) + 1
                ElseIf nHour = 7 Then : tb2In7.Text = Val(tb2In7.Text) + 1
                ElseIf nHour = 8 Then : tb2In8.Text = Val(tb2In8.Text) + 1
                ElseIf nHour = 9 Then : tb2In9.Text = Val(tb2In9.Text) + 1
                End If
            ElseIf sConveyor = "KW3" Or sConveyor = "SW3" Then
                If nHour = 1 Then : tb3In1.Text = Val(tb3In1.Text) + 1
                ElseIf nHour = 2 Then : tb3In2.Text = Val(tb3In2.Text) + 1
                ElseIf nHour = 3 Then : tb3In3.Text = Val(tb3In3.Text) + 1
                ElseIf nHour = 4 Then : tb3In4.Text = Val(tb3In4.Text) + 1
                ElseIf nHour = 5 Then : tb3In5.Text = Val(tb3In5.Text) + 1
                ElseIf nHour = 6 Then : tb3In6.Text = Val(tb3In6.Text) + 1
                ElseIf nHour = 7 Then : tb3In7.Text = Val(tb3In7.Text) + 1
                ElseIf nHour = 8 Then : tb3In8.Text = Val(tb3In8.Text) + 1
                ElseIf nHour = 9 Then : tb3In9.Text = Val(tb3In9.Text) + 1
                End If
            End If
        Else
            nHour = nHourCount
            sConveyor = sMachineCode


            If sConveyor = "KW1" Or sConveyor = "SW1" Then
                If nHour = 1 Then : tb1Out1.Text = Val(tb1Out1.Text) + 1
                ElseIf nHour = 2 Then : tb1Out2.Text = Val(tb1Out2.Text) + 1
                ElseIf nHour = 3 Then : tb1Out3.Text = Val(tb1Out3.Text) + 1
                ElseIf nHour = 4 Then : tb1Out4.Text = Val(tb1Out4.Text) + 1
                ElseIf nHour = 5 Then : tb1Out5.Text = Val(tb1Out5.Text) + 1
                ElseIf nHour = 6 Then : tb1Out6.Text = Val(tb1Out6.Text) + 1
                ElseIf nHour = 7 Then : tb1Out7.Text = Val(tb1Out7.Text) + 1
                ElseIf nHour = 8 Then : tb1Out8.Text = Val(tb1Out8.Text) + 1
                ElseIf nHour = 9 Then : tb1Out9.Text = Val(tb1Out9.Text) + 1
                End If
            ElseIf sConveyor = "KW2" Or sConveyor = "SW2" Then
                If nHour = 1 Then : tb2Out1.Text = Val(tb2Out1.Text) + 1
                ElseIf nHour = 2 Then : tb2Out2.Text = Val(tb2Out2.Text) + 1
                ElseIf nHour = 3 Then : tb2Out3.Text = Val(tb2Out3.Text) + 1
                ElseIf nHour = 4 Then : tb2Out4.Text = Val(tb2Out4.Text) + 1
                ElseIf nHour = 5 Then : tb2Out5.Text = Val(tb2Out5.Text) + 1
                ElseIf nHour = 6 Then : tb2Out6.Text = Val(tb2Out6.Text) + 1
                ElseIf nHour = 7 Then : tb2Out7.Text = Val(tb2Out7.Text) + 1
                ElseIf nHour = 8 Then : tb2Out8.Text = Val(tb2Out8.Text) + 1
                ElseIf nHour = 9 Then : tb2Out9.Text = Val(tb2Out9.Text) + 1
                End If
            ElseIf sConveyor = "KW3" Or sConveyor = "SW3" Then
                If nHour = 1 Then : tb3Out1.Text = Val(tb3Out1.Text) + 1
                ElseIf nHour = 2 Then : tb3Out2.Text = Val(tb3Out2.Text) + 1
                ElseIf nHour = 3 Then : tb3Out3.Text = Val(tb3Out3.Text) + 1
                ElseIf nHour = 4 Then : tb3Out4.Text = Val(tb3Out4.Text) + 1
                ElseIf nHour = 5 Then : tb3Out5.Text = Val(tb3Out5.Text) + 1
                ElseIf nHour = 6 Then : tb3Out6.Text = Val(tb3Out6.Text) + 1
                ElseIf nHour = 7 Then : tb3Out7.Text = Val(tb3Out7.Text) + 1
                ElseIf nHour = 8 Then : tb3Out8.Text = Val(tb3Out8.Text) + 1
                ElseIf nHour = 9 Then : tb3Out9.Text = Val(tb3Out9.Text) + 1
                End If
            End If
        End If
        CalculateTotal()
        '08.30 A.M to 09.30 A.M

        '09.30 A.M to 10.30 A.M
        '10.30 A.M to 11.30 A.M
        '11.30 A.M to 12.30 P.M
        '12.30 P.M to 01.30 P.M
        '01.30 P.M to 02.30 P.M
        '02.30 P.M to 03.30 P.M
        '03.30 P.M to 04.30 P.M
        '04.30 P.M to 05.30 P.M

        '12.30 P.M to 01.30 A.M

    End Sub
End Class