var AM = "AM";
var PM = "PM";

$(document).ready(function () {
    meridiemClickHandler();
    timeFormatHandler();
});

//switches from AM to PM
function meridiemClickHandler() {
    $("#Meridiem").on('click', function () {
        let mer = $(this).val();
        switch (mer) {
            case AM:
                $(this).val(PM);
                break;
            case PM:
                $(this).val(AM);
                break;
        }
    });
}

//minutes default to 15, 30, 45, or 00
function timeFormatHandler() {
    $("#Minute").on('change', function () {
        let min = $(this).val();
        if (min % 15 != 0) {
            if (min > 0) {
                $(this).val("00");
            }
            if (min > 15) {
                $(this).val(15);
            }
            if (min > 30) {
                $(this).val(30);
            }
            if (min > 45) {
                $(this).val(45);
            }
        }
    });
}