<?php include_once 'head.php'; ?>
<body>
	<?php include_once 'header.php';
	include_once 'controllers/Order.php';
	include_once 'controllers/Currency.php';
	?>

<?php 
if($_SESSION['user_role'] == "**" || $_SESSION['user_role'] == "***")
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
				<!--=== Page Content ===-->
				<div class="row">
					<!--=== General Buttons ===-->
					<div class="col-md-12">
						<div class="widget box">
							<a class="btn btn-primary" href="manage_orders.php">New Orders</a>
							<a class="btn btn-primary" href="confirm_orders.php">Confirm
								Orders</a> <a class="btn btn-primary" href="history_orders.php">Orders
								History</a>
						</div>
					</div>
				</div>
					
				<?php if(isset($_GET["order"])){
					$orderObj = new Order();
					$currencyObj = new Currency();

					$orderInfo = $orderObj->getRestaurantOrder($_GET['order']);
					$orderDetail = $orderObj->getRestaurantOrderDetail($_GET['order']);
					$currencySign = $currencyObj->getDetail();
					?>
				
					<div class="row" id="orderview">
					<!--=== Table For Order ===-->
					<div class="col-md-12">
						<div class="widget box">
							<div class="widget-header">
								<h4>
									<i class="icon-reorder"></i>  #<?=$orderInfo->order_id; ?> Order Detail</h4>
								<div class="toolbar no-padding">
									<div class="btn-group">
										<span class="btn btn-xs widget-collapse"><i
											class="icon-angle-down"></i></span>
									</div>
								</div>
							</div>
							<div class="widget-content no-padding">
								<table
									class="table table-hover table-striped table-bordered table-highlight-head">
									<tbody>
									<?php if($orderInfo->order_type == "serving"){?>
										<tr>
											<td>Table Number</td>
											<td><?=$orderInfo->order_table; ?></td>
										</tr>
										<?php } ?>
										<tr>
											<td>Order Type</td>
											<td><?=$orderInfo->order_type; ?></td>
										</tr>
										<tr>
											<td>Order Date</td>
											<td><?=$orderInfo->order_datetime; ?></td>
										</tr>
											<?php if($orderInfo->order_type == "delivery"){?>									
											<tr>
											<td>Customer Name</td>
											<td><?=$orderInfo->customer_name; ?></td>
										</tr>
										<tr>
											<td>Customer Address</td>
											<td><?=$orderInfo->customer_address; ?></td>
										</tr>
										<tr>
											<td>Customer Phone</td>
											<td><?=$orderInfo->customer_phone; ?></td>
										</tr>
										<?php } ?>
																				<tr>
											<td>Order Detail</td>
											<td>
												<table>
													<thead>
														<th>Dish Name</th>
														<th>Quantity</th>
														<th>Price per unit</th>
														<th>Total</th>
													</thead>
													<tbody>
													<?php foreach ($orderDetail as $detailItem){?>
													<tr>
															<td><?=$detailItem->dish_name; ?></td>
															<td><?=$detailItem->quantity; ?></td>
															<td><?=$detailItem->dish_price; ?><?=" ".$currencySign->currency_sign; ?></td>
															<td><?=($detailItem->quantity*$detailItem->dish_price); ?><?=" ".$currencySign->currency_sign; ?></td>
														</tr>
													<?php } ?>
													<tr>
															<td></td>
															<td></td>
															<td>Grand Total :</td>
															<td><b><?=$orderInfo->order_amount; ?><?=" ".$currencySign->currency_sign; ?> (<?=$orderInfo->order_tax; ?>% TAX Inc.)</b></td>
														</tr>

													</tbody>
												</table>
											</td>
										</tr>
										<tr>
											<td>Estimated Time</td>
											<td><?=$orderInfo->estimated_time; ?> Min</td>
										</tr>
										<tr>
											<td>Customer Instructions</td>
											<td><?=$orderInfo->customer_instruction; ?></td>
										</tr>
										<tr>
											<td>Order Status</td>
											<td><?=$orderInfo->order_status; ?></td>
										</tr>
									</tbody>
								</table>
							</div>
						</div>
					</div>
					<!-- /Table For Order -->
				</div>
				<?php } ?>
				
				
				<!--=== Table ===-->
				<div class="row">
					<div class="col-md-12">
						<div class="widget box">
							<div class="widget-header">
								<h4>
									<i class="icon-reorder"></i> Orders History
								</h4>
								<div class="toolbar no-padding">
									<div class="btn-group">
										<span class="btn btn-xs widget-collapse"><i
											class="icon-angle-down"></i></span>
									</div>
								</div>
							</div>
							<div class="widget-content">
								<table
									class="table table-striped table-bordered table-hover table-checkable datatable-all-order">
									<thead>
										<tr>
											<th>Order No.</th>
											<th>Order Type</th>
											<th>Table No</th>
											<th>Order Date</th>
											<th>Order Status</th>
											<th>Action</th>
										</tr>
									</thead>

								</table>
							</div>
						</div>
					</div>
				</div>
				<!-- /Table -->
			
				
                </div>
			<!-- /Page Content -->
		</div>
		<!-- /.container -->

	</div>
<?php include_once 'js_files.php'; ?>
<?php }
else 
{
	header("Location: index.php");
} ?>
</body>
</html>