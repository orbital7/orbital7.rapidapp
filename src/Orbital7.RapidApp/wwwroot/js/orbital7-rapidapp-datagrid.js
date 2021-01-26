﻿function setupDataGrid(id, tableHeight, updateRowColors) {

    var table = $("#" + id);

    if (tableHeight > 0)
        table.css("height", tableHeight + "px");

    if (updateRowColors)
        updateDataGridRowColors(table);
}

function updateDataGridRowColors(table) {

    var tableBody = table.find("tbody");   // From scrollbar.
    tableBody.children("tr").addClass("ra-datagrid-row");
    tableBody.children("tr").removeClass("ra-datagrid-row-alt");
    tableBody.children("tr:odd").addClass("ra-datagrid-row-alt");
}

//function selectDataGridRow(sourceRow) {

//    var row = $(sourceRow);
//    //var table = row.closest("table");
//    //var selectedRows = table.find(".ra-datagrid-row-selected");
//    //var container = table.closest(".ra-parent");
//    //var multiSelectEditor = container.find(".ra-datagrid-multiselect-toolbar-editor");

//    if (row.hasClass("ra-datagrid-row-selected")) {
//        row.removeClass("ra-datagrid-row-selected");
//    }
//    else {
//        row.addClass("ra-datagrid-row-selected");
//    }

//    //multiSelectEditor.hide();
//    //selectedRows = table.find(".ra-datagrid-row-selected");
//    //if (selectedRows.length >= 1)
//    //    multiSelectEditor.show();
//}

//function createSelectedDataGridItemsList(dataGridId, htmlFieldPrefix, includeNames, nameDivClass) {

//    var table = $("#" + dataGridId);
//    var html = "";
//    var i = 0;
//    table.find(".ra-datagrid-row-selected").each(function () {
//        var row = $(this);
//        html += "<input type='hidden' id='" + htmlFieldPrefix + "[" + i + "]' name='" +
//            htmlFieldPrefix.replace("_", ".") + "[" + i + "]' value='" + row.attr("data-row-id") + "' />";
//        if (includeNames)
//            html += "<div class='" + nameDivClass + "'>" + row.attr("data-row-name") + "</div>";
//        i++;
//    });

//    $("#selectedDataGridItemsList").html(html);
//}

// JVE: TODO: reference tablesort npm package directly and use beforeSort and afterSort event handlers.
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

    var getCellByKey = function (cells, key) {
        return [].slice.call(cells).find(function (cell) {
            return cell.getAttribute('data-sort-column-key') === key;
        });
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
                columnKey = header.getAttribute('data-sort-column-key'),
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
            }

            // JVE: Added below.
            $(that.table).find("thead").find(".ra-datagrid-sortaria").invisible();
            var aria = "&#9650;";
            if (sortOrder === 'ascending')
                aria = "&#9660;";
            $(header).find(".ra-datagrid-sortaria").html(aria).visible();
            //

            // JVE: On Safari, using that.table.rows and that.table.tbodies no longer works once
            // the OverlayScrollbars have been applied. To work around this, manually find tbody.
            var tbody = $(that.table).find("tbody");
            if (tbody[0].rows.length < 2) return;

            // If we force a sort method, it is not necessary to check rows
            if (!sortMethod) {
                var cell;
                while (items.length < 3 && i < tbody[0].rows.length) {
                    if (columnKey) {
                        cell = getCellByKey(tbody[0].rows[i].cells, columnKey);
                    } else {
                        cell = tbody[0].rows[i].cells[column];
                    }

                    // Treat missing cells as empty cells
                    item = cell ? getInnerText(cell) : "";

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

            for (i = 0; i < tbody.length; i++) {
                var newRows = [],
                    noSorts = {},
                    j,
                    totalRows = 0,
                    noSortsSoFar = 0;

                if (tbody[i].rows.length < 2) continue;

                for (j = 0; j < tbody[i].rows.length; j++) {
                    var cell;

                    item = tbody[i].rows[j];
                    
                    if (item.getAttribute('data-sort-method') === 'none') {
                        // keep no-sorts in separate list to be able to insert
                        // them back at their original position later
                        noSorts[totalRows] = item;
                    } else {
                        if (columnKey) {
                            cell = getCellByKey(item.cells, columnKey);
                        } else {
                            cell = item.cells[that.col];
                        }
                        // Save the index for stable sorting
                        newRows.push({
                            tr: item,
                            td: cell ? getInnerText(cell) : '',
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
                    tbody[i].appendChild(item);
                }
            }
            
            updateDataGridRowColors($(that.table), true);   // JVE: Added.
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
        date = date.replace(/(\d{1,2})[\/\-](\d{1,2})[\/\-](\d{2,4})/, '$3-$2-$1'); // format before getTime
        
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