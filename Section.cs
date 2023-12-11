using System;
using Life.UI;
using UnityEngine;
using Life.Network;
using Life;

namespace MyMenu
{
    public class Section
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public Action<UIPanel> Action { get; set; }

        public Section(string name, string version, string author)
        {
            Name = name;
            Version = version;
            Author = author;
        }

        public void Insert()
        {
            try
            {
                Menu.Panel.AddTabLine(Name, Action);
                Debug.Log($"Ajout d'un tabline");
                Debug.Log($"Action debug" + Action);
            }
            catch (Exception e)
            {
                Debug.Log($"Erreur lors d'un ajout d'une nouvelle ligne dans MyMenu: {e}");
            }
        }


        public Player GetPlayer(UIPanel ui)
        {
            return Nova.server.GetPlayer(ui.playerId);
        }
    }
}
