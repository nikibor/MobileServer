using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileServer.Models
{
    public class NeoUser
    {
        public long vk_id { set; get; }
        public string name { set; get; }        
        public int sex { set; get; }
        public long city_id { set; get; }        
        public string photo { set; get; }
    }
}