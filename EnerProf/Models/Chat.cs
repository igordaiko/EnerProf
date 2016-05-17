using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ep.Models
{
    public class Chat
    {
        public string RoomName { get; set; }
        public string Date { get; set; }
        public List<Users> Users { get; set; }
    }
}
