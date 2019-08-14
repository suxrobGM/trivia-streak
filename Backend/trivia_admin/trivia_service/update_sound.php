<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Update.php';

		$upObj = new Update();
	
		$result = $upObj->updateSound($_GET['user_id'],$_GET['issound']);
		if($result)
		{
		$response["success"] = 1;
		$response["message"] = "sound successfully updated.";
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "sound can't be updated.";
		}

		echo json_encode($response);

?>