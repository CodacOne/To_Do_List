# 📝 ToDoList Web API

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![C#](https://img.shields.io/badge/C%23-8.0-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

---

## 📋 نبذة عن المشروع

مشروع **ToDoList Web API** هو خدمة RESTful مبنية باستخدام ASP.NET Core 8.0، تهدف إلى إدارة مهام المستخدمين (ToDo Tasks) بطريقة بسيطة وفعالة. يدعم المشروع العمليات الأساسية مثل:

- إنشاء، تحديث، حذف، واستعراض المهام.
- إدارة المستخدمين وصلاحياتهم.
- المصادقة والتفويض باستخدام JWT (JSON Web Tokens).
- البحث والتصفية والفرز للمهام.
- وثائق API تلقائية باستخدام Swagger.

---

## 🚀 المميزات الرئيسية

- تصميم نظيف باستخدام Clean Architecture مع فصل الطبقات: Application, Domain, Infrastructure, WebAPI.
- دعم JWT Authentication و Role-Based Access Control (RBAC).
- استخدام Entity Framework Core مع SQL Server لإدارة البيانات.
- تكامل AutoMapper لتسهيل تحويل البيانات بين الكائنات.
- توثيق API احترافي باستخدام Swagger UI.
- Logging مخصص للطلبات والأخطاء.
- Middleware مخصص لمعالجة الطلبات.

---

## 📂 هيكل المشروع


/ToDoList
├── TodoApp.Application # منطق الأعمال والخدمات
├── TodoApp.Domain # الكيانات والنماذج
├── TodoApp.Infrastructure # الوصول إلى البيانات وEF Core
└── To_Do_List # Web API والواجهة الخارجية


---

## 🛠️ المتطلبات والتثبيت

### المتطلبات

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server (أو أي قاعدة بيانات تدعم EF Core)
- Visual Studio 2022 أو VS Code

### خطوات التثبيت والتشغيل محلياً

1. استنساخ المستودع:
   ```bash
   git clone https://github.com/yourusername/ToDoList.git
   cd ToDoList

إعداد الاتصال بقاعدة البيانات في ملف appsettings.json:

"ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ToDoListDB;Trusted_Connection=True;"
}

تنفيذ ترحيلات EF Core (Migrations) لإنشاء قاعدة البيانات:

cd To_Do_List
dotnet ef database update

تشغيل المشروع:

dotnet run

فتح متصفح والانتقال إلى:

    https://localhost:5001/swagger

    لتصفح وثائق API والتجربة.

🔐 المصادقة (Authentication)

    يستخدم المشروع JWT للتوثيق.

    يجب إرسال التوكن في ترويسة Authorization على الشكل:

Authorization: Bearer YOUR_JWT_TOKEN
