console.log("detail")

//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon"
//}).done((result) => {
//    console.log(result.results);
//    let temp = "";
//    $.each(result.results, (key, val) => {
//        temp += `<tr>
//                    <td>${key + 1}</td>
//                    <td>${val.name}</td>
//                    <td><button onclick="detail('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPoke" class="btn btn-primary">Detail</button></td>
//                </tr>`;
//    })
//    $("#tbodyPokemon").html(temp);
//})


$(document).ready(function () {

    $(`#employeeTable`).DataTable({

        ajax: {
            url: "https://localhost:7294/api/employees",
            dataType: "JSON",
            dataSrc: "data"

        },

        dom: 'Bfrtip',
        buttons: [
            {
                extend:'colvis',
                title: 'Colvis',
                text: 'Column Visibility'
            },
            {
                extend: 'excelHtml5',
                title: 'Excel',
                text: 'Export to excel',
                /*Columns to export*/
                exportOptions: {
                    columns: ':visible'
                 }
            },
            {
                extend: 'pdfHtml5',
                title: 'PDF',
                text: 'Export to PDF',
                /*Columns to export*/
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'print',
                title: 'Print',
                text: 'Print Table',
                /*Columns to export*/
                exportOptions: {
                    columns: ':visible'
                }
            }
        ],

        columns: [

            {
                data: 'no',
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {data: "nik"},

            {
                data: "fullName",
                render: function (data, type, row) {
                    return row.firstName +" "+ row.lastName;
                }
            },
            {
                data: 'birthDate',
                render: function (data, type, row){
                    return moment(data).format("DD MMMM YYYY");
                }
            },
            {data: 'gender'},
            {
                data: 'hiringDate',
                render: function (data, type, row) {
                    return moment(data).format("DD MMMM YYYY");
                }
            },
            { data: "email" },
            { data: "phoneNumber" },
          
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="deleteEmployee('${row.Guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ]
    });
});

function addEmployee() {
    let firstName = $("#firstName").val();
    let lastName = $("#lastName").val();
    let birthDate = $("#birthDate").val();
    let gender = $("#input[name=gender]:checked").val();
    let genderEnum;
    if (gender == "Female") {
        genderEnum = 0;
    } else
    {
        genderEnum = 1;
    }
    let hiringDate = $("hiringDate").val();
    let email = $("email").val();
    let phone = $("phoneNumber").val();

    let data =
    {
        firstName: firstName,
        lastName: lastName,
        birthDate: birthDate,
        gender: genderEnum,
        hiringDate: hiringDate,
        email: email,
        phone: phone
    };
    $.ajax({
        url: "https://localhost:7294/api/employees",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringfy(data),
    }).done((result) =>
    {
        alert("Data has been inserted")
    }).fail((error) => {
        alert("Failed to insert data. Please Try Again")
    })

}

function deleteEmployee(Guid)
{
    Swal.fire({
        title: 'Are You Sure?',
        text: 'Changes cant be reverted!',
        icon: 'warning',
        showCancelButton: true,
        confirmationButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Delete Data'
    }).then((result) => {
        $.ajax({
            url: "https://localhost:7294/api/employees/?guid=" + Guid,
            type: "DELETE",
        }).done((result) => {
            Swal.fire(
                'Deleted',
                'Your Data Has Been Succesfully Deleted',
                'Success'
            ).then(() => {
                location.reload();
            }).fail((error) => {
                alert("Failed to delete data. Please Try Again!")
            });

        });

    });
}


function detail(stringURL) {
    $.ajax({
        url: stringURL
        
    }).done(res => {
        $(".modal-title").html(res.name);
        $(".pokemon-img").attr("src", res.sprites.front_default);

        let types = "";
        res.types.forEach((type) => {
            let typeColor = getTypeColor(type.type.name)
            types += `<span class = "badge rounded-pill" style = "background-color: ${typeColor}" ">${type.type.name}</span>`;
        });
        $(".pokemon-type").html(types);

        let abilities = "";
        res.abilities.forEach((ability) => {
            abilities += `<li class = "list-group-item "> ${ability.ability.name}</li>`

        });
        $(".pokemon-abilities").html(abilities);

        $("#hp").css("width", res.stats[0].base_stat + "%").html("HP : " + res.stats[0].base_stat);
        $("#attack").css("width", res.stats[1].base_stat + "%").html("Attack : " + res.stats[1].base_stat);
        $("#defense").css("width", res.stats[2].base_stat + "%").html("Defense : " + res.stats[2].base_stat);
        $("#sattack").css("width", res.stats[3].base_stat + "%").html("Spesial Attack : " + res.stats[3].base_stat);
        $("#sdefense").css("width", res.stats[4].base_stat + "%").html("Spesial Defense : " + res.stats[4].base_stat);
        $("#speed").css("width", res.stats[5].base_stat + "%").html("Speed : " + res.stats[5].base_stat);
        console.log(res);
    })

    
}
