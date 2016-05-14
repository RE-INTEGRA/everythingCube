<?php 
$recebido =0;
$user ="";
$nSenha = $_POST['nSenha'];
$nemail = $_POST['nemail'];
$id = 0;



if (!empty($_POST) AND (empty($_POST['nemail']) OR empty($_POST['nSenha']))) {
        echo"<script language='javascript' type='text/javascript'>alert('preencha as caixas de texto');window.location.href='index.html'</script>";
    }else{
include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT `usuarios`.`senha` FROM `$nomedb`.`usuarios` WHERE senha= '$nSenha';";
$recebido = mysql_query($query2,$connect);
$row = mysql_fetch_array($recebido);

$query3 = "SELECT `usuarios`.`email` FROM `$nomedb`.`usuarios` WHERE email= '$nemail';";
$recebido2 = mysql_query($query3,$connect);
$row2 = mysql_fetch_array($recebido2);

$query4 = "SELECT `usuarios`.`usuario` FROM `$nomedb`.`usuarios` WHERE email= '$nemail' AND senha= '$nSenha';";
$recebido3 = mysql_query($query3,$connect);
$row3 = mysql_fetch_array($recebido2);


if($row['senha'] ==$nSenha){
	
	if($row2['email'] ==$nemail ){
		
		if (!isset($_SESSION)) session_start();
      
        // Salva os dados encontrados na sessão
        
        $_SESSION['UsuarioNome'] = $row2['email'];

	echo"<script language='javascript' type='text/javascript'>alert('Bem vindo');window.location.href='home.php'</script>";
        
	}
	
	
}else{
	
	echo"<script language='javascript' type='text/javascript'>alert('a senha ou nome de usuario, estão errados ');window.location.href='index.html'</script>";
}

 

  
mysql_close($connect);
	}
?>