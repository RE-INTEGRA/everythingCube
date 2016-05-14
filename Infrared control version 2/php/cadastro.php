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
$nomedoaparelho = $_POST['nomedoaparelho'];
$funcao = $_POST['funcao'];
$codigo = $_POST['codigo'];
$id = 0;
include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT idcodigos FROM codigorecebido WHERE user = '$user' ORDER BY numero DESC";
$recebido = mysql_query($query2,$connect);




 $query = "INSERT INTO cadastro (idcadastro,codigo,aparelho, funcao,user) VALUES ('$id','$codigo','$nomedoaparelho', '$funcao','$user')";
                $insert = mysql_query($query,$connect);
                 
                if($insert){
					
                    echo"<script language='javascript' type='text/javascript'>alert('cadastrado com sucesso!');window.location.href='shop.php'</script>";
                }else{
                    echo"<script language='javascript' type='text/javascript'>alert('Não foi possível cadastrar');window.location.href='shop.php'</script>";
                }



                 
                
mysql_close($connect);
?>