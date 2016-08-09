using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using EnerProf.DataBaseClasses;
using EnerProf.Models.AdminModels;
namespace EnerProf.Models
{
    /// <summary>
    /// Класс предназначен для получение списка с типом Модели Т. 
    /// </summary>
    /// <typeparam name="T">Имя класса модели</typeparam>
    public class GenericListOfElements<T>
    {
        ITable itable;
        TableReader reader;
        DataTable table;
        public GenericListOfElements(ITable itable)
        {
            this.itable = itable;
            reader = new TableReader(this.itable);
            table = reader.SelectRows();
            ListOfItems = ReturnList();
        }

        //Конструктор предназначен для выбора элементов соответсвующих критериям поиска
        public GenericListOfElements(ITable itable, string find, string row_name)
        {
            this.itable = itable;
            reader = new TableReader(this.itable);
            table = reader.SelectRows(find, row_name);
            ListOfItems = ReturnList();

        }
        //Конструктор предназначен для выбора по определенным колонкам
        public GenericListOfElements(ITable itable, string[] columns_)
        {
            this.itable = itable;
            reader = new TableReader(this.itable);
            table = reader.SelectRows(columns_);
            ListOfItems = ReturnList();
        }
        //Конструктор предназначен для заполнения модели одного элемента
        public GenericListOfElements(ITable itable, string id)
        {
            this.itable = itable;
            reader = new TableReader(this.itable);
            table = reader.SelectRows(id, "id");
            Model = ReturnModel();
        }

        //Коллекция типа Модели
        public List<T> ListOfItems { get; set; }
        public T Model { get; set; }

        private List<T> ReturnList()
        {
            List<T> list = new List<T>();
            T model;
            Type type;
            foreach (DataRow row in table.Rows)
            {
                //Создание экземляра класса модели
                model = (T)Activator.CreateInstance(typeof(T));
                //Нужно для рефлексии
                type = typeof(T);
                //Получение набора свойств
                PropertyInfo[] props = type.GetProperties();
                for(int i = 0; i < table.Columns.Count; i++)
                {
                    //Устанавливаю значение свойства за индексом
                    props[i].SetValue(model, row[i]);
                }
                list.Add(model);
            }
            return list;   
        }
        private T ReturnModel()
        {
            T model;
            Type type;
            model = (T)Activator.CreateInstance(typeof(T));
            type = typeof(T);
            DataRow row = table.Rows[0];
            //Получение набора свойств
            PropertyInfo[] props = type.GetProperties();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                //Устанавливаю значение свойства за индексом
                props[i].SetValue(model, row[i]);
            }
            return model;
        }
    }
}