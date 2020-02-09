using UnityEngine;

namespace HelicopterPhysics.Cameras
{
    public class BaseHeliCamera : MonoBehaviour, IHeliCamera
    {
        [Header("Base Camera Properties")]
        public Rigidbody Rb;
        public Transform LookatTarget;
        protected Vector3 CurrentVelocity = Vector3.zero;
        protected Vector3 TargetPos;

        public virtual void HandleCamera()
        {

        }

        protected virtual void FixedUpdate()
        {
            if (Rb)
            {
                HandleCamera();
            }
        }

    }


}

