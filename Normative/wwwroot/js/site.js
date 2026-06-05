/* Preloader */
$('a:not(.nloader)').on('click', function (e) {
    $('#loader').fadeIn(1000).show();
    return true;
});

// Hide if the user moves back in the history
$(window).on("pagecontainershow", function (e) {
    $("#loader").hide();
});

$(window).on('popstate', function (e) {
    $("#loader").hide();
});

window.onpageshow = function (event) {
    if (event.persisted) {
        $("#loader").hide();
        window.location.reload();
    }
};

/* End Preloader */

function QuickMessage(type, message) {
    const toast = $('#Toast');
    const toastInstance = new bootstrap.Toast(toast[0]);
    console.log(message);
    switch (type.toLowerCase()) {
        case "error":
            toast.addClass("text-bg-danger");
            break;
        case "info":
            toast.addClass("text-bg-primary");
            break
        case "success":
            toast.addClass("text-bg-success");
            break
        default:
            toast.addClass("text-bg-primary");
            break;
    }

    $('#Toast > div > div.toast-body').text(decodeHTMLEntities(message));
    toastInstance.show();
}

function decodeHTMLEntities(text) {
    var textarea = document.createElement("textarea");
    textarea.innerHTML = text;
    return textarea.value;
}



function GetPreparation(ProductSize) {
    var inputs = $("#preparation input.form-control");
    console.log(inputs)
    var x = 1;
    for (let i = 0; i < inputs.length; i += 2) { // i musi byt po 2

        var data = GetPreparationValue(ProductSize, x);

        if (data != null) {
            console.log(data)
            var obj = JSON.parse(data)

            $(inputs[i]).val(obj.Fitter);
            $(inputs[i+1]).val(obj.Welder);
        }
        x += 1;
    }
}

function GetPreparationValue(ProductSize, PreparationTypeId) {
    var response = null; $.ajax({
        url:
            "API/Preparation/GetValues/"
            + ProductSize +
            "/"
            + PreparationTypeId, type:
            'GET'
        , async: false,
        // Toto způsobí, že volání bude synchronní
        success: function (data) {
            response = data;
        },
        error: function () {
            QuickMessage("error", "values are not assigned to PreparationType");
        }
    }); return response;
}

//funkce na prepinani X411 chladici smycka a Velikost zasobniku v priprava
//function updatePriprava(text1, text2) {
//    document.getElementById('SmyTd1').innerText = text1;
//    document.getElementById('VelZasTd2').innerText = text2;
//}

//document.querySelectorAll('update-btn').forEach(button => {
//    button.addEventListener('click', function () {
//        const text1 = this.getAttribute('data-SmyTd1');
//        const text2 = this.getAttribute('data-VelZasTd2');
//        updatePriprava(text1, text2);
//    });
//});

//funkce pro vypocitani finalniho vysledku pro vysledovou tabulku operace 10 ohybani
function calculateTotalOhybani10() { //not working WIP
    var p15 = parseFloat($('#p15').attr('data-value')) || 0;
    var p21 = parseFloat($('#p21').attr('data-value')) || 0;
    var p33 = parseFloat($('#p33').attr('data-value')) || 0;
    var p48 = parseFloat($('#p48').attr('data-value')) || 0;
    var p60 = parseFloat($('#p60').attr('data-value')) || 0;
    var other = parseFloat($('#other').attr('data-value')) || 0;
    var pocetOhybu = parseFloat($('#PocetOhybuVOhybech').attr('data-value')) || 0;

    var sumP = (p15 + p21 + p33 + p48 + p60) * 30;
    var totalP = (sumP + (other * 5)) * 0.7 + 20;

    totalP = totalP.toFixed(1);
    totalPH = (totalP / 60).toFixed(1);

    $('#finalValueOhybaniMin10').text(totalP).attr('data-value', totalP);
    $('#finalValueOhybaniHod10').text(totalPH).attr('data-value', totalPH);
}
//end funkce pro vypocitani finalniho vysledku pro vysledovou tabulku operace 10 ohybani

//funkce pro vypocitani finalniho vysledku pro vysledovou tabulku operace 20 kotlar 
function calculateTotalKotlar20() { //not working WIP
    let totalKot = 0;

    $('span.HodnotyVysledekKotlar').each(function () {
        const value = parseFloat($(this).attr('data-value')) || 0;
        totalKot += value;
    });

    totalKot += (typeof KrytSpecialValue !== 'undefined' ? KrytSpecialValue : 0) +
        (typeof selectedSizeCountValue !== 'undefined' ? selectedSizeCountValue : 0);

    totalKot += parseFloat($('#OhybyMeziTotalVal').attr('data-value')) || 0;
    totalKot += parseFloat($('#ZvolenaVelikostSmycka').attr('data-value')) || 0;

    $('#fitter_1, #fitter_2, #fitter_3').each(function () {
        const value = parseFloat($(this).attr('data-value')) || 0;
        totalKot += value;
    });

    totalKot = totalKot.toFixed(1);
    totalKotH = (totalKot / 60).toFixed(1);

    $('#finalValueKotlarMin20').text(totalKot).attr('data-value', totalKot);
    $('#finalValueKotlarHod20').text(totalKotH).attr('data-value', totalKotH);
}
//end funkce pro vypocitani finalniho vysledku pro vysledovou tabulku operace 20 kotlar

//funkce pro vypocitani finalniho vysledku pro vysledovou tabulku operace 30 svarec
function calculateTotalSvarec30() { //not working WIP
    let totalSva = 0;

    $('span.HodnotyProVysledekSvareni').each(function () {
        const value = parseFloat($(this).attr('data-value')) || 0;
        totalSva += value;
    });

    totalSva += (typeof KrytSpecialValForSva !== 'undefined' ? KrytSpecialValForSva : 0);

    totalSva += parseFloat($('#ZvolenaVelikostSmycka').attr('data-value')) || 0;

    $('#welder_1').each(function () {
        const value = parseFloat($(this).attr('data-value')) || 0;
        totalSva += value;
    });

    totalSva = (totalSva).toFixed(1);
    totalSvaH = (totalSva/60).toFixed(1);
    
    $('#finalValueSvarecMin30').text(totalSva).attr('data-value', totalSva);
    $('#finalValueSvarecHod30').text(totalSvaH).attr('data-value', totalSvaH);
}
//end funkce pro vypocitani finalniho vysledku pro vysledovou tabulku operace 30 svarec

$("#ResultTab").on("click", function () {
    calculateTotalOhybani10();
    calculateTotalKotlar20();
    calculateTotalSvarec30();
}); 