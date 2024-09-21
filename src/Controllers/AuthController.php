<?php

session_start();
include 'config.php';

class AuthController {
    public function authenticateUser($email, $password, $conn) {
        $email = mysqli_real_escape_string($conn, $email);
        $pass = mysqli_real_escape_string($conn, md5($password));

        $select_users = mysqli_query($conn, "SELECT * FROM `users` WHERE email = '$email' AND password = '$pass'") or die('query failed');

        if (mysqli_num_rows($select_users) > 0) {
            return mysqli_fetch_assoc($select_users);
        }
        
        return null;
    }

    public function login() {
        if (isset($_POST['submit'])) {
            $user = $this->authenticateUser($_POST['email'], $_POST['password'], $conn);
            
            if ($user) {
                if ($user['user_type'] == 'admin') {
                    $_SESSION['admin_name'] = $user['name'];
                    $_SESSION['admin_email'] = $user['email'];
                    $_SESSION['admin_id'] = $user['id'];
                    header('location:admin_page.php');
                    exit();
                } elseif ($user['user_type'] == 'user') {
                    $_SESSION['user_name'] = $user['name'];
                    $_SESSION['user_email'] = $user['email'];
                    $_SESSION['user_id'] = $user['id'];
                    header('location:home.php');
                    exit();
                }
            } else {
                return 'Incorrect email or password!';
            }
        }
        return null;
    }
}

// Instancia y manejo de la autenticación
$controller = new AuthController();
$message = $controller->login();
?>