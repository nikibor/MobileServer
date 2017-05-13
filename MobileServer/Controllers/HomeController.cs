using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MobileServer.Core;
using MobileServer.Models;
using MobileServer.ViewModels;
using Neo4jClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileServer.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "root");
            //using (client)
            //{
            //    client.Connect();
            //}
            //var a = NeoTool.IsExist(new VkUser() { uid = 80974023 });
            //var b = NeoTool.IsExist(new VkUser() {uid = 148266446 });
            //if (a && b)
            //{
            //    var c = NeoTool.IsRelation(new VkUser() {uid = 80974023}, new VkUser() {uid = 148266446});
            //    var d = NeoTool.IsRelation(new VkUser() {uid = 148266446}, new VkUser() {uid = 80974023});
            //}
            return View();
        }

        public ActionResult All()
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "root");
            client.Connect();
            PersonA person = new PersonA()
            {                
                name = "Виктор"
            };
            client.Cypher
                .Create("(user:PersonA {newUser})")
                .WithParam("newUser", person)
                .ExecuteWithoutResults();

            var query = client.Cypher
                .Match("(user:PersonA)")
                .Return(user => user.As<PersonA>());                
            var longBooks = query.Results;
            
            return View(longBooks);
        }
        [HttpGet]
        public ActionResult VkSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VkSearch(VkCreateModel user_ids)
        {
            string json = HttpProtocol.TakeData(
                $"https://api.vk.com/method/users.get?user_ids={user_ids.user_id}&fields=sex,city,photo_50&v=5.62", "GET",
                Encoding.UTF8);
            var person = JsonConvert.DeserializeObject<Response>(json);
            NeoTool.CreateUser(person.response[0]);
            return RedirectToAction("VkResult", person.response[0]);
        }

        public ActionResult VkResult(VkUser person)
        {
            return View(person);
        }
    }

    
    public class PersonA
    {        
        public string name { set; get; }
    }    
}