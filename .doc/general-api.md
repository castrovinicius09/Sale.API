[Back to README](../README.md)

## General API Definitions

### Pagination

Pagination is supported for list endpoints using the following query parameters:

- `_page`: Page number (default: 1)
- `_size`: Number of items per page (default: 10)

Example:
```
GET /sales?_page=2&_size=20
```

## Error Handling

The API uses conventional HTTP response codes to indicate the success or failure of an API request. In general:

- 2xx range indicate success
- 4xx range indicate an error that failed given the information provided (e.g., a required parameter was omitted, etc.)
- 5xx range indicate an error with our servers

### Error Response Format

```json
{
  "type": "string",
  "error": "string",
  "detail": "string"
}
```

- `type`: A machine-readable error type identifier
- `error`: A short, human-readable summary of the problem
- `detail`: A human-readable explanation specific to this occurrence of the problem

Example error responses:

1. Resource Not Found
```json
{
  "type": "ResourceNotFound",
  "error": "Product not found",
  "detail": "The product with ID 12345 does not exist in our database"
}
```

2. Authentication Error
```json
{
  "type": "AuthenticationError",
  "error": "Invalid authentication token",
  "detail": "The provided authentication token has expired or is invalid"
}
```

3. Validation Error
```json
{
  "type": "ValidationError",
  "error": "Invalid input data",
  "detail": "The 'price' field must be a positive number"
}
```

For detailed error information, refer to the specific endpoint documentation.

<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./frameworks.md">Previous: Frameworks</a>
  <a href="./products-api.md">Next: Products API</a>
</div>