<?php
include_once('Database.php');
class Points{
	
	function getPlayerFunPoints($array){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
				
		$result = $db->queryWithParamsArray("SELECT * from game_fun_leaderboard 
				where prize_id=:prize_id AND game_user_id=:game_user_id AND game_user_type=:game_user_type AND cat_id=:cat_id AND sec_cat_id=:sec_cat_id AND th_cat_id=:th_cat_id ORDER BY game_points desc LIMIT 1", $array);
		if($result->rowCount() > 0 )
			return $result->fetch()->game_points;
		else
			return FALSE;
	}
	
		
	function getPlayerFunPointsForHistory($game_user_id,$game_user_type,$prize_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);	
				$array = array(
				'game_user_id'	  => $game_user_id,
				'game_user_type'	  => $game_user_type,
				'prize_id'	  => $prize_id
		);
		$result = $db->queryWithParamsArray("SELECT * from game_fun_leaderboard Where prize_id=:prize_id AND game_user_id=:game_user_id AND game_user_type=:game_user_type  ORDER BY game_points desc LIMIT 1", $array);
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
	function insertUserPoints($game_user_id, $game_user_type, $game_points, $cat_id, $sec_cat_id, $th_cat_id, $prize_id){
		$db = new Database();
		$array = array(
				'game_user_id'	  => $game_user_id,
				'game_user_type'	  => $game_user_type,
				'game_points'	  => $game_points,
				'cat_id'	  => $cat_id,
				'sec_cat_id'	  => $sec_cat_id,
				'th_cat_id'	  => $th_cat_id,
				'prize_id'	  => $prize_id,
				'game_date'	=> date('Y-m-d')
		);
		$stmt = $db->queryWithParamsArray("insert into game_fun_leaderboard(game_user_id,game_user_type, game_points,cat_id, sec_cat_id, th_cat_id,prize_id,game_date)
				values(:game_user_id,:game_user_type, :game_points,:cat_id, :sec_cat_id,:th_cat_id,:prize_id,:game_date)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	function getPlayerRankInGame($prize_id, $cat_id,$sec_cat_id,$th_cat_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
		$array = array(
		'prize_id'	  => $prize_id,
				'cat_id'	  => $cat_id,
				'sec_cat_id'	  => $sec_cat_id,
				'th_cat_id'	  => $th_cat_id,
				'prize_id1'	  => $prize_id,
				'cat_id1'	  => $cat_id,
				'sec_cat_id1'	  => $sec_cat_id,
				'th_cat_id1'	  => $th_cat_id,
		);
		
		$result = $db->queryWithParamsArray("SELECT  uo.game_user_id, uo.game_user_type, uo.game_points,
        (
        SELECT  COUNT(DISTINCT ui.game_points)
        FROM    game_fun_leaderboard  ui
        WHERE   (ui.game_points) >= (uo.game_points) AND prize_id=:prize_id1 AND cat_id=:cat_id1 AND sec_cat_id=:sec_cat_id1 AND th_cat_id=:th_cat_id1
        ) AS rank
FROM    game_fun_leaderboard  uo where prize_id=:prize_id AND cat_id=:cat_id AND sec_cat_id=:sec_cat_id AND th_cat_id=:th_cat_id", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	function getPlayerGame($game_user_id, $game_user_type){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
		$array = array(
				'game_user_id'	  => $game_user_id,
				'game_user_type'	  => $game_user_type,
				'game_date'	=> date('Y-m-d')
		);
		
		$result = $db->queryWithParamsArray("SELECT count(game_id) as daily_game FROM game_fun_leaderboard WHERE game_user_id =:game_user_id and game_user_type =:game_user_type AND game_date =:game_date", $array);
		if($result->rowCount() > 0 )
			return $result->fetch()->daily_game;
		else
			return FALSE;
	}
}

?>