using Life;
using Life.Network;
using UnityEngine;

namespace MyMenu
{
    public class Main : Plugin
    {
        public static Menu menu = new Menu();
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

            if(keyCode == KeyCode.P)
            {
                if(!onUI) menu.OpenMyMenu(player);
            }
        }
    }
}