<?PHP
	require "init.php";

	$ret_post = $_POST;
	$type = $ret_post["type"];

	$dbo = mysqli_connect("localhost", "root", "autoset", "gppj") or die("eeeee");
	
	$query = "Select * From items as i WHERE i.type = " . $type . " ORDER BY rand() LIMIT 3";

	$users_dbo = mysqli_query($dbo, $query);
	
	$array_assoc = array();

	$tmp = array();
	if($users_dbo->num_rows > 0) {
		while($is = mysqli_fetch_assoc($users_dbo)){
			array_push($tmp, $is);
		}
	}

	$ret_json["result"] = $tmp;

	mysqli_close($dbo);

	echo json_encode($ret_json, JSON_UNESCAPED_UNICODE);
?>