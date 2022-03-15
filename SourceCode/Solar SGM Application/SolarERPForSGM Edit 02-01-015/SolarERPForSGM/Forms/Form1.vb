Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO
Public Class Form1

    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim sSLIConstr As String = Global.SolarERPForSGM.My.Settings.SLIAHGroup
    Dim sSLICnn As New SqlConnection(sSLIConstr)


    Dim myOptimizerStrUserAuthentication As New StrUserAuthentication
    Dim myOptimizerComponentUser As New OptimizerComponent



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim mut As System.Threading.Mutex = New System.Threading.Mutex(False, Application.ProductName)
        'Dim running As Boolean = Not mut.WaitOne(0, False)
        'If running Then
        '    Application.ExitThread()
        '    Return
        'End If
        SingleInstances.Main()
        mdlSGM.strHostName = System.Net.Dns.GetHostName()
        '        mdlSGM.strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()
        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName)
        
        tbIP.Text = h.AddressList.GetValue(0).ToString
        mdlSGM.strIPAddress = tbIP.Text

        tbIP.Text = mdlSGM.strIPAddress
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        If mdlSGM.sUserName <> "" Then
            myOptimizerStrUserAuthentication.sUserName = mdlSGM.sUserName
            myOptimizerStrUserAuthentication.sLogoutTime = Format(Date.Now, "dd-MMM-yyyy::hh:mm:ss-tt")
            myOptimizerStrUserAuthentication.sIsActive = "0"
            If myOptimizerComponentUser.UpdateUserAuthentication(myOptimizerStrUserAuthentication) = True Then
                Me.DialogResult = DialogResult.OK
            End If
        End If
        End
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Hide()
        frmArticleLIst.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        frmInvoiceVer2.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Hide()
        frmOutstanding.Show()
    End Sub

    Private Sub cbAllinoneForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAllinoneForm.Click
        Me.Hide()
        'frmAllinOne.Show()
        frmAllinOnev1.Show()
    End Sub

    Private Sub cbAllMaterials_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAllMaterials.Click
        Me.Hide()
        frmMaterials.Show()
    End Sub

    Private Sub cbRejection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRejection.Click
        Me.Hide()
        frmRejection.Show()
    End Sub

    Private Sub cbRejection2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRejection2.Click
        'Dim sPrintingMessage As String
        'Dim i As Integer = 1

        'sPrintingMessage = ""
        'For i = 1 To 10
        '    sPrintingMessage = sPrintingMessage & vbCrLf & "Job - " + i.ToString + "."
        '    'PrintDocument1.Print()
        'Next

        'MsgBox(sPrintingMessage, MsgBoxStyle.Information)
        PrintDocument1.Print()
        'Me.Hide()
        'frmRejection2.Show()
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
            'PrintAreaHeight = .PaperSize.Height - 826 ''.Margins.Top - .Margins.Bottom
            'PrintAreaHeight = 1167
            'PrintAreaWidth = .PaperSize.Width + 100 '- .Margins.Left - .Margins.Right
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
        Dim sPrintingMessage As String
        'sPrintingMessage = Trim(tbBarcode.Text) + " : " + Trim(tbStatus.Text)
        'sPrintingMessage = "Jobcard No" ''Trim(tbBarcode.Text) + " | " + Trim(tbJobcardNo.Text)
        intCharsFitted = 1
        'e.Graphics.MeasureString(Mid(sPrintingMessage, intCurrentChar + 1), font, New SizeF(PrintAreaWidth, PrintAreaHeight), fmt, intCharsFitted, intLinesFilled)
        'e.Graphics.MeasureString(Mid(sPrintingMessage, 1), font, New SizeF(PrintAreaWidth, PrintAreaHeight), fmt, 1, intLinesFilled)
        ''-                                                 0                                   281             1167                        23      1           
        Dim i As Integer = 1

        sPrintingMessage = ""
        For i = 1 To 5
            sPrintingMessage = sPrintingMessage & vbCrLf & "Job No - " + i.ToString + "."
            'PrintDocument1.Print()
        Next

        i = 1
        For i = 1 To 10
            sPrintingMessage = sPrintingMessage & vbCrLf
        Next

        

        e.Graphics.DrawString(Mid(sPrintingMessage, intCurrentChar + 1), Font, Brushes.Black, rectPrintingArea, fmt)


        'intCurrentChar += intCharsFitted
        ''0                     23
    End Sub

    Private Sub PrintOption()
        Static intCurrentChar As Int32 = 0

        Dim PrintAreaHeight, PrintAreaWidth, marginLeft, marginTop As Int32

        With PrintDocument1.DefaultPageSettings

            .Margins.Top = 0
            .Margins.Bottom = 0
            .Margins.Left = 0
            .Margins.Right = 0
            PrintAreaHeight = .PaperSize.Height - 826
            PrintAreaWidth = .PaperSize.Width + 100
            marginLeft = .Margins.Left

            marginTop = .Margins.Top

        End With

        Dim intLineCount As Int32 = CInt(PrintAreaHeight / (Font.Height))
        Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, PrintAreaWidth, PrintAreaHeight)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
        Dim intLinesFilled, intCharsFitted As Int32
        Dim sPrintingMessage As String
        intCharsFitted = 1

        Dim i As Integer = 1

        sPrintingMessage = ""
        For i = 1 To 10
            sPrintingMessage = sPrintingMessage & vbCrLf & "Jobcard No - " + i.ToString + "."
        Next

        'e.Graphics.DrawString(Mid(sPrintingMessage, intCurrentChar + 1), Font, Brushes.Black, rectPrintingArea, fmt)


        'intCurrentChar += intCharsFitted
        ''0                     23
    End Sub
    Private Sub cbRejectionMainReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbRejectionMainReport.Click
        Me.Hide()
        frmRejectionMainReport.Show()
    End Sub

    Private Sub cbOrderPlanningReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOrderPlanningReport.Click
        Me.Hide()
        frmAOrderPlanning.Show()
    End Sub

    Private Sub cbSalesAnalysisReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSalesAnalysisReport.Click
        Me.Hide()
        frmSalesAnalysis.Show()
    End Sub

    Private Sub cbERPTracking1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbERPTracking1.Click
        Me.Hide()
        frmERPTrackingSystemv1.Show()
    End Sub

    Private Sub cbERPTracking2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbERPTracking2.Click
        Me.Hide()
        frmERPTrackingSystemv2.Show()
    End Sub

    Private Sub cbProductionEntries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProductionEntries.Click
        Me.Hide()
        frmProductionEntries.Show()
    End Sub

    Private Sub cbPackingEntries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPackingEntries.Click
        Me.Hide()
        frmPackingEntries.Show()
    End Sub


    Private Sub cbSpoolManageTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSpoolManageTools.Click
        Me.Hide()
        frmSpoolManagingTools.Show()
    End Sub



    Dim sCustomer, sEncryptedCustomer, sFirstString, sCurrentChar, sArrivedChar, sDecrytedCustomer, sString As String
    Dim nLen, nInitialLen, nCharId, nArrivedId, nSqrt As Integer

    Private Sub EncryptionDecryption()
        Dim daSelCustomers As New SqlDataAdapter("Select Distinct BuyerGroupCode from SalesOrder Where BuyerGroupCode Not in ('','8VG') Order by BuyerGroupCode", sConstr)
        Dim dsSelCustomers As New DataSet
        daSelCustomers.Fill(dsSelCustomers)

        Dim i As Integer = 0

        For i = 0 To dsSelCustomers.Tables(0).Rows.Count - 1
            sCustomer = dsSelCustomers.Tables(0).Rows(i).Item("BuyerGroupCode").ToString.ToUpper
            sFirstString = Microsoft.VisualBasic.Left(sCustomer, 1)

            nLen = Microsoft.VisualBasic.Len(sCustomer)
            nInitialLen = nLen
            nLen = nLen * 3
            nSqrt = nLen * nLen

            Dim j As Integer = 1

            If nSqrt < 100 Then
                sEncryptedCustomer = sFirstString + "0" + nSqrt.ToString
            Else
                sEncryptedCustomer = sFirstString + nSqrt.ToString
            End If

            ''Encryption
            For j = 1 To nInitialLen
                sCurrentChar = Microsoft.VisualBasic.Mid(sCustomer, j, 1)

                If sCurrentChar = "A" Then : nCharId = "1"
                ElseIf sCurrentChar = "B" Then : nCharId = "2"
                ElseIf sCurrentChar = "C" Then : nCharId = "3"
                ElseIf sCurrentChar = "D" Then : nCharId = "4"
                ElseIf sCurrentChar = "E" Then : nCharId = "5"
                ElseIf sCurrentChar = "F" Then : nCharId = "6"
                ElseIf sCurrentChar = "G" Then : nCharId = "7"
                ElseIf sCurrentChar = "H" Then : nCharId = "8"
                ElseIf sCurrentChar = "I" Then : nCharId = "9"
                ElseIf sCurrentChar = "J" Then : nCharId = "10"
                ElseIf sCurrentChar = "K" Then : nCharId = "11"
                ElseIf sCurrentChar = "L" Then : nCharId = "12"
                ElseIf sCurrentChar = "M" Then : nCharId = "13"
                ElseIf sCurrentChar = "N" Then : nCharId = "14"
                ElseIf sCurrentChar = "O" Then : nCharId = "15"
                ElseIf sCurrentChar = "P" Then : nCharId = "16"
                ElseIf sCurrentChar = "Q" Then : nCharId = "17"
                ElseIf sCurrentChar = "R" Then : nCharId = "18"
                ElseIf sCurrentChar = "S" Then : nCharId = "19"
                ElseIf sCurrentChar = "T" Then : nCharId = "20"
                ElseIf sCurrentChar = "U" Then : nCharId = "21"
                ElseIf sCurrentChar = "V" Then : nCharId = "22"
                ElseIf sCurrentChar = "W" Then : nCharId = "23"
                ElseIf sCurrentChar = "X" Then : nCharId = "24"
                ElseIf sCurrentChar = "Y" Then : nCharId = "25"
                ElseIf sCurrentChar = "Z" Then : nCharId = "26"
                End If

                nArrivedId = nCharId + nInitialLen

                If nArrivedId = 1 Then : sArrivedChar = "A"
                ElseIf nArrivedId = 2 Then : sArrivedChar = "B"
                ElseIf nArrivedId = 3 Then : sArrivedChar = "B"
                ElseIf nArrivedId = 4 Then : sArrivedChar = "D"
                ElseIf nArrivedId = 5 Then : sArrivedChar = "E"
                ElseIf nArrivedId = 6 Then : sArrivedChar = "F"
                ElseIf nArrivedId = 7 Then : sArrivedChar = "G"
                ElseIf nArrivedId = 8 Then : sArrivedChar = "H"
                ElseIf nArrivedId = 9 Then : sArrivedChar = "I"
                ElseIf nArrivedId = 10 Then : sArrivedChar = "J"
                ElseIf nArrivedId = 11 Then : sArrivedChar = "K"
                ElseIf nArrivedId = 12 Then : sArrivedChar = "L"
                ElseIf nArrivedId = 13 Then : sArrivedChar = "M"
                ElseIf nArrivedId = 14 Then : sArrivedChar = "N"
                ElseIf nArrivedId = 15 Then : sArrivedChar = "O"
                ElseIf nArrivedId = 16 Then : sArrivedChar = "P"
                ElseIf nArrivedId = 17 Then : sArrivedChar = "Q"
                ElseIf nArrivedId = 18 Then : sArrivedChar = "R"
                ElseIf nArrivedId = 19 Then : sArrivedChar = "S"
                ElseIf nArrivedId = 20 Then : sArrivedChar = "T"
                ElseIf nArrivedId = 21 Then : sArrivedChar = "U"
                ElseIf nArrivedId = 22 Then : sArrivedChar = "V"
                ElseIf nArrivedId = 23 Then : sArrivedChar = "W"
                ElseIf nArrivedId = 24 Then : sArrivedChar = "X"
                ElseIf nArrivedId = 25 Then : sArrivedChar = "Y"
                ElseIf nArrivedId = 26 Then : sArrivedChar = "Z"
                ElseIf nArrivedId = 27 Then : sArrivedChar = "z"
                ElseIf nArrivedId = 28 Then : sArrivedChar = "y"
                ElseIf nArrivedId = 29 Then : sArrivedChar = "x"
                ElseIf nArrivedId = 30 Then : sArrivedChar = "w"
                ElseIf nArrivedId = 31 Then : sArrivedChar = "v"
                ElseIf nArrivedId = 32 Then : sArrivedChar = "u"
                ElseIf nArrivedId = 33 Then : sArrivedChar = "t"
                ElseIf nArrivedId = 34 Then : sArrivedChar = "s"
                ElseIf nArrivedId = 35 Then : sArrivedChar = "r"
                ElseIf nArrivedId = 36 Then : sArrivedChar = "q"
                ElseIf nArrivedId = 37 Then : sArrivedChar = "p"
                ElseIf nArrivedId = 38 Then : sArrivedChar = "o"
                ElseIf nArrivedId = 39 Then : sArrivedChar = "n"
                ElseIf nArrivedId = 40 Then : sArrivedChar = "m"
                ElseIf nArrivedId = 41 Then : sArrivedChar = "l"
                ElseIf nArrivedId = 42 Then : sArrivedChar = "k"
                ElseIf nArrivedId = 43 Then : sArrivedChar = "j"
                ElseIf nArrivedId = 44 Then : sArrivedChar = "i"
                ElseIf nArrivedId = 45 Then : sArrivedChar = "h"
                ElseIf nArrivedId = 46 Then : sArrivedChar = "g"
                ElseIf nArrivedId = 47 Then : sArrivedChar = "f"
                ElseIf nArrivedId = 48 Then : sArrivedChar = "e"
                ElseIf nArrivedId = 49 Then : sArrivedChar = "d"
                ElseIf nArrivedId = 50 Then : sArrivedChar = "c"
                ElseIf nArrivedId = 51 Then : sArrivedChar = "b"
                ElseIf nArrivedId = 52 Then : sArrivedChar = "a"
                End If

                sEncryptedCustomer = sEncryptedCustomer + sArrivedChar
            Next
            ''Encryption

            ''Decryption
            nSqrt = Math.Sqrt(nSqrt)
            nLen = nSqrt / 3
            sDecrytedCustomer = ""
            Dim k As Integer = 5
            For k = 5 To Microsoft.VisualBasic.Len(sEncryptedCustomer)
                sCurrentChar = Microsoft.VisualBasic.Mid(sEncryptedCustomer, k, 1)

                If sCurrentChar = "A" Then : nArrivedId = 1
                ElseIf sCurrentChar = "B" Then : nArrivedId = 2
                ElseIf sCurrentChar = "B" Then : nArrivedId = 3
                ElseIf sCurrentChar = "D" Then : nArrivedId = 4
                ElseIf sCurrentChar = "E" Then : nArrivedId = 5
                ElseIf sCurrentChar = "F" Then : nArrivedId = 6
                ElseIf sCurrentChar = "G" Then : nArrivedId = 7
                ElseIf sCurrentChar = "H" Then : nArrivedId = 8
                ElseIf sCurrentChar = "I" Then : nArrivedId = 9
                ElseIf sCurrentChar = "J" Then : nArrivedId = 10
                ElseIf sCurrentChar = "K" Then : nArrivedId = 11
                ElseIf sCurrentChar = "L" Then : nArrivedId = 12
                ElseIf sCurrentChar = "M" Then : nArrivedId = 13
                ElseIf sCurrentChar = "N" Then : nArrivedId = 14
                ElseIf sCurrentChar = "O" Then : nArrivedId = 15
                ElseIf sCurrentChar = "P" Then : nArrivedId = 16
                ElseIf sCurrentChar = "Q" Then : nArrivedId = 17
                ElseIf sCurrentChar = "R" Then : nArrivedId = 18
                ElseIf sCurrentChar = "S" Then : nArrivedId = 19
                ElseIf sCurrentChar = "T" Then : nArrivedId = 20
                ElseIf sCurrentChar = "U" Then : nArrivedId = 21
                ElseIf sCurrentChar = "V" Then : nArrivedId = 22
                ElseIf sCurrentChar = "W" Then : nArrivedId = 23
                ElseIf sCurrentChar = "X" Then : nArrivedId = 24
                ElseIf sCurrentChar = "Y" Then : nArrivedId = 25
                ElseIf sCurrentChar = "Z" Then : nArrivedId = 26
                ElseIf sCurrentChar = "z" Then : nArrivedId = 27
                ElseIf sCurrentChar = "y" Then : nArrivedId = 28
                ElseIf sCurrentChar = "x" Then : nArrivedId = 29
                ElseIf sCurrentChar = "w" Then : nArrivedId = 30
                ElseIf sCurrentChar = "v" Then : nArrivedId = 31
                ElseIf sCurrentChar = "u" Then : nArrivedId = 32
                ElseIf sCurrentChar = "t" Then : nArrivedId = 33
                ElseIf sCurrentChar = "s" Then : nArrivedId = 34
                ElseIf sCurrentChar = "r" Then : nArrivedId = 35
                ElseIf sCurrentChar = "q" Then : nArrivedId = 36
                ElseIf sCurrentChar = "p" Then : nArrivedId = 37
                ElseIf sCurrentChar = "o" Then : nArrivedId = 38
                ElseIf sCurrentChar = "n" Then : nArrivedId = 39
                ElseIf sCurrentChar = "m" Then : nArrivedId = 40
                ElseIf sCurrentChar = "l" Then : nArrivedId = 41
                ElseIf sCurrentChar = "k" Then : nArrivedId = 42
                ElseIf sCurrentChar = "j" Then : nArrivedId = 43
                ElseIf sCurrentChar = "i" Then : nArrivedId = 44
                ElseIf sCurrentChar = "h" Then : nArrivedId = 45
                ElseIf sCurrentChar = "g" Then : nArrivedId = 46
                ElseIf sCurrentChar = "f" Then : nArrivedId = 47
                ElseIf sCurrentChar = "e" Then : nArrivedId = 48
                ElseIf sCurrentChar = "d" Then : nArrivedId = 49
                ElseIf sCurrentChar = "c" Then : nArrivedId = 50
                ElseIf sCurrentChar = "b" Then : nArrivedId = 51
                ElseIf sCurrentChar = "a" Then : nArrivedId = 52
                End If

                nCharId = nArrivedId - nInitialLen

                If nCharId = "1" Or nCharId = "52" Then : sCurrentChar = "A"
                ElseIf nCharId = "2" Or nCharId = "51" Then : sCurrentChar = "B"
                ElseIf nCharId = "3" Or nCharId = "50" Then : sCurrentChar = "C"
                ElseIf nCharId = "4" Or nCharId = "49" Then : sCurrentChar = "D"
                ElseIf nCharId = "5" Or nCharId = "48" Then : sCurrentChar = "E"
                ElseIf nCharId = "6" Or nCharId = "47" Then : sCurrentChar = "F"
                ElseIf nCharId = "7" Or nCharId = "46" Then : sCurrentChar = "G"
                ElseIf nCharId = "8" Or nCharId = "45" Then : sCurrentChar = "H"
                ElseIf nCharId = "9" Or nCharId = "44" Then : sCurrentChar = "I"
                ElseIf nCharId = "10" Or nCharId = "43" Then : sCurrentChar = "J"
                ElseIf nCharId = "11" Or nCharId = "42" Then : sCurrentChar = "K"
                ElseIf nCharId = "12" Or nCharId = "41" Then : sCurrentChar = "L"
                ElseIf nCharId = "13" Or nCharId = "40" Then : sCurrentChar = "M"
                ElseIf nCharId = "14" Or nCharId = "39" Then : sCurrentChar = "N"
                ElseIf nCharId = "15" Or nCharId = "38" Then : sCurrentChar = "O"
                ElseIf nCharId = "16" Or nCharId = "37" Then : sCurrentChar = "P"
                ElseIf nCharId = "17" Or nCharId = "36" Then : sCurrentChar = "Q"
                ElseIf nCharId = "18" Or nCharId = "35" Then : sCurrentChar = "R"
                ElseIf nCharId = "19" Or nCharId = "34" Then : sCurrentChar = "S"
                ElseIf nCharId = "20" Or nCharId = "33" Then : sCurrentChar = "T"
                ElseIf nCharId = "21" Or nCharId = "32" Then : sCurrentChar = "U"
                ElseIf nCharId = "22" Or nCharId = "31" Then : sCurrentChar = "V"
                ElseIf nCharId = "23" Or nCharId = "30" Then : sCurrentChar = "W"
                ElseIf nCharId = "24" Or nCharId = "29" Then : sCurrentChar = "X"
                ElseIf nCharId = "25" Or nCharId = "28" Then : sCurrentChar = "Y"
                ElseIf nCharId = "26" Or nCharId = "27" Then : sCurrentChar = "Z"
                End If

                sDecrytedCustomer = Trim(sDecrytedCustomer) + sCurrentChar

            Next
            ''Decryption

            sString = sCustomer + " | " + sEncryptedCustomer + " | " + sDecrytedCustomer
            UpdateNotePad()
        Next

    End Sub


    Private Sub Encryption()
        
        'sCustomer = dsSelCustomers.Tables(0).Rows(i).Item("BuyerGroupCode").ToString.ToUpper
        sFirstString = Microsoft.VisualBasic.Left(sCustomer, 1)

        nLen = Microsoft.VisualBasic.Len(sCustomer)
        nInitialLen = nLen
        nLen = nLen * 3
        nSqrt = nLen * nLen

        Dim j As Integer = 1

        If nSqrt < 100 Then
            sEncryptedCustomer = sFirstString + "0" + nSqrt.ToString
        Else
            sEncryptedCustomer = sFirstString + nSqrt.ToString
        End If

        ''Encryption
        For j = 1 To nInitialLen
            sCurrentChar = Microsoft.VisualBasic.Mid(sCustomer, j, 1)

            If sCurrentChar = "A" Then : nCharId = "1"
            ElseIf sCurrentChar = "B" Then : nCharId = "2"
            ElseIf sCurrentChar = "C" Then : nCharId = "3"
            ElseIf sCurrentChar = "D" Then : nCharId = "4"
            ElseIf sCurrentChar = "E" Then : nCharId = "5"
            ElseIf sCurrentChar = "F" Then : nCharId = "6"
            ElseIf sCurrentChar = "G" Then : nCharId = "7"
            ElseIf sCurrentChar = "H" Then : nCharId = "8"
            ElseIf sCurrentChar = "I" Then : nCharId = "9"
            ElseIf sCurrentChar = "J" Then : nCharId = "10"
            ElseIf sCurrentChar = "K" Then : nCharId = "11"
            ElseIf sCurrentChar = "L" Then : nCharId = "12"
            ElseIf sCurrentChar = "M" Then : nCharId = "13"
            ElseIf sCurrentChar = "N" Then : nCharId = "14"
            ElseIf sCurrentChar = "O" Then : nCharId = "15"
            ElseIf sCurrentChar = "P" Then : nCharId = "16"
            ElseIf sCurrentChar = "Q" Then : nCharId = "17"
            ElseIf sCurrentChar = "R" Then : nCharId = "18"
            ElseIf sCurrentChar = "S" Then : nCharId = "19"
            ElseIf sCurrentChar = "T" Then : nCharId = "20"
            ElseIf sCurrentChar = "U" Then : nCharId = "21"
            ElseIf sCurrentChar = "V" Then : nCharId = "22"
            ElseIf sCurrentChar = "W" Then : nCharId = "23"
            ElseIf sCurrentChar = "X" Then : nCharId = "24"
            ElseIf sCurrentChar = "Y" Then : nCharId = "25"
            ElseIf sCurrentChar = "Z" Then : nCharId = "26"
            End If

            nArrivedId = nCharId + nInitialLen

            If nArrivedId = 1 Then : sArrivedChar = "A"
            ElseIf nArrivedId = 2 Then : sArrivedChar = "B"
            ElseIf nArrivedId = 3 Then : sArrivedChar = "B"
            ElseIf nArrivedId = 4 Then : sArrivedChar = "D"
            ElseIf nArrivedId = 5 Then : sArrivedChar = "E"
            ElseIf nArrivedId = 6 Then : sArrivedChar = "F"
            ElseIf nArrivedId = 7 Then : sArrivedChar = "G"
            ElseIf nArrivedId = 8 Then : sArrivedChar = "H"
            ElseIf nArrivedId = 9 Then : sArrivedChar = "I"
            ElseIf nArrivedId = 10 Then : sArrivedChar = "J"
            ElseIf nArrivedId = 11 Then : sArrivedChar = "K"
            ElseIf nArrivedId = 12 Then : sArrivedChar = "L"
            ElseIf nArrivedId = 13 Then : sArrivedChar = "M"
            ElseIf nArrivedId = 14 Then : sArrivedChar = "N"
            ElseIf nArrivedId = 15 Then : sArrivedChar = "O"
            ElseIf nArrivedId = 16 Then : sArrivedChar = "P"
            ElseIf nArrivedId = 17 Then : sArrivedChar = "Q"
            ElseIf nArrivedId = 18 Then : sArrivedChar = "R"
            ElseIf nArrivedId = 19 Then : sArrivedChar = "S"
            ElseIf nArrivedId = 20 Then : sArrivedChar = "T"
            ElseIf nArrivedId = 21 Then : sArrivedChar = "U"
            ElseIf nArrivedId = 22 Then : sArrivedChar = "V"
            ElseIf nArrivedId = 23 Then : sArrivedChar = "W"
            ElseIf nArrivedId = 24 Then : sArrivedChar = "X"
            ElseIf nArrivedId = 25 Then : sArrivedChar = "Y"
            ElseIf nArrivedId = 26 Then : sArrivedChar = "Z"
            ElseIf nArrivedId = 27 Then : sArrivedChar = "z"
            ElseIf nArrivedId = 28 Then : sArrivedChar = "y"
            ElseIf nArrivedId = 29 Then : sArrivedChar = "x"
            ElseIf nArrivedId = 30 Then : sArrivedChar = "w"
            ElseIf nArrivedId = 31 Then : sArrivedChar = "v"
            ElseIf nArrivedId = 32 Then : sArrivedChar = "u"
            ElseIf nArrivedId = 33 Then : sArrivedChar = "t"
            ElseIf nArrivedId = 34 Then : sArrivedChar = "s"
            ElseIf nArrivedId = 35 Then : sArrivedChar = "r"
            ElseIf nArrivedId = 36 Then : sArrivedChar = "q"
            ElseIf nArrivedId = 37 Then : sArrivedChar = "p"
            ElseIf nArrivedId = 38 Then : sArrivedChar = "o"
            ElseIf nArrivedId = 39 Then : sArrivedChar = "n"
            ElseIf nArrivedId = 40 Then : sArrivedChar = "m"
            ElseIf nArrivedId = 41 Then : sArrivedChar = "l"
            ElseIf nArrivedId = 42 Then : sArrivedChar = "k"
            ElseIf nArrivedId = 43 Then : sArrivedChar = "j"
            ElseIf nArrivedId = 44 Then : sArrivedChar = "i"
            ElseIf nArrivedId = 45 Then : sArrivedChar = "h"
            ElseIf nArrivedId = 46 Then : sArrivedChar = "g"
            ElseIf nArrivedId = 47 Then : sArrivedChar = "f"
            ElseIf nArrivedId = 48 Then : sArrivedChar = "e"
            ElseIf nArrivedId = 49 Then : sArrivedChar = "d"
            ElseIf nArrivedId = 50 Then : sArrivedChar = "c"
            ElseIf nArrivedId = 51 Then : sArrivedChar = "b"
            ElseIf nArrivedId = 52 Then : sArrivedChar = "a"
            End If

            sEncryptedCustomer = sEncryptedCustomer + sArrivedChar
        Next
    End Sub

    Private Sub UpdateNotePad()

        'Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "BoxScanningStatus_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Dim filePath As String = String.Format("D:\" + Trim(mdlSGM.strIPAddress.Replace(":", "")) + "ProductStockComparision_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
        Using writer As New StreamWriter(filePath, True)
            If File.Exists(filePath) Then
                writer.WriteLine(sString + " | " & DateTime.Now)
            Else
                writer.WriteLine("Start Error Log for today")
            End If
        End Using
    End Sub

    Dim dJobCardDate As Date
    Dim nOrderQuantity, nQuantity, nShippedQty, nUpperDispatchQty As Decimal
    Dim nPackedQty, nPcs, nShipped As Integer
    Dim nCONVEYORIN, nCONVEYORISSUE, nCONVEYOROUT, nDISPATCH, nFEEDING, nFeedingStock, nFINALINSPECTION, nFORMING As Decimal
    Dim nFSCONVEYORIN, nFSCONVEYORINC, nFSCONVEYOROUT, nFSINPUTC, FSKITTING, nFSKITTINGC, nFSConvStock, nHANDSTITCHING As Decimal
    Dim nKITTING, nPACKING, nPREFITTING, nQUALITYCONTROL, nSOCKSPREPARATION, nUPPERNLININGCUTTING, nUpperinFeeding As Decimal
    Dim sArticle, sArticleGroup, sArticleType, sBuyerBuy, sBuyerCode, sBuyerGroupCode, sBuyerOrderNo, sColorName, sComponentGroup As String
    Dim sCustomerOrderNo, sCustWorkOrderNo, sJobCardNo, sJobCardStatus, sOldJobCardNo, sOrderNo, sSalesOrderNo, sSeason As String
    Dim sShipper, sTypeofOrder, sVariants As String


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim daSelOrder As New SqlDataAdapter("Select * from vw_OrderOutstandingJCWise Where Season in ('SS19','AW18') And  (SalesOrderNo like '%-F-%' OR SalesOrderNo like '%-W-%') Order by Season,BuyerGroupCode,JobCardNo", sSLIConstr)
        Dim dsSelOrder As New DataSet
        daSelOrder.Fill(dsSelOrder)

        Dim i As Integer = 0

        For i = 0 To dsSelOrder.Tables(0).Rows.Count - 1

        Next
    End Sub


    Dim sStage, sID, sSize, sStatus, sArticleCode As String
    Dim nProductStock, nCurrentQty As Integer
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        'Dim daSelProdStock As New SqlDataAdapter("Select WorkOrderNo,Stage,ArticleNo,Sum(Quantity) AS Stock from ProductStock Where WorkOrderNo in (Select JobcardNo from JobcardWIP) Group By WorkOrderNo,Stage,ArticleNo Having Sum(Quantity) <> 0 ORDER BY WorkOrderno,Stage", sConstr)
        UpdateProducedQuantity()
        Exit Sub
        Dim daSelProdStock As New SqlDataAdapter("Select WorkOrderNo,Stage,ArticleNo,Sum(Quantity) AS Stock from ProductStock Where WorkOrderNo in ('S-18-2630-002') Group By WorkOrderNo,Stage,ArticleNo Having Sum(Quantity) <> 0 ORDER BY WorkOrderno,Stage", sConstr)
        Dim dsSelProdStock As New DataSet
        daSelProdStock.Fill(dsSelProdStock)

        Dim i As Integer = 0

        For i = 0 To dsSelProdStock.Tables(0).Rows.Count - 1
            sJobCardNo = dsSelProdStock.Tables(0).Rows(i).Item("WorkOrderNo").ToString
            sStage = dsSelProdStock.Tables(0).Rows(i).Item("Stage").ToString
            nProductStock = Val(dsSelProdStock.Tables(0).Rows(i).Item("Stock").ToString)
            sArticleCode = Microsoft.VisualBasic.Left(dsSelProdStock.Tables(0).Rows(i).Item("ArticleNo").ToString, 10)

            If sStage = "MOULD" Then
                Dim dsSelMouldInfo As New DataSet

                If sArticleCode = "SOL-SYN-EV" Or sArticleCode = "SOL-LEA-EV" Or sArticleCode = "SOL-SYN-RU" Then
                    'Dim daSelMouldInfo As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity from PackingDetail where JobcardNo = '" & sJobCardNo & _
                    '                                         "' And MouldScanDate Is Not Null", sConstr)
                    'daSelMouldInfo.Fill(dsSelMouldInfo)
                    nQuantity = 0 'Val(dsSelMouldInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                Else
                    Dim daSelMouldInfo As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity from PackingDetail where JobcardNo = '" & sJobCardNo & _
                                                             "' And MouldScanDate Is Not Null And MtoFScanDate Is Null", sConstr)
                    daSelMouldInfo.Fill(dsSelMouldInfo)
                    nQuantity = Val(dsSelMouldInfo.Tables(0).Rows(0).Item("Quantity").ToString)
                End If



            ElseIf sStage = "FINISH" Then
                Dim daSelFinishInfo As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity from PackingDetail where JobcardNo = '" & sJobCardNo & _
                                                          "' And MtoFScanDate Is Not Null And FinishScanDate Is Null", sConstr)
                Dim dsSelFinishInfo As New DataSet
                daSelFinishInfo.Fill(dsSelFinishInfo)

                nQuantity = Val(dsSelFinishInfo.Tables(0).Rows(0).Item("Quantity").ToString)

            ElseIf sStage = "PACKING" Then
                Dim daSelFinishInfo As New SqlDataAdapter("Select IsNull(Sum(Quantity),0) As Quantity from PackingDetail where JobcardNo = '" & sJobCardNo & _
                                                          "' And FinishScanDate Is Not Null And (InvoiceNo = '' or InvoiceNo Is Null)", sConstr)
                Dim dsSelFinishInfo As New DataSet
                daSelFinishInfo.Fill(dsSelFinishInfo)

                nQuantity = Val(dsSelFinishInfo.Tables(0).Rows(0).Item("Quantity").ToString)

            End If

            If nProductStock = nQuantity Then
                sStatus = "Matching"
                'If sStage = "PACKING" Then
                '    GoTo Aa
                'End If
            Else
                sStatus = "Not Matching"
                'MsgBox(sJobCardNo)
