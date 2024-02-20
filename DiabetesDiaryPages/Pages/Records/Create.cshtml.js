function findSiblingOfElement(elem) {
    let siblingList = [];
    while (elem = elem.nextSibling) {
        if (elem.nodeType === 3) {
            // Text node skip
            continue;
        }
        siblingList.push(elem);
    }
    return siblingList;
};

function addNewFoodItem() {
    let mealDivCnt = document.getElementById('MealRecordsCnt');
    let numFoodItems = mealDivCnt.getElementsByClassName('meal-record-food-item').length;

    let newFoodItemCnt = document.createElement('div');
    newFoodItemCnt.classList.add('row', 'meal-record-food-item');
    newFoodItemCnt.id = `Record_MealRecord_Food_${numFoodItems}_`;

    let newFoodItemSecondLvlCnt = document.createElement('div');
    newFoodItemSecondLvlCnt.classList.add('col-md-5');

    let newLabelForFoodSelectList = document.createElement('label');
    newLabelForFoodSelectList.setAttribute('for', `Record_MealRecord_Food_${numFoodItems}__FoodId`);
    newLabelForFoodSelectList.classList.add('control-label');
    newLabelForFoodSelectList.innerText = 'DA SE PROMENI';

    //<label asp-for="Record.MealRecord.Food[i].FoodId" class="control-label"></label>

    let newFoodSelectList = document.createElement('select');
    newFoodSelectList.id = `Record_MealRecord_Food_${numFoodItems}__FoodId`;
    newFoodSelectList.name = `Record\.MealRecord\.Food\[${numFoodItems}\]\.FoodId`;
    newFoodSelectList.classList.add('form-control');
    newFoodSelectList.setAttribute('data-val', 'true');
    newFoodSelectList.setAttribute('data-val-required', 'The FoodId field is required.');
    let newDefaultOption = document.createElement('option');
    newDefaultOption.innerText = '-- Select Food item --';
    newDefaultOption.disabled = true;
    newDefaultOption.selected = true;
    newFoodSelectList.appendChild(newDefaultOption);

    let newFoodSelectListValidationSpan = document.createElement('span');
    newFoodSelectListValidationSpan.classList.add('text-danger');
    newFoodSelectListValidationSpan.setAttribute('data-valmsg-for', `Record\.MealRecord\.Food\[${numFoodItems}\]\.FoodId`);
    newFoodSelectListValidationSpan.setAttribute('data-valmsg-replace', 'true');


    newFoodItemSecondLvlCnt.appendChild(newLabelForFoodSelectList);
    newFoodItemSecondLvlCnt.appendChild(newFoodSelectList);
    newFoodItemSecondLvlCnt.appendChild(newFoodSelectListValidationSpan);
    newFoodItemCnt.appendChild(newFoodItemSecondLvlCnt);

    let newSecondFoodItemSecondLvlCnt = document.createElement('div');
    newSecondFoodItemSecondLvlCnt.classList.add('col-md-5');

    let newLabelForFoodAmountInput = document.createElement('label');
    newLabelForFoodAmountInput.setAttribute('for', `Record_MealRecord_Food_${numFoodItems}__Amount`);
    newLabelForFoodAmountInput.classList.add('control-label');
    newLabelForFoodAmountInput.innerText = 'DA SE PROMENI';

    let newFoodAmountInput = document.createElement('input');
    newFoodAmountInput.id = `Record_MealRecord_Food_${numFoodItems}__Amount`;
    newFoodAmountInput.name = `Record\.MealRecord\.Food\[${numFoodItems}\]\.Amount`;
    newFoodAmountInput.classList.add('form-control');
    newFoodAmountInput.type = 'number';
    newFoodAmountInput.setAttribute('data-val', 'true');
    newFoodAmountInput.setAttribute('data-val-required', 'The Amount field is required.');

    let newFoodAmountValidationSpan = document.createElement('span');
    newFoodAmountValidationSpan.classList.add('text-danger');
    newFoodAmountValidationSpan.setAttribute('data-valmsg-for', `Record\.MealRecord\.Food\[${numFoodItems}\]\.Amount`);
    newFoodAmountValidationSpan.setAttribute('data-valmsg-replace', 'true');

    newSecondFoodItemSecondLvlCnt.appendChild(newLabelForFoodAmountInput);
    newSecondFoodItemSecondLvlCnt.appendChild(newFoodAmountInput);
    newSecondFoodItemSecondLvlCnt.appendChild(newFoodAmountValidationSpan);

    newFoodItemCnt.appendChild(newSecondFoodItemSecondLvlCnt);

    let newThirdFoodItemSecondLvlCnt = document.createElement('div');
    newThirdFoodItemSecondLvlCnt.classList.add('col-md-2');

    let newRemoveBtn = document.createElement('button');
    newRemoveBtn.id = `RemoveBtn_Record_MealRecord_Food_${numFoodItems}_`;
    newRemoveBtn.type = 'button'
    newRemoveBtn.classList.add('btn', 'btn-outline-danger', 'btn-remove-meal-record');
    newRemoveBtn.innerText = 'Remove';

    newRemoveBtn.addEventListener('click', removeFoodItem);

    newThirdFoodItemSecondLvlCnt.appendChild(newRemoveBtn);
    newFoodItemCnt.appendChild(newThirdFoodItemSecondLvlCnt);

    //<button id="RemoveBtn_Record_MealRecord_Food_@(i)_" type="button" class="btn btn-outline-danger btn-remove-meal-record">Remove</button>

    //<select asp-for="Record.MealRecord.Food[i].FoodId" asp-items="Model.FoodSelectItems" class="form-control"></select>

    let addNewFoodItemButton = document.getElementById('AddFoodBtn');

    mealDivCnt.insertBefore(newFoodItemCnt, addNewFoodItemButton);

    fetch('/Records/Create?handler=FoodItems')
        .then((response) => {
            return response.json();
        }, (reason) => {
            console.log(reason);
        })
        .then((data) => {
            let dataObj = JSON.parse(data);
            newLabelForFoodSelectList.innerText = dataObj['FoodIdDisplayName'];
            newLabelForFoodAmountInput.innerText = dataObj['AmountDisplayName'];

            for (var i = 0; i < dataObj['FoodItems'].length; i++) {
                let newOption = document.createElement('option');
                newOption.value = dataObj['FoodItems'][i]['Id'];
                newOption.innerText = dataObj['FoodItems'][i]['Name'];
                newFoodSelectList.appendChild(newOption);
            }
        });

    let $form = $('form');
    $form.removeData('validator');
    $form.removeData('unobtrusiveValidation');

    $.validator.unobtrusive.parse($form);
}

