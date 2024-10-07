document.addEventListener('DOMContentLoaded', function() {
    var loginButton = document.getElementById('loginButton');
    if (loginButton) {
        loginButton.addEventListener('click', function() {
            loginButton.disabled = true;
            loginButton.textContent = 'Logging in...';

            window.location.href = '/api/login';
        });
    }
});

document.addEventListener('DOMContentLoaded', function() {
    var logoutButton = document.getElementById('logoutButton');
    if (logoutButton) {
        logoutButton.addEventListener('click', function() {
            logoutButton.disabled = true;
            logoutButton.textContent = 'Logging out...';

            window.location.href = '/api/logout';
        });
    }
});
