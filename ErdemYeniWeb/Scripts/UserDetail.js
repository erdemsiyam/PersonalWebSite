var detailUpload = function (whichDetail) {

    var which = whichDetail.toString();
    var newData = null;
    if (which === "IsAllowAutoMail" || which === "IsAutoMailSslEnable") {
        newData = ($("#" + which).is(":checked")) ? true : false;
    }
    else {
        newData = $("#" + which).val();
    }

    var formData = new FormData;
    formData.append("which", which);
    formData.append("newValue", newData);
    $.ajax({
        type: "POST",
        url: '/Admin/UserDetailUpload',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {//typeof response.hasError === "undefined"
                alert(which + " Updated.");
            }
            else {
                alert("Error : " + response.message);
            }
        },
        error: function (response) {
            alert(response);
        }
    });
}