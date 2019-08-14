<?php
session_start();
include_once '../controllers/Questions.php';

	$grand_parent_category_id = $_POST['grand_parent_category_id'];	
	//$parent_category_id = $_POST['parent_category_id'];
	//$child_category_id = $_POST['child_category_id'];
	$parent_category_id = 0;
	$child_category_id = 0;
	
	$qu_text = $_POST['qu_text'];
	
	$qu_option1 = $_POST['qu_option1'];
	$qu_option2 = $_POST['qu_option2'];
	$qu_option3 = $_POST['qu_option3'];
	$qu_option4 = $_POST['qu_option4'];
	
	$qu_answer = $_POST['qu_answer'];
	$qu_difficulty = $_POST['qu_difficulty'];
	
 	if(isset($_POST['status']))
 	{
 		$status = "Y";
 	}
 	else 
 	{
 		$status = "N";
 	}
 	
	$question = new Questions();
	
		
		$insert = $question->insert($grand_parent_category_id, $parent_category_id, $child_category_id, $qu_text, 
					$qu_option1, $qu_option2, $qu_option3, $qu_option4, $qu_answer, $qu_difficulty, $status);
		if($insert){
			header("Location: ../manage_questions.php?action=success");
		}
		else{
			header("Location: ../manage_questions.php?action=failed");
		}
	
	
?>