Aa:
                Dim dsSelPackInfo As New DataSet
                If sStage = "MOULD" Then
                    If sArticleCode = "SOL-SYN-EV" Or sArticleCode = "SOL-LEA-EV" Or sArticleCode = "SOL-SYN-RU" Then
                        'Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And MouldScanDate Is Not Null Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                        'daSelPackInfo.Fill(dsSelPackInfo)
                    Else
                        Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And MouldScanDate Is Not Null And MtoFScanDate Is Null Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                        daSelPackInfo.Fill(dsSelPackInfo)
                    End If
                ElseIf sStage = "FINISH" Then
                    Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And MtoFScanDate Is Not Null And FinishScanDate Is Null Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                    daSelPackInfo.Fill(dsSelPackInfo)
                ElseIf sStage = "PACKING" Then
                    Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And FinishScanDate Is Not Null And (InvoiceNo = '' or InvoiceNo Is Null) Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                    daSelPackInfo.Fill(dsSelPackInfo)
                End If

                Dim daRemoveProductStock As New SqlDataAdapter("Update ProductStock Set Quantity = 0 Where WorkOrderNo = '" & sJobCardNo & _
                                                               "' And Stage = '" & sStage & "'", sConstr)
                Dim dsRemoveProductStock As New DataSet
                daRemoveProductStock.Fill(dsRemoveProductStock)
                dsRemoveProductStock.AcceptChanges()

                If sArticleCode = "SOL-SYN-EV" Or sArticleCode = "SOL-LEA-EV" Or sArticleCode = "SOL-SYN-RU" Then
                    GoTo Ac
                End If

                Dim j As Integer = 0
                If dsSelPackInfo.Tables(0).Rows.Count > 0 Then
                    For j = 0 To dsSelPackInfo.Tables(0).Rows.Count - 1
                        sSize = dsSelPackInfo.Tables(0).Rows(j).Item("Size").ToString
                        nCurrentQty = Val(dsSelPackInfo.Tables(0).Rows(j).Item(2).ToString)

                        Dim daSelProductStock As New SqlDataAdapter("Select * from ProductStock where WorkOrderNo = '" & sJobCardNo & _
                                                                    "' And Size = '" & sSize & _
                                                                    "' And Stage = '" & sStage & "'", sConstr)
                        Dim dsSelProductStock As New DataSet
                        daSelProductStock.Fill(dsSelProductStock)

                        sID = dsSelProductStock.Tables(0).Rows(0).Item("ID")


                        Dim daUpdProdStock As New SqlDataAdapter("Update ProductStock Set Quantity = '" & nCurrentQty & _
                                                                 "' Where ID = '" & sID & "'", sConstr)
                        Dim dsUpdProdStock As New DataSet
                        daUpdProdStock.Fill(dsUpdProdStock)
                        dsUpdProdStock.AcceptChanges()


                    Next
                End If
