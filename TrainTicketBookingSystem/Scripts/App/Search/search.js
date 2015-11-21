(function () {
    var departureDD = document.getElementById('departure-dropdown'),
        arrivalDD = document.getElementById('arrival-dropdown'),
        submitBtn = document.getElementById('submit-btn'),
        departureIsDisabled = true,
        arrivalIsDisabled = true,
        currentDeparture,
        currentArrival;

    function changeButtonState() {
        currentDeparture = departureDD.options[departureDD.selectedIndex].value;
        currentArrival = arrivalDD.options[arrivalDD.selectedIndex].value;
        departureIsDisabled = departureDD.options[departureDD.selectedIndex].disabled;
        arrivalIsDisabled = arrivalDD.options[arrivalDD.selectedIndex].disabled;

        if ((departureIsDisabled || arrivalIsDisabled)
            || (currentDeparture === currentArrival)) {
            submitBtn.classList.add('disabled');
            submitBtn.disabled = true;
        } else {
            submitBtn.classList.remove('disabled');
            submitBtn.disabled = false;
        }
    }

    departureDD.addEventListener('change', changeButtonState);
    arrivalDD.addEventListener('change', changeButtonState);
    changeButtonState();
}());