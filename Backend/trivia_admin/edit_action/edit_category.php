<?php
session_start();
include_once '../controllers/Category.php';

	$category_id = $_POST['category_id'];
	$category_image_old = $_POST['category_image_old'];
 	$category_name = $_POST['category_name'];
 	$category_image = $_FILES['category_image']['name'];
 	$category_status = $_POST['category_status'];
 	
	$category = new Category();
	
		
		if($_FILES["category_image"]["size"] > 0)
		{
			$target_path = "../images/".$_SESSION['user_name']."/category/";
		
			$target_path = $target_path . basename( $_FILES['category_image']['name']);
		
			move_uploaded_file($_FILES['category_image']['tmp_name'], $target_path);
		}
		else
		{
			$category_image = $category_image_old;
		}
		
		$edit = $category->edit($category_id, $category_name, $category_image, $category_status);
		if($edit){
			header("Location: ../manage_categories.php?action=update_success");
		}
		else{
			header("Location: ../manage_categories.php?action=update_failed");
		}
	
?>