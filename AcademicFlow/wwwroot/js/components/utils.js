//This file is used to store commonly used functions to stay on DRY method

export function toggleObjectVisibility($form, state) {//State is false -> hide container, State is true -> show container
    console.log($form,state);
    if (state) {
        $form.removeClass('d-none');
    } else {
        $form.addClass('d-none');
    }
}