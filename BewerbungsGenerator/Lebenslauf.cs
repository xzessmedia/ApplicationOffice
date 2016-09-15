using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewerbungsGenerator
{
    /*
    * Lebenslauf- und AbschlussItem's
    * Diese Items sind Bestandteil des Lebenslauf's
    * und enthalten das Datum als String zur Darstellung
    * auf der Site als auch die DateTime passend zum DateTimePicker
    */
    public struct LebenslaufItem
    {
        public string strStartDate;
        public DateTime dateStartDate;
        public string strEndDate;
        public DateTime dateEndDate;
        public string   EventDescription;
    };

    public struct AbschlussItem
    {
        public string strDatePoint;
        public DateTime dateDatePoint;
        public string   EventDescription;
    };


    /*
    * Lebenslauf
    * 
    * Im wesentlichen enthält die Klasse 2 Listen, eine für Lebenslauf Einträge
    * und eine weitere für die Schulabschlüsse
    * 
    * @void AddItem             Fügt einen neuen Eintrag zum Lebenslauf hinzu
    * @void AddAbschluss        Fügt einen Schulabschluss hinzu
    * @void RemoveItem          Entfernt einen Eintrag nach Index
    * @void RemoveAbschluss     Entfernt einen Abschluss nach Index
    */
    public class Lebenslauf
    {
        public List<LebenslaufItem> LebenslaufItems;
        public List<AbschlussItem> AbschluesseItems;
        public Lebenslauf()
        {
            LebenslaufItems = new List<LebenslaufItem>();
            AbschluesseItems = new List<AbschlussItem>();
        }


        /*
        * AddItem
        *
        * @Parameter    DateTime    Start       Der zeitliche Beginn der Sache
        * @Parameter    DateTime    End         Das zeitliche Ende der Sache
        * @Parameter    string      Event       Beschreibung der Sache zB. Grundschule besucht
        */
        public void AddItem(DateTime Start, DateTime End, string Event)
        {
            
            LebenslaufItem tmp      = new LebenslaufItem();


            tmp.strStartDate        = Start.ToString("M/yyyy");
            tmp.dateStartDate       = Start;
            tmp.strEndDate          = End.ToString("M/yyyy");
            tmp.dateEndDate         = End;
            tmp.EventDescription    = Event;
            LebenslaufItems.Add(tmp);
        }
        /*
        * AddAbschluss
        *
        * @Parameter    DateTime    datePoint   Der Zeitpunkt des Abschlusses
        * @Parameter    string      Event       Beschreibung zB. Gesellenprüfung
        */
        public void AddAbschluss(DateTime datePoint, string Event)
        {
            

            AbschlussItem tmp       = new AbschlussItem();
            tmp.strDatePoint        = datePoint.ToString("yyyy");
            tmp.dateDatePoint       = datePoint;
            tmp.EventDescription    = Event;
            AbschluesseItems.Add(tmp);
        }
        /*
        * RemoveItem
        *
        * @Parameter    int         index       Der Index des zu entfernenden Eintrag's innerhalb der Liste
        */
        public void RemoveItem(int index)
        {
            LebenslaufItems.RemoveAt(index);
        }
        /*
        * RemoveAbschluss
        *
        * @Parameter    int         index       Der Index des zu entfernenden Eintrag's innerhalb der Liste
        */
        public void RemoveAbschluss(int index)
        {
            AbschluesseItems.RemoveAt(index);
        }
        /*
        * GetAbschluesseList
        *
        * @return       List<AbschlussItem>      Gibt die Abschluss Liste zurück welche alle Einträge enthält
        */
        public List<AbschlussItem> GetAbschluesseList() 
        {
            return AbschluesseItems;
        }
        /*
        * GetList
        *
        * @return       List<LebenslaufItem>      Gibt die Lebenslauf Liste zurück welche alle Einträge enthält
        */
        public List<LebenslaufItem> GetList()
        {
            return LebenslaufItems;
        }


        /*
        * UpdateDateStrings()
        * Schreibt nochmal alle Date Strings in die Json
        * (Sollte vorm exportieren passieren)
        */
        public void UpdateDateStrings()
        {
            for (int i = 0; i < LebenslaufItems.Count(); i++)
            {

                LebenslaufItem tmp = LebenslaufItems[i];

                tmp.strStartDate    = tmp.dateStartDate.ToString("M/yyyy");
                tmp.strEndDate      = tmp.dateEndDate.ToString("M/yyyy");
                LebenslaufItems[i] = tmp;
            }
            for (int i = 0; i < AbschluesseItems.Count(); i++)
            {

                AbschlussItem tmp = AbschluesseItems[i];

                tmp.strDatePoint = tmp.dateDatePoint.ToString("yyyy");
                AbschluesseItems[i] = tmp;
            }

        }

    };
}
