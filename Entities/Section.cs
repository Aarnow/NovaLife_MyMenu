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
    /// <summary>
    /// Object section are the representation of your menu in MyMenu
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Name of the source plugin file
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Title in the final menu
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Version of the plugin
        /// </summary>
        [JsonIgnore]
        public string Version { get; set; }

        /// <summary>
        /// Author of the plugin
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Line in the final menu
        /// </summary>
        [JsonIgnore]
        public UITabLine Line { get; set; }

        /// <summary>
        /// Biz Allowed to have access to the menu
        /// </summary>
        public List<int> BizIdAllowed { get; set; }

        /// <summary>
        /// The type of all the job who can open the menu
        /// </summary>
        public List<Activity.Type> BizTypeAllowed { get; set; }

        /// <summary>
        /// Only show the menu to the admin
        /// </summary>
        public bool OnlyAdmin { get; set; }

        /// <summary>
        /// Minimum level admin to have access
        /// </summary>
        public int MinAdminLevel { get; set; }

        /// <summary>
        /// Representation of your section (your plugin) in MyMenu
        /// </summary>
        /// <param name="sourceName">Name of the plugin</param>
        /// <param name="title">Title in the menu</param>
        /// <param name="version">Version of your plugin</param>
        /// <param name="author">Author of the plugin</param>
        /// <param name="bizIdAllowed">Id of the allowed biz</param>
        /// <param name="bizTypeAllowed">Type of biz who can have access</param>
        /// <param name="onlyAdmin">Set this menu only for Admin</param>
        /// <param name="minAdminLevel">Set a mininmum level to have access</param>
        public Section(string sourceName, string title, string version, string author, List<int> bizIdAllowed = null,
            List<Activity.Type> bizTypeAllowed = null, bool onlyAdmin = false, int minAdminLevel = 0)
        {
            SourceName = sourceName;
            Title = title;
            Version = version;
            Author = author;
            BizIdAllowed = bizIdAllowed ?? new List<int>();
            BizTypeAllowed = bizTypeAllowed ?? new List<Activity.Type>();
            OnlyAdmin = onlyAdmin;
            MinAdminLevel = minAdminLevel;
        }

        /// <summary>
        /// Insert your section in the menu
        /// </summary>
        /// <param name="isAdminSection">(Optional) Put your section in Admin part of the menu</param>
        public void Insert(bool isAdminSection = false)
        {
            try
            {
                if (!isAdminSection) Main.menu.Sections.Add(this);
                else Main.menu.AdminSections.Add(this);
            }
            catch (Exception e)
            {
                Debug.Log($"Erreur lors de l'ajout d'une nouvelle section dans MyMenu: {e}");
            }
        }

        /// <summary>
        /// Get the player who have open the menu
        /// </summary>
        /// <param name="ui">UiPanel to track</param>
        /// <returns>Nova-Life player object</returns>
        public Player GetPlayer(UIPanel ui)
        {
            return Nova.server.GetPlayer(ui.playerId);
        }

        /// <summary>
        /// Get Source name of assembly called
        /// </summary>
        /// <returns>Name of the assembly</returns>
        public static string GetSourceName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }

        /// <summary>
        /// Set all the biz id allowed
        /// </summary>
        /// <param name="bizIdAllowed">All biz id allowed to use menu</param>
        public void SetBizIdAllowed(params int[] bizIdAllowed)
        {
            BizIdAllowed.AddRange(bizIdAllowed.Distinct());
        }

        /// <summary>
        /// Set all the biz type allowed to open the menu
        /// </summary>
        /// <param name="bizTypeAllowed">array of biz type allowed</param>
        public void SetBizTypeAllowed(params Activity.Type[] bizTypeAllowed)
        {
            BizTypeAllowed.AddRange(bizTypeAllowed.Distinct());
        }
    }
}