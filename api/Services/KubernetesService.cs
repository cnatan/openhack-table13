using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public class KubernetesService
    {   
        private readonly string k8Namespace = "default";
        private IKubernetes kservice;
        public KubernetesService()
        {
            Connect();
        }

        public void Connect()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            kservice = new Kubernetes(config);
        }

        public void Delete(string name)
        {
            kservice.DeleteNamespacedService(new V1DeleteOptions(), name, this.k8Namespace);
        }

    } 
    
}
