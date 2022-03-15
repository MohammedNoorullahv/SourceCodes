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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizerAddOn.Packing
{
    public partial class FrmTempeScanning : Form
    {
        CCPacking ccPacking;

        StrOuterCartonPackedInfo strOuterCartonPackedInfo;

        int nOrderQty, nPkdQty, nBalQty, nScanningSize, nSizesCountinSingleBox;
        int nBoxCount, nPkdCount, nBalCount;
        int nBoxQty, nBoxPkdQty, nBoxBalQty;



        string sSalesOrderDetailsId = "";
        string sIsScanned;

        private void grdPackingStatusV1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                for (int i = 1; i <= 20; i++)
                {

                    if (e.Column.FieldName == i.ToString())
                    {

                        string status = "0";
                        status = View.GetRowCellDisplayText(e.RowHandle, View.Columns[i.ToString()]);

                        if (status != "")
                        {
                            if (status.Substring(status.Length - 1) == "0")
                            {
                                e.Appearance.ForeColor = Color.DarkRed;
                                e.Appearance.FontStyleDelta = FontStyle.Bold;
                            }
                            else if (status.Substring(status.Length - 1) == "1")
                            {
                                e.Appearance.BackColor = Color.Green;
                                e.Appearance.ForeColor = Color.Green;
                            }
                        }
                    }
                }
                //    if (e.Column.FieldName == "1")
                //    {
                //        string status = View.GetRowCellDisplayText(e.RowHandle, View.Columns["1"]);
                //        if (status.Substring(status.Length - 1) == "0")
                //        {
                //            e.Appearance.ForeColor = Color.DarkRed;
                //            e.Appearance.FontStyleDelta = FontStyle.Bold;
                //        }
                //        else if (status.Substring(status.Length - 1) == "1")
                //        {
                //            e.Appearance.BackColor = Color.Green;
                //            e.Appearance.ForeColor = Color.Green;
                //        }
                //        //e.Appearance.ForeColor = Color.DarkRed;
                //    //e.Handled = true;
                //}
            }
        }

        private void tbBoxNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbOuterBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmTempeScanning_Load(object sender, EventArgs e)
        {

        }

        private void tbBoxNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    tbOuterBarcode.Focus();
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

        public FrmTempeScanning()
        {
            InitializeComponent();
            ccPacking = new CCPacking();
            strOuterCartonPackedInfo = new StrOuterCartonPackedInfo();
        }

        private void tbOuterBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (sIsScanned == "Y")
                    {
                        tbOuterBarcode.Clear();
                        return;
                    }

                    if (tbOuterBarcode.Text.Trim() == "")
                    {
                        MessageBox.Show("Invalid Outer Barcode");
                        sIsScanned = "Y";
                    }
                    else if (tbOuterBarcode.Text.Trim().ToString().Length != 18)
                    {
                        MessageBox.Show("Invalid Outer Barcode");
                        sIsScanned = "Y";
                    }
                    else
                    {
                        string sOuterCartonNo = tbOuterBarcode.Text.Trim();

                        if (Convert.ToInt32(sOuterCartonNo.Substring(11, 2)) != nScanningSize)
                        {
                            MessageBox.Show("Invalid / Mismatch Outer Carton Scanned");
                            sIsScanned = "Y";
                        }
                        else
                        {
                            LoadOuterCartonPackedInfo();
                            if ((nBoxQty - nBoxPkdQty) <= 0)
                            {
                                MessageBox.Show("All the Inner Boxes of this Carton is Scanned Already");
                                sIsScanned = "Y";
                            }
                            else
                            {
                                sIsScanned = "N";
                                tbInnerBarcode.Focus();
                            }
                        }
                        //tbBoxNo.Focus();
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

        private void tbInnerBarcode_KeyPress(object sender, KeyPressEventArgs e)
        
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (sIsScanned == "Y")
                    {
                        tbInnerBarcode.Clear();
                        return;
                    }

                    if (tbInnerBarcode.Text.Trim().ToString().Substring(0,13) != tbOuterBarcode.Text.Trim().ToString().Substring(0, 13))
                    {
                        MessageBox.Show("Outer & Inner Barcode Mismatch. Can't Proceed");
                        return;
                    }

                    if (tbInnerBarcode.Text.Trim() == "")
                    {
                        MessageBox.Show("Invalid Inner Barcode");
                        sIsScanned = "Y";
                    }
                    else if (tbInnerBarcode.Text.Trim().ToString().Length != 14)
                    {
                        MessageBox.Show("Invalid Inner Barcode");
                        sIsScanned = "Y";
                    }
                    else
                    {
                        string sInnerCartonNo = tbInnerBarcode.Text.Trim();

                        if (Convert.ToInt32(sInnerCartonNo.Substring(11, 2)) != nScanningSize)
                        {
                            MessageBox.Show("Invalid / Mismatch Inner Carton Scanned");
                            sIsScanned = "Y";
                        }
                        else
                        {

                            strOuterCartonPackedInfo.JobcardNo = tbJobcardNo.Text.Trim();
                            strOuterCartonPackedInfo.BoxSlNo = Convert.ToInt32(tbBoxNo.Text.Trim());
                            strOuterCartonPackedInfo.CartonNo = tbOuterBarcode.Text.Trim();
                            strOuterCartonPackedInfo.InnerBoxNo = tbInnerBarcode.Text.Trim();
                            strOuterCartonPackedInfo.Quantity = 1;
                            strOuterCartonPackedInfo.Size = nScanningSize;
                            strOuterCartonPackedInfo.PackedOn = DateTime.Now;
                            ccPacking.InsertOuterCartonPackedInfo(strOuterCartonPackedInfo);
                            
                            LoadOuterCartonPackedInfo();
                            tbInnerBarcode.Clear();
                            if ((nBoxQty - nBoxPkdQty) <= 0)
                            {

                                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                                {

                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = "Update PackingDetail Set IsPacked = @mIsPacked, PackedOn = @mPackedOn Where SalesOrderDetailID = @mSalesOrderDetailsId And CartonNo = @mCartonNo";
                                    cmd.CommandType = CommandType.Text;

                                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderDetailsId", SqlDbType.VarChar)).Value = sSalesOrderDetailsId;
                                    cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = Convert.ToInt32(tbBoxNo.Text.Trim());
                                    cmd.Parameters.Add(new SqlParameter("@mIsPacked", SqlDbType.Bit)).Value = true;
                                    cmd.Parameters.Add(new SqlParameter("@mPackedOn", SqlDbType.DateTime)).Value = DateTime.Now;


                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adapter.Fill(ds);

                                    LoadPackedInfo();
                                }

                                tbBoxQty.Clear();
                                tbBoxScndQty.Clear();
                                tbBoxBalQty.Clear();

                                tbOuterBarcode.Clear();
                                tbBoxNo.Clear();
                                tbBoxNo.Focus();
                            }
                            else
                            {
                                tbInnerBarcode.Focus();
                            }
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

        private void tbBoxNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (tbBoxNo.Text.Trim() != "")
                {
                    LoadPackingDetailInfo();
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

        private void LoadPackingDetailInfo()
        {
            try
            {
                if (tbBoxNo.Text.Trim() != "")
                {
                    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                    {

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from PackingDetail Where SalesOrderDetailID = @mSalesOrderDetailsId And CartonNo = @mCartonNo";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new SqlParameter("@mSalesOrderDetailsId", SqlDbType.VarChar)).Value = sSalesOrderDetailsId;
                        cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.Int)).Value = Convert.ToInt32(tbBoxNo.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsPacked"]) == true)
                            {
                                MessageBox.Show("This Carton Already Scanned");
                                sIsScanned = "Y";
                            }
                            else
                            {
                                nBoxQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]);
                                tbBoxQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();

                                nScanningSize = 0; nSizesCountinSingleBox = 0;
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size01"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size02"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size03"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size04"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size05"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size06"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size07"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size08"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size09"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size10"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size11"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size12"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size13"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size14"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size15"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size16"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size17"]); nSizesCountinSingleBox += 1; }
                                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"]) > 0) { nScanningSize = Convert.ToInt32(ds.Tables[0].Rows[0]["Size18"]); nSizesCountinSingleBox += 1; }

                                sIsScanned = "N";
                                tbOuterBarcode.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Carton No. Cannot be Scanned");
                            sIsScanned = "Y";
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
        private void tbJobcardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (tbJobcardNo.Text.ToString().Length != 22)
                    {
                        MessageBox.Show("Invalid Jobcard No.");
                    }
                    else
                    {
                        string sJobcardNo = tbJobcardNo.Text.Trim();
                        tbSalesOrderNo.Text = sJobcardNo.Substring(0, 19);

                        using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                        {

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "Select * from SalesOrderDetails Where CustWorkOrderNo = @mCustWorkOrderNo";
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(new SqlParameter("@mCustWorkOrderNo", SqlDbType.VarChar)).Value = tbSalesOrderNo.Text.Trim();

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            nOrderQty = Convert.ToInt32(ds.Tables[0].Rows[0]["OrderQuantity"]);
                            tbTotalQty.Text = ds.Tables[0].Rows[0]["OrderQuantity"].ToString();
                            sSalesOrderDetailsId = ds.Tables[0].Rows[0]["Id"].ToString();

                            LoadPackedInfo();
                        }

                        tbBoxNo.Focus();
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

        private void LoadPackedInfo()
        {
            try
            {
                string sOrderId = sSalesOrderDetailsId;

                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandText = "Select  IsNull(Count(CartonNo),0) As BoxCnt, IsNull(Sum(Quantity),0) As PkdQty from PackingDetail Where SalesOrderDetailID = @mSalesOrderDetailsId";
                    cmd1.CommandType = CommandType.Text;

                    cmd1.Parameters.Add(new SqlParameter("@mSalesOrderDetailsId", SqlDbType.VarChar)).Value = sOrderId;

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);

                    nBoxCount = Convert.ToInt32(ds1.Tables[0].Rows[0]["BoxCnt"]);
                    tbTotalBox.Text = nBoxCount.ToString();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select  IsNull(Count(CartonNo),0) As BoxCnt, IsNull(Sum(Quantity),0) As PkdQty from PackingDetail Where SalesOrderDetailID = @mSalesOrderDetailsId And IsPacked = '1'";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderDetailsId", SqlDbType.VarChar)).Value = sOrderId;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);


                    nPkdQty = Convert.ToInt32(ds.Tables[0].Rows[0]["PkdQty"]);
                    tbScannedQty.Text = nPkdQty.ToString();

                    nPkdCount = Convert.ToInt32(ds.Tables[0].Rows[0]["BoxCnt"]);
                    tbScannedBox.Text = nPkdCount.ToString();

                    tbBalQty.Text = (nOrderQty - nPkdQty).ToString();
                    tbBalBox.Text = (nBoxCount - nPkdCount).ToString();


                    grdPackingStatus.Visible = true;
                    grdPackingStatus.BringToFront();
                    grdPackingStatus.DataSource = ccPacking.LoadPackingStatus(sOrderId);

                    var grd = this.grdPackingStatusV1;
                    grd.Columns[0].VisibleIndex = -1;



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

        private void LoadOuterCartonPackedInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from TempeOuterCartonPackedInfo Where JobcardNo = @mFKJobcardId And BoxSlNo = @mBoxSlNo And CartonNo = @mCartonNo";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@mFKJobcardId", SqlDbType.VarChar)).Value = tbJobcardNo.Text.Trim();
                    cmd.Parameters.Add(new SqlParameter("@mBoxSlNo", SqlDbType.Int)).Value = Convert.ToInt32(tbBoxNo.Text.Trim());
                    cmd.Parameters.Add(new SqlParameter("@mCartonNo", SqlDbType.VarChar)).Value = tbOuterBarcode.Text.Trim();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    nBoxPkdQty = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            nBoxPkdQty = nBoxPkdQty + (Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]));
                        }
                    }
                    tbBoxScndQty.Text = nBoxPkdQty.ToString();
                    tbBoxBalQty.Text = (nBoxQty - nBoxPkdQty).ToString();

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
    }
}




