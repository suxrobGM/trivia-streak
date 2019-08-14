<?php
//mysql_connect("localhost","nomi001_tservice","Comsats?1430");
//		mysql_select_db("nomi001_tservice");
    move_uploaded_file($_FILES["file"]["tmp_name"], "profile_images/" . $_FILES["file"]["name"]);
      //mysql_query("UPDATE users SET user_image='".$_FILES["file"]["name"]."' WHERE user_name='".$_Post['user_id']."'");
?>