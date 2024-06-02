import { getCurrentUser, addAssignmentTable } from '../../components/utils';

$(document).ready(async function () {
    const studentAssignmentBlock = $('#student-assignment-list')
    const professorAssignmentBlock = $('#professor-assignment-list')

    try {
        const userData = await getCurrentUser();
        if (!userData) {
            studentAssignmentBlock.append($(`<p>No user data provided</p>`));
            return;
        };

        const programs = await $.ajax({
            url: '/api/Program/GetProgramTable',
            method: "GET",
            dataType: "json",
            data: { 
                assignedUserId: userData.id 
            }
        });

        studentAssignmentBlock.append($('<h2 class="display-4 my-4">').text(`Student assignments`));
        studentAssignmentBlock.append($('<hr class="my-4">'));
        studentAssignmentBlock.append($('<p class="lead">List of student assignments.</p>'));

        // const studentPrograms = $('<div>')

        // if (programs?.length) {
        //     $.each(programs, async function(_, program) {
        //         const studentProgram = $('<div>');
        //         studentProgram.append($('<h3 class="display-6 my-4">').text(`${program.name} (${program.semesterNr}. sem)`));
        //         studentProgram.append($('<hr class="my-4">'));

        //         const studentCourses = await $.ajax({
        //             url: '/api/Course/GetCourseTable',
        //             method: "GET",
        //             dataType: "json",
        //             data: { 
        //                 assignedUserId: userData.id,
        //                 assingedProgramId: program.id
        //             }
        //         });

        //         if (studentCourses?.length) {
        //             $.each(studentCourses, function(_, course) {
        //             const studentAssignments = $('<div class="assignments d-flex flex-column gap-3 mt-5 mb-4">');
        //             studentAssignments.append($(`<a href="Course/${course.id}" class="lead text-success">${course.name}</a>`));
        
        //             addAssignmentTable(studentAssignments, course.id);
        
        //             studentProgram.append(studentAssignments)
        //         });
        //         } else {
        //             studentProgram.append($('<p>No assignments available.</p>'))
        //         }

        //         studentPrograms.append(studentProgram)
        //     });

        //     studentAssignmentBlock.append(studentPrograms)
        // } else {
        //     studentAssignmentBlock.append($('<p>No assignments available.</p>'))
        // }

        // QuickFix
        if (userData.roles.some(role => role == 'Student')) {
            studentAssignmentBlock.append($('<h3 class="display-6 my-4">').text(`Student courses`));
            studentAssignmentBlock.append($('<hr class="my-4">'));

            const studentCourses = await $.ajax({
                url: '/api/Course/GetCourseTable',
                method: "GET",
                dataType: "json",
                data: { 
                    assignedUserId: userData.id,
                    role: 'Student'
                }
            });

            if (studentCourses?.length) {
                $.each(studentCourses, function(_, course) {
                    const studentAssignments = $('<div class="assignments d-flex flex-column gap-3 mt-5">');
                    studentAssignments.append($(`<a href="Course/${course.id}" class="lead text-success">${course.name}</a>`));
        
                    addAssignmentTable(studentAssignments, course.id);
        
                    studentAssignmentBlock.append(studentAssignments)
                });
            } else {
                studentAssignmentBlock.append($('<p>No assignments available.</p>'))
            }
        } else {
            studentAssignmentBlock.append($('<p>No assignments available.</p>'))
        }
        // QuickFix

        professorAssignmentBlock.append($('<h2 class="display-4 my-4">').text(`Professor assignments`));
        professorAssignmentBlock.append($('<hr class="my-4">'));
        professorAssignmentBlock.append($('<p class="lead">List of professor assignments.</p>'));

        if (userData.roles.some(role => role == 'Professor')) {
            professorAssignmentBlock.append($('<h3 class="display-6 my-4">').text(`Professor courses`));
            professorAssignmentBlock.append($('<hr class="my-4">'));

            const professorCourses = await $.ajax({
                url: '/api/Course/GetCourseTable',
                method: "GET",
                dataType: "json",
                data: { 
                    assignedUserId: userData.id,
                    role: 'Professor'
                }
            });

            if (professorCourses?.length) {
                $.each(professorCourses, function(_, course) {
                    const professorAssignments = $('<div class="assignments d-flex flex-column gap-3 mt-5">');
                    professorAssignments.append($(`<a href="Course/${course.id}" class="lead text-success">${course.name}</a>`));
        
                    addAssignmentTable(professorAssignments, course.id);
        
                    professorAssignmentBlock.append(professorAssignments)
                });
            } else {
                professorAssignmentBlock.append($('<p>No assignments available.</p>'))
            }

            professorAssignmentBlock.append($('<a class="btn btn-success mt-4" href="/Home/Assignment/New">Add new assignment</a>'))
        } else {
            professorAssignmentBlock.append($('<p>No assignments available.</p>'))
        }

    } catch (e) {
        console.error("Error sending AJAX request:", e);
        professorAssignmentBlock.append(`<div class="alert alert-danger mt-2" role="alert">${e?.responseText ?? 'Internal Error'}</div>`);
    }
});