(function ($) {
  var cancelButtons = [].slice.call($('.cancelBtn'));

  cancelButtons.forEach(function (btn) {
    $(btn).on('click', function (event) {
      var url = 'tickets/IsCancellableWithoutFee',
          id = btn.id;

      event.preventDefault();

      $.get(url, { ticketId: id }, function (isCancellable) {
        $('#confirmCancellationBtn').attr('href', '/ticket/cancel/' + id);
        if (isCancellable === true) {
          $('#modal-body').text('You can cancel for free.');
        } else if (isCancellable === false) {
          $('#modal-body').text('You will only be refunded 50% of the ticket price.');
        }

        $('#cancelModal').modal;
        $('#cancelModal').modal('show');
      });
    });
  });
}(jQuery));
