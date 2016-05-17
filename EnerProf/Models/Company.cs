using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EnerProf.Models
{
    public class Company
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public int ID { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
