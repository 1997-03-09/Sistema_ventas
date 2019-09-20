
class Principal {
    constructor() {

    }
    userLink(URLactual) {
        let url = "";
        let cadena = URLactual.split("/");
        for (var i = 0; i < cadena.length; i++) {
            if (cadena[i] != "Index") {
                url += cadena[i];
            }
        }
        switch (url) {
            case "Principal":
                document.getElementById("enlace1").classList.add('active');
                break;
            case "Usuarios":
                document.getElementById("enlace2").classList.add('active');
                break;
            case "Cajas":
                document.getElementById("enlace3").classList.add('active');
                break;
            case "UsuariosEliminarEliminar":
                document.getElementById("enlace2").classList.add('active');
                break;
            case "UsuariosRegistrarRegistrar":
                document.getElementById("enlace2").classList.add('active');
                document.getElementById('files').addEventListener('change', imageUser, false);
                var id = getParameterByName('id');
                if (id != null) {
                    document.getElementById("Input_Role").selectedIndex = 1;
                    document.getElementById("imageRgistrar").innerHTML = ['<img class="responsive-img " src="', URL + "images/fotos/Usuarios/" + id + ".png", '" title="', escape(id), '"/>'].join('');
                    $("#divPassword").hide();
                }
                break;
            case "ClientesAccountRegistrar":
                document.getElementById('files').addEventListener('change', imageCliente, false);
                var id = getParameterByName('id');
                if (id == null) {
                    document.getElementById("clienteRgistrar").innerHTML = ['<img class="responsive-img " src="', URL + "images/fotos/Clientes/default.png", '" title="', escape(id), '"/>'].join('');
                }
                
                break;
            case "ProveedoresAccountRegistrar":
                document.getElementById('files').addEventListener('change', imageProveedor, false);
                var id = getParameterByName('id');
                if (id == null) {
                    document.getElementById('email').value = "";
                }
                break
            case "ComprasComprasCompras":
                document.getElementById('files').addEventListener('change', productoCompra, false);
                //var id = getParameterByName('id');
                //if (id == null) {
                //    document.getElementById('email').value = "";
                //}
                break
            case "VentasVentas":
                new Ventas().pagos(null, null);
                break
        }
       
    }
}
