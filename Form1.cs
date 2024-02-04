using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace HW_2_SWE_316
{
    public partial class Form1 : Form
    {
        String selectedFolder;
        Folder rootFolder;
        
        public Form1()
        {
            InitializeComponent();
            DrawingPanel.Paint += panel1_Paint;
            this.panel1.MouseWheel += panel1_MouseWheel;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = folderDialog.SelectedPath;
                rootFolder = new Folder();
                rootFolder.Name = Path.GetFileName(selectedFolder);
                TraverseFolder(selectedFolder, rootFolder);

            }

            DrawingPanel.Controls.Clear();
            DrawingPanel.Invalidate();
            DrawingPanel.HorizontalScroll.Maximum = 0;
            DrawingPanel.AutoScroll = true;
            

        }


        private void TraverseFolder(string folderPath, Folder parentFolder)
        {
            parentFolder.Name = Path.GetFileName(folderPath);

            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo != null)
                {
                    parentFolder.AddSubcomponent(new File
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Length,
                        Extension = fileInfo.Extension
                    });
                }
                else
                {
                    Console.WriteLine($"Skipping file: {filePath}");
                }
            }

            foreach (var subFolderPath in Directory.GetDirectories(folderPath))
            {
                Folder subfolder = new Folder();
                TraverseSubfolder(subFolderPath, subfolder, parentFolder);
            }
        }

        private void TraverseSubfolder(string folderPath, Folder subfolder, Folder parentFolder)
        {
            subfolder.Name = Path.GetFileName(folderPath);

            foreach (var filePath in Directory.GetFiles(folderPath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                subfolder.AddSubcomponent(new File
                {
                    Name = fileInfo.Name,
                    Size = fileInfo.Length,
                    Extension = fileInfo.Extension
                });
            }

            foreach (var subFolderPath in Directory.GetDirectories(folderPath))
            {
                Folder childSubfolder = new Folder();
                TraverseSubfolder(subFolderPath, childSubfolder, subfolder);
            }

            parentFolder.AddSubcomponent(subfolder);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = DrawingPanel.CreateGraphics();
            Brush txtBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            Font font = new Font("Arial", 10);    
            Point point = new Point(400, 50);

            g.DrawString(rootFolder.DisplayContentHorozantally(), Font, txtBrush, point, sf);

            txtBrush.Dispose(); 
            g.Dispose(); 
            font.Dispose(); 

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Graphics g = DrawingPanel.CreateGraphics();
            Brush txtBrush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            Font font = new Font("Arial", 10); 

            Point point = new Point(100, 200);
            
            g.DrawString(rootFolder.DisplayContentVirtically(), Font, txtBrush, point, sf);

            txtBrush.Dispose(); 
            g.Dispose(); 
            font.Dispose();

        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        { 

            if (e.Delta > 0)
            {
                panel1.Width = panel1.Width + 50;
                panel1.Height = panel1.Height + 50;

            }
            else
            {
                panel1.Width = panel1.Width - 50;
                panel1.Height = panel1.Height - 50;
            }
        
        }
            private void panel1_Paint(object sender, PaintEventArgs e)
            {

            }
    }
}
