using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace IndiePixel.UI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class IP_Base_Screen : MonoBehaviour 
    {
        #region Variables
        [Header("Main Properties")]
        public bool m_IsActive;
        public bool m_SetStartButton = false;
        public Selectable m_StartSelectable;

        [Header("Transition Properties")]
        public AnimationCurve m_ShowCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
        public AnimationCurve m_HideCurve = AnimationCurve.EaseInOut(1f,1f,0f,0f);

        [Header("Screen Data")]
        public IP_Screen_Data m_ScreenData;

        [Header("Events")]
        public UnityEvent OnScreenStart = new UnityEvent();
        public UnityEvent OnScreenClose = new UnityEvent();

        protected Animator animator;
        protected EventSystem eSystem;
        protected CanvasGroup cGroup;
        #endregion

        #region Methods
    	// Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
            eSystem = EventSystem.current;
            cGroup = GetComponent<CanvasGroup>();
        }

    	public virtual void Start () 
        {
            SetSelectable(m_StartSelectable);
    	}
        #endregion


        #region Utility Methods
        public void StartScreen()
        {
            HandleAnimator("show");

            if(OnScreenStart != null)
            {
                OnScreenStart.Invoke();
            }
        }

        public void CloseScreen()
        {
            HandleAnimator("hide");

            if(OnScreenClose != null)
            {
                OnScreenClose.Invoke();
            }
        }

        protected virtual void HandleAnimator(string aTriggerName)
        {
            if(animator && !string.IsNullOrEmpty(aTriggerName) && animator.runtimeAnimatorController)
            {
                animator.SetTrigger(aTriggerName);
            }
        }

        public virtual void SetSelectable(Selectable aSelectable)
        {
            if(aSelectable && eSystem)
            {
                eSystem.SetSelectedGameObject(aSelectable.gameObject);
            }
        }

        //TODO: finish this
        public virtual void HandleTransition(bool active)
        {
            
        }
        #endregion
    }

    [System.Serializable]
    public struct IP_Screen_Data
    {
        public string screenTitle;
        public bool allowBackButton;
        public bool showMenuButton;
        public bool showHeader;
        public bool showInfoButton;
        public bool showLogo;
        public bool showCloseButton;
        public bool showBG;
    }
}
