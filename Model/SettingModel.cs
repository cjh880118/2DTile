using System;
using UnityEngine;
using System.Collections.Generic;
using JHchoi.Constants;

namespace JHchoi.Models
{
    public class SettingModel : Model
    {
        GameModel _owner;
        public LocalizingType LocalizingType = LocalizingType.KR;

        public string _portName = "";
        public int _portRate = 0;

        public void Setup(GameModel owner)
        {
            _owner = owner;
            LoadSettingFile();
        }

        void LoadSettingFile()
        {
            string line;
            string pathBasic = Application.dataPath + "/StreamingAssets/";
            string path = "Setting/Setting.txt";
            using (System.IO.StreamReader file = new System.IO.StreamReader(@pathBasic + path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(";") || string.IsNullOrEmpty(line))
                        continue;

                    if (line.StartsWith("Localizing"))
                        LocalizingType = (LocalizingType)int.Parse(line.Split('=')[1]);
                    else if (line.StartsWith("PortName"))
                        _portName = line.Split('=')[1];
                    else if (line.StartsWith("BaudRate"))
                        _portRate = int.Parse(line.Split('=')[1]);
                }

                file.Close();
                line = string.Empty;
            }
        }

        public string GetLocalizingPath()
        {
            return LocalizingType.ToString();
        }
    }
}
