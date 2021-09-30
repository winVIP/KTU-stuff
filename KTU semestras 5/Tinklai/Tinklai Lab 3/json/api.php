<?php
// apipvz.php: paima iš lenteles "zinutes" paskutinius n ('kiek') į nurodytą miestą ('vardas') įrašus 
// Jei vardas nenurodyta =visi miestai, jei kiek nenurodyta=visi įrašai
//
header("Content-Type: application/json; charset=UTF-8");

//$vardas="Ryga";$kiek="3";// autonominiam testavimui
$vardas=$_GET['vardas']; 
$kiek=$_GET['kiek'];
//error_log ("Kreipinys: ".$vardas." ".$kiek); //derinimui jei negaunam atsakymo: kokios atėjo užklausos reikšmės?

$server = "localhost";
$user = "root";
$password = "";
$db = "stud";
$table = "zinutes";
		$dbc=new mysqli($server,$user,$password,$db);
		if($dbc->connect_error){
			die ("Negaliu prisijungti prie MySQL:"	.$dbc->connect_error);}
$sql = ("SET CHARACTER SET utf8"); $dbc->query($sql);      // del lietuviškų raidžių

// suformuojam sql užklausą pagal parametrus $vardas ir paskutinius $kiek
if (!empty($kiek))$sql="SELECT * FROM ( ";
else $sql="";
$sql= $sql."SELECT * FROM $table ";
if (!empty($vardas))$sql=$sql." WHERE vardas='".$vardas."'";
if (!empty($kiek))$sql=$sql." ORDER BY id DESC LIMIT ".$kiek.") sub ORDER BY id ASC";
// echo $sql; die;     //autonominiam testavimui kaip atrodo sql užklausa
$result =  $dbc->query($sql);
$outp = $result->fetch_all(MYSQLI_ASSOC);
echo json_encode($outp);

?>