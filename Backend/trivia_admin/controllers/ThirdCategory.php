<?php
include_once('Database.php');
class ThirdCategory{
	
	function insert($th_cat_name, $th_cat_image, $th_cat_status, $th_cat_parent, $th_cat_grand_parent){
		$db = new Database();
		
		$array = array(
				'th_cat_name'      	=> $th_cat_name,
				'th_cat_image'      	=> $th_cat_image,
				'th_created_on' 	    => date('Y-m-d'),
				'th_cat_status'       => $th_cat_status,
				'th_cat_parent'       => $th_cat_parent,
				'th_cat_grand_parent'       => $th_cat_grand_parent
		);
	
		
		$stmt = $db->queryWithParamsArray("insert into third_categories(th_cat_name,th_cat_image, th_cat_status,th_created_on,th_cat_parent, th_cat_grand_parent)  values(:th_cat_name, :th_cat_image, :th_cat_status, :th_created_on, :th_cat_parent, :th_cat_grand_parent)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	
	function update($th_cat_id,$th_cat_status){
			$db = new Database();
	
			$array = array(
					'th_cat_id'		  => $th_cat_id,
					'th_cat_status'    => $th_cat_status
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE third_categories set th_cat_status=:th_cat_status WHERE th_cat_id=:th_cat_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
	
		
		function delete($th_cat_id) {
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'th_cat_id' => $th_cat_id
			);
		
			$result = $db->queryWithParamsArray("DELETE FROM third_categories where th_cat_id=:th_cat_id", $array);
			if($result)
				return TRUE;
			else
				return FALSE;
		}
		
		function getCategoryDetail($th_cat_id){
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'th_cat_id'      => $th_cat_id
			);
		
			$stmt = $db->queryWithParamsArray("select * from third_categories where th_cat_id=:th_cat_id ",$array);
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
				'th_cat_status'              => "Y"
		);
		
		$result = $db->queryWithParamsArray("SELECT * from third_categories where th_cat_status=:th_cat_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	function getCategoriesWithParent($parent_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'th_cat_parent'              => $parent_id,
				'th_cat_status'              => "Y"
		);
	
		$result = $db->queryWithParamsArray("SELECT * from third_categories where th_cat_parent =:th_cat_parent AND th_cat_status=:th_cat_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	
	function isDuplicate($th_cat_name){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'th_cat_name'	      => $th_cat_name
		);
	
		$result = $db->queryWithParamsArray("SELECT * from third_categories where th_cat_name=:th_cat_name", $array);
		if($result->rowCount() > 0 )
			return TRUE;
		else
			return FALSE;
	}
	
}

?>