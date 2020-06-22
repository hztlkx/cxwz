$(function () {
    if ($("table").hasClass("table")) {
        $(".table").addClass("table-bordered table-hover table-striped");
    }

    var scrollTop = window.parent.document.scrollingElement;
    $(scrollTop).animate({
        scrollTop: '0px'
    }, 400);

})