(() => {
    const url = 'http://localhost:5234/api/v1/admin';

    //#region POST
    let insertAdmin = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getButtonUpdate = document.querySelector('.update-admin');
        getButtonUpdate.style.display = 'none';

        let username = document.querySelector('#username').value;
        let password = "123";
        let jobTitle = document.querySelector('#jobTitle').value;

        let getClose = document.querySelector('.close-admin');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {username, password, jobTitle};
    }

    let sendAdmin = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertAdmin()));
        request.onload = () => {
            console.log(JSON.parse(request.response));
            var result = JSON.parse(request.response);
            if (request.status >= 200 && request.status <= 300) {
                alert(result.message);
                location.reload();
            } else if (request.status >= 400) {
                for (let index in result.errors) {
                    result.errors[index].forEach(error => {
                        alert(error)
                    });
                }
            }
        }
    }

    let submit = () => {
        let getSubmit = document.querySelector('.insert-admin');
        getSubmit.addEventListener('click', (event) => {
            event.preventDefault();
            sendAdmin();
        });
    }

    let getAddNewAdmin = () => {
        let button = document.querySelector('.add');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            insertAdmin();
        });
    }
    //#endregion

    //#region PUT
    let updateAdmin = () => {
        let username = document.querySelector('#username').value;
        let jobTitle = document.querySelector('#jobTitle').value;

        let modal = document.querySelector('.modal');
        let getClose = document.querySelector('.close-admin');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {username, jobTitle};
    }

    let sendAdminUpdate = () => {
        let request = new XMLHttpRequest();
        request.open('PUT', `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(updateAdmin()));
        request.onload = () => {
            console.log(JSON.parse(request.response));
            var result = JSON.parse(request.response);
            if (request.status >= 200 && request.status <= 300) {
                alert(result.message);
                location.reload();
            } else if (request.status >= 400) {
                for (let index in result.errors) {
                    result.errors[index].forEach(error => {
                        alert(error)
                    });
                }
            }
        }
    }

    let submitUpdate = () => {
        let button = document.querySelector('.update-admin');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            sendAdminUpdate();
        });
    }

    let bindingAdmin = (admin) => {
        console.log(admin);
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getSubmit = document.querySelector('.insert-admin');
        getSubmit.style.display = 'none';

        let username = document.querySelector('#username');
        let jobTitle = document.querySelector('#jobTitle');
        username.disabled = true;

        username.value = admin.data.username;
        jobTitle.value = admin.data.jobTitle;
    }

    let getAdmin = (username) => {
        let request = new XMLHttpRequest();
        request.open('GET', `${url}/${username}`);
        request.send();
        request.onload = () => {
            console.log(request.response);
            let admin = JSON.parse(request.response);

            bindingAdmin(admin);
        }
    }

    let getUpdateAdmin = () => {
        let getButtons = document.querySelectorAll('.admin-edit');
        getButtons.forEach(admin => {
            admin.addEventListener('click', (event) => {
                event.preventDefault();
                getAdmin(admin.getAttribute("value"));
            });
        });
    }
    //#endregion

    getUpdateAdmin();
    submitUpdate();

    getAddNewAdmin();
    submit();
})();