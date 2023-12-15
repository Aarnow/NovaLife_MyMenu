using Life;
using Life.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UIPanelManager;
using UnityEngine;

namespace MyMenu.Entities
{
    public class Menu
    {
        public string Version { get; set; } = "1.1.0";
        public string Title { get; set; } = "MyMenu";
        public KeyCode Key { get; set; } = KeyCode.P;
        public List<Section> Sections { get; set; } = new List<Section>();
        [JsonIgnore]
        public List<Section> AdminSections { get; set; } = new List<Section>();

        public Menu()
        {                  
        }

        public static string CreateLabel(string text)
        {
            return $"<color={PanelManager.Colors[NotificationManager.Type.Warning]}>{text}:</color>";
        }

        public void Save()
        {
            string updatedJson = JsonConvert.SerializeObject(Main.menu, Formatting.Indented);
            string jsonFile = Directory.GetFiles(Main.directoryPath, Main.filename).FirstOrDefault();
            File.WriteAllText(jsonFile, updatedJson);
        }
    }
}
