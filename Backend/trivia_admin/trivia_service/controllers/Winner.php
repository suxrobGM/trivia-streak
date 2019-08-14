<?php
include_once('Database.php');
class Winner{
	
	function getWinners(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				
		); 
		
		$result = $db->queryWithParamsArray("SELECT game_winner.game_user_id ,game_winner.winner_points, game_prize.prize_title ,game_prize.prize_date  FROM game_winner inner join game_prize on game_winner.prize_id=game_prize.prize_id", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
}

?>