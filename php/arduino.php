<?php


 
$port = fopen("COM5","w+");
if ($_POST['estado']=="a")
{
   
    fwrite($port, 'a');
}
   
  if ($_POST['estado']=="A")
{
    
    fwrite($port, 'b');
}


            
fclose($port);
?>