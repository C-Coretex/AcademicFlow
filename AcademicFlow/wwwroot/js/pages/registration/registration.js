import { checkUserPermissionsLevel, getURLbyUserRole, getCurrentUser } from "./../../components/utils.js";

async function triggerGetCurrentUser() {
    const userData = await getCurrentUser();
    return userData;
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
    const $form = $('#registerForm');
    /*$.ajax({
        url: '/api/Authorization/RegisterUser',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(userData),
        success: function (response) {
            const currentUser = triggerGetCurrentUser();
            let roleValue = checkUserPermissionsLevel(currentUser.roles);
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
    });*/
    try {
        const response = await $.ajax({ // Use await for the AJAX call
            url: '/api/Authorization/RegisterUser',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(userData)
        });

        const currentUser = await triggerGetCurrentUser(); // Use await for the promise
        const roleValue = checkUserPermissionsLevel(currentUser.roles);
        const redirectionURL = getURLbyUserRole(roleValue);

        if (redirectionURL) {
            window.location.href = redirectionURL;
        } else {
            $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Something went wrong.</div>`);
        }
    } catch (error) {
        console.error('Error registering user:', error);
        $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Something went wrong. Please contact with our support team.</div>`);
    }
};
$(document).ready(function () {
    //Event Listeners
    $('.js-register-user').click(function () {
        registerUser();
    });
});