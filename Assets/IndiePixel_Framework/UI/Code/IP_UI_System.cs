using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IndiePixel.Core;
using UnityEngine.Events;

namespace IndiePixel.UI
{
    public class SwitchedScreenEvent : UnityEvent<IP_Screen_Data>{}

    [RequireComponent(typeof(AudioSource))]
    public class IP_UI_System : MonoBehaviour 
    {
        #region Variables
        [Header("Main Properties")]
        public Image m_FadeOverlay;
        public GameObject m_BGImage;
        public GameObject m_PopupGrp;
        public float m_FadeInDuration = 1f;
        public float m_FadeOutDuration = 1f;

        [Header("Screens")]
        public IP_Base_Screen m_StartScreen;

        [Header("Events")]
        public SwitchedScreenEvent OnSwitchedScreen = new SwitchedScreenEvent();

        public IP_Base_Screen m_CurrentScreen { get; private set; }
        private IP_Base_Screen m_PreviousScreen;
        private IP_Screen_Data currentScreenData;

        private Component[] foundScreens = new Component[0];
        #endregion

        #region Main Methods
    	// Use this for initialization
    	void Start () 
        {
            GetAllScreens();

            if(m_StartScreen)
            {
                SwitchScreens(m_StartScreen);
            }

            InitFader();
            FadeIn();
    	}

        public void SwitchScreens(IP_Base_Screen aScreen)
        {
            if(aScreen)
            {
                //Close the current Screen
                if(m_CurrentScreen)
                {
                    m_CurrentScreen.CloseScreen();
                    m_PreviousScreen = m_CurrentScreen;
                }

                //Start the Next Screen
                m_CurrentScreen = aScreen;
                aScreen.gameObject.SetActive(true);
                m_CurrentScreen.StartScreen();
                currentScreenData = m_CurrentScreen.m_ScreenData;


                //Fire the Switched Screen Event
                if(OnSwitchedScreen != null)
                {
                    OnSwitchedScreen.Invoke(currentScreenData);
                }

                HandleBGImage();
            }
        }
        #endregion

        #region Utility Methods
        void InitFader()
        {
            if(m_FadeOverlay)
            {
                m_FadeOverlay.gameObject.SetActive(true);    
            }
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(WaitToLoadScene(sceneIndex));
        }

        IEnumerator WaitToLoadScene(int sceneIndex)
        {
            FadeOut();

            yield return new WaitForSeconds(m_FadeOutDuration);

            if(IP_Game_Manager.Instance != null)
            {
                IP_Game_Manager.Instance.LoadScene(sceneIndex);
            }
        }

        void FadeIn()
        {
            if(m_FadeOverlay)
            {
                m_FadeOverlay.gameObject.SetActive(true);
                m_FadeOverlay.CrossFadeAlpha(0, m_FadeInDuration, false);
            }
        }

        void FadeOut()
        {
            if(m_FadeOverlay)
            {
                m_FadeOverlay.CrossFadeAlpha(1, m_FadeInDuration, false);
            }
        }

        public void GoToPreviousScreen()
        {
            if(m_PreviousScreen)
            {
                SwitchScreens(m_PreviousScreen);
            }
        }

        void HandleBGImage()
        {
            if(m_BGImage)
            {
                m_BGImage.SetActive(currentScreenData.showBG);
            }
        }

        void GetAllScreens()
        {
            foundScreens = GetComponentsInChildren<IP_Base_Screen>(true);
            foreach(IP_Base_Screen screen in foundScreens)
            {
                screen.gameObject.SetActive(true);
            }

            if(m_PopupGrp)
            {
                m_PopupGrp.SetActive(true);
            }
        }
        #endregion
    }
}
