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
    public class TableReader
    {
        SqlDataAdapter adapter;
        DataSet set;
        string con;
        ITable table;
        public TableReader(ITable table)
        {
            con = DataBaseInfo.GetDataBaseConnectionString();
            this.adapter = table.Adapter;
            this.table = table;
            set = new DataSet();
            adapter.Fill(set);
        }


        public DataTable SelectRows()
        {
            DataTable table = set.Tables[0].Clone();
            foreach (DataRow row in set.Tables[0].Rows)
            {
                table.ImportRow(row);
            }
            return table;
        }
        public DataTable SelectRows(int count)
        {
            DataTable table = set.Tables[0].Clone();
            foreach (DataRow row in set.Tables[0].Rows)
            {
                if (count >= 0)
                {
                    table.ImportRow(row);
                    count--;
                }
            }
            return table;
        }
        /// <summary>
        /// Find element by id
        /// </summary>
        public DataRow FindElementById(int id)
        {
            DataRow row = set.Tables[0].NewRow();
            foreach (DataRow r in set.Tables[0].Rows)
            {
                if (Convert.ToInt32(r["id"]) == id)
                    row = r;
            }

            return row;
        }
        public DataTable SelectRows(string find, string row_name)
        {
            DataTable table = set.Tables[0].Clone();
            int i = 0;
            foreach (DataRow row in set.Tables[0].Rows)
            {
                if (row[row_name].ToString() == find)
                {
                    table.ImportRow(row);
                    i++;
                }
            }
            return table;
        }
        public void AddRow(object[] colls)
        {
            DataTable table = SelectRows();
            DataRow row = table.NewRow();
            int i = 0;
            foreach (object coll in colls)
            {
                row[i] = coll;
                i++;
            }
            table.Rows.Add(row);
            using (new SqlCommandBuilder(adapter))
            {
                adapter.Update(table);
            }
        }
        public void UpdateRow(object[] colls)
        {
            SqlConnection connection = new SqlConnection(con);
            string query = table.queryUpdate;
            SqlCommand command = new SqlCommand(query, connection);
            SqlParameter parameter;
            for (int i = 0; i < colls.Length; i++)
            {
                parameter = new SqlParameter("@p" + (i + 1), colls[i]);
                command.Parameters.Add(parameter);
            }
            connection.Open();
            command.ExecuteNonQuery();
        }
        public void DeleteRow(int id, string table_name)
        {
            SqlConnection connection = new SqlConnection(con);
            string query = string.Format("delete {0} where id = {1}", table_name, id);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();           
        }
        
    }
}
