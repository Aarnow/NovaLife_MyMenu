using Life;
using Life.Network;
using Life.UI;
using MyMenu.Entities;
using System.Linq;
using UIPanelManager;

namespace MyMenu.Panels
{
    public class PlayerPanels
    {
        public static void OpenMenuPanel(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"{Main.menu.Title}");

            foreach (Section section in Main.menu.Sections)
            {
                if (section.OnlyAdmin && player.IsAdmin && player.serviceAdmin) panel.AddTabLine(section.Title, section.Line.action);
                else
                {
                    if (section.BizIdAllowed.Count > 0 || section.BizTypeAllowed.Count > 0)
                    {
                        if (player.HasBiz())
                        {
                            if (section.BizIdAllowed.Contains(player.biz.Id) ||
                                section.BizTypeAllowed.Contains(Nova.biz.GetBizActivities(player.biz.Id).FirstOrDefault()))
                            {
                                panel.AddTabLine(section.Title, section.Line.action);
                            }
                        }
                    }
                    else panel.AddTabLine(section.Title, section.Line.action);
                }
            }

            if (player.IsAdmin && player.setup.isAdminService && Main.menu.AdminSections.Count > 0)
            {
                panel.AddTabLine("Admin", ui => PanelManager.NextPanel(player, ui, () => OpenAdminMenuPanel(player)));
            }

            panel.AddButton("Sélectionner", ui => ui.SelectTab());
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }

        public static void OpenAdminMenuPanel(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"{Main.menu.Title}");

            foreach (Section section in Main.menu.AdminSections)
            {
                panel.AddTabLine(section.Title, section.Line.action);
            }

            panel.AddButton("Sélectionner", ui => ui.SelectTab());
            panel.AddButton("Retour", ui => PanelManager.NextPanel(player, ui, () => OpenMenuPanel(player)));
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }
    }
}
