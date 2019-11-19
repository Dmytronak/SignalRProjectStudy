$('#submitLogin').click(function (e) {
    e.preventDefault();
    let loginData = $('#LoginForm').serialize();
    $.ajax({
        type: 'POST',
        url: '/auth/login',
        data: loginData,
        success: (function (response) {
            localStorage.setItem('access_token', response.token);
            console.log(response.token);
        })
    });
});