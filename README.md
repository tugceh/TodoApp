# ASP.NET Core & EntityFramework Core Based TodoList Web Application
This project is a simple todolist web application api using ASP.NET Core and EntityFramework Core. It does not include frontend. You can test backend in swagger.

# Prerequirements
  - Visual Studio 2019
  - .Net Core Sdk
  - MsSql Server (for Mac -> you can create mssql server in docker and kitematic and can add database in Azure Data Studio)

# How To Run
  - Open solution in Visual Studio
  - Set todoApp project as Startup Project and build the project.
  - Run the application. Swagger is opened(swagger path is /swagger/index.html)

# How To Test
 This project has two controllers which is named Register and TodoList and they have get, add and delete end points.
 # Register Test
    - Firstly, please test listUsers end point and select one user in users and copy Id in somewhere.
    - AddUser end point: Enter Name, Email and Password informations in request body and click try it out and Execute button. You can check if it is successful in listUsers end point.
    - DeleteUser end point: Enter user id which is coppied. Click try it out and Execute button. You can check if it is successful in listUsers end point.
 # TodoList Test
    - AddTodoList end point: Enter Name, Time and Userid informations in request body and click try it out and Execute button.You can check if it is successful in listAllTodos end point.
    - ListTodos end point: Enter UserId which is coppied.
    - ListAllTodos end point: You can list all user's todos.
    - DeleteTodo end point: Enter todo id which you want to delete todo. Click try it out and Execute button. You can check if it is successful in listAllTodos end point.
