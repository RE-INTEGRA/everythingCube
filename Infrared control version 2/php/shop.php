



<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>Cadastro de controles</title>
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
	<link rel="icon" href="favicon.ico" type="/">	
</head>
<body>


<nav>
    <ul>
        <li>
            <a href="home.php">Home</a>
     
        </li>
        <li>
            <a href="oi.php">Históricos</a>
      
        </li>
        <li>
            <a href="shop.php">Cadastrar controles remotos</a>
    
        </li>
     		  <li>
            <a href="adm.php">Administrador</a>
    
        </li>
						  <li>
            <a href="sair.php">Sair</a>
    
        </li>
    </ul>
</nav>


<div id="form-main">
  <div id="form-div">
    <form class="form" id="form1">
      
      <h2>Cadastro</h2>
	  
	  <form action="action_page.php">



</form>

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

include 'test.php';


 if (!$connect)
	die ("Erro de conexão com localhost, o seguinte erro ocorreu -> ".mysql_error());
//conectando com a tabela do banco de dados
$banco = mysql_select_db($nomedb,$connect);

if (!$banco)
	die ("Erro de conexão com banco de dados, o seguinte erro ocorreu -> ".mysql_error());

$query2 = "SELECT idcodigos FROM codigorecebido WHERE user= '$user' ORDER BY numero DESC";
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
	<h3>Código recebido do IR Control(recarregue a página):</h3>
	     <p class="name">
        <input name="codigo" type="text" class="validate[required,custom[onlyLetter],length[0,100]] feedback-input" placeholder="ex: 657DF4" id="codigo" value="<?php   echo $row['idcodigos'];   ?>" />
		
		
      </p>


      
      
	    <button onclick="myFunction()">Cadastrar </button>
      
      
    </form>
	
	
	
  </div>
  
  

  
  
</body>
</html>
