
let redirectionURL;
function setRedirectURL() {
    //TODO change redirection URL by user role
    redirectionURL = "/Home/AdminCenter"
}
async function registerUser() {
    const secretKey = document.getElementById("secretKey").value;
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const userData = {
        secretKey: secretKey,
        username: username,
        password: password
    };

    $.ajax({
        url: '/api/Authorization/RegisterUser',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(userData),
        success: function (response) {
            window.location.href = redirectionURL;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Something went wrong. Please contact with our support team.</div>`);
            console.error('Error:', textStatus, errorThrown);
        }
    });
};
$(document).ready(function () {
    //Event Listeners
    setRedirectURL();
    $('.js-register-user').click(function () {
        registerUser();
    });
});