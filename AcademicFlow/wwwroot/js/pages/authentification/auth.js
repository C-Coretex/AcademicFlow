let redirectionURL;

function setRedirectURL(roleValue) {
    //TODO change redirection URL by user role
    switch (roleValue) {
        case 1:
            return "/Home/AdminCenter"
            break;
        case 2:
            return "/Home/MainPage"
            break;
        case 3:
            return "/Home/MainPage"
            break;
        default:
            return ""
    }
}

function checkRoleImportance(roles) {
    const rolePriorities = {
        "Admin": 1,
        "Professor": 2,
        "Student": 3,
    };

    let highestRole = 3; // Initialize with lowest priority (Student)
    for (const role of roles) {
        if (rolePriorities[role] && rolePriorities[role] < highestRole) {
            highestRole = rolePriorities[role];
        }
    }
    return highestRole;
}

async function loginUser() {
    const username = document.getElementById("username2").value;
    const password = document.getElementById("password2").value;
    const userData = {
        username: username,
        password: password
    };
    let userID;
    const $form = $('#loginForm');
    const formData = new FormData($form[0]);
    

    $.ajax({
        url: '/api/Authorization/LoginUser',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(userData),
        success: function (data) {
            let roleValue = checkRoleImportance(data.roles);
            let redirectionURL = setRedirectURL(roleValue);
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
$(document).ready(function () {
    //Event Listeners
    $('.js-login-user').click(function () {
        loginUser();
    });
});


