using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EnerProf.Models.AdminModels;
using EnerProf.Models;
using EnerProf.DataBaseClasses;
using EnerProf.DataBaseClasses.EnerProfDataSetTableAdapters;
using System.Windows.Forms;
using System.Reflection;

namespace EnerProf.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string tablename)
        {
            if (tablename == null)
                tablename = "Categories";
            AdminStartPage model = new AdminStartPage();
            DataSet set = new EnerProfDataSet();
            ITable itable = ITableReader(tablename);
            string[] columns = { "id", "name", "img" };
            GenericListOfElements<ListOfElements> give_list = new GenericListOfElements<ListOfElements>(itable, columns);
            List<ListOfElements> list_of_elements = give_list.ListOfItems;
            if (tablename != "images")
            {
                List<ImagesModel> images = GiveImages();
                foreach (var item in list_of_elements)
                {
                    int index = images.Select(i => i.ID).ToList().IndexOf(int.Parse(item.Img));
                    item.Img = images[index].Address;
                }
            }
            ViewBag.Title = tablename;
            set.Tables.Remove("images");
            model.Tables = set.Tables;
            model.List_Of_Elements = list_of_elements;
            return View(model);
        }
        public ActionResult ElementsList()
        {
            return PartialView();
        }

        public ViewResult Edit_Categories(string id)
        {
            string tablename = "Categories";
            ITable itable = ITableReader(tablename);
            IModel model = GiveModel(tablename, itable, id);
            ViewBag.Images = GiveImages();
            ViewBag.Categories = GiveCategories();
            return View(model);
        }
        public ViewResult Edit_clients(string id)
        {
            string tablename = "clients";
            ITable itable = ITableReader(tablename);
            IModel model = GiveModel(tablename, itable, id);
            ViewBag.Images = GiveImages();
            return View(model);
        }
        public ViewResult Edit_companies(string id)
        {
            string tablename = "clients";
            ITable itable = ITableReader(tablename);
            IModel model = GiveModel(tablename, itable, id);
            return View(model);
        }
        public ViewResult Edit_images(string id)
        {
            string tablename = "images";
            ITable itable = ITableReader(tablename);
            IModel model = GiveModel(tablename, itable, id);
            return View(model);
        }
        public ViewResult Edit_products(string id)
        {
            string tablename = "products";
            ITable itable = ITableReader(tablename);
            IModel model = GiveModel(tablename, itable, id);
            return View(model);
        }

        //Post методы
        [HttpPost]
        public ActionResult Edit_Categories(int id, string name, string img, string parentID)
        {
            ITable itable = new CategoriesReader();
            TableReader reader = new TableReader(itable);
            object[] param = new object[4] { id, name, img, int.Parse(parentID) };
            reader.UpdateRow(param);
            TempData["message"] = string.Format("Изменения в категории \"{0}\" были сохранены", name);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit_clients(int id, string name, string img)
        {
            ITable itable = new ClientsReader();
            TableReader reader = new TableReader(itable);
            object[] param = new object[3] { id, name, img };
            reader.UpdateRow(param);
            TempData["message"] = string.Format("Изменения в клиенте \"{0}\" были сохранены", name);
            return RedirectToAction("Index", new { tablename = "clients" });

        }
        [HttpPost]
        public ActionResult Edit_images(int id, string address)
        {
            ITable itable = new ClientsReader();
            TableReader reader = new TableReader(itable);
            object[] param = new object[2] { id, address };
            reader.UpdateRow(param);
            TempData["message"] = string.Format("Изображение было сохранено");
            return RedirectToAction("Index", new { tablename = "clients" });
        }



        private ITable ITableReader(string tablename)
        {
            ITable itable = null;
            switch (tablename)
            {
                case ("Categories"):
                    itable = new CategoriesReader();
                    break;
                case ("clients"):
                    itable = new ClientsReader();
                    break;
                case ("companies"):
                    itable = new CompaniesReader();
                    break;
                case ("images"):
                    itable = new ImagesReader();
                    break;
                case ("industries"):
                    //itable = new IndustriesReader();
                    break;
                case ("products"):
                    itable = new ProductsReader();
                    break;
            }
            return itable;
        }
        private IModel GiveModel(string tablename, ITable itable, string id)
        {
            IModel imodel = null;
            dynamic give_list;
            switch (tablename)
            {
                case ("Categories"):
                    
                    give_list = new GenericListOfElements<Category>(itable, id);
                    imodel = give_list.Model;
                    break;
                case ("clients"):
                    give_list = new GenericListOfElements<Clients>(itable, id);
                    imodel = give_list.Model;
                    break;
                case ("companies"):
                    imodel = new Company();
                    break;
                case ("images"):
                    give_list = new GenericListOfElements<ImagesModel>(itable, id);
                    imodel = give_list.Model;
                    break;
                case ("industries"):
                    //itable = new IndustriesReader();
                    break;
                case ("products"):
                    imodel = new Product();
                    break;
            }
            return imodel;
        }
        private List<ImagesModel> GiveImages()
        {
            List<ImagesModel> images;
            string tablename = "images";
            ITable itable = ITableReader(tablename);
            GenericListOfElements<ImagesModel> give_list = new GenericListOfElements<ImagesModel>(itable);
            images = give_list.ListOfItems;
            return images;
        }
        private List<Category> GiveCategories()
        {
            List<Category> categories;
            ITable itable = ITableReader("Categories");
            GenericListOfElements<Category> give_list = new GenericListOfElements<Category>(itable);
            categories = give_list.ListOfItems;
            return categories;
        }
    }
}