Ac:
            End If

            sString = sJobCardNo + " | " + sStage + " | " + nProductStock.ToString + " | " + nQuantity.ToString + " | " + sStatus
            UpdateNotePad()
        Next
        MsgBox("Completed")
    End Sub

    Dim sProcess As String
    Private Sub UpdateProducedQuantity()
        'Dim daSelProdStock As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo in (Select JobcardNo from JobcardWIP) Order By JobcardNo", sConstr)
        Dim daSelProdStock As New SqlDataAdapter("Select * from JobcardDetail where JobcardNo = 'S-18-3915-001'", sConstr)
        Dim dsSelProdStock As New DataSet
        daSelProdStock.Fill(dsSelProdStock)

        Dim i As Integer = 0

        For i = 0 To dsSelProdStock.Tables(0).Rows.Count - 1
            sJobCardNo = dsSelProdStock.Tables(0).Rows(i).Item("JobcardNo").ToString
            sArticleCode = Microsoft.VisualBasic.Left(dsSelProdStock.Tables(0).Rows(i).Item("Article").ToString, 10)
            sStage = "MOULD"
Ad:
            Dim dsSelPackInfo As New DataSet
            If sStage = "MOULD" Then
                Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And MouldScanDate Is Not Null Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                daSelPackInfo.Fill(dsSelPackInfo)
            ElseIf sStage = "FINISH" Then
                Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And FinishScanDate Is Not Null Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                daSelPackInfo.Fill(dsSelPackInfo)
            ElseIf sStage = "Packing" Then

                Dim daSelPackInfo As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) FROM  PackingDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & "' And FinishScanDate Is Not Null And (InvoiceNo = '' or InvoiceNo Is Null) Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sConstr)
                daSelPackInfo.Fill(dsSelPackInfo)
            End If

            Dim daRemoveProductStock As New SqlDataAdapter("Update ProductionByprocess Set Quantity = 0 Where WorkOrderNo = '" & sJobCardNo & _
                                                           "' And ProcessName = '" & sStage & "'", sConstr)
            Dim dsRemoveProductStock As New DataSet
            daRemoveProductStock.Fill(dsRemoveProductStock)
            dsRemoveProductStock.AcceptChanges()

            If sArticleCode = "SOL-SYN-EV" Or sArticleCode = "SOL-LEA-EV" Or sArticleCode = "SOL-SYN-RU" Then
                'GoTo Ac
            End If

            Dim j As Integer = 0
            If dsSelPackInfo.Tables(0).Rows.Count > 0 Then
                For j = 0 To dsSelPackInfo.Tables(0).Rows.Count - 1
                    sSize = dsSelPackInfo.Tables(0).Rows(j).Item("Size").ToString
                    nCurrentQty = Val(dsSelPackInfo.Tables(0).Rows(j).Item(2).ToString)

                    Dim daSelProductStock As New SqlDataAdapter("Select * from ProductionByprocess where WorkOrderNo = '" & sJobCardNo & _
                                                                "' And Size = '" & sSize & _
                                                                "' And ProcessName = '" & sStage & "'", sConstr)
                    Dim dsSelProductStock As New DataSet
                    daSelProductStock.Fill(dsSelProductStock)

                    If dsSelProductStock.Tables(0).Rows.Count > 0 Then
                        sID = dsSelProductStock.Tables(0).Rows(0).Item("ID")


                        Dim daUpdProdStock As New SqlDataAdapter("Update ProductionByprocess Set Quantity = '" & nCurrentQty & _
                                                                 "' Where ID = '" & sID & "'", sConstr)
                        Dim dsUpdProdStock As New DataSet
                        daUpdProdStock.Fill(dsUpdProdStock)
                        dsUpdProdStock.AcceptChanges()
                    End If

                Next

                If sStage = "MOULD" Then
                    sStage = "FINISH"
                    GoTo Ad
                End If

            End If
