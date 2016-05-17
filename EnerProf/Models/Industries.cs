using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnerProf.Models
{
    public class Industries : IModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
    }
}