using UnityEngine;

namespace HelicopterPhysics.Physics
{
    public class Forces : HelicopterBaseRigidbodyController
    {
        #region Variables
        public float maxSpeed = 1f;
        public Vector3 movementDirection = new Vector3(0f, 0f, 1f);
        #endregion


        #region Custom Methods
        protected override void HandlePhysics()
        {
            Rb.AddForce(movementDirection * maxSpeed);
        }
        #endregion
    }
}

