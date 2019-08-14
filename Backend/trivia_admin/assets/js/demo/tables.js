/*
 * general_ui.js
 *
 * Demo JavaScript used on General UI-page.
 */

"use strict";

$(document).ready(function(){
	
    
    // ------------------------------------------------------------------------------------------------
    // ------------------------------------ Resturant Admin Panel -------------------------------------
    
 // **##**##**##**##**##**##**##**##**##**## Category Management   **##**##**##**##**##**##**##**##**##**##**##

    // Category Datatable
       $(".datatable-category").dataTable({
   		 "bProcessing": true,
   			"bServerSide": true,
   			"sAjaxSource": "gridmodel/category_grid.php",
   			"aoColumnDefs": [
   			 				{ 'bSortable': false, 'aTargets': [0,5] }
   			 			],
   			"aoColumns": [
   			              null,
   			              null,
   			              null,
   			              null,
   			              null,
   			              {
   			                "mData": null,
   			                "sDefaultContent": "Action"
   			              }
   			            ],
   			  "fnRowCallback": function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {
       		      // Bold the grade for all 'A' grade browsers
   				$('td:eq(0)', nRow).html( iDisplayIndex+1 );
   				//
   				$('td:eq(5)', nRow).html( '<span class="btn-group"><a href="javascript:void(0);" value="'+aData[0]+'" title="Edit Category" class="btn btn-s bs-tooltip btn-category-edit"><i class="icon-edit"></i></a><a href="javascript:void(0);" value="'+aData[0]+'" title="Delete Category" class="btn btn-s bs-tooltip btn-delete-category"><i class="icon-trash"></i></a></span>');
   			  }
       });
    
       
       // ***************************************  Update Status Category  ****************************************
       $(document).on('click', '.category_status_disable_enable', function() {
       	var v = $( this ).find('a').attr( 'value' );
       	var i = $( this ).find('a').attr( 'row' );
       	var countTableRow = $( this ).find('a').attr( 'countTableRow' );

       	$.ajax(
                   {
                       url : "ajax_data/update_action.php",
                       type: "POST",
                       data : {id: i,value: v, tableRow: countTableRow, action: 'btn-category-update'},
                       success:function(data, textStatus, jqXHR)
                       {
                       	var item1 = $( ".category_restaurantStatus"+countTableRow );
                       	$( item1 ).html( data );
                       },
                       error: function(jqXHR, textStatus, errorThrown)
                       {
                           //if fails     
                       }
                   });
       	
       });
       
       // *************************************** END Update Status Category  ****************************************
       
       // ***************************************  Edit Category  ****************************************
      
         $("#category-edit").dialog({
            autoOpen: false,
            title: "Edit Category",
            modal: true,
            width: "700",
            buttons: [{
                text: "Update",
                click: function () {
                    $(this).find('form#validate-3').submit();
                }
            },{
                text: "Close",
                click: function () {
                    $(this).dialog("close");
                }
            }]
         });

         $(document).on('click', '.btn-category-edit', function() {
         	var value = $( this ).attr( 'value' );
         	$.ajax(
            {
                url : "ajax_data/edit_category.php",
                type: "POST",
                data : {id: value},
                success:function(data, textStatus, jqXHR)
                {
                	$('.mws-dialog-inner-category-edit').html(data);
                },
                error: function(jqXHR, textStatus, errorThrown)
                {
                    //if fails     
                }
            });
         	
         	$("#category-edit").dialog("option", {
                modal: false
            }).dialog("open");
            //event.preventDefault();
         });

     //  ******************************************* END Edit Category ***********************************************    
       
    // ***************************************  Delete Category  ****************************************
       $(document).on('click', '.btn-delete-category', function() {
       	var value = $( this ).attr( 'value' );
       	$('.mws-dialog-inner-category-delete').css('display','block');
       	
           $("#category-delete").dialog({
               autoOpen: false,
               title: "Delete Category",
               modal: true,
               width: "600",
               buttons: [{
                   text: "Delete",
                   click: function () {
                   	$.ajax(
                       {
                           url : "ajax_data/delete_action.php",
                           type: "POST",
                           data : {id: value, action: 'btn-category-delete'},
                           success:function(data, textStatus, jqXHR)
                           {
                        	  window.location ='manage_categories.php';
                           },
                           error: function(jqXHR, textStatus, errorThrown)
                           {
                               //if fails     
                           }
                       });
                   	$('.mws-dialog-inner-category-delete').css('display','none');
                   	$(this).dialog("close");
                   }
               },{
                   text: "Cancel",
                   click: function () {
                   	$('.mws-dialog-inner-category-delete').css('display','none');
                       $(this).dialog("close");
                   }
               }]
           
           }).dialog("open");
       	
           
       });
       
       // ***************************************  END Delete Category  ****************************************

    // ***************************************  Start Second Category  ****************************************
    // Second Category Datatable
       $(".datatable-sec-category").dataTable({
   		 "bProcessing": true,
   			"bServerSide": true,
   			"sAjaxSource": "gridmodel/sec_category_grid.php",
   			"aoColumnDefs": [
   			 				{ 'bSortable': false, 'aTargets': [0,5] }
   			 			],
   			"aoColumns": [
   			              null,
   			              null,
   			              null,
   			              null,
   			              null,
   			              {
   			                "mData": null,
   			                "sDefaultContent": "Action"
   			              }
   			            ],
   			  "fnRowCallback": function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {
       		      // Bold the grade for all 'A' grade browsers
   				$('td:eq(0)', nRow).html( iDisplayIndex+1 );
   				//
   				$('td:eq(5)', nRow).html( '<span class="btn-group"><a href="javascript:void(0);" value="'+aData[0]+'" title="Delete Category" class="btn btn-s bs-tooltip btn-delete-sec-category"><i class="icon-trash"></i></a></span>');
   			  }
       });
    
       
       // ***************************************  Update Status Second Category  ****************************************
       $(document).on('click', '.sec_category_status_disable_enable', function() {
       	var v = $( this ).find('a').attr( 'value' );
       	var i = $( this ).find('a').attr( 'row' );
       	var countTableRow = $( this ).find('a').attr( 'countTableRow' );

       	$.ajax(
                   {
                       url : "ajax_data/update_action.php",
                       type: "POST",
                       data : {id: i,value: v, tableRow: countTableRow, action: 'btn-sec-category-update'},
                       success:function(data, textStatus, jqXHR)
                       {
                       	var item1 = $( ".sec_category_restaurantStatus"+countTableRow );
                       	$( item1 ).html( data );
                       },
                       error: function(jqXHR, textStatus, errorThrown)
                       {
                           //if fails     
                       }
                   });
       	
       });
       
       // *************************************** END Update Status Second Category  ****************************************
       
          
       
    // ***************************************  Delete Second Category  ****************************************
      
       $(document).on('click', '.btn-delete-sec-category', function() {
       	var value = $( this ).attr( 'value' );
       	$('.mws-dialog-inner-sec-category-delete').css('display','block');
       	
           $("#sec-category-delete").dialog({
               autoOpen: false,
               title: "Delete Category",
               modal: true,
               width: "600",
               buttons: [{
                   text: "Delete",
                   click: function () {
                   	$.ajax(
                       {
                           url : "ajax_data/delete_action.php",
                           type: "POST",
                           data : {id: value, action: 'btn-sec-category-delete'},
                           success:function(data, textStatus, jqXHR)
                           {
                        	  window.location ='manage_sec_categories.php';
                           },
                           error: function(jqXHR, textStatus, errorThrown)
                           {
                               //if fails     
                           }
                       });
                   	$('.mws-dialog-inner-sec-category-delete').css('display','none');
                   	$(this).dialog("close");
                   }
               },{
                   text: "Cancel",
                   click: function () {
                   	$('.mws-dialog-inner-sec-category-delete').css('display','none');
                       $(this).dialog("close");
                   }
               }]
           
           }).dialog("open");
       	
           
       });
       
       // ***************************************  END Delete Second Category  ****************************************       
      
    // ***************************************  Start Third Category  ****************************************
       //load 2nd level categories
       function loadSecondLevel(){
    	   $("#grand_parent_category_id").change(function(){
    	       	
      	   	 var value = $(this).find(":selected").val(); 
      	   	   
      	   	 $.ajax(
      	                {
      	                    url : "ajax_data/load_div.php",
      	                    type: "POST",
      	                    data : {id: value},
      	                    success:function(data, textStatus, jqXHR)
      	                    {
      	                   	 $("#div_id").html(data);
      	                   	 loadThirdLevel();
      	                    },
      	                    error: function(jqXHR, textStatus, errorThrown)
      	                    {
      	                        //if fails     
      	                    }
      	                });
      	   	 
      	   	}).trigger('change');
       }
       
       //load 3rd level categories 
       function loadThirdLevel(){
    	   $("#div_id").change(function(){
        	   var value = $(this).find(":selected").val(); 
      	   	 $.ajax(
      	                {
      	                    url : "ajax_data/load_div_third.php",
      	                    type: "POST",
      	                    data : {id: value},
      	                    success:function(data, textStatus, jqXHR)
      	                    {
      	                   	 $("#div_id_third").html(data);
      	                    },
      	                    error: function(jqXHR, textStatus, errorThrown)
      	                    {
      	                        //if fails     
      	                    }
      	                });
      	   	 
      	   	}).trigger('change'); 
       }
       
       loadSecondLevel();
       
       
    // Third Category Datatable
       $(".datatable-th-category").dataTable({
   		 "bProcessing": true,
   			"bServerSide": true,
   			"sAjaxSource": "gridmodel/th_category_grid.php",
   			"aoColumnDefs": [
   			 				{ 'bSortable': false, 'aTargets': [0,6] }
   			 			],
   			"aoColumns": [
   			              null,
   			              null,
   			              null,
   			              null,
   			              null,
   			              null,
   			              {
   			                "mData": null,
   			                "sDefaultContent": "Action"
   			              }
   			            ],
   			  "fnRowCallback": function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {
       		      // Bold the grade for all 'A' grade browsers
   				$('td:eq(0)', nRow).html( iDisplayIndex+1 );
   				//
   				$('td:eq(6)', nRow).html( '<span class="btn-group"><a href="javascript:void(0);" value="'+aData[0]+'" title="Delete Category" class="btn btn-s bs-tooltip btn-delete-th-category"><i class="icon-trash"></i></a></span>');
   			  }
       });
    
       
       // ***************************************  Update Status Third Category  ****************************************
       $(document).on('click', '.th_category_status_disable_enable', function() {
       	var v = $( this ).find('a').attr( 'value' );
       	var i = $( this ).find('a').attr( 'row' );
       	var countTableRow = $( this ).find('a').attr( 'countTableRow' );

       	$.ajax(
                   {
                       url : "ajax_data/update_action.php",
                       type: "POST",
                       data : {id: i,value: v, tableRow: countTableRow, action: 'btn-th-category-update'},
                       success:function(data, textStatus, jqXHR)
                       {
                       	var item1 = $( ".th_category_restaurantStatus"+countTableRow );
                       	$( item1 ).html( data );
                       },
                       error: function(jqXHR, textStatus, errorThrown)
                       {
                           //if fails     
                       }
                   });
       	
       });
       
       // *************************************** END Update Status Third Category  ****************************************
       
          
       
    // ***************************************  Delete Third Category  ****************************************
      
       $(document).on('click', '.btn-delete-th-category', function() {
       	var value = $( this ).attr( 'value' );
       	$('.mws-dialog-inner-th-category-delete').css('display','block');
       	
           $("#th-category-delete").dialog({
               autoOpen: false,
               title: "Delete Category",
               modal: true,
               width: "600",
               buttons: [{
                   text: "Delete",
                   click: function () {
                   	$.ajax(
                       {
                           url : "ajax_data/delete_action.php",
                           type: "POST",
                           data : {id: value, action: 'btn-th-category-delete'},
                           success:function(data, textStatus, jqXHR)
                           {
                        	  window.location ='manage_th_categories.php';
                           },
                           error: function(jqXHR, textStatus, errorThrown)
                           {
                               //if fails     
                           }
                       });
                   	$('.mws-dialog-inner-th-category-delete').css('display','none');
                   	$(this).dialog("close");
                   }
               },{
                   text: "Cancel",
                   click: function () {
                   	$('.mws-dialog-inner-th-category-delete').css('display','none');
                       $(this).dialog("close");
                   }
               }]
           
           }).dialog("open");
       	
           
       });
       
       // ***************************************  END Delete Third Category  **************************************** 
       
 // **##**##**##**##**##**##**##**##**##**## END Category Management   **##**##**##**##**##**##**##**##**##**##**##
    
       //Question datatable
       $(".datatable-question").dataTable({
     		 "bProcessing": true,
     			"bServerSide": true,
     			"sAjaxSource": "gridmodel/question_grid.php",
     			"aoColumnDefs": [
     			 				{ 'bSortable': false, 'aTargets': [0,5] }
     			 			],
     			"aoColumns": [
     			              null,
     			              null,
							  null,
							    null,
     			              null,
     			              {
     			                "mData": null,
     			                "sDefaultContent": "Action"
     			              }
     			            ],
     			  "fnRowCallback": function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {
         		      // Bold the grade for all 'A' grade browsers
     				$('td:eq(0)', nRow).html( iDisplayIndex+1 );
     				//
     				$('td:eq(5)', nRow).html( '<span class="btn-group"><a href="javascript:void(0);" value="'+aData[0]+'" title="Delete Category" class="btn btn-s bs-tooltip btn-delete-question"><i class="icon-trash"></i></a></span>');
     			  }
         });
       
       //delete question
       $(document).on('click', '.btn-delete-question', function() {
          	var value = $( this ).attr( 'value' );
          	$('.mws-dialog-inner-question-delete').css('display','block');
          	
              $("#question-delete").dialog({
                  autoOpen: false,
                  title: "Delete Question",
                  modal: true,
                  width: "600",
                  buttons: [{
                      text: "Delete",
                      click: function () {
                      	$.ajax(
                          {
                              url : "ajax_data/delete_action.php",
                              type: "POST",
                              data : {id: value, action: 'btn-question-delete'},
                              success:function(data, textStatus, jqXHR)
                              {
                           	  window.location ='manage_questions.php';
                              },
                              error: function(jqXHR, textStatus, errorThrown)
                              {
                                  //if fails     
                              }
                          });
                      	$('.mws-dialog-inner-question-delete').css('display','none');
                      	$(this).dialog("close");
                      }
                  },{
                      text: "Cancel",
                      click: function () {
                      	$('.mws-dialog-inner-question-delete').css('display','none');
                          $(this).dialog("close");
                      }
                  }]
              
              }).dialog("open");
          	
              
          });
       
       //update question status
       $(document).on('click', '.question_status_disable_enable', function() {
          	var v = $( this ).find('a').attr( 'value' );
          	var i = $( this ).find('a').attr( 'row' );
          	var countTableRow = $( this ).find('a').attr( 'countTableRow' );

          	$.ajax(
                      {
                          url : "ajax_data/update_action.php",
                          type: "POST",
                          data : {id: i,value: v, tableRow: countTableRow, action: 'btn-question-update'},
                          success:function(data, textStatus, jqXHR)
                          {
                          	var item1 = $( ".question_restaurantStatus"+countTableRow );
                          	$( item1 ).html( data );
                          },
                          error: function(jqXHR, textStatus, errorThrown)
                          {
                              //if fails     
                          }
                      });
          	
          });
       
       ////////////////////////////////////////////////////////////////////////////////////////////
    // Category Prize
       $(".datatable-prize").dataTable({
   		 "bProcessing": true,
   			"bServerSide": true,
   			"sAjaxSource": "gridmodel/prize_grid.php",
   			"aoColumnDefs": [
   			 				{ 'bSortable': false, 'aTargets': [0,5] }
   			 			],
   			"aoColumns": [
   			              null,
						  null,
   			              null,
   			              null,
   			              null,
   			              {
   			                "mData": null,
   			                "sDefaultContent": "Action"
   			              }
   			            ],
   			  "fnRowCallback": function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {
       		      // Bold the grade for all 'A' grade browsers
   				$('td:eq(0)', nRow).html( iDisplayIndex+1 );
   				//
   				$('td:eq(5)', nRow).html( '<span class="btn-group"><a href="javascript:void(0);" value="'+aData[0]+'" title="Delete Category" class="btn btn-s bs-tooltip btn-delete-prize"><i class="icon-trash"></i></a><a href="choose_winner.php?prize_id='+aData[0]+'" target="_blank" value="'+aData[0]+'" title="Choose Winner" class="btn btn-s bs-tooltip btn-assign-prize"><i class="icon-ok"></i></a></span>');
   			  }
       });
    
       
       // ***************************************  Update Status Prize  ****************************************
       $(document).on('click', '.prize_status_disable_enable', function() {
       	var v = $( this ).find('a').attr( 'value' );
       	var i = $( this ).find('a').attr( 'row' );
       	var countTableRow = $( this ).find('a').attr( 'countTableRow' );

       	$.ajax(
                   {
                       url : "ajax_data/update_action.php",
                       type: "POST",
                       data : {id: i,value: v, tableRow: countTableRow, action: 'btn-prize-update'},
                       success:function(data, textStatus, jqXHR)
                       {
                       	var item1 = $( ".prize_restaurantStatus"+countTableRow );
                       	$( item1 ).html( data );
                       },
                       error: function(jqXHR, textStatus, errorThrown)
                       {
                           //if fails     
                       }
                   });
       	
       });
       
       // *************************************** END Update Status Prize  ****************************************
       
    // ***************************************  Delete Prize  ****************************************
       $(document).on('click', '.btn-delete-prize', function() {
       	var value = $( this ).attr( 'value' );
       	$('.mws-dialog-inner-prize-delete').css('display','block');
       	
           $("#prize-delete").dialog({
               autoOpen: false,
               title: "Delete Prize",
               modal: true,
               width: "600",
               buttons: [{
                   text: "Delete",
                   click: function () {
                   	$.ajax(
                       {
                           url : "ajax_data/delete_action.php",
                           type: "POST",
                           data : {id: value, action: 'btn-prize-delete'},
                           success:function(data, textStatus, jqXHR)
                           {
                        	  window.location ='manage_prizes.php';
                           },
                           error: function(jqXHR, textStatus, errorThrown)
                           {
                               //if fails     
                           }
                       });
                   	$('.mws-dialog-inner-prize-delete').css('display','none');
                   	$(this).dialog("close");
                   }
               },{
                   text: "Cancel",
                   click: function () {
                   	$('.mws-dialog-inner-prize-delete').css('display','none');
                       $(this).dialog("close");
                   }
               }]
           
           }).dialog("open");
       	
           
       });
       



//***************************************  View new Order  ****************************************
$(document).on('click', '.btn-new-order-view', function() {
	var value = $( this ).attr( 'value' );
	window.location = 'manage_orders.php?order='+value+'#orderview';
});
//***************************************  end view new order  ****************************************

//***************************************  View confirm Order  ****************************************
$(document).on('click', '.btn-confirm-order-view', function() {
	var value = $( this ).attr( 'value' );
	window.location = 'confirm_orders.php?order='+value+'#orderview';
});
//***************************************  end view confirm order  ****************************************

//***************************************  View confirm Order  ****************************************
$(document).on('click', '.btn-all-order-view', function() {
	var value = $( this ).attr( 'value' );
	window.location = 'history_orders.php?order='+value+'#orderview';
});
//***************************************  end view confirm order  ****************************************

// **##**##**##**##**##**##**##**##**##**## END New Order Management   **##**##**##**##**##**##**##**##**##**##**##
 
    
    // ------------------------------------ END Resturant Admin Panel -------------------------------------
 // ------------------------------------------------------------------------------------------------   

    
    $(".ui-dialog-buttonset :button").removeClass("ui-state-default");
    	
    
    
  //++==++==++==++==++==++==++==++==      					++==++==++==++==++==++==++==++==
	//++==++==++==++==++==++==++==++==      not related to us	++==++==++==++==++==++==++==++==           
	//++==++==++==++==++==++==++==++==      					++==++==++==++==++==++==++==++==
    
    
    
	//===== Date Pickers & Time Pickers & Color Pickers =====//
	$( ".datepicker" ).datepicker({
		defaultDate: +7,
		showOtherMonths:true,
		autoSize: true,
		appendText: '<span class="help-block">(yyyy-mm-dd)</span>',
		dateFormat: 'yy-mm-dd',
		minDate: 0
		});
	
	
	//===== Notifications =====//
	
	// @see: for default options, see assets/js/plugins.js (initNoty())

	$('.btn-notification').click(function() {
		var self = $(this);

		noty({
			text: self.data('text'),
			type: self.data('type'),
			layout: self.data('layout'),
			timeout: 2000,
			modal: self.data('modal'),
			buttons: (self.data('type') != 'confirm') ? false : [
				{
					addClass: 'btn btn-primary', text: 'Ok', onClick: function($noty) {
						$noty.close();
						noty({force: true, text: 'You clicked "Ok" button', type: 'success', layout: self.data('layout')});
					}
				}, {
					addClass: 'btn btn-danger', text: 'Cancel', onClick: function($noty) {
						$noty.close();
						noty({force: true, text: 'You clicked "Cancel" button', type: 'error', layout: self.data('layout')});
					}
				}
			]
		});

		return false;
	});

	
	//===== Slim Progress Bars (nprogress) =====//
	$('.btn-nprogress-start').click(function () {
		NProgress.start();
		$('#nprogress-info-msg').slideDown(200);
	});

	$('.btn-nprogress-set-40').click(function () {
		NProgress.set(0.4);
	});

	$('.btn-nprogress-inc').click(function () {
		NProgress.inc();
	});

	$('.btn-nprogress-done').click(function () {
		NProgress.done();
		$('#nprogress-info-msg').slideUp(200);
	});

	//===== Bootbox (Modals and Dialogs) =====//
	$("a.basic-alert").click(function(e) {
		e.preventDefault();
		bootbox.alert("Hello world!", function() {
			console.log("Alert Callback");
		});
	});

	$("a.confirm-dialog").click(function(e) {
		e.preventDefault();
		bootbox.confirm("Are you sure?", function(confirmed) {
			console.log("Confirmed: "+confirmed);
		});
	});

	$("a.multiple-buttons").click(function(e) {
		e.preventDefault();
		bootbox.dialog({
			message: "I am a custom dialog",
			title: "Custom title",
			buttons: {
				success: {
					label: "Success!",
					className: "btn-success",
					callback: function() {
						console.log("great success");
					}
				},
				danger: {
					label: "Danger!",
					className: "btn-danger",
					callback: function() {
						console.log("uh oh, look out!");
					}
				},
				main: {
					label: "Click ME!",
					className: "btn-primary",
					callback: function() {
						console.log("Primary button");
					}
				}
			}
		});
	});

	$("a.multiple-dialogs").click(function(e) {
		e.preventDefault();

		bootbox.alert("Prepare for multiboxes in 1 second...");

		setTimeout(function() {
			bootbox.dialog({
				message: "Do you like Melon?",
				title: "Fancy Title",
				buttons: {
					danger: {
						label: "No :-(",
						className: "btn-danger",
						callback: function() {
							bootbox.alert("Aww boo. Click the button below to get rid of all these popups.", function() {
								bootbox.hideAll();
							});
						}
					},
					success: {
						label: "Oh yeah!",
						className: "btn-success",
						callback: function() {
							bootbox.alert("Glad to hear it! Click the button below to get rid of all these popups.", function() {
								bootbox.hideAll();
							});
						}
					}
				}
			});
		}, 1000);
	});

	$("a.programmatic-close").click(function(e) {
		e.preventDefault();
		var box = bootbox.alert("This dialog will automatically close in two seconds...");
		setTimeout(function() {
			box.modal('hide');
		}, 2000);
	});

});


