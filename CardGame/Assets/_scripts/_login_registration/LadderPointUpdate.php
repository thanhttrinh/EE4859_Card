<?php
//PHP only
$hostName = "localhost";
$dbName = "id4819006_users";
$user = "id4819006_tinycivs";
$pwdDB = "Tinycivs2018";

//open connection to database
$con = mysqli_connect($hostName, $user, $pwdDB, $dbName) or die("Cannot connect to Database");
mysqli_select_db($con, $dbName) or die("Cannot connect to Database");

//get currentLP from LadderPoints.cs
$currentLP = $_POST["currentLP"];
$Username = $_POST["Username"];

if(!$Username){
    echo "Username is empty";
}
else{
    //update database by username
    $updateLP = "UPDATE users SET ladder='".$currentLP."' WHERE username='".$Username."'";

    if(mysqli_query($con, $updateLP)){
        echo "updateSuccess";
    }
    else{
        echo "updateUnsucessful";
    }
}
//close database
mysqli_close($con);
?>