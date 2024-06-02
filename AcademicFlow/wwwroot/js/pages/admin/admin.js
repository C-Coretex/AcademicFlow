import { toggleObjectVisibility, editCourseUserRoles, getUserCourses, editProgramUserRoles, getProgramByID, checkUserPermissionsLevel, getCurrentUser, renderHeaderLinks, getUserByID, getCourseByID, editCoursePrograms  } from "./../../components/utils.js";
import { Table } from "./../../components/table.js";

let selectedUsersIDs = [];
let selectedProgramsIDs = [];
let selectedCoursesIDs = [];
let coursesData;
let programData

//functions
function hideAllObjects(containers) {
    for (const containerName in containers) {
        toggleObjectVisibility(containers[containerName], false);
    }
}

async function getUsersData() {
    try {
        const response = await $.ajax({
            url: "/api/User/GetAllUsers",
            method: "GET",
            dataType: "json"
        });
        return response;
    } catch (error) {
        console.error("Error fetching data:", error);
        // Handle errors appropriately (e.g., display error message to user)
        return undefined; // Or throw an error for further handling
    }
}

async function getCoursesData() {
    try {
        const response = await $.ajax({
            url: "/api/Course/GetCourseTable",
            method: "GET",
            dataType: "json"
        });
        return response;
    } catch (error) {
        console.error("Error fetching data:", error);
        // Handle errors appropriately (e.g., display error message to user)
        return undefined; // Or throw an error for further handling
    }
}

async function getProgramsData() {
    try {
        const response = await $.ajax({
            url: "/api/Program/GetProgramTable",
            method: "GET",
            dataType: "json"
        });
        return response;
    } catch (error) {
        console.error("Error fetching data:", error);
        // Handle errors appropriately (e.g., display error message to user)
        return undefined; // Or throw an error for further handling
    }
}
function renderCoursesDropdownValues(courseData) {
    const dropdownMenu = $('.js-dd-courses').find('.js-course-dd-selected-value'); // Select the dropdown menu element
    dropdownMenu.empty();
    dropdownMenu.append(`<option value=""> </option>`);

    courseData.forEach(course => {
        const option = `<option value="${course.id}">${course.publicId}: ${course.name}</option>`;
        dropdownMenu.append(option);
    });
}

function renderProgrammsDropdownValues(programData) {
    const dropdownMenu = $('.js-dd-programs').find('.js-program-dd-selected-value'); // Select the dropdown menu element
    dropdownMenu.empty();
    dropdownMenu.append(`<option value=""> </option>`);

    programData.forEach(program => {
        const option = `<option value="${program.id}">${program.semesterNr}: ${program.name}</option>`;
        dropdownMenu.append(option);
    });
}

