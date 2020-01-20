using UnityEngine;

namespace HelicopterPhysics.Physics
{
    public class Torque : HelicopterBaseRigidbodyController
    {
        #region Varaibles
        public float TorqueSpeed = 2f;
        public Vector3 RotationDirection = new Vector3(0f, 1f, 0f);
        #endregion


        #region Custom Methods
        protected override void HandlePhysics()
        {
            Vector3 wantedTorque = Vector3.up * TorqueSpeed;
            Rb.AddTorque(wantedTorque);
        }
        #endregion
    }
}
