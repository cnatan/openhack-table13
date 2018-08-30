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
        public KubernetesService()
        {
            
        }

        public string Connect()
        {
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            IKubernetes client = new Kubernetes(config);
            Console.WriteLine("Starting Request!");

            var list = client.ListNamespacedPod("default");
            var pod = list.Items[0];          

            return pod.ToString();             
        }

    } 
    
}
