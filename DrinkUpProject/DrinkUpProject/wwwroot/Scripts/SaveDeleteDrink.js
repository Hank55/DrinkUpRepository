$(document).ready(function () {
    $(function () {
        if ($("#SaveDeleteButton").val() === "true") {
            $("#SaveDeleteButton").text("Remove From Drink List");
        }
        else {
            $("#SaveDeleteButton").text("Save To Drink List");
        }
    });

    $("#SaveDeleteButton").click(function () {
        let id = getDrinkId();
        if ($("#SaveDeleteButton").val() === "true") {
            doAjaxCallUpdate(id, false);
        }
        else if ($("#SaveDeleteButton").val() === "false") {
            doAjaxCallUpdate(id, true);
        }
    });
});


function getDrinkId() {

    var pathname = window.location.pathname;
    var id = pathname.substring(pathname.lastIndexOf('/') + 1);
    return id;
}

function doAjaxCallUpdate(id, isAdd) {
    $.ajax({
        url: "/updatedrinklist",
        type: "POST",
        data: { "drinkId": id, "isAdd": isAdd },
        success: function (result) {
            if (isAdd) {
                $("#SaveDeleteButton").val("true");
                $("#SaveDeleteButton").text("Remove From Drink List");
            }
            else {
                $("#SaveDeleteButton").val("false");
                $("#SaveDeleteButton").text("Save To Drink List");
            }
        }
    });

}
