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
##### Output
<img src="https://imgur.com/3pBQMED.jpg"> <br>

#### Get single product	
```javascript
fetch("https://localhost:7204/api/Product/1", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output
<img src="https://imgur.com/pLUECDh.jpg"> <br>

#### Limit result 
```javascript
fetch("https://localhost:7204/api/Product?PageNumber=1&PageSize=2", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output
<img src="https://imgur.com/cvYMMZV.jpg"> <br>

#### Sort result (base on Price)
```javascript
fetch("https://localhost:7204/api/Product?IsDescending=true", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output
<img src="https://imgur.com/QfiZo9o.jpg"> <br>

#### Get all Categories
```javascript
fetch("https://localhost:7204/api/Product/Categories", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output
<img src="https://imgur.com/o9eXzYs.jpg"> <br>

#### Get in Categories
```javascript
fetch("https://localhost:7204/api/Product/Category/jewelry", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output
<img src="https://imgur.com/IMYzyXD.jpg"> <br>





