# KaelStore

Project KaelStore Test

## Installation
Untuk Database menggunakan SQLite. Lokasi default database di C:\\Haulio.FarmFresh.db dapat dilihat di appSettings.json


Untuk inisiasi pertama, run command pada Package Manager Console dan pilih Default Project: KaelStore.Persistence
```bash
PM> Add-Migration Initial-Commit-Application -Context ApplicationDbContext -o Migrations/Application
PM> Add-Migration Initial-Commit-Identity -Context IdentityContext -o Migrations/Identity

PM> Update-Databasee -Context ApplicationDbContext
PM> Update-Databasee -Context IdentityContext 
```

## Usage

Untuk setiap HTTP Method PUT, DELETE dan POST pada route, diperlukan header Authorization Bearer JwtToken.
Gunakan account berikut :
```
[Post] /api/Account/authenticate
{
  "email": "superadmin@gmail.com",
  "password": "Password@123"
}
```
#### Result
```
{
  "status": "Success",
  "data": {
    "id": "34bb7bea-dcfb-404c-8405-ea7e4ed68a2d",
    "userName": "superadmin",
    "email": "superadmin@gmail.com",
    "roles": [
      "Admin",
      "Basic",
      "Moderator",
      "SuperAdmin"
    ],
    "isVerified": true,
    "jwToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdXBlcmFkbWluIiwianRpIjoiYzlkNGU1OWYtYTY1YS00YzgxLTk4NDMtOTVjNTBjOTgxNzU0IiwiZW1haWwiOiJzdXBlcmFkbWluQGdtYWlsLmNvbSIsInVpZCI6IjM0YmI3YmVhLWRjZmItNDA0Yy04NDA1LWVhN2U0ZWQ2OGEyZCIsImlwIjoiMTkyLjE2OC4xLjYiLCJyb2xlcyI6WyJBZG1pbiIsIkJhc2ljIiwiTW9kZXJhdG9yIiwiU3VwZXJBZG1pbiJdLCJleHAiOjE2NTg5NTY5MjQsImlzcyI6IklkZW50aXR5IiwiYXVkIjoiSWRlbnRpdHlVc2VyIn0.s2391rems_Co4O3QtJbNfqiEoM378suRYGmzWxdI_9c",
    "refreshToken": "91C4DF5EDFE8497BB5970AF014031989DCFF86AA3520B0BF40A5A910714D1E1C65C8E93E6D5A3E99"
  },
  "message": "Authenticated superadmin"
}
```

### 1. Products
```
# Get Products
# Nilai default untuk page = 1 dan limit = 5 jika query param tidak dicantumkan
[Get] /api/v1/product?page=1&limit=10

# Get Product by Id
[Get] /api/v1/product/{id}

# Add Product
[Post] /api/v1/product
# body
{
  "name": "string",
  "categoryId": 1,
  "strategy": "string",
  "price": 100000,
  "description": "string"
}

# Update Product By Id
[Put] /api/v1/product/{id}
# body
{
  "name": "string",
  "categoryId": 0,
  "strategy": "string",
  "price": 0,
  "description": "string"
}

# Delete Product By Id
# tidak ada response body untuk delete melainkan hanya http status code
[Delete] /api/v1/product/{id}
```

### 2. Customers
```
# Get Customers
# Nilai default untuk page = 1 dan limit = 5 jika query param tidak dicantumkan
[Get] /api/v1/customer?page=1&limit=5

# Get Customer By Id
[Get] /api/v1/customer/{id}

# Add Customer
[Post] /api/v1/customer
# body
{
  "customerName": "string",
  "contactName": "string",
  "contactTitle": "string",
  "address": "string",
  "city": "string",
  "region": "string",
  "postalCode": "string",
  "country": "string",
  "phone": "string",
  "fax": "string"
}

# Update Customer By Id
# [Put] /api/v1/customer/{id}
# body
{
  "customerName": "string",
  "contactName": "string",
  "contactTitle": "string",
  "address": "string",
  "city": "string",
  "region": "string",
  "postalCode": "string",
  "country": "string",
  "phone": "string",
  "fax": "string"
}

# Delete Customer By Id
# tidak ada response body untuk delete melainkan hanya http status code
# [Delete] /api/v1/customer/{id}
```

### 3. Orders
#### Add
```
[Post] /api/v1/order
{
  "customerId": 0,
  "orderDetails": [
    {
      "productId": 0,
      "quantity": 0
    }
  ]
}
```
#### Read All
```
[Get] /api/v1/order?page=1&limit=5
```

### 4. Product Menus
#### Get All
```
[Get] /api/v1/productmenu
```

#### Add Menu
```
[Post] /api/v1/productmenu
{
  "position": 0,
  "displayText": "string",
  "productIds": [
    0
  ]
}
```

#### Add Product to Existing Menu

Add 1 or more Products to existing Menu by passing array of Product Id
```
[Post] /api/v1/productmenu/{menuId}/product
[int, int, ...int]
```