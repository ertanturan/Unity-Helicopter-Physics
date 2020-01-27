using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliTailRotor : MonoBehaviour, IHeliRotor
    {
        public void UpdateRotor()
        {
            Debug.Log("Updating tail rotor..");
        }
    }
}