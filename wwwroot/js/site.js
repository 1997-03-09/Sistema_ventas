/*CODIGO PRINCIPAL*/
var principal = new Principal();


/*CODIGO DE USUARIOS*/
var usuarios = new Usuarios();
var imageUser = (evt) => {
    usuarios.archivo(evt, "imageRgistrar");
}

/*CODIGO DE CLIENTES*/
var clientes = new Clientes();
var imageCliente = (evt) => {
    clientes.archivo(evt, "clienteRgistrar");
}

/*CODIGO DE PROVEEDORES*/
var proveedores = new Proveedores();
var imageProveedor = (evt) => {
    proveedores.archivo(evt, "proveedorRegistrar");
}
/*CODIGO DE COMPRAS*/
var compras = new Compras();
var productoCompra = (evt) => {
    compras.archivo(evt, "productoCompra");
}

$().ready(() => {
    let URLactual = window.location.pathname;
    principal.userLink(URLactual);
    //$('.sidenav').sidenav();
    M.AutoInit();

   
});