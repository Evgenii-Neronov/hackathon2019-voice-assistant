var AudioContext = window.AudioContext || window.webkitAudioContext;
var audioContext;

var gumStream;
var rec;
var input;

function startRecording() {
    console.log("recordButton clicked");

    var constraints = { audio: true, video: false };


    navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {

        console.log("Подготовка к записи");

        audioContext = new AudioContext();
        gumStream = stream;
        input = audioContext.createMediaStreamSource(stream);
        initOnAudioprocess(audioContext, input);
        rec = new Recorder(input, { numChannels: 1 });
        rec.record();

        console.log("Запись начата");

    }).catch(function(err) {

        // TODO log error

    });
}

var recorder = null;
var bufferSize = 2048;

function initOnAudioprocess(context, mediaStream) {

    var numberOfInputChannels = 2;
    var numberOfOutputChannels = 1;

    if (context.createScriptProcessor) {
        recorder = context.createScriptProcessor(bufferSize,
            numberOfInputChannels,
            numberOfOutputChannels);
    } else {
        recorder = context.createJavaScriptNode(bufferSize,
            numberOfInputChannels,
            numberOfOutputChannels);
    }

    recorder.onaudioprocess = function(e) { drawBuffer(e.inputBuffer.getChannelData(0)); };

    mediaStream.connect(recorder);
    recorder.connect(context.destination);
}

function stopRecording() {
    console.log("Конец записи");

    rec.stop();
    
    gumStream.getAudioTracks()[0].stop();
    
    rec.exportWAV(createDownloadLink);
}

function createDownloadLink(blob) {

    uploadBlob(blob, startRecording);

}