
$(document).ready(function () {

    $(window).resize(function () {
        var windowWidth = $('body').innerWidth();
        if (windowWidth < 765)
        { $("#flex-gallery").removeClass("gallery__flex_column"); }

        else
        { $("#flex-gallery").addClass("gallery__flex_column"); }
    });
});