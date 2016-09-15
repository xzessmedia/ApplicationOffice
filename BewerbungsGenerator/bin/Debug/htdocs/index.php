<?php
require_once 'config.php';
require_once 'bewerbung.php';
session_start();



$bewerbung = new Bewerbung();

// Action Handler ohne Token
$action = $_GET['action'];
if (isset($action)) {
	switch ($action) {
		case "contact":
		$name 		= $_POST['name'];
		$email	 	= $_POST['email'];
		$datum 		= $_POST['vorstellungsdatum'];
		$time	 	= $_POST['vorstellungszeit'];
		$infomsg 	= $_POST['message'];
		
		$result = $bewerbung->Sendmail($name,$email,$infomsg,$datum,$time,"tim.koepsel@me.com");
		
		if ($result == true) {
			$bewerbung->ShowInfoBox("Info","<p>Bewerber wurde kontaktiert und über den Termin informiert! Vielen Dank für das gegenseitige Interesse!</p>");
		}else {
			$bewerbung->ShowInfoBox("Achtung","<p>Nachricht konnte aus unbekannten Gründen nicht abgesendet werden!</p>");
		}
		
		break;
		case "generate":
		$filename 	= $_GET['filename'];
		
		if(!isset($filename))
		{
			echo "Specify filename in adressbar like this: index.php?action=generate&filename=MeineBewerbung";
		} else {
			
			$ddata = $bewerbung->GenerateTokenForData($filename);

			
			$edata = $bewerbung->decrypt($ddata);
			echo '<h1>Generated Token</h1><p>'.$ddata.'</p>';
			echo '<h2>Token Inhalt:</h2><p>'.$edata.'</p>';
		}
		
		break;	
		case "upload":
		$f 	= $_GET['file'];
		if(isset($f))
		{
			
			if($_POST['submit'])
			{
			
				$filename = $_FILES['bewerbungsmappe']['name'];
			
				$extension = strtolower(pathinfo($_FILES['bewerbungsmappe']['name'], PATHINFO_EXTENSION));
				//Überprüfung der Dateiendung
				$allowed_extensions = array('json');
				if(!in_array($extension, $allowed_extensions)) {
					die('<h1>Falsche Dateiendung. </h1><p>Nur json-Dateien sind erlaubt!</p> <a class="btn btn-primary" role="button" href="javascript: history.back()">Andere Datei hochladen</a>');
				}
				
				// Wenn Datei schon existiert, einfach überschreiben
				if(file_exists('data/'.$filename)) unlink('data/'.$filename);
				
				// Datei aus dem Temp Verzeichnis ins data Verzeichnis verschieben
				move_uploaded_file($_FILES['bewerbungsmappe']['tmp_name'], 'data/'.$_FILES['bewerbungsmappe']['name']);
				
			
			
			
		$html = '
			<h3>Upload abgeschlossen</h3>
			';
			$ddata = $bewerbung->GenerateTokenForData($filename);
			$edata = $bewerbung->decrypt($ddata);
			$html.= '<h1>Generiertes Token</h1><p>'.$ddata.'</p>';
			$html.= '<h2>Token zu File:</h2><p>'.$edata.'</p><a class="btn btn-primary" role="button" href="index.php?token='.$ddata.'">Deine Bewerbung ansehen</a>';
			
		$bewerbung->ShowInfoBox("Bewerbungsmappe hochladen",$html);	}
			else {
				$bewerbung->ShowInfoBox("Error",'Da hat was nicht geklappt!');	
			}
		} else {
			
		
		$html = '<h1>Bewerbungsmappe hochladen</h1><p>Achtung damit der Upload klappt, darf der Name der Mappe nur aus Buchstaben bestehen!</p>
			<form action="index.php?action=upload&file=true" method="post" enctype="multipart/form-data">
		Bewerbungsmappe aus /htdocs/data/ hochladen:
			    <input type="file" name="bewerbungsmappe" id="fileToUpload">
			    <input type="submit" value="Bewerbungsmappe hochladen" name="submit">
					</form>';
			$bewerbung->ShowInfoBox("Bewerbungsmappe hochladen",$html);
			
		}
		break;
	}	
}


/* Token Verarbeitung  / Bewerbung nur verarbeiten bei Token*/
$token = $_GET['token'];
if (isset($token)) {

	$jsonfile = $bewerbung->decrypt($token);
		
		
		$url =  $siteurl.'/data/'.$jsonfile;
		if (filter_var($url, FILTER_VALIDATE_URL) === FALSE) {
			die('Token is incorrect, no authorisation granted!');
		}

		$content = file_get_contents($url);
		$json = json_decode($content);
		$json_array = json_decode($content,true);
		
		$applicant_name 		= $json->PersonalCollectionData->Applicant_FirstName.' '.$json->PersonalCollectionData->Applicant_LastName;
		$applicant_jobtitle 	= $json->PersonalCollectionData->Applicant_JobTitle;
		$applicant_adress		= $json->PersonalCollectionData->Applicant_Street.' '.$json->PersonalCollectionData->Applicant_HouseNumber.', '.$json->PersonalCollectionData->Applicant_ZipCode.' '.$json->PersonalCollectionData->Applicant_City;
		$applicant_birthdate 	= $json->PersonalCollectionData->Applicant_BirthDate;
		$applicant_phone 		= $json->PersonalCollectionData->Applicant_PhoneNumber;
		$applicant_mailadress	= $json->PersonalCollectionData->Applicant_MailAdress;
		$applicant_twitter		= $json->PersonalCollectionData->Applicant_Twitter;
		$applicant_facebook		= $json->PersonalCollectionData->Applicant_Facebook;
		$applicant_xing			= $json->PersonalCollectionData->Applicant_Xing;
		$applicant_github		= $json->PersonalCollectionData->Applicant_Github;
		$applicant_picture		= $json->PersonalCollectionData->Applicant_Picture;
		$contactname			= $json->PersonalCollectionData->Applicant_ContactFirstName.' '.$json->PersonalCollectionData->Applicant_ContactLastName;
		$contactgender			= $json->PersonalCollectionData->Applicant_ContactGender;
		$introtext				= $json->CollectionContentData->IntroText;
		$portfoliodata			= $json->CollectionContentData->RefData;
		$aboutdata				= $json->CollectionContentData->PersonalText;
		
		$cvdata_lebenslauf 		= "";
		$cvdata_abschluesse		= "";
		
		
		// Json Array durchparsen und lebenslauf und abschluesse arrays raussuchen
		foreach($json_array  as $row)
		{
			foreach($row as $key => $val)
			{
				
				if($key=='CVData')
				{
					$cvdata_lebenslauf = $val['LebenslaufItems'];	
					$cvdata_abschluesse = $val['AbschluesseItems'];
				}
				
				
				
			}
			
		}

		$bewerbung->GenerateApplication($url, $contactname, $contactgender, $applicant_picture, $applicant_name,$applicant_jobtitle,$applicant_birthdate,$applicant_adress, $applicant_phone,$applicant_mailadress,$applicant_twitter,$applicant_facebook,$applicant_github,$applicant_xing, $introtext,$cvdata_lebenslauf, $cvdata_abschluesse, $portfoliodata,$aboutdata); 
		
		
	
	
} else {
	if (isset($action)) {
		
	} else {
		$bewerbung->ShowInfoBox("Achtung", "Du hast leider keine Authorisation!");
	}
		
}




?>