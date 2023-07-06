console.log("Latihan Javascript")

function ChangeColor()
{
    var tes1 = document.getElementById("tes1")
    let randomColor = Math.floor(Math.random() * 16777215).toString(16)
    tes1.style.backgroundColor = "#" + randomColor;
  
}

function ChangeTextAndFont()
{
    var fonts = ['Arial', 'Verdana', 'Helvetica', 'Times New Roman', 'Courier New'];
    var tes2 = document.getElementById('tes2');
    var randomIndex = Math.floor(Math.random() * fonts.length);
    tes2.style.fontFamily = fonts[randomIndex];
    tes2.textContent = "BLUE!!!!";
    tes2.style.fontSize = "30px";
}

function ChangeBackgroundColorAndText()
{
    var tes4 = document.getElementById("tes3")
    tes4.style.backgroundColor = "aqua";
    tes4.textContent = "Dipencet!!!";
    let randomColor = Math.floor(Math.random() * 16777215).toString(16);
    tes4.style.backgroundColor = "#" + randomColor; 
    
}

let arrayMhsObj = [
    { nama: "budi", nim: "a112015", umur: 20, isActive: true, fakultas: { name: "komputer" } },
    { nama: "joko", nim: "a112035", umur: 22, isActive: false, fakultas: { name: "ekonomi" } },
    { nama: "herul", nim: "a112020", umur: 21, isActive: true, fakultas: { name: "komputer" } },
    { nama: "herul", nim: "a112032", umur: 25, isActive: true, fakultas: { name: "ekonomi" } },
    { nama: "herul", nim: "a112040", umur: 21, isActive: true, fakultas: { name: "komputer" } },
];

/*
Soal 1 buat sebuah variable 'fakultasKomputer' => yang didalamnya hanya
berisi object dengan fakultas komputer.
*/

let fakultasComputer = [];
for (let i = 0; i < arrayMhsObj.length; i++)
{
    if (arrayMhsObj[i].fakultas.name == "komputer")
    {
        fakultasComputer.push(arrayMhsObj[i]);
    }

    /*
    Soal 2
    jika 2 angka di nim terakhir adalah lebih dari >= 30, maka buat isactive == false.
    */

    let twoDigitOfNim = parseInt(arrayMhsObj[i].nim.slice(-2));

    if (twoDigitOfNim >= 30) {
        arrayMhsObj[i].isActive = false;
    }
}

console.log(fakultasComputer);
console.log(arrayMhsObj);