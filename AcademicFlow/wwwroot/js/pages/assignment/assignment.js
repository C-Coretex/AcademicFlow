import { getCurrentUser } from '../../components/utils';

$(document).ready(async function () {
  try {
    const userData = await getCurrentUser();
    if (!userData) return;

    const assignmentBlock = $('#assignment-item');
    const id = assignmentBlock.attr('assignmentId');

    const assignment = await $.ajax({
        url: '/api/Assignment/GetAssignmentTask',
        method: "GET",
        dataType: "json",
        data: { 
            id: id
        }
    });

    //!!!TODO 

    // const assignment = {
    //     id: 1,
    //     assignmentName: 'Test assignment',
    //     assignmentDescription: 'desc',
    //     assignmentWeight: 2,
    //     deadline: new Date(2025, 1, 29),
    //     modified: new Date(2024, 1, 29),
    //     createdBy: {
    //         id: 1,
    //         name: "John",
    //         surname: "Doe",
    //         personalCode: "123123-12312",
    //         email: "john.doe@example.com",
    //         phoneNumber: "+1234567890",
    //         age: 30,
    //         roles: ["Professor"],
    //         userRegistrationData: {
    //             isRegistered: true,
    //             registrationUrl: "https://example.com/register"
    //         }
    //     }
    // }

    $('.breadcrumb-item.active').text(assignment.assignmentName);

    assignmentBlock.append($(`<h1 class="display-4 my-4">${assignment.assignmentName}</h1>`));
    assignmentBlock.append($(`<hr class="my-4">`));
    assignmentBlock.append($(`<p class="lead">${assignment.assignmentDescription}</p>`));
    assignmentBlock.append($(`<p><strong>Lecturer:</strong> ${assignment.createdBy.name} ${assignment.createdBy.surname}</p>`));
    assignmentBlock.append($(`<p><strong>Last modified:</strong> ${assignment.modified.toLocaleDateString()}</p>`));
    assignmentBlock.append($(`<p><strong>Deadline:</strong> ${assignment.deadline.toLocaleDateString()}</p>`));

    if (userData.personalCode == assignment.createdBy.personalCode) {
        const assignmentEntriesBlock = $('<div class="my-6">')
        assignmentEntriesBlock.append($(`<h2 class="display-6 my-4">`).text('Student assignments'));
        assignmentEntriesBlock.append($(`<hr class="my-4">`));

        const assignmentEntries = await $.ajax({
            url: '/api/Assignment/GetAssignmentEntriesForAssignmentTask',
            method: "GET",
            dataType: "json",
            data: { 
                id: assignment.id
            }
        });

        //!!!TODO

        assignmentBlock.append(assignmentEntriesBlock);
    } else {

    }

  } catch (e) {
    console.error("Error sending AJAX request:", error);
  }
});