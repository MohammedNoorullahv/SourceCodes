using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SDP_MCDApplicationv1inCSharp.ComponentClasses
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
                cmd.CommandText = "proc_MCD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "LOADSHIFT";

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Shift");

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
                cmd.CommandText = "proc_MCD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "LOADMACHINE";

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Machine");

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
                cmd.CommandText = "proc_MCD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "LOADSCNDBOXES";
                cmd.Parameters.Add(new SqlParameter("@mSpoolHID", SqlDbType.VarChar)).Value = sSpoolId;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "ScannedBoxes");

                return ds.Tables[0];
            }
        }

        public DataTable LoadWrongScannedBoxes(string sSpoolId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_MCD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "LOADWRONGBOXES";
                cmd.Parameters.Add(new SqlParameter("@mSpoolHID", SqlDbType.VarChar)).Value = sSpoolId;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "ScannedBoxes");

                return ds.Tables[0];
            }
        }

    }
}
