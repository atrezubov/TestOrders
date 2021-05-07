# C# TestOrders
 ASP.NET CORE Демонстрация.

Приложение ASP.NET CORE 5.

Очень простой функционал - создание и редактирование заказчиков и заказов.

Используется Entity Framework 6, база данных SQL Server создается автоматически в соответствие со строкой подключения в appsetiings.json.
Три слоя: Data Layer, Business Layer, Presentation Layer.
Контекст базы данных и интерфейсы бизнес слоя внедряются как зависимости.
Presentation Layer построен как Web Api, непосредственно пользовательский интерфейс - DevExtreme dxDatagrid с двумя уровнями детализации (см. Views/Home/Index).
Код снабжен комментариями.

Демонстрация:

https://megazoob.com/testorders