Ac:


            sString = sJobCardNo + " | " + sStage + " | " + nProductStock.ToString + " | " + nQuantity.ToString + " | " + sStatus
            UpdateNotePad()
        Next
        MsgBox("Completed")
    End Sub

    Private Sub cbInvoiceGeneration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbInvoiceGeneration.Click
        Me.Hide()
        frmInvoiceGeneration.Show()
    End Sub


    Dim sWithDC, sFromStage As String
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        'GoTo ProductionByProcess
        GoTo ProductStock
        ''Production By Process
ProductionByProcess:
        Dim daSelJCCount As New SqlDataAdapter("Select * from TempTable Order By jobcardNo", sSLIConstr)
        Dim dsSelJCCount As New DataSet
        daSelJCCount.Fill(dsSelJCCount)

        Dim ijk As Integer = 0

        For ijk = 0 To dsSelJCCount.Tables(0).Rows.Count - 1
            sJobCardNo = dsSelJCCount.Tables(0).Rows(ijk).Item("JobCardNo").ToString


            Dim daSelJobcardDetail As New SqlDataAdapter("Select * from JobcardDetail Where ComponentGroup = 'FullShoe' And JobcardNo = '" & sJobCardNo & "'", sSLIConstr)
            Dim dsSelJobCardDetail As New DataSet
            daSelJobcardDetail.Fill(dsSelJobCardDetail)

            Dim I As Integer = 0
            Dim nJCQuantity, nProductionQty As Integer
            For I = 0 To dsSelJobCardDetail.Tables(0).Rows.Count - 1
                nJCQuantity = Val(dsSelJobCardDetail.Tables(0).Rows(I).Item("Quantity").ToString)
                sJobCardNo = dsSelJobCardDetail.Tables(0).Rows(I).Item("JobCardNo").ToString
                Dim daSelStages As New SqlDataAdapter("Select Distinct ProcessName,FromStage, SUM(Quantity) AS Quantity From ProductionByProcess Where WorkOrderNo = '" & sJobCardNo & _
                                                      "' And LEN(ProcessName) > 3 Group By ProcessName,FromStage Order by ProcessName", sSLIConstr)
                Dim dsSelStages As New DataSet
                daSelStages.Fill(dsSelStages)

                Dim j As Integer = 0
                For j = 0 To dsSelStages.Tables(0).Rows.Count - 1
                    sStage = dsSelStages.Tables(0).Rows(j).Item("ProcessName").ToString
                    sFromStage = dsSelStages.Tables(0).Rows(j).Item("FromStage").ToString

                    nProductionQty = Val(dsSelStages.Tables(0).Rows(j).Item("Quantity").ToString)

                    If nProductionQty > nJCQuantity Then
                        Dim daSelWorWODC As New SqlDataAdapter("Select Distinct DCNo from ProductionByProcess where WorkOrderNo = '" & sJobCardNo & _
                                                               "' And ProcessName = '" & sStage & "' And DCNo <> ''", sSLIConstr)
                        Dim dsSelWorWODC As New DataSet
                        daSelWorWODC.Fill(dsSelWorWODC)

                        If dsSelWorWODC.Tables(0).Rows.Count > 0 Then
                            sWithDC = "Y"
                        Else
                            sWithDC = "N"
                        End If

                        Dim daResetQty As New SqlDataAdapter("Update ProductionByProcess Set Pcs = Quantity Where WorkOrderNo = '" & sJobCardNo & _
                                                               "' And ProcessName = '" & sStage & "'", sSLIConstr)
                        Dim dsResetQty As New DataSet
                        daResetQty.Fill(dsResetQty)
                        dsResetQty.AcceptChanges()

                        Dim daResetQty2Zero As New SqlDataAdapter("Update ProductionByProcess Set Quantity = 0 Where WorkOrderNo = '" & sJobCardNo & _
                                                               "' And ProcessName = '" & sStage & "'", sSLIConstr)
                        Dim dsResetQty2Zero As New DataSet
                        daResetQty2Zero.Fill(dsResetQty2Zero)
                        dsResetQty2Zero.AcceptChanges()



                        Dim daJCQty As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) As Quantity FROM  JobCardDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & _
                                                          "'  And ComponentGroup = 'UPPER' Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sSLIConstr)
                        Dim dsJCQty As New DataSet
                        daJCQty.Fill(dsJCQty)


                        Dim k As Integer = 0
                        For k = 0 To dsJCQty.Tables(0).Rows.Count - 1
                            sSize = dsJCQty.Tables(0).Rows(k).Item("Size").ToString
                            nQuantity = Val(dsJCQty.Tables(0).Rows(k).Item("Quantity").ToString)

                            If sFromStage = "WIP" Then
                            Else
                                Dim daProdQty As New SqlDataAdapter("Select WorkOrderNo,Size, SUM(Quantity) AS Quantity from ProductionByProcess where WorkOrderNo = '" & sJobCardNo & _
                                                                    "' And ProcessName = '" & sFromStage & "' And Size = '" & Val(sSize) & "' Group By WorkorderNo,Size Order by Size", sSLIConstr)
                                Dim dsProdQty As New DataSet
                                daProdQty.Fill(dsProdQty)

                                nQuantity = Val(dsProdQty.Tables(0).Rows(0).Item("Quantity").ToString)
                            End If

                            Dim dsSelPBP As New DataSet

                            Dim daSelPBP As New SqlDataAdapter("Select * from ProductionByProcess where WorkOrderNo = '" & sJobCardNo & _
                                                               "' And ProcessName = '" & sStage & "'  And Size = '" & Val(sSize) & _
                                                               "' And IsNull(DCNo,'') <> ''", sSLIConstr)
                            daSelPBP.Fill(dsSelPBP)

                            If dsSelPBP.Tables(0).Rows.Count > 0 Then
                                sID = dsSelPBP.Tables(0).Rows(0).Item("ID").ToString
                            Else
                                Dim daSelPBPWDC As New SqlDataAdapter("Select * from ProductionByProcess where WorkOrderNo = '" & sJobCardNo & _
                                                               "' And ProcessName = '" & sStage & "'  And Size = '" & Val(sSize) & _
                                                               "' And IsNull(DCNo,'') = ''", sSLIConstr)
                                Dim dsSelPBPWDC As New DataSet
                                daSelPBPWDC.Fill(dsSelPBPWDC)

                                sID = dsSelPBPWDC.Tables(0).Rows(0).Item("ID").ToString
                            End If





                            Dim daUpdpbp As New SqlDataAdapter("Update ProductionByProcess Set Quantity = '" & nQuantity & _
                                                               "' Where ID = '" & sID & "'", sSLIConstr)
                            Dim dsUpdPbp As New DataSet
                            daUpdpbp.Fill(dsUpdPbp)
                            dsUpdPbp.AcceptChanges()
                        Next






                    End If
                Next

            Next
        Next
        MsgBox("Completed")
        ''Production By Process
