using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BewerbungsGenerator
{

    /*
    * DesignConfiguration
    * Diese Variablen beziehen sich auf die Design Einstellung der Site
    * @string Design_Theme          Enthält den Namen des zu verwendenden Bootstrap Theme
    * @string Design_SiteTemplate   Enthält das HTML Template für die Bewerbungssite (zu finden unter /htdocs/templates)
    * @string Design_MailTemplate   Enthält das HTML Template für die zu versendenden E-Mails
    * @bool   bUseCDN               Dieser Flag gibt an ob CDN Provider genutzt werden sollen oder lokale Ressourcen verwendet werden
    */
    public class DesignConfiguration
    {
        public string  Design_Theme;
        public string  Design_SiteTemplate;
        public string  Design_MailTemplate;
        public bool    bUseCDN;
    }
}
