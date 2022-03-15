using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerAddOn.Structures
{
    public class StrOuterCartonPackedInfo
    {
        public int PKID { get; set; }
        public string JobcardNo { get; set; }
        public int BoxSlNo { get; set; }
        public string CartonNo { get; set; }
        public string InnerBoxNo { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public DateTime PackedOn { get; set; }
            
    }
}
