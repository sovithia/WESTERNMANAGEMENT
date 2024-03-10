import type { HttpContextContract } from '@ioc:Adonis/Core/HttpContext'
import {DATAKEY,RESULTKEY,SUCCESSRESPONSE} from 'app/Controllers/Constants'
import Invoice from 'App/Models/Invoice'
import Utils from 'App/Controllers/Utils'
import fs from 'fs'

export default class InvoicesController {

	// partiallyvalidated,validated, waiting, rejected, approved
	public async index({params}: HttpContextContract) {
		var users
		if (params.type == "ALL"){
			users = await Invoice.all()
		}else if (params.type == "BOD"){
			users = await Invoice.query().where('amount','>',100).andWhere('status', 'waiting').exec()

		}
        var response = {}
        response[DATAKEY] = users
        response[RESULTKEY] = SUCCESSRESPONSE
        return response
	}

	public async create({params,request}: HttpContextContract) {
		var invoice = new Invoice()

		invoice.invoiceNumber = Utils.generateInvoiceNumber()
		invoice.amount = request.input('amount')		
		var invoice = await invoice.save()

		var medias = JSON.parse(request.input('medias'))
		for (var i = 0; i < medias.length; i++)
		{			
			var imgdata = request.input('image').replace(/^data:image\/\w+;base64,/, "");
        	var buf = Buffer.from(imgdata, 'base64');
        	fs.writeFile(__dirname + '/../../../images/medias/' + invoice.id + "_" +i + '.png',buf , function(err) {            
            console.log(err);
        	});	                  
			//Utils.sendPushNotificationToUser(id,"New Invoice","You have a new invoice")
		}

		var response = {}
		response[RESULTKEY] = SUCCESSRESPONSE		
		return response
	}

	

	public async update({params}: HttpContextContract) {
		var invoice = await Invoice.find(params.id)
		if (invoice != null){        
			if (invoice.amount < 100){

			}else if (invoice.amount < 100){
				if (invoice.amount < 501){

				}else if (invoice.amount < 500){

				}else if (invoice.amount > 500 && invoice.amount < 2000){

				}else if (invoice.amount > 2000 && invoice.amount < 50000){
					// 1 BOD
				}else if (invoice.amount > 50000 && invoice.amount < 100000){
					// 2 BOD
				}else if (invoice.amount > 100000){
					// 3 BOD
					
				}

			}
			invoice.save()
		}
		var response = {}
		response[RESULTKEY] = SUCCESSRESPONSE
		return response
	}



}

