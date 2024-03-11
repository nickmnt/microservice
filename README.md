
Add to hosts file:  
```127.0.0.1 acme.com```

Start up ingress:  
```kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.0/deploy/static/provider/cloud/deploy.yaml```

Set up the password for production db:  
```kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"```

Start up the cluster:  
```kubectl apply -f .```  
  
Note: For PlatformService, since we are using in-memory database for development and SQL Server for production, we temporarily commented out the code for in-memory database and switched to a SQL Server localhost database to apply migrations using EF Core to generate the migration code and switched back again to in-memory database after the migration code (needed for the production db seed code) was generated!
