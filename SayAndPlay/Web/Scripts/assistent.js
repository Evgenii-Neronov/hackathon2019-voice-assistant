$(function () {

    $("#sayButton").on('touchstart mousedown', function (e) {
        startRecording();
    });

    $("#sayButton").on('ontouchend mouseup', function (e) {
        stopRecording();
    });

});