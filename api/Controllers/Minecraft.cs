using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class Minecraft : Controller
    {
        [HttpGet]
        public IEnumerable<MinecraftServer> GetServers()
        {
            return new MinecraftServer[] { 
                new MinecraftServer{
                    Name = "MockServer",
                    Endpoints = new MinecraftEndpoint {
                        Minecraft = "127.0.0.1:25565",
                        RCon = "127.0.0.1:25575"
                    }
                }
             };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
