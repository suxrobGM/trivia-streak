<?php
class Database extends PDO {
// http://www.php.net/manual/en/pdo.prepared-statements.php
//http://prash.me/php-pdo-and-prepared-statements/
    const DB_HOST='localhost';
    const DB_PORT='3306';
    const DB_NAME='trivia';
    const DB_USER='root';
    const DB_PASS='';
      
	public function __construct($driver_options=null) {
	
		try {
			parent::__construct('mysql:host='.Database::DB_HOST.';port='.Database::DB_PORT.';dbname='.Database::DB_NAME,
								Database::DB_USER, Database::DB_PASS,$driver_options);
			$this->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION); // to show error
			$this->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);	// defend sql injection
	
		} catch(PDOException $e){                
			die('Uncaught exception: '. $e->getMessage());
		}
	}


    public function queryWithParams($query){ //secured query with prepare and execute
        $args = func_get_args();
        array_shift($args); //first element is not an argument but the query itself, should removed

        $stmt = parent::prepare($query);
        $stmt->execute($args);
        return $stmt;

    }
	
	public function queryWithParamsArray($query, $argArray){ //secured query with prepare and execute
        $stmt = parent::prepare($query);
        $stmt->execute($argArray);
        return $stmt;
	}
	
}

?>