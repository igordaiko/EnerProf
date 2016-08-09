using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
namespace EnerProf.DataBaseClasses
{
    static public class DataBaseInfo
    {
        private static string DataBaseConnectionString 
        {
            get
            {
                return "Data Source=SQL5026.Smarterasp.net;Initial Catalog=DB_9F29FB_enerProf;User Id=DB_9F29FB_enerProf_admin;Password=i1g6o0r4";
            }
        }
        private static string CommentsXmlFileConnectionString
        {
            get
            {
                return  "/App_Data/Comments.xml";
            }
        }
        static public string GetDataBaseConnectionString()
        {
            return DataBaseConnectionString;
        }
        static public string GetCommentsConnectionString()
        {
            return CommentsXmlFileConnectionString;
        }

    }
}