async function showUserManagementTools(userId) {
    const $manageUser = $('.add-user-container');
    const $editUser = $('.user-table');
    toggleObjectVisibility($manageUser, true);
    toggleObjectVisibility($editUser, false);

    const userData = await getUserByID(userId);
    const userCourses = await getUserCourses(userId);
    $('.js-user-fullname').text(userData.name + ' ' + userData.surname);
    renderUserData(userData);
    renderAssignedUserCourses(userCourses);
    console.log(userData);

}
async function showCourseManagementTools(courseId) {
    const $manageCourse = $('.course-manager');
    const $editCourse = $('.all-courses-tab');
    toggleObjectVisibility($manageCourse, true);
    toggleObjectVisibility($editCourse, false);

    const courseData = await getCourseByID(courseId);
    $('.js-course-title').text(courseData.publicId + ": " + courseData.name);
    renderCourseData(courseData);
    console.log(courseData);

}
async function showProgramManagementTools(programId) {
    const $manageProgram = $('.all-programs-tab');
    const $editProgram = $('.program-manager');
    toggleObjectVisibility($editProgram, true);
    toggleObjectVisibility($manageProgram, false);

    const programData = await getProgramByID(programId);
    $('.js-course-title').text("Semester: " + programData.semesterNr + ", Program: " + programData.name);
    renderProgramData(programData);
    console.log(programData);

}
function renderUserData(data) {
    const userElement = document.querySelector('.js-show-user-data');
    let htmlContent = '';

    htmlContent += `<h2>Information</h2>`;
    htmlContent += `<table>`;

    // Table header row
    htmlContent += `<tr>`;
    htmlContent += `<th>Field</th>`;
    htmlContent += `<th>Value</th>`;
    htmlContent += `</tr>`;

    // Table body rows
    for (const key in data) {
        if (key !== 'userRegistrationData') { // Exclude nested object
            const value = data[key] || 'N/A'; // Handle missing data
            htmlContent += `<tr>`;
            htmlContent += `<td>${key}</td>`;
            htmlContent += `<td class="js-user-info-${key}" data-${key}="${value}">${value}</td>`;
            htmlContent += `</tr>`;
        }
    }

    // Registration information (optional)
    if (data.userRegistrationData) {
        htmlContent += `<tr>`;
        htmlContent += `<td>Registered</td>`;
        htmlContent += `<td>${data.userRegistrationData.isRegistered ? 'Yes' : 'No'}</td>`;
        htmlContent += `</tr>`;

        if (data.userRegistrationData.registrationUrl) {
            htmlContent += `<tr>`;
            htmlContent += `<td>Registration Link</td>`;
            htmlContent += `<td><a href="${data.userRegistrationData.registrationUrl}">Link</a></td>`;
            htmlContent += `</tr>`;
        }
    }

    htmlContent += `</table>`;

    userElement.innerHTML = htmlContent;
}
function renderCourseData(data) {
    const userElement = document.querySelector('.js-show-course-data');
    let htmlContent = '';

    htmlContent += `<h2>Information</h2>`;
    htmlContent += `<table>`;

    // Table header row
    htmlContent += `<tr>`;
    htmlContent += `<th>Field</th>`;
    htmlContent += `<th>Value</th>`;
    htmlContent += `</tr>`;

    // Table body rows
    for (const key in data) {
        const value = data[key] || 'N/A'; // Handle missing data
        htmlContent += `<tr>`;
        htmlContent += `<td>${key}</td>`;
        htmlContent += `<td class="js-course-info-${key}" data-${key}="${value}">${value}</td>`;
        htmlContent += `</tr>`;
    }

    htmlContent += `</table>`;

    userElement.innerHTML = htmlContent;
}

function renderProgramData(data) {
    const userElement = document.querySelector('.js-show-program-data');
    let htmlContent = '';

    htmlContent += `<h2>Information</h2>`;
    htmlContent += `<table>`;

    // Table header row
    htmlContent += `<tr>`;
    htmlContent += `<th>Field</th>`;
    htmlContent += `<th>Value</th>`;
    htmlContent += `</tr>`;

    // Table body rows
    for (const key in data) {
        const value = data[key] || 'N/A'; // Handle missing data
        htmlContent += `<tr>`;
        htmlContent += `<td>${key}</td>`;
        htmlContent += `<td class="js-program-info-${key}" data-${key}="${value}">${value}</td>`;
        htmlContent += `</tr>`;
    }

    htmlContent += `</table>`;

    userElement.innerHTML = htmlContent;
}

function renderAssignedUserCourses(data) {
    const userElement = document.querySelector('.js-show-user-courses');
    let htmlContent = '';

    htmlContent += `<h2>User Courses</h2>`;
    htmlContent += `<table>`;

    // Table header row
    htmlContent += `<tr>`;
    htmlContent += `<th>Field</th>`;
    htmlContent += `<th>Value</th>`;
    htmlContent += `</tr>`;

    // Table body rows
    for (const key in data) {
        const value = data[key] || 'N/A'; // Handle missing data
        htmlContent += `<tr>`;
        htmlContent += `<td>${key}</td>`;
        htmlContent += `<td data-${key}="${value}">${value}</td>`;
        htmlContent += `</tr>`;
    }

    htmlContent += `</table>`;

    userElement.innerHTML = htmlContent;
}


