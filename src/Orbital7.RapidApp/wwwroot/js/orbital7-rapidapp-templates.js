function raLoadTemplate(source, url) {

    var templateId = $(source).val();
    if (templateId)
        navigateTo(url + "&templateId=" + templateId);
}

function raUploadTemplate(form, templateName, clearTemplateId, uploadUrl) {

    form.find("#TemplateName").val(templateName);
    if (clearTemplateId)
        form.find("#TemplateId").val(guidEmpty());

    $.post({
        cache: false,
        contentType: undefined,
        data: form.serialize(),
        processData: false,
        url: uploadUrl,
        success: function (response) {
            eval(response);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}

function raRecordTemplateModified(source) {

    var form = getParentForm(source);

    if (form.find(".ra-template-id").val() !== guidEmpty()) {
        var selected = form.find(".ra-template-id option:selected");
        var name = selected.text();
        var modified = "*";
        if (!endsWith(name, modified)) {
            selected.text(name + modified);
            form.find(".ra-template-name").val(name + modified);
            form.find(".ra-template-savebutton").removeClass("disabled");
        }
    }
}