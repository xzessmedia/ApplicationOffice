using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace BewerbungsGenerator
{
    class Bewerbungsmappe
    {
        public PersonalData            PersonalCollectionData;
        public DesignConfiguration     DesignConfigurationData;
        public ContentData             CollectionContentData;

        Bewerbungsmappe()
        {
            PersonalCollectionData  = new PersonalData();
            DesignConfigurationData = new DesignConfiguration();
            CollectionContentData   = new ContentData();
        }

        void SetPersonalData(string FirstName, string LastName, DateTime BirthDate, Image Picture, string PhoneNumber, string MailAdress, string StreetAdress, string HouseNumber, int ZipCode, string City)
        {
            PersonalData tmp            = new PersonalData();
            tmp.Applicant_FirstName     = FirstName;
            tmp.Applicant_LastName      = LastName;
            tmp.Applicant_BirthDate     = BirthDate;
            tmp.Applicant_Picture       = Picture;
            tmp.Applicant_PhoneNumber   = PhoneNumber;
            tmp.Applicant_MailAdress    = MailAdress;
            tmp.Applicant_Street        = StreetAdress;
            tmp.Applicant_HouseNumber   = HouseNumber;
            tmp.Applicant_ZipCode       = ZipCode;
            tmp.Applicant_City          = City;

            PersonalCollectionData = tmp;
        }
    }
}
