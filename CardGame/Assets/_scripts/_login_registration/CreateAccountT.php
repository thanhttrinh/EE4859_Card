<?php
//PHP only
$hostName = "localhost";
$dbName = "id4819006_users";
$user = "id4819006_tinycivs";
$pwdDB = "Tinycivs2018";

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
        $subject = "Welcome to Tiny Civs";

       /* $message = "Thank you for registering with us, ". $userName ."!
                    \n Please keep your infomation safe.\n \n Username: ". $userName . "\n Password: ". $pwd ."\n";*/
        $message = "Thank you for registering with us, ".$userName."!<br>";
        $message = "Please keep your information safe.<br><br><br>";
        $message = "Login Email: ".$email."<br>";
        $message = "Username: ".$userName."<br>";
        $message = "Password: ".$pwd."<br>";
        mail($email, $subject, $message);

        //send message to c# that user successfully registered
        echo "success";
    }
}
//close connection to database
mysqli_close($con);
?>