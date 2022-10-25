

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
    data.append("data", JSON.stringify(objDatosLogin));
    data.append("__RequestVerificationToken", JSON.stringify(validation))
    
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
                $("#formLogin").submit();
                $("#errorLogin").empty();
                $("#errorLogin").hide();
            } else {
                let error = response.respuestaLogin;

                console.log("error", error);
                $("#errorLogin").text(error.MensajeError);
                $("#errorLogin").show();
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

    console.log("fechaAgenda", forFecha);

    $("#errorEmail").hide();


    let url = "Login/ConsultaUsuario";

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
               
            } else {
                let error = response.respuestaLogin;
                $("#errorEmail").text(error.MensajeError)
                $("#errorEmail").show();

                console.log("error", error);
                
            }
        }
    });    
}