using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliMainRotor : MonoBehaviour, IHeliRotor
    {

        public void UpdateRotor()
        {
            Debug.Log("Updating main rotor");
        }
    }
}