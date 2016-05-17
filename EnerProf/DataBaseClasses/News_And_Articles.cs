using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
namespace ep.DataBase_Classes
{
    class News_And_ArticlesReader : ITable
    {
        public SqlDataAdapter adapter;
        SqlCommand command;
        SqlConnection connection;
        string query, con;
        public News_And_ArticlesReader()
        {
            query = "Select * from news_articles";
            command = new SqlCommand(query);
            con = DataBaseInfo.GetDataBaseConnectionString();
            connection = new SqlConnection(con);
            adapter = new SqlDataAdapter(query, connection);
            Adapter = adapter;
            queryUpdate = "Update news_arcticles set [name] = @p2, [news_arcticles] = @p3, [date] = @p4, [texxt] = @p5" +
                " where [id] = @p1";

        }
        public SqlDataAdapter Adapter { get; set; }
        public string queryUpdate { get; set; }

    }
}
