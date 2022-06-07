<?PHP
	require "init.php";

	$ret_post = $_POST;
	$user_id = $ret_post["user_id"];
	
	$dbo = mysqli_connect("localhost", "root", "autoset", "gppj") or die("eeeee");
	
	$query = "Select itemPK From users_items as ui WHERE ui.userPK = " . $user_id;

	$users_dbo = mysqli_query($dbo, $query);

	$tmp = array();
	if($users_dbo->num_rows > 0) {
		while($is = mysqli_fetch_assoc($users_dbo)){
			array_push($tmp, $is);
		}
	}

	$tmp2 = array();
	if(count($tmp) > 0) {
		$c = 0;
		while(count($tmp) != $c){
			$query = "Select * FROM items as i WHERE i.id = " . $tmp[$c]["itemPK"];
			$users_dbo = mysqli_query($dbo, $query);
			array_push($tmp2, mysqli_fetch_assoc($users_dbo));
			$c += 1;
		}
	}

	mysqli_close($dbo);

	$ret_json["result"] = $tmp2;

	echo json_encode($ret_json, JSON_UNESCAPED_UNICODE);
?>