function addNewInsulinItem() {
    let insulinDivCnt = document.getElementById('InsulinRecordsCnt');
    let numInsulinItems = insulinDivCnt.getElementsByClassName('insulin-record-insulin-administered-item').length;

    let newInsulinItemCnt = document.createElement('div');
    newInsulinItemCnt.classList.add('row', 'insulin-record-insulin-administered-item');
    newInsulinItemCnt.id = `Record_InsulinRecord_InsulinAdministrations_${numInsulinItems}_`;

    let newInsulinItemSecondLvlCnt = document.createElement('div');
    newInsulinItemSecondLvlCnt.classList.add('col-md-5');

    let newLabelForInsulinSelectList = document.createElement('label');
    newLabelForInsulinSelectList.setAttribute('for', `Record_InsulinRecord_InsulinAdministrations_${numInsulinItems}__InsulinId`);
    newLabelForInsulinSelectList.classList.add('control-label');
    newLabelForInsulinSelectList.innerText = 'DA SE PROMENI';

    //<label asp-for="Record.InsulinRecord.InsulinAdministrations[i].InsulinId" class ="control-label"></label>

    let newInsulinSelectList = document.createElement('select');
    newInsulinSelectList.id = `Record_InsulinRecord_InsulinAdministrations_${numInsulinItems}__InsulinId`;
    newInsulinSelectList.name = `Record\.InsulinRecord\.InsulinAdministrations\[${numInsulinItems}\]\.InsulinId`;
    newInsulinSelectList.classList.add('form-control');
    newInsulinSelectList.setAttribute('data-val', 'true');
    newInsulinSelectList.setAttribute('data-val-required', 'The InsulinId field is required.');
    let newDefaultOption = document.createElement('option');
    newDefaultOption.innerText = '-- Select Insulin item --';
    newDefaultOption.disabled = true;
    newDefaultOption.selected = true;
    newInsulinSelectList.appendChild(newDefaultOption);

    let newInsulinSelectListValidationSpan = document.createElement('span');
    newInsulinSelectListValidationSpan.classList.add('text-danger');
    newInsulinSelectListValidationSpan.setAttribute('data-valmsg-for', `Record\.InsulinRecord\.InsulinAdministrations\[${numInsulinItems}\]\.InsulinId`);
    newInsulinSelectListValidationSpan.setAttribute('data-valmsg-replace', 'true');


    newInsulinItemSecondLvlCnt.appendChild(newLabelForInsulinSelectList);
    newInsulinItemSecondLvlCnt.appendChild(newInsulinSelectList);
    newInsulinItemSecondLvlCnt.appendChild(newInsulinSelectListValidationSpan);
    newInsulinItemCnt.appendChild(newInsulinItemSecondLvlCnt);

    let newSecondInsulinItemSecondLvlCnt = document.createElement('div');
    newSecondInsulinItemSecondLvlCnt.classList.add('col-md-5');

    let newLabelForInsulinAmountInput = document.createElement('label');
    newLabelForInsulinAmountInput.setAttribute('for', `Record_InsulinRecord_InsulinAdministrations_${numInsulinItems}__Amount`);
    newLabelForInsulinAmountInput.classList.add('control-label');
    newLabelForInsulinAmountInput.innerText = 'DA SE PROMENI';

    let newInsulinAmountInput = document.createElement('input');
    newInsulinAmountInput.id = `Record_InsulinRecord_InsulinAdministrations_${numInsulinItems}__Amount`;
    newInsulinAmountInput.name = `Record\.InsulinRecord\.InsulinAdministrations\[${numInsulinItems}\]\.Amount`;
    newInsulinAmountInput.classList.add('form-control');
    newInsulinAmountInput.type = 'number';
    newInsulinAmountInput.setAttribute('data-val', 'true');
    newInsulinAmountInput.setAttribute('data-val-required', 'The Amount field is required.');

    let newInsulinAmountValidationSpan = document.createElement('span');
    newInsulinAmountValidationSpan.classList.add('text-danger');
    newInsulinAmountValidationSpan.setAttribute('data-valmsg-for', `Record\.InsulinRecord\.InsulinAdministrations\[${numInsulinItems}\]\.Amount`);
    newInsulinAmountValidationSpan.setAttribute('data-valmsg-replace', 'true');

    newSecondInsulinItemSecondLvlCnt.appendChild(newLabelForInsulinAmountInput);
    newSecondInsulinItemSecondLvlCnt.appendChild(newInsulinAmountInput);
    newSecondInsulinItemSecondLvlCnt.appendChild(newInsulinAmountValidationSpan);

    newInsulinItemCnt.appendChild(newSecondInsulinItemSecondLvlCnt);

    let newThirdInsulinItemSecondLvlCnt = document.createElement('div');
    newThirdInsulinItemSecondLvlCnt.classList.add('col-md-2');

    let newRemoveBtn = document.createElement('button');
    newRemoveBtn.id = `RemoveBtn_Record_InsulinRecord_InsulinAdministrations_${numInsulinItems}_`;
    newRemoveBtn.type = 'button'
    newRemoveBtn.classList.add('btn', 'btn-outline-danger', 'btn-remove-insulin-record');
    newRemoveBtn.innerText = 'Remove';

    newRemoveBtn.addEventListener('click', removeInsulinItem);

    newThirdInsulinItemSecondLvlCnt.appendChild(newRemoveBtn);
    newInsulinItemCnt.appendChild(newThirdInsulinItemSecondLvlCnt);

    //<button id="RemoveBtn_Record_InsulinRecord_InsulinAdministrations_@(i)_" type="button" class="btn btn-outline-danger btn-remove-insulin-record">Remove</button>

    //<select asp-for="Record.InsulinRecord.InsulinAdministrations[i].InsulinId" asp-items="Model.InsulinSelectItems" class="form-control"></select>

    let addNewInsulinItemButton = document.getElementById('AddInsulinBtn');

    insulinDivCnt.insertBefore(newInsulinItemCnt, addNewInsulinItemButton);

    fetch('/Records/Create?handler=InsulinItems')
        .then((response) => {
            return response.json();
        }, (reason) => {
            console.log(reason);
        })
        .then((data) => {
            let dataObj = JSON.parse(data);
            newLabelForInsulinSelectList.innerText = dataObj['InsulinIdDisplayName'];
            newLabelForInsulinAmountInput.innerText = dataObj['AmountDisplayName'];

            for (var i = 0; i < dataObj['InsulinItems'].length; i++) {
                let newOption = document.createElement('option');
                newOption.value = dataObj['InsulinItems'][i]['Id'];
                newOption.innerText = dataObj['InsulinItems'][i]['Name'];
                newInsulinSelectList.appendChild(newOption);
            }
        });

    let $form = $('form');
    $form.removeData('validator');
    $form.removeData('unobtrusiveValidation');

    $.validator.unobtrusive.parse($form);
}

