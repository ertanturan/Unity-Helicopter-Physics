using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel.UI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class IP_Menu_Manager : MonoBehaviour 
    {
        #region Variables
        public RectTransform m_ButtonRect;
        private RectTransform rectTrans;
        public bool isOpen = false;
        private CanvasGroup cGroup;
        private Animator animator;
        #endregion

        #region Main Methods
        void Start()
        {
            cGroup = GetComponent<CanvasGroup>();
            animator = GetComponent<Animator>();
            rectTrans = GetComponent<RectTransform>();

            isOpen = false;
        }

        void Update()
        {
            if(rectTrans && m_ButtonRect && Input.GetMouseButtonDown(0))
            {
                if(!RectTransformUtility.RectangleContainsScreenPoint(rectTrans, Input.mousePosition) &&
                    !RectTransformUtility.RectangleContainsScreenPoint(m_ButtonRect, Input.mousePosition) && isOpen)
                {
                    ToggleMenu();
                }
            }
        }

        public void ToggleMenu()
        {
            if(animator.runtimeAnimatorController)
            {
                if(isOpen)
                {
                    animator.SetTrigger("hide");
                    isOpen = false;
                }
                else
                {
                    animator.SetTrigger("show");
                    isOpen = true;
                }
            }
        }
        #endregion
    }
}
