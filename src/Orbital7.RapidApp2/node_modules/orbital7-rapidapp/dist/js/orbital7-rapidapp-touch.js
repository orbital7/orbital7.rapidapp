var _raOnCloseTouchDialog = onCloseTouchDialog;
var _raBodyOffsetTop = 80;

function onCloseTouchDialog() {
    
}

function raShowTouchDialog(e, source, contentUrl) {
    
    e.stopPropagation();
    
    var touchInputField = $(source);
    var alreadyOpen = touchInputField.hasClass("ra-touchinput-field-selected");
    raCloseTouchDialog();

    if (!alreadyOpen) {

        touchInputField.addClass("ra-touchinput-field-selected");

        var touchInputFieldValue = touchInputField.find(".ra-touchinput-field-value");
        touchInputFieldValue.addClass("ra-touchinput-field-value-selected");

        var offset = touchInputField.offset();
        var height = touchInputField.height();

        var bgTop = $("#ra-touchdialog-background-top");
        var bgMiddle = $("#ra-touchdialog-background-middle");
        var bgBottom = $("#ra-touchdialog-background-bottom");
        var container = $("#ra-touchdialog-container");
        bgTop.css("height", offset.top - _raBodyOffsetTop - 20 - 10 + "px");
        bgMiddle.css("left", offset.left - 10 + "px");
        bgMiddle.css("top", offset.top - _raBodyOffsetTop - 10 + "px");
        bgMiddle.css("height", height + 20 + "px");
        bgBottom.css("top", offset.top - _raBodyOffsetTop + height + 5 + "px");

        var width = touchInputFieldValue.width();
        bgMiddle.append(touchInputFieldValue);
        touchInputFieldValue.width(width);

        raShowLoading(container);
        bgTop.show();
        bgMiddle.show();
        bgBottom.show();
        container.show();

        $.get({
            url: contentUrl,
            cache: false,
            success: function (response) {
                container.html(response);
                raUpdateBindings();
                //$.validator.unobtrusive.parse(container.find("form"));
            },
            error: function (xhr) {
                if (xhr.responseText)
                    container.html(xhr.responseText);
                else
                    container.html("<strong class='ra-color-error'>ERROR:</strong> Dialog content could not be loaded");
            }
        });
    }
}

function raCloseTouchDialog() {

    _raOnCloseTouchDialog();
    _raOnCloseTouchDialog = onCloseTouchDialog;

    var touchInputField = $(".ra-touchinput-field-selected");
    if (touchInputField.length > 0) {
        
        var touchInputFieldValue = $(".ra-touchinput-field-value-selected");
        touchInputFieldValue.removeClass("ra-touchinput-field-value-selected");

        touchInputField.append(touchInputFieldValue);
        touchInputField.removeClass("ra-touchinput-field-selected");
    }

    $("#ra-touchdialog-background-top").hide();
    $("#ra-touchdialog-background-middle").hide();
    $("#ra-touchdialog-background-bottom").hide();
    $("#ra-touchdialog-container").hide();
}

