using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 169
#pragma warning disable 649
namespace Assets.Interface.Scripts.Base
{
    [RequireComponent(typeof(Image))]
    public class MenuPanel : MenuWidget
    {
        [SerializeField]
        private bool hideOnStart = true;

        private Text title;
        private Button closeButton;

        protected override void Awake()
        {
            base.Awake();
            InitTitle();
            InitCloseButton();
        }

        protected virtual void Start()
        {
            gameObject.SetActive(!hideOnStart);
        }

        private void ClosePanel()
        {
            ParentWindow.ClosePanel(name);
        }

        private void InitTitle()
        {
            var child = transform.Find("Header").Find("Title");
            title = child.GetComponent<Text>();
        }

        private void InitCloseButton()
        {
            var child = transform.Find("Header").Find("CloseButton");
            closeButton = child.GetComponent<Button>();
            Button.ButtonClickedEvent buttonClickedEvent = closeButton.onClick;
            buttonClickedEvent.AddListener(ClosePanel);
        }
    }
}
