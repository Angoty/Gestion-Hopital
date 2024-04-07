function afficherLieu() {
    var input = document.getElementById("rechercheLieu").value;
    var datalist = document.getElementById("optionsList");
    var data = JSON.parse(document.getElementById('dataLieux').value);
    var selectedAdresseId = document.getElementById("selectedAdresseId");

    console.log(data)
    datalist.innerHTML = '';
    data.forEach(function(option) {
        if (option.intitule.toLowerCase().includes(input.toLowerCase())) {
            var newOption = document.createElement('option');
            newOption.value = option.intitule; 
            newOption.setAttribute("data-id", option.idLieu);
            datalist.appendChild(newOption);
        }
    });
    selectedAdresseId.value = "";
    var selectedOption = document.querySelector('#optionsList option[value="' + input + '"]');
    console.log(selectedOption)
    if (selectedOption) {
        selectedAdresseId.value = selectedOption.getAttribute("data-id");
        console.log("valeur: "+selectedAdresseId.value)
    } else {
        selectedAdresseId.value = input;
    }
    if (input === '') {
        datalist.style.display = 'none';
    } else {
        datalist.style.display = 'block';
    }
}

function selectLieu(){
    var input= document.getElementById("rechercheLieu")
   
}
function setCustomValidity(message) {
    var input = document.getElementById("Angoty");
    input.setCustomValidity(message);
}