<?php 
$bits = array();
$dos = ":";

$servidor =  "127.0.0.1";
$port =  "3306";
$usuario = "root";
$senhaserv = "32412308";
$nomedb = "sys";

$finalcod = $servidor."".$dos."".$port;

	

error_reporting (E_ALL ^ E_DEPRECATED);

$connect=mysql_connect($finalcod,$usuario,$senhaserv) or die(mysql_error());

mysql_select_db($nomedb,$connect) or die(mysql_error());

?>