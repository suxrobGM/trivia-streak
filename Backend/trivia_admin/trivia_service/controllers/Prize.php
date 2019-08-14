<?php
include_once('Database.php');
class Prize{
	
	function getPrize(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'prize_status'	  => "Y",
				'prize_date'	  => date('Y-m-d')
		); 
		
		$result = $db->queryWithParamsArray("SELECT * from game_prize where prize_status=:prize_status AND prize_date >= :prize_date order by prize_date asc", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	function getHighScore($prize_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'prize_id'	  => $prize_id
		); 
		
		$result = $db->queryWithParamsArray("SELECT game_points FROM game_fun_leaderboard WHERE prize_id=:prize_id ORDER BY game_points DESC LIMIT 1 ", $array);
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
		function getAllPrize(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'prize_status'	  => "Y"
		); 
		
		$result = $db->queryWithParamsArray("SELECT * from game_prize where prize_status=:prize_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}

}

?>