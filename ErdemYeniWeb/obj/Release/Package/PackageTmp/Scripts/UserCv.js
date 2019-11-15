$(document).ready(function () {
    $("#englishCv").change(function () {
        var File = this.files;
        if (File && File[0]) { cvUpload("en"); }
    })
    $("#turkishCv").change(function () {
        var File = this.files;
        if (File && File[0]) { cvUpload("tr"); }
    })
})

var cvUpload = function (lang) {
    var language = lang.toString();
    var file;
    if (language == "tr") { file = $("#turkishCv").get(0).files; }
    else if (language == "en") { file = $("#englishCv").get(0).files; }
    var data = new FormData;
    data.append("cvFile", file[0]);
    data.append("language", language);
    $.ajax({
        type: "POST",
        url: '/Admin/UserCvUpload',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {
                $("#englishCv").val('');
                $("#turkishCv").val('');
                alert(language.toUpperCase() + " Cv Upload Success");
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