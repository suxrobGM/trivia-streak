<!--=== Page Header ===-->
 <?php
        $full_name = $_SERVER['PHP_SELF'];
        $name_array = explode('/',$full_name);
        $count = count($name_array);
        $page_name = $name_array[$count-1];
    ?>
				<div class="page-header">
					<div class="page-title">
						<h3>
						<?php 
							if($page_name=='manage_categories.php')
							{
								echo "Manage Categories";	
							} 
							if($page_name=='manage_items.php')
							{
								echo "Manage Items";
							}
							if($page_name=='manage_options.php')
							{
								echo "Manage Item Options";
							}
							if($page_name=='order_range.php')
							{
								echo "Minimum Order Range";
							}
						?>
						</h3>
						<span>Welcome, <?=$_SESSION['admin_user']; ?>!</span>
					</div>
					
					<?php if(isset($_SESSION['admin_id']) ){ ?>
					<!-- Page Stats -->
					<ul class="page-stats" id="page-stats-notificationCount">
						
					</ul>
					<!-- /Page Stats -->
					<?php } ?>
				</div>
				<!-- /Page Header -->