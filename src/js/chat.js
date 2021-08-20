import * as signalR from "@microsoft/signalr";

import * as validator from "jquery-validation";
import * as unobtrusive from "jquery-validation-unobtrusive";


"use strict";

var connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.None)
    .withUrl("/chatHub")
    .build();

$("#connect").on("click", function () {
    document.getElementById("connect").disabled = true;

    connection.start().then(function () {
        console.log("Connection Started");
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("NewUserOnline", function (userName) {
    console.log("on NewUserOnline"); 
    console.log(userName + "is online"); 
});



if ($.validator) {
    console.log("in that if state"); 
}



////Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

//connection.on("ReceiveMessage", function (user, message) {
//    var li = document.createElement("li");
//    document.getElementById("messagesList").appendChild(li);
//    // We can assign user-supplied strings to an element's textContent because it
//    // is not interpreted as markup. If you're assigning in any other way, you 
//    // should be aware of possible script injection concerns.
//    li.textContent = `${user} says ${message}`;
//});



//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});