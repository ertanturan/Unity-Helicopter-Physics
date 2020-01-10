using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IndiePixel.UI
{
    public class IP_Header_Manager : MonoBehaviour 
    {
        #region Variables
        [Header("Header Components")]
        public Text m_TitleText;
        public Button m_BackButton;
        public Button m_InfoButton;
        public Button m_CloseButton;
        public Button m_MenuButton;
        public GameObject m_Logo;

        private string currentTitle;
        #endregion

        #region Main Methods
        public void HandleHeader(IP_Base_Screen aScreen)
        {
            if(aScreen)
            {
                //Check to see if we want the Header first
                gameObject.SetActive(aScreen.m_ScreenData.showHeader);
                if(!aScreen.m_ScreenData.showHeader)
                {
                    return;
                }


                //Set the Title
                currentTitle = aScreen.m_ScreenData.screenTitle;
                if(m_TitleText)
                {
                    m_TitleText.text = currentTitle;
                }

                if(m_BackButton)
                {
                    m_BackButton.gameObject.SetActive(aScreen.m_ScreenData.allowBackButton);
                }

                if(m_InfoButton)
                {
                    m_InfoButton.gameObject.SetActive(aScreen.m_ScreenData.showInfoButton);
                }

                if(m_CloseButton)
                {
                    m_CloseButton.gameObject.SetActive(aScreen.m_ScreenData.showCloseButton);
                }

                if(m_MenuButton)
                {
                    m_MenuButton.gameObject.SetActive(aScreen.m_ScreenData.showMenuButton);
                }

                if(m_Logo)
                {
                    m_Logo.gameObject.SetActive(aScreen.m_ScreenData.showLogo);
                }
            }
        }
        #endregion
    }
}
