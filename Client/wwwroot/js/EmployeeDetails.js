$("#tbodyEmployee").DataTable({
    ajax: {
        url: "https://localhost:7242/api/Employee/details",
        dataSrc: "data",
        dataType: "JSON"
    },
    columns: [
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + 1;
            }
        },
        { data: "nik" },
        { data: "fullName" },
        { data: "birthDate" },
        { data: "gender" },
        { data: "hiringDate" },
        { data: "email" },
        { data: "phoneNumber" },
        { data: "major" },
        { data: "degree" },
        { data: "gpa" },
        { data: "university" },
        {
            data: null, //kolom ini berisi 2 button untuk update dan delete. masing2 tombol membawa guid dari masing2 baris data
            render: function (data, type, row, meta) {
                return ` <div class="btn-group" role="group"> 
                                <button type="button" data-target="#modalUpdate" onclick="PreUpdateForm('${data.guid}')" class="btn btn-primary mr-3 rounded" data-toggle="modal">Update</button>
                                <button type="button" onclick="DeleteEmployee('${data.guid}')" class="btn btn-danger rounded" data-toggle="modal" data-target="#modalDelete">Delete</button>
                        </div>`;
            }
        }
       
    ]

});




