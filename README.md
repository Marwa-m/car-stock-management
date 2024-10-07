Car Dealer Stock Management API
===============================

This is a web API built with C# for managing car stocks for multiple dealers. Each dealer can manage their own car inventory, while the system ensures that they cannot access or modify other dealers' cars or stock levels.

Features
--------

-   **Add Car**: Allows dealers to add a new car to their stock.
-   **Remove Car**: Dealers can remove cars from their stock.
-   **List Cars**: Dealers can view all cars they have in stock.
-   **Stock Level for specific car**: Dealers can view the stock level of a specific car.
-   **Update Car Stock Level**: Dealers can update the stock level of a specific car.
-   **Search Car**: Search for cars by make and model.
-   **Authentication**: JWT-based authentication ensures dealers can only manage their own cars.

Technologies Used
-----------------

-   **C#**
-   **ASP.NET Core**
-   **SQLite** (Database)
-   **Dapper** (For database operations)
-   **JWT Authentication**
-   **Validation with FluentValidation**

Getting Started
---------------

### Setting Up

1.  Clone the repository:
   ```bash
    git clone https://github.com/Marwa-m/car-stock-management.git
```

3.  Restore the .NET project dependencies:

  ```bash
    dotnet restore
   ```
3.  Build the project:

   ```bash
   dotnet build
   ```

4.  Run the API:

   ```bash
   dotnet run
   ```

### Database

The API uses SQLite for data storage. The database file is stored locally and will be created when the API is first run. I already pre-load the database with some test data for ease of use.

To simplify testing, the following dealers are already seeded in the database:

-   **Name:** Marwa\
    **Password:** password

-   **Name:** Sari\
    **Password:** password

You can use these dealers to authenticate and interact with the API or you can create a new one.

### Endpoints

#### Dealer

-   **POST** `/api/dealer/register`
    -   Register using (`Name`, `Email` and `Password`).
-   **POST** `/api/dealer/signin`
    -   sign in using (`Name` or `Email` and `Password`).

#### Car Management

-   **GET** `/api/cars`

    -   Retrieve all cars for the authenticated dealer.
-   **GET** `/api/cars/search?make={make}&model={model}`

    -   Search for a car by make and model.
-   **GET** `/api/cars/get-stock-level/{id}`

    -   Get the stock level for a car by its ID.
-   **POST** `/api/cars/add`

    -   Add a new car to the dealer's inventory.
    -   **Body:**

        json
```

        {
          "make": "Toyota",
          "model": "Corolla",
          "year": 2020,
          "stockLevel": 10
        }
```

-   **PUT** `/api/cars/update-stock-level`

    -   Update the stock level of an existing car.

-   **DELETE** `/api/cars/{id}`

    -   Delete a car from the dealer's inventory by its ID.

### Authentication & Authorization

The API uses JWT tokens for authentication. Each dealer must log in using their credentials to obtain a JWT token. The token must then be included in the `Authorization` header of all subsequent requests.

Example:

```
`Authorization: Bearer <your-jwt-token>`
```

### Error Handling & Validation

-   **Input Validation**: All endpoints validate the incoming data using FluentValidation.
-   **Error Handling**: Proper error messages are returned for invalid requests, unauthorized access, or any data-related issues (e.g., trying to modify another dealer's car).

## Seed Data

### Dealers Table


| ID | Name | Email | Password |
| --- | --- | ---| --- |
| 1   | Marwa | marwa@example.com   | password   |
| 2   | Sari  | sari@example.com    | password   |


### Cars Table
| ID | Make | Model | Year | StockLevel | DealerID |
| --- | --- | ---| --- | ---| --- |
| 1   | Toyota | Corolla| 2021 | 10  | 1|
| 2  | Honda | Civic | 2020 | 8           | 1         |
| 3   | Ford      | Focus      | 2019 | 5           | 1         |
| 4   | Tesla     | Model 3    | 2022 | 7           | 1         |
| 5   | BMW       | X5         | 2021 | 6           | 2         |
| 6   | Audi      | A4         | 2020 | 4           | 2         |
| 7   | Mercedes  | C-Class    | 2021 | 3           | 2         |
| 8   | Hyundai   | Elantra    | 2019 | 9           | 2         |
