<?php
include_once('Database.php');
class Prize{
	
	function insert($prize_title, $prize_image, $prize_status,$prize_date){
		$db = new Database();
		
		$array = array(
				'prize_title'      	=> $prize_title,
				'prize_image'      	=> $prize_image,
				'prize_status'       => $prize_status,
				'prize_date' => $prize_date
		);
	
		
		$stmt = $db->queryWithParamsArray("insert into game_prize(prize_title,prize_image, prize_status,prize_date)  values(:prize_title, :prize_image, :prize_status, :prize_date)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	
	function insertWinner($game_user_id, $game_user_type,$prize_id,$winner_points){
		$db = new Database();
		
		$array = array(
				'game_user_id'      	=> $game_user_id,
				'game_user_type'      	=> $game_user_type,
				'prize_id'       => $prize_id,
				'winner_points' => $winner_points
		);
	
		
		$stmt = $db->queryWithParamsArray("insert into game_winner(game_user_id,game_user_type, prize_id,winner_points)  values(:game_user_id, :game_user_type, :prize_id,:winner_points)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	function update($prize_id,$prize_status){
			$db = new Database();
	
			$array = array(
					'prize_id'		  => $prize_id,
					'prize_status'    => $prize_status
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE game_prize set prize_status=:prize_status WHERE prize_id=:prize_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
		
		function delete($prize_id) {
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'prize_id' => $prize_id
			);
		
			$result = $db->queryWithParamsArray("DELETE FROM game_prize where prize_id=:prize_id", $array);
			if($result)
				return TRUE;
			else
				return FALSE;
		}
		function getPrizeDetail($prize_id){
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'prize_id'      => $prize_id
			);
		
			$stmt = $db->queryWithParamsArray("select * from game_prize where prize_id=:prize_id ",$array);
			if($stmt){
				return $stmt->fetch();
			}
			else{
				return FALSE;
			}
		}
		
	
	function getAllPrizes(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'prize_status'              => "Y"
		);
		
		$result = $db->queryWithParamsArray("SELECT * from game_prizes where prize_status=:prize_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	function getAllUserForPrize($prize_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'prize_id'              => $prize_id
		);
		
		$result = $db->queryWithParamsArray("SELECT `game_user_id`,`game_user_type`,MAX(`game_points`) as points, prize_id  FROM `game_fun_leaderboard` WHERE `prize_id` = :prize_id group by `game_user_id` order by game_points desc limit 5", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	function checkPrizeWinner($prize_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'prize_id'              => $prize_id
		);
		
		$result = $db->queryWithParamsArray("SELECT * FROM `game_winner` WHERE `prize_id` = :prize_id", $array);
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
		function getUserEmail($game_user_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'game_user_id'              => $game_user_id
		);
		
		$result = $db->queryWithParamsArray("SELECT * FROM `users` WHERE `user_name` = :game_user_id", $array);
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
}

?>