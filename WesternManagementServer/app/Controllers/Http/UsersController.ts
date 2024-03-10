 import type { HttpContextContract } from '@ioc:Adonis/Core/HttpContext'
 import User from 'App/Models/User'
 import {DATAKEY,RESULTKEY,SUCCESSRESPONSE} from 'app/Controllers/Constants'

export default class UsersController {


  public async index({}: HttpContextContract) {
    var users = await User.all()
        var response = {}
        response[DATAKEY] = users
        response[RESULTKEY] = SUCCESSRESPONSE
        return response
  }

  public async create({ request }: HttpContextContract) {
    var user = new User();
        user.email = request.input('email');
        user.password = request.input('password');
        user.save()
        var response = {}
        response[RESULTKEY] = SUCCESSRESPONSE
        return response
  }

  public async update({ request, params }: HttpContextContract){
    var user = await User.find(params.id)
    if (user != null){        
        user.email = request.input('email')
        user.firstname = request.input('address')
        user.lastname = request.input('phone')        
        user.save()
    }
    var response = {}
    response[RESULTKEY] = SUCCESSRESPONSE
    return response
  }
}
