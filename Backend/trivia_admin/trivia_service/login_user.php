<?php

/*
 * Following code will Get the user and pass from the app and and give the success or fail response .
*/

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Register.php';

// check for post data
if (!empty($_GET["user_name"]) && !empty($_GET["user_pass"])) {
	$userObj = new Register();
	
		$result = $userObj->login($_GET);
		if($result)
		{
			$response["data"] = array();
			array_push($response["data"], $result);
			$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "Username OR Password didn't match";
		}
}
else 
{
	$response["success"] = 0;
	$response["message"] = "Required field(s) is missing";
}
echo json_encode($response);

?>