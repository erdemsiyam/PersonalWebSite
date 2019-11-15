var createBlog = function () {

    var selectedTags = [];
    $("#Tags li").each(function () {
        var input = $(this).children("input");
        if (input[0].checked) {
            selectedTags.push(input.attr('value'));
        }
    })
    var title = $("#Title").val();
    var summary = $("#Summary").val();
    var blogContentEncrypt = window.btoa(unescape(encodeURIComponent(CKEDITOR.instances.Content.getData()))); // encrpty ile gönderilir. DomString kabul etmiyor
    var titlePicture = $("#TitlePicture").get(0).files;
    var selectedPicTagId = $('#selectedPicTagId').find(":selected").val();
    //sending
    var data = new FormData;
    data.append("titlePicture", titlePicture[0]);
    data.append("selectedPicTagId", selectedPicTagId);
    data.append("title", title);
    data.append("summary", summary);
    data.append("selectedTags", selectedTags);
    data.append("blogContentEncrypt", blogContentEncrypt);
    $.ajax({
        type: "POST",
        url: '/Admin/BlogAdd',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (typeof response.hasError === "undefined") {
                alert("New Blog Added");
                $("#Tags li").each(function () {
                    var input = $(this).children("input");
                    input[0].checked = false;
                })
                $("#Title").val('');
                $("#Summary").val('');
                $("#TitlePicture").val('');
                CKEDITOR.instances.Content.setData('');
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