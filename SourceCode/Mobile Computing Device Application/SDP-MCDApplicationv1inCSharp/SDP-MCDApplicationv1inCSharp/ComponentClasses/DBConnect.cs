using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace SDP_MCDApplicationv1inCSharp.ComponentClasses
{
    public class DBConnect
    {
        SGM sgm;

        public DBConnect(SGM _sgm)
        {
            sgm = _sgm;
        }

        public static string GetConnectionString()
        {
            string connectionString = string.Empty;

            XmlTextReader reader = new XmlTextReader(System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\Settings.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                    connectionString = reader.Value.Trim();
            }
            return connectionString;

            
        }

        public void CheckUserAlreadyLogin(string userName)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    //cmd.CommandText = "Op_Modules_CHECKUSERALRDYLOG";
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@mAction",SqlDbType.VarChar)).Value = "CHECKUSERALRDYLOG"

                    cmd.CommandText = "SELECT * FROM ERPUserAuthentication WHERE UserName=@mUserName And IsActive='1'";
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.Parameters.Add(new SqlParameter("@mUserName", SqlDbType.VarChar)).Value = userName;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);   

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        sgm.sLoggedin = "N";
                    }
                    else
                    { 
                        sgm.nUAId = ds.Tables[0].Rows[0]["PKID"].ToString();
                        sgm.sLoggedin = "Y";
                        sgm.sIPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();
                    }
                }
                catch (Exception Exp)
                { 
                    
                }
            }
        }

        public void CheckIPAddress(string ipAddress)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Op_Modules_CHECKIPADDRLOG";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "CHECKIPADDRLOG";
                cmd.Parameters.Add(new SqlParameter("@mIPAddress", SqlDbType.VarChar)).Value = ipAddress;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    sgm.sLoggedin = "N";
                }
                else
                {
                    sgm.nUAId = ds.Tables[0].Rows[0]["PKID"].ToString();
                    sgm.sLoggedin = "Y";
                    sgm.sLoggedUser = ds.Tables[0].Rows[0]["UserName"].ToString();
                    sgm.sIPAddress = ds.Tables[0].Rows[0]["IPAddress"].ToString();
                }
            }
        }
        
        public bool InsertUserAuthentication(StrUserAuthentication uAuth)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                object sRes = null;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    //cmd.CommandText = "Op_Modules";
                    cmd.CommandText = "Op_Modules_INSERTUSERAUTH";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "INSERTUSERAUTH";
                    cmd.Parameters.Add(new SqlParameter("@mPKID", SqlDbType.Int)).Value = uAuth.nPKID;
                    cmd.Parameters.Add(new SqlParameter("@mUserName", SqlDbType.VarChar)).Value = uAuth.sUserName;
                    cmd.Parameters.Add(new SqlParameter("@mIPAddress", SqlDbType.Char)).Value = uAuth.sIPAddress;
                    cmd.Parameters.Add(new SqlParameter("@mSystemName", SqlDbType.Char)).Value = uAuth.sSystemName;
                    cmd.Parameters.Add(new SqlParameter("@mServer", SqlDbType.Char)).Value = uAuth.sServer;
                    cmd.Parameters.Add(new SqlParameter("@mLoginTime", SqlDbType.Char)).Value = uAuth.sLoginTime;
                    cmd.Parameters.Add(new SqlParameter("@mLogoutTime", SqlDbType.Char)).Value = uAuth.sLogoutTime;
                    cmd.Parameters.Add(new SqlParameter("@mIsActive", SqlDbType.Int)).Value = uAuth.sIsActive;
                    cmd.Parameters.Add(new SqlParameter("@mVersion", SqlDbType.VarChar)).Value = uAuth.sVersion;

                    sRes = cmd.ExecuteNonQuery();

                    if ((int)sRes == 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception Exp)
                {
                    return false;
                }
            }
        }

        public StrEmployee SelectEmployee(string userName)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "Op_Login";
                cmd.CommandText = "Op_Login_New";
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "SELALL";
                cmd.Parameters.Add(new SqlParameter("@mLoginName", SqlDbType.VarChar)).Value = userName;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                StrEmployee myRec = new StrEmployee();

                if (ds != null)
                {
                    myRec.userName = ds.Tables[0].Rows[0]["UserId"].ToString();
                    myRec.Password = ds.Tables[0].Rows[0]["sPassword"].ToString();
                    sgm.sUserPassword = ds.Tables[0].Rows[0]["sPassword"].ToString();
                    sgm.nUnitId = 0;
                    sgm.sUserName = myRec.userName;
                    sgm.sUnitName = "";
                    sgm.sUnitType = "";
                    sgm.sUserDesignation = "";
                    sgm.sUserType = "";

                    return myRec;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public bool UpdateUserAuthentication(StrUserAuthentication uAuth)
        {
            using (SqlConnection conn = new SqlConnection(sgm.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "Op_Modules";
                //cmd.CommandText = "Op_Modules_UPDATEUSERAUTH";
                cmd.CommandText = "UPDATE ERPUserAuthentication SET LogoutTime=@mLogoutTime, IsActive=@mIsActive WHERE UserName=@mUserName AND IsActive = '1'";
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@mAction", SqlDbType.Char)).Value = "UPDATEUSERAUTH";
                cmd.Parameters.Add(new SqlParameter("@mUserName", SqlDbType.VarChar)).Value = uAuth.sUserName;
                cmd.Parameters.Add(new SqlParameter("@mLogoutTime", SqlDbType.Char)).Value = uAuth.sLogoutTime;
                cmd.Parameters.Add(new SqlParameter("@mIsActive", SqlDbType.Int)).Value = uAuth.sIsActive;

                int sRes = cmd.ExecuteNonQuery();

                if (sRes == 1)
                    return true;
                else
                    return false;
            }
        }
    }
}
