using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EnerProf.Models
{
    public class Category : IModel
    {
        public int ID { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название")]
        public string Name { get; set; }
        [Display(Name = "Изображение")]
        public string Img { get; set; }
        [Display(Name = "Родительская категория")]
        public int ParentID { get; set; }
        public int CompanyId { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Description { get; set; }
        public List<Category> Sub { get; set; }
        public List<Product> Products { get; set; }
        public PagesInfo PagingInfo { get; set; }

    }
}
