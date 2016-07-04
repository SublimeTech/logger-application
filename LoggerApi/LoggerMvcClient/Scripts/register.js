$(document).ready(function () {
    $('#registerBtn').off('click').click(function(e) {
        e.preventDefault();

        var applicationName = $('#applicationName').val();

        //CALL API
        var data = {
            display_name: applicationName
        }

        $.ajax({
            url: apiURL + '/register',
            data: JSON.stringify(data),
            dataType: 'json',
            method: 'POST',
            error: function(jqXhr, textStatus, errorThrown) {
                alert(errorThrown);
            },
            contentType: 'application/json',
            success: function (application) {

                if (!application) {
                    alert('There was an error');
                }
                //Call /Auth to try authenticate application

                var authHeaderValue = application.application_id + ':' + application.application_secret;

                $.ajax({
                    url: apiURL + '/auth',
                    dataType: 'json',
                    method: 'POST',
                    headers: {
                        "Authorization" : authHeaderValue
                    },
                    contentType: 'application/json',
                    error: function (jqXhr, textStatus, errorThrown) {
                        alert(errorThrown);
                    },
                    success: function (token) {

                        var accessToken = token.access_token;

                        //Save the token for futher api calls use
                        sessionStorage.setItem('token', accessToken);
                        sessionStorage.setItem('applicationId', application.application_id);
                        location.replace("/Log");
                    }
                });
            }
        });
    });

    $('#applicationName').on('keyup', function () {
        var element = $(this);
        if (!element.val()) {
            element.addClass('not-valid');
            $('#registerBtn').attr('disabled', true);
        } else {
            element.removeClass('not-valid');
            $('#registerBtn').attr('disabled', false);
        }
    });

})