

<?php 
$recebido =0;
$aparelho = $_POST['aparelho'];
$funcao = $_POST['funcao'];

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

if($nada){
	
$query3 = "INSERT INTO hora (agora) VALUES ('$nada')" ;
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