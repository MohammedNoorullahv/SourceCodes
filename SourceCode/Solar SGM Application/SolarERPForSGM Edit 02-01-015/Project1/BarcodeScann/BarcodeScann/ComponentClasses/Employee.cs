﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BarcodeScann.ComponentClasses
{
    public class Employee
    {
        public long  FKUser { get; set; }
        public long FKFirm { get; set; }
        public string FirmName { get; set; }
        public string UnitType { get; set; }
        public string userName { get; set; }
        public string Designation { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
}