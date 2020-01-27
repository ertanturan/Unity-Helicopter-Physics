using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliTailRotor : MonoBehaviour, IHeliRotor
    {
        public void UpdateRotor(float dps)
        {
            Debug.Log("Updating tail rotor..");
        }
    }
}