function removeFoodItem(event) {
    let currentBtn = event.target;
    let currentBtnId = currentBtn.id
    let rowToRemoveId = currentBtnId.replace('RemoveBtn_', '');
    let rowToRemove = document.getElementById(rowToRemoveId);
    let rowToRemoveIdIndex = parseInt(rowToRemoveId.match(/\d+/)[0]);
    let rowsOfMealRecordFoodItems = rowToRemove.parentElement.getElementsByClassName('meal-record-food-item');

    for (var mealRecordFoodItemIndex = 0; mealRecordFoodItemIndex < rowsOfMealRecordFoodItems.length; mealRecordFoodItemIndex++) {
        let currentRowIdIndex = parseInt(rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].id.match(/\d+/)[0]);
        if (currentRowIdIndex > rowToRemoveIdIndex) {
            rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].id = rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].id.replace(/Food_\d+_/, `Food_${currentRowIdIndex - 1}_`);
            let currentRowSelects = rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].getElementsByTagName('select');
            for (var selectIndex = 0; selectIndex < currentRowSelects.length; selectIndex++) {
                currentRowSelects[selectIndex].id = currentRowSelects[selectIndex].id.replace(/Food_\d+_/, `Food_${currentRowIdIndex - 1}_`);
                currentRowSelects[selectIndex].name = currentRowSelects[selectIndex].name.replace(/Food\[\d+\]/, `Food\[${currentRowIdIndex - 1}\]`);
            }
            let currentRowInputs = rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].getElementsByTagName('input');
            for (let inputIndex = 0; inputIndex < currentRowInputs.length; ++inputIndex) {
                currentRowInputs[inputIndex].id = currentRowInputs[inputIndex].id.replace(/Food_\d+_/, `Food_${currentRowIdIndex - 1}_`);
                currentRowInputs[inputIndex].name = currentRowInputs[inputIndex].name.replace(/Food\[\d+\]/, `Food\[${currentRowIdIndex - 1}\]`);
            }
            let currentRowLabels = rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].getElementsByTagName('label');
            for (let labelIndex = 0; labelIndex < currentRowLabels.length; ++labelIndex) {
                currentRowLabels[labelIndex].setAttribute('for', currentRowLabels[labelIndex].getAttribute('for').replace(/Food_\d+_/, `Food_${currentRowIdIndex - 1}_`));
                /*label.name = label.name.replace(/Food\[\d+\]/, `Food[${currentRowIdIndex - 1}]`);*/
            }
            let currentRowSpans = rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].getElementsByTagName('span');
            for (let spanIndex = 0; spanIndex < currentRowSpans.length; ++spanIndex) {
                let replacementDataValmsgFor = currentRowSpans[spanIndex].getAttribute('data-valmsg-for').replace(/Food\[\d+\]/, `Food\[${currentRowIdIndex - 1}\]`);
                currentRowSpans[spanIndex].setAttribute('data-valmsg-for', replacementDataValmsgFor);
            }
            let currentRowRemoveBtn = rowsOfMealRecordFoodItems[mealRecordFoodItemIndex].getElementsByClassName('btn-remove-meal-record')[0];
            currentRowRemoveBtn.id = currentRowRemoveBtn.id.replace(/Food_\d+_/, `Food_${currentRowIdIndex - 1}_`);
        }
    }
    rowToRemove.remove();
    let $form = $('form');
    $form.removeData('validator');
    $form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse($form);
}

