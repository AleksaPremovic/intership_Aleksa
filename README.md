# 🎓 Internship Project – Student-Course API

This repository contains a guided internship project developed by **Aleksa Premović** as part of his high school practice, under the mentorship of **Davor Pajić**.

The goal of this internship is to provide practical hands-on experience in developing a service-oriented .NET API with clean architecture, database integration, and optional UI extension.

---

## 🧠 Tech Stack

- **Backend:** ASP.NET Core (.NET 8)
- **Architecture:** Clean separation (API / Domain / Data)
- **Database:** MSSQL (EF Core code-first)
- **ORM:** Entity Framework Core 8
- **API Documentation:** Swagger + JWT Auth support
- **Dev Tools:** JetBrains Rider / Visual Studio 2022+, GitHub

---

## 🧩 Project Structure

```
📦 Internship_Aleksa
├── 📁 Internship_Aleksa.API        → Main Web API (Startup project)
├── 📁 Internship_Aleksa.Domain     → Domain models and interfaces
├── 📁 Internship_Aleksa.Data       → EF Core context, repositories
└── 📄 Internship_Aleksa.sln        → Solution file
```

---

## 🎯 Internship Objective

Aleksa will learn and implement:

- How to design and build a real-world API
- CRUD operations and model validation
- Entity Framework Core & migrations
- Service-repository pattern with dependency injection
- Working with GitHub and commit flow
- Optional: connecting frontend (React)

---

## 🛠️ How to Run the Project

> Prerequisites: [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

1. Clone the repo:

```bash
git clone https://github.com/YOUR_USERNAME/intership_aleksa.git
cd intership_aleksa
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Apply database migrations:

```bash
dotnet ef database update --project ./Internship_Aleksa.Data --startup-project ./Internship_Aleksa.API
```

4. Run the API:

```bash
dotnet run --project ./Internship_Aleksa.API
```

5. Open Swagger UI:

[https://localhost:7231/swagger](https://localhost:7231/swagger)

---

## 🔐 JWT Authorization (Swagger)

This project includes JWT support in Swagger. To test protected endpoints:

- Add a token to `Authorize` button in Swagger
- Use format: `Bearer YOUR_TOKEN_HERE`

---

## 📚 API Entities & Endpoints

### 🧍 Student
| Field       | Type     |
|-------------|----------|
| Id          | int      |
| FirstName   | string   |
| LastName    | string   |
| Email       | string   |
| BirthDate   | DateTime|

### 📘 Course
| Field       | Type     |
|-------------|----------|
| Id          | int      |
| Name        | string   |
| Description | string   |
| StartDate   | DateTime|
| EndDate     | DateTime|

### 🔁 Enrollment (Join Table)
| Field         | Type     |
|---------------|----------|
| StudentId     | int      |
| CourseId      | int      |
| EnrollmentDate| DateTime|

### 🛠️ Core Endpoints:

#### Students
- `GET /api/students`
- `GET /api/students/{id}`
- `POST /api/students`
- `PUT /api/students/{id}`
- `DELETE /api/students/{id}`

#### Courses
- `GET /api/courses`
- `GET /api/courses/{id}`
- `POST /api/courses`
- `PUT /api/courses/{id}`
- `DELETE /api/courses/{id}`

#### Enrollments
- `POST /api/enrollments`
- `GET /api/students/{id}/courses`
- `GET /api/courses/{id}/students`

---

## 🧪 Optional Tasks (Stretch Goals)

- ✅ Pagination and filtering
- ✅ Basic React frontend with Axios (optional)
- ✅ Basic authentication & authorization

---

## 👨‍🏫 Mentor

**Davor Pajić**  
Senior Full Stack Engineer  
.NET / React / MSSQL / AWS / Clean Architecture  
[GitHub](https://github.com/dpajic-midenas)

---

## 📘 License

This project is developed for educational purposes.  
Feel free to fork and build upon it!
