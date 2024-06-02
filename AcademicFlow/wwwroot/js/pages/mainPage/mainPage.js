import { getCurrentUser, toggleObjectVisibility } from "./../../components/utils.js";
import { Table } from "./../../components/table.js";

$(document).ready(async function () {
    let currentUser;

    try {
        currentUser = await getCurrentUser();
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
        const $main = $('.main');

        if (isReadonly) {
            // Store the original value
            var originalValue = inputField.val();
            inputField.data('original-value', originalValue);

            inputField.prop('readonly', false).focus();
            $(this).text('Cancel').removeClass('btn-success').addClass('btn-danger');

            var saveButton = $('<button>', {
                type: 'button',
                class: 'btn btn-primary btn-sm save-button',
                css: {
                    'margin-left': '4px' 
                },
                text: 'Save',
                click: async function () {
                    var newValue = inputField.val();
                    var fieldName = inputField.attr('name');

                    var data = new FormData();
                    console.log($main[0]);
                    data.append('id', currentUser.id);
                    data.append('name', currentUser.name);
                    data.append('surname', currentUser.surname);
                    data.append('personalCode', currentUser.personalCode);
                    data.append('email', fieldName === 'email' ? newValue : currentUser.email);
                    data.append('phoneNumber', fieldName === 'phoneNumber' ? newValue : currentUser.phoneNumber);
                    data.append('age', currentUser.age);
                    data.append(fieldName, newValue);
                    
                    console.log(fieldName, newValue)
                    // Send the updated data to the server
                    try {
                        $.ajax({
                            url: '/api/User/EditUser',
                            method: 'POST',
                            data: data,
                            processData: false,
                            contentType: false,
                            success: function (_) {
                                // Change Cancel button back to Edit and update its color
                                
                                $('.edit-email, .edit-phone').text('Edit').removeClass('btn-danger').addClass('btn-success');

                                // Remove Save button
                                $('.save-button').remove();

                                $main.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Profile info is changed.</div>`);
                                console.log('User edited successfully');

                                // Update the input field with the new value
                                inputField.prop('readonly', true);

                                // Update currentUser to reflect changes
                                if (fieldName === 'email') {
                                    currentUser.email = newValue;
                                } else if (fieldName === 'phoneNumber') {
                                    currentUser.phoneNumber = newValue;
                                }

                            },
                            error: function (xhr, response, status, error) {
                                $main.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Profile info is not changed.</div>`);
                                console.error('Error editing user');
                            }
                        });

                    } catch (error) {
                        $main.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Profile info is not changed.</div>`);
                        console.error('Error sending the new value to the server:', error);
                        
                    }
                }
            });
            $(this).after(saveButton);
        } else {
            // Revert to the original value
            var originalValue = inputField.data('original-value');
            inputField.val(originalValue).prop('readonly', true);
            $(this).text('Edit').removeClass('btn-danger').addClass('btn-success');
            $(this).siblings('.save-button').remove();
        }
    });
});


