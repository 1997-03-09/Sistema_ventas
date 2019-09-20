class Ventas {

    pagos(evt, input) {
        if (evt != null) {
            if (filterFloat(evt, input)) {
                var importe = parseFloat($("#vtImporte").val());
                var key = window.Event ? evt.which : evt.keyCode;
                var chark = String.fromCharCode(key);
                var tempValue = input.value + chark;
                var pago = parseFloat(tempValue);
                var deuda = importe - pago;
               $("#vtDeuda").text("$" + numberDecimales(deuda));
            }
        } else {
            $("#vtDeuda").text("$0.0000000");
        }
    }
}