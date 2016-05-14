<?php 
$recebido =0;
$aparelho = $_POST['aparelho1'];
$funcao = $_POST['funcao1'];
$funcaofazer = $_POST['funcaofazer'];
$inicio = $_POST['inicio'];
$final = $_POST['final'];
$codigo = 0;
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

$query2 = "SELECT codigo  FROM cadastro WHERE aparelho = '$aparelho' AND funcao = '$funcao'; ";
$recebido = mysql_query($query2,$connect);
$row = mysql_fetch_array($recebido);

$nada =  $row [0];

$query4 = "SELECT codigo  FROM cadastro WHERE aparelho = '$aparelho' AND funcao = '$funcaofazer'; ";
$recebido1 = mysql_query($query4,$connect);
$row1 = mysql_fetch_array($recebido1);

$nada1 =  $row1 [0];


$query5 = "INSERT INTO bloqueio (codigo,horarioDebloqueioInicial,horarioDebloqueioFinal, funcaoAodetectarBloqueio ) VALUES ('$nada','$inicio','$final','$nada1')" ;
$insert = mysql_query($query5,$connect);	

if($insert){
					

 
 echo"<script language='javascript' type='text/javascript'>alert('bloqueio configurado');window.location.href='home.php'</script>";
}else{
echo"<script language='javascript' type='text/javascript'>alert('a funcao escolhida nao é do respectivo aparelho');window.location.href='home.php'</script>";
}
				


mysql_close($connect);


?>