function removeInsulinItem(event) {
    let currentBtn = event.target;
    let currentBtnId = currentBtn.id
    let rowToRemoveId = currentBtnId.replace('RemoveBtn_', '');
    let rowToRemove = document.getElementById(rowToRemoveId);
    let rowToRemoveIdIndex = parseInt(rowToRemoveId.match(/\d+/)[0]);
    let rowsOfInsulinRecordInsulinAdministrationItems = rowToRemove.parentElement.getElementsByClassName('insulin-record-insulin-administered-item');

    for (var insulinRecordInsulinAdministrationItemIndex = 0; insulinRecordInsulinAdministrationItemIndex < rowsOfInsulinRecordInsulinAdministrationItems.length; insulinRecordInsulinAdministrationItemIndex++) {
        let currentRowIdIndex = parseInt(rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].id.match(/\d+/)[0]);
        if (currentRowIdIndex > rowToRemoveIdIndex) {
            rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].id = rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].id.replace(/InsulinAdministrations_\d+_/, `InsulinAdministrations_${currentRowIdIndex - 1}_`);
            let currentRowSelects = rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].getElementsByTagName('select');
            for (var selectIndex = 0; selectIndex < currentRowSelects.length; selectIndex++) {
                currentRowSelects[selectIndex].id = currentRowSelects[selectIndex].id.replace(/InsulinAdministrations_\d+_/, `InsulinAdministrations_${currentRowIdIndex - 1}_`);
                currentRowSelects[selectIndex].name = currentRowSelects[selectIndex].name.replace(/InsulinAdministrations\[\d+\]/, `InsulinAdministrations\[${currentRowIdIndex - 1}\]`);
            }
            let currentRowInputs = rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].getElementsByTagName('input');
            for (let inputIndex = 0; inputIndex < currentRowInputs.length; ++inputIndex) {
                currentRowInputs[inputIndex].id = currentRowInputs[inputIndex].id.replace(/InsulinAdministrations_\d+_/, `InsulinAdministrations_${currentRowIdIndex - 1}_`);
                currentRowInputs[inputIndex].name = currentRowInputs[inputIndex].name.replace(/InsulinAdministrations\[\d+\]/, `InsulinAdministrations\[${currentRowIdIndex - 1}\]`);
            }
            let currentRowLabels = rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].getElementsByTagName('label');
            for (let labelIndex = 0; labelIndex < currentRowLabels.length; ++labelIndex) {
                currentRowLabels[labelIndex].setAttribute('for', currentRowLabels[labelIndex].getAttribute('for').replace(/InsulinAdministrations_\d+_/, `InsulinAdministrations_${currentRowIdIndex - 1}_`));
                /*label.name = label.name.replace(/Food\[\d+\]/, `Food[${currentRowIdIndex - 1}]`);*/
            }
            let currentRowSpans = rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].getElementsByTagName('span');
            for (let spanIndex = 0; spanIndex < currentRowSpans.length; ++spanIndex) {
                let replacementDataValmsgFor = currentRowSpans[spanIndex].getAttribute('data-valmsg-for').replace(/InsulinAdministrations\[\d+\]/, `InsulinAdministrations\[${currentRowIdIndex - 1}\]`);
                currentRowSpans[spanIndex].setAttribute('data-valmsg-for', replacementDataValmsgFor);
            }
            let currentRowRemoveBtn = rowsOfInsulinRecordInsulinAdministrationItems[insulinRecordInsulinAdministrationItemIndex].getElementsByClassName('btn-remove-insulin-record')[0];
            currentRowRemoveBtn.id = currentRowRemoveBtn.id.replace(/InsulinAdministrations_\d+_/, `InsulinAdministrations_${currentRowIdIndex - 1}_`);
        }
    }
    rowToRemove.remove();
    let $form = $('form');
    $form.removeData('validator');
    $form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse($form);
}

