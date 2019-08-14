<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Update.php';

		$upObj = new Update();
	
		$result = $upObj->updateEmail($_GET['user_id'],$_GET['user_email']);
		if($result)
		{
		$response["success"] = 1;
		$response["message"] = "Email successfully updated.";
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "Email can't be updated.";
		}

		echo json_encode($response);

?>