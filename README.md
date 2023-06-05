# Build Server Fake Store with .NET
## Description: .NET 7 Web API & Entity Framework SQL Server
## Document: 
### 1. Products
#### Get all products		
```javascript
fetch("https://localhost:7204/api/Product", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```





