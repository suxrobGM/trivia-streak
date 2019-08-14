<?php
include_once('Database.php');
class Questions{
	
	function getQuestions($category_id,$parent_id,$grand_parant_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'qu_th_cat_id'	  => $category_id,
				'qu_sec_cat_id'	  => $parent_id,
				'qu_cat_id'	  => $grand_parant_id,
				'easy'	  	  => "EASY",
				'medium'	  => "MEDIUM",
				'difficult'	  => "DIFFICULT",
				'qu_status'	  => "Y"
		);
	
		/* $result = $db->queryWithParamsArray("
				(SELECT * from questions where qu_th_cat_id =:qu_th_cat_id AND qu_sec_cat_id =:qu_sec_cat_id 
				AND qu_cat_id =:qu_cat_id AND qu_status=:qu_status AND qu_difficulty=:easy ORDER BY RAND() LIMIT 7)
				UNION
				(SELECT * from questions where qu_th_cat_id =:qu_th_cat_id AND qu_sec_cat_id =:qu_sec_cat_id 
				AND qu_cat_id =:qu_cat_id AND qu_status=:qu_status AND qu_difficulty=:medium ORDER BY RAND() LIMIT 2)
				UNION
				(SELECT * from questions where qu_th_cat_id =:qu_th_cat_id AND qu_sec_cat_id =:qu_sec_cat_id 
				AND qu_cat_id =:qu_cat_id AND qu_status=:qu_status AND qu_difficulty=:difficult ORDER BY RAND() LIMIT 1)
				", $array); */
		
		mysql_connect("localhost","root","");
		mysql_select_db("trivia");
		$result = mysql_query("
				(SELECT * from questions where qu_th_cat_id ={$category_id} AND qu_sec_cat_id ={$parent_id} AND qu_cat_id ={$grand_parant_id} AND qu_status='Y' AND qu_difficulty='EASY' ORDER BY RAND() LIMIT 7) 
UNION 
(SELECT * from questions where qu_th_cat_id ={$category_id} AND qu_sec_cat_id ={$parent_id} AND qu_cat_id ={$grand_parant_id} AND qu_status='Y' AND qu_difficulty='MEDIUM' ORDER BY RAND() LIMIT 2)
UNION 
(SELECT * from questions where qu_th_cat_id ={$category_id} AND qu_sec_cat_id ={$parent_id} AND qu_cat_id ={$grand_parant_id} AND qu_status='Y' AND qu_difficulty='DIFFICULT' ORDER BY RAND() LIMIT 1)
				");
		$data = array();
		while($row = mysql_fetch_assoc($result))
		{
			$data[] = $row;
		}
		return $data;
		/*
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE; */
	}
	

	

}

?>