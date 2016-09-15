# ApplicationOffice
For all your personal Applications to companies





Benutzung:
-----

```
[Bewerbung erstellen]
Öffne den Bewerbungsgenerator und erstelle deine Bewerbungsmappe.
Fülle dazu alle Felder aus!
Du kannst beim Einleitungstext, Referenzen und Personaltext HTML Code verwenden!

Wenn du mit der Mappe fertig bist, speichere deine Mappe im htdocs/data/ Verzeichnis
Du kannst beliebig viele Mappen erstellen!

Lade im Anschluss alle Dateien aus dem htdocs Verzeichnis auf deinen Webspace!


[Token generieren]
Du musst für jede einzelne Mappe für die du einen Zugriff freigeben möchtest ein Token erstellen!

Öffne dann die index.php mit folgendem Parameter in der Adressleiste:
domainurl/index.php?action=generate&filename=Mappe1
Um ein Token für die Mappe mit dem Dateinamen "Mappe1" zu generieren.
Kopiere nun dein Token in die Zwischenablage und rufe deine Bewerbung auf per
domainurl/index.php?token=TOKEN
TOKEN muss mit deinem token ersetzt werden

[Template Designs]
Du kannst auch Designs und Templates erstellen, schaue dazu ins 
htdocs/templates/ Verzeichnis.
Du musst nur die jeweiligen Platzhalter mit den Dollarzeichen einfügen

[Bewerbungssite auf Webspace installieren]
Um die Bewerbungssite zu installieren muss man sich einen FTP Klienten besorgen,
zB. Filezilla und der Inhalt des htdocs Verzeichnis in ein Unterverzeichnis deines Webspaces uploaden.
Editiere die config.php im htdocs Verzeichnis und setze die $SiteUrl auf die URL zu deinem Webspace.
Wenn du alles hochgeladen hast, musst du nur noch oben rechts die Site URL auf die URL deines Webspaces
setzen um alle Funktionen zu nutzen!


```



