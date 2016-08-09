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
    public class QuestionsReader : ITable
    {
        public SqlDataAdapter adapter;
        SqlCommand command;
        SqlConnection connection;
        DataSet set;
        string query, con;
        public QuestionsReader()
        {
            query = "Select * from Categories";
            command = new SqlCommand(query);
            con = DataBaseInfo.GetDataBaseConnectionString();
            connection = new SqlConnection(con);
            adapter = new SqlDataAdapter(query, connection);
            Adapter = adapter;
            queryUpdate = "Update Categories set [name] = @p2, [mail] = @p3, [text] = @p4, [status] = @p5" +
                " where [id] = @p1";
        }
        public SqlDataAdapter Adapter { get; set; }
        public string queryUpdate { get; set; }
    }
}