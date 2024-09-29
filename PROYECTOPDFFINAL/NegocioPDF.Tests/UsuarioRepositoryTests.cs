// Importar las bibliotecas necesarias
using NUnit.Framework; // Biblioteca para el framework de pruebas NUnit
using NegocioPDF.Repositories; // Importar la clase UsuarioRepository desde el proyecto principal
using NegocioPDF.Models; // Importar el modelo de Usuario
using System;
using System.Linq; // Para el uso de métodos LINQ en colecciones
using Dapper; // Biblioteca Dapper para interactuar con la base de datos
using MySql.Data.MySqlClient; // Biblioteca para manejar conexiones con MySQL

namespace NegocioPDF.Tests
{
    // Indica que esta es una clase de pruebas de NUnit
    [TestFixture]
    public class UsuarioRepositoryTests
    {
        // Instancia del repositorio de usuarios para realizar las pruebas
        private UsuarioRepository _usuarioRepository;

        // Cadena de conexión a la base de datos de prueba
        private readonly string _connectionString = "Server=localhost;Database=pdfsolutions_test;Uid=root;Pwd=;";

        // Método que se ejecuta antes de cada prueba para inicializar el entorno
        [SetUp]
        public void Setup()
        {
            // Crear una nueva instancia de UsuarioRepository con la cadena de conexión
            _usuarioRepository = new UsuarioRepository(_connectionString);

            // Limpiar la tabla de usuarios antes de cada prueba para asegurar un entorno limpio
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("DELETE FROM Usuarios;");
            }
        }

        // Prueba para verificar que el método RegistrarUsuario agrega correctamente un usuario a la base de datos
        [Test]
        public void RegistrarUsuario_DeberiaAgregarUsuarioALaBaseDeDatos()
        {
            // Arrange: Configurar el usuario que se va a registrar
            var usuario = new Usuario
            {
                Nombre = "Mario",
                Correo = "mario@example.com",
                Password = "password123"
            };

            // Act: Llamar al método para registrar el usuario
            _usuarioRepository.RegistrarUsuario(usuario);

            // Assert: Verificar que el usuario fue agregado a la base de datos
            var usuarios = _usuarioRepository.ObtenerUsuarios();
            Assert.AreEqual(1, usuarios.Count()); // Verificar que hay exactamente un usuario registrado
            Assert.AreEqual("Mario", usuarios.First().Nombre); // Verificar que el nombre del usuario es el esperado
        }

        // Prueba para verificar que se lanza una excepción si se intenta registrar un usuario con un correo ya existente
        [Test]
        public void RegistrarUsuario_DeberiaLanzarExcepcionSiCorreoYaExiste()
        {
            // Arrange: Configurar dos usuarios con el mismo correo
            var usuario1 = new Usuario
            {
                Nombre = "Mario",
                Correo = "mario@example.com",
                Password = "password123"
            };

            var usuario2 = new Usuario
            {
                Nombre = "Juan",
                Correo = "mario@example.com",
                Password = "password456"
            };

            // Act: Registrar el primer usuario
            _usuarioRepository.RegistrarUsuario(usuario1);

            // Assert: Verificar que se lanza una excepción al intentar registrar el segundo usuario con el mismo correo
            var ex = Assert.Throws<Exception>(() => _usuarioRepository.RegistrarUsuario(usuario2));
            Assert.AreEqual("El correo ya está registrado", ex.Message); // Verificar que el mensaje de la excepción es el esperado
        }

        // Prueba para verificar que el inicio de sesión con credenciales correctas es exitoso
        [Test]
        public void Login_DeberiaRetornarUsuarioSiCredencialesSonCorrectas()
        {
            // Arrange: Registrar un usuario en la base de datos
            var usuario = new Usuario
            {
                Nombre = "Mario",
                Correo = "mario@example.com",
                Password = "password123"
            };
            _usuarioRepository.RegistrarUsuario(usuario);

            // Act: Intentar iniciar sesión con las credenciales correctas
            var resultado = _usuarioRepository.Login("mario@example.com", "password123");

            // Assert: Verificar que el inicio de sesión fue exitoso
            Assert.IsNotNull(resultado); // Verificar que el usuario no es nulo
            Assert.AreEqual("Mario", resultado.Nombre); // Verificar que el nombre del usuario es el esperado
        }

        // Prueba para verificar que el inicio de sesión falla si las credenciales son incorrectas
        [Test]
        public void Login_DeberiaRetornarNullSiCredencialesSonIncorrectas()
        {
            // Arrange: Registrar un usuario en la base de datos
            var usuario = new Usuario
            {
                Nombre = "Mario",
                Correo = "mario@example.com",
                Password = "password123"
            };
            _usuarioRepository.RegistrarUsuario(usuario);

            // Act: Intentar iniciar sesión con una contraseña incorrecta
            var resultado = _usuarioRepository.Login("mario@example.com", "wrongpassword");

            // Assert: Verificar que el inicio de sesión falló
            Assert.IsNull(resultado); // Verificar que el resultado es nulo
        }
    }
}