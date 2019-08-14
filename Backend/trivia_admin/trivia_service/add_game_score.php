<?php

/*
 * Following code will Get the data from the app and insert in the database .
*/

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Points.php';

// check for post data
if (isset($_GET["game_user_id"]) && isset($_GET["game_user_type"]) && isset($_GET["game_points"]) ) {
	$pointObj = new Points();
	
	
		$result = $pointObj->insertUserPoints($_GET["game_user_id"], $_GET["game_user_type"], $_GET["game_points"], $_GET["cat_id"], $_GET["sec_cat_id"], $_GET["th_cat_id"], $_GET["prize_id"]);
		if($result)
		{
			$response["success"] = 1;
			$response["message"] = "Point inserted successfully.";
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "please try again later.";
		}
}
else 
{
	$response["success"] = 0;
	$response["message"] = "Required field(s) is missing";
}

echo json_encode($response);

?>