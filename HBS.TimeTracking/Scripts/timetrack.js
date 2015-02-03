/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-1.8.2-vsdoc.js" />
/// <reference path="jquery-ui-1.8.2.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="knockout-2.0.0.debug.js" />
/// <reference path="modernizr-2.0.6-development-only.js" />


$(document).ready(function () {

    $("table th:last-child").css("border-right", "0px");
    $("table td:last-child").css("border-right", "0px");
    $("table tr:last-child td").css("border-bottom", "0px");

    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    });
    //$("#combobox").combobox();

    $(":input[data-datepicker]").datepicker();

    //hide all update and cancel buttons
    $("a[data-updateHourlyRecord]").hide();
    $("a[data-cancelHourlyRecord]").hide();
    $("a[data-deleteHourlyRecord]").hide();

    $("a[data-editHourlyRecord]").unbind("click");
    $("a[data-editHourlyRecord]").click(function () {
        var parentRow = $(this).closest("tr[data-editRow]");
        parentRow.find("#spnClockInTime").hide("fast");
        parentRow.find("#txtClockInTime").show("slow");
        parentRow.find("#spnClockOutTime").hide("fast");
        parentRow.find("#txtClockOutTime").show("slow");
        parentRow.find("a[data-updateHourlyRecord]").show("slow");
        parentRow.find("a[data-cancelHourlyRecord]").show("slow");
        parentRow.find("a[data-deleteHourlyRecord]").show("slow");
        parentRow.find("a[data-editHourlyRecord]").hide("fast");
    });
    $("a[data-cancelHourlyRecord]").unbind("click");
    $("a[data-cancelHourlyRecord]").click(function () {
        var parentRow = $(this).closest("tr[data-editRow]");
        parentRow.find("#spnClockInTime").show("slow");
        parentRow.find("#txtClockInTime").hide("slow");
        parentRow.find("#spnClockOutTime").show("slow");
        parentRow.find("#txtClockOutTime").hide("fast");
        parentRow.find("a[data-cancelHourlyRecord]").hide("fast");
        parentRow.find("a[data-updateHourlyRecord]").hide("fast");
        parentRow.find("a[data-deleteHourlyRecord]").hide("fast");
        parentRow.find("a[data-editHourlyRecord]").show("slow");
    });

    $("a[data-deleteHourlyRecord]").unbind("click");
    $("a[data-deleteHourlyRecord]").click(function () {

        var result = confirm("Are you sure you want to delete this record?");

        if (result) {
            var parentRow = $(this).closest("tr[data-editRow]");

            var timeTrackId = parentRow.find("#dailytimeTrack_TimeTrackId").val();
            
            //var url = "/TimeTrack/DeleteTimeTrackRecord";
            var url = location.protocol + "//" + location.host + "/TimeTrack/DeleteTimeTrackRecord";
            
            $.post(url, { timeTrackId: timeTrackId }, function (data) {
                if (data) {
                    parentRow.fadeOut(500, function () {
                        parentRow.remove();
                    });
                }
            });

        }
    });


    $("a[data-updateHourlyRecord]").unbind("click");
    $("a[data-updateHourlyRecord]").click(function () {
        var parentRow = $(this).closest("tr[data-editRow]");
        var timeTrackId = parentRow.find("#dailytimeTrack_TimeTrackId").val();
        var timeTrackStampDate = parentRow.find("#dailytimeTrack_TimeTrackStampDate").val();
        var timeTrackSelectedUser = parentRow.find("#dailytimeTrack_SelectedUser").val();
        var clockInTime = parentRow.find("#txtClockInTime").val();
        var clockOutTime = parentRow.find("#txtClockOutTime").val();

        //var url = "/TimeTrack/UpdateClockInOutTime";
        var url = location.protocol + "//" + location.host + "/TimeTrack/UpdateClockInOutTime";

        $.post(url, { timeTrackId: timeTrackId, stampDate: timeTrackStampDate, selectedUser: timeTrackSelectedUser, clockInTime: clockInTime, clockOutTime: clockOutTime }
            , function (data) {
                if (data.ErrorMessage.length != 0) {
                    alert(data.ErrorMessage);
                }
                else {
                    parentRow.find("#spnClockInTime").text(data.TimeTrack.ClockInTimeDisplay);
                    parentRow.find("#spnClockOutTime").text(data.TimeTrack.ClockOutTimeDisplay);
                    parentRow.find("#txtClockInTime").val(data.TimeTrack.ClockInTimeForJs);
                    parentRow.find("#txtClockOutTime").val(data.TimeTrack.ClockOutTimeForJs);

                    parentRow.find("#spnClockInTime").show("fast");
                    parentRow.find("#txtClockInTime").hide("slow");
                    parentRow.find("#spnClockOutTime").show("fast");
                    parentRow.find("#txtClockOutTime").hide("slow");

                    parentRow.find("a[data-updateHourlyRecord]").hide("fast");
                    parentRow.find("a[data-editHourlyRecord]").show("slow");
                    parentRow.find("a[data-cancelHourlyRecord]").hide("fast");
                    parentRow.find("a[data-deleteHourlyRecord]").hide("fast");
                }
            }, "json");
    });

        BindWeekListEvent();
    if($("td:empty").length>0)
        $("td:empty").html("&nbsp;");
    if($("th:empty").length > 0)
        $("th:empty").html("&nbsp;");
});

