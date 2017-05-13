using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileServer.Core;
using MobileServer.Models;

namespace MobileServer.Controllers
{
    public class DataController : Controller
    {
        // GET: Data        

        public ActionResult AllUsers()
        {
            NeoTool.Client.Connect();
            var result = NeoTool.Client.Cypher
                .Match("(user:VkMobile)")                
                .Return(user => user.As<NeoUser>())
                .Results;
            NeoTool.Client.Dispose();
            return View(result);
        }                
    }
}