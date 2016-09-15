using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BewerbungsGenerator
{
    public partial class SiteUpload : Form
    {

        private ftp ftpClient;
        public SiteUpload()
        {
            InitializeComponent();
            
        }

        private void InitFTP(string hostip, string username, string password)
        {
            ftpClient = new ftp(hostip, username, password);

           string start = Path.Combine(Application.StartupPath, @"htdocs");
            recursiveDirectory(start, textBoxInstallDirectory.Text);
        }
       
        private void SiteUpload_Load(object sender, EventArgs e)
        {

        }

        private void recursiveDirectory(string dirPath, string uploadPath)
        {
            string[] files = Directory.GetFiles(dirPath, "*.*");
            string[] subDirs = Directory.GetDirectories(dirPath);

            foreach (string file in files)
            {
                ftpClient.upload(uploadPath + "/" + Path.GetFileName(file), file);
            }

            foreach (string subDir in subDirs)
            {
                ftpClient.createDirectory(uploadPath + "/" + Path.GetFileName(subDir));
                recursiveDirectory(subDir, uploadPath + "/" + Path.GetFileName(subDir));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitFTP(textBoxHostAdress.Text, textBoxUsername.Text, textBoxPassword.Text);
        }
    }
}
