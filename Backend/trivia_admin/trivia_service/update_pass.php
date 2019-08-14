<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Update.php';

		$upObj = new Update();
	
		$result = $upObj->updatePass($_GET['user_id'],$_GET['user_pass']);
		if($result)
		{
		$response["success"] = 1;
		$response["message"] = "Password successfully updated.";
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "Password can't be updated.";
		}

		echo json_encode($response);

?>