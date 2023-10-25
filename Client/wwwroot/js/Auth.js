function login() {
    $.ajax({
        url: "/Auth/Login/"
    }).done((res) => {
        console.log(res);
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




