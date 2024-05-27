$(document).ready(function () {
    const url = '/api/Course/GetCourseTable';
    const params = { 
        adminView: false 
    };

    $('#course-list').load(`${url}?${$.param(params)}`);
});