(function () {
  'use strict';

  var departure = document.getElementById('departure-dropdown'),
      arrival = document.getElementById('arrival-dropdown'),
      submitBtn = document.getElementById('submit-btn'),
      cities = GLOBAL.CITIES || [],
      currentDeparture = '',
      currentArrival = '',

      onDepartureSelectListClick = function (e) {
        var el = e.target,
            currentOption = el.options[el.selectedIndex],
            arrivalCities = [];

        arrivalCities = cities.filter(function (city) {
          return city !== currentOption.value;
        });

        arrival.innerHTML = '';
        arrival.appendChild(
          createDefaultSelectListOption('Choose an arrival.'));

        arrivalCities.forEach(function (city) {
          var newOption = document.createElement('option');
          newOption.text = city;
          newOption.value = city;

          arrival.appendChild(newOption);
        });

        arrival.disabled = currentOption.disabled;
        submitBtn.classList.add('disabled');
        submitBtn.disabled = true;
      },

      onArrivalSelectListClick = function (e) {
        var el = e.target;

        if (!el.options[el.selectedIndex].disabled) {
          submitBtn.classList.remove('disabled');
          submitBtn.disabled = false;
        }
      },

      createDefaultSelectListOption = function (text) {
        var defaultArrivalOption = document.createElement('option');
        defaultArrivalOption.text = text;
        defaultArrivalOption.disabled = true;
        defaultArrivalOption.selected = true;

        return defaultArrivalOption;
      };

  // select list initialization
  departure.selectedIndex = 0;
  arrival.selectedIndex = 0;
  arrival.disabled = true;
  submitBtn.classList.add('disabled');
  submitBtn.disabled = true;

  departure.addEventListener('change', onDepartureSelectListClick);
  arrival.addEventListener('change', onArrivalSelectListClick);
}());