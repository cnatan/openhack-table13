How to build the container?
> sudo docker build . 

How to create the kubernetes pod?
> kubectl apply -f Kubernetes/pod.yaml

How to create the kubernetes service that will expose the pod?
> kubectl apply -f Kubernetes/service.yaml