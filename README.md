# 🏨 Hotel Management System – Backend

A robust and scalable **Hotel Management System Backend** built using **ASP.NET Core Web API**. This project is designed following **Clean Architecture** principles to manage hotel operations efficiently, securely, and with high maintainability.

---

## 🚀 Features

* **User Authentication & Authorization:** Secure identity management using **JWT**.
* **Role-Based Access Control (RBAC):** Distinct permissions for **Admins** and **Users**.
* **Room Management:** Full CRUD operations for hotel rooms, categories, and pricing.
* **Booking System:** Logic for room availability tracking and reservations.
* **Orders & Services:** Management of guest requests and internal hotel orders.
* **Validation & Error Handling:** Clean API responses with global exception handling.

---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 8.0/9.0
* **ORM:** Entity Framework Core (EF Core)
* **Database:** SQL Server
* **Security:** JSON Web Tokens (JWT)
* **Querying:** LINQ
* **Design Patterns:** Repository Pattern, Dependency Injection, Clean Architecture.

---

## 🧱 Project structure

The solution is divided into layers to ensure separation of concerns:

* **Domain:** Entities, Enums, and Core logic.
* **Application:** DTOs, Mapping, Interfaces, and Business logic.
* **Infrastructure:** Data Access (EF Core), Migrations, and External Services.
* **API:** Controllers, Middlewares, and Configuration.

---

## ⚙️ Setup & Installation

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/Hammad4544/HotelAPI.git](https://github.com/Hammad4544/HotelAPI.git)
    ```

2.  **Configure `appsettings.json`:**
    Set your SQL Server connection string and JWT parameters:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=HotelDB;Trusted_Connection=True;"
    },
    "Jwt": {
      "Key": "Your_Very_Long_Secret_Key_Here",
      "Issuer": "HotelAPI",
      "Audience": "HotelUsers"
    }
    ```

3.  **Apply Database Migrations:**
    Run the following command in the Package Manager Console:
    ```powershell
    Update-Database
    ```

4.  **Run the Project:**
    ```bash
    dotnet run
    ```

---

## 📬 API Overview

| Method | Endpoint | Functionality | Access |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/register` | User Registration | Public |
| `POST` | `/api/auth/login` | Secure Login & Token Generation | Public |
| `GET` | `/api/rooms` | Fetch all available rooms | Authorized |
| `POST` | `/api/bookings` | Book a room | User |
| `PUT` | `/api/admin/rooms` | Update room details | Admin |

---

## 🎯 Future Enhancements

- [ ] **Payment Gateway:** Stripe or PayPal integration.
- [ ] **Real-time Notifications:** SignalR for booking updates.
- [ ] **Caching:** Redis for fast room searches.
- [ ] **Deployment:** Docker support & CI/CD Pipelines.
- [ ] **Documentation:** Comprehensive Swagger/OpenAPI UI.

---

## 👨‍💻 Author

**Ahmed Hammad**
*.NET Full Stack Developer*
*ITI Graduate*

---
