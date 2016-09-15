using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BewerbungsGenerator
{
    public partial class Form1 : Form
    {

        public Bewerbungsmappe  ActiveCollection;
        private string          SavedCollectionPath;

        public Form1()
        {
            InitializeComponent();
            ActiveCollection = new Bewerbungsmappe();
        }

       
        /*
        * Speichert die Bewerbungsmappe im JSON Format
        */
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            UpdateData();

            // Alle Date Strings refreshen
            ActiveCollection.CollectionContentData.CVData.UpdateDateStrings();

            string info                     = ActiveCollection.ExportData(saveFileDialog1.FileName);
            SavedCollectionPath             = saveFileDialog1.FileName;
            saveToolStripMenuItem.Enabled   = true;
        }


        /*
        * UpdateData
        * Syncronisiert die Bewerbungsklasse von den Daten des UI
        */
        private void UpdateData()
        {

            // Alle Date Strings refreshen
            ActiveCollection.CollectionContentData.CVData.UpdateDateStrings();

            try
            {
                string gender = "";
                if(radioButtonMale.Checked == true) { gender = "male"; }
                if (radioButtonFemale.Checked == true) { gender = "female"; }
                if (radioButtonNeutral.Checked == true) { gender = "neutral"; }

                string PictureFilename = Path.GetFileName(pictureBox1.ImageLocation);
                ActiveCollection.SetPersonalData(textBoxJobTitle.Text,textFirstname.Text, textLastname.Text, dateBirthdate.Value, PictureFilename , textPhone.Text, textEmail.Text, textStreet.Text, textStreetnumber.Text, Convert.ToInt32(textZipcode.Text), textCity.Text,textBoxContactFirstname.Text,textBoxContactLastname.Text, gender, textBoxFacebook.Text,textBoxTwitter.Text,textBoxXing.Text,textBoxGithub.Text);
                ActiveCollection.SetDesignConfigurationData(comboBoxTheme.Text, comboBoxSiteTemplate.Text, comboBoxMailTemplate.Text, checkBoxUseCDN.Checked);
                ActiveCollection.SetContentData(textEinleitung.Text,richTextBoxReferences.Text, textPersonal.Text);
            } catch
            {

            }
            
        }
        /*
        * UpdateUIFromData
        * Syncronisiert das UI auf die Bewerbungsklasse (Der umgekehrte Prozess zu UpdateData() )
        */
        private void UpdateUIFromData()
        {
            /*
            * Daten aus dem Json Code lesen und auf das UI übertragen
            */
            textFirstname.Text          = ActiveCollection.PersonalCollectionData.Applicant_FirstName;
            textLastname.Text           = ActiveCollection.PersonalCollectionData.Applicant_LastName;
            textStreet.Text             = ActiveCollection.PersonalCollectionData.Applicant_Street;
            textStreetnumber.Text       = ActiveCollection.PersonalCollectionData.Applicant_HouseNumber;
            textCity.Text               = ActiveCollection.PersonalCollectionData.Applicant_City;
            textZipcode.Text            = ActiveCollection.PersonalCollectionData.Applicant_ZipCode.ToString();
            textEmail.Text              = ActiveCollection.PersonalCollectionData.Applicant_MailAdress;
            textPhone.Text              = ActiveCollection.PersonalCollectionData.Applicant_PhoneNumber;
            dateBirthdate.Value         = ActiveCollection.PersonalCollectionData.Applicant_BirthDate;
            textBoxJobTitle.Text        = ActiveCollection.PersonalCollectionData.Applicant_JobTitle;



            /*
            * Avatar Bild laden
            */
            string path = Path.Combine(Application.StartupPath, @"htdocs\images\" + ActiveCollection.PersonalCollectionData.Applicant_Picture);

            if (File.Exists(path) == true)
            {
                pictureBox1.Load(path);
            } else
            {
                // Wenn das angegebene Bild nicht gefunden wurde, das Standard Bild laden
                string newpath = Path.Combine(Application.StartupPath, @"htdocs\images\avatar_empty.jpg");
                if(File.Exists(newpath)==true)
                {
                    pictureBox1.Load(newpath);
                } else
                {
                    // Wenn das Standard Bild nicht gefunden wurde ist die Installation bzw das htdocs Verzeichnis kaputt!
                    MessageBox.Show("Es konnte kein Avatar Bild geladen werden. Dies liegt daran, dass Fehler im htdocs Verzeichnis vorhanden sind und Ressourcen fehlen! Eine Neuinstallation sollte das Problem beheben. Bitte denken Sie daran ein Backup des Data Verzeichnis zu erstellen und vorhandene Bewerbungsmappen zu sichern bevor Sie das Programm neuinstallieren!");
                }
            }
            try
            {
                pictureBox1.Load(path);
            } catch
            {

            }
            
           
            

            // Design
            comboBoxTheme.Text          = ActiveCollection.DesignConfigurationData.Design_Theme;
            comboBoxSiteTemplate.Text   = ActiveCollection.DesignConfigurationData.Design_SiteTemplate;
            comboBoxMailTemplate.Text   = ActiveCollection.DesignConfigurationData.Design_MailTemplate;
            checkBoxUseCDN.Checked      = ActiveCollection.DesignConfigurationData.bUseCDN;

            // Content
            textEinleitung.Text         = ActiveCollection.CollectionContentData.IntroText;
            richTextBoxReferences.Text  = ActiveCollection.CollectionContentData.RefData;
            textPersonal.Text           = ActiveCollection.CollectionContentData.PersonalText;

            // Social Media
            textBoxXing.Text = ActiveCollection.PersonalCollectionData.Applicant_Xing;
            textBoxGithub.Text = ActiveCollection.PersonalCollectionData.Applicant_Github;
            textBoxFacebook.Text = ActiveCollection.PersonalCollectionData.Applicant_Facebook;
            textBoxTwitter.Text = ActiveCollection.PersonalCollectionData.Applicant_Twitter;

            // Contact
            textBoxContactFirstname.Text = ActiveCollection.PersonalCollectionData.Applicant_ContactFirstName;
            textBoxContactLastname.Text = ActiveCollection.PersonalCollectionData.Applicant_ContactLastName;

            switch(ActiveCollection.PersonalCollectionData.Applicant_ContactGender)
                {
                case "male":
                    radioButtonMale.Checked = true;
                    break;
                case "female":
                    radioButtonFemale.Checked = true;
                    break;
                case "neutral":
                    radioButtonNeutral.Checked = true;
                    break;

                 }

            // Listview auf Klasse syncronisieren um den Lebenslauf darzustellen
            UpdateListview();
        }

        /*
        * NewCollection
        * Startet eine neue Bewerbungsmappe und resettet alle nötigen Variablen
        */
        private void NewCollection()
        {
            ActiveCollection    = new Bewerbungsmappe();
            SavedCollectionPath = "";
            // Speichern deaktivieren / muss erst speichern unter
            saveToolStripMenuItem.Enabled = false;

            // Geburtstag manuell resetten
            DateTime now = new DateTime();
            now = DateTime.Now;
            ActiveCollection.PersonalCollectionData.Applicant_BirthDate = now;



            // Bild manuell resetten
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, @"htdocs\images\avatar_empty.jpg");
            ActiveCollection.PersonalCollectionData.Applicant_Picture = path;

            UpdateUIFromData();

            // Listview auf Klasse syncronisieren um den Lebenslauf darzustellen
            UpdateListview();
        }

        /*
        * GetSiteTemplates
        * Parsed das Template Verzeichnis nach vorhandenen Templates und fügt diese in die ComboBox ein
        */
        private void GetSiteTemplates()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, @"htdocs\templates\site\");
                foreach (string s in Directory.GetFiles(path, "*.php").Select(Path.GetFileName)) comboBoxSiteTemplate.Items.Add(s);
            } catch
            {
                MessageBox.Show("Templates nicht gefunden!");
            }
            
        }
        /*
        * GetMailTemplates
        * Parsed das Template Verzeichnis nach vorhandenen Templates und fügt diese in die ComboBox ein
        */
        private void GetMailTemplates()
        {
            string path = Path.Combine(Application.StartupPath, @"htdocs\templates\mail\");
            foreach (string s in Directory.GetFiles(path, "*.php").Select(Path.GetFileName)) comboBoxSiteTemplate.Items.Add(s);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetSiteTemplates();
        }

        private string FormatDate(DateTime date, string format)
        {
            string f = "{0:" + format + "}";
            String.Format(f, date);
            return date.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        /*
        *   Synchronisiert die Listview auf die Lebenslaufklasse in der ActiveCollection
        */
        private void UpdateListview()
        {
            // Als erstes die Listview leeren
            listView1.Items.Clear();
            listView2.Items.Clear();
            // Liste holen und Daten in Listview Format bringen
            List<LebenslaufItem> data_lebenslauf = ActiveCollection.CollectionContentData.CVData.GetList();
            List<AbschlussItem> data_abschluss = ActiveCollection.CollectionContentData.CVData.GetAbschluesseList();
            foreach (LebenslaufItem item in data_lebenslauf)
            {

                // Zeit aus dem Datum entfernen
                string StartDate    = item.dateStartDate.Month + "/" + item.dateStartDate.Year;
                string EndDate      = item.dateEndDate.Month + "/" + item.dateEndDate.Year;

                string[] row = { StartDate + " - " + EndDate, item.EventDescription };
                ListViewItem tmp = new ListViewItem(row);
                listView1.Items.Add(tmp);
                listView1.Update();

                // Veränderte Liste wieder in die Klasse übertragen
                ActiveCollection.CollectionContentData.CVData.LebenslaufItems = data_lebenslauf;
            }
            foreach (AbschlussItem item in data_abschluss)
            {

                // Zeit aus dem Datum entfernen
                string DatePoint = item.dateDatePoint.Year.ToString();

                string[] row = { DatePoint, item.EventDescription };
                ListViewItem tmp = new ListViewItem(row);
                listView2.Items.Add(tmp);
                listView2.Update();

                // Veränderte Liste wieder in die Klasse übertragen
                ActiveCollection.CollectionContentData.CVData.AbschluesseItems = data_abschluss;
            }
        }

        /*
        * Entfernt den ausgewählten Eintrag aus dem Lebenslauf
        */
        private void button4_Click_1(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[0].Index;
            ActiveCollection.CollectionContentData.CVData.RemoveItem(index);
            UpdateListview();

        }
        /*
        * Avatar Bild OpenFileDialog
        */
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.ShowDialog();
        }
        /*
        * Avatar Bild OpenFileDialog
        * Kopiert das gewählte Bild in das korrekte Site Verzeichnis und fügt dieses zur Bewerbungsmappe hinzu
        */
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string targetpath = Path.Combine(Application.StartupPath, @"htdocs\images\",openFileDialog1.SafeFileName);

            try
            {
                File.Copy(openFileDialog1.FileName, targetpath);
            } catch
            {
                MessageBox.Show("Bild konnte nicht ins htdocs/images Verzeichnis kopiert werden!");
            }
            
            // Überprüfen ob Bild vorhanden ist
            if(File.Exists(targetpath) == true)
            {
                pictureBox1.Load(targetpath);
            } else
            {
                MessageBox.Show("Bild konnte nicht geladen werden / wurde nicht gefunden! Bei " + targetpath);
            }
            
            ActiveCollection.PersonalCollectionData.Applicant_Picture = openFileDialog1.SafeFileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SavedCollectionPath != "")
            {
                UpdateData();
                string info = ActiveCollection.ExportData(SavedCollectionPath);
            }
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateData();
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            saveFileDialog1.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = Application.StartupPath;
            openFileDialog2.ShowDialog();
        }

        /*
        *
        * Bewerbungsmappe laden
        *
        */
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            Bewerbungsmappe newCollection = ActiveCollection.ImportData(openFileDialog2.FileName);
            ActiveCollection = newCollection;
            UpdateUIFromData();
            saveToolStripMenuItem.Enabled = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewCollection();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // Screen.ActiveControl.SelText = Clipboard.GetText();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            int index  = listView1.SelectedItems[0].Index; 
            ActiveCollection.CollectionContentData.CVData.RemoveItem(index);
            UpdateListview();
        }

        private void buttonLebenslaufAddItem_Click(object sender, EventArgs e)
        {
            ActiveCollection.CollectionContentData.CVData.AddAbschluss(dateTime.Value, textBoxAbschluss.Text);
            UpdateListview();
            textBoxAbschluss.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActiveCollection.CollectionContentData.CVData.AddItem(dateStart.Value, dateEnd.Value, textCVText.Text);
            UpdateListview();
            textCVText.Clear();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationUploader frm = new ApplicationUploader(textBoxSiteURL.Text);
            frm.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ApplicationUploader frm = new ApplicationUploader(textBoxSiteURL.Text);
            frm.Show();
        }

        private void tokenZuFileGenerierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tokenizer frm = new Tokenizer(textBoxSiteURL.Text);
            frm.Show();
        }

        public string GetSiteURL()
        {
            return textBoxSiteURL.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tokenizer frm = new Tokenizer(textBoxSiteURL.Text);
            frm.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            SiteUpload frm = new SiteUpload();
            frm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (sender as ToolStripItem);
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    MessageBox.Show(owner.SourceControl.Text);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int index = listView2.SelectedItems[0].Index;
            ActiveCollection.CollectionContentData.CVData.RemoveAbschluss(index);
            UpdateListview();
        }

        private void bewerbungAnsehenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bewerbungsmappen_Ansicht frm = new Bewerbungsmappen_Ansicht(textBoxSiteURL.Text);
            frm.Show();
        }
    }
}
