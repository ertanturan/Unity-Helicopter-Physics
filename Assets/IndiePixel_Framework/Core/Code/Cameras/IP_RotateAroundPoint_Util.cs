using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel
{
    public class IP_RotateAroundPoint_Util : MonoBehaviour 
    {
        #region Variables
        [Header("Utility Properties")]
        public Transform targetTransform;
        [Range(0f, 360f)]
        public float startAngle = 45f;
        public float rotationSpeed = 2f;
        public float height = 2f;
        public float distance = 2f;

        private Vector3 wantedPosition;
        private Vector3 finalPosition;

        private float wantedAngle;
        private float finalAngle;
        #endregion


        #region Main Methods
    	// Use this for initialization
    	void Start () 
        {
            wantedAngle = startAngle;
            finalAngle = wantedAngle;
    	}
    	
    	// Update is called once per frame
    	void Update () 
        {
            if(targetTransform)
            {
                wantedPosition = targetTransform.position + (Vector3.forward * distance);
                wantedAngle += rotationSpeed * Time.deltaTime;
                finalAngle = Mathf.Lerp(finalAngle, wantedAngle, Time.deltaTime * 2f);

                wantedPosition = Quaternion.Euler(0f, finalAngle, 0f) * wantedPosition + (Vector3.up * height);
                Debug.DrawLine(targetTransform.position, wantedPosition, Color.red);

                transform.position = wantedPosition;
                transform.LookAt(targetTransform);
            }
    	}
        #endregion
    }
}
