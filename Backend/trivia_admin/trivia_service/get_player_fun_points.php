<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Points.php';

		$pointObj = new Points();
if (isset($_GET["prize_id"]) && isset($_GET["cat_id"]) && isset($_GET["sec_cat_id"]) && isset($_GET["th_cat_id"]) && isset($_GET["game_user_id"]) && isset($_GET["game_user_type"]) )
{	
		$result = $pointObj->getPlayerFunPoints($_GET);
		if($result)
		{
			$response["points"] = $result;
			$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["points"] = 0;
		}
}
else
{
	$response["success"] = 0;
	$response["points"] = 0;
}

		echo json_encode($response);

?>