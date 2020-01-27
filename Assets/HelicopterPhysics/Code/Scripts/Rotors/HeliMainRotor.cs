using HelicopterPhysics.Inputs;
using UnityEngine;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliMainRotor : BaseRotor
    {
        public Transform LeftRotor;
        public Transform RightRotor;
        public float MaxPitch = 35f;

        public override void UpdateRotor(float dps, InputController inputController)
        {
            //base.UpdateRotor(dps, inputController);

            transform.Rotate(Vector3.up, dps);

            // pitch the blades up and down

            if (LeftRotor && RightRotor)
            {
                LeftRotor.localRotation = Quaternion.Euler(
                    inputController.CurrentInput.CollectiveInput * MaxPitch, 0f, 0f);
                RightRotor.localRotation = Quaternion.Euler(
                    -inputController.CurrentInput.CollectiveInput * MaxPitch, 0f, 0f);


            }

        }
    }
}