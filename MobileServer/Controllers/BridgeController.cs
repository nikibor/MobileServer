using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MobileServer.Core;

namespace MobileServer.Controllers
{
    public class BridgeController : ApiController
    {
        
        public string Get(long id)
        {
            var s = NeoTool.TakeFriends(148266446);
            return "";
        }
    }
}
