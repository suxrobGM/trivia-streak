<?php
session_start();
include_once '../controllers/Prize.php';

 	$prize_id = $_GET['prize_id'];
	$game_user_id = $_GET['game_user_id'];
	$game_user_type = $_GET['game_user_type'];
	$winner_points = $_GET['winner_points'];

	$prize = new Prize();
		$insert = $prize->insertWinner($game_user_id, $game_user_type,$prize_id,$winner_points);
		if($insert){
			header("Location: ../choose_winner.php?prize_id=".$prize_id);
		}
	
	
?>