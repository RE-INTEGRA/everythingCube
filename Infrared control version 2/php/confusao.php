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
$aparelho = $_POST['browserss'];
$funcao = $_POST['browsers2'];

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

if($nada){
	
$data = $_POST['usr_time'];	
	
$query3 = "INSERT INTO funcoescontroladas (horario,idcodigos,user) VALUES ('$data','$nada','$user')" ;
$insert = mysql_query($query3,$connect);	

}else{
	
echo"<script language='javascript' type='text/javascript'>alert('funcao nao esta cadastrada nesse aparelho');window.location.href='home.php'</script>";
	
}
 if($insert){
				
                    echo"<script language='javascript' type='text/javascript'>alert('enviado!');window.location.href='home.php'</script>";
                }else{
                    echo"<script language='javascript' type='text/javascript'>alert('Não foi possível enviar');window.location.href='home.php'</script>";
                }



                 
                
mysql_close($connect);
?>