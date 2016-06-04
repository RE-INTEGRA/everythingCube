

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


$id3 = $_POST['id3'];
$tab = $_POST['tab'];
$senha = $_POST['senha'];

$codigo = 0;

include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query4 = "SELECT `usuarios`.`senha` FROM `$nomedb`.`usuarios` WHERE email= '$user' ;";
$recebido3 = mysql_query($query4,$connect);
$row3 = mysql_fetch_array($recebido3);

$nada1 =  $row3 [0];

$query5 = "SELECT `cadastro`.`idcadastro` FROM `$nomedb`.`cadastro` WHERE user= '$user' AND idcadastro='$id3' ;";
$recebido4 = mysql_query($query5,$connect);
$row4 = mysql_fetch_array($recebido4);

$nada2 =  $row4 [0];


$query6 = "SELECT `bloqueio`.`idbloqueio` FROM `$nomedb`.`bloqueio` WHERE user= '$user' AND idbloqueio ='$id3' ;";
$recebido5 = mysql_query($query6,$connect);
$row5 = mysql_fetch_array($recebido5);

$nada3 =  $row5 [0];

$query7 = "SELECT `funcoescontroladas`.`idfuncoescontroladas` FROM `$nomedb`.`funcoescontroladas` WHERE user= '$user' AND idfuncoescontroladas='$id3'  ;";
$recebido6 = mysql_query($query7,$connect);
$row6 = mysql_fetch_array($recebido6);

$nada4 =  $row6 [0];

$query8 = "SELECT `historico`.`idhistorico` FROM `$nomedb`.`historico` WHERE user= '$user' AND idhistorico='$id3' ;";
$recebido7 = mysql_query($query8,$connect);
$row7 = mysql_fetch_array($recebido7);

$nada5 =  $row7 [0];


if($senha == $nada1){
	
	
if($nada2 == $id3 or $id3 == "tudo" ){
	if($tab == "Controles Cadastrados"){
	
	if($id3 != "tudo"){
	
$query3 = "DELETE FROM cadastro WHERE idcadastro = '$id3';" ;
$insert = mysql_query($query3,$connect);	
	}
	
	if($id3 == "tudo" or $id3 == "tudo"){
		$query3 = "DELETE FROM cadastro WHERE user='$user'  ;" ;
$insert = mysql_query($query3,$connect);
		
	}

}
	
}

if($nada3 == $id3 or $id3 == "tudo"){
	
	if($tab == "Bloqueios Cadastrados"){
	
	if($id3 != "tudo"){
	
$query1 = "DELETE FROM bloqueio WHERE  user='$user' AND  idbloqueio = '$id3' ;" ;
$insert = mysql_query($query1,$connect);	
	}
	if($id3 == "tudo"){
		
		$query1 = "DELETE FROM bloqueio WHERE user='$user';" ;
$insert = mysql_query($query1,$connect);
	}
	
}


	
}


if($nada4 == $id3 or $id3 == "tudo"){
if($tab == "Funcões Cadastradas"){
	
	if($id3 != "tudo"){
	
$query2 = "DELETE FROM funcoescontroladas WHERE idfuncoescontroladas = '$id3';" ;
$insert = mysql_query($query2,$connect);	
	}
	
	if($id3 == "tudo"){
	$query2 = "DELETE FROM funcoescontroladas WHERE user='$user'  ;" ;
$insert = mysql_query($query2,$connect);	
	}

}
	
}

if($nada5 == $id3 or $id3 == "tudo"){

if($tab == "Historico"){
	
	if($id3 != "tudo"){
	
$query4 = "DELETE FROM historico WHERE idhistorico = '$id3';" ;
$insert = mysql_query($query4,$connect);	
	}
	
		if($id3 == "tudo"){
	
$query4 = "DELETE FROM historico WHERE user='$user'  ;" ;
$insert = mysql_query($query4,$connect);	
	}
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