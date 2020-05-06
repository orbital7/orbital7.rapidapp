function raGetResumeItems(area, type) {

    var items = [];

    var start = area + "*" + type;

    for (var i = 0; i < localStorage.length; i++) {
        var key = localStorage.key(i);
        var frags = key.split("|");
        if (frags.length === 2 && frags[0] === start) {
            items.push({ id: frags[1], value: localStorage.getItem(key) });
        }
    }

    return items;
}

function raGetResumeValue(area, type, id) {

    return localStorage.getItem(area + "*" + type + "|" + id);

}

function raStoreResumeValue(area, type, id, value) {

    localStorage.setItem(area + "*" + type + "|" + id, value);
}

function raStoreResumeField(area, type, fieldSelector, isCheckbox) {

    var field = $(fieldSelector);
    if (!isCheckbox) {
        raStoreResumeValue(area, type, fieldSelector, field.val());
    }
    else {
        raStoreResumeValue(area, type, fieldSelector, field.prop("checked"));
    }
}

function raLoadResumeField(area, type, fieldSelector, isCheckBox) {

    var value = raGetResumeValue(resumeArea, type, fieldSelector);
    if (value) {
        if (!isCheckBox) {
            $(fieldSelector).val(value);
        }
        else {
            $(fieldSelector).prop("checked", value === 'true');
        }
    }
}