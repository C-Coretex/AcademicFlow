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
};

function redirectToLoginPage() {
    window.location.href = "/Home/Login";
};

$(document).ready(function () {
   
    $('.js-logout-user').click(function () {
        logoutUser()
            .then(() => { // After successful logout
                redirectToLoginPage();
            })
                .catch(error => {
                    console.error("Error logging out:", error);
                    // Handle logout errors (optional)
                });
        
    });
})