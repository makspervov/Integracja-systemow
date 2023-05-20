<?php

class Database{
    private $host = 'mysecond_dockerized_database_server';
    private $user = 'newuser';
    private $password = "newpassword";
    private $database = "world";

    public function getConnection(){
        $conn = new mysqli($this->host, $this->user, $this->password, $this->database);
        if($conn->connect_error){
            die("Error failed to connect to MySQL: ".$conn->connect_error);
        } 
        else {
            return $conn;
        }
    }
}
