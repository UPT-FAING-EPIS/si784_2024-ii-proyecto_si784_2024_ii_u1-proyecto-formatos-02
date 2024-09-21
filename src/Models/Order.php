<?php

class Order {
    private $id;
    private $userId;
    private $name;
    private $number;
    private $email;
    private $method;
    private $address;
    private $totalProducts;
    private $totalPrice;
    private $placedOn;
    private $paymentStatus;

    public function __construct($id, $userId, $name, $number, $email, $method, $address, $totalProducts, $totalPrice, $placedOn, $paymentStatus) {
        $this->id = $id;
        $this->userId = $userId;
        $this->name = $name;
        $this->number = $number;
        $this->email = $email;
        $this->method = $method;
        $this->address = $address;
        $this->totalProducts = $totalProducts;
        $this->totalPrice = $totalPrice;
        $this->placedOn = $placedOn;
        $this->paymentStatus = $paymentStatus;
    }
}
