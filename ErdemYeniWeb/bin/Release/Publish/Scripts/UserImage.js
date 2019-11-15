
$(document).ready(function () {
    $("#bigPictureFile").change(function () {
        var File = this.files;
        if (File && File[0]) { ReadImage(File[0], 'big'); }
    })
    $("#smallPictureFile").change(function () {
        var File = this.files;
        if (File && File[0]) { ReadImage(File[0], "small"); }
    })
})

var ReadImage = function (file, sizeParam) {
    var reader = new FileReader;
    var image = new Image;
    var bigOrSmall = sizeParam.toString();
    reader.readAsDataURL(file);

    reader.onload = function (_file) {
        image.src = _file.target.result;
        image.onload = function () {
            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = ~~~(file.size / 1024) + "KB";
            if (bigOrSmall == 'big') {
                $("#bigPictureTarget").attr('src', _file.target.result);
                $("#bigPictureDescription").text("Size:" + size + ", " + height + "X " + width + ", " + type);
                $("#bigPicturePreview").show();
            }
            else if (bigOrSmall == 'small') {
                $("#smallPictureTarget").attr('src', _file.target.result);
                $("#smallPictureDescription").text("Size:" + size + ", " + height + "X " + width + ", " + type);
                $("#smallPicturePreview").show();
            }

        }
    }
}

var ClearPreview = function (size) {
    var bigOrSmall = size.toString();
    if (bigOrSmall == "big") {
        $("#bigPictureFile").val('');
        $("#bigPictureDescription").text('');
        $("#bigPicturePreview").hide();
    }
    else if (bigOrSmall == "small") {
        $("#smallPictureFile").val('');
        $("#smallPictureDescription").text('');
        $("#smallPicturePreview").hide();
    }

}

var bigPictureUpload = function () {
    var file = $("#bigPictureFile").get(0).files;
    var data = new FormData;
    data.append("image", file[0]);
    data.append("type", "big");
    $.ajax({
        type: "POST",
        url: '/Admin/UserPictureUpload',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (typeof response.hasError === "undefined") {
                $("#bigPictureUploaded").html('');
                //ya Server'dan al
                $("#bigPictureUploaded").append('<img src="/Images/User/' + response + '" />');
                //yada zaten seçmiştik onu al oraya ekle.
                ClearPreview("big");
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
var smallPictureUpload = function () {
    var file = $("#smallPictureFile").get(0).files;
    var data = new FormData;
    data.append("image", file[0]);
    data.append("type", "small");
    $.ajax({
        type: "POST",
        url: '/Admin/UserPictureUpload',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (typeof response.hasError === "undefined") {
                $("#smallPictureUploaded").html('');
                //ya Server'dan al
                $("#smallPictureUploaded").append('<img src="/Images/User/' + response + '" />');
                //yada zaten seçmiştik onu al oraya ekle.
                ClearPreview("small");
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