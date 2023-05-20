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
    
    $sql = "CREATE TABLE sales (
        id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
        sale_id INT(6) NOT NULL,
        amount FLOAT NOT NULL,
        date DATE NOT NULL
    )";

    $result = mysqli_query($conn, $sql);

    if ($result) {
        echo "New table has been created!";
    } else {
        echo "An error occurred while creating the table!";
    }
    mysqli_close($conn);