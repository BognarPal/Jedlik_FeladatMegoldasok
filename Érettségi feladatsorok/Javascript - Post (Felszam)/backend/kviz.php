<?php
    error_reporting( E_ERROR | E_COMPILE_ERROR );

    class JSON {
        public static function decode($string) {
            if (is_string($string) && (is_object(json_decode($string)) || is_array(json_decode($string)))) 
                return json_decode($string);
            
            throw new Exception("Not JSON input");
        } 
    }

    class Headers {
        public static function setAccessControl() {
            // 2018-05-15. részletek: https://stackoverflow.com/questions/8719276/cors-with-php-headers
            // Access-Control headers are received during OPTIONS requests
            
            $http_origin = $_SERVER['HTTP_ORIGIN'];
            header("Access-Control-Allow-Origin: *");
            header('Access-Control-Allow-Credentials: true');
            if ($_SERVER['REQUEST_METHOD'] == 'OPTIONS') {
                header('Access-Control-Allow-Methods: GET, POST, OPTIONS, PUT, DELETE');
                header('Access-Control-Allow-Headers: login, token, Content-Type');
                header('Access-Control-Max-Age: 1728000');
                header('Content-Length: 0');
                header('Content-Type: text/plain');
                die();
            }
        }
    }


	Headers::setAccessControl();

	if (!isset($_SERVER["PATH_INFO"])) {
		echo ("502-es hiba (Internal Server Error)");
		header(("502-es hiba (Internal Server Error)"), true, 501);
		die();
	}

    try {
        if ($_SERVER['REQUEST_METHOD'] == 'GET') {
			if ($_SERVER["PATH_INFO"] == '/kategoriak') {
                $kategoriak = [];
				$file = fopen("felszam.txt","r");
                while(!feof($file)) {
                    $kerdes = fgets($file);
                    $masodiksor = fgets($file);
                    $kategoria = explode(" ", str_replace("\r\n","",$masodiksor))[2];
                    if (!in_array($kategoria, $kategoriak)) {
                        array_push($kategoriak, $kategoria);
                    }
                }
                fclose($file);
                /*$result = [];
                foreach ($kategoriak as $kategoria) {
                    $x->kategoria = $kategoria;
                    array_push($result, $x);
                    $x = null;
                }*/
                echo json_encode($kategoriak);
			}				
		}
        if ($_SERVER['REQUEST_METHOD'] == 'POST') {
			if ($_SERVER["PATH_INFO"] == '/kerdes') {
				try {
                    $input = JSON::Decode(file_get_contents('php://input'));
                } catch (Exception $e) {
                    $error->error = $e->getMessage();
                    echo json_encode($error);
                    header($e->getMessage(), true, 501);
                    die();
                }

                $kerdesek = [];
				$file = fopen("felszam.txt","r");
                while(!feof($file)) {
                    $kerdes = str_replace("\r\n","",fgets($file));
                    $masodiksor = fgets($file);
                    $kategoria = explode(" ", str_replace("\r\n","",$masodiksor))[2];                                       
                    if ($kategoria == $input->kategoria) {
                        array_push($kerdesek, $kerdes);
                    }
                }
                fclose($file);
                $index = rand(0, count($kerdesek) - 1);
                $result->kerdes = $kerdesek[$index];
                echo json_encode($result);
            }	
            if ($_SERVER["PATH_INFO"] == '/ellenorzes') {
				try {
                    $input = JSON::Decode(file_get_contents('php://input'));
                } catch (Exception $e) {
                    $error->error = $e->getMessage();
                    echo json_encode($error);
                    header($e->getMessage(), true, 501);
                    die();
                }

                $kerdesek = [];
				$file = fopen("felszam.txt","r");
                while(!feof($file)) {
                    $kerdes = str_replace("\r\n","",fgets($file));
                    $masodiksor = fgets($file);
                    $valasz = explode(" ", str_replace("\r\n","",$masodiksor))[0];                                       
                    if ($kerdes == $input->kerdes) {                        
                        $result->helyes = $valasz == $input->valasz;
                        if (!$result->helyes) {
                            $result->helyesvalasz = $valasz;
                        }
                    }
                }
                fclose($file);
                echo json_encode($result);
			}			
        }		
	} catch (Exception $e) {
		$error->error = $e->getMessage();
        echo json_encode($error);
		header($e->getMessage(), true, 502); 
		die();
    }
    

?>