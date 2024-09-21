<?php

class Cart {
    private $id;
    private $userId;
    private $name;
    private $price;
    private $quantity;
    private $image;

    public function __construct($id, $userId, $name, $price, $quantity, $image) {
        $this->id = $id;
        $this->userId = $userId;
        $this->name = $name;
        $this->price = $price;
        $this->quantity = $quantity;
        $this->image = $image;
    }

}
?>