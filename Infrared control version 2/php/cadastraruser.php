<?php 
$recebido =0;
$nUser = $_POST['nUser'];
$nSenha = $_POST['nSenha'];
$nemail = $_POST['nemail'];
$id = 0;
include 'test.php';

if (!empty($_POST) AND (empty($_POST['nUser']) OR empty($_POST['nSenha'])OR empty($_POST['nemail']))) {
        echo"<script language='javascript' type='text/javascript'>alert('Não foi possível cadastrar, falta preencher algo');window.location.href='logincadastro.html'</script>";
    }else{
	
 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT `usuarios`.`usuario` FROM `$nomedb`.`usuarios` WHERE usuario= '$nUser';";
$recebido = mysql_query($query2,$connect);
$row = mysql_fetch_array($recebido);

$query3 = "SELECT `usuarios`.`email` FROM `$nomedb`.`usuarios` WHERE email= '$nemail';";
$recebido2 = mysql_query($query3,$connect);
$row2 = mysql_fetch_array($recebido2);

if($row['usuario'] ==$nUser){
	echo"<script language='javascript' type='text/javascript'>alert('o usuario, ja esta em uso ');window.location.href='logincadastro.html'</script>";
	if($row2['email'] ==$nemail ){

	echo"<script language='javascript' type='text/javascript'>alert('o email, ja esta em uso ');window.location.href='logincadastro.html'</script>";
	}		
}else{


	
	 
$query = "INSERT INTO `$nomedb`.`usuarios`(`usuario`,`senha`,`email`)VALUES('$nUser','$nSenha','$nemail');";
                $insert = mysql_query($query,$connect);

 }                
                if($insert){
					
                    echo"<script language='javascript' type='text/javascript'>alert('cadastrado com sucesso!, agora acesse');window.location.href='index.html'</script>";
                }else{
                    echo"<script language='javascript' type='text/javascript'>alert('Não foi possível cadastrar');window.location.href='logincadastro.html'</script>";
                }



                 
                
mysql_close($connect);
	}
?>