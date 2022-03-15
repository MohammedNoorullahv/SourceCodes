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
    public partial class FrmGeneratePacking : Form
    {
        CCPacking ccPacking;

        StrPacking strPacking;
        int nBox01Count, nBox02Count, nBox03Count, nBox04Count, nBox05Count, nBox06Count, nBox07Count, nBox08Count, nBox09Count, nBox10Count, nBox11Count, nBox12Count, nBox13Count, nBox14Count, nBox15Count, nBox16Count, nBox17Count, nBox18Count;
        string sBuyerGroupCode, sSalesOrderDetailId, sArticleGroup, sArticle, sColorCode, sMainRawMaterialCode, sVariant;
        int nQuantity, nQuantity01, nQuantity02, nQuantity03, nQuantity04, nQuantity05, nQuantity06, nQuantity07, nQuantity08, nQuantity09, nQuantity10, nQuantity11, nQuantity12, nQuantity13, nQuantity14, nQuantity15, nQuantity16, nQuantity17, nQuantity18;

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

        int nBoxQty, nBoxQty01, nBoxQty02, nBoxQty03, nBoxQty04, nBoxQty05, nBoxQty06, nBoxQty07, nBoxQty08, nBoxQty09, nBoxQty10, nBoxQty11, nBoxQty12, nBoxQty13, nBoxQty14, nBoxQty15, nBoxQty16, nBoxQty17, nBoxQty18;
        int nPerBoxQty, nPerBoxQty01, nPerBoxQty02, nPerBoxQty03, nPerBoxQty04, nPerBoxQty05, nPerBoxQty06, nPerBoxQty07, nPerBoxQty08, nPerBoxQty09, nPerBoxQty10, nPerBoxQty11, nPerBoxQty12, nPerBoxQty13, nPerBoxQty14, nPerBoxQty15, nPerBoxQty16, nPerBoxQty17, nPerBoxQty18;
        public FrmGeneratePacking()
        {
            InitializeComponent();

            ccPacking = new CCPacking();
            strPacking = new StrPacking();
        }

        private void tbSalesOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    if (tbSalesOrderNo.Text.ToString().Length != 19)
                    {
                        MessageBox.Show("Invalid Sales Order No.");
                    }
                    else
                    {
                        string sSalesOrderNo = tbSalesOrderNo.Text.Trim();

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

                            tbTotalQty.Text = ds.Tables[0].Rows[0]["OrderQuantity"].ToString();

                            ClearQuantity();
                            sBuyerGroupCode = ds.Tables[0].Rows[0]["BuyerGroupCode"].ToString();
                            sSalesOrderDetailId = ds.Tables[0].Rows[0]["Id"].ToString();
                            sArticleGroup = ds.Tables[0].Rows[0]["ArticleGroup"].ToString();
                            sArticle = ds.Tables[0].Rows[0]["Article"].ToString();
                            sColorCode = ds.Tables[0].Rows[0]["ColorCode"].ToString();
                            sMainRawMaterialCode = ds.Tables[0].Rows[0]["MainRawMaterialCode"].ToString();
                            sVariant = ds.Tables[0].Rows[0]["Variant"].ToString();

                            tbSize01.Text = ds.Tables[0].Rows[0]["Size01"].ToString();
                            tbSize02.Text = ds.Tables[0].Rows[0]["Size02"].ToString();
                            tbSize03.Text = ds.Tables[0].Rows[0]["Size03"].ToString();
                            tbSize04.Text = ds.Tables[0].Rows[0]["Size04"].ToString();
                            tbSize05.Text = ds.Tables[0].Rows[0]["Size05"].ToString();
                            tbSize06.Text = ds.Tables[0].Rows[0]["Size06"].ToString();
                            tbSize07.Text = ds.Tables[0].Rows[0]["Size07"].ToString();
                            tbSize08.Text = ds.Tables[0].Rows[0]["Size08"].ToString();
                            tbSize09.Text = ds.Tables[0].Rows[0]["Size09"].ToString();
                            tbSize10.Text = ds.Tables[0].Rows[0]["Size10"].ToString();
                            tbSize11.Text = ds.Tables[0].Rows[0]["Size11"].ToString();
                            tbSize12.Text = ds.Tables[0].Rows[0]["Size12"].ToString();
                            tbSize13.Text = ds.Tables[0].Rows[0]["Size13"].ToString();
                            tbSize14.Text = ds.Tables[0].Rows[0]["Size14"].ToString();
                            tbSize15.Text = ds.Tables[0].Rows[0]["Size15"].ToString();
                            tbSize16.Text = ds.Tables[0].Rows[0]["Size16"].ToString();
                            tbSize17.Text = ds.Tables[0].Rows[0]["Size17"].ToString();
                            tbSize18.Text = ds.Tables[0].Rows[0]["Size18"].ToString();
                            ClearOrderQuantity();
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"]) > 0) { tbOrderQty01.Text = ds.Tables[0].Rows[0]["Quantity01"].ToString(); tbBoxQty01.Enabled = true; nQuantity01 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity01"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"]) > 0) { tbOrderQty02.Text = ds.Tables[0].Rows[0]["Quantity02"].ToString(); tbBoxQty02.Enabled = true; nQuantity02 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity02"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"]) > 0) { tbOrderQty03.Text = ds.Tables[0].Rows[0]["Quantity03"].ToString(); tbBoxQty03.Enabled = true; nQuantity03 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity03"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"]) > 0) { tbOrderQty04.Text = ds.Tables[0].Rows[0]["Quantity04"].ToString(); tbBoxQty04.Enabled = true; nQuantity04 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity04"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"]) > 0) { tbOrderQty05.Text = ds.Tables[0].Rows[0]["Quantity05"].ToString(); tbBoxQty05.Enabled = true; nQuantity05 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity05"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"]) > 0) { tbOrderQty06.Text = ds.Tables[0].Rows[0]["Quantity06"].ToString(); tbBoxQty06.Enabled = true; nQuantity06 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity06"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"]) > 0) { tbOrderQty07.Text = ds.Tables[0].Rows[0]["Quantity07"].ToString(); tbBoxQty07.Enabled = true; nQuantity07 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity07"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"]) > 0) { tbOrderQty08.Text = ds.Tables[0].Rows[0]["Quantity08"].ToString(); tbBoxQty08.Enabled = true; nQuantity08 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity08"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"]) > 0) { tbOrderQty09.Text = ds.Tables[0].Rows[0]["Quantity09"].ToString(); tbBoxQty09.Enabled = true; nQuantity09 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity09"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"]) > 0) { tbOrderQty10.Text = ds.Tables[0].Rows[0]["Quantity10"].ToString(); tbBoxQty10.Enabled = true; nQuantity10 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity10"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"]) > 0) { tbOrderQty11.Text = ds.Tables[0].Rows[0]["Quantity11"].ToString(); tbBoxQty11.Enabled = true; nQuantity11 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity11"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"]) > 0) { tbOrderQty12.Text = ds.Tables[0].Rows[0]["Quantity12"].ToString(); tbBoxQty12.Enabled = true; nQuantity12 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity12"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"]) > 0) { tbOrderQty13.Text = ds.Tables[0].Rows[0]["Quantity13"].ToString(); tbBoxQty13.Enabled = true; nQuantity13 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity13"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"]) > 0) { tbOrderQty14.Text = ds.Tables[0].Rows[0]["Quantity14"].ToString(); tbBoxQty14.Enabled = true; nQuantity14 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity14"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"]) > 0) { tbOrderQty15.Text = ds.Tables[0].Rows[0]["Quantity15"].ToString(); tbBoxQty15.Enabled = true; nQuantity15 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity15"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"]) > 0) { tbOrderQty16.Text = ds.Tables[0].Rows[0]["Quantity16"].ToString(); tbBoxQty16.Enabled = true; nQuantity16 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity16"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"]) > 0) { tbOrderQty17.Text = ds.Tables[0].Rows[0]["Quantity17"].ToString(); tbBoxQty17.Enabled = true; nQuantity17 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity17"]); }
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"]) > 0) { tbOrderQty18.Text = ds.Tables[0].Rows[0]["Quantity18"].ToString(); tbBoxQty18.Enabled = true; nQuantity18 = Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity18"]); }

                            if (tbOrderQty01.Text != "") { tbBoxQty01.Focus(); }
                            else if (tbOrderQty02.Text != "") { tbBoxQty02.Focus(); }
                            else if (tbOrderQty03.Text != "") { tbBoxQty03.Focus(); }
                            else if (tbOrderQty04.Text != "") { tbBoxQty04.Focus(); }
                            else if (tbOrderQty05.Text != "") { tbBoxQty05.Focus(); }
                            else if (tbOrderQty06.Text != "") { tbBoxQty06.Focus(); }
                            else if (tbOrderQty07.Text != "") { tbBoxQty07.Focus(); }
                            else if (tbOrderQty08.Text != "") { tbBoxQty08.Focus(); }
                            else if (tbOrderQty09.Text != "") { tbBoxQty09.Focus(); }
                            else if (tbOrderQty10.Text != "") { tbBoxQty10.Focus(); }
                            else if (tbOrderQty11.Text != "") { tbBoxQty11.Focus(); }
                            else if (tbOrderQty12.Text != "") { tbBoxQty12.Focus(); }
                            else if (tbOrderQty13.Text != "") { tbBoxQty13.Focus(); }
                            else if (tbOrderQty14.Text != "") { tbBoxQty14.Focus(); }
                            else if (tbOrderQty15.Text != "") { tbBoxQty15.Focus(); }
                            else if (tbOrderQty16.Text != "") { tbBoxQty16.Focus(); }
                            else if (tbOrderQty17.Text != "") { tbBoxQty17.Focus(); }
                            else if (tbOrderQty18.Text != "") { tbBoxQty18.Focus(); }

                            //tbOrderQty01.Text = ds.Tables[0].Rows[0]["Quantity01"].ToString();
                            //tbOrderQty02.Text = ds.Tables[0].Rows[0]["Quantity02"].ToString();
                            //tbOrderQty03.Text = ds.Tables[0].Rows[0]["Quantity03"].ToString();
                            //tbOrderQty04.Text = ds.Tables[0].Rows[0]["Quantity04"].ToString();
                            //tbOrderQty05.Text = ds.Tables[0].Rows[0]["Quantity05"].ToString();
                            //tbOrderQty06.Text = ds.Tables[0].Rows[0]["Quantity06"].ToString();
                            //tbOrderQty07.Text = ds.Tables[0].Rows[0]["Quantity07"].ToString();
                            //tbOrderQty08.Text = ds.Tables[0].Rows[0]["Quantity08"].ToString();
                            //tbOrderQty09.Text = ds.Tables[0].Rows[0]["Quantity09"].ToString();
                            //tbOrderQty10.Text = ds.Tables[0].Rows[0]["Quantity10"].ToString();
                            //tbOrderQty11.Text = ds.Tables[0].Rows[0]["Quantity11"].ToString();
                            //tbOrderQty12.Text = ds.Tables[0].Rows[0]["Quantity12"].ToString();
                            //tbOrderQty13.Text = ds.Tables[0].Rows[0]["Quantity13"].ToString();
                            //tbOrderQty14.Text = ds.Tables[0].Rows[0]["Quantity14"].ToString();
                            //tbOrderQty15.Text = ds.Tables[0].Rows[0]["Quantity15"].ToString();
                            //tbOrderQty16.Text = ds.Tables[0].Rows[0]["Quantity16"].ToString();
                            //tbOrderQty17.Text = ds.Tables[0].Rows[0]["Quantity17"].ToString();
                            //tbOrderQty18.Text = ds.Tables[0].Rows[0]["Quantity18"].ToString();
                            tbOrderQtyTotal.Text = ds.Tables[0].Rows[0]["OrderQuantity"].ToString();

                            cbGeneratePacking.Enabled = true;

                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.Connection = conn;
                            cmd1.CommandText = "Select * from Packing Where SalesOrderDetailId = @mSalesOrderDetailId";
                            cmd1.CommandType = CommandType.Text;

                            cmd1.Parameters.Add(new SqlParameter("@mSalesOrderDetailId", SqlDbType.VarChar)).Value = ds.Tables[0].Rows[0]["Id"].ToString();

                            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                            DataTable dt1 = new DataTable();
                            adapter1.Fill(dt1);

                            if (dt1.Rows.Count > 0)
                            {
                                MessageBox.Show("Packing List Already Generated for this Sales Order No.");
                                cbGeneratePacking.Enabled = false;
                            }

                            //cbGeneratePacking.Focus();
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

        private void ClearQuantity()
        {
            tbSize01.Clear(); tbSize02.Clear(); tbSize03.Clear(); tbSize04.Clear(); tbSize05.Clear(); tbSize06.Clear();
            tbSize07.Clear(); tbSize08.Clear(); tbSize09.Clear(); tbSize10.Clear(); tbSize11.Clear(); tbSize12.Clear();
            tbSize13.Clear(); tbSize14.Clear(); tbSize15.Clear(); tbSize16.Clear(); tbSize17.Clear(); tbSize18.Clear();

            tbOrderQty01.Clear(); tbOrderQty02.Clear(); tbOrderQty03.Clear(); tbOrderQty04.Clear(); tbOrderQty05.Clear(); tbOrderQty06.Clear();
            tbOrderQty07.Clear(); tbOrderQty08.Clear(); tbOrderQty09.Clear(); tbOrderQty10.Clear(); tbOrderQty11.Clear(); tbOrderQty12.Clear();
            tbOrderQty13.Clear(); tbOrderQty14.Clear(); tbOrderQty15.Clear(); tbOrderQty16.Clear(); tbOrderQty17.Clear(); tbOrderQty18.Clear();

            tbBoxQty01.Enabled = false; tbBoxQty02.Enabled = false; tbBoxQty03.Enabled = false; tbBoxQty04.Enabled = false; tbBoxQty05.Enabled = false;
            tbBoxQty06.Enabled = false; tbBoxQty07.Enabled = false; tbBoxQty08.Enabled = false; tbBoxQty09.Enabled = false; tbBoxQty10.Enabled = false;
            tbBoxQty11.Enabled = false; tbBoxQty12.Enabled = false; tbBoxQty13.Enabled = false; tbBoxQty14.Enabled = false; tbBoxQty15.Enabled = false;
            tbBoxQty16.Enabled = false; tbBoxQty17.Enabled = false; tbBoxQty18.Enabled = false;

            nBox01Count = 0; nBox02Count = 0; nBox03Count = 0; nBox04Count = 0; nBox05Count = 0; nBox06Count = 0;
            nBox07Count = 0; nBox08Count = 0; nBox09Count = 0; nBox10Count = 0; nBox11Count = 0; nBox12Count = 0;
            nBox13Count = 0; nBox14Count = 0; nBox15Count = 0; nBox16Count = 0; nBox17Count = 0; nBox18Count = 0;
        }

        private void ClearOrderQuantity()
        {
            nQuantity01 = 0; nQuantity02 = 0; nQuantity03 = 0; nQuantity04 = 0; nQuantity05 = 0; nQuantity06 = 0;
            nQuantity07 = 0; nQuantity08 = 0; nQuantity09 = 0; nQuantity10 = 0; nQuantity11 = 0; nQuantity12 = 0;
            nQuantity13 = 0; nQuantity14 = 0; nQuantity15 = 0; nQuantity16 = 0; nQuantity17 = 0; nQuantity18 = 0;
        }
        private void cbGeneratePacking_Click(object sender, EventArgs e)
        {
            try
            {
                string sBoxQuantityEntered = "Y";
                if (tbOrderQty01.Text.Trim() != "" && tbBoxQty01.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 01"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty02.Text.Trim() != "" && tbBoxQty02.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 02"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty03.Text.Trim() != "" && tbBoxQty03.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 03"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty04.Text.Trim() != "" && tbBoxQty04.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 04"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty05.Text.Trim() != "" && tbBoxQty05.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 05"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty06.Text.Trim() != "" && tbBoxQty06.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 06"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty07.Text.Trim() != "" && tbBoxQty07.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 07"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty08.Text.Trim() != "" && tbBoxQty08.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 08"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty09.Text.Trim() != "" && tbBoxQty09.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 09"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty10.Text.Trim() != "" && tbBoxQty10.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 10"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty11.Text.Trim() != "" && tbBoxQty11.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 11"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty12.Text.Trim() != "" && tbBoxQty12.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 12"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty13.Text.Trim() != "" && tbBoxQty13.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 13"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty14.Text.Trim() != "" && tbBoxQty14.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 14"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty15.Text.Trim() != "" && tbBoxQty15.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 15"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty16.Text.Trim() != "" && tbBoxQty16.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 16"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty17.Text.Trim() != "" && tbBoxQty17.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 17"); sBoxQuantityEntered = "N"; }
                if (tbOrderQty18.Text.Trim() != "" && tbBoxQty18.Text.Trim() == "") { MessageBox.Show("Box Quantity Not Entered for Order Quantity 18"); sBoxQuantityEntered = "N"; }


                if (sBoxQuantityEntered == "Y")
                {
                    nPerBoxQty01 = 0; nPerBoxQty02 = 0; nPerBoxQty03 = 0; nPerBoxQty04 = 0; nPerBoxQty05 = 0; nPerBoxQty06 = 0;
                    nPerBoxQty07 = 0; nPerBoxQty08 = 0; nPerBoxQty09 = 0; nPerBoxQty10 = 0; nPerBoxQty11 = 0; nPerBoxQty12 = 0;
                    nPerBoxQty13 = 0; nPerBoxQty14 = 0; nPerBoxQty15 = 0; nPerBoxQty16 = 0; nPerBoxQty17 = 0; nPerBoxQty18 = 0;

                    if (tbBoxQty01.Text.Trim() != "") { nPerBoxQty01 = Convert.ToInt32(tbBoxQty01.Text.Trim()); }
                    if (tbBoxQty02.Text.Trim() != "") { nPerBoxQty02 = Convert.ToInt32(tbBoxQty02.Text.Trim()); }
                    if (tbBoxQty03.Text.Trim() != "") { nPerBoxQty03 = Convert.ToInt32(tbBoxQty03.Text.Trim()); }
                    if (tbBoxQty04.Text.Trim() != "") { nPerBoxQty04 = Convert.ToInt32(tbBoxQty04.Text.Trim()); }
                    if (tbBoxQty05.Text.Trim() != "") { nPerBoxQty05 = Convert.ToInt32(tbBoxQty05.Text.Trim()); }
                    if (tbBoxQty06.Text.Trim() != "") { nPerBoxQty06 = Convert.ToInt32(tbBoxQty06.Text.Trim()); }
                    if (tbBoxQty07.Text.Trim() != "") { nPerBoxQty07 = Convert.ToInt32(tbBoxQty07.Text.Trim()); }
                    if (tbBoxQty08.Text.Trim() != "") { nPerBoxQty08 = Convert.ToInt32(tbBoxQty08.Text.Trim()); }
                    if (tbBoxQty09.Text.Trim() != "") { nPerBoxQty09 = Convert.ToInt32(tbBoxQty09.Text.Trim()); }
                    if (tbBoxQty10.Text.Trim() != "") { nPerBoxQty10 = Convert.ToInt32(tbBoxQty10.Text.Trim()); }
                    if (tbBoxQty11.Text.Trim() != "") { nPerBoxQty11 = Convert.ToInt32(tbBoxQty11.Text.Trim()); }
                    if (tbBoxQty12.Text.Trim() != "") { nPerBoxQty12 = Convert.ToInt32(tbBoxQty12.Text.Trim()); }
                    if (tbBoxQty13.Text.Trim() != "") { nPerBoxQty13 = Convert.ToInt32(tbBoxQty13.Text.Trim()); }
                    if (tbBoxQty14.Text.Trim() != "") { nPerBoxQty14 = Convert.ToInt32(tbBoxQty14.Text.Trim()); }
                    if (tbBoxQty15.Text.Trim() != "") { nPerBoxQty15 = Convert.ToInt32(tbBoxQty15.Text.Trim()); }
                    if (tbBoxQty16.Text.Trim() != "") { nPerBoxQty16 = Convert.ToInt32(tbBoxQty16.Text.Trim()); }
                    if (tbBoxQty17.Text.Trim() != "") { nPerBoxQty17 = Convert.ToInt32(tbBoxQty17.Text.Trim()); }
                    if (tbBoxQty18.Text.Trim() != "") { nPerBoxQty18 = Convert.ToInt32(tbBoxQty18.Text.Trim()); }

                    string sPackingListId = System.Guid.NewGuid().ToString();
                    strPacking.ID = sPackingListId;
                    strPacking.Shipper = "SLI";
                    strPacking.PackingDate = DateTime.Now.Date;
                    strPacking.BuyerGroupCode = sBuyerGroupCode;
                    strPacking.Quantity = 0;
                    strPacking.NetWt = 0;
                    strPacking.GrossWt = 0;
                    strPacking.FromCarton = 1;
                    strPacking.ToCarton = Convert.ToDecimal(tbBoxesTotal.Text.Trim());
                    strPacking.PerBoxPackingQty = 0;
                    strPacking.TotalCarton = Convert.ToDecimal(tbBoxesTotal.Text.Trim());
                    strPacking.EnteredOnMachineID = "";
                    strPacking.CreatedBy = "";
                    strPacking.CreatedDate = DateTime.Now;
                    strPacking.ModifiedBy = "";
                    strPacking.ModifiedDate = DateTime.Now;
                    strPacking.ExeVersionNo = "";
                    strPacking.ModuleName = "";
                    strPacking.SalesOrderNo = tbSalesOrderNo.Text.Trim().ToString().Substring(0, 15);
                    strPacking.Status = "OPEN";
                    strPacking.PackingListNo = "";//TODO
                    strPacking.OrderNo = tbSalesOrderNo.Text.Trim().ToString().Substring(0, 12);
                    strPacking.TypeOfPacking = "CBP";//TODO
                    strPacking.CustWorkorderNo = tbSalesOrderNo.Text.Trim();
                    strPacking.SalesOrderDetailID = sSalesOrderDetailId;
                    strPacking.ModeOfPacking = "P-O"; //TODO

                    ccPacking.InsertPacking(strPacking);

                    DataTable tblPkgDtl = new DataTable();
                    tblPkgDtl.Columns.Add(new DataColumn("ID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("JobCardNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("PackingDate", typeof(DateTime)));
                    tblPkgDtl.Columns.Add(new DataColumn("BuyerGroupCode", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("BuyerCode", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Shipper", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("ArticleGroup", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Article", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("ColorCode", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("LeatherCode", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("CartonNo", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                    tblPkgDtl.Columns.Add(new DataColumn("Unit", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Weight", typeof(decimal)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size01", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity01", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size02", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity02", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size03", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity03", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size04", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity04", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size05", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity05", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size06", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity06", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size07", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity07", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size08", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity08", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size09", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity09", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size10", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity10", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size11", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity11", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size12", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity12", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size13", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity13", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size14", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity14", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size15", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity15", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size16", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity16", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size17", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity17", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("Size18", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Quantity18", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("EnteredOnMachineID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                    tblPkgDtl.Columns.Add(new DataColumn("ModifiedBy", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("ModifiedDate", typeof(DateTime)));
                    tblPkgDtl.Columns.Add(new DataColumn("Variant", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("CustomerStyleNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("ExeVersionNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("IsApproved", typeof(bool)));
                    tblPkgDtl.Columns.Add(new DataColumn("ApprovedBy", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("ApprovedOn", typeof(DateTime)));
                    tblPkgDtl.Columns.Add(new DataColumn("ModuleName", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("IsPacked", typeof(bool)));
                    tblPkgDtl.Columns.Add(new DataColumn("DCCartonNo", typeof(int)));
                    tblPkgDtl.Columns.Add(new DataColumn("PackingNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("Location", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("PackingListNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("JobCardDetailsID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("SalesOrderDetailID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("AssortmentID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("SalesOrderNo", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("InvoiceID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("IsAssorted", typeof(bool)));
                    tblPkgDtl.Columns.Add(new DataColumn("MaterialCode", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("CartonCBM", typeof(decimal)));
                    tblPkgDtl.Columns.Add(new DataColumn("BarCode", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("PackedOn", typeof(DateTime)));
                    tblPkgDtl.Columns.Add(new DataColumn("HID", typeof(string)));
                    tblPkgDtl.Columns.Add(new DataColumn("IsShipped", typeof(bool)));
                    tblPkgDtl.Columns.Add(new DataColumn("NetWeight", typeof(decimal)));
                    tblPkgDtl.Columns.Add(new DataColumn("OCartonWeight", typeof(decimal)));


                    for (int i = 1; i <= Convert.ToInt32(tbBoxesTotal.Text.Trim()); i++)
                    {
                        CalculateBoxQty();
                        DataRow dr = tblPkgDtl.NewRow();
                        dr["ID"] = System.Guid.NewGuid().ToString();
                        dr["JobCardNo"] = DBNull.Value;
                        dr["PackingDate"] = DateTime.Now.Date;
                        dr["BuyerGroupCode"] = sBuyerGroupCode;
                        dr["BuyerCode"] = "";
                        dr["Shipper"] = "SLI";
                        dr["InvoiceNo"] = "";
                        dr["ArticleGroup"] = sArticleGroup;
                        dr["Article"] = sArticle;
                        dr["ColorCode"] = sColorCode;
                        dr["LeatherCode"] = sMainRawMaterialCode;
                        dr["CartonNo"] = i;
                        dr["Quantity"] = nBoxQty;
                        dr["Unit"] = "PRS";
                        dr["Weight"] = 0;
                        dr["Size01"] = tbSize01.Text.Trim();
                        dr["Quantity01"] = nBoxQty01;
                        dr["Size02"] = tbSize02.Text.Trim();
                        dr["Quantity02"] = nBoxQty02;
                        dr["Size03"] = tbSize03.Text.Trim();
                        dr["Quantity03"] = nBoxQty03;
                        dr["Size04"] = tbSize04.Text.Trim();
                        dr["Quantity04"] = nBoxQty04;
                        dr["Size05"] = tbSize05.Text.Trim();
                        dr["Quantity05"] = nBoxQty05;
                        dr["Size06"] = tbSize06.Text.Trim();
                        dr["Quantity06"] = nBoxQty06;
                        dr["Size07"] = tbSize07.Text.Trim();
                        dr["Quantity07"] = nBoxQty07;
                        dr["Size08"] = tbSize08.Text.Trim();
                        dr["Quantity08"] = nBoxQty08;
                        dr["Size09"] = tbSize09.Text.Trim();
                        dr["Quantity09"] = nBoxQty09;
                        dr["Size10"] = tbSize10.Text.Trim();
                        dr["Quantity10"] = nBoxQty10;
                        dr["Size11"] = tbSize11.Text.Trim();
                        dr["Quantity11"] = nBoxQty11;
                        dr["Size12"] = tbSize12.Text.Trim();
                        dr["Quantity12"] = nBoxQty12;
                        dr["Size13"] = tbSize13.Text.Trim();
                        dr["Quantity13"] = nBoxQty13;
                        dr["Size14"] = tbSize14.Text.Trim();
                        dr["Quantity14"] = nBoxQty14;
                        dr["Size15"] = tbSize15.Text.Trim();
                        dr["Quantity15"] = nBoxQty15;
                        dr["Size16"] = tbSize16.Text.Trim();
                        dr["Quantity16"] = nBoxQty16;
                        dr["Size17"] = tbSize17.Text.Trim();
                        dr["Quantity17"] = nBoxQty17;
                        dr["Size18"] = tbSize18.Text.Trim();
                        dr["Quantity18"] = nBoxQty18;
                        dr["EnteredOnMachineID"] = "";
                        dr["CreatedBy"] = "";
                        dr["CreatedDate"] = DateTime.Now;
                        dr["ModifiedBy"] = "";
                        dr["ModifiedDate"] = DateTime.Now;
                        dr["Variant"] = sVariant;
                        dr["CustomerStyleNo"] = DBNull.Value;
                        dr["ExeVersionNo"] = "";
                        dr["IsApproved"] = DBNull.Value;
                        dr["ApprovedBy"] = DBNull.Value;
                        dr["ApprovedOn"] = DBNull.Value;
                        dr["ModuleName"] = DBNull.Value;
                        dr["IsPacked"] = 0;
                        dr["DCCartonNo"] = 0;
                        dr["PackingNo"] = DBNull.Value;
                        dr["Location"] = "";
                        dr["PackingListNo"] = sPackingListId;
                        dr["JobCardDetailsID"] = DBNull.Value;
                        dr["SalesOrderDetailID"] = sSalesOrderDetailId;
                        dr["AssortmentID"] = DBNull.Value;
                        dr["OrderNo"] = tbSalesOrderNo.Text.Trim().ToString().Substring(0, 12);
                        dr["SalesOrderNo"] = tbSalesOrderNo.Text.Trim().ToString().Substring(0, 15);
                        dr["InvoiceID"] = DBNull.Value;
                        dr["IsAssorted"] = DBNull.Value;
                        dr["MaterialCode"] = DBNull.Value;
                        dr["CartonCBM"] = DBNull.Value;
                        dr["BarCode"] = DBNull.Value;
                        dr["PackedOn"] = DBNull.Value;
                        dr["HID"] = DBNull.Value;
                        dr["IsShipped"] = DBNull.Value;
                        dr["NetWeight"] = DBNull.Value;
                        dr["OCartonWeight"] = DBNull.Value;


                        tblPkgDtl.Rows.Add(dr);
                    }

                    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                    {

                        //SqlConnection con = new SqlConnection(conn);
                        //create object of SqlBulkCopy which help to insert  
                        SqlBulkCopy objbulk = new SqlBulkCopy(conn);

                        //assign Destination table name  
                        objbulk.DestinationTableName = "PackingDetail";

                        objbulk.ColumnMappings.Add("ID", "ID");
                        objbulk.ColumnMappings.Add("JobCardNo", "JobCardNo");
                        objbulk.ColumnMappings.Add("PackingDate", "PackingDate");
                        objbulk.ColumnMappings.Add("BuyerGroupCode", "BuyerGroupCode");
                        objbulk.ColumnMappings.Add("BuyerCode", "BuyerCode");
                        objbulk.ColumnMappings.Add("Shipper", "Shipper");
                        objbulk.ColumnMappings.Add("InvoiceNo", "InvoiceNo");
                        objbulk.ColumnMappings.Add("ArticleGroup", "ArticleGroup");
                        objbulk.ColumnMappings.Add("Article", "Article");
                        objbulk.ColumnMappings.Add("ColorCode", "ColorCode");
                        objbulk.ColumnMappings.Add("LeatherCode", "LeatherCode");
                        objbulk.ColumnMappings.Add("CartonNo", "CartonNo");
                        objbulk.ColumnMappings.Add("Quantity", "Quantity");
                        objbulk.ColumnMappings.Add("Unit", "Unit");
                        objbulk.ColumnMappings.Add("Weight", "Weight");
                        objbulk.ColumnMappings.Add("Size01", "Size01");
                        objbulk.ColumnMappings.Add("Quantity01", "Quantity01");
                        objbulk.ColumnMappings.Add("Size02", "Size02");
                        objbulk.ColumnMappings.Add("Quantity02", "Quantity02");
                        objbulk.ColumnMappings.Add("Size03", "Size03");
                        objbulk.ColumnMappings.Add("Quantity03", "Quantity03");
                        objbulk.ColumnMappings.Add("Size04", "Size04");
                        objbulk.ColumnMappings.Add("Quantity04", "Quantity04");
                        objbulk.ColumnMappings.Add("Size05", "Size05");
                        objbulk.ColumnMappings.Add("Quantity05", "Quantity05");
                        objbulk.ColumnMappings.Add("Size06", "Size06");
                        objbulk.ColumnMappings.Add("Quantity06", "Quantity06");
                        objbulk.ColumnMappings.Add("Size07", "Size07");
                        objbulk.ColumnMappings.Add("Quantity07", "Quantity07");
                        objbulk.ColumnMappings.Add("Size08", "Size08");
                        objbulk.ColumnMappings.Add("Quantity08", "Quantity08");
                        objbulk.ColumnMappings.Add("Size09", "Size09");
                        objbulk.ColumnMappings.Add("Quantity09", "Quantity09");
                        objbulk.ColumnMappings.Add("Size10", "Size10");
                        objbulk.ColumnMappings.Add("Quantity10", "Quantity10");
                        objbulk.ColumnMappings.Add("Size11", "Size11");
                        objbulk.ColumnMappings.Add("Quantity11", "Quantity11");
                        objbulk.ColumnMappings.Add("Size12", "Size12");
                        objbulk.ColumnMappings.Add("Quantity12", "Quantity12");
                        objbulk.ColumnMappings.Add("Size13", "Size13");
                        objbulk.ColumnMappings.Add("Quantity13", "Quantity13");
                        objbulk.ColumnMappings.Add("Size14", "Size14");
                        objbulk.ColumnMappings.Add("Quantity14", "Quantity14");
                        objbulk.ColumnMappings.Add("Size15", "Size15");
                        objbulk.ColumnMappings.Add("Quantity15", "Quantity15");
                        objbulk.ColumnMappings.Add("Size16", "Size16");
                        objbulk.ColumnMappings.Add("Quantity16", "Quantity16");
                        objbulk.ColumnMappings.Add("Size17", "Size17");
                        objbulk.ColumnMappings.Add("Quantity17", "Quantity17");
                        objbulk.ColumnMappings.Add("Size18", "Size18");
                        objbulk.ColumnMappings.Add("Quantity18", "Quantity18");
                        objbulk.ColumnMappings.Add("EnteredOnMachineID", "EnteredOnMachineID");
                        objbulk.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        objbulk.ColumnMappings.Add("CreatedDate", "CreatedDate");
                        objbulk.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
                        objbulk.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
                        objbulk.ColumnMappings.Add("Variant", "Variant");
                        objbulk.ColumnMappings.Add("CustomerStyleNo", "CustomerStyleNo");
                        objbulk.ColumnMappings.Add("ExeVersionNo", "ExeVersionNo");
                        objbulk.ColumnMappings.Add("IsApproved", "IsApproved");
                        objbulk.ColumnMappings.Add("ApprovedBy", "ApprovedBy");
                        objbulk.ColumnMappings.Add("ApprovedOn", "ApprovedOn");
                        objbulk.ColumnMappings.Add("ModuleName", "ModuleName");
                        objbulk.ColumnMappings.Add("IsPacked", "IsPacked");
                        objbulk.ColumnMappings.Add("DCCartonNo", "DCCartonNo");
                        objbulk.ColumnMappings.Add("PackingNo", "PackingNo");
                        objbulk.ColumnMappings.Add("Location", "Location");
                        objbulk.ColumnMappings.Add("PackingListNo", "PackingListNo");
                        objbulk.ColumnMappings.Add("JobCardDetailsID", "JobCardDetailsID");
                        objbulk.ColumnMappings.Add("SalesOrderDetailID", "SalesOrderDetailID");
                        objbulk.ColumnMappings.Add("AssortmentID", "AssortmentID");
                        objbulk.ColumnMappings.Add("OrderNo", "OrderNo");
                        objbulk.ColumnMappings.Add("SalesOrderNo", "SalesOrderNo");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");
                        objbulk.ColumnMappings.Add("IsAssorted", "IsAssorted");
                        objbulk.ColumnMappings.Add("MaterialCode", "MaterialCode");
                        objbulk.ColumnMappings.Add("CartonCBM", "CartonCBM");
                        objbulk.ColumnMappings.Add("BarCode", "BarCode");
                        objbulk.ColumnMappings.Add("PackedOn", "PackedOn");
                        objbulk.ColumnMappings.Add("HID", "HID");
                        objbulk.ColumnMappings.Add("IsShipped", "IsShipped");
                        objbulk.ColumnMappings.Add("NetWeight", "NetWeight");
                        objbulk.ColumnMappings.Add("OCartonWeight", "OCartonWeight");


                        conn.Open();
                        //insert bulk Records into DataBase.  
                        objbulk.WriteToServer(tblPkgDtl);
                        conn.Close();
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

        private void CalculateBoxQty()
        {
            nBoxQty01 = 0; nBoxQty02 = 0; nBoxQty03 = 0; nBoxQty04 = 0; nBoxQty05 = 0; nBoxQty06 = 0; nBoxQty07 = 0; nBoxQty08 = 0; nBoxQty09 = 0;
            nBoxQty10 = 0; nBoxQty11 = 0; nBoxQty12 = 0; nBoxQty13 = 0; nBoxQty14 = 0; nBoxQty15 = 0; nBoxQty16 = 0; nBoxQty17 = 0; nBoxQty18 = 0;

            if (nQuantity01 > 0 && nQuantity01 >= nPerBoxQty01) { nBoxQty01 = nPerBoxQty01; nQuantity01 = nQuantity01 - nBoxQty01; }
            else if (nQuantity02 > 0 && nQuantity02 >= nPerBoxQty02) { nBoxQty02 = nPerBoxQty02; nQuantity02 = nQuantity02 - nBoxQty02; }
            else if (nQuantity03 > 0 && nQuantity03 >= nPerBoxQty03) { nBoxQty03 = nPerBoxQty03; nQuantity03 = nQuantity03 - nBoxQty03; }
            else if (nQuantity04 > 0 && nQuantity04 >= nPerBoxQty04) { nBoxQty04 = nPerBoxQty04; nQuantity04 = nQuantity04 - nBoxQty04; }
            else if (nQuantity05 > 0 && nQuantity05 >= nPerBoxQty05) { nBoxQty05 = nPerBoxQty05; nQuantity05 = nQuantity05 - nBoxQty05; }
            else if (nQuantity06 > 0 && nQuantity06 >= nPerBoxQty06) { nBoxQty06 = nPerBoxQty06; nQuantity06 = nQuantity06 - nBoxQty06; }
            else if (nQuantity07 > 0 && nQuantity07 >= nPerBoxQty07) { nBoxQty07 = nPerBoxQty07; nQuantity07 = nQuantity07 - nBoxQty07; }
            else if (nQuantity08 > 0 && nQuantity08 >= nPerBoxQty08) { nBoxQty08 = nPerBoxQty08; nQuantity08 = nQuantity08 - nBoxQty08; }
            else if (nQuantity09 > 0 && nQuantity09 >= nPerBoxQty09) { nBoxQty09 = nPerBoxQty09; nQuantity09 = nQuantity09 - nBoxQty09; }
            else if (nQuantity10 > 0 && nQuantity10 >= nPerBoxQty10) { nBoxQty10 = nPerBoxQty10; nQuantity10 = nQuantity10 - nBoxQty10; }
            else if (nQuantity11 > 0 && nQuantity11 >= nPerBoxQty11) { nBoxQty11 = nPerBoxQty11; nQuantity11 = nQuantity11 - nBoxQty11; }
            else if (nQuantity12 > 0 && nQuantity12 >= nPerBoxQty12) { nBoxQty12 = nPerBoxQty12; nQuantity12 = nQuantity12 - nBoxQty12; }
            else if (nQuantity13 > 0 && nQuantity13 >= nPerBoxQty13) { nBoxQty13 = nPerBoxQty13; nQuantity13 = nQuantity13 - nBoxQty13; }
            else if (nQuantity14 > 0 && nQuantity14 >= nPerBoxQty14) { nBoxQty14 = nPerBoxQty14; nQuantity14 = nQuantity14 - nBoxQty14; }
            else if (nQuantity15 > 0 && nQuantity15 >= nPerBoxQty15) { nBoxQty15 = nPerBoxQty15; nQuantity15 = nQuantity15 - nBoxQty15; }
            else if (nQuantity16 > 0 && nQuantity16 >= nPerBoxQty16) { nBoxQty16 = nPerBoxQty16; nQuantity16 = nQuantity16 - nBoxQty16; }
            else if (nQuantity17 > 0 && nQuantity17 >= nPerBoxQty17) { nBoxQty17 = nPerBoxQty17; nQuantity17 = nQuantity17 - nBoxQty17; }
            else if (nQuantity18 > 0 && nQuantity18 >= nPerBoxQty18) { nBoxQty18 = nPerBoxQty18; nQuantity18 = nQuantity18 - nBoxQty18; }

            nBoxQty = nBoxQty01 + nBoxQty02 + nBoxQty03 + nBoxQty04 + nBoxQty05 + nBoxQty06 + nBoxQty07 + nBoxQty08 + nBoxQty09 + nBoxQty10 + nBoxQty11 + nBoxQty12 + nBoxQty13 + nBoxQty14 + nBoxQty15 + nBoxQty16 + nBoxQty17 + nBoxQty18;
        }
        private void tbBoxQty01_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty01.Text) > 0 && tbBoxQty01.Text.Trim() != "")
                {
                    tbBoxes01.Text = (Convert.ToInt32(tbOrderQty01.Text) / Convert.ToInt32(tbBoxQty01.Text)).ToString();
                    tbFrom01.Text = 1.ToString();
                    tbTo01.Text = tbBoxes01.Text;
                    nBox01Count = Convert.ToInt32(tbBoxes01.Text);

                }
                else
                {
                    tbBoxQty01.Text = "";
                    tbFrom01.Text = "";
                    tbTo01.Text = "";
                    nBox01Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxes02_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbBoxQty02_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty02.Text) > 0 && tbBoxQty02.Text.Trim() != "")
                {
                    tbBoxes02.Text = (Convert.ToInt32(tbOrderQty02.Text) / Convert.ToInt32(tbBoxQty02.Text)).ToString();
                    if (tbTo01.Text.Trim() == "")
                    {
                        tbTo01.Text = 0.ToString();
                    }
                    tbFrom02.Text = (Convert.ToInt32(tbTo01.Text) + 1).ToString();
                    tbTo02.Text = ((Convert.ToInt32(tbFrom02.Text) + Convert.ToInt32(tbBoxes02.Text)) - 1).ToString();
                    tbBoxQty01.ReadOnly = true;
                    nBox02Count = Convert.ToInt32(tbBoxes02.Text);

                }
                else
                {
                    tbBoxQty02.Text = "";
                    tbFrom02.Text = "";
                    tbTo02.Text = "";
                    tbBoxQty01.ReadOnly = false;
                    nBox02Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbSalesOrderNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbBoxQty03_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty03.Text) > 0 && tbBoxQty03.Text.Trim() != "")
                {
                    tbBoxes03.Text = (Convert.ToInt32(tbOrderQty03.Text) / Convert.ToInt32(tbBoxQty03.Text)).ToString();
                    if (tbTo02.Text.Trim() == "")
                    {
                        tbTo02.Text = 0.ToString();
                    }
                    tbFrom03.Text = (Convert.ToInt32(tbTo02.Text) + 1).ToString();
                    tbTo03.Text = ((Convert.ToInt32(tbFrom03.Text) + Convert.ToInt32(tbBoxes03.Text)) - 1).ToString();
                    tbBoxQty02.ReadOnly = true;
                    nBox03Count = Convert.ToInt32(tbBoxes03.Text);
                }
                else
                {
                    tbBoxQty03.Text = "";
                    tbFrom03.Text = "";
                    tbTo03.Text = "";
                    tbBoxQty02.ReadOnly = false;
                    nBox03Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty04_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty04.Text) > 0 && tbBoxQty04.Text.Trim() != "")
                {
                    tbBoxes04.Text = (Convert.ToInt32(tbOrderQty04.Text) / Convert.ToInt32(tbBoxQty04.Text)).ToString();
                    if (tbTo03.Text.Trim() == "")
                    {
                        tbTo03.Text = 0.ToString();
                    }
                    tbFrom04.Text = (Convert.ToInt32(tbTo03.Text) + 1).ToString();
                    tbTo04.Text = ((Convert.ToInt32(tbFrom04.Text) + Convert.ToInt32(tbBoxes04.Text)) - 1).ToString();
                    tbBoxQty03.ReadOnly = true;
                    nBox04Count = Convert.ToInt32(tbBoxes04.Text);

                }
                else
                {
                    tbBoxQty04.Text = "";
                    tbFrom04.Text = "";
                    tbTo04.Text = "";
                    tbBoxQty03.ReadOnly = false;
                    nBox04Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty05_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty05.Text) > 0 && tbBoxQty05.Text.Trim() != "")
                {
                    tbBoxes05.Text = (Convert.ToInt32(tbOrderQty05.Text) / Convert.ToInt32(tbBoxQty05.Text)).ToString();
                    if (tbTo04.Text.Trim() == "")
                    {
                        tbTo04.Text = 0.ToString();
                    }
                    tbFrom05.Text = (Convert.ToInt32(tbTo04.Text) + 1).ToString();
                    tbTo05.Text = ((Convert.ToInt32(tbFrom05.Text) + Convert.ToInt32(tbBoxes05.Text)) - 1).ToString();
                    tbBoxQty04.ReadOnly = true;
                    nBox05Count = Convert.ToInt32(tbBoxes05.Text);

                }
                else
                {
                    tbBoxQty05.Text = "";
                    tbFrom05.Text = "";
                    tbTo05.Text = "";
                    tbBoxQty04.ReadOnly = false;
                    nBox05Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty06_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty06.Text) > 0 && tbBoxQty06.Text.Trim() != "")
                {
                    tbBoxes06.Text = (Convert.ToInt32(tbOrderQty06.Text) / Convert.ToInt32(tbBoxQty06.Text)).ToString();
                    if (tbTo05.Text.Trim() == "")
                    {
                        tbTo05.Text = 0.ToString();
                    }
                    tbFrom06.Text = (Convert.ToInt32(tbTo05.Text) + 1).ToString();
                    tbTo06.Text = ((Convert.ToInt32(tbFrom06.Text) + Convert.ToInt32(tbBoxes06.Text)) - 1).ToString();
                    tbBoxQty05.ReadOnly = true;
                    nBox06Count = Convert.ToInt32(tbBoxes06.Text);

                }
                else
                {
                    tbBoxQty06.Text = "";
                    tbFrom06.Text = "";
                    tbTo06.Text = "";
                    tbBoxQty05.ReadOnly = false;
                    nBox06Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty07_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty07.Text) > 0 && tbBoxQty07.Text.Trim() != "")
                {
                    tbBoxes07.Text = (Convert.ToInt32(tbOrderQty07.Text) / Convert.ToInt32(tbBoxQty07.Text)).ToString();
                    if (tbTo06.Text.Trim() == "")
                    {
                        tbTo06.Text = 0.ToString();
                    }
                    tbFrom07.Text = (Convert.ToInt32(tbTo06.Text) + 1).ToString();
                    tbTo07.Text = ((Convert.ToInt32(tbFrom07.Text) + Convert.ToInt32(tbBoxes07.Text)) - 1).ToString();
                    tbBoxQty06.ReadOnly = true;
                    nBox07Count = Convert.ToInt32(tbBoxes07.Text);

                }
                else
                {
                    tbBoxQty07.Text = "";
                    tbFrom07.Text = "";
                    tbTo07.Text = "";
                    tbBoxQty06.ReadOnly = false;
                    nBox07Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty08_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty08.Text) > 0 && tbBoxQty08.Text.Trim() != "")
                {
                    tbBoxes08.Text = (Convert.ToInt32(tbOrderQty08.Text) / Convert.ToInt32(tbBoxQty08.Text)).ToString();
                    if (tbTo07.Text.Trim() == "")
                    {
                        tbTo07.Text = 0.ToString();
                    }
                    tbFrom08.Text = (Convert.ToInt32(tbTo07.Text) + 1).ToString();
                    tbTo08.Text = ((Convert.ToInt32(tbFrom08.Text) + Convert.ToInt32(tbBoxes08.Text)) - 1).ToString();
                    tbBoxQty07.ReadOnly = true;
                    nBox08Count = Convert.ToInt32(tbBoxes08.Text);

                }
                else
                {
                    tbBoxQty08.Text = "";
                    tbFrom08.Text = "";
                    tbTo08.Text = "";
                    tbBoxQty07.ReadOnly = false;
                    nBox08Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty09_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty09.Text) > 0 && tbBoxQty09.Text.Trim() != "")
                {
                    tbBoxes09.Text = (Convert.ToInt32(tbOrderQty09.Text) / Convert.ToInt32(tbBoxQty09.Text)).ToString();
                    if (tbTo08.Text.Trim() == "")
                    {
                        tbTo08.Text = 0.ToString();
                    }
                    tbFrom09.Text = (Convert.ToInt32(tbTo08.Text) + 1).ToString();
                    tbTo09.Text = ((Convert.ToInt32(tbFrom09.Text) + Convert.ToInt32(tbBoxes09.Text)) - 1).ToString();
                    tbBoxQty08.ReadOnly = true;
                    nBox09Count = Convert.ToInt32(tbBoxes09.Text);

                }
                else
                {
                    tbBoxQty09.Text = "";
                    tbFrom09.Text = "";
                    tbTo09.Text = "";
                    tbBoxQty08.ReadOnly = false;
                    nBox09Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty10.Text) > 0 && tbBoxQty10.Text.Trim() != "")
                {
                    tbBoxes10.Text = (Convert.ToInt32(tbOrderQty10.Text) / Convert.ToInt32(tbBoxQty10.Text)).ToString();
                    if (tbTo09.Text.Trim() == "")
                    {
                        tbTo09.Text = 0.ToString();
                    }
                    tbFrom10.Text = (Convert.ToInt32(tbTo09.Text) + 1).ToString();
                    tbTo10.Text = ((Convert.ToInt32(tbFrom10.Text) + Convert.ToInt32(tbBoxes10.Text)) - 1).ToString();
                    tbBoxQty09.ReadOnly = true;
                    nBox10Count = Convert.ToInt32(tbBoxes10.Text);

                }
                else
                {
                    tbBoxQty10.Text = "";
                    tbFrom10.Text = "";
                    tbTo10.Text = "";
                    tbBoxQty09.ReadOnly = false;
                    nBox10Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty11.Text) > 0 && tbBoxQty11.Text.Trim() != "")
                {
                    tbBoxes11.Text = (Convert.ToInt32(tbOrderQty11.Text) / Convert.ToInt32(tbBoxQty11.Text)).ToString();
                    if (tbTo10.Text.Trim() == "")
                    {
                        tbTo10.Text = 0.ToString();
                    }
                    tbFrom11.Text = (Convert.ToInt32(tbTo10.Text) + 1).ToString();
                    tbTo11.Text = ((Convert.ToInt32(tbFrom11.Text) + Convert.ToInt32(tbBoxes11.Text)) - 1).ToString();
                    tbBoxQty10.ReadOnly = true;
                    nBox11Count = Convert.ToInt32(tbBoxes11.Text);

                }
                else
                {
                    tbBoxQty11.Text = "";
                    tbFrom11.Text = "";
                    tbTo11.Text = "";
                    tbBoxQty10.ReadOnly = false;
                    nBox11Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty12.Text) > 0 && tbBoxQty12.Text.Trim() != "")
                {
                    tbBoxes12.Text = (Convert.ToInt32(tbOrderQty12.Text) / Convert.ToInt32(tbBoxQty12.Text)).ToString();
                    if (tbTo11.Text.Trim() == "")
                    {
                        tbTo11.Text = 0.ToString();
                    }
                    tbFrom12.Text = (Convert.ToInt32(tbTo11.Text) + 1).ToString();
                    tbTo12.Text = ((Convert.ToInt32(tbFrom12.Text) + Convert.ToInt32(tbBoxes12.Text)) - 1).ToString();
                    tbBoxQty11.ReadOnly = true;
                    nBox12Count = Convert.ToInt32(tbBoxes12.Text);

                }
                else
                {
                    tbBoxQty12.Text = "";
                    tbFrom12.Text = "";
                    tbTo12.Text = "";
                    tbBoxQty11.ReadOnly = false;
                    nBox12Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty13.Text) > 0 && tbBoxQty13.Text.Trim() != "")
                {
                    tbBoxes13.Text = (Convert.ToInt32(tbOrderQty13.Text) / Convert.ToInt32(tbBoxQty13.Text)).ToString();
                    if (tbTo12.Text.Trim() == "")
                    {
                        tbTo12.Text = 0.ToString();
                    }
                    tbFrom13.Text = (Convert.ToInt32(tbTo12.Text) + 1).ToString();
                    tbTo13.Text = ((Convert.ToInt32(tbFrom13.Text) + Convert.ToInt32(tbBoxes13.Text)) - 1).ToString();
                    tbBoxQty12.ReadOnly = true;
                    nBox13Count = Convert.ToInt32(tbBoxes13.Text);

                }
                else
                {
                    tbBoxQty13.Text = "";
                    tbFrom13.Text = "";
                    tbTo13.Text = "";
                    tbBoxQty12.ReadOnly = false;
                    nBox13Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty14_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty14.Text) > 0 && tbBoxQty14.Text.Trim() != "")
                {
                    tbBoxes14.Text = (Convert.ToInt32(tbOrderQty14.Text) / Convert.ToInt32(tbBoxQty14.Text)).ToString();
                    if (tbTo13.Text.Trim() == "")
                    {
                        tbTo13.Text = 0.ToString();
                    }
                    tbFrom14.Text = (Convert.ToInt32(tbTo10.Text) + 1).ToString();
                    tbTo14.Text = ((Convert.ToInt32(tbFrom14.Text) + Convert.ToInt32(tbBoxes14.Text)) - 1).ToString();
                    tbBoxQty13.ReadOnly = true;
                    nBox14Count = Convert.ToInt32(tbBoxes14.Text);

                }
                else
                {
                    tbBoxQty14.Text = "";
                    tbFrom14.Text = "";
                    tbTo14.Text = "";
                    tbBoxQty13.ReadOnly = false;
                    nBox14Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty15_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty15.Text) > 0 && tbBoxQty15.Text.Trim() != "")
                {
                    tbBoxes15.Text = (Convert.ToInt32(tbOrderQty15.Text) / Convert.ToInt32(tbBoxQty15.Text)).ToString();
                    if (tbTo14.Text.Trim() == "")
                    {
                        tbTo14.Text = 0.ToString();
                    }
                    tbFrom15.Text = (Convert.ToInt32(tbTo14.Text) + 1).ToString();
                    tbTo15.Text = ((Convert.ToInt32(tbFrom15.Text) + Convert.ToInt32(tbBoxes15.Text)) - 1).ToString();
                    tbBoxQty14.ReadOnly = true;
                    nBox15Count = Convert.ToInt32(tbBoxes15.Text);

                }
                else
                {
                    tbBoxQty15.Text = "";
                    tbFrom15.Text = "";
                    tbTo15.Text = "";
                    tbBoxQty14.ReadOnly = false;
                    nBox15Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty16_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty16.Text) > 0 && tbBoxQty16.Text.Trim() != "")
                {
                    tbBoxes16.Text = (Convert.ToInt32(tbOrderQty16.Text) / Convert.ToInt32(tbBoxQty16.Text)).ToString();
                    if (tbTo15.Text.Trim() == "")
                    {
                        tbTo15.Text = 0.ToString();
                    }
                    tbFrom16.Text = (Convert.ToInt32(tbTo15.Text) + 1).ToString();
                    tbTo16.Text = ((Convert.ToInt32(tbFrom16.Text) + Convert.ToInt32(tbBoxes16.Text)) - 1).ToString();
                    tbBoxQty15.ReadOnly = true;
                    nBox16Count = Convert.ToInt32(tbBoxes16.Text);

                }
                else
                {
                    tbBoxQty16.Text = "";
                    tbFrom16.Text = "";
                    tbTo16.Text = "";
                    tbBoxQty15.ReadOnly = false;
                    nBox16Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty17_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty17.Text) > 0 && tbBoxQty17.Text.Trim() != "")
                {
                    tbBoxes17.Text = (Convert.ToInt32(tbOrderQty17.Text) / Convert.ToInt32(tbBoxQty17.Text)).ToString();
                    if (tbTo16.Text.Trim() == "")
                    {
                        tbTo16.Text = 0.ToString();
                    }
                    tbFrom17.Text = (Convert.ToInt32(tbTo16.Text) + 1).ToString();
                    tbTo17.Text = ((Convert.ToInt32(tbFrom17.Text) + Convert.ToInt32(tbBoxes17.Text)) - 1).ToString();
                    tbBoxQty16.ReadOnly = true;
                    nBox17Count = Convert.ToInt32(tbBoxes17.Text);

                }
                else
                {
                    tbBoxQty17.Text = "";
                    tbFrom17.Text = "";
                    tbTo17.Text = "";
                    tbBoxQty16.ReadOnly = false;
                    nBox17Count = 0;
                }
                CalculateTotalCartons();
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

        private void tbBoxQty18_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbOrderQty18.Text) > 0 && tbBoxQty18.Text.Trim() != "")
                {
                    tbBoxes18.Text = (Convert.ToInt32(tbOrderQty18.Text) / Convert.ToInt32(tbBoxQty18.Text)).ToString();
                    if (tbTo17.Text.Trim() == "")
                    {
                        tbTo17.Text = 0.ToString();
                    }
                    tbFrom18.Text = (Convert.ToInt32(tbTo17.Text) + 1).ToString();
                    tbTo18.Text = ((Convert.ToInt32(tbFrom18.Text) + Convert.ToInt32(tbBoxes18.Text)) - 1).ToString();
                    tbBoxQty17.ReadOnly = true;
                    nBox18Count = Convert.ToInt32(tbBoxes18.Text);
                }
                else
                {
                    tbBoxQty18.Text = "";
                    tbFrom18.Text = "";
                    tbTo18.Text = "";
                    tbBoxQty17.ReadOnly = false;
                    nBox18Count = 0;
                }
                CalculateTotalCartons();
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

        private void CalculateTotalCartons()
        {
            tbBoxesTotal.Text = (nBox01Count + nBox02Count + nBox03Count + nBox04Count + nBox05Count + nBox06Count + nBox07Count + nBox08Count + nBox09Count + nBox10Count + nBox11Count + nBox12Count + nBox13Count + nBox14Count + nBox15Count + nBox16Count + nBox17Count + nBox18Count).ToString();

        }
    }
}








