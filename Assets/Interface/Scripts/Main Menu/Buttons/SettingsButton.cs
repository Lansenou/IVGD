using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Interface.Scripts.Main_Menu.Buttons
{
    public class SettingsButton : MenuButton
    {
        private bool collapsed = true;

        private Button[] childButtons;

        protected override void Awake()
        {
            base.Awake();
            childButtons = UnityUtils.GetComponentsInDirectChildrenExcludeParent<Button>(gameObject);
        }

        protected void Start()
        {
            ShowChildren(!collapsed);
        }

        public override void OnInteract()
        {
            base.OnInteract();
            ShowChildren(collapsed);
            collapsed = !collapsed;
        }

        private void ShowChildren(bool show)
        {
            foreach (Button b in childButtons)
            {
                Debug.Log(name + (show ? " showing: " : " hiding: ") + b.gameObject.name);
                b.gameObject.SetActive(show);
            }
        }
    }
}