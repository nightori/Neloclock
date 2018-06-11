using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using System.Windows.Forms;
using System.Drawing;

namespace Neloclock
{
    class Settings
    {
        public const int SKIN_COUNT = 9;
        IniData iniData;
        FileIniDataParser iniParser;
        public int d1Skin, d2Skin, d3Skin, d4Skin, delSkin;
        public Point location;
        public bool stayOnTop;

        public Settings()
        {
            iniParser = new FileIniDataParser();
            iniData = iniParser.ReadFile("settings.ini");
            d1Skin = Convert.ToInt32(iniData["skin"]["d1Skin"]);
            d2Skin = Convert.ToInt32(iniData["skin"]["d2Skin"]);
            d3Skin = Convert.ToInt32(iniData["skin"]["d3Skin"]);
            d4Skin = Convert.ToInt32(iniData["skin"]["d4Skin"]);
            delSkin = Convert.ToInt32(iniData["skin"]["delSkin"]);
            int x = Convert.ToInt32(iniData["other"]["Xcoord"]);
            int y = Convert.ToInt32(iniData["other"]["Ycoord"]);
            location = new Point(x, y);
            stayOnTop = Convert.ToBoolean(iniData["other"]["stayOnTop"]);
        }

        public void saveSettings()
        {
            location = MainForm.ActiveForm.Location;
            iniData["skin"]["d1Skin"] = Convert.ToString(d1Skin);
            iniData["skin"]["d2Skin"] = Convert.ToString(d2Skin);
            iniData["skin"]["d3Skin"] = Convert.ToString(d3Skin);
            iniData["skin"]["d4Skin"] = Convert.ToString(d4Skin);
            iniData["skin"]["delSkin"] = Convert.ToString(delSkin);
            iniData["other"]["Xcoord"] = Convert.ToString(location.X);
            iniData["other"]["Ycoord"] = Convert.ToString(location.Y);
            iniData["other"]["stayOnTop"] = Convert.ToString(stayOnTop);
            iniParser.WriteFile("settings.ini",iniData);
        }
    }
}
