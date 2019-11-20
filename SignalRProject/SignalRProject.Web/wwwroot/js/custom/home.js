$("#menu-toggle").click(function (e) {
        e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

/* Auto scroll */
$(".chat-container").stop().animate({
    scrollTop: $('.chat-container')[0].scrollHeight
}, 1000);
/* Auto scroll */