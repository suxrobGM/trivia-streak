<?php
include_once('Database.php');
class Questions{
	
	function insert($qu_cat_id, $qu_sec_cat_id, $qu_th_cat_id, $qu_text, $qu_option1, $qu_option2, 
			$qu_option3, $qu_option4, $qu_answer, $qu_difficulty, $qu_status){
		$db = new Database();
		
		$array = array(
				'qu_cat_id'      	    => $qu_th_cat_id,
				'qu_sec_cat_id'      	=> $qu_sec_cat_id,
				'qu_th_cat_id'      	=> $qu_cat_id,
				'qu_text'           	=> $qu_text,
				'qu_option1'      	    => $qu_option1,
				'qu_option2'      	    => $qu_option2,
				'qu_option3'      	    => $qu_option3,
				'qu_option4'      	    => $qu_option4,
				'qu_answer'      	    => $qu_answer,
				'qu_difficulty'      	=> $qu_difficulty,
				'qu_status'             => $qu_status,
				'qu_created_on' 	    => date('Y-m-d')
		);
	
		
		$stmt = $db->queryWithParamsArray("insert into questions(qu_cat_id,qu_sec_cat_id, qu_th_cat_id,
				qu_text,qu_option1, qu_option2, qu_option3, qu_option4, qu_answer, qu_difficulty, qu_status, qu_created_on) 
				values(:qu_cat_id, :qu_sec_cat_id, :qu_th_cat_id, :qu_text, :qu_option1, :qu_option2, :qu_option3, 
				:qu_option4, :qu_answer, :qu_difficulty, :qu_status, :qu_created_on)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	
	function update($qu_id,$qu_status){
			$db = new Database();
	
			$array = array(
					'qu_id'		  => $qu_id,
					'qu_status'    => $qu_status
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE questions set qu_status=:qu_status WHERE qu_id=:qu_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
	
		
		function delete($qu_id) {
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'qu_id' => $qu_id
			);
		
			$result = $db->queryWithParamsArray("DELETE FROM questions where qu_id=:qu_id", $array);
			if($result)
				return TRUE;
			else
				return FALSE;
		}
		
		
	
	function getAllQuestions(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'qu_status'              => "Y"
		);
		
		$result = $db->queryWithParamsArray("SELECT * from questions where qu_status=:qu_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	
	
}

?>