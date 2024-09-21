<?php

class User {
    private $id;
    private $name;
    private $email;
    private $password;
    private $userType;

    public function __construct($id, $name, $email, $password, $userType) {
        $this->id = $id;
        $this->name = $name;
        $this->email = $email;
        $this->password = $password;
        $this->userType = $userType;
    }

}
?>