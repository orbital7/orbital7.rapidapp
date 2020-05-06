var _raBeforeModalDialogSubmit = beforeModalDialogSubmit;
var _raBeforeModalDialogClose = beforeModalDialogClose;
var _raAfterModalDialogClosedSuccess = afterModalDialogClosedSuccess;
var _raOnModalDialogContentUnload = onModalDialogContentUnload;
var _modalDialogSubmitSuccess = false;
var _modalDialogSubmitCancelled = false;

function raShowTransition(title, message, transitionFunction) {

    if (!title)
        title = "";

    if (!message)
        message = "";

    var dialog = $("#ra-dialog");
    dialog.find(".modal-header").remove();
    dialog.find(".modal-footer").remove();
    dialog.find("#ra-dialog-body").html("<h2 id='ra-transition-title'>" + title + "</h2>" +
        "<h4 id='ra-transition-content' style='color: darkgreen;'>" + message + "</h4>" +
        "<div>&nbsp;</div><div>&nbsp;</div>" + getLoadingDiv() + "<div>&nbsp;</div>");

    dialog.modal({
        backdrop: "static",
        keyboard: false,
        show: true
    });
}

function raEnableModalDialogActionButton() {
    bsEnable($("#ra-dialog-actionbutton"));
}

function raDisableModalDialogActionButton() {
    bsDisable($("#ra-dialog-actionbutton"));
}

function onModalDialogContentUnload() {

}

function afterModalDialogClosedSuccess(success) {

}

function raShowModalDialog(
    contentUrl,
    returnAction,
    dialogTitle,
    showActionButton,
    actionButtonCaption,
    showCancelButton,
    cancelButtonCaption,
    dialogSize,
    source) {

    // Ready the dialog.
    _raBeforeModalDialogSubmit = beforeModalDialogSubmit;
    _raBeforeModalDialogClose = beforeModalDialogClose;
    _raAfterModalDialogClose = afterModalDialogClosedSuccess;
    _raOnModalDialogContentUnload = onModalDialogContentUnload;
    var dialog = $("#ra-dialog");

    // Size the dialog.
    var size = "med";
    if (dialogSize === "full")
        size = "full";
    else if (dialogSize === "large")
        size = "lg";
    else if (dialogSize === "medium")
        size = "med";
    else if (dialogSize === "small")
        size = "sm";
    $("#ra-dialog-modal").removeClass("ra-dialog-modal-sm")
        .removeClass("ra-dialog-modal-med")
        .removeClass("ra-dialog-modal-lg")
        .removeClass("ra-dialog-modal-full")
        .addClass("ra-dialog-modal-" + size);

    // Set the header.
    var dialogHeader = dialog.find(".modal-header");
    dialogHeader.find("#ra-dialog-title").text(dialogTitle);
    dialogHeader.find("#ra-dialog-returnaction").val(returnAction);

    // Show loading.
    var dialogBody = dialog.find("#ra-dialog-body");
    raShowLoading(dialogBody);

    // Ready the action button.
    var actionButton = dialog.find("#ra-dialog-actionbutton");
    bsDisable(actionButton);
    actionButton.text(actionButtonCaption);
    if (!showActionButton)
        actionButton.hide();
    else
        actionButton.show();

    // Ready the cancel button.
    var cancelButton = dialog.find("#ra-dialog-cancelbutton");
    cancelButton.text(cancelButtonCaption);
    if (!showCancelButton) {
        cancelButton.hide();
        dialog.find("#ra-dialog-cancelbutton-corner").hide();
    }
    else {
        cancelButton.show();
        dialog.find("#ra-dialog-cancelbutton-corner").show();
    }

    // Check for variables in the content url.
    if (contentUrl.indexOf("%7Bdatagrid-id%7D") !== -1) {
        var datagrids = $(source).closest(".ra-parent").find(".ra-datagrid-table");
        if (datagrids.length > 0)
            contentUrl = contentUrl.replace("%7Bdatagrid-id%7D", $(datagrids[0]).attr("id"));
    }

    // Show the dialog.
    dialog.modal({
        backdrop: "static",
        keyboard: showCancelButton,
        show: true
    });

    // Asychronously load content.
    $.get({
        url: contentUrl,
        cache: false,
        success: function (response) {
            updateDialogBodyHtml(dialog, dialogBody, actionButton, true, response, false);
        },
        error: function (xhr) {
            if (xhr.responseText)
                updateDialogBodyHtml(dialog, dialogBody, actionButton, false, xhr.responseText, false);
            else
                updateDialogBodyHtml(dialog, dialogBody, actionButton, false, "<strong class='red'>ERROR:</strong> Dialog content could not be loaded", false);
        }
    });
}

