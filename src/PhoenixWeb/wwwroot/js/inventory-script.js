(() => {
    const url = 'http://localhost:5234/api/v1/inventory';

    //#region POST
    let insertInventory = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getButtonUpdate = document.querySelector('.update-inventory');
        getButtonUpdate.style.display = 'none';

        let name = document.querySelector('#name').value;
        let stock = document.querySelector('#stock').value;
        let description = document.querySelector('#description').value;

        let getClose = document.querySelector('.close-inventory');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            name = '';
            stock = '';
            description = '';
            modal.style.display = 'none';
        });

        return {name, stock, description};
    }

    let sendInventory = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertInventory()));
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
        let getSubmit = document.querySelector('.insert-inventory');
        getSubmit.addEventListener('click', (event) => {
            event.preventDefault();
            sendInventory();
        });
    }

    let getAddNewInventory = () => {
        let button = document.querySelector('.add');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            insertInventory();
        });
    }
    //#endregion

    //#region PUT
    let updateInventory = () => {
        let name = document.querySelector('#name').value;
        let stock = document.querySelector('#stock').value;
        let description = document.querySelector('#description').value;

        let modal = document.querySelector('.modal');
        let getClose = document.querySelector('.close-inventory');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            name = '';
            stock = '';
            description = '';
            modal.style.display = 'none';
        });

        return {name, stock, description};
    }

    let sendInventoryUpdate = () => {
        let request = new XMLHttpRequest();
        request.open('PUT', `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(updateInventory()));
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
        let button = document.querySelector('.update-inventory');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            sendInventoryUpdate();
        });
    }

    let bindingInventory = (inventory) => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getSubmit = document.querySelector('.insert-inventory');
        getSubmit.style.display = 'none';

        let name = document.querySelector('#name');
        let stock = document.querySelector('#stock');
        let description = document.querySelector('#description');
        name.disabled = true;

        name.value = inventory.data.name;
        stock.value = inventory.data.stock;
        description.value = inventory.data.description;
    }

    let getInventory = (name) => {
        let request = new XMLHttpRequest();
        request.open('GET', `${url}/${name}`);
        request.send();
        request.onload = () => {
            let inventory = JSON.parse(request.response);
            console.log(inventory);

            bindingInventory(inventory);
        }
    }

    let getUpdateInventory = () => {
        let getButtons = document.querySelectorAll('.inventory-edit');
        getButtons.forEach(inventory => {
            inventory.addEventListener('click', (event) => {
                event.preventDefault();
                getInventory(inventory.getAttribute("value"));
            });
        });
    }
    //#endregion

    getUpdateInventory();
    submitUpdate();

    getAddNewInventory();
    submit();
})();