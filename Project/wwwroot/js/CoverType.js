var dataTable;
$(document).ready(function () {
    loadDataTable();
})
    function loadDataTable() {
        dataTable = $('#tbldata').DataTable({
            "ajax": {
                "url":"/Adimen/covertype/Getall"
            },
            "columns": [
                { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
<a href="/Adimen/covertype/Upsert/${data}"class="btn btn-info"><i class="fas fa-edit"></i></a>
<a class="btn btn-danger" onclick=Delete("/Adimen/covertype/delete/${data}")><i class="fas fa-trash-alt"></i></a>
</div>
`

                }
                }        ]
        })
}
function Delete(url) {
    swal({
        title: "want to delete data?",
        text: "delete Information",
        button: true,
        icon: "warning",
        dangerModel: true
    }).then((willdata) => {
        if (willdata) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload()
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}