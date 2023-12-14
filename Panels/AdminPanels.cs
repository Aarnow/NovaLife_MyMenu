using Life.BizSystem;
using Life.DB;
using Life;
using System;
using System.Linq;
using UIPanelManager;
using Life.UI;
using Life.Network;
using UnityEngine;
using MyMenu.Entities;

namespace MyMenu.Panels
{
    abstract class AdminPanels
    {
        public static void OpenConfigPanel(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu {Main.menu.Version}");

            panel.AddTabLine($"{Menu.CreateLabel("Nom du menu")} {Main.menu.Title}", ui => PanelManager.NextPanel(player, ui, () => SetTitleMenu(player)));
            panel.AddTabLine($"{Menu.CreateLabel("Touche d'ouverture")} {Main.menu.Key}", ui => PanelManager.NextPanel(player, ui, () => SetKeyMenu(player)));
            panel.AddTabLine($"{Menu.CreateLabel("Plugins reliés")} {(Main.menu.Sections.Count == 0 ? "Aucun" : $"{Main.menu.Sections.Count}")}", ui => PanelManager.NextPanel(player, ui, () => OpenPluginList(player)));

            panel.AddButton("Sélectionner", ui => ui.SelectTab());
            //panel.AddButton("Discord", ui => Application.OpenURL("http://www.google.com"));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void SetTitleMenu(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Input).SetTitle($"MyMenu {Main.menu.Version}");

            panel.inputPlaceholder = "Quel titre voulez-vous pour votre menu ?";

            panel.AddButton("Valider", ui =>
            {
                if (ui.inputText.Length > 0)
                {
                    Main.menu.Title = ui.inputText;
                    Main.menu.Save();
                    PanelManager.NextPanel(player, ui, () => OpenConfigPanel(player));
                }
                else PanelManager.Notification(player, "Erreur", "Vous devez indiquer un titre.", NotificationManager.Type.Error);
            });
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigPanel(player)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void SetKeyMenu(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Input).SetTitle($"MyMenu {Main.menu.Version}");

            panel.inputPlaceholder = "Indiquer une lettre pour ouvrir votre menu";

            panel.AddButton("Valider", ui =>
            {
                if (ui.inputText.Length == 1)
                {
                    if (ui.inputText.All(char.IsLetter))
                    {
                        if (Enum.TryParse(ui.inputText.ToUpper(), out KeyCode key))
                        {
                            Main.menu.Key = key;
                            Main.menu.Save();
                            PanelManager.NextPanel(player, ui, () => OpenConfigPanel(player));
                        }
                        else PanelManager.Notification(player, "Erreur", "Vous ne pouvez pas utiliser cette touche.", NotificationManager.Type.Error);
                    }
                    else PanelManager.Notification(player, "Erreur", "Vous ne pouvez configurer qu'une lettre.", NotificationManager.Type.Error);
                }
                else PanelManager.Notification(player, "Erreur", "Vous devez indiquer une lettre.", NotificationManager.Type.Error);
            });
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigPanel(player)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void OpenPluginList(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu {Main.menu.Version}");

            foreach (Section section in Main.menu.Sections)
            {
                panel.AddTabLine($"{section.SourceName} <i>({section.Title})</i>", ui => PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section)));
            }

            panel.AddButton("Configurer", ui => ui.SelectTab());
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigPanel(player)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void OpenConfigSection(Player player, Section section)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu {Main.menu.Version}");

            panel.AddTabLine($"{Menu.CreateLabel("Titre")} {section.Title}", ui => PanelManager.NextPanel(player, ui, () => SetTitleSection(player, section)));
            panel.AddTabLine($"{Menu.CreateLabel("Sociétés autorisés")} {(section.BizIdAllowed.Count == 0 ? "Désactivé" : $"{section.BizIdAllowed.Count}")}", ui => PanelManager.NextPanel(player, ui, () => SetBizIdAllowed(player, section)));
            panel.AddTabLine($"{Menu.CreateLabel("Types autorisés")} {(section.BizTypeAllowed.Count == 0 ? "Désactivé" : $"{section.BizTypeAllowed.Count}")}", ui => PanelManager.NextPanel(player, ui, () => SetBizTypeAllowed(player, section)));
            panel.AddTabLine($"{Menu.CreateLabel("Admin uniquement")} {(section.OnlyAdmin ? $"<color={PanelManager.Colors[NotificationManager.Type.Success]}>Oui</color>" : $"<color={PanelManager.Colors[NotificationManager.Type.Error]}>Non</color>")}", ui =>
            {
                section.OnlyAdmin = !section.OnlyAdmin;
                Main.menu.Save();
                PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section));
            });
            panel.AddTabLine($"{Menu.CreateLabel("Adminlevel minimum")} {section.MinAdminLevel}", ui => PanelManager.NextPanel(player, ui, () => SetMinAdminLevel(player, section)));
            panel.AddTabLine($"<color={PanelManager.Colors[NotificationManager.Type.Info]}>{section.SourceName} ({section.Version}) - {section.Author}</color>", ui => PanelManager.Notification(player, "Information", "Vous ne devez pas modifier cette valeur.", NotificationManager.Type.Warning));

