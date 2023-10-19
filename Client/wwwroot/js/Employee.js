//inisiasi datatable
$("#tbodyEmployee").DataTable({
    dom: 'Bfrtip', //buat nampilin tombol copy, print dll dari data tabelnya
    ajax: {
        url: "https://localhost:7242/api/Employee", //url api kita
        dataSrc: "data", // properti yang berisi data dalam respons JSON
        dataType: "JSON"
    },
    columns: [ //inisiasi columns yang akan ditampilkan di data tabel nya 
        {
            data: null,
            render: function (data, type, row, meta) {
                return meta.row + 1;
            }

        },
        { data: "nik" },
        { data: "firstName" },
        { data: "lastName" },
        { data: "birthDate" },
        {
            data: "gender",
            render: function (data, type, row) {
                return row.gender == "0" ? "Perempuan" : "Laki-Laki";
            }
        },
        { data: "hiringDate" },
        { data: "email" },
        { data: "phoneNumber" },
        {
            data: null, //kolom ini berisi 2 button untuk update dan delete. masing2 tombol membawa guid dari masing2 baris data
            render: function (data, type, row, meta) {
                return ` <div class="btn-group" role="group"> 
                                <button type="button" data-target="#modalUpdate" onclick="PreUpdateForm('${data.guid}')" class="btn btn-primary mr-3 rounded" data-toggle="modal">Update</button>
                                <button type="button" onclick="DeleteEmployee('${data.guid}')" class="btn btn-danger rounded" data-toggle="modal" data-target="#modalDelete">Delete</button>
                        </div>`;
            }
        }

    ],
    buttons: [
        {
            extend: 'copyHtml5', //inisiasi button copy
            className: 'btn btn-warning',
            exportOptions: {
                columns: [0, ':visible']
            }
        },
        {
            extend: 'excelHtml5', //button cetak to excel 
            className: 'btn btn-success',
            exportOptions: {
                columns: ':visible' //kalo mau dipilih beberapa, dikasih index aja
            }
        },
        {
            extend: 'pdfHtml5', //button cetak to pdf
            orientation: 'landscape',
            pageSize: 'LEGAL',
            className: 'btn btn-danger',
            exportOptions: {
                columns: ':visible'
            }
        },
        {
            extend: 'colvis', //button colvis untuk mengatur kolom mana yang akan diikutkan untuk dicetak 
            className: 'btn btn-primary',
        },
        {
            text: 'Create Data', //tombol create data 
            className: 'btn btn-info',
            action: function (e, dt, button, config) { //menambahkan action untuk datanya
                console.log('Button clicked');
                $('#modalCreate').modal('show'); //nampilin modalCreate ketika tombolnya di klik
            },
        }
    ]
});
$('.dt-buttons').removeClass('dt-buttons'); //menghapus class dt-buttons, biar tampilan dom nya itu bisa dicustom 


