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
        $username = $_POST["set_username"];
        $password = $_POST["set_password"];
        $email = $_POST["set_email"];

        //getting data from the database
        $sqlCheck = "SELECT username FROM users WHERE username = '" .$username. "' ";
        $resultCheck = mysqli_query($connection, $sqlCheck);

        //if connection fails, display a message
        if(!$connection)
        {
            //exit
            die("Connection Failed.".mysql_connect_error());
            echo("Failed");
        }
        else
        {
            if($resultCheck)
            {
                if(mysqli_num_rows($resultCheck) < 1)
                {
                    //insert data into the database
                    $sqlSet = "INSERT INTO users (username, password, email) 
                        VALUES ('".$username."','".$password."', '".$email." )";

                    $resultSet = mysqli_query($connection, $sqlSet);

                    if($resultSet)
                    {
                        echo("Success");
                    }
                    else
                    {
                        echo("Failed");
                    }
                }
                else
                {
                    echo("Existed");
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