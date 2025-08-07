//Create your JavaScript code here
$.jsHelper = {
    init: function() {
        // Initialize any required functionality here
    },
    showById: function(id) {
        $("#" + id).show();
    },
    hideById: function (id) {
        $("#" + id).hide();
    },
    showByClass: function (id) {
        $("." + id).show();
    },
    hideByClass: function (id) {
        $("." + id).hide();
    },
    isCheckedById: function (id) {
        return $("#" + id).attr("checked");
    },
    isValueEqualById: function (id, value) {
        return $("#" + id).val() == value;
    },
    addClassById: function (id, className) {
        $("." + id).addClass(className);
    },
    removeClassById: function (id, className) {
        $("." + id).removeClass(className);
    },
};

$(document).ready(function () {
    $.jsHelper.init();
});