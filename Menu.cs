using Life.Network;
using Life.UI;
using UIPanelManager;

namespace MyMenu
{
    public class Menu
    {
        private string Version = "0.1.0";
        private static UIPanel _Panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab);
        public static UIPanel Panel
        {
            get { return _Panel; }
            private set { _Panel = value; }
        }

        public Menu()
        {
            Panel.SetTitle($"MyMenu {Version}");
        }

        public void OpenMyMenu(Player player)
        {
            Panel.AddButton("Sélectionner", ui => ui.SelectTab());
            Panel.AddButton("Fermer", ui => PanelManager.Quit(ui, player));

            player.ShowPanelUI(Panel);
        }
    }
}
