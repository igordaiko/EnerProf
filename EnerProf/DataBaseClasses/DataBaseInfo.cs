using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
