let redirectionURL;

function setRedirectURL() {
    //TODO change redirection URL by user role
    redirectionURL = "/Home/AdminCenter"
}

async function loginUser() {
    const username = document.getElementById("username2").value;
    const password = document.getElementById("password2").value;
    const userData = {
        username: username,
        password: password
    };

    $.ajax({
        url: '/api/Authorization/LoginUser',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(userData),
        success: function (user) {
            window.location.href = redirectionURL;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Error:', textStatus, errorThrown);
        }
    });
};
$(document).ready(function () {
    //Event Listeners
    setRedirectURL();

    $('.js-login-user').click(function () {
        loginUser();
    });
});


