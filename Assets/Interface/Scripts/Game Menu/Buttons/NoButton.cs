﻿using Assets.Interface.Scripts.Game_Menu.Panel;

namespace Assets.Interface.Scripts.Game_Menu.Buttons
{
    public class NoButton : MenuButton {
        public override void OnInteract()
        {
            base.OnInteract();
            ParentWindow.ClosePanel(typeof(QuitPanel).Name);
        }
    }
}