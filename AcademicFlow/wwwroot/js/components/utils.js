//This file is used to store commonly used functions to stay on DRY method

export function toggleObjectVisibility($object, state) {//State is false -> hide container, State is true -> show container
    if (state) {
        $object.removeClass('d-none');
    } else {
        $object.addClass('d-none');
    }
}

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

export function proceedLogout() {
    if (confirm("Are you sure you want to proceed?") == true) {
        logoutUser()
            .then(() => { // After successful logout
                window.location.href = "/Home/Login";
            })
                .catch(error => {
                    console.error("Error logging out:", error);
                    // Handle logout errors (optional)
                });
    }
};

export async function renderAssignmentEntryTable(parent, assignmentEntries) {
    try {
        const table = $('<table class="table">');
    
        table.append($(`
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Created</th>
                    <th scope="col">User</th>
                    <th scope="col">File</th>
                    <th scope="col">Grade</th>
                    <th scope="col">Action</th>
                </tr>
            </thead>
        `));
    
        const tbody = $('<tbody>');

        $.each(assignmentEntries?.assignmentEntityOutputModels, function(_, assignment) {
            const assignmentEntry = assignment?.assignmentEntryOutputModel;
            if (!assignmentEntry) return;

            const tr = $('<tr>');
    
            tr.append($(`<th scope="row">${assignmentEntry.id}</th>`))
            tr.append($(`<td class="text-muted">${formatDate(new Date(assignmentEntry.modified))}</td>`))
            tr.append($(`<td class="text-muted">${assignmentEntry.createdBy?.name} ${assignmentEntry.createdBy?.surname}</td>`))
            tr.append($(`<td class="text-muted">${assignmentEntry.fileName}</td>`))
            tr.append($(`<td class="text-muted">${assignment?.assignmentGradeOutputModel ? `<strong>${assignment?.assignmentGradeOutputModel.grade}/10</strong>` : '-'}</td>`))
            tr.append($(`<td><a href="/Home/Assignment/${assignmentEntries?.assignmentTaskOutputModel?.id}/AssignmentEntry/${assignmentEntry.id}" class="text-success">View</a></td>`))
    
            tbody.append(tr);
        });

        table.append(tbody)

        parent.append(table)
    } catch(e) {
        parent.append('<p>Error fetching assignments</p>');
    }
}

export async function addAssignmentTable(parent, courseId) {
    try {
        const assignments = await $.ajax({
            url: '/api/Assignment/GetAllAssignmentsForCourse',
            method: "GET",
            dataType: "json",
            data: { 
                courseId: courseId
            }
        });
    
        if (!assignments?.length) {
            parent.append('<p>No assignments available.</p>');
            return;
        }
    
        const table = $('<table class="table">');
    
        table.append($(`
            <thead class="thead-dark">
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Title</th>
                    <th scope="col">Description</th>
                    <th scope="col">Deadline</th>
                </tr>
            </thead>
        `));
    
        const tbody = $('<tbody>');
    
        $.each(assignments, function(_, assignment) {
            const assignmentTask = assignment?.assignmentTaskOutputModel;
            if (!assignmentTask) return;

            const tr = $('<tr>');
    
            tr.append($(`<th scope="row">${assignmentTask.id}</th>`))
            tr.append($(`<td><a href="/Home/Assignment/${assignmentTask.id}" class="text-success">${assignmentTask.assignmentName}</a></td>`))
            tr.append($(`<td class="text-muted">${assignmentTask.assignmentDescription}</td>`))
            tr.append($(`<th>${formatDate(new Date(assignmentTask.deadline))}</th>`))
    
            tbody.append(tr);
        });

        table.append(tbody)

        parent.append(table)
    } catch(e) {
        parent.append('<p>Error fetching assignments</p>');
    }
}

export function formatDate(date) {
    if (!date || !(date instanceof Date)) return '-';

    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();

    var hours = ('0' + date.getHours()).slice(-2);
    var minutes = ('0' + date.getMinutes()).slice(-2);
    var seconds = ('0' + date.getSeconds()).slice(-2);

    return `${day}.${month}.${year} ${hours}:${minutes}:${seconds}`;
}

export async function getCurrentUser() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/api/Authorization/GetCurrentUser', // Assuming your endpoint path
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                resolve(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                reject(new Error("Error:", textStatus, errorThrown));
            }
        });
    });
}

