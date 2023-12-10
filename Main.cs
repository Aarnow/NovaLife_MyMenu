using Life;
using Life.Network;
using Life.UI;
using UnityEngine;

namespace MyMenu
{
    public class Main : Plugin
    {
        private string version = "0.1.0";
        public static UIPanel panel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle("MyMenu");
        public Main(IGameAPI api):base(api)
        {
        }

        public override void OnPluginInit()
        {
            base.OnPluginInit();
            Debug.Log($"Plugin \"MyMenu\" initialisé avec succès.");
        }

        public override void OnPlayerInput(Player player, KeyCode keyCode, bool onUI)
        {
            base.OnPlayerInput(player, keyCode, onUI);

            if(keyCode == KeyCode.Y)
            {
                UIPanel playerPanel = new UIPanel("MyMenu", UIPanel.PanelType.Tab).SetTitle($"MyMenu v{version}");

                foreach (UITabLine line in panel.lines)
                {
                    playerPanel.AddTabLine(line.name, line.action);
                }
                
                playerPanel.AddButton("Sélectionner", ui =>
                {
                    ui.lines[ui.selectedTab].action.Invoke(ui);
                });
                playerPanel.AddButton("Fermer", ui => player.ClosePanel(playerPanel));
                player.ShowPanelUI(playerPanel);
            }
        }
    }
}