ProductStock:
        Dim daSelJCCountPS As New SqlDataAdapter("Select * from TempTable Order By jobcardNo", sSLIConstr)
        Dim dsSelJCCountPS As New DataSet
        daSelJCCountPS.Fill(dsSelJCCountPS)

        ijk = 0

        For ijk = 0 To dsSelJCCountPS.Tables(0).Rows.Count - 1
            sJobCardNo = dsSelJCCountPS.Tables(0).Rows(ijk).Item("JobCardNo").ToString


            Dim daSelJobcardDetailPS As New SqlDataAdapter("Select * from JobcardDetail Where ComponentGroup = 'FullShoe' And JobcardNo = '" & sJobCardNo & "'", sSLIConstr)
            Dim dsSelJobCardDetailPS As New DataSet
            daSelJobcardDetailPS.Fill(dsSelJobCardDetailPS)

            Dim I As Integer = 0
            Dim nJCQuantity, nProductionQty As Integer
            For I = 0 To dsSelJobCardDetailPS.Tables(0).Rows.Count - 1
                nJCQuantity = Val(dsSelJobCardDetailPS.Tables(0).Rows(I).Item("Quantity").ToString)
                sJobCardNo = dsSelJobCardDetailPS.Tables(0).Rows(I).Item("JobCardNo").ToString
                Dim daSelStages As New SqlDataAdapter("Select Distinct stg.stagedisplay,PBP.ProcessName,PBP.FromStage, SUM(PBP.Quantity) AS Quantity From ProductionByProcess As PBP, Stages As STG Where PBP.ProcessName = stg.stagecode And PBP.WorkOrderNo = '" & sJobCardNo & _
                                                      "' And LEN(PBP.ProcessName) > 3  And stg.stagedisplay > 1 Group By stg.stagedisplay,PBP.ProcessName,PBP.FromStage  Order by stg.stagedisplay --PBP.ProcessName", sSLIConstr)
                Dim dsSelStages As New DataSet
                daSelStages.Fill(dsSelStages)

                Dim j As Integer = 0
                For j = 0 To dsSelStages.Tables(0).Rows.Count - 1
                    sStage = dsSelStages.Tables(0).Rows(j).Item("ProcessName").ToString
                    sFromStage = dsSelStages.Tables(0).Rows(j).Item("FromStage").ToString

                    nProductionQty = Val(dsSelStages.Tables(0).Rows(j).Item("Quantity").ToString)

                    If nJCQuantity > nProductionQty Then

                        Dim daResetQty2Zero As New SqlDataAdapter("Update ProductStock Set Quantity = 0 Where WorkOrderNo = '" & sJobCardNo & _
                                                                  "' And Stage = '" & sFromStage & "'", sSLIConstr)
                        Dim dsResetQty2Zero As New DataSet
                        daResetQty2Zero.Fill(dsResetQty2Zero)
                        dsResetQty2Zero.AcceptChanges()



                        Dim daJCQty As New SqlDataAdapter("SELECT j.JobcardNo, d .Size, Sum(d .Qty) As Quantity FROM  JobCardDetail AS j CROSS Apply (SELECT Size01, Quantity01 UNION ALL SELECT Size02, Quantity02 UNION ALL SELECT Size03, Quantity03 UNION ALL SELECT Size04, Quantity04 UNION ALL SELECT Size05, Quantity05 UNION ALL SELECT Size06, Quantity06 UNION ALL SELECT Size07, Quantity07 UNION ALL SELECT Size08, Quantity08 UNION ALL SELECT Size09, Quantity09 UNION ALL SELECT Size10, Quantity10 UNION ALL SELECT Size11, Quantity11 UNION ALL SELECT Size12, Quantity12 UNION ALL SELECT Size13, Quantity13 UNION ALL SELECT Size14, Quantity14 UNION ALL SELECT Size15, Quantity15 UNION ALL SELECT Size16, Quantity16 UNION ALL SELECT Size17, Quantity17 UNION ALL SELECT Size18, Quantity18) d (Size, Qty) WHERE d .Qty > 0 And JobcardNo = '" & sJobCardNo & _
                                                          "'  And ComponentGroup = 'UPPER' Group By j.JobcardNo, d .Size Order by j.JobcardNo, d .Size", sSLIConstr)
                        Dim dsJCQty As New DataSet
                        daJCQty.Fill(dsJCQty)


                        Dim k As Integer = 0
                        Dim nSizeQty As Integer
                        For k = 0 To dsJCQty.Tables(0).Rows.Count - 1
                            sSize = dsJCQty.Tables(0).Rows(k).Item("Size").ToString
                            nSizeQty = Val(dsJCQty.Tables(0).Rows(k).Item("Quantity").ToString)

                            Dim daProdQty As New SqlDataAdapter("Select WorkOrderNo,Size, SUM(Quantity) AS Quantity from ProductionByProcess where WorkOrderNo = '" & sJobCardNo & _
                                                                "' And FromStage = '" & sFromStage & "' And Cast(Size As Decimal(18,1)) = '" & Val(sSize) & "' Group By WorkorderNo,Size Order by Size", sSLIConstr)
                            Dim dsProdQty As New DataSet
                            daProdQty.Fill(dsProdQty)

                            If dsProdQty.Tables(0).Rows.Count > 0 Then


                                nQuantity = Val(dsProdQty.Tables(0).Rows(0).Item("Quantity").ToString)

                                If nSizeQty > nQuantity Then

                                    nQuantity = nSizeQty - nQuantity
                                    Dim dsSelPBP As New DataSet

                                    Dim daSelPBP As New SqlDataAdapter("Select * from ProductStock where WorkOrderNo = '" & sJobCardNo & _
                                                                       "' And Stage = '" & sFromStage & "'  And Cast(Size As Decimal(18,1)) = '" & Val(sSize) & _
                                                                       "'", sSLIConstr)
                                    daSelPBP.Fill(dsSelPBP)


                                    sID = dsSelPBP.Tables(0).Rows(0).Item("ID").ToString

                                    Dim daUpdpbp As New SqlDataAdapter("Update ProductStock Set Quantity = '" & nQuantity & _
                                                                       "' Where ID = '" & sID & "'", sSLIConstr)
                                    Dim dsUpdPbp As New DataSet
                                    daUpdpbp.Fill(dsUpdPbp)
                                    dsUpdPbp.AcceptChanges()
                                End If
                            End If
                        Next






                    End If
                Next

            Next
        Next
        MsgBox("Completed")
        ''Production By Process
    End Sub

    Private Sub cbProductStock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProductStock.Click
        Me.Hide()
        frmProductStock.Show()
    End Sub

    Private Sub cbProductStockIH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProductStockIH.Click
        Me.Hide()
        frmProductStockforInHouse.Show()
    End Sub
End Class
