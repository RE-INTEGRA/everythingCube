

<?php 

$id3 = $_POST['id3'];
$tab = $_POST['tab'];
$senha = $_POST['senha'];

$codigo = 0;

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

$query0 = "SELECT senha  FROM senha ; ";
$recebido1 = mysql_query($query0,$connect);
$row0 = mysql_fetch_array($recebido1);

$nada1 =  $row0 [0];

if($senha == $nada1){

if($tab == "Bloqueios Cadastrados"){
	
	if($id3 != "tudo"){
	
$query1 = "DELETE FROM bloqueio WHERE idbloqueio = '$id3';" ;
$insert = mysql_query($query1,$connect);	
	}
	if($id3 == "tudo"){
		
		$query1 = "DELETE FROM bloqueio WHERE idbloqueio;" ;
$insert = mysql_query($query1,$connect);
	}
	
}

if($tab == "Funcões Cadastradas"){
	
	if($id3 != "tudo"){
	
$query2 = "DELETE FROM funcoescontroladas WHERE idfuncoescontroladas = '$id3';" ;
$insert = mysql_query($query2,$connect);	
	}
	
	if($id3 == "tudo"){
	$query2 = "DELETE FROM funcoescontroladas WHERE idfuncoescontroladas ;" ;
$insert = mysql_query($query2,$connect);	
	}

}

if($tab == "Controles Cadastrados"){
	
	if($id3 != "tudo"){
	
$query3 = "DELETE FROM cadastro WHERE idcadastro = '$id3';" ;
$insert = mysql_query($query3,$connect);	
	}
	
	if($id3 == "tudo"){
		$query3 = "DELETE FROM cadastro WHERE idcadastro ;" ;
$insert = mysql_query($query3,$connect);
		
	}

}
if($tab == "Historico"){
	
	if($id3 != "tudo"){
	
$query4 = "DELETE FROM historico WHERE idhistorico = '$id3';" ;
$insert = mysql_query($query4,$connect);	
	}
	
		if($id3 == "tudo"){
	
$query4 = "DELETE FROM historico WHERE idhistorico ;" ;
$insert = mysql_query($query4,$connect);	
	}
}

 if($insert){
					
                    echo"<script language='javascript' type='text/javascript'>alert('tabela:$tab,id: $id3, foram apagados');window.location.href='adm.php'</script>";
                }else{
                    echo"<script language='javascript' type='text/javascript'>alert('Não foi possível fazer nada, digite os ids certos');window.location.href='adm.php'</script>";
                }



}else{
                    echo"<script language='javascript' type='text/javascript'>alert('senha de admin errada');window.location.href='adm.php'</script>";
                }
				
                 
                
mysql_close($connect);
?>