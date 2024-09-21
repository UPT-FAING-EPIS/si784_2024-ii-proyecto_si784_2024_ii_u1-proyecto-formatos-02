<?php

class Message {
    private $id;
    private $userId;
    private $name;
    private $email;
    private $number;
    private $message;

    public function __construct($id, $userId, $name, $email, $number, $message) {
        $this->id = $id;
        $this->userId = $userId;
        $this->name = $name;
        $this->email = $email;
        $this->number = $number;
        $this->message = $message;
    }

}
?>