<?php

// array for JSON response
$response = array();

// include db connect class
require_once __DIR__ . '/controllers/Categories.php';

		$catObj = new Categories();
	
		$result = $catObj->getFirstLevelCategories();
		if($result)
		{
			$response["data"] = array();
			foreach ($result as $cat)
			{
				if($catObj->checkFirstLevelCategoryValid($cat->cat_id))
				{
				array_push($response["data"], $cat);
				}
			}
		$response["success"] = 1;
		}
		else
		{
			$response["success"] = 0;
			$response["message"] = "No Category Found";
		}

		echo json_encode($response);

?>