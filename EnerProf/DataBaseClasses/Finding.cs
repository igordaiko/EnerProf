using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EnerProf.Models;
namespace EnerProf.DataBaseClasses
{
    public class Finding_Class
    {
        private string find_text { get; set; }
        public Find_Page find_page { get; private set; }
        ITable table;
        TableReader reader;
        public Finding_Class(string find)
        {
            find_text = find;
            find_page = new Find_Page();
            find_page.Products = Find_In_Products();
            find_page.Companies = Find_In_Companies();
            find_page.Company_Prod = Find_By_CompId();
        }

        private List<Product> Find_In_Products()
        {
            List<Product> products = new List<Product>();
            table = new ProductsReader();
            reader = new TableReader(table);
            DataTable prod_table = reader.SelectRows();
            DataTable result_table = prod_table.Clone();
            Product product;
            DataRow r;

            //Шукаю повне співпадіння в назві
            foreach (DataRow row in prod_table.Rows)
            {
                string name = row[3].ToString().ToLower();
                if (name.Equals(find_text.ToLower()) || name.Contains(find_text.ToLower()))
                {
                    r = result_table.NewRow();
                    for (int i = 0; i < result_table.Columns.Count; i++)
                        r[i] = row[i];
                    result_table.Rows.Add(r);
                    row.Delete();
                }
            }
            prod_table.AcceptChanges();

            //Шукаю співпадіння по словах
            char[] separators = { ',', ' ', ';' }; //создаём массив символов, служащих разделителями слова
            foreach (DataRow row in prod_table.Rows)
            {
                string[] words = row[3].ToString().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] words_find = find_text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    foreach (string word_find in words_find)
                    {
                        if (word_find.ToLower().Equals(word.ToLower()))
                        {
                            r = result_table.NewRow();
                            for (int i = 0; i < result_table.Columns.Count; i++)
                                r[i] = row[i];
                            result_table.Rows.Add(r);
                            break;
                        }
                    }
                }
            }

            //Данні, записані в таблицю результатів, кидаю в колекцію
            foreach (DataRow row in result_table.Rows)
            {
                product = new Product();
                product.ID = (int)row[0];
                product.CompanyID = (int)row[1];
                product.CatID = (int)row[2];
                product.Name = (string)row[3];
                product.Img = (string)row[4];
                product.Description = (string)row[5];
                products.Add(product);
            }
            return products;
        }

        private List<Company> Find_In_Companies()
        {
            List<Company> companies = new List<Company>();
            table = new CompaniesReader();
            reader = new TableReader(table);
            DataTable comp_table = reader.SelectRows();
            Company company;
            foreach (DataRow row in comp_table.Rows)
            {
                string name = row["name"].ToString().ToLower();
                if (name.Equals(find_text.ToLower()) || name.Contains(find_text.ToLower()))
                {
                    company = new Company();
                    company.ID = (int)row["id"];
                    company.Name = (string)row["name"];
                    company.Img = (string)row["img"];
                    company.Description = (string)row["description"];
                    companies.Add(company);
                }
            }
            
            return companies;
        }

        private List<Product> Find_By_CompId()
        {
            List<Product> products = new List<Product>();
            List<Company> companies = Find_In_Companies();
            if(companies.Count != 0)
            {
                int id = companies[0].ID;
                table = new ProductsReader();
                reader = new TableReader(table);
                DataTable prod_table = reader.SelectRows(id.ToString(), "companyID");
                Product product;
                foreach (DataRow row in prod_table.Rows)
                {
                        product = new Product();
                        product.ID = (int)row[0];
                        product.CompanyID = (int)row[1];
                        product.CatID = (int)row[2];
                        product.Name = (string)row[3];
                        product.Img = (string)row[4];
                        product.Description = (string)row[5];
                        products.Add(product);
                }
            }
            return products;
        }
    }
}
