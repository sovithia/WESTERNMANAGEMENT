import { HttpContextContract } from '@ioc:Adonis/Core/HttpContext'
import fs from 'fs'

export default class ResourcesController {   

  public async index({ request,response }: HttpContextContract) {    
    var type = request.input('type')
    var id = request.input('id')
    //const image = fs.createReadStream(params.path)        
    const image = fs.createReadStream("./images/" + type + "/" + id + ".png")        
    return response.stream(image)
  }

  

}
