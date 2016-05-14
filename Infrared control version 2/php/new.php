<?php

$user;


if (!isset($_SESSION)) session_start();

if (!isset($_SESSION['UsuarioNome'])) {
        // Destrói a sessão por segurança
        session_destroy();
        // Redireciona o visitante de volta pro login
        header("Location: index.html"); exit;
    }
$user= $_SESSION['UsuarioNome'];

include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT idcodigos FROM codigorecebido WHERE user= '$user' ORDER BY numero DESC";
$recebido = mysql_query($query2,$connect);
$row = mysql_fetch_array($recebido);


if( $_GET["codigo"]) {
      echo  $row ['idcodigos'];

   }






?>