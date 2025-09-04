[Back to README](../README.md)

### Sales

#### GET /api/sales/{id}
- Description: Retrieve a sale by id
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "saleNumber": 0,
    "userName": "string",
    "branchName": "string",
    "branchFullAddress": "string",
    "totalItems": 0,
    "totalSaleAmount": 0,
    "cancelled": true,
    "createdAt": "2025-09-04T18:23:10.580Z",
    "updatedAt": "2025-09-04T18:23:10.580Z",
    "cancelledAt": "2025-09-04T18:23:10.580Z",
    "saleItems": [
      {
        "quantity": 0,
        "unitPrice": 0,
        "totalAmount": 0,
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "productName": "string"
      }
    ]
  }
  }
  ```

  #### GET api//PaginatedSale/{pageNumber}/{pageSize}
- Description: Retrieve a list of the paginated sale
- Query Parameters:
  - `_pageNumber` (optional): Page number for pagination (default: 1)
  - `_pageSize` (optional): Number of items per page (default: 1)
- Response: 
  ```json
  {
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "saleNumber": 0,
      "userName": "string",
      "branchName": "string",
      "branchFullAddress": "string",
      "totalItems": 0,
      "totalSaleAmount": 0,
      "cancelled": true,
      "createdAt": "2025-09-04T18:24:29.162Z",
      "updatedAt": "2025-09-04T18:24:29.162Z",
      "cancelledAt": "2025-09-04T18:24:29.162Z"
    }
  ],
  "currentPage": 0,
  "totalPages": 0,
  "totalCount": 0
  }
  ```

#### POST api/Sales
- Description: Add a new sale and items
- Request Body:
  ```json
  {
  "saleNumber": 0,
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userName": "string",
  "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "branchName": "string",
  "branchFullAddress": "string",
  "items": [
    {
      "quantity": 0,
      "unitPrice": 0,
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "productName": "string"
    }
  ]
  }
  ```
- Response: 
  ```json
  {
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
  ```

#### PATCH api/products
- Description: Update a specific sale and items
- Request Body:
  ```json
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "cancelled": true,
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userName": "string",
  "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "branchName": "string",
  "branchFullAddress": "string",
  "items": [
    {
      "quantity": 0,
      "unitPrice": 0,
      "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "productName": "string"
    }
  ]
  }
  ```
- Response: 
  ```json
  {
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ],
  "data": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
  ```

#### DELETE /products/{id}
- Description: Delete a specific sale and items
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
  "success": true,
  "message": "string",
  "errors": [
    {
      "error": "string",
      "detail": "string"
    }
  ]
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./general-api.md">Previous: General API</a>
</div>