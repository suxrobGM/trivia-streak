<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Points.php';

		$pointObj = new Points();
if (isset($_GET["game_user_id"]) && isset($_GET["game_user_type"]) )
{	
		$result = $pointObj->getPlayerGame($_GET["game_user_id"],$_GET["game_user_type"]);
		if($result)
		{			
			$response["daily_game"] = 15-$result;
			//$response["daily_game"] = 100;
		}
		else
		{
			$response["daily_game"] = 15;
			//$response["daily_game"] = 100;
			
		}
}
else
{
			$response["daily_game"] = 0;	
}

		echo json_encode($response);

?>