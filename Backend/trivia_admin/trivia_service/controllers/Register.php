<?php
include_once('Database.php');
class Register{
	
	function insert($array){
		$db = new Database();
	
		$stmt = $db->queryWithParamsArray("insert into users(user_email,user_name, user_pass,user_type)
				values(:user_email, :user_name, :user_pass, :user_type)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	
	
	function login($array){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$result = $db->queryWithParamsArray("SELECT * from users where user_name =:user_name AND user_pass=:user_pass", $array);
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
	
		function getUserData($user_name){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'user_name'	      => $user_name
		);
		
		$result = $db->queryWithParamsArray("SELECT user_name,user_email,user_type from users where user_name=:user_name", $array);
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
	
	function isDuplicate($user_name){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'user_name'	      => $user_name
		);
	
		$result = $db->queryWithParamsArray("SELECT * from users where user_name=:user_name", $array);
		if($result->rowCount() > 0 )
			return TRUE;
		else
			return FALSE;
	}
	
}

?>