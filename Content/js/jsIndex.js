var BoolPVacio = false;
var DATE_TARGET = new Date('09/09/2022 04:22 PM');
const SPAN_SECONDS = document.querySelector('span#seconds');
const MILLISECONDS_OF_A_SECOND = 1000;
const MILLISECONDS_OF_A_MINUTE = MILLISECONDS_OF_A_SECOND * 60;
//setInterval(GetImage, 1000);
var updClock;

$("#iniciarPvacio").click(function () {
    BoolPVacio = true;
    GetImage();
    $("#closeModalPVacio").click();
    $("#divBlockRotor").hide();
    $("#divLoading").show();
    $("#divControlG").show();   
    $("#divCircutor").show();
    $("#btnPVacio").prop("disabled", "true");
    $("#btnPVacio").addClass("btn-disabled");
    $("#btnPRotor").removeAttr("disabled");
    $("#btnPRotor").removeClass("btn-disabled");
    $("#divControlVariac").show();
    
});


$("#iniciarPRotor").click(function () {
    BoolPVacio = true;
    GetImage();

    $("#divCircutor").show();
    $("#divControlG").show();  
    $("#divBlockRotor").show();  
    
    $("#divControlVariac").hide();
    $("#btnPVacio").removeAttr("disabled");
    $("#btnPVacio").removeClass("btn-disabled");
    $("#btnPRotor").prop("disabled", "true");
    $("#btnPRotor").addClass("btn-disabled");

    $("#closeModalPRotor").click();
    $("#divLoading").show();
    $("#divCircutor").show();

});

function GetImage() {
    if (BoolPVacio == true) {
        let url = "PruebaVacio/GetImagen";
        $.ajax({
            url: url,
            type: "POST",
            processData: false,
            contentType: false,
            success: function (response) {
                $("#imgCircutor").attr('src', 'data:image/bmp;base64,' + response.imagen);
                console.log("Get imagen OK");
            }
        });
    }    
}

function PostAccion(e) {
    let url = "PruebaVacio/PostAccion";
    let accionSend = e.getAttribute("data-funcion");
    let data = new FormData();
    data.append("key", JSON.stringify(accionSend))
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {

        }
    });
}

function EndPVacio() {
    BoolPVacio = false;
    $("#divBlockRotor").show();
    $("#divLoading").hide();
    $("#divControlG").hide();
    $("#divCircutor").hide();
    $("#btnPVacio").prop("disabled", "false");
    $("#btnPVacio").removeClass("btn-disabled");
    $("#btnPRotor").removeAttr("disabled");
    $("#btnPVacio").removeAttr("disabled");
    $("#btnPRotor").removeClass("btn-disabled");
    $("#divControlVariac").hide();

    $("#msjPRotor").removeClass("alert-danger");
    $("#msjPRotor").addClass("alert-warning");
    $("#btnPararRotor").prop("disabled", "false");
    $("#btnPararRotor").removeAttr("disabled");
    $("#btnPararRotor").removeClass("btn-disabled");
    $("#msjSpan").text('El rotor sera bloqueado por 12 segundos, tiempo restante: ');    
    //$("#msjPRotor").append('<span id="seconds"></span>');

    

}

function PararRotor() {
    $("#btnPararRotor").prop("disabled", "true");
    $("#btnPararRotor").addClass("btn-disabled");
    let url = "Fuente/PararRotor";
    let accionSend = "Run";
    let data = new FormData();
    data.append("key", JSON.stringify(accionSend))
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            $("#msjPRotor").addClass("alert-warning");
            $("#msjPRotor").show();
            var dateNow = new Date();
            dateNow.setSeconds(dateNow.getSeconds() + 14);
            console.log("date creada", dateNow);
            console.log("date const", DATE_TARGET);
            DATE_TARGET = dateNow;
            updClock  = setInterval(updateCountdown, 1000);

        }
    });
}

function updateCountdown() {
    // Calcs
    const NOW = new Date()
    const DURATION = DATE_TARGET - NOW;
    const REMAINING_SECONDS = Math.floor((DURATION % MILLISECONDS_OF_A_MINUTE) / MILLISECONDS_OF_A_SECOND);
    SPAN_SECONDS.textContent = REMAINING_SECONDS;
    if (REMAINING_SECONDS === 0) {
        clearInterval(updClock);
        SPAN_SECONDS.textContent = "";
        $("#msjPRotor").removeClass("alert-warning");
        $("#msjPRotor").addClass("alert-danger");
        $("#msjSpan").text('Prueba Finalizada, pulse el boton de "Descargar Imagen" para obtener una imagen del circutor tomada al momento de hacer la prueba.');     
        

    }
}

