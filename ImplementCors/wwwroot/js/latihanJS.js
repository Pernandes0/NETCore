/* const animals = [
    {
        name: "Fluppy", species: "cat", class:{ name: "mamalia" }
    },
    {
        name: "Nemo", species: "dog", class: { name: "vertebrata" }
    },
    {
        name: "Ursa", species: "cat", class: { name: "mamalia" }
    },
    {
        name: "Balmon", species: "dog", class: { name: "vertebrata" }
    },
    {
        name: "Tukul", species: "fish", class: { name: "pisces" }
    },
    {
        name: "Sugra", species: "cat", class: { name: "mamalia" }
    }
]
// dog otomatis classnya ditimpa menjadi nonmamalia
console.log(animals.length);
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "dog") {
        animals[i].class.name = "non mamalia";
    }
}
console.log(animals);
// looping copy ke array baru untuk species cat
var onlycat = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        onlycat.push(animals[i]);
    }
}
console.log(onlycat)
*/
// ajax parameternya objek
/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result);
    var text = " ";
    $.each(result.results, function (key, val) {
        // text += `<li>`+ val.name +`</li>`
        text += `<tr>
                <td>${key + 1}</td>
                <td>${val.name}</td>
                <td>${val.url}</td>
                <td><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#pokeapi" onclick="detail('${val.url}')">
                Detail
                </button><td></tr>`;
    });
    // $("#swapi").html(text);
    $("#pokeapis").html(text);
}).fail((result) => {
    console.log(ressult);
});

function detail(url) {
    console.log(url);
    $.ajax({
        url: url
    }).done((result) => {
        var type = " ";
        for (var i = 0; i < result.types.length; i++) {
            if (result.types[i].type.name == "grass") {
                type += `<span class="badge badge-pill badge-success">Grass</span>`;
            } else if (result.types[i].type.name == "poison"){
                type += `<span class="badge badge-pill badge-warning">Poison</span>`
            } else if (result.types[i].type.name == "fire") {
                type += `<span class="badge badge-pill badge-danger">Fire</span>`
            } else if (result.types[i].type.name == "flying") {
                type += `<span class="badge badge-pill badge-light">Flying</span>`
            } else if (result.types[i].type.name == "fighting") {
                type += `<span class="badge badge-pill badge-primary">Fighting</span>`
            } else if (result.types[i].type.name == "ground") {
                type += `<span class="badge badge-pill badge-info">Ground</span>`
            }
        }
        var abilities = " ";
        for (var i = 0; i < result.abilities.length; i++) {
            abilities += `<li>${result.abilities[i].ability.name}</li>`;
        }
        var text = " ";
        text += `<center><img src="${result.sprites.other.dream_world.front_default}" alt="Ini Gambar" />
                <p>${type}</p></center>
                <p>Name : ${result.name}</p>
                <p>Abilities : ${abilities}</p>
                `;
        // $("#swapi").html(text);
        $("#detailPokemon").html(text);
    }).fail((result) => {
        console.log(ressult);
    });
}*/

$(document).ready(function () {
    $('#dataTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1, 2]
                },
                bom: true,
                id: "expExcel"
            }
            ,
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2]
                },
                bom: true,
                id: "expPdf"
            }
        ],
        "filter": true,
        "ajax": {
            "url": 'Person/GetPersonVM',
            "datatype": 'json',
            "dataSrc": ''
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
                "orderable": false 
            },
            {
                "data": "nik"
            },
            {
                "data":"fullName"
            },
            {
                "render": function (data, type, row) {
                    return `
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#person" onclick="detailPerson('${row["nik"]}')">
                            Detail
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal"  onclick="deletes('${row["nik"]}')">Delete</button>`;
                },
                "orderable": false 
            }
        ],
    });
});

