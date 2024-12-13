(() => {
    const url = 'http://localhost:5234/api/v1/roomservice';

    //#region POST
    let insertRoomService = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getButtonUpdate = document.querySelector('.update-roomservice');
        getButtonUpdate.style.display = 'none';

        let employeeNumber = document.querySelector('#employeeNumber').value;
        let firstName = document.querySelector('#firstName').value;
        let middleName = document.querySelector('#middleName').value;
        let lastName = document.querySelector('#lastName').value;
        let outsourcingCompany = document.querySelector('#outsourcingCompany').value;

        let getClose = document.querySelector('.close-roomservice');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {employeeNumber, firstName, middleName, lastName, outsourcingCompany};
    }

    let sendRoomService = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertRoomService()));
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
        let getSubmit = document.querySelector('.insert-roomservice');
        getSubmit.addEventListener('click', (event) => {
            event.preventDefault();
            sendRoomService();
        });
    }

    let getAddNewRoomService = () => {
        let button = document.querySelector('.add');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            insertRoomService();
        });
    }
    //#endregion


    //#region PUT
    let updateRoomService = () => {
        let employeeNumber = document.querySelector('#employeeNumber').value;
        let firstName = document.querySelector('#firstName').value;
        let middleName = document.querySelector('#middleName').value;
        let lastName = document.querySelector('#lastName').value;
        let outsourcingCompany = document.querySelector('#outsourcingCompany').value;

        let modal = document.querySelector('.modal');
        let getClose = document.querySelector('.close-roomservice');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {employeeNumber, firstName, middleName, lastName, outsourcingCompany};
    }

    let sendRoomServiceUpdate = () => {
        let request = new XMLHttpRequest();
        request.open('PUT', `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(updateRoomService()));
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
        let button = document.querySelector('.update-roomservice');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            sendRoomServiceUpdate();
        });
    }

    let bindingRoomService = (service) => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getSubmit = document.querySelector('.insert-roomservice');
        getSubmit.style.display = 'none';

        let employeeNumber = document.querySelector('#employeeNumber');
        let firstName = document.querySelector('#firstName');
        let middleName = document.querySelector('#middleName');
        let lastName = document.querySelector('#lastName');
        let outsourcingCompany = document.querySelector('#outsourcingCompany');
        employeeNumber.disabled = true;

        employeeNumber.value = service.data.employeeNumber;
        firstName.value = service.data.firstName;
        middleName.value = service.data.middleName;
        lastName.value = service.data.lastName;
        outsourcingCompany.value = service.data.outsourcingCompany;
    }

    let getRoomService = (employeeNumber) => {
        let request = new XMLHttpRequest();
        request.open('GET', `${url}/${employeeNumber}`);
        request.send();
        request.onload = () => {
            let service = JSON.parse(request.response);

            bindingRoomService(service);
        }
    }

    let getUpdateRoomService = () => {
        let getButtons = document.querySelectorAll('.roomservice-edit');
        getButtons.forEach(service => {
            service.addEventListener('click', (event) => {
                event.preventDefault();
                getRoomService(service.getAttribute("value"));
            });
        });
    }
    //#endregion

    submitUpdate();
    getUpdateRoomService();

    submit();
    getAddNewRoomService();
})();