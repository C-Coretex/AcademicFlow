import { toggleObjectVisibility} from "./../../components/utils.js";

const $loginContainer = $(".login-container");
const $signupContainer = $(".register-container");
function toggleCard(state) {
    switch (state) {
        case 'signup':
            toggleObjectVisibility($loginContainer, false);
            toggleObjectVisibility($signupContainer, true);
            break;
        case 'login':
            toggleObjectVisibility($loginContainer, true);
            toggleObjectVisibility($signupContainer, false);
            break;
    };
};

async function registerUser() {
    const userId = document.getElementById("userId").value;
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const userData = {
        id: userId,
        username: username,
        password: password
    };

    try {
        const response = await fetch('/api/Authorization/RegisterUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage);
        }

        console.log("User registered successfully!");
        toggleCard('login');
        //TODO: Add success notification
    } catch (error) {
        console.error('Error:', error);
    }
};

async function loginUser() {
    const username = document.getElementById("username2").value;
    const password = document.getElementById("password2").value;
    const userData = {
        username: username,
        password: password
    };

    try {
        const response = await fetch('/api/Authorization/LoginUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData)
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage);
        }

        const user = await response.json();
        console.log("User loginned successfully!");
        toggleCard('login');
        //TODO: redirect to main page
    } catch (error) {
        console.error('Error:', error);
    }
};

async function logoutUser() {
    try {
        const response = await fetch('/api/Authorization/LogoutUser', {
            method: 'GET'
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage);
        }
        console.log("User logged out successfully!");
        // Optionally, redirect to another page or perform other actions upon successful logout
    } catch (error) {
        console.error('Error:', error);
        alert("Failed to logout. Please try again.");
    }
}
$(document).ready(function () {
    //Event Listeners
    $(".js-show-sign-up-form").click(function () {
        toggleCard('signup');
    });
    $(".js-show-log-in-form").click(function () {
        toggleCard('login');
    });

    $('.js-register-user').click(function () {
        registerUser();
    });

    $('.js-login-user').click(function () {
        loginUser();
    });
});


