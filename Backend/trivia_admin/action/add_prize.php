<?php
session_start();
include_once '../controllers/Prize.php';

 	$prize_title = $_POST['prize_title'];
 	$prize_image = $_FILES['prize_image']['name'];
 	$prize_date = $_POST['prize_date'];
	
	
 	if(isset($_POST['status']))
 	{
 		$status = "Y";
 	}
 	else 
 	{
 		$status = "N";
 	}
 	
	$prize = new Prize();
		
		if($_FILES["prize_image"]["size"] > 0)
		{
			$target_path = "../images/prize/";
		
			$target_path = $target_path . basename( $_FILES['prize_image']['name']);
			$target_path;
		
			move_uploaded_file($_FILES['prize_image']['tmp_name'], $target_path);
			
		}
		else
		{
			$prize_image = "";
		}
		
		$insert = $prize->insert($prize_title, $prize_image,$status,$prize_date);
		if($insert){
			header("Location: ../manage_prizes.php?action=success");
		}
		else{
			header("Location: ../manage_prizes.php?action=failed");
		}
	
	
?>