<?php
include_once('Database.php');
class Order{

	function getNewOrderCount(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'restaurant_user_id'	      => $_SESSION['user_id'],
				'order_status'              => "new"
		);
	
		$result = $db->queryWithParamsArray("SELECT * from  order_main where restaurant_user_id =:restaurant_user_id  AND order_status=:order_status", $array);
		if($result->rowCount() > 0 )
			return $result->rowCount();
		else
			return 0;
	}
	
	function getTotalOrderCount(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'restaurant_user_id'	      => $_SESSION['user_id'],
		);
	
		$result = $db->queryWithParamsArray("SELECT * from  order_main where restaurant_user_id =:restaurant_user_id", $array);
		if($result->rowCount() > 0 )
			return $result->rowCount();
		else
			return 0;
	}
	
	function getNewServingOrderCount(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'restaurant_user_id'	      => $_SESSION['user_id'],
				'order_type'				=> "serving",
				'order_status'              => "new"
		);
		
		$result = $db->queryWithParamsArray("SELECT * from  order_main where restaurant_user_id =:restaurant_user_id  AND order_status=:order_status AND order_type=:order_type", $array);
		if($result->rowCount() > 0 )
			return $result->rowCount();
		else
			return 0;
	}
	function getNewDeliveryOrderCount(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'restaurant_user_id'	      => $_SESSION['user_id'],
				'order_type'				=> "delivery",
				'order_status'              => "new"
		);
	
		$result = $db->queryWithParamsArray("SELECT * from  order_main where restaurant_user_id =:restaurant_user_id  AND order_status=:order_status AND order_type=:order_type", $array);
		if($result->rowCount() > 0 )
			return $result->rowCount();
		else
			return 0;
	}
	
	
	
	function getRestaurantOrder($restaurant_order_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'restaurant_order_id'   => $restaurant_order_id
	
		);
	
		$result = $db->queryWithParamsArray("SELECT * from order_main where order_id =:restaurant_order_id", $array);
	
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
	
	function getRestaurantOrderDetail($restaurant_order_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'restaurant_order_id'   => $restaurant_order_id
		);
	
		$result = $db->queryWithParamsArray("SELECT order_detail.quantity, dish.dish_name, dish.dish_price from order_detail inner join dish on order_detail.restaurant_dish_id = dish.dish_id
				where order_detail.restaurant_order_id =:restaurant_order_id", $array);
	
		if($result->rowCount() > 0 )
			return $result->fetchAll();
		else
			return FALSE;
	}
	
	function UpdateOrder($restaurant_order_id, $order_status){
		$db = new Database();

		$array = array(
				'restaurant_order_id'   => $restaurant_order_id,
				'order_status' => $order_status
		);
	
		$result = $db->queryWithParamsArray("UPDATE order_main set order_status=:order_status WHERE order_id=:restaurant_order_id", $array);
	
		if($result)
			return TRUE;
		else
			return FALSE;
	}
	
	function getRestaurantTodayOrder(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'order_datetime'   => date('Y-m-d'),
				'order_status' => 'completed',
				'restaurant_user_id'	      => $_SESSION['user_id']
		);
		$result = $db->queryWithParamsArray("SELECT sum(order_amount) as total_amount from order_main where restaurant_user_id =:restaurant_user_id AND order_datetime like concat('%', :order_datetime, '%') AND order_status=:order_status", $array);
	
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
	function getRestaurantTotalOrder(){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
		$array = array(
				'order_status' => 'completed',
				'restaurant_user_id'	      => $_SESSION['user_id']
		);
		$result = $db->queryWithParamsArray("SELECT sum(order_amount) as total_amount from order_main where restaurant_user_id =:restaurant_user_id AND order_status=:order_status", $array);
	
		if($result->rowCount() > 0 )
			return $result->fetch();
		else
			return FALSE;
	}
	
}

?>