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

function onInputCurrency(event) {
    let target = event.target;
    if (isNaN(target.valueAsNumber)) target.value = '';
    if (target.value.includes('e')) {
        target.value = target.value.replaceAll('e', '');
    }
    let parts = target.value.split('.');
    if (parts.length == 2 && parts[1].length > 2) {
        event.target.value = `${parts[0]}.${parts[1].substring(0, 2)}`;
    }
}