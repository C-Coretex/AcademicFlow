import { getCurrentUser  } from '../../components/utils';

function addCourseBlock(parent, tableData, title = null) {
    const containerDiv = $('<div class="mb-6">');

    if (title) {
        containerDiv.append(title);
    }

    $.ajax({
        url: '/api/Course/GetCourseTable',
        method: "GET",
        dataType: "html",
        data: tableData,
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

        const title = $('<h2 class="display-4 my-4">').text(`Student courses`);
        studentBlock.append(title);

        const hr = $('<hr class="my-4">');
        studentBlock.append(hr);

        if (!programs?.length) {
            studentBlock.append('<p>No courses available.</p>')
        } else {
            $.each(programs, function(_, program) {
                const tableData = { 
                    assignedUserId: userData.id,
                    assingedProgramId: program.id,
                    adminView: false 
                };
                const title = $('<h3 class="display-6 my-4">').text(`${program.name} (${program.semesterNr}. sem)`);
    
                addCourseBlock(studentBlock, tableData, title);
            });
        } 
        
        if (userData.roles.some(role => role == 'Professor')) {
            const professorBlock = $('#professor-course-list')

            const title = $('<h2 class="display-4 my-4">').text(`Professor courses`);
            professorBlock.append(title);

            const hr = $('<hr class="my-4">');
            professorBlock.append(hr);

            const tableData = { 
                assignedUserId: userData.id,
                role: 'Professor',
                adminView: false 
            };

            addCourseBlock(professorBlock, tableData);
        }

    } catch (e) {
        console.error("Error sending AJAX request:", error);
    }
});