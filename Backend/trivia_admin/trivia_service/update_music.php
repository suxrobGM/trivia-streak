<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Update.php';

		$upObj = new Update();
	
		$result = $upObj->updateMusic($_GET['user_id'],$_GET['ismusic']);
		if($result)
		{
		$response["success"] = 1;
		$response["message"] = "music successfully updated.";
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "music can't be updated.";
		}

		echo json_encode($response);

?>