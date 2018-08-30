using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models;
using api.Services;
using k8s.Models;
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
        public JsonResult ListServices()
        {
            KubernetesService service = new KubernetesService();
            return Json(service.ListServices());
        }

        [HttpPost]
        public void Post([FromBody]string name)
        {
            
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {

        }
    }
}