function detailPerson(nik) {
    $.ajax({
        url: '/Person/GetNIK/' + nik,
        method: 'GET'
    }).done((result) => {
        console.log(result)
        var gender;
        //for (var i = 0; i < result.length; i++) {
        if (result[0].gender == 0) {
                gender = "Female";
        } else if (result[0].gender == 1) {
                gender = "Male";
            }
        //}
        var text = " ";
        text += `
                        <div class="containerData">
                        <div class="row">
                            <div class="col-sm-3">Fullname</div>
                            <div class="col-sm-1">:</div>
                            <div class="col-sm-5">${result[0].fullName}</div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">Birthdate</div>
                            <div class="col-sm-1">:</div>
                            <div class="col-sm-5">${result[0].birthDate.substr(0, 10)}</div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">Degree</div>
                            <div class="col-sm-1">:</div>
                            <div class="col-sm-5">${result[0].degree}</div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">GPA</div>
                            <div class="col-sm-1">:</div>
                            <div class="col-sm-5">${result[0].gpa}</div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">Salary</div>
                            <div class="col-sm-1">:</div>
                            <div class="col-sm-5"> Rp ${result[0].salary}</div>
                        </div>
                    </div>
          `;
        // for (var i = 0; i < result.length; i++) {
            //if (result.nik == nik) {
            //    text += `
            //    NIK : ${result[0].nik}
            //    Nama Lengkap : ${result[0].fullName}
            //    Gender : ${gender}
            //    Phone : +62 ${result[0].phone.substr(1)}
            //    Tanggal Lahir : ${result[0].birthDate}
            //    Salary : RP. ${result[0].salary}
            //    E-Mail : ${result[0].email}
            //    UniversityId : ${result[0].universityId}`;
            //}
        //}
        $("#persondetail").html(text);
    }).fail((result) => {
        console.log(result);
    });
}
$("#btnsubmit").click(e => {
    e.preventDefault();
    var obj = {
        "NIK": $('#nik').val(),
        "FullName": $('#nama').val(),
        "Phone": $('#phone').val(),
        "BirthDate": $('#date').val(),
        "Salary": $('#salary').val(),
        "Email": $('#email').val(),
        "Gender": parseInt($('#gender').val()),
        "Password": $('#pass').val(),
        "UniversityId": parseInt($('#universityid').val()),
        "Degree": $('#degree').val(),
        "GPA": $('#gpa').val(),
    };

    console.log(JSON.stringify(obj));
    // data = JSON.stringify(obj);
    // post data to database
    insert(obj);
});
function insert(data) {
    debugger;
    console.log(data);
    $.ajax({
        url: '/Person/InsertPerson',
        type: 'POST',
        dataType: 'json',
        // contentType: 'application/json',
        data: data
    }).done((result) => {
        debugger;
        //buat alert pemberitahuan jika 
        //alert('SUCCESS')
        Swal.fire(
            'Registration Success!',
            'You clicked the button!',
            'success'
        )
        $('#dataTable').DataTable().ajax.reload();
        //idmodal di hide
        //$('#Register').modal('hide');

        //reload only datatable
        //setInterval(function () {
        //    table.ajax.reload(null, false); // user paging is not reset on reload
        //}, 0);
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        //alert('ERROR')
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })

    });
}
function deletes(data) {
    $.ajax({
        url: `https://localhost:44384/API/Person/${data}`,
        method: 'DELETE',
        dataType: 'json',
        contentType: 'application/json',
    }).done((result) => {
        Swal.fire({
            title: 'Success',
            text: 'Data Terhapus',
            icon: 'succes',
            confirmButtonText: 'Next'
        })
    }).fail((result) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Something went wrong!',
        })
    });
}

// Chart
$(document).ready(function () {
    $.ajax({
        url: "https://localhost:44384/API/Person/GetPersonVM",
        type: "GET"
    }).done((result) => {
        console.log(result);
        var female = result.filter(data => data.gender === 1).length;
        var male = result.filter(data => data.gender === 0).length;
        console.log(male);
        var options = {
            series: [female, male],
            colors: ['#fc6cba', '#1346f7'],
            chart: {
                type: 'donut'
            },
            labels: ['female', 'male'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 50
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }],
            plotOptions: {
                pie: {
                    expandOnClick: true
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#chartgender"), options);
        chart.render();
    }).fail((error) => {
        Swal.fire({
            title: 'Error!',
            text: 'Data Cannot Deleted',
            icon: 'Error',
            confirmButtonText: 'Next'
        })
    });
});
