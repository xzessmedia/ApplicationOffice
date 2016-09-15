using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewerbungsGenerator
{
    /*
    * ContentData
    * Enthält alle Inhalte wie Lebenslauf, Referenzen, persönlicher Text, Anschreiben
    * @string       IntroText               Das Anschreiben an die Kontakt Person (HTML)
    * @Lebenslauf   CVData                  Der erstellte Lebenslauf
    * @string       RefData                 Die eingescannten Dokumente wie Zeugnisse und Portfolio (HTML)
    * @string       PersonalText            Der persönliche Text über sich selber als Abschluss Text
    */
    public class ContentData
    {
        public string      IntroText;
        public Lebenslauf  CVData;
        public string      RefData;
        public string      PersonalText;

        public ContentData()
        {
            CVData  = new Lebenslauf();
        }
    }

}
