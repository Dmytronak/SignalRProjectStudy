function login(event) {
    event.preventDefault();
    let loginData = $('#loginForm').serialize();
    $.ajax({
        type: 'POST',
        url: '/auth/login',
        data: loginData,
        success: (function (response) {
            localStorage.setItem('access_token', response.token);
            console.log(response.token);
            window.location = '/chat/index';
        }),
        error: (function (error) {
            $('#loginErrorMessage').text('Login or password is not valid.')
            $('#loginErrorMessage').removeClass('d-none');
            console.log(error.statusText);
        })
    });
}

function register(event) {
    event.preventDefault();
    $("#registerForm")[0].submit();
}

function logOut() {
    localStorage.removeItem('access_token');
    window.location = '/auth/logOut';
}