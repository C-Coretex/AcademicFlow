$(document).ready(function () {
    const id = $('#course-list-item').attr('courseId');
    const url = '/api/Course/GetCourse';
    const params = { 
        id: id,
        adminView: false 
    };

    $('#course-list-item').load(`${url}?${$.param(params)}`);
});