function toggleValidationForMeal(state) {
    const mealDivCnt = document.getElementById('MealRecordsCnt');
    const mealInputElemList = mealDivCnt.getElementsByTagName('input');
    for (var i = 0; i < mealInputElemList.length; i++) {
        mealInputElemList[i].setAttribute('data-val', state);
        mealInputElemList[i].disabled = !state;
    }
    const mealSelectElemList = mealDivCnt.getElementsByTagName('select')
    for (var j = 0; j < mealSelectElemList.length; j++) {
        mealSelectElemList[j].setAttribute('data-val', state);
        mealSelectElemList[j].disabled = !state;
    }
}

function toggleValidationForInsulin(state) {
    const insulinDivCnt = document.getElementById('InsulinRecordsCnt');
    const insulinInputElemList = insulinDivCnt.getElementsByTagName('input');
    for (var i = 0; i < insulinInputElemList.length; i++) {
        insulinInputElemList[i].setAttribute('data-val', state);
        insulinInputElemList[i].disabled = !state;
    }
    const insulinSelectElemList = insulinDivCnt.getElementsByTagName('select')
    for (var j = 0; j < insulinSelectElemList.length; j++) {
        insulinSelectElemList[j].setAttribute('data-val', state);
        insulinSelectElemList[j].disabled = !state;
    }
}

