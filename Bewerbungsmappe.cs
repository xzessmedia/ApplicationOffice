/*
* Copyright © 2016 Tim Koepsel
* <www.xzessmedia.de>
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BewerbungsGenerator
{
    /* 
    * Bewerbungsmappe
    * Dies ist die Hauptklasse, welche alle Daten beinhaltet
    * Diese Klasse wird per Json exportiert
    * und im Site Framework per Php importiert
    */
    public class Bewerbungsmappe
    {
        public PersonalData            PersonalCollectionData;
        public DesignConfiguration     DesignConfigurationData;
        public ContentData             CollectionContentData;

        /*
        * Constructor der Klasse
        *
        */
        public Bewerbungsmappe()
        {
            PersonalCollectionData  = new PersonalData();
            DesignConfigurationData = new DesignConfiguration();
            CollectionContentData   = new ContentData();
        }

        /*
        * SetPersonalData
        * Diese Funktion fügt die persönlichen Daten hinzu und überschreibt ggf. vorhandene Werte!
        */
        public void SetPersonalData(string JobTitle, string FirstName, string LastName, DateTime BirthDate, string Picture, string PhoneNumber, string MailAdress, string StreetAdress, string HouseNumber, int ZipCode, string City, string contactFirstName, string contactLastName, string contactGender, string Facebook, string Twitter, string Xing, string Github)
        {
            PersonalData tmp                = new PersonalData();
            tmp.Applicant_FirstName         = FirstName;
            tmp.Applicant_LastName          = LastName;
            tmp.Applicant_BirthDate         = BirthDate;
            tmp.Applicant_Picture           = Picture;
            tmp.Applicant_PhoneNumber       = PhoneNumber;
            tmp.Applicant_MailAdress        = MailAdress;
            tmp.Applicant_Street            = StreetAdress;
            tmp.Applicant_HouseNumber       = HouseNumber;
            tmp.Applicant_ZipCode           = ZipCode;
            tmp.Applicant_City              = City;
            tmp.Applicant_ContactFirstName  = contactFirstName;
            tmp.Applicant_ContactLastName   = contactLastName;
            tmp.Applicant_ContactGender     = contactGender;
            tmp.Applicant_Facebook          = Facebook;
            tmp.Applicant_Twitter           = Twitter;
            tmp.Applicant_Xing              = Xing;
            tmp.Applicant_Github            = Github;
            tmp.Applicant_JobTitle          = JobTitle;
            PersonalCollectionData          = tmp;
        }

        /*
        * SetDesignConfigurationData
        * Diese Funktion fügt die Design Daten hinzu und überschreibt ggf. vorhandene Werte!
        */
        public void SetDesignConfigurationData(string Theme, string SiteTemplate, string MailTemplate, bool useCDN)
        {
            DesignConfigurationData.bUseCDN = useCDN;
            DesignConfigurationData.Design_Theme = Theme;
            DesignConfigurationData.Design_SiteTemplate = SiteTemplate;
            DesignConfigurationData.Design_MailTemplate = MailTemplate;
        }
        /*
        * SetContentData
        * Diese Funktion fügt die Design Daten hinzu und überschreibt ggf. vorhandene Werte!
        */
        public void SetContentData(string IntroText, string Portfolio, string PersonalText)
        {
            CollectionContentData.IntroText         = IntroText;
            CollectionContentData.RefData           = Portfolio;
            CollectionContentData.PersonalText      = PersonalText;

        }
       /*
       * ExportData
       * Serialisiert und exportiert alle Daten der Bewerbungsmappe im Json Format
       * @parameter string ExportPath   Der lokale Pfad wo die Bewerbungsmappe gespeichert werden soll (inkl. Filename)
       * @return    string json         Enthält den JSON String der alle Daten beinhaltet
       */
        public string ExportData(string ExportPath)
        {
            // Klasse serialisieren
            var json = new JavaScriptSerializer().Serialize(this);

            // Datei erstellen und json Inhalt abspeichern
            var writer = new StreamWriter(ExportPath);
            writer.Write(json);
            writer.Close();
            return json;
        }
       /*
       * ImportData
       * Deserialisiert und importiert alle Daten der Bewerbungsmappe im Json Format zurück in die Klasse
       *
       * @parameter string          ImportPath   Der lokale Pfad wo die Bewerbungsmappe geöffnet werden soll (inkl. Filename)
       * @return    Bewerbungsmappe              Gibt die Klasse Bewerbungsmappe zurück (Diese Klasse)
       */
        public Bewerbungsmappe ImportData(string ImportPath)
        {
            var reader = new StreamReader(ImportPath);
            string filedata = reader.ReadToEnd();
            reader.Close();

            Bewerbungsmappe data = new JavaScriptSerializer().Deserialize<Bewerbungsmappe>(filedata);
            return data;
        }
    }
}
