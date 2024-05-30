async function addAssignmentTable(parent, courseId) {
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
}

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