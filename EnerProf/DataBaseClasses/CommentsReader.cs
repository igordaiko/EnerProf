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

namespace EnerProf.DataBaseClasses
{
    public class CommentsReader : ITable
    {
        public SqlDataAdapter adapter;
        SqlCommand command;
        SqlConnection connection;
        DataSet set;
        string query, con;
        public CommentsReader()
        {
            query = "Select * from Comments";
            command = new SqlCommand(query);
            con = DataBaseInfo.GetDataBaseConnectionString();
            connection = new SqlConnection(con);
            adapter = new SqlDataAdapter(query, connection);
            Adapter = adapter;
            queryUpdate = "Update Categories set [product_id] = @p2, [name] = @p3, [mail] = @p4, [text] = @p5, [img] = @p6" +
                " where [id] = @p1";
        }
        public SqlDataAdapter Adapter { get; set; }
        public string queryUpdate { get; set; }

    }
}