function raUpdateTouchInputField(
    primaryValueId,
    primaryValue,
    primaryDisplayValueId,
    primaryDisplayValue,
    secondaryValueId,
    secondaryValue,
    secondaryDisplayValueId,
    secondaryDisplayValue,
    imageUrlId,
    imageUrl,
    postEditUpdateScript) {

    var primaryValueElement = $("#" + primaryValueId);
    var displayElement = primaryValueElement.closest(".ra-touchinput-field-value");
    var primaryType = displayElement.attr("data-touchinput-type-primary");
    var secondaryType = displayElement.attr("data-touchinput-type-secondary");

    // Record the primary value.
    primaryValueElement.val(getTouchInputFieldValue(primaryType, primaryValue));
    if (primaryDisplayValueId)
        $("#" + primaryDisplayValueId).val(primaryDisplayValue);

    // Record the secondary value.
    if (secondaryValueId) {
        $("#" + secondaryValueId).val(getTouchInputFieldValue(secondaryType, secondaryValue));
        if (secondaryDisplayValueId)
            $("#" + secondaryDisplayValueId).val(secondaryDisplayValue);
    }

    // Update the display element.
    displayElement.removeClass("ra-touchinput-field-value-content");
    displayElement.removeClass("ra-touchinput-field-value-contentempty");

    // Find the specific value cells and then update by type.
    var leftCell = displayElement.find(".ra-touchinput-field-value-table-cell-left");
    var rightCell = displayElement.find(".ra-touchinput-field-value-table-cell-right");
    if (primaryType === "currency" || primaryType === "integer" || primaryType === "double") {
        setTouchInputFieldCell(primaryType, primaryValue, primaryDisplayValue, displayElement, rightCell, false, 
            displayElement.attr("data-touchinput-minvalue"), displayElement.attr("data-touchinput-maxvalue"));
        setTouchInputFieldCell(secondaryType, secondaryValue, secondaryDisplayValue, displayElement, leftCell, false);
    }
    else {
        setTouchInputFieldCell(primaryType, primaryValue, primaryDisplayValue, displayElement, leftCell, true);
        setTouchInputFieldCell(secondaryType, secondaryValue, secondaryDisplayValue, displayElement, rightCell, false);
    }

    // Clear the image.
    var imageCell = displayElement.find(".ra-touchinput-field-value-table-cell-image");
    imageCell.removeClass("ra-touchinput-field-value-table-cell-image-content");
    imageCell.html("");

    // Set the image.
    if (imageUrl || imageUrlId) {
        
        if (imageUrl) {
            imageCell.html("<img class='ra-touchinput-image' src='" + imageUrl + "' />");
            imageCell.addClass("ra-touchinput-field-value-table-cell-image-content");
        }
        if (imageUrlId) {
            $("#" + imageUrlId).val(imageUrl);
        }
    }

    // Execute the post-edit update script if specified.
    if (postEditUpdateScript)
        eval(postEditUpdateScript);
}

function getTouchInputFieldValue(type, value) {
    if (type === "currency")
        return Number(value);
    else
        return value;
}

function getCurrentValue(value, minValue, maxValue) {

    var valueNumber = Number(value);

    if (minValue) {
        var minValueNumber = Number(minValue);
        if (valueNumber < minValueNumber)
            valueNumber = minValueNumber;
    }

    if (maxValue) {
        var maxValueNumber = Number(maxValue);
        if (valueNumber > maxValueNumber)
            valueNumber = maxValueNumber;
    }

    return valueNumber;
}

function setTouchInputFieldCell(
    type,
    value,
    displayValue,
    displayElement,
    cellElement,
    setPlaceHolder,
    minValue,
    maxValue) {
    
    if (type === "pin") {
        
        if (value !== "") {
            cellElement.addClass("ra-touchinput-field-value-content");
            var mask = "";
            for (i = 0; i < value.length; i++)
                mask += "•";
            cellElement.html(mask);
        }
        else {
            displayElement.addClass("ra-touchinput-field-value-contentempty");
            if (setPlaceHolder)
                cellElement.html("(Enter)");
            else
                cellElement.html("");
        }
    }
    else if (type === "currency") {

        var curValue = getCurrentValue(value, minValue, maxValue);
        if (curValue !== 0) {
            displayElement.addClass("ra-touchinput-field-value-content");
            cellElement.html(curValue.toUSCurrency());
        }
        else {
            displayElement.addClass("ra-touchinput-field-value-contentempty");
            cellElement.html(curValue.toUSCurrency());
        }
    }
    else if (type === "integer" || type === "double") {
        var curValue = getCurrentValue(value, minValue, maxValue);
        if (curValue !== 0) {
            displayElement.addClass("ra-touchinput-field-value-content");
            cellElement.html(curValue);
        }
        else {
            displayElement.addClass("ra-touchinput-field-value-contentempty");
            cellElement.html(curValue);
        }
    }
    else if (type === "select") {
        if (displayValue !== "") {  // if (value !== "") -> problem for Read-Only content.
            displayElement.addClass("ra-touchinput-field-value-content");
            cellElement.html(displayValue);
        }
        else {
            displayElement.addClass("ra-touchinput-field-value-contentempty");
            if (setPlaceHolder)
                cellElement.html("(Select)");
            else
                cellElement.html("");
        }
    }
    else if (type === "phone") {
        if (value !== "") {  // if (value !== "") -> problem for Read-Only content.
            displayElement.addClass("ra-touchinput-field-value-content");
            cellElement.html("<span class='ra-nowrap'>" + value.toPhoneNumber() + "</span>");
        }
        else {
            displayElement.addClass("ra-touchinput-field-value-contentempty");
            if (setPlaceHolder)
                cellElement.html("(***) ***-****");
            else
                cellElement.html("");
        }
    }
}