<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Questions.php';

		$questionObj = new Questions();
if (isset($_GET["category_id"]) && isset($_GET["parent_id"]) && isset($_GET["grand_parant_id"]))
{	
		$result = $questionObj->getQuestions($_GET["category_id"], $_GET["parent_id"], $_GET["grand_parant_id"]);
		if($result)
		{
			$response["data"] = array();
			foreach ($result as $cat)
			{
			//array_push($response["data"], $cat);
			htmlspecialchars_decode(array_push($response["data"], $cat));
			
			}
		$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "No Question Found";
		}
}
else
{
	$response["success"] = 0;
	$response["message"] = "Could not find the category ID";
}

		echo json_encode($response);

?>