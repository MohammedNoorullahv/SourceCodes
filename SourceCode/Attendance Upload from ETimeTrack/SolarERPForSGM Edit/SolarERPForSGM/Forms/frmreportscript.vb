Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports AP_DataAccess_BL
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI
Imports System.Data.SqlClient

Public Class frmreportscript


    Dim xr As XtraReport = Nothing
    'Dim dsRpt As DataSet = Nothing
    Dim strStampType As String = String.Empty
    Dim dblSize As Double = 0


    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.SSPLAHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)


    Private Sub BeforePrint()
        'Private Sub xtraReport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
        'xr = CType(sender, XtraReport)
        'dsRpt = CType(xr.DataSource, DataSet)
        Dim daRpt As New SqlDataAdapter("Select * from vw_SolarJobCard where JobcardNo = 'S-17-0890-001'", sConstr)
        Dim dsRpt As New DataSet
        daRpt.Fill(dsRpt, "vw_SolarJobCard")

        Dim daRpt1 As New SqlDataAdapter("Select * from vw_SolarMouldMappingJob where toolcode = 'SLM-00543'", sConstr)
        Dim dsRpt1 As New DataSet
        daRpt1.Fill(dsRpt1, "vw_SolarMouldMappingJob")

        'MsgBox("Opened")
        Dim i As Integer = 0
        If dsRpt.Tables.Contains("vw_SolarJobCard") AndAlso dsRpt.Tables("vw_SolarJobCard").Rows.Count > 0 Then
            strStampType = Convert.ToString(dsRpt.Tables("vw_SolarJobCard").Rows(0)("StampType")).ToLower
            Dim strColumnName As String = String.Empty
            For Each dr As DataRow In dsRpt.Tables("vw_SolarJobCard").Rows
                For i = 1 To 18 Step 1
                    strColumnName = i.ToString("00")
                    If Not Double.TryParse(Convert.ToString(dr("Size" + strColumnName)), dblSize) Then
                        dblSize = 0
                    End If
                    dr("Size" + strColumnName) = dblSize
                Next
            Next

        End If

        If dsRpt1.Tables.Contains("vw_SolarMouldMappingJob") Then
            Dim dv As DataView = New DataView(dsRpt1.Tables("vw_SolarMouldMappingJob"))


            For Each drv As DataRowView In dv
                If Not Double.TryParse(Convert.ToString(drv("MappingSize")), dblSize) Then
                    dblSize = 0
                End If
                drv("MappingSize") = dblSize

                If Not Double.TryParse(Convert.ToString(drv("Size")), dblSize) Then
                    dblSize = 0
                End If
                drv("Size") = dblSize

                If Not Double.TryParse(Convert.ToString(drv("Stamp1")), dblSize) Then
                    dblSize = 0
                End If
                drv("Stamp1") = dblSize

                If Not Double.TryParse(Convert.ToString(drv("Stamp")), dblSize) Then
                    dblSize = 0
                End If
                drv("Stamp") = dblSize
            Next
        End If

        GetSize4Print()

    End Sub


    Private Sub getSize(ByVal strSize As String, ByRef strMould As String, ByRef strStamp As String, ByRef strStamp1 As String)
        If Not String.IsNullOrEmpty(strSize) Then

            Dim daRpt As New SqlDataAdapter("Select * from vw_SolarMouldMappingJob where toolcode = 'SLM-00543'  AND abbrev_='PACKING SECTION'", sConstr)
            Dim dsRpt As New DataSet
            daRpt.Fill(dsRpt, "vw_SolarMouldMappingJob")

            If dsRpt.Tables.Contains("vw_SolarMouldMappingJob") Then
                Dim dv As DataView = New DataView(dsRpt.Tables("vw_SolarMouldMappingJob"))



                Select strStampType
                    'Select Case Microsoft.VisualBasic.UCase(strStampType)
                    Case "NOSTAMP"
                        'No Stamping
                        If dv.Count > 0 Then
                            dv.RowFilter = "Size='" + strSize + "'"
                            If dv.Count > 0 Then
                                strMould = Convert.ToString(dv(0).Row("Size"))
                                'strStamp = Convert.ToString(dv(0).Row("Stamp"))
                                'strStamp1 = Convert.ToString(dv(0).Row("Stamp1"))
                            Else
                                dv.RowFilter = "MappingSize = '" + strSize + "'"
                                If dv.Count > 0 Then
                                    strMould = Convert.ToString(dv(0).Row("Size"))
                                    ' strStamp = Convert.ToString(dv(0).Row("Stamp"))
                                    'strStamp1 =  Convert.ToString(dv(0).Row("Stamp1"))
                                End If
                            End If
                        End If

                    Case "stamp1" 'Stamp 1
                        If dv.Count > 0 Then
                            dv.RowFilter = "Size='" + strSize + "'"
                            If dv.Count > 0 Then
                                strMould = Convert.ToString(dv(0).Row("Size"))
                                strStamp = Convert.ToString(dv(0).Row("Stamp"))
                                'strStamp1 = Convert.ToString(dv(0).Row("Stamp1"))
                            Else
                                dv.RowFilter = "MappingSize = '" + strSize + "'"
                                If dv.Count > 0 Then
                                    strMould = Convert.ToString(dv(0).Row("Size"))
                                    strStamp = Convert.ToString(dv(0).Row("Stamp"))
                                    'strStamp1 =  Convert.ToString(dv(0).Row("Stamp1"))
                                End If
                            End If
                        End If
                    Case "stamp2" 'Stamp 2
                        If dv.Count > 0 Then
                            dv.RowFilter = "Size='" + strSize + "'"
                            If dv.Count > 0 Then
                                strMould = Convert.ToString(dv(0).Row("Size"))
                                'strStamp =  Convert.ToString(dv(0).Row("Stamp"))
                                strStamp1 = Convert.ToString(dv(0).Row("Stamp1"))
                            Else
                                dv.RowFilter = "MappingSize = '" + strSize + "'"
                                If dv.Count > 0 Then
                                    strMould = Convert.ToString(dv(0).Row("Size"))
                                    'strStamp =  Convert.ToString(dv(0).Row("Stamp"))
                                    strStamp1 = Convert.ToString(dv(0).Row("Stamp1"))
                                End If
                            End If
                        End If
                    Case "stamp1-stamp2"
                        If dv.Count > 0 Then 'Stamp1 Stamp2
                            dv.RowFilter = "Size='" + strSize + "'"
                            If dv.Count > 0 Then
                                strMould = Convert.ToString(dv(0).Row("Size"))
                                strStamp = Convert.ToString(dv(0).Row("Stamp"))
                                strStamp1 = Convert.ToString(dv(0).Row("Stamp1"))
                            Else
                                dv.RowFilter = "MappingSize = '" + strSize + "'"
                                If dv.Count > 0 Then
                                    strMould = Convert.ToString(dv(0).Row("Size"))
                                    strStamp = Convert.ToString(dv(0).Row("Stamp"))
                                    strStamp1 = Convert.ToString(dv(0).Row("Stamp1"))
                                End If
                            End If
                        End If
                    Case "stamp2-stamp1"
                        If dv.Count > 0 Then 'Stamp2 Stamp 1
                            dv.RowFilter = "Size='" + strSize + "'"
                            If dv.Count > 0 Then
                                strMould = Convert.ToString(dv(0).Row("Size"))
                                strStamp1 = Convert.ToString(dv(0).Row("Stamp"))
                                strStamp = Convert.ToString(dv(0).Row("Stamp1"))
                            Else
                                dv.RowFilter = "MappingSize = '" + strSize + "'"
                                If dv.Count > 0 Then
                                    strMould = Convert.ToString(dv(0).Row("Size"))
                                    strStamp1 = Convert.ToString(dv(0).Row("Stamp"))
                                    strStamp = Convert.ToString(dv(0).Row("Stamp1"))
                                End If
                            End If
                        End If

                End Select
            End If
        End If
        'Return ""
    End Sub

                            Dim Gsize As String = String.Empty
                            Dim GStamp1 As String = String.Empty
                            Dim GStamp As String = String.Empty
                            Dim GMould As String = String.Empty

                            'Private Sub ShowControlsAsPerStampSize(ByVal lbl As DevExpress.XtraReports.UI.XRLabel, ByVal GMouldVal As String, ByVal GStamp As String, ByVal GStamp1 As String, ByRef GMould As String)
    Private Sub ShowControlsAsPerStampSize(ByVal lbl As String, ByVal GMouldVal As String, ByVal GStamp As String, ByVal GStamp1 As String, ByRef GMould As String)
        ' MessageBox.Show("Stamp Type : " + strStampType)
        If String.IsNullOrEmpty(GStamp) Then
            GStamp = "-"
        End If
        If String.IsNullOrEmpty(GStamp1) Then
            GStamp1 = "-"
        End If

        If String.IsNullOrEmpty(GMouldVal) Then
            GMould = "-"
        End If


        Select Case strStampType
            Case "nostamp"
                lblbl.Visible = False
            Case "stamp1"
                lblbl.Text = Convert.ToString(GStamp)
            Case "stamp2"
                lblbl.Text = Convert.ToString(GStamp1)
            Case "stamp1-stamp2"
                lblbl.Text = Convert.ToString(GStamp) + Environment.NewLine + "───" + Environment.NewLine + GStamp1
            Case "stamp2-stamp1"
                lblbl.Text = Convert.ToString(GStamp) + Environment.NewLine + "───" + Environment.NewLine + GStamp1
        End Select
    End Sub

    'Private Sub tableCell1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = strSize01.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""

    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize01stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell1.text = GMould
    '    Else
    '        lblSize01stamp.text = "-"
    '        tableCell1.text = "-"
    '    End If
    '    'MessageBox.Show(GMould)
    'End Sub


    'Private Sub tableCell9_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell38.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""

    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize02stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell9.text = GMould
    '    Else
    '        lblSize02stamp.text = "-"
    '        tableCell9.text = "-"
    '    End If
    'End Sub


    Dim tableCell4, lblSize03stamp As String
    Private Sub GetSize4Print()
        'Private Sub tableCell4_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
        Gsize = "24.5" 'tableCell39.text
        GMould = ""
        GStamp = ""
        GStamp1 = ""

        If Not String.IsNullOrEmpty(Gsize) Then
            getSize(Gsize, GMould, GStamp, GStamp1)
            ShowControlsAsPerStampSize(lblSize03stamp, GMould, GStamp, GStamp1, GMould)
            'tableCell4.text = GMould
            tableCell4 = GMould
        Else
            'lblSize03stamp.TEXT = "-"
            'tableCell4.text = "-"
            lblSize03stamp = "-"
            tableCell4 = "-"
        End If

    End Sub

    'Private Sub tableCell10_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell40.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize04stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell10.text = GMould
    '    Else
    '        lblSize04stamp.TEXT = "-"
    '        tableCell10.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell5_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell41.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""

    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize05stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell5.text = GMould
    '    Else
    '        lblSize05stamp.TEXT = "-"
    '        tableCell5.text = "-"
    '    End If

    'End Sub

    'Private Sub tableCell11_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell42.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""

    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize06stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell11.text = GMould
    '    Else
    '        lblSize06stamp.TEXT = "-"
    '        tableCell11.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell43.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""

    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize07stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell2.text = GMould
    '    Else
    '        lblSize07stamp.TEXT = "-"
    '        tableCell2.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell12_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell44.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize08stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell12.text = GMould
    '    Else
    '        lblSize08stamp.TEXT = "-"
    '        tableCell12.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell6_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell45.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize09stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell6.text = GMould
    '    Else
    '        lblSize09stamp.TEXT = "-"
    '        tableCell6.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell13_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell46.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize10stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell13.text = GMould
    '    Else
    '        lblSize10stamp.TEXT = "-"
    '        tableCell13.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell7_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell47.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize11stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell7.text = GMould
    '    Else
    '        lblSize11stamp.TEXT = "-"
    '        tableCell7.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell14_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell48.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize12stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell14.text = GMould
    '    Else
    '        lblSize12stamp.TEXT = "-"
    '        tableCell14.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell3_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell49.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize13stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell3.text = GMould
    '    Else
    '        lblSize13stamp.TEXT = "-"
    '        tableCell3.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell15_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell50.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize14stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell15.text = GMould
    '    Else
    '        lblSize14stamp.TEXT = "-"
    '        tableCell15.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell8_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell51.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize15stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell8.text = GMould
    '    Else
    '        lblSize15stamp.TEXT = "-"
    '        tableCell8.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell16_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell52.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize16stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell16.text = GMould
    '    Else
    '        lblSize16stamp.TEXT = "-"
    '        tableCell16.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell17_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell53.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""
    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize17stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell17.text = GMould
    '    Else
    '        lblSize17stamp.TEXT = "-"
    '        tableCell17.text = "-"
    '    End If
    'End Sub

    'Private Sub tableCell18_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    '    Gsize = tableCell54.text
    '    GMould = ""
    '    GStamp = ""
    '    GStamp1 = ""

    '    If Not String.IsNullOrEmpty(Gsize) Then
    '        getSize(Gsize, GMould, GStamp, GStamp1)
    '        ShowControlsAsPerStampSize(lblSize18stamp, GMould, GStamp, GStamp1, GMould)
    '        tableCell18.text = GMould
    '    Else
    '        lblSize18stamp.TEXT = "-"
    '        tableCell18.text = "-"
    '    End If
    'End Sub




    '


    'Public Function InitDatabasePath(ByVal strFilePath As String, ByRef app_DataAccess As AppDataAccess) As Boolean
    '    app_DataAccess = Nothing
    '    Dim strDBServer, strDBName, strUser, strDBType As String
    '    Dim strPass As String = String.Empty
    '    Try
    '        strDBServer = String.Empty
    '        strDBName = String.Empty
    '        strUser = String.Empty

    '        strDBType = String.Empty
    '        strPass = String.Empty

    '        If Not String.IsNullOrEmpty(strFilePath) Then
    '            Dim strContent As String = String.Empty
    '            strContent = strFilePath ' IO.File.ReadAllText(strFilePath)
    '            If Not String.IsNullOrEmpty(strContent) Then
    '                Dim strSplit() As String = strContent.Split("~")
    '                If strSplit.Length > 5 Then
    '                    strDBType = strSplit(0)
    '                    If strDBType.ToLower.Equals("access") Then
    '                        strDBName = strSplit(5)
    '                    ElseIf strDBType.ToLower.Equals("sql") Then

    '                        strDBServer = strSplit(1)
    '                        strDBName = strSplit(2)
    '                        strUser = strSplit(3)
    '                        strPass = strSplit(4)

    '                    End If
    '                    Try
    '                        If strDBType.ToLower.Equals("sql") Then
    '                            app_DataAccess = New AppDataAccess(AppDataAccess.DatabaseProviders.SQL, strDBServer, strDBName, strUser, strPass)
    '                        ElseIf strDBType.ToLower.Equals("access") Then
    '                            app_DataAccess = New AppDataAccess(AppDataAccess.DatabaseProviders.OLEDB, strDBName)
    '                        End If
    '                    Catch ex As Exception
    '                        ' MessageBox.Show(ex.Message + "[Main Form][InitDatabasePath]")
    '                        'Return False
    '                    End Try
    '                    If app_DataAccess IsNot Nothing Then
    '                        Return True
    '                    End If

    '                End If
    '            End If
    '        End If

    '    Catch ex As Exception
    '        'MessageBox.Show(ex.Message + " [frmMainView][InitDatabasePath]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    '    Return False
    'End Function


    'Dim app_DataAccess As appDataAccess = Nothing
    '
    Public Function getImageForDesignFromPath(ByVal strPath As String) As Image
        Dim img As Image
        Try
            Dim imgFile As New System.IO.FileStream(strPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite)
            Dim fileBytes(imgFile.Length) As Byte
            imgFile.Read(fileBytes, 0, fileBytes.Length)
            imgFile.Close()
            Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(fileBytes)
            img = Image.FromStream(ms)

        Catch ex As Exception
            img = Nothing
        End Try
        Return img
    End Function

    '
    ' Public Function GetImageForTheArticleByID(ByVal strTableName As String, ByVal strImageFileName As String, ByVal strCompanyCode As String, ByVal blnGetImageTableDataByCompany As Boolean) As Image
    '     Try

    '        Dim strQuery As String = String.Empty
    '       Dim dtTemp As DataTable = Nothing
    '      Dim dtImage As DataTable = Nothing
    '     If InitDatabasePath("SQL~192.168.1.5~AHGroup~erp~KHLIerp1234~", app_DataAccess) Then
    '
    '
    '               strQuery = "Select * From ImagePaths where TableName = '" + strTableName + "'"
    '              If blnGetImageTableDataByCompany Then
    '                 strQuery += " And CompanyCode = '" + strCompanyCode + "'"
    '            End If
    '           app_DataAccess.GetDataTableFromDataBase(strQuery, dtImage)
    '          If dtImage IsNot Nothing AndAlso dtImage.Rows.Count > 0 Then
    '
    '                   Dim strImageCompletePath As String = Convert.ToString(dtImage.Rows(0).Item("ImagePath"))
    '                  strImageCompletePath += "\" + strImageFileName + ".JPG"
    '
    '
    '                   If System.IO.File.Exists(strImageCompletePath) Then
    '
    '
    '                       Return getImageForDesignFromPath(strImageCompletePath)
    '                  End If
    '             End If
    '        End If
    '   Catch ex As Exception
    '  End Try
    ' Return Nothing
    '   End 'Function
    ''
    '
    '
    ' 
    '
    'Private Sub pictureBox2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
    ' Try
    '	pictureBox2.Image =	GetImageForTheArticleByID("Materials",label37.Text,"SSPL",True)
    ' Catch ex As Exception
    '       End Try
    'End Sub




    Private Sub frmreportscript_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BeforePrint()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BeforePrint()
    End Sub
End Class