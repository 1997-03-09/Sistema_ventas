
var getParameterByName = (name) => {
    //El método replace () busca una cadena para un valor específico, o una expresión regular , y devuelve una nueva cadena donde se reemplazan los valores especificados.
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
   // La función decodeURIComponent() decodifica un componente URI.
    return results === null ? null : decodeURIComponent(results[1].replace(/\+/g, " "));
}
var printThisDiv = (id) => {
    var printContents = document.getElementById(id).innerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}
var filterFloat = (evt, input) =>{
     // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
var filter = (__val__) => {
    var preg = /^([0-9]+\.?[0-9]{0,2})$/;
    return preg.test(__val__);
}
var numberDecimales = (number) => {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}