function raDisablePanel(target) {
    var x = $(target);
    if (x.find(".ra-div-disabled").length === 0)
        x.prepend("<div class='ra-div-disabled'>&nbsp;</div>");
}

function raEnablePanel(target) {
    $(target).find(".ra-div-disabled").remove();
}

function bsEnable(target) {

    $(target).removeAttr("disabled").removeClass("disabled");
}

function bsDisable(target) {

    $(target).attr("disabled", true).addClass("disabled");
}

function raUpdateFlipswitchLabel(source) {

    var checkbox = $(source);
    var label = "";

    if (checkbox.prop("checked"))
        label = checkbox.data("checked");
    else
        label = checkbox.data("unchecked");

    checkbox.parent().find("label").html(label);
}

function raToggleSidebar(sidebarSelector) {

    if (!sidebarSelector)
        sidebarSelector = ".ra-layout-sidebar";

    var sidebar = $(sidebarSelector);

    if (sidebar.hasClass("ra-layout-sidebar-visible"))
        sidebar.removeClass("ra-layout-sidebar-visible");
    else
        sidebar.addClass("ra-layout-sidebar-visible");
}

function raHideSidebar(sidebarSelector) {

    if (!sidebarSelector)
        sidebarSelector = ".ra-layout-sidebar";

    $(sidebarSelector).removeClass("ra-layout-sidebar-visible");
}

function raShowLoading(element, fullSize) {

    var useFullSize = true;
    if (fullSize === "undefined" || fullSize === false)
        useFullSize = false;

    var html = "<table class='ra-loading-table";
    if (useFullSize)
        html += " ";    //ra-fullsize
    html += "'><tr><td class='ra-loading-table-cell'><img style='width: 100px;' src='/images/ra-spinner-lg.gif' /></td></tr></table>";

    $(element).html(html);
}

function getLoadingDiv() {
    return "<div style='text-align: center;'><img style='width: 100px;' src='/images/ra-spinner-lg.gif' /></div>";
}

function raDropdownValueExists(dropdown, targetValue) {

    var exists = false;

    dropdown.find('option').each(function () {
        if (this.value === targetValue) {
            exists = true;
            return;
        }
    });

    return exists;
}

function raUpdateAjaxDropdowns(dropdownsSelector, url, optionLabel, whenDone) {

    var dropdowns = $(dropdownsSelector);

    // TODO: This only works for a single dropdown; need to make this work with multiple dropdowns.
    var selected = dropdowns.val();

    dropdowns.each(function () {
        $(this).html("<option value=''>Loading...</option>");
    });

    $.ajax({
        url: url,
        dataType: 'json',
        cache: false,
        success: function (data, status, jqXHR) {

            var options = "";
            if (optionLabel)
                options += "<option value=''>" + optionLabel + "</option>";

            $.each(data, function (i, item) {
                options += "<option value='" + item.item1 + "'>" + item.item2 + "</option>";
            });

            dropdowns.each(function () {

                var dropdown = $(this);
                dropdown.html(options);

                if (selected)
                    dropdown.val(selected).attr("selected", true).siblings("option").removeAttr("selected");
            });

            whenDone();

        },
        error: function (xhr) {
            alert("Error updating dropdowns content");
        }
    });
}