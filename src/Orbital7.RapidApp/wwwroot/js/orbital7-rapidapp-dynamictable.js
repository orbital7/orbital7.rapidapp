function raAddDynamicTableRow(tableId, ajaxUrl) {

    var tableBody = $("#" + tableId);
    
    $.get(ajaxUrl).done(function (html) {
        tableBody.append($(html));
        raUpdateBindings();
    })
        .fail(function (xhr) {
            alert("ERROR: A new Dynamic Table row could not be created. " + xhr.responseText);
        })
        .always(function () {

        });

}

function raRemoveDynamicTableRow(source) {

    event.preventDefault();
    $(source).closest("tr").remove();

}