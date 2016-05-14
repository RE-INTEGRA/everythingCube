

<!DOCTYPE html>
<html>
<head lang="en">
    <meta charset="UTF-8">
    <title>Histórico</title>
    <link href="style2.css" rel="stylesheet"/>
	<meta name="viewport" content="width=device-width, initial-scale=0.8" />
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
            <a href="shop.php">Cadastrar controle remoto</a>
    
        </li>
     		  <li>
            <a href="adm.php">Administrador</a>
    
        </li>
						  <li>
            <a href="sair.php">Sair</a>
    
        </li>
    </ul>
</nav>
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
 
//execute a mysql query to retrieve all the users from users table
//if  query  fails stop further execution and show mysql error
$query=mysql_query("SELECT * FROM historico WHERE user='$user'") or die(mysql_error());
 
//if we get any results we show them in table data
if(mysql_num_rows($query)>0):
 
?>
 
<table id="t01">
  <tr>
    <td align="center">Id</td>
    <td align="center">Codigo IR</td>
    <td align="center">Função</td>
  <td align="center">Aparelho</td>
  <td align="center">Horário</td>
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
<h3>Sem dados no histórico</h3>
<?php endif; ?>
</body>
</html>