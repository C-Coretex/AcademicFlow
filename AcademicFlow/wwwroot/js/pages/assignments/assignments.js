import { getCurrentUser, addAssignmentTable } from '../../components/utils';

$(document).ready(async function () {
  try {
      const userData = await getCurrentUser();
      if (!userData) return;

      const courses = await $.ajax({
          url: '/api/Course/GetCourseTable',
          method: "GET",
          dataType: "json",
          data: { 
              assignedUserId: userData.id 
          }
      });

      const assignmentBlock = $('#assignment-list')

      if (courses?.length) {
        $.each(courses, function(_, course) {
          const div = $('<div>');
          div.append($(`<p class="display-6">${course.name}</p>`));

          addAssignmentTable(div, course.id);

          assignmentBlock.append(div)
      });
      } else {
            studentBlock.append($('<p>No assignments available.</p>'))
      }

  } catch (e) {
      console.error("Error sending AJAX request:", error);
  }
});