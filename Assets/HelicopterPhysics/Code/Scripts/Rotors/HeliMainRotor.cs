using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliMainRotor : MonoBehaviour, IHeliRotor
    {

        public void UpdateRotor(float dps)
        {
            Debug.Log("Updating main rotor");
            transform.rotation = Quaternion.Euler(0f, dps, 0f);
            //transform.Rotate(Vector3.up, dps);
        }
    }
}