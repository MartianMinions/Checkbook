function changeTotalColor() {
    var amount = ViewBag.TotalAmount;
    var documentDisplayedAmount = document.getElementById("headerTotal");
    documentDisplayedAmount.innerHTML = "$" + ViewBag.TotalAmount;

    if (amount > 0) {
        documentDisplayedAmount.style.color = "#00BFFF";
    } else {
        documentDisplayedAmount.style.color = "#B22222";
    }
};
changeTotalColor();

window.onload = function () {
    changeTotalColor();
    alert("This at least worked");
};