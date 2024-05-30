import { checkUserPermissionsLevel, getURLbyUserRole} from "./../../components/utils.js";

async function loginUser() {
    const username = document.getElementById("username2").value;
    const password = document.getElementById("password2").value;
    const userData = {
        username: username,
        password: password
    };
    const $form = $('#loginForm');    

    $.ajax({
        url: '/api/Authorization/LoginUser',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(userData),
        success: function (data) {
            let roleValue = checkUserPermissionsLevel(data.roles);
            let redirectionURL = getURLbyUserRole(roleValue);
            if (redirectionURL) {
                window.location.href = redirectionURL;
            } else {
                $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Something went wrong.</div>`);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">User with this username or password does not exist. </div>`);
            console.error('Error:', textStatus, errorThrown);
        }
    });
};
$(document).ready(async function () {
    //Event Listeners
    $('.js-login-user').click(function () {
        loginUser();
    });
});


