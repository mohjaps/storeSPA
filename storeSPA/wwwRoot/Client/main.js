let all = [];
window.onload = function () {
    setAuth();
    let xhr = new XMLHttpRequest();
    let url = `${window.location.origin}/api/Data/GetAll`;
    xhr.open("GET", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send();
    xhr.onload = function () {
        if (xhr.status == 200) {
            let data = JSON.parse(xhr.responseText);
            if (data["result"]) {
                let code = "";
                all = [];
                data["data"].forEach(function (item) {
                    var dta = {
                        id: item["id"],
                        name: item["name"],
                        description: item["description"],
                        image: item["image"],
                        quantity: item["quantity"],
                        price: item["price"],
                    }
                    all.push(dta);
                    let obj = `
                    <div class="col-12 col-md-6 col-lg-3">
                        <div class="card">
                            <div class="card-thumb">
                            <a href="#"><img src="${window.location.origin}/${item["image"]}" alt="" /></a>
                            <span class="card-category">${item['price']}$</span>
                            </div>
                            <div class="card-body">
                            <h2 class="card-title">${item["name"]}</h2>
                            <a style="cursor:pointer" onclick="details('${item['id']}')"><p class="fs-6">MORE</p></a>
                            <p class="card-description">
                                ${item["description"].substring(0, Math.min(90, item["description"].length))}
                            </p>
                            </div>
                        </div>
                    </div>`;
                    code += obj;
                });
                galary.innerHTML = code;
                setCarouselItems();
                console.log(all)
            }
        }
    };
};

function details(id) {
    let item;
    let editedItem = all.filter((element) => {
        return element["id"] == id;
    });
    item = editedItem[0];
    modalName.innerText = item.name;
    modalDesc.innerText = item.description;
    modalPrice.innerText = item.price + '$';
    modalImage.src = window.location.origin + '/' + item.image;
    var modal = new bootstrap.Modal(document.querySelector("#exampleModal"), {
        keyboard: false,
    });
    modal.show();
}

function setCarouselItems() {
    let carousel = '';
    let carouselBtns = '';
    for (let i = 0; i < all.length && i < 3; i++) {
        carousel += `
        <div class="carousel-item ${i == 0 ? "active" : ""}">
        <div class="beforeimg">
          <img
            src="${window.location.origin}/${all[i].image}"
            class="img-fluid w-100">
        </div>
        <div class="carousel-caption">
          <h3>${all[i].name}</h3>
        </div>
      </div>
        `;
        carouselBtns += `
        <button type="button" data-bs-target="#demo" data-bs-slide-to="${i}" class="${i == 0 ? "active" : ""}"></button>
        `;
    }
    carouselBtnsBox.innerHTML = carouselBtns;
    carouselBox.innerHTML = carousel;
}


function setAuth() {
    if (localStorage.getItem("token") != null) {
        auths.innerHTML = `
        <a class="nav-link fs-4" href="/Client/tabel.html">
                    Hello <span id="userName"></span>!
                </a>
                <a class="nav-link fs-4" onclick="Logout()">
                    <i class="fa-light fa-circle-user px-1"></i> Logout
                </a>
        `;
        userName.innerText = localStorage.getItem('userName');
    }
    else {
        auths.innerHTML = `<a class="nav-link fs-4" href="/Client/login.html">
        <i class="fa-light fa-circle-user px-1"></i> login
      </a>
      <a class="nav-link fs-4" href="/Client/regester.html">
        <i class="fa-light fa-id-card px-1"></i> Register
      </a>`
    }
}
function Logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    window.location.href = '/Client/index.html';
}