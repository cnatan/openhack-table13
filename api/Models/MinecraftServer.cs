using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Models
{
    public class MinecraftServer
    {
        public string Name { get; set; }

        public MinecraftEndpoint Endpoints { get; set; }
    }
} 