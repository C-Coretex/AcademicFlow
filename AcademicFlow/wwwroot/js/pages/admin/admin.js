import { toggleObjectVisibility, editCourseUserRoles, checkUserPermissionsLevel, getCurrentUser, renderHeaderLinks, getUserByID  } from "./../../components/utils.js";
import { Table } from "./../../components/table.js";

let selectedUserIDs = [];
let coursesData;
let programData

//functions
function hideAllObjects(containers) {
    for (const containerName in containers) {
        toggleObjectVisibility(containers[containerName], false);
    }
}

async function getUsersData() {
    $.ajax({
        url: "/api/User/GetAllUsers",
        method: "GET",
        dataType: "json",
        success: function (data) {
             initUsersTable(data);
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });
}

async function getCoursesData() {
    $.ajax({
        url: "/api/Course/GetCourseTable",
        method: "GET",
        dataType: "json",
        success: function (data) {
            initCoursesTable(data);
            renderCoursesDropdownValues(data);
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });
}

function renderCoursesDropdownValues(courseData) {
    const dropdownMenu = $('.js-dd-courses').find('.dropdown-menu'); // Select the dropdown menu element
    dropdownMenu.empty();

    courseData.forEach(course => {
        const option = `<li><a class="dropdown-item" value="${course.id}">${course.publicId}: ${course.name}</a></li>`;
        dropdownMenu.append(option);
    });
}

async function getProgramData() {
    $.ajax({
        url: "/api/Program/GetProgramTable",
        method: "GET",
        dataType: "json",
        success: function (data) {
            initProgramsTable(data);
        },
        error: function (error) {
            console.error("Error fetching data:", error);
        }
    });
}

function showUserManagementTools(userData){
    console.log('should show', userData);
}

function initUsersTable(users) {

    if ($.fn.DataTable.isDataTable('#usersTable')) {
        let table = $('#usersTable').DataTable();
        table.destroy();
    }
    const columns = [
        {
            targets: 0,
            title: 'ID',
            name: 'id',
            data: 'id'
        },
        {
            targets: 1,
            title: 'Name',
            name: 'name',
            data: 'name'
        },
        {
            targets: 2,
            title: 'Surname',
            name: 'surname',
            data: 'surname'
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
            data: 'age',
        },
        {
            targets: 7,
            title: 'Registration Link',
            name: 'registrationLink',
            data: 'registrationLink',
            width: '10%',
            data: function (data) {

                return data.userRegistrationData.isRegistered ? "" : data.userRegistrationData.registrationUrl ;
            },
            render: function (data, type, rowData, meta) {
                return data ? `<button class="btn btn-primary"><i class="fa-regular fa-copy js-copy p-2" data-link="${data}"></i></button>` : "";
            },
        },
        {
            targets: 8,
            title: 'Role',
            name: 'roles',
            data: 'roles'
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function () {
            let table = $('#usersTable').DataTable();
            table.rows().every(function () {
                $(this.node()).css('cursor', 'pointer');
            });


            $('#usersTable tbody').on('click', 'tr', function (data) {
                const clickedElement = $(event.target); 

                if (!clickedElement.hasClass('js-copy')) {
                    if ($(this).hasClass('selected')) {
                        $(this).removeClass('selected');
                    } else {
                        $(this).addClass('selected');
                    }
                    
                    const selectedData = table.row(this).data();

                    selectedUserIDs = $.map(table.rows('.selected').data(), function (item) {
                        return item.id
                    });
                    console.log('Selected IDs:', selectedUserIDs);

                    getUserByID(selectedData.id)
                        .then(userData => {
                            console.log('User data:', userData);
                            
                            //showUserManagementTools(userData);
                        })
                        .catch(error => {
                            console.error('Error fetching user data:', error);
                        });


                    console.log('Selected Data', selectedData);
                }
            });

            const copyIcons = document.querySelectorAll('.js-copy');

            copyIcons.forEach(icon => {
                icon.addEventListener('click', function () {

                    const link = $(this).data('link'); // Access the URL from the data attribute
                    console.log(link);
                    table.$('tr').removeClass('selected');
                    navigator.clipboard.writeText(link);

                });
            });
        }
    };

    const usersTable = new Table('#usersTable', users, columns, options);
};

function initCoursesTable(courses) {

    if ($.fn.DataTable.isDataTable('#coursesTable')) {
        let table = $('#coursesTable').DataTable();
        table.destroy();
    }
    const columns = [
        {
            targets: 0,
            title: 'ID',
            name: 'id',
            data: 'id'
        },
        {
            targets: 1,
            title: 'Course Title',
            name: 'name',
            data: 'name'
        },
        {
            targets: 2,
            title: 'CP',
            name: 'creditPoints',
            data: 'creditPoints'
        },
        {
            targets: 3,
            title: 'Course ID',
            name: 'publicId',
            data: 'publicId'
        },
        {
            targets: 4,
            title: 'Course Description',
            name: 'description',
            data: 'description'
        },
        {
            targets: 5,
            title: 'Image',
            name: 'imageUrl',
            data: 'imageUrl'
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function () {
            let table = $('#coursesTable').DataTable();
            table.rows().every(function () {
                $(this.node()).css('cursor', 'pointer');
            });
            $('#coursesTable tbody').on('click', 'tr', function (data) {
                table.$('tr').removeClass('selected');
                $(this).addClass('selected');
                console.log('Selected Data', table.row(this).data())
            });
        }
    };

    const coursesTable = new Table('#coursesTable', courses, columns, options);
};

function initProgramsTable(programs) {

    if ($.fn.DataTable.isDataTable('#programsTable')) {
        let table = $('#programsTable').DataTable();
        table.destroy();
    }
    const columns = [
        {
            targets: 0,
            title: 'ID',
            name: 'id',
            data: 'id'
        },
        {
            targets: 1,
            title: 'Title',
            name: 'name',
            data: 'name'
        },
        {
            targets: 2,
            title: 'Semester Number',
            name: 'semesterNr',
            data: 'semesterNr'
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function () {
            let table = $('#programsTable').DataTable();
            table.rows().every(function () {
                $(this.node()).css('cursor', 'pointer');
            });
            $('#programsTable tbody').on('click', 'tr', function (data) {
                table.$('tr').removeClass('selected');
                $(this).addClass('selected');
                console.log('Selected Data', table.row(this).data())
            });
        }
    };

    const programsTable = new Table('#programsTable', programs, columns, options);
};

function refreshUsersTable() {
    getUsersData();
};

function refreshCoursesTable() {
    coursesData = getCoursesData();
};

function refreshProgramsTable() {
    programData = getProgramData();
};

/*function refreshCoursesTable() {
    $('#all-courses-table').load('/api/Course/GetCourseTable');
}*/
/*function refreshProgramTable() {
    $('#all-programs-table').load('/api/Program/GetProgramTable');
}*/

function assignUsersToCourse(selectedCourseID, selectedUserIDs, selectedRole) {

    console.log(selectedCourseID, selectedUserIDs, selectedRole);
    editCourseUserRoles(selectedCourseID, selectedUserIDs, selectedRole);
}

$(document).ready(function () {
    const containers = { $manageUser: $('.add-user-container'), $allUsers: $('.user-table'), $ManageCourse: $('.course-manager'), $allCourses: $('.all-courses-tab'), $allPrograms: $('.all-programs-tab'), $ManageProgram: $('.program-manager') };

    refreshUsersTable();
    refreshCoursesTable();
    refreshProgramsTable();

    //Event Listeners

    $(".js-course-dd-selection-menu").on('click', 'li a', function (data) {
        const selectedCourseID = $(data.currentTarget).attr('value');

        $(".js-course-dd-selected-value div").text($(this).text());
        $(".js-course-dd-selected-value").attr('selectedCourseID', selectedCourseID);
    });

    $('.js-assign-users-to-course').on('click', function (data) {
        const selectedCourseID = $(".js-course-dd-selected-value").attr('selectedCourseID');
        const roleSelection = $('.js-select-role-to-course input[type="checkbox"]:checked');
        let selectedRole;
        let selectedRoleAsNum;
        if (roleSelection) {
            selectedRole = roleSelection.val();
            switch (selectedRole) {
                case "professor":
                    selectedRoleAsNum = 2;
                    break;
                case "student":
                    selectedRoleAsNum = 1;
                    break;
                default: selectedRoleAsNum = -1;
            }
                // Get the value of the checked checkbox
            console.log('Selected role:', roleSelection.val(), roleSelection);
        } else {
            console.log('No role selected.');
        }
        
        assignUsersToCourse(parseInt(selectedCourseID), selectedUserIDs, selectedRoleAsNum);
    });
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
                toggleObjectVisibility($(containers.$manageUser), true);
                break;
            case 'addCourse':
                toggleObjectVisibility($(containers.$ManageCourse), true);
                break;
            case 'allCourses':
                toggleObjectVisibility($(containers.$allCourses), true);
                refreshCoursesTable();
                break;
            case 'allPrograms':
                toggleObjectVisibility($(containers.$allPrograms), true);
                refreshProgramsTable();
                break;
            case 'addProgram':
                toggleObjectVisibility($(containers.$ManageProgram), true);
                break;
            default:
                hideAllObjects(containers);
                break;
        }
    });

    
    $('.js-add-user').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#registerForm');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            
            $.ajax({
                type: 'PUT',
                url: '/api/User/AddUser',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (response) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">User is added.</div>`);
                    console.error('Error:', textStatus, errorThrown);
                    console.log('User added successfully');
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">User is not added. </div>`);
                    console.error('Error adding user:', errorMessage);
                }
            })
        } else {
            //TODO add form validation errors
        }
  
    });

    $('.js-edit-user').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#editUserForm');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            
            $.ajax({
                type: "POST",
                url: '/api/User/EditUser',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (response) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">User is changed.</div>`);
                    console.error('Error:', textStatus, errorThrown);
                    console.log('User edited successfully');
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">User is not changed.</div>`);
                    console.error('Error editing user:', errorMessage);
                }
            })
        } else {
            //TODO add form validation errors
        }

    });

    $('.js-delete-user').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#deleteUserForm');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            $.ajax({
                type: 'Delete',
                url: '/api/User/DeleteUser',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (response) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">User is deleted.</div>`);
                    console.log('User deleted successfully');
                },
                error: function (xhr, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">User is not deleted.</div>`);
                    console.error('Error deleting user:', errorMessage);
                }
            })
        } else {
            //TODO add form validation errors
        }
    });

    $(".js-change-role").on("click", function (e) {
        e.preventDefault();
        const $form = $('#changeRolesForm');
        let t = $("#changeRolesForm");
        t.find("#userId").val();
        t.find("roleValues").val()
        {
            let e = new FormData(t[0]);
            console.log(t[0]);
            $.ajax({
                type: "POST",
                url: "/api/User/ChangeRoles",
                processData: !1,
                contentType: !1,
                data: e,
                success: function (e) {
                    console.log("Role changed successfully");
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Role is changed </div>`);

                },
                error: function (e, t, r) {
                    console.error("Error changing role:", r)
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Role is not changed</div>`);

                }
            })
        }
    });

    $('.js-add-course').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#createCourse');

        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            $.ajax({
                type: 'PUT',
                url: '/api/Course/AddCourse',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function () {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Course is added.</div>`);
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Course is not added. </div>`);
                    console.error('Error adding program:', errorMessage);
                }
            })
        } else {
            //TODO add form validation errors
        }

    });
    $('.js-edit-course').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#editCourse');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            $.ajax({
                type: "POST",
                url: '/api/Course/EditCourse',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Course is edited.</div>`);
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Course is not edited. </div>`);
                }
            })
        } else {
            //TODO add form validation errors
        }
    });

    $('.js-delete-course').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#deleteCourse');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            $.ajax({
                type: 'Delete',
                url: '/api/Course/DeleteCourse',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Course is deleted.</div>`);
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Course is not deleted.</div>`);
                }
            })
        } else {
            //TODO add form validation errors
        }
    });

    $('.js-add-program').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#createProgram');

        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            $.ajax({
                type: 'PUT',
                url: '/api/Program/AddProgram',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function () {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Program is added.</div>`);
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Program is not added. </div>`);
                    console.error('Error adding program:', errorMessage);
                }
            })
        } else {
            //TODO add form validation errors
        }

    });
    $('.js-edit-program').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#editProgram');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);
            
            $.ajax({
                type: "POST",
                url: '/api/Program/EditProgram',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Program is edited.</div>`);
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Program is not edited. </div>`);
                }
            })
        } else {
            //TODO add form validation errors
        }
    });

    $('.js-delete-program').on('click', function (ev) {
        ev.preventDefault();
        const $form = $('#deleteProgram');
        if (true) {  //TODO add form validation
            const formData = new FormData($form[0]);

            $.ajax({
                type: 'Delete',
                url: '/api/Program/DeleteProgram',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Program is deleted.</div>`);
                },
                error: function (xhr, response, status, error) {
                    const errorMessage = xhr.responseText;
                    $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Program is not deleted.</div>`);
                }
            })
        } else {
            //TODO add form validation errors
        }
    });

    $(".js-reset-pass").on("click", function (e) {
        e.preventDefault();
        const $form = $('#resetPasswordForm');
        let t = $("#resetPasswordForm");
        let formData = t.serialize(); 
        $.ajax({
            type: "POST",  
            url: "/api/User/ResetPassword",
            data: formData,  
            success: function (response) {
                console.log("Got it:", response);
                $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Password is reset.</div><div>Copy and send the link to the user:</div><span>${response}</span>`);
            },
            error: function (xhr, status, error) {
                console.error("Error:", error);
                $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Password is not reset. </div>`);
            }
        });
    });

});