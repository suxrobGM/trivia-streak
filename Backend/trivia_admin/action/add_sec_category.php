<?php
session_start();
include_once '../controllers/SecondCategory.php';

	$parent_category_id = $_POST['parent_category_id'];
	$category_name = $_POST['category_name'];
 	$category_image = $_FILES['category_image']['name'];
 	
 	if(isset($_POST['status']))
 	{
 		$status = "Y";
 	}
 	else 
 	{
 		$status = "N";
 	}
 	
	$category = new SecondCategory();
	if(!$category->isDuplicate($category_name))
	{
		
		if($_FILES["category_image"]["size"] > 0)
		{
			$target_path = "../images/2nd_level_categories/";
		
			$target_path = $target_path . basename( $_FILES['category_image']['name']);
			$target_path;
		
			move_uploaded_file($_FILES['category_image']['tmp_name'], $target_path);
			
		}
		else
		{
			$category_image = "";
		}
		
		$insert = $category->insert($category_name, $category_image,$status,$parent_category_id);
		if($insert){
			header("Location: ../manage_sec_categories.php?action=success");
		}
		else{
			header("Location: ../manage_sec_categories.php?action=failed");
		}
	}
	else 
	{	
		header("Location: ../manage_sec_categories.php?action=duplicate");
	}
	
	
?>