            panel.AddButton("Configurer", ui => ui.SelectTab());
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenPluginList(player)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void SetBizTypeAllowed(Player player, Section section)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu {Main.menu.Version}");

            foreach (Activity.Type type in Enum.GetValues(typeof(Activity.Type)))
            {
                panel.AddTabLine($"<color={(section.BizTypeAllowed.Contains(type) ? $"{PanelManager.Colors[NotificationManager.Type.Success]}" : $"{PanelManager.Colors[NotificationManager.Type.Error]}")}>{type}</color>", ui =>
                {
                    if (section.BizTypeAllowed.Contains(type)) section.BizTypeAllowed.Remove(type);
                    else section.BizTypeAllowed.Add(type);
                });
            }

            panel.AddButton("Ajouter/Retirer", ui =>
            {
                ui.SelectTab();
                Main.menu.Save();
                PanelManager.NextPanel(player, ui, () => SetBizTypeAllowed(player, section));
            });
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void SetBizIdAllowed(Player player, Section section)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu {Main.menu.Version}");

            foreach (Bizs biz in Nova.biz.bizs)
            {
                panel.AddTabLine($"<color={(section.BizIdAllowed.Contains(biz.Id) ? $"{PanelManager.Colors[NotificationManager.Type.Success]}" : $"{PanelManager.Colors[NotificationManager.Type.Error]}")}>{biz.BizName}</color>", ui =>
                {
                    if (section.BizIdAllowed.Contains(biz.Id)) section.BizIdAllowed.Remove(biz.Id);
                    else section.BizIdAllowed.Add(biz.Id);
                });
            }

            panel.AddButton("Ajouter/Retirer", ui =>
            {
                ui.SelectTab();
                Main.menu.Save();
                PanelManager.NextPanel(player, ui, () => SetBizIdAllowed(player, section));
            });
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void SetMinAdminLevel(Player player, Section section)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Input).SetTitle($"MyMenu {Main.menu.Version}");

            panel.inputPlaceholder = "Level admin minimum pour accéder à cette section ?";

            panel.AddButton("Valider", ui =>
            {
                if (int.TryParse(ui.inputText, out int level) && level <= 5 && level >= 0)
                {
                    section.MinAdminLevel = level;
                    Main.menu.Save();
                    PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section));
                }
                else PanelManager.Notification(player, "Erreur", "Vous devez indiquer une valeur (0 - 5)", NotificationManager.Type.Error);
            });
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void SetTitleSection(Player player, Section section)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Input).SetTitle($"MyMenu {Main.menu.Version}");

            panel.inputPlaceholder = "Quel titre voulez-vous pour cette section ?";

            panel.AddButton("Valider", ui =>
            {
                if (ui.inputText.Length > 0)
                {
                    section.Title = ui.inputText;
                    Main.menu.Save();
                    PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section));
                }
                else PanelManager.Notification(player, "Erreur", "Vous devez indiquer un titre.", NotificationManager.Type.Error);
            });
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenConfigSection(player, section)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }
    }
}
