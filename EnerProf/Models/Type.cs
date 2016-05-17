using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerProf.Models
{
    public class Product_Type
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<Property> Properies { get; set; }
    }
}