function BindWeekListEvent() {
    var options = {
        target: '#resultDiv',   // target element(s) to be updated with server response 
        beforeSubmit: showRequest,  // pre-submit callback 
        success: showResponse  // post-submit callback 
    };

    //http://jquery.malsup.com/form/
    // When the value of the dropdown changes force an ajax submit
    $('#weeklist').change(function () {
        $("#frmWeekList").ajaxSubmit(options);
        return false;
    });
}

function showRequest(formData, jqForm, options) {
    $("#weekListProgress").show();
    return true;
}
//// post-submit callback 
function showResponse(responseText, statusText, xhr, $form) {
    $("#weekListProgress").hide();
}

(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var input,
                    that = this,
                    select = this.element.hide(),
                    selected = select.children(":selected"),
                    value = selected.val() ? selected.text() : "",
                    wrapper = this.wrapper = $("<span>")
                        .addClass("ui-combobox")
                        .insertAfter(select);

            function removeIfInvalid(element) {
                var value = $(element).val(),
                        matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(value) + "$", "i"),
                        valid = false;
                select.children("option").each(function () {
                    if ($(this).text().match(matcher)) {
                        this.selected = valid = true;
                        return false;
                    }
                });
                if (!valid) {
                    // remove invalid value, as it didn't match anything
                    $(element)
                            .val("")
                            .attr("title", value + " didn't match any item")
                            .tooltip("open");
                    select.val("");
                    setTimeout(function () {
                        input.tooltip("close").attr("title", "");
                    }, 2500);
                    input.data("autocomplete").term = "";
                    return false;
                }
            }

            input = $("<input>")
                    .appendTo(wrapper)
                    .val(value)
                    .attr("title", "")
                    .addClass("ui-state-default ui-combobox-input")
                    .autocomplete({
                        delay: 0,
                        minLength: 0,
                        source: function (request, response) {
                            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                            response(select.children("option").map(function () {
                                var text = $(this).text();
                                if (this.value && (!request.term || matcher.test(text)))
                                    return {
                                        label: text.replace(
                                            new RegExp(
                                                "(?![^&;]+;)(?!<[^<>]*)(" +
                                                $.ui.autocomplete.escapeRegex(request.term) +
                                                ")(?![^<>]*>)(?![^&;]+;)", "gi"
                                            ), "<strong>$1</strong>"),
                                        value: text,
                                        option: this
                                    };
                            }));
                        },
                        select: function (event, ui) {
                            ui.item.option.selected = true;
                            that._trigger("selected", event, {
                                item: ui.item.option
                            });
                        },
                        change: function (event, ui) {
                            if (!ui.item)
                                return removeIfInvalid(this);
                        }
                    })
                    .addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li>")
                        .data("item.autocomplete", item)
                        .append("<a>" + item.label + "</a>")
                        .appendTo(ul);
            };

            $("<a>")
                    .attr("tabIndex", -1)
                    .attr("title", "Show All Items")
                    .tooltip()
                    .appendTo(wrapper)
                    .button({
                        icons: {
                            primary: "ui-icon-triangle-1-s"
                        },
                        text: false
                    })
                    .removeClass("ui-corner-all")
                    .addClass("ui-corner-right ui-combobox-toggle")
                    .click(function () {
                        // close if already visible
                        if (input.autocomplete("widget").is(":visible")) {
                            input.autocomplete("close");
                            removeIfInvalid(input);
                            return;
                        }

                        // work around a bug (likely same cause as #5265)
                        $(this).blur();

                        // pass empty string as value to search for, displaying all results
                        input.autocomplete("search", "");
                        input.focus();
                    });

            input
                        .tooltip({
                            position: {
                                of: this.button
                            },
                            tooltipClass: "ui-state-highlight"
                        });
        },

        destroy: function () {
            this.wrapper.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);