//ini kodingan dari tombol 'insert' yang ada atribut onclick="Insert()"
function Insert() {
    //inisiasi variabel yang akan diisi oleh value dari form inputan
    var firstName = $("#firstName").val();
    var lastName = $("#lastName").val();
    var birthDate = new Date($("#birthDate").val()); //diconvert ke objek biar bisa dijadikan perbandingan 
    var gender = parseInt($("#gender").val());
    var hiringDate = $("#hiringDate").val();
    var email = $("#email").val();
    var phoneNumber = $("#phoneNumber").val();

    // Validasi apakah semua kolom yang wajib diisi telah diisi
    if (firstName === "" || lastName === "" || birthDate === "" || gender === "" || hiringDate === "" || email === "" || phoneNumber === "") {
        Swal.fire({
            title: 'Data Inputan Tidak Boleh Kosong',
            icon: 'info',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        })
        return; // Hentikan create data jika validasi gagal
    };


    // convert tanggal 1 Januari 2005 ke objek Date
    var comparisonDate = new Date(2005, 0, 1);

    if (birthDate > comparisonDate) { //cek apakah inputan user > dari 2005?
        Swal.fire({
            title: 'Format Birth Date Salah !',
            icon: 'info',
            html: 'Minimal usia 18 tahun',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        });
        return;
    }

    var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/; //pattern untuk email
    if (!email.match(emailRegex)) { //cek apakah inputan user sesuai dengan patternnya
        Swal.fire({
            title: 'Format Email Salah !',
            icon: 'info',
            html: 'ex. ria@mail.com',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        });
        return;
    };
    // Validasi nomor handphone menggunakan regex
    var phoneRegex = /^[0-9]{10,16}$/; //pattern no hp harus angka dan maksimal 16, min 10
    if (!phoneNumber.match(phoneRegex)) {
        Swal.fire({
            title: 'Format No Hp Salah !',
            icon: 'info',
            html: 'ex. 0293874826',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText:
                '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        })
        return;
    };
    //bikin objek untuk insert data dari inputan ke dalam api -> db
    var obj = new Object();
    obj.firstName = firstName;
    obj.lastName = lastName;
    obj.birthDate = birthDate;
    obj.gender = gender;
    obj.hiringDate = hiringDate;
    obj.email = email;
    obj.phoneNumber = phoneNumber;

    $.ajax({
        url: "https://localhost:7242/api/Employee", //ini link api untuk update
        type: "POST", //type API  nya
        data: JSON.stringify(obj),  //mengubah format objek ke bentuk JSON
        contentType: "application/json", //mengatur agar konten yang dikirimkan ke api nya itu berbentuk json
        dataType: "json", //tipe data respons dari server akan berupa data JSON.
        headers: {
            Accept: 'application/json;charset=utf-8', //klien (browser) ingin menerima respons dalam format JSON dengan pengkodean karakter UTF-8.
            contentType: 'application/json;charset=utf-8' //konten yang dikirim dalam permintaan adalah JSON dengan pengkodean karakter UTF-8
        }
    }).done((result) => { //jika data berhasil diambil
        /*console.log(result);*/
        $('#tbodyEmployee').DataTable().ajax.reload();
        $('#modalCreate').modal('hide');
        Swal.fire({
            title: 'Insert Data Success!',
            icon: 'success'
        });
    }).fail((error) => { //jika data gagal diambil
        if (error.status >= 400 && error.status < 500) { //jika error status pada api >=400
            Swal.fire({
                icon: 'error',
                title: 'Kesalahan pada inputan',
                text: 'Perhatikan aturan content inputan',
            });
        } else if (error.status >= 500) { //jika error status dari api itu 500 atau lebbih 
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Terjadi error pada sistem',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            });
        }
    });
};

function DeleteEmployee(guid) {
    // Menggunakan Sweet Alert untuk konfirmasi delete data 
    Swal.fire({
        title: "Apakah Anda yakin?",
        text: "Anda tidak akan dapat mengembalikan data ini!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Ya, hapus!",
        cancelButtonText: "Batal"
    }).then((result) => {
        if (result.isConfirmed) { //jika pilih button ya
            $.ajax({
                url: `https://localhost:7242/api/Employee/${guid}`,
                type: "DELETE",
                contentType: "application/json",
                headers: {
                    Accept: 'application/json;charset=utf-8',
                    contentType: 'application/json;charset=utf-8'
                }
            }).done(function () {
                $('#tbodyEmployee').DataTable().ajax.reload();
                Swal.fire({
                    title: 'Deleted Data Success!',
                    icon: 'success'
                });

                // Lakukan tindakan lanjutan jika perlu, seperti memperbarui tabel atau tampilan.
            }).fail(function (error) {
                Swal.fire("Error!", "Gagal menghapus data: " + error, "error");

                // Tampilkan pesan kesalahan jika gagal menghapus data.
            });
        }
    });
};


// Variabel untuk menyimpan guid dan nik
var employeeId;
var employeeNik;

function PreUpdateForm(guid) {

    // Mengambil data employee dari API berdasarkan GUID
    $.ajax({
        url: `https://localhost:7242/api/Employee/${guid}`,
        type: 'GET',
    }).done((result) => {
        //menampung nik dan guid kedalam variabel untuk digunakan pada function update nanti
        employeeId = guid;
        employeeNik = empData.nik;
        // Mengisi data karyawan ke dalam modal update
        $("#firstNameUpdate").val(empData.firstName);
        $("#lastNameUpdate").val(empData.lastName);
        $("#birthDateUpdate").val(empData.birthDate);
        $("#genderUpdate").val(empData.gender.toString());
        $("#hiringDateUpdate").val(empData.hiringDate);
        $("#emailUpdate").val(empData.email);
        $("#phoneNumberUpdate").val(empData.phoneNumber);

    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!'
        });
    });
}

