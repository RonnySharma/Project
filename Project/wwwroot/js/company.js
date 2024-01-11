var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tbData').DataTable({
        "ajax": {
            "url": "/Adimen/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetaddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneno", "width": "15%" },
            /*   { "data": "postalCode", "width": "15%" },*/
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data)
                        return `<input type = "checkbox" checked disabled /> `;
                    else
                        return `<input type = "checkbox" disabled />`;
                }
            },
            {

                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                        <a href ="/Adimen/Company/upsert/${data}" class="btn btn-info">
                        <i class="fas fa-edit"></i>
                        </a>
                        <a class="btn btn-danger" onclick=Delete("/Adimen/Company/Delete/${data}")>
                        <i class="fas fa-trash-alt"></i>
                        </a>
                        </div>
                    
                     `;
                }

            }
        ]
    })
}
//function Delete(url) {
//    /*alert(url);*/
//    swal({
//        title: "Want to delete Data?",
//        text: "Delete Information !!!!",
//        buttons: true,
//        icon: "Warning",

//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                url: url,
//                type: "Delete",
//                success: function (data) {
//                    if (data.success) {
//                        toastr.success(data.message);
//                        dataTable.ajax.reload();

//                    }
//                    else {
//                        toastr.error(data.message);
//                    }
//                }

//            })
//        }
//    })
//}