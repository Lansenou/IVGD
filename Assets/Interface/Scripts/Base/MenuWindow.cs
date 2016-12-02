using System;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Interface.Scripts.Base
{
    [RequireComponent(typeof(RectTransform))]
    public class MenuWindow : MonoBehaviour
    {
        protected GameObject[] Children;

        protected virtual void Awake()
        {
            GetChildren();
        }

        public void ShowPanel(string panel)
        {
            Debug.Log(name + " showing panel: " + panel);
            ShowChildren(false, panel);
        }

        public void ClosePanel(string panel)
        {
            Debug.Log(name + " closing panel: " + panel);
            ShowChildren(true, panel);
        }

        protected void ShowChildren(bool show, string exclude)
        {
            foreach (GameObject v in Children)
            {
                if (v.name == exclude)
                {
                    v.SetActive(!show);
                }
                else
                {
                    // If this object is a MenuPanel skip it
                    if (v.GetComponent<MenuPanel>() == null)
                        v.SetActive(show);
                }
            }
        }

        private void GetChildren()
        {
            var rectTransforms = UnityUtils.GetComponentsInDirectChildrenExcludeParent<RectTransform>(gameObject);
            Children = Array.ConvertAll(rectTransforms, input => input.gameObject);
        }
    }
}