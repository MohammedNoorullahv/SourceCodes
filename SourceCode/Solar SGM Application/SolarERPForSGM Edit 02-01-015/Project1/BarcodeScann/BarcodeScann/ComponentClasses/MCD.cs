using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BarcodeScann.ComponentClasses
{
    public class MCD
    {
        string connectionString;

        public MCD(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public DataTable LoadShift()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_MCD_LOADSHIFT";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Shift");
                conn.Close();
                return ds.Tables[0];
            }
        }

        public DataTable LoadMachine()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_MCD_LOADMACHINE";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Machine");
                conn.Close();
                return ds.Tables[0];
            }
        }

        public DataTable LoadScannedBoxes(string sSpoolId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_MCD_LOADSCNDBOXES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mSpoolHID", SqlDbType.VarChar)).Value = sSpoolId;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "ScannedBoxes");
                conn.Close();
                return ds.Tables[0];
            }
        }
    }
}
