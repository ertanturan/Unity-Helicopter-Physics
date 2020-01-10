using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace IndiePixel.Core
{
    public class IP_CameraSwitcher_Util : MonoBehaviour
    {
        #region Variables
        [Header("Switcher Properties")]
        public float switchTime = 2f;

        private List<Camera> cameras = new List<Camera>();
        private int currentCameraID;
        private float lastSwitchTime = 0f;
        #endregion


        #region BuiltIn Methods
        // Use this for initialization
        void Start()
        {
            cameras = gameObject.GetComponentsInChildren<Camera>().ToList<Camera>();
            currentCameraID = 0;
            SwitchCamera();
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time >= lastSwitchTime + switchTime)
            {
                currentCameraID++;
                if(currentCameraID >= cameras.Count)
                {
                    currentCameraID = 0;
                }

                SwitchCamera();
            }
        }
        #endregion


        #region Custom Methods
        void SwitchCamera()
        {
            for(int i = 0; i < cameras.Count; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
            cameras[currentCameraID].gameObject.SetActive(true);

            lastSwitchTime = Time.time;
        }
        #endregion
    }
}
