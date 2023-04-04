// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var ConfirmEdit = function (id) {
    //$("#editNoteData").modal('show');
    var _id = id;
    $("#editId").val(id);
    $.ajax({
        type: "GET",
        url: '/ViewNotes/EditNotes',
        data: { id: _id },
        success: function (data) {
            $('#editNoteData').modal('show');
        },
    });
    /*$("#editId").val(id);*/
    console.log("THE ID OF THE NOTE IS =" + id);
    /*$('#editNoteData').modal('show');*/


}


$("#updateNote").click(function () {

    var _id = $("#editId").val();
    console.log("THIS IS THE ID = " + _id);
    var editFormData = $("#editForm").serialize();
    console.log("THIS IS THE FORM = " + editFormData);

    $.ajax({
        type: "POST",
        url: '/ViewNotes/EditNotes',
        data: editFormData,
        success: function (data) {
            $("#editNoteData").modal('hide');
            //$("#wrapper").Html(data);
            //$("body").html(result);
            ////$("#confirmDelete").modal('hide');
            //$("#deleteId").val(null);
            setInterval('location.reload()', 7000);
        }

    })

})