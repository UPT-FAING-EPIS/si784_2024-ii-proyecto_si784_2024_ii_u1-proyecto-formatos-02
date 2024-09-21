<?php
include '../config.php'; // Ajusta la ruta según tu estructura
session_start();

function authenticateUser($email, $password, $conn) {
    $email = mysqli_real_escape_string($conn, $email);
    $pass = mysqli_real_escape_string($conn, md5($password));

    $select_users = mysqli_query($conn, "SELECT * FROM `users` WHERE email = '$email' AND password = '$pass'") or die('query failed');

    if (mysqli_num_rows($select_users) > 0) {
        return mysqli_fetch_assoc($select_users);
    }
    
    return null;
}

$message = [];
if (isset($_POST['submit'])) {
    $user = authenticateUser($_POST['email'], $_POST['password'], $conn);
    
    if ($user) {
        if ($user['user_type'] == 'admin') {
            $_SESSION['admin_name'] = $user['name'];
            $_SESSION['admin_email'] = $user['email'];
            $_SESSION['admin_id'] = $user['id'];
            header('location: ../Views/admin_page.php');
            exit();
        } elseif ($user['user_type'] == 'user') {
            $_SESSION['user_name'] = $user['name'];
            $_SESSION['user_email'] = $user['email'];
            $_SESSION['user_id'] = $user['id'];
            header('location: ../Views/home.php');
            exit();
        }
    } else {
        $message[] = 'Correo o contraseña incorrectos.';
    }
}
?>