//***************************************  Order Update  ****************************************
function UpdateOrder(orderID, status, url){
	var value = orderID;
	var order_status = status;
	
 	$.ajax(
          {
              url : "ajax_data/update_order_status.php",
              type: "POST",
              data : {id: value, status: order_status},
              success:function(data, textStatus, jqXHR)
              {
           	  window.location = url;
              },
              error: function(jqXHR, textStatus, errorThrown)
              {
                  //if fails     
              }
          });
}
//***************************************  end order Update  ****************************************

//***************************************  Remove call  ****************************************
function RemoveCall(callID){
	var value = callID;
	
 	$.ajax(
          {
              url : "ajax_data/remove_call.php",
              type: "POST",
              data : {id: value},
              success:function(data, textStatus, jqXHR)
              {
           	  window.location = 'waiter_calls.php';
              },
              error: function(jqXHR, textStatus, errorThrown)
              {
                  //if fails     
              }
          });
}
//***************************************  end remove call  ****************************************
//reservation Update 
function UpdateReservation(reservationID, status, url){
	var value = reservationID;
	var order_status = status;
	
 	$.ajax(
          {
              url : "ajax_data/update_reservation_status.php",
              type: "POST",
              data : {id: value, status: order_status},
              success:function(data, textStatus, jqXHR)
              {
           	  window.location = url;
              },
              error: function(jqXHR, textStatus, errorThrown)
              {
                  //if fails     
              }
          });
}
// end reservation Update 
