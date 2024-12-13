(() => {
    const url = 'http://localhost:5234/api/v1/account';
    //#region ChangePassword
    let changePassword = () => {
        let modal = document.querySelector('.modal-change-password');
        modal.style.display = 'flex';

        let oldPassword = document.querySelector('#old-password').value;
        let newPassword = document.querySelector('#new-password').value;
        let confirmPassword = document.querySelector('#new-password').value;

        let getClose = document.querySelector('.close-change-password');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {oldPassword, newPassword, confirmPassword};
    }

    let sendChangePassword = () => {
        let request = new XMLHttpRequest();
        request.open('PUT', `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(changePassword()));
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

    let submitChangePassword = () => {
        let button = document.querySelector('.update-change-password');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            sendChangePassword();
        });
    }

    let getChangePassword = () => {
        let button = document.querySelector('.changePassword');
        button.addEventListener('click', (event) => {
            console.log("change password");
            event.preventDefault();
            changePassword();
        });
    }
    //#endregion

    getChangePassword();
    submitChangePassword();
})();