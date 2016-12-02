using Assets.Interface.Scripts.Game_Menu.Panel;

namespace Assets.Interface.Scripts.Game_Menu.Buttons
{
    public class QuitButton : MenuButton {
        public override void OnInteract()
        {
            base.OnInteract();
            ParentWindow.ShowPanel(typeof(QuitPanel).Name);
        }
    }
}
