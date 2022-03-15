using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScann.ComponentClasses
{
    public class UserAuthentication
    {
        public int nPKID { get; set; }
        public string sUserName { get; set; }
        public string sIPAddress { get; set; }
        public string sSystemName { get; set; }
        public string sServer { get; set; }
        public string sLoginTime { get; set; }
        public string sLogoutTime { get; set; }
        public int sIsActive { get; set; }
        public string sReason { get; set; }
        public string sVersion { get; set; }
    }
}
