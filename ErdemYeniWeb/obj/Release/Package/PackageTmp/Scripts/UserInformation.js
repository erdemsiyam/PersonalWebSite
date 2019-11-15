function informationAdd(whichOne) {
    var which = whichOne.toString();
    var object;

    switch (which) {
        case "Skill":
            object = { Name: $("#newSkillName").val(), Percent: $("#newSkillPercent").val() };
            break;
        case "Education":
            object = { SchoolName: $("#newEducationSchoolName").val(), Degree: $("#newEducationDegree").val(), Location: $("#newEducationLocation").val(), StartDate: $("#newEducationStartDate").val(), EndDate: $("#newEducationEndDate").val() };
            break;
        case "Experience":
            object = { CompanyName: $("#newExperienceCompanyName").val(), Degree: $("#newExperienceDegree").val(), Location: $("#newExperienceLocation").val(), StartDate: $("#newExperienceStartDate").val(), EndDate: $("#newExperienceEndDate").val() };
            break;
        case "Project":
            object = { Title: $("#newProjectTitle").val(), Summary: $("#newProjectSummary").val(), AddDate: $("#newProjectAddDate").val(), GithubLink: $("#newProjectGithubLink").val() };
            break;
    }

    //sending
    var formData = new FormData;
    formData.append("which", which);
    formData.append("jsonObject", JSON.stringify(object)); // we transing object to json string...
    $.ajax({
        type: "POST",
        url: '/Admin/UserInformationAdd',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {//typeof response.hasError === "undefined"
                // tabloya yeni satır ekle
                switch (which) {
                    case "Skill":
                        $('#skillTable > tbody > tr').eq($('#skillTable > tbody > tr').length - 1).before('<tr id="' + response.Id + '"><td></td><td>' + object.Name + '</td><td>' + object.Percent + '</td><td><a class="btn btn-danger btn-xs" onclick="informationDelete(\'Skill\',\'' + response.Id + '\')"><i class="icon-trash "></i></a></td></tr>');
                        $("#newSkillName").val("");
                        $("#newSkillPercent").val("");
                        break;
                    case "Education":
                        $('#educationTable > tbody > tr').eq($('#educationTable > tbody > tr').length - 1).before('<tr id="' + response.Id + '"><td></td><td>' + object.SchoolName + '</td><td>' + object.Degree + '</td><td>' + object.Location + '</td><td>' + object.StartDate + '</td><td>' + object.EndDate + '</td><td><a class="btn btn-danger btn-xs" onclick="informationDelete(\'Education\',\'' + response.Id + ' \')"><i class="icon-trash "></i></a></td></tr>');
                        $("#newEducationSchoolName").val("");
                        $("#newEducationDegree").val("");
                        $("#newEducationLocation").val("");
                        $("#newEducationStartDate").val("");
                        $("#newEducationEndDate").val("");
                        break;
                    case "Experience":
                        $('#experienceTable > tbody > tr').eq($('#experienceTable > tbody > tr').length - 1).before('<tr id="' + response.Id + '"><td></td><td>' + object.CompanyName + '</td><td>' + object.Degree + '</td><td>' + object.Location + '</td><td>' + object.StartDate + '</td><td>' + object.EndDate + '</td><td><a class="btn btn-danger btn-xs" onclick="informationDelete(\'Experience\',\'' + response.Id + ' \')"><i class="icon-trash "></i></a></td></tr>');
                        $("#newExperienceCompanyName").val("");
                        $("#newExperienceDegree").val("");
                        $("#newExperienceLocation").val("");
                        $("#newExperienceStartDate").val("");
                        $("#newExperienceEndDate").val("");
                        break;
                    case "Project":
                        $('#projectTable > tbody > tr').eq($('#projectTable > tbody > tr').length - 1).before('<tr id="' + response.Id + '"><td></td><td>' + object.Title + '</td><td>' + object.Summary + '</td><td>' + object.AddDate + '</td><td>' + object.GithubLink + '</td><td><a class="btn btn-danger btn-xs" onclick="informationDelete(\'Project\',\'' + response.Id + '\')"><i class="icon-trash "></i></a></td></tr>');
                        $("#newProjectTitle").val("");
                        $("#newProjectSummary").val("");
                        $("#newProjectAddDate").val("");
                        $("#newProjectGithubLink").val("");
                        break;
                }
                alert("New " + which + " Added");
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

function informationDelete(whichOne, theId) {
    var which = whichOne.toString();
    var id = theId.toString();

    //sending
    var formData = new FormData;
    formData.append("which", which);
    formData.append("id", id);
    $.ajax({
        type: "POST",
        url: '/Admin/UserInformationDelete',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response === null || typeof response.hasError === "undefined") {//typeof response.hasError === "undefined"
                // tablodaki satırı sil
                $("#" + id).remove();
                alert("Selected " + which + " Deleted");
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