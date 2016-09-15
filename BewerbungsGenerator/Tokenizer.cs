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
    public partial class Tokenizer : Form
    {
        private string SiteURL;
        public Tokenizer(string SiteUrl)
        {
            InitializeComponent();
            SiteURL = SiteUrl;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(SiteURL + "index.php?action=generate&filename=" + textBox1.Text);
        }

        private void Tokenizer_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(SiteURL + "index.php?action=generate&filename=" + textBox1.Text);
        }
    }
}
