using System.Configuration;


public static class MdlApp
{
    public static string ConnectionString = ConfigurationManager.ConnectionStrings["OptimizerAddOn.Properties.Settings.CString"].ConnectionString;
    public static string ConnectionStringHR = ConfigurationManager.ConnectionStrings["OptimizerAddOn.Properties.Settings.CHRString"].ConnectionString;

    public static int nStoreCount;
    public static string sStoreCode, sUnitCode;
    public static decimal CategoryA, CategoryB, CategoryC;
    public static string sSystemId = "";
    public static string sUserName, sEnteredOnMachineID, sRemarks;
    
}

