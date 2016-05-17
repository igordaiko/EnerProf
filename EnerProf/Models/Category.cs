using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerProf.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int ParentID { get; set; }
        public int CompanyId { get; set; }
        public List<Category> Sub { get; set; }
        public List<Product> Products { get; set; }
        public PagesInfo PagingInfo { get; set; }

    }
}
