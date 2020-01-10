using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IndiePixel
{
    [System.Serializable]
    public class IP_Action
    {
        #region Variables
        public string m_ActionName = "New Action";
        public float m_WaitTime = 0f;
        public bool collapsed = false;
        public UnityEvent OnStarted = new UnityEvent();
        public UnityEvent OnCompleted = new UnityEvent();

        private float startTime;
        #endregion


        #region Main Methods
        public virtual void StartAction()
        {
            Debug.Log("Starting Action: " + m_ActionName);
            startTime = Time.time;
            if(OnStarted != null)
            {
                OnStarted.Invoke();
            }
        }

        public virtual void UpdateAction()
        {
            if(m_WaitTime > 0f)
            {
                if(Time.time > startTime + m_WaitTime)
                {
                    CompleteAction();
                }
            }
            else
            {
                CompleteAction();
            }
        }

        public virtual void CompleteAction()
        {
            Debug.Log("Completing Action: " + m_ActionName);
            if(OnCompleted != null)
            {
                OnCompleted.Invoke();
            }
        }
        #endregion
    }
}
