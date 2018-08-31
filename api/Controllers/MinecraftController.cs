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
            KubernetesService service = new KubernetesService(this.config);
            return Json(service.ListServices());
        }

        [HttpPost]
        public void Post([FromBody]MinecraftServer request)
        {
            KubernetesService service = new KubernetesService(this.config);
            service.Add(request.Name.ToLower());
        }

        [HttpDelete("{name}")]
        public void Delete(string name)
        {
            KubernetesService service = new KubernetesService(this.config);
            service.Delete(name.ToLower());
        }
    }
}
