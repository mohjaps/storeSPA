const LOCALLINK = window.location.origin;
const GETFORUSER = LOCALLINK + '/api/Data/GetByUserPerfume';
const INSERTPERFUME = LOCALLINK + '/api/Data/NewPerfume';
const UPDATEPERFUME = LOCALLINK + '/api/Data/UpdatePerfume';
const DELETEPERFUME = LOCALLINK + '/api/Data/DeletePerfume';



window.addEventListener('load', function () {
    if (localStorage.getItem('token') == null) {
        window.location.href = '/Client/login.html';
    }
    userName.innerText = localStorage.getItem('userName');
    getAllData();
});

function getAllData() {
    let xhr = new XMLHttpRequest();
    xhr.open('GET', GETFORUSER, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.setRequestHeader('Authorization', `Bearer ${localStorage.getItem('token').toString()}`);
    xhr.send();
    xhr.onload = function () {
        if (xhr.status == 200) {
            let data = JSON.parse(xhr.responseText);
            if (data['result']) {
                let db = [];
                data['data'].forEach(function (item) {
                    db.push({
                        id: item['id'],
                        name: item['name'],
                        description: item['description'],
                        image: item['image'],
                        quantity: item['quantity'],
                        price: item['price'],
                    });
                })
                dataSet = db;
                initialData();
            }
        }
        else if (xhr.status === 401) {
            window.location.href = '/Client/login.html';
        }
        else {
            swal({
                title: "Error",
                text: "Something went wrong",
                icon: "error",
                button: "OK",
            });
        }
    }
}

// function insertData(e) {
//     e.preventDefault();
//     let data = {
//         "Name": frm.name.value,
//         "description": frm.description.value,
//         "imge": frm.imge.value,
//         "quantity": frm.quantity.value,
//         "price": frm.price.value
//     };
//     let xhr = new XMLHttpRequest();
//     let url = 'https://localhost:7026/api/Data/newperfume';
//     xhr.open('POST', url, true);
//     xhr.setRequestHeader('Content-Type', 'multipart/form-data');
//     xhr.send(JSON.stringify(data));
//     xhr.onload = function () {
//         if (xhr.status == 200) {
//             let data = JSON.parse(xhr.responseText);
//             if (data['result']) {
//                 getAllData();
//             }
//         }
//     }
// }

let image;
frm.imge.addEventListener('change', function (e) {
    let file = e.target.files[0];
    image = file;
})

frm.addEventListener('submit', function (e) {
    e.preventDefault();
    if (document.querySelectorAll('.was-validated .form-control:invalid').length == 0) {
        if (action == 1) { InsertData(); }
        else if (action == 2) { UpdateData() }
    }
})

function InsertData() {
    let form_data = new FormData();
    form_data.append('Name', frm.elements.name.value);
    form_data.append('Description', frm.elements.description.value);
    form_data.append('img', image);
    form_data.append('Quantity', frm.elements.quantity.value);
    form_data.append('Price', frm.elements.price.value);
    let xhr = new XMLHttpRequest();
    xhr.withCredentials = false;
    xhr.open('POST', INSERTPERFUME, true);
    xhr.setRequestHeader('Authorization', `Bearer ${localStorage.getItem('token').toString()}`);
    xhr.send(form_data);
    xhr.onload = function () {
        if (xhr.status == 200) {
            let data = JSON.parse(xhr.responseText);
            if (data['result']) {
                frm.reset();
                frm.classList.remove('was-validated');
                btnClose.click();
                getAllData();
                swal({
                    title: "Success",
                    text: "Data inserted successfully",
                    icon: "success",
                    button: "OK",
                });
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
        }
        else if (xhr.status === 401) {
            window.location.href = '/Client/login.html';
        }
        else {
            swal({
                title: "Error",
                text: "Something went wrong",
                icon: "error",
                button: "OK",
            });
        }
    }
}

function UpdateData() {
    let form_data = new FormData();
    form_data.append('id', frm.elements.id.value);
    form_data.append('Name', frm.elements.name.value);
    form_data.append('Description', frm.elements.description.value);
    form_data.append('img', image);
    form_data.append('Quantity', frm.elements.quantity.value);
    form_data.append('Price', frm.elements.price.value);
    let xhr = new XMLHttpRequest();
    xhr.withCredentials = false;
    xhr.open('PUT', UPDATEPERFUME, true);
    xhr.setRequestHeader('Authorization', `Bearer ${localStorage.getItem('token').toString()}`);
    xhr.send(form_data);
    xhr.onload = function () {
        if (xhr.status == 200) {
            let data = JSON.parse(xhr.responseText);
            if (data['result']) {
                frm.reset();
                frm.classList.remove('was-validated');
                btnClose.click();
                getAllData();
                swal({
                    title: "Success",
                    text: "Data Updated successfully",
                    icon: "success",
                    button: "OK",
                });
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
        }
        else if (xhr.status === 401) {
            window.location.href = '/Client/login.html';
        }
        else {
            swal({
                title: "Error",
                text: "Something went wrong",
                icon: "error",
                button: "OK",
            });
        }
    }
}

let xhr = new XMLHttpRequest();
function DeletePerfume(id) {
    let url = `${DELETEPERFUME}?id=` + id;
    xhr.open('DELETE', url, true);
    xhr.setRequestHeader('Authorization', `Bearer ${localStorage.getItem('token').toString()}`);
    xhr.send();
    xhr.onload = function () {
        if (xhr.status == 200) {
            let data = JSON.parse(xhr.responseText);
            if (data['result']) {
                getAllData();
                swal({
                    title: "Success",
                    text: "Data Deleted successfully",
                    icon: "success",
                    button: "OK",
                });
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
        }
        else if (xhr.status === 401) {
            window.location.href = '/Client/login.html';
        }
        else if(xhr.status === 403){
            swal({
                title: "Error",
                text: 'You Are Not Authorize To Do This Action',
                icon: "error",
                button: "OK",
            });
        }
        else {
            swal({
                title: "Error",
                text: "Something went wrong",
                icon: "error",
                button: "OK",
            });
        }
    }
}

function Logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    window.location.href = '/Client/index.html';
}