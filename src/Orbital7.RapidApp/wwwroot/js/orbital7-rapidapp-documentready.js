$(document).ready(function () {

    $(window).resize(resizeDialogBody);

    $(".ra-template-item").change(function () {
        raRecordTemplateModified(this);
    });

    $("#ra-dialog").on("hidden.bs.modal", function (e) {
        $(this).find("#ra-dialog-body").html("");
    });

    raUpdateBindings();
});