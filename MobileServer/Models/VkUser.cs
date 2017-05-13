using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileServer.Models
{
    public class VkUser
    {
        public long uid { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }
        public int sex { set; get; }
        public int city { set; get; }
        public string photo_50 { set; get; }

    }    
    public class Response
    {
        public VkUser[] response { set; get; }
    }    
}