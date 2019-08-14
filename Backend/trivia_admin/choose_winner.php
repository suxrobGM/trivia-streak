<?php include_once 'head.php'; ?>
<body>
	<?php include_once 'header.php';
		include_once 'controllers/Prize.php';
		$prizeObj = new Prize();
	?>
<?php 
$flag = "";
if(isset($_GET['action']))
{
	if($_GET['action'] == "success")
	{
		$flag = "success";
	}
	else if($_GET['action'] == "failed")
	{
		$flag = "failed";
	}else if($_GET['action'] == "duplicate")
	{
		$flag = "duplicate";
	}else if($_GET['action'] == "update_success")
	{
		$flag = "update_success";
	}
	else if($_GET['action'] == "update_failed")
	{
		$flag = "update_failed";
	}
}
?>
<?php 
if(isset($_SESSION['admin_id']) )
{
?>
	<div id="container">
	
		<div id="sidebar" class="sidebar-fixed">
			<div id="sidebar-content">
				<?php include_once 'navigation.php'; ?>
			</div>
		</div>
		<!-- /Sidebar -->

		<div id="content">
			<div class="container">

			<?php include_once 'page_header.php'; ?>
			<?php 
            if($flag == "success")
            {
            	?>
            	<div class="alert fade in alert-success">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Prize Added.
				</div>
            	<?php 
            }
            else if($flag == "failed")
            {
            	?>
            	<div class="alert fade in alert-danger">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Prize Couldnot Added.
				</div>
                <?php 
            }else if($flag == "update_success")
            {
            	?>
            	<div class="alert fade in alert-success">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Prize Updated.
				</div>
            	<?php 
            }
            else if($flag == "update_failed")
            {
            	?>
            	<div class="alert fade in alert-danger">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Prize Couldnot Updated.
				</div>
                <?php 
            }
            ?>
				<!--=== Page Content ===-->
			
		<?php 
		if($prizeObj->checkPrizeWinner($_GET['prize_id'])){
			echo "Game Winner Already Added";
			?>
			<br/>
			<b>Winner Name : <b> <?=$prizeObj->checkPrizeWinner($_GET['prize_id'])->game_user_id; ?>
			<br/>
			<b>Winner Email : <b> <td><?=$prizeObj->getUserEmail($prizeObj->checkPrizeWinner($_GET['prize_id'])->game_user_id)->user_email; ?></td>
			<br/>
			<b>Winner Points : <b> <?=$prizeObj->checkPrizeWinner($_GET['prize_id'])->winner_points; ?>
			<?php
		}else{
			$getList = $prizeObj->getAllUserForPrize($_GET['prize_id']);
		?>
				<!--=== Normal ===-->
				<div class="row">
					<div class="col-md-12">
						<div class="widget box">
							<div class="widget-header">
								<h4><i class="icon-reorder"></i> Choose Winner</h4>
								<div class="toolbar no-padding">
									<div class="btn-group">
										<span class="btn btn-xs widget-collapse"><i class="icon-angle-down"></i></span>
									</div>
								</div>
							</div>
							<div class="widget-content">
								<table class="table table-striped table-bordered table-hover table-checkable">
									<thead>
										<tr>
											<th>User ID</th>
											<th>Email</th>
											<th>Points</th>
											<th>Action</th>
										</tr>
									</thead>
									<?php 
									if($getList){
									foreach($getList as $item){
									?>
									<tr>
										<td><?=$item->game_user_id; ?></td>
										<td><?=$prizeObj->getUserEmail($item->game_user_id)->user_email; ?></td>
										<td><?=$item->points; ?></td>
										<td><span class="btn-group"><a href="action/choose_winner.php?prize_id=<?=$item->prize_id; ?>&game_user_id=<?=$item->game_user_id; ?>&game_user_type=<?=$item->game_user_type; ?>&winner_points=<?=$item->points; ?>" title="Choose Winner" class="btn btn-s bs-tooltip btn-assign-prize"><i class="icon-ok"></i></a></span></td>
									</tr>
									<?php }} ?>
								</table>
							</div>
						</div>
					</div>
				</div>
				
				<?php } ?>
				<!-- /Normal -->
				
                </div>
				<!-- /Page Content -->
				
			
			</div>
			<!-- /.container -->

		</div>
	</div>
<?php include_once 'js_files.php'; ?>
<?php }
else 
{
	header("Location: index.php");
} ?>
</body>
</html>