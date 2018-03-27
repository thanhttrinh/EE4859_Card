<?php
//PHP only
$hostName = "localhost";
$dbName = "id4819006_users";
$user = "id4819006_tinycivs";
$pwdDB = "Tinycivs2018";

//open connection to database
$con = mysqli_connect($hostName, $user, $pwdDB, $dbName) or die("Cannot connect to Database");
mysqli_select_db($con, $dbName) or die("Cannot connect to Database");

//username and ladder points
$email = $_POST["email"];
$Username = $_POST["Username"];
$LadderPoints = $_POST["LadderPoints"];

//search database by email
$checkEmail = "SELECT * FROM users WHERE email = '". $email ."'";
$result = mysqli_query($con, $checkEmail) or die("Database Error");
$emailExist = mysqli_num_rows($result);

if($emailExist == 0){
    //send message to c# that email does not exist
    echo"noExistEmail","\n";
}
else{
    //get username and ladderpoints
    /*
    $getUser = "SELECT username FROM users WHERE email = '". $email ."'";
    $getLP = "SELECT ladder FROM users WHERE email = '". $email ."'";
    $userResult = mysqli_query($con, $getUser);
    $lpResult = mysqli_query($con, $getLP);
    */
    while($row = mysqli_fetch_assoc($result))
        echo "success,".$row["username"].",".$row["ladder"];
}
?>