import { getCurrentUser  } from '../../components/utils';

function addCourseBlock(parent, tableParams, title = null) {
    const containerDiv = $('<div class="mb-6">');

    if (title) {
        containerDiv.append(title);
    }

    $.ajax({
        url: '/api/Course/GetCourseTable',
        method: "GET",
        dataType: "html",
        data: tableParams,
        success : function(data) {
            containerDiv.append(data);
        }
    });

    parent.append(containerDiv);
}

$(document).ready(async function () {
    try {
        const userData = await getCurrentUser();
        if (!userData) return;

        const programs = await $.ajax({
            url: '/api/Program/GetProgramTable',
            method: "GET",
            dataType: "json",
            data: { 
                assignedUserId: userData.id 
            }
        });

        const studentBlock = $('#student-course-list')

        studentBlock.append($('<h2 class="display-4 my-4">').text(`Student courses`));
        studentBlock.append($('<hr class="my-4">'));

        if (programs?.length) {
            $.each(programs, function(_, program) {
                const tableParams = { 
                    assignedUserId: userData.id,
                    assingedProgramId: program.id,
                    adminView: false 
                };
                const title = $('<h3 class="display-6 my-4">').text(`${program.name} (${program.semesterNr}. sem)`);
    
                addCourseBlock(studentBlock, tableParams, title);
            });
        } else {
            studentBlock.append($('<p>No courses available.</p>'))
        }

        const professorBlock = $('#professor-course-list')

        professorBlock.append($('<h2 class="display-4 my-4">').text(`Professor courses`));
        professorBlock.append($('<hr class="my-4">'));
        
        if (userData.roles.some(role => role == 'Professor')) {
            const tableParams = { 
                assignedUserId: userData.id,
                role: 'Professor',
                adminView: false 
            };

            addCourseBlock(professorBlock, tableParams);
        } else {
            professorBlock.append($('<p>No courses available.</p>'));
        }

    } catch (e) {
        console.error("Error sending AJAX request:", error);
    }
});