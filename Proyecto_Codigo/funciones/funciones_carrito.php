<?php
session_start();
include('../config/config.php');
// Establecer las cabeceras para indicar que la respuesta es en formato JSON
header('Content-Type: application/json');


if (isset($_POST["aumentarCantidad"])) {
    $idProd               = $_POST['idProd'];
    $precio               = $_POST['precio'];
    $tokenCliente         = $_POST['tokenCliente'];
    $cantidaProducto      = $_POST['aumentarCantidad'];

    $UpdateCant = "UPDATE pedidostemporales 
              SET cantidad ='$cantidaProducto'
              WHERE tokenCliente='$tokenCliente'
              AND id='$idProd'";
    $result = mysqli_query($con, $UpdateCant);

    $responseData = array(
        'estado' => 'OK',
        'totalPagar' => totalAccionAumentarDisminuir($con, $tokenCliente)
    );
    // Enviar la respuesta JSON
    echo json_encode($responseData);
}



// Agregar a carrito de compra el producto
if (isset($_POST["accion"]) && $_POST["accion"] == "addCar") {
    $_SESSION['tokenStoragel']  = $_POST['tokenCliente'];
    $idProduct                  = $_POST['idProduct'];
    $precio                     = $_POST['precio'];
    $tokenCliente               = $_POST['tokenCliente'];

    //Verifico si ya existe el producto almacenado en la tabla temporal de acuerdo al Token Unico del Cliente
    $ConsultarProduct = ("SELECT * FROM pedidostemporales WHERE tokenCliente='" . $tokenCliente . "' AND producto_id='" . $idProduct . "' ");
    $jqueryProduct    = mysqli_query($con, $ConsultarProduct);
    if (mysqli_num_rows($jqueryProduct) > 0) {
        $DataProducto     = mysqli_fetch_array($jqueryProduct);
        $newCantidad   = ($DataProducto['cantidad'] + 1);

        $UdateCantidad = ("UPDATE pedidostemporales SET cantidad='" . $newCantidad . "' WHERE producto_id='" . $idProduct . "' AND tokenCliente='" . $tokenCliente . "' ");
        $resultUpdat = mysqli_query($con, $UdateCantidad);
    } else {
        $InsertProduct = ("INSERT INTO pedidostemporales (producto_id, cantidad, tokenCliente) VALUES ('$idProduct','1','$tokenCliente')");
        $result = mysqli_query($con, $InsertProduct);
    }

    //Total carrito en el icono de compra
    $SqlTotalProduct       = ("SELECT SUM(cantidad) AS totalProd FROM pedidostemporales WHERE tokenCliente='" . $_SESSION['tokenStoragel'] . "' GROUP BY tokenCliente");
    $jqueryTotalProduct    = mysqli_query($con, $SqlTotalProduct);
    $DataTotalProducto     = mysqli_fetch_array($jqueryTotalProduct);
    echo $DataTotalProducto['totalProd'];
}


// Borrar producto del carrito

if (isset($_POST["accion"]) && $_POST["accion"] == "borrarproductoModal") {
    $nameTokenProducto  = $_POST['tokenCliente'];

    $DeleteRegistro = ("DELETE FROM pedidostemporales WHERE id= '" . $_POST["idProduct"] . "' ");
    mysqli_query($con, $DeleteRegistro);

    $respData = array(
        'totalProductos' => totalProductosSeleccionados($con, $nameTokenProducto),
        'totalProductoSeleccionados' => totalProductosBD($con, $nameTokenProducto),
        'totalPagar' => totalAccionAumentarDisminuir($con, $nameTokenProducto),
        'estado' => 'OK'
    );
    echo json_encode($respData);
}

//Total productos en mi carrito de compra

function totalProductosBD($con, $nameTokenProducto)
{
    $sqlTotalProduct = "SELECT SUM(cantidad) AS totalProd FROM pedidostemporales WHERE tokenCliente='" . $nameTokenProducto . "' GROUP BY tokenCliente";
    $jqueryTotalProduct = mysqli_query($con, $sqlTotalProduct);
    if ($jqueryTotalProduct) {
        $dataTotalProducto = mysqli_fetch_array($jqueryTotalProduct);
        return  $dataTotalProducto["totalProd"];
    } else {
        return 0;
    }
}

function totalAccionAumentarDisminuir($con, $tokenCliente)
{
    $SqlDeudaTotal = "
        SELECT SUM(p.precio * pt.cantidad) AS totalPagar 
        FROM products AS p
        INNER JOIN pedidostemporales AS pt
        ON p.id = pt.producto_id
        WHERE pt.tokenCliente = '" .  $tokenCliente . "'";
    $jqueryDeuda = mysqli_query($con, $SqlDeudaTotal);
    $dataDeuda = mysqli_fetch_array($jqueryDeuda);
    return $dataDeuda['totalPagar'];
}

//Funcion que esta al pendiente de verificar si hay pedidos activos por el usuario en cuestión

function totalProductosSeleccionados($con, $tokenCliente)
{
    $ConsultarProduct = ("SELECT * FROM pedidostemporales WHERE tokenCliente='" . $tokenCliente . "' ");
    $jqueryProduct    = mysqli_query($con, $ConsultarProduct);
    if (mysqli_num_rows($jqueryProduct) > 0) {
        return mysqli_num_rows($jqueryProduct);
    } else {
        return 0;
    }
}

//funcion limpiar carrito

if (isset($_POST["accion"]) && $_POST["accion"] == "limpiarTodoElCarrito") {
    // Cerrar todas las variables de sesión
    session_unset();

    // Destruir la sesión
    session_destroy();

    // Cerrar una variable de sesión específica
    // unset($_SESSION['tokenStoragel']);

    echo json_encode(['mensaje' => 1]);
}
