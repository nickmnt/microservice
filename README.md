
Add to hosts file:
127.0.0.1 acme.com

Start up ingress:
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.0/deploy/static/provider/cloud/deploy.yaml

kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
