using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Entities
{
    [Serializable]
    public class Group
    {
        public string AccessLevel { get; set; }
        public string City { get; set; }
        public int UserID { get; set; }

        public Group()
        {

        }

        public Group(string access,string city)
        {
            AccessLevel = access;
            City = city;
        }
    }
}
