# NekretnineApp

Real estate management application - Agent module.

The app for managing real-estates for sale or rent, as well as their advertisement.
Agents are also able to manage payements and contracts with various customers.
They can arrange date and time, when they are gonna see the real estate object in person with a customer.

Customer controller now just has a form which demonstrates these arrangement. When a customer fills the form, Agent will get it in the form of notification.

Notification system implemented using SignalR Core

Used C# with Entity Framework Core and Asp.Net Core MVC. I did this with CODE FIRST principle, so before running the app, do the migrations.

Note: This is a school project

Add connection string to appsettings.json file, and reference it in Startup.cs (ConfigureServices) like this:

```
services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("insertConnectionString")));
```
