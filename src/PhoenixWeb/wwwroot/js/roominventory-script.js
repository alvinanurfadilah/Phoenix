(() => {
    const url = "http://localhost:5041/api/v1/roominventory";

    let insertRoomInventory = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';

        let roomNumber = document.querySelector('#roomNumber').textContent;
        let inventoryName = document.querySelector('#inventory-dropdown').value;
        let quantity = document.querySelector('#quantity').value;

        let getClose = document.querySelector('.close-roominventory');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return { roomNumber, inventoryName, quantity}
    }

    let sendRoomInventory = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(insertRoomInventory()));
        request.onload = () => {
            console.log(request.response);
            location.reload();
        }
    }

    let getDropdown = () => {
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.onload = () => {
            let data = JSON.parse(request.response);
            let inventories = data.inventories;
            
            let selectInventory = document.querySelector('#inventory-dropdown');

            inventories.forEach((inventory) => {
                let option = new Option(inventory.text, inventory.value);
                selectInventory.options.add(option);
            });
        }
        request.send();
    }

    let submit = () => {
        let getSubmit = document.querySelector('.insert-roominventory');
        getSubmit.addEventListener('click', (event) => {
            event.preventDefault();
            sendRoomInventory();
        });
    }

    let getAddNewItem = () => {
        let button = document.querySelector('.add');
        button.addEventListener('click', () => {
            insertRoomInventory();
        });
    }

    getAddNewItem();
    submit();
    getDropdown();
})();