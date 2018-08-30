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
        //TODO: read k8 namespace from config.
        private readonly string k8Namespace = "default";
        private IKubernetes kservice;
        public KubernetesService()
        {
            Connect();
        }

        private void Connect()
        {
            //TODO: read connection config properly.
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            kservice = new Kubernetes(config);
        }

        public void Delete(string name)
        {
            kservice.DeleteNamespacedService(new V1DeleteOptions(), name, this.k8Namespace);
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
