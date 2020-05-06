﻿function raDisablePanel(target) {
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

function raToggleSidebar() {

    var sidebar = $(".ra-layout-sidebar");

    if (sidebar.hasClass("ra-layout-sidebar-visible"))
        sidebar.removeClass("ra-layout-sidebar-visible");
    else
        sidebar.addClass("ra-layout-sidebar-visible");
}

function raHideSidebar() {

    $(".ra-layout-sidebar").removeClass("ra-layout-sidebar-visible");
}

function raShowLoading(element, fullHeight) {

    var useFullHeight = true;
    if (fullHeight === "undefined" || fullHeight === false)
        useFullHeight = false;

    var html = "<table class='ra-loading-table";
    if (useFullHeight)
        html += " ra-fullheight";
    html += "'><tr><td class='ra-loading-table-cell'><img style='width: 100px;' src='/orbital7-rapidapp/dist/images/spinners/ra-spinner-lg.gif' /></td></tr></table>";

    $(element).html(html);
}

function getLoadingDiv() {
    return "<div style='text-align: center;'><img style='width: 100px;' src='/orbital7-rapidapp/dist/images/spinners/ra-spinner-lg.gif' /></div>";
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

function raUpdateAjaxDropdowns(dropdownsSelector, url, optionLabel) {

    var dropdowns = $(dropdownsSelector);

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
                var selected = dropdown.val();
                dropdown.html(options);

                if (selected)
                    dropdown.val(selected).attr("selected", true).siblings("option").removeAttr("selected");
            });

        },
        error: function (xhr) {
            alert("Error updating dropdowns content");
        }
    });
}