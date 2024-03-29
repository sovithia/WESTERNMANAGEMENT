var admin = require("firebase-admin")
var serviceAccount = require("../../config/push.json");
import User from 'App/Models/User'
import fs from 'fs'

export default class Utils {
 
  static  generateInvoiceNumber() {
    return new Date().getTime().toString()
  }

  static  signatureCount(id){
		var files = fs.readdirSync('../../../images/signatures').filter(fn => fn.startsWith(id + '_'))
		return files.length
	}

  static async sendPushNotificationToUser(userid,messagetitle,messagebody)
  {
    if (admin.apps.length === 0){
      admin.initializeApp({
        credential: admin.credential.cert(serviceAccount),
        databaseURL: "https://w-i-s-dcd7d.firebaseio.com"
      })
    }  
      var user = await User.find(userid)
      if (!user)
        return
      var myToken = user.fcmToken
    const message = {
      notification: {
        title: messagetitle,
        body: messagebody
      },
      token : myToken   
    };    
      
    admin.messaging().send(message).then((response) => {
      console.log('Successfully sent message:', response);})  
  }

  static sendPushNotification(token,messagetitle,messagebody)
  {
    if (admin.apps.length === 0){
      admin.initializeApp({
        credential: admin.credential.cert(serviceAccount),
        databaseURL: "https://w-i-s-dcd7d.firebaseio.com"
      })
    }  

    const message = {
      notification: {
        title: messagetitle,
        body: messagebody
      },
      token : token   
    };    
      
    admin.messaging().send(message)
      .then((response) => {
        console.log('Successfully sent message:', response);
      })  
      }   
  }
  





