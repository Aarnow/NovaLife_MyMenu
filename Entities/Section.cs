using System;
using Life.UI;
using UnityEngine;
using Life.Network;
using Life;
using System.Reflection;
using System.Collections.Generic;
using Life.BizSystem;
using Newtonsoft.Json;
using System.Linq;

namespace MyMenu.Entities
{
    public class Section
    {
        public string SourceName { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string Version { get; set; }
        public string Author { get; set; }
        [JsonIgnore]
        public UITabLine Line { get; set; }
        public List<int> BizIdAllowed { get; set; }
        public List<Activity.Type> BizTypeAllowed { get; set; }
        public bool OnlyAdmin { get; set; }
        public int MinAdminLevel { get; set; }

        public Section(string sourceName, string title, string version, string author, List<int> bizIdAllowed = null, List<Activity.Type> bizTypeAllowed = null, bool onlyAdmin = false, int minAdminLevel = 0)
        {
            SourceName = sourceName;
            Title = title;
            Version = version;
            Author = author;
            BizIdAllowed = bizIdAllowed == null ? new List<int>() : bizIdAllowed;
            BizTypeAllowed = bizTypeAllowed == null ? new List<Activity.Type>() : bizTypeAllowed;
            OnlyAdmin = onlyAdmin;
            MinAdminLevel = minAdminLevel;
        }

        public void Insert(bool isAdminSection)
        {
            try
            {
                if(!isAdminSection) Main.menu.Sections.Add(this);
                else Main.menu.AdminSections.Add(this);
            }
            catch (Exception e)
            {
                Debug.Log($"Erreur lors de l'ajout d'une nouvelle section dans MyMenu: {e}");
            }
        }

        public void Insert()
        {
            Insert(false);
        }

        public Player GetPlayer(UIPanel ui)
        {
            return Nova.server.GetPlayer(ui.playerId);
        }

        public static string GetSourceName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }

        public void SetBizIdAllowed(params int[] bizIdAllowed)
        {
            BizIdAllowed.AddRange(bizIdAllowed.Distinct());
        }

        public void SetBizTypeAllowed(params Activity.Type[] bizTypeAllowed)
        {
            BizTypeAllowed.AddRange(bizTypeAllowed.Distinct());
        }
    }
}
