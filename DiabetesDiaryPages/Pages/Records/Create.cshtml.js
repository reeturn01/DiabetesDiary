$(function () {
    let $foodSelectItemList = $('#FoodSelectItemList');
    let foodSelectItemList = document.getElementById('FoodSelectItemList');
    foodSelectItemList.addEventListener('change', (ev) => {
        let $selectedFoodItemValue = $(foodSelectItemList).val();
        if ($selectedFoodItemValue !== null && isNaN($selectedFoodItemValue) === false) {
            $.getJSON("/Records/Create?handler=FoodDetails", { id : $selectedFoodItemValue }, (data) => {
                let foodDetails = JSON.parse(data);
                let $foodDeatailsTableBody = $('#FoodDeatailsTable > tbody');
                $foodDeatailsTableBody.empty();
                let newTR = document.createElement('tr');

                //let newTD = document.createElement('td');
                //newTD.innerText = foodDetails['Id'];
                //newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Name'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Manufacturer'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['PreparationType'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Calories'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Proteins'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Fat'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Carbohydrates'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['Sugars'];
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = foodDetails['GlicemicIndex'];
                newTR.appendChild(newTD);

                $foodDeatailsTableBody.append(newTR);
            });
        } 
    });

    document.getElementById('AddFoodToMealBtn').addEventListener('click', () => {
        let $selectedFoodItemValue = $(foodSelectItemList).val();
        if ($selectedFoodItemValue !== null || isNaN($selectedFoodItemValue) === false) {
            let $foodAmountInput = $('#FoodAmountInput').val();
            if ($selectedFoodItemValue !== null && isNaN($selectedFoodItemValue) === false && $foodAmountInput !== null && isNaN($foodAmountInput) === false && parseInt($foodAmountInput) > 0) {
                let $mealTableBody = $('#MealTable > tbody');
                let mealItemsCount = $mealTableBody.children().length;
                let newTR = document.createElement('tr');

                let newTD = document.createElement('td');
                newTD.innerText = $foodSelectItemList.children(':selected').text()
                let newHiddenInput = document.createElement('input');
                newHiddenInput.name = `Record.MealRecord.Food[${mealItemsCount}].FoodId`;
                newHiddenInput.setAttribute('value', $selectedFoodItemValue);
                newHiddenInput.hidden = true;
                newTD.appendChild(newHiddenInput);
                newTR.appendChild(newTD);

                newTD = document.createElement('td');
                newTD.innerText = $foodAmountInput
                newHiddenInput = document.createElement('input');
                newHiddenInput.name = `Record.MealRecord.Food[${mealItemsCount}].Amount`;
                newHiddenInput.type = 'text'
                newHiddenInput.setAttribute('value', $foodAmountInput);
                newHiddenInput.hidden = true;
                newTD.appendChild(newHiddenInput);
                newTR.appendChild(newTD);

                let newDeleteAnchor = document.createElement('a');
                newDeleteAnchor.id = `DeleteMealFoodItem_${mealItemsCount}`;
                newDeleteAnchor.innerHTML = document.createTextNode('Remove food item');

                newDeleteAnchor.addEventListener('click', function () {
                    console.log('da se dopravi');
                })
                newTR.appendChild(newDeleteAnchor);

                $mealTableBody[0].appendChild(newTR);
            }
        }
    });
})