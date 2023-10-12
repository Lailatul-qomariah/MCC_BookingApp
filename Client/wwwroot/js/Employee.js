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
        { data: "hiringDate" },
        { data: "email" },
        { data: "phoneNumber" },
        { data: "major" },
        { data: "degree" },
        { data: "gpa" },
        { data: "university"},

    ]

});