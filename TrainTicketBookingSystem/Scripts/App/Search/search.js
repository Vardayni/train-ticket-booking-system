(function () {
    var departureDropdown = document.getElementById('departure-dropdown'),
        arrivalDropdown = document.getElementById('arrival-dropdown'),
        submitBtn = document.getElementById('submit-btn'),
        departureIsDisabled = true,
        arrivalIsDisabled = true;

    function changeButtonState() {
        departureIsDisabled = departureDropdown.options[departureDropdown.selectedIndex].disabled;
        arrivalIsDisabled = arrivalDropdown.options[arrivalDropdown.selectedIndex].disabled;

        if (departureIsDisabled || arrivalIsDisabled) {
            submitBtn.classList.add('disabled');
            submitBtn.disabled = true;
        } else {
            submitBtn.classList.remove('disabled');
            submitBtn.disabled = false;
        }
    }

    departureDropdown.addEventListener('change', changeButtonState);
    arrivalDropdown.addEventListener('change', changeButtonState);

    changeButtonState();
}());