function toggleValidationForBloodMeasurement(state) {
    const bloodMeasurementDivCnt = document.getElementById('BloodMeasurementCnt');
    const bloodMeasurementInputElemList = bloodMeasurementDivCnt.getElementsByTagName('input');
    for (var i = 0; i < bloodMeasurementInputElemList.length; i++) {
        bloodMeasurementInputElemList[i].setAttribute('data-val', state);
        bloodMeasurementInputElemList[i].disabled = !state;
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    //#region Prevent empty form submit

    let formElement = document.getElementById('NewRecordForm');
    formElement.addEventListener('submit', (event) => {
        const hasBloodMeasurementCheckboxValue = document.getElementById('Record_HasBloodMeasurement').checked;
        const hasInsulinCheckboxValue = document.getElementById('Record_HasInsulin').checked;
        const hasMealCheckboxValue = document.getElementById('Record_HasMeal').checked;
        console.log(hasBloodMeasurementCheckboxValue, hasInsulinCheckboxValue, hasMealCheckboxValue)
        if (hasBloodMeasurementCheckboxValue === false && hasInsulinCheckboxValue === false && hasMealCheckboxValue === false) {
            event.preventDefault();
            alert('Record without Blood measurement AND Insulin AND Meal IS NOT ALLOWED !');
        }
    })

    //#endregion

    //#region Show/Hide Blood measurement subfields on HasBloodMeasurement checkbox
    const hasBloodMeasurementCheckbox = document.getElementById('Record_HasBloodMeasurement');
    const bloodMeasurementCnt = document.getElementById('BloodMeasurementCnt').parentElement;
    if (hasBloodMeasurementCheckbox.checked) {
        bloodMeasurementCnt.style.display = '';
        toggleValidationForBloodMeasurement(true);
    } else {
        bloodMeasurementCnt.style.display = 'none';
        toggleValidationForBloodMeasurement(false);
    }

    hasBloodMeasurementCheckbox.addEventListener('change', (event) => {
        if (event.target.checked) {
            bloodMeasurementCnt.style.display = '';
            toggleValidationForBloodMeasurement(true);
        } else {
            bloodMeasurementCnt.style.display = 'none';
            toggleValidationForBloodMeasurement(false);
        }
    });

    //#endregion

    // #region Remove Insulin Items
    let removeInsulinRecordBtns = document.getElementsByClassName('btn-remove-insulin-record');
    for (var removeInsulinRecordBtnsIndex = 0; removeInsulinRecordBtnsIndex < removeInsulinRecordBtns.length; removeInsulinRecordBtnsIndex++) {
        removeInsulinRecordBtns[removeInsulinRecordBtnsIndex].addEventListener('click', removeInsulinItem);
    }
    // #endregion

    // #region Add Insulin Items
    let addInsulinBtn = document.getElementById('AddInsulinBtn');
    addInsulinBtn.addEventListener('click', (event) => {
        addNewInsulinItem();
    });
    // #endregion

    //#region Show/Hide Insulin subfields on HasInsulin checkbox
    const hasInsulinCheckbox = document.getElementById('Record_HasInsulin');
    const insulinCnt = document.getElementById('InsulinRecordsCnt').parentElement;
    if (hasInsulinCheckbox.checked) {
        insulinCnt.style.display = '';
        toggleValidationForInsulin(true);
    } else {
        insulinCnt.style.display = 'none';
        toggleValidationForInsulin(false);
    }

    hasInsulinCheckbox.addEventListener('change', (event) => {
        if (event.target.checked) {
            insulinCnt.style.display = '';
            toggleValidationForInsulin(true);
        } else {
            insulinCnt.style.display = 'none';
            toggleValidationForInsulin(false);
        }
    });

    //#endregion

    // #region Remove Food Items
    let removeMealRecordBtns = document.getElementsByClassName('btn-remove-meal-record');
    for (var removeMealRecordBtnsIndex = 0; removeMealRecordBtnsIndex < removeMealRecordBtns.length; removeMealRecordBtnsIndex++) {
        removeMealRecordBtns[removeMealRecordBtnsIndex].addEventListener('click', removeFoodItem);
    }
    // #endregion

    // #region Add Food Items
    let addFoodBtn = document.getElementById('AddFoodBtn');
    addFoodBtn.addEventListener('click', (event) => {
        addNewFoodItem();
    });
    // #endregion

    //#region Show/Hide Meal subfields on HasMeal checkbox
    const hasMealCheckbox = document.getElementById('Record_HasMeal');
    const mealCnt = document.getElementById('MealRecordsCnt').parentElement;
    if (hasMealCheckbox.checked) {
        mealCnt.style.display = '';
        toggleValidationForMeal(true);
    } else {
        mealCnt.style.display = 'none';
        toggleValidationForMeal(false);
    }

    hasMealCheckbox.addEventListener('change', (event) => {
        if (event.target.checked) {
            mealCnt.style.display = '';
            toggleValidationForMeal(true);
        } else {
            mealCnt.style.display = 'none';
            toggleValidationForMeal(false);
        }
    });

    //#endregion


});