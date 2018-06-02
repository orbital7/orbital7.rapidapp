var _raFullHeightBottomOffset = 30;
var _raBeforeSubmitModalDialog = beforeSubmitModalDialog;
var _raBeforeCloseModalDialog = beforeCloseModalDialog;
var _raOnModalDialogContentUnload = onModalDialogContentUnload;
var _raModalDialogSubmitCancelled = false;

function showLoading(element, fullHeight) {

    var useFullHeight = true;
    if (fullHeight === "undefined" || fullHeight === false)
        useFullHeight = false;

    var html = "<table class='ra-loading-table";
    if (useFullHeight)
        html += " ra-fullheight";
    html += "'><tr><td class='ra-loading-table-cell'><img style='width: 100px;' src='/images/RALoadingPanel.gif' /></td></tr></table>";

    $(element).html(html);

    if (useFullHeight)
        resizeAll();
}

function getLoadingDiv() {
    return "<div style='text-align: center;'><img style='width: 100px;' src='/images/RALoadingPanel.gif' /></div>";
}

function addDynamicTableRow(tableId, ajaxUrl) {

    var tableBody = $("#" + tableId);

    $.get(ajaxUrl).done(function (html) {
        tableBody.append($(html));
        updateBindings();
    })
        .fail(function (xhr) {
            alert("ERROR: A new Dynamic Table row could not be created. " + xhr.responseText);
        })
        .always(function () {

        });

}

function removeDynamicTableRow(source) {

    event.preventDefault();
    $(source).closest("tr").remove();

}

function bsEnable(target) {

    $(target).removeAttr("disabled").removeClass("disabled");
}

function bsDisable(target) {

    $(target).attr("disabled", true).addClass("disabled");
}

function loadTemplate(source, url) {

    var templateId = $(source).val();
    if (templateId)
        navigateTo(url + "&templateId=" + templateId);
}

