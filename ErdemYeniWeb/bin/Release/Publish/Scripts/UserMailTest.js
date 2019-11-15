var mailTest = function () {

    $.ajax({
        type: "POST",
        url: '/Admin/MailTest',
        data: null,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {//typeof response.hasError === "undefined"
                alert("Mail Atıldı.");
            }
            else {
                alert("Hata : " + response.message);
            }
        },
        error: function (response) {
            alert(response);
        }
    })
}