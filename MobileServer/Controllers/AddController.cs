using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MobileServer.Core;
using MobileServer.Models;
using MobileServer.ViewModels;
using Newtonsoft.Json;

namespace MobileServer.Controllers
{
    public class AddController : Controller
    {
        // GET: Add       
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(VkCreateModel model)
        {
            string json = HttpProtocol.TakeData(
                $"https://api.vk.com/method/users.get?user_ids={model.user_id}&fields=sex,city,photo_50", "GET",
                Encoding.UTF8);
            var person = JsonConvert.DeserializeObject<Response>(json);
            VkUser user =  person.response[0];
            NeoTool.CreateUser(user);
            return RedirectToAction("AllUsers","Data");
        }
        [HttpGet]
        public ActionResult AddUserFriends()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUserFriends(VkCreateModel model)
        {
            string jsonOrigin = HttpProtocol.TakeData(
                $"https://api.vk.com/method/users.get?user_ids={model.user_id}&fields=sex,city,photo_50", "GET",
                Encoding.UTF8);
            var person = JsonConvert.DeserializeObject<Response>(jsonOrigin);
            VkUser userOrigin = person.response[0];
            NeoTool.CreateUser(userOrigin);
            string jsonEnd = HttpProtocol.TakeData(
                $"https://api.vk.com/method/friends.get?user_id={userOrigin.uid}&fields=city,sex,photo_50", "GET",
                Encoding.UTF8);
            var friends = JsonConvert.DeserializeObject<Response>(jsonEnd);
            foreach (var vkUser in friends.response)
            {
                NeoTool.CreateUser(vkUser,userOrigin);
            }
            return RedirectToAction("AllUsers", "Data");
        }
    }
}