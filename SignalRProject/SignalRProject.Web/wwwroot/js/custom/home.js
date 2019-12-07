$("#menu-toggle").click(function (e) {
        e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

function loadFile(event) {
    var output = document.getElementById('output');
    output.src = URL.createObjectURL(event.target.files[0]);
}

function scrolToBottom() {
    var objDiv = document.getElementById("messagesList");
    objDiv.scrollTop = objDiv.scrollHeight;
}