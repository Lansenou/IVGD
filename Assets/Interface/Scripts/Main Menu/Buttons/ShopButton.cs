using Assets.Interface.Scripts.Main_Menu.Panel;

namespace Assets.Interface.Scripts.Main_Menu.Buttons
{
    public class ShopButton : MenuButton {

        public override void OnInteract()
        {
            base.OnInteract();
            ParentWindow.ShowPanel(typeof(ShopPanel).Name);
        }
    }
}
