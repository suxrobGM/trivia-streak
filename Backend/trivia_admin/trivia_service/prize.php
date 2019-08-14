<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Prize.php';

		$prizeObj = new Prize();
	
		$result = $prizeObj->getPrize();
		if($result)
		{
			$response["data"] = array();
			foreach ($result as $p)
			{
				$db_date = $p->prize_date;
				$old_date_timestamp = strtotime($db_date);
				$new_date = date('F, d Y ', $old_date_timestamp);
				$p->prize_date = $new_date;	
				$score = $prizeObj->getHighScore($p->prize_id);
				if($score){
					$p->score = $score->game_points;
				}
				else
				{
					$p->score = 0;
				}
			array_push($response["data"], $p);
			}
		$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "No Prize Found";
		}

		echo json_encode($response);

?>