🛒 E-Commerce System

Факултетен номер: 2401321004

Име:Радостин Шаламанов

Курс:Разпределени приложения

📌 Описание:

Системата представлява уеб-базирана платформа за електронна търговия, разработена с 5-слойна архитектура. 

Позволява управление на продукти, категории, поръчки и потребители чрез REST API и MVC клиент.

Технологии: ASP.NET Core 8 · Entity Framework Core · SQL Server · Bootstrap · JWT

🏗️ Архитектура

ECommerce/

├── E-Commerce.API          → REST API (Controllers, Swagger, JWT)

├── E-Commerce.Services     → Бизнес логика (Services, DTOs)

├── E-Commerce.Repository   → Достъп до данни (Repositories)

├── E-Commerce.Data         → Модели и DbContext (Entities, Migrations)

└── E-Commerce.FrontEnd     → MVC клиент (Views, Controllers)

✅ Функционалности

📦 CRUD за Продукти с филтриране по име, цена и категория

🗂️ CRUD за Категории

🛍️ CRUD за Поръчки с автоматично изчисляване на обща цена

👤 CRUD за Потребители

🔍 Филтриране, сортиране и странициране за всички ентитита

🔐 JWT автентикация и авторизация

📄 Swagger документация


⚙️ Инсталация и стартиране

Изисквания

.NET 8 SDK

SQL Server

Visual Studio 2022

Стъпки

1. Клонирай репото

bashgit clone https://github.com/RadostinShalamanov/distributed-applications-se.git

cd distributed-applications-se/course-work/implementations/ECommerce

2. Настрой connection string в E-Commerce.API/appsettings.json

json"ConnectionStrings": {

  "DefaultConnection": "Server=.;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True"

}


3. Приложи миграциите

bashdotnet ef database update --project E-Commerce.Data --startup-project E-Commerce.API

4. Стартирай API-то

bashcd E-Commerce.API

dotnet run

5. Настрой API URL в E-Commerce.FrontEnd/appsettings.json

json"ApiSettings": {

  "BaseUrl": "https://localhost:7087"

}


6. Стартирай Frontend-а

bashcd E-Commerce.FrontEnd

dotnet run

7. Отвори браузър

Приложение URL Frontend: https://localhost:7001 

Swaggerhttps://localhost:7087/swagger



🗄️ База данни

ТаблицаОписаниеUsersПотребители на систематаCategoriesКатегории на продуктитеProductsПродуктиOrdersПоръчкиOrderItemsПродукти в поръчка
