using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerProf.Models
{
    public class Find_Page
    {
        public List<Product> Products { get; set; }
        public List<Company> Companies { get; set; }
        public List<Product> Company_Prod { get; set; }
    }
}
