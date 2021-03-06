﻿using System;
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
            ViewBag.Title = "Enerprof";
            return View();
        }


        public ActionResult Contacts()
        {
            ViewBag.Title = "Контакты";
            return View();
        }
        [HttpPost]
        public ActionResult Contacts(string name, string mail, string text)
        {
            ITable itable = new QuestionsReader();
            TableReader reader = new TableReader(itable);
            int id = CreateId();
            object[] parameters = { id, name, mail, text, "Новое" };
            reader.AddRow(parameters);
            return Contacts();
        }

        public ActionResult categories(int? parent_id)
        {
            ITable itable = new CategoriesReader();
            GenericListOfElements<Category> cats_gen = new GenericListOfElements<Category>(itable);
            Categories_Page model = new Categories_Page();
            List<Category> categories = cats_gen.ListOfItems;
            if (parent_id != null)
            {
                model.Description = categories.Where(cat => cat.ID == parent_id).Select(desc => desc.Description).ToList()[0];
                ViewBag.CatName = categories.Where(cat => cat.ID == parent_id).Select(name => name.Name).ToList()[0];
                categories = categories.Where(parent => parent.ParentID == parent_id).ToList();
            }
            else
            { 
                categories = categories.Where(parent => parent.ParentID == 0).ToList();
                ViewBag.CatName = "Оборудование";
            }
            model.Categories = categories;

            if (categories.Count == 0)
                return products((int)parent_id);

            return View(model);
        }

        public ActionResult products(int cat_id)
        {
            ITable itable = new ProductsReader();
            GenericListOfElements<Product> products_gen = new GenericListOfElements<Models.Product>(itable, cat_id.ToString(), "catID");
            List<Product> model = products_gen.ListOfItems;
            return View(model);
        }

        public ActionResult Product(int id)
        {
            ITable itable = new ProductsReader();
            TableReader reader = new TableReader(itable);
            GenericListOfElements<Product> gen_prod = new GenericListOfElements<Models.Product>(itable, id.ToString());
            Product model = gen_prod.Model;
            XMLReader xml_reader = new XMLReader(id);
            model.Images.Remove(model.Images.Length - 1, 1);
            itable = new CommentsReader();
            GenericListOfElements<Comment> gen_comments = new GenericListOfElements<Comment>(itable);
            model.Comments = gen_comments.ListOfItems;
            model.Types = xml_reader.SelectTypes(xml_reader.node);
            List<Property> properties = new List<Property>();
            properties = xml_reader.SelectProps(xml_reader.node);
            ViewBag.Title = model.Name;
            return View(model);
        }

        public EmptyResult AddComment(string name, string mail, string text, int product_id)
        {
            if (name == null)
                name = "Гость";
            if (mail == null)
                mail = "";
            ITable itable = new CommentsReader();
            TableReader reader = new TableReader(itable);
            string img = "/images/user.png";
            int id = CreateId();
            object[] parameters = { id, product_id, name, mail, text, img };
            reader.AddRow(parameters);
            return new EmptyResult();
        }

        //Создает уникальный Id
        public int CreateId()
        {
            int id = (new Random().Next(0, 10000000) * Int32.Parse(DateTime.Now.Millisecond.ToString()));
            return id;
        }
    }
}