using Assets.Interface.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Interface.Scripts
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MenuWidget
    {
        protected Button Button;

        protected override void Awake()
        {
            base.Awake();
            Button = GetComponent<Button>();
            SetButtonCallback();
        }

        public virtual void OnInteract()
        {
            Debug.Log(name + " onInteract() called");
        }

        private void SetButtonCallback()
        {
            Button.ButtonClickedEvent clickedEvent = Button.onClick;
            clickedEvent.AddListener(OnInteract);
        }
    }
}
