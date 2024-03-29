﻿

var dt = new Date();
var Fromdatetime = dt.getFullYear() + "-" + ("0" + (dt.getMonth() + 1)).slice(-2) + "-" + ("0" + dt.getDate()).slice(-2) + "T" + ("0" + dt.getHours()).slice(-2) + ":" + ("0" + dt.getMinutes()).slice(-2) + ":" + ("00");
$('#fechaAgenda').attr('value', Fromdatetime);

function PostLogin(email, contrasenia, validation) {
    let emailActual = email;
    let contraseniaActual = contrasenia;
    let url = "Login/Login";
    let data = new FormData();
    console.log("validation", validation);
    let objDatosLogin = mapDatosLogin(emailActual, contraseniaActual);


    var fechaActual = new Date();
    var forFecha = fechaActual.getFullYear() + "-" + ("0" + (fechaActual.getMonth() + 1)).slice(-2) + "-" + ("0" + fechaActual.getDate()).slice(-2) + "T" + ("0" + fechaActual.getHours()).slice(-2) + ":" + ("0" + fechaActual.getMinutes()).slice(-2) + ":" + ("00");

    data.append("data", JSON.stringify(objDatosLogin));
    data.append("horaActual", JSON.stringify(forFecha));

    data.append("__RequestVerificationToken", JSON.stringify(validation))
    let dateLogin = new Date();
    
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        processData: false,  
        contentType: false,
        success: function (response) {
            console.log(response);

            if (response.success) {
                console.log(response);
                var fechaString = response.respuestaAgenda;
                var fechaFinSession = new Date(fechaString);


                document.cookie = "validatSesion=Session; expires=" + fechaFinSession.toUTCString();

                $("#formLogin").submit();
                $("#errorLogin").empty();
                $("#errorLogin").hide();
            } else {
                let error = response.respuestaLogin;

                $("#errorLogin").text(error.Mensaje);
                $("#errorLogin").show();

                setTimeout(() => {
                    $("#errorLogin").hide();
                }, "5000")
            }
        }
    });
}

function mapDatosLogin(_email, _passw) {
    let DatosLogin = {
        email: _email,
        password: _passw
    }

    return DatosLogin;
}

function ConsultarAgenda() {
    var fecha = $('#fechaAgenda').val();
    var email = $("#emailUser").val();
    var fechaActual = new Date();
    var forFecha = fechaActual.getFullYear() + "-" + ("0" + (fechaActual.getMonth() + 1)).slice(-2) + "-" + ("0" + fechaActual.getDate()).slice(-2) + "T" + ("0" + fechaActual.getHours()).slice(-2) + ":" + ("0" + fechaActual.getMinutes()).slice(-2) + ":" + ("00");

    $("#errorEmail").hide(); 
    $("#errorEmail").text("");
    $("#sucessLogin").text("");
    $("#sucessLogin").hide();

    let url = "/Login/ConsultaUsuario";

    let data = new FormData();
    data.append("data", JSON.stringify(email));
    data.append("fechaini", JSON.stringify(fecha));

    if (email == "" || email == null) {
        $("#errorEmail").show();
        $("#errorEmail").text("Es necesario ingresar el correo institucional");
        return
    }

    if (fecha < forFecha) {
        $("#errorEmail").show();
        $("#errorEmail").text("Fecha no permitida");
        return
    }
         

    $.ajax({
        url: url,
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {     

            if (response.success) {
                console.log(response);
                if (response.success) {
                    $("#sucessAgenda").text("Espacio agendado, ya puede cerrar esta ventana");
                    $("#sucessAgenda").show();
                    $("#agendaBody").hide();
                    $("#btnConfiAge").hide();
                    $("#btnCancelAge").hide();
                    $("#btnCloseAge").show();
                }

            } else {
                console.log(response);
                let error = response.respuestaLogin.Mensaje;
                $("#errorEmail").text(error);
                $("#errorEmail").show();

                
            }
        }
    });    
}

function CrearUsuario() {
    let emailNuevo = $("#emailUserNew").val();
    let contraseniaNuevo = $("#passwordUser").val();
    let nameNuevo = $("#nombreUser").val();
    let apellidoNuevo = $("#apellidoUser").val();
    let flag = true;
    $("#errorUsuario").hide();
    $("#errorUsuario").text('');
    $("#successUsuario").hide();
    $("#successUsuario").text('');

    $("#lbEmail").hide();
    $("#lbPass").hide();
    $("#lbLastname").hide();
    $("#lbName").hide();

    if (emailNuevo == null | emailNuevo == '') {
        $("#lbEmail").show();
        flag = false;
    }
    if (contraseniaNuevo == null | contraseniaNuevo == '') {
        $("#lbPass").show();
        flag = false;
    }
    if (nameNuevo == null | nameNuevo == '') {
        $("#lbName").show();
        flag = false;
    }
    if (apellidoNuevo == null | apellidoNuevo == '') {
        $("#lbLastname").show();
        flag = false;
    }

    if (!flag)
        return;

    let url = "/Login/Nuevousuario";

    let data = new FormData();
    let dataUser = mapDatosUsuario(emailNuevo, contraseniaNuevo, nameNuevo, apellidoNuevo);
    data.append("data", JSON.stringify(dataUser));
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {

            if (response.success) {
                console.log(response);
                $("#successUsuario").text("El usuario " + response.respuestaLogin.usuario + " ha sido " + response.respuestaLogin.Estado)
                $("#successUsuario").show();
                $("#generalForm").hide();
                $("#btnCancelUser").hide();
                $("#btnCrearUser").hide();
                $("#btnCloseAge").show();

            } else {
                let error = response.respuestaLogin;
                $("#errorUsuario").text(error);
                $("#errorUsuario").show();
            }
        }
    });    

}

