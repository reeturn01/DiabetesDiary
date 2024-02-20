document.addEventListener('DOMContentLoaded', (event) => {
    let medicalTypeSelectEl = document.getElementById('Item_MedicalItemType');
    medicalTypeSelectEl.addEventListener('change', (event) => {
        let currSelectList = event.target;
        let selectedValue = currSelectList.value;
        if (selectedValue === 'Insulin') {
            fetch('/Inventory/AddItem?Handler=InsulinItems')
                .then(response => response.text())
                .then(result => {
                    let json = JSON.parse(result);
                    let medicalItemsSelectEl = document.getElementById('Item_MedicalItemId');
                    medicalItemsSelectEl.innerHTML = "";
                    let defaultOption = document.createElement('option');
                    defaultOption.selected = true;
                    defaultOption.disabled = true;
                    defaultOption.innerText = "-- Izberi medikament --";
                    medicalItemsSelectEl.appendChild(defaultOption);

                    console.log(json);

                    for (var i = 0; i < json.length; i++) {
                        console.log(json[i]);
                        let option = document.createElement('option');
                        option.value = json[i].id;
                        option.innerText = json[i].type + ', ' + json[i].manufacturer + ', ' + json[i].name;
                        medicalItemsSelectEl.appendChild(option);
                    }
                })
        } else if (selectedValue === 'BloodMeasurementStrip') {
            fetch('/Inventory/AddItem?Handler=BloodMeasurementStrips')
                .then(response => response.text())
                .then(result => JSON.parse(result))
                .then(result => {
                    let medicalItemsSelectEl = document.getElementById('Item_MedicalItemId');
                    medicalItemsSelectEl.innerHTML = "";
                    let defaultOption = document.createElement('option');
                    defaultOption.selected = true;
                    defaultOption.disabled = true;
                    defaultOption.innerText = "-- Izberi medikament --";
                    medicalItemsSelectEl.appendChild(defaultOption);

                    for (var i = 0; i < result.length; i++) {
                        let option = document.createElement('option');
                        option.value = result[i].id;
                        let text = result[i].manufacturer + ', ' + result[i].name;
                        option.innerHTML = text;
                        medicalItemsSelectEl.appendChild(option);
                    }
                })
        } else {
            console.log("Greska ne postoi takov tip na medikament !");
            return;
        }
    })
});