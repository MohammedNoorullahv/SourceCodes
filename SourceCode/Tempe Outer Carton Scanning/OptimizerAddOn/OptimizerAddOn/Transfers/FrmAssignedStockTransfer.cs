
using OptimizerAddOn.ComponentClasses;
using OptimizerAddOn.MDI;
using OptimizerAddOn.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizerAddOn.Transfers
{
    public partial class FrmAssignedStockTransfer : Form
    {

        #region DECLARATION
        string sIsLoaded, sIsProductionCompleted, sJobcardNo, sSalesOrder;

        CCStock ccStock;
        CCAssignedStock ccAssignedStock;
        CCAvailabilityStatus ccAvailabilityStatus;
        StrTransferData strTransferData;
        #endregion
        public FrmAssignedStockTransfer()
        {
            InitializeComponent();
            ccStock = new CCStock();
            ccAssignedStock = new CCAssignedStock();
            ccAvailabilityStatus = new CCAvailabilityStatus();
            strTransferData = new StrTransferData();
        }

        #region TOOLS CODING
        private void FrmAssignedStockTransfer_Load(object sender, EventArgs e)
        {
            if (MdlApp.nStoreCount == 1)
            {
                tbStoreCode.Visible = true;
                tbStoreCode.Text = MdlApp.sStoreCode;
                cbxStoreCode.Visible = false;
            }
            else
            {
                
                cbxStoreCode.DataSource = ccAssignedStock.LoadStoresList(MdlApp.sSystemId);
                cbxStoreCode.DisplayMember = "Location";
                //cbxStoreCode.ValueMember = "Location";

                tbStoreCode.Visible = false;
                cbxStoreCode.Visible = true;
                //LoadStoreCode();
            }
        }

        private void cbExit_Click(object sender, EventArgs e)
        {
            try
            {
                //var frm = this.frmUnAssignedStockTransfer;
                //frm.MdiParent = this;
                //frm.WindowState = FormWindowState.Maximized;
                //frm.BringToFront();
                //frm.Show();
                //ToolsSendToBack();

                //var frm = OptimizerAddOn.MDI.FrmMDI();

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

        private void cbLoadStock_Click(object sender, EventArgs e)
        {
            if (MdlApp.nStoreCount > 1)
            {
                MdlApp.sStoreCode = cbxStoreCode.Text;
            }
            ccStock.LoadExcessLimit();
            LoadStock();
            
        }

        private void cbLoadDemand_Click(object sender, EventArgs e)
        {
            try
            {
                if (MdlApp.nStoreCount > 1)
                {
                    MdlApp.sStoreCode = cbxStoreCode.Text;
                }
                if (rbJobCard.Checked == true)
                {
                    if (tbTo.Text.Trim() == "" || tbFrom.Text.Trim() == "")
                    {
                        MessageBox.Show("From & To Jobcard Not Available to load demand");
                    }
                    else
                    {
                        cbTransferStock.Enabled = false;
                        sIsProductionCompleted = "N";
                        if (ccAvailabilityStatus.CheckforJobcardExists(tbTo.Text.Trim(),"Jobcard") == false)
                        {
                            MessageBox.Show("Jobcard No. Does not exist");
                            return;
                        }
                        if (ccAvailabilityStatus.CheckforJobcardExists(tbTo.Text.Trim(), "Shipment Plan") == false)
                        {
                            MessageBox.Show("Jobcard No. Does not available in the active Shipment Plans");
                            //return;
                        }
                        if (ccAvailabilityStatus.CheckforJobcardExists(tbTo.Text.Trim(), "Shipment Pending") == false)
                        {
                            MessageBox.Show("Shipment is already done for the sales Order of this Jobcard");
                            //return;
                        }
                        if (ccAvailabilityStatus.CheckforJobcardExists(tbTo.Text.Trim(), "Production Pending") == true)
                        {
                            MessageBox.Show("Production is already done for this Jobcard. Only Packing Materials will be available");
                            sIsProductionCompleted = "Y";
                        }
                        sJobcardNo = tbTo.Text.Trim();
                        LoadJobcardDemand(tbFrom.Text.Trim(), tbTo.Text.Trim());
                        cbTransferStock.Enabled = true;
                    }
                }
                else if (rbSalesOrder.Checked == true)
                {
                    if (tbTo.Text.Trim() == "" || tbFrom.Text.Trim() == "")
                    {
                        MessageBox.Show("From & To Sales Order Not Available to load demand");
                    }
                    else
                    {
                        cbTransferStock.Enabled = false;
                        sIsProductionCompleted = "N";
                        if (ccAvailabilityStatus.CheckforSalesOrderExists(tbFrom.Text.Trim(), "Jobcard") == false)
                        {
                            MessageBox.Show("Sales Order No. Does not exist");
                            return;
                        }
                        if (ccAvailabilityStatus.CheckforSalesOrderExists(tbFrom.Text.Trim(), "Shipment Plan") == false)
                        {
                            MessageBox.Show("Sales Order No. Does not available in the active Shipment Plans");
                            //return;
                        }
                        if (ccAvailabilityStatus.CheckforSalesOrderExists(tbFrom.Text.Trim(), "Shipment Pending") == false)
                        {
                            MessageBox.Show("Shipment is already done for this Sales Order");
                            //return;
                        }
                        if (ccAvailabilityStatus.CheckforSalesOrderExists(tbFrom.Text.Trim(), "Production Pending") == true)
                        {
                            MessageBox.Show("Production is already done for this Sales Order. Only Packing Materials will be available");
                            sIsProductionCompleted = "Y";
                        }
                        sSalesOrder = tbFrom.Text.Trim();

                        LoadSalesOrderDemand(tbFrom.Text.Trim(),tbTo.Text.Trim());
                        cbTransferStock.Enabled = true;
                        //aasdf
                    }
                }


            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }

        private void cbTransferStock_Click(object sender, EventArgs e)
        {
            try
            {
                sJobcardNo = "";
                sSalesOrder = "";
                int NGrdRowCount = grdJobcardDemandV1.RowCount;
                int i = 0;
                string sItemSelected = "N";
                if (NGrdRowCount > 0)
                {
                    for (i = 0; i < NGrdRowCount; i++)
                    {
                        if (Convert.ToBoolean(grdJobcardDemandV1.GetDataRow(i)["Select"]) == true )
                        {
                            sItemSelected = "Y";
                            break;
                        }
                    }

                    if (sItemSelected == "N")
                    {
                        MessageBox.Show("Not a single material selected for Transferring");
                    }
                    else
                    {
                        for (i = 0; i < NGrdRowCount; i++)
                        {
                            if (Convert.ToBoolean(grdJobcardDemandV1.GetDataRow(i)["Select"]) == true)
                            {
                                if (Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["IssueQuantity"]) <= 0)
                                {
                                    MessageBox.Show("Material Selected, But quantity not entered for one/multiple materials");
                                    sItemSelected = "N";
                                }
                            }
                        }
                        if (sItemSelected == "N")
                        {
                            return;
                        }
                        i = 0;
                        for (i = 0; i < NGrdRowCount; i++)
                        {
                            if (Convert.ToBoolean(grdJobcardDemandV1.GetDataRow(i)["Select"]) == true)
                            {
                                if (Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["IssueQuantity"]) > Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["StkQuantity"]))
                                {
                                    MessageBox.Show("Selected Material exceeds the stock quantity");
                                    sItemSelected = "N";
                                }
                            }
                        }
                        if (sItemSelected == "N")
                        {
                            return;
                        }

                        if (CheckforExcessQuantity() == false)
                        {
                            MessageBox.Show("Selected material exceeds the demand Quantity. This quantity cannot be transferred");
                            return;
                        }
                    }
                }
                UpdateTransaction();
                //if (ccAssignedStock.StockTransaction(strTransferData) == true)
                //{
                    if (rbSalesOrder.Checked == true)
                        LoadSalesOrderDemand(tbFrom.Text.Trim(), tbTo.Text.Trim());
                    else
                        LoadJobcardDemand(tbFrom.Text.Trim(), tbTo.Text.Trim());
                //}
            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }
        #endregion

        #region FUNCTIONS

        private void LoadStock()
        {
            try
            {
                sIsLoaded = "N";
                grdAssignedStock.Visible = true;
                grdAssignedStock.BringToFront();
                grdAssignedStock.DataSource = ccAssignedStock.LoadAssignedMaterial();

                var grd = this.grdAssignedStockV1;
                grd.Columns[0].VisibleIndex = -1;
               
                grd.Columns[2].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                sIsLoaded = "Y";

            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }

        private void LoadJobcardDemand(string sFromJobcardNo, string sJobcardNo)
        {
            try
            {
                sIsLoaded = "N";
                grdJobcardDemand.Visible = true;
                grdJobcardDemand.BringToFront();
                if (sIsProductionCompleted == "Y")
                    grdJobcardDemand.DataSource = ccAssignedStock.LoadJobcardDemand(sFromJobcardNo, sJobcardNo, "Packing");
                else
                    grdJobcardDemand.DataSource = ccAssignedStock.LoadJobcardDemand(sFromJobcardNo, sJobcardNo, "Production");


                var grd = this.grdJobcardDemandV1;
                //grd.Columns[0].VisibleIndex = -1;
                //grd.Columns[1].VisibleIndex = -1;
                //grd.Columns[3].VisibleIndex = -1;
                //grd.Columns[8].VisibleIndex = -1;
                //grd.Columns[10].VisibleIndex = -1;
                //grd.Columns[12].VisibleIndex = -1;
                grd.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                grd.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grd.Columns[11].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                sIsLoaded = "Y";

            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }

        private void rbSalesOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSalesOrder.Checked == true)
            {
                lblFrom.Text = "From Sales Order No. :-";
                lblTo.Text = "To Sales Order No. :-";
            }
        }

        private void rbJobCard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbJobCard.Checked == true)
            {
                lblFrom.Text = "From Jobcard No. :-";
                lblTo.Text = "To Jobcard No. :-";
            }
        }

        private void tbFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadSalesOrderDemand(string sFromSalesOrder, string sToSalesOrder)
        {
            try
            {
                sIsLoaded = "N";
                grdJobcardDemand.Visible = true;
                grdJobcardDemand.BringToFront();
                if (sIsProductionCompleted == "Y")
                    grdJobcardDemand.DataSource = ccAssignedStock.LoadSalesOrderDemand(sFromSalesOrder, sToSalesOrder, "Packing");
                else
                    grdJobcardDemand.DataSource = ccAssignedStock.LoadSalesOrderDemand(sFromSalesOrder, sToSalesOrder, "Production");

                var grd = this.grdJobcardDemandV1;
                
                grd.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                grd.Columns[9].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                grd.Columns[11].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                sIsLoaded = "Y";

            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }

        private Boolean CheckforExcessQuantity()
        {
            try
            {
                int NGrdRowCount = grdJobcardDemandV1.RowCount;
                int i = 0;
                string sMaterialCode, sComponentGroup, sPlaceOfUse, sMaterialCategory;
                decimal dAssignedQty;

                if (NGrdRowCount > 0)
                {
                    for (i = 0; i < NGrdRowCount; i++)
                    {
                        if (Convert.ToBoolean(grdJobcardDemandV1.GetDataRow(i)["Select"]) == true)
                        {
                            sMaterialCode = grdJobcardDemandV1.GetDataRow(i)["MaterialCode"].ToString();
                            sComponentGroup = grdJobcardDemandV1.GetDataRow(i)["ComponentGroup"].ToString();
                            sPlaceOfUse = grdJobcardDemandV1.GetDataRow(i)["PlaceOfUse"].ToString();
                            sMaterialCategory = grdJobcardDemandV1.GetDataRow(i)["MaterialCategory"].ToString();
                            dAssignedQty = Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["AssignQuantity"]);
                            int j = 0;
                            decimal dIssueQuantity = 0;
                            for (j = 0; j < NGrdRowCount; j++)
                            {
                                if (Convert.ToBoolean(grdJobcardDemandV1.GetDataRow(j)["Select"]) == true && 
                                    grdJobcardDemandV1.GetDataRow(j)["MaterialCode"].ToString() == sMaterialCode &&
                                    grdJobcardDemandV1.GetDataRow(j)["MaterialCategory"].ToString() != "C" &&
                                    grdJobcardDemandV1.GetDataRow(j)["ComponentGroup"].ToString() == sComponentGroup &&
                                    grdJobcardDemandV1.GetDataRow(j)["PlaceOfUse"].ToString() == sPlaceOfUse)
                                {
                                    dIssueQuantity += Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["IssueQuantity"]);
                                }
                            }

                            decimal dDemandQuantity = 0;
                            if (rbSalesOrder.Checked == true)
                            {
                                dDemandQuantity = Convert.ToDecimal(ccAvailabilityStatus.SalesOrderDemandQty( sComponentGroup, tbTo.Text.Trim(), sMaterialCode));
                            }
                            else
                            {
                                dDemandQuantity = Convert.ToDecimal(ccAvailabilityStatus.JobcardDemandQty(sPlaceOfUse, sComponentGroup, tbTo.Text.Trim(), sMaterialCode));
                            }
                            
                            decimal dExcessQuantity;
                            if (sMaterialCategory == "A")
                            { 
                                if ((dIssueQuantity + dAssignedQty)  > dDemandQuantity)
                                {
                                    return false;
                                }
                            }
                            else if (sMaterialCategory == "B")
                            {
                                dExcessQuantity = (dDemandQuantity * MdlApp.CategoryB / 100);
                                if ((dIssueQuantity + dAssignedQty) > (dDemandQuantity + dExcessQuantity))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                    return true;

            }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
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

        private void UpdateTransaction()
        {
            try
            {
                MdlApp.sRemarks = tbRemarks.Text.Trim();
                int NGrdRowCount = grdJobcardDemandV1.RowCount;
                int i = 0;
                if (NGrdRowCount > 0)
                {
                    for (i = 0; i < NGrdRowCount; i++)
                    {
                        decimal dIssueQuantity, dExistingStock, dRevisedStock;
                        decimal dAssignedStock, dRevisedAssignedStock;
                        string sStockId;
                        if (Convert.ToBoolean(grdJobcardDemandV1.GetDataRow(i)["Select"]) == true)
                        {
                            sStockId = grdJobcardDemandV1.GetDataRow(i)["Id"].ToString();
                            dExistingStock = Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["StkQuantity"]);
                            dIssueQuantity = Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["IssueQuantity"]);
                            dAssignedStock = Convert.ToDecimal(grdJobcardDemandV1.GetDataRow(i)["AssignQuantity"]);
                            dRevisedStock = dExistingStock - dIssueQuantity;
                            dRevisedAssignedStock = dAssignedStock - dIssueQuantity;

                            strTransferData.StockId = sStockId;
                            strTransferData.ExistingStock = dExistingStock;
                            strTransferData.IssueQuantity = dIssueQuantity;
                            strTransferData.RevisedStock = dRevisedStock;
                            strTransferData.RevisedAssignStock = dRevisedAssignedStock;

                            if (rbSalesOrder.Checked == true)
                            {
                                strTransferData.FromSalesOrderNo = tbFrom.Text.Trim();
                                strTransferData.SalesOrderNo = tbTo.Text.Trim();
                                strTransferData.FromJobcardNo = "";
                                strTransferData.JobcardNo = "";
                            }
                            else if (rbJobCard.Checked == true)
                            {
                                strTransferData.FromSalesOrderNo = "";
                                strTransferData.SalesOrderNo = "";
                                strTransferData.FromJobcardNo = tbFrom.Text.Trim(); 
                                strTransferData.JobcardNo = tbTo.Text.Trim(); 
                            }

                            strTransferData.MaterialCode = grdJobcardDemandV1.GetDataRow(i)["MaterialCode"].ToString();
                            strTransferData.ComponentGroup = grdJobcardDemandV1.GetDataRow(i)["ComponentGroup"].ToString();
                            strTransferData.PlaceofUse = grdJobcardDemandV1.GetDataRow(i)["PlaceofUse"].ToString();
                            strTransferData.MaterialSize  = grdJobcardDemandV1.GetDataRow(i)["MaterialSize"].ToString();
                            ccAssignedStock.StockTransaction(strTransferData);
                            //ccAssignedStock.StockTransaction(sStockId, dExistingStock, dIssueQuantity, dRevisedStock, sJobcardNo, "");
                            //if (ccAssignedStock.StockTransaction(strTransferData) == true)
                            //{
                            //    if (rbSalesOrder.Checked == true)
                            //        LoadSalesOrderDemand(tbFrom.Text.Trim(), tbTo.Text.Trim());
                            //    else
                            //        LoadJobcardDemand(tbFrom.Text.Trim(), tbTo.Text.Trim());
                            //}
                        }
                    }
                }
                }
            catch (Exception Exp)
            {
                MessageBox.Show("ERROR " + Exp);
                using (StreamWriter stream = new FileInfo("E:\\ErrorLog.txt").AppendText())
                {
                    stream.WriteLine(Exp);
                    stream.WriteLine("Date : " + DateTime.Now);
                    stream.WriteLine("");
                    stream.WriteLine("");
                }
            }
        }
        #endregion


    }
}
