

<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>Home</title>
    <link href="style2.css" rel="stylesheet"/>
	<meta name="viewport" content="width=device-width, initial-scale=0.8" />
<link rel="icon" href="favicon.ico" type="/">	
</head>
<body>

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

?>


<div id="pagewrap">

	<header id="header">

<nav>
    <ul>
        <li>
            <a href="home.php">Home</a>
     
        </li>
        <li>
            <a href="oi.php">Históricos</a>
      
        </li>
        <li>
            <a href="shop.php">Cadastrar controle remoto</a>
    
        </li>
		
		  <li>
            <a href="adm.php">Administrador</a>
    
        </li>				 

		
				  <li>
            <a href="sair.php">Sair</a>
    
        </li>
		
		<li>
	 <a href="/d/index.html">


 
 <tr>
    <td><img  width="30" height="30" src='olho.jpg'/></td>
    
    <td> Controle visual versão teste</td> 
  </tr>
 
 </a>
</li>
     
    </ul>
</nav>



</header>



<center>  

<aside id="sidebar">

<form method="POST" action="fazer.php" autocomplete = "off">
<h1>Controle</h1>
<p>Qual o aparelho desejado </p>

  <input list="browserss" name="aparelho">
  <datalist id="aparelho">
 <?php 

$qry=mysql_query("SELECT DISTINCT `cadastro`.`aparelho` FROM `$nomedb`.`cadastro` WHERE user = '$user';");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[aparelho]'>";
}
?>
  </datalist>
  <p>Qual a função desejada</p>
    <input list="browsers2" name="funcao">
  <datalist id="funcao">
    <?php 

$qry=mysql_query("SELECT DISTINCT`cadastro`.`funcao` FROM `$nomedb`.`cadastro` WHERE user = '$user';");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[funcao]'>";
}
?>
  </datalist>
  

  <br>
  <br>
  <button onclick="myFunction()">Enviar </button>
  
  
 
 </form>


</center>
</aside>


<section>
<center>
<div id="content">
<article>

<header>
<form method="POST" action="bloq.php" autocomplete = "off">
<h2>Bloqueio de função</h2>

</header>
<p>Qual aparelho deseja bloquear</p>





  <input list="browserss" name="aparelho1">
<datalist id="aparelho1">
 <?php 
$qry=mysql_query("SELECT DISTINCT`cadastro`.`aparelho` FROM `$nomedb`.`cadastro` WHERE user = '$user';");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[aparelho]'>";
}
?>
  </datalist>
  <p>Qual função deseja bloquear</p>
    <input list="browsers2" name="funcao1">
  <datalist id="funcao1">
    <?php 

$qry=mysql_query("SELECT DISTINCT`cadastro`.`funcao` FROM `$nomedb`.`cadastro` WHERE user = '$user';");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[funcao]'>";
}
?>
  </datalist>
  
  <p>Horario de bloqueio</p>
  <p>ex: de 23:00 ate 06:30</p>
<input id="inicio" name="inicio" type="time">

<input id="final" name="final" type="time">
  
  <p>O que fazer quando o bloqueio for detectado?</p>
    <input list="browsers2" name="funcaofazer">
  <datalist id="funcaofazer">
    <?php 

$qry=mysql_query("SELECT funcao From cadastro");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[funcao]'>";
}
?>
  </datalist>

<button onclick="myFunction()">Bloquear </button>

</form>

<br><br>
	  
	  </center>
</article>

<article>
<center>

</header>
<form method="POST" action="confusao.php" autocomplete = "off">
<h2>Progamar função</h2>

<p>Qual o aparelho deseja progamar</p>

  <input list="browserss" name="browserss">
  <datalist id="browserss">
 <?php 

$qry=mysql_query("SELECT DISTINCT`cadastro`.`aparelho` FROM `$nomedb`.`cadastro` WHERE user = '$user';");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[aparelho]'>";
}
?>
  </datalist>
  <p>Qual função deseja progamar</p>
    <input list="browsers2" name="browsers2">
  <datalist id="browsers2">
    <?php 

$qry=mysql_query("SELECT DISTINCT`cadastro`.`funcao` FROM `$nomedb`.`cadastro` WHERE user = '$user';");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[funcao]'>";
}
?>
  </datalist>
  <p>Hora desejada para executar a função</p>

  <p>ex: 23:00</p>
<input type="time" name="usr_time">



<br>
<br>


<button onclick="myFunction()">Configurar Ligamento </button>
</form>
</center>
</article>

</div>
</section>






</div>


</body>
</html>