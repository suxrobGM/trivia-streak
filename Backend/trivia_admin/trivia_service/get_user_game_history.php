<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Prize.php';
require_once __DIR__ . '/controllers/Points.php';

		$prizeObj = new Prize();
		$pointObj = new Points();
if (isset($_GET["game_user_id"]) && isset($_GET["game_user_type"]) )
{	
		$presult = $prizeObj->getAllPrize();
		$response["data"] = array();
		if($presult)
		{
			foreach($presult as $prizeItem){
				$pointResult = $pointObj->getPlayerFunPointsForHistory($_GET["game_user_id"],$_GET["game_user_type"], $prizeItem->prize_id);
				if($pointResult){
					$prizeItem->score = $pointResult->game_points;
					$rankResult = $pointObj->getPlayerRankInGame($prizeItem->prize_id,$pointResult->cat_id ,$pointResult->sec_cat_id ,$pointResult->th_cat_id );
					if($rankResult)
					{
						$max = 0;
						$rank = 0;
						foreach($rankResult as $item){
							if($item->game_user_id == $_GET["game_user_id"]){
								
								if($item->game_points > $max){
									$max = $item->game_points;
									$rank = $item->rank;
								}
					
								
							}
					
						}
			
					$prizeItem->rank = $rank;
					
									array_push($response["data"], $prizeItem);
						$response["success"] = 1;
					}
				}else{
					$prizeItem->score = 0;
					$prizeItem->rank = 0;
				}
				
				

			}
		}
		else
		{
			$response["success"] = 0;
		}
}
else
{
	$response["success"] = 0;
}

		echo json_encode($response);

?>