frmLogin.addEventListener('submit', function (e) {
    e.preventDefault();
    let inputs = document.querySelectorAll('.validate-input');
    let isValid = true;
    inputs.forEach((elements) => {
        if (elements.classList.contains('alert-validate')) {
            isValid = false;
        }
    });

    if (isValid) {
        let email = frmLogin.elements.email.value
        let password = frmLogin.elements.password.value
        let xhr = new XMLHttpRequest();
        xhr.open('POST', 'https://localhost:7026/api/AuthUsers/login', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.send(JSON.stringify({ email, password }));
        xhr.onload = function () {
            if (xhr.status == 200) {
                let data = JSON.parse(xhr.responseText);
                if (data['result']) {
                    console.log('Token', data['token']);
                    window.location.href = '/tabel.html';
                }
                else {
                    let errors = data['errors'];
                    errors.forEach((error) => {
                        alert(error)
                    });
                }
            } else {
                alert('Login failed')
            }
        }
    }
});



