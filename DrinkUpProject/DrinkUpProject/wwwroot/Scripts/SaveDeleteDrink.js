$(document).ready(function () {
    $("#SaveDeleteButton").click(function () {
        alert("It's happening!");
        if ($("#SaveDeleteButton").val() === "Remove") {

            let id = getDrinkId();
            $("#SaveDeleteButton").val("Save");
            $("#SaveDeleteButton").text("Save To Drink List");
            //doAjaxCallDelete(id);
        }
        if ($("#SaveDeleteButton").val() === "Save") {

            let id = getDrinkId();
            $("#SaveDeleteButton").val("Remove");
            $("#SaveDeleteButton").text("Remove From Drink List");
        //    //doAjaxCallSave(id);
        }
    });
});


function getDrinkId() {

    var pathname = window.location.pathname;
    var id = pathname.substring(pathname.lastIndexOf('/') + 1);
    return id;
}

//function doAjaxCallDelete(id) {
//    var bla = "";
//    $.post("DeleteRecipe/" + id, bla, function () {
//        alert("?");
//    });

//}

//function doAjaxCallSave(id) {
//    $.ajax({
//        url: "/usercontroller/saverecipe/id",
//        type: "POST",
//        success: function (result) {
//            alert("You did it. Maybe. It's javascript...")
//        }
//    });

//}

