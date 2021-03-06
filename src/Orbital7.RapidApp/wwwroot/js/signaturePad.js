﻿var _signaturePads = [];

// One could simply use Canvas#toBlob method instead, but it's just to show
// that it can be done using result of SignaturePad#toDataURL.
function dataURLToBlob(dataURL) {
    // Code taken from https://github.com/ebidel/filer.js
    var parts = dataURL.split(';base64,');
    var contentType = parts[0].split(":")[1];
    var raw = window.atob(parts[1]);
    var rawLength = raw.length;
    var uInt8Array = new Uint8Array(rawLength);

    for (var i = 0; i < rawLength; ++i) {
        uInt8Array[i] = raw.charCodeAt(i);
    }

    return uInt8Array; //new Blob([uInt8Array], { type: contentType });
}

function getSignaturePadPngImageContents(signaturePad) {

    var dataURL = signaturePad.toDataURL();
    var uInt8Array = dataURLToBlob(dataURL);
    return btoa(String.fromCharCode.apply(null, uInt8Array));
}

function initalizeSignaturePads() {

    _signaturePads = [];

    $(".signature-pad").each(function () {

        var wrapper = this;
        var canvas = wrapper.querySelector("canvas");
        if (canvas) {
            var signaturePad = new SignaturePad(canvas, {
                maxWidth: 1.75,
            });
            
            var clearButton = wrapper.querySelector("[data-action=signature-pad-clear]");
            clearButton.addEventListener("click", function (event) {
                signaturePad.clear();
            });

            // When zoomed out to less than 100%, for some very strange reason,
            // some browsers report devicePixelRatio as less than 1
            // and only part of the canvas is cleared then.
            var ratio = Math.max(window.devicePixelRatio || 1, 1);

            // This part causes the canvas to be cleared
            canvas.width = canvas.offsetWidth * ratio;
            canvas.height = canvas.offsetHeight * ratio;
            canvas.getContext("2d").scale(ratio, ratio);

            // This library does not listen for canvas changes, so after the canvas is automatically
            // cleared by the browser, SignaturePad#isEmpty might still return false, even though the
            // canvas looks empty, because the internal data of this library wasn't cleared. To make sure
            // that the state of this library is consistent with visual state of the canvas, you
            // have to clear it manually.
            signaturePad.clear();

            _signaturePads.push(signaturePad);
        }
    });
}