using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.DataModels
{
    public class SehirModel
    {
        public int SehirID { get; set; }
        public string SehirTitle { get; set; }
        public int SehirKey { get; set; }
    }

    public class SehirGridModel : SehirModel
    {

    }
}
