<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Update.php';

		$upObj = new Update();
	
		$result = $upObj->updateCountry($_GET['user_id'],$_GET['user_country']);
		if($result)
		{
		$response["success"] = 1;
		$response["message"] = "Country successfully updated.";
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "Country can't be updated.";
		}

		echo json_encode($response);

?>