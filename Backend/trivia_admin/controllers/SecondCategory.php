<?php
include_once('Database.php');
class SecondCategory{
	
	function insert($sec_cat_name, $sec_cat_image, $sec_cat_status, $sec_cat_parent){
		$db = new Database();
		
		$array = array(
				'sec_cat_name'      	=> $sec_cat_name,
				'sec_cat_image'      	=> $sec_cat_image,
				'sec_created_on' 	    => date('Y-m-d'),
				'sec_cat_status'       => $sec_cat_status,
				'sec_cat_parent'       => $sec_cat_parent
		);
	
		
		$stmt = $db->queryWithParamsArray("insert into second_categories(sec_cat_name,sec_cat_image, sec_cat_status,sec_created_on,sec_cat_parent)  values(:sec_cat_name, :sec_cat_image, :sec_cat_status, :sec_created_on, :sec_cat_parent)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	
	function update($sec_cat_id,$sec_cat_status){
			$db = new Database();
	
			$array = array(
					'sec_cat_id'		  => $sec_cat_id,
					'sec_cat_status'    => $sec_cat_status
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE second_categories set sec_cat_status=:sec_cat_status WHERE sec_cat_id=:sec_cat_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
	
		
		function delete($sec_cat_id) {
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'sec_cat_id' => $sec_cat_id
			);
		
			$result = $db->queryWithParamsArray("DELETE FROM second_categories where sec_cat_id=:sec_cat_id", $array);
			if($result)
				return TRUE;
			else
				return FALSE;
		}
		
		function getCategoryDetail($sec_cat_id){
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'sec_cat_id'      => $sec_cat_id
			);
		
			$stmt = $db->queryWithParamsArray("select * from second_categories where sec_cat_id=:sec_cat_id ",$array);
			if($stmt){
				return $stmt->fetch();
			}
			else{
				return FALSE;
			}
		}
	
	function getAllCategories(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'sec_cat_status'              => "Y"
		);
		
		$result = $db->queryWithParamsArray("SELECT * from second_categories where sec_cat_status=:sec_cat_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	function getCategoriesWithParent($parent_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'sec_cat_parent'              => $parent_id,
				'sec_cat_status'              => "Y"
		);
	
		$result = $db->queryWithParamsArray("SELECT * from second_categories where sec_cat_parent =:sec_cat_parent AND sec_cat_status=:sec_cat_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	function isDuplicate($sec_cat_name){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'sec_cat_name'	      => $sec_cat_name
		);
	
		$result = $db->queryWithParamsArray("SELECT * from second_categories where sec_cat_name=:sec_cat_name", $array);
		if($result->rowCount() > 0 )
			return TRUE;
		else
			return FALSE;
	}
	
}

?>