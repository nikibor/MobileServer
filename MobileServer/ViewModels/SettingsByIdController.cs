using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MobileServer.Controllers;
using MobileServer.Core;
using MobileServer.Models;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileServer.ViewModels
{
    public class SettingsByIdController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            NeoTool.Client.Connect();
            bool ulyanovsk = DataSafe.GraphSettingsById.ulyanovsk;
            if (ulyanovsk)
            {
                int sex = DataSafe.GraphSettingsById.sex;
                if (sex < 3)
                {
                    long id = Convert.ToInt64(DataSafe.GraphSettingsById.id);
                    var query = NeoTool.Client.Cypher
                        .Match($"(x:VkMobile)-[:FRIEND]-(y:VkMobile)")
                        .Where((NeoUser y) => y.city_id == 149)
                        .AndWhere((NeoUser y) => y.sex == sex)
                        .AndWhere((NeoUser x) => x.vk_id == id)
                        .Return((x, y) => new
                        {
                            movie = x.As<NeoUser>().name,
                            cast = Return.As<string>("collect(y.name)")
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
                else
                {
                    long id = Convert.ToInt64(DataSafe.GraphSettingsById.id);
                    var query = NeoTool.Client.Cypher
                        .Match($"(x:VkMobile)-[:FRIEND]-(y:VkMobile)")
                        .Where((NeoUser y) => y.city_id == 149)
                        .AndWhere((NeoUser x) => x.vk_id == id)
                        .Return((x, y) => new
                        {
                            movie = x.As<NeoUser>().name,
                            cast = Return.As<string>("collect(y.name)")
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
            else
            {
                int sex = DataSafe.GraphSettingsById.sex;
                if (sex < 3)
                {
                    long id = Convert.ToInt64(DataSafe.GraphSettingsById.id);
                    var query = NeoTool.Client.Cypher
                        .Match($"(x:VkMobile)-[:FRIEND]-(y:VkMobile)")
                        .Where((NeoUser y) => y.sex == sex)
                        .AndWhere((NeoUser x) => x.vk_id == id)
                        .Return((x, y) => new
                        {
                            movie = x.As<NeoUser>().name,
                            cast = Return.As<string>("collect(y.name)")
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
                else
                {
                    long id = Convert.ToInt64(DataSafe.GraphSettingsById.id);
                    var query = NeoTool.Client.Cypher
                        .Match($"(x:VkMobile)-[:FRIEND]-(y:VkMobile)")
                        .Where((NeoUser x) => x.vk_id == id)
                        .Return((x, y) => new
                        {
                            movie = x.As<NeoUser>().name,
                            cast = Return.As<string>("collect(y.name)")
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
    }
}
