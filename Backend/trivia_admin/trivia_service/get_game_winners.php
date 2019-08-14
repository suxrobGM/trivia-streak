<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Winner.php';

		$winnerObj = new Winner();

		$result = $winnerObj->getWinners();
		if($result)
		{
			$response["data"] = array();
			foreach ($result as $win)
			{
			array_push($response["data"], $win);
			}
		$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "No Data Found";
		}
		echo json_encode($response);

?>