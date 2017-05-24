using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileServer.Core;
using MobileServer.ViewModels;

namespace MobileServer.Controllers
{
    public class VisualisationController : Controller
    {
        // GET: Visualisation
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ByName(int deep, string name)
        {
            GraphByName graph = new GraphByName()
            {
                deep = deep,
                name = name
            };
            graph.name = graph.name.Remove(0, 1);
            graph.name = graph.name.Remove(graph.name.Length - 1, 1);
            DataSafe.GraphByName = graph;
            return View(graph);
        }

        public ActionResult ById(int deep, string id)
        {
            GraphById graph = new GraphById()
            {
                deep = deep,
                id = id
            };
            DataSafe.GraphById = graph;
            return View(graph);
        }

        public ActionResult SettingsByName(string name, int sex, bool ulyanovsk)
        {
            GraphSettingsByName graph = new GraphSettingsByName()
            {
                name = name,
                sex = sex,
                ulyanovsk = ulyanovsk
            };
            graph.name = graph.name.Remove(0, 1);
            graph.name = graph.name.Remove(graph.name.Length - 1, 1);
            DataSafe.GraphSettingsByName = graph;
            return View(graph);
        }

        public ActionResult SettingsById(string id, int sex, bool ulyanovsk)
        {
            GraphSettingsById graph = new GraphSettingsById()
            {
                id = id,
                sex = sex,
                ulyanovsk = ulyanovsk
            };
            DataSafe.GraphSettingsById = graph;
            return View(graph);
        }
    }
}