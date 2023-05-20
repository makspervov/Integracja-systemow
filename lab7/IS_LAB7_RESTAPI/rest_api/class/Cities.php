<?php

class Cities{

    private $citiesTable = "city";
    public $ID;
    public $Name;
    public $CountryCode;
    public $District;
    public $Population;
    
    public function __construct($db){
        $this->conn = $db;
    }

    function read(){
        if($this->ID) {
            $stmt = $this->conn->prepare("SELECT * FROM ".$this->citiesTable." WHERE ID = ?");
            $stmt->bind_param("i", $this->ID);
        } else {
            $stmt = $this->conn->prepare("SELECT * FROM " . $this->citiesTable);
        }
        $stmt->execute();
        $result = $stmt->get_result();
        return $result;
    }
}

