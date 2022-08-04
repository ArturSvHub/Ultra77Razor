$(document).ready(function () {
    $('#inputNumber').on('valueChanged',function(event) {
            $('#pricePerCount').html(function() {
                $('#priceOne').text * $('#inputNumber').event.args.value;
            });
        });
});
