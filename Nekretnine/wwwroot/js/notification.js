"use strict";

var $notificationHub = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

$(".snimiButton").prop("disabled", true);

$notificationHub.start().then(function () {
    $(".snimiButton").prop("disabled", false);
    console.log("Notification hub started");
});

$notificationHub.on("ReceiveNotification", function (message, id) {
    console.log('ReceiveNotification');
    var count = 0;
    var x = $('#count-' + id).html();
    count = parseInt($('#count-' + id).html()) || 0;
    count++;
    $('#count-' + id).html(count);
   
    

    if (message && message.toLowerCase() == "added") {
        
    }
});

$notificationHub.on("SuccessfulySubmited", function () {
    $('#cAlert').css("display", "block");

    setTimeout(function () {
        window.location.href = "/Kupac";
    }, 3000);
});

$notificationHub.on("ResetNotificationCounter", function (KorisnikID) {
    $('#count-' + KorisnikID).html('0');
});

$(".snimiButton").bind("click", function (event) {
    
    var formData = $('form').serializeArray();
    var datumObilaska = formData[2].value;
    var gradId = formData[3].value;
    var adresa = formData[4].value;
    var nekretninaId = formData[5].value;
    var agentId = formData[6].value;
    console.log(agentId);
    if (formData != null) {
        $notificationHub.invoke("Snimi", datumObilaska, gradId, adresa, nekretninaId, agentId).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
});

$("#noveNotifikacije").click(function () {
    $notificationHub.invoke("ResetNeprocitaneNotifikacije", 0).catch(function (err) {
        return console.error(err.toString());
    });
});