using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileServer.Models;
using Neo4jClient;

namespace MobileServer.Core
{
    public class NeoTool
    {
        public static GraphClient Client { get; set; } =
            new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "root");

        /// <summary>
        /// Проверка существования элемента в базе
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true - если существует, false - если нет элемента в базе</returns>
        public static bool IsExist(VkUser model)
        {

            Client.Connect();
            var result = Client.Cypher
                .Match("(user:VkMobile)")
                .Where((NeoUser user) => user.vk_id == model.uid)
                .Return(user => user.As<NeoUser>())
                .Results;
            Client.Dispose();
            if (result.Any())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Проверка связей между сущностями
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns>true - есть связь от origin к target, false - нет</returns>
        public static bool IsRelation(VkUser origin, VkUser target)
        {
            Client.Connect();
            var result = Client.Cypher
                .Match("(x:VkMobile)-[:FRIEND]->(y:VkMobile)")
                .Where((NeoUser x) => x.vk_id == origin.uid)
                .AndWhere((NeoUser y) => y.vk_id == target.uid)
                .Return((x, y) => new
                {
                    x = x.As<NeoUser>(),
                    y = y.As<NeoUser>()
                })
                .Results;
            Client.Dispose();
            if (result.Any())
            {
                return true;
            }
            return false;
        }
        

        public static List<NeoUser> TakeFriends(long id)
        {
            Client.Connect();
            var result = Client.Cypher
                .Match("(x:VkMobile)-[:FRIEND]->(y)")
                .Where((NeoUser x) => x.vk_id == id)
                .Return((x, y) => new
                {
                    x = x.As<NeoUser>(),
                    y = y.As<NeoUser>()
                })
                .Results;
            Client.Dispose();
            var s = result as List<NeoUser>;
            return new List<NeoUser>();
        }
        public static void CreateUser(VkUser user)
        {
            NeoUser db_user = new NeoUser()
            {
                city_id = user.city,                
                name = $"{user.first_name} {user.last_name}",
                photo = user.photo_50,
                sex = user.sex,
                vk_id = user.uid
            };
            if (!IsExist(user))
            {
                Client.Connect();
                Client.Cypher
                    .Create("(user:VkMobile {newUser})")
                    .WithParam("newUser", db_user)
                    .ExecuteWithoutResults();
            }
            
        }
        public static void CreateUser(VkUser user,VkUser origin)
        {
            NeoUser db_user = new NeoUser()
            {
                city_id = user.city,
                name = $"{user.first_name} {user.last_name}",
                photo = user.photo_50,
                sex = user.sex,
                vk_id = user.uid
            };
            if (!IsExist(user))
            {
                Client.Connect();
                Client.Cypher
                    .Create("(user:VkMobile {newUser})")
                    .WithParam("newUser", db_user)
                    .ExecuteWithoutResults();
            }            
            if(!IsRelation(user,origin))
                CreateRelation(user,origin);
            if(!IsRelation(origin,user))
                CreateRelation(origin,user);

        }

        public static void CreateRelation(VkUser origin, VkUser target)
        {
            Client.Connect();
            Client.Cypher
                .Match("(x:VkMobile)","(y:VkMobile)")
                .Where((NeoUser x) => x.vk_id == origin.uid)
                .AndWhere((NeoUser y) => y.vk_id == target.uid)
                .Create("(x)-[:FRIEND]->(y)")
                .ExecuteWithoutResults();
            Client.Dispose();
        }
    }
}