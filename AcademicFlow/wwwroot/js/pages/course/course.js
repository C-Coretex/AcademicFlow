import { addAssignmentTable } from "../../components/utils";


$(document).ready(async function () {
    const id = $('#course-list-item').attr('courseId');
    
    const url = '/api/Course/GetCourse';
    const params = { 
        id: id,
        adminView: false 
    };

    $('#course-list-item').load(`${url}?${$.param(params)}`);

    const assignmentBlock = $('#assignment-list');
    addAssignmentTable(assignmentBlock, id);
});