function initUsersTable(users) {

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
            createdCell: function (td, cellData, rowData, row, col) {
                if (td.querySelector('.js-copy')) {
                    td.querySelector('.js-copy').addEventListener('click', function (e) {
                        e.stopPropagation();
                        const $button = $(this);

                        const link = $button.data('link');
                        console.log(link);
                        navigator.clipboard.writeText(link);

                    }, true);
                }
                

            },
            render: function (data, type, rowData, meta) {
                return data ? `<button class="btn btn-primary not-for-select js-copy" data-link="${data}"><i class="fa-regular fa-copy not-for-select p-2" data-link="${data}"></i></button>` : "";
            }
            
        },
        {
            targets: 8,
            title: 'Role',
            name: 'roles',
            data: 'roles'
        },
        {
            targets: 9,
            title: 'Actions',
            name: 'id',
            data: 'id',
            createdCell: function (td, cellData, rowData, row, col) {
                if (td.querySelector('.js-table-edit-user')) {
                    td.querySelector('.js-table-edit-user').addEventListener('click', function (e) {
                        e.stopPropagation();
                        const $button = $(this);
                        let deleteBtn = $button[0];

                        const userID = parseInt($button.data('userid'));
                        console.log('selected ID', userID);
                        showUserManagementTools(userID);

                    }, true);
                }


            },
            render: function (data, type, rowData, meta) {

                if (data !== 1) {
                    return `<button class="btn btn-primary not-for-select js-table-edit-user" data-userid="${data}"><i class="fa-regular fa-edit p-2 not-for-select"></i></button>`;
                } else {
                    return "";
                }
            }
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function () {
            let table = $('#usersTable').DataTable();
            table.rows().every(row => $(row.node()).css('cursor', 'pointer'));

            $('#usersTable tbody').on('click', 'tr', function (event) {
                event.stopPropagation();
                const that = this;
                const clickedElement = $(event.target);
                console.log(clickedElement);

                if (!clickedElement.hasClass('not-for-select')) {
                    if ($(that).hasClass('selected')) {
                        $(that).removeClass('selected');
                    } else {
                        $(that).addClass('selected');
                    }
                    const selectedData = $('#usersTable').DataTable().row(that).data();
                    selectedUsersIDs = $.map(table.rows('.selected').data(), function (item) {
                        return item.id
                    });
                    //let requestedUser = await getUserByID();
                    
                    getUserByID(selectedData.id)
                        .then(userData => {
                            
                            //showUserManagementTools(userData);
                        })
                        .catch(error => {
                            console.error('Error fetching user data:', error);
                        });


                    console.log('Selected Data', selectedData);
                }
            });
        }
    };

    const usersTable = new Table('#usersTable', users, columns, options);
};

function initCoursesTable(courses) {
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
            data: 'description',
            width: 250
        },
        {
            targets: 5,
            title: 'Image',
            name: 'imageUrl',
            data: 'imageUrl'
        },
        {
            targets: 6,
            title: 'Actions',
            name: 'id',
            data: 'id',
            createdCell: function (td, cellData, rowData, row, col) {
                if (td.querySelector('.js-table-edit-course')) {
                    td.querySelector('.js-table-edit-course').addEventListener('click', function (e) {
                        e.stopPropagation();
                        const $button = $(this);

                        const courseID = parseInt($button.data('courseid'));
                        console.log('selected ID', courseID);
                        showCourseManagementTools(courseID);

                    }, true);
                }
            },
            render: function (data, type, rowData, meta) {

                return `<button class="btn btn-primary not-for-select js-table-edit-course" data-courseid="${data}"><i class="fa-regular fa-edit p-2 not-for-select"></i></button>`;

            }
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function () {
            let table = $('#coursesTable').DataTable();
            table.rows().every(row => $(row.node()).css('cursor', 'pointer'));

            $('#coursesTable tbody').on('click', 'tr', function (event) {
                /*event.stopPropagation();
                const that = this;
                console.log($(that).hasClass('selected'));
                if ($(that).hasClass('selected')) {
                    $(that).removeClass('selected');
                } else {
                    $(that).addClass('selected');
                }

                const selectedData = $('#coursesTable').DataTable().row(that).data();
                selectedCoursesIDs = $.map(table.rows('.selected').data(), function (item) {
                    return item.id
                });
                getCourseByID(selectedData.id)
                    .then(courseData => {
                        //showUserManagementTools(userData);
                    })
                    .catch(error => {
                        console.error('Error fetching user data:', error);
                    });

                console.log('Selected Data', selectedData);*/

                event.stopPropagation();
                const that = this;
                const clickedElement = $(event.target);
                console.log(clickedElement);

                if (!clickedElement.hasClass('not-for-select')) {
                    if ($(that).hasClass('selected')) {
                        $(that).removeClass('selected');
                    } else {
                        $(that).addClass('selected');
                    }
                    const selectedData = $('#coursesTable').DataTable().row(that).data();
                    selectedCoursesIDs = $.map(table.rows('.selected').data(), function (item) {
                        return item.id
                    });
                    //let requestedUser = await getUserByID();

                    getCourseByID(selectedData.id)
                        .then(courseData => {
                            //
                        })
                        .catch(error => {
                            console.error('Error fetching user data:', error);
                        });


                    console.log('Selected Data', selectedData);
                }
            });
        }
    };

    const coursesTable = new Table('#coursesTable', courses, columns, options);
};