function UpdateEmployee() {
    //inisiasi variabel untuk berdasarkan value dari form inputan 
    var firstName = $("#firstNameUpdate").val();
    var lastName = $("#lastNameUpdate").val();
    var birthDate = new Date($("#birthDateUpdate").val());
    var gender = parseInt($("#genderUpdate").val());
    var hiringDate = $("#hiringDateUpdate").val();
    var email = $("#emailUpdate").val();
    var phoneNumber = $("#phoneNumberUpdate").val();

    //validasi untuk form inputan apakah kosong 
    if (firstName === "" || lastName === "" || birthDate === "" || gender === "" || hiringDate === "" || email === "" || phoneNumber === "") {
        Swal.fire({
            title: 'Data Inputan Tidak Boleh Kosong',
            icon: 'info',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        })
        return; // Hentikan create data jika validasi gagal
    };

    //validasi untuk birthdate tidak boleh lebih dari 2005
    var comparisonDate = new Date(2005, 0, 1);
    if (birthDate > comparisonDate) {
        Swal.fire({
            title: 'Format Birth Date Salah !',
            icon: 'info',
            html: 'Minimal usia 18 tahun',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        });
        return;
    }
    // Validasi format email menggunakan regex
    var emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!email.match(emailRegex)) {
        Swal.fire({
            title: 'Format Email Salah !',
            icon: 'info',
            html: 'ex. ria@mail.com',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText: '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        });
        return;
    };
    // Validasi nomor handphone menggunakan regex
    var phoneRegex = /^[0-9]{0,16}$/;
    if (!phoneNumber.match(phoneRegex)) {
        Swal.fire({
            title: 'Format No Hp Salah !',
            icon: 'info',
            html: 'ex. 0293874826',
            showCloseButton: true,
            focusConfirm: false,
            confirmButtonText:
                '<i class="fa fa-thumbs-up"></i> Great!',
            confirmButtonAriaLabel: 'Thumbs up, great!',
        })
        return;
    };

    //bikin objek untuk update data dari form inputan ke dalam api -> db
    var obj = new Object();
    // mengisi guid dan nik berdassarkan value guid dan nik yang telah disimpan dalam variabel global diatas
    obj.guid = employeeId;
    obj.nik = employeeNik;
    obj.firstName = firstName;
    obj.lastName = lastName;
    obj.birthDate = birthDate;
    obj.gender = gender;
    obj.hiringDate = hiringDate;
    obj.email = email;
    obj.phoneNumber = phoneNumber;

    /*console.log(JSON.stringify(obj))*/

    $.ajax({
        url: 'https://localhost:7242/api/Employee',
        type: 'PUT',
        data: JSON.stringify(obj),
        contentType: 'application/json',
    }).done((result) => {
        console.log(result);
        $('#tbodyEmployee').DataTable().ajax.reload();
        Swal.fire({
            title: 'Update Data Success!',
            icon: 'success'
        });
        $('#modalUpdate').modal('hide');
    }).fail((error) => {
        console.log(error);
        if (error.status >= 400 && error.status < 500) {
            Swal.fire({
                icon: 'error',
                title: 'Kesalahan pada inputan',
                text: 'Perhatikan aturan content inputan',
            });
        } else if (error.status >= 500) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Terjadi error pada sistem',
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!',
            });
        }
    });

};





function showDeleteConfirmation(guid) {
    console.log(guid)
    Swal.fire({
        title: 'Konfirmasi Penghapusan',
        text: 'Anda yakin ingin menghapus item ini?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Ya, Hapus!',
        cancelButtonText: 'Batal'
    }).then((result) => {
        if (result.isConfirmed) {
            // Jika pengguna mengonfirmasi penghapusan, maka lakukan penghapusan dengan mengirimkan permintaan DELETE.
            fetch(`/Employee/RemoveEmployee?id=${guid}`, {
                method: 'DELETE'
            })
                .then(response => {
                    if (response.ok) {
                        // Penghapusan berhasil. Anda dapat mengambil tindakan sesuai dengan keberhasilan penghapusan.
                        Swal.fire({
                            title: 'Deleted Data Success!',
                            icon: 'success'
                        });
                    } else {
                        // Penanganan kesalahan jika penghapusan gagal.
                        Swal.fire({
                            title: 'Deleted Data FAILED!',
                            icon: 'error'
                        });
                    }
                });
        }
    });
}


