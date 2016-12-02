using UnityEngine.SceneManagement;

namespace Assets.Interface.Scripts.Main_Menu.Buttons
{
    public class PlayButton : MenuButton {
        public override void OnInteract()
        {
            base.OnInteract();
            SceneManager.LoadScene("Demolition");
        }
    }
}
