<?php 
session_start();
include_once('../controllers/Category.php');
include_once('../controllers/SecondCategory.php');
include_once('../controllers/ThirdCategory.php');
include_once('../controllers/Questions.php');
include_once('../controllers/Prize.php');

$id = $_POST['id'];
$action = $_POST['action'];

switch ($action){
	
	case 'btn-category-delete':
			$categoryObj = new Category();
			$data = $categoryObj->getCategoryDetail($id);
			$res = $categoryObj->delete($id);
			if($res){
				$path = "../images/1st_level_categories/".$data->cat_image;
				unlink($path);
			}
			break;
			
	case 'btn-sec-category-delete':
		$SecCategoryObj = new SecondCategory();
		$data = $SecCategoryObj->getCategoryDetail($id);
		$res = $SecCategoryObj->delete($id);
		if($res){
			$path = "../images/2nd_level_categories/".$data->sec_cat_image;
			unlink($path);
		}
		break;
	case 'btn-th-category-delete':
		$ThCategoryObj = new ThirdCategory();
		$data = $ThCategoryObj->getCategoryDetail($id);
		$res = $ThCategoryObj->delete($id);
		if($res){
			$path = "../images/3rd_level_categories/".$data->th_cat_image;
			unlink($path);
		}
		break;
	case 'btn-prize-delete':
		$prizeObj = new Prize();
		$data = $prizeObj->getPrizeDetail($id);
		$res = $prizeObj->delete($id);
		if($res){
			$path = "../images/prize/".$data->prize_image;
			unlink($path);
		}
		break;
	case 'btn-question-delete':
			$quObj = new Questions();
			$res = $quObj->delete($id);
			break;
	
	
}
?>