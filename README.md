# Build Server Fake Store with .NET
## Description: .NET 7 Web API & Entity Framework SQL Server
## Document: 

### 0. Database Diagram
<img src="https://imgur.com/4bCE2E0.jpg"> <br>

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

### 2. Auth
#### Register (Admin)

```javascript
var raw = JSON.stringify({
  "userName": "minh3",
  "userEmail": "minh@gmail.com",
  "password": "123"
  "role": "Admin"
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Auth/register", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));

```

#### Register (Customer)

```javascript
var raw = JSON.stringify({
  "userName": "minh4",
  "userEmail": "minh@gmail.com",
  "password": "123"
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Auth/register", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));

```
#### Login 
```javascript
var raw = JSON.stringify({
  "userName": "minh3",
  "userEmail": "minh@gmail.com",
  "password": "123"
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Auth/Login", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```

##### Output (JWT)
<img src="https://imgur.com/O0yke5s.jpg"> <br>


#### Get infor 
```javascript
var myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");
myHeaders.append("Authorization", "Bearer {token}");

var raw = JSON.stringify({
  "userName": "minh3",
  "userEmail": "minh@gmail.com",
  "password": "123"
});

var requestOptions = {
  method: 'GET',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Auth/me", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output 
<img src="https://imgur.com/zjGjwtk.jpg"> <br>




### 3. User
#### Add cart 
```javascript
var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer {token}");
var raw = JSON.stringify({
  "cartProduct": [
    {
      "productId": 2,
      "quantity": 1
    },
    {
      "productId": 1,
      "quantity": 2
    },
    {
      "productId": 3,
      "quantity": 1
    }
  ]
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/User/Cart", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
#### Get cart 
```javascript
var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer {token}");
var requestOptions = {
  method: 'GET',
  headers: myHeaders,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/User/Cart", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
  ```
##### Output 
<img src="https://imgur.com/fyHWLJT.jpg"> <br>
