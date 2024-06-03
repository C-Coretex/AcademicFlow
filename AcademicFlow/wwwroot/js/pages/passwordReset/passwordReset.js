import { checkUserPermissionsLevel, getURLbyUserRole, getCurrentUser } from "./../../components/utils.js";

async function triggerGetCurrentUser() {
    const userData = await getCurrentUser();
    return userData;
}
async function resetPassword() {
    const secretKey = document.getElementById("secretKey").value;
    const password = document.getElementById("password").value;
    const userData = {
        secretKey: secretKey,
        password: password
    };
    const $form = $('#passwordResetForm');
    try {
        const response = await $.ajax({ // Use await for the AJAX call
            url: '/api/Authorization/PasswordReset',
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
    $('.js-reset-password').click(function () {
        resetPassword();
    });
});