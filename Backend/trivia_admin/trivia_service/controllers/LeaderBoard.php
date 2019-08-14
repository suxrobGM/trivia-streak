<?php
include_once('Database.php');
class LeaderBoard{
	
	function getGameLeaderBoard($array){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
				
		$result = $db->queryWithParamsArray("SELECT game_user_id,max(game_points) AS game_point FROM game_fun_leaderboard WHERE prize_id=:prize_id group by game_user_id order by game_point desc", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
}

?>