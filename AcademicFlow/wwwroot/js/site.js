// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import { checkUserPermissionsLevel, getURLbyUserRole, renderHeaderLinks, getCurrentUser, proceedLogout } from "./components/utils.js";


$(document).ready(async function () {
    const userData = await getCurrentUser();
    if (userData) {
        const headerNav = $('#header-nav');

        const userDataTab = $('<li class="nav-item js-users-tab d-none ml-4">');
        userDataTab.append($(`<a class="nav-link text-dark">Hello, ${userData.name} ${userData.surname}!</a>`))

        const logoutTab = $('<li class="nav-item js-users-tab d-none">');
        const logoutLink = $('<a class="nav-link text-dark cursor-pointer">Logout</a>');
        logoutLink.click(proceedLogout);
        logoutTab.append(logoutLink);

        headerNav.append(userDataTab);
        headerNav.append(logoutTab);

        let userRole = await checkUserPermissionsLevel(userData.roles);
        //redirectUserWithoutAccess(userRole);
        if (userRole) {
            renderHeaderLinks(userRole);
            $('.js-user-name').html(`<strong>${userData.name}</strong>`);

            //Event Listeners
            $('.js-redirect-by-role').click(function () {
                let redirectURL = getURLbyUserRole(userRole);
                window.location.href = redirectURL;
            });
        }
    }
});
