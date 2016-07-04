$(document).ready(function () {
    checkAuthentication();

    $('#logBtn').off('click').click(function (e) {
        e.preventDefault();

        var level = $('#level').val();
        var logger = $('#logger').val();
        var message = $('#message').val();
        var applicationId = sessionStorage.getItem('applicationId');
        var token = sessionStorage.getItem('token');
        if (applicationId && token) {
            var data = {
                application_id: applicationId,
                logger: logger,
                level: level,
                message: message
            }

            $.ajax({
                url: apiURL + '/log',
                dataType: 'json',
                method: 'POST',
                headers: {
                    "Authorization": token
                },
                contentType: 'application/json',
                data: JSON.stringify(data),
                error: function (jqXhr, textStatus, errorThrown) {
                    alert(errorThrown);
                },
                success: function (result) {
                    if (result) {
                        alert('Message logged succesfully');
                    } else {
                        alert('There was an error when processing your request.');
                    }
                }
            });
        } else {
            alert('There was an error in authentication process');
        }

    });

    $('#logger, #message, #level').on('keyup', function () {
        var element = $(this);
        if (!element.val()) {
            element.addClass('not-valid');
            $('#logBtn').attr('disabled', true);
        } else {
            element.removeClass('not-valid');
            $('#logBtn').attr('disabled', false);
        }
    });
});

function checkAuthentication() {
    var token = sessionStorage.getItem('token');
    if (!token) {
        location.replace('/Home');
    }
}