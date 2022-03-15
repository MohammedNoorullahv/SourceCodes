using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizerAddOn.ComponentClasses
{
    public partial class CCAvailabilityStatus : Component
    {
        public CCAvailabilityStatus()
        {
            InitializeComponent();
        }

        public CCAvailabilityStatus(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public bool CheckforJobcardExists(string sJobcardNo, string sCheckingFor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_JCAvailability";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sCheckingFor == "Jobcard")
                    {
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJC";
                    }
                        
                    else if (sCheckingFor == "Shipment Plan")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCINPLAN";
                    else if (sCheckingFor == "Shipment Pending")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPENDSHPT";
                    else if (sCheckingFor == "Production Pending")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELPENDPROD";
                    

                    cmd.Parameters.Add(new SqlParameter("@mStoreCode", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                    cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardNo;
                    cmd.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = "FULLSHOE";

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (sCheckingFor == "Production Pending")
                    {
                        if (dt.Rows.Count <= 0)
                            return false;
                        else
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ProducedQty"]) >= Convert.ToInt32(ds.Tables[0].Rows[0]["Quantity"]))
                                return true;
                            else
                                return false;
                        }
                          
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                            return true;
                        else
                            return false;
                    }
                        
                }
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

                    DataTable dt = new DataTable();
                    return false;
                }
            }
        }

        public decimal JobcardDemandQty(string sPlaceofUsage, string sComponentGroup, string sJobcardno, string sMaterialCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_JCAvailability";
                    cmd.CommandType = CommandType.StoredProcedure;

                   
                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELJCDEMAND";
                    cmd.Parameters.Add(new SqlParameter("@mJobcardNo", SqlDbType.VarChar)).Value = sJobcardno;
                    cmd.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = sComponentGroup;
                    cmd.Parameters.Add(new SqlParameter("@mPlaceofUse", SqlDbType.VarChar)).Value = sPlaceofUsage;
                    cmd.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = sMaterialCode;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (dt.Rows.Count > 0)
                    {

                        return (Convert.ToDecimal(ds.Tables[0].Rows[0]["ConsumptionQuantity"]));
                    }
                    else
                    {
                        return 0;
                    }

                }
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

                    DataTable dt = new DataTable();
                    return 0;
                }
            }
        }

        public decimal SalesOrderDemandQty(string sComponentGroup, string sSalesOrderNo, string sMaterialCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_JCAvailability";
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSODEMAND";
                    cmd.Parameters.Add(new SqlParameter("@mSalesorderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = sComponentGroup;
                    cmd.Parameters.Add(new SqlParameter("@mMaterialCode", SqlDbType.VarChar)).Value = sMaterialCode;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (dt.Rows.Count > 0)
                    {

                        return (Convert.ToDecimal(ds.Tables[0].Rows[0]["ConsumptionQuantity"]));
                    }
                    else
                    {
                        return 0;
                    }

                }
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

                    DataTable dt = new DataTable();
                    return 0;
                }
            }
        }

        public bool CheckforSalesOrderExists(string sSalesOrderNo, string sCheckingFor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MdlApp.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SLI_JCAvailability";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sCheckingFor == "Jobcard")
                    {
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSO";
                    }

                    else if (sCheckingFor == "Shipment Plan")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOINPLAN";
                    else if (sCheckingFor == "Shipment Pending")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOPENDSHPT";
                    else if (sCheckingFor == "Production Pending")
                        cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.VarChar)).Value = "SELSOPENDPROD";


                    cmd.Parameters.Add(new SqlParameter("@mStoreCode", SqlDbType.VarChar)).Value = MdlApp.sStoreCode;
                    cmd.Parameters.Add(new SqlParameter("@mSalesOrderNo", SqlDbType.VarChar)).Value = sSalesOrderNo;
                    cmd.Parameters.Add(new SqlParameter("@mComponentGroup", SqlDbType.VarChar)).Value = "FULLSHOE";

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    if (sCheckingFor == "Production Pending")
                    {
                        if (dt.Rows.Count <= 0)
                            return false;
                        else
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ProducedQty"]) >= Convert.ToInt32(ds.Tables[0].Rows[0]["OrderQuantity"]))
                                return true;
                            else
                                return false;
                        }

                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                            return true;
                        else
                            return false;
                    }

                }
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

                    DataTable dt = new DataTable();
                    return false;
                }
            }
        }
    }
}
