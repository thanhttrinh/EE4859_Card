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
$userName = $_POST["username"];
$pwd = $_POST["password"];

if(!$email || !$pwd){
    echo "empty";
}
else{
    //search database by email
    $checkEmail = "SELECT * FROM users WHERE email = '". $email ."'";
    $result = mysqli_query($con, $checkEmail) or die("Database Error");
    $emailExist = mysqli_num_rows($result);

    if($emailExist > 0){
        //send message to c# that the email already exist
        echo "exist";
 
    }
    else{
        $insert = "INSERT INTO users (username, password, email) VALUES ('". $userName ."', '". $pwd ."', '". $email ."')";
        $sqlSet = mysqli_query($con, $insert);

        //send a mail to the user to document their register information
        $message = "Thank you for registering with us,". $userName ."!\n Please keep your infomation safe.\n \n \n
                    Username: ". $userName . "\n Password: ". $pwd ."\n";
        mail($email, "Welcome to Tiny Civs", $message);

        //send message to c# that user successfully registered
        echo "success";
    }
}
//close connection to database
mysqli_close($con);
?>