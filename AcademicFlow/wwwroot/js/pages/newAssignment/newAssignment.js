$(document).ready(async function () {
    try {
        $('.js-add-assignment').on('click', function (ev) {
            ev.preventDefault();
            const $form = $('#assignmentForm');
            const logBlock = $('#form-logs');
            logBlock.empty();

            if ($form.serializeArray().some((field) => !field.value)) {
                logBlock.append(`<div class="alert alert-danger mt-2" role="alert">Not all values are filled.</div>`);
                return;
            }

            const formData = new FormData($form[0]);
            
            $.ajax({
                type: 'PUT',
                url: '/api/Assignment/AddAssignmentTask',
                processData: false, // Prevent jQuery from processing data (handled by FormData)
                contentType: false, // Don't set content type header (FormData sets it)
                data: formData,
                success: function () {
                    logBlock.append(`<div class="alert alert-success mt-2" role="alert">Assignment is added.</div>`);
                },
                error: function (xhr) {
                    const errorMessage = xhr.responseText;
                    console.error('Error adding user:', errorMessage);

                    logBlock.append(`<div class="alert alert-danger mt-2" role="alert">Assignment is not added.</div>`);
                }
            })
        });
    } catch (e) {
        console.error("Error sending AJAX request:", error);
    }
});