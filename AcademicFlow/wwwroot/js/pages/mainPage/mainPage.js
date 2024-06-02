import { getCurrentUser, toggleObjectVisibility } from "./../../components/utils.js";
import { Table } from "./../../components/table.js";

$(document).ready(async function () {
    try {
        const currentUser = await getCurrentUser();
        const userNameSurname = $(".user-name-surname h2");
        const userRole = $(".user-role h5");
        const personalCode = $("#pers_code");
        const email = $("#e-mail");
        const phoneNumber = $("#phone-nr");
        const age = $("#age");

        if (currentUser) {
            userNameSurname.text(`${currentUser.name} ${currentUser.surname}`);
            // Check if roles is an array and join them by comma
            if (Array.isArray(currentUser.roles)) {
                userRole.text(currentUser.roles.join(', '));
            } else {
                userRole.text(currentUser.roles);
            }

            personalCode.val(currentUser.personalCode);
            email.val(currentUser.email);
            phoneNumber.val(currentUser.phoneNumber);
            age.val(currentUser.age);
      
        }
    } catch (error) {
        console.error('Error fetching user data:', error);
    }

    $('.edit-email, .edit-phone').on('click', function () {
        var inputField = $(this).siblings('input');
        var isReadonly = inputField.prop('readonly');

        if (isReadonly) {
            inputField.prop('readonly', false).focus();
            $(this).text('Cancel').removeClass('btn-success').addClass('btn-danger');

            var saveButton = $('<button>', {
                type: 'button',
                class: 'btn btn-primary btn-sm save-button',
                text: 'Save',
                click: function () {
                    inputField.prop('readonly', true);
                    $(this).siblings('.edit-email, .edit-phone').text('Edit').removeClass('btn-danger').addClass('btn-success');
                    $(this).remove();
                }
            });
            $(this).after(saveButton);
        } else {
            inputField.prop('readonly', true);
            $(this).text('Edit').removeClass('btn-danger').addClass('btn-success');
            $(this).siblings('.save-button').remove();
        }
    });
});


