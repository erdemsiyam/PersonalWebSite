function tagAdd() {

    var newTagName = $("#newTagName").val();
    var newTagPic = $("#newTagPic").get(0).files;
    var formData = new FormData;
    formData.append("tagName", newTagName);
    formData.append("tagPic", newTagPic[0]);

    $.ajax({
        type: "POST",
        url: '/Admin/TagAdd',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {
                $('#tagTable > tbody > tr').eq($('#tagTable > tbody > tr').length - 1).before('<tr id="' + response.Id + '"><td></td><td>' + response.Name + '</td><td>' + response.PicturePath + '</td><td><a class="btn btn-primary btn-xs" onclick="tagEditOpen(\'' + response.Id + '\')"><i class="icon-pencil"></i></a> <a class="btn btn-danger btn-xs" onclick="tagDelete(\'' + response.Id + '\')"><i class="icon-trash "></i></a></td></tr>');
                $("#newTagName").val("");
                alert("New Tag \"" + response.Name + "\" Added"); // which adlı parametre newTagName e dönüştürüldü.
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

function tagDelete(tagIdparam) {
    var tagId = tagIdparam.toString();
    var formData = new FormData;
    formData.append("tagId", tagId);
    $.ajax({
        type: "POST",
        url: '/Admin/TagDelete',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined" ) {
                $("#" + tagId).remove();
                alert("Selected Tag deleted");
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

function tagEditOpen(tagIdparam) {
    var tagId = tagIdparam.toString();
    if ($('#' + tagId + ' input').length < 1) {
        //
        $('#' + tagId).children("td").eq(1).append('<input id="new_' + tagId + '" placeholder="New Name" style="margin-left:5px;"/><a class="btn btn-success btn-xs" onclick="tagEditSend(\'' + tagId + '\')" style="margin-left:5px;"><i class="icon-ok"></i></a><a class="btn btn-danger btn-xs" onclick="tagEditClose(\'' + tagId + '\')" style="margin-left:5px;"><i class="icon-trash"></i></a>');
        $('#' + tagId).children("td").eq(2).append('<input id="pic_' + tagId + '" type="file" />');

    }
    else {
        alert("Already a tag editing now.");
    }

}
function tagEditClose(tagIdparam) {
    var tagId = tagIdparam.toString();
    $('#' + tagId).children("td").eq(1).children("input").remove(); // düzenleme kutucuğu sil
    $('#' + tagId).children("td").eq(2).children("input").remove(); // düzenleme resim inputu sil
    $('#' + tagId).children("td").eq(1).children("a").remove(); // düzenleme onay butonunu sil
}
function tagEditSend(tagIdparam) {
    var tagId = tagIdparam.toString();
    var tagNewName = $("#new_" + tagId).val();
    var tagNewPic = $("#pic_" + tagId).get(0).files;
    var formData = new FormData;
    formData.append("tagId", tagId);
    formData.append("tagNewName", tagNewName);
    formData.append("tagNewPic", tagNewPic[0]);
    $.ajax({
        type: "POST",
        url: '/Admin/TagEdit',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {
                tagEditClose(tagId);
                $('#' + tagId).children("td").eq(1).html(response.Name);
                $('#' + tagId).children("td").eq(2).html(response.PicturePath);
                alert("Selected Tag Changed");
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