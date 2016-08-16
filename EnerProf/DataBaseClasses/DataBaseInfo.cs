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
                return "Data Source=IGORDAIKO-PC;Initial Catalog=EnerProf;Integrated Security=True";
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
