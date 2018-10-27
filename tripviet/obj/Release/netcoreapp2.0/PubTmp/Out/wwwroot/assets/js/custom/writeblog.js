//ignore jQuery validation does not validate hidden fields
$.validator.setDefaults({ ignore: '' });
$(document).ready(function () {
    initAutocomplete();    

    $('.place_add').on('click', function () {
        if (isErrorInputtingPlace())
            return;
        var place = $('#autocomplete_placeid');
        place.removeAttr('id');
        $('.place_form').append(
            '<div class="place_container">'
            + '<div class= "place_name_container"> <img src="/assets/images/logo30.png" alt="Trip Viet logo"><span class="place_name"></span></div>'
            + '<input type="text" class="place_autocomplete" id="autocomplete_placeid" />'
            + '<button type="button" class="place_remove btn btn-secondary">'
            + '<i class="fa fa-trash"></i> Xóa'
            + '</button>'
            + '<input type="hidden" class="val_name" />'
            + '<input type="hidden" class="val_formatted_address" />'
            + '<input type="hidden" class="val_adr_address" />'
            + '<input type="hidden" class="val_reference" />'
            + '<input type="hidden" class="val_id" />'
            + '</div >');
        initAutocomplete();
    });
});

$(document).on('click', '.place_remove', function () {
    $(this).parent().remove();
    isErrorInputtingPlace();
});

function isErrorInputtingPlace() {
    var error = false;
    $('.place_name').each(function () {
        if ($(this).text() === '') {
            $('#place-error').css('display', 'block');
            error = true;
            return false;
        }
    });
    if (!error)
        $('#place-error').css('display', 'none');
    return error;
}

function initAutocomplete() {
    // Create the autocomplete object, restricting the search to geographical
    // location types.
    var autocomplete = new google.maps.places.Autocomplete(
        // type {!HTMLInputElement} * /
        (document.getElementById('autocomplete_placeid')),
        { types: ['geocode'] });
    var jqueryObject = $('#autocomplete_placeid');

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete.addListener('place_changed', function () {
        // Get the place details from the autocomplete object.
        var place = autocomplete.getPlace();
        var placeNameContainer = jqueryObject.siblings('.place_name_container');
        placeNameContainer.css('display', 'block');
        placeNameContainer.find('.place_name').text(place.formatted_address);
        //console.log(place);
        var placeContainer = jqueryObject.parent();
        placeContainer.find('.val_name').val(place.name);
        placeContainer.find('.val_formatted_address').val(place.formatted_address);
        placeContainer.find('.val_adr_address').val(place.adr_address);
        placeContainer.find('.val_reference').val(place.reference);
        placeContainer.find('.val_id').val(place.id);
        isErrorInputtingPlace();
    });
}
;
$('#blog_submit').click(function (evt) {
    evt.preventDefault();
    var $form = $('#my_form');
    if ($form.valid()) {
        //remove empty place
        var lastPlaceName = $('.place_name').last();
        if (lastPlaceName.text() === '') {
            lastPlaceName.parents('.place_container').remove();
        }
        //data
        var places = [];
        $('.place_container').each(function () {
            places.push({
                name: $(this).find('.val_name').val(),
                nonHtmlAddress: $(this).find('.val_formatted_address').val(),
                htmlAddress: $(this).find('.val_adr_address').val(),
                reference: $(this).find('.val_reference').val(),
                googleApiId: $(this).find('.val_id').val()
            });
        });
        var blog = {
            blogType: $('.blog_type').val(),
            title: $('.writeblog_title').val(),
            content: CKEDITOR.instances['editor'].getData(),
            places: places
        };

        //send ajax
        $.ajax({
            type: "POST",
            url: "../WriteBlog",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(blog),
            error: function (xhr) {
                //console.log('Error: ' + xhr.statusText);
            },
            success: function (id) {
                window.location.href = '../Detail/' + id;
            }
        }).done(function () {
        });
    } else {
        // cancel the submission of the form
        return false;
    }
});

CKEDITOR.replace('editor', {
    height: '500px'
});
CKEDITOR.instances['editor'].on('change', function (e) {
    //to validate textrea#editor
    var data = CKEDITOR.instances['editor'].getData();
    $("#editor").val(data);
});