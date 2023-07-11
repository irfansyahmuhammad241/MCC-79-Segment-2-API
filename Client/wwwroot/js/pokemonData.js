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

    $(`#mainTable`).DataTable({

        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon/",
            dataType: "JSON",
            dataSrc: "results"

        },

        columns: [

            {
                data: 'url',
                render: function (data, type, row) {
                    let number = data.split('/')[6];
                    return number;
                }
            },

            { data: "name" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="detail('${data.url}')" data-bs-toggle="modal" data-bs-target="#modalPoke" class="btn btn-primary">Detail</button></td>`;
                }
            }
        ]
    });
});


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

    function getTypeColor(typeName)
    {
        let typeColor = "";
        switch (typeName)
        {
            case "water":
                typeColor = "#B0E0E6";
                break;

            case "fire":
                typeColor = "#FF8C00";
                break;

            case "grass":
                typeColor = "#228B22";
                break;

            case "poison":
                typeColor = "#4B0082";
                break;

            case "flying":
                typeColor = "#778899";
                break;

            case "bug":
                typeColor = "#008080";
                break;
            case "normal":
                typeColor = "#696969"
                break;


        }
        return typeColor;

    }
}
