<?php
session_start();
include_once '../controllers/Login.php';
$restaurantObj = new Login();
$restaurantDetail = $restaurantObj->getRestaurantDetail($_SESSION['user_id']);

$qr = base64_decode($_GET['qr']);
$des = base64_decode($_GET['des']);

/*
 * PHP QR Code encoder
 *
 * Exemplatory usage
 *
 * PHP QR Code is distributed under LGPL 3
 * Copyright (C) 2010 Dominik Dzienia <deltalab at poczta dot fm>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA
 */
    
    
   
    include "qrlib.php";    
    
	QRcode::png($qr, 'test.png', 'H', 10, 2);
	?>
	<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Qr Code</title>
<style>
.inv 
{
	width: 300px;
	border:1px solid #ccc;
	background-color:#eee;
	padding: 10px;
}
#logo{float: left;}
.head1{ text-align: center; margin-bottom: 20px;}
.branch{ text-align: center; margin-bottom:20px;}
.data{text-align: center; font-weight: bold; }
.foot_inv{ margin-top:20px; text-align: center;}
</style>
</head>

<body onload="window.print();">

<div class="inv">
<div id="logo"><img src="../images/<?php echo $restaurantDetail->user_name; ?>/<?php echo $restaurantDetail->user_image; ?>" alt="<?php echo $restaurantDetail->user_name; ?>" width="64px;" height="64px;" style="margin-bottom:10px;" /></div> 
<div class="head1"><label style="font-size: 20px; font-weight: bold;" ><?php echo $restaurantDetail->restaurant_name; ?></label>
<br />
<?php echo $restaurantDetail->user_address; ?>
</div>
<div class="branch">
<?php
	echo '<img src="test.png" />';  
 ?>
</div>
<div class="data"><?=$des; ?></div>

<hr/>
  <div class="foot_inv">Thanks for your Corporation. © Obigarson</div>
</div>
</body>
</html>
	
	

    