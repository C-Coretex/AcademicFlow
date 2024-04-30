$(document).ready(function () {
    $(".js-show-sign-up-form").click(function () {
        console.log('aaaa');
        toggleObjectVisibility($(".login-container"), false);
        toggleObjectVisibility($(".register-container"), true);
    });
    $(".js-show-log-in-form").click(function () {
        toggleObjectVisibility($(".login-container"), true);
        toggleObjectVisibility($(".register-container"), false);
    });

});

function toggleObjectVisibility($form, state) {//False -> hide container, True -> show container
    if (state) {
        $form.removeClass('d-none');
    } else {
        $form.addClass('d-none');
    }
}
