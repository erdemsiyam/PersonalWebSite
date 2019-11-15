    var commentEditOpen = function (commentIdparam) {
        var id = commentIdparam.toString();
        if ($('#' + id + ' input').length < 1) {

            $('#' + id).children("td").eq(1).append('<input id="newpersonname_' + id + '" placeholder="New PersonName" style="margin-left:5px;"/>');
            $('#' + id).children("td").eq(4).append('<input id="newcontent_' + id + '" placeholder="New Content" style="margin-left:5px;"/>');
            $('#' + id).children("td").eq(5).append('<a id="editsend_' + id + '" class="btn btn-success btn-xs" onclick="commentEditSend(\'' + id + '\')" style="margin-left:5px;"><i class="icon-ok"></i></a><a id="editclose_' + id +'" class="btn btn-danger btn-xs" onclick="commentEditClose(\'' + id + '\')" style="margin-left:5px;"><i class="icon-trash"></i></a>');
        }
        else {
            alert("Already a comment editing now.");
        }

    }

    var commentEditClose = function (commentIdparam) {
        var id = commentIdparam.toString();
        $('#' + id).children("td").eq(1).children("input").remove();
        $('#' + id).children("td").eq(4).children("input").remove();
        $('#' + id).children("td").eq(5).children("#editsend_" + id).remove();
        $('#' + id).children("td").eq(5).children("#editclose_" + id).remove();
    }

    var commentEditSend = function (commentIdparam) {

        var id = commentIdparam.toString();
        var newPersonName = $("#newpersonname_" + id).val();
        var newContent = $("#newcontent_" + id).val();

        var formData = new FormData;
        formData.append("commentId", id);
        formData.append("newPersonName", newPersonName);
        formData.append("newContent", newContent);
        $.ajax({
            type: "POST",
            url: '/Admin/CommentEdit',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response === null || typeof response.hasError === "undefined") {
                    if (newPersonName.trim() != "")
                        $('#' + id).children("td").eq(1).html(newPersonName);
                    else
                        $('#' + id).children("td").eq(1).children("input").remove();
                    if (newContent.trim() != "")
                        $('#' + id).children("td").eq(4).html(newContent);
                    else
                        $('#' + id).children("td").eq(4).children("input").remove();

                    $('#' + id).children("td").eq(5).children("#editsend_" + id).remove();
                    $('#' + id).children("td").eq(5).children("#editclose_" + id).remove();
                    alert("Selected Comment Changed");
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

    var commentVerifery = function (commentIdparam,decisionParam) {
        var id = commentIdparam.toString();
        var decision = (decisionParam.toString() == 'true')

        var formData = new FormData;
        formData.append("commentId", id);
        formData.append("decision", decision);
        $.ajax({
            type: "POST",
            url: '/Admin/CommentVerifery',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response === null || typeof response.hasError === "undefined") {
                    if(decision)
                        $('#' + id).children("td").eq(5).html('<a class="btn btn-info" onclick="commentVerifery(\'' + id +'\',\'false\')">Set Deny</a>');
                    else
                        $('#' + id).children("td").eq(5).html('<a class="btn btn-warning" onclick="commentVerifery(\'' + id + '\',\'true\')">Set Allow</a>');

                    alert("Selected Comment Verifery Changed To " + decision.toString());
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

    var commentDelete = function (commentIdparam) {
        var id = commentIdparam.toString();
        var formData = new FormData;
        formData.append("commentId", id);
        $.ajax({
            type: "POST",
            url: '/Admin/CommentDelete',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response === null || typeof response.hasError === "undefined") {
                    $("#" + id).remove();
                    alert("Selected Comment deleted");
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