import { getCurrentUser, addAssignmentTable } from "../../components/utils";

$(document).ready(async function () {
    const courseBlock = $('#course-list-item');
    
    try {
        const userData = await getCurrentUser();
        if (!userData) {
            courseBlock.append($(`<p>No user data provided</p>`));
            return;
        };

        const id = courseBlock.attr('courseId');
    
        const url = '/api/Course/GetCourse';
        const params = { 
            id: id,
            adminView: false 
        };

        courseBlock.load(`${url}?${$.param(params)}`);

        const assignmentBlock = $('#assignment-list');
        await addAssignmentTable(assignmentBlock, id);
    } catch (e) {
        console.error("Error sending AJAX request:", e);
        assignmentBlock.append(`<div class="alert alert-danger mt-2" role="alert">${e?.responseText ?? 'Internal Error'}</div>`);
    }
    
});