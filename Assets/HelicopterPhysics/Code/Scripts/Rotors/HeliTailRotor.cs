using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliTailRotor : MonoBehaviour, IHeliRotor
    {
        public float rotationSpeedModifier = 1.5f;
        public void UpdateRotor(float dps)
        {
            transform.Rotate(Vector3.right, dps * rotationSpeedModifier);
        }
    }
}