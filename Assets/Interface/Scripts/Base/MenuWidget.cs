using UnityEngine;

namespace Assets.Interface.Scripts.Base
{
    public class MenuWidget : MonoBehaviour
    {
        protected MenuWindow ParentWindow;

        protected virtual void Awake()
        {
            ParentWindow = GetComponentInParent<MenuWindow>();
        }
    }
}
