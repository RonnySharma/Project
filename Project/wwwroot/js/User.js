var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tbldata').DataTable(
        {
            'ajax': {
                "url":"/adimen/user/getall"
            },
            "columns": [
                { "data": "name", "width": "15%" },
                { "data": "email", "width": "15%" },
                { "data": "streetAddress", "width": "15%" },
                { "data": "company.name", "width": "15%" },
                { "data": "role", "width": "15%" },
                { "data": "id", "width": "10%" },
                {
                    "data": {
                        id: "id", lockoutEnd: "lockoutEnd"
                    },
                    "render": function (data) {
                        var today = new Date().getTime();
                        var lockout = new Date(data.lockoutEnd).getTime();
                        if (lockout > today)
                        {
                            return `
                             <div class="text-center">
                            <a class="btn btn-danger" onclick=LockUnLock("${data.id}")>UnLock</a>

                             </div>`
                        }
                        else
                        { 
                            return `<div class=text-center>
                            <a class="btn btn-info" onclick=LockUnLock("${data.id}")>Lock</a>
                           </div>`;
                        }
                    }
                }
              


                ]
            }
        )
}
function LockUnLock(id) {
    $.ajax({
        url: "/adimen/user/LockUnLock",
        type: "POST",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                if (data.message = "Locked") {
                    toastr.error(data.message)
                }
                else {
                    toastr.success(data.message)
                }
                dataTable.ajax.reload()
            }
        }

    })
}