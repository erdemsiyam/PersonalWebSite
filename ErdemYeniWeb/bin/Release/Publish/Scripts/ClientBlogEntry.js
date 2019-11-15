var sendReply = function (seoUrlparam) {
    var seoUrl = seoUrlparam.toString();
    var name = $("#name").val();
    var email = $("#email").val();
    var message = $("#message").val();
    var isWantFollow = ($("#isWantFollow").is(":checked")) ? true : false;

    var formData = new FormData;
    formData.append("seoUrl", seoUrl);
    formData.append("name", name);
    formData.append("email", email);
    formData.append("message", message);
    formData.append("isWantFollow", isWantFollow);
    $.ajax({
        type: "POST",
        url: '/Blog/SendReply/1',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {
                alert("Your messages sended, when its verifery, its will show at this page.");
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