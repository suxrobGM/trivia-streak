<?php
include_once ('../controllers/ThirdCategory.php');
$category_id = $_POST ['id'];

$thCategoryObj = new ThirdCategory();
$thCategories = $thCategoryObj->getCategoriesWithParent($category_id);


if ($thCategories == FALSE) {
	?>
<option value="0">None</option>
<?php

} else {
	?>
	<option value="0">None</option>
	<?php 
	foreach ( $thCategories as $thCategory ) {
		?>
<option value="<?=$thCategory->th_cat_id; ?>"><?=$thCategory->th_cat_name; ?></option>
<?php
	}
}
?> 
