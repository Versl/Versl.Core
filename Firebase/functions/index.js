const functions = require('firebase-functions');
const admin = require("firebase-admin");

admin.initializeApp(functions.config().firebase);

exports.helloWorld = functions.https.onRequest((request, response) => {
  functions.logger.info("Hello logs!", {structuredData: true});
  response.send("Hello from Firebase!");
});

exports.sendPushNotification = functions.https.onRequest((request, response) => {
    var payload = {
        notification: {
           title: request.body.title,
           body: request.body.body
        }
    }
    
    return admin.messaging().sendToTopic(request.body.topic, payload)
        .then((res) => {            
            console.log('Notification sent successfully:', {'payload':payload, 'response': response});
            response.status(200).send();
            return 0;
        }) 
        .catch((error) => {
            console.log('Notification send failed:', error);
            response.status(500).send();
        }); 
  });