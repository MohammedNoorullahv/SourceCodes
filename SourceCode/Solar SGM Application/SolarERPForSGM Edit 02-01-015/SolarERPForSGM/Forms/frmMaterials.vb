Public Class frmMaterials


    Dim myccArticleMaster As New ccArticleMaster

    Dim mystrSolarArticleMaster4SGM4Print As New strSolarArticleMaster4SGM4Print

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cbReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbReferesh.Click
        ''Try




        LoadMaterials()

        ''Catch ex As Exception

        ''End Try
    End Sub



    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub



    Dim ngrdRowCount As Integer
    Private Sub LoadMaterials()
        ''Try


        Dim i As Integer = 0

Ab:
        ngrdRowCount = grdMaterialsV1.RowCount
        For i = 0 To ngrdRowCount
            grdMaterialsV1.DeleteRow(i)
        Next
        ngrdRowCount = grdMaterialsV1.RowCount
        If ngrdRowCount > 0 Then
            GoTo Ab
        End If

        'mdlSGM.dFromDate = Format(dpFrom.Value, "dd-MMMM-yyyy")
        'mdlSGM.dToDate = Format(dpTo.Value, "dd-MMMM-yyyy")

        grdMaterials.DataSource = myccArticleMaster.LoadMaterials

        With grdMaterialsV1

            .Columns(0).VisibleIndex = -1
            
            .Columns(1).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
            
        End With
        ''Catch Exp As Exception
        'HandleException(Me.Name, Exp)
        ''End Try
    End Sub



    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LoadMaterials()
    End Sub

 
    Private Sub Export2Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export2Excel.Click
        grdMaterials.ExportToXlsx("E:\MaterialMaster.xlsx")
        MsgBox("Export Completed to E:\")

    End Sub

    Private Sub cbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrint.Click
        

    End Sub
End Class