function uploadTemplate(form, templateName, clearTemplateId, uploadUrl) {

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

function recordTemplateModified(source) {

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

function resizeAll() {

    var mywindow = $(window);

    $(".ra-fullheight:visible").each(function () {

        var container = $(this);
        var offset = container.offset();
        if (offset.top >= 0) {
            var additionalOffset = container.data("fullheight-offset");
            if (!additionalOffset)
                additionalOffset = 0;
            container.css("height", mywindow.innerHeight() - offset.top - additionalOffset - _raFullHeightBottomOffset + "px");
        }
    });
}

function setupDataGrid() {

    $(".ra-datagrid-wrapper").each(function () {

        var wrapper = $(this);
        var table = wrapper.find(".ra-datagrid-table");
        if (table.width() >= 0 && table.attr("data-width-set") !== "true") {
            wrapper.css("width", table.width() + 18 + "px");
            table.attr("data-width-set", true);
        }

        updateDataGridRowColors(table);
    });
}

function updateDataGridRowColors(table) {

    var tableBody = table.find("tbody");
    tableBody.children("tr").addClass("ra-datagrid-row");
    tableBody.children("tr").removeClass("ra-datagrid-row-alt");
    tableBody.children("tr:odd").addClass("ra-datagrid-row-alt");
}

function selectDataGridRow(sourceRow) {

    var row = $(sourceRow);
    var table = row.closest("table");
    var selectedRows = table.find(".ra-datagrid-row-selected");
    var container = table.closest(".ra-container");
    var singleSelectEditor = container.find(".ra-datagrid-singleselect-toolbar-editor");
    var multiSelectEditor = container.find(".ra-datagrid-multiselect-toolbar-editor");

    if (row.hasClass("ra-datagrid-row-selected")) {
        row.removeClass("ra-datagrid-row-selected");
    }
    else if (selectedRows.length === 0 || multiSelectEditor.length > 0) {
        row.addClass("ra-datagrid-row-selected");
    }
    else if (selectedRows.length === 1 && multiSelectEditor.length === 0) {
        selectedRows.each(function () {
            $(this).removeClass("ra-datagrid-row-selected");
        });
        row.addClass("ra-datagrid-row-selected");
    }

    multiSelectEditor.hide();
    singleSelectEditor.hide();
    selectedRows = table.find(".ra-datagrid-row-selected");
    if (selectedRows.length > 1 || (selectedRows.length === 1 && singleSelectEditor.length === 0))
        multiSelectEditor.show();
    else if (selectedRows.length === 1)
        singleSelectEditor.show();
}

function createSelectedDataGridItemsList(dataGridId, htmlFieldPrefix, includeNames, nameDivClass) {

    var table = $("#" + dataGridId);
    var html = "";
    var i = 0;
    table.find(".ra-datagrid-row-selected").each(function () {
        var row = $(this);
        html += "<input type='hidden' id='" + htmlFieldPrefix + "[" + i + "]' name='" +
            htmlFieldPrefix.replace("_", ".") + "[" + i + "]' value='" + row.attr("data-row-id") + "' />";
        if (includeNames)
            html += "<div class='" + nameDivClass + "'>" + row.attr("data-row-name") + "</div>";
        i++;
    });

    $("#selectedDataGridItemsList").html(html);
}


function showTransition(title, message, transitionFunction) {

    if (!title)
        title = "";

    if (!message)
        message = "";

    var dialog = $("#ra-dialog");
    dialog.find(".modal-header").remove();
    dialog.find(".modal-footer").remove();
    dialog.find("#ra-dialog-body").html("<h1 id='ra-transition-title'>" + title + "</h1>" +
        "<h3 id='ra-transition-content' style='color: darkgreen;'>" + message + "</h3>" +
        "<div>&nbsp;</div><div>&nbsp;</div>" + getLoadingDiv() + "<div>&nbsp;</div>");

    dialog.modal({
        backdrop: "static",
        keyboard: false,
        show: true
    });
}

function enableModalDialogActionButton() {
    bsEnable($("#ra-dialog-actionbutton"));
}

function disableModalDialogActionButton() {
    bsDisable($("#ra-dialog-actionbutton"));
}

function onModalDialogContentUnload() {

}

function showModalDialog(contentUrl, returnAction, dialogTitle,
    showActionButton, actionButtonCaption, showCancelButton, cancelButtonCaption, source) {

    // Ready the dialog.
    var _raBeforeSubmitModalDialog = beforeSubmitModalDialog;
    var _raBeforeCloseModalDialog = beforeCloseModalDialog;
    var _raOnModalDialogContentUnload = onModalDialogContentUnload;
    var dialog = $("#ra-dialog");

    // Set the header.
    var dialogHeader = dialog.find(".modal-header");
    dialogHeader.find("#ra-dialog-title").text(dialogTitle);
    dialogHeader.find("#ra-dialog-returnaction").val(returnAction);

    // Show loading.
    var dialogBody = dialog.find("#ra-dialog-body");
    showLoading(dialogBody);

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
    if (contentUrl.indexOf("%7Bdatagrid-selected-row-id%7D") !== -1) {
        var selectedRows = $(source).closest(".ra-container").find(".ra-datagrid-row-selected");
        if (selectedRows.length > 0)
            contentUrl = contentUrl.replace("%7Bdatagrid-selected-row-id%7D", $(selectedRows[0]).attr("data-row-id"));
    }
    if (contentUrl.indexOf("%7Bdatagrid-id%7D") !== -1) {
        var datagrids = $(source).closest(".ra-container").find(".ra-datagrid-table");
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

function beforeSubmitModalDialog(event) {
    return true;
}

function beforeCloseModalDialog(event) {
    return true;
}

function submitModalDialog(event) {

    if (!_raBeforeSubmitModalDialog(event))
        return;

    _raModalDialogSubmitCancelled = false;

    var dialog = $("#ra-dialog");
    var returnAction = dialog.find("#ra-dialog-returnaction").val();
    var dialogBody = dialog.find("#ra-dialog-body");
    var actionButton = dialog.find("#ra-dialog-actionbutton");

    // Disable the form while processing.
    dialog.find("#ra-dialog-processing").show();
    bsDisable(actionButton);
    // TODO: Everything else.

    var form = dialogBody.find("form");
    //var formData = new FormData(form);
    postUrl = form.attr("action");

    $.post({
        cache: false,
        contentType: undefined,
        data: form.serialize(),
        processData: false,
        url: postUrl,
        success: function (response) {

            if (response !== "")
                returnAction = response;

            if (returnAction) {
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
                    updateAjaxContentSection(returnAction);
                }
            }

            closeModalDialog(event);
        },
        error: function (xhr, textStatus, errorThrown) {

            if (!_raModalDialogSubmitCancelled) {

                if (xhr.responseText)
                    updateDialogBodyHtml(dialog, dialogBody, actionButton, true, xhr.responseText, true);
                else
                    updateDialogBodyHtml(dialog, dialogBody, actionButton, true, "<strong class='red'>ERROR " + xhr.status + ":</strong > Dialog was unable to POST", true);
            }
        }
    });
}

function closeModalDialog(event) {

    if (!_raBeforeCloseModalDialog(event))
        return;

    _raModalDialogSubmitCancelled = true;

    $("#ra-dialog").modal("hide");

    _raOnModalDialogContentUnload();
    $("#ra-dialog-body").html("");
}

function updateDialogBodyHtml(dialog, dialogBody, actionButton, enableActionButton, html, notify) {

    if (notify)
        _raOnModalDialogContentUnload();

    dialogBody.html(html);
    updateBindings();
    dialog.modal("handleUpdate");

    if (enableActionButton)
        bsEnable(actionButton);
    else
        bsDisable(actionButton);

    dialog.find("#ra-dialog-processing").hide();
}

function loadAjaxContent(contentUrl, destination) {

    if (contentUrl) {

        var element = $(destination);
        showLoading(element);

        $.get({
            url: contentUrl,
            cache: false,
            success: function (response) {
                element.html(response);
                updateBindings();
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

function updateAjaxContentUrl(contentKey, contentUrl) {

    $("#" + contentKey + "AjaxContent").attr("data-content-url", url);

}

function updateAjaxContentSection(contentKey, updatedContentUrl) {

    var sectionId = "#" + contentKey + "AjaxContent";
    var section = $(sectionId);
    
    if (updatedContentUrl)
        section.attr("data-content-url", updatedContentUrl);

    var url = section.attr("data-content-url");

    if (!url) {
        var urlScript = section.attr("data-content-url-script");
        eval("url = " + urlScript + ";");
    }

    loadAjaxContent(url, section);

}

function updateAjaxDropdown(dropDownId, url, optionLabel) {

    var dropDown = $("#" + dropDownId);

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

            var selected = dropDown.val();
            dropDown.html(options);

            if (selected)
                dropDown.val(selected).attr("selected", true).siblings("option").removeAttr("selected");
        },
        error: function (xhr) {
            alert("Error updating dropdown content");
        }
    });
}

function updateBindings() {

    //$.validator.unobtrusive.parse(tabContent.find("form"));

    // RapidApp has direct support for Bootstrap Toggle control (Bower: bootstrap-toggle).
    try {
        $('input[type=checkbox][data-toggle^=toggle]').bootstrapToggle();
    }
    catch (err) { }

    // RapidApp has direct support for Bootstrap DatePicker control (Bower: bootstrap-datepicker).
    try {
        
        $(".ra-behavior-datepicker").datepicker({
            maxViewMode: 2,
            todayBtn: "linked",
            autoclose: true,
        });
    }
    catch (err) { }

    resizeAll();
}

$(document).ready(function () {

    $(window).resize(function () {
        resizeAll();
    });

    $(".ra-template-item").change(function () {
        recordTemplateModified(this);
    });

    $("#ra-dialog").on("hidden.bs.modal", function (e) {
        $(this).find("#ra-dialog-body").html("");
    });

    updateBindings();
});

/*!
 * tablesort v5.0.2 (2017-11-12)
 * http://tristen.ca/tablesort/demo/
 * Copyright (c) 2017 ; Licensed MIT
!function () { function a(b, c) { if (!(this instanceof a)) return new a(b, c); if (!b || "TABLE" !== b.tagName) throw new Error("Element must be a table"); this.init(b, c || {}) } var b = [], c = function (a) { var b; return window.CustomEvent && "function" == typeof window.CustomEvent ? b = new CustomEvent(a) : (b = document.createEvent("CustomEvent"), b.initCustomEvent(a, !1, !1, void 0)), b }, d = function (a) { return a.getAttribute("data-sort") || a.textContent || a.innerText || "" }, e = function (a, b) { return a = a.trim().toLowerCase(), b = b.trim().toLowerCase(), a === b ? 0 : a < b ? 1 : -1 }, f = function (a, b) { return function (c, d) { var e = a(c.td, d.td); return 0 === e ? b ? d.index - c.index : c.index - d.index : e } }; a.extend = function (a, c, d) { if ("function" != typeof c || "function" != typeof d) throw new Error("Pattern and sort must be a function"); b.push({ name: a, pattern: c, sort: d }) }, a.prototype = { init: function (a, b) { var c, d, e, f, g = this; if (g.table = a, g.thead = !1, g.options = b, a.rows && a.rows.length > 0) if (a.tHead && a.tHead.rows.length > 0) { for (e = 0; e < a.tHead.rows.length; e++)if ("thead" === a.tHead.rows[e].getAttribute("data-sort-method")) { c = a.tHead.rows[e]; break } c || (c = a.tHead.rows[a.tHead.rows.length - 1]), g.thead = !0 } else c = a.rows[0]; if (c) { var h = function () { g.current && g.current !== this && g.current.removeAttribute("aria-sort"), g.current = this, g.sortTable(this) }; for (e = 0; e < c.cells.length; e++)f = c.cells[e], f.setAttribute("role", "columnheader"), "none" !== f.getAttribute("data-sort-method") && (f.tabindex = 0, f.addEventListener("click", h, !1), null !== f.getAttribute("data-sort-default") && (d = f)); d && (g.current = d, g.sortTable(d)) } }, sortTable: function (a, g) { var h = this, i = a.cellIndex, j = e, k = "", l = [], m = h.thead ? 0 : 1, n = a.getAttribute("data-sort-method"), o = a.getAttribute("aria-sort"); if (h.table.dispatchEvent(c("beforeSort")), g || (o = "ascending" === o ? "descending" : "descending" === o ? "ascending" : h.options.descending ? "descending" : "ascending", a.setAttribute("aria-sort", o)), !(h.table.rows.length < 2)) { if (!n) { for (; l.length < 3 && m < h.table.tBodies[0].rows.length;)k = d(h.table.tBodies[0].rows[m].cells[i]), k = k.trim(), k.length > 0 && l.push(k), m++; if (!l) return } for (m = 0; m < b.length; m++)if (k = b[m], n) { if (k.name === n) { j = k.sort; break } } else if (l.every(k.pattern)) { j = k.sort; break } for (h.col = i, m = 0; m < h.table.tBodies.length; m++){ var p, q = [], r = {}, s = 0, t = 0; if (!(h.table.tBodies[m].rows.length < 2)) { for (p = 0; p < h.table.tBodies[m].rows.length; p++)k = h.table.tBodies[m].rows[p], "none" === k.getAttribute("data-sort-method") ? r[s] = k : q.push({ tr: k, td: d(k.cells[h.col]), index: s }), s++; for ("descending" === o ? q.sort(f(j, !0)) : (q.sort(f(j, !1)), q.reverse()), p = 0; p < s; p++)r[p] ? (k = r[p], t++) : k = q[p - t].tr, h.table.tBodies[m].appendChild(k) } } h.table.dispatchEvent(c("afterSort")) } }, refresh: function () { void 0 !== this.current && this.sortTable(this.current, !0) } }, "undefined" != typeof module && module.exports ? module.exports = a : window.Tablesort = a }();
*/

; (function () {
    function Tablesort(el, options) {
        if (!(this instanceof Tablesort)) return new Tablesort(el, options);

        if (!el || el.tagName !== 'TABLE') {
            throw new Error('Element must be a table');
        }
        this.init(el, options || {});
    }

    var sortOptions = [];

    var createEvent = function (name) {
        var evt;

        if (!window.CustomEvent || typeof window.CustomEvent !== 'function') {
            evt = document.createEvent('CustomEvent');
            evt.initCustomEvent(name, false, false, undefined);
        } else {
            evt = new CustomEvent(name);
        }

        return evt;
    };

    var getInnerText = function (el) {
        return el.getAttribute('data-sort') || el.textContent || el.innerText || '';
    };

    // Default sort method if no better sort method is found
    var caseInsensitiveSort = function (a, b) {
        a = a.trim().toLowerCase();
        b = b.trim().toLowerCase();

        if (a === b) return 0;
        if (a < b) return 1;

        return -1;
    };

    // Stable sort function
    // If two elements are equal under the original sort function,
    // then there relative order is reversed
    var stabilize = function (sort, antiStabilize) {
        return function (a, b) {
            var unstableResult = sort(a.td, b.td);

            if (unstableResult === 0) {
                if (antiStabilize) return b.index - a.index;
                return a.index - b.index;
            }

            return unstableResult;
        };
    };

    Tablesort.extend = function (name, pattern, sort) {
        if (typeof pattern !== 'function' || typeof sort !== 'function') {
            throw new Error('Pattern and sort must be a function');
        }

        sortOptions.push({
            name: name,
            pattern: pattern,
            sort: sort
        });
    };

    Tablesort.prototype = {

        init: function (el, options) {
            var that = this,
                firstRow,
                defaultSort,
                i,
                cell;

            that.table = el;
            that.thead = false;
            that.options = options;

            if (el.rows && el.rows.length > 0) {
                if (el.tHead && el.tHead.rows.length > 0) {
                    for (i = 0; i < el.tHead.rows.length; i++) {
                        if (el.tHead.rows[i].getAttribute('data-sort-method') === 'thead') {
                            firstRow = el.tHead.rows[i];
                            break;
                        }
                    }
                    if (!firstRow) {
                        firstRow = el.tHead.rows[el.tHead.rows.length - 1];
                    }
                    that.thead = true;
                } else {
                    firstRow = el.rows[0];
                }
            }

            if (!firstRow) return;

            var onClick = function () {
                if (that.current && that.current !== this) {
                    that.current.removeAttribute('aria-sort');
                    $(that.current).find(".ra-datagrid-sortaria").remove(); // JVE Added.
                }

                that.current = this;
                that.sortTable(this);
            };

            // Assume first row is the header and attach a click handler to each.
            for (i = 0; i < firstRow.cells.length; i++) {
                cell = firstRow.cells[i];
                cell.setAttribute('role', 'columnheader');
                if (cell.getAttribute('data-sort-method') !== 'none') {
                    cell.tabindex = 0;
                    cell.addEventListener('click', onClick, false);

                    if (cell.getAttribute('data-sort-default') !== null) {
                        defaultSort = cell;
                    }
                }
            }

            if (defaultSort) {
                that.current = defaultSort;
                that.sortTable(defaultSort);
            }
        },

        sortTable: function (header, update) {
            var that = this,
                column = header.cellIndex,
                sortFunction = caseInsensitiveSort,
                item = '',
                items = [],
                i = that.thead ? 0 : 1,
                sortMethod = header.getAttribute('data-sort-method'),
                sortOrder = header.getAttribute('aria-sort');

            that.table.dispatchEvent(createEvent('beforeSort'));

            // If updating an existing sort, direction should remain unchanged.
            if (!update) {
                if (sortOrder === 'ascending') {
                    sortOrder = 'descending';
                } else if (sortOrder === 'descending') {
                    sortOrder = 'ascending';
                } else {
                    sortOrder = that.options.descending ? 'descending' : 'ascending';
                }

                header.setAttribute('aria-sort', sortOrder);

                // JVE Added below.
                var headerDiv = $(header).find("div");
                headerDiv.find(".ra-datagrid-sortaria").remove();
                if (sortOrder == 'ascending')
                    headerDiv.append("<span class='ra-datagrid-sortaria'>&#9650;</span>");
                else
                    headerDiv.append("<span class='ra-datagrid-sortaria'>&#9660;</span>");
            }

            if (that.table.rows.length < 2) return;

            // If we force a sort method, it is not necessary to check rows
            if (!sortMethod) {
                while (items.length < 3 && i < that.table.tBodies[0].rows.length) {
                    item = getInnerText(that.table.tBodies[0].rows[i].cells[column]);
                    item = item.trim();

                    if (item.length > 0) {
                        items.push(item);
                    }

                    i++;
                }

                if (!items) return;
            }

            for (i = 0; i < sortOptions.length; i++) {
                item = sortOptions[i];

                if (sortMethod) {
                    if (item.name === sortMethod) {
                        sortFunction = item.sort;
                        break;
                    }
                } else if (items.every(item.pattern)) {
                    sortFunction = item.sort;
                    break;
                }
            }

            that.col = column;

            for (i = 0; i < that.table.tBodies.length; i++) {
                var newRows = [],
                    noSorts = {},
                    j,
                    totalRows = 0,
                    noSortsSoFar = 0;

                if (that.table.tBodies[i].rows.length < 2) continue;

                for (j = 0; j < that.table.tBodies[i].rows.length; j++) {
                    item = that.table.tBodies[i].rows[j];
                    if (item.getAttribute('data-sort-method') === 'none') {
                        // keep no-sorts in separate list to be able to insert
                        // them back at their original position later
                        noSorts[totalRows] = item;
                    } else {
                        // Save the index for stable sorting
                        newRows.push({
                            tr: item,
                            td: getInnerText(item.cells[that.col]),
                            index: totalRows
                        });
                    }
                    totalRows++;
                }
                // Before we append should we reverse the new array or not?
                // If we reverse, the sort needs to be `anti-stable` so that
                // the double negatives cancel out
                if (sortOrder === 'descending') {
                    newRows.sort(stabilize(sortFunction, true));
                } else {
                    newRows.sort(stabilize(sortFunction, false));
                    newRows.reverse();
                }

                // append rows that already exist rather than creating new ones
                for (j = 0; j < totalRows; j++) {
                    if (noSorts[j]) {
                        // We have a no-sort row for this position, insert it here.
                        item = noSorts[j];
                        noSortsSoFar++;
                    } else {
                        item = newRows[j - noSortsSoFar].tr;
                    }

                    // appendChild(x) moves x if already present somewhere else in the DOM
                    that.table.tBodies[i].appendChild(item);
                }
            }

            updateDataGridRowColors($(that.table)); // JVE Added.
            that.table.dispatchEvent(createEvent('afterSort'));
        },

        refresh: function () {
            if (this.current !== undefined) {
                this.sortTable(this.current, true);
            }
        }
    };

    if (typeof module !== 'undefined' && module.exports) {
        module.exports = Tablesort;
    } else {
        window.Tablesort = Tablesort;
    }
})();

// JVE: tablesort.date.js
// Basic dates in dd/mm/yy or dd-mm-yy format.
// Years can be 4 digits. Days and Months can be 1 or 2 digits.
(function () {
    var parseDate = function (date) {
        date = date.replace(/\-/g, '/');
        date = date.replace(/(\d{1,2})[\/\-](\d{1,2})[\/\-](\d{2})/, '$1/$2/$3'); // format before getTime

        return new Date(date).getTime() || -1;
    };

    Tablesort.extend('date', function (item) {
        return (
            item.search(/(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\.?\,?\s*/i) !== -1 ||
            item.search(/\d{1,2}[\/\-]\d{1,2}[\/\-]\d{2,4}/) !== -1 ||
            item.search(/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/i) !== -1
        ) && !isNaN(parseDate(item));
    }, function (a, b) {
        a = a.toLowerCase();
        b = b.toLowerCase();

        return parseDate(b) - parseDate(a);
    });
}());

// JVE: tablesort.number.js
(function () {
    var cleanNumber = function (i) {
        return i.replace(/[^\-?0-9.]/g, '');
    },

        compareNumber = function (a, b) {
            a = parseFloat(a);
            b = parseFloat(b);

            a = isNaN(a) ? 0 : a;
            b = isNaN(b) ? 0 : b;

            return a - b;
        };

    Tablesort.extend('number', function (item) {
        return item.match(/^[-+]?[£\x24Û¢´€]?\d+\s*([,\.]\d{0,2})/) || // Prefixed currency
            item.match(/^[-+]?\d+\s*([,\.]\d{0,2})?[£\x24Û¢´€]/) || // Suffixed currency
            item.match(/^[-+]?(\d)*-?([,\.]){0,1}-?(\d)+([E,e][\-+][\d]+)?%?$/); // Number
    }, function (a, b) {
        a = cleanNumber(a);
        b = cleanNumber(b);

        return compareNumber(b, a);
    });
}());