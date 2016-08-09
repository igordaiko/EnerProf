using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace EnerProf.Models.AdminModels
{
    public class AdminStartPage
    {
        public DataTableCollection Tables { get; set; }
        public List<ListOfElements> List_Of_Elements { get; set; }
    }
}