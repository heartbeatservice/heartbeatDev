
$(document).ready(function () {
    jQuery.support.cors = true;
    // $('#startDate').change(function () { $('#UKPrice').focus(); });
   // $('#InsuranceId').change(function () { alert(this.selectedIndex); });
   // $('#theCarousel').carousel();
   
});

function canvasSupport() {
    return Modernizr.canvas;
}

function createLogo(text) {

    var theCanvas = document.getElementById("theCanvas");
    var context = theCanvas.getContext("2d");
    function drawLogo() {

        context.fillStyle = "#ffffaa";
        context.fillRect(0, 0, 240, 80);
        
        //Text
        context.fillStyle = "#FFAA00";
        context.font = "20px Sans-Serif";
        context.textBaseline = "top";
        context.fillText("Heartbeat Service", 10, 10);

        //box
        context.strokStyle = "#000000";
        context.strokeRect(5, 5, 60, 30);

    }

    drawLogo();
}

function NullifyObject(obj) {
    if (obj.ISBN13 === "")
        obj.ISBN13 = null;
    if (obj.title === "")
        obj.title = null;
    if (obj.author === "")
        obj.author = null;

    return obj;
}

function showMessage(isVisible) {
    if (isVisible) {
        $('#dialog-confirm').dialog('open');
    }
    else {
        $('#dialog-confirm').dialog('close');
    }
}















//function assert(value,desc) {

//    var li = document.createElement("li");
//    li.className = value ? "pass" : "fail";
//    li.appendChild(document.createTextNode(desc));
//    document.getElementById("results").appendChild(li);

//}


//var things = function () {

//    return 'The thing function';
    
//}

//console.log(things());


//var ninja = {

//    yell: function yell(n) {
//        return n > 0 ? yell(n - 1) + 'a' : 'hiy';
//    }
//}

//assert(ninja.yell(4) == 'hiyaaaa', 'Works as we expect it to!');

//assert(ninja.yell(4) == 'hiyaaa', 'Ahh Man this failed!');