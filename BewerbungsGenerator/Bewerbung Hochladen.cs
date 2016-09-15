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
    public partial class ApplicationUploader : Form
    {
        private string SiteURL;
        public ApplicationUploader(string SiteUrl)
        {
            InitializeComponent();
            SiteURL = SiteUrl;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text + "index.php?action=upload");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text + "index.php?action=upload");
        }

        private void ApplicationUploader_Load(object sender, EventArgs e)
        {
            textBox1.Text = SiteURL;
        }
    }
}
