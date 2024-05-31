//This file is used to store commonly used functions to stay on DRY method

export function toggleObjectVisibility($object, state) {//State is false -> hide container, State is true -> show container
    if (state) {
        $object.removeClass('d-none');
    } else {
        $object.addClass('d-none');
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
            const tr = $('<tr>');
    
            tr.append($(`<th scope="row">${assignment.id}</th>`))
            tr.append($(`<td><a href="/Home/Assignment/${assignment.id}" class="text-success">${assignment.assignmentName}</a></td>`))
            tr.append($(`<td class="text-muted">${assignment.assignmentDescription}</td>`))
            tr.append($(`<th>${assignment.deadline}</th>`))
    
            tbody.append(tr);
        });

        parent.append(table)
    } catch(e) {
        parent.append('<p>Error fetching assignments</p>');
    }
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
                break;
            case 3:
                $('.js-users-tab').removeClass('d-none')
                break;
            default:
                $('.js-admin-tab').addClass('d-none');
                $('.js-users-tab').addClass('d-none');
        }
    } else {
        $('.js-admin-tab').addClass('d-none');
        $('.js-users-tab').addClass('d-none');
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