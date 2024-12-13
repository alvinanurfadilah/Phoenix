(() => {
    //#region Get Total Cost
    let getTotalCost = () => {

        let getButton = document.querySelector('#check-out');
        getButton.addEventListener('change', (event) => {
            event.preventDefault();

            let cost = document.querySelector('#cost').textContent;
            let checkIn = document.querySelector('#check-in').value;
            let checkOut = document.querySelector('#check-out').value;
            let totalCost = document.querySelector('#total-cost');
            
            let date1 = new Date(checkIn);
            let date2 = new Date(checkOut);

            let getDays = date2.getTime() - date1.getTime();
            let getDiffferenceDay = Math.round(getDays / (1000 * 3600 * 24));


            let getFinalCost = cost * getDiffferenceDay;

            totalCost.value = getFinalCost;
        });

    }
    //#endregion
    getTotalCost();
})();