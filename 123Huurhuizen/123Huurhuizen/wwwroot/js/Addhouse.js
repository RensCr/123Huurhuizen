var selectedProperties = [];

function addPropertyInput() {
    var selectedPropertiesDiv = document.getElementById("selectedProperties");
    var propertyInputDiv = document.createElement("div");
    propertyInputDiv.classList.add("property-input");

    var propertySelect = document.createElement("select");
    propertySelect.classList.add("property-select");
    propertySelect.onchange = function () {
        createAmountInput(this, propertyInputDiv);
        this.disabled = true;
    };

    var defaultOption = document.createElement("option");
    defaultOption.value = "";
    defaultOption.text = "Selecteer een huiseigenschap";
    defaultOption.disabled = true;
    defaultOption.selected = true;
    propertySelect.appendChild(defaultOption);

    var hasOptions = false;
    availableProperties.forEach(function (property) {
        if (!selectedProperties.includes(property.name)) {
            var option = document.createElement("option");
            option.value = property.id;
            option.text = property.name;
            option.dataset.unit = property.unit;
            propertySelect.appendChild(option);
            hasOptions = true;
        }
    });


    if (!hasOptions) {
        Swal.fire({
            icon: 'error',
            title: 'Alle beschikbare eigenschappen zijn gebruikt',
            showConfirmButton: false,
            timer: 1500
        });
        return;
    }

    propertyInputDiv.appendChild(propertySelect);
    selectedPropertiesDiv.appendChild(propertyInputDiv);

    document.getElementById("addPropertyButton").style.display = hasOptions ? "inline-block" : "none";
}

function createAmountInput(selectElement, propertyInputDiv) {

    var selectedPropertyId = selectElement.value;
    
    var selectedPropertyName = selectElement.options[selectElement.selectedIndex].text; 

    if (propertyInputDiv.querySelector(".amount-input")) {
        return;
    }

    var amountInput = document.createElement("input");
    amountInput.classList.add("amount-input");
    amountInput.type = "number";
    amountInput.name = "Properties[" + selectedProperties.length + "].Amount";
    amountInput.placeholder = selectElement.options[selectElement.selectedIndex].dataset.unit;
    amountInput.dataset.propertyId = selectedPropertyId;

    var propertyIdInput = document.createElement("input");
    propertyIdInput.type = "hidden";
    propertyIdInput.name = "Properties[" + selectedProperties.length + "].Id";
    propertyIdInput.value = selectedPropertyId; // Hier wordt het Id correct toegewezen aan het verborgen inputveld

    var propertyNameInput = document.createElement("input");
    propertyNameInput.type = "hidden";
    propertyNameInput.name = "Properties[" + selectedProperties.length + "].Name";
    propertyNameInput.value = selectedPropertyName; // Hier wordt de naam van de eigenschap correct toegewezen aan het verborgen inputveld

    var deleteButton = document.createElement("button");
    deleteButton.classList.add("delete-button");
    deleteButton.textContent = "🗑️";
    deleteButton.type = "button";
    deleteButton.onclick = function () {
        propertyInputDiv.remove();
        selectedProperties = selectedProperties.filter(item => item !== selectedPropertyName);
        document.getElementById("addPropertyButton").style.display = "inline-block";
        enablePropertyOption(selectedPropertyId);
    };

    propertyInputDiv.appendChild(amountInput);
    propertyInputDiv.appendChild(propertyIdInput);
    propertyInputDiv.appendChild(propertyNameInput);
    propertyInputDiv.appendChild(deleteButton);

    selectedProperties.push(selectedPropertyName);
    disablePropertyOption(selectedPropertyId);

    var allPropertiesUsed = selectedProperties.length === document.querySelectorAll('.property-select option').length - 1;
    document.getElementById("addPropertyButton").style.display = allPropertiesUsed ? "none" : "inline-block";
}


function disablePropertyOption(propertyId) {
    var allSelects = document.querySelectorAll('.property-select');
    allSelects.forEach(select => {
        var option = select.querySelector('option[value="' + propertyId + '"]');
        if (option) {
            option.disabled = true;
        }
    });
}

function enablePropertyOption(propertyId) {
    var allSelects = document.querySelectorAll('.property-select');
    allSelects.forEach(select => {
        var option = select.querySelector('option[value="' + propertyId + '"]');
        if (option) {
            option.disabled = false;
        }
    });
}

function validateForm() {
    var locationInput = document.getElementById("location").value;
    if (locationInput.trim() === "") {
        Swal.fire({
            icon: 'error',
            title: 'Voer een locatie in',
            showConfirmButton: false,
            timer: 1500
        });
        return false;
    }
    if (selectedProperties.length === 0) {
        Swal.fire({
            icon: 'error',
            title: 'Selecteer minstens 1 huiseigenschap.',
            showConfirmButton: false,
            timer: 1500
        });
        return false;
    }
    return true;
}

function displaySelectedFile(files) {
    var selectedFilesDiv = document.getElementById("selectedFiles");
    selectedFilesDiv.innerHTML = '';

    for (var i = 0; i < files.length; i++) {
        var fileElement = document.createElement("p");
        fileElement.textContent = files[i].name;
        selectedFilesDiv.appendChild(fileElement);
    }
}

document.getElementById("addPropertyButton").onclick = addPropertyInput;
document.getElementById("photos").onchange = function () {
    displaySelectedFile(this.files);
};
document.querySelector("form").onsubmit = validateForm;