function initProgramsTable(programs) {
    const columns = [
        {
            targets: 0,
            title: 'ID',
            name: 'id',
            data: 'id',
            width: 80
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
            data: 'semesterNr',
            width: 100
        },
        {
            targets: 3,
            title: 'Actions',
            name: 'id',
            data: 'id',
            createdCell: function (td, cellData, rowData, row, col) {
                if (td.querySelector('.js-table-edit-program')) {
                    td.querySelector('.js-table-edit-program').addEventListener('click', function (e) {
                        e.stopPropagation();
                        const $button = $(this);

                        const programID = parseInt($button.data('programid'));
                        console.log('selected ID', programID);
                        showProgramManagementTools(programID);

                    }, true);
                }
            },
            render: function (data, type, rowData, meta) {

                return `<button class="btn btn-primary not-for-select js-table-edit-program" data-programid="${data}"><i class="fa-regular fa-edit p-2 not-for-select"></i></button>`;

            }
        }
    ];
    const options = {
        ordering: true,
        order: [[0, 'asc']],
        select: true,
        initComplete: function (event) {
            let table = $('#programsTable').DataTable();
            table.rows().every(row => $(row.node()).css('cursor', 'pointer'));
            
            $('#programsTable tbody').on('click', 'tr', function (event) {
             /*   event.stopPropagation();
                const that = this;
                console.log($(that).hasClass('selected'));
                if ($(that).hasClass('selected')) {
                    $(that).removeClass('selected');
                } else {
                    $(that).addClass('selected');
                }
                const selectedData = $('#programsTable').DataTable().row(that).data();

                console.log('Selected Data', selectedData);

                selectedProgramsIDs = $.map(table.rows('.selected').data(), function (item) {
                    console.log('item',item);
                    return item.id
                });
                console.log('programsIds',selectedProgramsIDs);*/
                event.stopPropagation();
                const that = this;
                const clickedElement = $(event.target);
                console.log(clickedElement);

                if (!clickedElement.hasClass('not-for-select')) {
                    if ($(that).hasClass('selected')) {
                        $(that).removeClass('selected');
                    } else {
                        $(that).addClass('selected');
                    }
                    const selectedData = $('#programsTable').DataTable().row(that).data();
                    selectedProgramsIDs = $.map(table.rows('.selected').data(), function (item) {
                        return item.id
                    });
                    //let requestedUser = await getUserByID();

                    getProgramByID(selectedData.id)
                        .then(programData => {
                            //
                        })
                        .catch(error => {
                            console.error('Error fetching user data:', error);
                        });


                    console.log('Selected Data', selectedData);
                }
            });
        }
    };

    const programsTable = new Table('#programsTable', programs, columns, options);
};

function refreshUsersTable(tableData) {
    $('#usersTable').DataTable().clear(); // Clear existing data
    $('#usersTable').DataTable().rows.add(tableData).draw();
};

function refreshCoursesTable(tableData) {
    $('#coursesTable').DataTable().clear(); // Clear existing data
    $('#coursesTable').DataTable().rows.add(tableData).draw();
    renderCoursesDropdownValues(tableData);
};

