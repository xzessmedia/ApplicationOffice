using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BewerbungsGenerator
{
    public partial class Bewerbungsmappen_Ansicht : Form
    {
        private string BaseSiteURL;
        public Bewerbungsmappen_Ansicht(string SiteURL)
        {
            InitializeComponent();
            webBrowser1.Navigate(SiteURL);
            BaseSiteURL = SiteURL;
        }

        private void OpenToken(string Token)
        {
            webBrowser1.Navigate(BaseSiteURL + "/index.php?token=" + Token);
        }

        private void Bewerbungsmappen_Ansicht_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenToken(textBoxToken.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }
    }
}
