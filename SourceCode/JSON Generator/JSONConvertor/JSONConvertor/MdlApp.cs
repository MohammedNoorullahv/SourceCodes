using System;
using System.Configuration;

public static class MdlApp
{
    public static string ConnectionString = ConfigurationManager.ConnectionStrings["JSONConvertor.Properties.Settings.CString"].ConnectionString;
    //using (SqlConnection conn = new SqlConnection("Data Source=NOOR_LAPTOP;Initial Catalog=DeliveryInfo;Persist Security Info=True;User ID=erp;Password=KHLIerp1234"))
    //public static string ConnectionString = "Data Source=ahserver21.database.windows.net;Initial Catalog=AHSERVER;Persist Security Info=True;User ID=Admin21;Password=Helloworld@1234";
    //  connString := fmt.Sprintf("server=%s;user id=%s;password=%s;port=%d;database=%s;",
    //server, user, password, port, database)
    //var db *sql.DB
    //var server = "ahserver21.database.windows.net"
    //var port = 1433
    //var user = "Admin21"
    //var password = "<your_password>"
    //var database = "AHSERVER"
}