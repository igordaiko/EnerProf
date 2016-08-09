using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace EnerProf.Models
{
    public class Product:IModel
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int CatID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Description { get; set; }
        public string Images { get; set; }
        public List<Property> Properties { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Product_Type> Types { get; set; }
    }
}
