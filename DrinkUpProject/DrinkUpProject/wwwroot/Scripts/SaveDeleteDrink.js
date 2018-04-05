$(document).ready(function () {
    $("#SaveDeleteButton").click(function () {

        let id = getDrinkId();
        alert($("#SaveDeleteButton").val());
        if ($("#SaveDeleteButton").val() === "True") {
            doAjaxCallUpdate(id, false);
        }
        else if ($("#SaveDeleteButton").val() === "False") {
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
                $("#SaveDeleteButton").val("True");
                $("#SaveDeleteButton").text("Remove From Drink List");
            }
            else {
                $("#SaveDeleteButton").val("False");
                $("#SaveDeleteButton").text("Save To Drink List");
            }
            alert("Updated");
        }
    });

}
