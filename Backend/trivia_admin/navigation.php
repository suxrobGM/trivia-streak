<!--=== Navigation ===-->
 <?php
        $full_name = $_SERVER['PHP_SELF'];
        $name_array = explode('/',$full_name);
        $count = count($name_array);
        $page_name = $name_array[$count-1];
    ?>
    			<?php 
    			if(isset($_SESSION['admin_id']))
    			{
    			?>
				<ul id="nav">
					<li class="<?php echo ($page_name=='manage_categories.php')?'current':'';?>">
						<a href="manage_categories.php">
							<i class="icon-desktop"></i>
							Manage Categories
						</a>
					</li>
				<!--	<li class="<?php echo ($page_name=='manage_sec_categories.php')?'current':'';?>">
						<a href="manage_sec_categories.php">
							<i class="icon-desktop"></i>
							Manage Second Categories
						</a>
					</li>
					<li class="<?php echo ($page_name=='manage_th_categories.php')?'current':'';?>">
						<a href="manage_th_categories.php">
							<i class="icon-desktop"></i>
							Manage Third Categories
						</a>
					</li>-->
					<li class="<?php echo ($page_name=='manage_questions.php')?'current':'';?>">
						<a href="manage_questions.php">
							<i class="icon-table"></i>
							Manage Questions
						</a>
					</li>
					<li class="<?php echo ($page_name=='manage_prizes.php')?'current':'';?>">
						<a href="manage_prizes.php">
							<i class="icon-table"></i>
							Manage Prizes
						</a>
					</li>
					<li>
						<a href="action/logout.php">
							<i class="icon-table"></i>
							Logout
						</a>
					</li>
				</ul>
				<?php 
    			}
    			?>
				<!-- /Navigation -->