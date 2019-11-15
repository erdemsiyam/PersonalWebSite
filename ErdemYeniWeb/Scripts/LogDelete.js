var deleteLogs = function (whichLog) {
    var which = whichLog.toString();
    var formData = new FormData;
    formData.append("which", which);
    $.ajax({
        type: "POST",
        url: '/Admin/DeleteLogs',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response);
        }
    })
}