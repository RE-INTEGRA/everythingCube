

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


$recebido =0;
$nada = $_POST['fazer'];


$codigo = 0;
$id = 0;
include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

	
$query3 = "INSERT INTO hora (idhora,agora,user) VALUES ('$id','$nada','$user')" ;
$insert = mysql_query($query3,$connect);	

 
mysql_close($connect);
?>