﻿function raLoadAjaxContent(contentUrl, destination) {

    if (contentUrl) {

        var element = $(destination);
        raShowLoading(element);

        $.get({
            url: contentUrl,
            cache: false,
            success: function (response) {
                element.html(response);
                raUpdateBindings();
            },
            error: function (xhr) {
                if (xhr.responseText)
                    element.html(xhr.responseText);
                else
                    element.html("<strong class='red'>ERROR:</strong> Content could not be loaded");
            }
        });
    }
}

function raUpdateAjaxContentUrl(contentKey, contentUrl) {

    $("#" + contentKey + "AjaxContent").attr("data-content-url", url);

}

function raUpdateAjaxContentSection(contentKey, updatedContentUrl) {

    var sectionId = "#" + contentKey + "AjaxContent";
    var section = $(sectionId);

    if (updatedContentUrl)
        section.attr("data-content-url", updatedContentUrl);

    var url = section.attr("data-content-url");

    if (!url) {
        var urlScript = section.attr("data-content-url-script");
        eval("url = " + urlScript);
    }

    raLoadAjaxContent(url, section);
}

function loadTaskbarItem(
    taskName) {

    var taskbar = $(".ra-taskbar");

    var selectedTask = taskbar.findByContentText(taskName);
    if (selectedTask) {

        taskbar.find(".ra-taskbar-item-selected")
            .removeClass("ra-taskbar-item-selected")
            .addClass("ra-taskbar-item-selectable");

        selectedTask.removeClass("ra-taskbar-item-selectable");
        selectedTask.addClass("ra-taskbar-item-selected");

        var key = taskbar.data("key");
        sessionStorage.setItem("taskbar_" + key + "_selectedTask", taskName);

        var action = selectedTask.attr("data-task-action");
        eval(action);
    }
}

function raRefreshTaskbarItem() {

    var taskbar = $(".ra-taskbar");
    var selectedTask = taskbar.find(".ra-taskbar-item-selected");

    var action = selectedTask.attr("data-task-action");
    eval(action);
}

function loadPagePanel(contentUrl) {

    var pagePanel = $("#ra-pagePanel");
    pagePanel.attr("data-content-url", contentUrl);
    raLoadAjaxContent(contentUrl, pagePanel);
}

function raRefreshPagePanel(updatedContentUrl) {

    var pagePanel = $("#ra-pagePanel");

    if (updatedContentUrl)
        pagePanel.attr("data-content-url", updatedContentUrl);

    var contentUrl = pagePanel.attr("data-content-url");
    raLoadAjaxContent(contentUrl, pagePanel);
}

function raUpdateBindings() {

    //$.validator.unobtrusive.parse(tabContent.find("form"));

    //$('input[type=checkbox][data-toggle^=toggle]').bootstrapToggle();

    //$(".ra-behavior-datepicker").datepicker({
    //    maxViewMode: 2,
    //    todayBtn: "linked",
    //    autoclose: true
    //});

    $(".ra-container-scrollable-x").overlayScrollbars({
        overflowBehavior: {
            x: "scroll",
            y: "hidden"
        },
        paddingAbsolute: true
    });

    $(".ra-container-scrollable-y").overlayScrollbars({
        overflowBehavior: {
            x: "hidden",
            y: "scroll"
        },
        paddingAbsolute: true
    });

    $(".ra-container-scrollable-both").overlayScrollbars({
        overflowBehavior: {
            x: "scroll",
            y: "scroll"
        },
        paddingAbsolute: true
    });

    $("input").keypress(function (e) {
        if (e.which === 13) {

            var x = $(this);
            x.blur();

            if (x.closest("#ra-dialog").length > 0) {
                submitModalDialog();
            }
            else {
                $(":submit").click();
            }
            return false;
        }
    });

    $(":submit").click(function (e) {
        bsDisable(this);
        $("form").submit();
    });
}