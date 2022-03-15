Option Explicit On
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid




Public Class frmAllinOne2
    Dim sConstr As String = Global.SolarERPForSGM.My.Settings.AHGroup '' Global.SMS_Billing.My.Settings.PSPettyCash
    Dim sCnn As New SqlConnection(sConstr)

    Dim myccAllinOne As New ccAllinOne

    Private Sub cbExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbExit.Click
        Me.Hide()
        Form1.Show() : Form1.BringToFront()
    End Sub


    Dim ngrdRowCount As Integer

    'Dim grid As GridControl = grdSalesOrderDetails
    'Dim gridview1 As GridView = New GridView(grid)
    'Dim gridview2 As GridView = New GridView(grid)

    'Dim node1 As GridLevelNode = grid.LevelTree.Nodes.Add("tmptbl_SalesOrderDetailforSGM", gridview1)
    'Dim node11 As GridLevelNode = node1.Nodes.Add("tmptbl_SalesOrderDetailforSGM", gridview2)


    Private Sub frmArticleList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM' table. You can move, or remove it, as needed.

        'TODO: This line of code loads data into the 'DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM' table. You can move, or remove it, as needed.
        'Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM)
        'TODO: This line of code loads data into the 'DstmpPurchaseOrder.tmptbl_PurchaseOrderforSGM' table. You can move, or remove it, as needed.
        Me.Tmptbl_PurchaseOrderforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderforSGM)
        Me.Tmptbl_PurchaseOrderDetailsforSGMTableAdapter.Fill(Me.DstmpPurchaseOrder.tmptbl_PurchaseOrderDetailsforSGM)

       
    End Sub


  
    Dim sTypeofOrder, sTypeofDocument As String

 
 
  
  
   Dim sMaterialTypeDescription, sMaterialSubTypeDescription As String


 
  
 
 


End Class