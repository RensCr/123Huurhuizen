

    const togglePassword = document.querySelector('#togglePassword1');
    const password = document.querySelector('#password1');
    togglePassword.addEventListener('click', function (e) {
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        if (type === 'password') {
            this.innerHTML = '<i class="fas fa-eye-slash"></i>';
        } else {
            this.innerHTML = '<i class="fas fa-eye"></i>';
        }
    });

function checkUncheck(currentCheckbox, otherCheckboxId) {
        var otherCheckbox = document.getElementById(otherCheckboxId);
        if (currentCheckbox.checked) {
            otherCheckbox.checked = false;
            currentCheckbox.value = true;
            otherCheckbox.value = false; // Zet de waarde van het hidden input-veld op "false"
            otherCheckbox.removeAttribute('required'); // Verwijder de 'required' attribuut van het andere checkbox
        } else {
            otherCheckbox.setAttribute('required', ''); // Voeg de 'required' attribuut toe aan het andere checkbox
        }
        var extraOptionsDiv = document.getElementById('extraOptions');
        var body = document.querySelector('body');

        if (currentCheckbox.id === 'checkbox2' && currentCheckbox.checked) {
            body.style.overflow = 'auto';
            extraOptionsDiv.innerHTML = `
                    <div class="col-md-6 mb-3">
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" id="personalRent" name="personalRent" onclick="CheckTypeSeller(this, 'companyRent')" required>
                            <label class="form-check-label" for="personalRent">Particuliere verhuurder</label>
                        </div>
                    </div>
                    <div class="col-md-6 mb-3">
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" id="companyRent" name="companyRent" onclick="CheckTypeSeller(this, 'personalRent')" required>
                            <label class="form-check-label" for="companyRent">Zakelijke verhuurder</label>
                        </div>
                    </div>`;
        } else {
            extraOptionsDiv.innerHTML = '';
            window.scrollTo(0, 0);
            body.style.overflow = 'hidden';
        }
    }

    function CheckTypeSeller(currentCheckbox, otherCheckboxId) {
        var otherCheckbox = document.getElementById(otherCheckboxId);
        if (currentCheckbox.checked) {
            otherCheckbox.checked = false;
            currentCheckbox.value = true;
            otherCheckbox.value = false; // Zet de waarde van het hidden input-veld op "false"
            otherCheckbox.removeAttribute('required'); // Verwijder de 'required' attribuut van het andere checkbox
        } else {
            otherCheckbox.setAttribute('required', ''); // Voeg de 'required' attribuut toe aan het andere checkbox
        }
    }
