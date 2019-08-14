<?php
include_once('Database.php');
class Category{
	
	function insert($cat_name, $cat_image, $cat_status){
		$db = new Database();
		
		$array = array(
				'cat_name'      	=> $cat_name,
				'cat_image'      	=> $cat_image,
				'cat_created_on' 	=> date('Y-m-d'),
				'cat_status'       => $cat_status
		);
	
		
		$stmt = $db->queryWithParamsArray("insert into categories(cat_name,cat_image, cat_status,cat_created_on)  values(:cat_name, :cat_image, :cat_status, :cat_created_on)",$array);
		if($stmt){
			return TRUE;
		}
		else{
			return FALSE;
		}
	}
	
	function update($cat_id,$cat_status){
			$db = new Database();
	
			$array = array(
					'cat_id'		  => $cat_id,
					'cat_status'    => $cat_status
			);
	
	
			$stmt = $db->queryWithParamsArray("UPDATE categories set cat_status=:cat_status WHERE cat_id=:cat_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
	function edit($cat_id, $cat_name, $cat_image, $cat_status){
			$db = new Database();
		
			$array = array(
					'cat_id'		  => $cat_id,
					'cat_name'    => $cat_name,
					'cat_image'    => $cat_image,
					'cat_status'    => $cat_status
			);
		
			$stmt = $db->queryWithParamsArray("UPDATE categories set cat_name=:cat_name, cat_image=:cat_image, cat_status=:cat_status WHERE cat_id=:cat_id ",$array);
			if($stmt){
				return TRUE;
			}
			else{
				return FALSE;
			}
		}
		
		function delete($cat_id) {
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'cat_id' => $cat_id
			);
		
			$result = $db->queryWithParamsArray("DELETE FROM categories where cat_id=:cat_id", $array);
			if($result)
				return TRUE;
			else
				return FALSE;
		}
		
		function getCategoryDetail($cat_id){
			$db = new Database();
			$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		
			$array = array(
					'cat_id'      => $cat_id
			);
		
			$stmt = $db->queryWithParamsArray("select * from categories where cat_id=:cat_id ",$array);
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
				'cat_status'              => "Y"
		);
		
		$result = $db->queryWithParamsArray("SELECT * from categories where cat_status=:cat_status", $array);
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	function isDuplicate($cat_name){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'cat_name'	      => $cat_name
		);
	
		$result = $db->queryWithParamsArray("SELECT * from categories where cat_name=:cat_name", $array);
		if($result->rowCount() > 0 )
			return TRUE;
		else
			return FALSE;
	}
	
}

?>