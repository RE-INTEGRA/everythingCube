<?php 
$recebido =0;
$nomedoaparelho = $_POST['nomedoaparelho'];
$funcao = $_POST['funcao'];
$codigo = $_POST['codigo'];
$id = 0;
$bits = array();
$dos = ":";


if (($handle = fopen('C:\\irconfig\\config.txt','r')) !== FALSE) {
  while (($data = fgetcsv($handle, 0, ":")) !== FALSE) {
    $bits[$data[0]] = $data[1];
  }
}

$servidor =  $bits['servidor'];
$port =  $bits['porta'];
$usuario = $bits['usuario'];
$senhaserv = $bits['senhaservidor'];
$nomedb = $bits['nomeDb'];

$finalcod = $servidor."".$dos."".$port;



$connect=mysql_connect($finalcod,$usuario,$senhaserv) or die(mysql_error());

mysql_select_db($nomedb,$connect) or die(mysql_error());


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db('ircontrol',$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT idcodigos FROM codigorecebido ORDER BY numero DESC";
$recebido = mysql_query($query2,$connect);




 $query = "INSERT INTO cadastro (idcadastro,codigo,aparelho, funcao) VALUES ('$id','$codigo','$nomedoaparelho', '$funcao')";
                $insert = mysql_query($query,$connect);
                 
                if($insert){
					
                    echo"<script language='javascript' type='text/javascript'>alert('cadastrado com sucesso!');window.location.href='shop.php'</script>";
                }else{
                    echo"<script language='javascript' type='text/javascript'>alert('Não foi possível cadastrar');window.location.href='shop.php'</script>";
                }



                 
                
mysql_close($connect);
?>