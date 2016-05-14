

<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>HTML5 Navigation Bar Example - Example 1</title>
    <link href="style2.css" rel="stylesheet"/>
	<meta name="viewport" content="width=device-width, initial-scale=0.8" />

</head>
<body>

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

?>


<div id="pagewrap">

	<header id="header">

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



</header>



<center>
<aside id="sidebar">


<h1>controle</h1>
<p>Qual o aparelho deseja </p>
<form method="POST" action="fazer.php" autocomplete = "off">
  <input list="browserss" name="aparelho">
  <datalist id="aparelho">
 <?php 

$qry=mysql_query("SELECT aparelho From cadastro");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[aparelho]'>";
}
?>
  </datalist>
  <p>qual a funcao que dejeta</p>
    <input list="browsers2" name="funcao">
  <datalist id="funcao">
    <?php 

$qry=mysql_query("SELECT funcao From cadastro");
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
<h2>Bloqueio de funcao</h2>

</header>
<p>Qual o aparelho deseja bloquear</p>

<form method="POST" action="bloq.php" autocomplete = "off">



  <input list="browserss" name="aparelho1">
<datalist id="aparelho1">
 <?php 
$qry=mysql_query("SELECT aparelho From cadastro");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[aparelho]'>";
}
?>
  </datalist>
  <p>qual a funcao que dejeta bloquear</p>
    <input list="browsers2" name="funcao1">
  <datalist id="funcao1">
    <?php 

$qry=mysql_query("SELECT funcao From cadastro");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[funcao]'>";
}
?>
  </datalist>
  
  <p>horario de bloqueio</p>
  <p>ex: de 23:00 ate 06:30</p>
<input id="inicio" name="inicio" type="time">

<input id="final" name="final" type="time">
  
  <p>qual a funcao fazer quando bloqueio detectado</p>
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

<h2>Progamar funcao</h2>
</header>
<p>Qual o aparelho deseja progamar</p>
<form method="POST" action="confusao.php" autocomplete = "off">
  <input list="browserss" name="browserss">
  <datalist id="browserss">
 <?php 

$qry=mysql_query("SELECT aparelho From cadastro");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[aparelho]'>";
}
?>
  </datalist>
  <p>qual a funcao que dejeta progamar</p>
    <input list="browsers2" name="browsers2">
  <datalist id="browsers2">
    <?php 

$qry=mysql_query("SELECT funcao From cadastro");
while ($t=mysql_fetch_array($qry)) {
echo "<option value='$t[funcao]'>";
}
?>
  </datalist>
  <p>hora que deseja executar funcao</p>

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