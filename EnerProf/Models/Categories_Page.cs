using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EnerProf.Models
{
    public class Categories_Page
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Description { get; set; }

    }
}