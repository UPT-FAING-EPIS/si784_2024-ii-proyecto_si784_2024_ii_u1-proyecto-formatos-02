<?php

session_start();
include 'config.php';

class AboutController {
    public function show() {
        $user_id = $_SESSION['user_id'] ?? null;

        if (!$user_id) {
            header('location:login.php');
            exit();
        }

        include 'Views/about.php';
    }
}

// Instancia y llama al método
$controller = new AboutController();
$controller->show();
