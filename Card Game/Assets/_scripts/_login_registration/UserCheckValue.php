<?php
    //variables for SQL server
    $server_name = $_POST["serverName"];
    $server_username = $_POST["serverUsername"];
    $server_password = $_POST["serverPassword"];
    $server_database = $_POST["serverDatabase"];

    //variables for authentication
    $server_key = "defaultkey123";
    $server_auth = $_POST["serverKeycode"];

    if($server_auth == $server_key)
    {
        //create connection to mySQL server
        $connection = new mysqli($server_name, $server_username, $server_password, $server_database);

        //variables for user database
        $username = $_POST["check_username"];
        $password = $_POST["check_password"];
        $email = $_POST["check_email"];

        //check data
        $sql= "SELECT password, email FROM users WHERE username = '" .$username. "' ";
        $result = mysqli_query($connection, $sqlCheck);

        //if connection fails, display a message
        if(!$connection)
        {
            //exit
            die("Connection Failed.".mysql_connect_error());
            echo("Failed");
        }
        else
        {
            if($result)
            {
                //there is a colume which corresponds to the username
                if(mysqli_num_rows($result) > 0)
                {
                   while($row = mysqli_fetch_assoc($result))
                   {
                       if($row['password'] == $password)
                       {
                           echo("Success");
                       }
                       else{
                           echo("Incorrect");
                       }
                   }
                }
                else
                {
                    echo("Unknown");
                }
            }
            else
            {
                echo("Failed");
            }
        }
    }
    else
    {
        echo("Error");
    }
?>