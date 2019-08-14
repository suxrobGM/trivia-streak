var varNotify=setInterval(function(){checkNotification()},15000);

function checkNotification(id,file,refreshId)
{
	$.ajax(
	        {
	            url : "service/serviceEngine.php",
	            type: "POST",
	            success:function(data, textStatus, jqXHR)
	            {
	            	var topCount = null;
	            	var result = eval ("(" + data + ")");
	            	var totalCount = result.servingOrderCount+result.deliveryOrderCount+result.feedbackCount+result.callwaiterCount+result.reservationCount;
	            	if(totalCount > 0)
	            	{
	            		topCount = '<span class="badge">'+totalCount+'</span></a>';
	            	}
	            	else{
	            		topCount = '';
	            	}
	            	$("#notificationCount").html('<a href="#" class="dropdown-toggle" data-toggle="dropdown">'+
						'<i class="icon-bullhorn"></i>'+
						topCount+
					'<ul class="dropdown-menu extended notification">'+
						'<li class="title">'+
							'<p>You have '+totalCount+' new notifications</p>'+
						'</li>'+
						'<li>'+
							'<a href="manage_orders.php">'+
								'<span class="label label-success"><i class="icon-table"></i></span>'+
								'<span class="message">New Serving Order.</span>'+
								'<span class="time">'+result.servingOrderCount+'</span>'+
							'</a>'+
						'</li>'+
						'<li>'+
							'<a href="manage_orders.php">'+
								'<span class="label label-success"><i class="icon-truck"></i></span>'+
								'<span class="message">New Delivery Order.</span>'+
								'<span class="time">'+result.deliveryOrderCount+'</span>'+
							'</a>'+
						'</li>'+
						'<li>'+
						'<a href="manage_reservation.php">'+
							'<span class="label label-success"><i class="icon-bookmark"></i></span>'+
							'<span class="message">Reservation.</span>'+
							'<span class="time">'+result.reservationCount+'</span>'+
						'</a>'+
					'</li>'+
						'<li>'+
						'<a href="manage_feedbacks.php">'+
							'<span class="label label-success"><i class="icon-globe"></i></span>'+
							'<span class="message">Feedback.</span>'+
							'<span class="time">'+result.feedbackCount+'</span>'+
						'</a>'+
					'</li>'+
					'<li>'+
					'<a href="waiter_calls.php">'+
						'<span class="label label-success"><i class="icon-phone"></i></span>'+
						'<span class="message">Call Waiter.</span>'+
						'<span class="time">'+result.callwaiterCount+'</span>'+
					'</a>'+
				'</li>'+
						'</ul>'); 
	            	
	            	$("#page-stats-notificationCount").html('<li><div class="summary">'+
							'<span>New orders</span>'+
							'<h3>'+result.totalOrders+'</h3>'+
						'</div>'+
					'</li>'+
					'<li>'+
					'<div class="summary">'+
						'<span>New Reservations</span>'+
						'<h3>'+result.reservationCount+'</h3>'+
					'</div>'+
				'</li>'+
					'<li>'+
						'<div class="summary">'+
							'<span>New Calls</span>'+
							'<h3>'+result.callwaiterCount+'</h3>'+
						'</div>'+
					'</li>');
	            	
	            	$("#show-table-availability").html(result.tableAvailability);
	            },
	            error: function(jqXHR, textStatus, errorThrown)
	            {
	                //if fails     
	            }
	        });
	
}

checkNotification();

