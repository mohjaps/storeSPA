frmRegister.addEventListener('submit', function (e) {
    e.preventDefault();
    let inputs = document.querySelectorAll('.validate-input');
    let isValid = true;
    inputs.forEach((elements) => {
        if (elements.classList.contains('alert-validate')) {
            isValid = false;
        }
    });

    if (isValid) {
        let firstName = frmRegister.elements.fname.value
        let lastName = frmRegister.elements.lname.value
        let country = frmRegister.elements.country.value
        let email = frmRegister.elements.email.value
        let password = frmRegister.elements.pass.value
        let xhr = new XMLHttpRequest();
        xhr.open('POST', `${window.location.origin}/api/AuthUsers/register`, true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.send(JSON.stringify({ firstName, lastName, country, email, password }));
        xhr.onload = function () {
            if (xhr.status == 200) {
                let data = JSON.parse(xhr.responseText);
                if (data['result']) {
                    localStorage.setItem('token', data['token']);
                    localStorage.setItem('userName', data['name']);
                    window.location.href = '/Client/tabel.html';
                }
                else {
                    let errors = data['errors'];
                    swal({
                        title: "Error",
                        text: errors[0],
                        icon: "error",
                        button: "OK",
                    });
                }
            } else {
                swal({
                    title: "Error",
                    text: 'Login Failed',
                    icon: "error",
                    button: "OK",
                });
            }
        }
    }
});

