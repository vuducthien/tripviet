; (function () {

$(document).ready(function () {
    $("#search-location").click(function () {
        //searchPlaces();
    });

});

//function searchPlaces() {
//    $('#colorlib-hotel').css('display', 'block');
//}


}());

//var uniquePlace = {};
function initAutocomplete() {
    // Create the autocomplete object, restricting the search to geographical
    // location types.
    autocomplete = new google.maps.places.Autocomplete(
        // type {!HTMLInputElement} * /
        (document.getElementById('search_input')),
        { types: ['geocode'] });

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete.addListener('place_changed', function () {
        var place = autocomplete.getPlace();
        var searchText = '';
        for (var i = 0; i < place.address_components.length; i++) {
            if (place.address_components[i].types[0] == 'administrative_area_level_2') {
                searchText = place.address_components[i].long_name;
                break;
            } if (place.address_components[i].types[0] == 'administrative_area_level_1') {
                searchText = place.address_components[i].long_name;
                break;
            }
            if (place.address_components[i].types[0] == 'country') {
                searchText = place.address_components[i].long_name;
                break;
            }
        }

        var carousel = $('#colorlib-hotel .owl-carousel');
        //send ajax
        $.ajax({
            type: "POST",
            url: "Home/SearchPlace",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ text: searchText}),
            error: function (xhr) {},
            success: function (data) {
                if (data.length > 0) {
                    $('.search-no-found').css('display', 'none');
                    $('#colorlib-hotel').css('display', 'block');
                    //these 3 lines kill the owl, and returns the markup to the initial state
                    carousel.trigger('destroy.owl.carousel');
                    carousel.find('.owl-stage-outer').children().unwrap();
                    carousel.removeClass("owl-center owl-hidden owl-loaded owl-drag owl-text-select-on");
                    carousel.html('');
                    //carousel.html('');
                    for (var i = 0; i < data.length; i++) {
                        var placeName = '';
                        for (var j = 0; j < data[i].places.length; j++) {
                            placeName += data[i].places[j].nonHtmlAddress + '<br/>'
                        }
                        var str = '<div class="item">'
                            + '<div class="hotel-entry">'
                            + '<a href="/Blog/Detail/' + data[i].id + '" class="hotel-img" style="background-image: url(assets/images/custom/hotel-1.jpg);">'
                            //+ '<p class="price"><span>$120</span><small> /night</small></p>'
                            + '<p class="price">' + data[i].author + '</p>'
                            + '</a>'
                            + '<div class="desc">'
                            + '<p class="star"><span><i class="icon-star-full"></i><i class="icon-star-full"></i><i class="icon-star-full"></i><i class="icon-star-full"></i><i class="icon-star-full"></i></span> 545 Reviews</p>'
                            + '<h3><a href="/Blog/Detail/' + data[i].id + '">' + data[i].title + '</a></h3>'
                            + '<span class="place">' + placeName
                            + '</span >'
                            //+ '<p>' + data[i].content + '</p>'
                            + '</div>'
                            + '</div>'
                            + ' </div>';
                        carousel.append(str);
                    }

                    carousel.owlCarousel({
                        loop: true,
                        margin: 30,
                        nav: true,
                        dots: false,
                        autoplay: true,
                        autoplayHoverPause: true,
                        smartSpeed: 500,
                        responsive: {
                            0: {
                                items: 1
                            },
                            600: {
                                items: 2
                            },
                            1000: {
                                items: 3
                            }
                        },
                        navText: [
                            "<i class='icon-chevron-left owl-direction'></i>",
                            "<i class='icon-chevron-right owl-direction'></i>"
                        ]
                    });
                }
                else {
                    $('.search-no-found').css('display', 'block');
                    $('#colorlib-hotel').css('display', 'none');
                    carousel.addClass('owl-hidden');
                }                

            }
        }).done(function () {
        });
    });
}
