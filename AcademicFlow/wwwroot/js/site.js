// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import { checkUserPermissionsLevel, getURLbyUserRole, renderHeaderLinks, getCurrentUser, redirectUserWithoutAccess } from "./components/utils.js";


$(document).ready(async function () {
    let userData = await getCurrentUser();
    if (userData) {
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