export async function editCourseUserRoles(courseId, userIds, role) {
    const requestData = {
        id: courseId,
        userIds: userIds,
        role: role, // Assuming role is an enum value that matches the server-side definition
    };
    console.log(requestData);
    try {
        const response = await $.ajax({
            url: "/api/Course/EditCourseUserRoles", // Replace with your actual URL
            method: "POST",
            dataType: "json", // Expected response data type
            data: requestData
        });
        
        if (response.status === "success") {
            console.log("Course user roles edited successfully!");
            // Handle successful update (e.g., display success message)
            return (true);
        } else {
            console.error("Error editing course user roles:", response.message);
            // Handle error (e.g., display error message)
            return (false);
        }
    } catch (error) {
        console.error("Error sending AJAX request:", error);
        // Handle any errors during the AJAX request
        return (false);
    }
}
//EditProgramUserRoles
export async function editProgramUserRoles(programId, usersIds) {
    const requestData = {
        id: programId,
        userIds: usersIds
    };
    console.log('send data', requestData);
    try {
        const response = await $.ajax({
            url: "/api/Program/EditProgramUserRoles", // Replace with your actual URL
            method: "POST",
            dataType: "json", // Expected response data type
            data: requestData
        });

        if (response.status === "success") {
            console.log("Course user roles edited successfully!");
            // Handle successful update (e.g., display success message)
        } else {
            console.error("Error editing course user roles:", response.message);
            // Handle error (e.g., display error message)
        }
    } catch (error) {
        console.error("Error sending AJAX request:", error);
        // Handle any errors during the AJAX request
    }
}


//EditCoursePrograms
export async function editCoursePrograms(courseId, programsIds) {
    const requestData = {
        id: courseId,
        progamIds: programsIds
    };
    console.log('send data',requestData);
    try {
        const response = await $.ajax({
            url: "/api/Course/EditCoursePrograms", // Replace with your actual URL
            method: "POST",
            dataType: "json", // Expected response data type
            data: requestData
        });

        if (response.status === "success") {
            console.log("Course user roles edited successfully!");
            // Handle successful update (e.g., display success message)
        } else {
            console.error("Error editing course user roles:", response.message);
            // Handle error (e.g., display error message)
        }
    } catch (error) {
        console.error("Error sending AJAX request:", error);
        // Handle any errors during the AJAX request
    }
}

export async function getUserByID(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `/api/User/GetUserById?userId=${parseInt(id)}`,
            type: 'GET',
            dataType: 'json',
            processData: false,
            contentType: "application/json",
            success: function (data) {
                resolve(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error:", textStatus, errorThrown);
                reject(new Error("Failed to get user data")); 

            }
        });
    });
}

export async function getCourseByID(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `/api/Course/GetCourse?id=${parseInt(id)}`,
            type: 'GET',
            dataType: 'json',
            processData: false,
            contentType: "application/json",
            success: function (data) {
                resolve(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error:", textStatus, errorThrown);
                reject(new Error("Failed to get user data"));

            }
        });
    });
}

export function redirectUserWithoutAccess(userRole,page) {
    //TODO implement
    let userHasAccess;

    if (!userHasAccess) {
        alert('User does not have an access!');
        window.location.href = '/';
    }
}

export function checkUserPermissionsLevel(roles) {
    const rolePriorities = {
        "Admin": 1,
        "Professor": 2,
        "Student": 3,
    };

    let highestRole = 3; // Initialize with lowest priority (Student)
    for (const role of roles) {
        if (rolePriorities[role] && rolePriorities[role] < highestRole) {
            highestRole = rolePriorities[role];
        }
    }
    return highestRole;
}

export function renderHeaderLinks(userRole) {
    if (userRole) {
        switch (userRole) {
            case 1:
                $('.js-admin-tab').removeClass('d-none')
                break;
            case 2:
                $('.js-users-tab').removeClass('d-none')
                $('.js-assignments-tab').removeClass('d-none');
                $('.js-courses-tab').removeClass('d-none');
                break;
            case 3:
                $('.js-users-tab').removeClass('d-none')
                $('.js-assignments-tab').removeClass('d-none');
                $('.js-courses-tab').removeClass('d-none');
                break;
            default:
                $('.js-admin-tab').addClass('d-none');
                $('.js-users-tab').addClass('d-none');
        }
    } else {
        $('.js-admin-tab').addClass('d-none');
        $('.js-users-tab').addClass('d-none');
        $('.js-assignments-tab').addClass('d-none');
        $('.js-courses-tab').addClass('d-none');
    }
}

export function getURLbyUserRole(roleValue) {
    console.log(roleValue);
    switch (roleValue) {
        case 1:
            return "/Home/AdminCenter"
            break;
        case 2:
            return "/Home/MainPage"
            break;
        case 3:
            return "/Home/MainPage"
            break;
        default:
            return ""
    }
}