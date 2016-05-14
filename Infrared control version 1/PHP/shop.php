



<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>HTML5 Navigation Bar Example - Example 1</title>
    <link href="style2.css" rel="stylesheet"/>
	<meta name="viewport" content="width=device-width, initial-scale=0.7" />
	
	<!-- html5.js for IE less than 9 -->
<!--[if lt IE 9]>
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<!-- css3-mediaqueries.js for IE less than 9 -->
<!--[if lt IE 9]>
	<script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script>
<![endif]-->
	
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


<div id="form-main">
  <div id="form-div">
    <form class="form" id="form1">
      
      <h2>Cadastro</h2>
	  
	  <form action="action_page.php">
  <input list="browsers" name="browser">
  <datalist id="browsers">
    <option value="nada aqui">

  </datalist>
  <input type="submit">
</form>

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
$row = mysql_fetch_array($recebido);

?>

<form method="POST" action="cadastro.php" autocomplete = "off">
	  
	  <p class="name">
        <input name="nomedoaparelho" type="text" class="validate[required,custom[onlyLetter],length[0,100]] feedback-input" placeholder="ex: tv sansung sala" id="nomedoaparelho" />
      </p>
      
      <p class="name">
        <input name="funcao" type="text" class="validate[required,custom[onlyLetter],length[0,100]] feedback-input" placeholder="ex: ligar ar" id="funcao" />
      </p>
	<h3>Codigo recebido do IR Control(recarregue a pagina):</h3>
	     <p class="name">
        <input name="codigo" type="text" class="validate[required,custom[onlyLetter],length[0,100]] feedback-input" placeholder="ex: 657DF4" id="codigo" value="<?php   echo $row['idcodigos'];   ?>" />
		
		
      </p>


      
      
	    <button onclick="myFunction()">Cadastrar </button>
      
      
    </form>
	
	
	
  </div>
  
  

  
  
</body>
</html>
