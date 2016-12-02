using UnityEngine.SceneManagement;

namespace Assets.Interface.Scripts.Game_Menu.Buttons
{
    public class YesButton : MenuButton {
        public override void OnInteract()
        {
            base.OnInteract();
            SceneManager.LoadScene(0);
        }
    }
}
