<?PHP
	require "init.php";

	$ret_post = $_POST;
	$user_id = $ret_post["user_id"];
	$item_id = $ret_post["item_id"];
	
	$dbo = mysqli_connect("localhost", "root", "autoset", "gppj") or die("eeeee");

	$query = "Insert Into users_items Values (" . $user_id . ", " . $item_id . ")";

	$users_dbo = mysqli_query($dbo, $query) or die("���� ������ ������");

	mysqli_close($dbo);

	echo iconv("EUC-KR", "UTF-8", "������");
?>