<?php
session_start();
// array for JSON response
$response = array();

// include db connect class
include_once '../controllers/Order.php';
include_once '../controllers/Feedback.php';
include_once '../controllers/CallWaiter.php';
include_once '../controllers/Reservation.php';
include_once '../controllers/Table.php';

		$orderObj = new Order();
		$feedbackObj = new Feedback();
		$callwaiterObj = new CallWaiter();
		$reservationObj = new Reservation();
		$tableObj = new Table();

		$servingOrderCount = $orderObj->getNewServingOrderCount();
		$deliveryOrderCount = $orderObj->getNewDeliveryOrderCount();
		$feedbackCount = $feedbackObj->getFeedbackCount();
		$callwaiterCount = $callwaiterObj->getCallCount();
		$reservationCount = $reservationObj->getNewReservationCount();
		$tableAvailability = $tableObj->getTablesAvailability();
		
		$totalOrders = $orderObj->getNewOrderCount();
		
		$response["servingOrderCount"] = $servingOrderCount;
		$response["deliveryOrderCount"] = $deliveryOrderCount;
		$response["feedbackCount"] = $feedbackCount;
		$response["callwaiterCount"] = $callwaiterCount;
		$response["reservationCount"] = $reservationCount;
		
		$response["totalOrders"] = $totalOrders;
		$tableStr = null;
		foreach ($tableAvailability as $tableitem){
			$tableStr .= '<div class="col-md-3">';
			$tableStr .= '<div class="widget box">';
			$tableStr .= '<div class="widget-header">';
			$tableStr .= '<h4><i class="icon-reorder"></i> Table #'.$tableitem->table_no.' ('.$tableitem->table_description.')'.'</h4>';
			$tableStr .= '</div> <div class="widget-content">';
			if($tableitem->aval_status == "on"){
				$tableStr .= '<p style="height: 80px; background-color: red;"></p>';
			}else{
				$tableStr .= '<p style="height: 80px; background-color: green;"></p>';
			}
			
			//$tableStr .= '<a class="more" href="javascript:void(0);">See Details <i class="pull-right icon-angle-right"></i></a>';
			$tableStr .= '</div></div></div>';
		}
		
		$response["tableAvailability"] = $tableStr;
		echo json_encode($response);

?>