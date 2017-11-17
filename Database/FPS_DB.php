<?php
  $server = "localhost";
  $database = "FPS";
  $username = "23973_admin";
  $password = "admin";
  $tablename = $REQUEST["tableName"];
  $player_name = $_REQUEST["playerName"];
  $score = $_REQUEST["score"];
  $target_name = $_REQUEST["targetName"];
  $target_x = $_REQUEST["targetX"];
  $target_y = $_REQUEST["targetY"];
  $target_z = $_REQUEST["targetZ"];
  $target_pos = "[" . $target_x . ", " . $target_y . ", " . $target_z . "]";
  // echo $target_name;
  // echo $target_x;
  // echo $target_y;
  // echo $target_z;
  // echo $target_pos;
  $connection = mysqli_connect($server, $username, $password, $database);
  if ($connection) {
    echo "Connection succeeded <br />";
    echo $table_name;
  } else {
    echo "".mysqli_error($connection);
  }

  if ($table_name == "ScoreLog")
  {
    $query = "INSERT INTO `FPS`.`ScoreLog` (`id`, `player_name`, `score`, `time_stamp`)
              VALUES (NULL, '$player_name', '$score', CURRENT_TIMESTAMP)";
  }
  else
  {
    $query = "INSERT INTO `FPS`.`TargetPosLog` (`id`, `target_name`, `target_pos`, `time_stamp`)
              VALUES (NULL, '$target_name', '$target_pos', CURRENT_TIMESTAMP)";
  }

  $result = mysqli_query($connection, $query);
  if ($result) {
    echo "Query succeeded, entry added <br />";
    echo "result: " + $result;
  } else {
    echo "".mysqli_error($connection);
  }
 ?>
