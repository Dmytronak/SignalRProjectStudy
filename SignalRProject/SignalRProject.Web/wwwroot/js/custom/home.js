$("#menu-toggle").click(function (e) {
        e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

function loadFile(event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
}
function getRoomById(id) {
    const href = `/chat/room?roomId=${id}`;
    window.location = href;
}