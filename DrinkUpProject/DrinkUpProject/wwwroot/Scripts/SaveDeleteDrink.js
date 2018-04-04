$(document).ready(function () {
    $("#SaveDeleteButton").click(function () {
        //alert("It's happening!");
        let id = getDrinkId();
        if ($("#SaveDeleteButton").val() === "Remove") {
            doAjaxCallUpdate(id, false);
        }
        else if ($("#SaveDeleteButton").val() === "Save") {
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
                $("#SaveDeleteButton").val("Remove");
                $("#SaveDeleteButton").text("Remove From Drink List");
                alert("You SAVED it. Maybe. It's javascript...");
            }
            else {
                $("#SaveDeleteButton").val("Save");
                $("#SaveDeleteButton").text("Save To Drink List");
                alert("You deleted it. Maybe. It's javascript...");
            }

        }
    });

}
