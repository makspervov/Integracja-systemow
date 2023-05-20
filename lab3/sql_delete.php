<?php

    $servername = "localhost";
    $username = "sakila2";
    $password = "pass";
    $database = "sakila";
    $conn = new mysqli($servername, $username, $password, $database);

    if ($conn->connect_error) {
        die("Database connection failed: " . $conn->connect_error);
    }
    
    echo "Databse connected successfully, username " . $username . "<br><br>";
    
    $sql = "DELETE FROM film_actor WHERE actor_id = 1;";
    $conn->query($sql);
    
    $sql2 = "DELETE FROM actor WHERE actor_id = 1;";
    $conn->query($sql2);
    
    echo "Actor has been deleted";
    $conn->close();