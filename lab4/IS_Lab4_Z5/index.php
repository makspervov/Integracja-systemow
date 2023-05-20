<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "jpa";

// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

$sql = "SELECT image FROM users WHERE id = 6";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    $row = mysqli_fetch_assoc($result);
    $image = $row['image'];
    header("Content-type: image/png");
    echo $image;
} else {
    echo "0 results";
}

mysqli_close($conn);
