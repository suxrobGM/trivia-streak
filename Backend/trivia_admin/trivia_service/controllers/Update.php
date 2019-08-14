<?php
include_once('Database.php');
class Update{
	function updateEmail($user_id,$user_email){
			$db = new Database();
	
			$array = array(
					'user_id'		  => $user_id,
					'user_email'    => $user_email
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE users set user_email=:user_email WHERE  user_name=:user_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
	
function updatePass($user_id,$user_pass){
			$db = new Database();
	
			$array = array(
					'user_id'		  => $user_id,
					'user_pass'    => $user_pass
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE users set user_pass=:user_pass WHERE  user_name=:user_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
		
		function updateCountry($user_id,$user_country){
			$db = new Database();
	
			$array = array(
					'user_id'		  => $user_id,
					'user_country'    => $user_country
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE users set user_country=:user_country WHERE  user_name=:user_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
		function updateMusic($user_id,$ismusic){
			$db = new Database();
	
			$array = array(
					'user_id'		  => $user_id,
					'ismusic'    => $ismusic
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE users set ismusic=:ismusic WHERE  user_name=:user_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
			function updateSound($user_id,$issound){
			$db = new Database();
	
			$array = array(
					'user_id'		  => $user_id,
					'issound'    => $issound
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE users set issound=:issound WHERE  user_name=:user_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
}

?>