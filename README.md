# 🎓 Student Management System (ASP.NET Core API)

A **Student Management System** built with **ASP.NET Core Web API** following clean backend principles. The project focuses on **real-world backend architecture**, **authentication & authorization**, and **relational database design**.

This project was implemented as a **learning-by-building** experience, covering everything from database modeling to secure API development.

---

## 🚀 Features

* 🔐 **Authentication & Authorization** using JWT
* 👥 **Role-Based Access Control** (Admin / Teacher / Student)
* 🧑‍🏫 Teacher management
* 🧑‍🎓 Student profiles linked to Identity users
* 📚 Course management
* 🏫 Class management
* 📝 Attendance tracking system
* 📊 Grades system
* 🔗 Many-to-Many relationships (Students ↔ Courses)
* 🧠 Clean database schema with constraints & indexes

---

## 🛠️ Tech Stack

* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **JWT Authentication**
* **ASP.NET Core Identity**
* **LINQ**
* **RESTful API principles**

---

## 🧱 Project Architecture

The project follows a **layered architecture**:

* **Core / Domain Layer**
  Contains entities such as `Student`, `Teacher`, `Course`, `Attendance`, `Grade`, and business rules.

* **Data Layer**
  Handles database context, entity configurations, and migrations using Entity Framework Core.

* **API Layer**
  Exposes REST endpoints, handles authentication, authorization, and request validation.

---

## 📚 Main Modules

### 👤 Users & Identity

* ASP.NET Identity used for authentication
* Custom profiles for **Student** and **Teacher** linked to `ApplicationUser`
* One-to-One relationship between User and Profile

### 🧑‍🎓 Students

* Linked to Identity user
* Assigned to a class
* Enrolled in multiple courses
* Has grades and attendance records

### 🧑‍🏫 Teachers

* Linked to Identity user
* Can teach multiple courses
* Can be assigned to classes

### 📘 Courses

* Assigned to a teacher
* Many-to-Many with students
* Contains grades and attendance records

### 📝 Attendance System

* Tracks student attendance per course and date
* Unique constraint on (StudentId, CourseId, Date)
* Supports multiple attendance statuses (Present / Absent / etc.)

### 📊 Grades

* Stores student grades per course
* Linked to both Student and Course

---

## 🗄️ Database Design Highlights

* Proper use of **Primary Keys & Foreign Keys**
* Cascade / Restrict delete behaviors handled carefully
* Indexes added for performance
* Avoided multiple cascade paths in SQL Server

---

## 🔐 Security

* JWT-based authentication
* Role-based authorization using `[Authorize(Roles = "...")]`
* Secure endpoints based on user roles

---

## 📌 What I Learned

* Designing a real-world relational database
* Handling complex relationships in EF Core
* Fixing cascade delete & migration issues
* Implementing JWT authentication without shortcuts
* Writing clean, maintainable backend code

---

## ▶️ How to Run the Project

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run database migrations
4. Start the API

```bash
dotnet ef database update
dotnet run
```

---

## 📈 Future Improvements

* API documentation using Swagger
* Pagination & filtering
* Unit testing
* Logging & exception handling middleware

---

## 🤝 Author

**Abdelrahman Mohammed**
.NET Backend Developer
