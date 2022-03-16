function validateEmail(email) 
{
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}

function hideMessage(){
    $("#dominioCorreo").hide();
    $("#emptyCorreo").hide();        
    $("#emptyContrasenia").hide();


}

function validarDatos(){
    hideMessage();
    let emailActual = $("#emailInput").val();
    let contraseniaActual = $("#contraseniaInput").val();
    let flagLogin = true;
    if(emailActual != ""){ 
        let flagCorreo = validateEmail(emailActual);
        if(!flagCorreo){
            $("#dominioCorreo").show();
            flagLogin = false;
        }
    }else{
        $("#emptyCorreo").show();        
        flagLogin = false;

    }

    if(contraseniaActual == ""){
        $("#emptyContrasenia").show();
        flagLogin = false;
    }
    
    if(flagLogin){
        //Llamado a controlador para la validacion de la contraseña y usuario
        PostLogin(emailActual, contraseniaActual);
    }

}

