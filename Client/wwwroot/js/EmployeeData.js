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
                    return `<button onclick="ShowUpdate('${row.guid}')" data-bs-toggle="modal" data-bs-target="#modalUpdateEmployee" class="btn btn-primary"> Update </button>` +
                        `   <button onclick="deleteEmployee('${row.guid}')" class="btn btn-secondary"> Delete </button>`;
                }
            }
        ]
    });
});

function addEmployee() {
    //let firstName = $("#firstName").val();
    //let lastName = $("#lastName").val();
    //let birthDate = $("#birthDate").val();
    //let gender = $("input[name=gender]:checked").val();
    //let genderEnum;
    //if (gender == "Female") {
    //    genderEnum = 0;
    //} else
    //{
    //    genderEnum = 1;
    //}
    //let hiringDate = $("#hiringDate").val();
    //let email = $("#email").val();
    //let phone = $("#phoneNumber").val();

    //let data =
    //{
    //    firstName: firstName,
    //    lastName: lastName,
    //    birthDate: birthDate,
    //    gender: genderEnum,
    //    hiringDate: hiringDate,
    //    email: email,
    //    phone: phone
    //};

    let data = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        birthDate: $("#birthDate").val(),
        gender: $("input[name='gender']:checked").val() === "Female" ? 0 : 1,
        hiringDate: $("#hiringDate").val(),
        email: $("#email").val(),
        phoneNumber: $("#phoneNumber").val(),
    };

    $.ajax({
        url: "https://localhost:7294/api/employees",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
    }).done((result) =>
    {
        Swal.fire
        (
            'Data Has Been Successfuly Inserted',
            'Success'
        ).then(() => {
            location.reload();
        })
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops',
            text: 'Failed to insert data, Please Try Again'
        })
    })

}


function deleteEmployee(Guid) {
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

function ShowUpdate(guid) {
    $.ajax({
        url: "https://localhost:7294/api/employees/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        // Mengisi nilai form dengan data yang diterima dari server
        $("#guidUpd").val(result.data.guid);
        $("#nikUpd").val(result.data.nik);
        $("#firstNameUpd").val(result.data.firstName);
        $("#lastNameUpd").val(result.data.lastName);
        let birthDateFormat = moment(result.data.birthDate).format("yyyy-MM-DD");
        $("#birthDateUpd").val(birthDateFormat);
        // Melakukan penyesuaian untuk nilai gender
        if (result.data.gender === 0) {
            $("input[name='gender'][value='Female']").prop("checked", true);
        } else {
            $("input[name='gender'][value='Male']").prop("checked", true);
        }
        let hiringDateFormat = moment(result.data.hiringDate).format("yyyy-MM-DD");
        $("#hiringDateUpd").val(hiringDateFormat);
        $("#emailUpd").val(result.data.email);
        $("#phoneNumberUpd").val(result.data.phoneNumber);

        // Menampilkan modal update data employee
        $("#modalemp2").modal("show");
    }).fail((error) => {
        alert("Failed to fetch employee data. Please try again.");
    });
}

function UpdateEmployee()
{
    
   
    let data = {
        guid: $("#guidUpd").val(),
        nik: $("#nikUpd").val(),
        firstName: $("#firstNameUpd").val(),
        lastName: $("#lastNameUpd").val(),
        birthDate: $("#birthDateUpd").val(),
        gender: $("input[name='gender']:checked").val() === "Female" ? 0 : 1,
        hiringDate: $("#hiringDateUpd").val(),
        email: $("#emailUpd").val(),
        phoneNumber: $("#phoneNumberUpd").val(),
    };
    $.ajax({
        url: "https://localhost:7294/api/employees/",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data)
    }).done((result) => {
        Swal.fire(  //buat alert pemberitahuan jika success
            'Data has been successfully updated!',
            'success'
        ).then(() => {
            location.reload();
        });
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data! Please try again.'
        }) //alert pemberitahuan jika gagal
    })
}

document.addEventListener('DOMContentLoaded', function () {
    const chart = Highcharts.chart('container', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Fruit Consumption'
        },
        xAxis: {
            categories: ['Apples', 'Bananas', 'Oranges']
        },
        yAxis: {
            title: {
                text: 'Fruit eaten'
            }
        },
        series: [{
            name: 'Jane',
            data: [1, 0, 4]
        }, {
            name: 'John',
            data: [5, 7, 3]
        }]
    });
});

