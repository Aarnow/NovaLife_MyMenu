using Life;
using Life.Network;
using System;
using System.IO;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using MyMenu.Panels;
using MyMenu.Entities;

namespace MyMenu
{
    public class Main : Plugin
    {
        public static string filename = "Config.json";
        public static string directoryPath;
        public static Menu menu = new Menu();
        public Main(IGameAPI api):base(api)
        {
        }

        public override void OnPluginInit()
        {
            base.OnPluginInit();
            InitDirectory();

            try
            {
                string jsonFile = Directory.GetFiles(directoryPath, filename).FirstOrDefault();
                if (jsonFile != null)
                {
                    string json = File.ReadAllText(jsonFile);
                    Menu menuSetup = JsonConvert.DeserializeObject<Menu>(json);

                    menu.Title = menuSetup.Title;
                    menu.Key = menuSetup.Key;

                    foreach (Section section in menu.Sections)
                    {
                        Section currSection = menuSetup.Sections.FirstOrDefault(s => s.SourceName == section.SourceName);

                        if (currSection != null)
                        {
                            section.Title = currSection.Title;
                            section.BizIdAllowed = currSection.BizIdAllowed;
                            section.BizTypeAllowed = currSection.BizTypeAllowed;
                            section.OnlyAdmin = currSection.OnlyAdmin;
                            section.MinAdminLevel = currSection.MinAdminLevel;
                        }
                        else
                        {
                            menuSetup.Sections.Add(section);
                            string updatedJson = JsonConvert.SerializeObject(menuSetup, Formatting.Indented);
                            File.WriteAllText(jsonFile, updatedJson);
                        }
                    }
                }
                else
                {
                    string filePath = Path.Combine(directoryPath, filename);
                    string json = JsonConvert.SerializeObject(menu, Formatting.Indented);
                    File.WriteAllText(filePath, json);
                }                          
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la lecture des fichiers JSON : " + ex.Message);
            }

            new SChatCommand("/mymenu", "Permet d'ouvrir le panel du plugin MyMenu", "/mymenu", (player, arg) =>
            {
                if(player.IsAdmin) AdminPanels.OpenConfigPanel(player);
            }).Register();

            Debug.Log($"Plugin \"MyMenu\" initialisé avec succès.");
        }

        public override void OnPlayerInput(Player player, KeyCode keyCode, bool onUI)
        {
            base.OnPlayerInput(player, keyCode, onUI);
            if(keyCode == menu.Key && !onUI) PlayerPanels.OpenMenuPanel(player);        
        }

        public void InitDirectory()
        {
            directoryPath = pluginsPath + "/MyMenu";
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        }
    }
}