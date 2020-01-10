using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndiePixel.Core
{
    [RequireComponent(typeof(Cloth))]
    public class IP_ClothWind : MonoBehaviour 
    {
        #region Variables
        public WindZone m_WindZone;

        private Cloth clothComponent;
        #endregion

        #region Main Methods
    	// Use this for initialization
    	void Start () 
        {
            clothComponent = GetComponent<Cloth>();
    	}
    	
    	// Update is called once per frame
    	void Update () 
        {
            if(m_WindZone && clothComponent)
            {
                var randPos = Mathf.Abs(Mathf.Sin(Time.time * transform.position.x) * Time.deltaTime * 10f);
                clothComponent.externalAcceleration = (m_WindZone.transform.forward * m_WindZone.windPulseMagnitude * m_WindZone.windTurbulence) * (m_WindZone.windMain * Mathf.Abs(Mathf.Sin(Time.time)) * randPos);
            }
    	}
        #endregion
    }
}
