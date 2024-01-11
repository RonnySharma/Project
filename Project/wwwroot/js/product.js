var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Adimen/Product/getall"
        },
        "columns": [
            { "data": "title", "width": "%15" },
            { "data": "description", "width": "%15" },
            { "data": "author", "width": "%30" },
            { "data": "isbn", "width": "%15" },
            { "data": "price", "width": "%15" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
<a href="/Adimen/Product/Upsert/${data}"class="btn btn-info"><i class="fas fa-edit"></i></a>
<a class="btn btn-danger" onClick=Delete("/Adimen/Product/Delete/${data}")><i class="fas fa-trash-alt"></i></a>
</div>
`
                }
                }
  
          ]
        })
}
function Delete(url) {
    swal({
        title: "want to  delete?",
        text: "delete Information",
        button: true,
        icon: "warning",
        dangerModel: true
    }).then((WillDelete) => {
        if (WillDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}   