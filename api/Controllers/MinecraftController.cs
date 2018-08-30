using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class MinecraftController : Controller
    {
        private IConfiguration config;

        public MinecraftController(IConfiguration config)
        {
            this.config = config;
        }

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

        [HttpPost]
        public void Post([FromBody]MinecraftServer request)
        {
            var name = request.Name;
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            KubernetesService service = new KubernetesService();
            service.Delete(name);
        }
    }
}
