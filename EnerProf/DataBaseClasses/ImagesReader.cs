using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EnerProf.DataBaseClasses
{
    public class ImagesReader : ITable
    {
        public SqlDataAdapter adapter;
        SqlCommand command;
        SqlConnection connection;
        DataSet set;
        string query, con;
        public ImagesReader()
        {
            query = "Select * from images";
            command = new SqlCommand(query);
            con = DataBaseInfo.GetDataBaseConnectionString();
            connection = new SqlConnection(con);
            adapter = new SqlDataAdapter(query, connection);
            Adapter = adapter;
            queryUpdate = "Update images set [address] = @p2" +
                " where [id] = @p1";
        }
        public SqlDataAdapter Adapter { get; set; }
        public string queryUpdate { get; set; }
    }
}