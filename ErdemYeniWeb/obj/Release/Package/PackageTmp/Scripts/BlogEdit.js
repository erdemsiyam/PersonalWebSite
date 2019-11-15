var updateBlog = function () {

    var selectedTags = [];
    $("#Tags li").each(function () {
        var input = $(this).children("input");
        if (input[0].checked) {
            selectedTags.push(input.attr('value'));
        }
    })
    var id = $("#id").val();
    var title = $("#Title").val();
    var summary = $("#Summary").val();
    var blogContentEncrypt = window.btoa(CKEDITOR.instances.Content.getData()); // encrpty ile gönderilir. DomString kabul etmiyor
    var titlePicture = $("#TitlePicture").get(0).files;
    var selectedPicTagId = $('#selectedPicTagId option:selected').val();
    //sending
    var data = new FormData;
    data.append("newTitlePicture", titlePicture[0]);
    data.append("selectedPicTagId", selectedPicTagId);
    data.append("id", id);
    data.append("title", title);
    data.append("summary", summary);
    data.append("selectedTags", selectedTags);
    data.append("blogContentEncrypt", blogContentEncrypt);
    $.ajax({
        type: "POST",
        url: '/Admin/BlogEdit',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (typeof response.hasError === "undefined") {
                if (response.isRedirect) {
                    window.location.href = response.redirectUrl;
                }
            }
            else {
                alert(response.message);
            }

        },
        error: function (response) {
            alert(response);
        }
    })
}