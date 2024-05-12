import { toggleObjectVisibility } from "./../../components/utils.js";
import { Table } from "./../../components/table.js";


//functions
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
    /*tableBody.innerHTML = "";
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
                            <td>${user.userRegistrationData.isRegistered ? "" : user.userRegistrationData.registrationUrl}</td>                            
                        `;
        tableBody.appendChild(row);
    });*/

    const columns = [
        {
            targets: 0,
            title: 'ID',
            name: 'id',
            data: 'id',
            //width: 150,
            /*data: function (data) {
                return data['id'] ? `<div>${data.id}</div>` : '';
            }*/
        },
        {
            targets: 1,
            title: 'Name',
            name: 'name',
            data: 'name',
            //width: 150,
            /*data: function (data) {
                return data['id'] ? `<div>${data.id}</div>` : '';
            }*/
        },
        {
            targets: 2,
            title: 'Surname',
            name: 'surname',
            data: 'surname',
            //width: 150,
            /*data: function (data) {
                return data['id'] ? `<div>${data.id}</div>` : '';
            }*/
        },
        {
            targets: 3,
            title: 'Personal Code',
            name: 'personalCode',
            data: 'personalCode'
        },
        {
            targets: 4,
            title: 'Email',
            name: 'email',
            data: 'email'
        },
        {
            targets: 5,
            title: 'Phone Number',
            name: 'phoneNumber',
            data: 'phoneNumber'
        },
        {
            targets: 6,
            title: 'Age',
            name: 'age',
            data: 'age'
        },
        {
            targets: 7,
            title: 'Registration Link',
            name: 'registrationLink',
            data: 'registrationLink',
            data: function (data) {

                return data.userRegistrationData.isRegistered ? "" : data.userRegistrationData.registrationUrl ;
            }
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function () {
            let table = $('#userTable').DataTable();
            table.rows().every(function () {
                $(this.node()).css('cursor', 'pointer');
            });
            $('#userTable tbody').on('click', 'tr', function (data) {
                table.$('tr').removeClass('selected');
                $(this).addClass('selected');
                console.log('Selected Data', table.row(this).data())
            });
        }
    };

    const userTable = new Table('#userTable', users, columns, options);
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