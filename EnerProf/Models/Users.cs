using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerProf.Models
{
    public class Users
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public List<string> Messages { get; set; }
    }
}
