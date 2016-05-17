using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EnerProf.DataBaseClasses;
using EnerProf.Models;
namespace EnerProf.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Категории. Заполнение коллекции
            ITable itable = new CategoriesReader();
            GenericListOfElements<Category> categories_gen = new GenericListOfElements<Category>(itable, "0", "parentID");
            List<Category> model_categories = categories_gen.ListOfItems;
            //Подкатегории для каждой категории. Заполнение коллекции            
            foreach(Category category in model_categories)
            {
                categories_gen = new GenericListOfElements<Category>(itable, category.ID.ToString(), "parentID");
                category.Sub = categories_gen.ListOfItems;
            }
            ViewBag.ListOfCategories = model_categories;
            //Отраслевые решения. Заполнение коллекции
            itable = new IndustriesReader();
            GenericListOfElements<Industries> industries = new GenericListOfElements<Industries>(itable);            
            ViewBag.ListOfIndustries = industries.ListOfItems;
            //Наши клиенты. Заполнение коллекции
            itable = new ClientsReader();
            GenericListOfElements<Clients> clients = new GenericListOfElements<Clients>(itable);
            ViewBag.Clients = clients.ListOfItems;


            return View();
        }


    }
}