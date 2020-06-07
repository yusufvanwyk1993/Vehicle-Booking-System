// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Function used to set the client controls values when editing client
function EditClient(btn) {
    console.log(btn.id);
    var idnum = btn.id.replace("btneditclient", "");
    document.getElementById("editclientfirstname" + idnum).value = document.getElementById("firstname" + idnum).innerHTML;
}
//Function used to set the vehicle controls values when editing vehicle
function EditVehicle(btn) {
    console.log(btn.id);
    var idnum = btn.id.replace("btneditvehicle", "");
    document.getElementById("editvehiclemodelname" + idnum).value = document.getElementById("vehiclemodel" + idnum).innerHTML;
}