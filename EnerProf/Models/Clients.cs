using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnerProf.Models
{
    public class Clients : IModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
    }
}
