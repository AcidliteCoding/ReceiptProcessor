# Receipt Processor API (.NET 8)

This project is a take-home coding challenge that processes receipts and awards points based on specific rules. 
It's built with **ASP.NET Core 8**, runs entirely in memory (no database), and is fully Dockerized for easy deployment.

---

## Features

- RESTful API with two main endpoints
- In-memory data storage (no persistence required)
- Full support for Docker
- Swagger UI for easy testing

---

## Tech Stack

- .NET 8 (ASP.NET Core Web API)
- Docker (multi-stage build)
- Swagger (OpenAPI UI)

---

## API Endpoints

### GET /receipts/{id}/points
Returns the total number of points awarded for a processed receipt.

### POST `/receipts/process`

Accepts a JSON payload of a receipt and returns a unique `id`.

**Example Request:**

```json
{
  "retailer": "Target",
  "purchaseDate": "2022-01-01",
  "purchaseTime": "13:01",
  "items": [
    { "shortDescription": "Mountain Dew 12PK", "price": "6.49" },
    { "shortDescription": "Emils Cheese Pizza", "price": "12.25" }
  ],
  "total": "18.74"
}

