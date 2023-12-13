using Life;
using Life.Network;
using Life.UI;
using System.Collections.Generic;
using System.Linq;
using UIPanelManager;

namespace MyMenu
{
    public class Menu
    {
        private string Version = "0.1.0";

        public List<Section> Sections = new List<Section>();

        public void OpenMyMenu(Player player)
        {
            UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu {Version}");

            foreach (Section section in Sections)
            {
                if (section.OnlyAdmin && player.IsAdmin && player.serviceAdmin) panel.AddTabLine(section.Line.name, section.Line.action);             
                else
                {
                    if (section.BizIdAllowed.Count > 0 || section.BizTypeAllowed.Count > 0)
                    {
                        if (player.HasBiz())
                        {
                            if (section.BizIdAllowed.Contains(player.biz.Id) || 
                                section.BizTypeAllowed.Contains(Nova.biz.GetBizActivities(player.biz.Id).FirstOrDefault()))
                            {
                                panel.AddTabLine(section.Line.name, section.Line.action);
                            }                   
                        }
                    }
                    else panel.AddTabLine(section.Line.name, section.Line.action);
                }
            }

            panel.AddButton("Sélectionner", ui => ui.SelectTab());
            panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(panel);
        }
    }
}
