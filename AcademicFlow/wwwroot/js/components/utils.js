//This file is used to store commonly used functions to stay on DRY method

export function toggleObjectVisibility($object, state) {//State is false -> hide container, State is true -> show container
    if (state) {
        $object.removeClass('d-none');
    } else {
        $object.addClass('d-none');
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

//TODO NOT WORKING!!!
export async function getUserByID(id) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: `/api/User/GetUserById?id=${id}`,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log('User data:', data);
                resolve(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                reject(new Error("Error:", textStatus, errorThrown));
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
    console.log(roles,highestRole);
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