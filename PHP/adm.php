<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>HTML5 Navigation Bar Example - Example 1</title>
    <link href="style2.css" rel="stylesheet"/>
	<meta name="viewport" content="width=device-width, initial-scale=0.8" />
</head>
<body>

<nav>
    <ul>
        <li>
            <a href="home.php">Home</a>
     
        </li>
        <li>
            <a href="oi.php">Historicos</a>
      
        </li>
        <li>
            <a href="shop.php">Cadastrar controlhes remotos</a>
    
        </li>
     		  <li>
            <a href="adm.php">Admin</a>
    
        </li>
    </ul>
</nav>
<?php 
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



$connection=mysql_connect($finalcod,$usuario,$senhaserv) or die(mysql_error());

mysql_select_db($nomedb,$connection) or die(mysql_error());
 
//execute a mysql query to retrieve all the users from users table
//if  query  fails stop further execution and show mysql error
$query=mysql_query("SELECT * FROM bloqueio") or die(mysql_error());
 
//if we get any results we show them in table data
if(mysql_num_rows($query)>=0):

$query2=mysql_query("SELECT * FROM funcoescontroladas") or die(mysql_error());
 
//if we get any results we show them in table data
if(mysql_num_rows($query2)>=0):


$query3=mysql_query("SELECT * FROM cadastro") or die(mysql_error());
 
//if we get any results we show them in table data
if(mysql_num_rows($query2)>=0):

 
?>
 <h2>Bloqueios Cadastrados</h2>
 <br>
<table id="t01",name="bloqueio">
  <tr>
    <td align="center">Id</td>
    <td align="center">codigo IR</td>
    <td align="center">Horario Inicial</td>
  <td align="center">Horario Final</td>
  <td align="center">codigo a realizar</td>
  </tr>
  <?php 
  //while we going through each row we display info
  while($row=mysql_fetch_object($query)):?>
  <tr>
    <td align="center"><?php echo $row->idbloqueio;  //row id ?></td>
    <td align="center"><?php echo $row->codigo; // row first name ?></td>
    <td align="center"><?php echo $row->horarioDebloqueioInicial; //row las tname  ?></td>
<td align="center"><?php echo $row->horarioDebloqueioFinal; //row las tname  ?></td>
<td align="center"><?php echo $row->funcaoAodetectarBloqueio; //row las tname  ?></td>
  </tr>
  <?php endwhile;?>
</table>
<?php 
//if we can't get results we show information
else: ?>
<h3>Sem dados de bloqueio</h3>
<?php endif; ?>



 <h2>Funcões Cadastradas</h2>
 <br>
<table id="t01" ,name="bloqueio"/>
  <tr>
    <td align="center">Id</td>
    <td align="center">Horario</td>
    <td align="center">Codigos</td>

  </tr>
  <?php 
  //while we going through each row we display info
  while($row2=mysql_fetch_object($query2)):?>
  <tr>
    <td align="center"><?php echo $row2->idfuncoesControladas;  //row id ?></td>
    <td align="center"><?php echo $row2->horario; // row first name ?></td>
    <td align="center"><?php echo $row2->idcodigos; //row las tname  ?></td>

  </tr>
  <?php endwhile;?>
</table>
<?php 
//if we can't get results we show information
else: ?>
<h3>Sem dados de funções</h3>
<?php endif; ?>


<h2>Controles Cadastrados</h2>
 <br>
<table id="t01" ,name="cad"/>
  <tr>
    <td align="center">Id</td>
    
    <td align="center">Codigos</td>
<td align="center">Aparelho</td>
<td align="center">Função</td>
  </tr>
  <?php 
  //while we going through each row we display info
  while($row3=mysql_fetch_object($query3)):?>
  <tr>
    <td align="center"><?php echo $row3->idcadastro;  //row id ?></td>
    <td align="center"><?php echo $row3->codigo; // row first name ?></td>
    <td align="center"><?php echo $row3->aparelho; //row las tname  ?></td>
<td align="center"><?php echo $row3->funcao; //row las tname  ?></td>
  </tr>
  <?php endwhile;?>
</table>
<?php 
//if we can't get results we show information
else: ?>
<h3>Sem dados de funções</h3>
<?php endif; ?>




<p>Qual tabela deseja apagar</p>

<form method="POST" action="config.php" autocomplete = "off">



  <input list="tab" name="tab", placeholder="Selecione o codigo da tabela">
<datalist id="tab">
 <option value="Bloqueios Cadastrados">
  <option value="Funcões Cadastradas">
   <option value="Controles Cadastrados">
   <option value="Historico">

 </datalist>
 
 <p>Id da linha que deseja apagar</p>
 <p class="name">
        <input name="id3" type="text"  placeholder="Digite o id" id="id3"  />
      </p>
	  <p class="name">
        <input name="senha" type="text"  placeholder="senha de admin" id="senha" />
      </p>
	  <button onclick="myFunction()">Apagar </button>
	  </form>
	  

 


</body>
</html>