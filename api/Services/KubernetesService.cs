using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{
    public class KubernetesService
    {
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

        public List<MinecraftServer> Services()
        {
            var services = kservice.ListNamespacedService("default");

            List<MinecraftServer> kservices = new List<MinecraftServer>();

            foreach (var service in services.Items)
            {
                MinecraftServer ms = new MinecraftServer();
                ms.Name = service.Metadata.Name;                

                MinecraftEndpoint me = new MinecraftEndpoint();
                if(service.Status.LoadBalancer.Ingress != null)
                {
                    me.Minecraft = service.Status.LoadBalancer.Ingress.FirstOrDefault().Ip + ":" + service.Spec.Ports.FirstOrDefault(x => x.Name.Equals("main")).Port;
                    me.RCon = service.Status.LoadBalancer.Ingress.FirstOrDefault().Ip + ":" + service.Spec.Ports.FirstOrDefault(x => x.Name.Equals("openhackcheck")).Port;
                }else{
                    continue;                    
                }

                ms.Endpoints = me;
                kservices.Add(ms);              
            }

            return kservices;
        }

        public V1ServiceList GetAll()
        {
            var services = kservice.ListServiceForAllNamespaces();
            return services;
        }

    } 
    
}
