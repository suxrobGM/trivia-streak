<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Categories.php';

		$catObj = new Categories();
if (isset($_GET["parent_id"]) && isset($_GET["grand_parent_id"]))
{	
		$result = $catObj->getThirdLevelCategories($_GET["parent_id"], $_GET["grand_parent_id"]);
		if($result)
		{
			$response["data"] = array();
			foreach ($result as $cat)
			{
			array_push($response["data"], $cat);
			}
		$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "No Category Found";
		}
}
else
{
	$response["success"] = 0;
	$response["message"] = "Could not find the parent or grand parent category ID";
}

		echo json_encode($response);

?>