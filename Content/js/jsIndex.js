var BoolPVacio = false;
setInterval(GetImage, 1000);
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
}



