/*
|--------------------------------------------------------------------------
| Routes
|--------------------------------------------------------------------------
|
| This file is dedicated for defining HTTP routes. A single file is enough
| for majority of projects, however you can define routes in different
| files and just make sure to import them inside this file. For example
|
| Define routes in following two files
| ├── start/routes/cart.ts
| ├── start/routes/customer.ts
|
| and then import them inside `start/routes.ts` as follows
|
| import './routes/cart'
| import './routes/customer'
|
*/

import Route from '@ioc:Adonis/Core/Route'

Route.group(() => {
  Route.get("/resources","ResourcesController.index")
  Route.post("/login", "AuthController.login")
  Route.post('/user',"UsersController.create")
  Route.group(() => {  

    Route.get("/user","UsersController.index")
    Route.post("/user","UsersController.create")
    Route.put("/user","UsersController.update")

    Route.get("/invoice","InvoicesController.index")
    Route.post("/invoice","InvoicesController.create")
    Route.put("/invoice","InvoicesController.update")




  }).middleware("auth:api");
}).prefix("api");
