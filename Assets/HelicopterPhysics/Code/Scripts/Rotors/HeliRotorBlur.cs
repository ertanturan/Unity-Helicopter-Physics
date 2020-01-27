using HelicopterPhysics.Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace HelicopterPhysics.Mechanics.Rotors
{

    public class HeliRotorBlur : MonoBehaviour, IHeliRotor
    {

        [Header("Rotor Blur Properties")]
        public GameObject BlurGeo;

        public float MaxDPS = 5400f;

        public Material BlurMat;
        [Space]
        public List<GameObject> Blades = new List<GameObject>();
        public List<Texture2D> BlurTextures = new List<Texture2D>();

        private void Awake()
        {
            BlurGeo.GetComponent<Renderer>().material = BlurMat;
        }

        public void UpdateRotor(float dps, InputController inputController)
        {
            //Debug.Log(dps);
            float normalizedDPS = Mathf.InverseLerp(0f, MaxDPS, dps);
            int blurTexID = Mathf.FloorToInt(normalizedDPS * BlurTextures.Count);
            blurTexID = Mathf.Clamp(blurTexID, 0, BlurTextures.Count - 1);

            if (BlurMat && BlurTextures.Count > 0)
            {
                BlurMat.SetTexture("_MainTex", BlurTextures[blurTexID]);
            }

            if (blurTexID > 1 && Blades.Count > 0)
            {
                HandleGeo(false);
            }
            else
            {
                HandleGeo(true);
            }

        }


        private void HandleGeo(bool activeness)
        {
            foreach (GameObject blade in Blades)
            {
                blade.SetActive(activeness);
            }
        }
    }
}
