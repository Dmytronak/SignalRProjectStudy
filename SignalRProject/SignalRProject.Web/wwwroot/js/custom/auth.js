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
    let loginData = $('#registerForm').serialize();
    $.ajax({
        type: 'POST',
        url: '/auth/register',
        data: loginData,
        success: (function (response) {
            if (response.token) {
                window.location = '/auth/login';
                console.log(response.token);
            }
            console.log(response.token);
        }),
        error: (function (error) {
            $('#registerErrorMessage').text(`${error.statusText} try again`)
            $('#registerErrorMessage').removeClass("d-none");
            
        })
    });
}

function logOut() {
    localStorage.removeItem('access_token');
    window.location = '/auth/logOut';
}