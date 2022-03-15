using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScann.ComponentClasses
{
    public class SGM
    {
        public string connectionString { get; set; }
        public string sSellectionMessage { get; set; }
        public string sUserPassword { get; set; }
        public string sUserName { get; set; }
        public string strIPAddress { get; set; }
        public string sLoggedin { get; set; }
        public string nUAId { get; set; }
        public string sIPAddress { get; set; }
        public string sLoggedUser { get; set; }
        public string sPermissionGranted { get; set; }
        public int nUnitId { get; set; }
        public string sUnitType { get; set; }
        public string sUserDesignation { get; set; }
        public string sUserType { get; set; }
        public string sUnitName { get; set; }
        public string sSelectedSection { get; set; }
        public string sShiftCode { get; set; }
        public string sMachine { get; set; }
        public string sMachinecode { get; set; }
        public string strSystemName { get; set; }
        public string sSelectedArticle { get; set; }
        public string sSpoolId { get; set; }
        public string sSection { get; set; }
        public string sProcess { get; set; }
    }
}
