using HelicopterPhysics.Inputs;
using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliTailRotor : BaseRotor
    {
        public float rotationSpeedModifier = 1.5f;
        public override void UpdateRotor(float dps, InputController inputController)
        {
            //base.UpdateRotor(dps, inputController);
            transform.Rotate(Vector3.right, dps * rotationSpeedModifier);


            if (LeftRotor && RightRotor)
            {
                LeftRotor.localRotation = Quaternion.Euler(0f,
                    inputController.CurrentInput.PedalInput * MaxPitch, 0f);
                RightRotor.localRotation = Quaternion.Euler(0f,
                    inputController.CurrentInput.PedalInput * MaxPitch, 0f);

            }
        }
    }
}