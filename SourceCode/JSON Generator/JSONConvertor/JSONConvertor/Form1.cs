using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronBarCode;
using QRCoder;

namespace JSONConvertor
{
    public partial class Form1 : Form
    {

        int NInvLoc, NAckLoc, nIRNLoc, NQRCodeLoc;
        string SInvLoc, SAckLoc, SIRNLoc, SQRCodeLoc;
        int NInvLocBulk, NAckLocBulk, NIRNLocBulk, NQRCodeLocBulk;
        string SInvLocBulk, SAckLocBulk, SIRNLocBulk, SQRCodeLocBulk;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadColumnInfo();
        }

        private void cbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbGenerateJSON_Click(object sender, EventArgs e)
        {
            string SInvoice = "";
            string SInvoiceType = "";
            decimal DInvoiceValue = 0, DInvoiceDtlValue = 0, DDifference = 0, DInvoiceOtherCharges = 0;
            if (tbInvoiceNo.Text.Trim() == "")
            {
                MessageBox.Show("Invoice No should not be Empty");
                return;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select * from vw_GSTCombinedView Where [InvoiceNo] = @mInvoiceNo Order By invoiceno,SlNo";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = tbInvoiceNo.Text.Trim();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int NRowCount;
                    NRowCount = Convert.ToInt32(dt.Rows.Count);
                    //i = 0;



                    if (Convert.ToInt32(dt.Rows.Count) > 0)
                    {
                        SInvoiceType = ds.Tables[0].Rows[0]["SupTyp"].ToString();
                        SInvoice = "[" + Environment.NewLine;
                        SInvoice = SInvoice + "{" + Environment.NewLine;

                        //Version Info
                        SInvoice = SInvoice + "\"Version\":\"1.1\"," + Environment.NewLine;

                        //TranDtls
                        SInvoice = SInvoice + "\"TranDtls\":{" + Environment.NewLine;
                        SInvoice = SInvoice + "\"TaxSch\":\"GST\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"SupTyp\":\"" + ds.Tables[0].Rows[0]["SupTyp"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"IgstOnIntra\":\"N\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"RegRev\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"EcmGstin\":null" + Environment.NewLine;
                        SInvoice = SInvoice + "}," + Environment.NewLine;

                        //DocDtls
                        SInvoice = SInvoice + "\"DocDtls\":{" + Environment.NewLine;
                        SInvoice = SInvoice + "\"Typ\":\"" + ds.Tables[0].Rows[0]["Typ"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"No\":\"" + ds.Tables[0].Rows[0]["InvoiceNo"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Dt\":\"" + ds.Tables[0].Rows[0]["InvoiceDate"].ToString() + "\"" + Environment.NewLine;
                        SInvoice = SInvoice + "}," + Environment.NewLine;

                        //SellerDtls
                        SInvoice = SInvoice + "\"SellerDtls\":{" + Environment.NewLine;
                        SInvoice = SInvoice + "\"Gstin\":\"" + ds.Tables[0].Rows[0]["GSTNo"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"LglNm\":\"" + ds.Tables[0].Rows[0]["lglNm"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"TrdNm\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Addr1\":\"" + ds.Tables[0].Rows[0]["Addr1"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Addr2\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Loc\":\"" + ds.Tables[0].Rows[0]["Loc"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Pin\":" + ds.Tables[0].Rows[0]["Pincode"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Stcd\":\"" + ds.Tables[0].Rows[0]["Stcd"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Ph\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Em\":null" + Environment.NewLine;
                        SInvoice = SInvoice + "}," + Environment.NewLine;


                        //BuyerDtls
                        SInvoice = SInvoice + "\"BuyerDtls\":{" + Environment.NewLine;
                        SInvoice = SInvoice + "\"Gstin\":\"" + ds.Tables[0].Rows[0]["Gstin"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"LglNm\":\"" + ds.Tables[0].Rows[0]["BuyerName"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"TrdNm\":\"" + ds.Tables[0].Rows[0]["BuyerName"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Pos\":\"" + ds.Tables[0].Rows[0]["Pos"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Addr1\":\"" + ds.Tables[0].Rows[0]["BAddr1"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Addr2\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Loc\":\"" + ds.Tables[0].Rows[0]["BCity"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Pin\":" + ds.Tables[0].Rows[0]["BPin"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Stcd\":\"" + ds.Tables[0].Rows[0]["BStcd"].ToString() + "\"," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Ph\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Em\":null" + Environment.NewLine;
                        SInvoice = SInvoice + "}," + Environment.NewLine;

                        //ShipDtls
                        SInvoice = SInvoice + "\"ShipDtls\":null," + Environment.NewLine;

                        //ValDtls
                        SInvoice = SInvoice + "\"ValDtls\":{" + Environment.NewLine;
                        SInvoice = SInvoice + "\"AssVal\":" + ds.Tables[0].Rows[0]["AssVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"IgstVal\":" + ds.Tables[0].Rows[0]["IGSTVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"CgstVal\":" + ds.Tables[0].Rows[0]["CGSTVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"SgstVal\":" + ds.Tables[0].Rows[0]["SGSTVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"CesVal\":" + ds.Tables[0].Rows[0]["CesVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"StCesVal\":" + ds.Tables[0].Rows[0]["StCesVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Discount\":" + ds.Tables[0].Rows[0]["Discount"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"OthChrg\":" + ds.Tables[0].Rows[0]["OthChrg"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"RndOffAmt\":" + ds.Tables[0].Rows[0]["RndOffAmt"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"TotInvVal\":" + ds.Tables[0].Rows[0]["TotInvVal"].ToString() + "," + Environment.NewLine;
                        SInvoice = SInvoice + "\"TotInvValFc\":" + ds.Tables[0].Rows[0]["TotInvValFc"].ToString() + Environment.NewLine;
                        SInvoice = SInvoice + "}," + Environment.NewLine;

                        DInvoiceValue = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotInvVal"].ToString());
                        DInvoiceOtherCharges = Convert.ToDecimal(ds.Tables[0].Rows[0]["OthChrg"].ToString());
                        //ExpDtls
                        SInvoice = SInvoice + "\"ExpDtls\":null," + Environment.NewLine;
                        //EwbDtls
                        SInvoice = SInvoice + "\"EwbDtls\":null," + Environment.NewLine;
                        //PayDtls
                        SInvoice = SInvoice + "\"PayDtls\":null," + Environment.NewLine;
                        //RefDtls
                        SInvoice = SInvoice + "\"RefDtls\":null," + Environment.NewLine;

                        //AddlDocDtls
                        SInvoice = SInvoice + "\"AddlDocDtls\":[{" + Environment.NewLine;
                        SInvoice = SInvoice + "\"Url\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Docs\":null," + Environment.NewLine;
                        SInvoice = SInvoice + "\"Info\":null" + Environment.NewLine;
                        SInvoice = SInvoice + "}]," + Environment.NewLine;

                        //ItemsList
                        SInvoice = SInvoice + "\"ItemList\":[";


                        int NRowCount1;
                        NRowCount1 = Convert.ToInt32(dt.Rows.Count);
                        int j = 0;

                        if (Convert.ToInt32(dt.Rows.Count) > 0)
                        {
                            for (j = 0; j < NRowCount1; j++)
                            {
                                if (SInvoiceType == "EXPWOP")
                                {
                                    if (Convert.ToDecimal(ds.Tables[0].Rows[j]["IGSTValue"].ToString()) > 0)
                                    {
                                        MessageBox.Show("For Invoice Type EXPWOP IGST Value should be Zero");
                                        return;
                                    }
                                }

                                SInvoice = SInvoice + "{" + Environment.NewLine;
                                SInvoice = SInvoice + "\"SlNo\":\"" + Convert.ToInt16(j + 1) + "\"," + Environment.NewLine;
                                SInvoice = SInvoice + "\"PrdDesc\":\"" + ds.Tables[0].Rows[j]["PrdDesc"].ToString() + "\"," + Environment.NewLine;
                                SInvoice = SInvoice + "\"IsServc\":\"" + ds.Tables[0].Rows[j]["IsServc"].ToString() + "\"," + Environment.NewLine;
                                SInvoice = SInvoice + "\"HsnCd\":\"" + ds.Tables[0].Rows[j]["HsnCd"].ToString() + "\"," + Environment.NewLine;
                                SInvoice = SInvoice + "\"Barcde\":null," + Environment.NewLine;
                                SInvoice = SInvoice + "\"Qty\":" + ds.Tables[0].Rows[j]["quantity"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"FreeQty\":" + ds.Tables[0].Rows[j]["FreeQty"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"Unit\":\"" + ds.Tables[0].Rows[j]["Unit"].ToString() + "\"," + Environment.NewLine;

                                SInvoice = SInvoice + "\"UnitPrice\":" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[j]["UnitPrice"]), 2).ToString() + "," + Environment.NewLine;

                                SInvoice = SInvoice + "\"TotAmt\":" + ds.Tables[0].Rows[j]["TotAmt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"Discount\":" + ds.Tables[0].Rows[j]["ItmDiscount"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"PreTaxVal\":" + ds.Tables[0].Rows[j]["PreTaxVal"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"AssAmt\":" + ds.Tables[0].Rows[j]["AssAmt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"GstRt\":" + ds.Tables[0].Rows[j]["GstRt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"IgstAmt\":" + ds.Tables[0].Rows[j]["IGSTValue"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"CgstAmt\":" + ds.Tables[0].Rows[j]["CGSTValue"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"SgstAmt\":" + ds.Tables[0].Rows[j]["SGSTValue"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"CesRt\":" + ds.Tables[0].Rows[j]["CesRt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"CesAmt\":" + ds.Tables[0].Rows[j]["CesAmt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"CesNonAdvlAmt\":" + ds.Tables[0].Rows[j]["CesNonAdvlAmt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"StateCesRt\":" + ds.Tables[0].Rows[j]["StateCesRt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"StateCesAmt\":" + ds.Tables[0].Rows[j]["StateCesAmt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"StateCesNonAdvlAmt\":" + ds.Tables[0].Rows[j]["StateCesNonAdvlAmt"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"OthChrg\":" + ds.Tables[0].Rows[j]["ItmOthChrg"].ToString() + "," + Environment.NewLine;
                                SInvoice = SInvoice + "\"TotItemVal\":" + ds.Tables[0].Rows[j]["TotItemVal"].ToString() + Environment.NewLine;

                                DInvoiceDtlValue += Convert.ToDecimal(ds.Tables[0].Rows[j]["TotItemVal"].ToString());

                                if (j == NRowCount1 - 1)
                                {
                                    SInvoice = SInvoice + "}" + Environment.NewLine;
                                }
                                else
                                {
                                    SInvoice = SInvoice + "}," + Environment.NewLine;
                                }

                            }


                            SInvoice = SInvoice + "]" + Environment.NewLine;


                            SInvoice = SInvoice + "}" + Environment.NewLine;
                            SInvoice = SInvoice + "]";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Invoice No.");
                        return;
                    }
                }

                DDifference = Math.Round((DInvoiceValue - (DInvoiceDtlValue + DInvoiceOtherCharges)),2);
                

                if (DDifference < -1.95M || DDifference > 1.96M)
                {
                    MessageBox.Show("Invoice Value and Sum of Invoice Dtls value does not match. JSON Will not be generated");
                    tbInvoiceNo.Focus();
                }
                else
                {
                    string JSONResult = JsonConvert.SerializeObject(SInvoice, Formatting.Indented);
                    JSONResult = JSONResult.Replace("\\r\\n", "\n");
                    JSONResult = JSONResult.Replace("\\", "");

                    string FileName = tbInvoiceNo.Text.Trim().Replace("/", "");

                    string path = @"E:\json\" + FileName + ".json";
                    System.IO.File.WriteAllText(path, JSONResult);

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(JSONResult.ToString());
                        tw.Close();
                    }

                    MessageBox.Show("Completed");
                    tbInvoiceNo.Clear();
                    tbInvoiceNo.Focus();
                }

                //Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SInvoice = "";
            decimal DInvoiceValue = 0, DInvoiceDtlValue = 0;
            if (tbInvoiceNo.Text.Trim() == "")
            {
                MessageBox.Show("Invoice No should not be Empty");
                return;
            }
            else
            {
                string SInvoiceType;

                if (rbLocalInvoice.Checked == true)
                {
                    SInvoiceType = "L";
                }
                else
                {
                    SInvoiceType = "E";
                }

                if (SInvoiceType == "E")
                {
                    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from vw_FSInvoice Where [InvoiceNo] = @mInvoiceNo Order By invoiceno";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = tbInvoiceNo.Text.Trim();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int NRowCount;
                        NRowCount = Convert.ToInt32(dt.Rows.Count);
                        //i = 0;



                        if (Convert.ToInt32(dt.Rows.Count) > 0)
                        {

                            SInvoice = "[" + Environment.NewLine;
                            SInvoice = SInvoice + "{" + Environment.NewLine;

                            //Version Info
                            SInvoice = SInvoice + "\"Version\":\"1.1\"," + Environment.NewLine;

                            //TranDtls
                            SInvoice = SInvoice + "\"TranDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"TaxSch\":\"GST\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"SupTyp\":\"" + ds.Tables[0].Rows[0]["SupplyType"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"IgstOnIntra\":\"N\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"RegRev\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"EcmGstin\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            //DocDtls
                            SInvoice = SInvoice + "\"DocDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Typ\":\"INV\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"No\":\"" + ds.Tables[0].Rows[0]["InvoiceNo"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Dt\":\"" + ds.Tables[0].Rows[0]["InvDate"].ToString() + "\"" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            //SellerDtls
                            SInvoice = SInvoice + "\"SellerDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Gstin\":\"33AAAFS1730P1Z7\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"LglNm\":\"" + ds.Tables[0].Rows[0]["CompanyName"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TrdNm\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr1\":\"" + ds.Tables[0].Rows[0]["Address"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr2\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Loc\":\"" + ds.Tables[0].Rows[0]["City"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Pin\":" + ds.Tables[0].Rows[0]["Pincode"].ToString() + "," + Environment.NewLine;
                            //SInvoice = SInvoice   + "\"Stcd\":\"" + ds.Tables[0].Rows[0]["ShipperTINNo"].ToString().Substring(0,2) + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Stcd\":\"33\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Ph\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Em\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;


                            //BuyerDtls
                            SInvoice = SInvoice + "\"BuyerDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Gstin\":\"URP\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"LglNm\":\"" + ds.Tables[0].Rows[0]["BuyerName"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TrdNm\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Pos\":\"96\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr1\":\"" + ds.Tables[0].Rows[0]["ConsigAddress"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr2\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Loc\":\"" + ds.Tables[0].Rows[0]["B_city"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Pin\":999999," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Stcd\":\"96\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Ph\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Em\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            //ShipDtls
                            SInvoice = SInvoice + "\"ShipDtls\":null," + Environment.NewLine;

                            //ValDtls
                            SInvoice = SInvoice + "\"ValDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"AssVal\":" + ds.Tables[0].Rows[0]["TotalValue"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"IgstVal\":" + ds.Tables[0].Rows[0]["IGSTValue"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"CgstVal\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"SgstVal\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"CesVal\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"StCesVal\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Discount\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"OthChrg\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"RndOffAmt\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TotInvVal\":" + ds.Tables[0].Rows[0]["forbillamt"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TotInvValFc\":0" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            DInvoiceValue = Convert.ToDecimal(ds.Tables[0].Rows[0]["forbillamt"].ToString());

                            //ExpDtls
                            SInvoice = SInvoice + "\"ExpDtls\":null," + Environment.NewLine;
                            //EwbDtls
                            SInvoice = SInvoice + "\"EwbDtls\":null," + Environment.NewLine;
                            //PayDtls
                            SInvoice = SInvoice + "\"PayDtls\":null," + Environment.NewLine;
                            //RefDtls
                            SInvoice = SInvoice + "\"RefDtls\":null," + Environment.NewLine;

                            //AddlDocDtls
                            SInvoice = SInvoice + "\"AddlDocDtls\":[{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Url\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Docs\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Info\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}]," + Environment.NewLine;

                            //ItemsList
                            SInvoice = SInvoice + "\"ItemList\":[";
                            using (SqlConnection conn1 = new SqlConnection(MdlApp.ConnectionString))
                            {
                                conn1.Open();
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = conn1;
                                cmd1.CommandText = "Select * from vw_FSInvoiceDetail_Materials Where [InvoiceNo] = @mInvoiceNo Order By invoiceno";
                                cmd1.CommandType = CommandType.Text;

                                cmd1.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = tbInvoiceNo.Text.Trim();

                                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                DataSet ds1 = new DataSet();
                                adapter1.Fill(ds1);

                                DataTable dt1 = new DataTable();
                                adapter1.Fill(dt1);

                                int NRowCount1;
                                NRowCount1 = Convert.ToInt32(dt1.Rows.Count);
                                int j = 0;

                                if (Convert.ToInt32(dt1.Rows.Count) > 0)
                                {
                                    for (j = 0; j < NRowCount1; j++)
                                    {
                                        SInvoice = SInvoice + "{" + Environment.NewLine;
                                        SInvoice = SInvoice + "\"SlNo\":\"" + Convert.ToInt16(j + 1) + "\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"PrdDesc\":\"" + ds1.Tables[0].Rows[j]["ArticleGroupName"].ToString() + "\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"IsServc\":\"N\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"HsnCd\":\"6403\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Barcde\":null," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Qty\":" + ds1.Tables[0].Rows[j]["quantity"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"FreeQty\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Unit\":\"PRS\"," + Environment.NewLine;
                                        decimal DRate = Math.Round((Convert.ToDecimal(ds1.Tables[0].Rows[j]["rate"]) * Convert.ToDecimal(ds1.Tables[0].Rows[j]["CurrencyConversionRate"])), 2);
                                        SInvoice = SInvoice + "\"UnitPrice\":" + DRate.ToString() + "," + Environment.NewLine;

                                        SInvoice = SInvoice + "\"TotAmt\":" + ds1.Tables[0].Rows[j]["value"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Discount\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"PreTaxVal\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"AssAmt\":" + ds1.Tables[0].Rows[j]["value"].ToString() + "," + Environment.NewLine;
                                        //SInvoice = SInvoice + "\"GstRt\":0," + Environment.NewLine;
                                        //SInvoice = SInvoice + "\"IgstAmt\":0," + Environment.NewLine;
                                        //SInvoice = SInvoice + "\"CgstAmt\":0," + Environment.NewLine;
                                        //SInvoice = SInvoice + "\"SgstAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"GstRt\":" + (Convert.ToDecimal(ds1.Tables[0].Rows[j]["CGSTPercentage"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["SGSTPercentage"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["IGSTPercentage"].ToString())).ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"IgstAmt\":" + ds1.Tables[0].Rows[j]["IGSTValue"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CgstAmt\":" + ds1.Tables[0].Rows[j]["CGSTValue"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"SgstAmt\":" + ds1.Tables[0].Rows[j]["SGSTValue"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CesRt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CesAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CesNonAdvlAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"StateCesRt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"StateCesAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"StateCesNonAdvlAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"OthChrg\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"TotItemVal\":" + ds1.Tables[0].Rows[j]["value"].ToString() + Environment.NewLine;

                                        DInvoiceDtlValue += Convert.ToDecimal(ds1.Tables[0].Rows[j]["value"].ToString());

                                        if (j == NRowCount1 - 1)
                                        {
                                            SInvoice = SInvoice + "}" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            SInvoice = SInvoice + "}," + Environment.NewLine;
                                        }

                                    }
                                }

                                SInvoice = SInvoice + "]" + Environment.NewLine;


                                SInvoice = SInvoice + "}" + Environment.NewLine;
                                SInvoice = SInvoice + "]";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Invoice No.");
                            return;
                        }
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "Select * from vw_LocalInvoice Where [InvoiceNo] = @mInvoiceNo Order By invoiceno";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = tbInvoiceNo.Text.Trim();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int NRowCount;
                        NRowCount = Convert.ToInt32(dt.Rows.Count);
                        //i = 0;



                        if (Convert.ToInt32(dt.Rows.Count) > 0)
                        {
                            bool IsService = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsService"]);

                            SInvoice = "[" + Environment.NewLine;
                            SInvoice = SInvoice + "{" + Environment.NewLine;

                            //Version Info
                            SInvoice = SInvoice + "\"Version\":\"1.1\"," + Environment.NewLine;

                            //TranDtls
                            SInvoice = SInvoice + "\"TranDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"TaxSch\":\"GST\"," + Environment.NewLine;
                            //SInvoice = SInvoice + "\"SupTyp\":\"B2B\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"SupTyp\":\"" + ds.Tables[0].Rows[0]["SupplyType"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"IgstOnIntra\":\"N\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"RegRev\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"EcmGstin\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            //DocDtls
                            SInvoice = SInvoice + "\"DocDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Typ\":\"INV\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"No\":\"" + ds.Tables[0].Rows[0]["InvoiceNo"].ToString() + "\"," + Environment.NewLine;
                            //DateTime DInvDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["InvoiceDate"]);

                            //SInvoice = SInvoice + "\"Dt\":\"" + ds.Tables[0].Rows[0]["InvoiceDate"].ToString() + "\"" + Environment.NewLine;
                            //SInvoice = SInvoice + "\"Dt\":\"" + DInvDate.ToString("dd/MM/yyyy") + "\"" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Dt\":\"" + Convert.ToDateTime(ds.Tables[0].Rows[0]["InvoiceDate"]).ToString("dd/MM/yyyy").Replace("-", "/") + "\"" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            //SellerDtls
                            SInvoice = SInvoice + "\"SellerDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Gstin\":\"33AAAFS1730P1Z7\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"LglNm\":\"" + ds.Tables[0].Rows[0]["CompanyName"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TrdNm\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr1\":\"" + ds.Tables[0].Rows[0]["Address"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr2\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Loc\":\"" + ds.Tables[0].Rows[0]["City"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Pin\":" + ds.Tables[0].Rows[0]["Pincode"].ToString() + "," + Environment.NewLine;
                            //SInvoice = SInvoice   + "\"Stcd\":\"" + ds.Tables[0].Rows[0]["ShipperTINNo"].ToString().Substring(0,2) + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Stcd\":\"33\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Ph\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Em\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;


                            //BuyerDtls
                            SInvoice = SInvoice + "\"BuyerDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Gstin\":\"" + ds.Tables[0].Rows[0]["BGSTNumber"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"LglNm\":\"" + ds.Tables[0].Rows[0]["BName"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TrdNm\":\"" + ds.Tables[0].Rows[0]["BName"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Pos\":\"" + ds.Tables[0].Rows[0]["BGSTNumber"].ToString().Substring(0, 2) + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr1\":\"" + ds.Tables[0].Rows[0]["BAddress"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Addr2\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Loc\":\"" + ds.Tables[0].Rows[0]["BCity"].ToString() + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Pin\":" + ds.Tables[0].Rows[0]["BPinCode"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Stcd\":\"" + ds.Tables[0].Rows[0]["BGSTNumber"].ToString().Substring(0, 2) + "\"," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Ph\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Em\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            //ShipDtls
                            SInvoice = SInvoice + "\"ShipDtls\":null," + Environment.NewLine;

                            //ValDtls
                            SInvoice = SInvoice + "\"ValDtls\":{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"AssVal\":" + ds.Tables[0].Rows[0]["TaxableValue"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"IgstVal\":" + ds.Tables[0].Rows[0]["IGST"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"CgstVal\":" + ds.Tables[0].Rows[0]["CGST"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"SgstVal\":" + ds.Tables[0].Rows[0]["SGST"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"CesVal\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"StCesVal\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Discount\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"OthChrg\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"RndOffAmt\":0," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TotInvVal\":" + ds.Tables[0].Rows[0]["TotalValue"].ToString() + "," + Environment.NewLine;
                            SInvoice = SInvoice + "\"TotInvValFc\":0" + Environment.NewLine;
                            SInvoice = SInvoice + "}," + Environment.NewLine;

                            DInvoiceValue = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalValue"].ToString());

                            //ExpDtls
                            SInvoice = SInvoice + "\"ExpDtls\":null," + Environment.NewLine;
                            //EwbDtls
                            SInvoice = SInvoice + "\"EwbDtls\":null," + Environment.NewLine;
                            //PayDtls
                            SInvoice = SInvoice + "\"PayDtls\":null," + Environment.NewLine;
                            //RefDtls
                            SInvoice = SInvoice + "\"RefDtls\":null," + Environment.NewLine;

                            //AddlDocDtls
                            SInvoice = SInvoice + "\"AddlDocDtls\":[{" + Environment.NewLine;
                            SInvoice = SInvoice + "\"Url\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Docs\":null," + Environment.NewLine;
                            SInvoice = SInvoice + "\"Info\":null" + Environment.NewLine;
                            SInvoice = SInvoice + "}]," + Environment.NewLine;

                            //ItemsList
                            SInvoice = SInvoice + "\"ItemList\":[";
                            using (SqlConnection conn1 = new SqlConnection(MdlApp.ConnectionString))
                            {
                                conn1.Open();
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = conn1;
                                cmd1.CommandText = "Select * from vw_LocalInvoiceDetail Where [InvoiceNo] = @mInvoiceNo Order By invoiceno";
                                cmd1.CommandType = CommandType.Text;

                                cmd1.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = tbInvoiceNo.Text.Trim();

                                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                DataSet ds1 = new DataSet();
                                adapter1.Fill(ds1);

                                DataTable dt1 = new DataTable();
                                adapter1.Fill(dt1);

                                int NRowCount1;
                                NRowCount1 = Convert.ToInt32(dt1.Rows.Count);
                                int j = 0;

                                if (Convert.ToInt32(dt1.Rows.Count) > 0)
                                {
                                    for (j = 0; j < NRowCount1; j++)
                                    {
                                        SInvoice = SInvoice + "{" + Environment.NewLine;
                                        SInvoice = SInvoice + "\"SlNo\":\"" + Convert.ToInt16(j + 1) + "\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"PrdDesc\":\"" + ds1.Tables[0].Rows[j]["Description"].ToString() + "\"," + Environment.NewLine;
                                        if (IsService == true)
                                        {
                                            SInvoice = SInvoice + "\"IsServc\":\"Y\"," + Environment.NewLine;
                                        }
                                        else
                                        {
                                            SInvoice = SInvoice + "\"IsServc\":\"N\"," + Environment.NewLine;
                                        }

                                        SInvoice = SInvoice + "\"HsnCd\":\"" + ds1.Tables[0].Rows[j]["HSNCode"].ToString() + "\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Barcde\":null," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Qty\":" + ds1.Tables[0].Rows[j]["Quantity"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"FreeQty\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Unit\":\"PRS\"," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"UnitPrice\":" + ds1.Tables[0].Rows[j]["Price"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"TotAmt\":" + ds1.Tables[0].Rows[j]["SubValue"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"Discount\":" + ds1.Tables[0].Rows[j]["DiscountValue"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"PreTaxVal\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"AssAmt\":" + ds1.Tables[0].Rows[j]["TaxableValue"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"GstRt\":" + (Convert.ToDecimal(ds1.Tables[0].Rows[j]["CGSTPC"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["SGSTPC"].ToString()) + Convert.ToDecimal(ds1.Tables[0].Rows[j]["IGSTPC"].ToString())).ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"IgstAmt\":" + ds1.Tables[0].Rows[j]["IGST"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CgstAmt\":" + ds1.Tables[0].Rows[j]["CGST"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"SgstAmt\":" + ds1.Tables[0].Rows[j]["SGST"].ToString() + "," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CesRt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CesAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"CesNonAdvlAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"StateCesRt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"StateCesAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"StateCesNonAdvlAmt\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"OthChrg\":0," + Environment.NewLine;
                                        SInvoice = SInvoice + "\"TotItemVal\":" + ds1.Tables[0].Rows[j]["TotalValue"].ToString() + Environment.NewLine;


                                        DInvoiceDtlValue += Convert.ToDecimal(ds1.Tables[0].Rows[j]["TotalValue"].ToString());

                                        if (j == NRowCount1 - 1)
                                        {
                                            SInvoice = SInvoice + "}" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            SInvoice = SInvoice + "}," + Environment.NewLine;
                                        }

                                    }
                                }

                                SInvoice = SInvoice + "]" + Environment.NewLine;


                                SInvoice = SInvoice + "}" + Environment.NewLine;
                                SInvoice = SInvoice + "]";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Invoice No.");
                            return;
                        }
                    }
                }


                if (DInvoiceValue != DInvoiceDtlValue)
                {
                    MessageBox.Show("Invoice Value and Sum of Invoice Dtls value does not match. JSON Will not be generated");
                    tbInvoiceNo.Focus();
                }
                else
                {
                    string JSONResult = JsonConvert.SerializeObject(SInvoice, Formatting.Indented);
                    JSONResult = JSONResult.Replace("\\r\\n", "\n");
                    JSONResult = JSONResult.Replace("\\", "");

                    string FileName = tbInvoiceNo.Text.Trim().Replace("/", "");

                    string path = @"E:\json\" + FileName + ".json";
                    System.IO.File.WriteAllText(path, JSONResult);

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(JSONResult.ToString());
                        tw.Close();
                    }

                    MessageBox.Show("Completed");
                    tbInvoiceNo.Clear();
                    tbInvoiceNo.Focus();
                }

                //Application.Exit();
            }
        }

        private void cbImport_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            int NPixelSize;
            if (rbPixel1.Checked == true)
                NPixelSize = 1;
            else if (rbPixel2.Checked == true)
                NPixelSize = 2;
            else
                NPixelSize = 3;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "\\ahserver\\temp share\\ERP\\JSON";
                openFileDialog.Filter = "xlx files (*.xls)|*.xls|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    Microsoft.Office.Interop.Excel.Application xlApp;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    var fileName = filePath;

                    string SACKNo, SSignedQRCode, SInvoiceNo, SIRN;
                    SSignedQRCode = "";
                    SACKNo = "";
                    SInvoiceNo = "";
                    SIRN = "";
                    xlApp = new Microsoft.Office.Interop.Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    int NColumnNo = 1, NRowNo = 2;
                    if (rbSingle.Checked == true)
                    {
                        while ((xlWorkSheet.Cells[NRowNo, 1].Value != null))
                        {
                            if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbInvoiceNoLoc.Text.Trim().ToString())].Value != null))
                            {
                                SInvoiceNo = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbInvoiceNoLoc.Text.Trim().ToString())].Value.ToString();
                            }

                            if (SInvoiceNo == "")
                            {
                                MessageBox.Show("Empty Invoice No. QR Code Not Uploaded in Invoice");
                            }
                            else
                            {
                                //string SInvoiceType = xlWorkSheet.Cells[NRowNo, 8].Value.ToString();
                                //SInvoiceType = SInvoiceType.Trim();

                                if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbAckLoc.Text.Trim().ToString())].Value != null))
                                {
                                    SACKNo = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbAckLoc.Text.Trim().ToString())].Value.ToString();
                                }
                                else
                                {
                                    SACKNo = "";
                                }

                                if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbQRCodeLoc.Text.Trim().ToString())].Value != null))
                                {
                                    SSignedQRCode = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbQRCodeLoc.Text.Trim().ToString())].Value.ToString();
                                }
                                else
                                {
                                    SSignedQRCode = "";
                                }

                                if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbIRNLoc.Text.Trim().ToString())].Value != null))
                                {
                                    SIRN = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbIRNLoc.Text.Trim().ToString())].Value.ToString();
                                }
                                else
                                {
                                    SIRN = "";
                                }

                                //Fetch Data from Invoice Table to Update
                                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                                {
                                    conn.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = "Select * from GSTEInvoiceTableDtls Where [InvoiceNo] = @mInvoiceNo";
                                    cmd.CommandType = CommandType.Text;

                                    cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;

                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adapter.Fill(ds);

                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);

                                    int NRowCount;
                                    NRowCount = Convert.ToInt32(dt.Rows.Count);
                                    //i = 0;

                                    if (NRowCount == 0)
                                    {
                                        MessageBox.Show("Invalid Invoice No." + SInvoiceNo + " This Invoice No Not available in table [ GSTEInvoiceTableDtls ] ");
                                    }
                                    else
                                    {
                                        string STableName = ds.Tables[0].Rows[0]["TableName"].ToString();

                                        string sql = String.Format("Select * from {0} Where [InvoiceNo] = '" + SInvoiceNo + "'", STableName);

                                        //SqlCommand com = new SqlCommand(sql, con);
                                        //conn.Open();
                                        //SqlCommand cmd1 = new SqlCommand();
                                        SqlCommand cmd1 = new SqlCommand(sql, conn);
                                        cmd1.Connection = conn;
                                        //cmd1.CommandText = "Select * from " + STableName + " Where [InvoiceNo] = @mInvoiceNo";
                                        cmd1.CommandType = CommandType.Text;


                                        //cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;

                                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                        DataSet ds1 = new DataSet();
                                        adapter1.Fill(ds1);

                                        DataTable dt1 = new DataTable();
                                        adapter1.Fill(dt1);

                                        int NRowCount1;
                                        NRowCount1 = Convert.ToInt32(dt1.Rows.Count);
                                        //i = 0;

                                        if (Convert.ToInt32(dt1.Rows.Count) > 0)
                                        {

                                            string sql1 = String.Format("Update {0} Set AckNo = '" + SACKNo + "', QRCode = '" + SSignedQRCode + "', IRNNo = '" + SIRN + "' Where [InvoiceNo] = '" + SInvoiceNo + "'", STableName);

                                            SqlCommand cmd2 = new SqlCommand(sql1, conn);
                                            cmd2.Connection = conn;
                                            //cmd2.CommandText = "Update @mTableName  Set AckNo = @mAckNo, QRCode = @mQRCode, IRNNo = @mIRN Where [InvoiceNo] = @mInvoiceNo";
                                            cmd2.CommandType = CommandType.Text;

                                            //cmd2.Parameters.Add(new SqlParameter("@mTableName", SqlDbType.VarChar)).Value = STableName;
                                            //cmd2.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;
                                            //cmd2.Parameters.Add(new SqlParameter("@mAckNo", SqlDbType.VarChar)).Value = SACKNo;
                                            //cmd2.Parameters.Add(new SqlParameter("@mQRCode", SqlDbType.VarChar)).Value = SSignedQRCode;
                                            //cmd2.Parameters.Add(new SqlParameter("@mIRN", SqlDbType.VarChar)).Value = SIRN;

                                            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                                            DataSet ds2 = new DataSet();
                                            adapter2.Fill(ds2);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid Invoice No." + SInvoiceNo + " This Invoice No Not available in Database");
                                        }
                                    }
                                }




                                //QRCodeWriter.CreateQrCode(SSignedQRCode, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsJpeg("MyQR.jpg");
                                ////string FileName = @"\\ahserver\Images\AP Images\QRCode\" + SACKNo;
                                ////string Extension = ".jpg";

                                ////string Path = string.Concat(FileName, Extension);
                                ////QRCodeWriter.CreateQrCode(SSignedQRCode, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsJpeg(Path);

                                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                QRCodeData qrCodeData = qrGenerator.CreateQrCode(SSignedQRCode, QRCodeGenerator.ECCLevel.Q);
                                QRCode qrCode = new QRCode(qrCodeData);
                                Bitmap qrCodeImage = qrCode.GetGraphic(NPixelSize);
                                
                                pictureBox1.BackgroundImage = qrCodeImage;
                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                                //string FileName = @"\\ahserver\Images\AP Images\QRCode\" + SACKNo;
                                string FileName = @"\\ssplserver\Images\AP Images\QRCode\" + SACKNo;
                                string Extension = ".jpg";
                                string Path = string.Concat(FileName, Extension);

                                pictureBox1.BackgroundImage.Save(Path, ImageFormat.Jpeg);

                                //int PixSize = 1;

                                //for (PixSize = 1; PixSize < 7; PixSize++)
                                //{
                                //    QRCodeGenerator qrGenerator1 = new QRCodeGenerator();
                                //    QRCodeData qrCodeData1 = qrGenerator.CreateQrCode(SSignedQRCode, QRCodeGenerator.ECCLevel.Q);
                                //    QRCode qrCode1 = new QRCode(qrCodeData1);
                                //    Bitmap qrCodeImage1 = qrCode1.GetGraphic(PixSize);

                                //    pictureBox1.BackgroundImage = qrCodeImage1;
                                //    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                                //    string FileName1 = @"\\ahserver\Images\AP Images\QRCode\" + string.Concat(SACKNo,"-",PixSize.ToString(),"-Pixels");
                                //    string Extension1 = ".jpg";
                                //    string Path1 = string.Concat(FileName1, Extension1);

                                //    pictureBox1.BackgroundImage.Save(Path1, ImageFormat.Jpeg);
                                //}

                            }
                            NRowNo += 1;
                        }
                    }
                    else
                    {
                        while ((xlWorkSheet.Cells[NRowNo, 1].Value != null))
                        {
                            if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbInvoiceNoLocBulk.Text.Trim().ToString())].Value != null))
                            {
                                SInvoiceNo = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbInvoiceNoLocBulk.Text.Trim().ToString())].Value.ToString();
                            }

                            if (SInvoiceNo == "")
                            {
                                MessageBox.Show("Empty Invoice No. QR Code Not Uploaded in Invoice");
                            }
                            else
                            {
                                //string SInvoiceType = xlWorkSheet.Cells[NRowNo, 8].Value.ToString();
                                //SInvoiceType = SInvoiceType.Trim();

                                if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbAckLocBulk.Text.Trim().ToString())].Value != null))
                                {
                                    SACKNo = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbAckLocBulk.Text.Trim().ToString())].Value.ToString();
                                }
                                else
                                {
                                    SACKNo = "";
                                }

                                if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbQRCodeLocBulk.Text.Trim().ToString())].Value != null))
                                {
                                    SSignedQRCode = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbQRCodeLocBulk.Text.Trim().ToString())].Value.ToString();
                                }
                                else
                                {
                                    SSignedQRCode = "";
                                }

                                if ((xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbIRNLocBulk.Text.Trim().ToString())].Value != null))
                                {
                                    SIRN = xlWorkSheet.Cells[NRowNo, Convert.ToInt32(tbIRNLocBulk.Text.Trim().ToString())].Value.ToString();
                                }
                                else
                                {
                                    SIRN = "";
                                }

                                //Fetch Data from Invoice Table to Update
                                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                                {
                                    conn.Open();
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = conn;
                                    cmd.CommandText = "Select * from GSTEInvoiceTableDtls Where [InvoiceNo] = @mInvoiceNo";
                                    cmd.CommandType = CommandType.Text;

                                    cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;

                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adapter.Fill(ds);

                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);

                                    int NRowCount;
                                    NRowCount = Convert.ToInt32(dt.Rows.Count);
                                    //i = 0;

                                    if (NRowCount == 0)
                                    {
                                        MessageBox.Show("Invalid Invoice No." + SInvoiceNo + " This Invoice No Not available in taable [ GSTEInvoiceTableDtls ] ");
                                    }
                                    else
                                    {
                                        string STableName = ds.Tables[0].Rows[0]["TableName"].ToString();

                                        string sql = String.Format("Select * from {0} Where [InvoiceNo] = '" + SInvoiceNo + "'", STableName);

                                        //SqlCommand com = new SqlCommand(sql, con);
                                        //conn.Open();
                                        //SqlCommand cmd1 = new SqlCommand();
                                        SqlCommand cmd1 = new SqlCommand(sql, conn);
                                        cmd1.Connection = conn;
                                        //cmd1.CommandText = "Select * from " + STableName + " Where [InvoiceNo] = @mInvoiceNo";
                                        cmd1.CommandType = CommandType.Text;


                                        //cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;

                                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                        DataSet ds1 = new DataSet();
                                        adapter1.Fill(ds1);

                                        DataTable dt1 = new DataTable();
                                        adapter1.Fill(dt1);

                                        int NRowCount1;
                                        NRowCount1 = Convert.ToInt32(dt1.Rows.Count);
                                        //i = 0;

                                        if (Convert.ToInt32(dt1.Rows.Count) > 0)
                                        {

                                            string sql1 = String.Format("Update {0} Set AckNo = '" + SACKNo + "', QRCode = '" + SSignedQRCode + "', IRNNo = '" + SIRN + "' Where [InvoiceNo] = '" + SInvoiceNo + "'", STableName);

                                            SqlCommand cmd2 = new SqlCommand(sql1, conn);
                                            cmd2.Connection = conn;
                                            //cmd2.CommandText = "Update @mTableName  Set AckNo = @mAckNo, QRCode = @mQRCode, IRNNo = @mIRN Where [InvoiceNo] = @mInvoiceNo";
                                            cmd2.CommandType = CommandType.Text;

                                            //cmd2.Parameters.Add(new SqlParameter("@mTableName", SqlDbType.VarChar)).Value = STableName;
                                            //cmd2.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;
                                            //cmd2.Parameters.Add(new SqlParameter("@mAckNo", SqlDbType.VarChar)).Value = SACKNo;
                                            //cmd2.Parameters.Add(new SqlParameter("@mQRCode", SqlDbType.VarChar)).Value = SSignedQRCode;
                                            //cmd2.Parameters.Add(new SqlParameter("@mIRN", SqlDbType.VarChar)).Value = SIRN;

                                            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                                            DataSet ds2 = new DataSet();
                                            adapter2.Fill(ds2);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid Invoice No." + SInvoiceNo + " This Invoice No Not available in Database");
                                        }
                                    }
                                }
                                //Fetch Data from Invoice Table to Update

                                //Invoice Tables//
                                //if (SInvoiceType != "URP")
                                //{
                                //    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                                //    {
                                //        conn.Open();
                                //        SqlCommand cmd = new SqlCommand();
                                //        cmd.Connection = conn;
                                //        cmd.CommandText = "Select * from LocalInvoice Where [InvoiceNo] = @mInvoiceNo";
                                //        cmd.CommandType = CommandType.Text;

                                //        cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;

                                //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                //        DataSet ds = new DataSet();
                                //        adapter.Fill(ds);

                                //        DataTable dt = new DataTable();
                                //        adapter.Fill(dt);

                                //        int NRowCount;
                                //        NRowCount = Convert.ToInt32(dt.Rows.Count);
                                //        //i = 0;

                                //        if (Convert.ToInt32(dt.Rows.Count) > 0)
                                //        {
                                //            SqlCommand cmd1 = new SqlCommand();
                                //            cmd1.Connection = conn;
                                //            cmd1.CommandText = "Update LocalInvoice  Set AckNo = @mAckNo, QRCode = @mQRCode, IRNNo = @mIRN Where [InvoiceNo] = @mInvoiceNo";
                                //            cmd1.CommandType = CommandType.Text;

                                //            cmd1.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;
                                //            cmd1.Parameters.Add(new SqlParameter("@mAckNo", SqlDbType.VarChar)).Value = SACKNo;
                                //            cmd1.Parameters.Add(new SqlParameter("@mQRCode", SqlDbType.VarChar)).Value = SSignedQRCode;
                                //            cmd1.Parameters.Add(new SqlParameter("@mIRN", SqlDbType.VarChar)).Value = SIRN;

                                //            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                                //            DataSet ds1 = new DataSet();
                                //            adapter1.Fill(ds1);
                                //        }
                                //        else
                                //        {
                                //            MessageBox.Show("Invalid Invoice No." + SInvoiceNo + " This Invoice No Not available in Database");
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                                //    {
                                //        conn.Open();
                                //        SqlCommand cmd = new SqlCommand();
                                //        cmd.Connection = conn;
                                //        cmd.CommandText = "Select * from Invoice Where [InvoiceNo] = @mInvoiceNo";
                                //        cmd.CommandType = CommandType.Text;

                                //        cmd.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;

                                //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                //        DataSet ds = new DataSet();
                                //        adapter.Fill(ds);

                                //        DataTable dt = new DataTable();
                                //        adapter.Fill(dt);

                                //        int NRowCount;
                                //        NRowCount = Convert.ToInt32(dt.Rows.Count);
                                //        //i = 0;

                                //        if (Convert.ToInt32(dt.Rows.Count) > 0)
                                //        {
                                //            SqlCommand cmd2 = new SqlCommand();
                                //            cmd2.Connection = conn;
                                //            cmd2.CommandText = "Update Invoice  Set AckNo = @mAckNo, QRCode = @mQRCode, IRNNo = @mIRN Where [InvoiceNo] = @mInvoiceNo";
                                //            cmd2.CommandType = CommandType.Text;

                                //            cmd2.Parameters.Add(new SqlParameter("@mInvoiceNo", SqlDbType.VarChar)).Value = SInvoiceNo;
                                //            cmd2.Parameters.Add(new SqlParameter("@mAckNo", SqlDbType.VarChar)).Value = SACKNo;
                                //            cmd2.Parameters.Add(new SqlParameter("@mQRCode", SqlDbType.VarChar)).Value = SSignedQRCode;
                                //            cmd1.Parameters.Add(new SqlParameter("@mIRN", SqlDbType.VarChar)).Value = SIRN;

                                //            SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                                //            DataSet ds2 = new DataSet();
                                //            adapter2.Fill(ds2);
                                //        }
                                //        else
                                //        {
                                //            MessageBox.Show("Invalid Invoice No." + SInvoiceNo + " This Invoice No Not available in Database");
                                //        }
                                //    }
                                //}
                                //Invoice Tables//



                                //QRCodeWriter.CreateQrCode(SSignedQRCode, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsJpeg("MyQR.jpg");
                                //////string FileName = @"\\ahserver\Images\AP Images\QRCode\" + SACKNo;
                                //////string Extension = ".jpg";

                                //////string Path = string.Concat(FileName, Extension);
                                //////QRCodeWriter.CreateQrCode(SSignedQRCode, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsJpeg(Path);
                                //QRCodeWriter.CreateQrCode(SSignedQRCode, 500, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsJpeg("MyQR.jpg");


                                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                QRCodeData qrCodeData = qrGenerator.CreateQrCode(SSignedQRCode, QRCodeGenerator.ECCLevel.Q);
                                QRCode qrCode = new QRCode(qrCodeData);
                                Bitmap qrCodeImage = qrCode.GetGraphic(NPixelSize);
                                //pictureBox1.Image = qrCodeImage;
                                pictureBox1.BackgroundImage = qrCodeImage;
                                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                                //MessageBox.Show(SInvoiceNo);
                                string FileName = @"\\ahserver\Images\AP Images\QRCode\" + SACKNo;
                                string Extension = ".jpg";
                                string Path = string.Concat(FileName, Extension);

                                pictureBox1.BackgroundImage.Save(Path, ImageFormat.Jpeg);
                            }
                            NRowNo += 1;
                        }
                    }

                    MessageBox.Show("Completed");
                }
            }
        }

        private void tbInvoiceNoLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbAckLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbIRNLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbQRCodeLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void LoadColumnInfo()
        {
            using (SqlConnection conn1 = new SqlConnection(MdlApp.ConnectionString))
            {
                conn1.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn1;
                cmd1.CommandText = "Select * from AbbrevTable Where Group_ = 'INVOICEINFO'";
                cmd1.CommandType = CommandType.Text;

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                adapter1.Fill(ds1);

                DataTable dt1 = new DataTable();
                adapter1.Fill(dt1);

                int NRowCount1;
                NRowCount1 = Convert.ToInt32(dt1.Rows.Count);
                int j = 0;

                for (j = 0; j < NRowCount1; j++)
                {
                    String SColumnName = ds1.Tables[0].Rows[j]["FullName_"].ToString();

                    switch (SColumnName)
                    {
                        case "DOC NO":
                            tbInvoiceNoLoc.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            NInvLoc = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SInvLoc = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                        case "ACK NO":
                            tbAckLoc.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            NAckLoc = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SAckLoc = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                        case "IRN":
                            tbIRNLoc.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            nIRNLoc = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SIRNLoc = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                        case "SIGNED QR CODE":
                            tbQRCodeLoc.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            NQRCodeLoc = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SQRCodeLoc = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;

                        case "DOC NO BULK":
                            tbInvoiceNoLocBulk.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            NInvLocBulk = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SInvLocBulk = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                        case "ACK NO BULK":
                            tbAckLocBulk.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            NAckLocBulk = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SAckLocBulk = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                        case "IRN BULK":
                            tbIRNLocBulk.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();

                            NIRNLocBulk = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SIRNLocBulk = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                        case "SIGNED QR CODE BULK":
                            tbQRCodeLocBulk.Text = ds1.Tables[0].Rows[j]["Abbrev_"].ToString();
                            NQRCodeLocBulk = Convert.ToInt32(ds1.Tables[0].Rows[j]["Abbrev_"].ToString());
                            SQRCodeLocBulk = ds1.Tables[0].Rows[j]["ID"].ToString();
                            break;
                    }

                }
            }
        }

        private void cbUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
            {
                conn.Open();

                if (Convert.ToInt32(tbInvoiceNoLoc.Text.Trim()) != NInvLoc)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbInvoiceNoLoc.Text.Trim();
                    cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SInvLoc;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                }
                if (Convert.ToInt32(tbAckLoc.Text.Trim()) != NAckLoc)
                {
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd1.CommandType = CommandType.Text;

                    cmd1.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbAckLoc.Text.Trim();
                    cmd1.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SAckLoc;

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);
                }
                if (Convert.ToInt32(tbIRNLoc.Text.Trim()) != nIRNLoc)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbIRNLoc.Text.Trim();
                    cmd2.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SIRNLoc;

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    adapter2.Fill(ds2);
                }
                if (Convert.ToInt32(tbQRCodeLoc.Text.Trim()) != NQRCodeLoc)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = conn;
                    cmd3.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd3.CommandType = CommandType.Text;

                    cmd3.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbQRCodeLoc.Text.Trim();
                    cmd3.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SQRCodeLoc;

                    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                    DataSet ds3 = new DataSet();
                    adapter3.Fill(ds3);
                }

                if (Convert.ToInt32(tbInvoiceNoLocBulk.Text.Trim()) != NInvLocBulk)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbInvoiceNoLocBulk.Text.Trim();
                    cmd.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SInvLocBulk;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                }
                if (Convert.ToInt32(tbAckLocBulk.Text.Trim()) != NAckLocBulk)
                {
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd1.CommandType = CommandType.Text;

                    cmd1.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbAckLocBulk.Text.Trim();
                    cmd1.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SAckLocBulk;

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    adapter1.Fill(ds1);
                }
                if (Convert.ToInt32(tbIRNLocBulk.Text.Trim()) != NIRNLocBulk)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbIRNLocBulk.Text.Trim();
                    cmd2.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SIRNLocBulk;

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    adapter2.Fill(ds2);
                }
                if (Convert.ToInt32(tbQRCodeLocBulk.Text.Trim()) != NQRCodeLocBulk)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = conn;
                    cmd3.CommandText = "Update AbbrevTable Set Abbrev_ = @mAbbrev Where ID = @mID";
                    cmd3.CommandType = CommandType.Text;

                    cmd3.Parameters.Add(new SqlParameter("@mAbbrev", SqlDbType.VarChar)).Value = tbQRCodeLocBulk.Text.Trim();
                    cmd3.Parameters.Add(new SqlParameter("@mID", SqlDbType.VarChar)).Value = SQRCodeLocBulk;

                    SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);
                    DataSet ds3 = new DataSet();
                    adapter3.Fill(ds3);
                }
            }
        }


    }
}
