
function say(text) {

    $.get("/v1/talk/?text=" + text, function (data) {

        var a = new Audio("/v1/audio/?text=" + data);
        a.volume = 1;
        a.play();

    });

}

function uploadBlob(blob) {

    var fd = new FormData();
    fd.append('fname', 'record.wav');
    fd.append('data', blob);
    $.ajax({
        type: 'POST',
        url: '/v1/recognize/',
        data: fd,
        processData: false,
        contentType: false
    }).done(function (data) {

        $("#textout").text(data);

        say(data);

        console.log(data);
    });
}

function drawBuffer(data) {
    var width = $("#myCanvas").width();
    var height = $("#myCanvas").height();

    var c = document.getElementById("myCanvas");
    var context = c.getContext("2d");

    var step = Math.ceil(data.length / width);
    var amp = height / 2;
    context.fillStyle = "green";
    context.clearRect(0, 0, width, height);
    for (var i = 0; i < width; i++) {
        var min = 1.0;
        var max = -1.0;
        for (j = 0; j < step; j++) {
            var datum = data[(i * step) + j];
            if (datum < min)
                min = datum;
            if (datum > max)
                max = datum;
        }
        context.fillRect(i + 3, (1 + min) * amp, 1, Math.max(1, (max - min) * amp));
    }
}