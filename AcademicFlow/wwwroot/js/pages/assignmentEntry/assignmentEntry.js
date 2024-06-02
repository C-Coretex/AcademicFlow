import { getCurrentUser, formatDate } from '../../components/utils';

$(document).ready(async function () {
  const assignmentEntryBlock = $('#assignment-entry-item');

  try {
    const userData = await getCurrentUser();
    if (!userData) {
        assignmentEntryBlock.append($(`<p>No user data provided</p>`));
        return;
    };

    const assignmentId = assignmentEntryBlock.attr('assignmentId');
    const id = assignmentEntryBlock.attr('assignmentEntryId');

    let assignmentEntityOutputModel;
    let assignmentEntry;
    let assignmentGrade;

    try {
        const myAssignmentEntry = await $.ajax({
            url: '/api/Assignment/GetMyAssignmentEntryForAssignmentTask',
            method: "GET",
            dataType: "json",
            data: { 
                id: assignmentId
            }
        });

        assignmentEntityOutputModel = myAssignmentEntry?.assignmentEntityOutputModels.find(a => a.assignmentEntryOutputModel.id == id);
        assignmentEntry = assignmentEntityOutputModel?.assignmentEntryOutputModel;
        assignmentGrade = assignmentEntityOutputModel?.assignmentGradeOutputModel;
    } catch (e) {
        const assignmentEntries = await $.ajax({
            url: '/api/Assignment/GetAssignmentEntriesForAssignmentTask',
            method: "GET",
            dataType: "json",
            data: { 
                id: assignmentId
            }
        });

        assignmentEntityOutputModel = assignmentEntries?.assignmentEntityOutputModels.find(a => a.assignmentEntryOutputModel.id == id);
        assignmentEntry = assignmentEntityOutputModel?.assignmentEntryOutputModel;
        assignmentGrade = assignmentEntityOutputModel?.assignmentGradeOutputModel;
    }

    if (!assignmentEntry) {
        assignmentEntryBlock.append($(`<p>No assignment data provided</p>`));
        return;
    };

    const assignmentName = (assignmentEntry.createdBy?.id == userData.id) ? 'Your Assignment' : `"${assignmentEntry.createdBy?.name} ${assignmentEntry.createdBy?.surname}'s" Assignment`;

    $('.breadcrumb-item.assignment-id').text(assignmentId);
    $('.breadcrumb-item.assignment-entry-id').text(id);

    assignmentEntryBlock.append($(`<h1 class="display-4 my-4">${assignmentName}</h1>`));
    assignmentEntryBlock.append($(`<hr class="my-4">`));
    assignmentEntryBlock.append($(`<p class="lead">Download assignment: <a class="text-success" href="/api/Assignment/DownloadAssignmentFile?id=${assignmentEntry.id}">${assignmentEntry.fileName}</a></p>`));
    assignmentEntryBlock.append($(`<p class="lead my-4">Created: ${formatDate(new Date(assignmentEntry.modified))}</p>`));

    assignmentEntryBlock.append($(`<h2 class="display-6 my-4">Grade</h2>`));
    if (assignmentGrade) {
        assignmentEntryBlock.append($(`<p class="lead">Grade: <strong>${assignmentGrade.grade}/10</strong></p>`));
        assignmentEntryBlock.append($(`<p class="lead">Comment: "${assignmentGrade.comment}"</p>`));
    } else {
        assignmentEntryBlock.append($(`<p class="lead">Not graded yet</p>`));
    }

    if (assignmentEntry.createdBy?.id == userData.id) {
        const deleteButton = $('<button>', {
            type: 'button',
            class: 'btn btn-danger mt-4',
            id: 'deleteAssignmentEntry',
            text: 'Delete assignment entry'
        });

        deleteButton.on('click', function() {
            if (confirm("Are you sure you want to proceed?") == true) {
                $.ajax({
                    url: `/api/Assignment/DeleteAssignmentEntry?id=${assignmentEntry.id}`,
                    method: "Delete",
                    dataType: "json"
                });

                window.location.href = '/Home/Assignments';
            }
        });

        assignmentEntryBlock.append(deleteButton);
    } else {
        if (assignmentGrade) {
            const deleteGradeButton = $('<button>', {
                type: 'button',
                class: 'btn btn-danger mt-4',
                id: 'deleteAssignmentEntry',
                text: `Delete your grade (${assignmentGrade.grade}/10)`
            });
    
            deleteGradeButton.on('click', function() {
                if (confirm("Are you sure you want to proceed?") == true) {
                    $.ajax({
                        url: `/api/Assignment/DeleteAssignmentGrade?id=${assignmentGrade.id}`,
                        method: "Delete",
                        dataType: "json"
                    });
    
                    window.location.reload();
                }
            });

            assignmentEntryBlock.append(deleteGradeButton);
        } else {
            const assignmentEntryGradeBlock = $('#assignment-entry-grade');
            assignmentEntryGradeBlock.removeClass('d-none');

            $('.js-add-assignment-entry-grade').on('click', function (ev) {
                ev.preventDefault();
                const $form = $('#assignmentEntryGradeForm');
                const logBlock = $('#form-logs');
                logBlock.empty();
    
                if ($form.serializeArray().some((field) => !field.value)) {
                    logBlock.append(`<div class="alert alert-danger mt-2" role="alert">Not all values are filled.</div>`);
                    return;
                }
    
                const formData = new FormData($form[0]);
                
                $.ajax({
                    type: 'PUT',
                    url: '/api/Assignment/AddAssignmentGrade',
                    processData: false, // Prevent jQuery from processing data (handled by FormData)
                    contentType: false, // Don't set content type header (FormData sets it)
                    data: formData
                })

                window.location.reload();
            });
        }
    }

  } catch (e) {
    console.error("Error sending AJAX request:", e);
    assignmentEntryBlock.append(`<div class="alert alert-danger mt-2" role="alert">${e?.responseText ?? 'Internal Error'}</div>`);
  }
});