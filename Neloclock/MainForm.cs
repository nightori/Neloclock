using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;

namespace Neloclock
{
    public partial class MainForm : Form
    {
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        Settings settings;
        List<Image[]> pictures;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            settings = new Settings();
            pictures = loadImages();
            MainForm.ActiveForm.Location = settings.location;
            MainForm.ActiveForm.TopMost = settings.stayOnTop;
            stayOnTopToolStripMenuItem1.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItem2.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItem3.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItem4.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItemDel.Checked = settings.stayOnTop;
            var skins1 = skinToolStripMenuItem1.DropDownItems;
            var skins2 = skinToolStripMenuItem2.DropDownItems;
            var skins3 = skinToolStripMenuItem3.DropDownItems;
            var skins4 = skinToolStripMenuItem4.DropDownItems;
            var skinsDel = skinToolStripMenuItemDel.DropDownItems;
            ((ToolStripMenuItem)skins1[settings.d1Skin]).Checked = true;
            ((ToolStripMenuItem)skins2[settings.d2Skin]).Checked = true;
            ((ToolStripMenuItem)skins3[settings.d3Skin]).Checked = true;
            ((ToolStripMenuItem)skins4[settings.d4Skin]).Checked = true;
            ((ToolStripMenuItem)skinsDel[settings.delSkin]).Checked = true;
        }

        private void moveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private List<Image[]> loadImages()
        {
            List<Image[]> list = new List<Image[]>();
            for (int i = 0; i < Settings.SKIN_COUNT; i++)
            {
                Image[] images = new Image[11];
                for (int j = 0; j < 10; j++)
                {
                    String path = "skins\\" + i + "\\" + j + ".png";
                    images[j] = Image.FromFile(path);
                }
                images[10] = Image.FromFile("skins\\" + i + "\\d.png");
                list.Add(images);
            }
            return list;
        }

        private void timerUpdater_Tick(object sender, EventArgs e)
        {
            String time = DateTime.Now.ToString("HHmm");
            int d1 = (int)Char.GetNumericValue(time[0]);
            int d2 = (int)Char.GetNumericValue(time[1]);
            int d3 = (int)Char.GetNumericValue(time[2]);
            int d4 = (int)Char.GetNumericValue(time[3]);            
            pDigit1.Image = pictures[settings.d1Skin][d1];
            pDigit2.Image = pictures[settings.d2Skin][d2];
            pDigit3.Image = pictures[settings.d3Skin][d3];
            pDigit4.Image = pictures[settings.d4Skin][d4];
            pDelimiter.Image = pictures[settings.delSkin][10];
        }

        private void skin_Click(object sender, EventArgs e)
        {
            var skin = sender as ToolStripMenuItem;
            var skins = ((ToolStripMenuItem)skin.OwnerItem).DropDownItems;
            for (int i = 0; i < Settings.SKIN_COUNT; i++)
            {
                ((ToolStripMenuItem)skins[i]).Checked = false;
            }
            skin.Checked = true;
            int skinID = skins.IndexOf(skin);
            switch (skin.Name[skin.Name.Length - 1])
            {
                case '1': settings.d1Skin = skinID; break;
                case '2': settings.d2Skin = skinID; break;
                case '3': settings.d3Skin = skinID; break;
                case '4': settings.d4Skin = skinID; break;
                default : settings.delSkin = skinID; break;
            }
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clicked = sender as ToolStripMenuItem;
            settings.stayOnTop = clicked.Checked;
            stayOnTopToolStripMenuItem1.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItem2.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItem3.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItem4.Checked = settings.stayOnTop;
            stayOnTopToolStripMenuItemDel.Checked = settings.stayOnTop;
            MainForm.ActiveForm.TopMost = settings.stayOnTop;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.ActiveForm.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            settings.saveSettings();
        }
    }
}
