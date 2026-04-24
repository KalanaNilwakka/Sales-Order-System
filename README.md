# 📦 Sales Order Management System

## 📖 Overview
This is a full-stack Sales Order Management System designed to streamline order processing, customer management, and item tracking. The application provides a modern UI with efficient backend services, enabling users to create, manage, and monitor sales orders seamlessly.

---

## ✨ Features

### Core Features
- Item Management with pricing and tax handling
- Sales Order Creation with multiple order items
- Automatic calculation of:
    - Exclusive Amount
    - Tax Amount
    - Inclusive Amount
- PDF generation for orders

### Technical Features
- RESTful API architecture
- State management using Redux Toolkit
- Form handling with validation
- Clean architecture with separation of concerns
- Optimized database interactions using Entity Framework

---

## 🛠 Tech Stack

### Backend
- ASP.NET Core (.NET 8)
- C#
- Entity Framework Core
- SQL Server
- QuestPDF (for PDF generation)

### Frontend
- Next.js 16
- TypeScript
- Tailwind CSS
- Redux Toolkit
- React Hook Form

---

## 📂 Project Structure

```
root/
│
├── backend/
│   ├── Controllers/
│   ├── Services/
│   ├── Repositories/
│   ├── Models/
│   ├── DTOs/
│   ├── Data/
│   ├── Mappings/
│   └── Program.cs
│
├── frontend/
│   ├── app/
│   ├── components/
│   ├── redux/
│   ├── hooks/
│   ├── services/
│   └── styles/
│
└── README.md
```

### Architecture Layers
- Presentation Layer – Controllers / UI
- Application Layer – Services, DTOs
- Domain Layer – Models
- Infrastructure Layer – Database, Repositories

---

## ⚙️ Prerequisites

Make sure you have the following installed:

- .NET SDK 8.0+
- Node.js 18+
- SQL Server
- Git

---

## 🚀 Installation

### 1. Clone the Repository
```bash
git clone https://github.com/KalanaNilwakka/Sales-Order-System
```

### 2. Backend Setup
```bash
cd Sales-Order-System-Backend
dotnet restore
```

Update database:
```bash
dotnet ef database update
```

Run backend:
```bash
dotnet run
```

### 3. Frontend Setup
```bash
cd Sales-Order-System-frontend
npm install
```

Run frontend:
```bash
npm run dev
```

---

## 🔧 Configuration

### Backend (appsettings.json)
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SalesOrderDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Frontend (.env.local)
```env
NEXT_PUBLIC_API_URL=http://localhost:5000/api
```

---

## ▶️ Running the Application

### Development Mode

Backend:
```bash
dotnet run
```

Frontend:
```bash
npm run dev
```

### Production Build

Frontend:
```bash
npm run build
npm start
```

Backend:
```bash
dotnet publish -c Release
```

---

## 📡 API Documentation

Swagger UI is available at:
```
http://localhost:5000/swagger
```

### Sample Endpoints
- GET /api/customers
- POST /api/orders
- GET /api/items
- DELETE /api/orders/{id}

---

## 🧑‍💻 Development

### Scripts

Frontend:
```bash
npm run dev
npm run build
npm run lint
```

Backend:
```bash
dotnet build
dotnet run
```

### Best Practices
- Use DTOs for data transfer
- Keep controllers thin (business logic in services)
- Use AutoMapper for mapping
- Validate forms using React Hook Form
- Maintain consistent folder structure

---

## 🛠 Troubleshooting

### Common Issues

**Database Connection Error**
- Check SQL Server is running
- Verify connection string

**CORS Issues**
- Ensure backend allows frontend origin

**Migration Issues**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Node Modules Issues**
```bash
rm -rf node_modules
npm install
```

---

## 📊 Code Statistics

- TypeScript – 85.5%
- C# – 11.1%
- CSS – 3.3%

---

## 📌 Notes
- Ensure backend runs before frontend
- Environment variables must match API URL
- Use proper Git branching strategy (feature-based)
