<?php
include_once('Database.php');
class Categories{
	
	function getFirstLevelCategories(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'cat_status'	  => "Y"
		); 
		
		$result = $db->queryWithParamsArray("SELECT * from categories 
				where cat_status=:cat_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
		function checkFirstLevelCategoryValid($cat_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'qu_cat_id' => 0,
				'qu_sec_cat_id' => 0,
				'qu_th_cat_id'	  => $cat_id
		); 
		
		$result = $db->queryWithParamsArray("SELECT * from questions where qu_cat_id=:qu_cat_id AND qu_sec_cat_id=:qu_sec_cat_id AND qu_th_cat_id=:qu_th_cat_id ", $array);
		if($result->rowCount() > 7 )
			return True;
		else
			return FALSE;
	}
	
	
	function getSecondLevelCategories($parent_category_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'sec_cat_parent'	      => $parent_category_id,
				'sec_cat_status'	  => "Y"
		);
	
		$result = $db->queryWithParamsArray("SELECT * from second_categories 
				where sec_cat_parent =:sec_cat_parent AND sec_cat_status=:sec_cat_status", $array);
		
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	function getThirdLevelCategories($parent_category_id, $grand_parent_category_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'th_cat_parent'	      => $parent_category_id,
				'th_cat_grand_parent'	      => $grand_parent_category_id,
				'th_cat_status'	  => "Y"
		);
	
		$result = $db->queryWithParamsArray("SELECT * from third_categories
				where th_cat_parent =:th_cat_parent AND th_cat_grand_parent =:th_cat_grand_parent 
				AND th_cat_status=:th_cat_status", $array);
	
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	

}

?>