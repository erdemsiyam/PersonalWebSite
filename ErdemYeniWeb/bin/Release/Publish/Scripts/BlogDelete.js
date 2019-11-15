var deleteBlog = function (id) {

    var formData = new FormData;
    formData.append("blogId", id);
    $.ajax({
        type: "POST",
        url: '/Admin/BlogDelete',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {
                $("#" + id).remove();
                alert("Deleted");
            }
            else {
                alert("Error : " + response.message);
            }
        },
        error: function (response) {
            alert(response);
        }
    })
}