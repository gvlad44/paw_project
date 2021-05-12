using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Entities
{
    [Serializable]
    public class Right : Group
    {
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Execute { get; set; }
        public int UserIDv2 { get; set; }

        public Right()
        {

        }

        public Right(bool re, bool wr, bool ex, string s, string c)
        {
            Read = re;
            Write = wr;
            Execute = ex;
            AccessLevel = s;
            City = c;
        }

    }
}
