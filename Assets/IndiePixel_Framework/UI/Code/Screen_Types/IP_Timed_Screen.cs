using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IndiePixel.UI
{
    public class IP_Timed_Screen : IP_Base_Screen 
    {
        #region Variables
        [Header("Timed Screen Properties")]
        public float m_ScreenTime = 2f;
        public UnityEvent OnScreenTimeComplete = new UnityEvent();

        protected float m_StartTime;
        #endregion

        #region Methods
        public override void Start()
        {
            base.Start();
            m_StartTime = Time.time;
        }

        void Update()
        {
            if(Time.time > m_StartTime + m_ScreenTime)
            {
                if(OnScreenTimeComplete != null)
                {
                    OnScreenTimeComplete.Invoke();
                }
            }
        }
        #endregion
    }
}
