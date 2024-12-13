(() => {
    const url = "http://localhost:5041/api/v1/reservation";

    let getDropdown = () => {
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.onload = () => {
            let data = JSON.parse(request.response);
            let months = data.getMonth;
            let years = data.getYear;
            console.log(data);

            let selectMonth = document.querySelector('#month-dropdown');
            let selectYear = document.querySelector('#year-dropdown ');

            months.forEach((month) => {
                let option = new Option(month.text, month.value);
                selectMonth.options.add(option);
            });

            years.forEach((year) => {
                let option = new Option(year.text, year.value);
                selectYear.options.add(option);
            })
        }
        request.send();
    }

    let totalIncome = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';

        let getTotal = document.querySelector('#check');
        getTotal.addEventListener('click', (event) => {
            event.preventDefault();
            let month = document.querySelector('#month-dropdown').value;
            let year = document.querySelector('#year-dropdown').value;
            sendTotalIncome(month, year);
        });

        let getClose = document.querySelector('.close-reservation');
        let getIncome = document.querySelector('#total-income');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            getIncome.textContent = "";
            modal.style.display = 'none';
        });
    }

    let sendTotalIncome = (month, year) => {
        let request = new XMLHttpRequest();
        request.open('GET', `${url}/totalincome/${month}/${year}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send();
        request.onload = () => {
            var result = JSON.parse(request.response);
            console.log(result);

            resultTotalIncome(result.totalIncome);
        }
    }

    let resultTotalIncome = (totalIncome) =>
    {
        let getIncome = document.querySelector('#total-income');
        getIncome.textContent = totalIncome;
    }

    let getTotalIncome = () => {
        let button = document.querySelector('.add');
        button.addEventListener('click', () => {
            totalIncome();
        });
    }

    getDropdown();
    getTotalIncome();
})();