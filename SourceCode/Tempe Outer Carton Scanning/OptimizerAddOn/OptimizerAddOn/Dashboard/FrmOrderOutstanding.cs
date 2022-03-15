using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using OptimizerAddOn.ComponentClasses;
using OptimizerAddOn.MDI;
using OptimizerAddOn.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace OptimizerAddOn.Dashboard
{
    public partial class FrmOrderOutstanding : Form
    {
        CCDashboard ccDashboard;
        

        #region DECLARATION
        string sSeason, sBuyerGroupCode, sBuyerBuy, sArticleGroup, sArticle, sBuyerArticle, sSalesOrderID;
        string sColor, sCustomerOrderNo, sWorkOrderNo, sSalesOrderDetailsID, sSalesOrderNo, sId;
        int nOrderQuantity;

        int nTotalCutting, nCuttingBal, nUpperOutput, nUpperOutputBal, nUpperDispatch, nUpperDispatchBal, nFSCharging;
        int nFSChargingBal, nFSOutput, nFSOutputBal, nShippedQuantity, nShippedQuantityBal;

        string sSalesOrder;

     
        string SSelectedTab = "Customer Wise";
        int nRowNo;

        #endregion
        private void cbExit_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMDI frmMDI = new FrmMDI();
                frmMDI.ShowDialog();
                frmMDI.ToolsBringToFront();
                this.Hide();
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }


        public FrmOrderOutstanding()
        {
            InitializeComponent();

            ccDashboard = new CCDashboard();
        }

        private void rbAsperPO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAsperPO.Checked == true)
            {
                grdLEAPOOutstanding.DataSource = ccDashboard.LoadLeatherPurchaseOrderOS(sSalesOrder, "PO", sCustomerOrderNo);
            }
            else
            {
                grdLEAPOOutstanding.DataSource = ccDashboard.LoadLeatherPurchaseOrderOS(sSalesOrder, "CO", sCustomerOrderNo);
            }
        }

        private void rbUMAsperPO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUMAsperPO.Checked == true)
            {
                grdUpperMaterial.DataSource = ccDashboard.LoadUpperMaterials(sSalesOrder, "PO", sCustomerOrderNo);
            }
            else
            {
                grdUpperMaterial.DataSource = ccDashboard.LoadUpperMaterials(sSalesOrder, "CO", sCustomerOrderNo);
            }
        }

        private void rbSoleAsperPO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSoleAsperPO.Checked == true)
            {
                grdSoles.DataSource = ccDashboard.LoadSoles(sSalesOrder, "PO", sCustomerOrderNo);
            }
            else
            {
                grdSoles.DataSource = ccDashboard.LoadSoles(sSalesOrder, "CO", sCustomerOrderNo);
            }
        }

        private void rbFSAsperPO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFSAsperPO.Checked == true)
            {
                grdFSMaterials.DataSource = ccDashboard.LoadFullShoeMaterials(sSalesOrder, "PO", sCustomerOrderNo);
            }
            else
            {
                grdFSMaterials.DataSource = ccDashboard.LoadFullShoeMaterials(sSalesOrder, "CO", sCustomerOrderNo);
            }
        }

        private void rbPMAsperPO_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPMAsperPO.Checked == true)
            {
                grdPackingMaterials.DataSource = ccDashboard.LoadPackingMaterials(sSalesOrder, "PO", sCustomerOrderNo);
            }
            else
            {
                grdPackingMaterials.DataSource = ccDashboard.LoadPackingMaterials(sSalesOrder, "CO", sCustomerOrderNo);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage selectedTab = tabControl1.SelectedTab;
            int selectedIndex = tabControl1.SelectedIndex;
            tabControl1.SelectedTab = selectedTab;


            //TabPage tPage = TabControl.TabPageCollection(tabControl1_Selected);

            //    Dim tpage As TabPage = TabControl1.TabPages(TabControl1.SelectedIndex)
            //Button1.BackColor = tpage.BackColor
            //foreach (TabItem AllTabItems in tabControl1.ITE) // Change MyTabControl with your TabControl name
            //{
            //    if (!AllTabItems.IsSelected)
            //    {
            //        // do something for all unselected tabitems
            //    }
            //    else if (AllTabItems.IsSelected)
            //    {
            //        // do something for the selected tabitem
            //    }
            //    else if (AllTabItems.IsMouseOver)
            //    {
            //        // do something for mouseover tabitem
            //    }
            //}

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //TabPage SelectedTab = tabControl1.TabPages[e.Index];
            //Rectangle HeaderRect = tabControl1.GetTabRect(e.Index);
            //SolidBrush TextBrush = new SolidBrush(Color.Black);
            //StringFormat sf = new StringFormat();
            //sf.Alignment = StringAlignment.Center;
            //sf.LineAlignment = StringAlignment.Center;
            //if (e.State == DrawItemState.Selected)
            //{
            //    Font BoldFont = new Font(tabControl1.Font.Name, tabControl1.Font.Size, FontStyle.Bold);
            //    e.Graphics.DrawString(SelectedTab.Text, BoldFont, TextBrush, HeaderRect, sf);
            //}
            //else
            //    e.Graphics.DrawString(SelectedTab.Text, e.Font, TextBrush, HeaderRect, sf);
            //TextBrush.Dispose();

            TabControl tabControl = sender as TabControl;

            if (e.Index == tabControl.SelectedIndex)
            {
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
                    new Font(tabControl.Font, FontStyle.Bold),
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
                    tabControl.Font,
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }

        private void FrmOrderOutstanding_Load(object sender, EventArgs e)
        {
            LoadOutStandingBuyerWise();
            //grdOSCustomerWise.DataSource = ccDashboard.LoadOrderOutstanding();
            //var grd = this.grdOSCustomerWiseV1;
            //grd.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            //for (int i = 2; i <= 14; i++)
            //{
            //    grd.Columns[i].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //}

            //CheckProduction();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                SSelectedTab = tabControl1.SelectedTab.Text;

                switch (SSelectedTab)
                {
                    case "Customer Wise":
                        LoadOutStandingBuyerWise();
                        sBuyerGroupCode = "";
                        tbCustomer.Clear(); tbArticleGroup.Clear(); tbSalesOrderNo.Clear(); tbBuyerBuyNo.Clear(); tbCustomerOrderNo.Clear();
                        CheckProduction();
                        grdOSArticleGroupWise.DataSource = ccDashboard.LoadOrderOutstandingArticleGroupWise("");
                        break;
                    case "ArticleGroup Wise":
                        nRowNo = grdOSCustomerWiseV1.FocusedRowHandle;
                        sBuyerGroupCode = "";
                        if (nRowNo >= 0)
                        {
                            sBuyerGroupCode = grdOSCustomerWiseV1.GetDataRow(nRowNo)["BuyerGroupCode"].ToString();
                            tbCustomer.Text = sBuyerGroupCode;
                        }
                        grdOSArticleGroupWise.DataSource = ccDashboard.LoadOrderOutstandingArticleGroupWise(sBuyerGroupCode);

                        var grd1 = this.grdOSArticleGroupWiseV1;
                        grd1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        for (int i = 3; i <= 15; i++)
                        {
                            grd1.Columns[i].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        }
                        sArticleGroup = "";
                        tbArticleGroup.Clear(); tbSalesOrderNo.Clear(); tbBuyerBuyNo.Clear(); tbCustomerOrderNo.Clear();
                        grdSalesOrder.DataSource = ccDashboard.LoadOrderOutstandingSalesOrderWise("", "");
                        break;
                    case "Sales Order":
                        nRowNo = grdOSCustomerWiseV1.FocusedRowHandle;
                        sBuyerGroupCode = "";
                        if (nRowNo >= 0)
                        {
                            sBuyerGroupCode = grdOSCustomerWiseV1.GetDataRow(nRowNo)["BuyerGroupCode"].ToString();
                            tbCustomer.Text = sBuyerGroupCode;
                        }
                        nRowNo = grdOSArticleGroupWiseV1.FocusedRowHandle;
                        sArticleGroup = "";
                        if (nRowNo >= 0)
                        {
                            sArticleGroup = grdOSArticleGroupWiseV1.GetDataRow(nRowNo)["ArticleGroup"].ToString();
                            tbArticleGroup.Text = sArticleGroup;
                        }
                        grdSalesOrder.DataSource = ccDashboard.LoadOrderOutstandingSalesOrderWise(sBuyerGroupCode, sArticleGroup);
                        var grd2 = this.grdSalesOrderV1;
                        grd2.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        for (int i = 6; i <= 18; i++)
                        {
                            grd2.Columns[i].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        }
                        grd2.Columns[19].VisibleIndex = -1;
                        grd2.Columns[20].VisibleIndex = -1;

                        sSalesOrder = "";
                        tbSalesOrderNo.Clear(); tbBuyerBuyNo.Clear(); tbCustomerOrderNo.Clear();
                        grdPOOutstanding.DataSource = ccDashboard.LoadPurchaseOrderOS("");
                        break;
                    case "Purchase Outstanding":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            tbBuyerBuyNo.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Buyerbuy"].ToString();
                            tbCustomerOrderNo.Text = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                        }
                        grdPOOutstanding.DataSource = ccDashboard.LoadPurchaseOrderOS(sSalesOrder);
                        var grd3 = this.grdPOOutstandingV1;
                        grd3.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd3.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd3.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd3.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        break;
                    case "Leather":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            tbArticleinLeather.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Article"].ToString() + " [ " + grdSalesOrderV1.GetDataRow(nRowNo)["Color"].ToString() + " ]";
                        }
                        if (rbAsperPO.Checked == true)
                        {
                            grdLEAPOOutstanding.DataSource = ccDashboard.LoadLeatherPurchaseOrderOS(sSalesOrder, "PO", sCustomerOrderNo);
                        }
                        else
                        {
                            grdLEAPOOutstanding.DataSource = ccDashboard.LoadLeatherPurchaseOrderOS(sSalesOrder, "CO", sCustomerOrderNo);
                        }
                        var grd4 = this.grdLEAPOOutstandingV1;
                        grd4.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd4.Columns[5].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd4.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd4.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        break;
                    case "Upper Material":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            tbArticleinUPM.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Article"].ToString() + " [ " + grdSalesOrderV1.GetDataRow(nRowNo)["Color"].ToString() + " ]";
                        }
                        if (rbUMAsperPO.Checked == true)
                        {
                            grdUpperMaterial.DataSource = ccDashboard.LoadUpperMaterials(sSalesOrder, "PO", sCustomerOrderNo);
                        }
                        else
                        {
                            grdUpperMaterial.DataSource = ccDashboard.LoadUpperMaterials(sSalesOrder, "CO", sCustomerOrderNo);
                        }
                        var grd5 = this.grdUpperMaterialV1;
                        grd5.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd5.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd5.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd5.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        break;
                    case "Sole":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            tbArticleinSole.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Article"].ToString() + " [ " + grdSalesOrderV1.GetDataRow(nRowNo)["Color"].ToString() + " ]";
                        }
                        if (rbSoleAsperPO.Checked == true)
                        {
                            grdSoles.DataSource = ccDashboard.LoadSoles(sSalesOrder, "PO", sCustomerOrderNo);
                        }
                        else
                        {
                            grdSoles.DataSource = ccDashboard.LoadSoles(sSalesOrder, "CO", sCustomerOrderNo);
                        }
                        var grd6 = this.grdSolesV1;
                        grd6.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd6.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd6.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd6.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        break;
                    case "FS  Material":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            tbArticleinFSM.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Article"].ToString() + " [ " + grdSalesOrderV1.GetDataRow(nRowNo)["Color"].ToString() + " ]";
                        }
                        if (rbFSAsperPO.Checked == true)
                        {
                            grdFSMaterials.DataSource = ccDashboard.LoadFullShoeMaterials(sSalesOrder, "PO", sCustomerOrderNo);
                        }
                        else
                        {
                            grdFSMaterials.DataSource = ccDashboard.LoadFullShoeMaterials(sSalesOrder, "CO", sCustomerOrderNo);
                        }
                        var grd7 = this.grdFSMaterialsV1;
                        grd7.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd7.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd7.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd7.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        break;
                    case "Packing Material":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            tbArticleinPM.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Article"].ToString() + " [ " + grdSalesOrderV1.GetDataRow(nRowNo)["Color"].ToString() + " ]";
                        }
                        if (rbPMAsperPO.Checked == true)
                        {
                            grdPackingMaterials.DataSource = ccDashboard.LoadPackingMaterials(sSalesOrder, "PO", sCustomerOrderNo);
                        }
                        else
                        {
                            grdPackingMaterials.DataSource = ccDashboard.LoadPackingMaterials(sSalesOrder, "CO", sCustomerOrderNo);
                        }
                        var grd8 = this.grdPackingMaterialsV1;
                        grd8.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd8.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd8.Columns[7].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        grd8.Columns[8].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        break;
                    case "Production Status":
                        nRowNo = grdSalesOrderV1.FocusedRowHandle;
                        sSalesOrder = "";
                        sCustomerOrderNo = "";
                        if (nRowNo >= 0)
                        {
                            sSalesOrder = grdSalesOrderV1.GetDataRow(nRowNo)["SalesOrderNo"].ToString().Substring(0, 15);
                            tbSalesOrderNo.Text = sSalesOrder;
                            sCustomerOrderNo = grdSalesOrderV1.GetDataRow(nRowNo)["CustomerOrderNo"].ToString();
                            tbArticleinProduction.Text = grdSalesOrderV1.GetDataRow(nRowNo)["Article"].ToString() + " [ " + grdSalesOrderV1.GetDataRow(nRowNo)["Color"].ToString() + " ]";
                        }

                        grdProductionStatus.DataSource = ccDashboard.LoadProductionStatus(sSalesOrder);

                        var grd9 = this.grdProductionStatusV1;
                        grd9.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        grd9.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                        break;


                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }

        private void grdOSCustomerWiseV1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column == view.Columns["OrderQuantity"])
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            if (e.Column == view.Columns["TotalCutting"] || e.Column == view.Columns["CuttingBal"]
                || e.Column == view.Columns["UpperOutput"] || e.Column == view.Columns["UpperOutputBal"]
                || e.Column == view.Columns["UpperDispatch"] || e.Column == view.Columns["UpperDispatchBal"])
            {
                e.Appearance.BackColor = Color.LightSkyBlue;
            }
            if (e.Column == view.Columns["FSCharging"] || e.Column == view.Columns["FSChargingBal"]
                || e.Column == view.Columns["FSOutput"] || e.Column == view.Columns["FSOutputBal"])
            {
                e.Appearance.BackColor = Color.LightSalmon;
            }
            if (e.Column == view.Columns["ShippedQuantity"] || e.Column == view.Columns["ShippedQuantityBal"])
            {
                e.Appearance.BackColor = Color.LightSteelBlue;
            }
            if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["CuttingBal"])) < 0)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["UpperOutputBal"])) < 0)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["UpperDispatchBal"])) < 0)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["FSChargingBal"])) < 0)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["FSOutputBal"])) < 0)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Yellow;
            }
            if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ShippedQuantityBal"])) < 0)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Yellow;
            }
        }

        private void cbRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                pgbar.Visible = true;
                pgbar.BringToFront();
                pgbar.Value = 0;
                UpdateProduction();
                pgbar.Visible = false;
                CheckProduction();
                MessageBox.Show("Completed");
                //pgbar.Visible = false;
                LoadOutStandingBuyerWise();
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }

        private void cbExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    //saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    saveDialog.Filter = "Excel (2010) (.xlsx)|*.xlsx";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        //gridControl.ExportToXls(exportFilePath);
                        switch (SSelectedTab)
                        {
                            case "Customer Wise": grdOSCustomerWiseV1.ExportToXlsx(exportFilePath); break;
                            case "ArticleGroup Wise": grdOSArticleGroupWiseV1.ExportToXlsx(exportFilePath); break;
                            case "Sales Order": grdSalesOrderV1.ExportToXlsx(exportFilePath); break;
                            case "Purchase Outstanding": grdPOOutstandingV1.ExportToXlsx(exportFilePath); break;
                            case "Leather": grdLEAPOOutstandingV1.ExportToXlsx(exportFilePath); break;
                            case "Upper Material": grdUpperMaterialV1.ExportToXlsx(exportFilePath); break;
                            case "Sole": grdSolesV1.ExportToXlsx(exportFilePath); break;
                            case "FS  Material": grdFSMaterialsV1.ExportToXlsx(exportFilePath); break;
                            case "Packing Material": grdPackingMaterialsV1.ExportToXlsx(exportFilePath); break;
                            case "Production Status": grdProductionStatusV1.ExportToXlsx(exportFilePath); break;
                        }

                        //string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        //switch (fileExtenstion)
                        //{
                        //    case ".xls":
                        //        gridControl.ExportToXls(exportFilePath);
                        //        break;
                        //    case ".xlsx":
                        //        gridControl.ExportToXlsx(exportFilePath);
                        //        break;
                        //    case ".rtf":
                        //        gridControl.ExportToRtf(exportFilePath);
                        //        break;
                        //    case ".pdf":
                        //        gridControl.ExportToPdf(exportFilePath);
                        //        break;
                        //    case ".html":
                        //        gridControl.ExportToHtml(exportFilePath);
                        //        break;
                        //    case ".mht":
                        //        gridControl.ExportToMht(exportFilePath);
                        //        break;
                        //    default:
                        //        break;
                        //}

                        if (File.Exists(exportFilePath))
                        {
                            try
                            {
                                //Try to open the file and let windows decide how to open it.
                                System.Diagnostics.Process.Start(exportFilePath);
                            }
                            catch
                            {
                                String msg = "The file could not be opened." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                                MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            String msg = "The file could not be saved." + Environment.NewLine + Environment.NewLine + "Path: " + exportFilePath;
                            MessageBox.Show(msg, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }


            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())//ur file location//.AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }


        public Boolean CheckProduction()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmdProd = new SqlCommand();
                    cmdProd.Connection = conn;
                    cmdProd.CommandText = "SLI_Dashboard";
                    cmdProd.CommandType = CommandType.StoredProcedure;

                    cmdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPROD";

                    SqlDataAdapter adapterProd = new SqlDataAdapter(cmdProd);
                    DataTable dtProd = new DataTable();
                    adapterProd.Fill(dtProd);

                    if (dtProd.Rows.Count > 0)
                        cbRefresh.Enabled = true;
                    else
                    {
                        SqlCommand cmdUpDesp = new SqlCommand();
                        cmdUpDesp.Connection = conn;
                        cmdUpDesp.CommandText = "SLI_Dashboard";
                        cmdUpDesp.CommandType = CommandType.StoredProcedure;

                        cmdUpDesp.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELUPDISP";

                        SqlDataAdapter adapterUpDesp = new SqlDataAdapter(cmdUpDesp);
                        DataTable dtUpDesp = new DataTable();
                        adapterUpDesp.Fill(dtUpDesp);

                        if (dtUpDesp.Rows.Count > 0)
                            cbRefresh.Enabled = true;
                        else
                        {
                            SqlCommand cmdShipd = new SqlCommand();
                            cmdShipd.Connection = conn;
                            cmdShipd.CommandText = "SLI_Dashboard";
                            cmdShipd.CommandType = CommandType.StoredProcedure;

                            cmdShipd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "PENDINGSHIP";

                            SqlDataAdapter adapterShipd = new SqlDataAdapter(cmdShipd);
                            DataTable dtShipd = new DataTable();
                            adapterShipd.Fill(dtShipd);

                            if (dtShipd.Rows.Count > 0)
                                cbRefresh.Enabled = true;
                            else
                                cbRefresh.Enabled = false;
                        }
                    }
                }
                return true;
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    return false;
                }
            }
        }

        public Boolean UpdateProduction()
        {
            try
            {
                ClearAll();
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmdProd = new SqlCommand();
                    cmdProd.Connection = conn;
                    cmdProd.CommandText = "SLI_Dashboard";
                    cmdProd.CommandType = CommandType.StoredProcedure;

                    cmdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPROD";

                    SqlDataAdapter adapterProd = new SqlDataAdapter(cmdProd);
                    DataTable dtProd = new DataTable();
                    adapterProd.Fill(dtProd);

                    DataSet dsProd = new DataSet();
                    adapterProd.Fill(dsProd);

                    pgbar.Minimum = 0;
                    

                    int i = 0;
                    for (i = 0; i < dtProd.Rows.Count; i++)
                    {

                        pgbar.Maximum = dtProd.Rows.Count;
                        pgbar.Value = i;
                        lblProgress.Text = dtProd.Rows.Count.ToString() + "/" + dtProd.Rows.Count.ToString();
                        Thread.Sleep(100);

                        sWorkOrderNo = dsProd.Tables[0].Rows[i]["WorkOrderNo"].ToString();
                        sSalesOrderNo = sWorkOrderNo.ToString().Substring(0, 19);
                        sId = dsProd.Tables[0].Rows[i]["Id"].ToString();

                        SqlCommand cmdChkOS = new SqlCommand();
                        cmdChkOS.Connection = conn;
                        cmdChkOS.CommandText = "SLI_Dashboard";
                        cmdChkOS.CommandType = CommandType.StoredProcedure;

                        cmdChkOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "CHECKOS";
                        cmdChkOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                        SqlDataAdapter adapterChkOS = new SqlDataAdapter(cmdChkOS);
                        DataTable dtChkOS = new DataTable();
                        adapterChkOS.Fill(dtChkOS);

                        DataSet dsChkOS = new DataSet();
                        adapterChkOS.Fill(dsChkOS);
                        if (dtChkOS.Rows.Count == 0)
                        {
                            SqlCommand cmdSelJCD = new SqlCommand();
                            cmdSelJCD.Connection = conn;
                            cmdSelJCD.CommandText = "SLI_Dashboard";
                            cmdSelJCD.CommandType = CommandType.StoredProcedure;

                            cmdSelJCD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCD";
                            cmdSelJCD.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sWorkOrderNo;

                            SqlDataAdapter adapterSelJCD = new SqlDataAdapter(cmdSelJCD);
                            DataTable dtSelJCD = new DataTable();
                            adapterSelJCD.Fill(dtSelJCD);

                            DataSet dsSelJCD = new DataSet();
                            adapterSelJCD.Fill(dsSelJCD);
                            if (dtSelJCD.Rows.Count > 0)
                                sSalesOrderDetailsID = dsSelJCD.Tables[0].Rows[0]["SalesOrderDetailID"].ToString();
                            else
                            {
                                sSalesOrderDetailsID = "";
                                MessageBox.Show("Assortment Job Card");
                            }

                            SqlCommand cmdSelSOD = new SqlCommand();
                            cmdSelSOD.Connection = conn;
                            cmdSelSOD.CommandText = "SLI_Dashboard";
                            cmdSelSOD.CommandType = CommandType.StoredProcedure;

                            cmdSelSOD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOD";
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                            SqlDataAdapter adapterSelSOD = new SqlDataAdapter(cmdSelSOD);
                            DataTable dtSelSOD = new DataTable();
                            adapterSelSOD.Fill(dtSelSOD);

                            DataSet dsSelSOD = new DataSet();
                            adapterSelSOD.Fill(dsSelSOD);
                            if (dtSelSOD.Rows.Count > 0)
                            {
                                sSeason = dsSelSOD.Tables[0].Rows[0]["season"].ToString();
                                sBuyerGroupCode = dsSelSOD.Tables[0].Rows[0]["BuyerGroupCode"].ToString();
                                sBuyerBuy = dsSelSOD.Tables[0].Rows[0]["BuyerBuy"].ToString();
                                sArticleGroup = dsSelSOD.Tables[0].Rows[0]["ArticleGroup"].ToString();
                                sArticle = dsSelSOD.Tables[0].Rows[0]["Article"].ToString();
                                sBuyerArticle = dsSelSOD.Tables[0].Rows[0]["BuyerArticle"].ToString();
                                sColor = dsSelSOD.Tables[0].Rows[0]["ColorName"].ToString();
                                sCustomerOrderNo = dsSelSOD.Tables[0].Rows[0]["CustomerOrderNo"].ToString();
                                nOrderQuantity = Convert.ToInt32(dsSelSOD.Tables[0].Rows[0]["OrderQuantity"]);
                                //sSalesOrderID = dsSelSOD.Tables[0].Rows[0]["ID"].ToString();

                                ClearQuantity();
                                string sProcessName = dsProd.Tables[0].Rows[i]["ProcessName"].ToString();
                                int nProdQuantity = Convert.ToInt32(dsProd.Tables[0].Rows[i]["Quantity"]);
                                nShippedQuantity = nOrderQuantity;
                                nCuttingBal = nOrderQuantity;
                                nUpperOutputBal = nOrderQuantity;
                                nFSChargingBal = nOrderQuantity;
                                nFSOutputBal = nOrderQuantity;

                                switch (sProcessName)
                                {
                                    case "ULC":
                                        nTotalCutting = nProdQuantity;
                                        nCuttingBal = nOrderQuantity - nTotalCutting; break;
                                    case "COU":
                                        nUpperOutput = nProdQuantity;
                                        nUpperOutputBal = nOrderQuantity - nUpperOutput; break;
                                    case "KITTING":
                                        nFSCharging = nProdQuantity;
                                        nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                    case "KITTINGC":
                                        nFSCharging = nProdQuantity;
                                        nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                    case "CONVOUT":
                                        nFSOutput = nProdQuantity;
                                        nFSOutputBal = nOrderQuantity - nFSOutput; break;
                                    case "CONVOUTC":
                                        nFSOutput = nProdQuantity;
                                        nFSOutputBal = nOrderQuantity - nFSOutput; break;
                                    //default:
                                       
                                    //    break;
                                }

                                SqlCommand cmdInsOS = new SqlCommand();
                                cmdInsOS.Connection = conn;
                                cmdInsOS.CommandText = "SLI_Dashboard";
                                cmdInsOS.CommandType = CommandType.StoredProcedure;

                                cmdInsOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "InsOS";
                                cmdInsOS.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSeason", SqlDbType.VarChar)).Value = sSeason;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = sBuyerGroupCode;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerBuy", SqlDbType.VarChar)).Value = sBuyerBuy;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value = sArticleGroup;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticle", SqlDbType.VarChar)).Value = sArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerArticle", SqlDbType.VarChar)).Value = sBuyerArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mColor", SqlDbType.VarChar)).Value = sColor;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = sWorkOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderDetailsID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mOrderQuantity", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mTotalCutting", SqlDbType.Int)).Value = nTotalCutting;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCuttingBal", SqlDbType.Int)).Value = nCuttingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutput", SqlDbType.Int)).Value = nUpperOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutputBal", SqlDbType.Int)).Value = nUpperOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = 0;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSCharging", SqlDbType.Int)).Value = nFSCharging;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSChargingBal", SqlDbType.Int)).Value = nFSChargingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutput", SqlDbType.Int)).Value = nFSOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutputBal", SqlDbType.Int)).Value = nFSOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = 0;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                                SqlDataAdapter adapterInsOS = new SqlDataAdapter(cmdInsOS);
                                DataTable dtInsOS = new DataTable();
                                adapterInsOS.Fill(dtInsOS);
                            }
                        }
                        else
                        {
                            ClearQuantity();
                            string sOSId = dsChkOS.Tables[0].Rows[0]["Id"].ToString();
                            nTotalCutting = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["TotalCutting"]);
                            nCuttingBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["CuttingBal"]);
                            nUpperOutput = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperOutput"]);
                            nUpperOutputBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperOutputBal"]);
                            nUpperDispatch = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatch"]);
                            nUpperDispatchBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatchBal"]);
                            nFSCharging = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSCharging"]);
                            nFSChargingBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSChargingBal"]);
                            nFSOutput = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSOutput"]);
                            nFSOutputBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["FSOutputBal"]);
                            nShippedQuantity = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["ShippedQuantity"]);
                            nShippedQuantityBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["ShippedQuantityBal"]);
                            nOrderQuantity = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["OrderQuantity"]);

                            string sPBPId = dsProd.Tables[0].Rows[i]["ID"].ToString();
                            string sProcessName = dsProd.Tables[0].Rows[i]["ProcessName"].ToString();
                            int nProdQuantity = Convert.ToInt32(dsProd.Tables[0].Rows[i]["Quantity"]);

                            switch (sProcessName)
                            {
                                case "ULC":
                                    nTotalCutting = nTotalCutting + nProdQuantity;
                                    nCuttingBal = nOrderQuantity - nTotalCutting; break;
                                case "COU":
                                    nUpperOutput = nUpperOutput + nProdQuantity;
                                    nUpperOutputBal = nOrderQuantity - nUpperOutput; break;
                                case "KITTING":
                                    nFSCharging = nFSCharging + nProdQuantity;
                                    nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                case "KITTINGC":
                                    nFSCharging = nFSCharging + nProdQuantity;
                                    nFSChargingBal = nOrderQuantity - nFSCharging; break;
                                case "CONVOUT":
                                    nFSOutput = nFSOutput + nProdQuantity;
                                    nFSOutputBal = nOrderQuantity - nFSOutput; break;
                                case "CONVOUTC":
                                    nFSOutput = nFSOutput + nProdQuantity;
                                    nFSOutputBal = nOrderQuantity - nFSOutput; break;
                            }

                            SqlCommand cmdUpdOs = new SqlCommand();
                            cmdUpdOs.Connection = conn;
                            cmdUpdOs.CommandText = "SLI_Dashboard";
                            cmdUpdOs.CommandType = CommandType.StoredProcedure;

                            cmdUpdOs.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UpdOs";
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sOSId;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mTotalCutting", SqlDbType.Int)).Value = nTotalCutting;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mCuttingBal", SqlDbType.Int)).Value = nCuttingBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperOutput", SqlDbType.Int)).Value = nUpperOutput;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperOutputBal", SqlDbType.Int)).Value = nUpperOutputBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = nUpperDispatch;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nUpperDispatchBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSCharging", SqlDbType.Int)).Value = nFSCharging;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSChargingBal", SqlDbType.Int)).Value = nFSChargingBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSOutput", SqlDbType.Int)).Value = nFSOutput;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mFSOutputBal", SqlDbType.Int)).Value = nFSOutputBal;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = nShippedQuantity;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nShippedQuantityBal;


                            SqlDataAdapter adapterUpdOs = new SqlDataAdapter(cmdUpdOs);
                            DataTable dtUpdOs = new DataTable();
                            adapterUpdOs.Fill(dtUpdOs);
                        }

                        SqlCommand UpdProd = new SqlCommand();
                        UpdProd.Connection = conn;
                        UpdProd.CommandText = "SLI_Dashboard";
                        UpdProd.CommandType = CommandType.StoredProcedure;

                        UpdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDPROD";
                        UpdProd.Parameters.Add(new SqlParameter("@mId", SqlDbType.VarChar)).Value = sId;
                        UpdProd.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        SqlDataAdapter adapterUpdProd = new SqlDataAdapter(UpdProd);
                        DataTable dtUpdProd = new DataTable();
                        adapterUpdProd.Fill(dtUpdProd);
                    }

                    #region "UPPER DISPATCH QUANTITY UPDATE"
                    SqlCommand cmdUpDesp = new SqlCommand();
                    cmdUpDesp.Connection = conn;
                    cmdUpDesp.CommandText = "SLI_Dashboard";
                    cmdUpDesp.CommandType = CommandType.StoredProcedure;

                    cmdUpDesp.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELUPDISP";

                    SqlDataAdapter adapterUpDesp = new SqlDataAdapter(cmdUpDesp);
                    DataTable dtUpDesp = new DataTable();
                    adapterUpDesp.Fill(dtUpDesp);

                    DataSet dsUpDesp = new DataSet();
                    adapterUpDesp.Fill(dsUpDesp);

                    i = 0;
                    for (i = 0; i < dtUpDesp.Rows.Count; i++)
                    {

                        sWorkOrderNo = dsUpDesp.Tables[0].Rows[i]["WorkOrderNo"].ToString();
                        sSalesOrderNo = sWorkOrderNo.ToString().Substring(0, 19);
                        sId = dsUpDesp.Tables[0].Rows[i]["Id"].ToString();

                        //nUpperDispatch = Convert.ToInt32(dsUpDesp.Tables[0].Rows[i]["IssueQuantity"]);

                        SqlCommand cmdChkOS = new SqlCommand();
                        cmdChkOS.Connection = conn;
                        cmdChkOS.CommandText = "SLI_Dashboard";
                        cmdChkOS.CommandType = CommandType.StoredProcedure;

                        cmdChkOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "CHECKOS";
                        cmdChkOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                        SqlDataAdapter adapterChkOS = new SqlDataAdapter(cmdChkOS);
                        DataTable dtChkOS = new DataTable();
                        adapterChkOS.Fill(dtChkOS);

                        DataSet dsChkOS = new DataSet();
                        adapterChkOS.Fill(dsChkOS);
                        if (dtChkOS.Rows.Count == 0)
                        {
                            SqlCommand cmdSelJCD = new SqlCommand();
                            cmdSelJCD.Connection = conn;
                            cmdSelJCD.CommandText = "SLI_Dashboard";
                            cmdSelJCD.CommandType = CommandType.StoredProcedure;

                            cmdSelJCD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCD";
                            cmdSelJCD.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sWorkOrderNo;

                            SqlDataAdapter adapterSelJCD = new SqlDataAdapter(cmdSelJCD);
                            DataTable dtSelJCD = new DataTable();
                            adapterSelJCD.Fill(dtSelJCD);

                            DataSet dsSelJCD = new DataSet();
                            adapterSelJCD.Fill(dsSelJCD);
                            if (dtSelJCD.Rows.Count > 0)
                                sSalesOrderDetailsID = dsSelJCD.Tables[0].Rows[0]["SalesOrderDetailID"].ToString();
                            else
                            {
                                sSalesOrderDetailsID = "";
                                MessageBox.Show("Assortment Job Card");
                            }

                            SqlCommand cmdSelSOD = new SqlCommand();
                            cmdSelSOD.Connection = conn;
                            cmdSelSOD.CommandText = "SLI_Dashboard";
                            cmdSelSOD.CommandType = CommandType.StoredProcedure;

                            cmdSelSOD.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOD";
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                            cmdSelSOD.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                            SqlDataAdapter adapterSelSOD = new SqlDataAdapter(cmdSelSOD);
                            DataTable dtSelSOD = new DataTable();
                            adapterSelSOD.Fill(dtSelSOD);

                            DataSet dsSelSOD = new DataSet();
                            adapterSelSOD.Fill(dsSelSOD);
                            if (dtSelSOD.Rows.Count > 0)
                            {
                                sSeason = dsSelSOD.Tables[0].Rows[0]["season"].ToString();
                                sBuyerGroupCode = dsSelSOD.Tables[0].Rows[0]["BuyerGroupCode"].ToString();
                                sBuyerBuy = dsSelSOD.Tables[0].Rows[0]["BuyerBuy"].ToString();
                                sArticleGroup = dsSelSOD.Tables[0].Rows[0]["ArticleGroup"].ToString();
                                sArticle = dsSelSOD.Tables[0].Rows[0]["Article"].ToString();
                                sBuyerArticle = dsSelSOD.Tables[0].Rows[0]["BuyerArticle"].ToString();
                                sColor = dsSelSOD.Tables[0].Rows[0]["ColorName"].ToString();
                                sCustomerOrderNo = dsSelSOD.Tables[0].Rows[0]["CustomerOrderNo"].ToString();
                                nOrderQuantity = Convert.ToInt32(dsSelSOD.Tables[0].Rows[0]["OrderQuantity"]);
                                //sSalesOrderID = dsSelSOD.Tables[0].Rows[0]["ID"].ToString();

                                ClearQuantity();
                                nUpperDispatchBal = nOrderQuantity - nUpperDispatch;


                                SqlCommand cmdInsOS = new SqlCommand();
                                cmdInsOS.Connection = conn;
                                cmdInsOS.CommandText = "SLI_Dashboard";
                                cmdInsOS.CommandType = CommandType.StoredProcedure;

                                cmdInsOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "InsOS";
                                cmdInsOS.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = System.Guid.NewGuid().ToString();
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSeason", SqlDbType.VarChar)).Value = sSeason;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerGroupCode", SqlDbType.VarChar)).Value = sBuyerGroupCode;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerBuy", SqlDbType.VarChar)).Value = sBuyerBuy;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticleGroup", SqlDbType.VarChar)).Value = sArticleGroup;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mArticle", SqlDbType.VarChar)).Value = sArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mBuyerArticle", SqlDbType.VarChar)).Value = sBuyerArticle;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mColor", SqlDbType.VarChar)).Value = sColor;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCustomerOrderNo", SqlDbType.VarChar)).Value = sCustomerOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mWorkOrderNo", SqlDbType.VarChar)).Value = sWorkOrderNo;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderDetailsID", SqlDbType.VarChar)).Value = sSalesOrderDetailsID;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mOrderQuantity", SqlDbType.Int)).Value = nOrderQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mTotalCutting", SqlDbType.Int)).Value = nTotalCutting;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mCuttingBal", SqlDbType.Int)).Value = nCuttingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutput", SqlDbType.Int)).Value = nUpperOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperOutputBal", SqlDbType.Int)).Value = nUpperOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = nUpperDispatch;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nUpperDispatchBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSCharging", SqlDbType.Int)).Value = nFSCharging;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSChargingBal", SqlDbType.Int)).Value = nFSChargingBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutput", SqlDbType.Int)).Value = nFSOutput;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mFSOutputBal", SqlDbType.Int)).Value = nFSOutputBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = nShippedQuantity;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nShippedQuantityBal;
                                cmdInsOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                                SqlDataAdapter adapterInsOS = new SqlDataAdapter(cmdInsOS);
                                DataTable dtInsOS = new DataTable();
                                adapterInsOS.Fill(dtInsOS);
                            }
                        }
                        else
                        {
                            ClearQuantity();
                            nUpperDispatch = Convert.ToInt32(dsUpDesp.Tables[0].Rows[i]["IssueQuantity"]);
                            string sOSId = dsChkOS.Tables[0].Rows[0]["Id"].ToString();
                            if (sOSId == "1c519974-34c9-45c0-8540-811d6d5ba14b")
                            {
                                MessageBox.Show(sId);
                            }
                            //nUpperDispatchBal = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatchBal"]);
                            nOrderQuantity = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["OrderQuantity"]);

                            nUpperDispatch = Convert.ToInt32(dsChkOS.Tables[0].Rows[0]["UpperDispatch"]) + nUpperDispatch;
                            nUpperDispatchBal = nOrderQuantity - nUpperDispatch;

                            SqlCommand cmdUpdOs = new SqlCommand();
                            cmdUpdOs.Connection = conn;
                            cmdUpdOs.CommandText = "SLI_Dashboard";
                            cmdUpdOs.CommandType = CommandType.StoredProcedure;

                            cmdUpdOs.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UpdUppDisp";
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sOSId;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatch", SqlDbType.Int)).Value = nUpperDispatch;
                            cmdUpdOs.Parameters.Add(new SqlParameter("@mUpperDispatchBal", SqlDbType.Int)).Value = nUpperDispatchBal;

                            SqlDataAdapter adapterUpdOs = new SqlDataAdapter(cmdUpdOs);
                            DataTable dtUpdOs = new DataTable();
                            adapterUpdOs.Fill(dtUpdOs);
                        }

                        SqlCommand UpdProd = new SqlCommand();
                        UpdProd.Connection = conn;
                        UpdProd.CommandText = "SLI_Dashboard";
                        UpdProd.CommandType = CommandType.StoredProcedure;

                        UpdProd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDMATISS";
                        UpdProd.Parameters.Add(new SqlParameter("@mId", SqlDbType.VarChar)).Value = sId;
                        UpdProd.Parameters.Add(new SqlParameter("@mModifiedDate", SqlDbType.DateTime)).Value = DateTime.Now;
                        SqlDataAdapter adapterUpdProd = new SqlDataAdapter(UpdProd);
                        DataTable dtUpdProd = new DataTable();
                        adapterUpdProd.Fill(dtUpdProd);
                    }
                    #endregion

                    #region "SHIPMENT DISPATCH QUANTITY UPDATE"
                    SqlCommand cmdShipd = new SqlCommand();
                    cmdShipd.Connection = conn;
                    cmdShipd.CommandText = "SLI_Dashboard";
                    cmdShipd.CommandType = CommandType.StoredProcedure;

                    cmdShipd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "PENDINGSHIP";

                    SqlDataAdapter adapterShipd = new SqlDataAdapter(cmdShipd);
                    DataTable dtShipd = new DataTable();
                    adapterShipd.Fill(dtShipd);

                    DataSet dsShipd = new DataSet();
                    adapterShipd.Fill(dsShipd);
                    
                    i = 0;
                    for (i = 0; i < dtShipd.Rows.Count; i++)
                    {
                        sId = dsShipd.Tables[0].Rows[i]["Id"].ToString();
                        sSalesOrderNo = dsShipd.Tables[0].Rows[i]["CustWorkOrderNo"].ToString();

                        nOrderQuantity = 0; nShippedQuantity = 0; nShippedQuantityBal = 0;
                        
                        SqlCommand cmdSelOS = new SqlCommand();
                        cmdSelOS.Connection = conn;
                        cmdSelOS.CommandText = "SLI_Dashboard";
                        cmdSelOS.CommandType = CommandType.StoredProcedure;

                        cmdSelOS.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "CHECKOS";
                        cmdSelOS.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                        
                        SqlDataAdapter adapterSelOS = new SqlDataAdapter(cmdSelOS);
                        DataSet dsSelOS = new DataSet();
                        adapterSelOS.Fill(dsSelOS);

                        nShippedQuantity = Convert.ToInt32(dsShipd.Tables[0].Rows[i]["quantity"])  + Convert.ToInt32(dsSelOS.Tables[0].Rows[0]["ShippedQuantity"]);

                        SqlCommand cmdSelOrdQty = new SqlCommand();
                        cmdSelOrdQty.Connection = conn;
                        cmdSelOrdQty.CommandText = "SLI_Dashboard";
                        cmdSelOrdQty.CommandType = CommandType.StoredProcedure;

                        cmdSelOrdQty.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOD";
                        cmdSelOrdQty.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;

                        SqlDataAdapter adapterSelOrdQty = new SqlDataAdapter(cmdSelOrdQty);
                        DataSet dsSelOrdQty = new DataSet();
                        adapterSelOrdQty.Fill(dsSelOrdQty);
                        nOrderQuantity = Convert.ToInt32(dsSelOrdQty.Tables[0].Rows[0]["OrderQuantity"]);

                        nShippedQuantityBal = nOrderQuantity - nShippedQuantity;

                        SqlCommand cmdUpdOs = new SqlCommand();
                        cmdUpdOs.Connection = conn;
                        cmdUpdOs.CommandText = "SLI_Dashboard";
                        cmdUpdOs.CommandType = CommandType.StoredProcedure;

                        cmdUpdOs.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDSHIPD";
                        cmdUpdOs.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                        cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantity", SqlDbType.Int)).Value = nShippedQuantity;
                        cmdUpdOs.Parameters.Add(new SqlParameter("@mShippedQuantityBal", SqlDbType.Int)).Value = nShippedQuantityBal;

                        SqlDataAdapter adapterUpdOs = new SqlDataAdapter(cmdUpdOs);
                        DataTable dtUpdOs = new DataTable();
                        adapterUpdOs.Fill(dtUpdOs);

                        SqlCommand cmdUpdIdtl = new SqlCommand();
                        cmdUpdIdtl.Connection = conn;
                        cmdUpdIdtl.CommandText = "SLI_Dashboard";
                        cmdUpdIdtl.CommandType = CommandType.StoredProcedure;

                        cmdUpdIdtl.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "UPDINVDTL";
                        cmdUpdIdtl.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = sId;

                        SqlDataAdapter adapterUpdIdtl = new SqlDataAdapter(cmdUpdIdtl);
                        DataTable dtUpdIdtl = new DataTable();
                        adapterUpdIdtl.Fill(dtUpdIdtl);
                    }

                    #endregion
                }
                return true;
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR" + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");

                    return false;
                }
            }
        }

        public void ClearAll()
        {
            sSeason = ""; sBuyerGroupCode = ""; sBuyerBuy = ""; sArticleGroup = ""; sArticle = ""; sBuyerArticle = ""; sSalesOrderID = "";
            sColor = ""; sCustomerOrderNo = ""; sWorkOrderNo = ""; sSalesOrderDetailsID = ""; sSalesOrderNo = "";
            nOrderQuantity = 0;
        }

        public void ClearQuantity()
        {
            nTotalCutting = 0; nCuttingBal = 0; nUpperOutput = 0; nUpperOutputBal = 0; nUpperDispatch = 0; nUpperDispatchBal = 0; nFSCharging = 0;
            nFSChargingBal = 0; nFSOutput = 0; nFSOutputBal = 0; nShippedQuantity = 0; nShippedQuantityBal = 0;
        }

        private void LoadOutStandingBuyerWise()
        {
            grdOSCustomerWise.DataSource = ccDashboard.LoadOrderOutstanding();
            var grd = this.grdOSCustomerWiseV1;
            grd.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            grd.Columns[3].Caption = "Cutting";
            grd.Columns[5].Caption = "U-Output";
            grd.Columns[6].Caption = "U-Out Bal";
            grd.Columns[7].Caption = "U-Dispatch";
            grd.Columns[8].Caption = "U-Dis Bal";
            grd.Columns[13].Caption = "Ship'd";
            grd.Columns[14].Caption = "Ship Bal";

            //grd.Columns[2].DisplayFormat.FormatString = "n0";
            for (int i = 2; i <= 14; i++)
            {
                grd.Columns[i].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grd.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                grd.Columns[i].DisplayFormat.FormatString = "n0";
            }
        }
    }
}









