// tests/LoginTest.php
<?php
use PHPUnit\Framework\TestCase;

class LoginTest extends TestCase {
    private $conn;

    protected function setUp(): void {
        // Conectar a la base de datos
        $this->conn = new mysqli('localhost', 'root', '', 'tu_base_de_datos');
        if ($this->conn->connect_error) {
            die('Conexión fallida: ' . $this->conn->connect_error);
        }
    }

    public function testAuthenticateUserValid() {
        // Agregar un usuario de prueba a la base de datos
        $email = 'test@example.com';
        $password = md5('password');

        $this->conn->query("INSERT INTO users (email, password, user_type, name) VALUES ('$email', '$password', 'user', 'Test User')");

        $result = authenticateUser($email, 'password', $this->conn);

        $this->assertNotNull($result);
        $this->assertEquals('Test User', $result['name']);

        // Limpiar después de la prueba
        $this->conn->query("DELETE FROM users WHERE email = '$email'");
    }

    public function testAuthenticateUserInvalid() {
        $result = authenticateUser('invalid@example.com', 'wrongpassword', $this->conn);
        $this->assertNull($result);
    }

    protected function tearDown(): void {
        $this->conn->close();
    }
} 
?>