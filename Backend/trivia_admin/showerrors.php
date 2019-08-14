<?php 
include_once 'controllers/Category.php';
include_once 'controllers/Dish.php';
include_once 'controllers/Table.php';
include_once 'controllers/Unit.php';
include_once 'controllers/Currency.php';
include_once 'controllers/Tax.php';

$categoryObj = new Category();
$dishObj = new Dish();
$tableObj = new Table();
$unitObj = new Unit();
$currencyObj = new Currency();
$taxObj = new Tax();


				if(!$categoryObj->getAllCategories()){ ?>
					<a href="manage_categories.php" style="text-decoration: none;">
						<div class="alert fade in alert-danger">
						Please add atleast one category to your menu.
						</div></a>
					<?php } ?>
					
					<?php if(!$dishObj->getAllDishes()){ ?>
					<a href="manage_dishes.php" style="text-decoration: none;">
					<div class="alert fade in alert-danger">
						Please add atleast one dish to your menu.
						</div></a>
					<?php } ?>
					
					<?php if(!$tableObj->getAllTables()){ ?>
					<a href="table_setting.php" style="text-decoration: none;">
					<div class="alert fade in alert-danger">
						Please add atleast one Table to your restaurant.
						</div></a>
					<?php } ?>
					
					<?php if(!$unitObj->getAllUnits()){ ?>
					<a href="unit_setting.php" style="text-decoration: none;">
					<div class="alert fade in alert-danger">
						Please add atleast one unit to your menu to add dish.
						</div></a>
					<?php } ?>
					
					<?php if(!$currencyObj->getDetail()){ ?>
					<a href="currency_setting.php" style="text-decoration: none;">
					<div class="alert fade in alert-danger">
						Please set your curency symbol.
						</div></a>
					<?php } ?>
					
					<?php if(!$taxObj->getDetail()){ ?>
					<a href="tax_setting.php" style="text-decoration: none;">
					<div class="alert fade in alert-danger">
						Please set order Tax persent.
						</div></a>
					<?php } ?>