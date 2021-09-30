<?php
header("Content-Type: application/json; charset=UTF-8");

$server = "localhost";
$user = "root";
$password = "";
$db = "stud";
$table = "zinutes";
$failovardas = "zinutes.json";

$dbc=new mysqli($server,$user,$password,$db);
if($dbc->connect_error){
	die ("Negaliu prisijungti prie MySQL:"	.$dbc->connect_error);}
$sql = ("SET CHARACTER SET utf8"); $dbc->query($sql);      // del lietuviškų raidžių

$jsondata = file_get_contents($zinutes.json);  // nuskaityti failą į $jsondata
$array = json_decode($jsondata, true);      // konvertuoti  $jsondata  į PHP masyvą $array

foreach($array as $row) // įrašyti reikšmes iš kiekvienos eilutės   
{
	$vardas=$row["vardas"];
	$epastas=$row["epastas"];
	$kam=$row["kam"];
	$data=$row["data"];
	$ip=$row["ip"];
	$zinute=$row["zinute"];
	$sql="INSERT INTO $table (`vardas`, `epastas`, `kam`, `data`, `ip`, `zinute`) VALUES ('$vardas', '$epastas', '$kam', '$data', '$ip', '$zinute')"; 
	if (mysqli_query($dbc, $sql)) {
		echo "Įrašyta\n";
	} 
	else {
		echo "Klaida: " . $sql . "\n" . mysqli_error($dbc) . "\n\n";
	}
}


?>