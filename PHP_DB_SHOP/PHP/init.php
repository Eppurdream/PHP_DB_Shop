<?PHP
define("HOST_DIR", $_SERVER['DOCUMENT_ROOT']);
define("ROOT_DIR", HOST_DIR . "/GPPJ");
define("LIB_DIR", HOST_DIR . "/lib");
define("LOG_DIR", HOST_DIR . "/_log");
define("DEBUG_LEVEL", 1);

ini_set("include_path", LIB_DIR);
error_reporting(E_ALL ^ E_NOTICE ^ E_DEPRECATED ^ E_STRICT);
ini_set("display_errors", DEBUG_LEVEL);

require "lib.common.php";
?>