function refreshProgramsTable(tableData) {
    $('#programsTable').DataTable().clear(); // Clear existing data
    $('#programsTable').DataTable().rows.add(tableData).draw();
    renderProgrammsDropdownValues(tableData);
}

async function assignUsersToCourse(selectedCourseID, selectedUsersIDs, selectedRole) {
    const result = await editCourseUserRoles(selectedCourseID, selectedUsersIDs, selectedRole);
    if (result) {
        triggerRefreshUsersTable();
    }
}

async function assignProgramsToCourse(selectedProgramID, selectedUsersIDs) {
    const result = await editProgramUserRoles(selectedProgramID, selectedUsersIDs);
    if (result) {
        triggerRefreshUsersTable();
    }
}

async function triggerRefreshUsersTable() {
    const usersData = await getUsersData();
    console.log('test here',usersData);
    refreshUsersTable(usersData);
}
async function triggerRefreshCoursesTable() {
    const coursesData = await getCoursesData();
    console.log('test here', coursesData);
    refreshCoursesTable(coursesData);
}
async function triggerRefreshProgramsTable() {
    const programsData = await getProgramsData();
    console.log('test here', programsData);
    refreshProgramsTable(programsData);
}

async function triggerGetUserByID(id) {
    const userData = await getUserByID(id);
    return userData;
}
async function triggergetUserCourses(id) {
    const userCoursesData = await getUserCourses(id);
    return userCoursesData;
}
async function triggerGetCourseByID(id) {
    const courseData = await getCourseByID(id);
    return courseData;
}
async function triggerGetProgramByID(id) {
    const courseData = await getProgramByID(id);
    return courseData;
}

function refreshUsersInfoTable(id) {
    triggerGetUserByID(id)
        .then(userData => { // Resolve function receives the user data
            renderUserData(userData);
            console.log(userData); // Optional: Log the user data
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
            // Handle errors appropriately (e.g., display an error message to the user)
        });
}
function refreshUsersCoursesInfoTable(id) {
    triggergetUserCourses(id)
        .then(userData => { // Resolve function receives the user data
            renderAssignedUserCourses(userData);
            console.log(userData); // Optional: Log the user data
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
            // Handle errors appropriately (e.g., display an error message to the user)
        });
}
function refreshCoursesInfoTable(id) {
    triggerGetCourseByID(id)
        .then(courseData => { // Resolve function receives the user data
            renderCourseData(courseData);
            console.log(courseData); // Optional: Log the user data
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
            // Handle errors appropriately (e.g., display an error message to the user)
        });
}
function refreshProgramsInfoTable(id) {
    triggerGetProgramByID(id)
        .then(programData => { // Resolve function receives the user data
            renderProgramData(programData);
            console.log(programData); // Optional: Log the user data
        })
        .catch(error => {
            console.error('Error fetching user data:', error);
            // Handle errors appropriately (e.g., display an error message to the user)
        });
}

