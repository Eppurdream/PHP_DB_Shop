<?PHP
	require "init.php";

	$ret_post = $_POST;
	$user_id = $ret_post["user_id"];
	$update_value = $ret_post["update_value"];
	
	$dbo = mysqli_connect("localhost", "root", "autoset", "gppj") or die("eeeee");

	$query = "Update users set money = " . $update_value . " where id = " . $user_id;

	$users_dbo = mysqli_query($dbo, $query) or die("���� ������ ������");

	mysqli_close($dbo);
	
	echo iconv("EUC-KR", "UTF-8", "������");
?>