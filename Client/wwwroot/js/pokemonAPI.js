// Mengambil data dari API dengan URL tertentu
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
    // success: (result) => {
    //    console.log(result);
    // }
}).done((result) => { //jika get datanya berhasil disimpan di result
    console.log(result); 
    let temp = ""; //menampung data yang akan ditampilkan dalam tabel 
    // Loop melalui hasil data API
    $.each(result.results, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button type="button" onclick="detail('${val.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
                </tr>`;
    });
    // Menampilkan hasil data di dalam tabel
    $("#tbodyPoke").html(temp);
}).fail((error) => {
    console.log(error);
});

// Fungsi untuk menampilkan detail Pokemon
function detail(stringUrl) {
    // Mengambil data detail Pokemon dengan URL tertentu
    $.ajax({
        url: stringUrl
    }).done((res) => {
        // Menampilkan judul modal dengan nama Pokemon
        $(".modal-title").text(res.name);
        // Menampilkan gambar Pokemon
        $(".pokeImage").html(`<img src="${res.sprites.other.dream_world.front_default}" alt="${res.name}">`);
        let pokeType = "";
        // Loop melalui jenis-jenis Pokemon
        $.each(res.types, (key, val) => {
            pokeType += typeColor(val.type.name);
        });
        // Menampilkan jenis-jenis Pokemon
        $(".pokeType").html(pokeType);
        let pokeAbility = "";
        // Loop melalui kemampuan-kemampuan Pokemon
        $.each(res.abilities, (index) => {
            //Menangambil objek abilities berdasarkan index dengan nama objek ability.name
            pokeAbility += `<h6 class="text-capitalize mt-3">- ${res.abilities[index].ability.name}</h6>`;
        });
        // Menampilkan kemampuan-kemampuan Pokemon
        $(".pokeAbility").html(pokeAbility);
        let statNameValue = "";
        // Loop melalui statistik Pokemon
        $.each(res.stats, (index) => {
            let statName = res.stats[index].stat.name;
            let baseStat = res.stats[index].base_stat;
            let progressBarClass = "";

            if (baseStat >= 90) {
                progressBarClass = "bg-success"; // Warna hijau untuk baseStat di atas atau sama dengan 90
            } else if (baseStat >= 60) {
                progressBarClass = "bg-warning"; // Warna kuning untuk baseStat di atas atau sama dengan 60
            } else {
                progressBarClass = "bg-danger"; // Warna merah untuk baseStat di bawah 60
            }

            statNameValue += `<div class="row mt-3">
                        <div class="col-6 text-left text-capitalize font-weight-bold">${statName}</div>
                    </div>
                    <div class="progress max-auto">
                        <div class="progress-bar ${progressBarClass}" role="progressbar" style="width: ${baseStat}%" aria-valuenow="${baseStat}" aria-valuemin="0" aria-valuemax="100">
                            <span class="sr-only">${baseStat}%</span>
                        </div>
                    </div>`;
        });

        $(".statNameValue").html(statNameValue);

        // Menampilkan statistik Pokemon
        $(".statNameValue").html(statNameValue);

        // Menampilkan beberapa gerakan Pokemon (maksimal 5)
        let maxMovesToShow = 5; // Ubah sesuai dengan jumlah gerakan yang ingin ditampilkan
        let moveList = "";
        $.each(res.moves.slice(0, maxMovesToShow), (index, move) => {
            moveList += `<h6 class="text-capitalize mt-3">- ${move.move.name}</h6>`;
        });
        $(".moveList").html(moveList);
    }).fail((error) => {
        console.log(error);
    });
}

// Fungsi untuk memberi warna berdasarkan jenis Pokemon
function typeColor(type) {
    const typeColors = {
        "normal": "text-white",
        "fighting": "bg-danger text-white",
    };
    return `<h6 class="${typeColors[type]}rounded-sm p-1 mr-1">- ${type}</h6>`;
}
