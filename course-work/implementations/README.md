E-Commerce System
Факултетен номер: 2401321004
Име: Радостин Шаламанов

Описание на проекта
Системата представлява уеб-базирана платформа за електронна търговия (E-Commerce), разработена с 5-слойна архитектура. Позволява управление на продукти, категории, поръчки и потребители чрез REST API и MVC клиент.
Технологии: ASP.NET Core 8, Entity Framework Core, SQL Server, Bootstrap

Структура
ECommerce/
├── E-Commerce.API          → REST API
├── E-Commerce.Services     → Бизнес логика
├── E-Commerce.Repository   → Достъп до данни
├── E-Commerce.Data         → Модели и DbContext
└── E-Commerce.FrontEnd     → MVC клиент

Инсталация и стартиране
Изисквания

.NET 8 SDK
SQL Server
Visual Studio 2022

Стъпки
1. Клонирай репото
bashgit clone https://github.com/RadostinShalamanov/distributed-applications-se.git
2. Настрой connection string в E-Commerce.API/appsettings.json:
json"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True"
}
3. Приложи миграциите
bashdotnet ef database update --project E-Commerce.Data --startup-project E-Commerce.API
4. Стартирай API-то
bashcd E-Commerce.API
dotnet run
5. Настрой API URL в E-Commerce.FrontEnd/appsettings.json:
json"ApiSettings": {
  "BaseUrl": "https://localhost:7087"
}
6. Стартирай Frontend-а
bashcd E-Commerce.FrontEnd
dotnet run
7. Отвори браузър

Frontend: https://localhost:7001
Swagger: https://localhost:7087/swagger


Функционалности

✅ CRUD за Продукти, Категории, Поръчки, Потребители
✅ Филтриране, сортиране и странициране
✅ JWT автентикация
✅ REST API с Swagger документация
✅ MVC Frontend с Bootstrap
