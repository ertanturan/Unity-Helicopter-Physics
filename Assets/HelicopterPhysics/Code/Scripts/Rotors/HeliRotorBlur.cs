using HelicopterPhysics.Inputs;
using UnityEngine;

namespace HelicopterPhysics.Mechanics.Rotors
{

    public class HeliRotorBlur : MonoBehaviour, IHeliRotor
    {

        [Header("Rotor Blur Properties")]
        public GameObject LeftRotor;
        public GameObject RightRotor;
        public GameObject BlurGeo;


        public void UpdateRotor(float dps, InputController inputController)
        {
            Debug.Log("Rotor blur .");
        }

    }
}
