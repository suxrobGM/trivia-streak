<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Points.php';

		$pointObj = new Points();
if (isset($_GET["prize_id"]) && isset($_GET["cat_id"]) && isset($_GET["sec_cat_id"]) && isset($_GET["th_cat_id"]) && isset($_GET["game_user_id"]) && isset($_GET["game_user_type"]) )
{	
		$result = $pointObj->getPlayerRankInGame($_GET["prize_id"],$_GET["cat_id"],$_GET["sec_cat_id"],$_GET["th_cat_id"]);
		if($result)
		{
		
			$max = 0;
			$rank = 0;
			foreach($result as $item){
				if($item->game_user_id == $_GET["game_user_id"]){
					
					if($item->game_points > $max){
						$max = $item->game_points;
						$rank = $item->rank;
					}
		
					
				}
		
			}
			
			$response["rank"] = $rank;
			$response["points"] = $max;
		}
		else
		{
			$response["rank"] = 0;
			$response["points"] = 0;
			
		}
}
else
{
	$response["rank"] = 0;
	$response["points"] = 0;
	
}

		echo json_encode($response);

?>