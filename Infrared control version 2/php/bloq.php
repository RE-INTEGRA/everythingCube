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
$aparelho = $_POST['aparelho1'];
$funcao = $_POST['funcao1'];
$funcaofazer = $_POST['funcaofazer'];
$inicio = $_POST['inicio'];
$final = $_POST['final'];
$codigo = 0;
$id = 0;
include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT codigo  FROM cadastro WHERE aparelho = '$aparelho' AND funcao = '$funcao' AND user='$user'; ";
$recebido = mysql_query($query2,$connect);
$row = mysql_fetch_array($recebido);

$nada =  $row [0];

$query4 = "SELECT codigo  FROM cadastro WHERE aparelho = '$aparelho' AND funcao = '$funcaofazer' AND user='$user'; ";
$recebido1 = mysql_query($query4,$connect);
$row1 = mysql_fetch_array($recebido1);

$nada1 =  $row1 [0];


$query5 = "INSERT INTO bloqueio (codigo,horarioDebloqueioInicial,horarioDebloqueioFinal, funcaoAodetectarBloqueio,user ) VALUES ('$nada','$inicio','$final','$nada1','$user')" ;
$insert = mysql_query($query5,$connect);	

if($insert){
					

 
 echo"<script language='javascript' type='text/javascript'>alert('bloqueio configurado');window.location.href='home.php'</script>";
}else{
echo"<script language='javascript' type='text/javascript'>alert('a funcao escolhida nao é do respectivo aparelho');window.location.href='home.php'</script>";
}
				


mysql_close($connect);


?>