$("#btnCerrar").click(function () {
    $("#emailUserNew").val("");
    $("#passwordUser").val("");
    $("#nombreUser").val("");
    $("#apellidoUser").val("");

    $("#btnCancelUser").show();
    $("#btnCrearUser").show();
    $("#generalForm").show();
    $("#btnCerrar").hide();

    $("#successUsuario").hide();
    $("#successUsuario").text('');

    $("#errorUsuario").hide();
    $("#errorUsuario").text('');
});

$("#btnCloseGModal").click(function () {
    $("#emailUserNew").val("");
    $("#passwordUser").val(""); 
    $("#nombreUser").val("");
    $("#apellidoUser").val("");

    $("#successUsuario").hide();
    $("#successUsuario").text('');

    $("#errorUsuario").hide();
    $("#errorUsuario").text('');

    $("#generalForm").show();
    $("#btnCancelUser").show();
    $("#btnCrearUser").show();
});


$("#btnCancelUser").click(function () {
    $("#emailUserNew").val("");
    $("#passwordUser").val("");
    $("#nombreUser").val("");
    $("#apellidoUser").val("");

    $("#successUsuario").hide();
    $("#successUsuario").text('');

    $("#errorUsuario").hide();
    $("#errorUsuario").text('');

});


$("#btnCancelAge").click(function () {
    $("#emailUser").val("");
    

    $("#fechaAgenda").removeAttr("value");
    var dt = new Date();
    var Fromdatetime = dt.getFullYear() + "-" + ("0" + (dt.getMonth() + 1)).slice(-2) + "-" + ("0" + dt.getDate()).slice(-2) + "T" + ("0" + dt.getHours()).slice(-2) + ":" + ("0" + dt.getMinutes()).slice(-2) + ":" + ("00");
    $('#fechaAgenda').attr('value', Fromdatetime);
    $("#fechaAgenda").val(Fromdatetime);
   

    $("#sucessAgenda").hide();
    $("#sucessAgenda").text('');

    $("#errorEmail").hide();
    $("#errorEmail").text('');
});

$("#btnCloseAge").click(function () {
    $("#emailUser").val("");
    $("#fechaAgenda").val("");

    $("#fechaAgenda").removeAttr("value");
    var dt = new Date();
    var Fromdatetime = dt.getFullYear() + "-" + ("0" + (dt.getMonth() + 1)).slice(-2) + "-" + ("0" + dt.getDate()).slice(-2) + "T" + ("0" + dt.getHours()).slice(-2) + ":" + ("0" + dt.getMinutes()).slice(-2) + ":" + ("00");
    $('#fechaAgenda').attr('value', Fromdatetime);
    $("#fechaAgenda").val(Fromdatetime);

    $("#sucessAgenda").hide();
    $("#sucessAgenda").text('');

    $("#errorEmail").hide();
    $("#errorEmail").text('');

    $("#agendaBody").show();
    $("#btnCancelUser").show();

    $("#btnCloseAge").hide();
    $("#btnConfiAge").show();


});


$("#btnGCloseAge").click(function () {
    $("#emailUser").val("");
    $("#fechaAgenda").removeAttr("value");
    var dt = new Date();
    var Fromdatetime = dt.getFullYear() + "-" + ("0" + (dt.getMonth() + 1)).slice(-2) + "-" + ("0" + dt.getDate()).slice(-2) + "T" + ("0" + dt.getHours()).slice(-2) + ":" + ("0" + dt.getMinutes()).slice(-2) + ":" + ("00");
    $('#fechaAgenda').attr('value', Fromdatetime);


    $("#sucessAgenda").hide();
    $("#sucessAgenda").text('');

    $("#errorEmail").hide();
    $("#errorEmail").text('');

    $("#agendaBody").show();
    $("#btnCancelAge").show();
    $("#btnConfiAge").show();


    $("#btnCloseAge").hide();

});

function mapDatosUsuario(_email, _passw, _nombre, _apellido) {
    let DatosUsuario = {
        Nombres: _nombre,
        Apellidos: _apellido,
        Email: _email,
        Contrasenia: _passw,
    }
    return DatosUsuario;
}