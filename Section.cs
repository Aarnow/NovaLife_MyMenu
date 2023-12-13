using System;
using Life.UI;
using UnityEngine;
using Life.Network;
using Life;
using System.Reflection;
using System.Collections.Generic;
using Life.BizSystem;

namespace MyMenu
{
    public class Section
    {
        public string SourceName { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }
        public UITabLine Line { get; set; }
        public List<int> BizIdAllowed { get; set; }
        public List<Activity.Type> BizTypeAllowed { get; set; }
        public bool OnlyAdmin { get; set; }
        public int MinAdminLevel { get; set; }

        public Section(string title, string version, string author)
        {
            Title = title;
            Version = version;
            Author = author;
            BizIdAllowed = new List<int>();
            BizTypeAllowed = new List<Activity.Type>();
            OnlyAdmin = false;
            MinAdminLevel = 0;
        }

        public void Insert()
        {
            try
            {
                Main.menu.Sections.Add(this);
            }
            catch (Exception e)
            {
                Debug.Log($"Erreur lors de l'ajout d'une nouvelle section dans MyMenu: {e}");
            }
        }


        public Player GetPlayer(UIPanel ui)
        {
            return Nova.server.GetPlayer(ui.playerId);
        }

        public static string GetSourceName()
        { 
            return Assembly.GetCallingAssembly().GetName().Name;
        }
    }
}
