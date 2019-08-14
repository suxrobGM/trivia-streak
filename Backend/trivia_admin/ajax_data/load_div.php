<?php
include_once ('../controllers/SecondCategory.php');
$category_id = $_POST ['id'];

$secCategoryObj = new SecondCategory();
$secCategories = $secCategoryObj->getCategoriesWithParent($category_id);


if ($secCategories == FALSE) {
	?>
<option value="0">None</option>
<?php

} else {
	?>
<option value="0">None</option>	
	<?php 
	foreach ( $secCategories as $secCategory ) {
		?>
<option value="<?=$secCategory->sec_cat_id; ?>"><?=$secCategory->sec_cat_name; ?></option>
<?php
	}
}
?> 
