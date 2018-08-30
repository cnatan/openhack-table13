using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace api.Services
{
    public class KubernetesService
    {   
        //TODO: read k8 namespace from config.
        private string k8Namespace;
        private IKubernetes kservice;
        private IConfiguration appConfig; 
        public KubernetesService(IConfiguration appConfig)
        {
            this.appConfig = appConfig;
            this.k8Namespace = appConfig.GetValue<string>("K8_TARGET_NAMESPACE");
            Connect();
        }

        private KubernetesClientConfiguration GetKubernetesClient()
        {
            KubernetesClientConfiguration config = new KubernetesClientConfiguration();
            
            config.AccessToken = appConfig.GetValue<string>("K8_ACCESS_TOKEN");
            config.ClientCertificateData = appConfig.GetValue<string>("K8_CLIENT_CERTIFICATE_DATA");
            config.ClientCertificateKeyData = appConfig.GetValue<string>("K8_CLIENT_CERTIFICATE_KEY_DATA");
            config.ClientKeyFilePath = "";
            config.ClientCertificateFilePath = "";
            config.Host = appConfig.GetValue<string>("K8_TARGET_HOST");
            config.Namespace = "";
            config.Password = "";
            config.SkipTlsVerify = true;
            config.SslCaCert = null;
            config.UserAgent = null;
            config.Username = null;
            
            return config;
        }
        private void Connect()
        {
            KubernetesClientConfiguration config = GetKubernetesClient();
            kservice = new Kubernetes(config);
        }

        public void Delete(string name)
        {
            kservice.DeleteNamespacedService(new V1DeleteOptions(), name, this.k8Namespace);
        }

        public void Add(string name)
        {
            try
            {
                kservice.CreateNamespacedService(ServiceBody(name), this.k8Namespace);
            }
            catch (System.Exception ex)
            {                
                throw ex;
            }
            
        }

        private V1Service ServiceBody(string name)
        {
            V1Service service = new V1Service();
            service.ApiVersion = "v1";
            service.Kind = "Service";
            service.Metadata = new V1ObjectMeta() { Name = name};
            
            
            var serviceSpec = new V1ServiceSpec() { Type = "LoadBalancer", Ports = new List<V1ServicePort>() };
            serviceSpec.Ports.Add(new V1ServicePort(){ Name = "main" , Port = 25565 });
            serviceSpec.Ports.Add(new V1ServicePort(){ Name = "openhackcheck" , Port = 25575 });

            service.Spec = serviceSpec;
            return service;
        }

        public List<MinecraftServer> ListServices()
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
