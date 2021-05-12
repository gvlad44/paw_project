using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect.Entities
{
    [Serializable]
    public class User : Right
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string PCName { get; set; }
        public int ID  { get; set; }
        public Right drept = new Right();
        public long DBID { get; set; }

        public User()
        {
            
        }

        public User(string ln, string fn, int a, string pc, int id, 
            bool r, bool w, bool e, string ga, string c )
        {
            LastName = ln;
            FirstName = fn;
            Age = a;
            PCName = pc;
            ID = id;
            Read = r;
            Write = w;
            Execute = e;
            AccessLevel = ga;
            City = c;
            UserID = id;
            UserIDv2 = id;
        }

        public User(long v,string ln, string fn, int a, string pc, int id,
            bool r, bool w, bool e, string ga, string c)
        {
            DBID = v;
            LastName = ln;
            FirstName = fn;
            Age = a;
            PCName = pc;
            ID = id;
            Read = r;
            Write = w;
            Execute = e;
            AccessLevel = ga;
            City = c;
            UserID = id;
            UserIDv2 = id;
        }

    }
}
