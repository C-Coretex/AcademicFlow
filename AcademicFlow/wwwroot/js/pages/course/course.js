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

        const courseGradesResponse = await $.ajax({
            url: `/api/Assignment/GetAllAssignmentGradesForAllCourses`,
            method: "Get",
            dataType: "json"
        });
        
        const gradeBlock = $('#grade-block'); 
        gradeBlock.append($(`<h2 class="display-6 my-4">Grade</h2>`));

        const currentCourseGrade = courseGradesResponse?.find(c => c.course.id == id)?.averageCourseGrade;

        if (currentCourseGrade) {
            gradeBlock.append($(`<p class="lead">Grade: <strong>${currentCourseGrade}/10</strong></p>`));
        } else {
            gradeBlock.append($(`<p class="lead">Not graded yet</p>`));
        }

        const assignmentBlock = $('#assignment-list');
        await addAssignmentTable(assignmentBlock, id);
    } catch (e) {
        console.error("Error sending AJAX request:", e);
        courseBlock.append(`<div class="alert alert-danger mt-2" role="alert">${e?.responseText ?? 'Internal Error'}</div>`);
    }
    
});