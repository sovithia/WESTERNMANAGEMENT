import { HttpContextContract } from "@ioc:Adonis/Core/HttpContext";
import {RESULTKEY,DATAKEY,SUCCESSRESPONSE,FAILURERESPONSE, MESSAGEKEY} from 'App/Controllers/Constants'
import User from "App/Models/User";

export default class AuthController {
  public async login({ request, auth }: HttpContextContract) {
    const username = request.input("username");
    const password = request.input("password");
    var response = {}
    console.log(username)
    console.log("Login Attempt")
    try{
      const token = await auth.use("api").attempt(username, password, {
        expiresIn: "10 days",
      });
      const user = await User.query()
          .where('username',username)
          .first()  
      response[RESULTKEY] = SUCCESSRESPONSE
      response[DATAKEY] = {"user" : user,"token" : token}            
      console.log("Login Success")
      return response;
    }catch{
      response[RESULTKEY] = FAILURERESPONSE
      response[MESSAGEKEY] = "Invalid credentials"
      console.log("Login Failed")
      return response;
    }
    
  }

  // ADMIN, ORDERNAMAGER, ACCOUNTANTMANAGER, ACCOUNTANT,FABRICDEALER,SCHOOLPURCHASER,CLOTHEFACTORY
  public async register({ request, auth }: HttpContextContract) {
    const email = request.input("email");
    const password = request.input("password");
    const username = request.input("username");
    const firstname = request.input("firstname");
    const lastname = request.input("lastname");
    const phone = request.input("phone");
    const role = request.input("role");


    const newUser = new User();
    newUser.email = email;
    newUser.password = password;
    newUser.username = username;
    newUser.firstname = firstname;
    newUser.lastname = lastname;
    newUser.phone = phone;
    newUser.role = role;
    await newUser.save();
    const token = await auth.use("api").login(newUser, {
      expiresIn: "10 days",
    });
    return token.toJSON();
  }
}