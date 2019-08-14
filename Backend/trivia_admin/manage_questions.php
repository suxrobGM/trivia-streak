<?php include_once 'head.php'; ?>
<?php include_once 'controllers/Category.php'; ?>
<?php include_once 'controllers/SecondCategory.php'; ?>
<body>
	<?php include_once 'header.php'; ?>
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
					Question Added.
				</div>
            	<?php 
            }
            else if($flag == "failed")
            {
            	?>
            	<div class="alert fade in alert-danger">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Question Couldnot Added.
				</div>
                <?php 
            }else if($flag == "update_success")
            {
            	?>
            	<div class="alert fade in alert-success">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Question Updated.
				</div>
            	<?php 
            }
            else if($flag == "update_failed")
            {
            	?>
            	<div class="alert fade in alert-danger">
					<i class="icon-remove close" data-dismiss="alert"></i>
					Question Couldnot Updated.
				</div>
                <?php 
            }
            ?>
				<!--=== Page Content ===-->
				<div class="row">
					<!--=== General Buttons ===-->
					<div class="col-md-12">
						<div class="widget box">
						<a class="btn btn-primary" href="#add-qu">Add Question</a>
						</div>
					</div>
				</div>
		
				<!--=== Normal ===-->
				<div class="row">
					<div class="col-md-12">
						<div class="widget box">
							<div class="widget-header">
								<h4><i class="icon-reorder"></i> Manage Questions</h4>
								<div class="toolbar no-padding">
									<div class="btn-group">
										<span class="btn btn-xs widget-collapse"><i class="icon-angle-down"></i></span>
									</div>
								</div>
							</div>
							<div class="widget-content">
								<table class="table table-striped table-bordered table-hover table-checkable datatable-question">
									<thead>
										<tr>
											<th>No</th>
											<th>Category</th>
											<th>Question</th>
											<th>Difficulty</th>
											<th>Status</th>
											<th>Action</th>
										</tr>
									</thead>
									
								</table>
							</div>
						</div>
					</div>
				</div>
				<!-- /Normal -->
				
				<!--=== Form ===-->
				<div id="add-qu" class="row">
					<div class="col-md-12">
						<div class="widget box">
							<div class="widget-header">
								<h4><i class="icon-reorder"></i> Add Question</h4>
								<div class="toolbar no-padding">
									<div class="btn-group">
										<span class="btn btn-xs widget-collapse"><i class="icon-angle-down"></i></span>
									</div>
								</div>
							</div>
							<div class="widget-content">
                               <form class="form-horizontal row-border" id="validate-2" action="action/add_question.php"  method="post" enctype="multipart/form-data">
									<div class="form-group">
										<label class="col-md-2 control-label" for="input17">1st Level Category:<span class="required">*</span></label>
										<div class="col-md-4">
										 <?php 
										 		$categoryObj = new Category();
                                                $categories = $categoryObj->getAllCategories();
                                                if($categories){
                                         ?>
											<select id="grand_parent_category_id" name="grand_parent_category_id" class="select2-select-00 col-md-12 full-width-fix">
                                               <?php 
                                                foreach($categories as $category){
                                                	?>
                                                	<option value="<?php echo $category->cat_id; ?>"><?php echo $category->cat_name; ?></option>
                                                	<?php 
                                                	}
                                                ?>
                                                </select>
                                               <?php 
                                                }
                                                else{
                                                	?>
                                                	<b>Please Add Atleast One Category.</b>
                                                	<?php 
                                                }
                                                ?>
											
										</div>
									</div>
						<!--			<div class="form-group">
										<label class="col-md-2 control-label" for="input17">2nd Level Category:<span class="required">*</span></label>
										<div class="col-md-4">
										
											<select id="div_id" name="parent_category_id" class="col-md-12 full-width-fix">
                                               
                                                </select>
                                               
											
										</div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label" for="input17">3rd Level Category:<span class="required">*</span></label>
										<div class="col-md-4">
										
											<select id="div_id_third" name="child_category_id" class="col-md-12 full-width-fix">
                                               
                                                </select>
                                               
											
										</div>
									</div>  -->
									<div class="form-group">
										<label class="col-md-2 control-label">Question:<span class="required">*</span></label>
										<div class="col-md-4"><input type="text" name="qu_text" required class="form-control required"></div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label">Option 1:<span class="required">*</span></label>
										<div class="col-md-4"><input type="text" name="qu_option1" required class="form-control required"></div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label">Option 2:<span class="required">*</span></label>
										<div class="col-md-4"><input type="text" name="qu_option2" required class="form-control required"></div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label">Option 3:<span class="required">*</span></label>
										<div class="col-md-4"><input type="text" name="qu_option3" required class="form-control required"></div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label">Option 4:<span class="required">*</span></label>
										<div class="col-md-4"><input type="text" name="qu_option4" required class="form-control required"></div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label" for="input17">Answer:<span class="required">*</span></label>
										<div class="col-md-4">
											<select name="qu_answer" class="select2-select-00 col-md-12 full-width-fix">
                                                	<option value="1">Option 1</option>
                                                	<option value="2">Option 2</option>
                                                	<option value="3">Option 3</option>
                                                	<option value="4">Option 4</option>
                                                </select>
										</div>
									</div>
									<div class="form-group">
										<label class="col-md-2 control-label" for="input17">Difficulty:<span class="required">*</span></label>
										<div class="col-md-4">
											<select name="qu_difficulty" class="select2-select-00 col-md-12 full-width-fix">
                                                	<option value="EASY">Easy</option>
                                                	<option value="MEDIUM">Medium</option>
                                                	<option value="DIFFICULT">Difficult</option>
                                                </select>
										</div>
									</div>

									<div class="form-group">
										<label class="col-md-2 control-label">Status</label>
										<div class="col-md-8">
											<div class="make-switch" data-on="info" data-off="success">
												<input name="status" type="checkbox" checked class="toggle">
											</div>
										</div>
									</div>
									
									<div class="form-actions">
										<input type="submit" value="Submit" class="btn btn-primary pull-right">
									</div>
								</form>
							</div>
						</div>
					</div>
				</div>
				<!-- /Form -->
                </div>
				<!-- /Page Content -->
				
				 <div id="th-category-edit">
                        		<div class="mws-dialog-inner-th-category-edit">
                                </div>
                            </div>
                            
				  <div id="question-delete">
                        		<div class="mws-dialog-inner-question-delete" style="display: none;">
                        		Do you really want to delete this question?
                                </div>
                            </div>
                            
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