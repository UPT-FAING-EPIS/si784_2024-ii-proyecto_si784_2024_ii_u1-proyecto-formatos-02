<?php

class Product {
    private $id;
    private $name;
    private $price;
    private $image;

    public function __construct($id, $name, $price, $image) {
        $this->id = $id;
        $this->name = $name;
        $this->price = $price;
        $this->image = $image;
    }
}
?>