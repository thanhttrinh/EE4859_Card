<?php
//PHP only
$hostName = "sql9.freesqldatabase.com";
$dbName = "sql9222374";
$user = "sql9222374";
$pwdDB = "7BqezUyEub";

//open connection to database
$con = mysqli_connect($hostName, $user, $pwdDB, $dbName) or die("Cannot connect to Database");
mysqli_select_db($con, $dbName) or die("Cannot connect to Database");

//email, user and password
$email = $_POST["email"];
$pwd = $_POST["password"];

if(!$email || !$pwd){
    echo "Login or password cannot be empty";
}
else{
    //search database by email
    $checkEmail = "SELECT * FROM users WHERE email = '". $email ."'";
    $result = mysqli_query($con, $checkEmail) or die("Database Error");
    $emailExist = mysqli_num_rows($result);

    
    
    if($emailExist == 0){
       echo"Email does not Exist\n";
    }
    else{
        while($row = mysqli_fetch_assoc($result)){
            if($pwd == $row['password']){
                echo"success";
            }
            else{
                echo"Password does not match\n";
                echo $row['username'], "\n", $row['email'], "\n", $row['password'], "\n", $pwd;
            }
        }
    }
}
//close database
mysqli_close($con);
?>