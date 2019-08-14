<?php 
include_once '../controllers/Category.php';
include_once '../controllers/SecondCategory.php';
include_once '../controllers/ThirdCategory.php';
include_once '../controllers/Questions.php';
include_once '../controllers/Prize.php';


$id = $_POST['id'];
$value = $_POST['value'];
$Row = $_POST['tableRow'];
$action = $_POST['action'];


switch ($action){
	
		case 'btn-category-update':
				$categoryObj = new Category();
				
				$categoryObj->update($id, $value);
				if($value == "N")
				{
					echo '<a href="javascript:void(0);" title="Enable" row="'.$id.'" value="Y" countTableRow="'.$Row.'" ><span class="label label-danger">Disable</span></a>';
				}else if($value == "Y"){
					echo '<a href="javascript:void(0);" title="Disable" row="'.$id.'" value="N" countTableRow="'.$Row.'" ><span class="label label-success">Enable</span></a>';
				}
				break;
		case 'btn-sec-category-update':
			$secCategoryObj = new SecondCategory();
		
			$secCategoryObj->update($id, $value);
			if($value == "N")
			{
				echo '<a href="javascript:void(0);" title="Enable" row="'.$id.'" value="Y" countTableRow="'.$Row.'" ><span class="label label-danger">Disable</span></a>';
			}else if($value == "Y"){
				echo '<a href="javascript:void(0);" title="Disable" row="'.$id.'" value="N" countTableRow="'.$Row.'" ><span class="label label-success">Enable</span></a>';
			}
			break;
		case 'btn-th-category-update':
			$thCategoryObj = new ThirdCategory();
		
			$thCategoryObj->update($id, $value);
			if($value == "N")
			{
				echo '<a href="javascript:void(0);" title="Enable" row="'.$id.'" value="Y" countTableRow="'.$Row.'" ><span class="label label-danger">Disable</span></a>';
			}else if($value == "Y"){
				echo '<a href="javascript:void(0);" title="Disable" row="'.$id.'" value="N" countTableRow="'.$Row.'" ><span class="label label-success">Enable</span></a>';
			}
			break;
		
		case 'btn-question-update':
				$quObj = new Questions();
			
				$quObj->update($id, $value);
				if($value == "N")
				{
					echo '<a href="javascript:void(0);" title="Enable" row="'.$id.'" value="Y" countTableRow="'.$Row.'" ><span class="label label-danger">Disable</span></a>';
				}else if($value == "Y"){
					echo '<a href="javascript:void(0);" title="Disable" row="'.$id.'" value="N" countTableRow="'.$Row.'" ><span class="label label-success">Enable</span></a>';
				}
				break;
		case 'btn-prize-update':
			$prizeObj = new Prize();
				
			$prizeObj->update($id, $value);
			if($value == "N")
			{
				echo '<a href="javascript:void(0);" title="Enable" row="'.$id.'" value="Y" countTableRow="'.$Row.'" ><span class="label label-danger">Disable</span></a>';
			}else if($value == "Y"){
				echo '<a href="javascript:void(0);" title="Disable" row="'.$id.'" value="N" countTableRow="'.$Row.'" ><span class="label label-success">Enable</span></a>';
			}
			break;
	
	
}
?>