$(document).ready(async function () {
    const containers = { $manageUser: $('.add-user-container'), $allUsers: $('.user-table'), $ManageCourse: $('.course-manager'), $allCourses: $('.all-courses-tab'), $allPrograms: $('.all-programs-tab'), $ManageProgram: $('.program-manager') };
    initUsersTable();
    initCoursesTable();
    initProgramsTable();



    try {
        const usersData = await getUsersData();
        console.log(usersData);
        refreshUsersTable(usersData);
    } catch (error) {
        console.error("Error fetching user data:", error);
        // Handle errors here (e.g., display error message)
    }

    try {
        const coursesData = await getCoursesData();
        refreshCoursesTable(coursesData);
    } catch (error) {
        console.error("Error fetching user data:", error);
        // Handle errors here (e.g., display error message)
    }

    try {
        const programsData = await getProgramsData();
        refreshProgramsTable(programsData);
    } catch (error) {
        console.error("Error fetching user data:", error);
        // Handle errors here (e.g., display error message)
    }
    

    //Event Listeners
    $(".js-back-to-user-manager").on('click', function (data) {
        const $manageUser = $('.add-user-container');
        const $editUser = $('.user-table');
        toggleObjectVisibility($manageUser, false);
        toggleObjectVisibility($editUser, true);
        triggerRefreshUsersTable();

    });
    $(".js-back-to-course-manager").on('click', function (data) {
        const $manageCourse = $('.all-courses-tab');
        const $editCourse = $('.course-manager');
        toggleObjectVisibility($editCourse, false);
        toggleObjectVisibility($manageCourse, true);
        triggerRefreshCoursesTable();

    });
    $(".js-back-to-program-manager").on('click', function (data) {
        const $manageProgram = $('.all-programs-tab');
        const $editProgram = $('.program-manager');
        toggleObjectVisibility($editProgram, false);
        toggleObjectVisibility($manageProgram, true);
        triggerRefreshProgramsTable();

    });

   /* $(".js-course-dd-selection-menu").on('click', 'li a', function (data) {
        const selectedCourseID = $(data.currentTarget).attr('value');
        console.log($(this).text());
        $(".js-course-dd-selected-value").text(`${$(this).text()}`);
        console.log(selectedCourseID);
    });
    $(".js-program-dd-selection-menu").on('click', 'li a', function (data) {
        const selectedProgramID = $(data.currentTarget).attr('value');
        console.log($(this).text());
        $(".js-program-dd-selected-value").text(`${$(this).text()}`);
        console.log(selectedProgramID);
    });*/

    $('.js-assign-users-to-course').on('click', function (data) {
        const selectedCourseID = $(".user-table").find(".js-course-dd-selected-value").find(":selected").val();
        const selectedRole = $('.js-select-role-to-course').find(":selected").val();
        let selectedRoleAsNum;
        if (selectedRole) {
            switch (selectedRole) {
                case "professor":
                    selectedRoleAsNum = 2;
                    break;
                case "student":
                    selectedRoleAsNum = 1;
                    break;
                default: selectedRoleAsNum = null;
            }
                // Get the value of the checked checkbox
            console.log('Selected role:', selectedRole);
        } else {
            console.log('No role selected.');
        }
        
        assignUsersToCourse(parseInt(selectedCourseID), selectedUsersIDs, selectedRoleAsNum);
        //triggerRefreshUsersTable();
    });
    $('.js-assign-programms-to-course').on('click', function (data) {
        const selectedCourseID = $(".all-programs-tab").find(".js-course-dd-selected-value").find(":selected").val();
        assignProgramsToCourse(parseInt(selectedCourseID), selectedProgramsIDs);
        //triggerRefreshUsersTable();
    });

    $('.js-assign-users-to-program').on('click', function (data) {
        const selectedProgramID = $(".user-table").find(".js-program-dd-selected-value").find(":selected").val();
        console.log(selectedProgramID);
        assignProgramsToCourse(parseInt(selectedProgramID), selectedUsersIDs);
        //triggerRefreshUsersTable();
    });

    $('.nav-item').on('click', async function () {
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
                try {
                    const usersData = await getUsersData();
                    console.log(usersData);
                    refreshUsersTable(usersData);
                } catch (error) {
                    console.error("Error fetching user data:", error);
                    // Handle errors here (e.g., display error message)
                }
                
                break;
            case 'addUser':
                toggleObjectVisibility($(containers.$manageUser), true);
                break;
            case 'addCourse':
                toggleObjectVisibility($(containers.$ManageCourse), true);
                break;
            case 'allCourses':
                toggleObjectVisibility($(containers.$allCourses), true);
                try {
                    const coursesData = await getCoursesData();
                    refreshCoursesTable(coursesData);
                } catch (error) {
                    console.error("Error fetching user data:", error);
                    // Handle errors here (e.g., display error message)
                }
                break;
            case 'allPrograms':
                toggleObjectVisibility($(containers.$allPrograms), true);
                try {
                    const programsData = await getProgramsData();
                    console.log(programsData);
                    refreshProgramsTable(programsData);
                } catch (error) {
                    console.error("Error fetching user data:", error);
                    // Handle errors here (e.g., display error message)
                }
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
                    console.log('User added successfully');
                    triggerRefreshUsersTable();
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
        const userId = parseInt($('.js-user-info-id').data('id'));
        if (true) {  //TODO add form validation
            const formData = new FormData();
            console.log($form[0]);
            formData.append('id',parseInt($('.js-user-info-id').data('id')));
            const formElements = $form.serializeArray();
            formElements.forEach(element => {
                formData.append(element.name, element.value);
            });
            
            $.ajax({
                type: "POST",
                url: '/api/User/EditUser',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (response) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">User is changed.</div>`);
                    console.log('User edited successfully');
                    refreshUsersInfoTable(userId);
                    refreshUsersCoursesInfoTable(userId);

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
        const userID = parseInt($('.js-user-info-id').data('id'));
        if (true) {  //TODO add form validation
            const formData = new FormData();
            formData.append('userId', userID);
            $.ajax({
                type: 'Delete',
                url: '/api/User/DeleteUser',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (response) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">User is deleted.</div>`);
                    console.log('User deleted successfully');
                    document.querySelector('.js-back-to-user-manager').click();
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
        const userID = parseInt($('.js-user-info-id').data('id'));
        let t = $("#changeRolesForm");
        t.find("#userId").val();


        //const selectedRoles = t.find("roleValues").val();

        const roleCheckboxes = document.querySelectorAll('input[type="checkbox"][name="roles"]');
        const selectedRoles = [];

        let formData = new FormData();
        formData.append('userId', userID);
        roleCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                const roleValue = checkbox.value; 
                formData.append('roles', roleValue);
            }
        });

        console.log(formData);
        $.ajax({
            type: "POST",
            url: "/api/User/ChangeRoles",
            processData: !1,
            contentType: !1,
            data: formData,
            success: function (e) {
                console.log("Role changed successfully");
                $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Role is changed </div>`);
                refreshUsersInfoTable(userID);
            },
            error: function (e, t, r) {
                console.error("Error changing role:", r)
                $form.find('.error-message').html(`<div class="alert alert-danger mt-2" role="alert">Role is not changed</div>`);

            }
        })
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
                    triggerRefreshCoursesTable();
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
        const courseID = parseInt($('.js-course-info-id').data('id'))
        if (true) {  //TODO add form validation
            const formData = new FormData();
            formData.append('id', parseInt($('.js-course-info-id').data('id')));
            const formElements = $form.serializeArray();
            formElements.forEach(element => {
                formData.append(element.name, element.value);
            });
            $.ajax({
                type: "POST",
                url: '/api/Course/EditCourse',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Course is edited.</div>`);
                    refreshCoursesInfoTable(courseID);
                    //TODO refresh info table
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
        const courseID = parseInt($('.js-course-info-id').data('id'));
        if (true) {  //TODO add form validation
            const formData = new FormData();
            formData.append('id', courseID);
            $.ajax({
                type: 'Delete',
                url: '/api/Course/DeleteCourse',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Course is deleted.</div>`);
                    document.querySelector('.js-back-to-course-manager').click();
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
                    triggerRefreshProgramsTable();
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
        const programID = parseInt($('.js-program-info-id').data('id'))
        if (true) {  //TODO add form validation
            const formData = new FormData();
            formData.append('id', programID);
            const formElements = $form.serializeArray();
            formElements.forEach(element => {
                formData.append(element.name, element.value);
            });
            
            $.ajax({
                type: "POST",
                url: '/api/Program/EditProgram',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    refreshProgramsInfoTable(programID);
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
        const programID = parseInt($('.js-program-info-id').data('id'));
        if (true) {  //TODO add form validation
            const formData = new FormData();
            formData.append('id', programID);
            $.ajax({
                type: 'Delete',
                url: '/api/Program/DeleteProgram',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function (_) {
                    $form.find('.error-message').html(`<div class="alert alert-success mt-2" role="alert">Program is deleted.</div>`);
                    document.querySelector('.js-back-to-program-manager').click();
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
        let formData = new FormData();
        const userID = parseInt($('.js-user-info-id').data('id'));
        formData.append('userId', userID);

        $.ajax({
            type: "POST",  
            url: "/api/User/ResetPassword",
            data: { userId: userID },  
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