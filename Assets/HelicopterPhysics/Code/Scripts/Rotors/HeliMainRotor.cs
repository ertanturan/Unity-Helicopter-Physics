using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliMainRotor : MonoBehaviour, IHeliRotor
    {

        public void UpdateRotor(float dps)
        {
            transform.Rotate(Vector3.up, dps);
        }
    }
}