//This file is used to store commonly used functions to stay on DRY method

export function toggleObjectVisibility($object, state) {//State is false -> hide container, State is true -> show container
    if (state) {
        $object.removeClass('d-none');
    } else {
        $object.addClass('d-none');
    }
}