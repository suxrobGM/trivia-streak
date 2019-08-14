<?php
include_once('Database.php');
class Login {
	
	function authenticate($admin_user = null, $admin_pass = null){
		
	$db = new Database ();
	$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
	$array = array(
			'admin_user' => $admin_user,
			'admin_pass'  => $admin_pass
	);
	
	$stmt = $db->queryWithParamsArray("SELECT * from admin WHERE admin_user=:admin_user AND 
			admin_pass=:admin_pass",$array);
		if($stmt->rowCount() == 1)
		{
			$row = $stmt->fetch();
			$_SESSION['admin_user'] = $row->admin_user;
			$_SESSION['admin_id'] = $row->admin_id;
				
			return TRUE;
		}
		else
		{
			return FALSE;
		}
	
	} //FUNCTION AUTHENTICATE
	
	
	
	
	function sendEmail($to, $from, $subject, $message){
			
		$headers = "From: ". $from ." \n";
		$headers .= "MIME-Version: 1.0\n";
		$headers .= "Content-type: text/html; charset=iso-8859-1\n";
		$headers .= "Reply-To: me <". $from .">\n";
		$headers .= "X-Priority: 1\n";
		$headers .= "X-MSMail-Priority: High\n";
		$headers .= "X-Mailer: My mailer";
			
		mail($to,$subject,$message,$headers);
	}
	
	function getRestaurantDetail($user_id){
		$db = new Database();
		$db->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
	
		$array = array(
				'user_id'      => $user_id
		);
	
		$stmt = $db->queryWithParamsArray("select * from restaurant_user where user_id=:user_id ",$array);
		if($stmt){
			return $stmt->fetch();
		}
		else{
			return FALSE;
		}
	}
	
}

?>