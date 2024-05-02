import { toggleObjectVisibility } from "./../../components/utils.js";

function hideAllObjects(containers) {
    for (const containerName in containers) {
        toggleObjectVisibility(containers[containerName], false);
    }
}

async function getUsersData() {
    try {
        const response = await fetch("/api/User/GetAllUsers");
        const data = await response.json();
        populateTable(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
}

function populateTable(users) {
    const tableBody = document.getElementById("userData");
    tableBody.innerHTML = "";
    console.log('a');
    users.forEach(user => {
        const row = document.createElement("tr");
        row.innerHTML = `
                            <td>${user.id}</td>
                            <td>${user.name}</td>
                            <td>${user.surname}</td>
                            <td>${user.personalCode}</td>
                            <td>${user.email ? user.email : ""}</td>
                            <td>${user.phoneNumber ? user.phoneNumber : ""}</td>
                            <td>${user.age ? user.age : ""}</td>
                        `;
        tableBody.appendChild(row);
    });
};

function refreshUsersTable() {
    getUsersData();
};

$(document).ready(function () {

    $('.js-user-name').html(`<strong>Name</strong>`);

    const containers = { $addUser: $('.add-user-container'), $allUsers: $('.user-table') };

    refreshUsersTable();
    
    //Event Listeners
    $('.nav-item').on('click', function () {
        const self = this;

        //Styles
        $('.nav-item').find('.active').removeClass('active');
        $(self).find('.link-dark').removeClass('link-dark')
        $(self).find('.nav-link').addClass('active');

        //Logic
        const selectedEl = $(self).data('nav-el');
        hideAllObjects(containers);
        switch (selectedEl) {
            case 'allUsers':
                toggleObjectVisibility($(containers.$allUsers), true);
                refreshUsersTable();
                break;
            case 'addUser':
                toggleObjectVisibility($(containers.$addUser), true);
                break;
            default:
                hideAllObjects(containers);
                break;
        }
    });

    
    $('.js-add-user').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#registerForm');

        const formData = {
            name: $form.find('#name').val().trim(),
            surname: $form.find('#surname').val().trim(),
            personalCode: $form.find('#personalCode').val().trim(),
            email: $form.find('#email').val().trim(), // Optional (can be null)
            phoneNumber: $form.find('#phoneNumber').val().trim(), // Optional (can be null)
        };
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            console.log(formData);
            $.ajax({
                type: 'PUT',
                url: '/api/User/AddUser',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (response) {
                    console.log('User added successfully');
                },
                error: function (xhr, status, error) {
                    console.error('Error adding user:', error);
                }
            })
        } else {
            //TODO add form validation errors
        }
        
    });

});