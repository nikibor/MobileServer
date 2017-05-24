using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MobileServer.Core;
using MobileServer.Models;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileServer.Controllers
{
    public class ByNameController : ApiController
    {
        [HttpGet]        
        public IHttpActionResult Index()
        {
            NeoTool.Client.Connect();            
            int deep = DataSafe.GraphByName.deep;
            string name = DataSafe.GraphByName.name;
            var query = NeoTool.Client.Cypher
                .Match($"(m:VkMobile)-[:FRIEND*{deep}]-(a:VkMobile)")
                .Where((NeoUser m) => m.name == name)
                .Return((m, a) => new
                {
                    movie = m.As<NeoUser>().name,
                    cast = Return.As<string>("collect(a.name)")
                })
                .Limit(100);

            NeoTool.Client.Dispose();
            var data = query.Results.ToList();

            var nodes = new List<NodeResult>(); //узлы 
            var rels = new List<object>(); //связи
            int i = 0, target;
            foreach (var item in data)
            {
                nodes.Add(new NodeResult { title = item.movie, label = "movie" });
                target = i;
                i++;
                if (!string.IsNullOrEmpty(item.cast))
                {
                    var casts = JsonConvert.DeserializeObject<JArray>(item.cast);
                    foreach (var cast in casts)
                    {
                        var source = nodes.FindIndex(c => c.title == cast.Value<string>());
                        if (source == -1)
                        {
                            nodes.Add(new NodeResult { title = cast.Value<string>(), label = "actor" });
                            source = i;
                            i += 1;
                        }
                        rels.Add(new { source = source, target = target });
                    }
                }
            }

            return Ok(new { nodes = nodes, links = rels });
        }
    }
}
