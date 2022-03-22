﻿function PostLogin(email, contrasenia) {
    let emailActual = email;
    let contraseniaActual = contrasenia;
    let url = "Login/Login";
    let data = new FormData();
    let objDatosLogin = mapDatosLogin(emailActual, contraseniaActual);
    data.append("data", JSON.stringify(objDatosLogin));
    
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