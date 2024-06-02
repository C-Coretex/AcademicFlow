import { getCurrentUser, formatDate, renderAssignmentEntryTable } from '../../components/utils';

$(document).ready(async function () {
  const assignmentBlock = $('#assignment-item');

  try {
    const userData = await getCurrentUser();
    if (!userData) {
      assignmentBlock.append($(`<p>No user data provided</p>`));
      return;
    };

    const id = assignmentBlock.attr('assignmentId');

    const assignment = await $.ajax({
      url: '/api/Assignment/GetAssignmentTask',
      method: "GET",
      dataType: "json",
      data: { 
          id: id
      }
    });

    $('.breadcrumb-item.active').text(assignment.id);

    assignmentBlock.append($(`<h1 class="display-4 my-4">${assignment.assignmentName}</h1>`));
    assignmentBlock.append($(`<hr class="my-4">`));
    assignmentBlock.append($(`<p class="lead mb-4">Task: "${assignment.assignmentDescription}"</p>`));
    assignmentBlock.append($(`<p><strong>Author:</strong> ${assignment.createdBy?.name} ${assignment.createdBy?.surname}</p>`));
    assignmentBlock.append($(`<p><strong>Assignment Weight:</strong> ${assignment.assignmentWeight}</p>`));
    assignmentBlock.append($(`<p><strong>Last modified:</strong> ${formatDate(new Date(assignment.modified))}</p>`));
    assignmentBlock.append($(`<p><strong>Deadline:</strong> ${formatDate(new Date(assignment.deadline))}</p>`));

    if (assignment.createdBy?.id == userData.id) {
      const assignmentEntries = await $.ajax({
        url: '/api/Assignment/GetAssignmentEntriesForAssignmentTask',
        method: "GET",
        dataType: "json",
        data: { 
            id: assignment.id
        }
      });

      const assignmentEntriesBlock = $('<div class="my-6">');
      assignmentEntriesBlock.append($(`<h2 class="display-6 my-4">`).text('Student assignments'));
      assignmentEntriesBlock.append($(`<hr class="my-4">`));

      if (assignmentEntries?.assignmentEntityOutputModels?.length) {
        renderAssignmentEntryTable(assignmentEntriesBlock, assignmentEntries);
      } else {
        assignmentEntriesBlock.append($('<p>This assignment has no entries yet.</p>'))
      }

      assignmentBlock.append(assignmentEntriesBlock);

      const deleteButton = $('<button>', {
        type: 'button',
        class: 'btn btn-danger mt-4',
        id: 'deleteAssignment',
        text: 'Delete assignment'
      });

      deleteButton.on('click', function() {
        if (confirm("Are you sure you want to proceed?") == true) {
          $.ajax({
            url: `/api/Assignment/DeleteAssignmentTask?id=${assignment.id}`,
            method: "Delete",
            dataType: "json"
          });

          window.location.href = '/Home/Assignments';
        }
      });

      assignmentBlock.append(deleteButton);
    } else {
      const myAssignmentEntry = await $.ajax({
        url: '/api/Assignment/GetMyAssignmentEntryForAssignmentTask',
        method: "GET",
        dataType: "json",
        data: { 
            id: assignment.id
        }
      });

      const assignmentEntryBlock = $('<div class="my-6">');
      assignmentEntryBlock.append($(`<h2 class="display-6 my-4">`).text('Your submitted assignment'));
      assignmentEntryBlock.append($(`<hr class="my-4">`));

      if (myAssignmentEntry?.assignmentEntityOutputModels?.length) {
        renderAssignmentEntryTable(assignmentEntryBlock, myAssignmentEntry);
      } else {
        assignmentEntryBlock.append($('<p>This assignment has no entries yet.</p>'))
        assignmentEntryBlock.append($('<a class="btn btn-success mt-4" href="/Home/AssignmentEntry/New">Add new assignment entry</a>'))
      }

      assignmentBlock.append(assignmentEntryBlock);
    }

  } catch (e) {
    console.error("Error sending AJAX request:", e);
    assignmentBlock.append(`<div class="alert alert-danger mt-2" role="alert">${e?.responseText ?? 'Internal Error'}</div>`);
  }
});