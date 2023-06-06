# Backend Web Ecommerce
## Table of contents
- [Description](#description)
- [Documentation](#documentation)
  - [Database Diagram](#database-diagram)
  - [Products](#products)
    - [Get all products](#get-all-products)
    - [Get single product](#get-single-product)
    - [Limit result](#limit-result)
    - [Sort result (based on Price)](#sort-result-based-on-price)
    - [Get all Categories](#get-all-categories)
    - [Get products in Category](#get-products-in-category)
  - [Authentication](#authentication)
    - [Register (Admin)](#register-admin)
    - [Register (Customer)](#register-customer)
    - [Login](#login)
    - [Get info](#get-info)
  - [User](#user)
    - [Add to cart](#add-to-cart)
    - [Get cart](#get-cart)
  - [Admin](#admin)
    - [Get Users](#get-users)
    - [Get Single User](#get-single-user)
    - [Post Category](#post-category)
    - [Post Product](#post-product)
    - [Delete Product](#delete-product)
    - [Adjust Product](#adjust-product)

## Description
Backend web Ecommerce with .NET 7 Web API & Entity Framework SQL Server 
## Documentation
### Database Diagram
<img src="https://imgur.com/4bCE2E0.jpg"> <br>

### Products
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

### Auth
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




### User
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


### Admin (Role == "Admin")
#### Get users
```javascript
var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer {token}");

var requestOptions = {
  method: 'GET',
  headers: myHeaders,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Admin/user", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```
##### Output 
```json
{
  "Value": [
    {
      "Id": 1,
      "UserName": "minh",
      "UserEmail": "minh@gmail.com",
      "Role": "Admin",
      "Carts": []
    },
    {
      "Id": 2,
      "UserName": "minh2",
      "UserEmail": "minh@gmail.com",
      "Role": "Customer",
      "Carts": [
        {
          "Id": 1,
          "UserId": 2,
          "TotalPrice": 66.9,
          "Products": [
            {
              "Id": 2,
              "Title": "Mens Casual Premium Slim Fit T-Shirts ",
              "Price": 22.3,
              "Image": "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
              "Quantity": 3
            }
          ]
        },
        {
          "Id": 2,
          "UserId": 2,
          "TotalPrice": 396.75,
          "Products": [
            {
              "Id": 1,
              "Title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
              "Price": 109.95,
              "Image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
              "Quantity": 3
            },
            {
              "Id": 2,
              "Title": "Mens Casual Premium Slim Fit T-Shirts ",
              "Price": 22.3,
              "Image": "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
              "Quantity": 3
            }
          ]
        },
        {
          "Id": 3,
          "UserId": 2,
          "TotalPrice": 329.85,
          "Products": [
            {
              "Id": 1,
              "Title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
              "Price": 109.95,
              "Image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
              "Quantity": 3
            }
          ]
        }
      ]
    },
    {
      "Id": 3,
      "UserName": "minh3",
      "UserEmail": "minh@gmail.com",
      "Role": "Customer",
      "Carts": [
        {
          "Id": 4,
          "UserId": 3,
          "TotalPrice": 342.79,
          "Products": [
            {
              "Id": 1,
              "Title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
              "Price": 109.95,
              "Image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
              "Quantity": 2
            },
            {
              "Id": 2,
              "Title": "Mens Casual Premium Slim Fit T-Shirts ",
              "Price": 22.3,
              "Image": "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
              "Quantity": 3
            },
            {
              "Id": 3,
              "Title": "Mens Cotton Jacket",
              "Price": 55.99,
              "Image": "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg",
              "Quantity": 1
            }
          ]
        },
        {
          "Id": 5,
          "UserId": 3,
          "TotalPrice": 298.19,
          "Products": [
            {
              "Id": 1,
              "Title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
              "Price": 109.95,
              "Image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
              "Quantity": 2
            },
            {
              "Id": 2,
              "Title": "Mens Casual Premium Slim Fit T-Shirts ",
              "Price": 22.3,
              "Image": "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
              "Quantity": 1
            },
            {
              "Id": 3,
              "Title": "Mens Cotton Jacket",
              "Price": 55.99,
              "Image": "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg",
              "Quantity": 1
            }
          ]
        }
      ]
    }
  ]
}
```
#### Get Single User

```javascript
var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer {token}");

var requestOptions = {
  method: 'GET',
  headers: myHeaders,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Admin/user/2", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
```

##### Output
```json
{
  "Value": {
    "Id": 2,
    "UserName": "minh2",
    "UserEmail": "minh@gmail.com",
    "Role": "Customer",
    "Carts": [
      {
        "Id": 1,
        "UserId": 2,
        "TotalPrice": 66.9,
        "Products": [
          {
            "Id": 2,
            "Title": "Mens Casual Premium Slim Fit T-Shirts ",
            "Price": 22.3,
            "Image": "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
            "Quantity": 3
          }
        ]
      },
      {
        "Id": 2,
        "UserId": 2,
        "TotalPrice": 396.75,
        "Products": [
          {
            "Id": 1,
            "Title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
            "Price": 109.95,
            "Image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
            "Quantity": 3
          },
          {
            "Id": 2,
            "Title": "Mens Casual Premium Slim Fit T-Shirts ",
            "Price": 22.3,
            "Image": "https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg",
            "Quantity": 3
          }
        ]
      },
      {
        "Id": 3,
        "UserId": 2,
        "TotalPrice": 329.85,
        "Products": [
          {
            "Id": 1,
            "Title": "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
            "Price": 109.95,
            "Image": "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
            "Quantity": 3
          }
        ]
      }
    ]
  }
}
```
#### Post Category
```javascript
var myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");
myHeaders.append("Authorization", "Bearer {token}");

var raw = JSON.stringify({
  "name": "string",
  "products": [
    {
      "title": "string",
      "price": 0,
      "description": "string",
      "categoryID": 0,
      "image": "string",
      "rating": {
        "rate": 0,
        "count": 0
      }
    }
  ]
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Admin/categories", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));```
  
#### Post Product
Note: The 'categoryID' must match the 'categoryId' in the database
```javascript
var myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");
myHeaders.append("Authorization", "Bearer {token}");

var raw = JSON.stringify({
  "title": "string",
  "price": 0,
  "description": "string",
  "categoryID": 0,
  "image": "string",
  "rating": {
    "rate": 0,
    "count": 0
  }
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Admin/product", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
  
```

  
#### Delete Product
```javascript
var myHeaders = new Headers();
myHeaders.append("Authorization", "Bearer {token}");

var requestOptions = {
  method: 'DELETE',
  headers: myHeaders,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Admin/product/3", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
   ```
 
#### Adjust Product
Note: The 'categoryID' must match the 'categoryId' in the database
 ```javascript
var myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");
myHeaders.append("Authorization", "Bearer {token}");

var raw = JSON.stringify({
  "title": "string",
  "price": 0,
  "description": "string",
  "image": "string",
  "categoryID": 2
});

var requestOptions = {
  method: 'PUT',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://localhost:7204/api/Admin/product/3", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error)); 
 ```