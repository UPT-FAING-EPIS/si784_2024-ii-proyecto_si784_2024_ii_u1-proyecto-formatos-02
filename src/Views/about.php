<!DOCTYPE html>
<html lang="en">
<head>
   <meta charset="UTF-8">
   <meta http-equiv="X-UA-Compatible" content="IE=edge">
   <meta name="viewport" content="width=device-width, initial-scale=1.0">
   <title>About</title>

   <link rel="icon" id="png" href="images/icon2.png">
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
   <link rel="stylesheet" href="css/style.css">
</head>
<body>

<?php include 'Views/header.php'; ?>

<div class="heading"> 
   <h3>Nosotros</h3>  
   <p> <a href="home.php">Inicio</a> / nosotros </p>
</div>

<section class="about">
   <div class="flex">
      <div class="image">
         <img src="images/about-img.jpg" alt="img aboutmy"> 
      </div>
      <div class="content">
         <h3>¿Por qué elegirnos?</h3>
         <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit...</p>
         <a href="contact.php" class="btn">Contactanos</a> 
      </div>
   </div>
</section>

<section class="reviews"> 
   <h1 class="title">Reseñas de clientes</h1>
   <div class="box-container">
      <div class="box">
         <img src="images/pic-1.png" alt="">
         <p>Lorem ipsum dolor sit amet consectetur adipisicing elit...</p>
         <div class="stars">
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
            <i class="fas fa-star"></i>
            <i class="fas fa-star-half-alt"></i>
         </div>
         <h3>Jose Alvarado</h3>
      </div>
      <!-- Repite para las demás reseñas -->
   </div>
</section>

<section class="authors">
   <h1 class="title">grandes autores</h1>
   <div class="box-container">
      <div class="box">
         <img src="images/author-1.jpg" alt="">
         <div class="share">
            <a href="#" class="fab fa-facebook-f"></a>
            <a href="#" class="fab fa-twitter"></a>
            <a href="#" class="fab fa-instagram"></a>
            <a href="#" class="fab fa-linkedin"></a>
         </div>
         <h3>Autor 1</h3>
      </div>
      <!-- Repite para los demás autores -->
   </div>
</section>

<?php include 'Views/footer.php'; ?>
<script src="js/script.js"></script>
</body>
</html>
