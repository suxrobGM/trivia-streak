<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/LeaderBoard.php';

		$leaderObj = new LeaderBoard();
if (isset($_GET["prize_id"]))
{	
		$result = $leaderObj->getGameLeaderBoard($_GET);
		if($result)
		{
			$response["data"] = array();
			foreach ($result as $board)
			{
			array_push($response["data"], $board);
			}
		$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "No Data Found";
		}
}
else
{
	$response["success"] = 0;
	$response["message"] = "Required field(s) is missing";
	
}
		echo json_encode($response);

?>