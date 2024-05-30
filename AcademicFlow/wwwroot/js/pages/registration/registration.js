import { checkUserPermissionsLevel, getURLbyUserRole } from "./../../components/utils.js";
async function registerUser() {
    const secretKey = document.getElementById("secretKey").value;
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const userData = {
        secretKey: secretKey,
        username: username,
        password: password
    };
    const $form = $('#registerForm');
    $.ajax({
        url: '/api/Authorization/RegisterUser',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(userData),
        success: function (response) {
            let roleValue = checkUserPermissionsLevel(data.roles);
            let redirectionURL = getURLbyUserRole(roleValue);
            if (redirectionURL) {
                window.location.href = redirectionURL;
            } else {
                $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Something went wrong.</div>`);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Something went wrong. Please contact with our support team.</div>`);
            console.error('Error:', textStatus, errorThrown);
        }
    });
};
$(document).ready(function () {
    //Event Listeners
    getURLbyUserRole();
    $('.js-register-user').click(function () {
        registerUser();
    });
});