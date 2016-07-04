var apiURL = "http://localhost:8080/api";

$(document)
    .ready(function () {

        checkAuthentication();

        $('#logoutLink').off('click').click(function(e) {
            e.preventDefault();
            location.replace('/home');
            sessionStorage.removeItem('token');
            sessionStorage.removeItem('applicationId');
        });
    });

function checkAuthentication() {
    var token = sessionStorage.getItem('token');
    if (token) {
        displayLogoutLink();
    } else {
        displayRegisterLink();
    }
}

function displayLogoutLink() {
    $('#logoutLink').removeClass('not-display');
    $('#registerLink').addClass('not-display');
}

function displayRegisterLink() {
    $('#logoutLink').addClass('not-display');
    $('#registerLink').removeClass('not-display');
}