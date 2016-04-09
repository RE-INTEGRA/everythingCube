

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


//connect to mysql server with host,username,password
//if connection fails stop further execution and show mysql error
$connection=mysql_connect($finalcod,$usuario,$senhaserv) or die(mysql_error());
//select a database for given connection
//if database selection  fails stop further execution and show mysql error
mysql_select_db($nomedb,$connection) or die(mysql_error());
 
//execute a mysql query to retrieve all the users from users table
//if  query  fails stop further execution and show mysql error
$query=mysql_query("SELECT * FROM historico") or die(mysql_error());
 
//if we get any results we show them in table data
if(mysql_num_rows($query)>0):
 
?>
 
<table id="t01">
  <tr>
    <td align="center">Id</td>
    <td align="center">codigo IR</td>
    <td align="center">Funcão</td>
  <td align="center">Aparelho</td>
  <td align="center">Horario</td>
  </tr>
  <?php 
  //while we going through each row we display info
  while($row=mysql_fetch_object($query)):?>
  <tr>
    <td align="center"><?php echo $row->idhistorico;  //row id ?></td>
    <td align="center"><?php echo $row->codigo; // row first name ?></td>
    <td align="center"><?php echo $row->funcao; //row las tname  ?></td>
<td align="center"><?php echo $row->aparelho; //row las tname  ?></td>
<td align="center"><?php echo $row->hora; //row las tname  ?></td>
  </tr>
  <?php endwhile;?>
</table>
<?php 
//if we can't get results we show information
else: ?>
<h3>Sem dados no historico</h3>
<?php endif; ?>
</body>
</html>