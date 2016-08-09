using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EnerProf.Models
{
    public interface IModel
    {
        int ID { get; set; }
        String Name { get; set; }
        string Img { get; set; }
    }
}