function beforeModalDialogSubmit(event) {
    return true;
}

function beforeModalDialogClose(event) {
    return true;
}

function submitModalDialog(event) {

    if (!_raBeforeModalDialogSubmit(event))
        return;

    _modalDialogSubmitSuccess = false;
    _modalDialogSubmitCancelled = false;

    var dialog = $("#ra-dialog");
    var returnAction = dialog.find("#ra-dialog-returnaction").val();
    var dialogBody = dialog.find("#ra-dialog-body");
    var actionButton = dialog.find("#ra-dialog-actionbutton");

    // Disable the form while processing.
    raDisablePanel(dialog.find("#ra-dialog-modal-content"));
    dialog.find("#ra-dialog-processing").show();
    bsDisable(actionButton);

    var form = dialogBody.find("form");
    var formData = new FormData(form[0]);
    postUrl = form.attr("action");

    $.ajax({
        type: "POST",
        cache: false,
        async: false,
        enctype: 'multipart/form-data',
        contentType: false,
        data: formData,
        processData: false,
        url: postUrl,
        success: function (response) {

            _modalDialogSubmitSuccess = !_modalDialogSubmitCancelled;

            if (!returnAction && response)
                returnAction = response;

            if (returnAction && !_modalDialogSubmitCancelled) {

                // Url.
                if (returnAction.startsWith("/") || returnAction.toLowerCase().startsWith("http:") || returnAction.toLowerCase().startsWith("https:")) {
                    navigateTo(returnAction);
                }
                // Script.
                else if (returnAction.includes("(") || returnAction.endsWith(";") || returnAction.includes("'") || returnAction.includes("\"") || returnAction.includes("=")) {
                    eval(returnAction);
                }
                // Ajax Content Key.
                else {
                    raUpdateAjaxContentSection(returnAction);
                }
            }

            raCloseModalDialog(event);
        },
        error: function (xhr, textStatus, errorThrown) {

            if (!_modalDialogSubmitCancelled) {

                if (xhr.responseText)
                    updateDialogBodyHtml(dialog, dialogBody, actionButton, true, xhr.responseText, true);
                else
                    updateDialogBodyHtml(dialog, dialogBody, actionButton, true, "<strong class='red'>ERROR " + xhr.status + ":</strong > Dialog was unable to POST", true);
            }
        }
    });
}

function raCloseModalDialog(event) {

    if (!_raBeforeModalDialogClose(event))
        return;

    _modalDialogSubmitCancelled = true;

    $("#ra-dialog").modal("hide");

    _raOnModalDialogContentUnload();
    $("#ra-dialog-body").html("");

    if (_modalDialogSubmitSuccess) {
        setTimeout(function () {
            _raAfterModalDialogClosedSuccess();
        }, 400);
    }
}

function resizeDialogBody() {

    var dialogBody = $("#ra-dialog-body");
    if (dialogBody) {
        var bodyHeight = dialogBody.height();
        var windowHeight = $(window).height();
        var difference = $(window).height() - bodyHeight;
        //alert(bodyHeight + ", " + windowHeight + ", " + difference);
        if (difference < 250)
            dialogBody.css("height", (windowHeight - 210) + "px");
        else
            dialogBody.css("height", "100%");
    }
}

function updateDialogBodyHtml(dialog, dialogBody, actionButton, enableActionButton, html, notify) {

    if (notify)
        _raOnModalDialogContentUnload();

    dialogBody.html(html);
    dialog.modal("handleUpdate");
    raUpdateBindings();

    if (enableActionButton)
        bsEnable(actionButton);
    else
        bsDisable(actionButton);

    dialog.find("#ra-dialog-processing").hide();
    raEnablePanel(dialog.find("#ra-dialog-modal-content"));

    setTimeout(resizeDialogBody, 250);

    setTimeout(function () {
        dialogBody.find(":input:enabled:visible:first").focus().select();
    }, 500);
}