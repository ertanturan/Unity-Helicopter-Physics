using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace IndiePixel.Core
{
    public class IP_Loader_Manager : MonoBehaviour 
    {
        #region Variables
        [Header("UI Elements")]
        public Image m_LoadingIcon;
        public Image m_LoadingDoneIcon;
        public Text m_LoadingText;
        public Image m_ProgressBar;
        public Image m_FadeOverlay;

        [Header("Timing Properties")]
        public float m_WaitOnLoadEnd = 0.25f;
        public float m_FadeDuration = 0.25f;

        [Header("Loading Properties")]
        public LoadSceneMode m_LoadSceneMode = LoadSceneMode.Single;
        public ThreadPriority m_LoadThreadPriority;

        [Header("Misc Properties")]
        public AudioListener m_AudioListener;


        private AsyncOperation operation;
        Scene currentScene;

        public static int sceneToLoad = -1;
        static int loadingSceneIndex = 1;
        #endregion

        #region Main Methods
        public static void LoadScene(int levelNum)
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            sceneToLoad = levelNum;
            SceneManager.LoadScene(loadingSceneIndex);
        }


    	// Use this for initialization
    	void Start () 
        {
            if(sceneToLoad < 0)
            {
                return;
            }

            if(m_FadeOverlay)
            {
                m_FadeOverlay.gameObject.SetActive(true);
            }
            StartCoroutine(LoadAsync(sceneToLoad));
    	}

        private IEnumerator LoadAsync(int LevelNum)
        {
            ShowLoadingVisuals();

            yield return null;

            FadeIn();
            StartOperation(LevelNum);

            float lastProgress = 0f;

            while(DoneLoading() == false)
            {
                yield return null;
                if(Mathf.Approximately(operation.progress, lastProgress) == false)
                {
                    if(m_ProgressBar)
                    {
                        m_ProgressBar.fillAmount = Mathf.Lerp(m_ProgressBar.fillAmount, operation.progress, Time.deltaTime * 0.5f);
                    }
                    lastProgress = operation.progress;
                }
            }

            if(m_LoadSceneMode == LoadSceneMode.Additive)
            {
                if(m_AudioListener)
                {
                    m_AudioListener.enabled = false;
                }
            }

            ShowCompletionVisuals();

            yield return new WaitForSeconds(m_WaitOnLoadEnd);

            FadeOut();

            yield return new WaitForSeconds(m_FadeDuration);

            if(m_LoadSceneMode == LoadSceneMode.Additive)
            {
                SceneManager.UnloadSceneAsync(currentScene.name);
            }
            else
            {
                operation.allowSceneActivation = true;
            }
        }

        private void StartOperation(int levelNum)
        {
            Application.backgroundLoadingPriority = m_LoadThreadPriority;
            operation = SceneManager.LoadSceneAsync(levelNum, m_LoadSceneMode);

            if(m_LoadSceneMode == LoadSceneMode.Single)
            {
                operation.allowSceneActivation = false;
            }
        }

        private bool DoneLoading()
        {
            return (m_LoadSceneMode == LoadSceneMode.Additive && operation.isDone) || (m_LoadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
        }
        #endregion



        #region Utility Methods
        void FadeIn()
        {
            if(m_FadeOverlay)
            {
                m_FadeOverlay.CrossFadeAlpha(0, m_FadeDuration, true);
            }
        }

        void FadeOut()
        {
            if(m_FadeOverlay)
            {
                m_FadeOverlay.CrossFadeAlpha(1, m_FadeDuration, true);
            }
        }

        void ShowLoadingVisuals()
        {
            if(m_LoadingIcon && m_LoadingDoneIcon)
            {
                m_LoadingIcon.gameObject.SetActive(true);
                m_LoadingDoneIcon.gameObject.SetActive(true);
            }

            if(m_ProgressBar && m_LoadingText)
            {
                m_ProgressBar.fillAmount = 0f;
                m_LoadingText.text = "loading...";
            }
        }

        void ShowCompletionVisuals()
        {
            if(m_LoadingIcon && m_LoadingDoneIcon)
            {
                m_LoadingIcon.gameObject.SetActive(false);
                m_LoadingDoneIcon.gameObject.SetActive(true);
            }

            if(m_ProgressBar && m_LoadingText)
            {
                m_ProgressBar.fillAmount = 1f;
                m_LoadingText.text = "Loading Complete";
            }
        }
        #endregion

    }
}
