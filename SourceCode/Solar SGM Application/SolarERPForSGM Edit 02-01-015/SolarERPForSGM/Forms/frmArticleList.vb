Public Class frmArticleLIst


    Dim myccArticleMaster As New ccArticleMaster

    Dim mystrSolarArticleMaster4SGM4Print As New strSolarArticleMaster4SGM4Print

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles plSelectionCriteria.Enter

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try
       

        If chkbxArticleName.Checked = True Then
            mdlSGM.sSelectOption = "Customers Article - Article Wise"
            mdlSGM.sSelectedCustomer = cbxCustomer.Text
            mdlSGM.sSelectedArticle = cbxArticleName.Text
        ElseIf chkbxCodification.Checked = True Then
            mdlSGM.sSelectOption = "Customers Article - Codification Wise"
            mdlSGM.sSelectedCustomer = cbxCustomer.Text
            mdlSGM.sSelectedCodification = cbxCodification.Text
        Else
            mdlSGM.sSelectOption = "Customers Article"
            mdlSGM.sSelectedCustomer = cbxCustomer.Text
            mdlSGM.sSelectOption = "All Articles"
        End If


        LoadAllTransactions()

        ''Catch ex As Exception

        ''End Try
    End Sub



    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub


    Private Sub chkbxCodification_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxCodification.CheckedChanged
        Try
            If chkbxCodification.Checked = True Then
                chkbxArticleName.Checked = False
                cbxCodification.Enabled = True
                cbxArticleName.Enabled = False
                LoadCodification()
            Else
                cbxArticleName.Enabled = True
                cbxCodification.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Dim ngrdRowCount As Integer
    Private Sub LoadAllTransactions()
        ''Try


        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdArticleMasterV1.RowCount
        For i = 0 To ngrdRowCount
            grdArticleMasterV1.DeleteRow(i)
        Next
        ngrdRowCount = grdArticleMasterV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        grdArticleMaster.DataSource = myccArticleMaster.LoadArticles

        With grdArticleMasterV1

            .Columns(2).VisibleIndex = -1
            '.Columns(7).VisibleIndex = -1
            '.Columns(13).VisibleIndex = -1
            '.Columns(15).VisibleIndex = -1
            .Columns(4).VisibleIndex = 16

            .Columns(0).Width = 100
            .Columns(3).Width = 150
            
            .Columns(0).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            '.Columns(7).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
            '.Columns(8).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum


        End With
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCustomer()
        ''Try

        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCustomer.DataSource = Nothing : cbxCustomer.Items.Clear()
        cbxCustomer.DataSource = myccArticleMaster.LoadCustomer
        cbxCustomer.DisplayMember = "Client" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub LoadArticle()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxArticleName.DataSource = Nothing : cbxArticleName.Items.Clear()
        cbxArticleName.DataSource = myccArticleMaster.LoadArticleofCustomer
        cbxArticleName.DisplayMember = "SoleName" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub

    Private Sub LoadCodification()
        ''Try
        mdlSGM.sSelectedCustomer = cbxCustomer.Text
        mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")


        cbxCodification.DataSource = Nothing : cbxCodification.Items.Clear()
        cbxCodification.DataSource = myccArticleMaster.LoadCodificationofCustomer
        cbxCodification.DisplayMember = "CodificationNew" '': cbxArticleName.ValueMember = "PKID"


        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dpFrom.Value = DateAdd(DateInterval.Month, -1, dpTo.Value)
    End Sub

    Private Sub chkbxArticleName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxArticleName.CheckedChanged
        If chkbxArticleName.Checked = True Then
            chkbxCodification.Checked = False
            LoadArticle()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdArticleMaster.ExportToXlsx("E:\ArticleMaster.xlsx")
        MsgBox("Export Completed")

    End Sub

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        Dim i As Integer = 0

        myccArticleMaster.DelArticle4Print()

        For i = 0 To grdArticleMasterV1.RowCount - 1

            mystrSolarArticleMaster4SGM4Print.PKID = 0
            'mystrSolarArticleMaster4SGM4Print.Client = grdArticleMasterV1.GetDataRow(i).Item("Client").ToString
            mystrSolarArticleMaster4SGM4Print.Code = grdArticleMasterV1.GetDataRow(i).Item("Code").ToString
            mystrSolarArticleMaster4SGM4Print.Gender = grdArticleMasterV1.GetDataRow(i).Item("Gender").ToString
            mystrSolarArticleMaster4SGM4Print.SoleType = grdArticleMasterV1.GetDataRow(i).Item("SoleType").ToString
            mystrSolarArticleMaster4SGM4Print.SoleName = grdArticleMasterV1.GetDataRow(i).Item("SoleName").ToString
            mystrSolarArticleMaster4SGM4Print.Colour = grdArticleMasterV1.GetDataRow(i).Item("Colour").ToString
            mystrSolarArticleMaster4SGM4Print.Granules = grdArticleMasterV1.GetDataRow(i).Item("Granules").ToString
            mystrSolarArticleMaster4SGM4Print.NettWt = Val(grdArticleMasterV1.GetDataRow(i).Item("NettWt").ToString)
            mystrSolarArticleMaster4SGM4Print.LeatherSQM = grdArticleMasterV1.GetDataRow(i).Item("Leather-SQM").ToString
            mystrSolarArticleMaster4SGM4Print.SQMConsumption = Val(grdArticleMasterV1.GetDataRow(i).Item("SQM-Consumption").ToString)
            mystrSolarArticleMaster4SGM4Print.SQMDeclaredConsumption = Val(grdArticleMasterV1.GetDataRow(i).Item("SQM-DeclaredConsumption").ToString)
            mystrSolarArticleMaster4SGM4Print.LeatherKGS = grdArticleMasterV1.GetDataRow(i).Item("Leather-KGS").ToString
            mystrSolarArticleMaster4SGM4Print.KGSConsumption = Val(grdArticleMasterV1.GetDataRow(i).Item("KGS-Consumption").ToString)
            mystrSolarArticleMaster4SGM4Print.KGSDeclaredConsumption = Val(grdArticleMasterV1.GetDataRow(i).Item("KGS-DeclaredConsumption").ToString)
            mystrSolarArticleMaster4SGM4Print.DeclaredWt = Val(grdArticleMasterV1.GetDataRow(i).Item("DeclaredWt").ToString)
            mystrSolarArticleMaster4SGM4Print.Codification = grdArticleMasterV1.GetDataRow(i).Item("Codification").ToString
            mystrSolarArticleMaster4SGM4Print.CodificationNew = grdArticleMasterV1.GetDataRow(i).Item("CodificationNew").ToString

            myccArticleMaster.InsArticle4Print(mystrSolarArticleMaster4SGM4Print)
        Next
        mdlSGM.sReportType = "Article"
        frmReport.Show()

    End Sub
End Class