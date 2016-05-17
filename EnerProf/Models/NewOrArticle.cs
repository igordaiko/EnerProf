using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EnerProf.Models
{
    public class NewOrArticle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NewArticle { get; set; }
        public DateTime Date { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Text { get; set; }

    }
}
