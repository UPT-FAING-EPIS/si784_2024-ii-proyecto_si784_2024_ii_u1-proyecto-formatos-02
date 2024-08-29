let ruta = `funciones/funciones_carrito.php`; //Ruta para almacenar el pedido.

//Función para agregar un producto al carrito de compra desde el detalle del producto.
function agregarCarrito(btn_cart, idProduct, precio) {
  if (!btn_cart.classList.contains("loading")) {
    btn_cart.classList.add("loading");
    setTimeout(() => btn_cart.classList.remove("loading"), 1500);
  }

  let nameTokenProducto;

  if ("miProducto" in localStorage) {
    nameTokenProducto = localStorage.getItem("miProducto");
  } else {
    nameTokenProducto = tokenUnico();
    localStorage.setItem("miProducto", nameTokenProducto);
  }

  let dataString = `accion=addCar&idProduct=${idProduct}&precio=${precio}&tokenCliente=${nameTokenProducto}`;

  // Realizar la petición POST con Axios
  axios
    .post(ruta, dataString)
    .then((response) => {
      document.querySelector(".checkout_items").innerHTML = response.data;
    })
    .catch((error) => {
      console.error("Error al realizar la petición: ", error);
    });
  return false;
}

//Generando Token Unico del Cliente aleatoriamente
function tokenUnico() {
  const caracteres = "abcdefghijkmnpqrtuvwxyzABCDEFGHJKMNPQRTUVWXYZ23467890";
  const longitud = 32;
  let tokenCliente = "";

  for (let i = 0; i < longitud; i++) {
    const index = Math.floor(Math.random() * caracteres.length);
    tokenCliente += caracteres[index];
  }
  return tokenCliente;
}

//Función para aumentar de cantidad un producto
function aumentar_cantidad(idProd, precio) {
  let cantidaProducto = document.querySelector(`#display${idProd}`);
  let valorActual = parseInt(cantidaProducto.innerText);
  let nuevaCantidad = valorActual + 1;
  cantidaProducto.innerText = nuevaCantidad;

  let cantidadCarrito = document.getElementById("checkout_items").innerText;
  cantidadCarrito = parseInt(cantidadCarrito) + 1;
  document.getElementById("checkout_items").innerText = cantidadCarrito;

  if ("miProducto" in localStorage) {
    let nameTokenProducto = localStorage.getItem("miProducto"); //Obtener el token generado ya LocalStorage
    let dataString = `idProd=${idProd}&precio=${precio}&tokenCliente=${nameTokenProducto}&aumentarCantidad=${nuevaCantidad}`;

    axios
      .post(ruta, dataString)
      .then(function (response) {
        let total = parseInt(response.data.totalPagar);
        document.querySelector(
          "#totalPuntos"
        ).textContent = `$ ${formatearCantidad(total)}`;
      })
      .catch(function (error) {
        console.error("Error:", error);
      });
  }
}

//Disminuir la cantidad de productos en mi carrito de compra
function disminuir_cantidad(idProd, precio) {
  let cantidaProducto = document.querySelector(`#display${idProd}`);
  let valorActual = parseInt(cantidaProducto.innerText);

  if (valorActual >= 1) {
    if (valorActual == 1) {
      let fila = document.querySelector(`#resp${idProd}`);
      fila.remove();
    }

    let nuevaCantidad = valorActual - 1;
    cantidaProducto.innerText = nuevaCantidad;

    let cantidadCarrito = document.getElementById("checkout_items").innerText;
    cantidadCarrito = parseInt(cantidadCarrito) - 1;
    document.getElementById("checkout_items").innerText = cantidadCarrito;

    if ("miProducto" in localStorage) {
      let nameTokenProducto = localStorage.getItem("miProducto"); //Obtener el token generado ya LocalStorage
      let dataString = `accion=disminuirCantidad&idProd=${idProd}&precio=${precio}&tokenCliente=${nameTokenProducto}&cantidad_Disminuida=${nuevaCantidad}`;

      axios
        .post(ruta, dataString)
        .then(function (response) {
          if (response.data.totalProductos != 0) {
            let totalP = parseInt(response.data.totalPagar);
            document.querySelector(
              "#totalPuntos"
            ).textContent = `$ ${formatearCantidad(totalP)}`;
            return;
          } else {
            clearCart();
          }
        })
        .catch(function (error) {
          console.error("Error:", error);
        });
    }
  }
}

// Función para mostrar el modal de confirmación para borrar el producto
function mostrarModal(idProduct) {
  let btnBorrarProducto = document.querySelector("#btnYesEliminarProduct");
  btnBorrarProducto.setAttribute(
    "onclick",
    `yesEliminarProducto(${idProduct})`
  );
}

function yesEliminarProducto(idProduct) {
  let nameTokenProducto = localStorage.getItem("miProducto");
  let dataString = `accion=borrarproductoModal&idProduct=${idProduct}&tokenCliente=${nameTokenProducto}`;

  axios
    .post(ruta, dataString)
    .then(function (response) {
      if (response.data.totalProductos != 0) {
        document.querySelector(".checkout_items").innerHTML =
          response.data.totalProductoSeleccionados;

        document.querySelector(`#resp${idProduct}`).remove();
        $("#eliminarPrdoct").modal("hide");

        let totalP = parseInt(response.data.totalPagar);
        document.querySelector(
          "#totalPuntos"
        ).textContent = `$ ${formatearCantidad(totalP)}`;
        return;
      } else {
        clearCart();
      }
    })
    .catch(function (error) {
      console.error("Error:", error);
    });
}

function formatearCantidad(cantidad) {
  let formattedTotal = cantidad.toLocaleString("es-ES", {
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  });
  return formattedTotal;
}

//Funcion que recibe la solicitud para gestionar el pedido
const solictarPedido = (codPedido) => {
  const whatsappAPI = "https://api.whatsapp.com/send?phone=";
  const phoneNumber = "+51 982531296";

  const link = `http://localhost:8080/test/pdfPedido.php?codPedido=${codPedido}`;
  const message = `¡Hola! Me interesa el siguiente pedido: ${link}`;
  const whatsappURL = `${whatsappAPI}${phoneNumber}&text=${message}`;

  window.open(whatsappURL, "_blank");
};

//Funcion para limpiar todo mi carrito
function clearCart() {
  let dataString = `accion=limpiarTodoElCarrito`;
  axios
    .post(ruta, dataString)
    .then(function (response) {
      if (response.data.mensaje == 1) {
        localStorage.removeItem("miProducto");
        window.location.href = window.location.href;
      }
    })
    .catch(function (error) {
      console.error("Error:", error);
    });
}
